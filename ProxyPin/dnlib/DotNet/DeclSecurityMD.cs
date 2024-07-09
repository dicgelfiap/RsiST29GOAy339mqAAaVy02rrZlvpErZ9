using System;
using System.Collections.Generic;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x020007A0 RID: 1952
	internal sealed class DeclSecurityMD : DeclSecurity, IMDTokenProviderMD, IMDTokenProvider
	{
		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x060045D9 RID: 17881 RVA: 0x0016EFFC File Offset: 0x0016EFFC
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x060045DA RID: 17882 RVA: 0x0016F004 File Offset: 0x0016F004
		protected override void InitializeSecurityAttributes()
		{
			GenericParamContext gpContext = default(GenericParamContext);
			IList<SecurityAttribute> value = DeclSecurityReader.Read(this.readerModule, this.permissionSet, gpContext);
			Interlocked.CompareExchange<IList<SecurityAttribute>>(ref this.securityAttributes, value, null);
		}

		// Token: 0x060045DB RID: 17883 RVA: 0x0016F040 File Offset: 0x0016F040
		protected override void InitializeCustomAttributes()
		{
			RidList list = this.readerModule.Metadata.GetCustomAttributeRidList(Table.DeclSecurity, this.origRid);
			CustomAttributeCollection value = new CustomAttributeCollection(list.Count, list, (object list2, int index) => this.readerModule.ReadCustomAttribute(list[index]));
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, value, null);
		}

		// Token: 0x060045DC RID: 17884 RVA: 0x0016F0B4 File Offset: 0x0016F0B4
		protected override void InitializeCustomDebugInfos()
		{
			List<PdbCustomDebugInfo> list = new List<PdbCustomDebugInfo>();
			GenericParamContext gpContext = default(GenericParamContext);
			this.readerModule.InitializeCustomDebugInfos(new MDToken(base.MDToken.Table, this.origRid), gpContext, list);
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, list, null);
		}

		// Token: 0x060045DD RID: 17885 RVA: 0x0016F108 File Offset: 0x0016F108
		public DeclSecurityMD(ModuleDefMD readerModule, uint rid)
		{
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			RawDeclSecurityRow rawDeclSecurityRow;
			readerModule.TablesStream.TryReadDeclSecurityRow(this.origRid, out rawDeclSecurityRow);
			this.permissionSet = rawDeclSecurityRow.PermissionSet;
			this.action = (SecurityAction)rawDeclSecurityRow.Action;
		}

		// Token: 0x060045DE RID: 17886 RVA: 0x0016F160 File Offset: 0x0016F160
		public override byte[] GetBlob()
		{
			return this.readerModule.BlobStream.Read(this.permissionSet);
		}

		// Token: 0x0400245B RID: 9307
		private readonly ModuleDefMD readerModule;

		// Token: 0x0400245C RID: 9308
		private readonly uint origRid;

		// Token: 0x0400245D RID: 9309
		private readonly uint permissionSet;
	}
}
