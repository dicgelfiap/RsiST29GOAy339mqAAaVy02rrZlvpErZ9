using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x0200063A RID: 1594
	public class OCSPRespGenerator
	{
		// Token: 0x0600379B RID: 14235 RVA: 0x0012A2EC File Offset: 0x0012A2EC
		public OcspResp Generate(int status, object response)
		{
			if (response == null)
			{
				return new OcspResp(new OcspResponse(new OcspResponseStatus(status), null));
			}
			if (response is BasicOcspResp)
			{
				BasicOcspResp basicOcspResp = (BasicOcspResp)response;
				Asn1OctetString response2;
				try
				{
					response2 = new DerOctetString(basicOcspResp.GetEncoded());
				}
				catch (Exception e)
				{
					throw new OcspException("can't encode object.", e);
				}
				ResponseBytes responseBytes = new ResponseBytes(OcspObjectIdentifiers.PkixOcspBasic, response2);
				return new OcspResp(new OcspResponse(new OcspResponseStatus(status), responseBytes));
			}
			throw new OcspException("unknown response object");
		}

		// Token: 0x04001D5C RID: 7516
		public const int Successful = 0;

		// Token: 0x04001D5D RID: 7517
		public const int MalformedRequest = 1;

		// Token: 0x04001D5E RID: 7518
		public const int InternalError = 2;

		// Token: 0x04001D5F RID: 7519
		public const int TryLater = 3;

		// Token: 0x04001D60 RID: 7520
		public const int SigRequired = 5;

		// Token: 0x04001D61 RID: 7521
		public const int Unauthorized = 6;
	}
}
