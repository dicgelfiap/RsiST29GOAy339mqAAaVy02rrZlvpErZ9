using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000B65 RID: 2917
	internal abstract class BsonToken
	{
		// Token: 0x1700186B RID: 6251
		// (get) Token: 0x0600759F RID: 30111
		public abstract BsonType Type { get; }

		// Token: 0x1700186C RID: 6252
		// (get) Token: 0x060075A0 RID: 30112 RVA: 0x00235C30 File Offset: 0x00235C30
		// (set) Token: 0x060075A1 RID: 30113 RVA: 0x00235C38 File Offset: 0x00235C38
		public BsonToken Parent { get; set; }

		// Token: 0x1700186D RID: 6253
		// (get) Token: 0x060075A2 RID: 30114 RVA: 0x00235C44 File Offset: 0x00235C44
		// (set) Token: 0x060075A3 RID: 30115 RVA: 0x00235C4C File Offset: 0x00235C4C
		public int CalculatedSize { get; set; }
	}
}
