using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x020000FD RID: 253
	public class AuthenticatedData : Asn1Encodable
	{
		// Token: 0x06000934 RID: 2356 RVA: 0x00044944 File Offset: 0x00044944
		public AuthenticatedData(OriginatorInfo originatorInfo, Asn1Set recipientInfos, AlgorithmIdentifier macAlgorithm, AlgorithmIdentifier digestAlgorithm, ContentInfo encapsulatedContent, Asn1Set authAttrs, Asn1OctetString mac, Asn1Set unauthAttrs)
		{
			if ((digestAlgorithm != null || authAttrs != null) && (digestAlgorithm == null || authAttrs == null))
			{
				throw new ArgumentException("digestAlgorithm and authAttrs must be set together");
			}
			this.version = new DerInteger(AuthenticatedData.CalculateVersion(originatorInfo));
			this.originatorInfo = originatorInfo;
			this.macAlgorithm = macAlgorithm;
			this.digestAlgorithm = digestAlgorithm;
			this.recipientInfos = recipientInfos;
			this.encapsulatedContentInfo = encapsulatedContent;
			this.authAttrs = authAttrs;
			this.mac = mac;
			this.unauthAttrs = unauthAttrs;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x000449D0 File Offset: 0x000449D0
		private AuthenticatedData(Asn1Sequence seq)
		{
			int num = 0;
			this.version = (DerInteger)seq[num++];
			Asn1Encodable asn1Encodable = seq[num++];
			if (asn1Encodable is Asn1TaggedObject)
			{
				this.originatorInfo = OriginatorInfo.GetInstance((Asn1TaggedObject)asn1Encodable, false);
				asn1Encodable = seq[num++];
			}
			this.recipientInfos = Asn1Set.GetInstance(asn1Encodable);
			this.macAlgorithm = AlgorithmIdentifier.GetInstance(seq[num++]);
			asn1Encodable = seq[num++];
			if (asn1Encodable is Asn1TaggedObject)
			{
				this.digestAlgorithm = AlgorithmIdentifier.GetInstance((Asn1TaggedObject)asn1Encodable, false);
				asn1Encodable = seq[num++];
			}
			this.encapsulatedContentInfo = ContentInfo.GetInstance(asn1Encodable);
			asn1Encodable = seq[num++];
			if (asn1Encodable is Asn1TaggedObject)
			{
				this.authAttrs = Asn1Set.GetInstance((Asn1TaggedObject)asn1Encodable, false);
				asn1Encodable = seq[num++];
			}
			this.mac = Asn1OctetString.GetInstance(asn1Encodable);
			if (seq.Count > num)
			{
				this.unauthAttrs = Asn1Set.GetInstance((Asn1TaggedObject)seq[num], false);
			}
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00044AFC File Offset: 0x00044AFC
		public static AuthenticatedData GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return AuthenticatedData.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00044B0C File Offset: 0x00044B0C
		public static AuthenticatedData GetInstance(object obj)
		{
			if (obj == null || obj is AuthenticatedData)
			{
				return (AuthenticatedData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AuthenticatedData((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid AuthenticatedData: " + Platform.GetTypeName(obj));
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x00044B64 File Offset: 0x00044B64
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x00044B6C File Offset: 0x00044B6C
		public OriginatorInfo OriginatorInfo
		{
			get
			{
				return this.originatorInfo;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x00044B74 File Offset: 0x00044B74
		public Asn1Set RecipientInfos
		{
			get
			{
				return this.recipientInfos;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x00044B7C File Offset: 0x00044B7C
		public AlgorithmIdentifier MacAlgorithm
		{
			get
			{
				return this.macAlgorithm;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x00044B84 File Offset: 0x00044B84
		public AlgorithmIdentifier DigestAlgorithm
		{
			get
			{
				return this.digestAlgorithm;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x00044B8C File Offset: 0x00044B8C
		public ContentInfo EncapsulatedContentInfo
		{
			get
			{
				return this.encapsulatedContentInfo;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00044B94 File Offset: 0x00044B94
		public Asn1Set AuthAttrs
		{
			get
			{
				return this.authAttrs;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x00044B9C File Offset: 0x00044B9C
		public Asn1OctetString Mac
		{
			get
			{
				return this.mac;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00044BA4 File Offset: 0x00044BA4
		public Asn1Set UnauthAttrs
		{
			get
			{
				return this.unauthAttrs;
			}
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00044BAC File Offset: 0x00044BAC
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
				this.macAlgorithm
			});
			asn1EncodableVector.AddOptionalTagged(false, 1, this.digestAlgorithm);
			asn1EncodableVector.Add(this.encapsulatedContentInfo);
			asn1EncodableVector.AddOptionalTagged(false, 2, this.authAttrs);
			asn1EncodableVector.Add(this.mac);
			asn1EncodableVector.AddOptionalTagged(false, 3, this.unauthAttrs);
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00044C58 File Offset: 0x00044C58
		public static int CalculateVersion(OriginatorInfo origInfo)
		{
			if (origInfo == null)
			{
				return 0;
			}
			int result = 0;
			foreach (object obj in origInfo.Certificates)
			{
				if (obj is Asn1TaggedObject)
				{
					Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
					if (asn1TaggedObject.TagNo == 2)
					{
						result = 1;
					}
					else if (asn1TaggedObject.TagNo == 3)
					{
						result = 3;
						break;
					}
				}
			}
			foreach (object obj2 in origInfo.Crls)
			{
				if (obj2 is Asn1TaggedObject)
				{
					Asn1TaggedObject asn1TaggedObject2 = (Asn1TaggedObject)obj2;
					if (asn1TaggedObject2.TagNo == 1)
					{
						result = 3;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x040006A7 RID: 1703
		private DerInteger version;

		// Token: 0x040006A8 RID: 1704
		private OriginatorInfo originatorInfo;

		// Token: 0x040006A9 RID: 1705
		private Asn1Set recipientInfos;

		// Token: 0x040006AA RID: 1706
		private AlgorithmIdentifier macAlgorithm;

		// Token: 0x040006AB RID: 1707
		private AlgorithmIdentifier digestAlgorithm;

		// Token: 0x040006AC RID: 1708
		private ContentInfo encapsulatedContentInfo;

		// Token: 0x040006AD RID: 1709
		private Asn1Set authAttrs;

		// Token: 0x040006AE RID: 1710
		private Asn1OctetString mac;

		// Token: 0x040006AF RID: 1711
		private Asn1Set unauthAttrs;
	}
}
