using System;
using System.Collections.Generic;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x02000881 RID: 2177
	internal sealed class TypeSpecMD : TypeSpec, IMDTokenProviderMD, IMDTokenProvider, IContainsGenericParameter2
	{
		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x06005333 RID: 21299 RVA: 0x00196A68 File Offset: 0x00196A68
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x06005334 RID: 21300 RVA: 0x00196A70 File Offset: 0x00196A70
		protected override TypeSig GetTypeSigAndExtraData_NoLock(out byte[] extraData)
		{
			TypeSig typeSig = this.readerModule.ReadTypeSignature(this.signatureOffset, this.gpContext, out extraData);
			if (typeSig != null)
			{
				typeSig.Rid = this.origRid;
			}
			return typeSig;
		}

		// Token: 0x06005335 RID: 21301 RVA: 0x00196AB0 File Offset: 0x00196AB0
		protected override void InitializeCustomAttributes()
		{
			RidList list = this.readerModule.Metadata.GetCustomAttributeRidList(Table.TypeSpec, this.origRid);
			CustomAttributeCollection value = new CustomAttributeCollection(list.Count, list, (object list2, int index) => this.readerModule.ReadCustomAttribute(list[index]));
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, value, null);
		}

		// Token: 0x06005336 RID: 21302 RVA: 0x00196B24 File Offset: 0x00196B24
		protected override void InitializeCustomDebugInfos()
		{
			List<PdbCustomDebugInfo> list = new List<PdbCustomDebugInfo>();
			this.readerModule.InitializeCustomDebugInfos(new MDToken(base.MDToken.Table, this.origRid), this.gpContext, list);
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, list, null);
		}

		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x06005337 RID: 21303 RVA: 0x00196B74 File Offset: 0x00196B74
		bool IContainsGenericParameter2.ContainsGenericParameter
		{
			get
			{
				return !this.gpContext.IsEmpty && base.ContainsGenericParameter;
			}
		}

		// Token: 0x06005338 RID: 21304 RVA: 0x00196B90 File Offset: 0x00196B90
		public TypeSpecMD(ModuleDefMD readerModule, uint rid, GenericParamContext gpContext)
		{
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			this.gpContext = gpContext;
			RawTypeSpecRow rawTypeSpecRow;
			readerModule.TablesStream.TryReadTypeSpecRow(this.origRid, out rawTypeSpecRow);
			this.signatureOffset = rawTypeSpecRow.Signature;
		}

		// Token: 0x040027E1 RID: 10209
		private readonly ModuleDefMD readerModule;

		// Token: 0x040027E2 RID: 10210
		private readonly uint origRid;

		// Token: 0x040027E3 RID: 10211
		private readonly GenericParamContext gpContext;

		// Token: 0x040027E4 RID: 10212
		private readonly uint signatureOffset;
	}
}
