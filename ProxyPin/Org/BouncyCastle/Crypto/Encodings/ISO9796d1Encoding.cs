﻿using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Encodings
{
	// Token: 0x02000372 RID: 882
	public class ISO9796d1Encoding : IAsymmetricBlockCipher
	{
		// Token: 0x06001B50 RID: 6992 RVA: 0x00093F44 File Offset: 0x00093F44
		public ISO9796d1Encoding(IAsymmetricBlockCipher cipher)
		{
			this.engine = cipher;
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x00093F5C File Offset: 0x00093F5C
		public string AlgorithmName
		{
			get
			{
				return this.engine.AlgorithmName + "/ISO9796-1Padding";
			}
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x00093F74 File Offset: 0x00093F74
		public IAsymmetricBlockCipher GetUnderlyingCipher()
		{
			return this.engine;
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00093F7C File Offset: 0x00093F7C
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			RsaKeyParameters rsaKeyParameters;
			if (parameters is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
				rsaKeyParameters = (RsaKeyParameters)parametersWithRandom.Parameters;
			}
			else
			{
				rsaKeyParameters = (RsaKeyParameters)parameters;
			}
			this.engine.Init(forEncryption, parameters);
			this.modulus = rsaKeyParameters.Modulus;
			this.bitSize = this.modulus.BitLength;
			this.forEncryption = forEncryption;
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x00093FE8 File Offset: 0x00093FE8
		public int GetInputBlockSize()
		{
			int inputBlockSize = this.engine.GetInputBlockSize();
			if (this.forEncryption)
			{
				return (inputBlockSize + 1) / 2;
			}
			return inputBlockSize;
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x00094018 File Offset: 0x00094018
		public int GetOutputBlockSize()
		{
			int outputBlockSize = this.engine.GetOutputBlockSize();
			if (this.forEncryption)
			{
				return outputBlockSize;
			}
			return (outputBlockSize + 1) / 2;
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x00094048 File Offset: 0x00094048
		public void SetPadBits(int padBits)
		{
			if (padBits > 7)
			{
				throw new ArgumentException("padBits > 7");
			}
			this.padBits = padBits;
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x00094064 File Offset: 0x00094064
		public int GetPadBits()
		{
			return this.padBits;
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x0009406C File Offset: 0x0009406C
		public byte[] ProcessBlock(byte[] input, int inOff, int length)
		{
			if (this.forEncryption)
			{
				return this.EncodeBlock(input, inOff, length);
			}
			return this.DecodeBlock(input, inOff, length);
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x0009408C File Offset: 0x0009408C
		private byte[] EncodeBlock(byte[] input, int inOff, int inLen)
		{
			byte[] array = new byte[(this.bitSize + 7) / 8];
			int num = this.padBits + 1;
			int num2 = (this.bitSize + 13) / 16;
			for (int i = 0; i < num2; i += inLen)
			{
				if (i > num2 - inLen)
				{
					Array.Copy(input, inOff + inLen - (num2 - i), array, array.Length - num2, num2 - i);
				}
				else
				{
					Array.Copy(input, inOff, array, array.Length - (i + inLen), inLen);
				}
			}
			for (int num3 = array.Length - 2 * num2; num3 != array.Length; num3 += 2)
			{
				byte b = array[array.Length - num2 + num3 / 2];
				array[num3] = (byte)((int)ISO9796d1Encoding.shadows[(int)((UIntPtr)((uint)(b & byte.MaxValue) >> 4))] << 4 | (int)ISO9796d1Encoding.shadows[(int)(b & 15)]);
				array[num3 + 1] = b;
			}
			byte[] array2;
			IntPtr intPtr;
			(array2 = array)[(int)(intPtr = (IntPtr)(array.Length - 2 * inLen))] = (array2[(int)intPtr] ^ (byte)num);
			array[array.Length - 1] = (byte)((int)array[array.Length - 1] << 4 | 6);
			int num4 = 8 - (this.bitSize - 1) % 8;
			int num5 = 0;
			if (num4 != 8)
			{
				(array2 = array)[0] = (array2[0] & (byte)(255 >> num4));
				(array2 = array)[0] = (array2[0] | (byte)(128 >> num4));
			}
			else
			{
				array[0] = 0;
				(array2 = array)[1] = (array2[1] | 128);
				num5 = 1;
			}
			return this.engine.ProcessBlock(array, num5, array.Length - num5);
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x0009420C File Offset: 0x0009420C
		private byte[] DecodeBlock(byte[] input, int inOff, int inLen)
		{
			byte[] array = this.engine.ProcessBlock(input, inOff, inLen);
			int num = 1;
			int num2 = (this.bitSize + 13) / 16;
			BigInteger bigInteger = new BigInteger(1, array);
			BigInteger bigInteger2;
			if (bigInteger.Mod(ISO9796d1Encoding.Sixteen).Equals(ISO9796d1Encoding.Six))
			{
				bigInteger2 = bigInteger;
			}
			else
			{
				bigInteger2 = this.modulus.Subtract(bigInteger);
				if (!bigInteger2.Mod(ISO9796d1Encoding.Sixteen).Equals(ISO9796d1Encoding.Six))
				{
					throw new InvalidCipherTextException("resulting integer iS or (modulus - iS) is not congruent to 6 mod 16");
				}
			}
			array = bigInteger2.ToByteArrayUnsigned();
			if ((array[array.Length - 1] & 15) != 6)
			{
				throw new InvalidCipherTextException("invalid forcing byte in block");
			}
			array[array.Length - 1] = (byte)((ushort)(array[array.Length - 1] & byte.MaxValue) >> 4 | (int)ISO9796d1Encoding.inverse[(array[array.Length - 2] & byte.MaxValue) >> 4] << 4);
			array[0] = (byte)((int)ISO9796d1Encoding.shadows[(int)((UIntPtr)((uint)(array[1] & byte.MaxValue) >> 4))] << 4 | (int)ISO9796d1Encoding.shadows[(int)(array[1] & 15)]);
			bool flag = false;
			int num3 = 0;
			for (int i = array.Length - 1; i >= array.Length - 2 * num2; i -= 2)
			{
				int num4 = (int)ISO9796d1Encoding.shadows[(int)((UIntPtr)((uint)(array[i] & byte.MaxValue) >> 4))] << 4 | (int)ISO9796d1Encoding.shadows[(int)(array[i] & 15)];
				if ((((int)array[i - 1] ^ num4) & 255) != 0)
				{
					if (flag)
					{
						throw new InvalidCipherTextException("invalid tsums in block");
					}
					flag = true;
					num = (((int)array[i - 1] ^ num4) & 255);
					num3 = i - 1;
				}
			}
			array[num3] = 0;
			byte[] array2 = new byte[(array.Length - num3) / 2];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = array[2 * j + num3 + 1];
			}
			this.padBits = num - 1;
			return array2;
		}

		// Token: 0x0400121D RID: 4637
		private static readonly BigInteger Sixteen = BigInteger.ValueOf(16L);

		// Token: 0x0400121E RID: 4638
		private static readonly BigInteger Six = BigInteger.ValueOf(6L);

		// Token: 0x0400121F RID: 4639
		private static readonly byte[] shadows = new byte[]
		{
			14,
			3,
			5,
			8,
			9,
			4,
			2,
			15,
			0,
			13,
			11,
			6,
			7,
			10,
			12,
			1
		};

		// Token: 0x04001220 RID: 4640
		private static readonly byte[] inverse = new byte[]
		{
			8,
			15,
			6,
			1,
			5,
			2,
			11,
			12,
			3,
			4,
			13,
			10,
			14,
			9,
			0,
			7
		};

		// Token: 0x04001221 RID: 4641
		private readonly IAsymmetricBlockCipher engine;

		// Token: 0x04001222 RID: 4642
		private bool forEncryption;

		// Token: 0x04001223 RID: 4643
		private int bitSize;

		// Token: 0x04001224 RID: 4644
		private int padBits = 0;

		// Token: 0x04001225 RID: 4645
		private BigInteger modulus;
	}
}
