using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200053C RID: 1340
	public class TlsFatalAlert : TlsException
	{
		// Token: 0x06002945 RID: 10565 RVA: 0x000DD814 File Offset: 0x000DD814
		public TlsFatalAlert(byte alertDescription) : this(alertDescription, null)
		{
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x000DD820 File Offset: 0x000DD820
		public TlsFatalAlert(byte alertDescription, Exception alertCause) : base(Org.BouncyCastle.Crypto.Tls.AlertDescription.GetText(alertDescription), alertCause)
		{
			this.alertDescription = alertDescription;
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06002947 RID: 10567 RVA: 0x000DD838 File Offset: 0x000DD838
		public virtual byte AlertDescription
		{
			get
			{
				return this.alertDescription;
			}
		}

		// Token: 0x04001AE2 RID: 6882
		private readonly byte alertDescription;
	}
}
