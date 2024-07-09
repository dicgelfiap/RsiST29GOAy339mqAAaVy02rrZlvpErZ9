using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000438 RID: 1080
	public class DHPrivateKeyParameters : DHKeyParameters
	{
		// Token: 0x06002212 RID: 8722 RVA: 0x000C3E80 File Offset: 0x000C3E80
		public DHPrivateKeyParameters(BigInteger x, DHParameters parameters) : base(true, parameters)
		{
			this.x = x;
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x000C3E94 File Offset: 0x000C3E94
		public DHPrivateKeyParameters(BigInteger x, DHParameters parameters, DerObjectIdentifier algorithmOid) : base(true, parameters, algorithmOid)
		{
			this.x = x;
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x000C3EA8 File Offset: 0x000C3EA8
		public BigInteger X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x000C3EB0 File Offset: 0x000C3EB0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DHPrivateKeyParameters dhprivateKeyParameters = obj as DHPrivateKeyParameters;
			return dhprivateKeyParameters != null && this.Equals(dhprivateKeyParameters);
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x000C3EE0 File Offset: 0x000C3EE0
		protected bool Equals(DHPrivateKeyParameters other)
		{
			return this.x.Equals(other.x) && base.Equals(other);
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x000C3F04 File Offset: 0x000C3F04
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x040015EA RID: 5610
		private readonly BigInteger x;
	}
}
