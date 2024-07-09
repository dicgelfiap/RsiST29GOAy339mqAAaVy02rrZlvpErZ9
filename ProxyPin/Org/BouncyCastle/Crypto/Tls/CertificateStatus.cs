using System;
using System.IO;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004D3 RID: 1235
	public class CertificateStatus
	{
		// Token: 0x0600262D RID: 9773 RVA: 0x000D02DC File Offset: 0x000D02DC
		public CertificateStatus(byte statusType, object response)
		{
			if (!CertificateStatus.IsCorrectType(statusType, response))
			{
				throw new ArgumentException("not an instance of the correct type", "response");
			}
			this.mStatusType = statusType;
			this.mResponse = response;
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x0600262E RID: 9774 RVA: 0x000D0310 File Offset: 0x000D0310
		public virtual byte StatusType
		{
			get
			{
				return this.mStatusType;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x0600262F RID: 9775 RVA: 0x000D0318 File Offset: 0x000D0318
		public virtual object Response
		{
			get
			{
				return this.mResponse;
			}
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x000D0320 File Offset: 0x000D0320
		public virtual OcspResponse GetOcspResponse()
		{
			if (!CertificateStatus.IsCorrectType(1, this.mResponse))
			{
				throw new InvalidOperationException("'response' is not an OcspResponse");
			}
			return (OcspResponse)this.mResponse;
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x000D034C File Offset: 0x000D034C
		public virtual void Encode(Stream output)
		{
			TlsUtilities.WriteUint8(this.mStatusType, output);
			byte b = this.mStatusType;
			if (b == 1)
			{
				byte[] encoded = ((OcspResponse)this.mResponse).GetEncoded("DER");
				TlsUtilities.WriteOpaque24(encoded, output);
				return;
			}
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x000D039C File Offset: 0x000D039C
		public static CertificateStatus Parse(Stream input)
		{
			byte b = TlsUtilities.ReadUint8(input);
			byte b2 = b;
			if (b2 == 1)
			{
				byte[] encoding = TlsUtilities.ReadOpaque24(input);
				object instance = OcspResponse.GetInstance(TlsUtilities.ReadDerObject(encoding));
				return new CertificateStatus(b, instance);
			}
			throw new TlsFatalAlert(50);
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x000D03E4 File Offset: 0x000D03E4
		protected static bool IsCorrectType(byte statusType, object response)
		{
			if (statusType == 1)
			{
				return response is OcspResponse;
			}
			throw new ArgumentException("unsupported CertificateStatusType", "statusType");
		}

		// Token: 0x040017DF RID: 6111
		protected readonly byte mStatusType;

		// Token: 0x040017E0 RID: 6112
		protected readonly object mResponse;
	}
}
