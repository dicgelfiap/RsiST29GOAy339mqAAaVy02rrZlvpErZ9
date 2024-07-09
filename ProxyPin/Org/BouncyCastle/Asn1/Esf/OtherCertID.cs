using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000157 RID: 343
	public class OtherCertID : Asn1Encodable
	{
		// Token: 0x06000BC7 RID: 3015 RVA: 0x0004D44C File Offset: 0x0004D44C
		public static OtherCertID GetInstance(object obj)
		{
			if (obj == null || obj is OtherCertID)
			{
				return (OtherCertID)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OtherCertID((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OtherCertID' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0004D4A8 File Offset: 0x0004D4A8
		private OtherCertID(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.otherCertHash = OtherHash.GetInstance(seq[0].ToAsn1Object());
			if (seq.Count > 1)
			{
				this.issuerSerial = IssuerSerial.GetInstance(seq[1].ToAsn1Object());
			}
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0004D544 File Offset: 0x0004D544
		public OtherCertID(OtherHash otherCertHash) : this(otherCertHash, null)
		{
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0004D550 File Offset: 0x0004D550
		public OtherCertID(OtherHash otherCertHash, IssuerSerial issuerSerial)
		{
			if (otherCertHash == null)
			{
				throw new ArgumentNullException("otherCertHash");
			}
			this.otherCertHash = otherCertHash;
			this.issuerSerial = issuerSerial;
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x0004D578 File Offset: 0x0004D578
		public OtherHash OtherCertHash
		{
			get
			{
				return this.otherCertHash;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0004D580 File Offset: 0x0004D580
		public IssuerSerial IssuerSerial
		{
			get
			{
				return this.issuerSerial;
			}
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0004D588 File Offset: 0x0004D588
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.otherCertHash.ToAsn1Object()
			});
			if (this.issuerSerial != null)
			{
				asn1EncodableVector.Add(this.issuerSerial.ToAsn1Object());
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000812 RID: 2066
		private readonly OtherHash otherCertHash;

		// Token: 0x04000813 RID: 2067
		private readonly IssuerSerial issuerSerial;
	}
}
