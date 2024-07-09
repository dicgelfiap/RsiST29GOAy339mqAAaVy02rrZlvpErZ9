using System;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000B0F RID: 2831
	[Flags]
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public enum JsonSchemaType
	{
		// Token: 0x04003846 RID: 14406
		None = 0,
		// Token: 0x04003847 RID: 14407
		String = 1,
		// Token: 0x04003848 RID: 14408
		Float = 2,
		// Token: 0x04003849 RID: 14409
		Integer = 4,
		// Token: 0x0400384A RID: 14410
		Boolean = 8,
		// Token: 0x0400384B RID: 14411
		Object = 16,
		// Token: 0x0400384C RID: 14412
		Array = 32,
		// Token: 0x0400384D RID: 14413
		Null = 64,
		// Token: 0x0400384E RID: 14414
		Any = 127
	}
}
