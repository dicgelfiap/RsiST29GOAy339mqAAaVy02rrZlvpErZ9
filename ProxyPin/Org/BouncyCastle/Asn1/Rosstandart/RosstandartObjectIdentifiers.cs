using System;

namespace Org.BouncyCastle.Asn1.Rosstandart
{
	// Token: 0x020001BD RID: 445
	public abstract class RosstandartObjectIdentifiers
	{
		// Token: 0x04000AB2 RID: 2738
		public static readonly DerObjectIdentifier rosstandart = new DerObjectIdentifier("1.2.643.7");

		// Token: 0x04000AB3 RID: 2739
		public static readonly DerObjectIdentifier id_tc26 = RosstandartObjectIdentifiers.rosstandart.Branch("1");

		// Token: 0x04000AB4 RID: 2740
		public static readonly DerObjectIdentifier id_tc26_gost_3411_12_256 = RosstandartObjectIdentifiers.id_tc26.Branch("1.2.2");

		// Token: 0x04000AB5 RID: 2741
		public static readonly DerObjectIdentifier id_tc26_gost_3411_12_512 = RosstandartObjectIdentifiers.id_tc26.Branch("1.2.3");

		// Token: 0x04000AB6 RID: 2742
		public static readonly DerObjectIdentifier id_tc26_hmac_gost_3411_12_256 = RosstandartObjectIdentifiers.id_tc26.Branch("1.4.1");

		// Token: 0x04000AB7 RID: 2743
		public static readonly DerObjectIdentifier id_tc26_hmac_gost_3411_12_512 = RosstandartObjectIdentifiers.id_tc26.Branch("1.4.2");

		// Token: 0x04000AB8 RID: 2744
		public static readonly DerObjectIdentifier id_tc26_gost_3410_12_256 = RosstandartObjectIdentifiers.id_tc26.Branch("1.1.1");

		// Token: 0x04000AB9 RID: 2745
		public static readonly DerObjectIdentifier id_tc26_gost_3410_12_512 = RosstandartObjectIdentifiers.id_tc26.Branch("1.1.2");

		// Token: 0x04000ABA RID: 2746
		public static readonly DerObjectIdentifier id_tc26_signwithdigest_gost_3410_12_256 = RosstandartObjectIdentifiers.id_tc26.Branch("1.3.2");

		// Token: 0x04000ABB RID: 2747
		public static readonly DerObjectIdentifier id_tc26_signwithdigest_gost_3410_12_512 = RosstandartObjectIdentifiers.id_tc26.Branch("1.3.3");

		// Token: 0x04000ABC RID: 2748
		public static readonly DerObjectIdentifier id_tc26_agreement = RosstandartObjectIdentifiers.id_tc26.Branch("1.6");

		// Token: 0x04000ABD RID: 2749
		public static readonly DerObjectIdentifier id_tc26_agreement_gost_3410_12_256 = RosstandartObjectIdentifiers.id_tc26_agreement.Branch("1");

		// Token: 0x04000ABE RID: 2750
		public static readonly DerObjectIdentifier id_tc26_agreement_gost_3410_12_512 = RosstandartObjectIdentifiers.id_tc26_agreement.Branch("2");

		// Token: 0x04000ABF RID: 2751
		public static readonly DerObjectIdentifier id_tc26_gost_3410_12_256_paramSet = RosstandartObjectIdentifiers.id_tc26.Branch("2.1.1");

		// Token: 0x04000AC0 RID: 2752
		public static readonly DerObjectIdentifier id_tc26_gost_3410_12_256_paramSetA = RosstandartObjectIdentifiers.id_tc26_gost_3410_12_256_paramSet.Branch("1");

		// Token: 0x04000AC1 RID: 2753
		public static readonly DerObjectIdentifier id_tc26_gost_3410_12_512_paramSet = RosstandartObjectIdentifiers.id_tc26.Branch("2.1.2");

		// Token: 0x04000AC2 RID: 2754
		public static readonly DerObjectIdentifier id_tc26_gost_3410_12_512_paramSetA = RosstandartObjectIdentifiers.id_tc26_gost_3410_12_512_paramSet.Branch("1");

		// Token: 0x04000AC3 RID: 2755
		public static readonly DerObjectIdentifier id_tc26_gost_3410_12_512_paramSetB = RosstandartObjectIdentifiers.id_tc26_gost_3410_12_512_paramSet.Branch("2");

		// Token: 0x04000AC4 RID: 2756
		public static readonly DerObjectIdentifier id_tc26_gost_3410_12_512_paramSetC = RosstandartObjectIdentifiers.id_tc26_gost_3410_12_512_paramSet.Branch("3");

		// Token: 0x04000AC5 RID: 2757
		public static readonly DerObjectIdentifier id_tc26_gost_28147_param_Z = RosstandartObjectIdentifiers.id_tc26.Branch("2.5.1.1");
	}
}
