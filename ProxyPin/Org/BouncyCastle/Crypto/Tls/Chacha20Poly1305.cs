using System;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004D9 RID: 1241
	public class Chacha20Poly1305 : TlsCipher
	{
		// Token: 0x06002645 RID: 9797 RVA: 0x000D06C4 File Offset: 0x000D06C4
		public Chacha20Poly1305(TlsContext context)
		{
			if (!TlsUtilities.IsTlsV12(context))
			{
				throw new TlsFatalAlert(80);
			}
			this.context = context;
			int num = 32;
			int num2 = 12;
			int num3 = 2 * num + 2 * num2;
			byte[] array = TlsUtilities.CalculateKeyBlock(context, num3);
			int num4 = 0;
			KeyParameter keyParameter = new KeyParameter(array, num4, num);
			num4 += num;
			KeyParameter keyParameter2 = new KeyParameter(array, num4, num);
			num4 += num;
			byte[] array2 = Arrays.CopyOfRange(array, num4, num4 + num2);
			num4 += num2;
			byte[] array3 = Arrays.CopyOfRange(array, num4, num4 + num2);
			num4 += num2;
			if (num4 != num3)
			{
				throw new TlsFatalAlert(80);
			}
			this.encryptCipher = new ChaCha7539Engine();
			this.decryptCipher = new ChaCha7539Engine();
			KeyParameter parameters;
			KeyParameter parameters2;
			if (context.IsServer)
			{
				parameters = keyParameter2;
				parameters2 = keyParameter;
				this.encryptIV = array3;
				this.decryptIV = array2;
			}
			else
			{
				parameters = keyParameter;
				parameters2 = keyParameter2;
				this.encryptIV = array2;
				this.decryptIV = array3;
			}
			this.encryptCipher.Init(true, new ParametersWithIV(parameters, this.encryptIV));
			this.decryptCipher.Init(false, new ParametersWithIV(parameters2, this.decryptIV));
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x000D07F0 File Offset: 0x000D07F0
		public virtual int GetPlaintextLimit(int ciphertextLimit)
		{
			return ciphertextLimit - 16;
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x000D07F8 File Offset: 0x000D07F8
		public virtual byte[] EncodePlaintext(long seqNo, byte type, byte[] plaintext, int offset, int len)
		{
			KeyParameter macKey = this.InitRecord(this.encryptCipher, true, seqNo, this.encryptIV);
			byte[] array = new byte[len + 16];
			this.encryptCipher.ProcessBytes(plaintext, offset, len, array, 0);
			byte[] additionalData = this.GetAdditionalData(seqNo, type, len);
			byte[] array2 = this.CalculateRecordMac(macKey, additionalData, array, 0, len);
			Array.Copy(array2, 0, array, len, array2.Length);
			return array;
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x000D0864 File Offset: 0x000D0864
		public virtual byte[] DecodeCiphertext(long seqNo, byte type, byte[] ciphertext, int offset, int len)
		{
			if (this.GetPlaintextLimit(len) < 0)
			{
				throw new TlsFatalAlert(50);
			}
			KeyParameter macKey = this.InitRecord(this.decryptCipher, false, seqNo, this.decryptIV);
			int num = len - 16;
			byte[] additionalData = this.GetAdditionalData(seqNo, type, num);
			byte[] a = this.CalculateRecordMac(macKey, additionalData, ciphertext, offset, num);
			byte[] b = Arrays.CopyOfRange(ciphertext, offset + num, offset + len);
			if (!Arrays.ConstantTimeAreEqual(a, b))
			{
				throw new TlsFatalAlert(20);
			}
			byte[] array = new byte[num];
			this.decryptCipher.ProcessBytes(ciphertext, offset, num, array, 0);
			return array;
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x000D0900 File Offset: 0x000D0900
		protected virtual KeyParameter InitRecord(IStreamCipher cipher, bool forEncryption, long seqNo, byte[] iv)
		{
			byte[] iv2 = this.CalculateNonce(seqNo, iv);
			cipher.Init(forEncryption, new ParametersWithIV(null, iv2));
			return this.GenerateRecordMacKey(cipher);
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x000D0930 File Offset: 0x000D0930
		protected virtual byte[] CalculateNonce(long seqNo, byte[] iv)
		{
			byte[] array = new byte[12];
			TlsUtilities.WriteUint64(seqNo, array, 4);
			for (int i = 0; i < 12; i++)
			{
				byte[] array2;
				IntPtr intPtr;
				(array2 = array)[(int)(intPtr = (IntPtr)i)] = (array2[(int)intPtr] ^ iv[i]);
			}
			return array;
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x000D0974 File Offset: 0x000D0974
		protected virtual KeyParameter GenerateRecordMacKey(IStreamCipher cipher)
		{
			byte[] array = new byte[64];
			cipher.ProcessBytes(array, 0, array.Length, array, 0);
			KeyParameter result = new KeyParameter(array, 0, 32);
			Arrays.Fill(array, 0);
			return result;
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x000D09AC File Offset: 0x000D09AC
		protected virtual byte[] CalculateRecordMac(KeyParameter macKey, byte[] additionalData, byte[] buf, int off, int len)
		{
			IMac mac = new Poly1305();
			mac.Init(macKey);
			this.UpdateRecordMacText(mac, additionalData, 0, additionalData.Length);
			this.UpdateRecordMacText(mac, buf, off, len);
			this.UpdateRecordMacLength(mac, additionalData.Length);
			this.UpdateRecordMacLength(mac, len);
			return MacUtilities.DoFinal(mac);
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x000D09FC File Offset: 0x000D09FC
		protected virtual void UpdateRecordMacLength(IMac mac, int len)
		{
			byte[] array = Pack.UInt64_To_LE((ulong)((long)len));
			mac.BlockUpdate(array, 0, array.Length);
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x000D0A20 File Offset: 0x000D0A20
		protected virtual void UpdateRecordMacText(IMac mac, byte[] buf, int off, int len)
		{
			mac.BlockUpdate(buf, off, len);
			int num = len % 16;
			if (num != 0)
			{
				mac.BlockUpdate(Chacha20Poly1305.Zeroes, 0, 16 - num);
			}
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x000D0A58 File Offset: 0x000D0A58
		protected virtual byte[] GetAdditionalData(long seqNo, byte type, int len)
		{
			byte[] array = new byte[13];
			TlsUtilities.WriteUint64(seqNo, array, 0);
			TlsUtilities.WriteUint8(type, array, 8);
			TlsUtilities.WriteVersion(this.context.ServerVersion, array, 9);
			TlsUtilities.WriteUint16(len, array, 11);
			return array;
		}

		// Token: 0x040017E9 RID: 6121
		private static readonly byte[] Zeroes = new byte[15];

		// Token: 0x040017EA RID: 6122
		protected readonly TlsContext context;

		// Token: 0x040017EB RID: 6123
		protected readonly ChaCha7539Engine encryptCipher;

		// Token: 0x040017EC RID: 6124
		protected readonly ChaCha7539Engine decryptCipher;

		// Token: 0x040017ED RID: 6125
		protected readonly byte[] encryptIV;

		// Token: 0x040017EE RID: 6126
		protected readonly byte[] decryptIV;
	}
}
