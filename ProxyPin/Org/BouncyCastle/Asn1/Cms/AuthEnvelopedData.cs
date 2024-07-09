using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x020000FF RID: 255
	public class AuthEnvelopedData : Asn1Encodable
	{
		// Token: 0x0600094D RID: 2381 RVA: 0x0004507C File Offset: 0x0004507C
		public AuthEnvelopedData(OriginatorInfo originatorInfo, Asn1Set recipientInfos, EncryptedContentInfo authEncryptedContentInfo, Asn1Set authAttrs, Asn1OctetString mac, Asn1Set unauthAttrs)
		{
			this.version = new DerInteger(0);
			this.originatorInfo = originatorInfo;
			this.recipientInfos = recipientInfos;
			this.authEncryptedContentInfo = authEncryptedContentInfo;
			this.authAttrs = authAttrs;
			this.mac = mac;
			this.unauthAttrs = unauthAttrs;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x000450CC File Offset: 0x000450CC
		private AuthEnvelopedData(Asn1Sequence seq)
		{
			int num = 0;
			Asn1Object asn1Object = seq[num++].ToAsn1Object();
			this.version = (DerInteger)asn1Object;
			asn1Object = seq[num++].ToAsn1Object();
			if (asn1Object is Asn1TaggedObject)
			{
				this.originatorInfo = OriginatorInfo.GetInstance((Asn1TaggedObject)asn1Object, false);
				asn1Object = seq[num++].ToAsn1Object();
			}
			this.recipientInfos = Asn1Set.GetInstance(asn1Object);
			asn1Object = seq[num++].ToAsn1Object();
			this.authEncryptedContentInfo = EncryptedContentInfo.GetInstance(asn1Object);
			asn1Object = seq[num++].ToAsn1Object();
			if (asn1Object is Asn1TaggedObject)
			{
				this.authAttrs = Asn1Set.GetInstance((Asn1TaggedObject)asn1Object, false);
				asn1Object = seq[num++].ToAsn1Object();
			}
			this.mac = Asn1OctetString.GetInstance(asn1Object);
			if (seq.Count > num)
			{
				asn1Object = seq[num++].ToAsn1Object();
				this.unauthAttrs = Asn1Set.GetInstance((Asn1TaggedObject)asn1Object, false);
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x000451E4 File Offset: 0x000451E4
		public static AuthEnvelopedData GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return AuthEnvelopedData.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x000451F4 File Offset: 0x000451F4
		public static AuthEnvelopedData GetInstance(object obj)
		{
			if (obj == null || obj is AuthEnvelopedData)
			{
				return (AuthEnvelopedData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AuthEnvelopedData((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid AuthEnvelopedData: " + Platform.GetTypeName(obj));
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x0004524C File Offset: 0x0004524C
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x00045254 File Offset: 0x00045254
		public OriginatorInfo OriginatorInfo
		{
			get
			{
				return this.originatorInfo;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x0004525C File Offset: 0x0004525C
		public Asn1Set RecipientInfos
		{
			get
			{
				return this.recipientInfos;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x00045264 File Offset: 0x00045264
		public EncryptedContentInfo AuthEncryptedContentInfo
		{
			get
			{
				return this.authEncryptedContentInfo;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x0004526C File Offset: 0x0004526C
		public Asn1Set AuthAttrs
		{
			get
			{
				return this.authAttrs;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x00045274 File Offset: 0x00045274
		public Asn1OctetString Mac
		{
			get
			{
				return this.mac;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x0004527C File Offset: 0x0004527C
		public Asn1Set UnauthAttrs
		{
			get
			{
				return this.unauthAttrs;
			}
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00045284 File Offset: 0x00045284
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version
			});
			asn1EncodableVector.AddOptionalTagged(false, 0, this.originatorInfo);
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.recipientInfos,
				this.authEncryptedContentInfo
			});
			asn1EncodableVector.AddOptionalTagged(false, 1, this.authAttrs);
			asn1EncodableVector.Add(this.mac);
			asn1EncodableVector.AddOptionalTagged(false, 2, this.unauthAttrs);
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x040006B4 RID: 1716
		private DerInteger version;

		// Token: 0x040006B5 RID: 1717
		private OriginatorInfo originatorInfo;

		// Token: 0x040006B6 RID: 1718
		private Asn1Set recipientInfos;

		// Token: 0x040006B7 RID: 1719
		private EncryptedContentInfo authEncryptedContentInfo;

		// Token: 0x040006B8 RID: 1720
		private Asn1Set authAttrs;

		// Token: 0x040006B9 RID: 1721
		private Asn1OctetString mac;

		// Token: 0x040006BA RID: 1722
		private Asn1Set unauthAttrs;
	}
}
