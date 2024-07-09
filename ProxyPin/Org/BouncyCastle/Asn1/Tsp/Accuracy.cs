using System;

namespace Org.BouncyCastle.Asn1.Tsp
{
	// Token: 0x020001CA RID: 458
	public class Accuracy : Asn1Encodable
	{
		// Token: 0x06000ECD RID: 3789 RVA: 0x0005955C File Offset: 0x0005955C
		public Accuracy(DerInteger seconds, DerInteger millis, DerInteger micros)
		{
			if (millis != null)
			{
				int intValueExact = millis.IntValueExact;
				if (intValueExact < 1 || intValueExact > 999)
				{
					throw new ArgumentException("Invalid millis field : not in (1..999)");
				}
			}
			if (micros != null)
			{
				int intValueExact2 = micros.IntValueExact;
				if (intValueExact2 < 1 || intValueExact2 > 999)
				{
					throw new ArgumentException("Invalid micros field : not in (1..999)");
				}
			}
			this.seconds = seconds;
			this.millis = millis;
			this.micros = micros;
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x000595DC File Offset: 0x000595DC
		private Accuracy(Asn1Sequence seq)
		{
			for (int i = 0; i < seq.Count; i++)
			{
				if (seq[i] is DerInteger)
				{
					this.seconds = (DerInteger)seq[i];
				}
				else if (seq[i] is Asn1TaggedObject)
				{
					Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[i];
					switch (asn1TaggedObject.TagNo)
					{
					case 0:
					{
						this.millis = DerInteger.GetInstance(asn1TaggedObject, false);
						int intValueExact = this.millis.IntValueExact;
						if (intValueExact < 1 || intValueExact > 999)
						{
							throw new ArgumentException("Invalid millis field : not in (1..999)");
						}
						break;
					}
					case 1:
					{
						this.micros = DerInteger.GetInstance(asn1TaggedObject, false);
						int intValueExact2 = this.micros.IntValueExact;
						if (intValueExact2 < 1 || intValueExact2 > 999)
						{
							throw new ArgumentException("Invalid micros field : not in (1..999)");
						}
						break;
					}
					default:
						throw new ArgumentException("Invalid tag number");
					}
				}
			}
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x000596E4 File Offset: 0x000596E4
		public static Accuracy GetInstance(object obj)
		{
			if (obj is Accuracy)
			{
				return (Accuracy)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new Accuracy(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x0005970C File Offset: 0x0005970C
		public DerInteger Seconds
		{
			get
			{
				return this.seconds;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x00059714 File Offset: 0x00059714
		public DerInteger Millis
		{
			get
			{
				return this.millis;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x0005971C File Offset: 0x0005971C
		public DerInteger Micros
		{
			get
			{
				return this.micros;
			}
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00059724 File Offset: 0x00059724
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.seconds
			});
			asn1EncodableVector.AddOptionalTagged(false, 0, this.millis);
			asn1EncodableVector.AddOptionalTagged(false, 1, this.micros);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000B24 RID: 2852
		protected const int MinMillis = 1;

		// Token: 0x04000B25 RID: 2853
		protected const int MaxMillis = 999;

		// Token: 0x04000B26 RID: 2854
		protected const int MinMicros = 1;

		// Token: 0x04000B27 RID: 2855
		protected const int MaxMicros = 999;

		// Token: 0x04000B28 RID: 2856
		private readonly DerInteger seconds;

		// Token: 0x04000B29 RID: 2857
		private readonly DerInteger millis;

		// Token: 0x04000B2A RID: 2858
		private readonly DerInteger micros;
	}
}
