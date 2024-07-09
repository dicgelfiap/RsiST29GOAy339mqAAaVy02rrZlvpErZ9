using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002F2 RID: 754
	public class CmsSignedDataGenerator : CmsSignedGenerator
	{
		// Token: 0x060016B6 RID: 5814 RVA: 0x00075CCC File Offset: 0x00075CCC
		public CmsSignedDataGenerator()
		{
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x00075CE0 File Offset: 0x00075CE0
		public CmsSignedDataGenerator(SecureRandom rand) : base(rand)
		{
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x00075CF4 File Offset: 0x00075CF4
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string digestOID)
		{
			this.AddSigner(privateKey, cert, CmsSignedDataGenerator.Helper.GetEncOid(privateKey, digestOID), digestOID);
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x00075D0C File Offset: 0x00075D0C
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string encryptionOID, string digestOID)
		{
			this.doAddSigner(privateKey, CmsSignedGenerator.GetSignerIdentifier(cert), encryptionOID, digestOID, new DefaultSignedAttributeTableGenerator(), null, null);
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x00075D34 File Offset: 0x00075D34
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string digestOID)
		{
			this.AddSigner(privateKey, subjectKeyID, CmsSignedDataGenerator.Helper.GetEncOid(privateKey, digestOID), digestOID);
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x00075D4C File Offset: 0x00075D4C
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string encryptionOID, string digestOID)
		{
			this.doAddSigner(privateKey, CmsSignedGenerator.GetSignerIdentifier(subjectKeyID), encryptionOID, digestOID, new DefaultSignedAttributeTableGenerator(), null, null);
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x00075D74 File Offset: 0x00075D74
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string digestOID, Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.AddSigner(privateKey, cert, CmsSignedDataGenerator.Helper.GetEncOid(privateKey, digestOID), digestOID, signedAttr, unsignedAttr);
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x00075DA0 File Offset: 0x00075DA0
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string encryptionOID, string digestOID, Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.doAddSigner(privateKey, CmsSignedGenerator.GetSignerIdentifier(cert), encryptionOID, digestOID, new DefaultSignedAttributeTableGenerator(signedAttr), new SimpleAttributeTableGenerator(unsignedAttr), signedAttr);
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x00075DD4 File Offset: 0x00075DD4
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string digestOID, Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.AddSigner(privateKey, subjectKeyID, CmsSignedDataGenerator.Helper.GetEncOid(privateKey, digestOID), digestOID, signedAttr, unsignedAttr);
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x00075E00 File Offset: 0x00075E00
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string encryptionOID, string digestOID, Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.doAddSigner(privateKey, CmsSignedGenerator.GetSignerIdentifier(subjectKeyID), encryptionOID, digestOID, new DefaultSignedAttributeTableGenerator(signedAttr), new SimpleAttributeTableGenerator(unsignedAttr), signedAttr);
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x00075E34 File Offset: 0x00075E34
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string digestOID, CmsAttributeTableGenerator signedAttrGen, CmsAttributeTableGenerator unsignedAttrGen)
		{
			this.AddSigner(privateKey, cert, CmsSignedDataGenerator.Helper.GetEncOid(privateKey, digestOID), digestOID, signedAttrGen, unsignedAttrGen);
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x00075E60 File Offset: 0x00075E60
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string encryptionOID, string digestOID, CmsAttributeTableGenerator signedAttrGen, CmsAttributeTableGenerator unsignedAttrGen)
		{
			this.doAddSigner(privateKey, CmsSignedGenerator.GetSignerIdentifier(cert), encryptionOID, digestOID, signedAttrGen, unsignedAttrGen, null);
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00075E88 File Offset: 0x00075E88
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string digestOID, CmsAttributeTableGenerator signedAttrGen, CmsAttributeTableGenerator unsignedAttrGen)
		{
			this.AddSigner(privateKey, subjectKeyID, CmsSignedDataGenerator.Helper.GetEncOid(privateKey, digestOID), digestOID, signedAttrGen, unsignedAttrGen);
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00075EB4 File Offset: 0x00075EB4
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string encryptionOID, string digestOID, CmsAttributeTableGenerator signedAttrGen, CmsAttributeTableGenerator unsignedAttrGen)
		{
			this.doAddSigner(privateKey, CmsSignedGenerator.GetSignerIdentifier(subjectKeyID), encryptionOID, digestOID, signedAttrGen, unsignedAttrGen, null);
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x00075EDC File Offset: 0x00075EDC
		public void AddSignerInfoGenerator(SignerInfoGenerator signerInfoGenerator)
		{
			this.signerInfs.Add(new CmsSignedDataGenerator.SignerInf(this, signerInfoGenerator.contentSigner, signerInfoGenerator.sigId, signerInfoGenerator.signedGen, signerInfoGenerator.unsignedGen, null));
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x00075F0C File Offset: 0x00075F0C
		private void doAddSigner(AsymmetricKeyParameter privateKey, SignerIdentifier signerIdentifier, string encryptionOID, string digestOID, CmsAttributeTableGenerator signedAttrGen, CmsAttributeTableGenerator unsignedAttrGen, Org.BouncyCastle.Asn1.Cms.AttributeTable baseSignedTable)
		{
			this.signerInfs.Add(new CmsSignedDataGenerator.SignerInf(this, privateKey, signerIdentifier, digestOID, encryptionOID, signedAttrGen, unsignedAttrGen, baseSignedTable));
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x00075F3C File Offset: 0x00075F3C
		public CmsSignedData Generate(CmsProcessable content)
		{
			return this.Generate(content, false);
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x00075F48 File Offset: 0x00075F48
		public CmsSignedData Generate(string signedContentType, CmsProcessable content, bool encapsulate)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector();
			this._digests.Clear();
			foreach (object obj in this._signers)
			{
				SignerInformation signerInformation = (SignerInformation)obj;
				asn1EncodableVector.Add(CmsSignedDataGenerator.Helper.FixAlgID(signerInformation.DigestAlgorithmID));
				asn1EncodableVector2.Add(signerInformation.ToSignerInfo());
			}
			DerObjectIdentifier contentType = (signedContentType == null) ? null : new DerObjectIdentifier(signedContentType);
			foreach (object obj2 in this.signerInfs)
			{
				CmsSignedDataGenerator.SignerInf signerInf = (CmsSignedDataGenerator.SignerInf)obj2;
				try
				{
					asn1EncodableVector.Add(signerInf.DigestAlgorithmID);
					asn1EncodableVector2.Add(signerInf.ToSignerInfo(contentType, content, this.rand));
				}
				catch (IOException e)
				{
					throw new CmsException("encoding error.", e);
				}
				catch (InvalidKeyException e2)
				{
					throw new CmsException("key inappropriate for signature.", e2);
				}
				catch (SignatureException e3)
				{
					throw new CmsException("error creating signature.", e3);
				}
				catch (CertificateEncodingException e4)
				{
					throw new CmsException("error creating sid.", e4);
				}
			}
			Asn1Set certificates = null;
			if (this._certs.Count != 0)
			{
				certificates = (base.UseDerForCerts ? CmsUtilities.CreateDerSetFromList(this._certs) : CmsUtilities.CreateBerSetFromList(this._certs));
			}
			Asn1Set crls = null;
			if (this._crls.Count != 0)
			{
				crls = (base.UseDerForCrls ? CmsUtilities.CreateDerSetFromList(this._crls) : CmsUtilities.CreateBerSetFromList(this._crls));
			}
			Asn1OctetString content2 = null;
			if (encapsulate)
			{
				MemoryStream memoryStream = new MemoryStream();
				if (content != null)
				{
					try
					{
						content.Write(memoryStream);
					}
					catch (IOException e5)
					{
						throw new CmsException("encapsulation error.", e5);
					}
				}
				content2 = new BerOctetString(memoryStream.ToArray());
			}
			ContentInfo contentInfo = new ContentInfo(contentType, content2);
			SignedData content3 = new SignedData(new DerSet(asn1EncodableVector), contentInfo, certificates, crls, new DerSet(asn1EncodableVector2));
			ContentInfo sigData = new ContentInfo(CmsObjectIdentifiers.SignedData, content3);
			return new CmsSignedData(content, sigData);
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x000761D8 File Offset: 0x000761D8
		public CmsSignedData Generate(CmsProcessable content, bool encapsulate)
		{
			return this.Generate(CmsSignedGenerator.Data, content, encapsulate);
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x000761E8 File Offset: 0x000761E8
		public SignerInformationStore GenerateCounterSigners(SignerInformation signer)
		{
			return this.Generate(null, new CmsProcessableByteArray(signer.GetSignature()), false).GetSignerInfos();
		}

		// Token: 0x04000F6A RID: 3946
		private static readonly CmsSignedHelper Helper = CmsSignedHelper.Instance;

		// Token: 0x04000F6B RID: 3947
		private readonly IList signerInfs = Platform.CreateArrayList();

		// Token: 0x02000DDF RID: 3551
		private class SignerInf
		{
			// Token: 0x06008B80 RID: 35712 RVA: 0x0029DDD4 File Offset: 0x0029DDD4
			internal SignerInf(CmsSignedGenerator outer, AsymmetricKeyParameter key, SignerIdentifier signerIdentifier, string digestOID, string encOID, CmsAttributeTableGenerator sAttr, CmsAttributeTableGenerator unsAttr, Org.BouncyCastle.Asn1.Cms.AttributeTable baseSignedTable)
			{
				string digestAlgName = CmsSignedDataGenerator.Helper.GetDigestAlgName(digestOID);
				string algorithm = digestAlgName + "with" + CmsSignedDataGenerator.Helper.GetEncryptionAlgName(encOID);
				this.outer = outer;
				this.sigCalc = new Asn1SignatureFactory(algorithm, key);
				this.signerIdentifier = signerIdentifier;
				this.digestOID = digestOID;
				this.encOID = encOID;
				this.sAttr = sAttr;
				this.unsAttr = unsAttr;
				this.baseSignedTable = baseSignedTable;
			}

			// Token: 0x06008B81 RID: 35713 RVA: 0x0029DE54 File Offset: 0x0029DE54
			internal SignerInf(CmsSignedGenerator outer, ISignatureFactory sigCalc, SignerIdentifier signerIdentifier, CmsAttributeTableGenerator sAttr, CmsAttributeTableGenerator unsAttr, Org.BouncyCastle.Asn1.Cms.AttributeTable baseSignedTable)
			{
				this.outer = outer;
				this.sigCalc = sigCalc;
				this.signerIdentifier = signerIdentifier;
				this.digestOID = new DefaultDigestAlgorithmIdentifierFinder().find((AlgorithmIdentifier)sigCalc.AlgorithmDetails).Algorithm.Id;
				this.encOID = ((AlgorithmIdentifier)sigCalc.AlgorithmDetails).Algorithm.Id;
				this.sAttr = sAttr;
				this.unsAttr = unsAttr;
				this.baseSignedTable = baseSignedTable;
			}

			// Token: 0x17001D6D RID: 7533
			// (get) Token: 0x06008B82 RID: 35714 RVA: 0x0029DED8 File Offset: 0x0029DED8
			internal AlgorithmIdentifier DigestAlgorithmID
			{
				get
				{
					return new AlgorithmIdentifier(new DerObjectIdentifier(this.digestOID), DerNull.Instance);
				}
			}

			// Token: 0x17001D6E RID: 7534
			// (get) Token: 0x06008B83 RID: 35715 RVA: 0x0029DEF0 File Offset: 0x0029DEF0
			internal CmsAttributeTableGenerator SignedAttributes
			{
				get
				{
					return this.sAttr;
				}
			}

			// Token: 0x17001D6F RID: 7535
			// (get) Token: 0x06008B84 RID: 35716 RVA: 0x0029DEF8 File Offset: 0x0029DEF8
			internal CmsAttributeTableGenerator UnsignedAttributes
			{
				get
				{
					return this.unsAttr;
				}
			}

			// Token: 0x06008B85 RID: 35717 RVA: 0x0029DF00 File Offset: 0x0029DF00
			internal SignerInfo ToSignerInfo(DerObjectIdentifier contentType, CmsProcessable content, SecureRandom random)
			{
				AlgorithmIdentifier digestAlgorithmID = this.DigestAlgorithmID;
				string digestAlgName = CmsSignedDataGenerator.Helper.GetDigestAlgName(this.digestOID);
				string algorithm = digestAlgName + "with" + CmsSignedDataGenerator.Helper.GetEncryptionAlgName(this.encOID);
				byte[] array;
				if (this.outer._digests.Contains(this.digestOID))
				{
					array = (byte[])this.outer._digests[this.digestOID];
				}
				else
				{
					IDigest digestInstance = CmsSignedDataGenerator.Helper.GetDigestInstance(digestAlgName);
					if (content != null)
					{
						content.Write(new DigestSink(digestInstance));
					}
					array = DigestUtilities.DoFinal(digestInstance);
					this.outer._digests.Add(this.digestOID, array.Clone());
				}
				IStreamCalculator streamCalculator = this.sigCalc.CreateCalculator();
				Stream stream = new BufferedStream(streamCalculator.Stream);
				Asn1Set asn1Set = null;
				if (this.sAttr != null)
				{
					IDictionary baseParameters = this.outer.GetBaseParameters(contentType, digestAlgorithmID, array);
					Org.BouncyCastle.Asn1.Cms.AttributeTable attributeTable = this.sAttr.GetAttributes(baseParameters);
					if (contentType == null && attributeTable != null && attributeTable[CmsAttributes.ContentType] != null)
					{
						IDictionary dictionary = attributeTable.ToDictionary();
						dictionary.Remove(CmsAttributes.ContentType);
						attributeTable = new Org.BouncyCastle.Asn1.Cms.AttributeTable(dictionary);
					}
					asn1Set = this.outer.GetAttributeSet(attributeTable);
					new DerOutputStream(stream).WriteObject(asn1Set);
				}
				else if (content != null)
				{
					content.Write(stream);
				}
				Platform.Dispose(stream);
				byte[] array2 = ((IBlockResult)streamCalculator.GetResult()).Collect();
				Asn1Set unauthenticatedAttributes = null;
				if (this.unsAttr != null)
				{
					IDictionary baseParameters2 = this.outer.GetBaseParameters(contentType, digestAlgorithmID, array);
					baseParameters2[CmsAttributeTableParameter.Signature] = array2.Clone();
					Org.BouncyCastle.Asn1.Cms.AttributeTable attributes = this.unsAttr.GetAttributes(baseParameters2);
					unauthenticatedAttributes = this.outer.GetAttributeSet(attributes);
				}
				Asn1Encodable defaultX509Parameters = SignerUtilities.GetDefaultX509Parameters(algorithm);
				AlgorithmIdentifier encAlgorithmIdentifier = CmsSignedDataGenerator.Helper.GetEncAlgorithmIdentifier(new DerObjectIdentifier(this.encOID), defaultX509Parameters);
				return new SignerInfo(this.signerIdentifier, digestAlgorithmID, asn1Set, encAlgorithmIdentifier, new DerOctetString(array2), unauthenticatedAttributes);
			}

			// Token: 0x04004080 RID: 16512
			private readonly CmsSignedGenerator outer;

			// Token: 0x04004081 RID: 16513
			private readonly ISignatureFactory sigCalc;

			// Token: 0x04004082 RID: 16514
			private readonly SignerIdentifier signerIdentifier;

			// Token: 0x04004083 RID: 16515
			private readonly string digestOID;

			// Token: 0x04004084 RID: 16516
			private readonly string encOID;

			// Token: 0x04004085 RID: 16517
			private readonly CmsAttributeTableGenerator sAttr;

			// Token: 0x04004086 RID: 16518
			private readonly CmsAttributeTableGenerator unsAttr;

			// Token: 0x04004087 RID: 16519
			private readonly Org.BouncyCastle.Asn1.Cms.AttributeTable baseSignedTable;
		}
	}
}
