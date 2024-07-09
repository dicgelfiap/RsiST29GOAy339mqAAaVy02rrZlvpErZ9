using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000B69 RID: 2921
	internal class BsonValue : BsonToken
	{
		// Token: 0x060075B2 RID: 30130 RVA: 0x00235D50 File Offset: 0x00235D50
		public BsonValue(object value, BsonType type)
		{
			this._value = value;
			this._type = type;
		}

		// Token: 0x17001871 RID: 6257
		// (get) Token: 0x060075B3 RID: 30131 RVA: 0x00235D68 File Offset: 0x00235D68
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17001872 RID: 6258
		// (get) Token: 0x060075B4 RID: 30132 RVA: 0x00235D70 File Offset: 0x00235D70
		public override BsonType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04003920 RID: 14624
		private readonly object _value;

		// Token: 0x04003921 RID: 14625
		private readonly BsonType _type;
	}
}
