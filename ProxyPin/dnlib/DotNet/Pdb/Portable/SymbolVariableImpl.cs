using System;
using dnlib.DotNet.Pdb.Symbols;

namespace dnlib.DotNet.Pdb.Portable
{
	// Token: 0x02000947 RID: 2375
	internal sealed class SymbolVariableImpl : SymbolVariable
	{
		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x06005B48 RID: 23368 RVA: 0x001BDBB4 File Offset: 0x001BDBB4
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700133C RID: 4924
		// (get) Token: 0x06005B49 RID: 23369 RVA: 0x001BDBBC File Offset: 0x001BDBBC
		public override PdbLocalAttributes Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x1700133D RID: 4925
		// (get) Token: 0x06005B4A RID: 23370 RVA: 0x001BDBC4 File Offset: 0x001BDBC4
		public override int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x06005B4B RID: 23371 RVA: 0x001BDBCC File Offset: 0x001BDBCC
		public override PdbCustomDebugInfo[] CustomDebugInfos
		{
			get
			{
				return this.customDebugInfos;
			}
		}

		// Token: 0x06005B4C RID: 23372 RVA: 0x001BDBD4 File Offset: 0x001BDBD4
		public SymbolVariableImpl(string name, PdbLocalAttributes attributes, int index, PdbCustomDebugInfo[] customDebugInfos)
		{
			this.name = name;
			this.attributes = attributes;
			this.index = index;
			this.customDebugInfos = customDebugInfos;
		}

		// Token: 0x04002C22 RID: 11298
		private readonly string name;

		// Token: 0x04002C23 RID: 11299
		private readonly PdbLocalAttributes attributes;

		// Token: 0x04002C24 RID: 11300
		private readonly int index;

		// Token: 0x04002C25 RID: 11301
		private readonly PdbCustomDebugInfo[] customDebugInfos;
	}
}
