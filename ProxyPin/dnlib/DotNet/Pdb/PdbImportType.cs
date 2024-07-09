using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000915 RID: 2325
	[DebuggerDisplay("{GetDebuggerString(),nq}")]
	[ComVisible(true)]
	public sealed class PdbImportType : PdbImport
	{
		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x060059CA RID: 22986 RVA: 0x001B62A0 File Offset: 0x001B62A0
		public sealed override PdbImportDefinitionKind Kind
		{
			get
			{
				return PdbImportDefinitionKind.ImportType;
			}
		}

		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x060059CB RID: 22987 RVA: 0x001B62A4 File Offset: 0x001B62A4
		// (set) Token: 0x060059CC RID: 22988 RVA: 0x001B62AC File Offset: 0x001B62AC
		public ITypeDefOrRef TargetType { get; set; }

		// Token: 0x060059CD RID: 22989 RVA: 0x001B62B8 File Offset: 0x001B62B8
		public PdbImportType()
		{
		}

		// Token: 0x060059CE RID: 22990 RVA: 0x001B62C0 File Offset: 0x001B62C0
		public PdbImportType(ITypeDefOrRef targetType)
		{
			this.TargetType = targetType;
		}

		// Token: 0x060059CF RID: 22991 RVA: 0x001B62D0 File Offset: 0x001B62D0
		internal sealed override void PreventNewClasses()
		{
		}

		// Token: 0x060059D0 RID: 22992 RVA: 0x001B62D4 File Offset: 0x001B62D4
		private string GetDebuggerString()
		{
			return string.Format("{0}: {1}", this.Kind, this.TargetType);
		}
	}
}
