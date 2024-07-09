using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x020003B3 RID: 947
	public class CipherKeyGenerator
	{
		// Token: 0x06001E40 RID: 7744 RVA: 0x000B1474 File Offset: 0x000B1474
		public CipherKeyGenerator()
		{
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x000B1484 File Offset: 0x000B1484
		internal CipherKeyGenerator(int defaultStrength)
		{
			if (defaultStrength < 1)
			{
				throw new ArgumentException("strength must be a positive value", "defaultStrength");
			}
			this.defaultStrength = defaultStrength;
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001E42 RID: 7746 RVA: 0x000B14B4 File Offset: 0x000B14B4
		public int DefaultStrength
		{
			get
			{
				return this.defaultStrength;
			}
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x000B14BC File Offset: 0x000B14BC
		public void Init(KeyGenerationParameters parameters)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this.uninitialised = false;
			this.engineInit(parameters);
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x000B14E0 File Offset: 0x000B14E0
		protected virtual void engineInit(KeyGenerationParameters parameters)
		{
			this.random = parameters.Random;
			this.strength = (parameters.Strength + 7) / 8;
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x000B1500 File Offset: 0x000B1500
		public byte[] GenerateKey()
		{
			if (this.uninitialised)
			{
				if (this.defaultStrength < 1)
				{
					throw new InvalidOperationException("Generator has not been initialised");
				}
				this.uninitialised = false;
				this.engineInit(new KeyGenerationParameters(new SecureRandom(), this.defaultStrength));
			}
			return this.engineGenerateKey();
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x000B1558 File Offset: 0x000B1558
		protected virtual byte[] engineGenerateKey()
		{
			return SecureRandom.GetNextBytes(this.random, this.strength);
		}

		// Token: 0x04001414 RID: 5140
		protected internal SecureRandom random;

		// Token: 0x04001415 RID: 5141
		protected internal int strength;

		// Token: 0x04001416 RID: 5142
		private bool uninitialised = true;

		// Token: 0x04001417 RID: 5143
		private int defaultStrength;
	}
}
