using System;
using System.Collections;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Prng.Drbg
{
	// Token: 0x02000481 RID: 1153
	public class HashSP800Drbg : ISP80090Drbg
	{
		// Token: 0x060023A8 RID: 9128 RVA: 0x000C7C34 File Offset: 0x000C7C34
		static HashSP800Drbg()
		{
			HashSP800Drbg.seedlens.Add("SHA-1", 440);
			HashSP800Drbg.seedlens.Add("SHA-224", 440);
			HashSP800Drbg.seedlens.Add("SHA-256", 440);
			HashSP800Drbg.seedlens.Add("SHA-512/256", 440);
			HashSP800Drbg.seedlens.Add("SHA-512/224", 440);
			HashSP800Drbg.seedlens.Add("SHA-384", 888);
			HashSP800Drbg.seedlens.Add("SHA-512", 888);
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x000C7D28 File Offset: 0x000C7D28
		public HashSP800Drbg(IDigest digest, int securityStrength, IEntropySource entropySource, byte[] personalizationString, byte[] nonce)
		{
			if (securityStrength > DrbgUtilities.GetMaxSecurityStrength(digest))
			{
				throw new ArgumentException("Requested security strength is not supported by the derivation function");
			}
			if (entropySource.EntropySize < securityStrength)
			{
				throw new ArgumentException("Not enough entropy for security strength required");
			}
			this.mDigest = digest;
			this.mEntropySource = entropySource;
			this.mSecurityStrength = securityStrength;
			this.mSeedLength = (int)HashSP800Drbg.seedlens[digest.AlgorithmName];
			byte[] entropy = this.GetEntropy();
			byte[] seedMaterial = Arrays.ConcatenateAll(new byte[][]
			{
				entropy,
				nonce,
				personalizationString
			});
			byte[] array = DrbgUtilities.HashDF(this.mDigest, seedMaterial, this.mSeedLength);
			this.mV = array;
			byte[] array2 = new byte[this.mV.Length + 1];
			Array.Copy(this.mV, 0, array2, 1, this.mV.Length);
			this.mC = DrbgUtilities.HashDF(this.mDigest, array2, this.mSeedLength);
			this.mReseedCounter = 1L;
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060023AA RID: 9130 RVA: 0x000C7E30 File Offset: 0x000C7E30
		public int BlockSize
		{
			get
			{
				return this.mDigest.GetDigestSize() * 8;
			}
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x000C7E40 File Offset: 0x000C7E40
		public int Generate(byte[] output, byte[] additionalInput, bool predictionResistant)
		{
			int num = output.Length * 8;
			if (num > HashSP800Drbg.MAX_BITS_REQUEST)
			{
				throw new ArgumentException("Number of bits per request limited to " + HashSP800Drbg.MAX_BITS_REQUEST, "output");
			}
			if (this.mReseedCounter > HashSP800Drbg.RESEED_MAX)
			{
				return -1;
			}
			if (predictionResistant)
			{
				this.Reseed(additionalInput);
				additionalInput = null;
			}
			if (additionalInput != null)
			{
				byte[] array = new byte[1 + this.mV.Length + additionalInput.Length];
				array[0] = 2;
				Array.Copy(this.mV, 0, array, 1, this.mV.Length);
				Array.Copy(additionalInput, 0, array, 1 + this.mV.Length, additionalInput.Length);
				byte[] shorter = this.Hash(array);
				this.AddTo(this.mV, shorter);
			}
			byte[] sourceArray = this.hashgen(this.mV, num);
			byte[] array2 = new byte[this.mV.Length + 1];
			Array.Copy(this.mV, 0, array2, 1, this.mV.Length);
			array2[0] = 3;
			byte[] shorter2 = this.Hash(array2);
			this.AddTo(this.mV, shorter2);
			this.AddTo(this.mV, this.mC);
			byte[] shorter3 = new byte[]
			{
				(byte)(this.mReseedCounter >> 24),
				(byte)(this.mReseedCounter >> 16),
				(byte)(this.mReseedCounter >> 8),
				(byte)this.mReseedCounter
			};
			this.AddTo(this.mV, shorter3);
			this.mReseedCounter += 1L;
			Array.Copy(sourceArray, 0, output, 0, output.Length);
			return num;
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x000C7FCC File Offset: 0x000C7FCC
		private byte[] GetEntropy()
		{
			byte[] entropy = this.mEntropySource.GetEntropy();
			if (entropy.Length < (this.mSecurityStrength + 7) / 8)
			{
				throw new InvalidOperationException("Insufficient entropy provided by entropy source");
			}
			return entropy;
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x000C8008 File Offset: 0x000C8008
		private void AddTo(byte[] longer, byte[] shorter)
		{
			int num = longer.Length - shorter.Length;
			uint num2 = 0U;
			int num3 = shorter.Length;
			while (--num3 >= 0)
			{
				num2 += (uint)(longer[num + num3] + shorter[num3]);
				longer[num + num3] = (byte)num2;
				num2 >>= 8;
			}
			num3 = num;
			while (--num3 >= 0)
			{
				num2 += (uint)longer[num3];
				longer[num3] = (byte)num2;
				num2 >>= 8;
			}
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x000C806C File Offset: 0x000C806C
		public void Reseed(byte[] additionalInput)
		{
			byte[] entropy = this.GetEntropy();
			byte[] seedMaterial = Arrays.ConcatenateAll(new byte[][]
			{
				HashSP800Drbg.ONE,
				this.mV,
				entropy,
				additionalInput
			});
			byte[] array = DrbgUtilities.HashDF(this.mDigest, seedMaterial, this.mSeedLength);
			this.mV = array;
			byte[] array2 = new byte[this.mV.Length + 1];
			array2[0] = 0;
			Array.Copy(this.mV, 0, array2, 1, this.mV.Length);
			this.mC = DrbgUtilities.HashDF(this.mDigest, array2, this.mSeedLength);
			this.mReseedCounter = 1L;
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x000C8124 File Offset: 0x000C8124
		private byte[] Hash(byte[] input)
		{
			byte[] array = new byte[this.mDigest.GetDigestSize()];
			this.DoHash(input, array);
			return array;
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x000C8150 File Offset: 0x000C8150
		private void DoHash(byte[] input, byte[] output)
		{
			this.mDigest.BlockUpdate(input, 0, input.Length);
			this.mDigest.DoFinal(output, 0);
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x000C8170 File Offset: 0x000C8170
		private byte[] hashgen(byte[] input, int lengthInBits)
		{
			int digestSize = this.mDigest.GetDigestSize();
			int num = lengthInBits / 8 / digestSize;
			byte[] array = new byte[input.Length];
			Array.Copy(input, 0, array, 0, input.Length);
			byte[] array2 = new byte[lengthInBits / 8];
			byte[] array3 = new byte[this.mDigest.GetDigestSize()];
			for (int i = 0; i <= num; i++)
			{
				this.DoHash(array, array3);
				int length = (array2.Length - i * array3.Length > array3.Length) ? array3.Length : (array2.Length - i * array3.Length);
				Array.Copy(array3, 0, array2, i * array3.Length, length);
				this.AddTo(array, HashSP800Drbg.ONE);
			}
			return array2;
		}

		// Token: 0x040016A0 RID: 5792
		private static readonly byte[] ONE = new byte[]
		{
			1
		};

		// Token: 0x040016A1 RID: 5793
		private static readonly long RESEED_MAX = 140737488355328L;

		// Token: 0x040016A2 RID: 5794
		private static readonly int MAX_BITS_REQUEST = 262144;

		// Token: 0x040016A3 RID: 5795
		private static readonly IDictionary seedlens = Platform.CreateHashtable();

		// Token: 0x040016A4 RID: 5796
		private readonly IDigest mDigest;

		// Token: 0x040016A5 RID: 5797
		private readonly IEntropySource mEntropySource;

		// Token: 0x040016A6 RID: 5798
		private readonly int mSecurityStrength;

		// Token: 0x040016A7 RID: 5799
		private readonly int mSeedLength;

		// Token: 0x040016A8 RID: 5800
		private byte[] mV;

		// Token: 0x040016A9 RID: 5801
		private byte[] mC;

		// Token: 0x040016AA RID: 5802
		private long mReseedCounter;
	}
}
