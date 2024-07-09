using System;
using Org.BouncyCastle.Asn1.Pkcs;

namespace Org.BouncyCastle.Asn1.Smime
{
	// Token: 0x020001C1 RID: 449
	public abstract class SmimeAttributes
	{
		// Token: 0x04000AEC RID: 2796
		public static readonly DerObjectIdentifier SmimeCapabilities = PkcsObjectIdentifiers.Pkcs9AtSmimeCapabilities;

		// Token: 0x04000AED RID: 2797
		public static readonly DerObjectIdentifier EncrypKeyPref = PkcsObjectIdentifiers.IdAAEncrypKeyPref;
	}
}
