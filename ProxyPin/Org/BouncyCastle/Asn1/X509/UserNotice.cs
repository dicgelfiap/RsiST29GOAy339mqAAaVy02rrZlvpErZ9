using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000217 RID: 535
	public class UserNotice : Asn1Encodable
	{
		// Token: 0x06001132 RID: 4402 RVA: 0x00062694 File Offset: 0x00062694
		public UserNotice(NoticeReference noticeRef, DisplayText explicitText)
		{
			this.noticeRef = noticeRef;
			this.explicitText = explicitText;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x000626AC File Offset: 0x000626AC
		public UserNotice(NoticeReference noticeRef, string str) : this(noticeRef, new DisplayText(str))
		{
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x000626BC File Offset: 0x000626BC
		[Obsolete("Use GetInstance() instead")]
		public UserNotice(Asn1Sequence seq)
		{
			if (seq.Count == 2)
			{
				this.noticeRef = NoticeReference.GetInstance(seq[0]);
				this.explicitText = DisplayText.GetInstance(seq[1]);
				return;
			}
			if (seq.Count == 1)
			{
				if (seq[0].ToAsn1Object() is Asn1Sequence)
				{
					this.noticeRef = NoticeReference.GetInstance(seq[0]);
					this.explicitText = null;
					return;
				}
				this.noticeRef = null;
				this.explicitText = DisplayText.GetInstance(seq[0]);
				return;
			}
			else
			{
				if (seq.Count == 0)
				{
					this.noticeRef = null;
					this.explicitText = null;
					return;
				}
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00062790 File Offset: 0x00062790
		public static UserNotice GetInstance(object obj)
		{
			if (obj is UserNotice)
			{
				return (UserNotice)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new UserNotice(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x000627B8 File Offset: 0x000627B8
		public virtual NoticeReference NoticeRef
		{
			get
			{
				return this.noticeRef;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x000627C0 File Offset: 0x000627C0
		public virtual DisplayText ExplicitText
		{
			get
			{
				return this.explicitText;
			}
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x000627C8 File Offset: 0x000627C8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.noticeRef,
				this.explicitText
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000C5A RID: 3162
		private readonly NoticeReference noticeRef;

		// Token: 0x04000C5B RID: 3163
		private readonly DisplayText explicitText;
	}
}
