using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Icao
{
	// Token: 0x0200016F RID: 367
	public class CscaMasterList : Asn1Encodable
	{
		// Token: 0x06000C6B RID: 3179 RVA: 0x000500F4 File Offset: 0x000500F4
		public static CscaMasterList GetInstance(object obj)
		{
			if (obj is CscaMasterList)
			{
				return (CscaMasterList)obj;
			}
			if (obj != null)
			{
				return new CscaMasterList(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0005011C File Offset: 0x0005011C
		private CscaMasterList(Asn1Sequence seq)
		{
			if (seq == null || seq.Count == 0)
			{
				throw new ArgumentException("null or empty sequence passed.");
			}
			if (seq.Count != 2)
			{
				throw new ArgumentException("Incorrect sequence size: " + seq.Count);
			}
			this.version = DerInteger.GetInstance(seq[0]);
			Asn1Set instance = Asn1Set.GetInstance(seq[1]);
			this.certList = new X509CertificateStructure[instance.Count];
			for (int i = 0; i < this.certList.Length; i++)
			{
				this.certList[i] = X509CertificateStructure.GetInstance(instance[i]);
			}
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x000501E0 File Offset: 0x000501E0
		public CscaMasterList(X509CertificateStructure[] certStructs)
		{
			this.certList = CscaMasterList.CopyCertList(certStructs);
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x00050200 File Offset: 0x00050200
		public virtual int Version
		{
			get
			{
				return this.version.IntValueExact;
			}
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00050210 File Offset: 0x00050210
		public X509CertificateStructure[] GetCertStructs()
		{
			return CscaMasterList.CopyCertList(this.certList);
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00050220 File Offset: 0x00050220
		private static X509CertificateStructure[] CopyCertList(X509CertificateStructure[] orig)
		{
			return (X509CertificateStructure[])orig.Clone();
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00050230 File Offset: 0x00050230
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.version,
				new DerSet(this.certList)
			});
		}

		// Token: 0x0400089F RID: 2207
		private DerInteger version = new DerInteger(0);

		// Token: 0x040008A0 RID: 2208
		private X509CertificateStructure[] certList;
	}
}
