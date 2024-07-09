using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000919 RID: 2329
	[DebuggerDisplay("{GetDebuggerString(),nq}")]
	[ComVisible(true)]
	public sealed class PdbAliasNamespace : PdbImport
	{
		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x060059EA RID: 23018 RVA: 0x001B6450 File Offset: 0x001B6450
		public sealed override PdbImportDefinitionKind Kind
		{
			get
			{
				return PdbImportDefinitionKind.AliasNamespace;
			}
		}

		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x060059EB RID: 23019 RVA: 0x001B6454 File Offset: 0x001B6454
		// (set) Token: 0x060059EC RID: 23020 RVA: 0x001B645C File Offset: 0x001B645C
		public string Alias { get; set; }

		// Token: 0x170012DA RID: 4826
		// (get) Token: 0x060059ED RID: 23021 RVA: 0x001B6468 File Offset: 0x001B6468
		// (set) Token: 0x060059EE RID: 23022 RVA: 0x001B6470 File Offset: 0x001B6470
		public string TargetNamespace { get; set; }

		// Token: 0x060059EF RID: 23023 RVA: 0x001B647C File Offset: 0x001B647C
		public PdbAliasNamespace()
		{
		}

		// Token: 0x060059F0 RID: 23024 RVA: 0x001B6484 File Offset: 0x001B6484
		public PdbAliasNamespace(string alias, string targetNamespace)
		{
			this.Alias = alias;
			this.TargetNamespace = targetNamespace;
		}

		// Token: 0x060059F1 RID: 23025 RVA: 0x001B649C File Offset: 0x001B649C
		internal sealed override void PreventNewClasses()
		{
		}

		// Token: 0x060059F2 RID: 23026 RVA: 0x001B64A0 File Offset: 0x001B64A0
		private string GetDebuggerString()
		{
			return string.Format("{0}: {1} = {2}", this.Kind, this.Alias, this.TargetNamespace);
		}
	}
}
