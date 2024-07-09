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
	// Token: 0x020002DC RID: 732
	public class CmsAuthenticatedDataStreamGenerator : CmsAuthenticatedGenerator
	{
		// Token: 0x06001626 RID: 5670 RVA: 0x00073C04 File Offset: 0x00073C04
		public CmsAuthenticatedDataStreamGenerator()
		{
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x00073C0C File Offset: 0x00073C0C
		public CmsAuthenticatedDataStreamGenerator(SecureRandom rand) : base(rand)
		{
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x00073C18 File Offset: 0x00073C18
		public void SetBufferSize(int bufferSize)
		{
			this._bufferSize = bufferSize;
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x00073C24 File Offset: 0x00073C24
		public void SetBerEncodeRecipients(bool berEncodeRecipientSet)
		{
			this._berEncodeRecipientSet = berEncodeRecipientSet;
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x00073C30 File Offset: 0x00073C30
		private Stream Open(Stream outStr, string macOid, CipherKeyGenerator keyGen)
		{
			byte[] array = keyGen.GenerateKey();
			KeyParameter keyParameter = ParameterUtilities.CreateKeyParameter(macOid, array);
			Asn1Encodable asn1Params = this.GenerateAsn1Parameters(macOid, array);
			ICipherParameters cipherParameters;
			AlgorithmIdentifier algorithmIdentifier = this.GetAlgorithmIdentifier(macOid, keyParameter, asn1Params, out cipherParameters);
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
			return this.Open(outStr, algorithmIdentifier, keyParameter, asn1EncodableVector);
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00073D1C File Offset: 0x00073D1C
		protected Stream Open(Stream outStr, AlgorithmIdentifier macAlgId, ICipherParameters cipherParameters, Asn1EncodableVector recipientInfos)
		{
			Stream result;
			try
			{
				BerSequenceGenerator berSequenceGenerator = new BerSequenceGenerator(outStr);
				berSequenceGenerator.AddObject(CmsObjectIdentifiers.AuthenticatedData);
				BerSequenceGenerator berSequenceGenerator2 = new BerSequenceGenerator(berSequenceGenerator.GetRawOutputStream(), 0, true);
				berSequenceGenerator2.AddObject(new DerInteger(AuthenticatedData.CalculateVersion(null)));
				Stream rawOutputStream = berSequenceGenerator2.GetRawOutputStream();
				Asn1Generator asn1Generator = this._berEncodeRecipientSet ? new BerSetGenerator(rawOutputStream) : new DerSetGenerator(rawOutputStream);
				foreach (object obj in recipientInfos)
				{
					Asn1Encodable obj2 = (Asn1Encodable)obj;
					asn1Generator.AddObject(obj2);
				}
				asn1Generator.Close();
				berSequenceGenerator2.AddObject(macAlgId);
				BerSequenceGenerator berSequenceGenerator3 = new BerSequenceGenerator(rawOutputStream);
				berSequenceGenerator3.AddObject(CmsObjectIdentifiers.Data);
				Stream output = CmsUtilities.CreateBerOctetOutputStream(berSequenceGenerator3.GetRawOutputStream(), 0, false, this._bufferSize);
				IMac mac = MacUtilities.GetMac(macAlgId.Algorithm);
				mac.Init(cipherParameters);
				Stream macStream = new TeeOutputStream(output, new MacSink(mac));
				result = new CmsAuthenticatedDataStreamGenerator.CmsAuthenticatedDataOutputStream(macStream, mac, berSequenceGenerator, berSequenceGenerator2, berSequenceGenerator3);
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

		// Token: 0x0600162C RID: 5676 RVA: 0x00073ECC File Offset: 0x00073ECC
		public Stream Open(Stream outStr, string encryptionOid)
		{
			CipherKeyGenerator keyGenerator = GeneratorUtilities.GetKeyGenerator(encryptionOid);
			keyGenerator.Init(new KeyGenerationParameters(this.rand, keyGenerator.DefaultStrength));
			return this.Open(outStr, encryptionOid, keyGenerator);
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x00073F04 File Offset: 0x00073F04
		public Stream Open(Stream outStr, string encryptionOid, int keySize)
		{
			CipherKeyGenerator keyGenerator = GeneratorUtilities.GetKeyGenerator(encryptionOid);
			keyGenerator.Init(new KeyGenerationParameters(this.rand, keySize));
			return this.Open(outStr, encryptionOid, keyGenerator);
		}

		// Token: 0x04000F1C RID: 3868
		private int _bufferSize;

		// Token: 0x04000F1D RID: 3869
		private bool _berEncodeRecipientSet;

		// Token: 0x02000DD9 RID: 3545
		private class CmsAuthenticatedDataOutputStream : BaseOutputStream
		{
			// Token: 0x06008B68 RID: 35688 RVA: 0x0029D91C File Offset: 0x0029D91C
			public CmsAuthenticatedDataOutputStream(Stream macStream, IMac mac, BerSequenceGenerator cGen, BerSequenceGenerator authGen, BerSequenceGenerator eiGen)
			{
				this.macStream = macStream;
				this.mac = mac;
				this.cGen = cGen;
				this.authGen = authGen;
				this.eiGen = eiGen;
			}

			// Token: 0x06008B69 RID: 35689 RVA: 0x0029D94C File Offset: 0x0029D94C
			public override void WriteByte(byte b)
			{
				this.macStream.WriteByte(b);
			}

			// Token: 0x06008B6A RID: 35690 RVA: 0x0029D95C File Offset: 0x0029D95C
			public override void Write(byte[] bytes, int off, int len)
			{
				this.macStream.Write(bytes, off, len);
			}

			// Token: 0x06008B6B RID: 35691 RVA: 0x0029D96C File Offset: 0x0029D96C
			public override void Close()
			{
				Platform.Dispose(this.macStream);
				this.eiGen.Close();
				byte[] str = MacUtilities.DoFinal(this.mac);
				this.authGen.AddObject(new DerOctetString(str));
				this.authGen.Close();
				this.cGen.Close();
				base.Close();
			}

			// Token: 0x0400406B RID: 16491
			private readonly Stream macStream;

			// Token: 0x0400406C RID: 16492
			private readonly IMac mac;

			// Token: 0x0400406D RID: 16493
			private readonly BerSequenceGenerator cGen;

			// Token: 0x0400406E RID: 16494
			private readonly BerSequenceGenerator authGen;

			// Token: 0x0400406F RID: 16495
			private readonly BerSequenceGenerator eiGen;
		}
	}
}
