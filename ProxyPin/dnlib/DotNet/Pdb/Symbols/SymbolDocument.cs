using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Symbols
{
	// Token: 0x02000930 RID: 2352
	[ComVisible(true)]
	public abstract class SymbolDocument
	{
		// Token: 0x17001309 RID: 4873
		// (get) Token: 0x06005AA9 RID: 23209
		public abstract string URL { get; }

		// Token: 0x1700130A RID: 4874
		// (get) Token: 0x06005AAA RID: 23210
		public abstract Guid Language { get; }

		// Token: 0x1700130B RID: 4875
		// (get) Token: 0x06005AAB RID: 23211
		public abstract Guid LanguageVendor { get; }

		// Token: 0x1700130C RID: 4876
		// (get) Token: 0x06005AAC RID: 23212
		public abstract Guid DocumentType { get; }

		// Token: 0x1700130D RID: 4877
		// (get) Token: 0x06005AAD RID: 23213
		public abstract Guid CheckSumAlgorithmId { get; }

		// Token: 0x1700130E RID: 4878
		// (get) Token: 0x06005AAE RID: 23214
		public abstract byte[] CheckSum { get; }

		// Token: 0x1700130F RID: 4879
		// (get) Token: 0x06005AAF RID: 23215
		public abstract PdbCustomDebugInfo[] CustomDebugInfos { get; }
	}
}
