using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200043F RID: 1087
	public class DsaPrivateKeyParameters : DsaKeyParameters
	{
		// Token: 0x0600223C RID: 8764 RVA: 0x000C43A0 File Offset: 0x000C43A0
		public DsaPrivateKeyParameters(BigInteger x, DsaParameters parameters) : base(true, parameters)
		{
			if (x == null)
			{
				throw new ArgumentNullException("x");
			}
			this.x = x;
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x0600223D RID: 8765 RVA: 0x000C43C4 File Offset: 0x000C43C4
		public BigInteger X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x000C43CC File Offset: 0x000C43CC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DsaPrivateKeyParameters dsaPrivateKeyParameters = obj as DsaPrivateKeyParameters;
			return dsaPrivateKeyParameters != null && this.Equals(dsaPrivateKeyParameters);
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x000C43FC File Offset: 0x000C43FC
		protected bool Equals(DsaPrivateKeyParameters other)
		{
			return this.x.Equals(other.x) && base.Equals(other);
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x000C4420 File Offset: 0x000C4420
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x040015FB RID: 5627
		private readonly BigInteger x;
	}
}
