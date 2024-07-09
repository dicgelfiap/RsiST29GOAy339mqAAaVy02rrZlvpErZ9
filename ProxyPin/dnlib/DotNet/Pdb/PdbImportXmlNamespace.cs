using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000916 RID: 2326
	[DebuggerDisplay("{GetDebuggerString(),nq}")]
	[ComVisible(true)]
	public sealed class PdbImportXmlNamespace : PdbImport
	{
		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x060059D1 RID: 22993 RVA: 0x001B62F4 File Offset: 0x001B62F4
		public sealed override PdbImportDefinitionKind Kind
		{
			get
			{
				return PdbImportDefinitionKind.ImportXmlNamespace;
			}
		}

		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x060059D2 RID: 22994 RVA: 0x001B62F8 File Offset: 0x001B62F8
		// (set) Token: 0x060059D3 RID: 22995 RVA: 0x001B6300 File Offset: 0x001B6300
		public string Alias { get; set; }

		// Token: 0x170012D2 RID: 4818
		// (get) Token: 0x060059D4 RID: 22996 RVA: 0x001B630C File Offset: 0x001B630C
		// (set) Token: 0x060059D5 RID: 22997 RVA: 0x001B6314 File Offset: 0x001B6314
		public string TargetNamespace { get; set; }

		// Token: 0x060059D6 RID: 22998 RVA: 0x001B6320 File Offset: 0x001B6320
		public PdbImportXmlNamespace()
		{
		}

		// Token: 0x060059D7 RID: 22999 RVA: 0x001B6328 File Offset: 0x001B6328
		public PdbImportXmlNamespace(string alias, string targetNamespace)
		{
			this.Alias = alias;
			this.TargetNamespace = targetNamespace;
		}

		// Token: 0x060059D8 RID: 23000 RVA: 0x001B6340 File Offset: 0x001B6340
		internal sealed override void PreventNewClasses()
		{
		}

		// Token: 0x060059D9 RID: 23001 RVA: 0x001B6344 File Offset: 0x001B6344
		private string GetDebuggerString()
		{
			return string.Format("{0}: {1} = {2}", this.Kind, this.Alias, this.TargetNamespace);
		}
	}
}
