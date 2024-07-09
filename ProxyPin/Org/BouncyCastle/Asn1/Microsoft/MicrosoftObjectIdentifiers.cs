using System;

namespace Org.BouncyCastle.Asn1.Microsoft
{
	// Token: 0x02000181 RID: 385
	public abstract class MicrosoftObjectIdentifiers
	{
		// Token: 0x040008FA RID: 2298
		public static readonly DerObjectIdentifier Microsoft = new DerObjectIdentifier("1.3.6.1.4.1.311");

		// Token: 0x040008FB RID: 2299
		public static readonly DerObjectIdentifier MicrosoftCertTemplateV1 = MicrosoftObjectIdentifiers.Microsoft.Branch("20.2");

		// Token: 0x040008FC RID: 2300
		public static readonly DerObjectIdentifier MicrosoftCAVersion = MicrosoftObjectIdentifiers.Microsoft.Branch("21.1");

		// Token: 0x040008FD RID: 2301
		public static readonly DerObjectIdentifier MicrosoftPrevCACertHash = MicrosoftObjectIdentifiers.Microsoft.Branch("21.2");

		// Token: 0x040008FE RID: 2302
		public static readonly DerObjectIdentifier MicrosoftCrlNextPublish = MicrosoftObjectIdentifiers.Microsoft.Branch("21.4");

		// Token: 0x040008FF RID: 2303
		public static readonly DerObjectIdentifier MicrosoftCertTemplateV2 = MicrosoftObjectIdentifiers.Microsoft.Branch("21.7");

		// Token: 0x04000900 RID: 2304
		public static readonly DerObjectIdentifier MicrosoftAppPolicies = MicrosoftObjectIdentifiers.Microsoft.Branch("21.10");
	}
}
