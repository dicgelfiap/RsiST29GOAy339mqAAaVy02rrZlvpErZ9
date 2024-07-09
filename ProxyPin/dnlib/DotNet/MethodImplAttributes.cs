using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000813 RID: 2067
	[Flags]
	[ComVisible(true)]
	public enum MethodImplAttributes : ushort
	{
		// Token: 0x040025B1 RID: 9649
		CodeTypeMask = 3,
		// Token: 0x040025B2 RID: 9650
		IL = 0,
		// Token: 0x040025B3 RID: 9651
		Native = 1,
		// Token: 0x040025B4 RID: 9652
		OPTIL = 2,
		// Token: 0x040025B5 RID: 9653
		Runtime = 3,
		// Token: 0x040025B6 RID: 9654
		ManagedMask = 4,
		// Token: 0x040025B7 RID: 9655
		Unmanaged = 4,
		// Token: 0x040025B8 RID: 9656
		Managed = 0,
		// Token: 0x040025B9 RID: 9657
		ForwardRef = 16,
		// Token: 0x040025BA RID: 9658
		PreserveSig = 128,
		// Token: 0x040025BB RID: 9659
		InternalCall = 4096,
		// Token: 0x040025BC RID: 9660
		Synchronized = 32,
		// Token: 0x040025BD RID: 9661
		NoInlining = 8,
		// Token: 0x040025BE RID: 9662
		AggressiveInlining = 256,
		// Token: 0x040025BF RID: 9663
		NoOptimization = 64,
		// Token: 0x040025C0 RID: 9664
		AggressiveOptimization = 512,
		// Token: 0x040025C1 RID: 9665
		SecurityMitigations = 1024
	}
}
