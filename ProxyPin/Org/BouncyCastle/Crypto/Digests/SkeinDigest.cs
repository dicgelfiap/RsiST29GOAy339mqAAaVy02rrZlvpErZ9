using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x0200036B RID: 875
	public class SkeinDigest : IDigest, IMemoable
	{
		// Token: 0x06001ADD RID: 6877 RVA: 0x000911F0 File Offset: 0x000911F0
		public SkeinDigest(int stateSizeBits, int digestSizeBits)
		{
			this.engine = new SkeinEngine(stateSizeBits, digestSizeBits);
			this.Init(null);
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0009120C File Offset: 0x0009120C
		public SkeinDigest(SkeinDigest digest)
		{
			this.engine = new SkeinEngine(digest.engine);
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x00091228 File Offset: 0x00091228
		public void Reset(IMemoable other)
		{
			SkeinDigest skeinDigest = (SkeinDigest)other;
			this.engine.Reset(skeinDigest.engine);
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x00091254 File Offset: 0x00091254
		public IMemoable Copy()
		{
			return new SkeinDigest(this);
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x0009125C File Offset: 0x0009125C
		public string AlgorithmName
		{
			get
			{
				return string.Concat(new object[]
				{
					"Skein-",
					this.engine.BlockSize * 8,
					"-",
					this.engine.OutputSize * 8
				});
			}
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x000912B4 File Offset: 0x000912B4
		public int GetDigestSize()
		{
			return this.engine.OutputSize;
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x000912C4 File Offset: 0x000912C4
		public int GetByteLength()
		{
			return this.engine.BlockSize;
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x000912D4 File Offset: 0x000912D4
		public void Init(SkeinParameters parameters)
		{
			this.engine.Init(parameters);
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x000912E4 File Offset: 0x000912E4
		public void Reset()
		{
			this.engine.Reset();
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x000912F4 File Offset: 0x000912F4
		public void Update(byte inByte)
		{
			this.engine.Update(inByte);
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00091304 File Offset: 0x00091304
		public void BlockUpdate(byte[] inBytes, int inOff, int len)
		{
			this.engine.Update(inBytes, inOff, len);
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x00091314 File Offset: 0x00091314
		public int DoFinal(byte[] outBytes, int outOff)
		{
			return this.engine.DoFinal(outBytes, outOff);
		}

		// Token: 0x040011D6 RID: 4566
		public const int SKEIN_256 = 256;

		// Token: 0x040011D7 RID: 4567
		public const int SKEIN_512 = 512;

		// Token: 0x040011D8 RID: 4568
		public const int SKEIN_1024 = 1024;

		// Token: 0x040011D9 RID: 4569
		private readonly SkeinEngine engine;
	}
}
