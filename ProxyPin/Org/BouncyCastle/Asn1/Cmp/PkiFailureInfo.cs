using System;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000E6 RID: 230
	public class PkiFailureInfo : DerBitString
	{
		// Token: 0x06000885 RID: 2181 RVA: 0x000429C4 File Offset: 0x000429C4
		public PkiFailureInfo(int info) : base(info)
		{
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x000429D0 File Offset: 0x000429D0
		public PkiFailureInfo(DerBitString info) : base(info.GetBytes(), info.PadBits)
		{
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x000429E4 File Offset: 0x000429E4
		public override string ToString()
		{
			return "PkiFailureInfo: 0x" + this.IntValue.ToString("X");
		}

		// Token: 0x0400063C RID: 1596
		public const int BadAlg = 128;

		// Token: 0x0400063D RID: 1597
		public const int BadMessageCheck = 64;

		// Token: 0x0400063E RID: 1598
		public const int BadRequest = 32;

		// Token: 0x0400063F RID: 1599
		public const int BadTime = 16;

		// Token: 0x04000640 RID: 1600
		public const int BadCertId = 8;

		// Token: 0x04000641 RID: 1601
		public const int BadDataFormat = 4;

		// Token: 0x04000642 RID: 1602
		public const int WrongAuthority = 2;

		// Token: 0x04000643 RID: 1603
		public const int IncorrectData = 1;

		// Token: 0x04000644 RID: 1604
		public const int MissingTimeStamp = 32768;

		// Token: 0x04000645 RID: 1605
		public const int BadPop = 16384;

		// Token: 0x04000646 RID: 1606
		public const int CertRevoked = 8192;

		// Token: 0x04000647 RID: 1607
		public const int CertConfirmed = 4096;

		// Token: 0x04000648 RID: 1608
		public const int WrongIntegrity = 2048;

		// Token: 0x04000649 RID: 1609
		public const int BadRecipientNonce = 1024;

		// Token: 0x0400064A RID: 1610
		public const int TimeNotAvailable = 512;

		// Token: 0x0400064B RID: 1611
		public const int UnacceptedPolicy = 256;

		// Token: 0x0400064C RID: 1612
		public const int UnacceptedExtension = 8388608;

		// Token: 0x0400064D RID: 1613
		public const int AddInfoNotAvailable = 4194304;

		// Token: 0x0400064E RID: 1614
		public const int BadSenderNonce = 2097152;

		// Token: 0x0400064F RID: 1615
		public const int BadCertTemplate = 1048576;

		// Token: 0x04000650 RID: 1616
		public const int SignerNotTrusted = 524288;

		// Token: 0x04000651 RID: 1617
		public const int TransactionIdInUse = 262144;

		// Token: 0x04000652 RID: 1618
		public const int UnsupportedVersion = 131072;

		// Token: 0x04000653 RID: 1619
		public const int NotAuthorized = 65536;

		// Token: 0x04000654 RID: 1620
		public const int SystemUnavail = -2147483648;

		// Token: 0x04000655 RID: 1621
		public const int SystemFailure = 1073741824;

		// Token: 0x04000656 RID: 1622
		public const int DuplicateCertReq = 536870912;
	}
}
