using System;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x020005B3 RID: 1459
	public abstract class AbstractF2mPoint : ECPointBase
	{
		// Token: 0x06002F47 RID: 12103 RVA: 0x000F78C8 File Offset: 0x000F78C8
		protected AbstractF2mPoint(ECCurve curve, ECFieldElement x, ECFieldElement y, bool withCompression) : base(curve, x, y, withCompression)
		{
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x000F78D8 File Offset: 0x000F78D8
		protected AbstractF2mPoint(ECCurve curve, ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression) : base(curve, x, y, zs, withCompression)
		{
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x000F78E8 File Offset: 0x000F78E8
		protected override bool SatisfiesCurveEquation()
		{
			ECCurve curve = this.Curve;
			ECFieldElement rawXCoord = base.RawXCoord;
			ECFieldElement rawYCoord = base.RawYCoord;
			ECFieldElement ecfieldElement = curve.A;
			ECFieldElement ecfieldElement2 = curve.B;
			int coordinateSystem = curve.CoordinateSystem;
			ECFieldElement ecfieldElement4;
			ECFieldElement ecfieldElement5;
			if (coordinateSystem == 6)
			{
				ECFieldElement ecfieldElement3 = base.RawZCoords[0];
				bool isOne = ecfieldElement3.IsOne;
				if (rawXCoord.IsZero)
				{
					ecfieldElement4 = rawYCoord.Square();
					ecfieldElement5 = ecfieldElement2;
					if (!isOne)
					{
						ECFieldElement b = ecfieldElement3.Square();
						ecfieldElement5 = ecfieldElement5.Multiply(b);
					}
				}
				else
				{
					ECFieldElement ecfieldElement6 = rawYCoord;
					ECFieldElement ecfieldElement7 = rawXCoord.Square();
					if (isOne)
					{
						ecfieldElement4 = ecfieldElement6.Square().Add(ecfieldElement6).Add(ecfieldElement);
						ecfieldElement5 = ecfieldElement7.Square().Add(ecfieldElement2);
					}
					else
					{
						ECFieldElement ecfieldElement8 = ecfieldElement3.Square();
						ECFieldElement y = ecfieldElement8.Square();
						ecfieldElement4 = ecfieldElement6.Add(ecfieldElement3).MultiplyPlusProduct(ecfieldElement6, ecfieldElement, ecfieldElement8);
						ecfieldElement5 = ecfieldElement7.SquarePlusProduct(ecfieldElement2, y);
					}
					ecfieldElement4 = ecfieldElement4.Multiply(ecfieldElement7);
				}
			}
			else
			{
				ecfieldElement4 = rawYCoord.Add(rawXCoord).Multiply(rawYCoord);
				switch (coordinateSystem)
				{
				case 0:
					break;
				case 1:
				{
					ECFieldElement ecfieldElement9 = base.RawZCoords[0];
					if (!ecfieldElement9.IsOne)
					{
						ECFieldElement b2 = ecfieldElement9.Square();
						ECFieldElement b3 = ecfieldElement9.Multiply(b2);
						ecfieldElement4 = ecfieldElement4.Multiply(ecfieldElement9);
						ecfieldElement = ecfieldElement.Multiply(ecfieldElement9);
						ecfieldElement2 = ecfieldElement2.Multiply(b3);
					}
					break;
				}
				default:
					throw new InvalidOperationException("unsupported coordinate system");
				}
				ecfieldElement5 = rawXCoord.Add(ecfieldElement).Multiply(rawXCoord.Square()).Add(ecfieldElement2);
			}
			return ecfieldElement4.Equals(ecfieldElement5);
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x000F7AA4 File Offset: 0x000F7AA4
		protected override bool SatisfiesOrder()
		{
			ECCurve curve = this.Curve;
			BigInteger cofactor = curve.Cofactor;
			if (BigInteger.Two.Equals(cofactor))
			{
				ECPoint ecpoint = this.Normalize();
				ECFieldElement affineXCoord = ecpoint.AffineXCoord;
				return 0 != ((AbstractF2mFieldElement)affineXCoord).Trace();
			}
			if (!BigInteger.ValueOf(4L).Equals(cofactor))
			{
				return base.SatisfiesOrder();
			}
			ECPoint ecpoint2 = this.Normalize();
			ECFieldElement affineXCoord2 = ecpoint2.AffineXCoord;
			ECFieldElement ecfieldElement = ((AbstractF2mCurve)curve).SolveQuadraticEquation(affineXCoord2.Add(curve.A));
			if (ecfieldElement == null)
			{
				return false;
			}
			ECFieldElement affineYCoord = ecpoint2.AffineYCoord;
			ECFieldElement ecfieldElement2 = affineXCoord2.Multiply(ecfieldElement).Add(affineYCoord);
			return 0 == ((AbstractF2mFieldElement)ecfieldElement2).Trace();
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x000F7B6C File Offset: 0x000F7B6C
		public override ECPoint ScaleX(ECFieldElement scale)
		{
			if (base.IsInfinity)
			{
				return this;
			}
			switch (this.CurveCoordinateSystem)
			{
			case 5:
			{
				ECFieldElement rawXCoord = base.RawXCoord;
				ECFieldElement rawYCoord = base.RawYCoord;
				ECFieldElement b = rawXCoord.Multiply(scale);
				ECFieldElement y = rawYCoord.Add(rawXCoord).Divide(scale).Add(b);
				return this.Curve.CreateRawPoint(rawXCoord, y, base.RawZCoords, base.IsCompressed);
			}
			case 6:
			{
				ECFieldElement rawXCoord2 = base.RawXCoord;
				ECFieldElement rawYCoord2 = base.RawYCoord;
				ECFieldElement ecfieldElement = base.RawZCoords[0];
				ECFieldElement b2 = rawXCoord2.Multiply(scale.Square());
				ECFieldElement y2 = rawYCoord2.Add(rawXCoord2).Add(b2);
				ECFieldElement ecfieldElement2 = ecfieldElement.Multiply(scale);
				return this.Curve.CreateRawPoint(rawXCoord2, y2, new ECFieldElement[]
				{
					ecfieldElement2
				}, base.IsCompressed);
			}
			default:
				return base.ScaleX(scale);
			}
		}

		// Token: 0x06002F4C RID: 12108 RVA: 0x000F7C68 File Offset: 0x000F7C68
		public override ECPoint ScaleXNegateY(ECFieldElement scale)
		{
			return this.ScaleX(scale);
		}

		// Token: 0x06002F4D RID: 12109 RVA: 0x000F7C74 File Offset: 0x000F7C74
		public override ECPoint ScaleY(ECFieldElement scale)
		{
			if (base.IsInfinity)
			{
				return this;
			}
			switch (this.CurveCoordinateSystem)
			{
			case 5:
			case 6:
			{
				ECFieldElement rawXCoord = base.RawXCoord;
				ECFieldElement rawYCoord = base.RawYCoord;
				ECFieldElement y = rawYCoord.Add(rawXCoord).Multiply(scale).Add(rawXCoord);
				return this.Curve.CreateRawPoint(rawXCoord, y, base.RawZCoords, base.IsCompressed);
			}
			default:
				return base.ScaleY(scale);
			}
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x000F7CF4 File Offset: 0x000F7CF4
		public override ECPoint ScaleYNegateX(ECFieldElement scale)
		{
			return this.ScaleY(scale);
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x000F7D00 File Offset: 0x000F7D00
		public override ECPoint Subtract(ECPoint b)
		{
			if (b.IsInfinity)
			{
				return this;
			}
			return this.Add(b.Negate());
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x000F7D1C File Offset: 0x000F7D1C
		public virtual AbstractF2mPoint Tau()
		{
			if (base.IsInfinity)
			{
				return this;
			}
			ECCurve curve = this.Curve;
			int coordinateSystem = curve.CoordinateSystem;
			ECFieldElement rawXCoord = base.RawXCoord;
			switch (coordinateSystem)
			{
			case 0:
			case 5:
			{
				ECFieldElement rawYCoord = base.RawYCoord;
				return (AbstractF2mPoint)curve.CreateRawPoint(rawXCoord.Square(), rawYCoord.Square(), base.IsCompressed);
			}
			case 1:
			case 6:
			{
				ECFieldElement rawYCoord2 = base.RawYCoord;
				ECFieldElement ecfieldElement = base.RawZCoords[0];
				return (AbstractF2mPoint)curve.CreateRawPoint(rawXCoord.Square(), rawYCoord2.Square(), new ECFieldElement[]
				{
					ecfieldElement.Square()
				}, base.IsCompressed);
			}
			}
			throw new InvalidOperationException("unsupported coordinate system");
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x000F7DF8 File Offset: 0x000F7DF8
		public virtual AbstractF2mPoint TauPow(int pow)
		{
			if (base.IsInfinity)
			{
				return this;
			}
			ECCurve curve = this.Curve;
			int coordinateSystem = curve.CoordinateSystem;
			ECFieldElement rawXCoord = base.RawXCoord;
			switch (coordinateSystem)
			{
			case 0:
			case 5:
			{
				ECFieldElement rawYCoord = base.RawYCoord;
				return (AbstractF2mPoint)curve.CreateRawPoint(rawXCoord.SquarePow(pow), rawYCoord.SquarePow(pow), base.IsCompressed);
			}
			case 1:
			case 6:
			{
				ECFieldElement rawYCoord2 = base.RawYCoord;
				ECFieldElement ecfieldElement = base.RawZCoords[0];
				return (AbstractF2mPoint)curve.CreateRawPoint(rawXCoord.SquarePow(pow), rawYCoord2.SquarePow(pow), new ECFieldElement[]
				{
					ecfieldElement.SquarePow(pow)
				}, base.IsCompressed);
			}
			}
			throw new InvalidOperationException("unsupported coordinate system");
		}
	}
}
