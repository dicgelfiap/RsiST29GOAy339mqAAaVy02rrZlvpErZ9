using System;
using System.Security.Cryptography;

namespace Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x02000488 RID: 1160
	public class CryptoApiRandomGenerator : IRandomGenerator
	{
		// Token: 0x060023C7 RID: 9159 RVA: 0x000C8650 File Offset: 0x000C8650
		public CryptoApiRandomGenerator() : this(RandomNumberGenerator.Create())
		{
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000C8660 File Offset: 0x000C8660
		public CryptoApiRandomGenerator(RandomNumberGenerator rng)
		{
			this.rndProv = rng;
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000C8670 File Offset: 0x000C8670
		public virtual void AddSeedMaterial(byte[] seed)
		{
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x000C8674 File Offset: 0x000C8674
		public virtual void AddSeedMaterial(long seed)
		{
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000C8678 File Offset: 0x000C8678
		public virtual void NextBytes(byte[] bytes)
		{
			this.rndProv.GetBytes(bytes);
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x000C8688 File Offset: 0x000C8688
		public virtual void NextBytes(byte[] bytes, int start, int len)
		{
			if (start < 0)
			{
				throw new ArgumentException("Start offset cannot be negative", "start");
			}
			if (bytes.Length < start + len)
			{
				throw new ArgumentException("Byte array too small for requested offset and length");
			}
			if (bytes.Length == len && start == 0)
			{
				this.NextBytes(bytes);
				return;
			}
			byte[] array = new byte[len];
			this.NextBytes(array);
			Array.Copy(array, 0, bytes, start, len);
		}

		// Token: 0x040016B7 RID: 5815
		private readonly RandomNumberGenerator rndProv;
	}
}
