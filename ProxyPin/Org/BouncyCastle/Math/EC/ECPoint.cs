using System;
using System.Collections;
using System.Text;
using Org.BouncyCastle.Math.EC.Multiplier;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x02000577 RID: 1399
	public abstract class ECPoint
	{
		// Token: 0x06002BB9 RID: 11193 RVA: 0x000E7BA8 File Offset: 0x000E7BA8
		protected static ECFieldElement[] GetInitialZCoords(ECCurve curve)
		{
			int num = (curve == null) ? 0 : curve.CoordinateSystem;
			int num2 = num;
			if (num2 == 0 || num2 == 5)
			{
				return ECPoint.EMPTY_ZS;
			}
			ECFieldElement ecfieldElement = curve.FromBigInteger(BigInteger.One);
			switch (num)
			{
			case 1:
			case 2:
			case 6:
				return new ECFieldElement[]
				{
					ecfieldElement
				};
			case 3:
				return new ECFieldElement[]
				{
					ecfieldElement,
					ecfieldElement,
					ecfieldElement
				};
			case 4:
				return new ECFieldElement[]
				{
					ecfieldElement,
					curve.A
				};
			}
			throw new ArgumentException("unknown coordinate system");
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x000E7C70 File Offset: 0x000E7C70
		protected ECPoint(ECCurve curve, ECFieldElement x, ECFieldElement y, bool withCompression) : this(curve, x, y, ECPoint.GetInitialZCoords(curve), withCompression)
		{
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x000E7C84 File Offset: 0x000E7C84
		internal ECPoint(ECCurve curve, ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			this.m_curve = curve;
			this.m_x = x;
			this.m_y = y;
			this.m_zs = zs;
			this.m_withCompression = withCompression;
		}

		// Token: 0x06002BBC RID: 11196
		protected abstract bool SatisfiesCurveEquation();

		// Token: 0x06002BBD RID: 11197 RVA: 0x000E7CB8 File Offset: 0x000E7CB8
		protected virtual bool SatisfiesOrder()
		{
			if (BigInteger.One.Equals(this.Curve.Cofactor))
			{
				return true;
			}
			BigInteger order = this.Curve.Order;
			return order == null || ECAlgorithms.ReferenceMultiply(this, order).IsInfinity;
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x000E7D08 File Offset: 0x000E7D08
		public ECPoint GetDetachedPoint()
		{
			return this.Normalize().Detach();
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06002BBF RID: 11199 RVA: 0x000E7D18 File Offset: 0x000E7D18
		public virtual ECCurve Curve
		{
			get
			{
				return this.m_curve;
			}
		}

		// Token: 0x06002BC0 RID: 11200
		protected abstract ECPoint Detach();

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06002BC1 RID: 11201 RVA: 0x000E7D20 File Offset: 0x000E7D20
		protected virtual int CurveCoordinateSystem
		{
			get
			{
				if (this.m_curve != null)
				{
					return this.m_curve.CoordinateSystem;
				}
				return 0;
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06002BC2 RID: 11202 RVA: 0x000E7D3C File Offset: 0x000E7D3C
		public virtual ECFieldElement AffineXCoord
		{
			get
			{
				this.CheckNormalized();
				return this.XCoord;
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06002BC3 RID: 11203 RVA: 0x000E7D4C File Offset: 0x000E7D4C
		public virtual ECFieldElement AffineYCoord
		{
			get
			{
				this.CheckNormalized();
				return this.YCoord;
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06002BC4 RID: 11204 RVA: 0x000E7D5C File Offset: 0x000E7D5C
		public virtual ECFieldElement XCoord
		{
			get
			{
				return this.m_x;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06002BC5 RID: 11205 RVA: 0x000E7D64 File Offset: 0x000E7D64
		public virtual ECFieldElement YCoord
		{
			get
			{
				return this.m_y;
			}
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x000E7D6C File Offset: 0x000E7D6C
		public virtual ECFieldElement GetZCoord(int index)
		{
			if (index >= 0 && index < this.m_zs.Length)
			{
				return this.m_zs[index];
			}
			return null;
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x000E7D94 File Offset: 0x000E7D94
		public virtual ECFieldElement[] GetZCoords()
		{
			int num = this.m_zs.Length;
			if (num == 0)
			{
				return this.m_zs;
			}
			ECFieldElement[] array = new ECFieldElement[num];
			Array.Copy(this.m_zs, 0, array, 0, num);
			return array;
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06002BC8 RID: 11208 RVA: 0x000E7DD4 File Offset: 0x000E7DD4
		protected internal ECFieldElement RawXCoord
		{
			get
			{
				return this.m_x;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06002BC9 RID: 11209 RVA: 0x000E7DDC File Offset: 0x000E7DDC
		protected internal ECFieldElement RawYCoord
		{
			get
			{
				return this.m_y;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06002BCA RID: 11210 RVA: 0x000E7DE4 File Offset: 0x000E7DE4
		protected internal ECFieldElement[] RawZCoords
		{
			get
			{
				return this.m_zs;
			}
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x000E7DEC File Offset: 0x000E7DEC
		protected virtual void CheckNormalized()
		{
			if (!this.IsNormalized())
			{
				throw new InvalidOperationException("point not in normal form");
			}
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x000E7E04 File Offset: 0x000E7E04
		public virtual bool IsNormalized()
		{
			int curveCoordinateSystem = this.CurveCoordinateSystem;
			return curveCoordinateSystem == 0 || curveCoordinateSystem == 5 || this.IsInfinity || this.RawZCoords[0].IsOne;
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x000E7E48 File Offset: 0x000E7E48
		public virtual ECPoint Normalize()
		{
			if (this.IsInfinity)
			{
				return this;
			}
			int curveCoordinateSystem = this.CurveCoordinateSystem;
			if (curveCoordinateSystem == 0 || curveCoordinateSystem == 5)
			{
				return this;
			}
			ECFieldElement ecfieldElement = this.RawZCoords[0];
			if (ecfieldElement.IsOne)
			{
				return this;
			}
			return this.Normalize(ecfieldElement.Invert());
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x000E7EA4 File Offset: 0x000E7EA4
		internal virtual ECPoint Normalize(ECFieldElement zInv)
		{
			switch (this.CurveCoordinateSystem)
			{
			case 1:
			case 6:
				return this.CreateScaledPoint(zInv, zInv);
			case 2:
			case 3:
			case 4:
			{
				ECFieldElement ecfieldElement = zInv.Square();
				ECFieldElement sy = ecfieldElement.Multiply(zInv);
				return this.CreateScaledPoint(ecfieldElement, sy);
			}
			}
			throw new InvalidOperationException("not a projective coordinate system");
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x000E7F0C File Offset: 0x000E7F0C
		protected virtual ECPoint CreateScaledPoint(ECFieldElement sx, ECFieldElement sy)
		{
			return this.Curve.CreateRawPoint(this.RawXCoord.Multiply(sx), this.RawYCoord.Multiply(sy), this.IsCompressed);
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06002BD0 RID: 11216 RVA: 0x000E7F48 File Offset: 0x000E7F48
		public bool IsInfinity
		{
			get
			{
				return this.m_x == null && this.m_y == null;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06002BD1 RID: 11217 RVA: 0x000E7F60 File Offset: 0x000E7F60
		public bool IsCompressed
		{
			get
			{
				return this.m_withCompression;
			}
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x000E7F68 File Offset: 0x000E7F68
		public bool IsValid()
		{
			return this.ImplIsValid(false, true);
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x000E7F74 File Offset: 0x000E7F74
		internal bool IsValidPartial()
		{
			return this.ImplIsValid(false, false);
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x000E7F80 File Offset: 0x000E7F80
		internal bool ImplIsValid(bool decompressed, bool checkOrder)
		{
			if (this.IsInfinity)
			{
				return true;
			}
			ECPoint.ValidityCallback callback = new ECPoint.ValidityCallback(this, decompressed, checkOrder);
			ValidityPreCompInfo validityPreCompInfo = (ValidityPreCompInfo)this.Curve.Precompute(this, ValidityPreCompInfo.PRECOMP_NAME, callback);
			return !validityPreCompInfo.HasFailed();
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x000E7FC8 File Offset: 0x000E7FC8
		public virtual ECPoint ScaleX(ECFieldElement scale)
		{
			if (!this.IsInfinity)
			{
				return this.Curve.CreateRawPoint(this.RawXCoord.Multiply(scale), this.RawYCoord, this.RawZCoords, this.IsCompressed);
			}
			return this;
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x000E8010 File Offset: 0x000E8010
		public virtual ECPoint ScaleXNegateY(ECFieldElement scale)
		{
			if (!this.IsInfinity)
			{
				return this.Curve.CreateRawPoint(this.RawXCoord.Multiply(scale), this.RawYCoord.Negate(), this.RawZCoords, this.IsCompressed);
			}
			return this;
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x000E805C File Offset: 0x000E805C
		public virtual ECPoint ScaleY(ECFieldElement scale)
		{
			if (!this.IsInfinity)
			{
				return this.Curve.CreateRawPoint(this.RawXCoord, this.RawYCoord.Multiply(scale), this.RawZCoords, this.IsCompressed);
			}
			return this;
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x000E80A4 File Offset: 0x000E80A4
		public virtual ECPoint ScaleYNegateX(ECFieldElement scale)
		{
			if (!this.IsInfinity)
			{
				return this.Curve.CreateRawPoint(this.RawXCoord.Negate(), this.RawYCoord.Multiply(scale), this.RawZCoords, this.IsCompressed);
			}
			return this;
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x000E80F0 File Offset: 0x000E80F0
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ECPoint);
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x000E8100 File Offset: 0x000E8100
		public virtual bool Equals(ECPoint other)
		{
			if (this == other)
			{
				return true;
			}
			if (other == null)
			{
				return false;
			}
			ECCurve curve = this.Curve;
			ECCurve curve2 = other.Curve;
			bool flag = null == curve;
			bool flag2 = null == curve2;
			bool isInfinity = this.IsInfinity;
			bool isInfinity2 = other.IsInfinity;
			if (isInfinity || isInfinity2)
			{
				return isInfinity && isInfinity2 && (flag || flag2 || curve.Equals(curve2));
			}
			ECPoint ecpoint = this;
			ECPoint ecpoint2 = other;
			if (!flag || !flag2)
			{
				if (flag)
				{
					ecpoint2 = ecpoint2.Normalize();
				}
				else if (flag2)
				{
					ecpoint = ecpoint.Normalize();
				}
				else
				{
					if (!curve.Equals(curve2))
					{
						return false;
					}
					ECPoint[] array = new ECPoint[]
					{
						this,
						curve.ImportPoint(ecpoint2)
					};
					curve.NormalizeAll(array);
					ecpoint = array[0];
					ecpoint2 = array[1];
				}
			}
			return ecpoint.XCoord.Equals(ecpoint2.XCoord) && ecpoint.YCoord.Equals(ecpoint2.YCoord);
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x000E8234 File Offset: 0x000E8234
		public override int GetHashCode()
		{
			ECCurve curve = this.Curve;
			int num = (curve == null) ? 0 : (~curve.GetHashCode());
			if (!this.IsInfinity)
			{
				ECPoint ecpoint = this.Normalize();
				num ^= ecpoint.XCoord.GetHashCode() * 17;
				num ^= ecpoint.YCoord.GetHashCode() * 257;
			}
			return num;
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x000E8298 File Offset: 0x000E8298
		public override string ToString()
		{
			if (this.IsInfinity)
			{
				return "INF";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			stringBuilder.Append(this.RawXCoord);
			stringBuilder.Append(',');
			stringBuilder.Append(this.RawYCoord);
			for (int i = 0; i < this.m_zs.Length; i++)
			{
				stringBuilder.Append(',');
				stringBuilder.Append(this.m_zs[i]);
			}
			stringBuilder.Append(')');
			return stringBuilder.ToString();
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x000E8330 File Offset: 0x000E8330
		public virtual byte[] GetEncoded()
		{
			return this.GetEncoded(this.m_withCompression);
		}

		// Token: 0x06002BDE RID: 11230
		public abstract byte[] GetEncoded(bool compressed);

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06002BDF RID: 11231
		protected internal abstract bool CompressionYTilde { get; }

		// Token: 0x06002BE0 RID: 11232
		public abstract ECPoint Add(ECPoint b);

		// Token: 0x06002BE1 RID: 11233
		public abstract ECPoint Subtract(ECPoint b);

		// Token: 0x06002BE2 RID: 11234
		public abstract ECPoint Negate();

		// Token: 0x06002BE3 RID: 11235 RVA: 0x000E8340 File Offset: 0x000E8340
		public virtual ECPoint TimesPow2(int e)
		{
			if (e < 0)
			{
				throw new ArgumentException("cannot be negative", "e");
			}
			ECPoint ecpoint = this;
			while (--e >= 0)
			{
				ecpoint = ecpoint.Twice();
			}
			return ecpoint;
		}

		// Token: 0x06002BE4 RID: 11236
		public abstract ECPoint Twice();

		// Token: 0x06002BE5 RID: 11237
		public abstract ECPoint Multiply(BigInteger b);

		// Token: 0x06002BE6 RID: 11238 RVA: 0x000E8380 File Offset: 0x000E8380
		public virtual ECPoint TwicePlus(ECPoint b)
		{
			return this.Twice().Add(b);
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x000E8390 File Offset: 0x000E8390
		public virtual ECPoint ThreeTimes()
		{
			return this.TwicePlus(this);
		}

		// Token: 0x04001B6C RID: 7020
		protected static ECFieldElement[] EMPTY_ZS = new ECFieldElement[0];

		// Token: 0x04001B6D RID: 7021
		protected internal readonly ECCurve m_curve;

		// Token: 0x04001B6E RID: 7022
		protected internal readonly ECFieldElement m_x;

		// Token: 0x04001B6F RID: 7023
		protected internal readonly ECFieldElement m_y;

		// Token: 0x04001B70 RID: 7024
		protected internal readonly ECFieldElement[] m_zs;

		// Token: 0x04001B71 RID: 7025
		protected internal readonly bool m_withCompression;

		// Token: 0x04001B72 RID: 7026
		protected internal IDictionary m_preCompTable = null;

		// Token: 0x02000E2A RID: 3626
		private class ValidityCallback : IPreCompCallback
		{
			// Token: 0x06008C7B RID: 35963 RVA: 0x002A2AD4 File Offset: 0x002A2AD4
			internal ValidityCallback(ECPoint outer, bool decompressed, bool checkOrder)
			{
				this.m_outer = outer;
				this.m_decompressed = decompressed;
				this.m_checkOrder = checkOrder;
			}

			// Token: 0x06008C7C RID: 35964 RVA: 0x002A2AF4 File Offset: 0x002A2AF4
			public PreCompInfo Precompute(PreCompInfo existing)
			{
				ValidityPreCompInfo validityPreCompInfo = existing as ValidityPreCompInfo;
				if (validityPreCompInfo == null)
				{
					validityPreCompInfo = new ValidityPreCompInfo();
				}
				if (validityPreCompInfo.HasFailed())
				{
					return validityPreCompInfo;
				}
				if (!validityPreCompInfo.HasCurveEquationPassed())
				{
					if (!this.m_decompressed && !this.m_outer.SatisfiesCurveEquation())
					{
						validityPreCompInfo.ReportFailed();
						return validityPreCompInfo;
					}
					validityPreCompInfo.ReportCurveEquationPassed();
				}
				if (this.m_checkOrder && !validityPreCompInfo.HasOrderPassed())
				{
					if (!this.m_outer.SatisfiesOrder())
					{
						validityPreCompInfo.ReportFailed();
						return validityPreCompInfo;
					}
					validityPreCompInfo.ReportOrderPassed();
				}
				return validityPreCompInfo;
			}

			// Token: 0x0400419C RID: 16796
			private readonly ECPoint m_outer;

			// Token: 0x0400419D RID: 16797
			private readonly bool m_decompressed;

			// Token: 0x0400419E RID: 16798
			private readonly bool m_checkOrder;
		}
	}
}
