using System;

namespace Org.BouncyCastle.Asn1.Eac
{
	// Token: 0x02000147 RID: 327
	public abstract class EacObjectIdentifiers
	{
		// Token: 0x040007CF RID: 1999
		public static readonly DerObjectIdentifier bsi_de = new DerObjectIdentifier("0.4.0.127.0.7");

		// Token: 0x040007D0 RID: 2000
		public static readonly DerObjectIdentifier id_PK = new DerObjectIdentifier(EacObjectIdentifiers.bsi_de + ".2.2.1");

		// Token: 0x040007D1 RID: 2001
		public static readonly DerObjectIdentifier id_PK_DH = new DerObjectIdentifier(EacObjectIdentifiers.id_PK + ".1");

		// Token: 0x040007D2 RID: 2002
		public static readonly DerObjectIdentifier id_PK_ECDH = new DerObjectIdentifier(EacObjectIdentifiers.id_PK + ".2");

		// Token: 0x040007D3 RID: 2003
		public static readonly DerObjectIdentifier id_CA = new DerObjectIdentifier(EacObjectIdentifiers.bsi_de + ".2.2.3");

		// Token: 0x040007D4 RID: 2004
		public static readonly DerObjectIdentifier id_CA_DH = new DerObjectIdentifier(EacObjectIdentifiers.id_CA + ".1");

		// Token: 0x040007D5 RID: 2005
		public static readonly DerObjectIdentifier id_CA_DH_3DES_CBC_CBC = new DerObjectIdentifier(EacObjectIdentifiers.id_CA_DH + ".1");

		// Token: 0x040007D6 RID: 2006
		public static readonly DerObjectIdentifier id_CA_ECDH = new DerObjectIdentifier(EacObjectIdentifiers.id_CA + ".2");

		// Token: 0x040007D7 RID: 2007
		public static readonly DerObjectIdentifier id_CA_ECDH_3DES_CBC_CBC = new DerObjectIdentifier(EacObjectIdentifiers.id_CA_ECDH + ".1");

		// Token: 0x040007D8 RID: 2008
		public static readonly DerObjectIdentifier id_TA = new DerObjectIdentifier(EacObjectIdentifiers.bsi_de + ".2.2.2");

		// Token: 0x040007D9 RID: 2009
		public static readonly DerObjectIdentifier id_TA_RSA = new DerObjectIdentifier(EacObjectIdentifiers.id_TA + ".1");

		// Token: 0x040007DA RID: 2010
		public static readonly DerObjectIdentifier id_TA_RSA_v1_5_SHA_1 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_RSA + ".1");

		// Token: 0x040007DB RID: 2011
		public static readonly DerObjectIdentifier id_TA_RSA_v1_5_SHA_256 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_RSA + ".2");

		// Token: 0x040007DC RID: 2012
		public static readonly DerObjectIdentifier id_TA_RSA_PSS_SHA_1 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_RSA + ".3");

		// Token: 0x040007DD RID: 2013
		public static readonly DerObjectIdentifier id_TA_RSA_PSS_SHA_256 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_RSA + ".4");

		// Token: 0x040007DE RID: 2014
		public static readonly DerObjectIdentifier id_TA_ECDSA = new DerObjectIdentifier(EacObjectIdentifiers.id_TA + ".2");

		// Token: 0x040007DF RID: 2015
		public static readonly DerObjectIdentifier id_TA_ECDSA_SHA_1 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_ECDSA + ".1");

		// Token: 0x040007E0 RID: 2016
		public static readonly DerObjectIdentifier id_TA_ECDSA_SHA_224 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_ECDSA + ".2");

		// Token: 0x040007E1 RID: 2017
		public static readonly DerObjectIdentifier id_TA_ECDSA_SHA_256 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_ECDSA + ".3");

		// Token: 0x040007E2 RID: 2018
		public static readonly DerObjectIdentifier id_TA_ECDSA_SHA_384 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_ECDSA + ".4");

		// Token: 0x040007E3 RID: 2019
		public static readonly DerObjectIdentifier id_TA_ECDSA_SHA_512 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_ECDSA + ".5");
	}
}
