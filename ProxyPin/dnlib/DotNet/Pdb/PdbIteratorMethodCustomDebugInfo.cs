using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x0200090C RID: 2316
	[ComVisible(true)]
	public sealed class PdbIteratorMethodCustomDebugInfo : PdbCustomDebugInfo
	{
		// Token: 0x170012B6 RID: 4790
		// (get) Token: 0x06005991 RID: 22929 RVA: 0x001B5E80 File Offset: 0x001B5E80
		public override PdbCustomDebugInfoKind Kind
		{
			get
			{
				return PdbCustomDebugInfoKind.IteratorMethod;
			}
		}

		// Token: 0x170012B7 RID: 4791
		// (get) Token: 0x06005992 RID: 22930 RVA: 0x001B5E88 File Offset: 0x001B5E88
		public override Guid Guid
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x170012B8 RID: 4792
		// (get) Token: 0x06005993 RID: 22931 RVA: 0x001B5E90 File Offset: 0x001B5E90
		// (set) Token: 0x06005994 RID: 22932 RVA: 0x001B5E98 File Offset: 0x001B5E98
		public MethodDef KickoffMethod { get; set; }

		// Token: 0x06005995 RID: 22933 RVA: 0x001B5EA4 File Offset: 0x001B5EA4
		public PdbIteratorMethodCustomDebugInfo()
		{
		}

		// Token: 0x06005996 RID: 22934 RVA: 0x001B5EAC File Offset: 0x001B5EAC
		public PdbIteratorMethodCustomDebugInfo(MethodDef kickoffMethod)
		{
			this.KickoffMethod = kickoffMethod;
		}
	}
}
