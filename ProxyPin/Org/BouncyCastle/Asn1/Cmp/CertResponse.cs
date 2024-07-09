using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000D3 RID: 211
	public class CertResponse : Asn1Encodable
	{
		// Token: 0x0600080E RID: 2062 RVA: 0x000410CC File Offset: 0x000410CC
		private CertResponse(Asn1Sequence seq)
		{
			this.certReqId = DerInteger.GetInstance(seq[0]);
			this.status = PkiStatusInfo.GetInstance(seq[1]);
			if (seq.Count >= 3)
			{
				if (seq.Count == 3)
				{
					Asn1Encodable asn1Encodable = seq[2];
					if (asn1Encodable is Asn1OctetString)
					{
						this.rspInfo = Asn1OctetString.GetInstance(asn1Encodable);
						return;
					}
					this.certifiedKeyPair = CertifiedKeyPair.GetInstance(asn1Encodable);
					return;
				}
				else
				{
					this.certifiedKeyPair = CertifiedKeyPair.GetInstance(seq[2]);
					this.rspInfo = Asn1OctetString.GetInstance(seq[3]);
				}
			}
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00041170 File Offset: 0x00041170
		public static CertResponse GetInstance(object obj)
		{
			if (obj is CertResponse)
			{
				return (CertResponse)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertResponse((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x000411C4 File Offset: 0x000411C4
		public CertResponse(DerInteger certReqId, PkiStatusInfo status) : this(certReqId, status, null, null)
		{
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x000411D0 File Offset: 0x000411D0
		public CertResponse(DerInteger certReqId, PkiStatusInfo status, CertifiedKeyPair certifiedKeyPair, Asn1OctetString rspInfo)
		{
			if (certReqId == null)
			{
				throw new ArgumentNullException("certReqId");
			}
			if (status == null)
			{
				throw new ArgumentNullException("status");
			}
			this.certReqId = certReqId;
			this.status = status;
			this.certifiedKeyPair = certifiedKeyPair;
			this.rspInfo = rspInfo;
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x00041228 File Offset: 0x00041228
		public virtual DerInteger CertReqID
		{
			get
			{
				return this.certReqId;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x00041230 File Offset: 0x00041230
		public virtual PkiStatusInfo Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x00041238 File Offset: 0x00041238
		public virtual CertifiedKeyPair CertifiedKeyPair
		{
			get
			{
				return this.certifiedKeyPair;
			}
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00041240 File Offset: 0x00041240
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certReqId,
				this.status
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.certifiedKeyPair,
				this.rspInfo
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040005E3 RID: 1507
		private readonly DerInteger certReqId;

		// Token: 0x040005E4 RID: 1508
		private readonly PkiStatusInfo status;

		// Token: 0x040005E5 RID: 1509
		private readonly CertifiedKeyPair certifiedKeyPair;

		// Token: 0x040005E6 RID: 1510
		private readonly Asn1OctetString rspInfo;
	}
}
