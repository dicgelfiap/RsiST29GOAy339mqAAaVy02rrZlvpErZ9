using System;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x0200055C RID: 1372
	public class BufferedAeadBlockCipher : BufferedCipherBase
	{
		// Token: 0x06002AA2 RID: 10914 RVA: 0x000E46A8 File Offset: 0x000E46A8
		public BufferedAeadBlockCipher(IAeadBlockCipher cipher)
		{
			if (cipher == null)
			{
				throw new ArgumentNullException("cipher");
			}
			this.cipher = cipher;
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06002AA3 RID: 10915 RVA: 0x000E46C8 File Offset: 0x000E46C8
		public override string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x000E46D8 File Offset: 0x000E46D8
		public override void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			this.cipher.Init(forEncryption, parameters);
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x000E4700 File Offset: 0x000E4700
		public override int GetBlockSize()
		{
			return this.cipher.GetBlockSize();
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x000E4710 File Offset: 0x000E4710
		public override int GetUpdateOutputSize(int length)
		{
			return this.cipher.GetUpdateOutputSize(length);
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x000E4720 File Offset: 0x000E4720
		public override int GetOutputSize(int length)
		{
			return this.cipher.GetOutputSize(length);
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x000E4730 File Offset: 0x000E4730
		public override int ProcessByte(byte input, byte[] output, int outOff)
		{
			return this.cipher.ProcessByte(input, output, outOff);
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x000E4740 File Offset: 0x000E4740
		public override byte[] ProcessByte(byte input)
		{
			int updateOutputSize = this.GetUpdateOutputSize(1);
			byte[] array = (updateOutputSize > 0) ? new byte[updateOutputSize] : null;
			int num = this.ProcessByte(input, array, 0);
			if (updateOutputSize > 0 && num < updateOutputSize)
			{
				byte[] array2 = new byte[num];
				Array.Copy(array, 0, array2, 0, num);
				array = array2;
			}
			return array;
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x000E479C File Offset: 0x000E479C
		public override byte[] ProcessBytes(byte[] input, int inOff, int length)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (length < 1)
			{
				return null;
			}
			int updateOutputSize = this.GetUpdateOutputSize(length);
			byte[] array = (updateOutputSize > 0) ? new byte[updateOutputSize] : null;
			int num = this.ProcessBytes(input, inOff, length, array, 0);
			if (updateOutputSize > 0 && num < updateOutputSize)
			{
				byte[] array2 = new byte[num];
				Array.Copy(array, 0, array2, 0, num);
				array = array2;
			}
			return array;
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x000E4814 File Offset: 0x000E4814
		public override int ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			return this.cipher.ProcessBytes(input, inOff, length, output, outOff);
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x000E4828 File Offset: 0x000E4828
		public override byte[] DoFinal()
		{
			byte[] array = new byte[this.GetOutputSize(0)];
			int num = this.DoFinal(array, 0);
			if (num < array.Length)
			{
				byte[] array2 = new byte[num];
				Array.Copy(array, 0, array2, 0, num);
				array = array2;
			}
			return array;
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x000E486C File Offset: 0x000E486C
		public override byte[] DoFinal(byte[] input, int inOff, int inLen)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			byte[] array = new byte[this.GetOutputSize(inLen)];
			int num = (inLen > 0) ? this.ProcessBytes(input, inOff, inLen, array, 0) : 0;
			num += this.DoFinal(array, num);
			if (num < array.Length)
			{
				byte[] array2 = new byte[num];
				Array.Copy(array, 0, array2, 0, num);
				array = array2;
			}
			return array;
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x000E48DC File Offset: 0x000E48DC
		public override int DoFinal(byte[] output, int outOff)
		{
			return this.cipher.DoFinal(output, outOff);
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x000E48EC File Offset: 0x000E48EC
		public override void Reset()
		{
			this.cipher.Reset();
		}

		// Token: 0x04001B34 RID: 6964
		private readonly IAeadBlockCipher cipher;
	}
}
