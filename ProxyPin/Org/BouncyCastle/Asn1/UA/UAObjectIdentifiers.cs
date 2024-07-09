using System;

namespace Org.BouncyCastle.Asn1.UA
{
	// Token: 0x020001CF RID: 463
	public abstract class UAObjectIdentifiers
	{
		// Token: 0x04000B3F RID: 2879
		public static readonly DerObjectIdentifier UaOid = new DerObjectIdentifier("1.2.804.2.1.1.1");

		// Token: 0x04000B40 RID: 2880
		public static readonly DerObjectIdentifier dstu4145le = UAObjectIdentifiers.UaOid.Branch("1.3.1.1");

		// Token: 0x04000B41 RID: 2881
		public static readonly DerObjectIdentifier dstu4145be = UAObjectIdentifiers.UaOid.Branch("1.3.1.1.1.1");

		// Token: 0x04000B42 RID: 2882
		public static readonly DerObjectIdentifier dstu7564digest_256 = UAObjectIdentifiers.UaOid.Branch("1.2.2.1");

		// Token: 0x04000B43 RID: 2883
		public static readonly DerObjectIdentifier dstu7564digest_384 = UAObjectIdentifiers.UaOid.Branch("1.2.2.2");

		// Token: 0x04000B44 RID: 2884
		public static readonly DerObjectIdentifier dstu7564digest_512 = UAObjectIdentifiers.UaOid.Branch("1.2.2.3");

		// Token: 0x04000B45 RID: 2885
		public static readonly DerObjectIdentifier dstu7564mac_256 = UAObjectIdentifiers.UaOid.Branch("1.2.2.4");

		// Token: 0x04000B46 RID: 2886
		public static readonly DerObjectIdentifier dstu7564mac_384 = UAObjectIdentifiers.UaOid.Branch("1.2.2.5");

		// Token: 0x04000B47 RID: 2887
		public static readonly DerObjectIdentifier dstu7564mac_512 = UAObjectIdentifiers.UaOid.Branch("1.2.2.6");

		// Token: 0x04000B48 RID: 2888
		public static readonly DerObjectIdentifier dstu7624ecb_128 = UAObjectIdentifiers.UaOid.Branch("1.1.3.1.1");

		// Token: 0x04000B49 RID: 2889
		public static readonly DerObjectIdentifier dstu7624ecb_256 = UAObjectIdentifiers.UaOid.Branch("1.1.3.1.2");

		// Token: 0x04000B4A RID: 2890
		public static readonly DerObjectIdentifier dstu7624ecb_512 = UAObjectIdentifiers.UaOid.Branch("1.1.3.1.3");

		// Token: 0x04000B4B RID: 2891
		public static readonly DerObjectIdentifier dstu7624ctr_128 = UAObjectIdentifiers.UaOid.Branch("1.1.3.2.1");

		// Token: 0x04000B4C RID: 2892
		public static readonly DerObjectIdentifier dstu7624ctr_256 = UAObjectIdentifiers.UaOid.Branch("1.1.3.2.2");

		// Token: 0x04000B4D RID: 2893
		public static readonly DerObjectIdentifier dstu7624ctr_512 = UAObjectIdentifiers.UaOid.Branch("1.1.3.2.3");

		// Token: 0x04000B4E RID: 2894
		public static readonly DerObjectIdentifier dstu7624cfb_128 = UAObjectIdentifiers.UaOid.Branch("1.1.3.3.1");

		// Token: 0x04000B4F RID: 2895
		public static readonly DerObjectIdentifier dstu7624cfb_256 = UAObjectIdentifiers.UaOid.Branch("1.1.3.3.2");

		// Token: 0x04000B50 RID: 2896
		public static readonly DerObjectIdentifier dstu7624cfb_512 = UAObjectIdentifiers.UaOid.Branch("1.1.3.3.3");

		// Token: 0x04000B51 RID: 2897
		public static readonly DerObjectIdentifier dstu7624cmac_128 = UAObjectIdentifiers.UaOid.Branch("1.1.3.4.1");

