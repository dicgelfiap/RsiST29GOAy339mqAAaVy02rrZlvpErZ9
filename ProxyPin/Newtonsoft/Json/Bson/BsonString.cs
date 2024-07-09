using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000B6B RID: 2923
	internal class BsonString : BsonValue
	{
		// Token: 0x17001873 RID: 6259
		// (get) Token: 0x060075B7 RID: 30135 RVA: 0x00235DA0 File Offset: 0x00235DA0
		// (set) Token: 0x060075B8 RID: 30136 RVA: 0x00235DA8 File Offset: 0x00235DA8
		public int ByteCount { get; set; }

		// Token: 0x17001874 RID: 6260
		// (get) Token: 0x060075B9 RID: 30137 RVA: 0x00235DB4 File Offset: 0x00235DB4
		public bool IncludeLength { get; }

		// Token: 0x060075BA RID: 30138 RVA: 0x00235DBC File Offset: 0x00235DBC
		public BsonString(object value, bool includeLength) : base(value, BsonType.String)
		{
			this.IncludeLength = includeLength;
		}
	}
}
