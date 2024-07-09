using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200043E RID: 1086
	public class DsaParameters : ICipherParameters
	{
		// Token: 0x06002233 RID: 8755 RVA: 0x000C4264 File Offset: 0x000C4264
		public DsaParameters(BigInteger p, BigInteger q, BigInteger g) : this(p, q, g, null)
		{
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x000C4270 File Offset: 0x000C4270
		public DsaParameters(BigInteger p, BigInteger q, BigInteger g, DsaValidationParameters parameters)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			if (q == null)
			{
				throw new ArgumentNullException("q");
			}
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			this.p = p;
			this.q = q;
			this.g = g;
			this.validation = parameters;
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x000C42D8 File Offset: 0x000C42D8
		public BigInteger P
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06002236 RID: 8758 RVA: 0x000C42E0 File Offset: 0x000C42E0
		public BigInteger Q
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06002237 RID: 8759 RVA: 0x000C42E8 File Offset: 0x000C42E8
		public BigInteger G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06002238 RID: 8760 RVA: 0x000C42F0 File Offset: 0x000C42F0
		public DsaValidationParameters ValidationParameters
		{
			get
			{
				return this.validation;
			}
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x000C42F8 File Offset: 0x000C42F8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DsaParameters dsaParameters = obj as DsaParameters;
			return dsaParameters != null && this.Equals(dsaParameters);
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x000C4328 File Offset: 0x000C4328
		protected bool Equals(DsaParameters other)
		{
			return this.p.Equals(other.p) && this.q.Equals(other.q) && this.g.Equals(other.g);
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x000C4378 File Offset: 0x000C4378
		public override int GetHashCode()
		{
			return this.p.GetHashCode() ^ this.q.GetHashCode() ^ this.g.GetHashCode();
		}

		// Token: 0x040015F7 RID: 5623
		private readonly BigInteger p;

		// Token: 0x040015F8 RID: 5624
		private readonly BigInteger q;

		// Token: 0x040015F9 RID: 5625
		private readonly BigInteger g;

		// Token: 0x040015FA RID: 5626
		private readonly DsaValidationParameters validation;
	}
}
