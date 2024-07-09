using System;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005C8 RID: 1480
	internal class SecT193R1Point : AbstractF2mPoint
	{
		// Token: 0x060030B4 RID: 12468 RVA: 0x000FE278 File Offset: 0x000FE278
		public SecT193R1Point(ECCurve curve, ECFieldElement x, ECFieldElement y) : this(curve, x, y, false)
		{
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x000FE284 File Offset: 0x000FE284
		public SecT193R1Point(ECCurve curve, ECFieldElement x, ECFieldElement y, bool withCompression) : base(curve, x, y, withCompression)
		{
			if (x == null != (y == null))
			{
				throw new ArgumentException("Exactly one of the field elements is null");
			}
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x000FE2AC File Offset: 0x000FE2AC
		internal SecT193R1Point(ECCurve curve, ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression) : base(curve, x, y, zs, withCompression)
		{
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x000FE2BC File Offset: 0x000FE2BC
		protected override ECPoint Detach()
		{
			return new SecT193R1Point(null, this.AffineXCoord, this.AffineYCoord);
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x060030B8 RID: 12472 RVA: 0x000FE2D0 File Offset: 0x000FE2D0
		public override ECFieldElement YCoord
		{
			get
			{
				ECFieldElement rawXCoord = base.RawXCoord;
				ECFieldElement rawYCoord = base.RawYCoord;
				if (base.IsInfinity || rawXCoord.IsZero)
				{
					return rawYCoord;
				}
				ECFieldElement ecfieldElement = rawYCoord.Add(rawXCoord).Multiply(rawXCoord);
				ECFieldElement ecfieldElement2 = base.RawZCoords[0];
				if (!ecfieldElement2.IsOne)
				{
					ecfieldElement = ecfieldElement.Divide(ecfieldElement2);
				}
				return ecfieldElement;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x060030B9 RID: 12473 RVA: 0x000FE338 File Offset: 0x000FE338
		protected internal override bool CompressionYTilde
		{
			get
			{
				ECFieldElement rawXCoord = base.RawXCoord;
				if (rawXCoord.IsZero)
				{
					return false;
				}
				ECFieldElement rawYCoord = base.RawYCoord;
				return rawYCoord.TestBitZero() != rawXCoord.TestBitZero();
			}
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x000FE378 File Offset: 0x000FE378
		public override ECPoint Add(ECPoint b)
		{
			if (base.IsInfinity)
			{
				return b;
			}
			if (b.IsInfinity)
			{
				return this;
			}
			ECCurve curve = this.Curve;
			ECFieldElement ecfieldElement = base.RawXCoord;
			ECFieldElement rawXCoord = b.RawXCoord;
			if (ecfieldElement.IsZero)
			{
				if (rawXCoord.IsZero)
				{
					return curve.Infinity;
				}
				return b.Add(this);
			}
			else
			{
				ECFieldElement rawYCoord = base.RawYCoord;
				ECFieldElement ecfieldElement2 = base.RawZCoords[0];
				ECFieldElement rawYCoord2 = b.RawYCoord;
				ECFieldElement ecfieldElement3 = b.RawZCoords[0];
				bool isOne = ecfieldElement2.IsOne;
				ECFieldElement ecfieldElement4 = rawXCoord;
				ECFieldElement ecfieldElement5 = rawYCoord2;
				if (!isOne)
				{
					ecfieldElement4 = ecfieldElement4.Multiply(ecfieldElement2);
					ecfieldElement5 = ecfieldElement5.Multiply(ecfieldElement2);
				}
				bool isOne2 = ecfieldElement3.IsOne;
				ECFieldElement ecfieldElement6 = ecfieldElement;
				ECFieldElement ecfieldElement7 = rawYCoord;
				if (!isOne2)
				{
					ecfieldElement6 = ecfieldElement6.Multiply(ecfieldElement3);
					ecfieldElement7 = ecfieldElement7.Multiply(ecfieldElement3);
				}
				ECFieldElement ecfieldElement8 = ecfieldElement7.Add(ecfieldElement5);
				ECFieldElement ecfieldElement9 = ecfieldElement6.Add(ecfieldElement4);
				if (!ecfieldElement9.IsZero)
				{
					ECFieldElement ecfieldElement11;
					ECFieldElement y;
					ECFieldElement ecfieldElement13;
					if (rawXCoord.IsZero)
					{
						ECPoint ecpoint = this.Normalize();
						ecfieldElement = ecpoint.XCoord;
						ECFieldElement ycoord = ecpoint.YCoord;
						ECFieldElement b2 = rawYCoord2;
						ECFieldElement ecfieldElement10 = ycoord.Add(b2).Divide(ecfieldElement);
						ecfieldElement11 = ecfieldElement10.Square().Add(ecfieldElement10).Add(ecfieldElement).Add(curve.A);
						if (ecfieldElement11.IsZero)
						{
							return new SecT193R1Point(curve, ecfieldElement11, curve.B.Sqrt(), base.IsCompressed);
						}
						ECFieldElement ecfieldElement12 = ecfieldElement10.Multiply(ecfieldElement.Add(ecfieldElement11)).Add(ecfieldElement11).Add(ycoord);
						y = ecfieldElement12.Divide(ecfieldElement11).Add(ecfieldElement11);
						ecfieldElement13 = curve.FromBigInteger(BigInteger.One);
					}
					else
					{
						ecfieldElement9 = ecfieldElement9.Square();
						ECFieldElement ecfieldElement14 = ecfieldElement8.Multiply(ecfieldElement6);
						ECFieldElement ecfieldElement15 = ecfieldElement8.Multiply(ecfieldElement4);
						ecfieldElement11 = ecfieldElement14.Multiply(ecfieldElement15);
						if (ecfieldElement11.IsZero)
						{
							return new SecT193R1Point(curve, ecfieldElement11, curve.B.Sqrt(), base.IsCompressed);
						}
						ECFieldElement ecfieldElement16 = ecfieldElement8.Multiply(ecfieldElement9);
						if (!isOne2)
						{
							ecfieldElement16 = ecfieldElement16.Multiply(ecfieldElement3);
						}
						y = ecfieldElement15.Add(ecfieldElement9).SquarePlusProduct(ecfieldElement16, rawYCoord.Add(ecfieldElement2));
						ecfieldElement13 = ecfieldElement16;
						if (!isOne)
						{
							ecfieldElement13 = ecfieldElement13.Multiply(ecfieldElement2);
						}
					}
					return new SecT193R1Point(curve, ecfieldElement11, y, new ECFieldElement[]
					{
						ecfieldElement13
					}, base.IsCompressed);
				}
				if (ecfieldElement8.IsZero)
				{
					return this.Twice();
				}
				return curve.Infinity;
			}
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x000FE624 File Offset: 0x000FE624
		public override ECPoint Twice()
		{
			if (base.IsInfinity)
			{
				return this;
			}
			ECCurve curve = this.Curve;
			ECFieldElement rawXCoord = base.RawXCoord;
			if (rawXCoord.IsZero)
			{
				return curve.Infinity;
			}
			ECFieldElement rawYCoord = base.RawYCoord;
			ECFieldElement ecfieldElement = base.RawZCoords[0];
			bool isOne = ecfieldElement.IsOne;
			ECFieldElement ecfieldElement2 = isOne ? rawYCoord : rawYCoord.Multiply(ecfieldElement);
			ECFieldElement b = isOne ? ecfieldElement : ecfieldElement.Square();
			ECFieldElement a = curve.A;
			ECFieldElement b2 = isOne ? a : a.Multiply(b);
			ECFieldElement ecfieldElement3 = rawYCoord.Square().Add(ecfieldElement2).Add(b2);
			if (ecfieldElement3.IsZero)
			{
				return new SecT193R1Point(curve, ecfieldElement3, curve.B.Sqrt(), base.IsCompressed);
			}
			ECFieldElement ecfieldElement4 = ecfieldElement3.Square();
			ECFieldElement ecfieldElement5 = isOne ? ecfieldElement3 : ecfieldElement3.Multiply(b);
			ECFieldElement ecfieldElement6 = isOne ? rawXCoord : rawXCoord.Multiply(ecfieldElement);
			ECFieldElement y = ecfieldElement6.SquarePlusProduct(ecfieldElement3, ecfieldElement2).Add(ecfieldElement4).Add(ecfieldElement5);
			return new SecT193R1Point(curve, ecfieldElement4, y, new ECFieldElement[]
			{
				ecfieldElement5
			}, base.IsCompressed);
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x000FE780 File Offset: 0x000FE780
		public override ECPoint TwicePlus(ECPoint b)
		{
			if (base.IsInfinity)
			{
				return b;
			}
			if (b.IsInfinity)
			{
				return this.Twice();
			}
			ECCurve curve = this.Curve;
			ECFieldElement rawXCoord = base.RawXCoord;
			if (rawXCoord.IsZero)
			{
				return b;
			}
			ECFieldElement rawXCoord2 = b.RawXCoord;
			ECFieldElement ecfieldElement = b.RawZCoords[0];
			if (rawXCoord2.IsZero || !ecfieldElement.IsOne)
			{
				return this.Twice().Add(b);
			}
			ECFieldElement rawYCoord = base.RawYCoord;
			ECFieldElement ecfieldElement2 = base.RawZCoords[0];
			ECFieldElement rawYCoord2 = b.RawYCoord;
			ECFieldElement x = rawXCoord.Square();
			ECFieldElement b2 = rawYCoord.Square();
			ECFieldElement ecfieldElement3 = ecfieldElement2.Square();
			ECFieldElement b3 = rawYCoord.Multiply(ecfieldElement2);
			ECFieldElement b4 = curve.A.Multiply(ecfieldElement3).Add(b2).Add(b3);
			ECFieldElement ecfieldElement4 = rawYCoord2.AddOne();
			ECFieldElement ecfieldElement5 = curve.A.Add(ecfieldElement4).Multiply(ecfieldElement3).Add(b2).MultiplyPlusProduct(b4, x, ecfieldElement3);
			ECFieldElement ecfieldElement6 = rawXCoord2.Multiply(ecfieldElement3);
			ECFieldElement ecfieldElement7 = ecfieldElement6.Add(b4).Square();
			if (ecfieldElement7.IsZero)
			{
				if (ecfieldElement5.IsZero)
				{
					return b.Twice();
				}
				return curve.Infinity;
			}
			else
			{
				if (ecfieldElement5.IsZero)
				{
					return new SecT193R1Point(curve, ecfieldElement5, curve.B.Sqrt(), base.IsCompressed);
				}
				ECFieldElement x2 = ecfieldElement5.Square().Multiply(ecfieldElement6);
				ECFieldElement ecfieldElement8 = ecfieldElement5.Multiply(ecfieldElement7).Multiply(ecfieldElement3);
				ECFieldElement y = ecfieldElement5.Add(ecfieldElement7).Square().MultiplyPlusProduct(b4, ecfieldElement4, ecfieldElement8);
				return new SecT193R1Point(curve, x2, y, new ECFieldElement[]
				{
					ecfieldElement8
				}, base.IsCompressed);
			}
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x000FE95C File Offset: 0x000FE95C
		public override ECPoint Negate()
		{
			if (base.IsInfinity)
			{
				return this;
			}
			ECFieldElement rawXCoord = base.RawXCoord;
			if (rawXCoord.IsZero)
			{
				return this;
			}
			ECFieldElement rawYCoord = base.RawYCoord;
			ECFieldElement ecfieldElement = base.RawZCoords[0];
			return new SecT193R1Point(this.Curve, rawXCoord, rawYCoord.Add(ecfieldElement), new ECFieldElement[]
			{
				ecfieldElement
			}, base.IsCompressed);
		}
	}
}
