using System;

namespace Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x0200013F RID: 319
	public class SubsequentMessage : DerInteger
	{
		// Token: 0x06000B3E RID: 2878 RVA: 0x0004ABD8 File Offset: 0x0004ABD8
		private SubsequentMessage(int value) : base(value)
		{
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0004ABE4 File Offset: 0x0004ABE4
		public static SubsequentMessage ValueOf(int value)
		{
			if (value == 0)
			{
				return SubsequentMessage.encrCert;
			}
			if (value == 1)
			{
				return SubsequentMessage.challengeResp;
			}
			throw new ArgumentException("unknown value: " + value, "value");
		}

		// Token: 0x0400079E RID: 1950
		public static readonly SubsequentMessage encrCert = new SubsequentMessage(0);

		// Token: 0x0400079F RID: 1951
		public static readonly SubsequentMessage challengeResp = new SubsequentMessage(1);
	}
}
