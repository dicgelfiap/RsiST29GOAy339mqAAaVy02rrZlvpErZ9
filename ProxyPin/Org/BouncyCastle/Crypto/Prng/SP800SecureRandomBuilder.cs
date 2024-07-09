using System;
using Org.BouncyCastle.Crypto.Prng.Drbg;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x0200048F RID: 1167
	public class SP800SecureRandomBuilder
	{
		// Token: 0x060023FD RID: 9213 RVA: 0x000C9038 File Offset: 0x000C9038
		public SP800SecureRandomBuilder() : this(new SecureRandom(), false)
		{
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x000C9048 File Offset: 0x000C9048
		public SP800SecureRandomBuilder(SecureRandom entropySource, bool predictionResistant)
		{
			this.mPersonalizationString = null;
			this.mSecurityStrength = 256;
			this.mEntropyBitsRequired = 256;
			base..ctor();
			this.mRandom = entropySource;
			this.mEntropySourceProvider = new BasicEntropySourceProvider(entropySource, predictionResistant);
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x000C9084 File Offset: 0x000C9084
		public SP800SecureRandomBuilder(IEntropySourceProvider entropySourceProvider)
		{
			this.mPersonalizationString = null;
			this.mSecurityStrength = 256;
			this.mEntropyBitsRequired = 256;
			base..ctor();
			this.mRandom = null;
			this.mEntropySourceProvider = entropySourceProvider;
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x000C90B8 File Offset: 0x000C90B8
		public SP800SecureRandomBuilder SetPersonalizationString(byte[] personalizationString)
		{
			this.mPersonalizationString = personalizationString;
			return this;
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x000C90C4 File Offset: 0x000C90C4
		public SP800SecureRandomBuilder SetSecurityStrength(int securityStrength)
		{
			this.mSecurityStrength = securityStrength;
			return this;
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x000C90D0 File Offset: 0x000C90D0
		public SP800SecureRandomBuilder SetEntropyBitsRequired(int entropyBitsRequired)
		{
			this.mEntropyBitsRequired = entropyBitsRequired;
			return this;
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x000C90DC File Offset: 0x000C90DC
		public SP800SecureRandom BuildHash(IDigest digest, byte[] nonce, bool predictionResistant)
		{
			return new SP800SecureRandom(this.mRandom, this.mEntropySourceProvider.Get(this.mEntropyBitsRequired), new SP800SecureRandomBuilder.HashDrbgProvider(digest, nonce, this.mPersonalizationString, this.mSecurityStrength), predictionResistant);
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x000C9110 File Offset: 0x000C9110
		public SP800SecureRandom BuildCtr(IBlockCipher cipher, int keySizeInBits, byte[] nonce, bool predictionResistant)
		{
			return new SP800SecureRandom(this.mRandom, this.mEntropySourceProvider.Get(this.mEntropyBitsRequired), new SP800SecureRandomBuilder.CtrDrbgProvider(cipher, keySizeInBits, nonce, this.mPersonalizationString, this.mSecurityStrength), predictionResistant);
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x000C9154 File Offset: 0x000C9154
		public SP800SecureRandom BuildHMac(IMac hMac, byte[] nonce, bool predictionResistant)
		{
			return new SP800SecureRandom(this.mRandom, this.mEntropySourceProvider.Get(this.mEntropyBitsRequired), new SP800SecureRandomBuilder.HMacDrbgProvider(hMac, nonce, this.mPersonalizationString, this.mSecurityStrength), predictionResistant);
		}

		// Token: 0x040016CA RID: 5834
		private readonly SecureRandom mRandom;

		// Token: 0x040016CB RID: 5835
		private readonly IEntropySourceProvider mEntropySourceProvider;

		// Token: 0x040016CC RID: 5836
		private byte[] mPersonalizationString;

		// Token: 0x040016CD RID: 5837
		private int mSecurityStrength;

		// Token: 0x040016CE RID: 5838
		private int mEntropyBitsRequired;

		// Token: 0x02000E11 RID: 3601
		private class HashDrbgProvider : IDrbgProvider
		{
			// Token: 0x06008C28 RID: 35880 RVA: 0x002A1BD4 File Offset: 0x002A1BD4
			public HashDrbgProvider(IDigest digest, byte[] nonce, byte[] personalizationString, int securityStrength)
			{
				this.mDigest = digest;
				this.mNonce = nonce;
				this.mPersonalizationString = personalizationString;
				this.mSecurityStrength = securityStrength;
			}

			// Token: 0x06008C29 RID: 35881 RVA: 0x002A1BFC File Offset: 0x002A1BFC
			public ISP80090Drbg Get(IEntropySource entropySource)
			{
				return new HashSP800Drbg(this.mDigest, this.mSecurityStrength, entropySource, this.mPersonalizationString, this.mNonce);
			}

			// Token: 0x0400414C RID: 16716
			private readonly IDigest mDigest;

			// Token: 0x0400414D RID: 16717
			private readonly byte[] mNonce;

			// Token: 0x0400414E RID: 16718
			private readonly byte[] mPersonalizationString;

			// Token: 0x0400414F RID: 16719
			private readonly int mSecurityStrength;
		}

		// Token: 0x02000E12 RID: 3602
		private class HMacDrbgProvider : IDrbgProvider
		{
			// Token: 0x06008C2A RID: 35882 RVA: 0x002A1C1C File Offset: 0x002A1C1C
			public HMacDrbgProvider(IMac hMac, byte[] nonce, byte[] personalizationString, int securityStrength)
			{
				this.mHMac = hMac;
				this.mNonce = nonce;
				this.mPersonalizationString = personalizationString;
				this.mSecurityStrength = securityStrength;
			}

			// Token: 0x06008C2B RID: 35883 RVA: 0x002A1C44 File Offset: 0x002A1C44
			public ISP80090Drbg Get(IEntropySource entropySource)
			{
				return new HMacSP800Drbg(this.mHMac, this.mSecurityStrength, entropySource, this.mPersonalizationString, this.mNonce);
			}

			// Token: 0x04004150 RID: 16720
			private readonly IMac mHMac;

			// Token: 0x04004151 RID: 16721
			private readonly byte[] mNonce;

			// Token: 0x04004152 RID: 16722
			private readonly byte[] mPersonalizationString;

			// Token: 0x04004153 RID: 16723
			private readonly int mSecurityStrength;
		}

		// Token: 0x02000E13 RID: 3603
		private class CtrDrbgProvider : IDrbgProvider
		{
			// Token: 0x06008C2C RID: 35884 RVA: 0x002A1C64 File Offset: 0x002A1C64
			public CtrDrbgProvider(IBlockCipher blockCipher, int keySizeInBits, byte[] nonce, byte[] personalizationString, int securityStrength)
			{
				this.mBlockCipher = blockCipher;
				this.mKeySizeInBits = keySizeInBits;
				this.mNonce = nonce;
				this.mPersonalizationString = personalizationString;
				this.mSecurityStrength = securityStrength;
			}

			// Token: 0x06008C2D RID: 35885 RVA: 0x002A1C94 File Offset: 0x002A1C94
			public ISP80090Drbg Get(IEntropySource entropySource)
			{
				return new CtrSP800Drbg(this.mBlockCipher, this.mKeySizeInBits, this.mSecurityStrength, entropySource, this.mPersonalizationString, this.mNonce);
			}

			// Token: 0x04004154 RID: 16724
			private readonly IBlockCipher mBlockCipher;

			// Token: 0x04004155 RID: 16725
			private readonly int mKeySizeInBits;

			// Token: 0x04004156 RID: 16726
			private readonly byte[] mNonce;

			// Token: 0x04004157 RID: 16727
			private readonly byte[] mPersonalizationString;

			// Token: 0x04004158 RID: 16728
			private readonly int mSecurityStrength;
		}
	}
}
