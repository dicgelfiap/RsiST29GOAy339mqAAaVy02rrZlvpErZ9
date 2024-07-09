using System;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000D1 RID: 209
	public class CertOrEncCert : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06000801 RID: 2049 RVA: 0x00040DCC File Offset: 0x00040DCC
		private CertOrEncCert(Asn1TaggedObject tagged)
		{
			if (tagged.TagNo == 0)
			{
				this.certificate = CmpCertificate.GetInstance(tagged.GetObject());
				return;
			}
			if (tagged.TagNo == 1)
			{
				this.encryptedCert = EncryptedValue.GetInstance(tagged.GetObject());
				return;
			}
			throw new ArgumentException("unknown tag: " + tagged.TagNo, "tagged");
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00040E40 File Offset: 0x00040E40
		public static CertOrEncCert GetInstance(object obj)
		{
			if (obj is CertOrEncCert)
			{
				return (CertOrEncCert)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return new CertOrEncCert((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00040E94 File Offset: 0x00040E94
		public CertOrEncCert(CmpCertificate certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			this.certificate = certificate;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00040EB4 File Offset: 0x00040EB4
		public CertOrEncCert(EncryptedValue encryptedCert)
		{
			if (encryptedCert == null)
			{
				throw new ArgumentNullException("encryptedCert");
			}
			this.encryptedCert = encryptedCert;
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x00040ED4 File Offset: 0x00040ED4
		public virtual CmpCertificate Certificate
		{
			get
			{
				return this.certificate;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x00040EDC File Offset: 0x00040EDC
		public virtual EncryptedValue EncryptedCert
		{
			get
			{
				return this.encryptedCert;
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00040EE4 File Offset: 0x00040EE4
		public override Asn1Object ToAsn1Object()
		{
			if (this.certificate != null)
			{
				return new DerTaggedObject(true, 0, this.certificate);
			}
			return new DerTaggedObject(true, 1, this.encryptedCert);
		}

		// Token: 0x040005DF RID: 1503
		private readonly CmpCertificate certificate;

		// Token: 0x040005E0 RID: 1504
		private readonly EncryptedValue encryptedCert;
	}
}
