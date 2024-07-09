using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000556 RID: 1366
	public class UseSrtpData
	{
		// Token: 0x06002A65 RID: 10853 RVA: 0x000E38A0 File Offset: 0x000E38A0
		public UseSrtpData(int[] protectionProfiles, byte[] mki)
		{
			if (protectionProfiles == null || protectionProfiles.Length < 1 || protectionProfiles.Length >= 32768)
			{
				throw new ArgumentException("must have length from 1 to (2^15 - 1)", "protectionProfiles");
			}
			if (mki == null)
			{
				mki = TlsUtilities.EmptyBytes;
			}
			else if (mki.Length > 255)
			{
				throw new ArgumentException("cannot be longer than 255 bytes", "mki");
			}
			this.mProtectionProfiles = protectionProfiles;
			this.mMki = mki;
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06002A66 RID: 10854 RVA: 0x000E3920 File Offset: 0x000E3920
		public virtual int[] ProtectionProfiles
		{
			get
			{
				return this.mProtectionProfiles;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06002A67 RID: 10855 RVA: 0x000E3928 File Offset: 0x000E3928
		public virtual byte[] Mki
		{
			get
			{
				return this.mMki;
			}
		}

		// Token: 0x04001B2C RID: 6956
		protected readonly int[] mProtectionProfiles;

		// Token: 0x04001B2D RID: 6957
		protected readonly byte[] mMki;
	}
}
