using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crmf
{
	// Token: 0x0200032B RID: 811
	public class ProofOfPossessionSigningKeyBuilder
	{
		// Token: 0x06001856 RID: 6230 RVA: 0x0007DB20 File Offset: 0x0007DB20
		public ProofOfPossessionSigningKeyBuilder(CertRequest certRequest)
		{
			this._certRequest = certRequest;
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x0007DB30 File Offset: 0x0007DB30
		public ProofOfPossessionSigningKeyBuilder(SubjectPublicKeyInfo pubKeyInfo)
		{
			this._pubKeyInfo = pubKeyInfo;
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0007DB40 File Offset: 0x0007DB40
		public ProofOfPossessionSigningKeyBuilder SetSender(GeneralName name)
		{
			this._name = name;
			return this;
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0007DB4C File Offset: 0x0007DB4C
		public ProofOfPossessionSigningKeyBuilder SetPublicKeyMac(PKMacBuilder generator, char[] password)
		{
			IMacFactory macFactory = generator.Build(password);
			IStreamCalculator streamCalculator = macFactory.CreateCalculator();
			byte[] derEncoded = this._pubKeyInfo.GetDerEncoded();
			streamCalculator.Stream.Write(derEncoded, 0, derEncoded.Length);
			streamCalculator.Stream.Flush();
			Platform.Dispose(streamCalculator.Stream);
			this._publicKeyMAC = new PKMacValue((AlgorithmIdentifier)macFactory.AlgorithmDetails, new DerBitString(((IBlockResult)streamCalculator.GetResult()).Collect()));
			return this;
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x0007DBCC File Offset: 0x0007DBCC
		public PopoSigningKey Build(ISignatureFactory signer)
		{
			if (this._name != null && this._publicKeyMAC != null)
			{
				throw new InvalidOperationException("name and publicKeyMAC cannot both be set.");
			}
			IStreamCalculator streamCalculator = signer.CreateCalculator();
			PopoSigningKeyInput popoSigningKeyInput;
			if (this._certRequest != null)
			{
				popoSigningKeyInput = null;
				byte[] derEncoded = this._certRequest.GetDerEncoded();
				streamCalculator.Stream.Write(derEncoded, 0, derEncoded.Length);
			}
			else if (this._name != null)
			{
				popoSigningKeyInput = new PopoSigningKeyInput(this._name, this._pubKeyInfo);
				byte[] derEncoded = popoSigningKeyInput.GetDerEncoded();
				streamCalculator.Stream.Write(derEncoded, 0, derEncoded.Length);
			}
			else
			{
				popoSigningKeyInput = new PopoSigningKeyInput(this._publicKeyMAC, this._pubKeyInfo);
				byte[] derEncoded = popoSigningKeyInput.GetDerEncoded();
				streamCalculator.Stream.Write(derEncoded, 0, derEncoded.Length);
			}
			streamCalculator.Stream.Flush();
			Platform.Dispose(streamCalculator.Stream);
			DefaultSignatureResult defaultSignatureResult = (DefaultSignatureResult)streamCalculator.GetResult();
			return new PopoSigningKey(popoSigningKeyInput, (AlgorithmIdentifier)signer.AlgorithmDetails, new DerBitString(defaultSignatureResult.Collect()));
		}

		// Token: 0x0400101C RID: 4124
		private CertRequest _certRequest;

		// Token: 0x0400101D RID: 4125
		private SubjectPublicKeyInfo _pubKeyInfo;

		// Token: 0x0400101E RID: 4126
		private GeneralName _name;

		// Token: 0x0400101F RID: 4127
		private PKMacValue _publicKeyMAC;
	}
}
