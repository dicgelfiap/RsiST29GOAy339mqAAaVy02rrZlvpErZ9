using System;
using Org.BouncyCastle.Math.EC.Abc;
using Org.BouncyCastle.Math.Field;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x020005B1 RID: 1457
	public abstract class AbstractF2mCurve : ECCurve
	{
		// Token: 0x06002F2E RID: 12078 RVA: 0x000F7338 File Offset: 0x000F7338
		public static BigInteger Inverse(int m, int[] ks, BigInteger x)
		{
			return new LongArray(x).ModInverse(m, ks).ToBigInteger();
		}

		// Token: 0x06002F2F RID: 12079 RVA: 0x000F734C File Offset: 0x000F734C
		private static IFiniteField BuildField(int m, int k1, int k2, int k3)
		{
			if (k1 == 0)
			{
				throw new ArgumentException("k1 must be > 0");
			}
			if (k2 == 0)
			{
				if (k3 != 0)
				{
					throw new ArgumentException("k3 must be 0 if k2 == 0");
				}
				return FiniteFields.GetBinaryExtensionField(new int[]
				{
					0,
					k1,
					m
				});
			}
			else
			{
				if (k2 <= k1)
				{
					throw new ArgumentException("k2 must be > k1");
				}
				if (k3 <= k2)
				{
					throw new ArgumentException("k3 must be > k2");
				}
				return FiniteFields.GetBinaryExtensionField(new int[]
				{
					0,
					k1,
					k2,
					k3,
					m
				});
			}
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x000F73DC File Offset: 0x000F73DC
		protected AbstractF2mCurve(int m, int k1, int k2, int k3) : base(AbstractF2mCurve.BuildField(m, k1, k2, k3))
		{
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x000F73F8 File Offset: 0x000F73F8
		public override bool IsValidFieldElement(BigInteger x)
		{
			return x != null && x.SignValue >= 0 && x.BitLength <= this.FieldSize;
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x000F7420 File Offset: 0x000F7420
		[Obsolete("Per-point compression property will be removed")]
		public override ECPoint CreatePoint(BigInteger x, BigInteger y, bool withCompression)
		{
			ECFieldElement ecfieldElement = this.FromBigInteger(x);
			ECFieldElement ecfieldElement2 = this.FromBigInteger(y);
			switch (this.CoordinateSystem)
			{
			case 5:
			case 6:
				if (ecfieldElement.IsZero)
				{
					if (!ecfieldElement2.Square().Equals(this.B))
					{
						throw new ArgumentException();
					}
				}
				else
				{
					ecfieldElement2 = ecfieldElement2.Divide(ecfieldElement).Add(ecfieldElement);
				}
				break;
			}
			return this.CreateRawPoint(ecfieldElement, ecfieldElement2, withCompression);
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x000F749C File Offset: 0x000F749C
		protected override ECPoint DecompressPoint(int yTilde, BigInteger X1)
		{
			ECFieldElement ecfieldElement = this.FromBigInteger(X1);
			ECFieldElement ecfieldElement2 = null;
			if (ecfieldElement.IsZero)
			{
				ecfieldElement2 = this.B.Sqrt();
			}
			else
			{
				ECFieldElement beta = ecfieldElement.Square().Invert().Multiply(this.B).Add(this.A).Add(ecfieldElement);
				ECFieldElement ecfieldElement3 = this.SolveQuadraticEquation(beta);
				if (ecfieldElement3 != null)
				{
					if (ecfieldElement3.TestBitZero() != (yTilde == 1))
					{
						ecfieldElement3 = ecfieldElement3.AddOne();
					}
					switch (this.CoordinateSystem)
					{
					case 5:
					case 6:
						ecfieldElement2 = ecfieldElement3.Add(ecfieldElement);
						break;
					default:
						ecfieldElement2 = ecfieldElement3.Multiply(ecfieldElement);
						break;
					}
				}
			}
			if (ecfieldElement2 == null)
			{
				throw new ArgumentException("Invalid point compression");
			}
			return this.CreateRawPoint(ecfieldElement, ecfieldElement2, true);
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x000F756C File Offset: 0x000F756C
		internal ECFieldElement SolveQuadraticEquation(ECFieldElement beta)
		{
			AbstractF2mFieldElement abstractF2mFieldElement = (AbstractF2mFieldElement)beta;
			bool hasFastTrace = abstractF2mFieldElement.HasFastTrace;
			if (hasFastTrace && abstractF2mFieldElement.Trace() != 0)
			{
				return null;
			}
			int fieldSize = this.FieldSize;
			if ((fieldSize & 1) != 0)
			{
				ECFieldElement ecfieldElement = abstractF2mFieldElement.HalfTrace();
				if (hasFastTrace || ecfieldElement.Square().Add(ecfieldElement).Add(beta).IsZero)
				{
					return ecfieldElement;
				}
				return null;
			}
			else
			{
				if (beta.IsZero)
				{
					return beta;
				}
				ECFieldElement ecfieldElement2 = this.FromBigInteger(BigInteger.Zero);
				for (;;)
				{
					ECFieldElement b = this.FromBigInteger(BigInteger.Arbitrary(fieldSize));
					ECFieldElement ecfieldElement3 = ecfieldElement2;
					ECFieldElement ecfieldElement4 = beta;
					for (int i = 1; i < fieldSize; i++)
					{
						ECFieldElement ecfieldElement5 = ecfieldElement4.Square();
						ecfieldElement3 = ecfieldElement3.Square().Add(ecfieldElement5.Multiply(b));
						ecfieldElement4 = ecfieldElement5.Add(beta);
					}
					if (!ecfieldElement4.IsZero)
					{
						break;
					}
					ECFieldElement ecfieldElement6 = ecfieldElement3.Square().Add(ecfieldElement3);
					if (!ecfieldElement6.IsZero)
					{
						return ecfieldElement3;
					}
				}
				return null;
			}
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x000F7670 File Offset: 0x000F7670
		internal virtual BigInteger[] GetSi()
		{
			if (this.si == null)
			{
				lock (this)
				{
					if (this.si == null)
					{
						this.si = Tnaf.GetSi(this);
					}
				}
			}
			return this.si;
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06002F36 RID: 12086 RVA: 0x000F76CC File Offset: 0x000F76CC
		public virtual bool IsKoblitz
		{
			get
			{
				return this.m_order != null && this.m_cofactor != null && this.m_b.IsOne && (this.m_a.IsZero || this.m_a.IsOne);
			}
		}

		// Token: 0x04001C0C RID: 7180
		private BigInteger[] si = null;
	}
}
