using System;
using System.Collections.Generic;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x020007EF RID: 2031
	internal sealed class InterfaceImplMD : InterfaceImpl, IMDTokenProviderMD, IMDTokenProvider, IContainsGenericParameter2
	{
		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x0600495C RID: 18780 RVA: 0x00179A30 File Offset: 0x00179A30
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x0600495D RID: 18781 RVA: 0x00179A38 File Offset: 0x00179A38
		bool IContainsGenericParameter2.ContainsGenericParameter
		{
			get
			{
				return TypeHelper.ContainsGenericParameter(this);
			}
		}

		// Token: 0x0600495E RID: 18782 RVA: 0x00179A40 File Offset: 0x00179A40
		protected override void InitializeCustomAttributes()
		{
			RidList list = this.readerModule.Metadata.GetCustomAttributeRidList(Table.InterfaceImpl, this.origRid);
			CustomAttributeCollection value = new CustomAttributeCollection(list.Count, list, (object list2, int index) => this.readerModule.ReadCustomAttribute(list[index]));
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, value, null);
		}

		// Token: 0x0600495F RID: 18783 RVA: 0x00179AB4 File Offset: 0x00179AB4
		protected override void InitializeCustomDebugInfos()
		{
			List<PdbCustomDebugInfo> list = new List<PdbCustomDebugInfo>();
			this.readerModule.InitializeCustomDebugInfos(new MDToken(base.MDToken.Table, this.origRid), this.gpContext, list);
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, list, null);
		}

		// Token: 0x06004960 RID: 18784 RVA: 0x00179B04 File Offset: 0x00179B04
		public InterfaceImplMD(ModuleDefMD readerModule, uint rid, GenericParamContext gpContext)
		{
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			this.gpContext = gpContext;
			RawInterfaceImplRow rawInterfaceImplRow;
			readerModule.TablesStream.TryReadInterfaceImplRow(this.origRid, out rawInterfaceImplRow);
			this.@interface = readerModule.ResolveTypeDefOrRef(rawInterfaceImplRow.Interface, gpContext);
		}

		// Token: 0x0400252F RID: 9519
		private readonly ModuleDefMD readerModule;

		// Token: 0x04002530 RID: 9520
		private readonly uint origRid;

		// Token: 0x04002531 RID: 9521
		private readonly GenericParamContext gpContext;
	}
}
