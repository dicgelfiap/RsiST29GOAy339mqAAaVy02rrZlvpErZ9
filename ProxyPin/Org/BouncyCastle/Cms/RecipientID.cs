using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000311 RID: 785
	public class RecipientID : X509CertStoreSelector
	{
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x0007B650 File Offset: 0x0007B650
		// (set) Token: 0x060017B9 RID: 6073 RVA: 0x0007B660 File Offset: 0x0007B660
		public byte[] KeyIdentifier
		{
			get
			{
				return Arrays.Clone(this.keyIdentifier);
			}
			set
			{
				this.keyIdentifier = Arrays.Clone(value);
			}
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x0007B670 File Offset: 0x0007B670
		public override int GetHashCode()
		{
			int num = Arrays.GetHashCode(this.keyIdentifier) ^ Arrays.GetHashCode(base.SubjectKeyIdentifier);
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

		// Token: 0x060017BB RID: 6075 RVA: 0x0007B6C8 File Offset: 0x0007B6C8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			RecipientID recipientID = obj as RecipientID;
			return recipientID != null && (Arrays.AreEqual(this.keyIdentifier, recipientID.keyIdentifier) && Arrays.AreEqual(base.SubjectKeyIdentifier, recipientID.SubjectKeyIdentifier) && object.Equals(base.SerialNumber, recipientID.SerialNumber)) && X509CertStoreSelector.IssuersMatch(base.Issuer, recipientID.Issuer);
		}

		// Token: 0x04000FD4 RID: 4052
		private byte[] keyIdentifier;
	}
}
