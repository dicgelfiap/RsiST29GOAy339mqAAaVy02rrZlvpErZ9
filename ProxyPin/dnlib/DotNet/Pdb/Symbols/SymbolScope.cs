using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Symbols
{
	// Token: 0x02000934 RID: 2356
	[ComVisible(true)]
	public abstract class SymbolScope
	{
		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x06005AC0 RID: 23232
		public abstract SymbolMethod Method { get; }

		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x06005AC1 RID: 23233
		public abstract SymbolScope Parent { get; }

		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x06005AC2 RID: 23234
		public abstract int StartOffset { get; }

		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x06005AC3 RID: 23235
		public abstract int EndOffset { get; }

		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x06005AC4 RID: 23236
		public abstract IList<SymbolScope> Children { get; }

		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x06005AC5 RID: 23237
		public abstract IList<SymbolVariable> Locals { get; }

		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x06005AC6 RID: 23238
		public abstract IList<SymbolNamespace> Namespaces { get; }

		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x06005AC7 RID: 23239
		public abstract IList<PdbCustomDebugInfo> CustomDebugInfos { get; }

		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x06005AC8 RID: 23240
		public abstract PdbImportScope ImportScope { get; }

		// Token: 0x06005AC9 RID: 23241
		public abstract IList<PdbConstant> GetConstants(ModuleDef module, GenericParamContext gpContext);
	}
}
