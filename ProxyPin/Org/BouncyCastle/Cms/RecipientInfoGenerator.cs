using System;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000300 RID: 768
	public interface RecipientInfoGenerator
	{
		// Token: 0x0600173D RID: 5949
		RecipientInfo Generate(KeyParameter contentEncryptionKey, SecureRandom random);
	}
}
