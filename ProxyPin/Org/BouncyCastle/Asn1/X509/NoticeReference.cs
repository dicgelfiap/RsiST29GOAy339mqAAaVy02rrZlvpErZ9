using System;
using System.Collections;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000203 RID: 515
	public class NoticeReference : Asn1Encodable
	{
		// Token: 0x0600109D RID: 4253 RVA: 0x000609E4 File Offset: 0x000609E4
		private static Asn1EncodableVector ConvertVector(IList numbers)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			foreach (object obj in numbers)
			{
				DerInteger element;
				if (obj is BigInteger)
				{
					element = new DerInteger((BigInteger)obj);
				}
				else
				{
					if (!(obj is int))
					{
						throw new ArgumentException();
					}
					element = new DerInteger((int)obj);
				}
				asn1EncodableVector.Add(element);
			}
			return asn1EncodableVector;
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00060A88 File Offset: 0x00060A88
		public NoticeReference(string organization, IList numbers) : this(organization, NoticeReference.ConvertVector(numbers))
		{
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00060A98 File Offset: 0x00060A98
		public NoticeReference(string organization, Asn1EncodableVector noticeNumbers) : this(new DisplayText(organization), noticeNumbers)
		{
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00060AA8 File Offset: 0x00060AA8
		public NoticeReference(DisplayText organization, Asn1EncodableVector noticeNumbers)
		{
			this.organization = organization;
			this.noticeNumbers = new DerSequence(noticeNumbers);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00060AC4 File Offset: 0x00060AC4
		private NoticeReference(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.organization = DisplayText.GetInstance(seq[0]);
			this.noticeNumbers = Asn1Sequence.GetInstance(seq[1]);
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00060B2C File Offset: 0x00060B2C
		public static NoticeReference GetInstance(object obj)
		{
			if (obj is NoticeReference)
			{
				return (NoticeReference)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new NoticeReference(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x00060B54 File Offset: 0x00060B54
		public virtual DisplayText Organization
		{
			get
			{
				return this.organization;
			}
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00060B5C File Offset: 0x00060B5C
		public virtual DerInteger[] GetNoticeNumbers()
		{
			DerInteger[] array = new DerInteger[this.noticeNumbers.Count];
			for (int num = 0; num != this.noticeNumbers.Count; num++)
			{
				array[num] = DerInteger.GetInstance(this.noticeNumbers[num]);
			}
			return array;
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00060BB0 File Offset: 0x00060BB0
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.organization,
				this.noticeNumbers
			});
		}

		// Token: 0x04000C19 RID: 3097
		private readonly DisplayText organization;

		// Token: 0x04000C1A RID: 3098
		private readonly Asn1Sequence noticeNumbers;
	}
}
