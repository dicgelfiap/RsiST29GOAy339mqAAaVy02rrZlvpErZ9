using System;
using dnlib.DotNet.MD;

namespace dnlib.DotNet
{
	// Token: 0x0200078C RID: 1932
	internal sealed class ClassLayoutMD : ClassLayout, IMDTokenProviderMD, IMDTokenProvider
	{
		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x0600450D RID: 17677 RVA: 0x0016C970 File Offset: 0x0016C970
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x0600450E RID: 17678 RVA: 0x0016C978 File Offset: 0x0016C978
		public ClassLayoutMD(ModuleDefMD readerModule, uint rid)
		{
			this.origRid = rid;
			this.rid = rid;
			RawClassLayoutRow rawClassLayoutRow;
			readerModule.TablesStream.TryReadClassLayoutRow(this.origRid, out rawClassLayoutRow);
			this.classSize = rawClassLayoutRow.ClassSize;
			this.packingSize = rawClassLayoutRow.PackingSize;
		}

		// Token: 0x04002423 RID: 9251
		private readonly uint origRid;
	}
}
