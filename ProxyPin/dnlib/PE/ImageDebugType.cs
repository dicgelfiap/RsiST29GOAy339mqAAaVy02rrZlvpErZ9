using System;
using System.Runtime.InteropServices;

namespace dnlib.PE
{
	// Token: 0x0200074C RID: 1868
	[ComVisible(true)]
	public enum ImageDebugType : uint
	{
		// Token: 0x040022C9 RID: 8905
		Unknown,
		// Token: 0x040022CA RID: 8906
		Coff,
		// Token: 0x040022CB RID: 8907
		CodeView,
		// Token: 0x040022CC RID: 8908
		FPO,
		// Token: 0x040022CD RID: 8909
		Misc,
		// Token: 0x040022CE RID: 8910
		Exception,
		// Token: 0x040022CF RID: 8911
		Fixup,
		// Token: 0x040022D0 RID: 8912
		OmapToSrc,
		// Token: 0x040022D1 RID: 8913
		OmapFromSrc,
		// Token: 0x040022D2 RID: 8914
		Borland,
		// Token: 0x040022D3 RID: 8915
		Reserved10,
		// Token: 0x040022D4 RID: 8916
		CLSID,
		// Token: 0x040022D5 RID: 8917
		VcFeature,
		// Token: 0x040022D6 RID: 8918
		POGO,
		// Token: 0x040022D7 RID: 8919
		ILTCG,
		// Token: 0x040022D8 RID: 8920
		MPX,
		// Token: 0x040022D9 RID: 8921
		Reproducible,
		// Token: 0x040022DA RID: 8922
		EmbeddedPortablePdb,
		// Token: 0x040022DB RID: 8923
		PdbChecksum = 19U
	}
}
