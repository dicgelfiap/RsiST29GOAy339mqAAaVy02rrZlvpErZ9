using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.Field
{
	// Token: 0x0200061D RID: 1565
	internal class GenericPolynomialExtensionField : IPolynomialExtensionField, IExtensionField, IFiniteField
	{
		// Token: 0x06003529 RID: 13609 RVA: 0x0011820C File Offset: 0x0011820C
		internal GenericPolynomialExtensionField(IFiniteField subfield, IPolynomial polynomial)
		{
			this.subfield = subfield;
			this.minimalPolynomial = polynomial;
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x0600352A RID: 13610 RVA: 0x00118224 File Offset: 0x00118224
		public virtual BigInteger Characteristic
		{
			get
			{
				return this.subfield.Characteristic;
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x0600352B RID: 13611 RVA: 0x00118234 File Offset: 0x00118234
		public virtual int Dimension
		{
			get
			{
				return this.subfield.Dimension * this.minimalPolynomial.Degree;
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x0600352C RID: 13612 RVA: 0x00118250 File Offset: 0x00118250
		public virtual IFiniteField Subfield
		{
			get
			{
				return this.subfield;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x0600352D RID: 13613 RVA: 0x00118258 File Offset: 0x00118258
		public virtual int Degree
		{
			get
			{
				return this.minimalPolynomial.Degree;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x0600352E RID: 13614 RVA: 0x00118268 File Offset: 0x00118268
		public virtual IPolynomial MinimalPolynomial
		{
			get
			{
				return this.minimalPolynomial;
			}
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x00118270 File Offset: 0x00118270
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			GenericPolynomialExtensionField genericPolynomialExtensionField = obj as GenericPolynomialExtensionField;
			return genericPolynomialExtensionField != null && this.subfield.Equals(genericPolynomialExtensionField.subfield) && this.minimalPolynomial.Equals(genericPolynomialExtensionField.minimalPolynomial);
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x001182C4 File Offset: 0x001182C4
		public override int GetHashCode()
		{
			return this.subfield.GetHashCode() ^ Integers.RotateLeft(this.minimalPolynomial.GetHashCode(), 16);
		}

		// Token: 0x04001D19 RID: 7449
		protected readonly IFiniteField subfield;

		// Token: 0x04001D1A RID: 7450
		protected readonly IPolynomial minimalPolynomial;
	}
}
