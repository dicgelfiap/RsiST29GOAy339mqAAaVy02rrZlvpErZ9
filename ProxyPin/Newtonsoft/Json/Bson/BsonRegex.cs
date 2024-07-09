using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000B6D RID: 2925
	internal class BsonRegex : BsonToken
	{
		// Token: 0x17001876 RID: 6262
		// (get) Token: 0x060075BE RID: 30142 RVA: 0x00235DF8 File Offset: 0x00235DF8
		// (set) Token: 0x060075BF RID: 30143 RVA: 0x00235E00 File Offset: 0x00235E00
		public BsonString Pattern { get; set; }

		// Token: 0x17001877 RID: 6263
		// (get) Token: 0x060075C0 RID: 30144 RVA: 0x00235E0C File Offset: 0x00235E0C
		// (set) Token: 0x060075C1 RID: 30145 RVA: 0x00235E14 File Offset: 0x00235E14
		public BsonString Options { get; set; }

		// Token: 0x060075C2 RID: 30146 RVA: 0x00235E20 File Offset: 0x00235E20
		public BsonRegex(string pattern, string options)
		{
			this.Pattern = new BsonString(pattern, false);
			this.Options = new BsonString(options, false);
		}

		// Token: 0x17001878 RID: 6264
		// (get) Token: 0x060075C3 RID: 30147 RVA: 0x00235E44 File Offset: 0x00235E44
		public override BsonType Type
		{
			get
			{
				return BsonType.Regex;
			}
		}
	}
}
