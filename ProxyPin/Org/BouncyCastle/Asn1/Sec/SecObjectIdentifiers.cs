using System;
using Org.BouncyCastle.Asn1.X9;

namespace Org.BouncyCastle.Asn1.Sec
{
	// Token: 0x020001C0 RID: 448
	public abstract class SecObjectIdentifiers
	{
		// Token: 0x04000ACA RID: 2762
		public static readonly DerObjectIdentifier EllipticCurve = new DerObjectIdentifier("1.3.132.0");

		// Token: 0x04000ACB RID: 2763
		public static readonly DerObjectIdentifier SecT163k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".1");

		// Token: 0x04000ACC RID: 2764
		public static readonly DerObjectIdentifier SecT163r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".2");

		// Token: 0x04000ACD RID: 2765
		public static readonly DerObjectIdentifier SecT239k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".3");

		// Token: 0x04000ACE RID: 2766
		public static readonly DerObjectIdentifier SecT113r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".4");

		// Token: 0x04000ACF RID: 2767
		public static readonly DerObjectIdentifier SecT113r2 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".5");

		// Token: 0x04000AD0 RID: 2768
		public static readonly DerObjectIdentifier SecP112r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".6");

		// Token: 0x04000AD1 RID: 2769
		public static readonly DerObjectIdentifier SecP112r2 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".7");

		// Token: 0x04000AD2 RID: 2770
		public static readonly DerObjectIdentifier SecP160r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".8");

		// Token: 0x04000AD3 RID: 2771
		public static readonly DerObjectIdentifier SecP160k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".9");

		// Token: 0x04000AD4 RID: 2772
		public static readonly DerObjectIdentifier SecP256k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".10");

		// Token: 0x04000AD5 RID: 2773
		public static readonly DerObjectIdentifier SecT163r2 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".15");

		// Token: 0x04000AD6 RID: 2774
		public static readonly DerObjectIdentifier SecT283k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".16");

		// Token: 0x04000AD7 RID: 2775
		public static readonly DerObjectIdentifier SecT283r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".17");

		// Token: 0x04000AD8 RID: 2776
		public static readonly DerObjectIdentifier SecT131r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".22");

		// Token: 0x04000AD9 RID: 2777
		public static readonly DerObjectIdentifier SecT131r2 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".23");

		// Token: 0x04000ADA RID: 2778
		public static readonly DerObjectIdentifier SecT193r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".24");

		// Token: 0x04000ADB RID: 2779
		public static readonly DerObjectIdentifier SecT193r2 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".25");

		// Token: 0x04000ADC RID: 2780
		public static readonly DerObjectIdentifier SecT233k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".26");

		// Token: 0x04000ADD RID: 2781
		public static readonly DerObjectIdentifier SecT233r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".27");

		// Token: 0x04000ADE RID: 2782
		public static readonly DerObjectIdentifier SecP128r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".28");

		// Token: 0x04000ADF RID: 2783
		public static readonly DerObjectIdentifier SecP128r2 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".29");

		// Token: 0x04000AE0 RID: 2784
		public static readonly DerObjectIdentifier SecP160r2 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".30");

		// Token: 0x04000AE1 RID: 2785
		public static readonly DerObjectIdentifier SecP192k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".31");

		// Token: 0x04000AE2 RID: 2786
		public static readonly DerObjectIdentifier SecP224k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".32");

		// Token: 0x04000AE3 RID: 2787
		public static readonly DerObjectIdentifier SecP224r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".33");

		// Token: 0x04000AE4 RID: 2788
		public static readonly DerObjectIdentifier SecP384r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".34");

		// Token: 0x04000AE5 RID: 2789
		public static readonly DerObjectIdentifier SecP521r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".35");

		// Token: 0x04000AE6 RID: 2790
		public static readonly DerObjectIdentifier SecT409k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".36");

		// Token: 0x04000AE7 RID: 2791
		public static readonly DerObjectIdentifier SecT409r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".37");

		// Token: 0x04000AE8 RID: 2792
		public static readonly DerObjectIdentifier SecT571k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".38");

		// Token: 0x04000AE9 RID: 2793
		public static readonly DerObjectIdentifier SecT571r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".39");

		// Token: 0x04000AEA RID: 2794
		public static readonly DerObjectIdentifier SecP192r1 = X9ObjectIdentifiers.Prime192v1;

		// Token: 0x04000AEB RID: 2795
		public static readonly DerObjectIdentifier SecP256r1 = X9ObjectIdentifiers.Prime256v1;
	}
}
