using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x02000A7E RID: 2686
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class JsonException : Exception
	{
		// Token: 0x060068B9 RID: 26809 RVA: 0x001FC230 File Offset: 0x001FC230
		public JsonException()
		{
		}

		// Token: 0x060068BA RID: 26810 RVA: 0x001FC238 File Offset: 0x001FC238
		public JsonException(string message) : base(message)
		{
		}

		// Token: 0x060068BB RID: 26811 RVA: 0x001FC244 File Offset: 0x001FC244
		public JsonException(string message, [Nullable(2)] Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060068BC RID: 26812 RVA: 0x001FC250 File Offset: 0x001FC250
		public JsonException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060068BD RID: 26813 RVA: 0x001FC25C File Offset: 0x001FC25C
		internal static JsonException Create(IJsonLineInfo lineInfo, string path, string message)
		{
			message = JsonPosition.FormatMessage(lineInfo, path, message);
			return new JsonException(message);
		}
	}
}
