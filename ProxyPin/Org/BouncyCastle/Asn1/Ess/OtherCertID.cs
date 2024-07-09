using System;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x02000167 RID: 359
	[Obsolete("Use version in Asn1.Esf instead")]
	public class OtherCertID : Asn1Encodable
	{
		// Token: 0x06000C3E RID: 3134 RVA: 0x0004F014 File Offset: 0x0004F014
		public static OtherCertID GetInstance(object o)
		{
			if (o == null || o is OtherCertID)
			{
				return (OtherCertID)o;
			}
			if (o is Asn1Sequence)
			{
				return new OtherCertID((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in 'OtherCertID' factory : " + Platform.GetTypeName(o) + ".");
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0004F070 File Offset: 0x0004F070
		public OtherCertID(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			if (seq[0].ToAsn1Object() is Asn1OctetString)
			{
				this.otherCertHash = Asn1OctetString.GetInstance(seq[0]);
			}
			else
			{
				this.otherCertHash = DigestInfo.GetInstance(seq[0]);
			}
			if (seq.Count > 1)
			{
				this.issuerSerial = IssuerSerial.GetInstance(Asn1Sequence.GetInstance(seq[1]));
			}
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0004F11C File Offset: 0x0004F11C
		public OtherCertID(AlgorithmIdentifier algId, byte[] digest)
		{
			this.otherCertHash = new DigestInfo(algId, digest);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0004F134 File Offset: 0x0004F134
		public OtherCertID(AlgorithmIdentifier algId, byte[] digest, IssuerSerial issuerSerial)
		{
			this.otherCertHash = new DigestInfo(algId, digest);
			this.issuerSerial = issuerSerial;
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0004F150 File Offset: 0x0004F150
		public AlgorithmIdentifier AlgorithmHash
		{
			get
			{
				if (this.otherCertHash.ToAsn1Object() is Asn1OctetString)
				{
					return new AlgorithmIdentifier(OiwObjectIdentifiers.IdSha1);
				}
				return DigestInfo.GetInstance(this.otherCertHash).AlgorithmID;
			}
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0004F184 File Offset: 0x0004F184
		public byte[] GetCertHash()
		{
			if (this.otherCertHash.ToAsn1Object() is Asn1OctetString)
			{
				return ((Asn1OctetString)this.otherCertHash.ToAsn1Object()).GetOctets();
			}
			return DigestInfo.GetInstance(this.otherCertHash).GetDigest();
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x0004F1C4 File Offset: 0x0004F1C4
		public IssuerSerial IssuerSerial
		{
			get
			{
				return this.issuerSerial;
			}
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0004F1CC File Offset: 0x0004F1CC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.otherCertHash
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.issuerSerial
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000835 RID: 2101
		private Asn1Encodable otherCertHash;

		// Token: 0x04000836 RID: 2102
		private IssuerSerial issuerSerial;
	}
}
