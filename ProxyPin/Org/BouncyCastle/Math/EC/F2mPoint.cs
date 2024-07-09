using System;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x02000611 RID: 1553
	public class F2mPoint : AbstractF2mPoint
	{
		// Token: 0x060034C3 RID: 13507 RVA: 0x00115324 File Offset: 0x00115324
		[Obsolete("Use ECCurve.CreatePoint to construct points")]
		public F2mPoint(ECCurve curve, ECFieldElement x, ECFieldElement y) : this(curve, x, y, false)
		{
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x00115330 File Offset: 0x00115330
		[Obsolete("Per-point compression property will be removed, see GetEncoded(bool)")]
		public F2mPoint(ECCurve curve, ECFieldElement x, ECFieldElement y, bool withCompression) : base(curve, x, y, withCompression)
		{
			if (x == null != (y == null))
			{
				throw new ArgumentException("Exactly one of the field elements is null");
			}
			if (x != null)
			{
				F2mFieldElement.CheckFieldElements(x, y);
				if (curve != null)
				{
					F2mFieldElement.CheckFieldElements(x, curve.A);
				}
			}
		}

		// Token: 0x060034C5 RID: 13509 RVA: 0x00115384 File Offset: 0x00115384
		internal F2mPoint(ECCurve curve, ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression) : base(curve, x, y, zs, withCompression)
		{
		}

		// Token: 0x060034C6 RID: 13510 RVA: 0x00115394 File Offset: 0x00115394
		protected override ECPoint Detach()
		{
			return new F2mPoint(null, this.AffineXCoord, this.AffineYCoord, false);
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x060034C7 RID: 13511 RVA: 0x001153AC File Offset: 0x001153AC
		public override ECFieldElement YCoord
		{
			get
			{
				int curveCoordinateSystem = this.CurveCoordinateSystem;
				switch (curveCoordinateSystem)
				{
				case 5:
				case 6:
				{
					ECFieldElement rawXCoord = base.RawXCoord;
					ECFieldElement rawYCoord = base.RawYCoord;
					if (base.IsInfinity || rawXCoord.IsZero)
					{
						return rawYCoord;
					}
					ECFieldElement ecfieldElement = rawYCoord.Add(rawXCoord).Multiply(rawXCoord);
					if (6 == curveCoordinateSystem)
					{
						ECFieldElement ecfieldElement2 = base.RawZCoords[0];
						if (!ecfieldElement2.IsOne)
						{
							ecfieldElement = ecfieldElement.Divide(ecfieldElement2);
						}
					}
					return ecfieldElement;
				}
				default:
					return base.RawYCoord;
				}
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x060034C8 RID: 13512 RVA: 0x00115448 File Offset: 0x00115448
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
				switch (this.CurveCoordinateSystem)
				{
				case 5:
				case 6:
					return rawYCoord.TestBitZero() != rawXCoord.TestBitZero();
				default:
					return rawYCoord.Divide(rawXCoord).TestBitZero();
				}
			}
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x001154B0 File Offset: 0x001154B0
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
			int coordinateSystem = curve.CoordinateSystem;
			ECFieldElement rawXCoord = base.RawXCoord;
			ECFieldElement rawXCoord2 = b.RawXCoord;
			int num = coordinateSystem;
			switch (num)
			{
			case 0:
			{
				ECFieldElement rawYCoord = base.RawYCoord;
				ECFieldElement rawYCoord2 = b.RawYCoord;
				ECFieldElement ecfieldElement = rawXCoord.Add(rawXCoord2);
				ECFieldElement ecfieldElement2 = rawYCoord.Add(rawYCoord2);
				if (!ecfieldElement.IsZero)
				{
					ECFieldElement ecfieldElement3 = ecfieldElement2.Divide(ecfieldElement);
					ECFieldElement ecfieldElement4 = ecfieldElement3.Square().Add(ecfieldElement3).Add(ecfieldElement).Add(curve.A);
					ECFieldElement y = ecfieldElement3.Multiply(rawXCoord.Add(ecfieldElement4)).Add(ecfieldElement4).Add(rawYCoord);
					return new F2mPoint(curve, ecfieldElement4, y, base.IsCompressed);
				}
				if (ecfieldElement2.IsZero)
				{
					return this.Twice();
				}
				return curve.Infinity;
			}
			case 1:
			{
				ECFieldElement rawYCoord3 = base.RawYCoord;
				ECFieldElement ecfieldElement5 = base.RawZCoords[0];
				ECFieldElement rawYCoord4 = b.RawYCoord;
				ECFieldElement ecfieldElement6 = b.RawZCoords[0];
				bool isOne = ecfieldElement5.IsOne;
				ECFieldElement ecfieldElement7 = rawYCoord4;
				ECFieldElement ecfieldElement8 = rawXCoord2;
				if (!isOne)
				{
					ecfieldElement7 = ecfieldElement7.Multiply(ecfieldElement5);
					ecfieldElement8 = ecfieldElement8.Multiply(ecfieldElement5);
				}
				bool isOne2 = ecfieldElement6.IsOne;
				ECFieldElement ecfieldElement9 = rawYCoord3;
				ECFieldElement ecfieldElement10 = rawXCoord;
				if (!isOne2)
				{
					ecfieldElement9 = ecfieldElement9.Multiply(ecfieldElement6);
					ecfieldElement10 = ecfieldElement10.Multiply(ecfieldElement6);
				}
				ECFieldElement ecfieldElement11 = ecfieldElement7.Add(ecfieldElement9);
				ECFieldElement ecfieldElement12 = ecfieldElement8.Add(ecfieldElement10);
				if (!ecfieldElement12.IsZero)
				{
					ECFieldElement ecfieldElement13 = ecfieldElement12.Square();
					ECFieldElement ecfieldElement14 = ecfieldElement13.Multiply(ecfieldElement12);
					ECFieldElement b2 = isOne ? ecfieldElement6 : (isOne2 ? ecfieldElement5 : ecfieldElement5.Multiply(ecfieldElement6));
					ECFieldElement ecfieldElement15 = ecfieldElement11.Add(ecfieldElement12);
					ECFieldElement ecfieldElement16 = ecfieldElement15.MultiplyPlusProduct(ecfieldElement11, ecfieldElement13, curve.A).Multiply(b2).Add(ecfieldElement14);
					ECFieldElement x = ecfieldElement12.Multiply(ecfieldElement16);
					ECFieldElement b3 = isOne2 ? ecfieldElement13 : ecfieldElement13.Multiply(ecfieldElement6);
					ECFieldElement y2 = ecfieldElement11.MultiplyPlusProduct(rawXCoord, ecfieldElement12, rawYCoord3).MultiplyPlusProduct(b3, ecfieldElement15, ecfieldElement16);
					ECFieldElement ecfieldElement17 = ecfieldElement14.Multiply(b2);
					return new F2mPoint(curve, x, y2, new ECFieldElement[]
					{
						ecfieldElement17
					}, base.IsCompressed);
				}
				if (ecfieldElement11.IsZero)
				{
					return this.Twice();
				}
				return curve.Infinity;
			}
			default:
				if (num != 6)
				{
					throw new InvalidOperationException("unsupported coordinate system");
				}
				if (rawXCoord.IsZero)
				{
					if (rawXCoord2.IsZero)
					{
						return curve.Infinity;
					}
					return b.Add(this);
				}
				else
				{
					ECFieldElement rawYCoord5 = base.RawYCoord;
					ECFieldElement ecfieldElement18 = base.RawZCoords[0];
					ECFieldElement rawYCoord6 = b.RawYCoord;
					ECFieldElement ecfieldElement19 = b.RawZCoords[0];
					bool isOne3 = ecfieldElement18.IsOne;
					ECFieldElement ecfieldElement20 = rawXCoord2;
					ECFieldElement ecfieldElement21 = rawYCoord6;
					if (!isOne3)
					{
						ecfieldElement20 = ecfieldElement20.Multiply(ecfieldElement18);
						ecfieldElement21 = ecfieldElement21.Multiply(ecfieldElement18);
					}
					bool isOne4 = ecfieldElement19.IsOne;
					ECFieldElement ecfieldElement22 = rawXCoord;
					ECFieldElement ecfieldElement23 = rawYCoord5;
					if (!isOne4)
					{
						ecfieldElement22 = ecfieldElement22.Multiply(ecfieldElement19);
						ecfieldElement23 = ecfieldElement23.Multiply(ecfieldElement19);
					}
					ECFieldElement ecfieldElement24 = ecfieldElement23.Add(ecfieldElement21);
					ECFieldElement ecfieldElement25 = ecfieldElement22.Add(ecfieldElement20);
					if (!ecfieldElement25.IsZero)
					{
						ECFieldElement ecfieldElement27;
						ECFieldElement y3;
						ECFieldElement ecfieldElement29;
						if (rawXCoord2.IsZero)
						{
							ECPoint ecpoint = this.Normalize();
							rawXCoord = ecpoint.RawXCoord;
							ECFieldElement ycoord = ecpoint.YCoord;
							ECFieldElement b4 = rawYCoord6;
							ECFieldElement ecfieldElement26 = ycoord.Add(b4).Divide(rawXCoord);
							ecfieldElement27 = ecfieldElement26.Square().Add(ecfieldElement26).Add(rawXCoord).Add(curve.A);
							if (ecfieldElement27.IsZero)
							{
								return new F2mPoint(curve, ecfieldElement27, curve.B.Sqrt(), base.IsCompressed);
							}
							ECFieldElement ecfieldElement28 = ecfieldElement26.Multiply(rawXCoord.Add(ecfieldElement27)).Add(ecfieldElement27).Add(ycoord);
							y3 = ecfieldElement28.Divide(ecfieldElement27).Add(ecfieldElement27);
							ecfieldElement29 = curve.FromBigInteger(BigInteger.One);
						}
						else
						{
							ecfieldElement25 = ecfieldElement25.Square();
							ECFieldElement ecfieldElement30 = ecfieldElement24.Multiply(ecfieldElement22);
							ECFieldElement ecfieldElement31 = ecfieldElement24.Multiply(ecfieldElement20);
							ecfieldElement27 = ecfieldElement30.Multiply(ecfieldElement31);
							if (ecfieldElement27.IsZero)
							{
								return new F2mPoint(curve, ecfieldElement27, curve.B.Sqrt(), base.IsCompressed);
							}
							ECFieldElement ecfieldElement32 = ecfieldElement24.Multiply(ecfieldElement25);
							if (!isOne4)
							{
								ecfieldElement32 = ecfieldElement32.Multiply(ecfieldElement19);
							}
							y3 = ecfieldElement31.Add(ecfieldElement25).SquarePlusProduct(ecfieldElement32, rawYCoord5.Add(ecfieldElement18));
							ecfieldElement29 = ecfieldElement32;
							if (!isOne3)
							{
								ecfieldElement29 = ecfieldElement29.Multiply(ecfieldElement18);
							}
						}
						return new F2mPoint(curve, ecfieldElement27, y3, new ECFieldElement[]
						{
							ecfieldElement29
						}, base.IsCompressed);
					}
					if (ecfieldElement24.IsZero)
					{
						return this.Twice();
					}
					return curve.Infinity;
				}
				break;
			}
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x001159CC File Offset: 0x001159CC
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
			int coordinateSystem = curve.CoordinateSystem;
			int num = coordinateSystem;
			switch (num)
			{
			case 0:
			{
				ECFieldElement rawYCoord = base.RawYCoord;
				ECFieldElement ecfieldElement = rawYCoord.Divide(rawXCoord).Add(rawXCoord);
				ECFieldElement x = ecfieldElement.Square().Add(ecfieldElement).Add(curve.A);
				ECFieldElement y = rawXCoord.SquarePlusProduct(x, ecfieldElement.AddOne());
				return new F2mPoint(curve, x, y, base.IsCompressed);
			}
			case 1:
			{
				ECFieldElement rawYCoord2 = base.RawYCoord;
				ECFieldElement ecfieldElement2 = base.RawZCoords[0];
				bool isOne = ecfieldElement2.IsOne;
				ECFieldElement ecfieldElement3 = isOne ? rawXCoord : rawXCoord.Multiply(ecfieldElement2);
				ECFieldElement b = isOne ? rawYCoord2 : rawYCoord2.Multiply(ecfieldElement2);
				ECFieldElement ecfieldElement4 = rawXCoord.Square();
				ECFieldElement ecfieldElement5 = ecfieldElement4.Add(b);
				ECFieldElement ecfieldElement6 = ecfieldElement3;
				ECFieldElement ecfieldElement7 = ecfieldElement6.Square();
				ECFieldElement ecfieldElement8 = ecfieldElement5.Add(ecfieldElement6);
				ECFieldElement ecfieldElement9 = ecfieldElement8.MultiplyPlusProduct(ecfieldElement5, ecfieldElement7, curve.A);
				ECFieldElement x2 = ecfieldElement6.Multiply(ecfieldElement9);
				ECFieldElement y2 = ecfieldElement4.Square().MultiplyPlusProduct(ecfieldElement6, ecfieldElement9, ecfieldElement8);
				ECFieldElement ecfieldElement10 = ecfieldElement6.Multiply(ecfieldElement7);
				return new F2mPoint(curve, x2, y2, new ECFieldElement[]
				{
					ecfieldElement10
				}, base.IsCompressed);
			}
			default:
			{
				if (num != 6)
				{
					throw new InvalidOperationException("unsupported coordinate system");
				}
				ECFieldElement rawYCoord3 = base.RawYCoord;
				ECFieldElement ecfieldElement11 = base.RawZCoords[0];
				bool isOne2 = ecfieldElement11.IsOne;
				ECFieldElement ecfieldElement12 = isOne2 ? rawYCoord3 : rawYCoord3.Multiply(ecfieldElement11);
				ECFieldElement ecfieldElement13 = isOne2 ? ecfieldElement11 : ecfieldElement11.Square();
				ECFieldElement a = curve.A;
				ECFieldElement ecfieldElement14 = isOne2 ? a : a.Multiply(ecfieldElement13);
				ECFieldElement ecfieldElement15 = rawYCoord3.Square().Add(ecfieldElement12).Add(ecfieldElement14);
				if (ecfieldElement15.IsZero)
				{
					return new F2mPoint(curve, ecfieldElement15, curve.B.Sqrt(), base.IsCompressed);
				}
				ECFieldElement ecfieldElement16 = ecfieldElement15.Square();
				ECFieldElement ecfieldElement17 = isOne2 ? ecfieldElement15 : ecfieldElement15.Multiply(ecfieldElement13);
				ECFieldElement b2 = curve.B;
				ECFieldElement ecfieldElement19;
				if (b2.BitLength < curve.FieldSize >> 1)
				{
					ECFieldElement ecfieldElement18 = rawYCoord3.Add(rawXCoord).Square();
					ECFieldElement b3;
					if (b2.IsOne)
					{
						b3 = ecfieldElement14.Add(ecfieldElement13).Square();
					}
					else
					{
						b3 = ecfieldElement14.SquarePlusProduct(b2, ecfieldElement13.Square());
					}
					ecfieldElement19 = ecfieldElement18.Add(ecfieldElement15).Add(ecfieldElement13).Multiply(ecfieldElement18).Add(b3).Add(ecfieldElement16);
					if (a.IsZero)
					{
						ecfieldElement19 = ecfieldElement19.Add(ecfieldElement17);
					}
					else if (!a.IsOne)
					{
						ecfieldElement19 = ecfieldElement19.Add(a.AddOne().Multiply(ecfieldElement17));
					}
				}
				else
				{
					ECFieldElement ecfieldElement20 = isOne2 ? rawXCoord : rawXCoord.Multiply(ecfieldElement11);
					ecfieldElement19 = ecfieldElement20.SquarePlusProduct(ecfieldElement15, ecfieldElement12).Add(ecfieldElement16).Add(ecfieldElement17);
				}
				return new F2mPoint(curve, ecfieldElement16, ecfieldElement19, new ECFieldElement[]
				{
					ecfieldElement17
				}, base.IsCompressed);
			}
			}
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x00115D58 File Offset: 0x00115D58
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
			int coordinateSystem = curve.CoordinateSystem;
			int num = coordinateSystem;
			if (num != 6)
			{
				return this.Twice().Add(b);
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
					return new F2mPoint(curve, ecfieldElement5, curve.B.Sqrt(), base.IsCompressed);
				}
				ECFieldElement x2 = ecfieldElement5.Square().Multiply(ecfieldElement6);
				ECFieldElement ecfieldElement8 = ecfieldElement5.Multiply(ecfieldElement7).Multiply(ecfieldElement3);
				ECFieldElement y = ecfieldElement5.Add(ecfieldElement7).Square().MultiplyPlusProduct(b4, ecfieldElement4, ecfieldElement8);
				return new F2mPoint(curve, x2, y, new ECFieldElement[]
				{
					ecfieldElement8
				}, base.IsCompressed);
			}
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x00115F54 File Offset: 0x00115F54
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
			ECCurve curve = this.Curve;
			switch (curve.CoordinateSystem)
			{
			case 0:
			{
				ECFieldElement rawYCoord = base.RawYCoord;
				return new F2mPoint(curve, rawXCoord, rawYCoord.Add(rawXCoord), base.IsCompressed);
			}
			case 1:
			{
				ECFieldElement rawYCoord2 = base.RawYCoord;
				ECFieldElement ecfieldElement = base.RawZCoords[0];
				return new F2mPoint(curve, rawXCoord, rawYCoord2.Add(rawXCoord), new ECFieldElement[]
				{
					ecfieldElement
				}, base.IsCompressed);
			}
			case 5:
			{
				ECFieldElement rawYCoord3 = base.RawYCoord;
				return new F2mPoint(curve, rawXCoord, rawYCoord3.AddOne(), base.IsCompressed);
			}
			case 6:
			{
				ECFieldElement rawYCoord4 = base.RawYCoord;
				ECFieldElement ecfieldElement2 = base.RawZCoords[0];
				return new F2mPoint(curve, rawXCoord, rawYCoord4.Add(ecfieldElement2), new ECFieldElement[]
				{
					ecfieldElement2
				}, base.IsCompressed);
			}
			}
			throw new InvalidOperationException("unsupported coordinate system");
		}
	}
}
