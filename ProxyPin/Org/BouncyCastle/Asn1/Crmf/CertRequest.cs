using System;

namespace Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x0200012D RID: 301
	public class CertRequest : Asn1Encodable
	{
		// Token: 0x06000AA8 RID: 2728 RVA: 0x00049264 File Offset: 0x00049264
		private CertRequest(Asn1Sequence seq)
		{
			this.certReqId = DerInteger.GetInstance(seq[0]);
			this.certTemplate = CertTemplate.GetInstance(seq[1]);
			if (seq.Count > 2)
			{
				this.controls = Controls.GetInstance(seq[2]);
			}
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x000492C0 File Offset: 0x000492C0
		public static CertRequest GetInstance(object obj)
		{
			if (obj is CertRequest)
			{
				return (CertRequest)obj;
			}
			if (obj != null)
			{
				return new CertRequest(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x000492E8 File Offset: 0x000492E8
		public CertRequest(int certReqId, CertTemplate certTemplate, Controls controls) : this(new DerInteger(certReqId), certTemplate, controls)
		{
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000492F8 File Offset: 0x000492F8
		public CertRequest(DerInteger certReqId, CertTemplate certTemplate, Controls controls)
		{
			this.certReqId = certReqId;
			this.certTemplate = certTemplate;
			this.controls = controls;
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x00049318 File Offset: 0x00049318
		public virtual DerInteger CertReqID
		{
			get
			{
				return this.certReqId;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x00049320 File Offset: 0x00049320
		public virtual CertTemplate CertTemplate
		{
			get
			{
				return this.certTemplate;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x00049328 File Offset: 0x00049328
		public virtual Controls Controls
		{
			get
			{
				return this.controls;
			}
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00049330 File Offset: 0x00049330
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certReqId,
				this.certTemplate
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.controls
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400074F RID: 1871
		private readonly DerInteger certReqId;

		// Token: 0x04000750 RID: 1872
		private readonly CertTemplate certTemplate;

		// Token: 0x04000751 RID: 1873
		private readonly Controls controls;
	}
}
