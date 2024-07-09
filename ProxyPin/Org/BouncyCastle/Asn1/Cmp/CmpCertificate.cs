using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000D6 RID: 214
	public class CmpCertificate : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06000822 RID: 2082 RVA: 0x00041534 File Offset: 0x00041534
		public CmpCertificate(AttributeCertificate x509v2AttrCert)
		{
			this.x509v2AttrCert = x509v2AttrCert;
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00041544 File Offset: 0x00041544
		public CmpCertificate(X509CertificateStructure x509v3PKCert)
		{
			if (x509v3PKCert.Version != 3)
			{
				throw new ArgumentException("only version 3 certificates allowed", "x509v3PKCert");
			}
			this.x509v3PKCert = x509v3PKCert;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00041570 File Offset: 0x00041570
		public static CmpCertificate GetInstance(object obj)
		{
			if (obj is CmpCertificate)
			{
				return (CmpCertificate)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CmpCertificate(X509CertificateStructure.GetInstance(obj));
			}
			if (obj is Asn1TaggedObject)
			{
				return new CmpCertificate(AttributeCertificate.GetInstance(((Asn1TaggedObject)obj).GetObject()));
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x000415E8 File Offset: 0x000415E8
		public virtual bool IsX509v3PKCert
		{
			get
			{
				return this.x509v3PKCert != null;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x000415F8 File Offset: 0x000415F8
		public virtual X509CertificateStructure X509v3PKCert
		{
			get
			{
				return this.x509v3PKCert;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x00041600 File Offset: 0x00041600
		public virtual AttributeCertificate X509v2AttrCert
		{
			get
			{
				return this.x509v2AttrCert;
			}
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00041608 File Offset: 0x00041608
		public override Asn1Object ToAsn1Object()
		{
			if (this.x509v2AttrCert != null)
			{
				return new DerTaggedObject(true, 1, this.x509v2AttrCert);
			}
			return this.x509v3PKCert.ToAsn1Object();
		}

		// Token: 0x040005ED RID: 1517
		private readonly X509CertificateStructure x509v3PKCert;

		// Token: 0x040005EE RID: 1518
		private readonly AttributeCertificate x509v2AttrCert;
	}
}
