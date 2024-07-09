using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x02000A89 RID: 2697
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class JsonSerializationException : JsonException
	{
		// Token: 0x17001637 RID: 5687
		// (get) Token: 0x06006959 RID: 26969 RVA: 0x001FE20C File Offset: 0x001FE20C
		public int LineNumber { get; }

		// Token: 0x17001638 RID: 5688
		// (get) Token: 0x0600695A RID: 26970 RVA: 0x001FE214 File Offset: 0x001FE214
		public int LinePosition { get; }

		// Token: 0x17001639 RID: 5689
		// (get) Token: 0x0600695B RID: 26971 RVA: 0x001FE21C File Offset: 0x001FE21C
		[Nullable(2)]
		public string Path { [NullableContext(2)] get; }

		// Token: 0x0600695C RID: 26972 RVA: 0x001FE224 File Offset: 0x001FE224
		public JsonSerializationException()
		{
		}

		// Token: 0x0600695D RID: 26973 RVA: 0x001FE22C File Offset: 0x001FE22C
		public JsonSerializationException(string message) : base(message)
		{
		}

		// Token: 0x0600695E RID: 26974 RVA: 0x001FE238 File Offset: 0x001FE238
		public JsonSerializationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600695F RID: 26975 RVA: 0x001FE244 File Offset: 0x001FE244
		public JsonSerializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06006960 RID: 26976 RVA: 0x001FE250 File Offset: 0x001FE250
		public JsonSerializationException(string message, string path, int lineNumber, int linePosition, [Nullable(2)] Exception innerException) : base(message, innerException)
		{
			this.Path = path;
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}

		// Token: 0x06006961 RID: 26977 RVA: 0x001FE274 File Offset: 0x001FE274
		internal static JsonSerializationException Create(JsonReader reader, string message)
		{
			return JsonSerializationException.Create(reader, message, null);
		}

		// Token: 0x06006962 RID: 26978 RVA: 0x001FE280 File Offset: 0x001FE280
		internal static JsonSerializationException Create(JsonReader reader, string message, [Nullable(2)] Exception ex)
		{
			return JsonSerializationException.Create(reader as IJsonLineInfo, reader.Path, message, ex);
		}

		// Token: 0x06006963 RID: 26979 RVA: 0x001FE298 File Offset: 0x001FE298
		internal static JsonSerializationException Create([Nullable(2)] IJsonLineInfo lineInfo, string path, string message, [Nullable(2)] Exception ex)
		{
			message = JsonPosition.FormatMessage(lineInfo, path, message);
			int lineNumber;
			int linePosition;
			if (lineInfo != null && lineInfo.HasLineInfo())
			{
				lineNumber = lineInfo.LineNumber;
				linePosition = lineInfo.LinePosition;
			}
			else
			{
				lineNumber = 0;
				linePosition = 0;
			}
			return new JsonSerializationException(message, path, lineNumber, linePosition, ex);
		}
	}
}
