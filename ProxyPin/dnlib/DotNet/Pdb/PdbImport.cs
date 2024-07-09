using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000912 RID: 2322
	[ComVisible(true)]
	public abstract class PdbImport
	{
		// Token: 0x170012C8 RID: 4808
		// (get) Token: 0x060059B7 RID: 22967
		public abstract PdbImportDefinitionKind Kind { get; }

		// Token: 0x060059B8 RID: 22968
		internal abstract void PreventNewClasses();
	}
}
