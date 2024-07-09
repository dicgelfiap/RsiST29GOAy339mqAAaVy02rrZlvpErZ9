using System;
using System.Collections.Generic;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x02000780 RID: 1920
	internal sealed class AssemblyRefMD : AssemblyRef, IMDTokenProviderMD, IMDTokenProvider
	{
		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x0600445A RID: 17498 RVA: 0x0016AAE8 File Offset: 0x0016AAE8
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x0600445B RID: 17499 RVA: 0x0016AAF0 File Offset: 0x0016AAF0
		protected override void InitializeCustomAttributes()
		{
			RidList list = this.readerModule.Metadata.GetCustomAttributeRidList(Table.AssemblyRef, this.origRid);
			CustomAttributeCollection value = new CustomAttributeCollection(list.Count, list, (object list2, int index) => this.readerModule.ReadCustomAttribute(list[index]));
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, value, null);
		}

		// Token: 0x0600445C RID: 17500 RVA: 0x0016AB64 File Offset: 0x0016AB64
		protected override void InitializeCustomDebugInfos()
		{
			List<PdbCustomDebugInfo> list = new List<PdbCustomDebugInfo>();
			this.readerModule.InitializeCustomDebugInfos(new MDToken(base.MDToken.Table, this.origRid), default(GenericParamContext), list);
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, list, null);
		}

		// Token: 0x0600445D RID: 17501 RVA: 0x0016ABB8 File Offset: 0x0016ABB8
		public AssemblyRefMD(ModuleDefMD readerModule, uint rid)
		{
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			RawAssemblyRefRow rawAssemblyRefRow;
			readerModule.TablesStream.TryReadAssemblyRefRow(this.origRid, out rawAssemblyRefRow);
			this.version = new Version((int)rawAssemblyRefRow.MajorVersion, (int)rawAssemblyRefRow.MinorVersion, (int)rawAssemblyRefRow.BuildNumber, (int)rawAssemblyRefRow.RevisionNumber);
			this.attributes = (int)rawAssemblyRefRow.Flags;
			byte[] data = readerModule.BlobStream.Read(rawAssemblyRefRow.PublicKeyOrToken);
			if (((long)this.attributes & 1L) != 0L)
			{
				this.publicKeyOrToken = new PublicKey(data);
			}
			else
			{
				this.publicKeyOrToken = new PublicKeyToken(data);
			}
			this.name = readerModule.StringsStream.ReadNoNull(rawAssemblyRefRow.Name);
			this.culture = readerModule.StringsStream.ReadNoNull(rawAssemblyRefRow.Locale);
			this.hashValue = readerModule.BlobStream.Read(rawAssemblyRefRow.HashValue);
		}

		// Token: 0x040023F2 RID: 9202
		private readonly ModuleDefMD readerModule;

		// Token: 0x040023F3 RID: 9203
		private readonly uint origRid;
	}
}
