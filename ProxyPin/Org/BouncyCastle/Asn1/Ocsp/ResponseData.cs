using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x02000199 RID: 409
	public class ResponseData : Asn1Encodable
	{
		// Token: 0x06000D6B RID: 3435 RVA: 0x000540A8 File Offset: 0x000540A8
		public static ResponseData GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return ResponseData.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x000540B8 File Offset: 0x000540B8
		public static ResponseData GetInstance(object obj)
		{
			if (obj == null || obj is ResponseData)
			{
				return (ResponseData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ResponseData((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00054114 File Offset: 0x00054114
		public ResponseData(DerInteger version, ResponderID responderID, DerGeneralizedTime producedAt, Asn1Sequence responses, X509Extensions responseExtensions)
		{
			this.version = version;
			this.responderID = responderID;
			this.producedAt = producedAt;
			this.responses = responses;
			this.responseExtensions = responseExtensions;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00054144 File Offset: 0x00054144
		public ResponseData(ResponderID responderID, DerGeneralizedTime producedAt, Asn1Sequence responses, X509Extensions responseExtensions) : this(ResponseData.V1, responderID, producedAt, responses, responseExtensions)
		{
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00054158 File Offset: 0x00054158
		private ResponseData(Asn1Sequence seq)
		{
			int num = 0;
			Asn1Encodable asn1Encodable = seq[0];
			if (asn1Encodable is Asn1TaggedObject)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)asn1Encodable;
				if (asn1TaggedObject.TagNo == 0)
				{
					this.versionPresent = true;
					this.version = DerInteger.GetInstance(asn1TaggedObject, true);
					num++;
				}
				else
				{
					this.version = ResponseData.V1;
				}
			}
			else
			{
				this.version = ResponseData.V1;
			}
			this.responderID = ResponderID.GetInstance(seq[num++]);
			this.producedAt = (DerGeneralizedTime)seq[num++];
			this.responses = (Asn1Sequence)seq[num++];
			if (seq.Count > num)
			{
				this.responseExtensions = X509Extensions.GetInstance((Asn1TaggedObject)seq[num], true);
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x00054234 File Offset: 0x00054234
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x0005423C File Offset: 0x0005423C
		public ResponderID ResponderID
		{
			get
			{
				return this.responderID;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x00054244 File Offset: 0x00054244
		public DerGeneralizedTime ProducedAt
		{
			get
			{
				return this.producedAt;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x0005424C File Offset: 0x0005424C
		public Asn1Sequence Responses
		{
			get
			{
				return this.responses;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x00054254 File Offset: 0x00054254
		public X509Extensions ResponseExtensions
		{
			get
			{
				return this.responseExtensions;
			}
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0005425C File Offset: 0x0005425C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			if (this.versionPresent || !this.version.Equals(ResponseData.V1))
			{
				asn1EncodableVector.Add(new DerTaggedObject(true, 0, this.version));
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.responderID,
				this.producedAt,
				this.responses
			});
			asn1EncodableVector.AddOptionalTagged(true, 1, this.responseExtensions);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040009A3 RID: 2467
		private static readonly DerInteger V1 = new DerInteger(0);

		// Token: 0x040009A4 RID: 2468
		private readonly bool versionPresent;

		// Token: 0x040009A5 RID: 2469
		private readonly DerInteger version;

		// Token: 0x040009A6 RID: 2470
		private readonly ResponderID responderID;

		// Token: 0x040009A7 RID: 2471
		private readonly DerGeneralizedTime producedAt;

		// Token: 0x040009A8 RID: 2472
		private readonly Asn1Sequence responses;

		// Token: 0x040009A9 RID: 2473
		private readonly X509Extensions responseExtensions;
	}
}
