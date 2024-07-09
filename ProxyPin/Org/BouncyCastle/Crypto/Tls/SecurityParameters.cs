using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000516 RID: 1302
	public class SecurityParameters
	{
		// Token: 0x060027B5 RID: 10165 RVA: 0x000D68E0 File Offset: 0x000D68E0
		internal virtual void Clear()
		{
			if (this.masterSecret != null)
			{
				Arrays.Fill(this.masterSecret, 0);
				this.masterSecret = null;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060027B6 RID: 10166 RVA: 0x000D6900 File Offset: 0x000D6900
		public virtual int Entity
		{
			get
			{
				return this.entity;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060027B7 RID: 10167 RVA: 0x000D6908 File Offset: 0x000D6908
		public virtual int CipherSuite
		{
			get
			{
				return this.cipherSuite;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060027B8 RID: 10168 RVA: 0x000D6910 File Offset: 0x000D6910
		public virtual byte CompressionAlgorithm
		{
			get
			{
				return this.compressionAlgorithm;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x060027B9 RID: 10169 RVA: 0x000D6918 File Offset: 0x000D6918
		public virtual int PrfAlgorithm
		{
			get
			{
				return this.prfAlgorithm;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x060027BA RID: 10170 RVA: 0x000D6920 File Offset: 0x000D6920
		public virtual int VerifyDataLength
		{
			get
			{
				return this.verifyDataLength;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060027BB RID: 10171 RVA: 0x000D6928 File Offset: 0x000D6928
		public virtual byte[] MasterSecret
		{
			get
			{
				return this.masterSecret;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060027BC RID: 10172 RVA: 0x000D6930 File Offset: 0x000D6930
		public virtual byte[] ClientRandom
		{
			get
			{
				return this.clientRandom;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x060027BD RID: 10173 RVA: 0x000D6938 File Offset: 0x000D6938
		public virtual byte[] ServerRandom
		{
			get
			{
				return this.serverRandom;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x060027BE RID: 10174 RVA: 0x000D6940 File Offset: 0x000D6940
		public virtual byte[] SessionHash
		{
			get
			{
				return this.sessionHash;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x060027BF RID: 10175 RVA: 0x000D6948 File Offset: 0x000D6948
		public virtual byte[] PskIdentity
		{
			get
			{
				return this.pskIdentity;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x060027C0 RID: 10176 RVA: 0x000D6950 File Offset: 0x000D6950
		public virtual byte[] SrpIdentity
		{
			get
			{
				return this.srpIdentity;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x060027C1 RID: 10177 RVA: 0x000D6958 File Offset: 0x000D6958
		public virtual bool IsExtendedMasterSecret
		{
			get
			{
				return this.extendedMasterSecret;
			}
		}

		// Token: 0x04001A2E RID: 6702
		internal int entity = -1;

		// Token: 0x04001A2F RID: 6703
		internal int cipherSuite = -1;

		// Token: 0x04001A30 RID: 6704
		internal byte compressionAlgorithm = 0;

		// Token: 0x04001A31 RID: 6705
		internal int prfAlgorithm = -1;

		// Token: 0x04001A32 RID: 6706
		internal int verifyDataLength = -1;

		// Token: 0x04001A33 RID: 6707
		internal byte[] masterSecret = null;

		// Token: 0x04001A34 RID: 6708
		internal byte[] clientRandom = null;

		// Token: 0x04001A35 RID: 6709
		internal byte[] serverRandom = null;

		// Token: 0x04001A36 RID: 6710
		internal byte[] sessionHash = null;

		// Token: 0x04001A37 RID: 6711
		internal byte[] pskIdentity = null;

		// Token: 0x04001A38 RID: 6712
		internal byte[] srpIdentity = null;

		// Token: 0x04001A39 RID: 6713
		internal short maxFragmentLength = -1;

		// Token: 0x04001A3A RID: 6714
		internal bool truncatedHMac = false;

		// Token: 0x04001A3B RID: 6715
		internal bool encryptThenMac = false;

		// Token: 0x04001A3C RID: 6716
		internal bool extendedMasterSecret = false;
	}
}
