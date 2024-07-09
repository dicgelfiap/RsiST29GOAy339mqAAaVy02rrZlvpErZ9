using System;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x0200055D RID: 1373
	public class BufferedAeadCipher : BufferedCipherBase
	{
		// Token: 0x06002AB0 RID: 10928 RVA: 0x000E48FC File Offset: 0x000E48FC
		public BufferedAeadCipher(IAeadCipher cipher)
		{
			if (cipher == null)
			{
				throw new ArgumentNullException("cipher");
			}
			this.cipher = cipher;
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06002AB1 RID: 10929 RVA: 0x000E491C File Offset: 0x000E491C
		public override string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x000E492C File Offset: 0x000E492C
		public override void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			this.cipher.Init(forEncryption, parameters);
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x000E4954 File Offset: 0x000E4954
		public override int GetBlockSize()
		{
			return 0;
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x000E4958 File Offset: 0x000E4958
		public override int GetUpdateOutputSize(int length)
		{
			return this.cipher.GetUpdateOutputSize(length);
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x000E4968 File Offset: 0x000E4968
		public override int GetOutputSize(int length)
		{
			return this.cipher.GetOutputSize(length);
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x000E4978 File Offset: 0x000E4978
		public override int ProcessByte(byte input, byte[] output, int outOff)
		{
			return this.cipher.ProcessByte(input, output, outOff);
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x000E4988 File Offset: 0x000E4988
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

		// Token: 0x06002AB8 RID: 10936 RVA: 0x000E49E4 File Offset: 0x000E49E4
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

		// Token: 0x06002AB9 RID: 10937 RVA: 0x000E4A5C File Offset: 0x000E4A5C
		public override int ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			return this.cipher.ProcessBytes(input, inOff, length, output, outOff);
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x000E4A70 File Offset: 0x000E4A70
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

		// Token: 0x06002ABB RID: 10939 RVA: 0x000E4AB4 File Offset: 0x000E4AB4
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

		// Token: 0x06002ABC RID: 10940 RVA: 0x000E4B24 File Offset: 0x000E4B24
		public override int DoFinal(byte[] output, int outOff)
		{
			return this.cipher.DoFinal(output, outOff);
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x000E4B34 File Offset: 0x000E4B34
		public override void Reset()
		{
			this.cipher.Reset();
		}

		// Token: 0x04001B35 RID: 6965
		private readonly IAeadCipher cipher;
	}
}
