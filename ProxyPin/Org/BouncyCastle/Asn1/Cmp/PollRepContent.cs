using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000EF RID: 239
	public class PollRepContent : Asn1Encodable
	{
		// Token: 0x060008D8 RID: 2264 RVA: 0x000436F8 File Offset: 0x000436F8
		private PollRepContent(Asn1Sequence seq)
		{
			this.certReqId = DerInteger.GetInstance(seq[0]);
			this.checkAfter = DerInteger.GetInstance(seq[1]);
			if (seq.Count > 2)
			{
				this.reason = PkiFreeText.GetInstance(seq[2]);
			}
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00043754 File Offset: 0x00043754
		public static PollRepContent GetInstance(object obj)
		{
			if (obj is PollRepContent)
			{
				return (PollRepContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PollRepContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x000437A8 File Offset: 0x000437A8
		public PollRepContent(DerInteger certReqId, DerInteger checkAfter)
		{
			this.certReqId = certReqId;
			this.checkAfter = checkAfter;
			this.reason = null;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x000437C8 File Offset: 0x000437C8
		public PollRepContent(DerInteger certReqId, DerInteger checkAfter, PkiFreeText reason)
		{
			this.certReqId = certReqId;
			this.checkAfter = checkAfter;
			this.reason = reason;
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x000437E8 File Offset: 0x000437E8
		public virtual DerInteger CertReqID
		{
			get
			{
				return this.certReqId;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x000437F0 File Offset: 0x000437F0
		public virtual DerInteger CheckAfter
		{
			get
			{
				return this.checkAfter;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x000437F8 File Offset: 0x000437F8
		public virtual PkiFreeText Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00043800 File Offset: 0x00043800
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certReqId,
				this.checkAfter
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.reason
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400068B RID: 1675
		private readonly DerInteger certReqId;

		// Token: 0x0400068C RID: 1676
		private readonly DerInteger checkAfter;

		// Token: 0x0400068D RID: 1677
		private readonly PkiFreeText reason;
	}
}
