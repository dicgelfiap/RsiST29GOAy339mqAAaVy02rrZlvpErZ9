using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200042F RID: 1071
	[Obsolete("Use AeadParameters")]
	public class CcmParameters : AeadParameters
	{
		// Token: 0x060021DA RID: 8666 RVA: 0x000C35C0 File Offset: 0x000C35C0
		public CcmParameters(KeyParameter key, int macSize, byte[] nonce, byte[] associatedText) : base(key, macSize, nonce, associatedText)
		{
		}
	}
}
