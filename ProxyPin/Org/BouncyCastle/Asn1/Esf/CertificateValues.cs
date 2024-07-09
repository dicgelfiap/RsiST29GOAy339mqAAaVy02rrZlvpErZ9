using System;
using System.Collections;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000149 RID: 329
	public class CertificateValues : Asn1Encodable
	{
		// Token: 0x06000B74 RID: 2932 RVA: 0x0004BE6C File Offset: 0x0004BE6C
		public static CertificateValues GetInstance(object obj)
		{
			if (obj == null || obj is CertificateValues)
			{
				return (CertificateValues)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertificateValues((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CertificateValues' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0004BEC8 File Offset: 0x0004BEC8
		private CertificateValues(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			foreach (object obj in seq)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				X509CertificateStructure.GetInstance(asn1Encodable.ToAsn1Object());
			}
			this.certificates = seq;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0004BF48 File Offset: 0x0004BF48
		public CertificateValues(params X509CertificateStructure[] certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			this.certificates = new DerSequence(certificates);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0004BF70 File Offset: 0x0004BF70
		public CertificateValues(IEnumerable certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			if (!CollectionUtilities.CheckElementsAreOfType(certificates, typeof(X509CertificateStructure)))
			{
				throw new ArgumentException("Must contain only 'X509CertificateStructure' objects", "certificates");
			}
			this.certificates = new DerSequence(Asn1EncodableVector.FromEnumerable(certificates));
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0004BFD0 File Offset: 0x0004BFD0
		public X509CertificateStructure[] GetCertificates()
		{
			X509CertificateStructure[] array = new X509CertificateStructure[this.certificates.Count];
			for (int i = 0; i < this.certificates.Count; i++)
			{
				array[i] = X509CertificateStructure.GetInstance(this.certificates[i]);
			}
			return array;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0004C024 File Offset: 0x0004C024
		public override Asn1Object ToAsn1Object()
		{
			return this.certificates;
		}

		// Token: 0x040007E9 RID: 2025
		private readonly Asn1Sequence certificates;
	}
}
