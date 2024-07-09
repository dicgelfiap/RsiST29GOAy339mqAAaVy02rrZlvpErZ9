using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crmf
{
	// Token: 0x0200031C RID: 796
	public class CertificateRequestMessageBuilder
	{
		// Token: 0x0600180E RID: 6158 RVA: 0x0007CE10 File Offset: 0x0007CE10
		public CertificateRequestMessageBuilder(BigInteger certReqId)
		{
			this._certReqId = certReqId;
			this._extGenerator = new X509ExtensionsGenerator();
			this._templateBuilder = new CertTemplateBuilder();
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x0007CE48 File Offset: 0x0007CE48
		public CertificateRequestMessageBuilder SetPublicKey(SubjectPublicKeyInfo publicKeyInfo)
		{
			if (publicKeyInfo != null)
			{
				this._templateBuilder.SetPublicKey(publicKeyInfo);
			}
			return this;
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x0007CE60 File Offset: 0x0007CE60
		public CertificateRequestMessageBuilder SetIssuer(X509Name issuer)
		{
			if (issuer != null)
			{
				this._templateBuilder.SetIssuer(issuer);
			}
			return this;
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x0007CE78 File Offset: 0x0007CE78
		public CertificateRequestMessageBuilder SetSubject(X509Name subject)
		{
			if (subject != null)
			{
				this._templateBuilder.SetSubject(subject);
			}
			return this;
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x0007CE90 File Offset: 0x0007CE90
		public CertificateRequestMessageBuilder SetSerialNumber(BigInteger serialNumber)
		{
			if (serialNumber != null)
			{
				this._templateBuilder.SetSerialNumber(new DerInteger(serialNumber));
			}
			return this;
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x0007CEAC File Offset: 0x0007CEAC
		public CertificateRequestMessageBuilder SetValidity(Time notBefore, Time notAfter)
		{
			this._templateBuilder.SetValidity(new OptionalValidity(notBefore, notAfter));
			return this;
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x0007CEC4 File Offset: 0x0007CEC4
		public CertificateRequestMessageBuilder AddExtension(DerObjectIdentifier oid, bool critical, Asn1Encodable value)
		{
			this._extGenerator.AddExtension(oid, critical, value);
			return this;
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x0007CED8 File Offset: 0x0007CED8
		public CertificateRequestMessageBuilder AddExtension(DerObjectIdentifier oid, bool critical, byte[] value)
		{
			this._extGenerator.AddExtension(oid, critical, value);
			return this;
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x0007CEEC File Offset: 0x0007CEEC
		public CertificateRequestMessageBuilder AddControl(IControl control)
		{
			this._controls.Add(control);
			return this;
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x0007CEFC File Offset: 0x0007CEFC
		public CertificateRequestMessageBuilder SetProofOfPossessionSignKeySigner(ISignatureFactory popoSignatureFactory)
		{
			if (this._popoPrivKey != null || this._popRaVerified != null || this._agreeMac != null)
			{
				throw new InvalidOperationException("only one proof of possession is allowed.");
			}
			this._popSigner = popoSignatureFactory;
			return this;
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x0007CF34 File Offset: 0x0007CF34
		public CertificateRequestMessageBuilder SetProofOfPossessionSubsequentMessage(SubsequentMessage msg)
		{
			if (this._popoPrivKey != null || this._popRaVerified != null || this._agreeMac != null)
			{
				throw new InvalidOperationException("only one proof of possession is allowed.");
			}
			this._popoType = 2;
			this._popoPrivKey = new PopoPrivKey(msg);
			return this;
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x0007CF88 File Offset: 0x0007CF88
		public CertificateRequestMessageBuilder SetProofOfPossessionSubsequentMessage(int type, SubsequentMessage msg)
		{
			if (this._popoPrivKey != null || this._popRaVerified != null || this._agreeMac != null)
			{
				throw new InvalidOperationException("only one proof of possession is allowed.");
			}
			if (type != 2 && type != 3)
			{
				throw new ArgumentException("type must be ProofOfPossession.TYPE_KEY_ENCIPHERMENT || ProofOfPossession.TYPE_KEY_AGREEMENT");
			}
			this._popoType = type;
			this._popoPrivKey = new PopoPrivKey(msg);
			return this;
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x0007CFF4 File Offset: 0x0007CFF4
		public CertificateRequestMessageBuilder SetProofOfPossessionAgreeMac(PKMacValue macValue)
		{
			if (this._popSigner != null || this._popRaVerified != null || this._popoPrivKey != null)
			{
				throw new InvalidOperationException("only one proof of possession allowed");
			}
			this._agreeMac = macValue;
			return this;
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x0007D02C File Offset: 0x0007D02C
		public CertificateRequestMessageBuilder SetProofOfPossessionRaVerified()
		{
			if (this._popSigner != null || this._popoPrivKey != null)
			{
				throw new InvalidOperationException("only one proof of possession allowed");
			}
			this._popRaVerified = DerNull.Instance;
			return this;
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x0007D05C File Offset: 0x0007D05C
		public CertificateRequestMessageBuilder SetAuthInfoPKMAC(PKMacBuilder pkmacFactory, char[] password)
		{
			this._pkMacBuilder = pkmacFactory;
			this._password = password;
			return this;
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x0007D070 File Offset: 0x0007D070
		public CertificateRequestMessageBuilder SetAuthInfoSender(X509Name sender)
		{
			return this.SetAuthInfoSender(new GeneralName(sender));
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x0007D080 File Offset: 0x0007D080
		public CertificateRequestMessageBuilder SetAuthInfoSender(GeneralName sender)
		{
			this._sender = sender;
			return this;
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x0007D08C File Offset: 0x0007D08C
		public CertificateRequestMessage Build()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				new DerInteger(this._certReqId)
			});
			if (!this._extGenerator.IsEmpty)
			{
				this._templateBuilder.SetExtensions(this._extGenerator.Generate());
			}
			asn1EncodableVector.Add(this._templateBuilder.Build());
			if (this._controls.Count > 0)
			{
				Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector();
				foreach (object obj in this._controls)
				{
					IControl control = (IControl)obj;
					asn1EncodableVector2.Add(new AttributeTypeAndValue(control.Type, control.Value));
				}
				asn1EncodableVector.Add(new DerSequence(asn1EncodableVector2));
			}
			CertRequest instance = CertRequest.GetInstance(new DerSequence(asn1EncodableVector));
			asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				instance
			});
			if (this._popSigner != null)
			{
				CertTemplate certTemplate = instance.CertTemplate;
				if (certTemplate.Subject == null || certTemplate.PublicKey == null)
				{
					SubjectPublicKeyInfo publicKey = instance.CertTemplate.PublicKey;
					ProofOfPossessionSigningKeyBuilder proofOfPossessionSigningKeyBuilder = new ProofOfPossessionSigningKeyBuilder(publicKey);
					if (this._sender != null)
					{
						proofOfPossessionSigningKeyBuilder.SetSender(this._sender);
					}
					else
					{
						proofOfPossessionSigningKeyBuilder.SetPublicKeyMac(this._pkMacBuilder, this._password);
					}
					asn1EncodableVector.Add(new ProofOfPossession(proofOfPossessionSigningKeyBuilder.Build(this._popSigner)));
				}
				else
				{
					ProofOfPossessionSigningKeyBuilder proofOfPossessionSigningKeyBuilder2 = new ProofOfPossessionSigningKeyBuilder(instance);
					asn1EncodableVector.Add(new ProofOfPossession(proofOfPossessionSigningKeyBuilder2.Build(this._popSigner)));
				}
			}
			else if (this._popoPrivKey != null)
			{
				asn1EncodableVector.Add(new ProofOfPossession(this._popoType, this._popoPrivKey));
			}
			else if (this._agreeMac != null)
			{
				asn1EncodableVector.Add(new ProofOfPossession(3, PopoPrivKey.GetInstance(new DerTaggedObject(false, 3, this._agreeMac), true)));
			}
			else if (this._popRaVerified != null)
			{
				asn1EncodableVector.Add(new ProofOfPossession());
			}
			return new CertificateRequestMessage(CertReqMsg.GetInstance(new DerSequence(asn1EncodableVector)));
		}

		// Token: 0x04000FFA RID: 4090
		private readonly BigInteger _certReqId;

		// Token: 0x04000FFB RID: 4091
		private X509ExtensionsGenerator _extGenerator;

		// Token: 0x04000FFC RID: 4092
		private CertTemplateBuilder _templateBuilder;

		// Token: 0x04000FFD RID: 4093
		private IList _controls = Platform.CreateArrayList();

		// Token: 0x04000FFE RID: 4094
		private ISignatureFactory _popSigner;

		// Token: 0x04000FFF RID: 4095
		private PKMacBuilder _pkMacBuilder;

		// Token: 0x04001000 RID: 4096
		private char[] _password;

		// Token: 0x04001001 RID: 4097
		private GeneralName _sender;

		// Token: 0x04001002 RID: 4098
		private int _popoType = 2;

		// Token: 0x04001003 RID: 4099
		private PopoPrivKey _popoPrivKey;

		// Token: 0x04001004 RID: 4100
		private Asn1Null _popRaVerified;

		// Token: 0x04001005 RID: 4101
		private PKMacValue _agreeMac;
	}
}
