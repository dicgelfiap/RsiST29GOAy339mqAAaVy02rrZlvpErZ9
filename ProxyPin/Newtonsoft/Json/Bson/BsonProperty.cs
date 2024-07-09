using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000B6E RID: 2926
	internal class BsonProperty
	{
		// Token: 0x17001879 RID: 6265
		// (get) Token: 0x060075C4 RID: 30148 RVA: 0x00235E48 File Offset: 0x00235E48
		// (set) Token: 0x060075C5 RID: 30149 RVA: 0x00235E50 File Offset: 0x00235E50
		public BsonString Name { get; set; }

		// Token: 0x1700187A RID: 6266
		// (get) Token: 0x060075C6 RID: 30150 RVA: 0x00235E5C File Offset: 0x00235E5C
		// (set) Token: 0x060075C7 RID: 30151 RVA: 0x00235E64 File Offset: 0x00235E64
		public BsonToken Value { get; set; }
	}
}
