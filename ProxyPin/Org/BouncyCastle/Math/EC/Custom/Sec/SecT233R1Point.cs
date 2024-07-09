using System;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005D0 RID: 1488
	internal class SecT233R1Point : AbstractF2mPoint
	{
		// Token: 0x0600313C RID: 12604 RVA: 0x00100A30 File Offset: 0x00100A30
		public SecT233R1Point(ECCurve curve, ECFieldElement x, ECFieldElement y) : this(curve, x, y, false)
		{
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x00100A3C File Offset: 0x00100A3C
		public SecT233R1Point(ECCurve curve, ECFieldElement x, ECFieldElement y, bool withCompression) : base(curve, x, y, withCompression)
		{
			if (x == null != (y == null))
			{
				throw new ArgumentException("Exactly one of the field elements is null");
			}
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x00100A64 File Offset: 0x00100A64
		internal SecT233R1Point(ECCurve curve, ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression) : base(curve, x, y, zs, withCompression)
		{
		}

		// Token: 0x0600313F RID: 12607 RVA: 0x00100A74 File Offset: 0x00100A74
		protected override ECPoint Detach()
		{
			return new SecT233R1Point(null, this.AffineXCoord, this.AffineYCoord);
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06003140 RID: 12608 RVA: 0x00100A88 File Offset: 0x00100A88
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

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06003141 RID: 12609 RVA: 0x00100AF0 File Offset: 0x00100AF0
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

		// Token: 0x06003142 RID: 12610 RVA: 0x00100B30 File Offset: 0x00100B30
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
						ecfieldElement11 = ecfieldElement10.Square().Add(ecfieldElement10).Add(ecfieldElement).AddOne();
						if (ecfieldElement11.IsZero)
						{
							return new SecT233R1Point(curve, ecfieldElement11, curve.B.Sqrt(), base.IsCompressed);
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
							return new SecT233R1Point(curve, ecfieldElement11, curve.B.Sqrt(), base.IsCompressed);
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
					return new SecT233R1Point(curve, ecfieldElement11, y, new ECFieldElement[]
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

		// Token: 0x06003143 RID: 12611 RVA: 0x00100DD4 File Offset: 0x00100DD4
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
			ECFieldElement ecfieldElement3 = rawYCoord.Square().Add(ecfieldElement2).Add(b);
			if (ecfieldElement3.IsZero)
			{
				return new SecT233R1Point(curve, ecfieldElement3, curve.B.Sqrt(), base.IsCompressed);
			}
			ECFieldElement ecfieldElement4 = ecfieldElement3.Square();
			ECFieldElement ecfieldElement5 = isOne ? ecfieldElement3 : ecfieldElement3.Multiply(b);
			ECFieldElement ecfieldElement6 = isOne ? rawXCoord : rawXCoord.Multiply(ecfieldElement);
			ECFieldElement y = ecfieldElement6.SquarePlusProduct(ecfieldElement3, ecfieldElement2).Add(ecfieldElement4).Add(ecfieldElement5);
			return new SecT233R1Point(curve, ecfieldElement4, y, new ECFieldElement[]
			{
				ecfieldElement5
			}, base.IsCompressed);
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x00100F0C File Offset: 0x00100F0C
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
			ECFieldElement b4 = ecfieldElement3.Add(b2).Add(b3);
			ECFieldElement ecfieldElement4 = rawYCoord2.Multiply(ecfieldElement3).Add(b2).MultiplyPlusProduct(b4, x, ecfieldElement3);
			ECFieldElement ecfieldElement5 = rawXCoord2.Multiply(ecfieldElement3);
			ECFieldElement ecfieldElement6 = ecfieldElement5.Add(b4).Square();
			if (ecfieldElement6.IsZero)
			{
				if (ecfieldElement4.IsZero)
				{
					return b.Twice();
				}
				return curve.Infinity;
			}
			else
			{
				if (ecfieldElement4.IsZero)
				{
					return new SecT233R1Point(curve, ecfieldElement4, curve.B.Sqrt(), base.IsCompressed);
				}
				ECFieldElement x2 = ecfieldElement4.Square().Multiply(ecfieldElement5);
				ECFieldElement ecfieldElement7 = ecfieldElement4.Multiply(ecfieldElement6).Multiply(ecfieldElement3);
				ECFieldElement y = ecfieldElement4.Add(ecfieldElement6).Square().MultiplyPlusProduct(b4, rawYCoord2.AddOne(), ecfieldElement7);
				return new SecT233R1Point(curve, x2, y, new ECFieldElement[]
				{
					ecfieldElement7
				}, base.IsCompressed);
			}
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x001010CC File Offset: 0x001010CC
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
			return new SecT233R1Point(this.Curve, rawXCoord, rawYCoord.Add(ecfieldElement), new ECFieldElement[]
			{
				ecfieldElement
			}, base.IsCompressed);
		}
	}
}
