using System;
using System.Threading;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Security
{
	// Token: 0x0200048D RID: 1165
	public class SecureRandom : Random
	{
		// Token: 0x060023E0 RID: 9184 RVA: 0x000C8B44 File Offset: 0x000C8B44
		private static long NextCounterValue()
		{
			return Interlocked.Increment(ref SecureRandom.counter);
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060023E1 RID: 9185 RVA: 0x000C8B50 File Offset: 0x000C8B50
		private static SecureRandom Master
		{
			get
			{
				return SecureRandom.master;
			}
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x000C8B58 File Offset: 0x000C8B58
		private static DigestRandomGenerator CreatePrng(string digestName, bool autoSeed)
		{
			IDigest digest = DigestUtilities.GetDigest(digestName);
			if (digest == null)
			{
				return null;
			}
			DigestRandomGenerator digestRandomGenerator = new DigestRandomGenerator(digest);
			if (autoSeed)
			{
				digestRandomGenerator.AddSeedMaterial(SecureRandom.NextCounterValue());
				digestRandomGenerator.AddSeedMaterial(SecureRandom.GetNextBytes(SecureRandom.Master, digest.GetDigestSize()));
			}
			return digestRandomGenerator;
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x000C8BA8 File Offset: 0x000C8BA8
		public static byte[] GetNextBytes(SecureRandom secureRandom, int length)
		{
			byte[] array = new byte[length];
			secureRandom.NextBytes(array);
			return array;
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x000C8BC8 File Offset: 0x000C8BC8
		public static SecureRandom GetInstance(string algorithm)
		{
			return SecureRandom.GetInstance(algorithm, true);
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x000C8BD4 File Offset: 0x000C8BD4
		public static SecureRandom GetInstance(string algorithm, bool autoSeed)
		{
			string text = Platform.ToUpperInvariant(algorithm);
			if (Platform.EndsWith(text, "PRNG"))
			{
				string digestName = text.Substring(0, text.Length - "PRNG".Length);
				DigestRandomGenerator digestRandomGenerator = SecureRandom.CreatePrng(digestName, autoSeed);
				if (digestRandomGenerator != null)
				{
					return new SecureRandom(digestRandomGenerator);
				}
			}
			throw new ArgumentException("Unrecognised PRNG algorithm: " + algorithm, "algorithm");
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x000C8C40 File Offset: 0x000C8C40
		[Obsolete("Call GenerateSeed() on a SecureRandom instance instead")]
		public static byte[] GetSeed(int length)
		{
			return SecureRandom.GetNextBytes(SecureRandom.Master, length);
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x000C8C50 File Offset: 0x000C8C50
		public SecureRandom() : this(SecureRandom.CreatePrng("SHA256", true))
		{
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x000C8C64 File Offset: 0x000C8C64
		[Obsolete("Use GetInstance/SetSeed instead")]
		public SecureRandom(byte[] seed) : this(SecureRandom.CreatePrng("SHA1", false))
		{
			this.SetSeed(seed);
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x000C8C80 File Offset: 0x000C8C80
		public SecureRandom(IRandomGenerator generator) : base(0)
		{
			this.generator = generator;
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x000C8C90 File Offset: 0x000C8C90
		public virtual byte[] GenerateSeed(int length)
		{
			return SecureRandom.GetNextBytes(SecureRandom.Master, length);
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x000C8CA0 File Offset: 0x000C8CA0
		public virtual void SetSeed(byte[] seed)
		{
			this.generator.AddSeedMaterial(seed);
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x000C8CB0 File Offset: 0x000C8CB0
		public virtual void SetSeed(long seed)
		{
			this.generator.AddSeedMaterial(seed);
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x000C8CC0 File Offset: 0x000C8CC0
		public override int Next()
		{
			return this.NextInt() & int.MaxValue;
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x000C8CD0 File Offset: 0x000C8CD0
		public override int Next(int maxValue)
		{
			if (maxValue < 2)
			{
				if (maxValue < 0)
				{
					throw new ArgumentOutOfRangeException("maxValue", "cannot be negative");
				}
				return 0;
			}
			else
			{
				int num;
				if ((maxValue & maxValue - 1) == 0)
				{
					num = (this.NextInt() & int.MaxValue);
					return (int)((long)num * (long)maxValue >> 31);
				}
				int num2;
				do
				{
					num = (this.NextInt() & int.MaxValue);
					num2 = num % maxValue;
				}
				while (num - num2 + (maxValue - 1) < 0);
				return num2;
			}
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x000C8D40 File Offset: 0x000C8D40
		public override int Next(int minValue, int maxValue)
		{
			if (maxValue <= minValue)
			{
				if (maxValue == minValue)
				{
					return minValue;
				}
				throw new ArgumentException("maxValue cannot be less than minValue");
			}
			else
			{
				int num = maxValue - minValue;
				if (num > 0)
				{
					return minValue + this.Next(num);
				}
				int num2;
				do
				{
					num2 = this.NextInt();
				}
				while (num2 < minValue || num2 >= maxValue);
				return num2;
			}
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x000C8D94 File Offset: 0x000C8D94
		public override void NextBytes(byte[] buf)
		{
			this.generator.NextBytes(buf);
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x000C8DA4 File Offset: 0x000C8DA4
		public virtual void NextBytes(byte[] buf, int off, int len)
		{
			this.generator.NextBytes(buf, off, len);
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x000C8DB4 File Offset: 0x000C8DB4
		public override double NextDouble()
		{
			return Convert.ToDouble((ulong)this.NextLong()) / SecureRandom.DoubleScale;
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x000C8DC8 File Offset: 0x000C8DC8
		public virtual int NextInt()
		{
			byte[] array = new byte[4];
			this.NextBytes(array);
			uint num = (uint)array[0];
			num <<= 8;
			num |= (uint)array[1];
			num <<= 8;
			num |= (uint)array[2];
			num <<= 8;
			return (int)(num | (uint)array[3]);
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x000C8E0C File Offset: 0x000C8E0C
		public virtual long NextLong()
		{
			return (long)((ulong)this.NextInt() << 32 | (ulong)this.NextInt());
		}

		// Token: 0x040016C1 RID: 5825
		private static long counter = Times.NanoTime();

		// Token: 0x040016C2 RID: 5826
		private static readonly SecureRandom master = new SecureRandom(new CryptoApiRandomGenerator());

		// Token: 0x040016C3 RID: 5827
		protected readonly IRandomGenerator generator;

		// Token: 0x040016C4 RID: 5828
		private static readonly double DoubleScale = Math.Pow(2.0, 64.0);
	}
}
