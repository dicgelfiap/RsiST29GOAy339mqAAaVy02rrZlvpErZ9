using System;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x02000610 RID: 1552
	public class FpPoint : AbstractFpPoint
	{
		// Token: 0x060034B0 RID: 13488 RVA: 0x00114094 File Offset: 0x00114094
		[Obsolete("Use ECCurve.CreatePoint to construct points")]
		public FpPoint(ECCurve curve, ECFieldElement x, ECFieldElement y) : this(curve, x, y, false)
		{
		}

		// Token: 0x060034B1 RID: 13489 RVA: 0x001140A0 File Offset: 0x001140A0
		[Obsolete("Per-point compression property will be removed, see GetEncoded(bool)")]
		public FpPoint(ECCurve curve, ECFieldElement x, ECFieldElement y, bool withCompression) : base(curve, x, y, withCompression)
		{
			if (x == null != (y == null))
			{
				throw new ArgumentException("Exactly one of the field elements is null");
			}
		}

		// Token: 0x060034B2 RID: 13490 RVA: 0x001140C8 File Offset: 0x001140C8
		internal FpPoint(ECCurve curve, ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression) : base(curve, x, y, zs, withCompression)
		{
		}

		// Token: 0x060034B3 RID: 13491 RVA: 0x001140D8 File Offset: 0x001140D8
		protected override ECPoint Detach()
		{
			return new FpPoint(null, this.AffineXCoord, this.AffineYCoord, false);
		}

		// Token: 0x060034B4 RID: 13492 RVA: 0x001140F0 File Offset: 0x001140F0
		public override ECFieldElement GetZCoord(int index)
		{
			if (index == 1 && 4 == this.CurveCoordinateSystem)
			{
				return this.GetJacobianModifiedW();
			}
			return base.GetZCoord(index);
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x00114114 File Offset: 0x00114114
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
			if (this == b)
			{
				return this.Twice();
			}
			ECCurve curve = this.Curve;
			int coordinateSystem = curve.CoordinateSystem;
			ECFieldElement rawXCoord = base.RawXCoord;
			ECFieldElement rawYCoord = base.RawYCoord;
			ECFieldElement rawXCoord2 = b.RawXCoord;
			ECFieldElement rawYCoord2 = b.RawYCoord;
			switch (coordinateSystem)
			{
			case 0:
			{
				ECFieldElement ecfieldElement = rawXCoord2.Subtract(rawXCoord);
				ECFieldElement ecfieldElement2 = rawYCoord2.Subtract(rawYCoord);
				if (!ecfieldElement.IsZero)
				{
					ECFieldElement ecfieldElement3 = ecfieldElement2.Divide(ecfieldElement);
					ECFieldElement ecfieldElement4 = ecfieldElement3.Square().Subtract(rawXCoord).Subtract(rawXCoord2);
					ECFieldElement y = ecfieldElement3.Multiply(rawXCoord.Subtract(ecfieldElement4)).Subtract(rawYCoord);
					return new FpPoint(this.Curve, ecfieldElement4, y, base.IsCompressed);
				}
				if (ecfieldElement2.IsZero)
				{
					return this.Twice();
				}
				return this.Curve.Infinity;
			}
			case 1:
			{
				ECFieldElement ecfieldElement5 = base.RawZCoords[0];
				ECFieldElement ecfieldElement6 = b.RawZCoords[0];
				bool isOne = ecfieldElement5.IsOne;
				bool isOne2 = ecfieldElement6.IsOne;
				ECFieldElement ecfieldElement7 = isOne ? rawYCoord2 : rawYCoord2.Multiply(ecfieldElement5);
				ECFieldElement ecfieldElement8 = isOne2 ? rawYCoord : rawYCoord.Multiply(ecfieldElement6);
				ECFieldElement ecfieldElement9 = ecfieldElement7.Subtract(ecfieldElement8);
				ECFieldElement ecfieldElement10 = isOne ? rawXCoord2 : rawXCoord2.Multiply(ecfieldElement5);
				ECFieldElement b2 = isOne2 ? rawXCoord : rawXCoord.Multiply(ecfieldElement6);
				ECFieldElement ecfieldElement11 = ecfieldElement10.Subtract(b2);
				if (!ecfieldElement11.IsZero)
				{
					ECFieldElement b3 = isOne ? ecfieldElement6 : (isOne2 ? ecfieldElement5 : ecfieldElement5.Multiply(ecfieldElement6));
					ECFieldElement ecfieldElement12 = ecfieldElement11.Square();
					ECFieldElement ecfieldElement13 = ecfieldElement12.Multiply(ecfieldElement11);
					ECFieldElement ecfieldElement14 = ecfieldElement12.Multiply(b2);
					ECFieldElement b4 = ecfieldElement9.Square().Multiply(b3).Subtract(ecfieldElement13).Subtract(this.Two(ecfieldElement14));
					ECFieldElement x = ecfieldElement11.Multiply(b4);
					ECFieldElement y2 = ecfieldElement14.Subtract(b4).MultiplyMinusProduct(ecfieldElement9, ecfieldElement8, ecfieldElement13);
					ECFieldElement ecfieldElement15 = ecfieldElement13.Multiply(b3);
					return new FpPoint(curve, x, y2, new ECFieldElement[]
					{
						ecfieldElement15
					}, base.IsCompressed);
				}
				if (ecfieldElement9.IsZero)
				{
					return this.Twice();
				}
				return curve.Infinity;
			}
			case 2:
			case 4:
			{
				ECFieldElement ecfieldElement16 = base.RawZCoords[0];
				ECFieldElement ecfieldElement17 = b.RawZCoords[0];
				bool isOne3 = ecfieldElement16.IsOne;
				ECFieldElement zsquared = null;
				ECFieldElement ecfieldElement22;
				ECFieldElement y3;
				ECFieldElement ecfieldElement23;
				if (!isOne3 && ecfieldElement16.Equals(ecfieldElement17))
				{
					ECFieldElement ecfieldElement18 = rawXCoord.Subtract(rawXCoord2);
					ECFieldElement ecfieldElement19 = rawYCoord.Subtract(rawYCoord2);
					if (ecfieldElement18.IsZero)
					{
						if (ecfieldElement19.IsZero)
						{
							return this.Twice();
						}
						return curve.Infinity;
					}
					else
					{
						ECFieldElement ecfieldElement20 = ecfieldElement18.Square();
						ECFieldElement ecfieldElement21 = rawXCoord.Multiply(ecfieldElement20);
						ECFieldElement b5 = rawXCoord2.Multiply(ecfieldElement20);
						ECFieldElement b6 = ecfieldElement21.Subtract(b5).Multiply(rawYCoord);
						ecfieldElement22 = ecfieldElement19.Square().Subtract(ecfieldElement21).Subtract(b5);
						y3 = ecfieldElement21.Subtract(ecfieldElement22).Multiply(ecfieldElement19).Subtract(b6);
						ecfieldElement23 = ecfieldElement18;
						if (isOne3)
						{
							zsquared = ecfieldElement20;
						}
						else
						{
							ecfieldElement23 = ecfieldElement23.Multiply(ecfieldElement16);
						}
					}
				}
				else
				{
					ECFieldElement b7;
					ECFieldElement b8;
					if (isOne3)
					{
						b7 = rawXCoord2;
						b8 = rawYCoord2;
					}
					else
					{
						ECFieldElement ecfieldElement24 = ecfieldElement16.Square();
						b7 = ecfieldElement24.Multiply(rawXCoord2);
						ECFieldElement ecfieldElement25 = ecfieldElement24.Multiply(ecfieldElement16);
						b8 = ecfieldElement25.Multiply(rawYCoord2);
					}
					bool isOne4 = ecfieldElement17.IsOne;
					ECFieldElement ecfieldElement26;
					ECFieldElement ecfieldElement27;
					if (isOne4)
					{
						ecfieldElement26 = rawXCoord;
						ecfieldElement27 = rawYCoord;
					}
					else
					{
						ECFieldElement ecfieldElement28 = ecfieldElement17.Square();
						ecfieldElement26 = ecfieldElement28.Multiply(rawXCoord);
						ECFieldElement ecfieldElement29 = ecfieldElement28.Multiply(ecfieldElement17);
						ecfieldElement27 = ecfieldElement29.Multiply(rawYCoord);
					}
					ECFieldElement ecfieldElement30 = ecfieldElement26.Subtract(b7);
					ECFieldElement ecfieldElement31 = ecfieldElement27.Subtract(b8);
					if (ecfieldElement30.IsZero)
					{
						if (ecfieldElement31.IsZero)
						{
							return this.Twice();
						}
						return curve.Infinity;
					}
					else
					{
						ECFieldElement ecfieldElement32 = ecfieldElement30.Square();
						ECFieldElement ecfieldElement33 = ecfieldElement32.Multiply(ecfieldElement30);
						ECFieldElement ecfieldElement34 = ecfieldElement32.Multiply(ecfieldElement26);
						ecfieldElement22 = ecfieldElement31.Square().Add(ecfieldElement33).Subtract(this.Two(ecfieldElement34));
						y3 = ecfieldElement34.Subtract(ecfieldElement22).MultiplyMinusProduct(ecfieldElement31, ecfieldElement33, ecfieldElement27);
						ecfieldElement23 = ecfieldElement30;
						if (!isOne3)
						{
							ecfieldElement23 = ecfieldElement23.Multiply(ecfieldElement16);
						}
						if (!isOne4)
						{
							ecfieldElement23 = ecfieldElement23.Multiply(ecfieldElement17);
						}
						if (ecfieldElement23 == ecfieldElement30)
						{
							zsquared = ecfieldElement32;
						}
					}
				}
				ECFieldElement[] zs;
				if (coordinateSystem == 4)
				{
					ECFieldElement ecfieldElement35 = this.CalculateJacobianModifiedW(ecfieldElement23, zsquared);
					zs = new ECFieldElement[]
					{
						ecfieldElement23,
						ecfieldElement35
					};
				}
				else
				{
					zs = new ECFieldElement[]
					{
						ecfieldElement23
					};
				}
				return new FpPoint(curve, ecfieldElement22, y3, zs, base.IsCompressed);
			}
			}
			throw new InvalidOperationException("unsupported coordinate system");
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x00114660 File Offset: 0x00114660
		public override ECPoint Twice()
		{
			if (base.IsInfinity)
			{
				return this;
			}
			ECCurve curve = this.Curve;
			ECFieldElement rawYCoord = base.RawYCoord;
			if (rawYCoord.IsZero)
			{
				return curve.Infinity;
			}
			int coordinateSystem = curve.CoordinateSystem;
			ECFieldElement rawXCoord = base.RawXCoord;
			switch (coordinateSystem)
			{
			case 0:
			{
				ECFieldElement x = rawXCoord.Square();
				ECFieldElement ecfieldElement = this.Three(x).Add(this.Curve.A).Divide(this.Two(rawYCoord));
				ECFieldElement ecfieldElement2 = ecfieldElement.Square().Subtract(this.Two(rawXCoord));
				ECFieldElement y = ecfieldElement.Multiply(rawXCoord.Subtract(ecfieldElement2)).Subtract(rawYCoord);
				return new FpPoint(this.Curve, ecfieldElement2, y, base.IsCompressed);
			}
			case 1:
			{
				ECFieldElement ecfieldElement3 = base.RawZCoords[0];
				bool isOne = ecfieldElement3.IsOne;
				ECFieldElement ecfieldElement4 = curve.A;
				if (!ecfieldElement4.IsZero && !isOne)
				{
					ecfieldElement4 = ecfieldElement4.Multiply(ecfieldElement3.Square());
				}
				ecfieldElement4 = ecfieldElement4.Add(this.Three(rawXCoord.Square()));
				ECFieldElement ecfieldElement5 = isOne ? rawYCoord : rawYCoord.Multiply(ecfieldElement3);
				ECFieldElement ecfieldElement6 = isOne ? rawYCoord.Square() : ecfieldElement5.Multiply(rawYCoord);
				ECFieldElement x2 = rawXCoord.Multiply(ecfieldElement6);
				ECFieldElement ecfieldElement7 = this.Four(x2);
				ECFieldElement ecfieldElement8 = ecfieldElement4.Square().Subtract(this.Two(ecfieldElement7));
				ECFieldElement ecfieldElement9 = this.Two(ecfieldElement5);
				ECFieldElement x3 = ecfieldElement8.Multiply(ecfieldElement9);
				ECFieldElement ecfieldElement10 = this.Two(ecfieldElement6);
				ECFieldElement y2 = ecfieldElement7.Subtract(ecfieldElement8).Multiply(ecfieldElement4).Subtract(this.Two(ecfieldElement10.Square()));
				ECFieldElement x4 = isOne ? this.Two(ecfieldElement10) : ecfieldElement9.Square();
				ECFieldElement ecfieldElement11 = this.Two(x4).Multiply(ecfieldElement5);
				return new FpPoint(curve, x3, y2, new ECFieldElement[]
				{
					ecfieldElement11
				}, base.IsCompressed);
			}
			case 2:
			{
				ECFieldElement ecfieldElement12 = base.RawZCoords[0];
				bool isOne2 = ecfieldElement12.IsOne;
				ECFieldElement ecfieldElement13 = rawYCoord.Square();
				ECFieldElement x5 = ecfieldElement13.Square();
				ECFieldElement a = curve.A;
				ECFieldElement ecfieldElement14 = a.Negate();
				ECFieldElement ecfieldElement15;
				ECFieldElement ecfieldElement16;
				if (ecfieldElement14.ToBigInteger().Equals(BigInteger.ValueOf(3L)))
				{
					ECFieldElement b = isOne2 ? ecfieldElement12 : ecfieldElement12.Square();
					ecfieldElement15 = this.Three(rawXCoord.Add(b).Multiply(rawXCoord.Subtract(b)));
					ecfieldElement16 = this.Four(ecfieldElement13.Multiply(rawXCoord));
				}
				else
				{
					ECFieldElement x6 = rawXCoord.Square();
					ecfieldElement15 = this.Three(x6);
					if (isOne2)
					{
						ecfieldElement15 = ecfieldElement15.Add(a);
					}
					else if (!a.IsZero)
					{
						ECFieldElement ecfieldElement17 = isOne2 ? ecfieldElement12 : ecfieldElement12.Square();
						ECFieldElement ecfieldElement18 = ecfieldElement17.Square();
						if (ecfieldElement14.BitLength < a.BitLength)
						{
							ecfieldElement15 = ecfieldElement15.Subtract(ecfieldElement18.Multiply(ecfieldElement14));
						}
						else
						{
							ecfieldElement15 = ecfieldElement15.Add(ecfieldElement18.Multiply(a));
						}
					}
					ecfieldElement16 = this.Four(rawXCoord.Multiply(ecfieldElement13));
				}
				ECFieldElement ecfieldElement19 = ecfieldElement15.Square().Subtract(this.Two(ecfieldElement16));
				ECFieldElement y3 = ecfieldElement16.Subtract(ecfieldElement19).Multiply(ecfieldElement15).Subtract(this.Eight(x5));
				ECFieldElement ecfieldElement20 = this.Two(rawYCoord);
				if (!isOne2)
				{
					ecfieldElement20 = ecfieldElement20.Multiply(ecfieldElement12);
				}
				return new FpPoint(curve, ecfieldElement19, y3, new ECFieldElement[]
				{
					ecfieldElement20
				}, base.IsCompressed);
			}
			case 4:
				return this.TwiceJacobianModified(true);
			}
			throw new InvalidOperationException("unsupported coordinate system");
		}

		// Token: 0x060034B7 RID: 13495 RVA: 0x00114A4C File Offset: 0x00114A4C
		public override ECPoint TwicePlus(ECPoint b)
		{
			if (this == b)
			{
				return this.ThreeTimes();
			}
			if (base.IsInfinity)
			{
				return b;
			}
			if (b.IsInfinity)
			{
				return this.Twice();
			}
			ECFieldElement rawYCoord = base.RawYCoord;
			if (rawYCoord.IsZero)
			{
				return b;
			}
			ECCurve curve = this.Curve;
			int coordinateSystem = curve.CoordinateSystem;
			int num = coordinateSystem;
			if (num != 0)
			{
				if (num != 4)
				{
					return this.Twice().Add(b);
				}
				return this.TwiceJacobianModified(false).Add(b);
			}
			else
			{
				ECFieldElement rawXCoord = base.RawXCoord;
				ECFieldElement rawXCoord2 = b.RawXCoord;
				ECFieldElement rawYCoord2 = b.RawYCoord;
				ECFieldElement ecfieldElement = rawXCoord2.Subtract(rawXCoord);
				ECFieldElement ecfieldElement2 = rawYCoord2.Subtract(rawYCoord);
				if (ecfieldElement.IsZero)
				{
					if (ecfieldElement2.IsZero)
					{
						return this.ThreeTimes();
					}
					return this;
				}
				else
				{
					ECFieldElement ecfieldElement3 = ecfieldElement.Square();
					ECFieldElement b2 = ecfieldElement2.Square();
					ECFieldElement ecfieldElement4 = ecfieldElement3.Multiply(this.Two(rawXCoord).Add(rawXCoord2)).Subtract(b2);
					if (ecfieldElement4.IsZero)
					{
						return this.Curve.Infinity;
					}
					ECFieldElement ecfieldElement5 = ecfieldElement4.Multiply(ecfieldElement);
					ECFieldElement b3 = ecfieldElement5.Invert();
					ECFieldElement ecfieldElement6 = ecfieldElement4.Multiply(b3).Multiply(ecfieldElement2);
					ECFieldElement ecfieldElement7 = this.Two(rawYCoord).Multiply(ecfieldElement3).Multiply(ecfieldElement).Multiply(b3).Subtract(ecfieldElement6);
					ECFieldElement ecfieldElement8 = ecfieldElement7.Subtract(ecfieldElement6).Multiply(ecfieldElement6.Add(ecfieldElement7)).Add(rawXCoord2);
					ECFieldElement y = rawXCoord.Subtract(ecfieldElement8).Multiply(ecfieldElement7).Subtract(rawYCoord);
					return new FpPoint(this.Curve, ecfieldElement8, y, base.IsCompressed);
				}
			}
		}

		// Token: 0x060034B8 RID: 13496 RVA: 0x00114C0C File Offset: 0x00114C0C
		public override ECPoint ThreeTimes()
		{
			if (base.IsInfinity)
			{
				return this;
			}
			ECFieldElement rawYCoord = base.RawYCoord;
			if (rawYCoord.IsZero)
			{
				return this;
			}
			ECCurve curve = this.Curve;
			int coordinateSystem = curve.CoordinateSystem;
			int num = coordinateSystem;
			if (num != 0)
			{
				if (num != 4)
				{
					return this.Twice().Add(this);
				}
				return this.TwiceJacobianModified(false).Add(this);
			}
			else
			{
				ECFieldElement rawXCoord = base.RawXCoord;
				ECFieldElement ecfieldElement = this.Two(rawYCoord);
				ECFieldElement ecfieldElement2 = ecfieldElement.Square();
				ECFieldElement ecfieldElement3 = this.Three(rawXCoord.Square()).Add(this.Curve.A);
				ECFieldElement b = ecfieldElement3.Square();
				ECFieldElement ecfieldElement4 = this.Three(rawXCoord).Multiply(ecfieldElement2).Subtract(b);
				if (ecfieldElement4.IsZero)
				{
					return this.Curve.Infinity;
				}
				ECFieldElement ecfieldElement5 = ecfieldElement4.Multiply(ecfieldElement);
				ECFieldElement b2 = ecfieldElement5.Invert();
				ECFieldElement ecfieldElement6 = ecfieldElement4.Multiply(b2).Multiply(ecfieldElement3);
				ECFieldElement ecfieldElement7 = ecfieldElement2.Square().Multiply(b2).Subtract(ecfieldElement6);
				ECFieldElement ecfieldElement8 = ecfieldElement7.Subtract(ecfieldElement6).Multiply(ecfieldElement6.Add(ecfieldElement7)).Add(rawXCoord);
				ECFieldElement y = rawXCoord.Subtract(ecfieldElement8).Multiply(ecfieldElement7).Subtract(rawYCoord);
				return new FpPoint(this.Curve, ecfieldElement8, y, base.IsCompressed);
			}
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x00114D78 File Offset: 0x00114D78
		public override ECPoint TimesPow2(int e)
		{
			if (e < 0)
			{
				throw new ArgumentException("cannot be negative", "e");
			}
			if (e == 0 || base.IsInfinity)
			{
				return this;
			}
			if (e == 1)
			{
				return this.Twice();
			}
			ECCurve curve = this.Curve;
			ECFieldElement ecfieldElement = base.RawYCoord;
			if (ecfieldElement.IsZero)
			{
				return curve.Infinity;
			}
			int coordinateSystem = curve.CoordinateSystem;
			ECFieldElement ecfieldElement2 = curve.A;
			ECFieldElement ecfieldElement3 = base.RawXCoord;
			ECFieldElement ecfieldElement4 = (base.RawZCoords.Length < 1) ? curve.FromBigInteger(BigInteger.One) : base.RawZCoords[0];
			if (!ecfieldElement4.IsOne)
			{
				switch (coordinateSystem)
				{
				case 1:
				{
					ECFieldElement ecfieldElement5 = ecfieldElement4.Square();
					ecfieldElement3 = ecfieldElement3.Multiply(ecfieldElement4);
					ecfieldElement = ecfieldElement.Multiply(ecfieldElement5);
					ecfieldElement2 = this.CalculateJacobianModifiedW(ecfieldElement4, ecfieldElement5);
					break;
				}
				case 2:
					ecfieldElement2 = this.CalculateJacobianModifiedW(ecfieldElement4, null);
					break;
				case 4:
					ecfieldElement2 = this.GetJacobianModifiedW();
					break;
				}
			}
			for (int i = 0; i < e; i++)
			{
				if (ecfieldElement.IsZero)
				{
					return curve.Infinity;
				}
				ECFieldElement x = ecfieldElement3.Square();
				ECFieldElement ecfieldElement6 = this.Three(x);
				ECFieldElement ecfieldElement7 = this.Two(ecfieldElement);
				ECFieldElement ecfieldElement8 = ecfieldElement7.Multiply(ecfieldElement);
				ECFieldElement ecfieldElement9 = this.Two(ecfieldElement3.Multiply(ecfieldElement8));
				ECFieldElement x2 = ecfieldElement8.Square();
				ECFieldElement ecfieldElement10 = this.Two(x2);
				if (!ecfieldElement2.IsZero)
				{
					ecfieldElement6 = ecfieldElement6.Add(ecfieldElement2);
					ecfieldElement2 = this.Two(ecfieldElement10.Multiply(ecfieldElement2));
				}
				ecfieldElement3 = ecfieldElement6.Square().Subtract(this.Two(ecfieldElement9));
				ecfieldElement = ecfieldElement6.Multiply(ecfieldElement9.Subtract(ecfieldElement3)).Subtract(ecfieldElement10);
				ecfieldElement4 = (ecfieldElement4.IsOne ? ecfieldElement7 : ecfieldElement7.Multiply(ecfieldElement4));
			}
			switch (coordinateSystem)
			{
			case 0:
			{
				ECFieldElement ecfieldElement11 = ecfieldElement4.Invert();
				ECFieldElement ecfieldElement12 = ecfieldElement11.Square();
				ECFieldElement b = ecfieldElement12.Multiply(ecfieldElement11);
				return new FpPoint(curve, ecfieldElement3.Multiply(ecfieldElement12), ecfieldElement.Multiply(b), base.IsCompressed);
			}
			case 1:
				ecfieldElement3 = ecfieldElement3.Multiply(ecfieldElement4);
				ecfieldElement4 = ecfieldElement4.Multiply(ecfieldElement4.Square());
				return new FpPoint(curve, ecfieldElement3, ecfieldElement, new ECFieldElement[]
				{
					ecfieldElement4
				}, base.IsCompressed);
			case 2:
				return new FpPoint(curve, ecfieldElement3, ecfieldElement, new ECFieldElement[]
				{
					ecfieldElement4
				}, base.IsCompressed);
			case 4:
				return new FpPoint(curve, ecfieldElement3, ecfieldElement, new ECFieldElement[]
				{
					ecfieldElement4,
					ecfieldElement2
				}, base.IsCompressed);
			}
			throw new InvalidOperationException("unsupported coordinate system");
		}

		// Token: 0x060034BA RID: 13498 RVA: 0x00115070 File Offset: 0x00115070
		protected virtual ECFieldElement Two(ECFieldElement x)
		{
			return x.Add(x);
		}

		// Token: 0x060034BB RID: 13499 RVA: 0x0011507C File Offset: 0x0011507C
		protected virtual ECFieldElement Three(ECFieldElement x)
		{
			return this.Two(x).Add(x);
		}

		// Token: 0x060034BC RID: 13500 RVA: 0x0011508C File Offset: 0x0011508C
		protected virtual ECFieldElement Four(ECFieldElement x)
		{
			return this.Two(this.Two(x));
		}

		// Token: 0x060034BD RID: 13501 RVA: 0x0011509C File Offset: 0x0011509C
		protected virtual ECFieldElement Eight(ECFieldElement x)
		{
			return this.Four(this.Two(x));
		}

		// Token: 0x060034BE RID: 13502 RVA: 0x001150AC File Offset: 0x001150AC
		protected virtual ECFieldElement DoubleProductFromSquares(ECFieldElement a, ECFieldElement b, ECFieldElement aSquared, ECFieldElement bSquared)
		{
			return a.Add(b).Square().Subtract(aSquared).Subtract(bSquared);
		}

		// Token: 0x060034BF RID: 13503 RVA: 0x001150D8 File Offset: 0x001150D8
		public override ECPoint Negate()
		{
			if (base.IsInfinity)
			{
				return this;
			}
			ECCurve curve = this.Curve;
			int coordinateSystem = curve.CoordinateSystem;
			if (coordinateSystem != 0)
			{
				return new FpPoint(curve, base.RawXCoord, base.RawYCoord.Negate(), base.RawZCoords, base.IsCompressed);
			}
			return new FpPoint(curve, base.RawXCoord, base.RawYCoord.Negate(), base.IsCompressed);
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x0011514C File Offset: 0x0011514C
		protected virtual ECFieldElement CalculateJacobianModifiedW(ECFieldElement Z, ECFieldElement ZSquared)
		{
			ECFieldElement a = this.Curve.A;
			if (a.IsZero || Z.IsOne)
			{
				return a;
			}
			if (ZSquared == null)
			{
				ZSquared = Z.Square();
			}
			ECFieldElement ecfieldElement = ZSquared.Square();
			ECFieldElement ecfieldElement2 = a.Negate();
			if (ecfieldElement2.BitLength < a.BitLength)
			{
				ecfieldElement = ecfieldElement.Multiply(ecfieldElement2).Negate();
			}
			else
			{
				ecfieldElement = ecfieldElement.Multiply(a);
			}
			return ecfieldElement;
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x001151CC File Offset: 0x001151CC
		protected virtual ECFieldElement GetJacobianModifiedW()
		{
			ECFieldElement[] rawZCoords = base.RawZCoords;
			ECFieldElement ecfieldElement = rawZCoords[1];
			if (ecfieldElement == null)
			{
				ecfieldElement = (rawZCoords[1] = this.CalculateJacobianModifiedW(rawZCoords[0], null));
			}
			return ecfieldElement;
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x00115208 File Offset: 0x00115208
		protected virtual FpPoint TwiceJacobianModified(bool calculateW)
		{
			ECFieldElement rawXCoord = base.RawXCoord;
			ECFieldElement rawYCoord = base.RawYCoord;
			ECFieldElement ecfieldElement = base.RawZCoords[0];
			ECFieldElement jacobianModifiedW = this.GetJacobianModifiedW();
			ECFieldElement x = rawXCoord.Square();
			ECFieldElement ecfieldElement2 = this.Three(x).Add(jacobianModifiedW);
			ECFieldElement ecfieldElement3 = this.Two(rawYCoord);
			ECFieldElement ecfieldElement4 = ecfieldElement3.Multiply(rawYCoord);
			ECFieldElement ecfieldElement5 = this.Two(rawXCoord.Multiply(ecfieldElement4));
			ECFieldElement ecfieldElement6 = ecfieldElement2.Square().Subtract(this.Two(ecfieldElement5));
			ECFieldElement x2 = ecfieldElement4.Square();
			ECFieldElement ecfieldElement7 = this.Two(x2);
			ECFieldElement y = ecfieldElement2.Multiply(ecfieldElement5.Subtract(ecfieldElement6)).Subtract(ecfieldElement7);
			ECFieldElement ecfieldElement8 = calculateW ? this.Two(ecfieldElement7.Multiply(jacobianModifiedW)) : null;
			ECFieldElement ecfieldElement9 = ecfieldElement.IsOne ? ecfieldElement3 : ecfieldElement3.Multiply(ecfieldElement);
			return new FpPoint(this.Curve, ecfieldElement6, y, new ECFieldElement[]
			{
				ecfieldElement9,
				ecfieldElement8
			}, base.IsCompressed);
		}
	}
}
