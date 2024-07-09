using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x02000A92 RID: 2706
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class JsonWriterException : JsonException
	{
		// Token: 0x1700169B RID: 5787
		// (get) Token: 0x06006C39 RID: 27705 RVA: 0x0020AC44 File Offset: 0x0020AC44
		[Nullable(2)]
		public string Path { [NullableContext(2)] get; }

		// Token: 0x06006C3A RID: 27706 RVA: 0x0020AC4C File Offset: 0x0020AC4C
		public JsonWriterException()
		{
		}

		// Token: 0x06006C3B RID: 27707 RVA: 0x0020AC54 File Offset: 0x0020AC54
		public JsonWriterException(string message) : base(message)
		{
		}

		// Token: 0x06006C3C RID: 27708 RVA: 0x0020AC60 File Offset: 0x0020AC60
		public JsonWriterException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06006C3D RID: 27709 RVA: 0x0020AC6C File Offset: 0x0020AC6C
		public JsonWriterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06006C3E RID: 27710 RVA: 0x0020AC78 File Offset: 0x0020AC78
		public JsonWriterException(string message, string path, [Nullable(2)] Exception innerException) : base(message, innerException)
		{
			this.Path = path;
		}

		// Token: 0x06006C3F RID: 27711 RVA: 0x0020AC8C File Offset: 0x0020AC8C
		internal static JsonWriterException Create(JsonWriter writer, string message, [Nullable(2)] Exception ex)
		{
			return JsonWriterException.Create(writer.ContainerPath, message, ex);
		}

		// Token: 0x06006C40 RID: 27712 RVA: 0x0020AC9C File Offset: 0x0020AC9C
		internal static JsonWriterException Create(string path, string message, [Nullable(2)] Exception ex)
		{
			message = JsonPosition.FormatMessage(null, path, message);
			return new JsonWriterException(message, path, ex);
		}
	}
}
