using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;

namespace Org.BouncyCastle.Pkcs
{
	// Token: 0x02000682 RID: 1666
	public class Pkcs12StoreBuilder
	{
		// Token: 0x06003A3E RID: 14910 RVA: 0x00139F68 File Offset: 0x00139F68
		public Pkcs12Store Build()
		{
			return new Pkcs12Store(this.keyAlgorithm, this.certAlgorithm, this.useDerEncoding);
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x00139F84 File Offset: 0x00139F84
		public Pkcs12StoreBuilder SetCertAlgorithm(DerObjectIdentifier certAlgorithm)
		{
			this.certAlgorithm = certAlgorithm;
			return this;
		}

		// Token: 0x06003A40 RID: 14912 RVA: 0x00139F90 File Offset: 0x00139F90
		public Pkcs12StoreBuilder SetKeyAlgorithm(DerObjectIdentifier keyAlgorithm)
		{
			this.keyAlgorithm = keyAlgorithm;
			return this;
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x00139F9C File Offset: 0x00139F9C
		public Pkcs12StoreBuilder SetUseDerEncoding(bool useDerEncoding)
		{
			this.useDerEncoding = useDerEncoding;
			return this;
		}

		// Token: 0x04001E42 RID: 7746
		private DerObjectIdentifier keyAlgorithm = PkcsObjectIdentifiers.PbeWithShaAnd3KeyTripleDesCbc;

		// Token: 0x04001E43 RID: 7747
		private DerObjectIdentifier certAlgorithm = PkcsObjectIdentifiers.PbewithShaAnd40BitRC2Cbc;

		// Token: 0x04001E44 RID: 7748
		private bool useDerEncoding = false;
	}
}
