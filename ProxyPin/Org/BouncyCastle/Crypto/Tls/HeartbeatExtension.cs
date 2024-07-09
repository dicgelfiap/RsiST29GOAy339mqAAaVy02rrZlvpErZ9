using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000506 RID: 1286
	public class HeartbeatExtension
	{
		// Token: 0x0600275D RID: 10077 RVA: 0x000D5804 File Offset: 0x000D5804
		public HeartbeatExtension(byte mode)
		{
			if (!HeartbeatMode.IsValid(mode))
			{
				throw new ArgumentException("not a valid HeartbeatMode value", "mode");
			}
			this.mMode = mode;
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x0600275E RID: 10078 RVA: 0x000D5830 File Offset: 0x000D5830
		public virtual byte Mode
		{
			get
			{
				return this.mMode;
			}
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x000D5838 File Offset: 0x000D5838
		public virtual void Encode(Stream output)
		{
			TlsUtilities.WriteUint8(this.mMode, output);
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x000D5848 File Offset: 0x000D5848
		public static HeartbeatExtension Parse(Stream input)
		{
			byte b = TlsUtilities.ReadUint8(input);
			if (!HeartbeatMode.IsValid(b))
			{
				throw new TlsFatalAlert(47);
			}
			return new HeartbeatExtension(b);
		}

		// Token: 0x040019B7 RID: 6583
		protected readonly byte mMode;
	}
}
