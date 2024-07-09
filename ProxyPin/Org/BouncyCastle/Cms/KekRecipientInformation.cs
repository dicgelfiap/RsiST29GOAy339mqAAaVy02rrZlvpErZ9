using System;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000303 RID: 771
	public class KekRecipientInformation : RecipientInformation
	{
		// Token: 0x0600174F RID: 5967 RVA: 0x00079CB8 File Offset: 0x00079CB8
		internal KekRecipientInformation(KekRecipientInfo info, CmsSecureReadable secureReadable) : base(info.KeyEncryptionAlgorithm, secureReadable)
		{
			this.info = info;
			this.rid = new RecipientID();
			KekIdentifier kekID = info.KekID;
			this.rid.KeyIdentifier = kekID.KeyIdentifier.GetOctets();
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x00079D08 File Offset: 0x00079D08
		public override CmsTypedStream GetContentStream(ICipherParameters key)
		{
			CmsTypedStream contentFromSessionKey;
			try
			{
				byte[] octets = this.info.EncryptedKey.GetOctets();
				IWrapper wrapper = WrapperUtilities.GetWrapper(this.keyEncAlg.Algorithm.Id);
				wrapper.Init(false, key);
				KeyParameter sKey = ParameterUtilities.CreateKeyParameter(base.GetContentAlgorithmName(), wrapper.Unwrap(octets, 0, octets.Length));
				contentFromSessionKey = base.GetContentFromSessionKey(sKey);
			}
			catch (SecurityUtilityException e)
			{
				throw new CmsException("couldn't create cipher.", e);
			}
			catch (InvalidKeyException e2)
			{
				throw new CmsException("key invalid in message.", e2);
			}
			return contentFromSessionKey;
		}

		// Token: 0x04000FAD RID: 4013
		private KekRecipientInfo info;
	}
}