		// Token: 0x04000B52 RID: 2898
		public static readonly DerObjectIdentifier dstu7624cmac_256 = UAObjectIdentifiers.UaOid.Branch("1.1.3.4.2");

		// Token: 0x04000B53 RID: 2899
		public static readonly DerObjectIdentifier dstu7624cmac_512 = UAObjectIdentifiers.UaOid.Branch("1.1.3.4.3");

		// Token: 0x04000B54 RID: 2900
		public static readonly DerObjectIdentifier dstu7624cbc_128 = UAObjectIdentifiers.UaOid.Branch("1.1.3.5.1");

		// Token: 0x04000B55 RID: 2901
		public static readonly DerObjectIdentifier dstu7624cbc_256 = UAObjectIdentifiers.UaOid.Branch("1.1.3.5.2");

		// Token: 0x04000B56 RID: 2902
		public static readonly DerObjectIdentifier dstu7624cbc_512 = UAObjectIdentifiers.UaOid.Branch("1.1.3.5.3");

		// Token: 0x04000B57 RID: 2903
		public static readonly DerObjectIdentifier dstu7624ofb_128 = UAObjectIdentifiers.UaOid.Branch("1.1.3.6.1");

		// Token: 0x04000B58 RID: 2904
		public static readonly DerObjectIdentifier dstu7624ofb_256 = UAObjectIdentifiers.UaOid.Branch("1.1.3.6.2");

		// Token: 0x04000B59 RID: 2905
		public static readonly DerObjectIdentifier dstu7624ofb_512 = UAObjectIdentifiers.UaOid.Branch("1.1.3.6.3");

		// Token: 0x04000B5A RID: 2906
		public static readonly DerObjectIdentifier dstu7624gmac_128 = UAObjectIdentifiers.UaOid.Branch("1.1.3.7.1");

		// Token: 0x04000B5B RID: 2907
		public static readonly DerObjectIdentifier dstu7624gmac_256 = UAObjectIdentifiers.UaOid.Branch("1.1.3.7.2");

		// Token: 0x04000B5C RID: 2908
		public static readonly DerObjectIdentifier dstu7624gmac_512 = UAObjectIdentifiers.UaOid.Branch("1.1.3.7.3");

		// Token: 0x04000B5D RID: 2909
		public static readonly DerObjectIdentifier dstu7624ccm_128 = UAObjectIdentifiers.UaOid.Branch("1.1.3.8.1");

		// Token: 0x04000B5E RID: 2910
		public static readonly DerObjectIdentifier dstu7624ccm_256 = UAObjectIdentifiers.UaOid.Branch("1.1.3.8.2");

		// Token: 0x04000B5F RID: 2911
		public static readonly DerObjectIdentifier dstu7624ccm_512 = UAObjectIdentifiers.UaOid.Branch("1.1.3.8.3");

		// Token: 0x04000B60 RID: 2912
		public static readonly DerObjectIdentifier dstu7624xts_128 = UAObjectIdentifiers.UaOid.Branch("1.1.3.9.1");

		// Token: 0x04000B61 RID: 2913
		public static readonly DerObjectIdentifier dstu7624xts_256 = UAObjectIdentifiers.UaOid.Branch("1.1.3.9.2");

		// Token: 0x04000B62 RID: 2914
		public static readonly DerObjectIdentifier dstu7624xts_512 = UAObjectIdentifiers.UaOid.Branch("1.1.3.9.3");

		// Token: 0x04000B63 RID: 2915
		public static readonly DerObjectIdentifier dstu7624kw_128 = UAObjectIdentifiers.UaOid.Branch("1.1.3.10.1");

		// Token: 0x04000B64 RID: 2916
		public static readonly DerObjectIdentifier dstu7624kw_256 = UAObjectIdentifiers.UaOid.Branch("1.1.3.10.2");

		// Token: 0x04000B65 RID: 2917
		public static readonly DerObjectIdentifier dstu7624kw_512 = UAObjectIdentifiers.UaOid.Branch("1.1.3.10.3");
	}
}
