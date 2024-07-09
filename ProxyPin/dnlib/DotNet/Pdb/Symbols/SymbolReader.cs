using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Symbols
{
	// Token: 0x02000933 RID: 2355
	[ComVisible(true)]
	public abstract class SymbolReader : IDisposable
	{
		// Token: 0x06005AB8 RID: 23224
		public abstract void Initialize(ModuleDef module);

		// Token: 0x17001314 RID: 4884
		// (get) Token: 0x06005AB9 RID: 23225
		public abstract PdbFileKind PdbFileKind { get; }

		// Token: 0x17001315 RID: 4885
		// (get) Token: 0x06005ABA RID: 23226
		public abstract int UserEntryPoint { get; }

		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x06005ABB RID: 23227
		public abstract IList<SymbolDocument> Documents { get; }

		// Token: 0x06005ABC RID: 23228
		public abstract SymbolMethod GetMethod(MethodDef method, int version);

		// Token: 0x06005ABD RID: 23229
		public abstract void GetCustomDebugInfos(int token, GenericParamContext gpContext, IList<PdbCustomDebugInfo> result);

		// Token: 0x06005ABE RID: 23230 RVA: 0x001B9E70 File Offset: 0x001B9E70
		public virtual void Dispose()
		{
		}
	}
}
