using System;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x0200060C RID: 1548
	public class FpCurve : AbstractFpCurve
	{
		// Token: 0x0600344E RID: 13390 RVA: 0x001128E4 File Offset: 0x001128E4
		[Obsolete("Use constructor taking order/cofactor")]
		public FpCurve(BigInteger q, BigInteger a, BigInteger b) : this(q, a, b, null, null)
		{
		}

		// Token: 0x0600344F RID: 13391 RVA: 0x001128F4 File Offset: 0x001128F4
		public FpCurve(BigInteger q, BigInteger a, BigInteger b, BigInteger order, BigInteger cofactor) : base(q)
		{
			this.m_q = q;
			this.m_r = FpFieldElement.CalculateResidue(q);
			this.m_infinity = new FpPoint(this, null, null, false);
			this.m_a = this.FromBigInteger(a);
			this.m_b = this.FromBigInteger(b);
			this.m_order = order;
			this.m_cofactor = cofactor;
			this.m_coord = 4;
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x00112960 File Offset: 0x00112960
		[Obsolete("Use constructor taking order/cofactor")]
		protected FpCurve(BigInteger q, BigInteger r, ECFieldElement a, ECFieldElement b) : this(q, r, a, b, null, null)
		{
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x00112970 File Offset: 0x00112970
		protected FpCurve(BigInteger q, BigInteger r, ECFieldElement a, ECFieldElement b, BigInteger order, BigInteger cofactor) : base(q)
		{
			this.m_q = q;
			this.m_r = r;
			this.m_infinity = new FpPoint(this, null, null, false);
			this.m_a = a;
			this.m_b = b;
			this.m_order = order;
			this.m_cofactor = cofactor;
			this.m_coord = 4;
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x001129CC File Offset: 0x001129CC
		protected override ECCurve CloneCurve()
		{
			return new FpCurve(this.m_q, this.m_r, this.m_a, this.m_b, this.m_order, this.m_cofactor);
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x001129F8 File Offset: 0x001129F8
		public override bool SupportsCoordinateSystem(int coord)
		{
			switch (coord)
			{
			case 0:
			case 1:
			case 2:
			case 4:
				return true;
			}
			return false;
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06003454 RID: 13396 RVA: 0x00112A30 File Offset: 0x00112A30
		public virtual BigInteger Q
		{
			get
			{
				return this.m_q;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06003455 RID: 13397 RVA: 0x00112A38 File Offset: 0x00112A38
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06003456 RID: 13398 RVA: 0x00112A40 File Offset: 0x00112A40
		public override int FieldSize
		{
			get
			{
				return this.m_q.BitLength;
			}
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x00112A50 File Offset: 0x00112A50
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new FpFieldElement(this.m_q, this.m_r, x);
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x00112A64 File Offset: 0x00112A64
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new FpPoint(this, x, y, withCompression);
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x00112A70 File Offset: 0x00112A70
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new FpPoint(this, x, y, zs, withCompression);
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x00112A80 File Offset: 0x00112A80
		public override ECPoint ImportPoint(ECPoint p)
		{
			if (this != p.Curve && this.CoordinateSystem == 2 && !p.IsInfinity)
			{
				switch (p.Curve.CoordinateSystem)
				{
				case 2:
				case 3:
				case 4:
					return new FpPoint(this, this.FromBigInteger(p.RawXCoord.ToBigInteger()), this.FromBigInteger(p.RawYCoord.ToBigInteger()), new ECFieldElement[]
					{
						this.FromBigInteger(p.GetZCoord(0).ToBigInteger())
					}, p.IsCompressed);
				}
			}
			return base.ImportPoint(p);
		}

		// Token: 0x04001CF6 RID: 7414
		private const int FP_DEFAULT_COORDS = 4;

		// Token: 0x04001CF7 RID: 7415
		protected readonly BigInteger m_q;

		// Token: 0x04001CF8 RID: 7416
		protected readonly BigInteger m_r;

		// Token: 0x04001CF9 RID: 7417
		protected readonly FpPoint m_infinity;
	}
}
