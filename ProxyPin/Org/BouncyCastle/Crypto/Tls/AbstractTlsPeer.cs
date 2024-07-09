using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004BA RID: 1210
	public abstract class AbstractTlsPeer : TlsPeer
	{
		// Token: 0x0600252E RID: 9518 RVA: 0x000CE99C File Offset: 0x000CE99C
		public virtual void Cancel()
		{
			TlsCloseable tlsCloseable = this.mCloseHandle;
			if (tlsCloseable != null)
			{
				tlsCloseable.Close();
			}
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x000CE9C4 File Offset: 0x000CE9C4
		public virtual void NotifyCloseHandle(TlsCloseable closeHandle)
		{
			this.mCloseHandle = closeHandle;
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x000CE9D0 File Offset: 0x000CE9D0
		public virtual bool RequiresExtendedMasterSecret()
		{
			return false;
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x000CE9D4 File Offset: 0x000CE9D4
		public virtual bool ShouldUseGmtUnixTime()
		{
			return false;
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x000CE9D8 File Offset: 0x000CE9D8
		public virtual void NotifySecureRenegotiation(bool secureRenegotiation)
		{
			if (!secureRenegotiation)
			{
				throw new TlsFatalAlert(40);
			}
		}

		// Token: 0x06002533 RID: 9523
		public abstract TlsCompression GetCompression();

		// Token: 0x06002534 RID: 9524
		public abstract TlsCipher GetCipher();

		// Token: 0x06002535 RID: 9525 RVA: 0x000CE9E8 File Offset: 0x000CE9E8
		public virtual void NotifyAlertRaised(byte alertLevel, byte alertDescription, string message, Exception cause)
		{
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x000CE9EC File Offset: 0x000CE9EC
		public virtual void NotifyAlertReceived(byte alertLevel, byte alertDescription)
		{
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x000CE9F0 File Offset: 0x000CE9F0
		public virtual void NotifyHandshakeComplete()
		{
		}

		// Token: 0x04001780 RID: 6016
		private volatile TlsCloseable mCloseHandle;
	}
}
