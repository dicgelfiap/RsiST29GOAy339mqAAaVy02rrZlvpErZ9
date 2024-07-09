using System;

namespace Org.BouncyCastle.Asn1.Icao
{
	// Token: 0x02000171 RID: 369
	public abstract class IcaoObjectIdentifiers
	{
		// Token: 0x040008A3 RID: 2211
		public static readonly DerObjectIdentifier IdIcao = new DerObjectIdentifier("2.23.136");

		// Token: 0x040008A4 RID: 2212
		public static readonly DerObjectIdentifier IdIcaoMrtd = IcaoObjectIdentifiers.IdIcao.Branch("1");

		// Token: 0x040008A5 RID: 2213
		public static readonly DerObjectIdentifier IdIcaoMrtdSecurity = IcaoObjectIdentifiers.IdIcaoMrtd.Branch("1");

		// Token: 0x040008A6 RID: 2214
		public static readonly DerObjectIdentifier IdIcaoLdsSecurityObject = IcaoObjectIdentifiers.IdIcaoMrtdSecurity.Branch("1");

		// Token: 0x040008A7 RID: 2215
		public static readonly DerObjectIdentifier IdIcaoCscaMasterList = IcaoObjectIdentifiers.IdIcaoMrtdSecurity.Branch("2");

		// Token: 0x040008A8 RID: 2216
		public static readonly DerObjectIdentifier IdIcaoCscaMasterListSigningKey = IcaoObjectIdentifiers.IdIcaoMrtdSecurity.Branch("3");

		// Token: 0x040008A9 RID: 2217
		public static readonly DerObjectIdentifier IdIcaoDocumentTypeList = IcaoObjectIdentifiers.IdIcaoMrtdSecurity.Branch("4");

		// Token: 0x040008AA RID: 2218
		public static readonly DerObjectIdentifier IdIcaoAAProtocolObject = IcaoObjectIdentifiers.IdIcaoMrtdSecurity.Branch("5");

		// Token: 0x040008AB RID: 2219
		public static readonly DerObjectIdentifier IdIcaoExtensions = IcaoObjectIdentifiers.IdIcaoMrtdSecurity.Branch("6");

		// Token: 0x040008AC RID: 2220
		public static readonly DerObjectIdentifier IdIcaoExtensionsNamechangekeyrollover = IcaoObjectIdentifiers.IdIcaoExtensions.Branch("1");
	}
}
