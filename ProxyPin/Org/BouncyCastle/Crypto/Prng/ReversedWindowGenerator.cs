using System;

namespace Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x0200048C RID: 1164
	public class ReversedWindowGenerator : IRandomGenerator
	{
		// Token: 0x060023DA RID: 9178 RVA: 0x000C89B8 File Offset: 0x000C89B8
		public ReversedWindowGenerator(IRandomGenerator generator, int windowSize)
		{
			if (generator == null)
			{
				throw new ArgumentNullException("generator");
			}
			if (windowSize < 2)
			{
				throw new ArgumentException("Window size must be at least 2", "windowSize");
			}
			this.generator = generator;
			this.window = new byte[windowSize];
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000C8A0C File Offset: 0x000C8A0C
		public virtual void AddSeedMaterial(byte[] seed)
		{
			lock (this)
			{
				this.windowCount = 0;
				this.generator.AddSeedMaterial(seed);
			}
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x000C8A50 File Offset: 0x000C8A50
		public virtual void AddSeedMaterial(long seed)
		{
			lock (this)
			{
				this.windowCount = 0;
				this.generator.AddSeedMaterial(seed);
			}
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x000C8A94 File Offset: 0x000C8A94
		public virtual void NextBytes(byte[] bytes)
		{
			this.doNextBytes(bytes, 0, bytes.Length);
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x000C8AA4 File Offset: 0x000C8AA4
		public virtual void NextBytes(byte[] bytes, int start, int len)
		{
			this.doNextBytes(bytes, start, len);
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x000C8AB0 File Offset: 0x000C8AB0
		private void doNextBytes(byte[] bytes, int start, int len)
		{
			lock (this)
			{
				int i = 0;
				while (i < len)
				{
					if (this.windowCount < 1)
					{
						this.generator.NextBytes(this.window, 0, this.window.Length);
						this.windowCount = this.window.Length;
					}
					bytes[start + i++] = this.window[--this.windowCount];
				}
			}
		}

		// Token: 0x040016BE RID: 5822
		private readonly IRandomGenerator generator;

		// Token: 0x040016BF RID: 5823
		private byte[] window;

		// Token: 0x040016C0 RID: 5824
		private int windowCount;
	}
}
