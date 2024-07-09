using System;
using System.Collections;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000312 RID: 786
	public class RecipientInformationStore
	{
		// Token: 0x060017BD RID: 6077 RVA: 0x0007B750 File Offset: 0x0007B750
		public RecipientInformationStore(ICollection recipientInfos)
		{
			foreach (object obj in recipientInfos)
			{
				RecipientInformation recipientInformation = (RecipientInformation)obj;
				RecipientID recipientID = recipientInformation.RecipientID;
				IList list = (IList)this.table[recipientID];
				if (list == null)
				{
					list = (this.table[recipientID] = Platform.CreateArrayList(1));
				}
				list.Add(recipientInformation);
			}
			this.all = Platform.CreateArrayList(recipientInfos);
		}

		// Token: 0x17000544 RID: 1348
		public RecipientInformation this[RecipientID selector]
		{
			get
			{
				return this.GetFirstRecipient(selector);
			}
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x0007B810 File Offset: 0x0007B810
		public RecipientInformation GetFirstRecipient(RecipientID selector)
		{
			IList list = (IList)this.table[selector];
			if (list != null)
			{
				return (RecipientInformation)list[0];
			}
			return null;
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x0007B848 File Offset: 0x0007B848
		public int Count
		{
			get
			{
				return this.all.Count;
			}
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x0007B858 File Offset: 0x0007B858
		public ICollection GetRecipients()
		{
			return Platform.CreateArrayList(this.all);
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x0007B868 File Offset: 0x0007B868
		public ICollection GetRecipients(RecipientID selector)
		{
			IList list = (IList)this.table[selector];
			if (list != null)
			{
				return Platform.CreateArrayList(list);
			}
			return Platform.CreateArrayList();
		}

		// Token: 0x04000FD5 RID: 4053
		private readonly IList all;

		// Token: 0x04000FD6 RID: 4054
		private readonly IDictionary table = Platform.CreateHashtable();
	}
}
