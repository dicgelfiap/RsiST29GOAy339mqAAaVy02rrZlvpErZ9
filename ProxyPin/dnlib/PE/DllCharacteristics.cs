using System;
using System.Runtime.InteropServices;

namespace dnlib.PE
{
	// Token: 0x02000748 RID: 1864
	[Flags]
	[ComVisible(true)]
	public enum DllCharacteristics : ushort
	{
		// Token: 0x040022AE RID: 8878
		Reserved1 = 1,
		// Token: 0x040022AF RID: 8879
		Reserved2 = 2,
		// Token: 0x040022B0 RID: 8880
		Reserved3 = 4,
		// Token: 0x040022B1 RID: 8881
		Reserved4 = 8,
		// Token: 0x040022B2 RID: 8882
		Reserved5 = 16,
		// Token: 0x040022B3 RID: 8883
		HighEntropyVA = 32,
		// Token: 0x040022B4 RID: 8884
		DynamicBase = 64,
		// Token: 0x040022B5 RID: 8885
		ForceIntegrity = 128,
		// Token: 0x040022B6 RID: 8886
		NxCompat = 256,
		// Token: 0x040022B7 RID: 8887
		NoIsolation = 512,
		// Token: 0x040022B8 RID: 8888
		NoSeh = 1024,
		// Token: 0x040022B9 RID: 8889
		NoBind = 2048,
		// Token: 0x040022BA RID: 8890
		AppContainer = 4096,
		// Token: 0x040022BB RID: 8891
		WdmDriver = 8192,
		// Token: 0x040022BC RID: 8892
		GuardCf = 16384,
		// Token: 0x040022BD RID: 8893
		TerminalServerAware = 32768
	}
}
