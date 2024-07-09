using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x02000192 RID: 402
	public class OcspRequest : Asn1Encodable
	{
		// Token: 0x06000D38 RID: 3384 RVA: 0x0005377C File Offset: 0x0005377C
		public static OcspRequest GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return OcspRequest.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0005378C File Offset: 0x0005378C
		public static OcspRequest GetInstance(object obj)
		{
			if (obj == null || obj is OcspRequest)
			{
				return (OcspRequest)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OcspRequest((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x000537E8 File Offset: 0x000537E8
		public OcspRequest(TbsRequest tbsRequest, Signature optionalSignature)
		{
			if (tbsRequest == null)
			{
				throw new ArgumentNullException("tbsRequest");
			}
			this.tbsRequest = tbsRequest;
			this.optionalSignature = optionalSignature;
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00053810 File Offset: 0x00053810
		private OcspRequest(Asn1Sequence seq)
		{
			this.tbsRequest = TbsRequest.GetInstance(seq[0]);
			if (seq.Count == 2)
			{
				this.optionalSignature = Signature.GetInstance((Asn1TaggedObject)seq[1], true);
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x00053860 File Offset: 0x00053860
		public TbsRequest TbsRequest
		{
			get
			{
				return this.tbsRequest;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x00053868 File Offset: 0x00053868
		public Signature OptionalSignature
		{
			get
			{
				return this.optionalSignature;
			}
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00053870 File Offset: 0x00053870
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.tbsRequest
			});
			asn1EncodableVector.AddOptionalTagged(true, 0, this.optionalSignature);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000991 RID: 2449
		private readonly TbsRequest tbsRequest;

		// Token: 0x04000992 RID: 2450
		private readonly Signature optionalSignature;
	}
}
