using System;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000528 RID: 1320
	public class TlsAeadCipher : TlsCipher
	{
		// Token: 0x06002818 RID: 10264 RVA: 0x000D75D4 File Offset: 0x000D75D4
		public TlsAeadCipher(TlsContext context, IAeadBlockCipher clientWriteCipher, IAeadBlockCipher serverWriteCipher, int cipherKeySize, int macSize) : this(context, clientWriteCipher, serverWriteCipher, cipherKeySize, macSize, 1)
		{
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x000D75E4 File Offset: 0x000D75E4
		internal TlsAeadCipher(TlsContext context, IAeadBlockCipher clientWriteCipher, IAeadBlockCipher serverWriteCipher, int cipherKeySize, int macSize, int nonceMode)
		{
			if (!TlsUtilities.IsTlsV12(context))
			{
				throw new TlsFatalAlert(80);
			}
			this.nonceMode = nonceMode;
			int num;
			switch (nonceMode)
			{
			case 1:
				num = 4;
				this.record_iv_length = 8;
				break;
			case 2:
				num = 12;
				this.record_iv_length = 0;
				break;
			default:
				throw new TlsFatalAlert(80);
			}
			this.context = context;
			this.macSize = macSize;
			int num2 = 2 * cipherKeySize + 2 * num;
			byte[] array = TlsUtilities.CalculateKeyBlock(context, num2);
			int num3 = 0;
			KeyParameter keyParameter = new KeyParameter(array, num3, cipherKeySize);
			num3 += cipherKeySize;
			KeyParameter keyParameter2 = new KeyParameter(array, num3, cipherKeySize);
			num3 += cipherKeySize;
			byte[] array2 = Arrays.CopyOfRange(array, num3, num3 + num);
			num3 += num;
			byte[] array3 = Arrays.CopyOfRange(array, num3, num3 + num);
			num3 += num;
			if (num3 != num2)
			{
				throw new TlsFatalAlert(80);
			}
			KeyParameter key;
			KeyParameter key2;
			if (context.IsServer)
			{
				this.encryptCipher = serverWriteCipher;
				this.decryptCipher = clientWriteCipher;
				this.encryptImplicitNonce = array3;
				this.decryptImplicitNonce = array2;
				key = keyParameter2;
				key2 = keyParameter;
			}
			else
			{
				this.encryptCipher = clientWriteCipher;
				this.decryptCipher = serverWriteCipher;
				this.encryptImplicitNonce = array2;
				this.decryptImplicitNonce = array3;
				key = keyParameter;
				key2 = keyParameter2;
			}
			byte[] array4 = new byte[num + this.record_iv_length];
			array4[0] = ~this.encryptImplicitNonce[0];
			array4[1] = ~this.decryptImplicitNonce[1];
			this.encryptCipher.Init(true, new AeadParameters(key, 8 * macSize, array4));
			this.decryptCipher.Init(false, new AeadParameters(key2, 8 * macSize, array4));
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x000D7790 File Offset: 0x000D7790
		public virtual int GetPlaintextLimit(int ciphertextLimit)
		{
			return ciphertextLimit - this.macSize - this.record_iv_length;
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x000D77A4 File Offset: 0x000D77A4
		public virtual byte[] EncodePlaintext(long seqNo, byte type, byte[] plaintext, int offset, int len)
		{
			byte[] array = new byte[this.encryptImplicitNonce.Length + this.record_iv_length];
			switch (this.nonceMode)
			{
			case 1:
				Array.Copy(this.encryptImplicitNonce, 0, array, 0, this.encryptImplicitNonce.Length);
				TlsUtilities.WriteUint64(seqNo, array, this.encryptImplicitNonce.Length);
				break;
			case 2:
				TlsUtilities.WriteUint64(seqNo, array, array.Length - 8);
				for (int i = 0; i < this.encryptImplicitNonce.Length; i++)
				{
					byte[] array2;
					IntPtr intPtr;
					(array2 = array)[(int)(intPtr = (IntPtr)i)] = (array2[(int)intPtr] ^ this.encryptImplicitNonce[i]);
				}
				break;
			default:
				throw new TlsFatalAlert(80);
			}
			int outputSize = this.encryptCipher.GetOutputSize(len);
			byte[] array3 = new byte[this.record_iv_length + outputSize];
			if (this.record_iv_length != 0)
			{
				Array.Copy(array, array.Length - this.record_iv_length, array3, 0, this.record_iv_length);
			}
			int num = this.record_iv_length;
			byte[] additionalData = this.GetAdditionalData(seqNo, type, len);
			AeadParameters parameters = new AeadParameters(null, 8 * this.macSize, array, additionalData);
			try
			{
				this.encryptCipher.Init(true, parameters);
				num += this.encryptCipher.ProcessBytes(plaintext, offset, len, array3, num);
				num += this.encryptCipher.DoFinal(array3, num);
			}
			catch (Exception alertCause)
			{
				throw new TlsFatalAlert(80, alertCause);
			}
			if (num != array3.Length)
			{
				throw new TlsFatalAlert(80);
			}
			return array3;
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x000D7934 File Offset: 0x000D7934
		public virtual byte[] DecodeCiphertext(long seqNo, byte type, byte[] ciphertext, int offset, int len)
		{
			if (this.GetPlaintextLimit(len) < 0)
			{
				throw new TlsFatalAlert(50);
			}
			byte[] array = new byte[this.decryptImplicitNonce.Length + this.record_iv_length];
			switch (this.nonceMode)
			{
			case 1:
				Array.Copy(this.decryptImplicitNonce, 0, array, 0, this.decryptImplicitNonce.Length);
				Array.Copy(ciphertext, offset, array, array.Length - this.record_iv_length, this.record_iv_length);
				break;
			case 2:
				TlsUtilities.WriteUint64(seqNo, array, array.Length - 8);
				for (int i = 0; i < this.decryptImplicitNonce.Length; i++)
				{
					byte[] array2;
					IntPtr intPtr;
					(array2 = array)[(int)(intPtr = (IntPtr)i)] = (array2[(int)intPtr] ^ this.decryptImplicitNonce[i]);
				}
				break;
			default:
				throw new TlsFatalAlert(80);
			}
			int inOff = offset + this.record_iv_length;
			int len2 = len - this.record_iv_length;
			int outputSize = this.decryptCipher.GetOutputSize(len2);
			byte[] array3 = new byte[outputSize];
			int num = 0;
			byte[] additionalData = this.GetAdditionalData(seqNo, type, outputSize);
			AeadParameters parameters = new AeadParameters(null, 8 * this.macSize, array, additionalData);
			try
			{
				this.decryptCipher.Init(false, parameters);
				num += this.decryptCipher.ProcessBytes(ciphertext, inOff, len2, array3, num);
				num += this.decryptCipher.DoFinal(array3, num);
			}
			catch (Exception alertCause)
			{
				throw new TlsFatalAlert(20, alertCause);
			}
			if (num != array3.Length)
			{
				throw new TlsFatalAlert(80);
			}
			return array3;
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x000D7AC4 File Offset: 0x000D7AC4
		protected virtual byte[] GetAdditionalData(long seqNo, byte type, int len)
		{
			byte[] array = new byte[13];
			TlsUtilities.WriteUint64(seqNo, array, 0);
			TlsUtilities.WriteUint8(type, array, 8);
			TlsUtilities.WriteVersion(this.context.ServerVersion, array, 9);
			TlsUtilities.WriteUint16(len, array, 11);
			return array;
		}

		// Token: 0x04001A6D RID: 6765
		public const int NONCE_RFC5288 = 1;

		// Token: 0x04001A6E RID: 6766
		internal const int NONCE_DRAFT_CHACHA20_POLY1305 = 2;

		// Token: 0x04001A6F RID: 6767
		protected readonly TlsContext context;

		// Token: 0x04001A70 RID: 6768
		protected readonly int macSize;

		// Token: 0x04001A71 RID: 6769
		protected readonly int record_iv_length;

		// Token: 0x04001A72 RID: 6770
		protected readonly IAeadBlockCipher encryptCipher;

		// Token: 0x04001A73 RID: 6771
		protected readonly IAeadBlockCipher decryptCipher;

		// Token: 0x04001A74 RID: 6772
		protected readonly byte[] encryptImplicitNonce;

		// Token: 0x04001A75 RID: 6773
		protected readonly byte[] decryptImplicitNonce;

		// Token: 0x04001A76 RID: 6774
		protected readonly int nonceMode;
	}
}
