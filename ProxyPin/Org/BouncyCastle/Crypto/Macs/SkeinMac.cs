using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x020003EE RID: 1006
	public class SkeinMac : IMac
	{
		// Token: 0x06001FF3 RID: 8179 RVA: 0x000BA10C File Offset: 0x000BA10C
		public SkeinMac(int stateSizeBits, int digestSizeBits)
		{
			this.engine = new SkeinEngine(stateSizeBits, digestSizeBits);
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x000BA124 File Offset: 0x000BA124
		public SkeinMac(SkeinMac mac)
		{
			this.engine = new SkeinEngine(mac.engine);
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001FF5 RID: 8181 RVA: 0x000BA140 File Offset: 0x000BA140
		public string AlgorithmName
		{
			get
			{
				return string.Concat(new object[]
				{
					"Skein-MAC-",
					this.engine.BlockSize * 8,
					"-",
					this.engine.OutputSize * 8
				});
			}
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x000BA198 File Offset: 0x000BA198
		public void Init(ICipherParameters parameters)
		{
			SkeinParameters skeinParameters;
			if (parameters is SkeinParameters)
			{
				skeinParameters = (SkeinParameters)parameters;
			}
			else
			{
				if (!(parameters is KeyParameter))
				{
					throw new ArgumentException("Invalid parameter passed to Skein MAC init - " + Platform.GetTypeName(parameters));
				}
				skeinParameters = new SkeinParameters.Builder().SetKey(((KeyParameter)parameters).GetKey()).Build();
			}
			if (skeinParameters.GetKey() == null)
			{
				throw new ArgumentException("Skein MAC requires a key parameter.");
			}
			this.engine.Init(skeinParameters);
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x000BA224 File Offset: 0x000BA224
		public int GetMacSize()
		{
			return this.engine.OutputSize;
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x000BA234 File Offset: 0x000BA234
		public void Reset()
		{
			this.engine.Reset();
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x000BA244 File Offset: 0x000BA244
		public void Update(byte inByte)
		{
			this.engine.Update(inByte);
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x000BA254 File Offset: 0x000BA254
		public void BlockUpdate(byte[] input, int inOff, int len)
		{
			this.engine.Update(input, inOff, len);
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x000BA264 File Offset: 0x000BA264
		public int DoFinal(byte[] output, int outOff)
		{
			return this.engine.DoFinal(output, outOff);
		}

		// Token: 0x040014F9 RID: 5369
		public const int SKEIN_256 = 256;

		// Token: 0x040014FA RID: 5370
		public const int SKEIN_512 = 512;

		// Token: 0x040014FB RID: 5371
		public const int SKEIN_1024 = 1024;

		// Token: 0x040014FC RID: 5372
		private readonly SkeinEngine engine;
	}
}
