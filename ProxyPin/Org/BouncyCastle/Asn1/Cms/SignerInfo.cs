using System;
using System.Collections;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000123 RID: 291
	public class SignerInfo : Asn1Encodable
	{
		// Token: 0x06000A60 RID: 2656 RVA: 0x00048454 File Offset: 0x00048454
		public static SignerInfo GetInstance(object obj)
		{
			if (obj == null || obj is SignerInfo)
			{
				return (SignerInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SignerInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x000484B0 File Offset: 0x000484B0
		public SignerInfo(SignerIdentifier sid, AlgorithmIdentifier digAlgorithm, Asn1Set authenticatedAttributes, AlgorithmIdentifier digEncryptionAlgorithm, Asn1OctetString encryptedDigest, Asn1Set unauthenticatedAttributes)
		{
			this.version = new DerInteger(sid.IsTagged ? 3 : 1);
			this.sid = sid;
			this.digAlgorithm = digAlgorithm;
			this.authenticatedAttributes = authenticatedAttributes;
			this.digEncryptionAlgorithm = digEncryptionAlgorithm;
			this.encryptedDigest = encryptedDigest;
			this.unauthenticatedAttributes = unauthenticatedAttributes;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00048514 File Offset: 0x00048514
		public SignerInfo(SignerIdentifier sid, AlgorithmIdentifier digAlgorithm, Attributes authenticatedAttributes, AlgorithmIdentifier digEncryptionAlgorithm, Asn1OctetString encryptedDigest, Attributes unauthenticatedAttributes)
		{
			this.version = new DerInteger(sid.IsTagged ? 3 : 1);
			this.sid = sid;
			this.digAlgorithm = digAlgorithm;
			this.authenticatedAttributes = Asn1Set.GetInstance(authenticatedAttributes);
			this.digEncryptionAlgorithm = digEncryptionAlgorithm;
			this.encryptedDigest = encryptedDigest;
			this.unauthenticatedAttributes = Asn1Set.GetInstance(unauthenticatedAttributes);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00048580 File Offset: 0x00048580
		[Obsolete("Use 'GetInstance' instead")]
		public SignerInfo(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.version = (DerInteger)enumerator.Current;
			enumerator.MoveNext();
			this.sid = SignerIdentifier.GetInstance(enumerator.Current);
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

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00048684 File Offset: 0x00048684
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x0004868C File Offset: 0x0004868C
		public SignerIdentifier SignerID
		{
			get
			{
				return this.sid;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00048694 File Offset: 0x00048694
		public Asn1Set AuthenticatedAttributes
		{
			get
			{
				return this.authenticatedAttributes;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x0004869C File Offset: 0x0004869C
		public AlgorithmIdentifier DigestAlgorithm
		{
			get
			{
				return this.digAlgorithm;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x000486A4 File Offset: 0x000486A4
		public Asn1OctetString EncryptedDigest
		{
			get
			{
				return this.encryptedDigest;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x000486AC File Offset: 0x000486AC
		public AlgorithmIdentifier DigestEncryptionAlgorithm
		{
			get
			{
				return this.digEncryptionAlgorithm;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x000486B4 File Offset: 0x000486B4
		public Asn1Set UnauthenticatedAttributes
		{
			get
			{
				return this.unauthenticatedAttributes;
			}
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x000486BC File Offset: 0x000486BC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.sid,
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

		// Token: 0x04000731 RID: 1841
		private DerInteger version;

		// Token: 0x04000732 RID: 1842
		private SignerIdentifier sid;

		// Token: 0x04000733 RID: 1843
		private AlgorithmIdentifier digAlgorithm;

		// Token: 0x04000734 RID: 1844
		private Asn1Set authenticatedAttributes;

		// Token: 0x04000735 RID: 1845
		private AlgorithmIdentifier digEncryptionAlgorithm;

		// Token: 0x04000736 RID: 1846
		private Asn1OctetString encryptedDigest;

		// Token: 0x04000737 RID: 1847
		private Asn1Set unauthenticatedAttributes;
	}
}
