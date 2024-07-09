using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.Field
{
	// Token: 0x0200061F RID: 1567
	internal class GF2Polynomial : IPolynomial
	{
		// Token: 0x06003533 RID: 13619 RVA: 0x001182E4 File Offset: 0x001182E4
		internal GF2Polynomial(int[] exponents)
		{
			this.exponents = Arrays.Clone(exponents);
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06003534 RID: 13620 RVA: 0x001182F8 File Offset: 0x001182F8
		public virtual int Degree
		{
			get
			{
				return this.exponents[this.exponents.Length - 1];
			}
		}

		// Token: 0x06003535 RID: 13621 RVA: 0x0011830C File Offset: 0x0011830C
		public virtual int[] GetExponentsPresent()
		{
			return Arrays.Clone(this.exponents);
		}

		// Token: 0x06003536 RID: 13622 RVA: 0x0011831C File Offset: 0x0011831C
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			GF2Polynomial gf2Polynomial = obj as GF2Polynomial;
			return gf2Polynomial != null && Arrays.AreEqual(this.exponents, gf2Polynomial.exponents);
		}

		// Token: 0x06003537 RID: 13623 RVA: 0x00118358 File Offset: 0x00118358
		public override int GetHashCode()
		{
			return Arrays.GetHashCode(this.exponents);
		}

		// Token: 0x04001D1B RID: 7451
		protected readonly int[] exponents;
	}
}
