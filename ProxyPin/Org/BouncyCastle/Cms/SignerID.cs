using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000313 RID: 787
	public class SignerID : X509CertStoreSelector
	{
		// Token: 0x060017C3 RID: 6083 RVA: 0x0007B8A0 File Offset: 0x0007B8A0
		public override int GetHashCode()
		{
			int num = Arrays.GetHashCode(base.SubjectKeyIdentifier);
			BigInteger serialNumber = base.SerialNumber;
			if (serialNumber != null)
			{
				num ^= serialNumber.GetHashCode();
			}
			X509Name issuer = base.Issuer;
			if (issuer != null)
			{
				num ^= issuer.GetHashCode();
			}
			return num;
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x0007B8EC File Offset: 0x0007B8EC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return false;
			}
			SignerID signerID = obj as SignerID;
			return signerID != null && (Arrays.AreEqual(base.SubjectKeyIdentifier, signerID.SubjectKeyIdentifier) && object.Equals(base.SerialNumber, signerID.SerialNumber)) && X509CertStoreSelector.IssuersMatch(base.Issuer, signerID.Issuer);
		}
	}
}
