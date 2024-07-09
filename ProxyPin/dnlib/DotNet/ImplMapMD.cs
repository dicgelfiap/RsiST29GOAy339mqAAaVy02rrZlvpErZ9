using System;
using dnlib.DotNet.MD;

namespace dnlib.DotNet
{
	// Token: 0x020007E9 RID: 2025
	internal sealed class ImplMapMD : ImplMap, IMDTokenProviderMD, IMDTokenProvider
	{
		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06004903 RID: 18691 RVA: 0x00177630 File Offset: 0x00177630
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x06004904 RID: 18692 RVA: 0x00177638 File Offset: 0x00177638
		public ImplMapMD(ModuleDefMD readerModule, uint rid)
		{
			this.origRid = rid;
			this.rid = rid;
			RawImplMapRow rawImplMapRow;
			readerModule.TablesStream.TryReadImplMapRow(this.origRid, out rawImplMapRow);
			this.attributes = (int)rawImplMapRow.MappingFlags;
			this.name = readerModule.StringsStream.ReadNoNull(rawImplMapRow.ImportName);
			this.module = readerModule.ResolveModuleRef(rawImplMapRow.ImportScope);
		}

		// Token: 0x0400251F RID: 9503
		private readonly uint origRid;
	}
}
