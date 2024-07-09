using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Tsp;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities.Date;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x020006BE RID: 1726
	public class TimeStampResponseGenerator
	{
		// Token: 0x06003C6D RID: 15469 RVA: 0x0014DF28 File Offset: 0x0014DF28
		public TimeStampResponseGenerator(TimeStampTokenGenerator tokenGenerator, IList acceptedAlgorithms) : this(tokenGenerator, acceptedAlgorithms, null, null)
		{
		}

		// Token: 0x06003C6E RID: 15470 RVA: 0x0014DF34 File Offset: 0x0014DF34
		public TimeStampResponseGenerator(TimeStampTokenGenerator tokenGenerator, IList acceptedAlgorithms, IList acceptedPolicy) : this(tokenGenerator, acceptedAlgorithms, acceptedPolicy, null)
		{
		}

		// Token: 0x06003C6F RID: 15471 RVA: 0x0014DF40 File Offset: 0x0014DF40
		public TimeStampResponseGenerator(TimeStampTokenGenerator tokenGenerator, IList acceptedAlgorithms, IList acceptedPolicies, IList acceptedExtensions)
		{
			this.tokenGenerator = tokenGenerator;
			this.acceptedAlgorithms = acceptedAlgorithms;
			this.acceptedPolicies = acceptedPolicies;
			this.acceptedExtensions = acceptedExtensions;
			this.statusStrings = new Asn1EncodableVector();
		}

		// Token: 0x06003C70 RID: 15472 RVA: 0x0014DF70 File Offset: 0x0014DF70
		private void AddStatusString(string statusString)
		{
			this.statusStrings.Add(new DerUtf8String(statusString));
		}

		// Token: 0x06003C71 RID: 15473 RVA: 0x0014DF84 File Offset: 0x0014DF84
		private void SetFailInfoField(int field)
		{
			this.failInfo |= field;
		}

		// Token: 0x06003C72 RID: 15474 RVA: 0x0014DF94 File Offset: 0x0014DF94
		private PkiStatusInfo GetPkiStatusInfo()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				new DerInteger((int)this.status)
			});
			if (this.statusStrings.Count > 0)
			{
				asn1EncodableVector.Add(new PkiFreeText(new DerSequence(this.statusStrings)));
			}
			if (this.failInfo != 0)
			{
				asn1EncodableVector.Add(new TimeStampResponseGenerator.FailInfo(this.failInfo));
			}
			return new PkiStatusInfo(new DerSequence(asn1EncodableVector));
		}

		// Token: 0x06003C73 RID: 15475 RVA: 0x0014E014 File Offset: 0x0014E014
		public TimeStampResponse Generate(TimeStampRequest request, BigInteger serialNumber, DateTime genTime)
		{
			return this.Generate(request, serialNumber, new DateTimeObject(genTime));
		}

		// Token: 0x06003C74 RID: 15476 RVA: 0x0014E024 File Offset: 0x0014E024
		public TimeStampResponse Generate(TimeStampRequest request, BigInteger serialNumber, DateTimeObject genTime)
		{
			TimeStampResp resp;
			try
			{
				if (genTime == null)
				{
					throw new TspValidationException("The time source is not available.", 512);
				}
				request.Validate(this.acceptedAlgorithms, this.acceptedPolicies, this.acceptedExtensions);
				this.status = PkiStatus.Granted;
				this.AddStatusString("Operation Okay");
				PkiStatusInfo pkiStatusInfo = this.GetPkiStatusInfo();
				ContentInfo instance;
				try
				{
					TimeStampToken timeStampToken = this.tokenGenerator.Generate(request, serialNumber, genTime.Value);
					byte[] encoded = timeStampToken.ToCmsSignedData().GetEncoded();
					instance = ContentInfo.GetInstance(Asn1Object.FromByteArray(encoded));
				}
				catch (IOException e)
				{
					throw new TspException("Timestamp token received cannot be converted to ContentInfo", e);
				}
				resp = new TimeStampResp(pkiStatusInfo, instance);
			}
			catch (TspValidationException ex)
			{
				this.status = PkiStatus.Rejection;
				this.SetFailInfoField(ex.FailureCode);
				this.AddStatusString(ex.Message);
				PkiStatusInfo pkiStatusInfo2 = this.GetPkiStatusInfo();
				resp = new TimeStampResp(pkiStatusInfo2, null);
			}
			TimeStampResponse result;
			try
			{
				result = new TimeStampResponse(resp);
			}
			catch (IOException e2)
			{
				throw new TspException("created badly formatted response!", e2);
			}
			return result;
		}

		// Token: 0x06003C75 RID: 15477 RVA: 0x0014E148 File Offset: 0x0014E148
		public TimeStampResponse GenerateFailResponse(PkiStatus status, int failInfoField, string statusString)
		{
			this.status = status;
			this.SetFailInfoField(failInfoField);
			if (statusString != null)
			{
				this.AddStatusString(statusString);
			}
			PkiStatusInfo pkiStatusInfo = this.GetPkiStatusInfo();
			TimeStampResp resp = new TimeStampResp(pkiStatusInfo, null);
			TimeStampResponse result;
			try
			{
				result = new TimeStampResponse(resp);
			}
			catch (IOException e)
			{
				throw new TspException("created badly formatted response!", e);
			}
			return result;
		}

		// Token: 0x04001EB2 RID: 7858
		private PkiStatus status;

		// Token: 0x04001EB3 RID: 7859
		private Asn1EncodableVector statusStrings;

		// Token: 0x04001EB4 RID: 7860
		private int failInfo;

		// Token: 0x04001EB5 RID: 7861
		private TimeStampTokenGenerator tokenGenerator;

		// Token: 0x04001EB6 RID: 7862
		private IList acceptedAlgorithms;

		// Token: 0x04001EB7 RID: 7863
		private IList acceptedPolicies;

		// Token: 0x04001EB8 RID: 7864
		private IList acceptedExtensions;

		// Token: 0x02000E6F RID: 3695
		private class FailInfo : DerBitString
		{
			// Token: 0x06008D73 RID: 36211 RVA: 0x002A69BC File Offset: 0x002A69BC
			internal FailInfo(int failInfoValue) : base(failInfoValue)
			{
			}
		}
	}
}
