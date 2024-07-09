using System;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000924 RID: 2340
	internal static class PdbUtils
	{
		// Token: 0x06005A4D RID: 23117 RVA: 0x001B7590 File Offset: 0x001B7590
		public static bool IsEndInclusive(PdbFileKind pdbFileKind, Compiler compiler)
		{
			return pdbFileKind == PdbFileKind.WindowsPDB && compiler == Compiler.VisualBasic;
		}
	}
}
