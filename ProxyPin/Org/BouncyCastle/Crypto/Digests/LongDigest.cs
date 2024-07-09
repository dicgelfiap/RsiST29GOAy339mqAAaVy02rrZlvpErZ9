using System;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000357 RID: 855
	public abstract class LongDigest : IDigest, IMemoable
	{
		// Token: 0x060019BD RID: 6589 RVA: 0x000863EC File Offset: 0x000863EC
		internal LongDigest()
		{
			this.xBuf = new byte[8];
			this.Reset();
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x00086420 File Offset: 0x00086420
		internal LongDigest(LongDigest t)
		{
			this.xBuf = new byte[t.xBuf.Length];
			this.CopyIn(t);
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x0008645C File Offset: 0x0008645C
		protected void CopyIn(LongDigest t)
		{
			Array.Copy(t.xBuf, 0, this.xBuf, 0, t.xBuf.Length);
			this.xBufOff = t.xBufOff;
			this.byteCount1 = t.byteCount1;
			this.byteCount2 = t.byteCount2;
			this.H1 = t.H1;
			this.H2 = t.H2;
			this.H3 = t.H3;
			this.H4 = t.H4;
			this.H5 = t.H5;
			this.H6 = t.H6;
			this.H7 = t.H7;
			this.H8 = t.H8;
			Array.Copy(t.W, 0, this.W, 0, t.W.Length);
			this.wOff = t.wOff;
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x00086534 File Offset: 0x00086534
		public void Update(byte input)
		{
			this.xBuf[this.xBufOff++] = input;
			if (this.xBufOff == this.xBuf.Length)
			{
				this.ProcessWord(this.xBuf, 0);
				this.xBufOff = 0;
			}
			this.byteCount1 += 1L;
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x00086594 File Offset: 0x00086594
		public void BlockUpdate(byte[] input, int inOff, int length)
		{
			while (this.xBufOff != 0)
			{
				if (length <= 0)
				{
					break;
				}
				this.Update(input[inOff]);
				inOff++;
				length--;
			}
			while (length > this.xBuf.Length)
			{
				this.ProcessWord(input, inOff);
				inOff += this.xBuf.Length;
				length -= this.xBuf.Length;
				this.byteCount1 += (long)this.xBuf.Length;
			}
			while (length > 0)
			{
				this.Update(input[inOff]);
				inOff++;
				length--;
			}
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x00086630 File Offset: 0x00086630
		public void Finish()
		{
			this.AdjustByteCounts();
			long lowW = this.byteCount1 << 3;
			long hiW = this.byteCount2;
			this.Update(128);
			while (this.xBufOff != 0)
			{
				this.Update(0);
			}
			this.ProcessLength(lowW, hiW);
			this.ProcessBlock();
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x00086684 File Offset: 0x00086684
		public virtual void Reset()
		{
			this.byteCount1 = 0L;
			this.byteCount2 = 0L;
			this.xBufOff = 0;
			for (int i = 0; i < this.xBuf.Length; i++)
			{
				this.xBuf[i] = 0;
			}
			this.wOff = 0;
			Array.Clear(this.W, 0, this.W.Length);
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x000866E8 File Offset: 0x000866E8
		internal void ProcessWord(byte[] input, int inOff)
		{
			this.W[this.wOff] = Pack.BE_To_UInt64(input, inOff);
			if (++this.wOff == 16)
			{
				this.ProcessBlock();
			}
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x0008672C File Offset: 0x0008672C
		private void AdjustByteCounts()
		{
			if (this.byteCount1 > 2305843009213693951L)
			{
				this.byteCount2 += (long)((ulong)this.byteCount1 >> 61);
				this.byteCount1 &= 2305843009213693951L;
			}
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x00086780 File Offset: 0x00086780
		internal void ProcessLength(long lowW, long hiW)
		{
			if (this.wOff > 14)
			{
				this.ProcessBlock();
			}
			this.W[14] = (ulong)hiW;
			this.W[15] = (ulong)lowW;
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x000867AC File Offset: 0x000867AC
		internal void ProcessBlock()
		{
			this.AdjustByteCounts();
			for (int i = 16; i <= 79; i++)
			{
				this.W[i] = LongDigest.Sigma1(this.W[i - 2]) + this.W[i - 7] + LongDigest.Sigma0(this.W[i - 15]) + this.W[i - 16];
			}
			ulong num = this.H1;
			ulong num2 = this.H2;
			ulong num3 = this.H3;
			ulong num4 = this.H4;
			ulong num5 = this.H5;
			ulong num6 = this.H6;
			ulong num7 = this.H7;
			ulong num8 = this.H8;
			int num9 = 0;
			for (int j = 0; j < 10; j++)
			{
				num8 += LongDigest.Sum1(num5) + LongDigest.Ch(num5, num6, num7) + LongDigest.K[num9] + this.W[num9++];
				num4 += num8;
				num8 += LongDigest.Sum0(num) + LongDigest.Maj(num, num2, num3);
				num7 += LongDigest.Sum1(num4) + LongDigest.Ch(num4, num5, num6) + LongDigest.K[num9] + this.W[num9++];
				num3 += num7;
				num7 += LongDigest.Sum0(num8) + LongDigest.Maj(num8, num, num2);
				num6 += LongDigest.Sum1(num3) + LongDigest.Ch(num3, num4, num5) + LongDigest.K[num9] + this.W[num9++];
				num2 += num6;
				num6 += LongDigest.Sum0(num7) + LongDigest.Maj(num7, num8, num);
				num5 += LongDigest.Sum1(num2) + LongDigest.Ch(num2, num3, num4) + LongDigest.K[num9] + this.W[num9++];
				num += num5;
				num5 += LongDigest.Sum0(num6) + LongDigest.Maj(num6, num7, num8);
				num4 += LongDigest.Sum1(num) + LongDigest.Ch(num, num2, num3) + LongDigest.K[num9] + this.W[num9++];
				num8 += num4;
				num4 += LongDigest.Sum0(num5) + LongDigest.Maj(num5, num6, num7);
				num3 += LongDigest.Sum1(num8) + LongDigest.Ch(num8, num, num2) + LongDigest.K[num9] + this.W[num9++];
				num7 += num3;
				num3 += LongDigest.Sum0(num4) + LongDigest.Maj(num4, num5, num6);
				num2 += LongDigest.Sum1(num7) + LongDigest.Ch(num7, num8, num) + LongDigest.K[num9] + this.W[num9++];
				num6 += num2;
				num2 += LongDigest.Sum0(num3) + LongDigest.Maj(num3, num4, num5);
				num += LongDigest.Sum1(num6) + LongDigest.Ch(num6, num7, num8) + LongDigest.K[num9] + this.W[num9++];
				num5 += num;
				num += LongDigest.Sum0(num2) + LongDigest.Maj(num2, num3, num4);
			}
			this.H1 += num;
			this.H2 += num2;
			this.H3 += num3;
			this.H4 += num4;
			this.H5 += num5;
			this.H6 += num6;
			this.H7 += num7;
			this.H8 += num8;
			this.wOff = 0;
			Array.Clear(this.W, 0, 16);
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x00086B40 File Offset: 0x00086B40
		private static ulong Ch(ulong x, ulong y, ulong z)
		{
			return (x & y) ^ (~x & z);
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x00086B4C File Offset: 0x00086B4C
		private static ulong Maj(ulong x, ulong y, ulong z)
		{
			return (x & y) ^ (x & z) ^ (y & z);
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x00086B5C File Offset: 0x00086B5C
		private static ulong Sum0(ulong x)
		{
			return (x << 36 | x >> 28) ^ (x << 30 | x >> 34) ^ (x << 25 | x >> 39);
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x00086B7C File Offset: 0x00086B7C
		private static ulong Sum1(ulong x)
		{
			return (x << 50 | x >> 14) ^ (x << 46 | x >> 18) ^ (x << 23 | x >> 41);
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x00086B9C File Offset: 0x00086B9C
		private static ulong Sigma0(ulong x)
		{
			return (x << 63 | x >> 1) ^ (x << 56 | x >> 8) ^ x >> 7;
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x00086BB4 File Offset: 0x00086BB4
		private static ulong Sigma1(ulong x)
		{
			return (x << 45 | x >> 19) ^ (x << 3 | x >> 61) ^ x >> 6;
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x00086BCC File Offset: 0x00086BCC
		public int GetByteLength()
		{
			return this.MyByteLength;
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060019CF RID: 6607
		public abstract string AlgorithmName { get; }

		// Token: 0x060019D0 RID: 6608
		public abstract int GetDigestSize();

		// Token: 0x060019D1 RID: 6609
		public abstract int DoFinal(byte[] output, int outOff);

		// Token: 0x060019D2 RID: 6610
		public abstract IMemoable Copy();

		// Token: 0x060019D3 RID: 6611
		public abstract void Reset(IMemoable t);

		// Token: 0x04001138 RID: 4408
		private int MyByteLength = 128;

		// Token: 0x04001139 RID: 4409
		private byte[] xBuf;

		// Token: 0x0400113A RID: 4410
		private int xBufOff;

		// Token: 0x0400113B RID: 4411
		private long byteCount1;

		// Token: 0x0400113C RID: 4412
		private long byteCount2;

		// Token: 0x0400113D RID: 4413
		internal ulong H1;

		// Token: 0x0400113E RID: 4414
		internal ulong H2;

		// Token: 0x0400113F RID: 4415
		internal ulong H3;

		// Token: 0x04001140 RID: 4416
		internal ulong H4;

		// Token: 0x04001141 RID: 4417
		internal ulong H5;

		// Token: 0x04001142 RID: 4418
		internal ulong H6;

		// Token: 0x04001143 RID: 4419
		internal ulong H7;

		// Token: 0x04001144 RID: 4420
		internal ulong H8;

		// Token: 0x04001145 RID: 4421
		private ulong[] W = new ulong[80];

		// Token: 0x04001146 RID: 4422
		private int wOff;

		// Token: 0x04001147 RID: 4423
		internal static readonly ulong[] K = new ulong[]
		{
			4794697086780616226UL,
			8158064640168781261UL,
			13096744586834688815UL,
			16840607885511220156UL,
			4131703408338449720UL,
			6480981068601479193UL,
			10538285296894168987UL,
			12329834152419229976UL,
			15566598209576043074UL,
			1334009975649890238UL,
			2608012711638119052UL,
			6128411473006802146UL,
			8268148722764581231UL,
			9286055187155687089UL,
			11230858885718282805UL,
			13951009754708518548UL,
			16472876342353939154UL,
			17275323862435702243UL,
			1135362057144423861UL,
			2597628984639134821UL,
			3308224258029322869UL,
			5365058923640841347UL,
			6679025012923562964UL,
			8573033837759648693UL,
			10970295158949994411UL,
			12119686244451234320UL,
			12683024718118986047UL,
			13788192230050041572UL,
			14330467153632333762UL,
			15395433587784984357UL,
			489312712824947311UL,
			1452737877330783856UL,
			2861767655752347644UL,
			3322285676063803686UL,
			5560940570517711597UL,
			5996557281743188959UL,
			7280758554555802590UL,
			8532644243296465576UL,
			9350256976987008742UL,
			10552545826968843579UL,
			11727347734174303076UL,
			12113106623233404929UL,
			14000437183269869457UL,
			14369950271660146224UL,
			15101387698204529176UL,
			15463397548674623760UL,
			17586052441742319658UL,
			1182934255886127544UL,
			1847814050463011016UL,
			2177327727835720531UL,
			2830643537854262169UL,
			3796741975233480872UL,
			4115178125766777443UL,
			5681478168544905931UL,
			6601373596472566643UL,
			7507060721942968483UL,
			8399075790359081724UL,
			8693463985226723168UL,
			9568029438360202098UL,
			10144078919501101548UL,
			10430055236837252648UL,
			11840083180663258601UL,
			13761210420658862357UL,
			14299343276471374635UL,
			14566680578165727644UL,
			15097957966210449927UL,
			16922976911328602910UL,
			17689382322260857208UL,
			500013540394364858UL,
			748580250866718886UL,
			1242879168328830382UL,
			1977374033974150939UL,
			2944078676154940804UL,
			3659926193048069267UL,
			4368137639120453308UL,
			4836135668995329356UL,
			5532061633213252278UL,
			6448918945643986474UL,
			6902733635092675308UL,
			7801388544844847127UL
		};
	}
}
