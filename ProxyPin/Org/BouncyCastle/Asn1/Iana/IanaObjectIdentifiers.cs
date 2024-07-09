using System;

namespace Org.BouncyCastle.Asn1.Iana
{
	// Token: 0x0200016E RID: 366
	public abstract class IanaObjectIdentifiers
	{
		// Token: 0x0400089A RID: 2202
		public static readonly DerObjectIdentifier IsakmpOakley = new DerObjectIdentifier("1.3.6.1.5.5.8.1");

		// Token: 0x0400089B RID: 2203
		public static readonly DerObjectIdentifier HmacMD5 = new DerObjectIdentifier(IanaObjectIdentifiers.IsakmpOakley + ".1");

		// Token: 0x0400089C RID: 2204
		public static readonly DerObjectIdentifier HmacSha1 = new DerObjectIdentifier(IanaObjectIdentifiers.IsakmpOakley + ".2");

		// Token: 0x0400089D RID: 2205
		public static readonly DerObjectIdentifier HmacTiger = new DerObjectIdentifier(IanaObjectIdentifiers.IsakmpOakley + ".3");

		// Token: 0x0400089E RID: 2206
		public static readonly DerObjectIdentifier HmacRipeMD160 = new DerObjectIdentifier(IanaObjectIdentifiers.IsakmpOakley + ".4");
	}
}
