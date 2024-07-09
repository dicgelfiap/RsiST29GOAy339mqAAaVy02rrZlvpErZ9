using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000B6A RID: 2922
	internal class BsonBoolean : BsonValue
	{
		// Token: 0x060075B5 RID: 30133 RVA: 0x00235D78 File Offset: 0x00235D78
		private BsonBoolean(bool value) : base(value, BsonType.Boolean)
		{
		}

		// Token: 0x04003922 RID: 14626
		public static readonly BsonBoolean False = new BsonBoolean(false);

		// Token: 0x04003923 RID: 14627
		public static readonly BsonBoolean True = new BsonBoolean(true);
	}
}
