using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001F2 RID: 498
	public class CrlReason : DerEnumerated
	{
		// Token: 0x0600100B RID: 4107 RVA: 0x0005E4D8 File Offset: 0x0005E4D8
		public CrlReason(int reason) : base(reason)
		{
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0005E4E4 File Offset: 0x0005E4E4
		public CrlReason(DerEnumerated reason) : base(reason.IntValueExact)
		{
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0005E4F4 File Offset: 0x0005E4F4
		public override string ToString()
		{
			int intValueExact = base.IntValueExact;
			string str = (intValueExact < 0 || intValueExact > 10) ? "Invalid" : CrlReason.ReasonString[intValueExact];
			return "CrlReason: " + str;
		}

		// Token: 0x04000BBE RID: 3006
		public const int Unspecified = 0;

		// Token: 0x04000BBF RID: 3007
		public const int KeyCompromise = 1;

		// Token: 0x04000BC0 RID: 3008
		public const int CACompromise = 2;

		// Token: 0x04000BC1 RID: 3009
		public const int AffiliationChanged = 3;

		// Token: 0x04000BC2 RID: 3010
		public const int Superseded = 4;

		// Token: 0x04000BC3 RID: 3011
		public const int CessationOfOperation = 5;

		// Token: 0x04000BC4 RID: 3012
		public const int CertificateHold = 6;

		// Token: 0x04000BC5 RID: 3013
		public const int RemoveFromCrl = 8;

		// Token: 0x04000BC6 RID: 3014
		public const int PrivilegeWithdrawn = 9;

		// Token: 0x04000BC7 RID: 3015
		public const int AACompromise = 10;

		// Token: 0x04000BC8 RID: 3016
		private static readonly string[] ReasonString = new string[]
		{
			"Unspecified",
			"KeyCompromise",
			"CACompromise",
			"AffiliationChanged",
			"Superseded",
			"CessationOfOperation",
			"CertificateHold",
			"Unknown",
			"RemoveFromCrl",
			"PrivilegeWithdrawn",
			"AACompromise"
		};
	}
}
