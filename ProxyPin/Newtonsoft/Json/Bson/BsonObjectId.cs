using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000B63 RID: 2915
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	public class BsonObjectId
	{
		// Token: 0x17001867 RID: 6247
		// (get) Token: 0x0600757B RID: 30075 RVA: 0x00234EAC File Offset: 0x00234EAC
		public byte[] Value { get; }

		// Token: 0x0600757C RID: 30076 RVA: 0x00234EB4 File Offset: 0x00234EB4
		public BsonObjectId(byte[] value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			if (value.Length != 12)
			{
				throw new ArgumentException("An ObjectId must be 12 bytes", "value");
			}
			this.Value = value;
		}
	}
}
