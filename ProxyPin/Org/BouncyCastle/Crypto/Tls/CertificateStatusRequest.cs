using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004D4 RID: 1236
	public class CertificateStatusRequest
	{
		// Token: 0x06002634 RID: 9780 RVA: 0x000D0418 File Offset: 0x000D0418
		public CertificateStatusRequest(byte statusType, object request)
		{
			if (!CertificateStatusRequest.IsCorrectType(statusType, request))
			{
				throw new ArgumentException("not an instance of the correct type", "request");
			}
			this.mStatusType = statusType;
			this.mRequest = request;
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06002635 RID: 9781 RVA: 0x000D044C File Offset: 0x000D044C
		public virtual byte StatusType
		{
			get
			{
				return this.mStatusType;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06002636 RID: 9782 RVA: 0x000D0454 File Offset: 0x000D0454
		public virtual object Request
		{
			get
			{
				return this.mRequest;
			}
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x000D045C File Offset: 0x000D045C
		public virtual OcspStatusRequest GetOcspStatusRequest()
		{
			if (!CertificateStatusRequest.IsCorrectType(1, this.mRequest))
			{
				throw new InvalidOperationException("'request' is not an OCSPStatusRequest");
			}
			return (OcspStatusRequest)this.mRequest;
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x000D0488 File Offset: 0x000D0488
		public virtual void Encode(Stream output)
		{
			TlsUtilities.WriteUint8(this.mStatusType, output);
			byte b = this.mStatusType;
			if (b == 1)
			{
				((OcspStatusRequest)this.mRequest).Encode(output);
				return;
			}
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x000D04CC File Offset: 0x000D04CC
		public static CertificateStatusRequest Parse(Stream input)
		{
			byte b = TlsUtilities.ReadUint8(input);
			byte b2 = b;
			if (b2 == 1)
			{
				object request = OcspStatusRequest.Parse(input);
				return new CertificateStatusRequest(b, request);
			}
			throw new TlsFatalAlert(50);
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x000D0508 File Offset: 0x000D0508
		protected static bool IsCorrectType(byte statusType, object request)
		{
			if (statusType == 1)
			{
				return request is OcspStatusRequest;
			}
			throw new ArgumentException("unsupported CertificateStatusType", "statusType");
		}

		// Token: 0x040017E1 RID: 6113
		protected readonly byte mStatusType;

		// Token: 0x040017E2 RID: 6114
		protected readonly object mRequest;
	}
}
