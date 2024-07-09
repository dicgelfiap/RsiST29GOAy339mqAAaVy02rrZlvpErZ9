using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x0200030B RID: 779
	public class OriginatorInfoGenerator
	{
		// Token: 0x0600179D RID: 6045 RVA: 0x0007B0AC File Offset: 0x0007B0AC
		public OriginatorInfoGenerator(X509Certificate origCert)
		{
			this.origCerts = Platform.CreateArrayList(1);
			this.origCrls = null;
			this.origCerts.Add(origCert.CertificateStructure);
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0007B0DC File Offset: 0x0007B0DC
		public OriginatorInfoGenerator(IX509Store origCerts) : this(origCerts, null)
		{
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0007B0E8 File Offset: 0x0007B0E8
		public OriginatorInfoGenerator(IX509Store origCerts, IX509Store origCrls)
		{
			this.origCerts = CmsUtilities.GetCertificatesFromStore(origCerts);
			this.origCrls = ((origCrls == null) ? null : CmsUtilities.GetCrlsFromStore(origCrls));
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0007B114 File Offset: 0x0007B114
		public virtual OriginatorInfo Generate()
		{
			Asn1Set certs = CmsUtilities.CreateDerSetFromList(this.origCerts);
			Asn1Set crls = (this.origCrls == null) ? null : CmsUtilities.CreateDerSetFromList(this.origCrls);
			return new OriginatorInfo(certs, crls);
		}

		// Token: 0x04000FCC RID: 4044
		private readonly IList origCerts;

		// Token: 0x04000FCD RID: 4045
		private readonly IList origCrls;
	}
}
