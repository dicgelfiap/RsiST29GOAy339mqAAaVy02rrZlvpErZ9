using System;
using Org.BouncyCastle.Asn1;

namespace Org.BouncyCastle.Crypto.Operators
{
	// Token: 0x02000416 RID: 1046
	internal class RsaOaepWrapperProvider : WrapperProvider
	{
		// Token: 0x06002173 RID: 8563 RVA: 0x000C21E8 File Offset: 0x000C21E8
		internal RsaOaepWrapperProvider(DerObjectIdentifier digestOid)
		{
			this.digestOid = digestOid;
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x000C21F8 File Offset: 0x000C21F8
		object WrapperProvider.CreateWrapper(bool forWrapping, ICipherParameters parameters)
		{
			return new RsaOaepWrapper(forWrapping, parameters, this.digestOid);
		}

		// Token: 0x040015B9 RID: 5561
		private readonly DerObjectIdentifier digestOid;
	}
}
