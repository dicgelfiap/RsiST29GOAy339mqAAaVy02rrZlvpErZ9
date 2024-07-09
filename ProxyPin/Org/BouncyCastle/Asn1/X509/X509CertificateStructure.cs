using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200021E RID: 542
	public class X509CertificateStructure : Asn1Encodable
	{
		// Token: 0x06001179 RID: 4473 RVA: 0x000632AC File Offset: 0x000632AC
		public static X509CertificateStructure GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return X509CertificateStructure.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x000632BC File Offset: 0x000632BC
		public static X509CertificateStructure GetInstance(object obj)
		{
			if (obj is X509CertificateStructure)
			{
				return (X509CertificateStructure)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new X509CertificateStructure(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x000632E4 File Offset: 0x000632E4
		public X509CertificateStructure(TbsCertificateStructure tbsCert, AlgorithmIdentifier sigAlgID, DerBitString sig)
		{
			if (tbsCert == null)
			{
				throw new ArgumentNullException("tbsCert");
			}
			if (sigAlgID == null)
			{
				throw new ArgumentNullException("sigAlgID");
			}
			if (sig == null)
			{
				throw new ArgumentNullException("sig");
			}
			this.tbsCert = tbsCert;
			this.sigAlgID = sigAlgID;
			this.sig = sig;
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00063344 File Offset: 0x00063344
		private X509CertificateStructure(Asn1Sequence seq)
		{
			if (seq.Count != 3)
			{
				throw new ArgumentException("sequence wrong size for a certificate", "seq");
			}
			this.tbsCert = TbsCertificateStructure.GetInstance(seq[0]);
			this.sigAlgID = AlgorithmIdentifier.GetInstance(seq[1]);
			this.sig = DerBitString.GetInstance(seq[2]);
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x000633B0 File Offset: 0x000633B0
		public TbsCertificateStructure TbsCertificate
		{
			get
			{
				return this.tbsCert;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x000633B8 File Offset: 0x000633B8
		public int Version
		{
			get
			{
				return this.tbsCert.Version;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x000633C8 File Offset: 0x000633C8
		public DerInteger SerialNumber
		{
			get
			{
				return this.tbsCert.SerialNumber;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x000633D8 File Offset: 0x000633D8
		public X509Name Issuer
		{
			get
			{
				return this.tbsCert.Issuer;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x000633E8 File Offset: 0x000633E8
		public Time StartDate
		{
			get
			{
				return this.tbsCert.StartDate;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x000633F8 File Offset: 0x000633F8
		public Time EndDate
		{
			get
			{
				return this.tbsCert.EndDate;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x00063408 File Offset: 0x00063408
		public X509Name Subject
		{
			get
			{
				return this.tbsCert.Subject;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x00063418 File Offset: 0x00063418
		public SubjectPublicKeyInfo SubjectPublicKeyInfo
		{
			get
			{
				return this.tbsCert.SubjectPublicKeyInfo;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x00063428 File Offset: 0x00063428
		public AlgorithmIdentifier SignatureAlgorithm
		{
			get
			{
				return this.sigAlgID;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x00063430 File Offset: 0x00063430
		public DerBitString Signature
		{
			get
			{
				return this.sig;
			}
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00063438 File Offset: 0x00063438
		public byte[] GetSignatureOctets()
		{
			return this.sig.GetOctets();
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00063448 File Offset: 0x00063448
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.tbsCert,
				this.sigAlgID,
				this.sig
			});
		}

		// Token: 0x04000C85 RID: 3205
		private readonly TbsCertificateStructure tbsCert;

		// Token: 0x04000C86 RID: 3206
		private readonly AlgorithmIdentifier sigAlgID;

		// Token: 0x04000C87 RID: 3207
		private readonly DerBitString sig;
	}
}
