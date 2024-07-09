using System;

namespace Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x0200012C RID: 300
	public class CertReqMsg : Asn1Encodable
	{
		// Token: 0x06000AA0 RID: 2720 RVA: 0x000490B0 File Offset: 0x000490B0
		private CertReqMsg(Asn1Sequence seq)
		{
			this.certReq = CertRequest.GetInstance(seq[0]);
			for (int i = 1; i < seq.Count; i++)
			{
				object obj = seq[i];
				if (obj is Asn1TaggedObject || obj is ProofOfPossession)
				{
					this.popo = ProofOfPossession.GetInstance(obj);
				}
				else
				{
					this.regInfo = Asn1Sequence.GetInstance(obj);
				}
			}
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00049128 File Offset: 0x00049128
		public static CertReqMsg GetInstance(object obj)
		{
			if (obj is CertReqMsg)
			{
				return (CertReqMsg)obj;
			}
			if (obj != null)
			{
				return new CertReqMsg(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00049150 File Offset: 0x00049150
		public static CertReqMsg GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return CertReqMsg.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00049160 File Offset: 0x00049160
		public CertReqMsg(CertRequest certReq, ProofOfPossession popo, AttributeTypeAndValue[] regInfo)
		{
			if (certReq == null)
			{
				throw new ArgumentNullException("certReq");
			}
			this.certReq = certReq;
			this.popo = popo;
			if (regInfo != null)
			{
				this.regInfo = new DerSequence(regInfo);
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x0004919C File Offset: 0x0004919C
		public virtual CertRequest CertReq
		{
			get
			{
				return this.certReq;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x000491A4 File Offset: 0x000491A4
		public virtual ProofOfPossession Popo
		{
			get
			{
				return this.popo;
			}
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x000491AC File Offset: 0x000491AC
		public virtual AttributeTypeAndValue[] GetRegInfo()
		{
			if (this.regInfo == null)
			{
				return null;
			}
			AttributeTypeAndValue[] array = new AttributeTypeAndValue[this.regInfo.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = AttributeTypeAndValue.GetInstance(this.regInfo[num]);
			}
			return array;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00049208 File Offset: 0x00049208
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certReq
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.popo,
				this.regInfo
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400074C RID: 1868
		private readonly CertRequest certReq;

		// Token: 0x0400074D RID: 1869
		private readonly ProofOfPossession popo;

		// Token: 0x0400074E RID: 1870
		private readonly Asn1Sequence regInfo;
	}
}
