using System;
using System.Runtime.InteropServices;

namespace dnlib.PE
{
	// Token: 0x02000758 RID: 1880
	[ComVisible(true)]
	public enum Machine : ushort
	{
		// Token: 0x04002330 RID: 9008
		Unknown,
		// Token: 0x04002331 RID: 9009
		I386 = 332,
		// Token: 0x04002332 RID: 9010
		R3000 = 354,
		// Token: 0x04002333 RID: 9011
		R4000 = 358,
		// Token: 0x04002334 RID: 9012
		R10000 = 360,
		// Token: 0x04002335 RID: 9013
		WCEMIPSV2,
		// Token: 0x04002336 RID: 9014
		ALPHA = 388,
		// Token: 0x04002337 RID: 9015
		SH3 = 418,
		// Token: 0x04002338 RID: 9016
		SH3DSP,
		// Token: 0x04002339 RID: 9017
		SH3E,
		// Token: 0x0400233A RID: 9018
		SH4 = 422,
		// Token: 0x0400233B RID: 9019
		SH5 = 424,
		// Token: 0x0400233C RID: 9020
		ARM = 448,
		// Token: 0x0400233D RID: 9021
		THUMB = 450,
		// Token: 0x0400233E RID: 9022
		ARMNT = 452,
		// Token: 0x0400233F RID: 9023
		AM33 = 467,
		// Token: 0x04002340 RID: 9024
		POWERPC = 496,
		// Token: 0x04002341 RID: 9025
		POWERPCFP,
		// Token: 0x04002342 RID: 9026
		IA64 = 512,
		// Token: 0x04002343 RID: 9027
		MIPS16 = 614,
		// Token: 0x04002344 RID: 9028
		ALPHA64 = 644,
		// Token: 0x04002345 RID: 9029
		MIPSFPU = 870,
		// Token: 0x04002346 RID: 9030
		MIPSFPU16 = 1126,
		// Token: 0x04002347 RID: 9031
		TRICORE = 1312,
		// Token: 0x04002348 RID: 9032
		CEF = 3311,
		// Token: 0x04002349 RID: 9033
		EBC = 3772,
		// Token: 0x0400234A RID: 9034
		AMD64 = 34404,
		// Token: 0x0400234B RID: 9035
		M32R = 36929,
		// Token: 0x0400234C RID: 9036
		ARM64 = 43620,
		// Token: 0x0400234D RID: 9037
		CEE = 49390,
		// Token: 0x0400234E RID: 9038
		I386_Native_Apple = 18184,
		// Token: 0x0400234F RID: 9039
		AMD64_Native_Apple = 49184,
		// Token: 0x04002350 RID: 9040
		ARMNT_Native_Apple = 18304,
		// Token: 0x04002351 RID: 9041
		ARM64_Native_Apple = 60448,
		// Token: 0x04002352 RID: 9042
		I386_Native_FreeBSD = 44168,
		// Token: 0x04002353 RID: 9043
		AMD64_Native_FreeBSD = 11168,
		// Token: 0x04002354 RID: 9044
		ARMNT_Native_FreeBSD = 44032,
		// Token: 0x04002355 RID: 9045
		ARM64_Native_FreeBSD = 1952,
		// Token: 0x04002356 RID: 9046
		I386_Native_Linux = 31285,
		// Token: 0x04002357 RID: 9047
		AMD64_Native_Linux = 64797,
		// Token: 0x04002358 RID: 9048
		ARMNT_Native_Linux = 31421,
		// Token: 0x04002359 RID: 9049
		ARM64_Native_Linux = 53533,
		// Token: 0x0400235A RID: 9050
		I386_Native_NetBSD = 6367,
		// Token: 0x0400235B RID: 9051
		AMD64_Native_NetBSD = 40951,
		// Token: 0x0400235C RID: 9052
		ARMNT_Native_NetBSD = 6231,
		// Token: 0x0400235D RID: 9053
		ARM64_Native_NetBSD = 46071
	}
}
