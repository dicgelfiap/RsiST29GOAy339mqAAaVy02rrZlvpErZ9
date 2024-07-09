using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x02000A87 RID: 2695
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class JsonReaderException : JsonException
	{
		// Token: 0x17001634 RID: 5684
		// (get) Token: 0x0600694D RID: 26957 RVA: 0x001FE128 File Offset: 0x001FE128
		public int LineNumber { get; }

		// Token: 0x17001635 RID: 5685
		// (get) Token: 0x0600694E RID: 26958 RVA: 0x001FE130 File Offset: 0x001FE130
		public int LinePosition { get; }

		// Token: 0x17001636 RID: 5686
		// (get) Token: 0x0600694F RID: 26959 RVA: 0x001FE138 File Offset: 0x001FE138
		[Nullable(2)]
		public string Path { [NullableContext(2)] get; }

		// Token: 0x06006950 RID: 26960 RVA: 0x001FE140 File Offset: 0x001FE140
		public JsonReaderException()
		{
		}

		// Token: 0x06006951 RID: 26961 RVA: 0x001FE148 File Offset: 0x001FE148
		public JsonReaderException(string message) : base(message)
		{
		}

		// Token: 0x06006952 RID: 26962 RVA: 0x001FE154 File Offset: 0x001FE154
		public JsonReaderException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06006953 RID: 26963 RVA: 0x001FE160 File Offset: 0x001FE160
		public JsonReaderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06006954 RID: 26964 RVA: 0x001FE16C File Offset: 0x001FE16C
		public JsonReaderException(string message, string path, int lineNumber, int linePosition, [Nullable(2)] Exception innerException) : base(message, innerException)
		{
			this.Path = path;
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}

		// Token: 0x06006955 RID: 26965 RVA: 0x001FE190 File Offset: 0x001FE190
		internal static JsonReaderException Create(JsonReader reader, string message)
		{
			return JsonReaderException.Create(reader, message, null);
		}

		// Token: 0x06006956 RID: 26966 RVA: 0x001FE19C File Offset: 0x001FE19C
		internal static JsonReaderException Create(JsonReader reader, string message, [Nullable(2)] Exception ex)
		{
			return JsonReaderException.Create(reader as IJsonLineInfo, reader.Path, message, ex);
		}

		// Token: 0x06006957 RID: 26967 RVA: 0x001FE1B4 File Offset: 0x001FE1B4
		internal static JsonReaderException Create([Nullable(2)] IJsonLineInfo lineInfo, string path, string message, [Nullable(2)] Exception ex)
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
			return new JsonReaderException(message, path, lineNumber, linePosition, ex);
		}
	}
}
