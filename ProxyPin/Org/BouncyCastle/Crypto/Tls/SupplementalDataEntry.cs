using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000526 RID: 1318
	public class SupplementalDataEntry
	{
		// Token: 0x06002814 RID: 10260 RVA: 0x000D75A4 File Offset: 0x000D75A4
		public SupplementalDataEntry(int dataType, byte[] data)
		{
			this.mDataType = dataType;
			this.mData = data;
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06002815 RID: 10261 RVA: 0x000D75BC File Offset: 0x000D75BC
		public virtual int DataType
		{
			get
			{
				return this.mDataType;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06002816 RID: 10262 RVA: 0x000D75C4 File Offset: 0x000D75C4
		public virtual byte[] Data
		{
			get
			{
				return this.mData;
			}
		}

		// Token: 0x04001A6A RID: 6762
		protected readonly int mDataType;

		// Token: 0x04001A6B RID: 6763
		protected readonly byte[] mData;
	}
}
