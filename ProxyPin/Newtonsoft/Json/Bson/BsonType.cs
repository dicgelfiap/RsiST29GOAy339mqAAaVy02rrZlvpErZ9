using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000B6F RID: 2927
	internal enum BsonType : sbyte
	{
		// Token: 0x0400392C RID: 14636
		Number = 1,
		// Token: 0x0400392D RID: 14637
		String,
		// Token: 0x0400392E RID: 14638
		Object,
		// Token: 0x0400392F RID: 14639
		Array,
		// Token: 0x04003930 RID: 14640
		Binary,
		// Token: 0x04003931 RID: 14641
		Undefined,
		// Token: 0x04003932 RID: 14642
		Oid,
		// Token: 0x04003933 RID: 14643
		Boolean,
		// Token: 0x04003934 RID: 14644
		Date,
		// Token: 0x04003935 RID: 14645
		Null,
		// Token: 0x04003936 RID: 14646
		Regex,
		// Token: 0x04003937 RID: 14647
		Reference,
		// Token: 0x04003938 RID: 14648
		Code,
		// Token: 0x04003939 RID: 14649
		Symbol,
		// Token: 0x0400393A RID: 14650
		CodeWScope,
		// Token: 0x0400393B RID: 14651
		Integer,
		// Token: 0x0400393C RID: 14652
		TimeStamp,
		// Token: 0x0400393D RID: 14653
		Long,
		// Token: 0x0400393E RID: 14654
		MinKey = -1,
		// Token: 0x0400393F RID: 14655
		MaxKey = 127
	}
}
