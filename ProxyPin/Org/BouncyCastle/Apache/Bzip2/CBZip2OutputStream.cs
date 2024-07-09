using System;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Apache.Bzip2
{
	// Token: 0x02000648 RID: 1608
	public class CBZip2OutputStream : Stream
	{
		// Token: 0x060037CF RID: 14287 RVA: 0x0012AF74 File Offset: 0x0012AF74
		private static void Panic()
		{
		}

		// Token: 0x060037D0 RID: 14288 RVA: 0x0012AF78 File Offset: 0x0012AF78
		private void MakeMaps()
		{
			this.nInUse = 0;
			for (int i = 0; i < 256; i++)
			{
				if (this.inUse[i])
				{
					this.seqToUnseq[this.nInUse] = (char)i;
					this.unseqToSeq[i] = (char)this.nInUse;
					this.nInUse++;
				}
			}
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x0012AFDC File Offset: 0x0012AFDC
		protected static void HbMakeCodeLengths(char[] len, int[] freq, int alphaSize, int maxLen)
		{
			int[] array = new int[260];
			int[] array2 = new int[516];
			int[] array3 = new int[516];
			for (int i = 0; i < alphaSize; i++)
			{
				array2[i + 1] = ((freq[i] == 0) ? 1 : freq[i]) << 8;
			}
			for (;;)
			{
				int num = alphaSize;
				int j = 0;
				array[0] = 0;
				array2[0] = 0;
				array3[0] = -2;
				for (int i = 1; i <= alphaSize; i++)
				{
					array3[i] = -1;
					j++;
					array[j] = i;
					int num2 = j;
					int num3 = array[num2];
					while (array2[num3] < array2[array[num2 >> 1]])
					{
						array[num2] = array[num2 >> 1];
						num2 >>= 1;
					}
					array[num2] = num3;
				}
				if (j >= 260)
				{
					CBZip2OutputStream.Panic();
				}
				while (j > 1)
				{
					int num4 = array[1];
					array[1] = array[j];
					j--;
					int num5 = 1;
					int num6 = array[num5];
					for (;;)
					{
						int num7 = num5 << 1;
						if (num7 > j)
						{
							break;
						}
						if (num7 < j && array2[array[num7 + 1]] < array2[array[num7]])
						{
							num7++;
						}
						if (array2[num6] < array2[array[num7]])
						{
							break;
						}
						array[num5] = array[num7];
						num5 = num7;
					}
					array[num5] = num6;
					int num8 = array[1];
					array[1] = array[j];
					j--;
					int num9 = 1;
					int num10 = array[num9];
					for (;;)
					{
						int num11 = num9 << 1;
						if (num11 > j)
						{
							break;
						}
						if (num11 < j && array2[array[num11 + 1]] < array2[array[num11]])
						{
							num11++;
						}
						if (array2[num10] < array2[array[num11]])
						{
							break;
						}
						array[num9] = array[num11];
						num9 = num11;
					}
					array[num9] = num10;
					num++;
					array3[num4] = (array3[num8] = num);
					array2[num] = (int)((uint)(((long)array2[num4] & (long)((ulong)-256)) + ((long)array2[num8] & (long)((ulong)-256))) | (uint)(1 + (((array2[num4] & 255) > (array2[num8] & 255)) ? (array2[num4] & 255) : (array2[num8] & 255))));
					array3[num] = -1;
					j++;
					array[j] = num;
					int num12 = j;
					int num13 = array[num12];
					while (array2[num13] < array2[array[num12 >> 1]])
					{
						array[num12] = array[num12 >> 1];
						num12 >>= 1;
					}
					array[num12] = num13;
				}
				if (num >= 516)
				{
					CBZip2OutputStream.Panic();
				}
				bool flag = false;
				for (int i = 1; i <= alphaSize; i++)
				{
					int num14 = 0;
					int num15 = i;
					while (array3[num15] >= 0)
					{
						num15 = array3[num15];
						num14++;
					}
					len[i - 1] = (char)num14;
					if (num14 > maxLen)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					break;
				}
				for (int i = 1; i < alphaSize; i++)
				{
					int num14 = array2[i] >> 8;
					num14 = 1 + num14 / 2;
					array2[i] = num14 << 8;
				}
			}
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x0012B2E0 File Offset: 0x0012B2E0
		public CBZip2OutputStream(Stream inStream) : this(inStream, 9)
		{
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x0012B2EC File Offset: 0x0012B2EC
		public CBZip2OutputStream(Stream inStream, int inBlockSize)
		{
			this.block = null;
			this.quadrant = null;
			this.zptr = null;
			this.ftab = null;
			inStream.WriteByte(66);
			inStream.WriteByte(90);
			this.BsSetStream(inStream);
			this.workFactor = 50;
			if (inBlockSize > 9)
			{
				inBlockSize = 9;
			}
			if (inBlockSize < 1)
			{
				inBlockSize = 1;
			}
			this.blockSize100k = inBlockSize;
			this.AllocateCompressStructures();
			this.Initialize();
			this.InitBlock();
		}

		// Token: 0x060037D4 RID: 14292 RVA: 0x0012B408 File Offset: 0x0012B408
		public override void WriteByte(byte bv)
		{
			int num = (256 + (int)bv) % 256;
			if (this.currentChar != -1)
			{
				if (this.currentChar != num)
				{
					this.WriteRun();
					this.runLength = 1;
					this.currentChar = num;
					return;
				}
				this.runLength++;
				if (this.runLength > 254)
				{
					this.WriteRun();
					this.currentChar = -1;
					this.runLength = 0;
					return;
				}
			}
			else
			{
				this.currentChar = num;
				this.runLength++;
			}
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x0012B49C File Offset: 0x0012B49C
		private void WriteRun()
		{
			if (this.last >= this.allowableBlockSize)
			{
				this.EndBlock();
				this.InitBlock();
				this.WriteRun();
				return;
			}
			this.inUse[this.currentChar] = true;
			for (int i = 0; i < this.runLength; i++)
			{
				this.mCrc.UpdateCRC((int)((ushort)this.currentChar));
			}
			switch (this.runLength)
			{
			case 1:
				this.last++;
				this.block[this.last + 1] = (char)this.currentChar;
				return;
			case 2:
				this.last++;
				this.block[this.last + 1] = (char)this.currentChar;
				this.last++;
				this.block[this.last + 1] = (char)this.currentChar;
				return;
			case 3:
				this.last++;
				this.block[this.last + 1] = (char)this.currentChar;
				this.last++;
				this.block[this.last + 1] = (char)this.currentChar;
				this.last++;
				this.block[this.last + 1] = (char)this.currentChar;
				return;
			default:
				this.inUse[this.runLength - 4] = true;
				this.last++;
				this.block[this.last + 1] = (char)this.currentChar;
				this.last++;
				this.block[this.last + 1] = (char)this.currentChar;
				this.last++;
				this.block[this.last + 1] = (char)this.currentChar;
				this.last++;
				this.block[this.last + 1] = (char)this.currentChar;
				this.last++;
				this.block[this.last + 1] = (char)(this.runLength - 4);
				return;
			}
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x0012B6C8 File Offset: 0x0012B6C8
		public override void Close()
		{
			if (this.closed)
			{
				return;
			}
			this.Finish();
			this.closed = true;
			Platform.Dispose(this.bsStream);
			base.Close();
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x0012B6F4 File Offset: 0x0012B6F4
		public void Finish()
		{
			if (this.finished)
			{
				return;
			}
			if (this.runLength > 0)
			{
				this.WriteRun();
			}
			this.currentChar = -1;
			this.EndBlock();
			this.EndCompression();
			this.finished = true;
			this.Flush();
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x0012B734 File Offset: 0x0012B734
		public override void Flush()
		{
			this.bsStream.Flush();
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x0012B744 File Offset: 0x0012B744
		private void Initialize()
		{
			this.bytesOut = 0;
			this.nBlocksRandomised = 0;
			this.BsPutUChar(104);
			this.BsPutUChar(48 + this.blockSize100k);
			this.combinedCRC = 0;
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x0012B774 File Offset: 0x0012B774
		private void InitBlock()
		{
			this.mCrc.InitialiseCRC();
			this.last = -1;
			for (int i = 0; i < 256; i++)
			{
				this.inUse[i] = false;
			}
			this.allowableBlockSize = 100000 * this.blockSize100k - 20;
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x0012B7C8 File Offset: 0x0012B7C8
		private void EndBlock()
		{
			this.blockCRC = this.mCrc.GetFinalCRC();
			this.combinedCRC = (this.combinedCRC << 1 | (int)((uint)this.combinedCRC >> 31));
			this.combinedCRC ^= this.blockCRC;
			this.DoReversibleTransformation();
			this.BsPutUChar(49);
			this.BsPutUChar(65);
			this.BsPutUChar(89);
			this.BsPutUChar(38);
			this.BsPutUChar(83);
			this.BsPutUChar(89);
			this.BsPutint(this.blockCRC);
			if (this.blockRandomised)
			{
				this.BsW(1, 1);
				this.nBlocksRandomised++;
			}
			else
			{
				this.BsW(1, 0);
			}
			this.MoveToFrontCodeAndSend();
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x0012B88C File Offset: 0x0012B88C
		private void EndCompression()
		{
			this.BsPutUChar(23);
			this.BsPutUChar(114);
			this.BsPutUChar(69);
			this.BsPutUChar(56);
			this.BsPutUChar(80);
			this.BsPutUChar(144);
			this.BsPutint(this.combinedCRC);
			this.BsFinishedWithStream();
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x0012B8E4 File Offset: 0x0012B8E4
		private void HbAssignCodes(int[] code, char[] length, int minLen, int maxLen, int alphaSize)
		{
			int num = 0;
			for (int i = minLen; i <= maxLen; i++)
			{
				for (int j = 0; j < alphaSize; j++)
				{
					if ((int)length[j] == i)
					{
						code[j] = num;
						num++;
					}
				}
				num <<= 1;
			}
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x0012B92C File Offset: 0x0012B92C
		private void BsSetStream(Stream f)
		{
			this.bsStream = f;
			this.bsLive = 0;
			this.bsBuff = 0;
			this.bytesOut = 0;
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x0012B94C File Offset: 0x0012B94C
		private void BsFinishedWithStream()
		{
			while (this.bsLive > 0)
			{
				int num = this.bsBuff >> 24;
				try
				{
					this.bsStream.WriteByte((byte)num);
				}
				catch (IOException ex)
				{
					throw ex;
				}
				this.bsBuff <<= 8;
				this.bsLive -= 8;
				this.bytesOut++;
			}
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x0012B9C0 File Offset: 0x0012B9C0
		private void BsW(int n, int v)
		{
			while (this.bsLive >= 8)
			{
				int num = this.bsBuff >> 24;
				try
				{
					this.bsStream.WriteByte((byte)num);
				}
				catch (IOException ex)
				{
					throw ex;
				}
				this.bsBuff <<= 8;
				this.bsLive -= 8;
				this.bytesOut++;
			}
			this.bsBuff |= v << 32 - this.bsLive - n;
			this.bsLive += n;
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x0012BA60 File Offset: 0x0012BA60
		private void BsPutUChar(int c)
		{
			this.BsW(8, c);
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x0012BA6C File Offset: 0x0012BA6C
		private void BsPutint(int u)
		{
			this.BsW(8, u >> 24 & 255);
			this.BsW(8, u >> 16 & 255);
			this.BsW(8, u >> 8 & 255);
			this.BsW(8, u & 255);
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x0012BAC0 File Offset: 0x0012BAC0
		private void BsPutIntVS(int numBits, int c)
		{
			this.BsW(numBits, c);
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x0012BACC File Offset: 0x0012BACC
		private void SendMTFValues()
		{
			char[][] array = CBZip2InputStream.InitCharArray(6, 258);
			int num = 0;
			int num2 = this.nInUse + 2;
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					array[i][j] = '\u000f';
				}
			}
			if (this.nMTF <= 0)
			{
				CBZip2OutputStream.Panic();
			}
			int num3;
			if (this.nMTF < 200)
			{
				num3 = 2;
			}
			else if (this.nMTF < 600)
			{
				num3 = 3;
			}
			else if (this.nMTF < 1200)
			{
				num3 = 4;
			}
			else if (this.nMTF < 2400)
			{
				num3 = 5;
			}
			else
			{
				num3 = 6;
			}
			int k = num3;
			int num4 = this.nMTF;
			int l = 0;
			while (k > 0)
			{
				int num5 = num4 / k;
				int num6 = l - 1;
				int num7 = 0;
				while (num7 < num5 && num6 < num2 - 1)
				{
					num6++;
					num7 += this.mtfFreq[num6];
				}
				if (num6 > l && k != num3 && k != 1 && (num3 - k) % 2 == 1)
				{
					num7 -= this.mtfFreq[num6];
					num6--;
				}
				for (int j = 0; j < num2; j++)
				{
					if (j >= l && j <= num6)
					{
						array[k - 1][j] = '\0';
					}
					else
					{
						array[k - 1][j] = '\u000f';
					}
				}
				k--;
				l = num6 + 1;
				num4 -= num7;
			}
			int[][] array2 = CBZip2InputStream.InitIntArray(6, 258);
			int[] array3 = new int[6];
			short[] array4 = new short[6];
			for (int m = 0; m < 4; m++)
			{
				for (int i = 0; i < num3; i++)
				{
					array3[i] = 0;
				}
				for (int i = 0; i < num3; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						array2[i][j] = 0;
					}
				}
				num = 0;
				int num8 = 0;
				int num6;
				for (l = 0; l < this.nMTF; l = num6 + 1)
				{
					num6 = l + 50 - 1;
					if (num6 >= this.nMTF)
					{
						num6 = this.nMTF - 1;
					}
					for (int i = 0; i < num3; i++)
					{
						array4[i] = 0;
					}
					IntPtr intPtr;
					if (num3 == 6)
					{
						short num14;
						short num13;
						short num12;
						short num11;
						short num10;
						short num9 = num10 = (num11 = (num12 = (num13 = (num14 = 0))));
						for (int n = l; n <= num6; n++)
						{
							short num15 = this.szptr[n];
							num10 += (short)array[0][(int)num15];
							num9 += (short)array[1][(int)num15];
							num11 += (short)array[2][(int)num15];
							num12 += (short)array[3][(int)num15];
							num13 += (short)array[4][(int)num15];
							num14 += (short)array[5][(int)num15];
						}
						array4[0] = num10;
						array4[1] = num9;
						array4[2] = num11;
						array4[3] = num12;
						array4[4] = num13;
						array4[5] = num14;
					}
					else
					{
						for (int n = l; n <= num6; n++)
						{
							short num16 = this.szptr[n];
							for (int i = 0; i < num3; i++)
							{
								short[] array5;
								(array5 = array4)[(int)(intPtr = (IntPtr)i)] = array5[(int)intPtr] + (short)array[i][(int)num16];
							}
						}
					}
					int num17 = 999999999;
					int num18 = -1;
					for (int i = 0; i < num3; i++)
					{
						if ((int)array4[i] < num17)
						{
							num17 = (int)array4[i];
							num18 = i;
						}
					}
					num8 += num17;
					int[] array6;
					(array6 = array3)[(int)(intPtr = (IntPtr)num18)] = array6[(int)intPtr] + 1;
					this.selector[num] = (char)num18;
					num++;
					for (int n = l; n <= num6; n++)
					{
						(array6 = array2[num18])[(int)(intPtr = (IntPtr)this.szptr[n])] = array6[(int)intPtr] + 1;
					}
				}
				for (int i = 0; i < num3; i++)
				{
					CBZip2OutputStream.HbMakeCodeLengths(array[i], array2[i], num2, 20);
				}
			}
			if (num3 >= 8)
			{
				CBZip2OutputStream.Panic();
			}
			if (num >= 32768 || num > 18002)
			{
				CBZip2OutputStream.Panic();
			}
			char[] array7 = new char[6];
			for (int n = 0; n < num3; n++)
			{
				array7[n] = (char)n;
			}
			for (int n = 0; n < num; n++)
			{
				char c = this.selector[n];
				int num19 = 0;
				char c2 = array7[num19];
				while (c != c2)
				{
					num19++;
					char c3 = c2;
					c2 = array7[num19];
					array7[num19] = c3;
				}
				array7[0] = c2;
				this.selectorMtf[n] = (char)num19;
			}
			int[][] array8 = CBZip2InputStream.InitIntArray(6, 258);
			for (int i = 0; i < num3; i++)
			{
				int num20 = 32;
				int num21 = 0;
				for (int n = 0; n < num2; n++)
				{
					if ((int)array[i][n] > num21)
					{
						num21 = (int)array[i][n];
					}
					if ((int)array[i][n] < num20)
					{
						num20 = (int)array[i][n];
					}
				}
				if (num21 > 20)
				{
					CBZip2OutputStream.Panic();
				}
				if (num20 < 1)
				{
					CBZip2OutputStream.Panic();
				}
				this.HbAssignCodes(array8[i], array[i], num20, num21, num2);
			}
			bool[] array9 = new bool[16];
			for (int n = 0; n < 16; n++)
			{
				array9[n] = false;
				for (int num19 = 0; num19 < 16; num19++)
				{
					if (this.inUse[n * 16 + num19])
					{
						array9[n] = true;
					}
				}
			}
			for (int n = 0; n < 16; n++)
			{
				if (array9[n])
				{
					this.BsW(1, 1);
				}
				else
				{
					this.BsW(1, 0);
				}
			}
			for (int n = 0; n < 16; n++)
			{
				if (array9[n])
				{
					for (int num19 = 0; num19 < 16; num19++)
					{
						if (this.inUse[n * 16 + num19])
						{
							this.BsW(1, 1);
						}
						else
						{
							this.BsW(1, 0);
						}
					}
				}
			}
			this.BsW(3, num3);
			this.BsW(15, num);
			for (int n = 0; n < num; n++)
			{
				for (int num19 = 0; num19 < (int)this.selectorMtf[n]; num19++)
				{
					this.BsW(1, 1);
				}
				this.BsW(1, 0);
			}
			for (int i = 0; i < num3; i++)
			{
				int num22 = (int)array[i][0];
				this.BsW(5, num22);
				for (int n = 0; n < num2; n++)
				{
					while (num22 < (int)array[i][n])
					{
						this.BsW(2, 2);
						num22++;
					}
					while (num22 > (int)array[i][n])
					{
						this.BsW(2, 3);
						num22--;
					}
					this.BsW(1, 0);
				}
			}
			int num23 = 0;
			l = 0;
			while (l < this.nMTF)
			{
				int num6 = l + 50 - 1;
				if (num6 >= this.nMTF)
				{
					num6 = this.nMTF - 1;
				}
				for (int n = l; n <= num6; n++)
				{
					this.BsW((int)array[(int)this.selector[num23]][(int)this.szptr[n]], array8[(int)this.selector[num23]][(int)this.szptr[n]]);
				}
				l = num6 + 1;
				num23++;
			}
			if (num23 != num)
			{
				CBZip2OutputStream.Panic();
			}
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x0012C2B8 File Offset: 0x0012C2B8
		private void MoveToFrontCodeAndSend()
		{
			this.BsPutIntVS(24, this.origPtr);
			this.GenerateMTFValues();
			this.SendMTFValues();
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x0012C2D4 File Offset: 0x0012C2D4
		private void SimpleSort(int lo, int hi, int d)
		{
			int num = hi - lo + 1;
			if (num < 2)
			{
				return;
			}
			int i = 0;
			while (this.incs[i] < num)
			{
				i++;
			}
			for (i--; i >= 0; i--)
			{
				int num2 = this.incs[i];
				int j = lo + num2;
				while (j <= hi)
				{
					int num3 = this.zptr[j];
					int num4 = j;
					while (this.FullGtU(this.zptr[num4 - num2] + d, num3 + d))
					{
						this.zptr[num4] = this.zptr[num4 - num2];
						num4 -= num2;
						if (num4 <= lo + num2 - 1)
						{
							break;
						}
					}
					this.zptr[num4] = num3;
					j++;
					if (j > hi)
					{
						break;
					}
					num3 = this.zptr[j];
					num4 = j;
					while (this.FullGtU(this.zptr[num4 - num2] + d, num3 + d))
					{
						this.zptr[num4] = this.zptr[num4 - num2];
						num4 -= num2;
						if (num4 <= lo + num2 - 1)
						{
							break;
						}
					}
					this.zptr[num4] = num3;
					j++;
					if (j > hi)
					{
						break;
					}
					num3 = this.zptr[j];
					num4 = j;
					while (this.FullGtU(this.zptr[num4 - num2] + d, num3 + d))
					{
						this.zptr[num4] = this.zptr[num4 - num2];
						num4 -= num2;
						if (num4 <= lo + num2 - 1)
						{
							break;
						}
					}
					this.zptr[num4] = num3;
					j++;
					if (this.workDone > this.workLimit && this.firstAttempt)
					{
						return;
					}
				}
			}
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x0012C47C File Offset: 0x0012C47C
		private void Vswap(int p1, int p2, int n)
		{
			while (n > 0)
			{
				int num = this.zptr[p1];
				this.zptr[p1] = this.zptr[p2];
				this.zptr[p2] = num;
				p1++;
				p2++;
				n--;
			}
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x0012C4CC File Offset: 0x0012C4CC
		private char Med3(char a, char b, char c)
		{
			if (a > b)
			{
				char c2 = a;
				a = b;
				b = c2;
			}
			if (b > c)
			{
				char c2 = b;
				b = c;
				c = c2;
			}
			if (a > b)
			{
				b = a;
			}
			return b;
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x0012C508 File Offset: 0x0012C508
		private void QSort3(int loSt, int hiSt, int dSt)
		{
			CBZip2OutputStream.StackElem[] array = new CBZip2OutputStream.StackElem[1000];
			for (int i = 0; i < 1000; i++)
			{
				array[i] = new CBZip2OutputStream.StackElem();
			}
			int j = 0;
			array[j].ll = loSt;
			array[j].hh = hiSt;
			array[j].dd = dSt;
			j++;
			while (j > 0)
			{
				if (j >= 1000)
				{
					CBZip2OutputStream.Panic();
				}
				j--;
				int ll = array[j].ll;
				int hh = array[j].hh;
				int dd = array[j].dd;
				if (hh - ll < 20 || dd > 10)
				{
					this.SimpleSort(ll, hh, dd);
					if (this.workDone > this.workLimit && this.firstAttempt)
					{
						return;
					}
				}
				else
				{
					int num = (int)this.Med3(this.block[this.zptr[ll] + dd + 1], this.block[this.zptr[hh] + dd + 1], this.block[this.zptr[ll + hh >> 1] + dd + 1]);
					int k;
					int num2 = k = ll;
					int num4;
					int num3 = num4 = hh;
					for (;;)
					{
						if (k <= num4)
						{
							int num5 = (int)this.block[this.zptr[k] + dd + 1] - num;
							if (num5 == 0)
							{
								int num6 = this.zptr[k];
								this.zptr[k] = this.zptr[num2];
								this.zptr[num2] = num6;
								num2++;
								k++;
								continue;
							}
							if (num5 <= 0)
							{
								k++;
								continue;
							}
						}
						while (k <= num4)
						{
							int num5 = (int)this.block[this.zptr[num4] + dd + 1] - num;
							if (num5 == 0)
							{
								int num7 = this.zptr[num4];
								this.zptr[num4] = this.zptr[num3];
								this.zptr[num3] = num7;
								num3--;
								num4--;
							}
							else
							{
								if (num5 < 0)
								{
									break;
								}
								num4--;
							}
						}
						if (k > num4)
						{
							break;
						}
						int num8 = this.zptr[k];
						this.zptr[k] = this.zptr[num4];
						this.zptr[num4] = num8;
						k++;
						num4--;
					}
					if (num3 < num2)
					{
						array[j].ll = ll;
						array[j].hh = hh;
						array[j].dd = dd + 1;
						j++;
					}
					else
					{
						int num5 = (num2 - ll < k - num2) ? (num2 - ll) : (k - num2);
						this.Vswap(ll, k - num5, num5);
						int num9 = (hh - num3 < num3 - num4) ? (hh - num3) : (num3 - num4);
						this.Vswap(k, hh - num9 + 1, num9);
						num5 = ll + k - num2 - 1;
						num9 = hh - (num3 - num4) + 1;
						array[j].ll = ll;
						array[j].hh = num5;
						array[j].dd = dd;
						j++;
						array[j].ll = num5 + 1;
						array[j].hh = num9 - 1;
						array[j].dd = dd + 1;
						j++;
						array[j].ll = num9;
						array[j].hh = hh;
						array[j].dd = dd;
						j++;
					}
				}
			}
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x0012C8B4 File Offset: 0x0012C8B4
		private void MainSort()
		{
			int[] array = new int[256];
			int[] array2 = new int[256];
			bool[] array3 = new bool[256];
			for (int i = 0; i < 20; i++)
			{
				this.block[this.last + i + 2] = this.block[i % (this.last + 1) + 1];
			}
			for (int i = 0; i <= this.last + 20; i++)
			{
				this.quadrant[i] = 0;
			}
			this.block[0] = this.block[this.last + 1];
			if (this.last < 4000)
			{
				for (int i = 0; i <= this.last; i++)
				{
					this.zptr[i] = i;
				}
				this.firstAttempt = false;
				this.workDone = (this.workLimit = 0);
				this.SimpleSort(0, this.last, 0);
				return;
			}
			int num = 0;
			for (int i = 0; i <= 255; i++)
			{
				array3[i] = false;
			}
			for (int i = 0; i <= 65536; i++)
			{
				this.ftab[i] = 0;
			}
			int num2 = (int)this.block[0];
			int[] array4;
			IntPtr intPtr;
			for (int i = 0; i <= this.last; i++)
			{
				int num3 = (int)this.block[i + 1];
				(array4 = this.ftab)[(int)(intPtr = (IntPtr)((num2 << 8) + num3))] = array4[(int)intPtr] + 1;
				num2 = num3;
			}
			for (int i = 1; i <= 65536; i++)
			{
				(array4 = this.ftab)[(int)(intPtr = (IntPtr)i)] = array4[(int)intPtr] + this.ftab[i - 1];
			}
			num2 = (int)this.block[1];
			int j;
			for (int i = 0; i < this.last; i++)
			{
				int num3 = (int)this.block[i + 2];
				j = (num2 << 8) + num3;
				num2 = num3;
				(array4 = this.ftab)[(int)(intPtr = (IntPtr)j)] = array4[(int)intPtr] - 1;
				this.zptr[this.ftab[j]] = i;
			}
			j = (int)(((int)this.block[this.last + 1] << 8) + this.block[1]);
			(array4 = this.ftab)[(int)(intPtr = (IntPtr)j)] = array4[(int)intPtr] - 1;
			this.zptr[this.ftab[j]] = this.last;
			for (int i = 0; i <= 255; i++)
			{
				array[i] = i;
			}
			int num4 = 1;
			do
			{
				num4 = 3 * num4 + 1;
			}
			while (num4 <= 256);
			do
			{
				num4 /= 3;
				for (int i = num4; i <= 255; i++)
				{
					int num5 = array[i];
					j = i;
					while (this.ftab[array[j - num4] + 1 << 8] - this.ftab[array[j - num4] << 8] > this.ftab[num5 + 1 << 8] - this.ftab[num5 << 8])
					{
						array[j] = array[j - num4];
						j -= num4;
						if (j <= num4 - 1)
						{
							break;
						}
					}
					array[j] = num5;
				}
			}
			while (num4 != 1);
			for (int i = 0; i <= 255; i++)
			{
				int num6 = array[i];
				for (j = 0; j <= 255; j++)
				{
					int num7 = (num6 << 8) + j;
					if ((this.ftab[num7] & 2097152) != 2097152)
					{
						int num8 = this.ftab[num7] & -2097153;
						int num9 = (this.ftab[num7 + 1] & -2097153) - 1;
						if (num9 > num8)
						{
							this.QSort3(num8, num9, 2);
							num += num9 - num8 + 1;
							if (this.workDone > this.workLimit && this.firstAttempt)
							{
								return;
							}
						}
						(array4 = this.ftab)[(int)(intPtr = (IntPtr)num7)] = (array4[(int)intPtr] | 2097152);
					}
				}
				array3[num6] = true;
				if (i < 255)
				{
					int num10 = this.ftab[num6 << 8] & -2097153;
					int num11 = (this.ftab[num6 + 1 << 8] & -2097153) - num10;
					int num12 = 0;
					while (num11 >> num12 > 65534)
					{
						num12++;
					}
					for (j = 0; j < num11; j++)
					{
						int num13 = this.zptr[num10 + j];
						int num14 = j >> num12;
						this.quadrant[num13] = num14;
						if (num13 < 20)
						{
							this.quadrant[num13 + this.last + 1] = num14;
						}
					}
					if (num11 - 1 >> num12 > 65535)
					{
						CBZip2OutputStream.Panic();
					}
				}
				for (j = 0; j <= 255; j++)
				{
					array2[j] = (this.ftab[(j << 8) + num6] & -2097153);
				}
				for (j = (this.ftab[num6 << 8] & -2097153); j < (this.ftab[num6 + 1 << 8] & -2097153); j++)
				{
					num2 = (int)this.block[this.zptr[j]];
					if (!array3[num2])
					{
						this.zptr[array2[num2]] = ((this.zptr[j] == 0) ? this.last : (this.zptr[j] - 1));
						(array4 = array2)[(int)(intPtr = (IntPtr)num2)] = array4[(int)intPtr] + 1;
					}
				}
				for (j = 0; j <= 255; j++)
				{
					(array4 = this.ftab)[(int)(intPtr = (IntPtr)((j << 8) + num6))] = (array4[(int)intPtr] | 2097152);
				}
			}
		}

		// Token: 0x060037EB RID: 14315 RVA: 0x0012CE48 File Offset: 0x0012CE48
		private void RandomiseBlock()
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < 256; i++)
			{
				this.inUse[i] = false;
			}
			for (int i = 0; i <= this.last; i++)
			{
				if (num == 0)
				{
					num = (int)((ushort)BZip2Constants.rNums[num2]);
					num2++;
					if (num2 == 512)
					{
						num2 = 0;
					}
				}
				num--;
				char[] array;
				IntPtr intPtr;
				(array = this.block)[(int)(intPtr = (IntPtr)(i + 1))] = (array[(int)intPtr] ^ ((num == 1) ? '\u0001' : '\0'));
				(array = this.block)[(int)(intPtr = (IntPtr)(i + 1))] = (array[(int)intPtr] & 'ÿ');
				this.inUse[(int)this.block[i + 1]] = true;
			}
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x0012CF04 File Offset: 0x0012CF04
		private void DoReversibleTransformation()
		{
			this.workLimit = this.workFactor * this.last;
			this.workDone = 0;
			this.blockRandomised = false;
			this.firstAttempt = true;
			this.MainSort();
			if (this.workDone > this.workLimit && this.firstAttempt)
			{
				this.RandomiseBlock();
				this.workLimit = (this.workDone = 0);
				this.blockRandomised = true;
				this.firstAttempt = false;
				this.MainSort();
			}
			this.origPtr = -1;
			for (int i = 0; i <= this.last; i++)
			{
				if (this.zptr[i] == 0)
				{
					this.origPtr = i;
					break;
				}
			}
			if (this.origPtr == -1)
			{
				CBZip2OutputStream.Panic();
			}
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x0012CFD0 File Offset: 0x0012CFD0
		private bool FullGtU(int i1, int i2)
		{
			char c = this.block[i1 + 1];
			char c2 = this.block[i2 + 1];
			if (c != c2)
			{
				return c > c2;
			}
			i1++;
			i2++;
			c = this.block[i1 + 1];
			c2 = this.block[i2 + 1];
			if (c != c2)
			{
				return c > c2;
			}
			i1++;
			i2++;
			c = this.block[i1 + 1];
			c2 = this.block[i2 + 1];
			if (c != c2)
			{
				return c > c2;
			}
			i1++;
			i2++;
			c = this.block[i1 + 1];
			c2 = this.block[i2 + 1];
			if (c != c2)
			{
				return c > c2;
			}
			i1++;
			i2++;
			c = this.block[i1 + 1];
			c2 = this.block[i2 + 1];
			if (c != c2)
			{
				return c > c2;
			}
			i1++;
			i2++;
			c = this.block[i1 + 1];
			c2 = this.block[i2 + 1];
			if (c != c2)
			{
				return c > c2;
			}
			i1++;
			i2++;
			int num = this.last + 1;
			int num2;
			int num3;
			for (;;)
			{
				c = this.block[i1 + 1];
				c2 = this.block[i2 + 1];
				if (c != c2)
				{
					break;
				}
				num2 = this.quadrant[i1];
				num3 = this.quadrant[i2];
				if (num2 != num3)
				{
					goto Block_8;
				}
				i1++;
				i2++;
				c = this.block[i1 + 1];
				c2 = this.block[i2 + 1];
				if (c != c2)
				{
					goto Block_9;
				}
				num2 = this.quadrant[i1];
				num3 = this.quadrant[i2];
				if (num2 != num3)
				{
					goto Block_10;
				}
				i1++;
				i2++;
				c = this.block[i1 + 1];
				c2 = this.block[i2 + 1];
				if (c != c2)
				{
					goto Block_11;
				}
				num2 = this.quadrant[i1];
				num3 = this.quadrant[i2];
				if (num2 != num3)
				{
					goto Block_12;
				}
				i1++;
				i2++;
				c = this.block[i1 + 1];
				c2 = this.block[i2 + 1];
				if (c != c2)
				{
					goto Block_13;
				}
				num2 = this.quadrant[i1];
				num3 = this.quadrant[i2];
				if (num2 != num3)
				{
					goto Block_14;
				}
				i1++;
				i2++;
				if (i1 > this.last)
				{
					i1 -= this.last;
					i1--;
				}
				if (i2 > this.last)
				{
					i2 -= this.last;
					i2--;
				}
				num -= 4;
				this.workDone++;
				if (num < 0)
				{
					return false;
				}
			}
			return c > c2;
			Block_8:
			return num2 > num3;
			Block_9:
			return c > c2;
			Block_10:
			return num2 > num3;
			Block_11:
			return c > c2;
			Block_12:
			return num2 > num3;
			Block_13:
			return c > c2;
			Block_14:
			return num2 > num3;
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x0012D278 File Offset: 0x0012D278
		private void AllocateCompressStructures()
		{
			int num = 100000 * this.blockSize100k;
			this.block = new char[num + 1 + 20];
			this.quadrant = new int[num + 20];
			this.zptr = new int[num];
			this.ftab = new int[65537];
			if (this.block != null && this.quadrant != null && this.zptr != null)
			{
				int[] array = this.ftab;
			}
			this.szptr = new short[2 * num];
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x0012D308 File Offset: 0x0012D308
		private void GenerateMTFValues()
		{
			char[] array = new char[256];
			this.MakeMaps();
			int num = this.nInUse + 1;
			for (int i = 0; i <= num; i++)
			{
				this.mtfFreq[i] = 0;
			}
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < this.nInUse; i++)
			{
				array[i] = (char)i;
			}
			int[] array2;
			IntPtr intPtr;
			for (int i = 0; i <= this.last; i++)
			{
				char c = this.unseqToSeq[(int)this.block[this.zptr[i]]];
				int num4 = 0;
				char c2 = array[num4];
				while (c != c2)
				{
					num4++;
					char c3 = c2;
					c2 = array[num4];
					array[num4] = c3;
				}
				array[0] = c2;
				if (num4 == 0)
				{
					num3++;
				}
				else
				{
					if (num3 > 0)
					{
						num3--;
						for (;;)
						{
							switch (num3 % 2)
							{
							case 0:
								this.szptr[num2] = 0;
								num2++;
								(array2 = this.mtfFreq)[0] = array2[0] + 1;
								break;
							case 1:
								this.szptr[num2] = 1;
								num2++;
								(array2 = this.mtfFreq)[1] = array2[1] + 1;
								break;
							}
							if (num3 < 2)
							{
								break;
							}
							num3 = (num3 - 2) / 2;
						}
						num3 = 0;
					}
					this.szptr[num2] = (short)(num4 + 1);
					num2++;
					(array2 = this.mtfFreq)[(int)(intPtr = (IntPtr)(num4 + 1))] = array2[(int)intPtr] + 1;
				}
			}
			if (num3 > 0)
			{
				num3--;
				for (;;)
				{
					switch (num3 % 2)
					{
					case 0:
						this.szptr[num2] = 0;
						num2++;
						(array2 = this.mtfFreq)[0] = array2[0] + 1;
						break;
					case 1:
						this.szptr[num2] = 1;
						num2++;
						(array2 = this.mtfFreq)[1] = array2[1] + 1;
						break;
					}
					if (num3 < 2)
					{
						break;
					}
					num3 = (num3 - 2) / 2;
				}
			}
			this.szptr[num2] = (short)num;
			num2++;
			(array2 = this.mtfFreq)[(int)(intPtr = (IntPtr)num)] = array2[(int)intPtr] + 1;
			this.nMTF = num2;
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x0012D528 File Offset: 0x0012D528
		public override int Read(byte[] buffer, int offset, int count)
		{
			return 0;
		}

		// Token: 0x060037F1 RID: 14321 RVA: 0x0012D52C File Offset: 0x0012D52C
		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		// Token: 0x060037F2 RID: 14322 RVA: 0x0012D530 File Offset: 0x0012D530
		public override void SetLength(long value)
		{
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x0012D534 File Offset: 0x0012D534
		public override void Write(byte[] buffer, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				this.WriteByte(buffer[i + offset]);
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x060037F4 RID: 14324 RVA: 0x0012D560 File Offset: 0x0012D560
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x060037F5 RID: 14325 RVA: 0x0012D564 File Offset: 0x0012D564
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x060037F6 RID: 14326 RVA: 0x0012D568 File Offset: 0x0012D568
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x060037F7 RID: 14327 RVA: 0x0012D56C File Offset: 0x0012D56C
		public override long Length
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x060037F8 RID: 14328 RVA: 0x0012D570 File Offset: 0x0012D570
		// (set) Token: 0x060037F9 RID: 14329 RVA: 0x0012D574 File Offset: 0x0012D574
		public override long Position
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x04001D75 RID: 7541
		protected const int SETMASK = 2097152;

		// Token: 0x04001D76 RID: 7542
		protected const int CLEARMASK = -2097153;

		// Token: 0x04001D77 RID: 7543
		protected const int GREATER_ICOST = 15;

		// Token: 0x04001D78 RID: 7544
		protected const int LESSER_ICOST = 0;

		// Token: 0x04001D79 RID: 7545
		protected const int SMALL_THRESH = 20;

		// Token: 0x04001D7A RID: 7546
		protected const int DEPTH_THRESH = 10;

		// Token: 0x04001D7B RID: 7547
		protected const int QSORT_STACK_SIZE = 1000;

		// Token: 0x04001D7C RID: 7548
		private bool finished;

		// Token: 0x04001D7D RID: 7549
		private int last;

		// Token: 0x04001D7E RID: 7550
		private int origPtr;

		// Token: 0x04001D7F RID: 7551
		private int blockSize100k;

		// Token: 0x04001D80 RID: 7552
		private bool blockRandomised;

		// Token: 0x04001D81 RID: 7553
		private int bytesOut;

		// Token: 0x04001D82 RID: 7554
		private int bsBuff;

		// Token: 0x04001D83 RID: 7555
		private int bsLive;

		// Token: 0x04001D84 RID: 7556
		private CRC mCrc = new CRC();

		// Token: 0x04001D85 RID: 7557
		private bool[] inUse = new bool[256];

		// Token: 0x04001D86 RID: 7558
		private int nInUse;

		// Token: 0x04001D87 RID: 7559
		private char[] seqToUnseq = new char[256];

		// Token: 0x04001D88 RID: 7560
		private char[] unseqToSeq = new char[256];

		// Token: 0x04001D89 RID: 7561
		private char[] selector = new char[18002];

		// Token: 0x04001D8A RID: 7562
		private char[] selectorMtf = new char[18002];

		// Token: 0x04001D8B RID: 7563
		private char[] block;

		// Token: 0x04001D8C RID: 7564
		private int[] quadrant;

		// Token: 0x04001D8D RID: 7565
		private int[] zptr;

		// Token: 0x04001D8E RID: 7566
		private short[] szptr;

		// Token: 0x04001D8F RID: 7567
		private int[] ftab;

		// Token: 0x04001D90 RID: 7568
		private int nMTF;

		// Token: 0x04001D91 RID: 7569
		private int[] mtfFreq = new int[258];

		// Token: 0x04001D92 RID: 7570
		private int workFactor;

		// Token: 0x04001D93 RID: 7571
		private int workDone;

		// Token: 0x04001D94 RID: 7572
		private int workLimit;

		// Token: 0x04001D95 RID: 7573
		private bool firstAttempt;

		// Token: 0x04001D96 RID: 7574
		private int nBlocksRandomised;

		// Token: 0x04001D97 RID: 7575
		private int currentChar = -1;

		// Token: 0x04001D98 RID: 7576
		private int runLength = 0;

		// Token: 0x04001D99 RID: 7577
		private bool closed = false;

		// Token: 0x04001D9A RID: 7578
		private int blockCRC;

		// Token: 0x04001D9B RID: 7579
		private int combinedCRC;

		// Token: 0x04001D9C RID: 7580
		private int allowableBlockSize;

		// Token: 0x04001D9D RID: 7581
		private Stream bsStream;

		// Token: 0x04001D9E RID: 7582
		private int[] incs = new int[]
		{
			1,
			4,
			13,
			40,
			121,
			364,
			1093,
			3280,
			9841,
			29524,
			88573,
			265720,
			797161,
			2391484
		};

		// Token: 0x02000E5F RID: 3679
		internal class StackElem
		{
			// Token: 0x04004234 RID: 16948
			internal int ll;

			// Token: 0x04004235 RID: 16949
			internal int hh;

			// Token: 0x04004236 RID: 16950
			internal int dd;
		}
	}
}
