using System;
using System.Collections.Generic;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;
using dnlib.PE;

namespace dnlib.DotNet
{
	// Token: 0x020007AD RID: 1965
	internal sealed class FieldDefMD : FieldDef, IMDTokenProviderMD, IMDTokenProvider
	{
		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06004713 RID: 18195 RVA: 0x00171044 File Offset: 0x00171044
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x06004714 RID: 18196 RVA: 0x0017104C File Offset: 0x0017104C
		protected override void InitializeCustomAttributes()
		{
			RidList list = this.readerModule.Metadata.GetCustomAttributeRidList(Table.Field, this.origRid);
			CustomAttributeCollection value = new CustomAttributeCollection(list.Count, list, (object list2, int index) => this.readerModule.ReadCustomAttribute(list[index]));
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, value, null);
		}

		// Token: 0x06004715 RID: 18197 RVA: 0x001710C0 File Offset: 0x001710C0
		protected override void InitializeCustomDebugInfos()
		{
			List<PdbCustomDebugInfo> list = new List<PdbCustomDebugInfo>();
			this.readerModule.InitializeCustomDebugInfos(new MDToken(base.MDToken.Table, this.origRid), new GenericParamContext(this.declaringType2), list);
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, list, null);
		}

		// Token: 0x06004716 RID: 18198 RVA: 0x00171118 File Offset: 0x00171118
		protected override uint? GetFieldOffset_NoLock()
		{
			RawFieldLayoutRow rawFieldLayoutRow;
			if (this.readerModule.TablesStream.TryReadFieldLayoutRow(this.readerModule.Metadata.GetFieldLayoutRid(this.origRid), out rawFieldLayoutRow))
			{
				return new uint?(rawFieldLayoutRow.OffSet);
			}
			return null;
		}

		// Token: 0x06004717 RID: 18199 RVA: 0x0017116C File Offset: 0x0017116C
		protected override MarshalType GetMarshalType_NoLock()
		{
			return this.readerModule.ReadMarshalType(Table.Field, this.origRid, new GenericParamContext(this.declaringType2));
		}

		// Token: 0x06004718 RID: 18200 RVA: 0x0017118C File Offset: 0x0017118C
		protected override RVA GetRVA_NoLock()
		{
			RVA result;
			this.GetFieldRVA_NoLock(out result);
			return result;
		}

		// Token: 0x06004719 RID: 18201 RVA: 0x001711A8 File Offset: 0x001711A8
		protected override byte[] GetInitialValue_NoLock()
		{
			RVA rva;
			if (!this.GetFieldRVA_NoLock(out rva))
			{
				return null;
			}
			return this.ReadInitialValue_NoLock(rva);
		}

		// Token: 0x0600471A RID: 18202 RVA: 0x001711D0 File Offset: 0x001711D0
		protected override ImplMap GetImplMap_NoLock()
		{
			return this.readerModule.ResolveImplMap(this.readerModule.Metadata.GetImplMapRid(Table.Field, this.origRid));
		}

		// Token: 0x0600471B RID: 18203 RVA: 0x001711F4 File Offset: 0x001711F4
		protected override Constant GetConstant_NoLock()
		{
			return this.readerModule.ResolveConstant(this.readerModule.Metadata.GetConstantRid(Table.Field, this.origRid));
		}

		// Token: 0x0600471C RID: 18204 RVA: 0x00171218 File Offset: 0x00171218
		public FieldDefMD(ModuleDefMD readerModule, uint rid)
		{
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			RawFieldRow rawFieldRow;
			readerModule.TablesStream.TryReadFieldRow(this.origRid, out rawFieldRow);
			this.name = readerModule.StringsStream.ReadNoNull(rawFieldRow.Name);
			this.attributes = (int)rawFieldRow.Flags;
			this.origAttributes = (FieldAttributes)this.attributes;
			this.declaringType2 = readerModule.GetOwnerType(this);
			this.signature = readerModule.ReadSignature(rawFieldRow.Signature, new GenericParamContext(this.declaringType2));
		}

		// Token: 0x0600471D RID: 18205 RVA: 0x001712B4 File Offset: 0x001712B4
		internal FieldDefMD InitializeAll()
		{
			MemberMDInitializer.Initialize<CustomAttribute>(base.CustomAttributes);
			MemberMDInitializer.Initialize(base.Attributes);
			MemberMDInitializer.Initialize(base.Name);
			MemberMDInitializer.Initialize(base.Signature);
			MemberMDInitializer.Initialize(base.FieldOffset);
			MemberMDInitializer.Initialize(base.MarshalType);
			MemberMDInitializer.Initialize(base.RVA);
			MemberMDInitializer.Initialize<byte>(base.InitialValue);
			MemberMDInitializer.Initialize(base.ImplMap);
			MemberMDInitializer.Initialize(base.Constant);
			MemberMDInitializer.Initialize(base.DeclaringType);
			return this;
		}

		// Token: 0x0600471E RID: 18206 RVA: 0x00171350 File Offset: 0x00171350
		private bool GetFieldRVA_NoLock(out RVA rva)
		{
			if ((this.origAttributes & FieldAttributes.HasFieldRVA) == FieldAttributes.PrivateScope)
			{
				rva = (RVA)0U;
				return false;
			}
			RawFieldRVARow rawFieldRVARow;
			if (!this.readerModule.TablesStream.TryReadFieldRVARow(this.readerModule.Metadata.GetFieldRVARid(this.origRid), out rawFieldRVARow))
			{
				rva = (RVA)0U;
				return false;
			}
			rva = (RVA)rawFieldRVARow.RVA;
			return true;
		}

		// Token: 0x0600471F RID: 18207 RVA: 0x001713B4 File Offset: 0x001713B4
		private byte[] ReadInitialValue_NoLock(RVA rva)
		{
			uint num;
			if (!base.GetFieldSize(this.declaringType2, this.signature as FieldSig, out num))
			{
				return null;
			}
			if (num >= 2147483647U)
			{
				return null;
			}
			return this.readerModule.ReadDataAt(rva, (int)num);
		}

		// Token: 0x040024CF RID: 9423
		private readonly ModuleDefMD readerModule;

		// Token: 0x040024D0 RID: 9424
		private readonly uint origRid;

		// Token: 0x040024D1 RID: 9425
		private readonly FieldAttributes origAttributes;
	}
}
