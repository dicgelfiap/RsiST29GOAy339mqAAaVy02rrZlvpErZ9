using System;
using System.Collections.Generic;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x02000851 RID: 2129
	internal sealed class StandAloneSigMD : StandAloneSig, IMDTokenProviderMD, IMDTokenProvider, IContainsGenericParameter2
	{
		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x06005068 RID: 20584 RVA: 0x0018FABC File Offset: 0x0018FABC
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x06005069 RID: 20585 RVA: 0x0018FAC4 File Offset: 0x0018FAC4
		protected override void InitializeCustomAttributes()
		{
			RidList list = this.readerModule.Metadata.GetCustomAttributeRidList(Table.StandAloneSig, this.origRid);
			CustomAttributeCollection value = new CustomAttributeCollection(list.Count, list, (object list2, int index) => this.readerModule.ReadCustomAttribute(list[index]));
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, value, null);
		}

		// Token: 0x0600506A RID: 20586 RVA: 0x0018FB38 File Offset: 0x0018FB38
		protected override void InitializeCustomDebugInfos()
		{
			List<PdbCustomDebugInfo> list = new List<PdbCustomDebugInfo>();
			this.readerModule.InitializeCustomDebugInfos(new MDToken(base.MDToken.Table, this.origRid), this.gpContext, list);
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, list, null);
		}

		// Token: 0x0600506B RID: 20587 RVA: 0x0018FB88 File Offset: 0x0018FB88
		public StandAloneSigMD(ModuleDefMD readerModule, uint rid, GenericParamContext gpContext)
		{
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			this.gpContext = gpContext;
			RawStandAloneSigRow rawStandAloneSigRow;
			readerModule.TablesStream.TryReadStandAloneSigRow(this.origRid, out rawStandAloneSigRow);
			this.signature = readerModule.ReadSignature(rawStandAloneSigRow.Signature, gpContext);
		}

		// Token: 0x04002754 RID: 10068
		private readonly ModuleDefMD readerModule;

		// Token: 0x04002755 RID: 10069
		private readonly uint origRid;

		// Token: 0x04002756 RID: 10070
		private readonly GenericParamContext gpContext;
	}
}
