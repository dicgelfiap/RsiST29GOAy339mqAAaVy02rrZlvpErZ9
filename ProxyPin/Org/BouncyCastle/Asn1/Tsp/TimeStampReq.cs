using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Tsp
{
	// Token: 0x020001CC RID: 460
	public class TimeStampReq : Asn1Encodable
	{
		// Token: 0x06000EDA RID: 3802 RVA: 0x00059864 File Offset: 0x00059864
		public static TimeStampReq GetInstance(object obj)
		{
			if (obj is TimeStampReq)
			{
				return (TimeStampReq)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new TimeStampReq(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0005988C File Offset: 0x0005988C
		private TimeStampReq(Asn1Sequence seq)
		{
			int count = seq.Count;
			int num = 0;
			this.version = DerInteger.GetInstance(seq[num++]);
			this.messageImprint = MessageImprint.GetInstance(seq[num++]);
			for (int i = num; i < count; i++)
			{
				if (seq[i] is DerObjectIdentifier)
				{
					this.tsaPolicy = DerObjectIdentifier.GetInstance(seq[i]);
				}
				else if (seq[i] is DerInteger)
				{
					this.nonce = DerInteger.GetInstance(seq[i]);
				}
				else if (seq[i] is DerBoolean)
				{
					this.certReq = DerBoolean.GetInstance(seq[i]);
				}
				else if (seq[i] is Asn1TaggedObject)
				{
					Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[i];
					if (asn1TaggedObject.TagNo == 0)
					{
						this.extensions = X509Extensions.GetInstance(asn1TaggedObject, false);
					}
				}
			}
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x00059998 File Offset: 0x00059998
		public TimeStampReq(MessageImprint messageImprint, DerObjectIdentifier tsaPolicy, DerInteger nonce, DerBoolean certReq, X509Extensions extensions)
		{
			this.version = new DerInteger(1);
			this.messageImprint = messageImprint;
			this.tsaPolicy = tsaPolicy;
			this.nonce = nonce;
			this.certReq = certReq;
			this.extensions = extensions;
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x000599D4 File Offset: 0x000599D4
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000EDE RID: 3806 RVA: 0x000599DC File Offset: 0x000599DC
		public MessageImprint MessageImprint
		{
			get
			{
				return this.messageImprint;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x000599E4 File Offset: 0x000599E4
		public DerObjectIdentifier ReqPolicy
		{
			get
			{
				return this.tsaPolicy;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x000599EC File Offset: 0x000599EC
		public DerInteger Nonce
		{
			get
			{
				return this.nonce;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x000599F4 File Offset: 0x000599F4
		public DerBoolean CertReq
		{
			get
			{
				return this.certReq;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x000599FC File Offset: 0x000599FC
		public X509Extensions Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00059A04 File Offset: 0x00059A04
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.messageImprint
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.tsaPolicy,
				this.nonce
			});
			if (this.certReq != null && this.certReq.IsTrue)
			{
				asn1EncodableVector.Add(this.certReq);
			}
			asn1EncodableVector.AddOptionalTagged(false, 0, this.extensions);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000B2D RID: 2861
		private readonly DerInteger version;

		// Token: 0x04000B2E RID: 2862
		private readonly MessageImprint messageImprint;

		// Token: 0x04000B2F RID: 2863
		private readonly DerObjectIdentifier tsaPolicy;

		// Token: 0x04000B30 RID: 2864
		private readonly DerInteger nonce;

		// Token: 0x04000B31 RID: 2865
		private readonly DerBoolean certReq;

		// Token: 0x04000B32 RID: 2866
		private readonly X509Extensions extensions;
	}
}
