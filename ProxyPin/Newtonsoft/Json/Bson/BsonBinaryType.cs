using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000B61 RID: 2913
	internal enum BsonBinaryType : byte
	{
		// Token: 0x040038FE RID: 14590
		Binary,
		// Token: 0x040038FF RID: 14591
		Function,
		// Token: 0x04003900 RID: 14592
		[Obsolete("This type has been deprecated in the BSON specification. Use Binary instead.")]
		BinaryOld,
		// Token: 0x04003901 RID: 14593
		[Obsolete("This type has been deprecated in the BSON specification. Use Uuid instead.")]
		UuidOld,
		// Token: 0x04003902 RID: 14594
		Uuid,
		// Token: 0x04003903 RID: 14595
		Md5,
		// Token: 0x04003904 RID: 14596
		UserDefined = 128
	}
}
