using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x020003E6 RID: 998
	public class Dstu7564Mac : IMac
	{
		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x000B7C98 File Offset: 0x000B7C98
		public string AlgorithmName
		{
			get
			{
				return "DSTU7564Mac";
			}
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x000B7CA0 File Offset: 0x000B7CA0
		public Dstu7564Mac(int macSizeBits)
		{
			this.engine = new Dstu7564Digest(macSizeBits);
			this.macSize = macSizeBits / 8;
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x000B7CC0 File Offset: 0x000B7CC0
		public void Init(ICipherParameters parameters)
		{
			if (parameters is KeyParameter)
			{
				byte[] key = ((KeyParameter)parameters).GetKey();
				this.invertedKey = new byte[key.Length];
				this.paddedKey = this.PadKey(key);
				for (int i = 0; i < this.invertedKey.Length; i++)
				{
					this.invertedKey[i] = (key[i] ^ byte.MaxValue);
				}
				this.engine.BlockUpdate(this.paddedKey, 0, this.paddedKey.Length);
				return;
			}
			throw new ArgumentException("Bad parameter passed");
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x000B7D58 File Offset: 0x000B7D58
		public int GetMacSize()
		{
			return this.macSize;
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x000B7D60 File Offset: 0x000B7D60
		public void BlockUpdate(byte[] input, int inOff, int len)
		{
			Check.DataLength(input, inOff, len, "Input buffer too short");
			if (this.paddedKey == null)
			{
				throw new InvalidOperationException(this.AlgorithmName + " not initialised");
			}
			this.engine.BlockUpdate(input, inOff, len);
			this.inputLength += (ulong)((long)len);
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x000B7DBC File Offset: 0x000B7DBC
		public void Update(byte input)
		{
			this.engine.Update(input);
			this.inputLength += 1UL;
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x000B7DDC File Offset: 0x000B7DDC
		public int DoFinal(byte[] output, int outOff)
		{
			Check.OutputLength(output, outOff, this.macSize, "Output buffer too short");
			if (this.paddedKey == null)
			{
				throw new InvalidOperationException(this.AlgorithmName + " not initialised");
			}
			this.Pad();
			this.engine.BlockUpdate(this.invertedKey, 0, this.invertedKey.Length);
			this.inputLength = 0UL;
			return this.engine.DoFinal(output, outOff);
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x000B7E58 File Offset: 0x000B7E58
		public void Reset()
		{
			this.inputLength = 0UL;
			this.engine.Reset();
			if (this.paddedKey != null)
			{
				this.engine.BlockUpdate(this.paddedKey, 0, this.paddedKey.Length);
			}
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x000B7E94 File Offset: 0x000B7E94
		private void Pad()
		{
			int num = this.engine.GetByteLength() - (int)(this.inputLength % (ulong)((long)this.engine.GetByteLength()));
			if (num < 13)
			{
				num += this.engine.GetByteLength();
			}
			byte[] array = new byte[num];
			array[0] = 128;
			Pack.UInt64_To_LE(this.inputLength * 8UL, array, array.Length - 12);
			this.engine.BlockUpdate(array, 0, array.Length);
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x000B7F10 File Offset: 0x000B7F10
		private byte[] PadKey(byte[] input)
		{
			int num = (input.Length + this.engine.GetByteLength() - 1) / this.engine.GetByteLength() * this.engine.GetByteLength();
			int num2 = this.engine.GetByteLength() - input.Length % this.engine.GetByteLength();
			if (num2 < 13)
			{
				num += this.engine.GetByteLength();
			}
			byte[] array = new byte[num];
			Array.Copy(input, 0, array, 0, input.Length);
			array[input.Length] = 128;
			Pack.UInt32_To_LE((uint)(input.Length * 8), array, array.Length - 12);
			return array;
		}

		// Token: 0x040014AE RID: 5294
		private Dstu7564Digest engine;

		// Token: 0x040014AF RID: 5295
		private int macSize;

		// Token: 0x040014B0 RID: 5296
		private ulong inputLength;

		// Token: 0x040014B1 RID: 5297
		private byte[] paddedKey;

		// Token: 0x040014B2 RID: 5298
		private byte[] invertedKey;
	}
}
