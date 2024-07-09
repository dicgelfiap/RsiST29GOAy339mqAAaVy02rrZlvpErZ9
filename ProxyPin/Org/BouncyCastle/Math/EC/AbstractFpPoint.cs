using System;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x0200057A RID: 1402
	public abstract class AbstractFpPoint : ECPointBase
	{
		// Token: 0x06002BEE RID: 11246 RVA: 0x000E848C File Offset: 0x000E848C
		protected AbstractFpPoint(ECCurve curve, ECFieldElement x, ECFieldElement y, bool withCompression) : base(curve, x, y, withCompression)
		{
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x000E849C File Offset: 0x000E849C
		protected AbstractFpPoint(ECCurve curve, ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression) : base(curve, x, y, zs, withCompression)
		{
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06002BF0 RID: 11248 RVA: 0x000E84AC File Offset: 0x000E84AC
		protected internal override bool CompressionYTilde
		{
			get
			{
				return this.AffineYCoord.TestBitZero();
			}
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x000E84BC File Offset: 0x000E84BC
		protected override bool SatisfiesCurveEquation()
		{
			ECFieldElement rawXCoord = base.RawXCoord;
			ECFieldElement rawYCoord = base.RawYCoord;
			ECFieldElement ecfieldElement = this.Curve.A;
			ECFieldElement ecfieldElement2 = this.Curve.B;
			ECFieldElement ecfieldElement3 = rawYCoord.Square();
			switch (this.CurveCoordinateSystem)
			{
			case 0:
				break;
			case 1:
			{
				ECFieldElement ecfieldElement4 = base.RawZCoords[0];
				if (!ecfieldElement4.IsOne)
				{
					ECFieldElement b = ecfieldElement4.Square();
					ECFieldElement b2 = ecfieldElement4.Multiply(b);
					ecfieldElement3 = ecfieldElement3.Multiply(ecfieldElement4);
					ecfieldElement = ecfieldElement.Multiply(b);
					ecfieldElement2 = ecfieldElement2.Multiply(b2);
				}
				break;
			}
			case 2:
			case 3:
			case 4:
			{
				ECFieldElement ecfieldElement5 = base.RawZCoords[0];
				if (!ecfieldElement5.IsOne)
				{
					ECFieldElement ecfieldElement6 = ecfieldElement5.Square();
					ECFieldElement b3 = ecfieldElement6.Square();
					ECFieldElement b4 = ecfieldElement6.Multiply(b3);
					ecfieldElement = ecfieldElement.Multiply(b3);
					ecfieldElement2 = ecfieldElement2.Multiply(b4);
				}
				break;
			}
			default:
				throw new InvalidOperationException("unsupported coordinate system");
			}
			ECFieldElement other = rawXCoord.Square().Add(ecfieldElement).Multiply(rawXCoord).Add(ecfieldElement2);
			return ecfieldElement3.Equals(other);
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x000E85F0 File Offset: 0x000E85F0
		public override ECPoint Subtract(ECPoint b)
		{
			if (b.IsInfinity)
			{
				return this;
			}
			return this.Add(b.Negate());
		}
	}
}
