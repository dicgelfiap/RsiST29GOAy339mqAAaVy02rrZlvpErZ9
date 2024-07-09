using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using dnlib.DotNet.Emit;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000921 RID: 2337
	[DebuggerDisplay("{Start} - {End}")]
	[ComVisible(true)]
	public sealed class PdbScope : IHasCustomDebugInformation
	{
		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x06005A23 RID: 23075 RVA: 0x001B6920 File Offset: 0x001B6920
		// (set) Token: 0x06005A24 RID: 23076 RVA: 0x001B6928 File Offset: 0x001B6928
		public Instruction Start { get; set; }

		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x06005A25 RID: 23077 RVA: 0x001B6934 File Offset: 0x001B6934
		// (set) Token: 0x06005A26 RID: 23078 RVA: 0x001B693C File Offset: 0x001B693C
		public Instruction End { get; set; }

		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x06005A27 RID: 23079 RVA: 0x001B6948 File Offset: 0x001B6948
		public IList<PdbScope> Scopes
		{
			get
			{
				return this.scopes;
			}
		}

		// Token: 0x170012F1 RID: 4849
		// (get) Token: 0x06005A28 RID: 23080 RVA: 0x001B6950 File Offset: 0x001B6950
		public bool HasScopes
		{
			get
			{
				return this.scopes.Count > 0;
			}
		}

		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x06005A29 RID: 23081 RVA: 0x001B6960 File Offset: 0x001B6960
		public IList<PdbLocal> Variables
		{
			get
			{
				return this.locals;
			}
		}

		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x06005A2A RID: 23082 RVA: 0x001B6968 File Offset: 0x001B6968
		public bool HasVariables
		{
			get
			{
				return this.locals.Count > 0;
			}
		}

		// Token: 0x170012F4 RID: 4852
		// (get) Token: 0x06005A2B RID: 23083 RVA: 0x001B6978 File Offset: 0x001B6978
		public IList<string> Namespaces
		{
			get
			{
				return this.namespaces;
			}
		}

		// Token: 0x170012F5 RID: 4853
		// (get) Token: 0x06005A2C RID: 23084 RVA: 0x001B6980 File Offset: 0x001B6980
		public bool HasNamespaces
		{
			get
			{
				return this.namespaces.Count > 0;
			}
		}

		// Token: 0x170012F6 RID: 4854
		// (get) Token: 0x06005A2D RID: 23085 RVA: 0x001B6990 File Offset: 0x001B6990
		// (set) Token: 0x06005A2E RID: 23086 RVA: 0x001B6998 File Offset: 0x001B6998
		public PdbImportScope ImportScope { get; set; }

		// Token: 0x170012F7 RID: 4855
		// (get) Token: 0x06005A2F RID: 23087 RVA: 0x001B69A4 File Offset: 0x001B69A4
		public IList<PdbConstant> Constants
		{
			get
			{
				return this.constants;
			}
		}

		// Token: 0x170012F8 RID: 4856
		// (get) Token: 0x06005A30 RID: 23088 RVA: 0x001B69AC File Offset: 0x001B69AC
		public bool HasConstants
		{
			get
			{
				return this.constants.Count > 0;
			}
		}

		// Token: 0x170012F9 RID: 4857
		// (get) Token: 0x06005A31 RID: 23089 RVA: 0x001B69BC File Offset: 0x001B69BC
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170012FA RID: 4858
		// (get) Token: 0x06005A32 RID: 23090 RVA: 0x001B69C0 File Offset: 0x001B69C0
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x170012FB RID: 4859
		// (get) Token: 0x06005A33 RID: 23091 RVA: 0x001B69D0 File Offset: 0x001B69D0
		public IList<PdbCustomDebugInfo> CustomDebugInfos
		{
			get
			{
				return this.customDebugInfos;
			}
		}

		// Token: 0x04002B9C RID: 11164
		private readonly IList<PdbScope> scopes = new List<PdbScope>();

		// Token: 0x04002B9D RID: 11165
		private readonly IList<PdbLocal> locals = new List<PdbLocal>();

		// Token: 0x04002B9E RID: 11166
		private readonly IList<string> namespaces = new List<string>();

		// Token: 0x04002B9F RID: 11167
		private readonly IList<PdbConstant> constants = new List<PdbConstant>();

		// Token: 0x04002BA3 RID: 11171
		private readonly IList<PdbCustomDebugInfo> customDebugInfos = new List<PdbCustomDebugInfo>();
	}
}
