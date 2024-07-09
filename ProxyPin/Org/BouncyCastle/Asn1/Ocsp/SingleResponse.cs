using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x0200019D RID: 413
	public class SingleResponse : Asn1Encodable
	{
		// Token: 0x06000D91 RID: 3473 RVA: 0x0005471C File Offset: 0x0005471C
		public SingleResponse(CertID certID, CertStatus certStatus, DerGeneralizedTime thisUpdate, DerGeneralizedTime nextUpdate, X509Extensions singleExtensions)
		{
			this.certID = certID;
			this.certStatus = certStatus;
			this.thisUpdate = thisUpdate;
			this.nextUpdate = nextUpdate;
			this.singleExtensions = singleExtensions;
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0005474C File Offset: 0x0005474C
		public SingleResponse(Asn1Sequence seq)
		{
			this.certID = CertID.GetInstance(seq[0]);
			this.certStatus = CertStatus.GetInstance(seq[1]);
			this.thisUpdate = (DerGeneralizedTime)seq[2];
			if (seq.Count > 4)
			{
				this.nextUpdate = DerGeneralizedTime.GetInstance((Asn1TaggedObject)seq[3], true);
				this.singleExtensions = X509Extensions.GetInstance((Asn1TaggedObject)seq[4], true);
				return;
			}
			if (seq.Count > 3)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[3];
				if (asn1TaggedObject.TagNo == 0)
				{
					this.nextUpdate = DerGeneralizedTime.GetInstance(asn1TaggedObject, true);
					return;
				}
				this.singleExtensions = X509Extensions.GetInstance(asn1TaggedObject, true);
			}
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00054818 File Offset: 0x00054818
		public static SingleResponse GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return SingleResponse.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00054828 File Offset: 0x00054828
		public static SingleResponse GetInstance(object obj)
		{
			if (obj == null || obj is SingleResponse)
			{
				return (SingleResponse)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SingleResponse((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x00054884 File Offset: 0x00054884
		public CertID CertId
		{
			get
			{
				return this.certID;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x0005488C File Offset: 0x0005488C
		public CertStatus CertStatus
		{
			get
			{
				return this.certStatus;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x00054894 File Offset: 0x00054894
		public DerGeneralizedTime ThisUpdate
		{
			get
			{
				return this.thisUpdate;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x0005489C File Offset: 0x0005489C
		public DerGeneralizedTime NextUpdate
		{
			get
			{
				return this.nextUpdate;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x000548A4 File Offset: 0x000548A4
		public X509Extensions SingleExtensions
		{
			get
			{
				return this.singleExtensions;
			}
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x000548AC File Offset: 0x000548AC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certID,
				this.certStatus,
				this.thisUpdate
			});
			asn1EncodableVector.AddOptionalTagged(true, 0, this.nextUpdate);
			asn1EncodableVector.AddOptionalTagged(true, 1, this.singleExtensions);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040009B1 RID: 2481
		private readonly CertID certID;

		// Token: 0x040009B2 RID: 2482
		private readonly CertStatus certStatus;

		// Token: 0x040009B3 RID: 2483
		private readonly DerGeneralizedTime thisUpdate;

		// Token: 0x040009B4 RID: 2484
		private readonly DerGeneralizedTime nextUpdate;

		// Token: 0x040009B5 RID: 2485
		private readonly X509Extensions singleExtensions;
	}
}
