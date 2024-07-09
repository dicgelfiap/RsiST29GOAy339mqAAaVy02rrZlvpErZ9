using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000B12 RID: 2834
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class ValidationEventArgs : EventArgs
	{
		// Token: 0x0600718D RID: 29069 RVA: 0x00225354 File Offset: 0x00225354
		internal ValidationEventArgs(JsonSchemaException ex)
		{
			ValidationUtils.ArgumentNotNull(ex, "ex");
			this._ex = ex;
		}

		// Token: 0x170017AF RID: 6063
		// (get) Token: 0x0600718E RID: 29070 RVA: 0x00225370 File Offset: 0x00225370
		public JsonSchemaException Exception
		{
			get
			{
				return this._ex;
			}
		}

		// Token: 0x170017B0 RID: 6064
		// (get) Token: 0x0600718F RID: 29071 RVA: 0x00225378 File Offset: 0x00225378
		public string Path
		{
			get
			{
				return this._ex.Path;
			}
		}

		// Token: 0x170017B1 RID: 6065
		// (get) Token: 0x06007190 RID: 29072 RVA: 0x00225388 File Offset: 0x00225388
		public string Message
		{
			get
			{
				return this._ex.Message;
			}
		}

		// Token: 0x04003855 RID: 14421
		private readonly JsonSchemaException _ex;
	}
}
