using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x02000640 RID: 1600
	public class RespID
	{
		// Token: 0x060037B0 RID: 14256 RVA: 0x0012AA08 File Offset: 0x0012AA08
		public RespID(ResponderID id)
		{
			this.id = id;
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x0012AA18 File Offset: 0x0012AA18
		public RespID(X509Name name)
		{
			this.id = new ResponderID(name);
		}

		// Token: 0x060037B2 RID: 14258 RVA: 0x0012AA2C File Offset: 0x0012AA2C
		public RespID(AsymmetricKeyParameter publicKey)
		{
			try
			{
				SubjectPublicKeyInfo subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);
				byte[] str = DigestUtilities.CalculateDigest("SHA1", subjectPublicKeyInfo.PublicKeyData.GetBytes());
				this.id = new ResponderID(new DerOctetString(str));
			}
			catch (Exception ex)
			{
				throw new OcspException("problem creating ID: " + ex, ex);
			}
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x0012AA98 File Offset: 0x0012AA98
		public ResponderID ToAsn1Object()
		{
			return this.id;
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x0012AAA0 File Offset: 0x0012AAA0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			RespID respID = obj as RespID;
			return respID != null && this.id.Equals(respID.id);
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x0012AADC File Offset: 0x0012AADC
		public override int GetHashCode()
		{
			return this.id.GetHashCode();
		}

		// Token: 0x04001D6D RID: 7533
		internal readonly ResponderID id;
	}
}
