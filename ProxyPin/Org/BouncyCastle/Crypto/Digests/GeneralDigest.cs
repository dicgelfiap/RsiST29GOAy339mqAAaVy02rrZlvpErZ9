using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000351 RID: 849
	public abstract class GeneralDigest : IDigest, IMemoable
	{
		// Token: 0x0600195F RID: 6495 RVA: 0x0008395C File Offset: 0x0008395C
		internal GeneralDigest()
		{
			this.xBuf = new byte[4];
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x00083970 File Offset: 0x00083970
		internal GeneralDigest(GeneralDigest t)
		{
			this.xBuf = new byte[t.xBuf.Length];
			this.CopyIn(t);
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x00083994 File Offset: 0x00083994
		protected void CopyIn(GeneralDigest t)
		{
			Array.Copy(t.xBuf, 0, this.xBuf, 0, t.xBuf.Length);
			this.xBufOff = t.xBufOff;
			this.byteCount = t.byteCount;
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x000839CC File Offset: 0x000839CC
		public void Update(byte input)
		{
			this.xBuf[this.xBufOff++] = input;
			if (this.xBufOff == this.xBuf.Length)
			{
				this.ProcessWord(this.xBuf, 0);
				this.xBufOff = 0;
			}
			this.byteCount += 1L;
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x00083A2C File Offset: 0x00083A2C
		public void BlockUpdate(byte[] input, int inOff, int length)
		{
			length = Math.Max(0, length);
			int i = 0;
			if (this.xBufOff != 0)
			{
				while (i < length)
				{
					this.xBuf[this.xBufOff++] = input[inOff + i++];
					if (this.xBufOff == 4)
					{
						this.ProcessWord(this.xBuf, 0);
						this.xBufOff = 0;
						break;
					}
				}
			}
			int num = (length - i & -4) + i;
			while (i < num)
			{
				this.ProcessWord(input, inOff + i);
				i += 4;
			}
			while (i < length)
			{
				this.xBuf[this.xBufOff++] = input[inOff + i++];
			}
			this.byteCount += (long)length;
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x00083AFC File Offset: 0x00083AFC
		public void Finish()
		{
			long bitLength = this.byteCount << 3;
			this.Update(128);
			while (this.xBufOff != 0)
			{
				this.Update(0);
			}
			this.ProcessLength(bitLength);
			this.ProcessBlock();
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x00083B44 File Offset: 0x00083B44
		public virtual void Reset()
		{
			this.byteCount = 0L;
			this.xBufOff = 0;
			Array.Clear(this.xBuf, 0, this.xBuf.Length);
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x00083B6C File Offset: 0x00083B6C
		public int GetByteLength()
		{
			return 64;
		}

		// Token: 0x06001967 RID: 6503
		internal abstract void ProcessWord(byte[] input, int inOff);

		// Token: 0x06001968 RID: 6504
		internal abstract void ProcessLength(long bitLength);

		// Token: 0x06001969 RID: 6505
		internal abstract void ProcessBlock();

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x0600196A RID: 6506
		public abstract string AlgorithmName { get; }

		// Token: 0x0600196B RID: 6507
		public abstract int GetDigestSize();

		// Token: 0x0600196C RID: 6508
		public abstract int DoFinal(byte[] output, int outOff);

		// Token: 0x0600196D RID: 6509
		public abstract IMemoable Copy();

		// Token: 0x0600196E RID: 6510
		public abstract void Reset(IMemoable t);

		// Token: 0x0400110B RID: 4363
		private const int BYTE_LENGTH = 64;

		// Token: 0x0400110C RID: 4364
		private byte[] xBuf;

		// Token: 0x0400110D RID: 4365
		private int xBufOff;

		// Token: 0x0400110E RID: 4366
		private long byteCount;
	}
}
