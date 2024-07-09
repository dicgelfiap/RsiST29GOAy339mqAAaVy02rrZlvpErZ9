using System;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x02000283 RID: 643
	public class EmbeddedSignature : SignatureSubpacket
	{
		// Token: 0x0600145A RID: 5210 RVA: 0x0006D5D8 File Offset: 0x0006D5D8
		public EmbeddedSignature(bool critical, bool isLongLength, byte[] data) : base(SignatureSubpacketTag.EmbeddedSignature, critical, isLongLength, data)
		{
		}
	}
}
