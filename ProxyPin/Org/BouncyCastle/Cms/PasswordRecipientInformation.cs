using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x0200030E RID: 782
	public class PasswordRecipientInformation : RecipientInformation
	{
		// Token: 0x060017AB RID: 6059 RVA: 0x0007B414 File Offset: 0x0007B414
		internal PasswordRecipientInformation(PasswordRecipientInfo info, CmsSecureReadable secureReadable) : base(info.KeyEncryptionAlgorithm, secureReadable)
		{
			this.info = info;
			this.rid = new RecipientID();
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x0007B438 File Offset: 0x0007B438
		public virtual AlgorithmIdentifier KeyDerivationAlgorithm
		{
			get
			{
				return this.info.KeyDerivationAlgorithm;
			}
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x0007B448 File Offset: 0x0007B448
		public override CmsTypedStream GetContentStream(ICipherParameters key)
		{
			CmsTypedStream contentFromSessionKey;
			try
			{
				AlgorithmIdentifier instance = AlgorithmIdentifier.GetInstance(this.info.KeyEncryptionAlgorithm);
				Asn1Sequence asn1Sequence = (Asn1Sequence)instance.Parameters;
				byte[] octets = this.info.EncryptedKey.GetOctets();
				string id = DerObjectIdentifier.GetInstance(asn1Sequence[0]).Id;
				string rfc3211WrapperName = CmsEnvelopedHelper.Instance.GetRfc3211WrapperName(id);
				IWrapper wrapper = WrapperUtilities.GetWrapper(rfc3211WrapperName);
				byte[] octets2 = Asn1OctetString.GetInstance(asn1Sequence[1]).GetOctets();
				ICipherParameters parameters = ((CmsPbeKey)key).GetEncoded(id);
				parameters = new ParametersWithIV(parameters, octets2);
				wrapper.Init(false, parameters);
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

		// Token: 0x04000FD3 RID: 4051
		private readonly PasswordRecipientInfo info;
	}
}
