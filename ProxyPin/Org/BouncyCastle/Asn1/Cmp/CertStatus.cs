using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000D4 RID: 212
	public class CertStatus : Asn1Encodable
	{
		// Token: 0x06000816 RID: 2070 RVA: 0x000412A8 File Offset: 0x000412A8
		private CertStatus(Asn1Sequence seq)
		{
			this.certHash = Asn1OctetString.GetInstance(seq[0]);
			this.certReqId = DerInteger.GetInstance(seq[1]);
			if (seq.Count > 2)
			{
				this.statusInfo = PkiStatusInfo.GetInstance(seq[2]);
			}
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00041304 File Offset: 0x00041304
		public CertStatus(byte[] certHash, BigInteger certReqId)
		{
			this.certHash = new DerOctetString(certHash);
			this.certReqId = new DerInteger(certReqId);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00041324 File Offset: 0x00041324
		public CertStatus(byte[] certHash, BigInteger certReqId, PkiStatusInfo statusInfo)
		{
			this.certHash = new DerOctetString(certHash);
			this.certReqId = new DerInteger(certReqId);
			this.statusInfo = statusInfo;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0004134C File Offset: 0x0004134C
		public static CertStatus GetInstance(object obj)
		{
			if (obj is CertStatus)
			{
				return (CertStatus)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertStatus((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x000413A0 File Offset: 0x000413A0
		public virtual Asn1OctetString CertHash
		{
			get
			{
				return this.certHash;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x000413A8 File Offset: 0x000413A8
		public virtual DerInteger CertReqID
		{
			get
			{
				return this.certReqId;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x000413B0 File Offset: 0x000413B0
		public virtual PkiStatusInfo StatusInfo
		{
			get
			{
				return this.statusInfo;
			}
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x000413B8 File Offset: 0x000413B8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certHash,
				this.certReqId
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.statusInfo
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040005E7 RID: 1511
		private readonly Asn1OctetString certHash;

		// Token: 0x040005E8 RID: 1512
		private readonly DerInteger certReqId;

		// Token: 0x040005E9 RID: 1513
		private readonly PkiStatusInfo statusInfo;
	}
}
