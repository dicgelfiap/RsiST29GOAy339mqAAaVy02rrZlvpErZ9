using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000917 RID: 2327
	[DebuggerDisplay("{GetDebuggerString(),nq}")]
	[ComVisible(true)]
	public sealed class PdbImportAssemblyReferenceAlias : PdbImport
	{
		// Token: 0x170012D3 RID: 4819
		// (get) Token: 0x060059DA RID: 23002 RVA: 0x001B6378 File Offset: 0x001B6378
		public sealed override PdbImportDefinitionKind Kind
		{
			get
			{
				return PdbImportDefinitionKind.ImportAssemblyReferenceAlias;
			}
		}

		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x060059DB RID: 23003 RVA: 0x001B637C File Offset: 0x001B637C
		// (set) Token: 0x060059DC RID: 23004 RVA: 0x001B6384 File Offset: 0x001B6384
		public string Alias { get; set; }

		// Token: 0x060059DD RID: 23005 RVA: 0x001B6390 File Offset: 0x001B6390
		public PdbImportAssemblyReferenceAlias()
		{
		}

		// Token: 0x060059DE RID: 23006 RVA: 0x001B6398 File Offset: 0x001B6398
		public PdbImportAssemblyReferenceAlias(string alias)
		{
			this.Alias = alias;
		}

		// Token: 0x060059DF RID: 23007 RVA: 0x001B63A8 File Offset: 0x001B63A8
		internal sealed override void PreventNewClasses()
		{
		}

		// Token: 0x060059E0 RID: 23008 RVA: 0x001B63AC File Offset: 0x001B63AC
		private string GetDebuggerString()
		{
			return string.Format("{0}: {1}", this.Kind, this.Alias);
		}
	}
}
