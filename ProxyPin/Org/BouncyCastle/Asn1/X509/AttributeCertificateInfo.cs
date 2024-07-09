using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001E6 RID: 486
	public class AttributeCertificateInfo : Asn1Encodable
	{
		// Token: 0x06000F95 RID: 3989 RVA: 0x0005CCD8 File Offset: 0x0005CCD8
		public static AttributeCertificateInfo GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return AttributeCertificateInfo.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0005CCE8 File Offset: 0x0005CCE8
		public static AttributeCertificateInfo GetInstance(object obj)
		{
			if (obj is AttributeCertificateInfo)
			{
				return (AttributeCertificateInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AttributeCertificateInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0005CD3C File Offset: 0x0005CD3C
		private AttributeCertificateInfo(Asn1Sequence seq)
		{
			if (seq.Count < 7 || seq.Count > 9)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.version = DerInteger.GetInstance(seq[0]);
			this.holder = Holder.GetInstance(seq[1]);
			this.issuer = AttCertIssuer.GetInstance(seq[2]);
			this.signature = AlgorithmIdentifier.GetInstance(seq[3]);
			this.serialNumber = DerInteger.GetInstance(seq[4]);
			this.attrCertValidityPeriod = AttCertValidityPeriod.GetInstance(seq[5]);
			this.attributes = Asn1Sequence.GetInstance(seq[6]);
			for (int i = 7; i < seq.Count; i++)
			{
				Asn1Encodable asn1Encodable = seq[i];
				if (asn1Encodable is DerBitString)
				{
					this.issuerUniqueID = DerBitString.GetInstance(seq[i]);
				}
				else if (asn1Encodable is Asn1Sequence || asn1Encodable is X509Extensions)
				{
					this.extensions = X509Extensions.GetInstance(seq[i]);
				}
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x0005CE6C File Offset: 0x0005CE6C
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x0005CE74 File Offset: 0x0005CE74
		public Holder Holder
		{
			get
			{
				return this.holder;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x0005CE7C File Offset: 0x0005CE7C
		public AttCertIssuer Issuer
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x0005CE84 File Offset: 0x0005CE84
		public AlgorithmIdentifier Signature
		{
			get
			{
				return this.signature;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x0005CE8C File Offset: 0x0005CE8C
		public DerInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x0005CE94 File Offset: 0x0005CE94
		public AttCertValidityPeriod AttrCertValidityPeriod
		{
			get
			{
				return this.attrCertValidityPeriod;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x0005CE9C File Offset: 0x0005CE9C
		public Asn1Sequence Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x0005CEA4 File Offset: 0x0005CEA4
		public DerBitString IssuerUniqueID
		{
			get
			{
				return this.issuerUniqueID;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x0005CEAC File Offset: 0x0005CEAC
		public X509Extensions Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0005CEB4 File Offset: 0x0005CEB4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.holder,
				this.issuer,
				this.signature,
				this.serialNumber,
				this.attrCertValidityPeriod,
				this.attributes
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.issuerUniqueID,
				this.extensions
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000BA3 RID: 2979
		internal readonly DerInteger version;

		// Token: 0x04000BA4 RID: 2980
		internal readonly Holder holder;

		// Token: 0x04000BA5 RID: 2981
		internal readonly AttCertIssuer issuer;

		// Token: 0x04000BA6 RID: 2982
		internal readonly AlgorithmIdentifier signature;

		// Token: 0x04000BA7 RID: 2983
		internal readonly DerInteger serialNumber;

		// Token: 0x04000BA8 RID: 2984
		internal readonly AttCertValidityPeriod attrCertValidityPeriod;

		// Token: 0x04000BA9 RID: 2985
		internal readonly Asn1Sequence attributes;

		// Token: 0x04000BAA RID: 2986
		internal readonly DerBitString issuerUniqueID;

		// Token: 0x04000BAB RID: 2987
		internal readonly X509Extensions extensions;
	}
}
