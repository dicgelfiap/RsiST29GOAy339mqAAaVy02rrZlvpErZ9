using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003BD RID: 957
	public class DsaParametersGenerator
	{
		// Token: 0x06001E69 RID: 7785 RVA: 0x000B1C94 File Offset: 0x000B1C94
		public DsaParametersGenerator() : this(new Sha1Digest())
		{
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x000B1CA4 File Offset: 0x000B1CA4
		public DsaParametersGenerator(IDigest digest)
		{
			this.digest = digest;
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x000B1CB4 File Offset: 0x000B1CB4
		public virtual void Init(int size, int certainty, SecureRandom random)
		{
			if (!DsaParametersGenerator.IsValidDsaStrength(size))
			{
				throw new ArgumentException("size must be from 512 - 1024 and a multiple of 64", "size");
			}
			this.use186_3 = false;
			this.L = size;
			this.N = DsaParametersGenerator.GetDefaultN(size);
			this.certainty = certainty;
			this.random = random;
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x000B1D08 File Offset: 0x000B1D08
		public virtual void Init(DsaParameterGenerationParameters parameters)
		{
			this.use186_3 = true;
			this.L = parameters.L;
			this.N = parameters.N;
			this.certainty = parameters.Certainty;
			this.random = parameters.Random;
			this.usageIndex = parameters.UsageIndex;
			if (this.L < 1024 || this.L > 3072 || this.L % 1024 != 0)
			{
				throw new ArgumentException("Values must be between 1024 and 3072 and a multiple of 1024", "L");
			}
			if (this.L == 1024 && this.N != 160)
			{
				throw new ArgumentException("N must be 160 for L = 1024");
			}
			if (this.L == 2048 && this.N != 224 && this.N != 256)
			{
				throw new ArgumentException("N must be 224 or 256 for L = 2048");
			}
			if (this.L == 3072 && this.N != 256)
			{
				throw new ArgumentException("N must be 256 for L = 3072");
			}
			if (this.digest.GetDigestSize() * 8 < this.N)
			{
				throw new InvalidOperationException("Digest output size too small for value of N");
			}
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x000B1E54 File Offset: 0x000B1E54
		public virtual DsaParameters GenerateParameters()
		{
			if (!this.use186_3)
			{
				return this.GenerateParameters_FIPS186_2();
			}
			return this.GenerateParameters_FIPS186_3();
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x000B1E70 File Offset: 0x000B1E70
		protected virtual DsaParameters GenerateParameters_FIPS186_2()
		{
			byte[] array = new byte[20];
			byte[] array2 = new byte[20];
			byte[] array3 = new byte[20];
			byte[] array4 = new byte[20];
			int num = (this.L - 1) / 160;
			byte[] array5 = new byte[this.L / 8];
			if (!(this.digest is Sha1Digest))
			{
				throw new InvalidOperationException("can only use SHA-1 for generating FIPS 186-2 parameters");
			}
			BigInteger bigInteger;
			int i;
			BigInteger bigInteger4;
			for (;;)
			{
				this.random.NextBytes(array);
				DsaParametersGenerator.Hash(this.digest, array, array2);
				Array.Copy(array, 0, array3, 0, array.Length);
				DsaParametersGenerator.Inc(array3);
				DsaParametersGenerator.Hash(this.digest, array3, array3);
				for (int num2 = 0; num2 != array4.Length; num2++)
				{
					array4[num2] = (array2[num2] ^ array3[num2]);
				}
				byte[] array6;
				(array6 = array4)[0] = (array6[0] | 128);
				(array6 = array4)[19] = (array6[19] | 1);
				bigInteger = new BigInteger(1, array4);
				if (bigInteger.IsProbablePrime(this.certainty))
				{
					byte[] array7 = Arrays.Clone(array);
					DsaParametersGenerator.Inc(array7);
					for (i = 0; i < 4096; i++)
					{
						for (int j = 0; j < num; j++)
						{
							DsaParametersGenerator.Inc(array7);
							DsaParametersGenerator.Hash(this.digest, array7, array2);
							Array.Copy(array2, 0, array5, array5.Length - (j + 1) * array2.Length, array2.Length);
						}
						DsaParametersGenerator.Inc(array7);
						DsaParametersGenerator.Hash(this.digest, array7, array2);
						Array.Copy(array2, array2.Length - (array5.Length - num * array2.Length), array5, 0, array5.Length - num * array2.Length);
						(array6 = array5)[0] = (array6[0] | 128);
						BigInteger bigInteger2 = new BigInteger(1, array5);
						BigInteger bigInteger3 = bigInteger2.Mod(bigInteger.ShiftLeft(1));
						bigInteger4 = bigInteger2.Subtract(bigInteger3.Subtract(BigInteger.One));
						if (bigInteger4.BitLength == this.L && bigInteger4.IsProbablePrime(this.certainty))
						{
							goto Block_6;
						}
					}
				}
			}
			Block_6:
			BigInteger g = this.CalculateGenerator_FIPS186_2(bigInteger4, bigInteger, this.random);
			return new DsaParameters(bigInteger4, bigInteger, g, new DsaValidationParameters(array, i));
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x000B20A4 File Offset: 0x000B20A4
		protected virtual BigInteger CalculateGenerator_FIPS186_2(BigInteger p, BigInteger q, SecureRandom r)
		{
			BigInteger e = p.Subtract(BigInteger.One).Divide(q);
			BigInteger max = p.Subtract(BigInteger.Two);
			BigInteger bigInteger2;
			do
			{
				BigInteger bigInteger = BigIntegers.CreateRandomInRange(BigInteger.Two, max, r);
				bigInteger2 = bigInteger.ModPow(e, p);
			}
			while (bigInteger2.BitLength <= 1);
			return bigInteger2;
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x000B20F4 File Offset: 0x000B20F4
		protected virtual DsaParameters GenerateParameters_FIPS186_3()
		{
			IDigest digest = this.digest;
			int num = digest.GetDigestSize() * 8;
			int n = this.N;
			byte[] array = new byte[n / 8];
			int num2 = (this.L - 1) / num;
			int n2 = (this.L - 1) % num;
			byte[] array2 = new byte[digest.GetDigestSize()];
			BigInteger bigInteger2;
			int i;
			BigInteger bigInteger7;
			for (;;)
			{
				this.random.NextBytes(array);
				DsaParametersGenerator.Hash(digest, array, array2);
				BigInteger bigInteger = new BigInteger(1, array2).Mod(BigInteger.One.ShiftLeft(this.N - 1));
				bigInteger2 = bigInteger.SetBit(0).SetBit(this.N - 1);
				if (bigInteger2.IsProbablePrime(this.certainty))
				{
					byte[] array3 = Arrays.Clone(array);
					int num3 = 4 * this.L;
					for (i = 0; i < num3; i++)
					{
						BigInteger bigInteger3 = BigInteger.Zero;
						int j = 0;
						int num4 = 0;
						while (j <= num2)
						{
							DsaParametersGenerator.Inc(array3);
							DsaParametersGenerator.Hash(digest, array3, array2);
							BigInteger bigInteger4 = new BigInteger(1, array2);
							if (j == num2)
							{
								bigInteger4 = bigInteger4.Mod(BigInteger.One.ShiftLeft(n2));
							}
							bigInteger3 = bigInteger3.Add(bigInteger4.ShiftLeft(num4));
							j++;
							num4 += num;
						}
						BigInteger bigInteger5 = bigInteger3.Add(BigInteger.One.ShiftLeft(this.L - 1));
						BigInteger bigInteger6 = bigInteger5.Mod(bigInteger2.ShiftLeft(1));
						bigInteger7 = bigInteger5.Subtract(bigInteger6.Subtract(BigInteger.One));
						if (bigInteger7.BitLength == this.L && bigInteger7.IsProbablePrime(this.certainty))
						{
							goto Block_5;
						}
					}
				}
			}
			Block_5:
			if (this.usageIndex >= 0)
			{
				BigInteger bigInteger8 = this.CalculateGenerator_FIPS186_3_Verifiable(digest, bigInteger7, bigInteger2, array, this.usageIndex);
				if (bigInteger8 != null)
				{
					return new DsaParameters(bigInteger7, bigInteger2, bigInteger8, new DsaValidationParameters(array, i, this.usageIndex));
				}
			}
			BigInteger g = this.CalculateGenerator_FIPS186_3_Unverifiable(bigInteger7, bigInteger2, this.random);
			return new DsaParameters(bigInteger7, bigInteger2, g, new DsaValidationParameters(array, i));
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x000B2310 File Offset: 0x000B2310
		protected virtual BigInteger CalculateGenerator_FIPS186_3_Unverifiable(BigInteger p, BigInteger q, SecureRandom r)
		{
			return this.CalculateGenerator_FIPS186_2(p, q, r);
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x000B231C File Offset: 0x000B231C
		protected virtual BigInteger CalculateGenerator_FIPS186_3_Verifiable(IDigest d, BigInteger p, BigInteger q, byte[] seed, int index)
		{
			BigInteger e = p.Subtract(BigInteger.One).Divide(q);
			byte[] array = Hex.DecodeStrict("6767656E");
			byte[] array2 = new byte[seed.Length + array.Length + 1 + 2];
			Array.Copy(seed, 0, array2, 0, seed.Length);
			Array.Copy(array, 0, array2, seed.Length, array.Length);
			array2[array2.Length - 3] = (byte)index;
			byte[] array3 = new byte[d.GetDigestSize()];
			for (int i = 1; i < 65536; i++)
			{
				DsaParametersGenerator.Inc(array2);
				DsaParametersGenerator.Hash(d, array2, array3);
				BigInteger bigInteger = new BigInteger(1, array3);
				BigInteger bigInteger2 = bigInteger.ModPow(e, p);
				if (bigInteger2.CompareTo(BigInteger.Two) >= 0)
				{
					return bigInteger2;
				}
			}
			return null;
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x000B23E0 File Offset: 0x000B23E0
		private static bool IsValidDsaStrength(int strength)
		{
			return strength >= 512 && strength <= 1024 && strength % 64 == 0;
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x000B2404 File Offset: 0x000B2404
		protected static void Hash(IDigest d, byte[] input, byte[] output)
		{
			d.BlockUpdate(input, 0, input.Length);
			d.DoFinal(output, 0);
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x000B241C File Offset: 0x000B241C
		private static int GetDefaultN(int L)
		{
			if (L <= 1024)
			{
				return 160;
			}
			return 256;
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x000B2434 File Offset: 0x000B2434
		protected static void Inc(byte[] buf)
		{
			for (int i = buf.Length - 1; i >= 0; i--)
			{
				byte b = buf[i] + 1;
				buf[i] = b;
				if (b != 0)
				{
					return;
				}
			}
		}

		// Token: 0x04001424 RID: 5156
		private IDigest digest;

		// Token: 0x04001425 RID: 5157
		private int L;

		// Token: 0x04001426 RID: 5158
		private int N;

		// Token: 0x04001427 RID: 5159
		private int certainty;

		// Token: 0x04001428 RID: 5160
		private SecureRandom random;

		// Token: 0x04001429 RID: 5161
		private bool use186_3;

		// Token: 0x0400142A RID: 5162
		private int usageIndex;
	}
}
