using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000560 RID: 1376
	public class BufferedStreamCipher : BufferedCipherBase
	{
		// Token: 0x06002AD5 RID: 10965 RVA: 0x000E4E90 File Offset: 0x000E4E90
		public BufferedStreamCipher(IStreamCipher cipher)
		{
			if (cipher == null)
			{
				throw new ArgumentNullException("cipher");
			}
			this.cipher = cipher;
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06002AD6 RID: 10966 RVA: 0x000E4EB0 File Offset: 0x000E4EB0
		public override string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x000E4EC0 File Offset: 0x000E4EC0
		public override void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			this.cipher.Init(forEncryption, parameters);
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x000E4EE8 File Offset: 0x000E4EE8
		public override int GetBlockSize()
		{
			return 0;
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x000E4EEC File Offset: 0x000E4EEC
		public override int GetOutputSize(int inputLen)
		{
			return inputLen;
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x000E4EF0 File Offset: 0x000E4EF0
		public override int GetUpdateOutputSize(int inputLen)
		{
			return inputLen;
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x000E4EF4 File Offset: 0x000E4EF4
		public override byte[] ProcessByte(byte input)
		{
			return new byte[]
			{
				this.cipher.ReturnByte(input)
			};
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x000E4F1C File Offset: 0x000E4F1C
		public override int ProcessByte(byte input, byte[] output, int outOff)
		{
			if (outOff >= output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			output[outOff] = this.cipher.ReturnByte(input);
			return 1;
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x000E4F44 File Offset: 0x000E4F44
		public override byte[] ProcessBytes(byte[] input, int inOff, int length)
		{
			if (length < 1)
			{
				return null;
			}
			byte[] array = new byte[length];
			this.cipher.ProcessBytes(input, inOff, length, array, 0);
			return array;
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x000E4F78 File Offset: 0x000E4F78
		public override int ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			if (length < 1)
			{
				return 0;
			}
			if (length > 0)
			{
				this.cipher.ProcessBytes(input, inOff, length, output, outOff);
			}
			return length;
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x000E4FA0 File Offset: 0x000E4FA0
		public override byte[] DoFinal()
		{
			this.Reset();
			return BufferedCipherBase.EmptyBuffer;
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x000E4FB0 File Offset: 0x000E4FB0
		public override byte[] DoFinal(byte[] input, int inOff, int length)
		{
			if (length < 1)
			{
				return BufferedCipherBase.EmptyBuffer;
			}
			byte[] result = this.ProcessBytes(input, inOff, length);
			this.Reset();
			return result;
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x000E4FE0 File Offset: 0x000E4FE0
		public override void Reset()
		{
			this.cipher.Reset();
		}

		// Token: 0x04001B3C RID: 6972
		private readonly IStreamCipher cipher;
	}
}
