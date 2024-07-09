using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x020007DF RID: 2015
	[ComVisible(true)]
	public interface IHasCustomDebugInformation
	{
		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x060048AF RID: 18607
		int HasCustomDebugInformationTag { get; }

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x060048B0 RID: 18608
		IList<PdbCustomDebugInfo> CustomDebugInfos { get; }

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x060048B1 RID: 18609
		bool HasCustomDebugInfos { get; }
	}
}
