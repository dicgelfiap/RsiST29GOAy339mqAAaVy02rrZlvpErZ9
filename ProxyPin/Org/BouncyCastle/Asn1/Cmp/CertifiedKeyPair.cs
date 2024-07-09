using System;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000CF RID: 207
	public class CertifiedKeyPair : Asn1Encodable
	{
		// Token: 0x060007F9 RID: 2041 RVA: 0x00040C28 File Offset: 0x00040C28
		private CertifiedKeyPair(Asn1Sequence seq)
		{
			this.certOrEncCert = CertOrEncCert.GetInstance(seq[0]);
			if (seq.Count >= 2)
			{
				if (seq.Count == 2)
				{
					Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[1]);
					if (instance.TagNo == 0)
					{
						this.privateKey = EncryptedValue.GetInstance(instance.GetObject());
						return;
					}
					this.publicationInfo = PkiPublicationInfo.GetInstance(instance.GetObject());
					return;
				}
				else
				{
					this.privateKey = EncryptedValue.GetInstance(Asn1TaggedObject.GetInstance(seq[1]));
					this.publicationInfo = PkiPublicationInfo.GetInstance(Asn1TaggedObject.GetInstance(seq[2]));
				}
			}
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00040CD4 File Offset: 0x00040CD4
		public static CertifiedKeyPair GetInstance(object obj)
		{
			if (obj is CertifiedKeyPair)
			{
				return (CertifiedKeyPair)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertifiedKeyPair((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00040D28 File Offset: 0x00040D28
		public CertifiedKeyPair(CertOrEncCert certOrEncCert) : this(certOrEncCert, null, null)
		{
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00040D34 File Offset: 0x00040D34
		public CertifiedKeyPair(CertOrEncCert certOrEncCert, EncryptedValue privateKey, PkiPublicationInfo publicationInfo)
		{
			if (certOrEncCert == null)
			{
				throw new ArgumentNullException("certOrEncCert");
			}
			this.certOrEncCert = certOrEncCert;
			this.privateKey = privateKey;
			this.publicationInfo = publicationInfo;
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x00040D64 File Offset: 0x00040D64
		public virtual CertOrEncCert CertOrEncCert
		{
			get
			{
				return this.certOrEncCert;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x00040D6C File Offset: 0x00040D6C
		public virtual EncryptedValue PrivateKey
		{
			get
			{
				return this.privateKey;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x00040D74 File Offset: 0x00040D74
		public virtual PkiPublicationInfo PublicationInfo
		{
			get
			{
				return this.publicationInfo;
			}
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00040D7C File Offset: 0x00040D7C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certOrEncCert
			});
			asn1EncodableVector.AddOptionalTagged(true, 0, this.privateKey);
			asn1EncodableVector.AddOptionalTagged(true, 1, this.publicationInfo);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040005DC RID: 1500
		private readonly CertOrEncCert certOrEncCert;

		// Token: 0x040005DD RID: 1501
		private readonly EncryptedValue privateKey;

		// Token: 0x040005DE RID: 1502
		private readonly PkiPublicationInfo publicationInfo;
	}
}
