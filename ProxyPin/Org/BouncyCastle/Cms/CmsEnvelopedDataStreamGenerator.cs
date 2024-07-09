using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002E7 RID: 743
	public class CmsEnvelopedDataStreamGenerator : CmsEnvelopedGenerator
	{
		// Token: 0x0600165C RID: 5724 RVA: 0x00074A48 File Offset: 0x00074A48
		public CmsEnvelopedDataStreamGenerator()
		{
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x00074A60 File Offset: 0x00074A60
		public CmsEnvelopedDataStreamGenerator(SecureRandom rand) : base(rand)
		{
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x00074A78 File Offset: 0x00074A78
		public void SetBufferSize(int bufferSize)
		{
			this._bufferSize = bufferSize;
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x00074A84 File Offset: 0x00074A84
		public void SetBerEncodeRecipients(bool berEncodeRecipientSet)
		{
			this._berEncodeRecipientSet = berEncodeRecipientSet;
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x00074A90 File Offset: 0x00074A90
		private DerInteger Version
		{
			get
			{
				int value = (this._originatorInfo != null || this._unprotectedAttributes != null) ? 2 : 0;
				return new DerInteger(value);
			}
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x00074AC8 File Offset: 0x00074AC8
		private Stream Open(Stream outStream, string encryptionOid, CipherKeyGenerator keyGen)
		{
			byte[] array = keyGen.GenerateKey();
			KeyParameter keyParameter = ParameterUtilities.CreateKeyParameter(encryptionOid, array);
			Asn1Encodable asn1Params = this.GenerateAsn1Parameters(encryptionOid, array);
			ICipherParameters cipherParameters;
			AlgorithmIdentifier algorithmIdentifier = this.GetAlgorithmIdentifier(encryptionOid, keyParameter, asn1Params, out cipherParameters);
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			foreach (object obj in this.recipientInfoGenerators)
			{
				RecipientInfoGenerator recipientInfoGenerator = (RecipientInfoGenerator)obj;
				try
				{
					asn1EncodableVector.Add(recipientInfoGenerator.Generate(keyParameter, this.rand));
				}
				catch (InvalidKeyException e)
				{
					throw new CmsException("key inappropriate for algorithm.", e);
				}
				catch (GeneralSecurityException e2)
				{
					throw new CmsException("error making encrypted content.", e2);
				}
			}
			return this.Open(outStream, algorithmIdentifier, cipherParameters, asn1EncodableVector);
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x00074BB4 File Offset: 0x00074BB4
		private Stream Open(Stream outStream, AlgorithmIdentifier encAlgID, ICipherParameters cipherParameters, Asn1EncodableVector recipientInfos)
		{
			Stream result;
			try
			{
				BerSequenceGenerator berSequenceGenerator = new BerSequenceGenerator(outStream);
				berSequenceGenerator.AddObject(CmsObjectIdentifiers.EnvelopedData);
				BerSequenceGenerator berSequenceGenerator2 = new BerSequenceGenerator(berSequenceGenerator.GetRawOutputStream(), 0, true);
				berSequenceGenerator2.AddObject(this.Version);
				Stream rawOutputStream = berSequenceGenerator2.GetRawOutputStream();
				Asn1Generator asn1Generator = this._berEncodeRecipientSet ? new BerSetGenerator(rawOutputStream) : new DerSetGenerator(rawOutputStream);
				foreach (object obj in recipientInfos)
				{
					Asn1Encodable obj2 = (Asn1Encodable)obj;
					asn1Generator.AddObject(obj2);
				}
				asn1Generator.Close();
				BerSequenceGenerator berSequenceGenerator3 = new BerSequenceGenerator(rawOutputStream);
				berSequenceGenerator3.AddObject(CmsObjectIdentifiers.Data);
				berSequenceGenerator3.AddObject(encAlgID);
				Stream stream = CmsUtilities.CreateBerOctetOutputStream(berSequenceGenerator3.GetRawOutputStream(), 0, false, this._bufferSize);
				IBufferedCipher cipher = CipherUtilities.GetCipher(encAlgID.Algorithm);
				cipher.Init(true, new ParametersWithRandom(cipherParameters, this.rand));
				CipherStream outStream2 = new CipherStream(stream, null, cipher);
				result = new CmsEnvelopedDataStreamGenerator.CmsEnvelopedDataOutputStream(this, outStream2, berSequenceGenerator, berSequenceGenerator2, berSequenceGenerator3);
			}
			catch (SecurityUtilityException e)
			{
				throw new CmsException("couldn't create cipher.", e);
			}
			catch (InvalidKeyException e2)
			{
				throw new CmsException("key invalid in message.", e2);
			}
			catch (IOException e3)
			{
				throw new CmsException("exception decoding algorithm parameters.", e3);
			}
			return result;
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x00074D68 File Offset: 0x00074D68
		public Stream Open(Stream outStream, string encryptionOid)
		{
			CipherKeyGenerator keyGenerator = GeneratorUtilities.GetKeyGenerator(encryptionOid);
			keyGenerator.Init(new KeyGenerationParameters(this.rand, keyGenerator.DefaultStrength));
			return this.Open(outStream, encryptionOid, keyGenerator);
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x00074DA0 File Offset: 0x00074DA0
		public Stream Open(Stream outStream, string encryptionOid, int keySize)
		{
			CipherKeyGenerator keyGenerator = GeneratorUtilities.GetKeyGenerator(encryptionOid);
			keyGenerator.Init(new KeyGenerationParameters(this.rand, keySize));
			return this.Open(outStream, encryptionOid, keyGenerator);
		}

		// Token: 0x04000F38 RID: 3896
		private object _originatorInfo = null;

		// Token: 0x04000F39 RID: 3897
		private object _unprotectedAttributes = null;

		// Token: 0x04000F3A RID: 3898
		private int _bufferSize;

		// Token: 0x04000F3B RID: 3899
		private bool _berEncodeRecipientSet;

		// Token: 0x02000DDC RID: 3548
		private class CmsEnvelopedDataOutputStream : BaseOutputStream
		{
			// Token: 0x06008B74 RID: 35700 RVA: 0x0029DA78 File Offset: 0x0029DA78
			public CmsEnvelopedDataOutputStream(CmsEnvelopedGenerator outer, CipherStream outStream, BerSequenceGenerator cGen, BerSequenceGenerator envGen, BerSequenceGenerator eiGen)
			{
				this._outer = outer;
				this._out = outStream;
				this._cGen = cGen;
				this._envGen = envGen;
				this._eiGen = eiGen;
			}

			// Token: 0x06008B75 RID: 35701 RVA: 0x0029DAA8 File Offset: 0x0029DAA8
			public override void WriteByte(byte b)
			{
				this._out.WriteByte(b);
			}

			// Token: 0x06008B76 RID: 35702 RVA: 0x0029DAB8 File Offset: 0x0029DAB8
			public override void Write(byte[] bytes, int off, int len)
			{
				this._out.Write(bytes, off, len);
			}

			// Token: 0x06008B77 RID: 35703 RVA: 0x0029DAC8 File Offset: 0x0029DAC8
			public override void Close()
			{
				Platform.Dispose(this._out);
				this._eiGen.Close();
				if (this._outer.unprotectedAttributeGenerator != null)
				{
					Org.BouncyCastle.Asn1.Cms.AttributeTable attributes = this._outer.unprotectedAttributeGenerator.GetAttributes(Platform.CreateHashtable());
					Asn1Set obj = new BerSet(attributes.ToAsn1EncodableVector());
					this._envGen.AddObject(new DerTaggedObject(false, 1, obj));
				}
				this._envGen.Close();
				this._cGen.Close();
				base.Close();
			}

			// Token: 0x04004075 RID: 16501
			private readonly CmsEnvelopedGenerator _outer;

			// Token: 0x04004076 RID: 16502
			private readonly CipherStream _out;

			// Token: 0x04004077 RID: 16503
			private readonly BerSequenceGenerator _cGen;

			// Token: 0x04004078 RID: 16504
			private readonly BerSequenceGenerator _envGen;

			// Token: 0x04004079 RID: 16505
			private readonly BerSequenceGenerator _eiGen;
		}
	}
}
