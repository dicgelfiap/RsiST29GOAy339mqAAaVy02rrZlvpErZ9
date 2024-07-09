using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000388 RID: 904
	public class DesEdeWrapEngine : IWrapper
	{
		// Token: 0x06001C51 RID: 7249 RVA: 0x0009F3EC File Offset: 0x0009F3EC
		public virtual void Init(bool forWrapping, ICipherParameters parameters)
		{
			this.forWrapping = forWrapping;
			this.engine = new CbcBlockCipher(new DesEdeEngine());
			SecureRandom secureRandom;
			if (parameters is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
				parameters = parametersWithRandom.Parameters;
				secureRandom = parametersWithRandom.Random;
			}
			else
			{
				secureRandom = new SecureRandom();
			}
			if (parameters is KeyParameter)
			{
				this.param = (KeyParameter)parameters;
				if (this.forWrapping)
				{
					this.iv = new byte[8];
					secureRandom.NextBytes(this.iv);
					this.paramPlusIV = new ParametersWithIV(this.param, this.iv);
					return;
				}
			}
			else if (parameters is ParametersWithIV)
			{
				if (!forWrapping)
				{
					throw new ArgumentException("You should not supply an IV for unwrapping");
				}
				this.paramPlusIV = (ParametersWithIV)parameters;
				this.iv = this.paramPlusIV.GetIV();
				this.param = (KeyParameter)this.paramPlusIV.Parameters;
				if (this.iv.Length != 8)
				{
					throw new ArgumentException("IV is not 8 octets", "parameters");
				}
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001C52 RID: 7250 RVA: 0x0009F500 File Offset: 0x0009F500
		public virtual string AlgorithmName
		{
			get
			{
				return "DESede";
			}
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x0009F508 File Offset: 0x0009F508
		public virtual byte[] Wrap(byte[] input, int inOff, int length)
		{
			if (!this.forWrapping)
			{
				throw new InvalidOperationException("Not initialized for wrapping");
			}
			byte[] array = new byte[length];
			Array.Copy(input, inOff, array, 0, length);
			byte[] array2 = this.CalculateCmsKeyChecksum(array);
			byte[] array3 = new byte[array.Length + array2.Length];
			Array.Copy(array, 0, array3, 0, array.Length);
			Array.Copy(array2, 0, array3, array.Length, array2.Length);
			int blockSize = this.engine.GetBlockSize();
			if (array3.Length % blockSize != 0)
			{
				throw new InvalidOperationException("Not multiple of block length");
			}
			this.engine.Init(true, this.paramPlusIV);
			byte[] array4 = new byte[array3.Length];
			for (int num = 0; num != array3.Length; num += blockSize)
			{
				this.engine.ProcessBlock(array3, num, array4, num);
			}
			byte[] array5 = new byte[this.iv.Length + array4.Length];
			Array.Copy(this.iv, 0, array5, 0, this.iv.Length);
			Array.Copy(array4, 0, array5, this.iv.Length, array4.Length);
			byte[] array6 = DesEdeWrapEngine.reverse(array5);
			ParametersWithIV parameters = new ParametersWithIV(this.param, DesEdeWrapEngine.IV2);
			this.engine.Init(true, parameters);
			for (int num2 = 0; num2 != array6.Length; num2 += blockSize)
			{
				this.engine.ProcessBlock(array6, num2, array6, num2);
			}
			return array6;
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x0009F66C File Offset: 0x0009F66C
		public virtual byte[] Unwrap(byte[] input, int inOff, int length)
		{
			if (this.forWrapping)
			{
				throw new InvalidOperationException("Not set for unwrapping");
			}
			if (input == null)
			{
				throw new InvalidCipherTextException("Null pointer as ciphertext");
			}
			int blockSize = this.engine.GetBlockSize();
			if (length % blockSize != 0)
			{
				throw new InvalidCipherTextException("Ciphertext not multiple of " + blockSize);
			}
			ParametersWithIV parameters = new ParametersWithIV(this.param, DesEdeWrapEngine.IV2);
			this.engine.Init(false, parameters);
			byte[] array = new byte[length];
			for (int num = 0; num != array.Length; num += blockSize)
			{
				this.engine.ProcessBlock(input, inOff + num, array, num);
			}
			byte[] array2 = DesEdeWrapEngine.reverse(array);
			this.iv = new byte[8];
			byte[] array3 = new byte[array2.Length - 8];
			Array.Copy(array2, 0, this.iv, 0, 8);
			Array.Copy(array2, 8, array3, 0, array2.Length - 8);
			this.paramPlusIV = new ParametersWithIV(this.param, this.iv);
			this.engine.Init(false, this.paramPlusIV);
			byte[] array4 = new byte[array3.Length];
			for (int num2 = 0; num2 != array4.Length; num2 += blockSize)
			{
				this.engine.ProcessBlock(array3, num2, array4, num2);
			}
			byte[] array5 = new byte[array4.Length - 8];
			byte[] array6 = new byte[8];
			Array.Copy(array4, 0, array5, 0, array4.Length - 8);
			Array.Copy(array4, array4.Length - 8, array6, 0, 8);
			if (!this.CheckCmsKeyChecksum(array5, array6))
			{
				throw new InvalidCipherTextException("Checksum inside ciphertext is corrupted");
			}
			return array5;
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x0009F80C File Offset: 0x0009F80C
		private byte[] CalculateCmsKeyChecksum(byte[] key)
		{
			this.sha1.BlockUpdate(key, 0, key.Length);
			this.sha1.DoFinal(this.digest, 0);
			byte[] array = new byte[8];
			Array.Copy(this.digest, 0, array, 0, 8);
			return array;
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x0009F858 File Offset: 0x0009F858
		private bool CheckCmsKeyChecksum(byte[] key, byte[] checksum)
		{
			return Arrays.ConstantTimeAreEqual(this.CalculateCmsKeyChecksum(key), checksum);
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x0009F868 File Offset: 0x0009F868
		private static byte[] reverse(byte[] bs)
		{
			byte[] array = new byte[bs.Length];
			for (int i = 0; i < bs.Length; i++)
			{
				array[i] = bs[bs.Length - (i + 1)];
			}
			return array;
		}

		// Token: 0x040012D2 RID: 4818
		private CbcBlockCipher engine;

		// Token: 0x040012D3 RID: 4819
		private KeyParameter param;

		// Token: 0x040012D4 RID: 4820
		private ParametersWithIV paramPlusIV;

		// Token: 0x040012D5 RID: 4821
		private byte[] iv;

		// Token: 0x040012D6 RID: 4822
		private bool forWrapping;

		// Token: 0x040012D7 RID: 4823
		private static readonly byte[] IV2 = new byte[]
		{
			74,
			221,
			162,
			44,
			121,
			232,
			33,
			5
		};

		// Token: 0x040012D8 RID: 4824
		private readonly IDigest sha1 = new Sha1Digest();

		// Token: 0x040012D9 RID: 4825
		private readonly byte[] digest = new byte[20];
	}
}
