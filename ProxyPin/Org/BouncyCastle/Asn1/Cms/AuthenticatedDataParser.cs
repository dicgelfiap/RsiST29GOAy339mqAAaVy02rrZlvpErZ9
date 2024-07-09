using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x020000FE RID: 254
	public class AuthenticatedDataParser
	{
		// Token: 0x06000943 RID: 2371 RVA: 0x00044D68 File Offset: 0x00044D68
		public AuthenticatedDataParser(Asn1SequenceParser seq)
		{
			this.seq = seq;
			this.version = (DerInteger)seq.ReadObject();
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x00044D88 File Offset: 0x00044D88
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00044D90 File Offset: 0x00044D90
		public OriginatorInfo GetOriginatorInfo()
		{
			this.originatorInfoCalled = true;
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			if (this.nextObject is Asn1TaggedObjectParser && ((Asn1TaggedObjectParser)this.nextObject).TagNo == 0)
			{
				Asn1SequenceParser asn1SequenceParser = (Asn1SequenceParser)((Asn1TaggedObjectParser)this.nextObject).GetObjectParser(16, false);
				this.nextObject = null;
				return OriginatorInfo.GetInstance(asn1SequenceParser.ToAsn1Object());
			}
			return null;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00044E18 File Offset: 0x00044E18
		public Asn1SetParser GetRecipientInfos()
		{
			if (!this.originatorInfoCalled)
			{
				this.GetOriginatorInfo();
			}
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			Asn1SetParser result = (Asn1SetParser)this.nextObject;
			this.nextObject = null;
			return result;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00044E6C File Offset: 0x00044E6C
		public AlgorithmIdentifier GetMacAlgorithm()
		{
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			if (this.nextObject != null)
			{
				Asn1SequenceParser asn1SequenceParser = (Asn1SequenceParser)this.nextObject;
				this.nextObject = null;
				return AlgorithmIdentifier.GetInstance(asn1SequenceParser.ToAsn1Object());
			}
			return null;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00044EC4 File Offset: 0x00044EC4
		public AlgorithmIdentifier GetDigestAlgorithm()
		{
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			if (this.nextObject is Asn1TaggedObjectParser)
			{
				AlgorithmIdentifier instance = AlgorithmIdentifier.GetInstance((Asn1TaggedObject)this.nextObject.ToAsn1Object(), false);
				this.nextObject = null;
				return instance;
			}
			return null;
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00044F24 File Offset: 0x00044F24
		public ContentInfoParser GetEnapsulatedContentInfo()
		{
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			if (this.nextObject != null)
			{
				Asn1SequenceParser asn1SequenceParser = (Asn1SequenceParser)this.nextObject;
				this.nextObject = null;
				return new ContentInfoParser(asn1SequenceParser);
			}
			return null;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00044F78 File Offset: 0x00044F78
		public Asn1SetParser GetAuthAttrs()
		{
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			if (this.nextObject is Asn1TaggedObjectParser)
			{
				IAsn1Convertible asn1Convertible = this.nextObject;
				this.nextObject = null;
				return (Asn1SetParser)((Asn1TaggedObjectParser)asn1Convertible).GetObjectParser(17, false);
			}
			return null;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00044FD8 File Offset: 0x00044FD8
		public Asn1OctetString GetMac()
		{
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			IAsn1Convertible asn1Convertible = this.nextObject;
			this.nextObject = null;
			return Asn1OctetString.GetInstance(asn1Convertible.ToAsn1Object());
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00045020 File Offset: 0x00045020
		public Asn1SetParser GetUnauthAttrs()
		{
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			if (this.nextObject != null)
			{
				IAsn1Convertible asn1Convertible = this.nextObject;
				this.nextObject = null;
				return (Asn1SetParser)((Asn1TaggedObjectParser)asn1Convertible).GetObjectParser(17, false);
			}
			return null;
		}

		// Token: 0x040006B0 RID: 1712
		private Asn1SequenceParser seq;

		// Token: 0x040006B1 RID: 1713
		private DerInteger version;

		// Token: 0x040006B2 RID: 1714
		private IAsn1Convertible nextObject;

		// Token: 0x040006B3 RID: 1715
		private bool originatorInfoCalled;
	}
}
