using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200053D RID: 1341
	public class TlsFatalAlertReceived : TlsException
	{
		// Token: 0x06002948 RID: 10568 RVA: 0x000DD840 File Offset: 0x000DD840
		public TlsFatalAlertReceived(byte alertDescription) : base(Org.BouncyCastle.Crypto.Tls.AlertDescription.GetText(alertDescription), null)
		{
			this.alertDescription = alertDescription;
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06002949 RID: 10569 RVA: 0x000DD858 File Offset: 0x000DD858
		public virtual byte AlertDescription
		{
			get
			{
				return this.alertDescription;
			}
		}

		// Token: 0x04001AE3 RID: 6883
		private readonly byte alertDescription;
	}
}
