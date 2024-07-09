using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Tsp;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x020006BD RID: 1725
	public class TimeStampResponse
	{
		// Token: 0x06003C63 RID: 15459 RVA: 0x0014DC60 File Offset: 0x0014DC60
		public TimeStampResponse(TimeStampResp resp)
		{
			this.resp = resp;
			if (resp.TimeStampToken != null)
			{
				this.timeStampToken = new TimeStampToken(resp.TimeStampToken);
			}
		}

		// Token: 0x06003C64 RID: 15460 RVA: 0x0014DC8C File Offset: 0x0014DC8C
		public TimeStampResponse(byte[] resp) : this(TimeStampResponse.readTimeStampResp(new Asn1InputStream(resp)))
		{
		}

		// Token: 0x06003C65 RID: 15461 RVA: 0x0014DCA0 File Offset: 0x0014DCA0
		public TimeStampResponse(Stream input) : this(TimeStampResponse.readTimeStampResp(new Asn1InputStream(input)))
		{
		}

		// Token: 0x06003C66 RID: 15462 RVA: 0x0014DCB4 File Offset: 0x0014DCB4
		private static TimeStampResp readTimeStampResp(Asn1InputStream input)
		{
			TimeStampResp instance;
			try
			{
				instance = TimeStampResp.GetInstance(input.ReadObject());
			}
			catch (ArgumentException ex)
			{
				throw new TspException("malformed timestamp response: " + ex, ex);
			}
			catch (InvalidCastException ex2)
			{
				throw new TspException("malformed timestamp response: " + ex2, ex2);
			}
			return instance;
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06003C67 RID: 15463 RVA: 0x0014DD18 File Offset: 0x0014DD18
		public int Status
		{
			get
			{
				return this.resp.Status.Status.IntValue;
			}
		}

		// Token: 0x06003C68 RID: 15464 RVA: 0x0014DD30 File Offset: 0x0014DD30
		public string GetStatusString()
		{
			if (this.resp.Status.StatusString == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			PkiFreeText statusString = this.resp.Status.StatusString;
			for (int num = 0; num != statusString.Count; num++)
			{
				stringBuilder.Append(statusString[num].GetString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003C69 RID: 15465 RVA: 0x0014DD9C File Offset: 0x0014DD9C
		public PkiFailureInfo GetFailInfo()
		{
			if (this.resp.Status.FailInfo == null)
			{
				return null;
			}
			return new PkiFailureInfo(this.resp.Status.FailInfo);
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06003C6A RID: 15466 RVA: 0x0014DDCC File Offset: 0x0014DDCC
		public TimeStampToken TimeStampToken
		{
			get
			{
				return this.timeStampToken;
			}
		}

		// Token: 0x06003C6B RID: 15467 RVA: 0x0014DDD4 File Offset: 0x0014DDD4
		public void Validate(TimeStampRequest request)
		{
			TimeStampToken timeStampToken = this.TimeStampToken;
			if (timeStampToken != null)
			{
				TimeStampTokenInfo timeStampInfo = timeStampToken.TimeStampInfo;
				if (request.Nonce != null && !request.Nonce.Equals(timeStampInfo.Nonce))
				{
					throw new TspValidationException("response contains wrong nonce value.");
				}
				if (this.Status != 0 && this.Status != 1)
				{
					throw new TspValidationException("time stamp token found in failed request.");
				}
				if (!Arrays.ConstantTimeAreEqual(request.GetMessageImprintDigest(), timeStampInfo.GetMessageImprintDigest()))
				{
					throw new TspValidationException("response for different message imprint digest.");
				}
				if (!timeStampInfo.MessageImprintAlgOid.Equals(request.MessageImprintAlgOid))
				{
					throw new TspValidationException("response for different message imprint algorithm.");
				}
				Org.BouncyCastle.Asn1.Cms.Attribute attribute = timeStampToken.SignedAttributes[PkcsObjectIdentifiers.IdAASigningCertificate];
				Org.BouncyCastle.Asn1.Cms.Attribute attribute2 = timeStampToken.SignedAttributes[PkcsObjectIdentifiers.IdAASigningCertificateV2];
				if (attribute == null && attribute2 == null)
				{
					throw new TspValidationException("no signing certificate attribute present.");
				}
				if (attribute != null)
				{
				}
				if (request.ReqPolicy != null && !request.ReqPolicy.Equals(timeStampInfo.Policy))
				{
					throw new TspValidationException("TSA policy wrong for request.");
				}
			}
			else if (this.Status == 0 || this.Status == 1)
			{
				throw new TspValidationException("no time stamp token found and one expected.");
			}
		}

		// Token: 0x06003C6C RID: 15468 RVA: 0x0014DF18 File Offset: 0x0014DF18
		public byte[] GetEncoded()
		{
			return this.resp.GetEncoded();
		}

		// Token: 0x04001EB0 RID: 7856
		private TimeStampResp resp;

		// Token: 0x04001EB1 RID: 7857
		private TimeStampToken timeStampToken;
	}
}
