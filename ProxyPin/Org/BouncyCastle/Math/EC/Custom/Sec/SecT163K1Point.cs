using System;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005C0 RID: 1472
	internal class SecT163K1Point : AbstractF2mPoint
	{
		// Token: 0x0600302D RID: 12333 RVA: 0x000FBC24 File Offset: 0x000FBC24
		public SecT163K1Point(ECCurve curve, ECFieldElement x, ECFieldElement y) : this(curve, x, y, false)
		{
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x000FBC30 File Offset: 0x000FBC30
		public SecT163K1Point(ECCurve curve, ECFieldElement x, ECFieldElement y, bool withCompression) : base(curve, x, y, withCompression)
		{
			if (x == null != (y == null))
			{
				throw new ArgumentException("Exactly one of the field elements is null");
			}
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x000FBC58 File Offset: 0x000FBC58
		internal SecT163K1Point(ECCurve curve, ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression) : base(curve, x, y, zs, withCompression)
		{
		}

		// Token: 0x06003030 RID: 12336 RVA: 0x000FBC68 File Offset: 0x000FBC68
		protected override ECPoint Detach()
		{
			return new SecT163K1Point(null, this.AffineXCoord, this.AffineYCoord);
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06003031 RID: 12337 RVA: 0x000FBC7C File Offset: 0x000FBC7C
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

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06003032 RID: 12338 RVA: 0x000FBCE4 File Offset: 0x000FBCE4
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

		// Token: 0x06003033 RID: 12339 RVA: 0x000FBD24 File Offset: 0x000FBD24
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
							return new SecT163K1Point(curve, ecfieldElement11, curve.B, base.IsCompressed);
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
							return new SecT163K1Point(curve, ecfieldElement11, curve.B, base.IsCompressed);
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
					return new SecT163K1Point(curve, ecfieldElement11, y, new ECFieldElement[]
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

		// Token: 0x06003034 RID: 12340 RVA: 0x000FBFC0 File Offset: 0x000FBFC0
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
			ECFieldElement b = isOne ? rawYCoord : rawYCoord.Multiply(ecfieldElement);
			ECFieldElement b2 = isOne ? ecfieldElement : ecfieldElement.Square();
			ECFieldElement ecfieldElement2 = rawYCoord.Square().Add(b).Add(b2);
			if (ecfieldElement2.IsZero)
			{
				return new SecT163K1Point(curve, ecfieldElement2, curve.B, base.IsCompressed);
			}
			ECFieldElement ecfieldElement3 = ecfieldElement2.Square();
			ECFieldElement ecfieldElement4 = isOne ? ecfieldElement2 : ecfieldElement2.Multiply(b2);
			ECFieldElement ecfieldElement5 = rawYCoord.Add(rawXCoord).Square();
			ECFieldElement y = ecfieldElement5.Add(ecfieldElement2).Add(b2).Multiply(ecfieldElement5).Add(ecfieldElement3);
			return new SecT163K1Point(curve, ecfieldElement3, y, new ECFieldElement[]
			{
				ecfieldElement4
			}, base.IsCompressed);
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x000FC0F0 File Offset: 0x000FC0F0
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
					return new SecT163K1Point(curve, ecfieldElement4, curve.B, base.IsCompressed);
				}
				ECFieldElement x2 = ecfieldElement4.Square().Multiply(ecfieldElement5);
				ECFieldElement ecfieldElement7 = ecfieldElement4.Multiply(ecfieldElement6).Multiply(ecfieldElement3);
				ECFieldElement y = ecfieldElement4.Add(ecfieldElement6).Square().MultiplyPlusProduct(b4, rawYCoord2.AddOne(), ecfieldElement7);
				return new SecT163K1Point(curve, x2, y, new ECFieldElement[]
				{
					ecfieldElement7
				}, base.IsCompressed);
			}
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x000FC2AC File Offset: 0x000FC2AC
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
			return new SecT163K1Point(this.Curve, rawXCoord, rawYCoord.Add(ecfieldElement), new ECFieldElement[]
			{
				ecfieldElement
			}, base.IsCompressed);
		}
	}
}
