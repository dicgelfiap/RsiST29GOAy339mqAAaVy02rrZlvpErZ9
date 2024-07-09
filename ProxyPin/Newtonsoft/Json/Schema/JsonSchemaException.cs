using System;
using System.Runtime.Serialization;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000B08 RID: 2824
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	[Serializable]
	public class JsonSchemaException : JsonException
	{
		// Token: 0x1700178A RID: 6026
		// (get) Token: 0x0600711B RID: 28955 RVA: 0x002234A4 File Offset: 0x002234A4
		public int LineNumber { get; }

		// Token: 0x1700178B RID: 6027
		// (get) Token: 0x0600711C RID: 28956 RVA: 0x002234AC File Offset: 0x002234AC
		public int LinePosition { get; }

		// Token: 0x1700178C RID: 6028
		// (get) Token: 0x0600711D RID: 28957 RVA: 0x002234B4 File Offset: 0x002234B4
		public string Path { get; }

		// Token: 0x0600711E RID: 28958 RVA: 0x002234BC File Offset: 0x002234BC
		public JsonSchemaException()
		{
		}

		// Token: 0x0600711F RID: 28959 RVA: 0x002234C4 File Offset: 0x002234C4
		public JsonSchemaException(string message) : base(message)
		{
		}

		// Token: 0x06007120 RID: 28960 RVA: 0x002234D0 File Offset: 0x002234D0
		public JsonSchemaException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007121 RID: 28961 RVA: 0x002234DC File Offset: 0x002234DC
		public JsonSchemaException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007122 RID: 28962 RVA: 0x002234E8 File Offset: 0x002234E8
		internal JsonSchemaException(string message, Exception innerException, string path, int lineNumber, int linePosition) : base(message, innerException)
		{
			this.Path = path;
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}
	}
}
