using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200010A RID: 266
	public class EnvelopedData : Asn1Encodable
	{
		// Token: 0x0600098E RID: 2446 RVA: 0x00045D50 File Offset: 0x00045D50
		public EnvelopedData(OriginatorInfo originatorInfo, Asn1Set recipientInfos, EncryptedContentInfo encryptedContentInfo, Asn1Set unprotectedAttrs)
		{
			this.version = new DerInteger(EnvelopedData.CalculateVersion(originatorInfo, recipientInfos, unprotectedAttrs));
			this.originatorInfo = originatorInfo;
			this.recipientInfos = recipientInfos;
			this.encryptedContentInfo = encryptedContentInfo;
			this.unprotectedAttrs = unprotectedAttrs;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00045D8C File Offset: 0x00045D8C
		public EnvelopedData(OriginatorInfo originatorInfo, Asn1Set recipientInfos, EncryptedContentInfo encryptedContentInfo, Attributes unprotectedAttrs)
		{
			this.version = new DerInteger(EnvelopedData.CalculateVersion(originatorInfo, recipientInfos, Asn1Set.GetInstance(unprotectedAttrs)));
			this.originatorInfo = originatorInfo;
			this.recipientInfos = recipientInfos;
			this.encryptedContentInfo = encryptedContentInfo;
			this.unprotectedAttrs = Asn1Set.GetInstance(unprotectedAttrs);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00045DE0 File Offset: 0x00045DE0
		[Obsolete("Use 'GetInstance' instead")]
		public EnvelopedData(Asn1Sequence seq)
		{
			int num = 0;
			this.version = (DerInteger)seq[num++];
			object obj = seq[num++];
			if (obj is Asn1TaggedObject)
			{
				this.originatorInfo = OriginatorInfo.GetInstance((Asn1TaggedObject)obj, false);
				obj = seq[num++];
			}
			this.recipientInfos = Asn1Set.GetInstance(obj);
			this.encryptedContentInfo = EncryptedContentInfo.GetInstance(seq[num++]);
			if (seq.Count > num)
			{
				this.unprotectedAttrs = Asn1Set.GetInstance((Asn1TaggedObject)seq[num], false);
			}
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00045E8C File Offset: 0x00045E8C
		public static EnvelopedData GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return EnvelopedData.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00045E9C File Offset: 0x00045E9C
		public static EnvelopedData GetInstance(object obj)
		{
			if (obj is EnvelopedData)
			{
				return (EnvelopedData)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new EnvelopedData(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x00045EC4 File Offset: 0x00045EC4
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x00045ECC File Offset: 0x00045ECC
		public OriginatorInfo OriginatorInfo
		{
			get
			{
				return this.originatorInfo;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x00045ED4 File Offset: 0x00045ED4
		public Asn1Set RecipientInfos
		{
			get
			{
				return this.recipientInfos;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x00045EDC File Offset: 0x00045EDC
		public EncryptedContentInfo EncryptedContentInfo
		{
			get
			{
				return this.encryptedContentInfo;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x00045EE4 File Offset: 0x00045EE4
		public Asn1Set UnprotectedAttrs
		{
			get
			{
				return this.unprotectedAttrs;
			}
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00045EEC File Offset: 0x00045EEC
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
				this.encryptedContentInfo
			});
			asn1EncodableVector.AddOptionalTagged(false, 1, this.unprotectedAttrs);
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00045F64 File Offset: 0x00045F64
		public static int CalculateVersion(OriginatorInfo originatorInfo, Asn1Set recipientInfos, Asn1Set unprotectedAttrs)
		{
			if (originatorInfo != null || unprotectedAttrs != null)
			{
				return 2;
			}
			foreach (object o in recipientInfos)
			{
				RecipientInfo instance = RecipientInfo.GetInstance(o);
				if (instance.Version.IntValueExact != 0)
				{
					return 2;
				}
			}
			return 0;
		}

		// Token: 0x040006E4 RID: 1764
		private DerInteger version;

		// Token: 0x040006E5 RID: 1765
		private OriginatorInfo originatorInfo;

		// Token: 0x040006E6 RID: 1766
		private Asn1Set recipientInfos;

		// Token: 0x040006E7 RID: 1767
		private EncryptedContentInfo encryptedContentInfo;

		// Token: 0x040006E8 RID: 1768
		private Asn1Set unprotectedAttrs;
	}
}
