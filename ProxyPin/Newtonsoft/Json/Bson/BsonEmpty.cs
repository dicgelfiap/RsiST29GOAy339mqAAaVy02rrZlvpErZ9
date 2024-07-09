using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000B68 RID: 2920
	internal class BsonEmpty : BsonToken
	{
		// Token: 0x060075AF RID: 30127 RVA: 0x00235D1C File Offset: 0x00235D1C
		private BsonEmpty(BsonType type)
		{
			this.Type = type;
		}

		// Token: 0x17001870 RID: 6256
		// (get) Token: 0x060075B0 RID: 30128 RVA: 0x00235D2C File Offset: 0x00235D2C
		public override BsonType Type { get; }

		// Token: 0x0400391D RID: 14621
		public static readonly BsonToken Null = new BsonEmpty(BsonType.Null);

		// Token: 0x0400391E RID: 14622
		public static readonly BsonToken Undefined = new BsonEmpty(BsonType.Undefined);
	}
}
