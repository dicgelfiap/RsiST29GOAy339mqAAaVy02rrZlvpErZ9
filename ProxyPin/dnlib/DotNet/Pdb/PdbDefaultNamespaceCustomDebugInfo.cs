using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000905 RID: 2309
	[ComVisible(true)]
	public sealed class PdbDefaultNamespaceCustomDebugInfo : PdbCustomDebugInfo
	{
		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x06005969 RID: 22889 RVA: 0x001B5CD4 File Offset: 0x001B5CD4
		public override PdbCustomDebugInfoKind Kind
		{
			get
			{
				return PdbCustomDebugInfoKind.DefaultNamespace;
			}
		}

		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x0600596A RID: 22890 RVA: 0x001B5CDC File Offset: 0x001B5CDC
		public override Guid Guid
		{
			get
			{
				return CustomDebugInfoGuids.DefaultNamespace;
			}
		}

		// Token: 0x170012A4 RID: 4772
		// (get) Token: 0x0600596B RID: 22891 RVA: 0x001B5CE4 File Offset: 0x001B5CE4
		// (set) Token: 0x0600596C RID: 22892 RVA: 0x001B5CEC File Offset: 0x001B5CEC
		public string Namespace { get; set; }

		// Token: 0x0600596D RID: 22893 RVA: 0x001B5CF8 File Offset: 0x001B5CF8
		public PdbDefaultNamespaceCustomDebugInfo()
		{
		}

		// Token: 0x0600596E RID: 22894 RVA: 0x001B5D00 File Offset: 0x001B5D00
		public PdbDefaultNamespaceCustomDebugInfo(string defaultNamespace)
		{
			this.Namespace = defaultNamespace;
		}
	}
}
