using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000B6C RID: 2924
	internal class BsonBinary : BsonValue
	{
		// Token: 0x17001875 RID: 6261
		// (get) Token: 0x060075BB RID: 30139 RVA: 0x00235DD0 File Offset: 0x00235DD0
		// (set) Token: 0x060075BC RID: 30140 RVA: 0x00235DD8 File Offset: 0x00235DD8
		public BsonBinaryType BinaryType { get; set; }

		// Token: 0x060075BD RID: 30141 RVA: 0x00235DE4 File Offset: 0x00235DE4
		public BsonBinary(byte[] value, BsonBinaryType binaryType) : base(value, BsonType.Binary)
		{
			this.BinaryType = binaryType;
		}
	}
}
