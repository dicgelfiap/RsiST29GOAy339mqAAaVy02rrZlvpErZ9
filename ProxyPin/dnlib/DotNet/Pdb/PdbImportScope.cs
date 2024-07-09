using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000910 RID: 2320
	[ComVisible(true)]
	public sealed class PdbImportScope : IHasCustomDebugInformation
	{
		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x060059B0 RID: 22960 RVA: 0x001B6178 File Offset: 0x001B6178
		// (set) Token: 0x060059B1 RID: 22961 RVA: 0x001B6180 File Offset: 0x001B6180
		public PdbImportScope Parent { get; set; }

		// Token: 0x170012C3 RID: 4803
		// (get) Token: 0x060059B2 RID: 22962 RVA: 0x001B618C File Offset: 0x001B618C
		public IList<PdbImport> Imports
		{
			get
			{
				return this.imports;
			}
		}

		// Token: 0x170012C4 RID: 4804
		// (get) Token: 0x060059B3 RID: 22963 RVA: 0x001B6194 File Offset: 0x001B6194
		public bool HasImports
		{
			get
			{
				return this.imports.Count > 0;
			}
		}

		// Token: 0x170012C5 RID: 4805
		// (get) Token: 0x060059B4 RID: 22964 RVA: 0x001B61A4 File Offset: 0x001B61A4
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x170012C6 RID: 4806
		// (get) Token: 0x060059B5 RID: 22965 RVA: 0x001B61A8 File Offset: 0x001B61A8
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x170012C7 RID: 4807
		// (get) Token: 0x060059B6 RID: 22966 RVA: 0x001B61B8 File Offset: 0x001B61B8
		public IList<PdbCustomDebugInfo> CustomDebugInfos
		{
			get
			{
				return this.customDebugInfos;
			}
		}

		// Token: 0x04002B6F RID: 11119
		private readonly IList<PdbImport> imports = new List<PdbImport>();

		// Token: 0x04002B71 RID: 11121
		private readonly IList<PdbCustomDebugInfo> customDebugInfos = new List<PdbCustomDebugInfo>();
	}
}
