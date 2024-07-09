using System;
using Org.BouncyCastle.Math.Field;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x02000571 RID: 1393
	public abstract class AbstractFpCurve : ECCurve
	{
		// Token: 0x06002B63 RID: 11107 RVA: 0x000E6E74 File Offset: 0x000E6E74
		protected AbstractFpCurve(BigInteger q) : base(FiniteFields.GetPrimeField(q))
		{
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x000E6E84 File Offset: 0x000E6E84
		public override bool IsValidFieldElement(BigInteger x)
		{
			return x != null && x.SignValue >= 0 && x.CompareTo(this.Field.Characteristic) < 0;
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x000E6EC0 File Offset: 0x000E6EC0
		protected override ECPoint DecompressPoint(int yTilde, BigInteger X1)
		{
			ECFieldElement ecfieldElement = this.FromBigInteger(X1);
			ECFieldElement ecfieldElement2 = ecfieldElement.Square().Add(this.A).Multiply(ecfieldElement).Add(this.B);
			ECFieldElement ecfieldElement3 = ecfieldElement2.Sqrt();
			if (ecfieldElement3 == null)
			{
				throw new ArgumentException("Invalid point compression");
			}
			if (ecfieldElement3.TestBitZero() != (yTilde == 1))
			{
				ecfieldElement3 = ecfieldElement3.Negate();
			}
			return this.CreateRawPoint(ecfieldElement, ecfieldElement3, true);
		}
	}
}
