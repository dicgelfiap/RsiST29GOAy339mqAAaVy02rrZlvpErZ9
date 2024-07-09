using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Operators;

namespace Org.BouncyCastle.Crmf
{
	// Token: 0x0200031B RID: 795
	public class CertificateRequestMessage
	{
		// Token: 0x060017FE RID: 6142 RVA: 0x0007CAD8 File Offset: 0x0007CAD8
		private static CertReqMsg ParseBytes(byte[] encoding)
		{
			return CertReqMsg.GetInstance(encoding);
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x0007CAE0 File Offset: 0x0007CAE0
		public CertificateRequestMessage(byte[] encoded) : this(CertReqMsg.GetInstance(encoded))
		{
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x0007CAF0 File Offset: 0x0007CAF0
		public CertificateRequestMessage(CertReqMsg certReqMsg)
		{
			this.certReqMsg = certReqMsg;
			this.controls = certReqMsg.CertReq.Controls;
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x0007CB10 File Offset: 0x0007CB10
		public CertReqMsg ToAsn1Structure()
		{
			return this.certReqMsg;
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x0007CB18 File Offset: 0x0007CB18
		public CertTemplate GetCertTemplate()
		{
			return this.certReqMsg.CertReq.CertTemplate;
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x0007CB2C File Offset: 0x0007CB2C
		public bool HasControls
		{
			get
			{
				return this.controls != null;
			}
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x0007CB3C File Offset: 0x0007CB3C
		public bool HasControl(DerObjectIdentifier objectIdentifier)
		{
			return this.FindControl(objectIdentifier) != null;
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x0007CB4C File Offset: 0x0007CB4C
		public IControl GetControl(DerObjectIdentifier type)
		{
			AttributeTypeAndValue attributeTypeAndValue = this.FindControl(type);
			if (attributeTypeAndValue != null)
			{
				if (attributeTypeAndValue.Type.Equals(CrmfObjectIdentifiers.id_regCtrl_pkiArchiveOptions))
				{
					return new PkiArchiveControl(PkiArchiveOptions.GetInstance(attributeTypeAndValue.Value));
				}
				if (attributeTypeAndValue.Type.Equals(CrmfObjectIdentifiers.id_regCtrl_regToken))
				{
					return new RegTokenControl(DerUtf8String.GetInstance(attributeTypeAndValue.Value));
				}
				if (attributeTypeAndValue.Type.Equals(CrmfObjectIdentifiers.id_regCtrl_authenticator))
				{
					return new AuthenticatorControl(DerUtf8String.GetInstance(attributeTypeAndValue.Value));
				}
			}
			return null;
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x0007CBE0 File Offset: 0x0007CBE0
		public AttributeTypeAndValue FindControl(DerObjectIdentifier type)
		{
			if (this.controls == null)
			{
				return null;
			}
			AttributeTypeAndValue[] array = this.controls.ToAttributeTypeAndValueArray();
			AttributeTypeAndValue result = null;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Type.Equals(type))
				{
					result = array[i];
					break;
				}
			}
			return result;
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x0007CC44 File Offset: 0x0007CC44
		public bool HasProofOfPossession
		{
			get
			{
				return this.certReqMsg.Popo != null;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x0007CC58 File Offset: 0x0007CC58
		public int ProofOfPossession
		{
			get
			{
				return this.certReqMsg.Popo.Type;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x0007CC6C File Offset: 0x0007CC6C
		public bool HasSigningKeyProofOfPossessionWithPkMac
		{
			get
			{
				ProofOfPossession popo = this.certReqMsg.Popo;
				if (popo.Type == CertificateRequestMessage.popSigningKey)
				{
					PopoSigningKey instance = PopoSigningKey.GetInstance(popo.Object);
					return instance.PoposkInput.PublicKeyMac != null;
				}
				return false;
			}
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x0007CCB8 File Offset: 0x0007CCB8
		public bool IsValidSigningKeyPop(IVerifierFactoryProvider verifierProvider)
		{
			ProofOfPossession popo = this.certReqMsg.Popo;
			if (popo.Type != CertificateRequestMessage.popSigningKey)
			{
				throw new InvalidOperationException("not Signing Key type of proof of possession");
			}
			PopoSigningKey instance = PopoSigningKey.GetInstance(popo.Object);
			if (instance.PoposkInput != null && instance.PoposkInput.PublicKeyMac != null)
			{
				throw new InvalidOperationException("verification requires password check");
			}
			return this.verifySignature(verifierProvider, instance);
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x0007CD2C File Offset: 0x0007CD2C
		private bool verifySignature(IVerifierFactoryProvider verifierFactoryProvider, PopoSigningKey signKey)
		{
			IStreamCalculator streamCalculator;
			try
			{
				IVerifierFactory verifierFactory = verifierFactoryProvider.CreateVerifierFactory(signKey.AlgorithmIdentifier);
				streamCalculator = verifierFactory.CreateCalculator();
			}
			catch (Exception ex)
			{
				throw new CrmfException("unable to create verifier: " + ex.Message, ex);
			}
			if (signKey.PoposkInput != null)
			{
				byte[] derEncoded = signKey.GetDerEncoded();
				streamCalculator.Stream.Write(derEncoded, 0, derEncoded.Length);
			}
			else
			{
				byte[] derEncoded2 = this.certReqMsg.CertReq.GetDerEncoded();
				streamCalculator.Stream.Write(derEncoded2, 0, derEncoded2.Length);
			}
			DefaultVerifierResult defaultVerifierResult = (DefaultVerifierResult)streamCalculator.GetResult();
			return defaultVerifierResult.IsVerified(signKey.Signature.GetBytes());
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x0007CDE4 File Offset: 0x0007CDE4
		public byte[] GetEncoded()
		{
			return this.certReqMsg.GetEncoded();
		}

		// Token: 0x04000FF4 RID: 4084
		public static readonly int popRaVerified = 0;

		// Token: 0x04000FF5 RID: 4085
		public static readonly int popSigningKey = 1;

		// Token: 0x04000FF6 RID: 4086
		public static readonly int popKeyEncipherment = 2;

		// Token: 0x04000FF7 RID: 4087
		public static readonly int popKeyAgreement = 3;

		// Token: 0x04000FF8 RID: 4088
		private readonly CertReqMsg certReqMsg;

		// Token: 0x04000FF9 RID: 4089
		private readonly Controls controls;
	}
}
