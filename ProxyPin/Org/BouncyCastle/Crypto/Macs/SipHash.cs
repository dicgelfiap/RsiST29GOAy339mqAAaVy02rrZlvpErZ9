using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x020003ED RID: 1005
	public class SipHash : IMac
	{
		// Token: 0x06001FE6 RID: 8166 RVA: 0x000B9C4C File Offset: 0x000B9C4C
		public SipHash() : this(2, 4)
		{
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x000B9C58 File Offset: 0x000B9C58
		public SipHash(int c, int d)
		{
			this.c = c;
			this.d = d;
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001FE8 RID: 8168 RVA: 0x000B9C84 File Offset: 0x000B9C84
		public virtual string AlgorithmName
		{
			get
			{
				return string.Concat(new object[]
				{
					"SipHash-",
					this.c,
					"-",
					this.d
				});
			}
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x000B9CD0 File Offset: 0x000B9CD0
		public virtual int GetMacSize()
		{
			return 8;
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x000B9CD4 File Offset: 0x000B9CD4
		public virtual void Init(ICipherParameters parameters)
		{
			KeyParameter keyParameter = parameters as KeyParameter;
			if (keyParameter == null)
			{
				throw new ArgumentException("must be an instance of KeyParameter", "parameters");
			}
			byte[] key = keyParameter.GetKey();
			if (key.Length != 16)
			{
				throw new ArgumentException("must be a 128-bit key", "parameters");
			}
			this.k0 = (long)Pack.LE_To_UInt64(key, 0);
			this.k1 = (long)Pack.LE_To_UInt64(key, 8);
			this.Reset();
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x000B9D44 File Offset: 0x000B9D44
		public virtual void Update(byte input)
		{
			this.m = (long)((ulong)this.m >> 8 | (ulong)input << 56);
			if (++this.wordPos == 8)
			{
				this.ProcessMessageWord();
				this.wordPos = 0;
			}
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x000B9D90 File Offset: 0x000B9D90
		public virtual void BlockUpdate(byte[] input, int offset, int length)
		{
			int i = 0;
			int num = length & -8;
			if (this.wordPos == 0)
			{
				while (i < num)
				{
					this.m = (long)Pack.LE_To_UInt64(input, offset + i);
					this.ProcessMessageWord();
					i += 8;
				}
				while (i < length)
				{
					this.m = (long)((ulong)this.m >> 8 | (ulong)input[offset + i] << 56);
					i++;
				}
				this.wordPos = length - num;
				return;
			}
			int num2 = this.wordPos << 3;
			while (i < num)
			{
				ulong num3 = Pack.LE_To_UInt64(input, offset + i);
				this.m = (long)(num3 << num2 | (ulong)this.m >> -num2);
				this.ProcessMessageWord();
				this.m = (long)num3;
				i += 8;
			}
			while (i < length)
			{
				this.m = (long)((ulong)this.m >> 8 | (ulong)input[offset + i] << 56);
				if (++this.wordPos == 8)
				{
					this.ProcessMessageWord();
					this.wordPos = 0;
				}
				i++;
			}
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x000B9E98 File Offset: 0x000B9E98
		public virtual long DoFinal()
		{
			this.m = (long)((ulong)this.m >> (7 - this.wordPos << 3));
			this.m = (long)((ulong)this.m >> 8);
			this.m |= (long)((this.wordCount << 3) + this.wordPos) << 56;
			this.ProcessMessageWord();
			this.v2 ^= 255L;
			this.ApplySipRounds(this.d);
			long result = this.v0 ^ this.v1 ^ this.v2 ^ this.v3;
			this.Reset();
			return result;
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x000B9F3C File Offset: 0x000B9F3C
		public virtual int DoFinal(byte[] output, int outOff)
		{
			long n = this.DoFinal();
			Pack.UInt64_To_LE((ulong)n, output, outOff);
			return 8;
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x000B9F60 File Offset: 0x000B9F60
		public virtual void Reset()
		{
			this.v0 = (this.k0 ^ 8317987319222330741L);
			this.v1 = (this.k1 ^ 7237128888997146477L);
			this.v2 = (this.k0 ^ 7816392313619706465L);
			this.v3 = (this.k1 ^ 8387220255154660723L);
			this.m = 0L;
			this.wordPos = 0;
			this.wordCount = 0;
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x000B9FE0 File Offset: 0x000B9FE0
		protected virtual void ProcessMessageWord()
		{
			this.wordCount++;
			this.v3 ^= this.m;
			this.ApplySipRounds(this.c);
			this.v0 ^= this.m;
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x000BA034 File Offset: 0x000BA034
		protected virtual void ApplySipRounds(int n)
		{
			long num = this.v0;
			long num2 = this.v1;
			long num3 = this.v2;
			long num4 = this.v3;
			for (int i = 0; i < n; i++)
			{
				num += num2;
				num3 += num4;
				num2 = SipHash.RotateLeft(num2, 13);
				num4 = SipHash.RotateLeft(num4, 16);
				num2 ^= num;
				num4 ^= num3;
				num = SipHash.RotateLeft(num, 32);
				num3 += num2;
				num += num4;
				num2 = SipHash.RotateLeft(num2, 17);
				num4 = SipHash.RotateLeft(num4, 21);
				num2 ^= num3;
				num4 ^= num;
				num3 = SipHash.RotateLeft(num3, 32);
			}
			this.v0 = num;
			this.v1 = num2;
			this.v2 = num3;
			this.v3 = num4;
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x000BA0E8 File Offset: 0x000BA0E8
		protected static long RotateLeft(long x, int n)
		{
			return x << n | (long)((ulong)x >> -n);
		}

		// Token: 0x040014EE RID: 5358
		protected readonly int c;

		// Token: 0x040014EF RID: 5359
		protected readonly int d;

		// Token: 0x040014F0 RID: 5360
		protected long k0;

		// Token: 0x040014F1 RID: 5361
		protected long k1;

		// Token: 0x040014F2 RID: 5362
		protected long v0;

		// Token: 0x040014F3 RID: 5363
		protected long v1;

		// Token: 0x040014F4 RID: 5364
		protected long v2;

		// Token: 0x040014F5 RID: 5365
		protected long v3;

		// Token: 0x040014F6 RID: 5366
		protected long m = 0L;

		// Token: 0x040014F7 RID: 5367
		protected int wordPos = 0;

		// Token: 0x040014F8 RID: 5368
		protected int wordCount = 0;
	}
}
