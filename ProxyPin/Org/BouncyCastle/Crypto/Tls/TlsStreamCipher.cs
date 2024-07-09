﻿using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000552 RID: 1362
	public class TlsStreamCipher : TlsCipher
	{
		// Token: 0x060029D4 RID: 10708 RVA: 0x000E02D8 File Offset: 0x000E02D8
		public TlsStreamCipher(TlsContext context, IStreamCipher clientWriteCipher, IStreamCipher serverWriteCipher, IDigest clientWriteDigest, IDigest serverWriteDigest, int cipherKeySize, bool usesNonce)
		{
			bool isServer = context.IsServer;
			this.context = context;
			this.usesNonce = usesNonce;
			this.encryptCipher = clientWriteCipher;
			this.decryptCipher = serverWriteCipher;
			int num = 2 * cipherKeySize + clientWriteDigest.GetDigestSize() + serverWriteDigest.GetDigestSize();
			byte[] key = TlsUtilities.CalculateKeyBlock(context, num);
			int num2 = 0;
			TlsMac tlsMac = new TlsMac(context, clientWriteDigest, key, num2, clientWriteDigest.GetDigestSize());
			num2 += clientWriteDigest.GetDigestSize();
			TlsMac tlsMac2 = new TlsMac(context, serverWriteDigest, key, num2, serverWriteDigest.GetDigestSize());
			num2 += serverWriteDigest.GetDigestSize();
			KeyParameter keyParameter = new KeyParameter(key, num2, cipherKeySize);
			num2 += cipherKeySize;
			KeyParameter keyParameter2 = new KeyParameter(key, num2, cipherKeySize);
			num2 += cipherKeySize;
			if (num2 != num)
			{
				throw new TlsFatalAlert(80);
			}
			ICipherParameters parameters;
			ICipherParameters parameters2;
			if (isServer)
			{
				this.writeMac = tlsMac2;
				this.readMac = tlsMac;
				this.encryptCipher = serverWriteCipher;
				this.decryptCipher = clientWriteCipher;
				parameters = keyParameter2;
				parameters2 = keyParameter;
			}
			else
			{
				this.writeMac = tlsMac;
				this.readMac = tlsMac2;
				this.encryptCipher = clientWriteCipher;
				this.decryptCipher = serverWriteCipher;
				parameters = keyParameter;
				parameters2 = keyParameter2;
			}
			if (usesNonce)
			{
				byte[] iv = new byte[8];
				parameters = new ParametersWithIV(parameters, iv);
				parameters2 = new ParametersWithIV(parameters2, iv);
			}
			this.encryptCipher.Init(true, parameters);
			this.decryptCipher.Init(false, parameters2);
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x000E0434 File Offset: 0x000E0434
		public virtual int GetPlaintextLimit(int ciphertextLimit)
		{
			return ciphertextLimit - this.writeMac.Size;
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x000E0444 File Offset: 0x000E0444
		public virtual byte[] EncodePlaintext(long seqNo, byte type, byte[] plaintext, int offset, int len)
		{
			if (this.usesNonce)
			{
				this.UpdateIV(this.encryptCipher, true, seqNo);
			}
			byte[] array = new byte[len + this.writeMac.Size];
			this.encryptCipher.ProcessBytes(plaintext, offset, len, array, 0);
			byte[] array2 = this.writeMac.CalculateMac(seqNo, type, plaintext, offset, len);
			this.encryptCipher.ProcessBytes(array2, 0, array2.Length, array, len);
			return array;
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x000E04BC File Offset: 0x000E04BC
		public virtual byte[] DecodeCiphertext(long seqNo, byte type, byte[] ciphertext, int offset, int len)
		{
			if (this.usesNonce)
			{
				this.UpdateIV(this.decryptCipher, false, seqNo);
			}
			int size = this.readMac.Size;
			if (len < size)
			{
				throw new TlsFatalAlert(50);
			}
			int num = len - size;
			byte[] array = new byte[len];
			this.decryptCipher.ProcessBytes(ciphertext, offset, len, array, 0);
			this.CheckMac(seqNo, type, array, num, len, array, 0, num);
			return Arrays.CopyOfRange(array, 0, num);
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x000E0538 File Offset: 0x000E0538
		protected virtual void CheckMac(long seqNo, byte type, byte[] recBuf, int recStart, int recEnd, byte[] calcBuf, int calcOff, int calcLen)
		{
			byte[] a = Arrays.CopyOfRange(recBuf, recStart, recEnd);
			byte[] b = this.readMac.CalculateMac(seqNo, type, calcBuf, calcOff, calcLen);
			if (!Arrays.ConstantTimeAreEqual(a, b))
			{
				throw new TlsFatalAlert(20);
			}
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x000E057C File Offset: 0x000E057C
		protected virtual void UpdateIV(IStreamCipher cipher, bool forEncryption, long seqNo)
		{
			byte[] array = new byte[8];
			TlsUtilities.WriteUint64(seqNo, array, 0);
			cipher.Init(forEncryption, new ParametersWithIV(null, array));
		}

		// Token: 0x04001B1C RID: 6940
		protected readonly TlsContext context;

		// Token: 0x04001B1D RID: 6941
		protected readonly IStreamCipher encryptCipher;

		// Token: 0x04001B1E RID: 6942
		protected readonly IStreamCipher decryptCipher;

		// Token: 0x04001B1F RID: 6943
		protected readonly TlsMac writeMac;

		// Token: 0x04001B20 RID: 6944
		protected readonly TlsMac readMac;

		// Token: 0x04001B21 RID: 6945
		protected readonly bool usesNonce;
	}
}
