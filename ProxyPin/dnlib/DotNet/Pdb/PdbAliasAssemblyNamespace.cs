using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x0200091A RID: 2330
	[DebuggerDisplay("{GetDebuggerString(),nq}")]
	[ComVisible(true)]
	public sealed class PdbAliasAssemblyNamespace : PdbImport
	{
		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x060059F3 RID: 23027 RVA: 0x001B64D4 File Offset: 0x001B64D4
		public sealed override PdbImportDefinitionKind Kind
		{
			get
			{
				return PdbImportDefinitionKind.AliasAssemblyNamespace;
			}
		}

		// Token: 0x170012DC RID: 4828
		// (get) Token: 0x060059F4 RID: 23028 RVA: 0x001B64D8 File Offset: 0x001B64D8
		// (set) Token: 0x060059F5 RID: 23029 RVA: 0x001B64E0 File Offset: 0x001B64E0
		public string Alias { get; set; }

		// Token: 0x170012DD RID: 4829
		// (get) Token: 0x060059F6 RID: 23030 RVA: 0x001B64EC File Offset: 0x001B64EC
		// (set) Token: 0x060059F7 RID: 23031 RVA: 0x001B64F4 File Offset: 0x001B64F4
		public AssemblyRef TargetAssembly { get; set; }

		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x060059F8 RID: 23032 RVA: 0x001B6500 File Offset: 0x001B6500
		// (set) Token: 0x060059F9 RID: 23033 RVA: 0x001B6508 File Offset: 0x001B6508
		public string TargetNamespace { get; set; }

		// Token: 0x060059FA RID: 23034 RVA: 0x001B6514 File Offset: 0x001B6514
		public PdbAliasAssemblyNamespace()
		{
		}

		// Token: 0x060059FB RID: 23035 RVA: 0x001B651C File Offset: 0x001B651C
		public PdbAliasAssemblyNamespace(string alias, AssemblyRef targetAssembly, string targetNamespace)
		{
			this.Alias = alias;
			this.TargetAssembly = targetAssembly;
			this.TargetNamespace = targetNamespace;
		}

		// Token: 0x060059FC RID: 23036 RVA: 0x001B6548 File Offset: 0x001B6548
		internal sealed override void PreventNewClasses()
		{
		}

		// Token: 0x060059FD RID: 23037 RVA: 0x001B654C File Offset: 0x001B654C
		private string GetDebuggerString()
		{
			return string.Format("{0}: {1} = {2} {3}", new object[]
			{
				this.Kind,
				this.Alias,
				this.TargetAssembly,
				this.TargetNamespace
			});
		}
	}
}
