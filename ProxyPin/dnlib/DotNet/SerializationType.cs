using System;

namespace dnlib.DotNet
{
	// Token: 0x02000844 RID: 2116
	internal enum SerializationType : byte
	{
		// Token: 0x040026EF RID: 9967
		Undefined,
		// Token: 0x040026F0 RID: 9968
		Boolean = 2,
		// Token: 0x040026F1 RID: 9969
		Char,
		// Token: 0x040026F2 RID: 9970
		I1,
		// Token: 0x040026F3 RID: 9971
		U1,
		// Token: 0x040026F4 RID: 9972
		I2,
		// Token: 0x040026F5 RID: 9973
		U2,
		// Token: 0x040026F6 RID: 9974
		I4,
		// Token: 0x040026F7 RID: 9975
		U4,
		// Token: 0x040026F8 RID: 9976
		I8,
		// Token: 0x040026F9 RID: 9977
		U8,
		// Token: 0x040026FA RID: 9978
		R4,
		// Token: 0x040026FB RID: 9979
		R8,
		// Token: 0x040026FC RID: 9980
		String,
		// Token: 0x040026FD RID: 9981
		SZArray = 29,
		// Token: 0x040026FE RID: 9982
		Type = 80,
		// Token: 0x040026FF RID: 9983
		TaggedObject,
		// Token: 0x04002700 RID: 9984
		Field = 83,
		// Token: 0x04002701 RID: 9985
		Property,
		// Token: 0x04002702 RID: 9986
		Enum
	}
}
