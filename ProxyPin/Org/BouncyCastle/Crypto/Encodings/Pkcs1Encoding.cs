using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Encodings
{
	// Token: 0x02000374 RID: 884
	public class Pkcs1Encoding : IAsymmetricBlockCipher
	{
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001B6A RID: 7018 RVA: 0x00094A0C File Offset: 0x00094A0C
		// (set) Token: 0x06001B6B RID: 7019 RVA: 0x00094A18 File Offset: 0x00094A18
		public static bool StrictLengthEnabled
		{
			get
			{
				return Pkcs1Encoding.strictLengthEnabled[0];
			}
			set
			{
				Pkcs1Encoding.strictLengthEnabled[0] = value;
			}
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x00094A24 File Offset: 0x00094A24
		static Pkcs1Encoding()
		{
			string environmentVariable = Platform.GetEnvironmentVariable("Org.BouncyCastle.Pkcs1.Strict");
			Pkcs1Encoding.strictLengthEnabled = new bool[]
			{
				environmentVariable == null || Platform.EqualsIgnoreCase("true", environmentVariable)
			};
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x00094A68 File Offset: 0x00094A68
		public Pkcs1Encoding(IAsymmetricBlockCipher cipher)
		{
			this.engine = cipher;
			this.useStrictLength = Pkcs1Encoding.StrictLengthEnabled;
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x00094A98 File Offset: 0x00094A98
		public Pkcs1Encoding(IAsymmetricBlockCipher cipher, int pLen)
		{
			this.engine = cipher;
			this.useStrictLength = Pkcs1Encoding.StrictLengthEnabled;
			this.pLen = pLen;
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x00094AD0 File Offset: 0x00094AD0
		public Pkcs1Encoding(IAsymmetricBlockCipher cipher, byte[] fallback)
		{
			this.engine = cipher;
			this.useStrictLength = Pkcs1Encoding.StrictLengthEnabled;
			this.fallback = fallback;
			this.pLen = fallback.Length;
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x00094B10 File Offset: 0x00094B10
		public IAsymmetricBlockCipher GetUnderlyingCipher()
		{
			return this.engine;
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x00094B18 File Offset: 0x00094B18
		public string AlgorithmName
		{
			get
			{
				return this.engine.AlgorithmName + "/PKCS1Padding";
			}
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x00094B30 File Offset: 0x00094B30
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			AsymmetricKeyParameter asymmetricKeyParameter;
			if (parameters is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
				this.random = parametersWithRandom.Random;
				asymmetricKeyParameter = (AsymmetricKeyParameter)parametersWithRandom.Parameters;
			}
			else
			{
				this.random = new SecureRandom();
				asymmetricKeyParameter = (AsymmetricKeyParameter)parameters;
			}
			this.engine.Init(forEncryption, parameters);
			this.forPrivateKey = asymmetricKeyParameter.IsPrivate;
			this.forEncryption = forEncryption;
			this.blockBuffer = new byte[this.engine.GetOutputBlockSize()];
			if (this.pLen > 0 && this.fallback == null && this.random == null)
			{
				throw new ArgumentException("encoder requires random");
			}
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x00094BE8 File Offset: 0x00094BE8
		public int GetInputBlockSize()
		{
			int inputBlockSize = this.engine.GetInputBlockSize();
			if (!this.forEncryption)
			{
				return inputBlockSize;
			}
			return inputBlockSize - 10;
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x00094C18 File Offset: 0x00094C18
		public int GetOutputBlockSize()
		{
			int outputBlockSize = this.engine.GetOutputBlockSize();
			if (!this.forEncryption)
			{
				return outputBlockSize - 10;
			}
			return outputBlockSize;
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x00094C48 File Offset: 0x00094C48
		public byte[] ProcessBlock(byte[] input, int inOff, int length)
		{
			if (!this.forEncryption)
			{
				return this.DecodeBlock(input, inOff, length);
			}
			return this.EncodeBlock(input, inOff, length);
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x00094C68 File Offset: 0x00094C68
		private byte[] EncodeBlock(byte[] input, int inOff, int inLen)
		{
			if (inLen > this.GetInputBlockSize())
			{
				throw new ArgumentException("input data too large", "inLen");
			}
			byte[] array = new byte[this.engine.GetInputBlockSize()];
			if (this.forPrivateKey)
			{
				array[0] = 1;
				for (int num = 1; num != array.Length - inLen - 1; num++)
				{
					array[num] = byte.MaxValue;
				}
			}
			else
			{
				this.random.NextBytes(array);
				array[0] = 2;
				for (int num2 = 1; num2 != array.Length - inLen - 1; num2++)
				{
					while (array[num2] == 0)
					{
						array[num2] = (byte)this.random.NextInt();
					}
				}
			}
			array[array.Length - inLen - 1] = 0;
			Array.Copy(input, inOff, array, array.Length - inLen, inLen);
			return this.engine.ProcessBlock(array, 0, array.Length);
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x00094D38 File Offset: 0x00094D38
		private static int CheckPkcs1Encoding(byte[] encoded, int pLen)
		{
			int num = 0;
			num |= (int)(encoded[0] ^ 2);
			int num2 = encoded.Length - (pLen + 1);
			for (int i = 1; i < num2; i++)
			{
				int num3 = (int)encoded[i];
				num3 |= num3 >> 1;
				num3 |= num3 >> 2;
				num3 |= num3 >> 4;
				num |= (num3 & 1) - 1;
			}
			num |= (int)encoded[encoded.Length - (pLen + 1)];
			num |= num >> 1;
			num |= num >> 2;
			num |= num >> 4;
			return ~((num & 1) - 1);
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x00094DAC File Offset: 0x00094DAC
		private byte[] DecodeBlockOrRandom(byte[] input, int inOff, int inLen)
		{
			if (!this.forPrivateKey)
			{
				throw new InvalidCipherTextException("sorry, this method is only for decryption, not for signing");
			}
			byte[] array = this.engine.ProcessBlock(input, inOff, inLen);
			byte[] array2;
			if (this.fallback == null)
			{
				array2 = new byte[this.pLen];
				this.random.NextBytes(array2);
			}
			else
			{
				array2 = this.fallback;
			}
			byte[] array3 = (this.useStrictLength & array.Length != this.engine.GetOutputBlockSize()) ? this.blockBuffer : array;
			int num = Pkcs1Encoding.CheckPkcs1Encoding(array3, this.pLen);
			byte[] array4 = new byte[this.pLen];
			for (int i = 0; i < this.pLen; i++)
			{
				array4[i] = (byte)(((int)array3[i + (array3.Length - this.pLen)] & ~num) | ((int)array2[i] & num));
			}
			Arrays.Fill(array3, 0);
			return array4;
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x00094E98 File Offset: 0x00094E98
		private byte[] DecodeBlock(byte[] input, int inOff, int inLen)
		{
			if (this.pLen != -1)
			{
				return this.DecodeBlockOrRandom(input, inOff, inLen);
			}
			byte[] array = this.engine.ProcessBlock(input, inOff, inLen);
			bool flag = this.useStrictLength & array.Length != this.engine.GetOutputBlockSize();
			byte[] array2;
			if (array.Length < this.GetOutputBlockSize())
			{
				array2 = this.blockBuffer;
			}
			else
			{
				array2 = array;
			}
			byte b = this.forPrivateKey ? 2 : 1;
			byte b2 = array2[0];
			bool flag2 = b2 != b;
			int num = this.FindStart(b2, array2);
			num++;
			if (flag2 | num < 10)
			{
				Arrays.Fill(array2, 0);
				throw new InvalidCipherTextException("block incorrect");
			}
			if (flag)
			{
				Arrays.Fill(array2, 0);
				throw new InvalidCipherTextException("block incorrect size");
			}
			byte[] array3 = new byte[array2.Length - num];
			Array.Copy(array2, num, array3, 0, array3.Length);
			return array3;
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x00094F90 File Offset: 0x00094F90
		private int FindStart(byte type, byte[] block)
		{
			int num = -1;
			bool flag = false;
			for (int num2 = 1; num2 != block.Length; num2++)
			{
				byte b = block[num2];
				if (b == 0 & num < 0)
				{
					num = num2;
				}
				flag |= (type == 1 & num < 0 & b != byte.MaxValue);
			}
			if (!flag)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x0400122B RID: 4651
		public const string StrictLengthEnabledProperty = "Org.BouncyCastle.Pkcs1.Strict";

		// Token: 0x0400122C RID: 4652
		private const int HeaderLength = 10;

		// Token: 0x0400122D RID: 4653
		private static readonly bool[] strictLengthEnabled;

		// Token: 0x0400122E RID: 4654
		private SecureRandom random;

		// Token: 0x0400122F RID: 4655
		private IAsymmetricBlockCipher engine;

		// Token: 0x04001230 RID: 4656
		private bool forEncryption;

		// Token: 0x04001231 RID: 4657
		private bool forPrivateKey;

		// Token: 0x04001232 RID: 4658
		private bool useStrictLength;

		// Token: 0x04001233 RID: 4659
		private int pLen = -1;

		// Token: 0x04001234 RID: 4660
		private byte[] fallback = null;

		// Token: 0x04001235 RID: 4661
		private byte[] blockBuffer = null;
	}
}
