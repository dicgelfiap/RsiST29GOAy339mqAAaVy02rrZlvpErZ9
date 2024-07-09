using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Prng.Drbg
{
	// Token: 0x02000482 RID: 1154
	public class HMacSP800Drbg : ISP80090Drbg
	{
		// Token: 0x060023B2 RID: 9138 RVA: 0x000C8228 File Offset: 0x000C8228
		public HMacSP800Drbg(IMac hMac, int securityStrength, IEntropySource entropySource, byte[] personalizationString, byte[] nonce)
		{
			if (securityStrength > DrbgUtilities.GetMaxSecurityStrength(hMac))
			{
				throw new ArgumentException("Requested security strength is not supported by the derivation function");
			}
			if (entropySource.EntropySize < securityStrength)
			{
				throw new ArgumentException("Not enough entropy for security strength required");
			}
			this.mHMac = hMac;
			this.mSecurityStrength = securityStrength;
			this.mEntropySource = entropySource;
			byte[] entropy = this.GetEntropy();
			byte[] seedMaterial = Arrays.ConcatenateAll(new byte[][]
			{
				entropy,
				nonce,
				personalizationString
			});
			this.mK = new byte[hMac.GetMacSize()];
			this.mV = new byte[this.mK.Length];
			Arrays.Fill(this.mV, 1);
			this.hmac_DRBG_Update(seedMaterial);
			this.mReseedCounter = 1L;
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x000C82F0 File Offset: 0x000C82F0
		private void hmac_DRBG_Update(byte[] seedMaterial)
		{
			this.hmac_DRBG_Update_Func(seedMaterial, 0);
			if (seedMaterial != null)
			{
				this.hmac_DRBG_Update_Func(seedMaterial, 1);
			}
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x000C8308 File Offset: 0x000C8308
		private void hmac_DRBG_Update_Func(byte[] seedMaterial, byte vValue)
		{
			this.mHMac.Init(new KeyParameter(this.mK));
			this.mHMac.BlockUpdate(this.mV, 0, this.mV.Length);
			this.mHMac.Update(vValue);
			if (seedMaterial != null)
			{
				this.mHMac.BlockUpdate(seedMaterial, 0, seedMaterial.Length);
			}
			this.mHMac.DoFinal(this.mK, 0);
			this.mHMac.Init(new KeyParameter(this.mK));
			this.mHMac.BlockUpdate(this.mV, 0, this.mV.Length);
			this.mHMac.DoFinal(this.mV, 0);
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060023B5 RID: 9141 RVA: 0x000C83C4 File Offset: 0x000C83C4
		public int BlockSize
		{
			get
			{
				return this.mV.Length * 8;
			}
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x000C83D0 File Offset: 0x000C83D0
		public int Generate(byte[] output, byte[] additionalInput, bool predictionResistant)
		{
			int num = output.Length * 8;
			if (num > HMacSP800Drbg.MAX_BITS_REQUEST)
			{
				throw new ArgumentException("Number of bits per request limited to " + HMacSP800Drbg.MAX_BITS_REQUEST, "output");
			}
			if (this.mReseedCounter > HMacSP800Drbg.RESEED_MAX)
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
				this.hmac_DRBG_Update(additionalInput);
			}
			byte[] array = new byte[output.Length];
			int num2 = output.Length / this.mV.Length;
			this.mHMac.Init(new KeyParameter(this.mK));
			for (int i = 0; i < num2; i++)
			{
				this.mHMac.BlockUpdate(this.mV, 0, this.mV.Length);
				this.mHMac.DoFinal(this.mV, 0);
				Array.Copy(this.mV, 0, array, i * this.mV.Length, this.mV.Length);
			}
			if (num2 * this.mV.Length < array.Length)
			{
				this.mHMac.BlockUpdate(this.mV, 0, this.mV.Length);
				this.mHMac.DoFinal(this.mV, 0);
				Array.Copy(this.mV, 0, array, num2 * this.mV.Length, array.Length - num2 * this.mV.Length);
			}
			this.hmac_DRBG_Update(additionalInput);
			this.mReseedCounter += 1L;
			Array.Copy(array, 0, output, 0, output.Length);
			return num;
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x000C8550 File Offset: 0x000C8550
		public void Reseed(byte[] additionalInput)
		{
			byte[] entropy = this.GetEntropy();
			byte[] seedMaterial = Arrays.Concatenate(entropy, additionalInput);
			this.hmac_DRBG_Update(seedMaterial);
			this.mReseedCounter = 1L;
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x000C8580 File Offset: 0x000C8580
		private byte[] GetEntropy()
		{
			byte[] entropy = this.mEntropySource.GetEntropy();
			if (entropy.Length < (this.mSecurityStrength + 7) / 8)
			{
				throw new InvalidOperationException("Insufficient entropy provided by entropy source");
			}
			return entropy;
		}

		// Token: 0x040016AB RID: 5803
		private static readonly long RESEED_MAX = 140737488355328L;

		// Token: 0x040016AC RID: 5804
		private static readonly int MAX_BITS_REQUEST = 262144;

		// Token: 0x040016AD RID: 5805
		private readonly byte[] mK;

		// Token: 0x040016AE RID: 5806
		private readonly byte[] mV;

		// Token: 0x040016AF RID: 5807
		private readonly IEntropySource mEntropySource;

		// Token: 0x040016B0 RID: 5808
		private readonly IMac mHMac;

		// Token: 0x040016B1 RID: 5809
		private readonly int mSecurityStrength;

		// Token: 0x040016B2 RID: 5810
		private long mReseedCounter;
	}
}
