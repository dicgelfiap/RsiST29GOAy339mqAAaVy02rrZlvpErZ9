using System;
using System.Collections;
using Org.BouncyCastle.Math.EC.Endo;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Math.Field;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x0200056E RID: 1390
	public abstract class ECCurve
	{
		// Token: 0x06002B36 RID: 11062 RVA: 0x000E6628 File Offset: 0x000E6628
		public static int[] GetAllCoordinateSystems()
		{
			return new int[]
			{
				0,
				1,
				2,
				3,
				4,
				5,
				6,
				7
			};
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x000E663C File Offset: 0x000E663C
		protected ECCurve(IFiniteField field)
		{
			this.m_field = field;
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06002B38 RID: 11064
		public abstract int FieldSize { get; }

		// Token: 0x06002B39 RID: 11065
		public abstract ECFieldElement FromBigInteger(BigInteger x);

		// Token: 0x06002B3A RID: 11066
		public abstract bool IsValidFieldElement(BigInteger x);

		// Token: 0x06002B3B RID: 11067 RVA: 0x000E6660 File Offset: 0x000E6660
		public virtual ECCurve.Config Configure()
		{
			return new ECCurve.Config(this, this.m_coord, this.m_endomorphism, this.m_multiplier);
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x000E667C File Offset: 0x000E667C
		public virtual ECPoint ValidatePoint(BigInteger x, BigInteger y)
		{
			ECPoint ecpoint = this.CreatePoint(x, y);
			if (!ecpoint.IsValid())
			{
				throw new ArgumentException("Invalid point coordinates");
			}
			return ecpoint;
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x000E66B0 File Offset: 0x000E66B0
		[Obsolete("Per-point compression property will be removed")]
		public virtual ECPoint ValidatePoint(BigInteger x, BigInteger y, bool withCompression)
		{
			ECPoint ecpoint = this.CreatePoint(x, y, withCompression);
			if (!ecpoint.IsValid())
			{
				throw new ArgumentException("Invalid point coordinates");
			}
			return ecpoint;
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x000E66E4 File Offset: 0x000E66E4
		public virtual ECPoint CreatePoint(BigInteger x, BigInteger y)
		{
			return this.CreatePoint(x, y, false);
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x000E66F0 File Offset: 0x000E66F0
		[Obsolete("Per-point compression property will be removed")]
		public virtual ECPoint CreatePoint(BigInteger x, BigInteger y, bool withCompression)
		{
			return this.CreateRawPoint(this.FromBigInteger(x), this.FromBigInteger(y), withCompression);
		}

		// Token: 0x06002B40 RID: 11072
		protected abstract ECCurve CloneCurve();

		// Token: 0x06002B41 RID: 11073
		protected internal abstract ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression);

		// Token: 0x06002B42 RID: 11074
		protected internal abstract ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression);

		// Token: 0x06002B43 RID: 11075 RVA: 0x000E6718 File Offset: 0x000E6718
		protected virtual ECMultiplier CreateDefaultMultiplier()
		{
			GlvEndomorphism glvEndomorphism = this.m_endomorphism as GlvEndomorphism;
			if (glvEndomorphism != null)
			{
				return new GlvMultiplier(this, glvEndomorphism);
			}
			return new WNafL2RMultiplier();
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x000E6748 File Offset: 0x000E6748
		public virtual bool SupportsCoordinateSystem(int coord)
		{
			return coord == 0;
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x000E6750 File Offset: 0x000E6750
		public virtual PreCompInfo GetPreCompInfo(ECPoint point, string name)
		{
			this.CheckPoint(point);
			IDictionary preCompTable;
			lock (point)
			{
				preCompTable = point.m_preCompTable;
			}
			if (preCompTable == null)
			{
				return null;
			}
			PreCompInfo result;
			lock (preCompTable)
			{
				result = (PreCompInfo)preCompTable[name];
			}
			return result;
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x000E67C8 File Offset: 0x000E67C8
		public virtual PreCompInfo Precompute(ECPoint point, string name, IPreCompCallback callback)
		{
			this.CheckPoint(point);
			IDictionary dictionary;
			lock (point)
			{
				dictionary = point.m_preCompTable;
				if (dictionary == null)
				{
					dictionary = (point.m_preCompTable = Platform.CreateHashtable(4));
				}
			}
			PreCompInfo result;
			lock (dictionary)
			{
				PreCompInfo preCompInfo = (PreCompInfo)dictionary[name];
				PreCompInfo preCompInfo2 = callback.Precompute(preCompInfo);
				if (preCompInfo2 != preCompInfo)
				{
					dictionary[name] = preCompInfo2;
				}
				result = preCompInfo2;
			}
			return result;
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x000E6868 File Offset: 0x000E6868
		public virtual ECPoint ImportPoint(ECPoint p)
		{
			if (this == p.Curve)
			{
				return p;
			}
			if (p.IsInfinity)
			{
				return this.Infinity;
			}
			p = p.Normalize();
			return this.CreatePoint(p.XCoord.ToBigInteger(), p.YCoord.ToBigInteger(), p.IsCompressed);
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x000E68C4 File Offset: 0x000E68C4
		public virtual void NormalizeAll(ECPoint[] points)
		{
			this.NormalizeAll(points, 0, points.Length, null);
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x000E68D4 File Offset: 0x000E68D4
		public virtual void NormalizeAll(ECPoint[] points, int off, int len, ECFieldElement iso)
		{
			this.CheckPoints(points, off, len);
			int coordinateSystem = this.CoordinateSystem;
			if (coordinateSystem == 0 || coordinateSystem == 5)
			{
				if (iso != null)
				{
					throw new ArgumentException("not valid for affine coordinates", "iso");
				}
				return;
			}
			else
			{
				ECFieldElement[] array = new ECFieldElement[len];
				int[] array2 = new int[len];
				int num = 0;
				for (int i = 0; i < len; i++)
				{
					ECPoint ecpoint = points[off + i];
					if (ecpoint != null && (iso != null || !ecpoint.IsNormalized()))
					{
						array[num] = ecpoint.GetZCoord(0);
						array2[num++] = off + i;
					}
				}
				if (num == 0)
				{
					return;
				}
				ECAlgorithms.MontgomeryTrick(array, 0, num, iso);
				for (int j = 0; j < num; j++)
				{
					int num2 = array2[j];
					points[num2] = points[num2].Normalize(array[j]);
				}
				return;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06002B4A RID: 11082
		public abstract ECPoint Infinity { get; }

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06002B4B RID: 11083 RVA: 0x000E69C8 File Offset: 0x000E69C8
		public virtual IFiniteField Field
		{
			get
			{
				return this.m_field;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06002B4C RID: 11084 RVA: 0x000E69D0 File Offset: 0x000E69D0
		public virtual ECFieldElement A
		{
			get
			{
				return this.m_a;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06002B4D RID: 11085 RVA: 0x000E69D8 File Offset: 0x000E69D8
		public virtual ECFieldElement B
		{
			get
			{
				return this.m_b;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06002B4E RID: 11086 RVA: 0x000E69E0 File Offset: 0x000E69E0
		public virtual BigInteger Order
		{
			get
			{
				return this.m_order;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06002B4F RID: 11087 RVA: 0x000E69E8 File Offset: 0x000E69E8
		public virtual BigInteger Cofactor
		{
			get
			{
				return this.m_cofactor;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06002B50 RID: 11088 RVA: 0x000E69F0 File Offset: 0x000E69F0
		public virtual int CoordinateSystem
		{
			get
			{
				return this.m_coord;
			}
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x000E69F8 File Offset: 0x000E69F8
		public virtual ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			int num = (this.FieldSize + 7) / 8;
			byte[] array = new byte[len * num * 2];
			int num2 = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				byte[] array2 = ecpoint.RawXCoord.ToBigInteger().ToByteArray();
				byte[] array3 = ecpoint.RawYCoord.ToBigInteger().ToByteArray();
				int num3 = (array2.Length > num) ? 1 : 0;
				int num4 = array2.Length - num3;
				int num5 = (array3.Length > num) ? 1 : 0;
				int num6 = array3.Length - num5;
				Array.Copy(array2, num3, array, num2 + num - num4, num4);
				num2 += num;
				Array.Copy(array3, num5, array, num2 + num - num6, num6);
				num2 += num;
			}
			return new ECCurve.DefaultLookupTable(this, array, len);
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x000E6AD0 File Offset: 0x000E6AD0
		protected virtual void CheckPoint(ECPoint point)
		{
			if (point == null || this != point.Curve)
			{
				throw new ArgumentException("must be non-null and on this curve", "point");
			}
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x000E6AF4 File Offset: 0x000E6AF4
		protected virtual void CheckPoints(ECPoint[] points)
		{
			this.CheckPoints(points, 0, points.Length);
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x000E6B04 File Offset: 0x000E6B04
		protected virtual void CheckPoints(ECPoint[] points, int off, int len)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			if (off < 0 || len < 0 || off > points.Length - len)
			{
				throw new ArgumentException("invalid range specified", "points");
			}
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				if (ecpoint != null && this != ecpoint.Curve)
				{
					throw new ArgumentException("entries must be null or on this curve", "points");
				}
			}
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x000E6B8C File Offset: 0x000E6B8C
		public virtual bool Equals(ECCurve other)
		{
			return this == other || (other != null && (this.Field.Equals(other.Field) && this.A.ToBigInteger().Equals(other.A.ToBigInteger())) && this.B.ToBigInteger().Equals(other.B.ToBigInteger()));
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x000E6C04 File Offset: 0x000E6C04
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ECCurve);
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x000E6C14 File Offset: 0x000E6C14
		public override int GetHashCode()
		{
			return this.Field.GetHashCode() ^ Integers.RotateLeft(this.A.ToBigInteger().GetHashCode(), 8) ^ Integers.RotateLeft(this.B.ToBigInteger().GetHashCode(), 16);
		}

		// Token: 0x06002B58 RID: 11096
		protected abstract ECPoint DecompressPoint(int yTilde, BigInteger X1);

		// Token: 0x06002B59 RID: 11097 RVA: 0x000E6C60 File Offset: 0x000E6C60
		public virtual ECEndomorphism GetEndomorphism()
		{
			return this.m_endomorphism;
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x000E6C68 File Offset: 0x000E6C68
		public virtual ECMultiplier GetMultiplier()
		{
			ECMultiplier multiplier;
			lock (this)
			{
				if (this.m_multiplier == null)
				{
					this.m_multiplier = this.CreateDefaultMultiplier();
				}
				multiplier = this.m_multiplier;
			}
			return multiplier;
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x000E6CB8 File Offset: 0x000E6CB8
		public virtual ECPoint DecodePoint(byte[] encoded)
		{
			int num = (this.FieldSize + 7) / 8;
			byte b = encoded[0];
			ECPoint ecpoint;
			switch (b)
			{
			case 0:
				if (encoded.Length != 1)
				{
					throw new ArgumentException("Incorrect length for infinity encoding", "encoded");
				}
				ecpoint = this.Infinity;
				goto IL_172;
			case 2:
			case 3:
			{
				if (encoded.Length != num + 1)
				{
					throw new ArgumentException("Incorrect length for compressed encoding", "encoded");
				}
				int yTilde = (int)(b & 1);
				BigInteger x = new BigInteger(1, encoded, 1, num);
				ecpoint = this.DecompressPoint(yTilde, x);
				if (!ecpoint.ImplIsValid(true, true))
				{
					throw new ArgumentException("Invalid point");
				}
				goto IL_172;
			}
			case 4:
			{
				if (encoded.Length != 2 * num + 1)
				{
					throw new ArgumentException("Incorrect length for uncompressed encoding", "encoded");
				}
				BigInteger x2 = new BigInteger(1, encoded, 1, num);
				BigInteger y = new BigInteger(1, encoded, 1 + num, num);
				ecpoint = this.ValidatePoint(x2, y);
				goto IL_172;
			}
			case 6:
			case 7:
			{
				if (encoded.Length != 2 * num + 1)
				{
					throw new ArgumentException("Incorrect length for hybrid encoding", "encoded");
				}
				BigInteger x3 = new BigInteger(1, encoded, 1, num);
				BigInteger bigInteger = new BigInteger(1, encoded, 1 + num, num);
				if (bigInteger.TestBit(0) != (b == 7))
				{
					throw new ArgumentException("Inconsistent Y coordinate in hybrid encoding", "encoded");
				}
				ecpoint = this.ValidatePoint(x3, bigInteger);
				goto IL_172;
			}
			}
			throw new FormatException("Invalid point encoding " + b);
			IL_172:
			if (b != 0 && ecpoint.IsInfinity)
			{
				throw new ArgumentException("Invalid infinity encoding", "encoded");
			}
			return ecpoint;
		}

		// Token: 0x04001B4E RID: 6990
		public const int COORD_AFFINE = 0;

		// Token: 0x04001B4F RID: 6991
		public const int COORD_HOMOGENEOUS = 1;

		// Token: 0x04001B50 RID: 6992
		public const int COORD_JACOBIAN = 2;

		// Token: 0x04001B51 RID: 6993
		public const int COORD_JACOBIAN_CHUDNOVSKY = 3;

		// Token: 0x04001B52 RID: 6994
		public const int COORD_JACOBIAN_MODIFIED = 4;

		// Token: 0x04001B53 RID: 6995
		public const int COORD_LAMBDA_AFFINE = 5;

		// Token: 0x04001B54 RID: 6996
		public const int COORD_LAMBDA_PROJECTIVE = 6;

		// Token: 0x04001B55 RID: 6997
		public const int COORD_SKEWED = 7;

		// Token: 0x04001B56 RID: 6998
		protected readonly IFiniteField m_field;

		// Token: 0x04001B57 RID: 6999
		protected ECFieldElement m_a;

		// Token: 0x04001B58 RID: 7000
		protected ECFieldElement m_b;

		// Token: 0x04001B59 RID: 7001
		protected BigInteger m_order;

		// Token: 0x04001B5A RID: 7002
		protected BigInteger m_cofactor;

		// Token: 0x04001B5B RID: 7003
		protected int m_coord = 0;

		// Token: 0x04001B5C RID: 7004
		protected ECEndomorphism m_endomorphism = null;

		// Token: 0x04001B5D RID: 7005
		protected ECMultiplier m_multiplier = null;

		// Token: 0x02000E27 RID: 3623
		public class Config
		{
			// Token: 0x06008C6C RID: 35948 RVA: 0x002A2738 File Offset: 0x002A2738
			internal Config(ECCurve outer, int coord, ECEndomorphism endomorphism, ECMultiplier multiplier)
			{
				this.outer = outer;
				this.coord = coord;
				this.endomorphism = endomorphism;
				this.multiplier = multiplier;
			}

			// Token: 0x06008C6D RID: 35949 RVA: 0x002A2760 File Offset: 0x002A2760
			public ECCurve.Config SetCoordinateSystem(int coord)
			{
				this.coord = coord;
				return this;
			}

			// Token: 0x06008C6E RID: 35950 RVA: 0x002A276C File Offset: 0x002A276C
			public ECCurve.Config SetEndomorphism(ECEndomorphism endomorphism)
			{
				this.endomorphism = endomorphism;
				return this;
			}

			// Token: 0x06008C6F RID: 35951 RVA: 0x002A2778 File Offset: 0x002A2778
			public ECCurve.Config SetMultiplier(ECMultiplier multiplier)
			{
				this.multiplier = multiplier;
				return this;
			}

			// Token: 0x06008C70 RID: 35952 RVA: 0x002A2784 File Offset: 0x002A2784
			public ECCurve Create()
			{
				if (!this.outer.SupportsCoordinateSystem(this.coord))
				{
					throw new InvalidOperationException("unsupported coordinate system");
				}
				ECCurve eccurve = this.outer.CloneCurve();
				if (eccurve == this.outer)
				{
					throw new InvalidOperationException("implementation returned current curve");
				}
				eccurve.m_coord = this.coord;
				eccurve.m_endomorphism = this.endomorphism;
				eccurve.m_multiplier = this.multiplier;
				return eccurve;
			}

			// Token: 0x04004192 RID: 16786
			protected ECCurve outer;

			// Token: 0x04004193 RID: 16787
			protected int coord;

			// Token: 0x04004194 RID: 16788
			protected ECEndomorphism endomorphism;

			// Token: 0x04004195 RID: 16789
			protected ECMultiplier multiplier;
		}

		// Token: 0x02000E28 RID: 3624
		private class DefaultLookupTable : AbstractECLookupTable
		{
			// Token: 0x06008C71 RID: 35953 RVA: 0x002A2800 File Offset: 0x002A2800
			internal DefaultLookupTable(ECCurve outer, byte[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D80 RID: 7552
			// (get) Token: 0x06008C72 RID: 35954 RVA: 0x002A2820 File Offset: 0x002A2820
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008C73 RID: 35955 RVA: 0x002A2828 File Offset: 0x002A2828
			public override ECPoint Lookup(int index)
			{
				int num = (this.m_outer.FieldSize + 7) / 8;
				byte[] array = new byte[num];
				byte[] array2 = new byte[num];
				int num2 = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					byte b = (byte)((i ^ index) - 1 >> 31);
					for (int j = 0; j < num; j++)
					{
						byte[] array3;
						IntPtr intPtr;
						(array3 = array)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num2 + j] & b));
						(array3 = array2)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num2 + num + j] & b));
					}
					num2 += num * 2;
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008C74 RID: 35956 RVA: 0x002A28E0 File Offset: 0x002A28E0
			public override ECPoint LookupVar(int index)
			{
				int num = (this.m_outer.FieldSize + 7) / 8;
				byte[] array = new byte[num];
				byte[] array2 = new byte[num];
				int num2 = index * num * 2;
				for (int i = 0; i < num; i++)
				{
					array[i] = this.m_table[num2 + i];
					array2[i] = this.m_table[num2 + num + i];
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008C75 RID: 35957 RVA: 0x002A2950 File Offset: 0x002A2950
			private ECPoint CreatePoint(byte[] x, byte[] y)
			{
				ECFieldElement x2 = this.m_outer.FromBigInteger(new BigInteger(1, x));
				ECFieldElement y2 = this.m_outer.FromBigInteger(new BigInteger(1, y));
				return this.m_outer.CreateRawPoint(x2, y2, false);
			}

			// Token: 0x04004196 RID: 16790
			private readonly ECCurve m_outer;

			// Token: 0x04004197 RID: 16791
			private readonly byte[] m_table;

			// Token: 0x04004198 RID: 16792
			private readonly int m_size;
		}
	}
}
