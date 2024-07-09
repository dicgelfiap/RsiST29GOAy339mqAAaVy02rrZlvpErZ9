using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000918 RID: 2328
	[DebuggerDisplay("{GetDebuggerString(),nq}")]
	[ComVisible(true)]
	public sealed class PdbAliasAssemblyReference : PdbImport
	{
		// Token: 0x170012D5 RID: 4821
		// (get) Token: 0x060059E1 RID: 23009 RVA: 0x001B63CC File Offset: 0x001B63CC
		public sealed override PdbImportDefinitionKind Kind
		{
			get
			{
				return PdbImportDefinitionKind.AliasAssemblyReference;
			}
		}

		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x060059E2 RID: 23010 RVA: 0x001B63D0 File Offset: 0x001B63D0
		// (set) Token: 0x060059E3 RID: 23011 RVA: 0x001B63D8 File Offset: 0x001B63D8
		public string Alias { get; set; }

		// Token: 0x170012D7 RID: 4823
		// (get) Token: 0x060059E4 RID: 23012 RVA: 0x001B63E4 File Offset: 0x001B63E4
		// (set) Token: 0x060059E5 RID: 23013 RVA: 0x001B63EC File Offset: 0x001B63EC
		public AssemblyRef TargetAssembly { get; set; }

		// Token: 0x060059E6 RID: 23014 RVA: 0x001B63F8 File Offset: 0x001B63F8
		public PdbAliasAssemblyReference()
		{
		}

		// Token: 0x060059E7 RID: 23015 RVA: 0x001B6400 File Offset: 0x001B6400
		public PdbAliasAssemblyReference(string alias, AssemblyRef targetAssembly)
		{
			this.Alias = alias;
			this.TargetAssembly = targetAssembly;
		}

		// Token: 0x060059E8 RID: 23016 RVA: 0x001B6418 File Offset: 0x001B6418
		internal sealed override void PreventNewClasses()
		{
		}

		// Token: 0x060059E9 RID: 23017 RVA: 0x001B641C File Offset: 0x001B641C
		private string GetDebuggerString()
		{
			return string.Format("{0}: {1} = {2}", this.Kind, this.Alias, this.TargetAssembly);
		}
	}
}
