using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000200 RID: 512
	public sealed class KeyPurposeID : DerObjectIdentifier
	{
		// Token: 0x0600108E RID: 4238 RVA: 0x00060644 File Offset: 0x00060644
		private KeyPurposeID(string id) : base(id)
		{
		}

		// Token: 0x04000C01 RID: 3073
		private const string IdKP = "1.3.6.1.5.5.7.3";

		// Token: 0x04000C02 RID: 3074
		public static readonly KeyPurposeID AnyExtendedKeyUsage = new KeyPurposeID(X509Extensions.ExtendedKeyUsage.Id + ".0");

		// Token: 0x04000C03 RID: 3075
		public static readonly KeyPurposeID IdKPServerAuth = new KeyPurposeID("1.3.6.1.5.5.7.3.1");

		// Token: 0x04000C04 RID: 3076
		public static readonly KeyPurposeID IdKPClientAuth = new KeyPurposeID("1.3.6.1.5.5.7.3.2");

		// Token: 0x04000C05 RID: 3077
		public static readonly KeyPurposeID IdKPCodeSigning = new KeyPurposeID("1.3.6.1.5.5.7.3.3");

		// Token: 0x04000C06 RID: 3078
		public static readonly KeyPurposeID IdKPEmailProtection = new KeyPurposeID("1.3.6.1.5.5.7.3.4");

		// Token: 0x04000C07 RID: 3079
		public static readonly KeyPurposeID IdKPIpsecEndSystem = new KeyPurposeID("1.3.6.1.5.5.7.3.5");

		// Token: 0x04000C08 RID: 3080
		public static readonly KeyPurposeID IdKPIpsecTunnel = new KeyPurposeID("1.3.6.1.5.5.7.3.6");

		// Token: 0x04000C09 RID: 3081
		public static readonly KeyPurposeID IdKPIpsecUser = new KeyPurposeID("1.3.6.1.5.5.7.3.7");

		// Token: 0x04000C0A RID: 3082
		public static readonly KeyPurposeID IdKPTimeStamping = new KeyPurposeID("1.3.6.1.5.5.7.3.8");

		// Token: 0x04000C0B RID: 3083
		public static readonly KeyPurposeID IdKPOcspSigning = new KeyPurposeID("1.3.6.1.5.5.7.3.9");

		// Token: 0x04000C0C RID: 3084
		public static readonly KeyPurposeID IdKPSmartCardLogon = new KeyPurposeID("1.3.6.1.4.1.311.20.2.2");

		// Token: 0x04000C0D RID: 3085
		public static readonly KeyPurposeID IdKPMacAddress = new KeyPurposeID("1.3.6.1.1.1.1.22");
	}
}
