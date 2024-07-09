using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000909 RID: 2313
	[ComVisible(true)]
	public sealed class PdbSourceServerCustomDebugInfo : PdbCustomDebugInfo
	{
		// Token: 0x170012AE RID: 4782
		// (get) Token: 0x06005981 RID: 22913 RVA: 0x001B5DC4 File Offset: 0x001B5DC4
		public override PdbCustomDebugInfoKind Kind
		{
			get
			{
				return PdbCustomDebugInfoKind.SourceServer;
			}
		}

		// Token: 0x170012AF RID: 4783
		// (get) Token: 0x06005982 RID: 22914 RVA: 0x001B5DCC File Offset: 0x001B5DCC
		public override Guid Guid
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x170012B0 RID: 4784
		// (get) Token: 0x06005983 RID: 22915 RVA: 0x001B5DD4 File Offset: 0x001B5DD4
		// (set) Token: 0x06005984 RID: 22916 RVA: 0x001B5DDC File Offset: 0x001B5DDC
		public byte[] FileBlob { get; set; }

		// Token: 0x06005985 RID: 22917 RVA: 0x001B5DE8 File Offset: 0x001B5DE8
		public PdbSourceServerCustomDebugInfo()
		{
		}

		// Token: 0x06005986 RID: 22918 RVA: 0x001B5DF0 File Offset: 0x001B5DF0
		public PdbSourceServerCustomDebugInfo(byte[] fileBlob)
		{
			this.FileBlob = fileBlob;
		}
	}
}
