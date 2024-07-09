using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x0200019E RID: 414
	public class TbsRequest : Asn1Encodable
	{
		// Token: 0x06000D9B RID: 3483 RVA: 0x00054914 File Offset: 0x00054914
		public static TbsRequest GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return TbsRequest.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00054924 File Offset: 0x00054924
		public static TbsRequest GetInstance(object obj)
		{
			if (obj == null || obj is TbsRequest)
			{
				return (TbsRequest)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new TbsRequest((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00054980 File Offset: 0x00054980
		public TbsRequest(GeneralName requestorName, Asn1Sequence requestList, X509Extensions requestExtensions)
		{
			this.version = TbsRequest.V1;
			this.requestorName = requestorName;
			this.requestList = requestList;
			this.requestExtensions = requestExtensions;
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x000549A8 File Offset: 0x000549A8
		private TbsRequest(Asn1Sequence seq)
		{
			int num = 0;
			Asn1Encodable asn1Encodable = seq[0];
			if (asn1Encodable is Asn1TaggedObject)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)asn1Encodable;
				if (asn1TaggedObject.TagNo == 0)
				{
					this.versionSet = true;
					this.version = DerInteger.GetInstance(asn1TaggedObject, true);
					num++;
				}
				else
				{
					this.version = TbsRequest.V1;
				}
			}
			else
			{
				this.version = TbsRequest.V1;
			}
			if (seq[num] is Asn1TaggedObject)
			{
				this.requestorName = GeneralName.GetInstance((Asn1TaggedObject)seq[num++], true);
			}
			this.requestList = (Asn1Sequence)seq[num++];
			if (seq.Count == num + 1)
			{
				this.requestExtensions = X509Extensions.GetInstance((Asn1TaggedObject)seq[num], true);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x00054A88 File Offset: 0x00054A88
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x00054A90 File Offset: 0x00054A90
		public GeneralName RequestorName
		{
			get
			{
				return this.requestorName;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x00054A98 File Offset: 0x00054A98
		public Asn1Sequence RequestList
		{
			get
			{
				return this.requestList;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x00054AA0 File Offset: 0x00054AA0
		public X509Extensions RequestExtensions
		{
			get
			{
				return this.requestExtensions;
			}
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00054AA8 File Offset: 0x00054AA8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			if (!this.version.Equals(TbsRequest.V1) || this.versionSet)
			{
				asn1EncodableVector.Add(new DerTaggedObject(true, 0, this.version));
			}
			asn1EncodableVector.AddOptionalTagged(true, 1, this.requestorName);
			asn1EncodableVector.Add(this.requestList);
			asn1EncodableVector.AddOptionalTagged(true, 2, this.requestExtensions);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040009B6 RID: 2486
		private static readonly DerInteger V1 = new DerInteger(0);

		// Token: 0x040009B7 RID: 2487
		private readonly DerInteger version;

		// Token: 0x040009B8 RID: 2488
		private readonly GeneralName requestorName;

		// Token: 0x040009B9 RID: 2489
		private readonly Asn1Sequence requestList;

		// Token: 0x040009BA RID: 2490
		private readonly X509Extensions requestExtensions;

		// Token: 0x040009BB RID: 2491
		private bool versionSet;
	}
}
