using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200054C RID: 1356
	internal class TlsSessionImpl : TlsSession
	{
		// Token: 0x0600299E RID: 10654 RVA: 0x000DF7DC File Offset: 0x000DF7DC
		internal TlsSessionImpl(byte[] sessionID, SessionParameters sessionParameters)
		{
			if (sessionID == null)
			{
				throw new ArgumentNullException("sessionID");
			}
			if (sessionID.Length > 32)
			{
				throw new ArgumentException("cannot be longer than 32 bytes", "sessionID");
			}
			this.mSessionID = Arrays.Clone(sessionID);
			this.mSessionParameters = sessionParameters;
			this.mResumable = (sessionID.Length > 0 && sessionParameters != null && sessionParameters.IsExtendedMasterSecret);
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x000DF854 File Offset: 0x000DF854
		public virtual SessionParameters ExportSessionParameters()
		{
			SessionParameters result;
			lock (this)
			{
				result = ((this.mSessionParameters == null) ? null : this.mSessionParameters.Copy());
			}
			return result;
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x060029A0 RID: 10656 RVA: 0x000DF8A4 File Offset: 0x000DF8A4
		public virtual byte[] SessionID
		{
			get
			{
				byte[] result;
				lock (this)
				{
					result = this.mSessionID;
				}
				return result;
			}
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x000DF8E0 File Offset: 0x000DF8E0
		public virtual void Invalidate()
		{
			lock (this)
			{
				this.mResumable = false;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x060029A2 RID: 10658 RVA: 0x000DF918 File Offset: 0x000DF918
		public virtual bool IsResumable
		{
			get
			{
				bool result;
				lock (this)
				{
					result = this.mResumable;
				}
				return result;
			}
		}

		// Token: 0x04001B09 RID: 6921
		internal readonly byte[] mSessionID;

		// Token: 0x04001B0A RID: 6922
		internal readonly SessionParameters mSessionParameters;

		// Token: 0x04001B0B RID: 6923
		internal bool mResumable;
	}
}
