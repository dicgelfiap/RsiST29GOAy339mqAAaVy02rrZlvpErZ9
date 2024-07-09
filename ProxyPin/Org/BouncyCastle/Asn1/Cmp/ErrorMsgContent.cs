using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000D9 RID: 217
	public class ErrorMsgContent : Asn1Encodable
	{
		// Token: 0x0600082F RID: 2095 RVA: 0x00041888 File Offset: 0x00041888
		private ErrorMsgContent(Asn1Sequence seq)
		{
			this.pkiStatusInfo = PkiStatusInfo.GetInstance(seq[0]);
			for (int i = 1; i < seq.Count; i++)
			{
				Asn1Encodable asn1Encodable = seq[i];
				if (asn1Encodable is DerInteger)
				{
					this.errorCode = DerInteger.GetInstance(asn1Encodable);
				}
				else
				{
					this.errorDetails = PkiFreeText.GetInstance(asn1Encodable);
				}
			}
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x000418F8 File Offset: 0x000418F8
		public static ErrorMsgContent GetInstance(object obj)
		{
			if (obj is ErrorMsgContent)
			{
				return (ErrorMsgContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ErrorMsgContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0004194C File Offset: 0x0004194C
		public ErrorMsgContent(PkiStatusInfo pkiStatusInfo) : this(pkiStatusInfo, null, null)
		{
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00041958 File Offset: 0x00041958
		public ErrorMsgContent(PkiStatusInfo pkiStatusInfo, DerInteger errorCode, PkiFreeText errorDetails)
		{
			if (pkiStatusInfo == null)
			{
				throw new ArgumentNullException("pkiStatusInfo");
			}
			this.pkiStatusInfo = pkiStatusInfo;
			this.errorCode = errorCode;
			this.errorDetails = errorDetails;
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x00041988 File Offset: 0x00041988
		public virtual PkiStatusInfo PkiStatusInfo
		{
			get
			{
				return this.pkiStatusInfo;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x00041990 File Offset: 0x00041990
		public virtual DerInteger ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x00041998 File Offset: 0x00041998
		public virtual PkiFreeText ErrorDetails
		{
			get
			{
				return this.errorDetails;
			}
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x000419A0 File Offset: 0x000419A0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.pkiStatusInfo
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.errorCode,
				this.errorDetails
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400060A RID: 1546
		private readonly PkiStatusInfo pkiStatusInfo;

		// Token: 0x0400060B RID: 1547
		private readonly DerInteger errorCode;

		// Token: 0x0400060C RID: 1548
		private readonly PkiFreeText errorDetails;
	}
}
