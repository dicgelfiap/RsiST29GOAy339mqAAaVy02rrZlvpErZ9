using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x0200091B RID: 2331
	[DebuggerDisplay("{GetDebuggerString(),nq}")]
	[ComVisible(true)]
	public sealed class PdbAliasType : PdbImport
	{
		// Token: 0x170012DF RID: 4831
		// (get) Token: 0x060059FE RID: 23038 RVA: 0x001B6598 File Offset: 0x001B6598
		public sealed override PdbImportDefinitionKind Kind
		{
			get
			{
				return PdbImportDefinitionKind.AliasType;
			}
		}

		// Token: 0x170012E0 RID: 4832
		// (get) Token: 0x060059FF RID: 23039 RVA: 0x001B659C File Offset: 0x001B659C
		// (set) Token: 0x06005A00 RID: 23040 RVA: 0x001B65A4 File Offset: 0x001B65A4
		public string Alias { get; set; }

		// Token: 0x170012E1 RID: 4833
		// (get) Token: 0x06005A01 RID: 23041 RVA: 0x001B65B0 File Offset: 0x001B65B0
		// (set) Token: 0x06005A02 RID: 23042 RVA: 0x001B65B8 File Offset: 0x001B65B8
		public ITypeDefOrRef TargetType { get; set; }

		// Token: 0x06005A03 RID: 23043 RVA: 0x001B65C4 File Offset: 0x001B65C4
		public PdbAliasType()
		{
		}

		// Token: 0x06005A04 RID: 23044 RVA: 0x001B65CC File Offset: 0x001B65CC
		public PdbAliasType(string alias, ITypeDefOrRef targetType)
		{
			this.Alias = alias;
			this.TargetType = targetType;
		}

		// Token: 0x06005A05 RID: 23045 RVA: 0x001B65E4 File Offset: 0x001B65E4
		internal sealed override void PreventNewClasses()
		{
		}

		// Token: 0x06005A06 RID: 23046 RVA: 0x001B65E8 File Offset: 0x001B65E8
		private string GetDebuggerString()
		{
			return string.Format("{0}: {1} = {2}", this.Kind, this.Alias, this.TargetType);
		}
	}
}
