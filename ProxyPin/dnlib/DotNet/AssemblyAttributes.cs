using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000774 RID: 1908
	[Flags]
	[ComVisible(true)]
	public enum AssemblyAttributes : uint
	{
		// Token: 0x040023A1 RID: 9121
		None = 0U,
		// Token: 0x040023A2 RID: 9122
		PublicKey = 1U,
		// Token: 0x040023A3 RID: 9123
		PA_None = 0U,
		// Token: 0x040023A4 RID: 9124
		PA_MSIL = 16U,
		// Token: 0x040023A5 RID: 9125
		PA_x86 = 32U,
		// Token: 0x040023A6 RID: 9126
		PA_IA64 = 48U,
		// Token: 0x040023A7 RID: 9127
		PA_AMD64 = 64U,
		// Token: 0x040023A8 RID: 9128
		PA_ARM = 80U,
		// Token: 0x040023A9 RID: 9129
		PA_NoPlatform = 112U,
		// Token: 0x040023AA RID: 9130
		PA_Specified = 128U,
		// Token: 0x040023AB RID: 9131
		PA_Mask = 112U,
		// Token: 0x040023AC RID: 9132
		PA_FullMask = 240U,
		// Token: 0x040023AD RID: 9133
		PA_Shift = 4U,
		// Token: 0x040023AE RID: 9134
		EnableJITcompileTracking = 32768U,
		// Token: 0x040023AF RID: 9135
		DisableJITcompileOptimizer = 16384U,
		// Token: 0x040023B0 RID: 9136
		Retargetable = 256U,
		// Token: 0x040023B1 RID: 9137
		ContentType_Default = 0U,
		// Token: 0x040023B2 RID: 9138
		ContentType_WindowsRuntime = 512U,
		// Token: 0x040023B3 RID: 9139
		ContentType_Mask = 3584U
	}
}
