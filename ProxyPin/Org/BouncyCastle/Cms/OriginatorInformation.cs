using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x0200030C RID: 780
	public class OriginatorInformation
	{
		// Token: 0x060017A1 RID: 6049 RVA: 0x0007B158 File Offset: 0x0007B158
		internal OriginatorInformation(OriginatorInfo originatorInfo)
		{
			this.originatorInfo = originatorInfo;
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x0007B168 File Offset: 0x0007B168
		public virtual IX509Store GetCertificates()
		{
			Asn1Set certificates = this.originatorInfo.Certificates;
			if (certificates != null)
			{
				IList list = Platform.CreateArrayList(certificates.Count);
				foreach (object obj in certificates)
				{
					Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
					Asn1Object asn1Object = asn1Encodable.ToAsn1Object();
					if (asn1Object is Asn1Sequence)
					{
						list.Add(new X509Certificate(X509CertificateStructure.GetInstance(asn1Object)));
					}
				}
				return X509StoreFactory.Create("Certificate/Collection", new X509CollectionStoreParameters(list));
			}
			return X509StoreFactory.Create("Certificate/Collection", new X509CollectionStoreParameters(Platform.CreateArrayList()));
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x0007B22C File Offset: 0x0007B22C
		public virtual IX509Store GetCrls()
		{
			Asn1Set certificates = this.originatorInfo.Certificates;
			if (certificates != null)
			{
				IList list = Platform.CreateArrayList(certificates.Count);
				foreach (object obj in certificates)
				{
					Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
					Asn1Object asn1Object = asn1Encodable.ToAsn1Object();
					if (asn1Object is Asn1Sequence)
					{
						list.Add(new X509Crl(CertificateList.GetInstance(asn1Object)));
					}
				}
				return X509StoreFactory.Create("CRL/Collection", new X509CollectionStoreParameters(list));
			}
			return X509StoreFactory.Create("CRL/Collection", new X509CollectionStoreParameters(Platform.CreateArrayList()));
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0007B2F0 File Offset: 0x0007B2F0
		public virtual OriginatorInfo ToAsn1Structure()
		{
			return this.originatorInfo;
		}

		// Token: 0x04000FCE RID: 4046
		private readonly OriginatorInfo originatorInfo;
	}
}
