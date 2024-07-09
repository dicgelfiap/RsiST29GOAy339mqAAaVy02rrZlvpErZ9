using System;
using System.Collections;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000317 RID: 791
	public class SignerInformationStore
	{
		// Token: 0x060017EF RID: 6127 RVA: 0x0007C8E8 File Offset: 0x0007C8E8
		public SignerInformationStore(SignerInformation signerInfo)
		{
			this.all = Platform.CreateArrayList(1);
			this.all.Add(signerInfo);
			SignerID signerID = signerInfo.SignerID;
			this.table[signerID] = this.all;
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x0007C93C File Offset: 0x0007C93C
		public SignerInformationStore(ICollection signerInfos)
		{
			foreach (object obj in signerInfos)
			{
				SignerInformation signerInformation = (SignerInformation)obj;
				SignerID signerID = signerInformation.SignerID;
				IList list = (IList)this.table[signerID];
				if (list == null)
				{
					list = (this.table[signerID] = Platform.CreateArrayList(1));
				}
				list.Add(signerInformation);
			}
			this.all = Platform.CreateArrayList(signerInfos);
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x0007C9F0 File Offset: 0x0007C9F0
		public SignerInformation GetFirstSigner(SignerID selector)
		{
			IList list = (IList)this.table[selector];
			if (list != null)
			{
				return (SignerInformation)list[0];
			}
			return null;
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x0007CA28 File Offset: 0x0007CA28
		public int Count
		{
			get
			{
				return this.all.Count;
			}
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x0007CA38 File Offset: 0x0007CA38
		public ICollection GetSigners()
		{
			return Platform.CreateArrayList(this.all);
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x0007CA48 File Offset: 0x0007CA48
		public ICollection GetSigners(SignerID selector)
		{
			IList list = (IList)this.table[selector];
			if (list != null)
			{
				return Platform.CreateArrayList(list);
			}
			return Platform.CreateArrayList();
		}

		// Token: 0x04000FEF RID: 4079
		private readonly IList all;

		// Token: 0x04000FF0 RID: 4080
		private readonly IDictionary table = Platform.CreateHashtable();
	}
}
