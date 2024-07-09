using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200011F RID: 287
	public class ScvpReqRes : Asn1Encodable
	{
		// Token: 0x06000A3D RID: 2621 RVA: 0x00047B64 File Offset: 0x00047B64
		public static ScvpReqRes GetInstance(object obj)
		{
			if (obj is ScvpReqRes)
			{
				return (ScvpReqRes)obj;
			}
			if (obj != null)
			{
				return new ScvpReqRes(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00047B8C File Offset: 0x00047B8C
		private ScvpReqRes(Asn1Sequence seq)
		{
			if (seq[0] is Asn1TaggedObject)
			{
				this.request = ContentInfo.GetInstance(Asn1TaggedObject.GetInstance(seq[0]), true);
				this.response = ContentInfo.GetInstance(seq[1]);
				return;
			}
			this.request = null;
			this.response = ContentInfo.GetInstance(seq[0]);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00047BF8 File Offset: 0x00047BF8
		public ScvpReqRes(ContentInfo response) : this(null, response)
		{
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00047C04 File Offset: 0x00047C04
		public ScvpReqRes(ContentInfo request, ContentInfo response)
		{
			this.request = request;
			this.response = response;
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x00047C1C File Offset: 0x00047C1C
		public virtual ContentInfo Request
		{
			get
			{
				return this.request;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x00047C24 File Offset: 0x00047C24
		public virtual ContentInfo Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00047C2C File Offset: 0x00047C2C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptionalTagged(true, 0, this.request);
			asn1EncodableVector.Add(this.response);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400071D RID: 1821
		private readonly ContentInfo request;

		// Token: 0x0400071E RID: 1822
		private readonly ContentInfo response;
	}
}
