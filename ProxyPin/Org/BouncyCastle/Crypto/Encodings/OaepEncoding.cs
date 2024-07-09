using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Encodings
{
	// Token: 0x02000373 RID: 883
	public class OaepEncoding : IAsymmetricBlockCipher
	{
		// Token: 0x06001B5C RID: 7004 RVA: 0x0009443C File Offset: 0x0009443C
		public OaepEncoding(IAsymmetricBlockCipher cipher) : this(cipher, new Sha1Digest(), null)
		{
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x0009444C File Offset: 0x0009444C
		public OaepEncoding(IAsymmetricBlockCipher cipher, IDigest hash) : this(cipher, hash, null)
		{
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x00094458 File Offset: 0x00094458
		public OaepEncoding(IAsymmetricBlockCipher cipher, IDigest hash, byte[] encodingParams) : this(cipher, hash, hash, encodingParams)
		{
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x00094464 File Offset: 0x00094464
		public OaepEncoding(IAsymmetricBlockCipher cipher, IDigest hash, IDigest mgf1Hash, byte[] encodingParams)
		{
			this.engine = cipher;
			this.mgf1Hash = mgf1Hash;
			this.defHash = new byte[hash.GetDigestSize()];
			hash.Reset();
			if (encodingParams != null)
			{
				hash.BlockUpdate(encodingParams, 0, encodingParams.Length);
			}
			hash.DoFinal(this.defHash, 0);
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x000944C4 File Offset: 0x000944C4
		public IAsymmetricBlockCipher GetUnderlyingCipher()
		{
			return this.engine;
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001B61 RID: 7009 RVA: 0x000944CC File Offset: 0x000944CC
		public string AlgorithmName
		{
			get
			{
				return this.engine.AlgorithmName + "/OAEPPadding";
			}
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x000944E4 File Offset: 0x000944E4
		public void Init(bool forEncryption, ICipherParameters param)
		{
			if (param is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)param;
				this.random = parametersWithRandom.Random;
			}
			else
			{
				this.random = new SecureRandom();
			}
			this.engine.Init(forEncryption, param);
			this.forEncryption = forEncryption;
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x00094538 File Offset: 0x00094538
		public int GetInputBlockSize()
		{
			int inputBlockSize = this.engine.GetInputBlockSize();
			if (this.forEncryption)
			{
				return inputBlockSize - 1 - 2 * this.defHash.Length;
			}
			return inputBlockSize;
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x00094570 File Offset: 0x00094570
		public int GetOutputBlockSize()
		{
			int outputBlockSize = this.engine.GetOutputBlockSize();
			if (this.forEncryption)
			{
				return outputBlockSize;
			}
			return outputBlockSize - 1 - 2 * this.defHash.Length;
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x000945A8 File Offset: 0x000945A8
		public byte[] ProcessBlock(byte[] inBytes, int inOff, int inLen)
		{
			if (this.forEncryption)
			{
				return this.EncodeBlock(inBytes, inOff, inLen);
			}
			return this.DecodeBlock(inBytes, inOff, inLen);
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x000945C8 File Offset: 0x000945C8
		private byte[] EncodeBlock(byte[] inBytes, int inOff, int inLen)
		{
			Check.DataLength(inLen > this.GetInputBlockSize(), "input data too long");
			byte[] array = new byte[this.GetInputBlockSize() + 1 + 2 * this.defHash.Length];
			Array.Copy(inBytes, inOff, array, array.Length - inLen, inLen);
			array[array.Length - inLen - 1] = 1;
			Array.Copy(this.defHash, 0, array, this.defHash.Length, this.defHash.Length);
			byte[] nextBytes = SecureRandom.GetNextBytes(this.random, this.defHash.Length);
			byte[] array2 = this.maskGeneratorFunction1(nextBytes, 0, nextBytes.Length, array.Length - this.defHash.Length);
			for (int num = this.defHash.Length; num != array.Length; num++)
			{
				byte[] array3;
				IntPtr intPtr;
				(array3 = array)[(int)(intPtr = (IntPtr)num)] = (array3[(int)intPtr] ^ array2[num - this.defHash.Length]);
			}
			Array.Copy(nextBytes, 0, array, 0, this.defHash.Length);
			array2 = this.maskGeneratorFunction1(array, this.defHash.Length, array.Length - this.defHash.Length, this.defHash.Length);
			for (int num2 = 0; num2 != this.defHash.Length; num2++)
			{
				byte[] array3;
				IntPtr intPtr;
				(array3 = array)[(int)(intPtr = (IntPtr)num2)] = (array3[(int)intPtr] ^ array2[num2]);
			}
			return this.engine.ProcessBlock(array, 0, array.Length);
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x00094710 File Offset: 0x00094710
		private byte[] DecodeBlock(byte[] inBytes, int inOff, int inLen)
		{
			byte[] array = this.engine.ProcessBlock(inBytes, inOff, inLen);
			byte[] array2 = new byte[this.engine.GetOutputBlockSize()];
			bool flag = array2.Length < 2 * this.defHash.Length + 1;
			if (array.Length <= array2.Length)
			{
				Array.Copy(array, 0, array2, array2.Length - array.Length, array.Length);
			}
			else
			{
				Array.Copy(array, 0, array2, 0, array2.Length);
				flag = true;
			}
			byte[] array3 = this.maskGeneratorFunction1(array2, this.defHash.Length, array2.Length - this.defHash.Length, this.defHash.Length);
			for (int num = 0; num != this.defHash.Length; num++)
			{
				byte[] array4;
				IntPtr intPtr;
				(array4 = array2)[(int)(intPtr = (IntPtr)num)] = (array4[(int)intPtr] ^ array3[num]);
			}
			array3 = this.maskGeneratorFunction1(array2, 0, this.defHash.Length, array2.Length - this.defHash.Length);
			for (int num2 = this.defHash.Length; num2 != array2.Length; num2++)
			{
				byte[] array4;
				IntPtr intPtr;
				(array4 = array2)[(int)(intPtr = (IntPtr)num2)] = (array4[(int)intPtr] ^ array3[num2 - this.defHash.Length]);
			}
			bool flag2 = false;
			for (int num3 = 0; num3 != this.defHash.Length; num3++)
			{
				if (this.defHash[num3] != array2[this.defHash.Length + num3])
				{
					flag2 = true;
				}
			}
			int num4 = array2.Length;
			for (int num5 = 2 * this.defHash.Length; num5 != array2.Length; num5++)
			{
				if (array2[num5] != 0 & num4 == array2.Length)
				{
					num4 = num5;
				}
			}
			bool flag3 = num4 > array2.Length - 1 | array2[num4] != 1;
			num4++;
			if (flag2 || flag || flag3)
			{
				Arrays.Fill(array2, 0);
				throw new InvalidCipherTextException("data wrong");
			}
			byte[] array5 = new byte[array2.Length - num4];
			Array.Copy(array2, num4, array5, 0, array5.Length);
			return array5;
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x00094900 File Offset: 0x00094900
		private void ItoOSP(int i, byte[] sp)
		{
			sp[0] = (byte)((uint)i >> 24);
			sp[1] = (byte)((uint)i >> 16);
			sp[2] = (byte)((uint)i >> 8);
			sp[3] = (byte)i;
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x00094920 File Offset: 0x00094920
		private byte[] maskGeneratorFunction1(byte[] Z, int zOff, int zLen, int length)
		{
			byte[] array = new byte[length];
			byte[] array2 = new byte[this.mgf1Hash.GetDigestSize()];
			byte[] array3 = new byte[4];
			int i = 0;
			this.mgf1Hash.Reset();
			while (i < length / array2.Length)
			{
				this.ItoOSP(i, array3);
				this.mgf1Hash.BlockUpdate(Z, zOff, zLen);
				this.mgf1Hash.BlockUpdate(array3, 0, array3.Length);
				this.mgf1Hash.DoFinal(array2, 0);
				Array.Copy(array2, 0, array, i * array2.Length, array2.Length);
				i++;
			}
			if (i * array2.Length < length)
			{
				this.ItoOSP(i, array3);
				this.mgf1Hash.BlockUpdate(Z, zOff, zLen);
				this.mgf1Hash.BlockUpdate(array3, 0, array3.Length);
				this.mgf1Hash.DoFinal(array2, 0);
				Array.Copy(array2, 0, array, i * array2.Length, array.Length - i * array2.Length);
			}
			return array;
		}

		// Token: 0x04001226 RID: 4646
		private byte[] defHash;

		// Token: 0x04001227 RID: 4647
		private IDigest mgf1Hash;

		// Token: 0x04001228 RID: 4648
		private IAsymmetricBlockCipher engine;

		// Token: 0x04001229 RID: 4649
		private SecureRandom random;

		// Token: 0x0400122A RID: 4650
		private bool forEncryption;
	}
}
