using System;

namespace Newtonsoft.Json
{
	// Token: 0x02000A8F RID: 2703
	public enum JsonToken
	{
		// Token: 0x040035ED RID: 13805
		None,
		// Token: 0x040035EE RID: 13806
		StartObject,
		// Token: 0x040035EF RID: 13807
		StartArray,
		// Token: 0x040035F0 RID: 13808
		StartConstructor,
		// Token: 0x040035F1 RID: 13809
		PropertyName,
		// Token: 0x040035F2 RID: 13810
		Comment,
		// Token: 0x040035F3 RID: 13811
		Raw,
		// Token: 0x040035F4 RID: 13812
		Integer,
		// Token: 0x040035F5 RID: 13813
		Float,
		// Token: 0x040035F6 RID: 13814
		String,
		// Token: 0x040035F7 RID: 13815
		Boolean,
		// Token: 0x040035F8 RID: 13816
		Null,
		// Token: 0x040035F9 RID: 13817
		Undefined,
		// Token: 0x040035FA RID: 13818
		EndObject,
		// Token: 0x040035FB RID: 13819
		EndArray,
		// Token: 0x040035FC RID: 13820
		EndConstructor,
		// Token: 0x040035FD RID: 13821
		Date,
		// Token: 0x040035FE RID: 13822
		Bytes
	}
}
