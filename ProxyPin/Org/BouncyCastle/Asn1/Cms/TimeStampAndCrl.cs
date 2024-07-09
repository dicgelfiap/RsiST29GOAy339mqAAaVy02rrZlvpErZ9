using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000125 RID: 293
	public class TimeStampAndCrl : Asn1Encodable
	{
		// Token: 0x06000A73 RID: 2675 RVA: 0x00048948 File Offset: 0x00048948
		public TimeStampAndCrl(ContentInfo timeStamp)
		{
			this.timeStamp = timeStamp;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00048958 File Offset: 0x00048958
		private TimeStampAndCrl(Asn1Sequence seq)
		{
			this.timeStamp = ContentInfo.GetInstance(seq[0]);
			if (seq.Count == 2)
			{
				this.crl = CertificateList.GetInstance(seq[1]);
			}
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x000489A0 File Offset: 0x000489A0
		public static TimeStampAndCrl GetInstance(object obj)
		{
			if (obj is TimeStampAndCrl)
			{
				return (TimeStampAndCrl)obj;
			}
			if (obj != null)
			{
				return new TimeStampAndCrl(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x000489C8 File Offset: 0x000489C8
		public virtual ContentInfo TimeStampToken
		{
			get
			{
				return this.timeStamp;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x000489D0 File Offset: 0x000489D0
		public virtual CertificateList Crl
		{
			get
			{
				return this.crl;
			}
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x000489D8 File Offset: 0x000489D8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.timeStamp
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.crl
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000739 RID: 1849
		private ContentInfo timeStamp;

		// Token: 0x0400073A RID: 1850
		private CertificateList crl;
	}
}
