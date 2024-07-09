using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000913 RID: 2323
	[DebuggerDisplay("{GetDebuggerString(),nq}")]
	[ComVisible(true)]
	public sealed class PdbImportNamespace : PdbImport
	{
		// Token: 0x170012C9 RID: 4809
		// (get) Token: 0x060059BA RID: 22970 RVA: 0x001B61C8 File Offset: 0x001B61C8
		public sealed override PdbImportDefinitionKind Kind
		{
			get
			{
				return PdbImportDefinitionKind.ImportNamespace;
			}
		}

		// Token: 0x170012CA RID: 4810
		// (get) Token: 0x060059BB RID: 22971 RVA: 0x001B61CC File Offset: 0x001B61CC
		// (set) Token: 0x060059BC RID: 22972 RVA: 0x001B61D4 File Offset: 0x001B61D4
		public string TargetNamespace { get; set; }

		// Token: 0x060059BD RID: 22973 RVA: 0x001B61E0 File Offset: 0x001B61E0
		public PdbImportNamespace()
		{
		}

		// Token: 0x060059BE RID: 22974 RVA: 0x001B61E8 File Offset: 0x001B61E8
		public PdbImportNamespace(string targetNamespace)
		{
			this.TargetNamespace = targetNamespace;
		}

		// Token: 0x060059BF RID: 22975 RVA: 0x001B61F8 File Offset: 0x001B61F8
		internal sealed override void PreventNewClasses()
		{
		}

		// Token: 0x060059C0 RID: 22976 RVA: 0x001B61FC File Offset: 0x001B61FC
		private string GetDebuggerString()
		{
			return string.Format("{0}: {1}", this.Kind, this.TargetNamespace);
		}
	}
}
