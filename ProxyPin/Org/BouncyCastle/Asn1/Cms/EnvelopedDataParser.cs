using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200010B RID: 267
	public class EnvelopedDataParser
	{
		// Token: 0x0600099A RID: 2458 RVA: 0x00045FE8 File Offset: 0x00045FE8
		public EnvelopedDataParser(Asn1SequenceParser seq)
		{
			this._seq = seq;
			this._version = (DerInteger)seq.ReadObject();
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x00046008 File Offset: 0x00046008
		public DerInteger Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00046010 File Offset: 0x00046010
		public OriginatorInfo GetOriginatorInfo()
		{
			this._originatorInfoCalled = true;
			if (this._nextObject == null)
			{
				this._nextObject = this._seq.ReadObject();
			}
			if (this._nextObject is Asn1TaggedObjectParser && ((Asn1TaggedObjectParser)this._nextObject).TagNo == 0)
			{
				Asn1SequenceParser asn1SequenceParser = (Asn1SequenceParser)((Asn1TaggedObjectParser)this._nextObject).GetObjectParser(16, false);
				this._nextObject = null;
				return OriginatorInfo.GetInstance(asn1SequenceParser.ToAsn1Object());
			}
			return null;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00046098 File Offset: 0x00046098
		public Asn1SetParser GetRecipientInfos()
		{
			if (!this._originatorInfoCalled)
			{
				this.GetOriginatorInfo();
			}
			if (this._nextObject == null)
			{
				this._nextObject = this._seq.ReadObject();
			}
			Asn1SetParser result = (Asn1SetParser)this._nextObject;
			this._nextObject = null;
			return result;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x000460EC File Offset: 0x000460EC
		public EncryptedContentInfoParser GetEncryptedContentInfo()
		{
			if (this._nextObject == null)
			{
				this._nextObject = this._seq.ReadObject();
			}
			if (this._nextObject != null)
			{
				Asn1SequenceParser seq = (Asn1SequenceParser)this._nextObject;
				this._nextObject = null;
				return new EncryptedContentInfoParser(seq);
			}
			return null;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00046140 File Offset: 0x00046140
		public Asn1SetParser GetUnprotectedAttrs()
		{
			if (this._nextObject == null)
			{
				this._nextObject = this._seq.ReadObject();
			}
			if (this._nextObject != null)
			{
				IAsn1Convertible nextObject = this._nextObject;
				this._nextObject = null;
				return (Asn1SetParser)((Asn1TaggedObjectParser)nextObject).GetObjectParser(17, false);
			}
			return null;
		}

		// Token: 0x040006E9 RID: 1769
		private Asn1SequenceParser _seq;

		// Token: 0x040006EA RID: 1770
		private DerInteger _version;

		// Token: 0x040006EB RID: 1771
		private IAsn1Convertible _nextObject;

		// Token: 0x040006EC RID: 1772
		private bool _originatorInfoCalled;
	}
}
