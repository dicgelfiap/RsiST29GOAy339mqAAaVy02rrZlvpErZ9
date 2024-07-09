using System;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000712 RID: 1810
	public interface IX509AttributeCertificate : IX509Extension
	{
		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06003F57 RID: 16215
		int Version { get; }

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06003F58 RID: 16216
		BigInteger SerialNumber { get; }

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06003F59 RID: 16217
		DateTime NotBefore { get; }

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06003F5A RID: 16218
		DateTime NotAfter { get; }

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06003F5B RID: 16219
		AttributeCertificateHolder Holder { get; }

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06003F5C RID: 16220
		AttributeCertificateIssuer Issuer { get; }

		// Token: 0x06003F5D RID: 16221
		X509Attribute[] GetAttributes();

		// Token: 0x06003F5E RID: 16222
		X509Attribute[] GetAttributes(string oid);

		// Token: 0x06003F5F RID: 16223
		bool[] GetIssuerUniqueID();

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06003F60 RID: 16224
		bool IsValidNow { get; }

		// Token: 0x06003F61 RID: 16225
		bool IsValid(DateTime date);

		// Token: 0x06003F62 RID: 16226
		void CheckValidity();

		// Token: 0x06003F63 RID: 16227
		void CheckValidity(DateTime date);

		// Token: 0x06003F64 RID: 16228
		byte[] GetSignature();

		// Token: 0x06003F65 RID: 16229
		void Verify(AsymmetricKeyParameter publicKey);

		// Token: 0x06003F66 RID: 16230
		byte[] GetEncoded();
	}
}
