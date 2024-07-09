using System;
using System.Collections.Generic;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x020007B1 RID: 1969
	internal sealed class FileDefMD : FileDef, IMDTokenProviderMD, IMDTokenProvider
	{
		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x0600473C RID: 18236 RVA: 0x00171598 File Offset: 0x00171598
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x0600473D RID: 18237 RVA: 0x001715A0 File Offset: 0x001715A0
		protected override void InitializeCustomAttributes()
		{
			RidList list = this.readerModule.Metadata.GetCustomAttributeRidList(Table.File, this.origRid);
			CustomAttributeCollection value = new CustomAttributeCollection(list.Count, list, (object list2, int index) => this.readerModule.ReadCustomAttribute(list[index]));
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, value, null);
		}

		// Token: 0x0600473E RID: 18238 RVA: 0x00171614 File Offset: 0x00171614
		protected override void InitializeCustomDebugInfos()
		{
			List<PdbCustomDebugInfo> list = new List<PdbCustomDebugInfo>();
			this.readerModule.InitializeCustomDebugInfos(new MDToken(base.MDToken.Table, this.origRid), default(GenericParamContext), list);
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, list, null);
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x00171668 File Offset: 0x00171668
		public FileDefMD(ModuleDefMD readerModule, uint rid)
		{
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			RawFileRow rawFileRow;
			readerModule.TablesStream.TryReadFileRow(this.origRid, out rawFileRow);
			this.attributes = (int)rawFileRow.Flags;
			this.name = readerModule.StringsStream.ReadNoNull(rawFileRow.Name);
			this.hashValue = readerModule.BlobStream.Read(rawFileRow.HashValue);
		}

		// Token: 0x040024DB RID: 9435
		private readonly ModuleDefMD readerModule;

		// Token: 0x040024DC RID: 9436
		private readonly uint origRid;
	}
}
