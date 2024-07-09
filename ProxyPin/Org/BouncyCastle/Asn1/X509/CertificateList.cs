using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001EB RID: 491
	public class CertificateList : Asn1Encodable
	{
		// Token: 0x06000FCB RID: 4043 RVA: 0x0005D748 File Offset: 0x0005D748
		public static CertificateList GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return CertificateList.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0005D758 File Offset: 0x0005D758
		public static CertificateList GetInstance(object obj)
		{
			if (obj is CertificateList)
			{
				return (CertificateList)obj;
			}
			if (obj != null)
			{
				return new CertificateList(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0005D780 File Offset: 0x0005D780
		private CertificateList(Asn1Sequence seq)
		{
			if (seq.Count != 3)
			{
				throw new ArgumentException("sequence wrong size for CertificateList", "seq");
			}
			this.tbsCertList = TbsCertificateList.GetInstance(seq[0]);
			this.sigAlgID = AlgorithmIdentifier.GetInstance(seq[1]);
			this.sig = DerBitString.GetInstance(seq[2]);
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x0005D7EC File Offset: 0x0005D7EC
		public TbsCertificateList TbsCertList
		{
			get
			{
				return this.tbsCertList;
			}
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0005D7F4 File Offset: 0x0005D7F4
		public CrlEntry[] GetRevokedCertificates()
		{
			return this.tbsCertList.GetRevokedCertificates();
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0005D804 File Offset: 0x0005D804
		public IEnumerable GetRevokedCertificateEnumeration()
		{
			return this.tbsCertList.GetRevokedCertificateEnumeration();
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x0005D814 File Offset: 0x0005D814
		public AlgorithmIdentifier SignatureAlgorithm
		{
			get
			{
				return this.sigAlgID;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x0005D81C File Offset: 0x0005D81C
		public DerBitString Signature
		{
			get
			{
				return this.sig;
			}
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0005D824 File Offset: 0x0005D824
		public byte[] GetSignatureOctets()
		{
			return this.sig.GetOctets();
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x0005D834 File Offset: 0x0005D834
		public int Version
		{
			get
			{
				return this.tbsCertList.Version;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x0005D844 File Offset: 0x0005D844
		public X509Name Issuer
		{
			get
			{
				return this.tbsCertList.Issuer;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x0005D854 File Offset: 0x0005D854
		public Time ThisUpdate
		{
			get
			{
				return this.tbsCertList.ThisUpdate;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x0005D864 File Offset: 0x0005D864
		public Time NextUpdate
		{
			get
			{
				return this.tbsCertList.NextUpdate;
			}
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0005D874 File Offset: 0x0005D874
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.tbsCertList,
				this.sigAlgID,
				this.sig
			});
		}

		// Token: 0x04000BB3 RID: 2995
		private readonly TbsCertificateList tbsCertList;

		// Token: 0x04000BB4 RID: 2996
		private readonly AlgorithmIdentifier sigAlgID;

		// Token: 0x04000BB5 RID: 2997
		private readonly DerBitString sig;
	}
}
