using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000100 RID: 256
	public class AuthEnvelopedDataParser
	{
		// Token: 0x06000959 RID: 2393 RVA: 0x00045314 File Offset: 0x00045314
		public AuthEnvelopedDataParser(Asn1SequenceParser seq)
		{
			this.seq = seq;
			this.version = (DerInteger)seq.ReadObject();
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x00045334 File Offset: 0x00045334
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0004533C File Offset: 0x0004533C
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

		// Token: 0x0600095C RID: 2396 RVA: 0x000453C4 File Offset: 0x000453C4
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

		// Token: 0x0600095D RID: 2397 RVA: 0x00045418 File Offset: 0x00045418
		public EncryptedContentInfoParser GetAuthEncryptedContentInfo()
		{
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			if (this.nextObject != null)
			{
				Asn1SequenceParser asn1SequenceParser = (Asn1SequenceParser)this.nextObject;
				this.nextObject = null;
				return new EncryptedContentInfoParser(asn1SequenceParser);
			}
			return null;
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0004546C File Offset: 0x0004546C
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

		// Token: 0x0600095F RID: 2399 RVA: 0x000454CC File Offset: 0x000454CC
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

		// Token: 0x06000960 RID: 2400 RVA: 0x00045514 File Offset: 0x00045514
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

		// Token: 0x040006BB RID: 1723
		private Asn1SequenceParser seq;

		// Token: 0x040006BC RID: 1724
		private DerInteger version;

		// Token: 0x040006BD RID: 1725
		private IAsn1Convertible nextObject;

		// Token: 0x040006BE RID: 1726
		private bool originatorInfoCalled;
	}
}
