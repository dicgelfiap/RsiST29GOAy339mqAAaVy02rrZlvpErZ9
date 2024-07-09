using System;
using dnlib.DotNet.Pdb.Symbols;

namespace dnlib.DotNet.Pdb.Managed
{
	// Token: 0x0200094B RID: 2379
	internal sealed class DbiNamespace : SymbolNamespace
	{
		// Token: 0x17001356 RID: 4950
		// (get) Token: 0x06005B7B RID: 23419 RVA: 0x001BE688 File Offset: 0x001BE688
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06005B7C RID: 23420 RVA: 0x001BE690 File Offset: 0x001BE690
		public DbiNamespace(string ns)
		{
			this.name = ns;
		}

		// Token: 0x04002C3E RID: 11326
		private readonly string name;
	}
}
