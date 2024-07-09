using System;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x02000166 RID: 358
	public class EssCertIDv2 : Asn1Encodable
	{
		// Token: 0x06000C33 RID: 3123 RVA: 0x0004EE18 File Offset: 0x0004EE18
		public static EssCertIDv2 GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			EssCertIDv2 essCertIDv = obj as EssCertIDv2;
			if (essCertIDv != null)
			{
				return essCertIDv;
			}
			return new EssCertIDv2(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0004EE4C File Offset: 0x0004EE4C
		private EssCertIDv2(Asn1Sequence seq)
		{
			if (seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			int num = 0;
			if (seq[0] is Asn1OctetString)
			{
				this.hashAlgorithm = EssCertIDv2.DefaultAlgID;
			}
			else
			{
				this.hashAlgorithm = AlgorithmIdentifier.GetInstance(seq[num++].ToAsn1Object());
			}
			this.certHash = Asn1OctetString.GetInstance(seq[num++].ToAsn1Object()).GetOctets();
			if (seq.Count > num)
			{
				this.issuerSerial = IssuerSerial.GetInstance(Asn1Sequence.GetInstance(seq[num].ToAsn1Object()));
			}
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0004EF18 File Offset: 0x0004EF18
		public EssCertIDv2(byte[] certHash) : this(null, certHash, null)
		{
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0004EF24 File Offset: 0x0004EF24
		public EssCertIDv2(AlgorithmIdentifier algId, byte[] certHash) : this(algId, certHash, null)
		{
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0004EF30 File Offset: 0x0004EF30
		public EssCertIDv2(byte[] certHash, IssuerSerial issuerSerial) : this(null, certHash, issuerSerial)
		{
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0004EF3C File Offset: 0x0004EF3C
		public EssCertIDv2(AlgorithmIdentifier algId, byte[] certHash, IssuerSerial issuerSerial)
		{
			if (algId == null)
			{
				this.hashAlgorithm = EssCertIDv2.DefaultAlgID;
			}
			else
			{
				this.hashAlgorithm = algId;
			}
			this.certHash = certHash;
			this.issuerSerial = issuerSerial;
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0004EF70 File Offset: 0x0004EF70
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0004EF78 File Offset: 0x0004EF78
		public byte[] GetCertHash()
		{
			return Arrays.Clone(this.certHash);
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x0004EF88 File Offset: 0x0004EF88
		public IssuerSerial IssuerSerial
		{
			get
			{
				return this.issuerSerial;
			}
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0004EF90 File Offset: 0x0004EF90
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			if (!this.hashAlgorithm.Equals(EssCertIDv2.DefaultAlgID))
			{
				asn1EncodableVector.Add(this.hashAlgorithm);
			}
			asn1EncodableVector.Add(new DerOctetString(this.certHash).ToAsn1Object());
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.issuerSerial
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000831 RID: 2097
		private readonly AlgorithmIdentifier hashAlgorithm;

		// Token: 0x04000832 RID: 2098
		private readonly byte[] certHash;

		// Token: 0x04000833 RID: 2099
		private readonly IssuerSerial issuerSerial;

		// Token: 0x04000834 RID: 2100
		private static readonly AlgorithmIdentifier DefaultAlgID = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha256);
	}
}
