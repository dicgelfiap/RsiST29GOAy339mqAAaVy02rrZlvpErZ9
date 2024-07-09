using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000ABE RID: 2750
	internal static class JsonTokenUtils
	{
		// Token: 0x06006D7F RID: 28031 RVA: 0x00212B60 File Offset: 0x00212B60
		internal static bool IsEndToken(JsonToken token)
		{
			return token - JsonToken.EndObject <= 2;
		}

		// Token: 0x06006D80 RID: 28032 RVA: 0x00212B70 File Offset: 0x00212B70
		internal static bool IsStartToken(JsonToken token)
		{
			return token - JsonToken.StartObject <= 2;
		}

		// Token: 0x06006D81 RID: 28033 RVA: 0x00212B80 File Offset: 0x00212B80
		internal static bool IsPrimitiveToken(JsonToken token)
		{
			return token - JsonToken.Integer <= 5 || token - JsonToken.Date <= 1;
		}
	}
}
