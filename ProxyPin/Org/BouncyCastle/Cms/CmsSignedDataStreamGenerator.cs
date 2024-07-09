using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.IO;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002F4 RID: 756
	public class CmsSignedDataStreamGenerator : CmsSignedGenerator
	{
		// Token: 0x060016DC RID: 5852 RVA: 0x0007684C File Offset: 0x0007684C
		public CmsSignedDataStreamGenerator()
		{
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x00076880 File Offset: 0x00076880
		public CmsSignedDataStreamGenerator(SecureRandom rand) : base(rand)
		{
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x000768B8 File Offset: 0x000768B8
		public void SetBufferSize(int bufferSize)
		{
			this._bufferSize = bufferSize;
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x000768C4 File Offset: 0x000768C4
		public void AddDigests(params string[] digestOids)
		{
			this.AddDigests(digestOids);
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x000768D0 File Offset: 0x000768D0
		public void AddDigests(IEnumerable digestOids)
		{
			foreach (object obj in digestOids)
			{
				string digestOid = (string)obj;
				this.ConfigureDigest(digestOid);
			}
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x00076930 File Offset: 0x00076930
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string digestOid)
		{
			this.AddSigner(privateKey, cert, digestOid, new DefaultSignedAttributeTableGenerator(), null);
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00076944 File Offset: 0x00076944
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string encryptionOid, string digestOid)
		{
			this.AddSigner(privateKey, cert, encryptionOid, digestOid, new DefaultSignedAttributeTableGenerator(), null);
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x00076958 File Offset: 0x00076958
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string digestOid, Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.AddSigner(privateKey, cert, digestOid, new DefaultSignedAttributeTableGenerator(signedAttr), new SimpleAttributeTableGenerator(unsignedAttr));
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00076974 File Offset: 0x00076974
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string encryptionOid, string digestOid, Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.AddSigner(privateKey, cert, encryptionOid, digestOid, new DefaultSignedAttributeTableGenerator(signedAttr), new SimpleAttributeTableGenerator(unsignedAttr));
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x00076990 File Offset: 0x00076990
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string digestOid, CmsAttributeTableGenerator signedAttrGenerator, CmsAttributeTableGenerator unsignedAttrGenerator)
		{
			this.AddSigner(privateKey, cert, CmsSignedDataStreamGenerator.Helper.GetEncOid(privateKey, digestOid), digestOid, signedAttrGenerator, unsignedAttrGenerator);
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x000769BC File Offset: 0x000769BC
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string encryptionOid, string digestOid, CmsAttributeTableGenerator signedAttrGenerator, CmsAttributeTableGenerator unsignedAttrGenerator)
		{
			this.DoAddSigner(privateKey, CmsSignedGenerator.GetSignerIdentifier(cert), encryptionOid, digestOid, signedAttrGenerator, unsignedAttrGenerator);
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x000769D4 File Offset: 0x000769D4
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string digestOid)
		{
			this.AddSigner(privateKey, subjectKeyID, digestOid, new DefaultSignedAttributeTableGenerator(), null);
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x000769E8 File Offset: 0x000769E8
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string encryptionOid, string digestOid)
		{
			this.AddSigner(privateKey, subjectKeyID, encryptionOid, digestOid, new DefaultSignedAttributeTableGenerator(), null);
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x000769FC File Offset: 0x000769FC
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string digestOid, Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.AddSigner(privateKey, subjectKeyID, digestOid, new DefaultSignedAttributeTableGenerator(signedAttr), new SimpleAttributeTableGenerator(unsignedAttr));
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x00076A18 File Offset: 0x00076A18
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string digestOid, CmsAttributeTableGenerator signedAttrGenerator, CmsAttributeTableGenerator unsignedAttrGenerator)
		{
			this.AddSigner(privateKey, subjectKeyID, CmsSignedDataStreamGenerator.Helper.GetEncOid(privateKey, digestOid), digestOid, signedAttrGenerator, unsignedAttrGenerator);
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x00076A44 File Offset: 0x00076A44
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string encryptionOid, string digestOid, CmsAttributeTableGenerator signedAttrGenerator, CmsAttributeTableGenerator unsignedAttrGenerator)
		{
			this.DoAddSigner(privateKey, CmsSignedGenerator.GetSignerIdentifier(subjectKeyID), encryptionOid, digestOid, signedAttrGenerator, unsignedAttrGenerator);
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x00076A5C File Offset: 0x00076A5C
		private void DoAddSigner(AsymmetricKeyParameter privateKey, SignerIdentifier signerIdentifier, string encryptionOid, string digestOid, CmsAttributeTableGenerator signedAttrGenerator, CmsAttributeTableGenerator unsignedAttrGenerator)
		{
			this.ConfigureDigest(digestOid);
			CmsSignedDataStreamGenerator.SignerInfoGeneratorImpl signerInf = new CmsSignedDataStreamGenerator.SignerInfoGeneratorImpl(this, privateKey, signerIdentifier, digestOid, encryptionOid, signedAttrGenerator, unsignedAttrGenerator);
			this._signerInfs.Add(new CmsSignedDataStreamGenerator.DigestAndSignerInfoGeneratorHolder(signerInf, digestOid));
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x00076A9C File Offset: 0x00076A9C
		internal override void AddSignerCallback(SignerInformation si)
		{
			this.RegisterDigestOid(si.DigestAlgorithmID.Algorithm.Id);
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x00076AC4 File Offset: 0x00076AC4
		public Stream Open(Stream outStream)
		{
			return this.Open(outStream, false);
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x00076AD0 File Offset: 0x00076AD0
		public Stream Open(Stream outStream, bool encapsulate)
		{
			return this.Open(outStream, CmsSignedGenerator.Data, encapsulate);
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00076AE0 File Offset: 0x00076AE0
		public Stream Open(Stream outStream, bool encapsulate, Stream dataOutputStream)
		{
			return this.Open(outStream, CmsSignedGenerator.Data, encapsulate, dataOutputStream);
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x00076AF0 File Offset: 0x00076AF0
		public Stream Open(Stream outStream, string signedContentType, bool encapsulate)
		{
			return this.Open(outStream, signedContentType, encapsulate, null);
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x00076AFC File Offset: 0x00076AFC
		public Stream Open(Stream outStream, string signedContentType, bool encapsulate, Stream dataOutputStream)
		{
			if (outStream == null)
			{
				throw new ArgumentNullException("outStream");
			}
			if (!outStream.CanWrite)
			{
				throw new ArgumentException("Expected writeable stream", "outStream");
			}
			if (dataOutputStream != null && !dataOutputStream.CanWrite)
			{
				throw new ArgumentException("Expected writeable stream", "dataOutputStream");
			}
			this._messageDigestsLocked = true;
			BerSequenceGenerator berSequenceGenerator = new BerSequenceGenerator(outStream);
			berSequenceGenerator.AddObject(CmsObjectIdentifiers.SignedData);
			BerSequenceGenerator berSequenceGenerator2 = new BerSequenceGenerator(berSequenceGenerator.GetRawOutputStream(), 0, true);
			DerObjectIdentifier derObjectIdentifier = (signedContentType == null) ? null : new DerObjectIdentifier(signedContentType);
			berSequenceGenerator2.AddObject(this.CalculateVersion(derObjectIdentifier));
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			foreach (object obj in this._messageDigestOids)
			{
				string identifier = (string)obj;
				asn1EncodableVector.Add(new AlgorithmIdentifier(new DerObjectIdentifier(identifier), DerNull.Instance));
			}
			byte[] encoded = new DerSet(asn1EncodableVector).GetEncoded();
			berSequenceGenerator2.GetRawOutputStream().Write(encoded, 0, encoded.Length);
			BerSequenceGenerator berSequenceGenerator3 = new BerSequenceGenerator(berSequenceGenerator2.GetRawOutputStream());
			berSequenceGenerator3.AddObject(derObjectIdentifier);
			Stream s = encapsulate ? CmsUtilities.CreateBerOctetOutputStream(berSequenceGenerator3.GetRawOutputStream(), 0, true, this._bufferSize) : null;
			Stream safeTeeOutputStream = CmsSignedDataStreamGenerator.GetSafeTeeOutputStream(dataOutputStream, s);
			Stream outStream2 = CmsSignedDataStreamGenerator.AttachDigestsToOutputStream(this._messageDigests.Values, safeTeeOutputStream);
			return new CmsSignedDataStreamGenerator.CmsSignedDataOutputStream(this, outStream2, signedContentType, berSequenceGenerator, berSequenceGenerator2, berSequenceGenerator3);
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x00076C9C File Offset: 0x00076C9C
		private void RegisterDigestOid(string digestOid)
		{
			if (this._messageDigestsLocked)
			{
				if (!this._messageDigestOids.Contains(digestOid))
				{
					throw new InvalidOperationException("Cannot register new digest OIDs after the data stream is opened");
				}
			}
			else
			{
				this._messageDigestOids.Add(digestOid);
			}
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x00076CD4 File Offset: 0x00076CD4
		private void ConfigureDigest(string digestOid)
		{
			this.RegisterDigestOid(digestOid);
			string digestAlgName = CmsSignedDataStreamGenerator.Helper.GetDigestAlgName(digestOid);
			if ((IDigest)this._messageDigests[digestAlgName] == null)
			{
				if (this._messageDigestsLocked)
				{
					throw new InvalidOperationException("Cannot configure new digests after the data stream is opened");
				}
				IDigest digestInstance = CmsSignedDataStreamGenerator.Helper.GetDigestInstance(digestAlgName);
				this._messageDigests[digestAlgName] = digestInstance;
			}
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x00076D40 File Offset: 0x00076D40
		internal void Generate(Stream outStream, string eContentType, bool encapsulate, Stream dataOutputStream, CmsProcessable content)
		{
			Stream stream = this.Open(outStream, eContentType, encapsulate, dataOutputStream);
			if (content != null)
			{
				content.Write(stream);
			}
			Platform.Dispose(stream);
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x00076D74 File Offset: 0x00076D74
		private DerInteger CalculateVersion(DerObjectIdentifier contentOid)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			if (this._certs != null)
			{
				foreach (object obj in this._certs)
				{
					if (obj is Asn1TaggedObject)
					{
						Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
						if (asn1TaggedObject.TagNo == 1)
						{
							flag3 = true;
						}
						else if (asn1TaggedObject.TagNo == 2)
						{
							flag4 = true;
						}
						else if (asn1TaggedObject.TagNo == 3)
						{
							flag = true;
							break;
						}
					}
				}
			}
			if (flag)
			{
				return new DerInteger(5);
			}
			if (this._crls != null)
			{
				foreach (object obj2 in this._crls)
				{
					if (obj2 is Asn1TaggedObject)
					{
						flag2 = true;
						break;
					}
				}
			}
			if (flag2)
			{
				return new DerInteger(5);
			}
			if (flag4)
			{
				return new DerInteger(4);
			}
			if (flag3 || !CmsObjectIdentifiers.Data.Equals(contentOid) || this.CheckForVersion3(this._signers))
			{
				return new DerInteger(3);
			}
			return new DerInteger(1);
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00076EFC File Offset: 0x00076EFC
		private bool CheckForVersion3(IList signerInfos)
		{
			foreach (object obj in signerInfos)
			{
				SignerInformation signerInformation = (SignerInformation)obj;
				SignerInfo instance = SignerInfo.GetInstance(signerInformation.ToSignerInfo());
				if (instance.Version.IntValueExact == 3)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x00076F7C File Offset: 0x00076F7C
		private static Stream AttachDigestsToOutputStream(ICollection digests, Stream s)
		{
			Stream stream = s;
			foreach (object obj in digests)
			{
				IDigest digest = (IDigest)obj;
				stream = CmsSignedDataStreamGenerator.GetSafeTeeOutputStream(stream, new DigestSink(digest));
			}
			return stream;
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00076FE4 File Offset: 0x00076FE4
		private static Stream GetSafeOutputStream(Stream s)
		{
			if (s == null)
			{
				return new NullOutputStream();
			}
			return s;
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x00076FF4 File Offset: 0x00076FF4
		private static Stream GetSafeTeeOutputStream(Stream s1, Stream s2)
		{
			if (s1 == null)
			{
				return CmsSignedDataStreamGenerator.GetSafeOutputStream(s2);
			}
			if (s2 == null)
			{
				return CmsSignedDataStreamGenerator.GetSafeOutputStream(s1);
			}
			return new TeeOutputStream(s1, s2);
		}

		// Token: 0x04000F79 RID: 3961
		private static readonly CmsSignedHelper Helper = CmsSignedHelper.Instance;

		// Token: 0x04000F7A RID: 3962
		private readonly IList _signerInfs = Platform.CreateArrayList();

		// Token: 0x04000F7B RID: 3963
		private readonly ISet _messageDigestOids = new HashSet();

		// Token: 0x04000F7C RID: 3964
		private readonly IDictionary _messageDigests = Platform.CreateHashtable();

		// Token: 0x04000F7D RID: 3965
		private readonly IDictionary _messageHashes = Platform.CreateHashtable();

		// Token: 0x04000F7E RID: 3966
		private bool _messageDigestsLocked;

		// Token: 0x04000F7F RID: 3967
		private int _bufferSize;

		// Token: 0x02000DE0 RID: 3552
		private class DigestAndSignerInfoGeneratorHolder
		{
			// Token: 0x06008B86 RID: 35718 RVA: 0x0029E120 File Offset: 0x0029E120
			internal DigestAndSignerInfoGeneratorHolder(ISignerInfoGenerator signerInf, string digestOID)
			{
				this.signerInf = signerInf;
				this.digestOID = digestOID;
			}

			// Token: 0x17001D70 RID: 7536
			// (get) Token: 0x06008B87 RID: 35719 RVA: 0x0029E138 File Offset: 0x0029E138
			internal AlgorithmIdentifier DigestAlgorithm
			{
				get
				{
					return new AlgorithmIdentifier(new DerObjectIdentifier(this.digestOID), DerNull.Instance);
				}
			}

			// Token: 0x04004088 RID: 16520
			internal readonly ISignerInfoGenerator signerInf;

			// Token: 0x04004089 RID: 16521
			internal readonly string digestOID;
		}

		// Token: 0x02000DE1 RID: 3553
		private class SignerInfoGeneratorImpl : ISignerInfoGenerator
		{
			// Token: 0x06008B88 RID: 35720 RVA: 0x0029E150 File Offset: 0x0029E150
			internal SignerInfoGeneratorImpl(CmsSignedDataStreamGenerator outer, AsymmetricKeyParameter key, SignerIdentifier signerIdentifier, string digestOID, string encOID, CmsAttributeTableGenerator sAttr, CmsAttributeTableGenerator unsAttr)
			{
				this.outer = outer;
				this._signerIdentifier = signerIdentifier;
				this._digestOID = digestOID;
				this._encOID = encOID;
				this._sAttr = sAttr;
				this._unsAttr = unsAttr;
				this._encName = CmsSignedDataStreamGenerator.Helper.GetEncryptionAlgName(this._encOID);
				string digestAlgName = CmsSignedDataStreamGenerator.Helper.GetDigestAlgName(this._digestOID);
				string algorithm = digestAlgName + "with" + this._encName;
				if (this._sAttr != null)
				{
					this._sig = CmsSignedDataStreamGenerator.Helper.GetSignatureInstance(algorithm);
				}
				else if (this._encName.Equals("RSA"))
				{
					this._sig = CmsSignedDataStreamGenerator.Helper.GetSignatureInstance("RSA");
				}
				else
				{
					if (!this._encName.Equals("DSA"))
					{
						throw new SignatureException("algorithm: " + this._encName + " not supported in base signatures.");
					}
					this._sig = CmsSignedDataStreamGenerator.Helper.GetSignatureInstance("NONEwithDSA");
				}
				this._sig.Init(true, new ParametersWithRandom(key, outer.rand));
			}

			// Token: 0x06008B89 RID: 35721 RVA: 0x0029E280 File Offset: 0x0029E280
			public SignerInfo Generate(DerObjectIdentifier contentType, AlgorithmIdentifier digestAlgorithm, byte[] calculatedDigest)
			{
				SignerInfo result;
				try
				{
					string digestAlgName = CmsSignedDataStreamGenerator.Helper.GetDigestAlgName(this._digestOID);
					string algorithm = digestAlgName + "with" + this._encName;
					byte[] array = calculatedDigest;
					Asn1Set asn1Set = null;
					if (this._sAttr != null)
					{
						IDictionary baseParameters = this.outer.GetBaseParameters(contentType, digestAlgorithm, calculatedDigest);
						Org.BouncyCastle.Asn1.Cms.AttributeTable attributeTable = this._sAttr.GetAttributes(baseParameters);
						if (contentType == null && attributeTable != null && attributeTable[CmsAttributes.ContentType] != null)
						{
							IDictionary dictionary = attributeTable.ToDictionary();
							dictionary.Remove(CmsAttributes.ContentType);
							attributeTable = new Org.BouncyCastle.Asn1.Cms.AttributeTable(dictionary);
						}
						asn1Set = this.outer.GetAttributeSet(attributeTable);
						array = asn1Set.GetEncoded("DER");
					}
					else if (this._encName.Equals("RSA"))
					{
						DigestInfo digestInfo = new DigestInfo(digestAlgorithm, calculatedDigest);
						array = digestInfo.GetEncoded("DER");
					}
					this._sig.BlockUpdate(array, 0, array.Length);
					byte[] array2 = this._sig.GenerateSignature();
					Asn1Set unauthenticatedAttributes = null;
					if (this._unsAttr != null)
					{
						IDictionary baseParameters2 = this.outer.GetBaseParameters(contentType, digestAlgorithm, calculatedDigest);
						baseParameters2[CmsAttributeTableParameter.Signature] = array2.Clone();
						Org.BouncyCastle.Asn1.Cms.AttributeTable attributes = this._unsAttr.GetAttributes(baseParameters2);
						unauthenticatedAttributes = this.outer.GetAttributeSet(attributes);
					}
					Asn1Encodable defaultX509Parameters = SignerUtilities.GetDefaultX509Parameters(algorithm);
					AlgorithmIdentifier encAlgorithmIdentifier = CmsSignedDataStreamGenerator.Helper.GetEncAlgorithmIdentifier(new DerObjectIdentifier(this._encOID), defaultX509Parameters);
					result = new SignerInfo(this._signerIdentifier, digestAlgorithm, asn1Set, encAlgorithmIdentifier, new DerOctetString(array2), unauthenticatedAttributes);
				}
				catch (IOException e)
				{
					throw new CmsStreamException("encoding error.", e);
				}
				catch (SignatureException e2)
				{
					throw new CmsStreamException("error creating signature.", e2);
				}
				return result;
			}

			// Token: 0x0400408A RID: 16522
			private readonly CmsSignedDataStreamGenerator outer;

			// Token: 0x0400408B RID: 16523
			private readonly SignerIdentifier _signerIdentifier;

			// Token: 0x0400408C RID: 16524
			private readonly string _digestOID;

			// Token: 0x0400408D RID: 16525
			private readonly string _encOID;

			// Token: 0x0400408E RID: 16526
			private readonly CmsAttributeTableGenerator _sAttr;

			// Token: 0x0400408F RID: 16527
			private readonly CmsAttributeTableGenerator _unsAttr;

			// Token: 0x04004090 RID: 16528
			private readonly string _encName;

			// Token: 0x04004091 RID: 16529
			private readonly ISigner _sig;
		}

		// Token: 0x02000DE2 RID: 3554
		private class CmsSignedDataOutputStream : BaseOutputStream
		{
			// Token: 0x06008B8A RID: 35722 RVA: 0x0029E468 File Offset: 0x0029E468
			public CmsSignedDataOutputStream(CmsSignedDataStreamGenerator outer, Stream outStream, string contentOID, BerSequenceGenerator sGen, BerSequenceGenerator sigGen, BerSequenceGenerator eiGen)
			{
				this.outer = outer;
				this._out = outStream;
				this._contentOID = new DerObjectIdentifier(contentOID);
				this._sGen = sGen;
				this._sigGen = sigGen;
				this._eiGen = eiGen;
			}

			// Token: 0x06008B8B RID: 35723 RVA: 0x0029E4A4 File Offset: 0x0029E4A4
			public override void WriteByte(byte b)
			{
				this._out.WriteByte(b);
			}

			// Token: 0x06008B8C RID: 35724 RVA: 0x0029E4B4 File Offset: 0x0029E4B4
			public override void Write(byte[] bytes, int off, int len)
			{
				this._out.Write(bytes, off, len);
			}

			// Token: 0x06008B8D RID: 35725 RVA: 0x0029E4C4 File Offset: 0x0029E4C4
			public override void Close()
			{
				this.DoClose();
				base.Close();
			}

			// Token: 0x06008B8E RID: 35726 RVA: 0x0029E4D4 File Offset: 0x0029E4D4
			private void DoClose()
			{
				Platform.Dispose(this._out);
				this._eiGen.Close();
				this.outer._digests.Clear();
				if (this.outer._certs.Count > 0)
				{
					Asn1Set obj = this.outer.UseDerForCerts ? CmsUtilities.CreateDerSetFromList(this.outer._certs) : CmsUtilities.CreateBerSetFromList(this.outer._certs);
					CmsSignedDataStreamGenerator.CmsSignedDataOutputStream.WriteToGenerator(this._sigGen, new BerTaggedObject(false, 0, obj));
				}
				if (this.outer._crls.Count > 0)
				{
					Asn1Set obj2 = this.outer.UseDerForCrls ? CmsUtilities.CreateDerSetFromList(this.outer._crls) : CmsUtilities.CreateBerSetFromList(this.outer._crls);
					CmsSignedDataStreamGenerator.CmsSignedDataOutputStream.WriteToGenerator(this._sigGen, new BerTaggedObject(false, 1, obj2));
				}
				foreach (object obj3 in this.outer._messageDigests)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
					this.outer._messageHashes.Add(dictionaryEntry.Key, DigestUtilities.DoFinal((IDigest)dictionaryEntry.Value));
				}
				Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
				foreach (object obj4 in this.outer._signerInfs)
				{
					CmsSignedDataStreamGenerator.DigestAndSignerInfoGeneratorHolder digestAndSignerInfoGeneratorHolder = (CmsSignedDataStreamGenerator.DigestAndSignerInfoGeneratorHolder)obj4;
					AlgorithmIdentifier digestAlgorithm = digestAndSignerInfoGeneratorHolder.DigestAlgorithm;
					byte[] array = (byte[])this.outer._messageHashes[CmsSignedDataStreamGenerator.Helper.GetDigestAlgName(digestAndSignerInfoGeneratorHolder.digestOID)];
					this.outer._digests[digestAndSignerInfoGeneratorHolder.digestOID] = array.Clone();
					asn1EncodableVector.Add(digestAndSignerInfoGeneratorHolder.signerInf.Generate(this._contentOID, digestAlgorithm, array));
				}
				foreach (object obj5 in this.outer._signers)
				{
					SignerInformation signerInformation = (SignerInformation)obj5;
					asn1EncodableVector.Add(signerInformation.ToSignerInfo());
				}
				CmsSignedDataStreamGenerator.CmsSignedDataOutputStream.WriteToGenerator(this._sigGen, new DerSet(asn1EncodableVector));
				this._sigGen.Close();
				this._sGen.Close();
			}

			// Token: 0x06008B8F RID: 35727 RVA: 0x0029E7A4 File Offset: 0x0029E7A4
			private static void WriteToGenerator(Asn1Generator ag, Asn1Encodable ae)
			{
				byte[] encoded = ae.GetEncoded();
				ag.GetRawOutputStream().Write(encoded, 0, encoded.Length);
			}

			// Token: 0x04004092 RID: 16530
			private readonly CmsSignedDataStreamGenerator outer;

			// Token: 0x04004093 RID: 16531
			private Stream _out;

			// Token: 0x04004094 RID: 16532
			private DerObjectIdentifier _contentOID;

			// Token: 0x04004095 RID: 16533
			private BerSequenceGenerator _sGen;

			// Token: 0x04004096 RID: 16534
			private BerSequenceGenerator _sigGen;

			// Token: 0x04004097 RID: 16535
			private BerSequenceGenerator _eiGen;
		}
	}
}
