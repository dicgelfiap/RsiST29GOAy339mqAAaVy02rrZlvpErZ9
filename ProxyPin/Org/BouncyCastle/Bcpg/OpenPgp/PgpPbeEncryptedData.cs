using System;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200065B RID: 1627
	public class PgpPbeEncryptedData : PgpEncryptedData
	{
		// Token: 0x06003868 RID: 14440 RVA: 0x0012EEBC File Offset: 0x0012EEBC
		internal PgpPbeEncryptedData(SymmetricKeyEncSessionPacket keyData, InputStreamPacket encData) : base(encData)
		{
			this.keyData = keyData;
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x0012EECC File Offset: 0x0012EECC
		public override Stream GetInputStream()
		{
			return this.encData.GetInputStream();
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x0012EEDC File Offset: 0x0012EEDC
		public Stream GetDataStream(char[] passPhrase)
		{
			return this.DoGetDataStream(PgpUtilities.EncodePassPhrase(passPhrase, false), true);
		}

		// Token: 0x0600386B RID: 14443 RVA: 0x0012EEEC File Offset: 0x0012EEEC
		public Stream GetDataStreamUtf8(char[] passPhrase)
		{
			return this.DoGetDataStream(PgpUtilities.EncodePassPhrase(passPhrase, true), true);
		}

		// Token: 0x0600386C RID: 14444 RVA: 0x0012EEFC File Offset: 0x0012EEFC
		public Stream GetDataStreamRaw(byte[] rawPassPhrase)
		{
			return this.DoGetDataStream(rawPassPhrase, false);
		}

		// Token: 0x0600386D RID: 14445 RVA: 0x0012EF08 File Offset: 0x0012EF08
		internal Stream DoGetDataStream(byte[] rawPassPhrase, bool clearPassPhrase)
		{
			Stream encStream;
			try
			{
				SymmetricKeyAlgorithmTag symmetricKeyAlgorithmTag = this.keyData.EncAlgorithm;
				KeyParameter parameters = PgpUtilities.DoMakeKeyFromPassPhrase(symmetricKeyAlgorithmTag, this.keyData.S2k, rawPassPhrase, clearPassPhrase);
				byte[] secKeyData = this.keyData.GetSecKeyData();
				if (secKeyData != null && secKeyData.Length > 0)
				{
					IBufferedCipher cipher = CipherUtilities.GetCipher(PgpUtilities.GetSymmetricCipherName(symmetricKeyAlgorithmTag) + "/CFB/NoPadding");
					cipher.Init(false, new ParametersWithIV(parameters, new byte[cipher.GetBlockSize()]));
					byte[] array = cipher.DoFinal(secKeyData);
					symmetricKeyAlgorithmTag = (SymmetricKeyAlgorithmTag)array[0];
					parameters = ParameterUtilities.CreateKeyParameter(PgpUtilities.GetSymmetricCipherName(symmetricKeyAlgorithmTag), array, 1, array.Length - 1);
				}
				IBufferedCipher bufferedCipher = this.CreateStreamCipher(symmetricKeyAlgorithmTag);
				byte[] array2 = new byte[bufferedCipher.GetBlockSize()];
				bufferedCipher.Init(false, new ParametersWithIV(parameters, array2));
				this.encStream = BcpgInputStream.Wrap(new CipherStream(this.encData.GetInputStream(), bufferedCipher, null));
				if (this.encData is SymmetricEncIntegrityPacket)
				{
					this.truncStream = new PgpEncryptedData.TruncatedStream(this.encStream);
					string digestName = PgpUtilities.GetDigestName(HashAlgorithmTag.Sha1);
					IDigest digest = DigestUtilities.GetDigest(digestName);
					this.encStream = new DigestStream(this.truncStream, digest, null);
				}
				if (Streams.ReadFully(this.encStream, array2, 0, array2.Length) < array2.Length)
				{
					throw new EndOfStreamException("unexpected end of stream.");
				}
				int num = this.encStream.ReadByte();
				int num2 = this.encStream.ReadByte();
				if (num < 0 || num2 < 0)
				{
					throw new EndOfStreamException("unexpected end of stream.");
				}
				bool flag = array2[array2.Length - 2] == (byte)num && array2[array2.Length - 1] == (byte)num2;
				bool flag2 = num == 0 && num2 == 0;
				if (!flag && !flag2)
				{
					throw new PgpDataValidationException("quick check failed.");
				}
				encStream = this.encStream;
			}
			catch (PgpException ex)
			{
				throw ex;
			}
			catch (Exception exception)
			{
				throw new PgpException("Exception creating cipher", exception);
			}
			return encStream;
		}

		// Token: 0x0600386E RID: 14446 RVA: 0x0012F134 File Offset: 0x0012F134
		private IBufferedCipher CreateStreamCipher(SymmetricKeyAlgorithmTag keyAlgorithm)
		{
			string str = (this.encData is SymmetricEncIntegrityPacket) ? "CFB" : "OpenPGPCFB";
			string algorithm = PgpUtilities.GetSymmetricCipherName(keyAlgorithm) + "/" + str + "/NoPadding";
			return CipherUtilities.GetCipher(algorithm);
		}

		// Token: 0x04001DD3 RID: 7635
		private readonly SymmetricKeyEncSessionPacket keyData;
	}
}
