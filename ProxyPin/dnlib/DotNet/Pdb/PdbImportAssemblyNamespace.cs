using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000914 RID: 2324
	[DebuggerDisplay("{GetDebuggerString(),nq}")]
	[ComVisible(true)]
	public sealed class PdbImportAssemblyNamespace : PdbImport
	{
		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x060059C1 RID: 22977 RVA: 0x001B621C File Offset: 0x001B621C
		public sealed override PdbImportDefinitionKind Kind
		{
			get
			{
				return PdbImportDefinitionKind.ImportAssemblyNamespace;
			}
		}

		// Token: 0x170012CC RID: 4812
		// (get) Token: 0x060059C2 RID: 22978 RVA: 0x001B6220 File Offset: 0x001B6220
		// (set) Token: 0x060059C3 RID: 22979 RVA: 0x001B6228 File Offset: 0x001B6228
		public AssemblyRef TargetAssembly { get; set; }

		// Token: 0x170012CD RID: 4813
		// (get) Token: 0x060059C4 RID: 22980 RVA: 0x001B6234 File Offset: 0x001B6234
		// (set) Token: 0x060059C5 RID: 22981 RVA: 0x001B623C File Offset: 0x001B623C
		public string TargetNamespace { get; set; }

		// Token: 0x060059C6 RID: 22982 RVA: 0x001B6248 File Offset: 0x001B6248
		public PdbImportAssemblyNamespace()
		{
		}

		// Token: 0x060059C7 RID: 22983 RVA: 0x001B6250 File Offset: 0x001B6250
		public PdbImportAssemblyNamespace(AssemblyRef targetAssembly, string targetNamespace)
		{
			this.TargetAssembly = targetAssembly;
			this.TargetNamespace = targetNamespace;
		}

		// Token: 0x060059C8 RID: 22984 RVA: 0x001B6268 File Offset: 0x001B6268
		internal sealed override void PreventNewClasses()
		{
		}

		// Token: 0x060059C9 RID: 22985 RVA: 0x001B626C File Offset: 0x001B626C
		private string GetDebuggerString()
		{
			return string.Format("{0}: {1} {2}", this.Kind, this.TargetAssembly, this.TargetNamespace);
		}
	}
}
