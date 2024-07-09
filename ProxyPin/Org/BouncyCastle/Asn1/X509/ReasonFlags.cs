using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200020A RID: 522
	public class ReasonFlags : DerBitString
	{
		// Token: 0x060010C8 RID: 4296 RVA: 0x0006125C File Offset: 0x0006125C
		public ReasonFlags(int reasons) : base(reasons)
		{
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00061268 File Offset: 0x00061268
		public ReasonFlags(DerBitString reasons) : base(reasons.GetBytes(), reasons.PadBits)
		{
		}

		// Token: 0x04000C2C RID: 3116
		public const int Unused = 128;

		// Token: 0x04000C2D RID: 3117
		public const int KeyCompromise = 64;

		// Token: 0x04000C2E RID: 3118
		public const int CACompromise = 32;

		// Token: 0x04000C2F RID: 3119
		public const int AffiliationChanged = 16;

		// Token: 0x04000C30 RID: 3120
		public const int Superseded = 8;

		// Token: 0x04000C31 RID: 3121
		public const int CessationOfOperation = 4;

		// Token: 0x04000C32 RID: 3122
		public const int CertificateHold = 2;

		// Token: 0x04000C33 RID: 3123
		public const int PrivilegeWithdrawn = 1;

		// Token: 0x04000C34 RID: 3124
		public const int AACompromise = 32768;
	}
}
