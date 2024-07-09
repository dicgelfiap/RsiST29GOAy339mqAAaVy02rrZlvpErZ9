using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;
using dnlib.PE;

namespace dnlib.DotNet
{
	// Token: 0x0200081E RID: 2078
	[ComVisible(true)]
	public class ModuleDefMD2 : ModuleDef, IMDTokenProviderMD, IMDTokenProvider
	{
		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06004C7E RID: 19582 RVA: 0x00180018 File Offset: 0x00180018
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x06004C7F RID: 19583 RVA: 0x00180020 File Offset: 0x00180020
		protected override void InitializeCustomAttributes()
		{
			RidList list = this.readerModule.Metadata.GetCustomAttributeRidList(Table.Module, this.origRid);
			CustomAttributeCollection value = new CustomAttributeCollection(list.Count, list, (object list2, int index) => this.readerModule.ReadCustomAttribute(list[index]));
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, value, null);
		}

		// Token: 0x06004C80 RID: 19584 RVA: 0x00180094 File Offset: 0x00180094
		protected override void InitializeCustomDebugInfos()
		{
			List<PdbCustomDebugInfo> list = new List<PdbCustomDebugInfo>();
			this.readerModule.InitializeCustomDebugInfos(new MDToken(base.MDToken.Table, this.origRid), default(GenericParamContext), list);
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, list, null);
		}

		// Token: 0x06004C81 RID: 19585 RVA: 0x001800E8 File Offset: 0x001800E8
		protected override RVA GetNativeEntryPoint_NoLock()
		{
			return this.readerModule.GetNativeEntryPoint();
		}

		// Token: 0x06004C82 RID: 19586 RVA: 0x001800F8 File Offset: 0x001800F8
		protected override IManagedEntryPoint GetManagedEntryPoint_NoLock()
		{
			return this.readerModule.GetManagedEntryPoint();
		}

		// Token: 0x06004C83 RID: 19587 RVA: 0x00180108 File Offset: 0x00180108
		internal ModuleDefMD2(ModuleDefMD readerModule, uint rid)
		{
			if (rid == 1U && readerModule == null)
			{
				readerModule = (ModuleDefMD)this;
			}
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			if (rid != 1U)
			{
				base.Kind = ModuleKind.Windows;
				base.Characteristics = (Characteristics.ExecutableImage | Characteristics.Bit32Machine);
				base.DllCharacteristics = (DllCharacteristics.DynamicBase | DllCharacteristics.NxCompat | DllCharacteristics.NoSeh | DllCharacteristics.TerminalServerAware);
				base.RuntimeVersion = "v2.0.50727";
				base.Machine = Machine.I386;
				this.cor20HeaderFlags = 1;
				base.Cor20HeaderRuntimeVersion = new uint?(131077U);
				base.TablesHeaderVersion = new ushort?((ushort)512);
				this.corLibTypes = new CorLibTypes(this);
				this.location = string.Empty;
				this.InitializeFromRawRow();
			}
		}

		// Token: 0x06004C84 RID: 19588 RVA: 0x001801C8 File Offset: 0x001801C8
		protected void InitializeFromRawRow()
		{
			RawModuleRow rawModuleRow;
			this.readerModule.TablesStream.TryReadModuleRow(this.origRid, out rawModuleRow);
			this.generation = rawModuleRow.Generation;
			this.mvid = this.readerModule.GuidStream.Read(rawModuleRow.Mvid);
			this.encId = this.readerModule.GuidStream.Read(rawModuleRow.EncId);
			this.encBaseId = this.readerModule.GuidStream.Read(rawModuleRow.EncBaseId);
			this.name = this.readerModule.StringsStream.ReadNoNull(rawModuleRow.Name);
			if (this.origRid == 1U)
			{
				this.assembly = this.readerModule.ResolveAssembly(this.origRid);
			}
		}

		// Token: 0x0400260A RID: 9738
		private readonly ModuleDefMD readerModule;

		// Token: 0x0400260B RID: 9739
		private readonly uint origRid;
	}
}
