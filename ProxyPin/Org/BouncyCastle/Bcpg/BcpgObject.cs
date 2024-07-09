using System;
using System.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000298 RID: 664
	public abstract class BcpgObject
	{
		// Token: 0x060014C9 RID: 5321 RVA: 0x0006F5AC File Offset: 0x0006F5AC
		public virtual byte[] GetEncoded()
		{
			MemoryStream memoryStream = new MemoryStream();
			BcpgOutputStream bcpgOutputStream = new BcpgOutputStream(memoryStream);
			bcpgOutputStream.WriteObject(this);
			return memoryStream.ToArray();
		}

		// Token: 0x060014CA RID: 5322
		public abstract void Encode(BcpgOutputStream bcpgOut);
	}
}
