using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x02000165 RID: 357
	public class EssCertID : Asn1Encodable
	{
		// Token: 0x06000C2C RID: 3116 RVA: 0x0004ECA8 File Offset: 0x0004ECA8
		public static EssCertID GetInstance(object o)
		{
			if (o == null || o is EssCertID)
			{
				return (EssCertID)o;
			}
			if (o is Asn1Sequence)
			{
				return new EssCertID((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in 'EssCertID' factory : " + Platform.GetTypeName(o) + ".");
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0004ED04 File Offset: 0x0004ED04
		public EssCertID(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.certHash = Asn1OctetString.GetInstance(seq[0]);
			if (seq.Count > 1)
			{
				this.issuerSerial = IssuerSerial.GetInstance(seq[1]);
			}
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0004ED80 File Offset: 0x0004ED80
		public EssCertID(byte[] hash)
		{
			this.certHash = new DerOctetString(hash);
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0004ED94 File Offset: 0x0004ED94
		public EssCertID(byte[] hash, IssuerSerial issuerSerial)
		{
			this.certHash = new DerOctetString(hash);
			this.issuerSerial = issuerSerial;
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0004EDB0 File Offset: 0x0004EDB0
		public byte[] GetCertHash()
		{
			return this.certHash.GetOctets();
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0004EDC0 File Offset: 0x0004EDC0
		public IssuerSerial IssuerSerial
		{
			get
			{
				return this.issuerSerial;
			}
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0004EDC8 File Offset: 0x0004EDC8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certHash
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.issuerSerial
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400082F RID: 2095
		private Asn1OctetString certHash;

		// Token: 0x04000830 RID: 2096
		private IssuerSerial issuerSerial;
	}
}
