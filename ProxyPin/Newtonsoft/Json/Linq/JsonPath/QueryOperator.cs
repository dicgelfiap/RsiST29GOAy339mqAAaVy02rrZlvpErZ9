using System;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x02000B33 RID: 2867
	internal enum QueryOperator
	{
		// Token: 0x040038B7 RID: 14519
		None,
		// Token: 0x040038B8 RID: 14520
		Equals,
		// Token: 0x040038B9 RID: 14521
		NotEquals,
		// Token: 0x040038BA RID: 14522
		Exists,
		// Token: 0x040038BB RID: 14523
		LessThan,
		// Token: 0x040038BC RID: 14524
		LessThanOrEquals,
		// Token: 0x040038BD RID: 14525
		GreaterThan,
		// Token: 0x040038BE RID: 14526
		GreaterThanOrEquals,
		// Token: 0x040038BF RID: 14527
		And,
		// Token: 0x040038C0 RID: 14528
		Or,
		// Token: 0x040038C1 RID: 14529
		RegexEquals,
		// Token: 0x040038C2 RID: 14530
		StrictEquals,
		// Token: 0x040038C3 RID: 14531
		StrictNotEquals
	}
}
