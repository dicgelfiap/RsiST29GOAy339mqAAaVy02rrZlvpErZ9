using System;
using System.Collections;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Tsp
{
	// Token: 0x020001CE RID: 462
	public class TstInfo : Asn1Encodable
	{
		// Token: 0x06000EEA RID: 3818 RVA: 0x00059B88 File Offset: 0x00059B88
		public static TstInfo GetInstance(object obj)
		{
			if (obj is TstInfo)
			{
				return (TstInfo)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new TstInfo(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x00059BB0 File Offset: 0x00059BB0
		private TstInfo(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.version = DerInteger.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			this.tsaPolicyId = DerObjectIdentifier.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			this.messageImprint = MessageImprint.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			this.serialNumber = DerInteger.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			this.genTime = DerGeneralizedTime.GetInstance(enumerator.Current);
			this.ordering = DerBoolean.False;
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Asn1Object asn1Object = (Asn1Object)obj;
				if (asn1Object is Asn1TaggedObject)
				{
					DerTaggedObject derTaggedObject = (DerTaggedObject)asn1Object;
					switch (derTaggedObject.TagNo)
					{
					case 0:
						this.tsa = GeneralName.GetInstance(derTaggedObject, true);
						break;
					case 1:
						this.extensions = X509Extensions.GetInstance(derTaggedObject, false);
						break;
					default:
						throw new ArgumentException("Unknown tag value " + derTaggedObject.TagNo);
					}
				}
				if (asn1Object is DerSequence)
				{
					this.accuracy = Accuracy.GetInstance(asn1Object);
				}
				if (asn1Object is DerBoolean)
				{
					this.ordering = DerBoolean.GetInstance(asn1Object);
				}
				if (asn1Object is DerInteger)
				{
					this.nonce = DerInteger.GetInstance(asn1Object);
				}
			}
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00059D20 File Offset: 0x00059D20
		public TstInfo(DerObjectIdentifier tsaPolicyId, MessageImprint messageImprint, DerInteger serialNumber, DerGeneralizedTime genTime, Accuracy accuracy, DerBoolean ordering, DerInteger nonce, GeneralName tsa, X509Extensions extensions)
		{
			this.version = new DerInteger(1);
			this.tsaPolicyId = tsaPolicyId;
			this.messageImprint = messageImprint;
			this.serialNumber = serialNumber;
			this.genTime = genTime;
			this.accuracy = accuracy;
			this.ordering = ordering;
			this.nonce = nonce;
			this.tsa = tsa;
			this.extensions = extensions;
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x00059D88 File Offset: 0x00059D88
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x00059D90 File Offset: 0x00059D90
		public MessageImprint MessageImprint
		{
			get
			{
				return this.messageImprint;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x00059D98 File Offset: 0x00059D98
		public DerObjectIdentifier Policy
		{
			get
			{
				return this.tsaPolicyId;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x00059DA0 File Offset: 0x00059DA0
		public DerInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x00059DA8 File Offset: 0x00059DA8
		public Accuracy Accuracy
		{
			get
			{
				return this.accuracy;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x00059DB0 File Offset: 0x00059DB0
		public DerGeneralizedTime GenTime
		{
			get
			{
				return this.genTime;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x00059DB8 File Offset: 0x00059DB8
		public DerBoolean Ordering
		{
			get
			{
				return this.ordering;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x00059DC0 File Offset: 0x00059DC0
		public DerInteger Nonce
		{
			get
			{
				return this.nonce;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x00059DC8 File Offset: 0x00059DC8
		public GeneralName Tsa
		{
			get
			{
				return this.tsa;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x00059DD0 File Offset: 0x00059DD0
		public X509Extensions Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00059DD8 File Offset: 0x00059DD8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.tsaPolicyId,
				this.messageImprint,
				this.serialNumber,
				this.genTime
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.accuracy
			});
			if (this.ordering != null && this.ordering.IsTrue)
			{
				asn1EncodableVector.Add(this.ordering);
			}
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.nonce
			});
			asn1EncodableVector.AddOptionalTagged(true, 0, this.tsa);
			asn1EncodableVector.AddOptionalTagged(false, 1, this.extensions);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000B35 RID: 2869
		private readonly DerInteger version;

		// Token: 0x04000B36 RID: 2870
		private readonly DerObjectIdentifier tsaPolicyId;

		// Token: 0x04000B37 RID: 2871
		private readonly MessageImprint messageImprint;

		// Token: 0x04000B38 RID: 2872
		private readonly DerInteger serialNumber;

		// Token: 0x04000B39 RID: 2873
		private readonly DerGeneralizedTime genTime;

		// Token: 0x04000B3A RID: 2874
		private readonly Accuracy accuracy;

		// Token: 0x04000B3B RID: 2875
		private readonly DerBoolean ordering;

		// Token: 0x04000B3C RID: 2876
		private readonly DerInteger nonce;

		// Token: 0x04000B3D RID: 2877
		private readonly GeneralName tsa;

		// Token: 0x04000B3E RID: 2878
		private readonly X509Extensions extensions;
	}
}
