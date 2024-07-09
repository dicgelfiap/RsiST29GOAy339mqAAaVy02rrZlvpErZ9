using System;
using dnlib.DotNet.Pdb.Symbols;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x0200097C RID: 2428
	internal sealed class SymbolNamespaceImpl : SymbolNamespace
	{
		// Token: 0x06005D8B RID: 23947 RVA: 0x001C13C0 File Offset: 0x001C13C0
		public SymbolNamespaceImpl(ISymUnmanagedNamespace @namespace)
		{
			this.ns = @namespace;
		}

		// Token: 0x1700137B RID: 4987
		// (get) Token: 0x06005D8C RID: 23948 RVA: 0x001C13D0 File Offset: 0x001C13D0
		public override string Name
		{
			get
			{
				uint num;
				this.ns.GetName(0U, out num, null);
				char[] array = new char[num];
				this.ns.GetName((uint)array.Length, out num, array);
				if (array.Length == 0)
				{
					return string.Empty;
				}
				return new string(array, 0, array.Length - 1);
			}
		}

		// Token: 0x04002D64 RID: 11620
		private readonly ISymUnmanagedNamespace ns;
	}
}
