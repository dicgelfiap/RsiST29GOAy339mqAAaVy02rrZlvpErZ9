using System;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Cms;

namespace Org.BouncyCastle.Asn1.Tsp
{
	// Token: 0x020001CD RID: 461
	public class TimeStampResp : Asn1Encodable
	{
		// Token: 0x06000EE4 RID: 3812 RVA: 0x00059AA0 File Offset: 0x00059AA0
		public static TimeStampResp GetInstance(object obj)
		{
			if (obj is TimeStampResp)
			{
				return (TimeStampResp)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new TimeStampResp(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00059AC8 File Offset: 0x00059AC8
		private TimeStampResp(Asn1Sequence seq)
		{
			this.pkiStatusInfo = PkiStatusInfo.GetInstance(seq[0]);
			if (seq.Count > 1)
			{
				this.timeStampToken = ContentInfo.GetInstance(seq[1]);
			}
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x00059B10 File Offset: 0x00059B10
		public TimeStampResp(PkiStatusInfo pkiStatusInfo, ContentInfo timeStampToken)
		{
			this.pkiStatusInfo = pkiStatusInfo;
			this.timeStampToken = timeStampToken;
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x00059B28 File Offset: 0x00059B28
		public PkiStatusInfo Status
		{
			get
			{
				return this.pkiStatusInfo;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x00059B30 File Offset: 0x00059B30
		public ContentInfo TimeStampToken
		{
			get
			{
				return this.timeStampToken;
			}
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00059B38 File Offset: 0x00059B38
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.pkiStatusInfo
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.timeStampToken
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000B33 RID: 2867
		private readonly PkiStatusInfo pkiStatusInfo;

		// Token: 0x04000B34 RID: 2868
		private readonly ContentInfo timeStampToken;
	}
}
