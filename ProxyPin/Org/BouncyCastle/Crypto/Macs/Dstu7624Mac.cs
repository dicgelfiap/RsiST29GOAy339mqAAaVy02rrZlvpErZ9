using System;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x020003E7 RID: 999
	public class Dstu7624Mac : IMac
	{
		// Token: 0x06001FA4 RID: 8100 RVA: 0x000B7FAC File Offset: 0x000B7FAC
		public Dstu7624Mac(int blockSizeBits, int q)
		{
			this.engine = new Dstu7624Engine(blockSizeBits);
			this.blockSize = blockSizeBits / 8;
			this.macSize = q / 8;
			this.c = new byte[this.blockSize];
			this.cTemp = new byte[this.blockSize];
			this.kDelta = new byte[this.blockSize];
			this.buf = new byte[this.blockSize];
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x000B8028 File Offset: 0x000B8028
		public void Init(ICipherParameters parameters)
		{
			if (parameters is KeyParameter)
			{
				this.engine.Init(true, (KeyParameter)parameters);
				this.engine.ProcessBlock(this.kDelta, 0, this.kDelta, 0);
				return;
			}
			throw new ArgumentException("invalid parameter passed to Dstu7624Mac init - " + Platform.GetTypeName(parameters));
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001FA6 RID: 8102 RVA: 0x000B8088 File Offset: 0x000B8088
		public string AlgorithmName
		{
			get
			{
				return "Dstu7624Mac";
			}
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x000B8090 File Offset: 0x000B8090
		public int GetMacSize()
		{
			return this.macSize;
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x000B8098 File Offset: 0x000B8098
		public void Update(byte input)
		{
			if (this.bufOff == this.buf.Length)
			{
				this.processBlock(this.buf, 0);
				this.bufOff = 0;
			}
			this.buf[this.bufOff++] = input;
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x000B80EC File Offset: 0x000B80EC
		public void BlockUpdate(byte[] input, int inOff, int len)
		{
			if (len < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			int num = this.engine.GetBlockSize();
			int num2 = num - this.bufOff;
			if (len > num2)
			{
				Array.Copy(input, inOff, this.buf, this.bufOff, num2);
				this.processBlock(this.buf, 0);
				this.bufOff = 0;
				len -= num2;
				inOff += num2;
				while (len > num)
				{
					this.processBlock(input, inOff);
					len -= num;
					inOff += num;
				}
			}
			Array.Copy(input, inOff, this.buf, this.bufOff, len);
			this.bufOff += len;
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x000B819C File Offset: 0x000B819C
		private void processBlock(byte[] input, int inOff)
		{
			this.Xor(this.c, 0, input, inOff, this.cTemp);
			this.engine.ProcessBlock(this.cTemp, 0, this.c, 0);
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x000B81D0 File Offset: 0x000B81D0
		private void Xor(byte[] c, int cOff, byte[] input, int inOff, byte[] xorResult)
		{
			for (int i = 0; i < this.blockSize; i++)
			{
				xorResult[i] = (c[i + cOff] ^ input[i + inOff]);
			}
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x000B8208 File Offset: 0x000B8208
		public int DoFinal(byte[] output, int outOff)
		{
			if (this.bufOff % this.buf.Length != 0)
			{
				throw new DataLengthException("Input must be a multiple of blocksize");
			}
			this.Xor(this.c, 0, this.buf, 0, this.cTemp);
			this.Xor(this.cTemp, 0, this.kDelta, 0, this.c);
			this.engine.ProcessBlock(this.c, 0, this.c, 0);
			if (this.macSize + outOff > output.Length)
			{
				throw new DataLengthException("Output buffer too short");
			}
			Array.Copy(this.c, 0, output, outOff, this.macSize);
			return this.macSize;
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x000B82BC File Offset: 0x000B82BC
		public void Reset()
		{
			Arrays.Fill(this.c, 0);
			Arrays.Fill(this.cTemp, 0);
			Arrays.Fill(this.kDelta, 0);
			Arrays.Fill(this.buf, 0);
			this.engine.Reset();
			this.engine.ProcessBlock(this.kDelta, 0, this.kDelta, 0);
			this.bufOff = 0;
		}

		// Token: 0x040014B3 RID: 5299
		private int macSize;

		// Token: 0x040014B4 RID: 5300
		private Dstu7624Engine engine;

		// Token: 0x040014B5 RID: 5301
		private int blockSize;

		// Token: 0x040014B6 RID: 5302
		private byte[] c;

		// Token: 0x040014B7 RID: 5303
		private byte[] cTemp;

		// Token: 0x040014B8 RID: 5304
		private byte[] kDelta;

		// Token: 0x040014B9 RID: 5305
		private byte[] buf;

		// Token: 0x040014BA RID: 5306
		private int bufOff;
	}
}
