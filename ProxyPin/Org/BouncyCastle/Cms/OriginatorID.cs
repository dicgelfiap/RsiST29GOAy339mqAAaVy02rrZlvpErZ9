using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x0200030A RID: 778
	public class OriginatorID : X509CertStoreSelector
	{
		// Token: 0x0600179A RID: 6042 RVA: 0x0007AFF0 File Offset: 0x0007AFF0
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

		// Token: 0x0600179B RID: 6043 RVA: 0x0007B03C File Offset: 0x0007B03C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return false;
			}
			OriginatorID originatorID = obj as OriginatorID;
			return originatorID != null && (Arrays.AreEqual(base.SubjectKeyIdentifier, originatorID.SubjectKeyIdentifier) && object.Equals(base.SerialNumber, originatorID.SerialNumber)) && X509CertStoreSelector.IssuersMatch(base.Issuer, originatorID.Issuer);
		}
	}
}
