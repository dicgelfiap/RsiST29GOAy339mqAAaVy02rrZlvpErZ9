using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004FC RID: 1276
	public class DtlsTransport : DatagramTransport, TlsCloseable
	{
		// Token: 0x06002746 RID: 10054 RVA: 0x000D5528 File Offset: 0x000D5528
		internal DtlsTransport(DtlsRecordLayer recordLayer)
		{
			this.mRecordLayer = recordLayer;
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x000D5538 File Offset: 0x000D5538
		public virtual int GetReceiveLimit()
		{
			return this.mRecordLayer.GetReceiveLimit();
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x000D5548 File Offset: 0x000D5548
		public virtual int GetSendLimit()
		{
			return this.mRecordLayer.GetSendLimit();
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x000D5558 File Offset: 0x000D5558
		public virtual int Receive(byte[] buf, int off, int len, int waitMillis)
		{
			int result;
			try
			{
				result = this.mRecordLayer.Receive(buf, off, len, waitMillis);
			}
			catch (TlsFatalAlert tlsFatalAlert)
			{
				this.mRecordLayer.Fail(tlsFatalAlert.AlertDescription);
				throw tlsFatalAlert;
			}
			catch (IOException ex)
			{
				this.mRecordLayer.Fail(80);
				throw ex;
			}
			catch (Exception alertCause)
			{
				this.mRecordLayer.Fail(80);
				throw new TlsFatalAlert(80, alertCause);
			}
			return result;
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x000D55E0 File Offset: 0x000D55E0
		public virtual void Send(byte[] buf, int off, int len)
		{
			try
			{
				this.mRecordLayer.Send(buf, off, len);
			}
			catch (TlsFatalAlert tlsFatalAlert)
			{
				this.mRecordLayer.Fail(tlsFatalAlert.AlertDescription);
				throw tlsFatalAlert;
			}
			catch (IOException ex)
			{
				this.mRecordLayer.Fail(80);
				throw ex;
			}
			catch (Exception alertCause)
			{
				this.mRecordLayer.Fail(80);
				throw new TlsFatalAlert(80, alertCause);
			}
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x000D5664 File Offset: 0x000D5664
		public virtual void Close()
		{
			this.mRecordLayer.Close();
		}

		// Token: 0x04001954 RID: 6484
		private readonly DtlsRecordLayer mRecordLayer;
	}
}
