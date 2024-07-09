using System;
using System.Collections;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001BC RID: 444
	public class SignerInfo : Asn1Encodable
	{
		// Token: 0x06000E77 RID: 3703 RVA: 0x00057BA4 File Offset: 0x00057BA4
		public static SignerInfo GetInstance(object obj)
		{
			if (obj is SignerInfo)
			{
				return (SignerInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SignerInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00057BF8 File Offset: 0x00057BF8
		public SignerInfo(DerInteger version, IssuerAndSerialNumber issuerAndSerialNumber, AlgorithmIdentifier digAlgorithm, Asn1Set authenticatedAttributes, AlgorithmIdentifier digEncryptionAlgorithm, Asn1OctetString encryptedDigest, Asn1Set unauthenticatedAttributes)
		{
			this.version = version;
			this.issuerAndSerialNumber = issuerAndSerialNumber;
			this.digAlgorithm = digAlgorithm;
			this.authenticatedAttributes = authenticatedAttributes;
			this.digEncryptionAlgorithm = digEncryptionAlgorithm;
			this.encryptedDigest = encryptedDigest;
			this.unauthenticatedAttributes = unauthenticatedAttributes;
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00057C38 File Offset: 0x00057C38
		public SignerInfo(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.version = (DerInteger)enumerator.Current;
			enumerator.MoveNext();
			this.issuerAndSerialNumber = IssuerAndSerialNumber.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			this.digAlgorithm = AlgorithmIdentifier.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			object obj = enumerator.Current;
			if (obj is Asn1TaggedObject)
			{
				this.authenticatedAttributes = Asn1Set.GetInstance((Asn1TaggedObject)obj, false);
				enumerator.MoveNext();
				this.digEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(enumerator.Current);
			}
			else
			{
				this.authenticatedAttributes = null;
				this.digEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(obj);
			}
			enumerator.MoveNext();
			this.encryptedDigest = Asn1OctetString.GetInstance(enumerator.Current);
			if (enumerator.MoveNext())
			{
				this.unauthenticatedAttributes = Asn1Set.GetInstance((Asn1TaggedObject)enumerator.Current, false);
				return;
			}
			this.unauthenticatedAttributes = null;
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x00057D3C File Offset: 0x00057D3C
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x00057D44 File Offset: 0x00057D44
		public IssuerAndSerialNumber IssuerAndSerialNumber
		{
			get
			{
				return this.issuerAndSerialNumber;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x00057D4C File Offset: 0x00057D4C
		public Asn1Set AuthenticatedAttributes
		{
			get
			{
				return this.authenticatedAttributes;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x00057D54 File Offset: 0x00057D54
		public AlgorithmIdentifier DigestAlgorithm
		{
			get
			{
				return this.digAlgorithm;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x00057D5C File Offset: 0x00057D5C
		public Asn1OctetString EncryptedDigest
		{
			get
			{
				return this.encryptedDigest;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x00057D64 File Offset: 0x00057D64
		public AlgorithmIdentifier DigestEncryptionAlgorithm
		{
			get
			{
				return this.digEncryptionAlgorithm;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x00057D6C File Offset: 0x00057D6C
		public Asn1Set UnauthenticatedAttributes
		{
			get
			{
				return this.unauthenticatedAttributes;
			}
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00057D74 File Offset: 0x00057D74
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.issuerAndSerialNumber,
				this.digAlgorithm
			});
			asn1EncodableVector.AddOptionalTagged(false, 0, this.authenticatedAttributes);
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.digEncryptionAlgorithm,
				this.encryptedDigest
			});
			asn1EncodableVector.AddOptionalTagged(false, 1, this.unauthenticatedAttributes);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000AAB RID: 2731
		private DerInteger version;

		// Token: 0x04000AAC RID: 2732
		private IssuerAndSerialNumber issuerAndSerialNumber;

		// Token: 0x04000AAD RID: 2733
		private AlgorithmIdentifier digAlgorithm;

		// Token: 0x04000AAE RID: 2734
		private Asn1Set authenticatedAttributes;

		// Token: 0x04000AAF RID: 2735
		private AlgorithmIdentifier digEncryptionAlgorithm;

		// Token: 0x04000AB0 RID: 2736
		private Asn1OctetString encryptedDigest;

		// Token: 0x04000AB1 RID: 2737
		private Asn1Set unauthenticatedAttributes;
	}
}
