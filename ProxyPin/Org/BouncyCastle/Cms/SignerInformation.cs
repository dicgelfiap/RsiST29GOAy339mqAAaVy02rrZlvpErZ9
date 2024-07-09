using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000316 RID: 790
	public class SignerInformation
	{
		// Token: 0x060017D1 RID: 6097 RVA: 0x0007BAF8 File Offset: 0x0007BAF8
		internal SignerInformation(Org.BouncyCastle.Asn1.Cms.SignerInfo info, DerObjectIdentifier contentType, CmsProcessable content, IDigestCalculator digestCalculator)
		{
			this.info = info;
			this.sid = new SignerID();
			this.contentType = contentType;
			this.isCounterSignature = (contentType == null);
			try
			{
				SignerIdentifier signerID = info.SignerID;
				if (signerID.IsTagged)
				{
					Asn1OctetString instance = Asn1OctetString.GetInstance(signerID.ID);
					this.sid.SubjectKeyIdentifier = instance.GetEncoded();
				}
				else
				{
					Org.BouncyCastle.Asn1.Cms.IssuerAndSerialNumber instance2 = Org.BouncyCastle.Asn1.Cms.IssuerAndSerialNumber.GetInstance(signerID.ID);
					this.sid.Issuer = instance2.Name;
					this.sid.SerialNumber = instance2.SerialNumber.Value;
				}
			}
			catch (IOException)
			{
				throw new ArgumentException("invalid sid in SignerInfo");
			}
			this.digestAlgorithm = info.DigestAlgorithm;
			this.signedAttributeSet = info.AuthenticatedAttributes;
			this.unsignedAttributeSet = info.UnauthenticatedAttributes;
			this.encryptionAlgorithm = info.DigestEncryptionAlgorithm;
			this.signature = info.EncryptedDigest.GetOctets();
			this.content = content;
			this.digestCalculator = digestCalculator;
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x0007BC08 File Offset: 0x0007BC08
		protected SignerInformation(SignerInformation baseInfo)
		{
			this.info = baseInfo.info;
			this.contentType = baseInfo.contentType;
			this.isCounterSignature = baseInfo.IsCounterSignature;
			this.sid = baseInfo.SignerID;
			this.digestAlgorithm = this.info.DigestAlgorithm;
			this.signedAttributeSet = this.info.AuthenticatedAttributes;
			this.unsignedAttributeSet = this.info.UnauthenticatedAttributes;
			this.encryptionAlgorithm = this.info.DigestEncryptionAlgorithm;
			this.signature = this.info.EncryptedDigest.GetOctets();
			this.content = baseInfo.content;
			this.resultDigest = baseInfo.resultDigest;
			this.signedAttributeTable = baseInfo.signedAttributeTable;
			this.unsignedAttributeTable = baseInfo.unsignedAttributeTable;
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060017D3 RID: 6099 RVA: 0x0007BCDC File Offset: 0x0007BCDC
		public bool IsCounterSignature
		{
			get
			{
				return this.isCounterSignature;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x0007BCE4 File Offset: 0x0007BCE4
		public DerObjectIdentifier ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060017D5 RID: 6101 RVA: 0x0007BCEC File Offset: 0x0007BCEC
		public SignerID SignerID
		{
			get
			{
				return this.sid;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x0007BCF4 File Offset: 0x0007BCF4
		public int Version
		{
			get
			{
				return this.info.Version.IntValueExact;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060017D7 RID: 6103 RVA: 0x0007BD08 File Offset: 0x0007BD08
		public AlgorithmIdentifier DigestAlgorithmID
		{
			get
			{
				return this.digestAlgorithm;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x0007BD10 File Offset: 0x0007BD10
		public string DigestAlgOid
		{
			get
			{
				return this.digestAlgorithm.Algorithm.Id;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060017D9 RID: 6105 RVA: 0x0007BD24 File Offset: 0x0007BD24
		public Asn1Object DigestAlgParams
		{
			get
			{
				Asn1Encodable parameters = this.digestAlgorithm.Parameters;
				if (parameters != null)
				{
					return parameters.ToAsn1Object();
				}
				return null;
			}
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x0007BD50 File Offset: 0x0007BD50
		public byte[] GetContentDigest()
		{
			if (this.resultDigest == null)
			{
				throw new InvalidOperationException("method can only be called after verify.");
			}
			return (byte[])this.resultDigest.Clone();
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060017DB RID: 6107 RVA: 0x0007BD78 File Offset: 0x0007BD78
		public AlgorithmIdentifier EncryptionAlgorithmID
		{
			get
			{
				return this.encryptionAlgorithm;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x0007BD80 File Offset: 0x0007BD80
		public string EncryptionAlgOid
		{
			get
			{
				return this.encryptionAlgorithm.Algorithm.Id;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x060017DD RID: 6109 RVA: 0x0007BD94 File Offset: 0x0007BD94
		public Asn1Object EncryptionAlgParams
		{
			get
			{
				Asn1Encodable parameters = this.encryptionAlgorithm.Parameters;
				if (parameters != null)
				{
					return parameters.ToAsn1Object();
				}
				return null;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x0007BDC0 File Offset: 0x0007BDC0
		public Org.BouncyCastle.Asn1.Cms.AttributeTable SignedAttributes
		{
			get
			{
				if (this.signedAttributeSet != null && this.signedAttributeTable == null)
				{
					this.signedAttributeTable = new Org.BouncyCastle.Asn1.Cms.AttributeTable(this.signedAttributeSet);
				}
				return this.signedAttributeTable;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060017DF RID: 6111 RVA: 0x0007BDF0 File Offset: 0x0007BDF0
		public Org.BouncyCastle.Asn1.Cms.AttributeTable UnsignedAttributes
		{
			get
			{
				if (this.unsignedAttributeSet != null && this.unsignedAttributeTable == null)
				{
					this.unsignedAttributeTable = new Org.BouncyCastle.Asn1.Cms.AttributeTable(this.unsignedAttributeSet);
				}
				return this.unsignedAttributeTable;
			}
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x0007BE20 File Offset: 0x0007BE20
		public byte[] GetSignature()
		{
			return (byte[])this.signature.Clone();
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x0007BE34 File Offset: 0x0007BE34
		public SignerInformationStore GetCounterSignatures()
		{
			Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttributes = this.UnsignedAttributes;
			if (unsignedAttributes == null)
			{
				return new SignerInformationStore(Platform.CreateArrayList(0));
			}
			IList list = Platform.CreateArrayList();
			Asn1EncodableVector all = unsignedAttributes.GetAll(CmsAttributes.CounterSignature);
			foreach (object obj in all)
			{
				Org.BouncyCastle.Asn1.Cms.Attribute attribute = (Org.BouncyCastle.Asn1.Cms.Attribute)obj;
				Asn1Set attrValues = attribute.AttrValues;
				int count = attrValues.Count;
				foreach (object obj2 in attrValues)
				{
					Asn1Encodable asn1Encodable = (Asn1Encodable)obj2;
					Org.BouncyCastle.Asn1.Cms.SignerInfo instance = Org.BouncyCastle.Asn1.Cms.SignerInfo.GetInstance(asn1Encodable.ToAsn1Object());
					string digestAlgName = CmsSignedHelper.Instance.GetDigestAlgName(instance.DigestAlgorithm.Algorithm.Id);
					list.Add(new SignerInformation(instance, null, null, new CounterSignatureDigestCalculator(digestAlgName, this.GetSignature())));
				}
			}
			return new SignerInformationStore(list);
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x0007BF6C File Offset: 0x0007BF6C
		public byte[] GetEncodedSignedAttributes()
		{
			if (this.signedAttributeSet != null)
			{
				return this.signedAttributeSet.GetEncoded("DER");
			}
			return null;
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x0007BF8C File Offset: 0x0007BF8C
		private bool DoVerify(AsymmetricKeyParameter key)
		{
			string digestAlgName = SignerInformation.Helper.GetDigestAlgName(this.DigestAlgOid);
			IDigest digestInstance = SignerInformation.Helper.GetDigestInstance(digestAlgName);
			DerObjectIdentifier algorithm = this.encryptionAlgorithm.Algorithm;
			Asn1Encodable parameters = this.encryptionAlgorithm.Parameters;
			ISigner signer;
			if (algorithm.Equals(PkcsObjectIdentifiers.IdRsassaPss))
			{
				if (parameters == null)
				{
					throw new CmsException("RSASSA-PSS signature must specify algorithm parameters");
				}
				try
				{
					RsassaPssParameters instance = RsassaPssParameters.GetInstance(parameters.ToAsn1Object());
					if (!instance.HashAlgorithm.Algorithm.Equals(this.digestAlgorithm.Algorithm))
					{
						throw new CmsException("RSASSA-PSS signature parameters specified incorrect hash algorithm");
					}
					if (!instance.MaskGenAlgorithm.Algorithm.Equals(PkcsObjectIdentifiers.IdMgf1))
					{
						throw new CmsException("RSASSA-PSS signature parameters specified unknown MGF");
					}
					IDigest digest = DigestUtilities.GetDigest(instance.HashAlgorithm.Algorithm);
					int intValueExact = instance.SaltLength.IntValueExact;
					byte b = (byte)instance.TrailerField.IntValueExact;
					if (b != 1)
					{
						throw new CmsException("RSASSA-PSS signature parameters must have trailerField of 1");
					}
					signer = new PssSigner(new RsaBlindedEngine(), digest, intValueExact);
					goto IL_147;
				}
				catch (Exception e)
				{
					throw new CmsException("failed to set RSASSA-PSS signature parameters", e);
				}
			}
			string algorithm2 = digestAlgName + "with" + SignerInformation.Helper.GetEncryptionAlgName(this.EncryptionAlgOid);
			signer = SignerInformation.Helper.GetSignatureInstance(algorithm2);
			try
			{
				IL_147:
				if (this.digestCalculator != null)
				{
					this.resultDigest = this.digestCalculator.GetDigest();
				}
				else
				{
					if (this.content != null)
					{
						this.content.Write(new DigestSink(digestInstance));
					}
					else if (this.signedAttributeSet == null)
					{
						throw new CmsException("data not encapsulated in signature - use detached constructor.");
					}
					this.resultDigest = DigestUtilities.DoFinal(digestInstance);
				}
			}
			catch (IOException e2)
			{
				throw new CmsException("can't process mime object to create signature.", e2);
			}
			Asn1Object singleValuedSignedAttribute = this.GetSingleValuedSignedAttribute(CmsAttributes.ContentType, "content-type");
			if (singleValuedSignedAttribute == null)
			{
				if (!this.isCounterSignature && this.signedAttributeSet != null)
				{
					throw new CmsException("The content-type attribute type MUST be present whenever signed attributes are present in signed-data");
				}
			}
			else
			{
				if (this.isCounterSignature)
				{
					throw new CmsException("[For counter signatures,] the signedAttributes field MUST NOT contain a content-type attribute");
				}
				if (!(singleValuedSignedAttribute is DerObjectIdentifier))
				{
					throw new CmsException("content-type attribute value not of ASN.1 type 'OBJECT IDENTIFIER'");
				}
				DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)singleValuedSignedAttribute;
				if (!derObjectIdentifier.Equals(this.contentType))
				{
					throw new CmsException("content-type attribute value does not match eContentType");
				}
			}
			Asn1Object singleValuedSignedAttribute2 = this.GetSingleValuedSignedAttribute(CmsAttributes.MessageDigest, "message-digest");
			if (singleValuedSignedAttribute2 == null)
			{
				if (this.signedAttributeSet != null)
				{
					throw new CmsException("the message-digest signed attribute type MUST be present when there are any signed attributes present");
				}
			}
			else
			{
				if (!(singleValuedSignedAttribute2 is Asn1OctetString))
				{
					throw new CmsException("message-digest attribute value not of ASN.1 type 'OCTET STRING'");
				}
				Asn1OctetString asn1OctetString = (Asn1OctetString)singleValuedSignedAttribute2;
				if (!Arrays.AreEqual(this.resultDigest, asn1OctetString.GetOctets()))
				{
					throw new CmsException("message-digest attribute value does not match calculated value");
				}
			}
			Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttributes = this.SignedAttributes;
			if (signedAttributes != null && signedAttributes.GetAll(CmsAttributes.CounterSignature).Count > 0)
			{
				throw new CmsException("A countersignature attribute MUST NOT be a signed attribute");
			}
			Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttributes = this.UnsignedAttributes;
			if (unsignedAttributes != null)
			{
				foreach (object obj in unsignedAttributes.GetAll(CmsAttributes.CounterSignature))
				{
					Org.BouncyCastle.Asn1.Cms.Attribute attribute = (Org.BouncyCastle.Asn1.Cms.Attribute)obj;
					if (attribute.AttrValues.Count < 1)
					{
						throw new CmsException("A countersignature attribute MUST contain at least one AttributeValue");
					}
				}
			}
			bool result;
			try
			{
				signer.Init(false, key);
				if (this.signedAttributeSet == null)
				{
					if (this.digestCalculator != null)
					{
						return this.VerifyDigest(this.resultDigest, key, this.GetSignature());
					}
					if (this.content == null)
					{
						goto IL_3EB;
					}
					try
					{
						this.content.Write(new SignerSink(signer));
						goto IL_3EB;
					}
					catch (SignatureException arg)
					{
						throw new CmsStreamException("signature problem: " + arg);
					}
				}
				byte[] encodedSignedAttributes = this.GetEncodedSignedAttributes();
				signer.BlockUpdate(encodedSignedAttributes, 0, encodedSignedAttributes.Length);
				IL_3EB:
				result = signer.VerifySignature(this.GetSignature());
			}
			catch (InvalidKeyException e3)
			{
				throw new CmsException("key not appropriate to signature in message.", e3);
			}
			catch (IOException e4)
			{
				throw new CmsException("can't process mime object to create signature.", e4);
			}
			catch (SignatureException ex)
			{
				throw new CmsException("invalid signature format in message: " + ex.Message, ex);
			}
			return result;
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x0007C42C File Offset: 0x0007C42C
		private bool IsNull(Asn1Encodable o)
		{
			return o is Asn1Null || o == null;
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x0007C440 File Offset: 0x0007C440
		private DigestInfo DerDecode(byte[] encoding)
		{
			if (encoding[0] != 48)
			{
				throw new IOException("not a digest info object");
			}
			DigestInfo instance = DigestInfo.GetInstance(Asn1Object.FromByteArray(encoding));
			if (instance.GetEncoded().Length != encoding.Length)
			{
				throw new CmsException("malformed RSA signature");
			}
			return instance;
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x0007C490 File Offset: 0x0007C490
		private bool VerifyDigest(byte[] digest, AsymmetricKeyParameter key, byte[] signature)
		{
			string encryptionAlgName = SignerInformation.Helper.GetEncryptionAlgName(this.EncryptionAlgOid);
			bool result;
			try
			{
				if (encryptionAlgName.Equals("RSA"))
				{
					IBufferedCipher bufferedCipher = CmsEnvelopedHelper.Instance.CreateAsymmetricCipher("RSA/ECB/PKCS1Padding");
					bufferedCipher.Init(false, key);
					byte[] encoding = bufferedCipher.DoFinal(signature);
					DigestInfo digestInfo = this.DerDecode(encoding);
					if (!digestInfo.AlgorithmID.Algorithm.Equals(this.digestAlgorithm.Algorithm))
					{
						result = false;
					}
					else if (!this.IsNull(digestInfo.AlgorithmID.Parameters))
					{
						result = false;
					}
					else
					{
						byte[] digest2 = digestInfo.GetDigest();
						result = Arrays.ConstantTimeAreEqual(digest, digest2);
					}
				}
				else
				{
					if (!encryptionAlgName.Equals("DSA"))
					{
						throw new CmsException("algorithm: " + encryptionAlgName + " not supported in base signatures.");
					}
					ISigner signer = SignerUtilities.GetSigner("NONEwithDSA");
					signer.Init(false, key);
					signer.BlockUpdate(digest, 0, digest.Length);
					result = signer.VerifySignature(signature);
				}
			}
			catch (SecurityUtilityException ex)
			{
				throw ex;
			}
			catch (GeneralSecurityException ex2)
			{
				throw new CmsException("Exception processing signature: " + ex2, ex2);
			}
			catch (IOException ex3)
			{
				throw new CmsException("Exception decoding signature: " + ex3, ex3);
			}
			return result;
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x0007C5F4 File Offset: 0x0007C5F4
		public bool Verify(AsymmetricKeyParameter pubKey)
		{
			if (pubKey.IsPrivate)
			{
				throw new ArgumentException("Expected public key", "pubKey");
			}
			this.GetSigningTime();
			return this.DoVerify(pubKey);
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x0007C620 File Offset: 0x0007C620
		public bool Verify(X509Certificate cert)
		{
			Org.BouncyCastle.Asn1.Cms.Time signingTime = this.GetSigningTime();
			if (signingTime != null)
			{
				cert.CheckValidity(signingTime.Date);
			}
			return this.DoVerify(cert.GetPublicKey());
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x0007C658 File Offset: 0x0007C658
		public Org.BouncyCastle.Asn1.Cms.SignerInfo ToSignerInfo()
		{
			return this.info;
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x0007C660 File Offset: 0x0007C660
		private Asn1Object GetSingleValuedSignedAttribute(DerObjectIdentifier attrOID, string printableName)
		{
			Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttributes = this.UnsignedAttributes;
			if (unsignedAttributes != null && unsignedAttributes.GetAll(attrOID).Count > 0)
			{
				throw new CmsException("The " + printableName + " attribute MUST NOT be an unsigned attribute");
			}
			Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttributes = this.SignedAttributes;
			if (signedAttributes == null)
			{
				return null;
			}
			Asn1EncodableVector all = signedAttributes.GetAll(attrOID);
			switch (all.Count)
			{
			case 0:
				return null;
			case 1:
			{
				Org.BouncyCastle.Asn1.Cms.Attribute attribute = (Org.BouncyCastle.Asn1.Cms.Attribute)all[0];
				Asn1Set attrValues = attribute.AttrValues;
				if (attrValues.Count != 1)
				{
					throw new CmsException("A " + printableName + " attribute MUST have a single attribute value");
				}
				return attrValues[0].ToAsn1Object();
			}
			default:
				throw new CmsException("The SignedAttributes in a signerInfo MUST NOT include multiple instances of the " + printableName + " attribute");
			}
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x0007C738 File Offset: 0x0007C738
		private Org.BouncyCastle.Asn1.Cms.Time GetSigningTime()
		{
			Asn1Object singleValuedSignedAttribute = this.GetSingleValuedSignedAttribute(CmsAttributes.SigningTime, "signing-time");
			if (singleValuedSignedAttribute == null)
			{
				return null;
			}
			Org.BouncyCastle.Asn1.Cms.Time instance;
			try
			{
				instance = Org.BouncyCastle.Asn1.Cms.Time.GetInstance(singleValuedSignedAttribute);
			}
			catch (ArgumentException)
			{
				throw new CmsException("signing-time attribute value not a valid 'Time' structure");
			}
			return instance;
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x0007C788 File Offset: 0x0007C788
		public static SignerInformation ReplaceUnsignedAttributes(SignerInformation signerInformation, Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttributes)
		{
			Org.BouncyCastle.Asn1.Cms.SignerInfo signerInfo = signerInformation.info;
			Asn1Set unauthenticatedAttributes = null;
			if (unsignedAttributes != null)
			{
				unauthenticatedAttributes = new DerSet(unsignedAttributes.ToAsn1EncodableVector());
			}
			return new SignerInformation(new Org.BouncyCastle.Asn1.Cms.SignerInfo(signerInfo.SignerID, signerInfo.DigestAlgorithm, signerInfo.AuthenticatedAttributes, signerInfo.DigestEncryptionAlgorithm, signerInfo.EncryptedDigest, unauthenticatedAttributes), signerInformation.contentType, signerInformation.content, null);
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x0007C7EC File Offset: 0x0007C7EC
		public static SignerInformation AddCounterSigners(SignerInformation signerInformation, SignerInformationStore counterSigners)
		{
			Org.BouncyCastle.Asn1.Cms.SignerInfo signerInfo = signerInformation.info;
			Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttributes = signerInformation.UnsignedAttributes;
			Asn1EncodableVector asn1EncodableVector;
			if (unsignedAttributes != null)
			{
				asn1EncodableVector = unsignedAttributes.ToAsn1EncodableVector();
			}
			else
			{
				asn1EncodableVector = new Asn1EncodableVector();
			}
			Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector();
			foreach (object obj in counterSigners.GetSigners())
			{
				SignerInformation signerInformation2 = (SignerInformation)obj;
				asn1EncodableVector2.Add(signerInformation2.ToSignerInfo());
			}
			asn1EncodableVector.Add(new Org.BouncyCastle.Asn1.Cms.Attribute(CmsAttributes.CounterSignature, new DerSet(asn1EncodableVector2)));
			return new SignerInformation(new Org.BouncyCastle.Asn1.Cms.SignerInfo(signerInfo.SignerID, signerInfo.DigestAlgorithm, signerInfo.AuthenticatedAttributes, signerInfo.DigestEncryptionAlgorithm, signerInfo.EncryptedDigest, new DerSet(asn1EncodableVector)), signerInformation.contentType, signerInformation.content, null);
		}

		// Token: 0x04000FE0 RID: 4064
		private static readonly CmsSignedHelper Helper = CmsSignedHelper.Instance;

		// Token: 0x04000FE1 RID: 4065
		private SignerID sid;

		// Token: 0x04000FE2 RID: 4066
		private Org.BouncyCastle.Asn1.Cms.SignerInfo info;

		// Token: 0x04000FE3 RID: 4067
		private AlgorithmIdentifier digestAlgorithm;

		// Token: 0x04000FE4 RID: 4068
		private AlgorithmIdentifier encryptionAlgorithm;

		// Token: 0x04000FE5 RID: 4069
		private readonly Asn1Set signedAttributeSet;

		// Token: 0x04000FE6 RID: 4070
		private readonly Asn1Set unsignedAttributeSet;

		// Token: 0x04000FE7 RID: 4071
		private CmsProcessable content;

		// Token: 0x04000FE8 RID: 4072
		private byte[] signature;

		// Token: 0x04000FE9 RID: 4073
		private DerObjectIdentifier contentType;

		// Token: 0x04000FEA RID: 4074
		private IDigestCalculator digestCalculator;

		// Token: 0x04000FEB RID: 4075
		private byte[] resultDigest;

		// Token: 0x04000FEC RID: 4076
		private Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttributeTable;

		// Token: 0x04000FED RID: 4077
		private Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttributeTable;

		// Token: 0x04000FEE RID: 4078
		private readonly bool isCounterSignature;
	}
}
