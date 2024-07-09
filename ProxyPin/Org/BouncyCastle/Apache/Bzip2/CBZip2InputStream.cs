using System;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Apache.Bzip2
{
	// Token: 0x02000729 RID: 1833
	public class CBZip2InputStream : Stream
	{
		// Token: 0x06004060 RID: 16480 RVA: 0x0015FE64 File Offset: 0x0015FE64
		private static void Cadvise()
		{
		}

		// Token: 0x06004061 RID: 16481 RVA: 0x0015FE68 File Offset: 0x0015FE68
		private static void CompressedStreamEOF()
		{
			CBZip2InputStream.Cadvise();
		}

		// Token: 0x06004062 RID: 16482 RVA: 0x0015FE70 File Offset: 0x0015FE70
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

		// Token: 0x06004063 RID: 16483 RVA: 0x0015FED4 File Offset: 0x0015FED4
		public CBZip2InputStream(Stream zStream)
		{
			this.ll8 = null;
			this.tt = null;
			this.BsSetStream(zStream);
			this.Initialize();
			this.InitBlock();
			this.SetupBlock();
		}

		// Token: 0x06004064 RID: 16484 RVA: 0x0015FFE0 File Offset: 0x0015FFE0
		internal static int[][] InitIntArray(int n1, int n2)
		{
			int[][] array = new int[n1][];
			for (int i = 0; i < n1; i++)
			{
				array[i] = new int[n2];
			}
			return array;
		}

		// Token: 0x06004065 RID: 16485 RVA: 0x00160018 File Offset: 0x00160018
		internal static char[][] InitCharArray(int n1, int n2)
		{
			char[][] array = new char[n1][];
			for (int i = 0; i < n1; i++)
			{
				array[i] = new char[n2];
			}
			return array;
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x00160050 File Offset: 0x00160050
		public override int ReadByte()
		{
			if (this.streamEnd)
			{
				return -1;
			}
			int result = this.currentChar;
			switch (this.currentState)
			{
			case 3:
				this.SetupRandPartB();
				break;
			case 4:
				this.SetupRandPartC();
				break;
			case 6:
				this.SetupNoRandPartB();
				break;
			case 7:
				this.SetupNoRandPartC();
				break;
			}
			return result;
		}

		// Token: 0x06004067 RID: 16487 RVA: 0x001600D0 File Offset: 0x001600D0
		private void Initialize()
		{
			char c = this.BsGetUChar();
			char c2 = this.BsGetUChar();
			if (c != 'B' && c2 != 'Z')
			{
				throw new IOException("Not a BZIP2 marked stream");
			}
			c = this.BsGetUChar();
			c2 = this.BsGetUChar();
			if (c != 'h' || c2 < '1' || c2 > '9')
			{
				this.BsFinishedWithStream();
				this.streamEnd = true;
				return;
			}
			this.SetDecompressStructureSizes((int)(c2 - '0'));
			this.computedCombinedCRC = 0;
		}

		// Token: 0x06004068 RID: 16488 RVA: 0x00160150 File Offset: 0x00160150
		private void InitBlock()
		{
			char c = this.BsGetUChar();
			char c2 = this.BsGetUChar();
			char c3 = this.BsGetUChar();
			char c4 = this.BsGetUChar();
			char c5 = this.BsGetUChar();
			char c6 = this.BsGetUChar();
			if (c == '\u0017' && c2 == 'r' && c3 == 'E' && c4 == '8' && c5 == 'P' && c6 == '\u0090')
			{
				this.Complete();
				return;
			}
			if (c != '1' || c2 != 'A' || c3 != 'Y' || c4 != '&' || c5 != 'S' || c6 != 'Y')
			{
				CBZip2InputStream.BadBlockHeader();
				this.streamEnd = true;
				return;
			}
			this.storedBlockCRC = this.BsGetInt32();
			if (this.BsR(1) == 1)
			{
				this.blockRandomised = true;
			}
			else
			{
				this.blockRandomised = false;
			}
			this.GetAndMoveToFrontDecode();
			this.mCrc.InitialiseCRC();
			this.currentState = 1;
		}

		// Token: 0x06004069 RID: 16489 RVA: 0x0016024C File Offset: 0x0016024C
		private void EndBlock()
		{
			this.computedBlockCRC = this.mCrc.GetFinalCRC();
			if (this.storedBlockCRC != this.computedBlockCRC)
			{
				CBZip2InputStream.CrcError();
			}
			this.computedCombinedCRC = (this.computedCombinedCRC << 1 | (int)((uint)this.computedCombinedCRC >> 31));
			this.computedCombinedCRC ^= this.computedBlockCRC;
		}

		// Token: 0x0600406A RID: 16490 RVA: 0x001602B0 File Offset: 0x001602B0
		private void Complete()
		{
			this.storedCombinedCRC = this.BsGetInt32();
			if (this.storedCombinedCRC != this.computedCombinedCRC)
			{
				CBZip2InputStream.CrcError();
			}
			this.BsFinishedWithStream();
			this.streamEnd = true;
		}

		// Token: 0x0600406B RID: 16491 RVA: 0x001602E4 File Offset: 0x001602E4
		private static void BlockOverrun()
		{
			CBZip2InputStream.Cadvise();
		}

		// Token: 0x0600406C RID: 16492 RVA: 0x001602EC File Offset: 0x001602EC
		private static void BadBlockHeader()
		{
			CBZip2InputStream.Cadvise();
		}

		// Token: 0x0600406D RID: 16493 RVA: 0x001602F4 File Offset: 0x001602F4
		private static void CrcError()
		{
			CBZip2InputStream.Cadvise();
		}

		// Token: 0x0600406E RID: 16494 RVA: 0x001602FC File Offset: 0x001602FC
		private void BsFinishedWithStream()
		{
			try
			{
				if (this.bsStream != null)
				{
					Platform.Dispose(this.bsStream);
					this.bsStream = null;
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600406F RID: 16495 RVA: 0x00160344 File Offset: 0x00160344
		private void BsSetStream(Stream f)
		{
			this.bsStream = f;
			this.bsLive = 0;
			this.bsBuff = 0;
		}

		// Token: 0x06004070 RID: 16496 RVA: 0x0016035C File Offset: 0x0016035C
		private int BsR(int n)
		{
			while (this.bsLive < n)
			{
				char c = '\0';
				try
				{
					c = (char)this.bsStream.ReadByte();
				}
				catch (IOException)
				{
					CBZip2InputStream.CompressedStreamEOF();
				}
				if (c == '￿')
				{
					CBZip2InputStream.CompressedStreamEOF();
				}
				int num = (int)c;
				this.bsBuff = (this.bsBuff << 8 | (num & 255));
				this.bsLive += 8;
			}
			int result = this.bsBuff >> this.bsLive - n & (1 << n) - 1;
			this.bsLive -= n;
			return result;
		}

		// Token: 0x06004071 RID: 16497 RVA: 0x00160408 File Offset: 0x00160408
		private char BsGetUChar()
		{
			return (char)this.BsR(8);
		}

		// Token: 0x06004072 RID: 16498 RVA: 0x00160414 File Offset: 0x00160414
		private int BsGetint()
		{
			int num = 0;
			num = (num << 8 | this.BsR(8));
			num = (num << 8 | this.BsR(8));
			num = (num << 8 | this.BsR(8));
			return num << 8 | this.BsR(8);
		}

		// Token: 0x06004073 RID: 16499 RVA: 0x00160458 File Offset: 0x00160458
		private int BsGetIntVS(int numBits)
		{
			return this.BsR(numBits);
		}

		// Token: 0x06004074 RID: 16500 RVA: 0x00160464 File Offset: 0x00160464
		private int BsGetInt32()
		{
			return this.BsGetint();
		}

		// Token: 0x06004075 RID: 16501 RVA: 0x0016046C File Offset: 0x0016046C
		private void HbCreateDecodeTables(int[] limit, int[] basev, int[] perm, char[] length, int minLen, int maxLen, int alphaSize)
		{
			int num = 0;
			for (int i = minLen; i <= maxLen; i++)
			{
				for (int j = 0; j < alphaSize; j++)
				{
					if ((int)length[j] == i)
					{
						perm[num] = j;
						num++;
					}
				}
			}
			for (int i = 0; i < 23; i++)
			{
				basev[i] = 0;
			}
			for (int i = 0; i < alphaSize; i++)
			{
				IntPtr intPtr;
				basev[(int)(intPtr = (IntPtr)(length[i] + '\u0001'))] = basev[(int)intPtr] + 1;
			}
			for (int i = 1; i < 23; i++)
			{
				IntPtr intPtr;
				basev[(int)(intPtr = (IntPtr)i)] = basev[(int)intPtr] + basev[i - 1];
			}
			for (int i = 0; i < 23; i++)
			{
				limit[i] = 0;
			}
			int num2 = 0;
			for (int i = minLen; i <= maxLen; i++)
			{
				num2 += basev[i + 1] - basev[i];
				limit[i] = num2 - 1;
				num2 <<= 1;
			}
			for (int i = minLen + 1; i <= maxLen; i++)
			{
				basev[i] = (limit[i - 1] + 1 << 1) - basev[i];
			}
		}

		// Token: 0x06004076 RID: 16502 RVA: 0x00160574 File Offset: 0x00160574
		private void RecvDecodingTables()
		{
			char[][] array = CBZip2InputStream.InitCharArray(6, 258);
			bool[] array2 = new bool[16];
			for (int i = 0; i < 16; i++)
			{
				if (this.BsR(1) == 1)
				{
					array2[i] = true;
				}
				else
				{
					array2[i] = false;
				}
			}
			for (int i = 0; i < 256; i++)
			{
				this.inUse[i] = false;
			}
			for (int i = 0; i < 16; i++)
			{
				if (array2[i])
				{
					for (int j = 0; j < 16; j++)
					{
						if (this.BsR(1) == 1)
						{
							this.inUse[i * 16 + j] = true;
						}
					}
				}
			}
			this.MakeMaps();
			int num = this.nInUse + 2;
			int num2 = this.BsR(3);
			int num3 = this.BsR(15);
			for (int i = 0; i < num3; i++)
			{
				int j = 0;
				while (this.BsR(1) == 1)
				{
					j++;
				}
				this.selectorMtf[i] = (char)j;
			}
			char[] array3 = new char[6];
			char c = '\0';
			while ((int)c < num2)
			{
				array3[(int)c] = c;
				c += '\u0001';
			}
			for (int i = 0; i < num3; i++)
			{
				c = this.selectorMtf[i];
				char c2 = array3[(int)c];
				while (c > '\0')
				{
					array3[(int)c] = array3[(int)(c - '\u0001')];
					c -= '\u0001';
				}
				array3[0] = c2;
				this.selector[i] = c2;
			}
			for (int k = 0; k < num2; k++)
			{
				int num4 = this.BsR(5);
				for (int i = 0; i < num; i++)
				{
					while (this.BsR(1) == 1)
					{
						if (this.BsR(1) == 0)
						{
							num4++;
						}
						else
						{
							num4--;
						}
					}
					array[k][i] = (char)num4;
				}
			}
			for (int k = 0; k < num2; k++)
			{
				int num5 = 32;
				int num6 = 0;
				for (int i = 0; i < num; i++)
				{
					if ((int)array[k][i] > num6)
					{
						num6 = (int)array[k][i];
					}
					if ((int)array[k][i] < num5)
					{
						num5 = (int)array[k][i];
					}
				}
				this.HbCreateDecodeTables(this.limit[k], this.basev[k], this.perm[k], array[k], num5, num6, num);
				this.minLens[k] = num5;
			}
		}

		// Token: 0x06004077 RID: 16503 RVA: 0x001607F8 File Offset: 0x001607F8
		private void GetAndMoveToFrontDecode()
		{
			char[] array = new char[256];
			int num = 100000 * this.blockSize100k;
			this.origPtr = this.BsGetIntVS(24);
			this.RecvDecodingTables();
			int num2 = this.nInUse + 1;
			int num3 = -1;
			int num4 = 0;
			for (int i = 0; i <= 255; i++)
			{
				this.unzftab[i] = 0;
			}
			for (int i = 0; i <= 255; i++)
			{
				array[i] = (char)i;
			}
			this.last = -1;
			if (num4 == 0)
			{
				num3++;
				num4 = 50;
			}
			num4--;
			int num5 = (int)this.selector[num3];
			int num6 = this.minLens[num5];
			int j;
			int num8;
			for (j = this.BsR(num6); j > this.limit[num5][num6]; j = (j << 1 | num8))
			{
				num6++;
				while (this.bsLive < 1)
				{
					char c = '\0';
					try
					{
						c = (char)this.bsStream.ReadByte();
					}
					catch (IOException)
					{
						CBZip2InputStream.CompressedStreamEOF();
					}
					if (c == '￿')
					{
						CBZip2InputStream.CompressedStreamEOF();
					}
					int num7 = (int)c;
					this.bsBuff = (this.bsBuff << 8 | (num7 & 255));
					this.bsLive += 8;
				}
				num8 = (this.bsBuff >> this.bsLive - 1 & 1);
				this.bsLive--;
			}
			int num9 = this.perm[num5][j - this.basev[num5][num6]];
			while (num9 != num2)
			{
				if (num9 == 0 || num9 == 1)
				{
					int k = -1;
					int num10 = 1;
					do
					{
						if (num9 == 0)
						{
							k += num10;
						}
						else if (num9 == 1)
						{
							k += 2 * num10;
						}
						num10 *= 2;
						if (num4 == 0)
						{
							num3++;
							num4 = 50;
						}
						num4--;
						int num11 = (int)this.selector[num3];
						int num12 = this.minLens[num11];
						int l;
						int num14;
						for (l = this.BsR(num12); l > this.limit[num11][num12]; l = (l << 1 | num14))
						{
							num12++;
							while (this.bsLive < 1)
							{
								char c2 = '\0';
								try
								{
									c2 = (char)this.bsStream.ReadByte();
								}
								catch (IOException)
								{
									CBZip2InputStream.CompressedStreamEOF();
								}
								if (c2 == '￿')
								{
									CBZip2InputStream.CompressedStreamEOF();
								}
								int num13 = (int)c2;
								this.bsBuff = (this.bsBuff << 8 | (num13 & 255));
								this.bsLive += 8;
							}
							num14 = (this.bsBuff >> this.bsLive - 1 & 1);
							this.bsLive--;
						}
						num9 = this.perm[num11][l - this.basev[num11][num12]];
					}
					while (num9 == 0 || num9 == 1);
					k++;
					char c3 = this.seqToUnseq[(int)array[0]];
					int[] array2;
					IntPtr intPtr;
					(array2 = this.unzftab)[(int)(intPtr = (IntPtr)c3)] = array2[(int)intPtr] + k;
					while (k > 0)
					{
						this.last++;
						this.ll8[this.last] = c3;
						k--;
					}
					if (this.last >= num)
					{
						CBZip2InputStream.BlockOverrun();
					}
				}
				else
				{
					this.last++;
					if (this.last >= num)
					{
						CBZip2InputStream.BlockOverrun();
					}
					char c4 = array[num9 - 1];
					int[] array2;
					IntPtr intPtr;
					(array2 = this.unzftab)[(int)(intPtr = (IntPtr)this.seqToUnseq[(int)c4])] = array2[(int)intPtr] + 1;
					this.ll8[this.last] = this.seqToUnseq[(int)c4];
					int m;
					for (m = num9 - 1; m > 3; m -= 4)
					{
						array[m] = array[m - 1];
						array[m - 1] = array[m - 2];
						array[m - 2] = array[m - 3];
						array[m - 3] = array[m - 4];
					}
					while (m > 0)
					{
						array[m] = array[m - 1];
						m--;
					}
					array[0] = c4;
					if (num4 == 0)
					{
						num3++;
						num4 = 50;
					}
					num4--;
					int num15 = (int)this.selector[num3];
					int num16 = this.minLens[num15];
					int n;
					int num18;
					for (n = this.BsR(num16); n > this.limit[num15][num16]; n = (n << 1 | num18))
					{
						num16++;
						while (this.bsLive < 1)
						{
							char c5 = '\0';
							try
							{
								c5 = (char)this.bsStream.ReadByte();
							}
							catch (IOException)
							{
								CBZip2InputStream.CompressedStreamEOF();
							}
							int num17 = (int)c5;
							this.bsBuff = (this.bsBuff << 8 | (num17 & 255));
							this.bsLive += 8;
						}
						num18 = (this.bsBuff >> this.bsLive - 1 & 1);
						this.bsLive--;
					}
					num9 = this.perm[num15][n - this.basev[num15][num16]];
				}
			}
		}

		// Token: 0x06004078 RID: 16504 RVA: 0x00160D50 File Offset: 0x00160D50
		private void SetupBlock()
		{
			int[] array = new int[257];
			array[0] = 0;
			this.i = 1;
			while (this.i <= 256)
			{
				array[this.i] = this.unzftab[this.i - 1];
				this.i++;
			}
			this.i = 1;
			while (this.i <= 256)
			{
				int[] array2;
				IntPtr intPtr;
				(array2 = array)[(int)(intPtr = (IntPtr)this.i)] = array2[(int)intPtr] + array[this.i - 1];
				this.i++;
			}
			this.i = 0;
			while (this.i <= this.last)
			{
				char c = this.ll8[this.i];
				this.tt[array[(int)c]] = this.i;
				int[] array2;
				IntPtr intPtr;
				(array2 = array)[(int)(intPtr = (IntPtr)c)] = array2[(int)intPtr] + 1;
				this.i++;
			}
			this.tPos = this.tt[this.origPtr];
			this.count = 0;
			this.i2 = 0;
			this.ch2 = 256;
			if (this.blockRandomised)
			{
				this.rNToGo = 0;
				this.rTPos = 0;
				this.SetupRandPartA();
				return;
			}
			this.SetupNoRandPartA();
		}

		// Token: 0x06004079 RID: 16505 RVA: 0x00160E98 File Offset: 0x00160E98
		private void SetupRandPartA()
		{
			if (this.i2 <= this.last)
			{
				this.chPrev = this.ch2;
				this.ch2 = (int)this.ll8[this.tPos];
				this.tPos = this.tt[this.tPos];
				if (this.rNToGo == 0)
				{
					this.rNToGo = BZip2Constants.rNums[this.rTPos];
					this.rTPos++;
					if (this.rTPos == 512)
					{
						this.rTPos = 0;
					}
				}
				this.rNToGo--;
				this.ch2 ^= ((this.rNToGo == 1) ? 1 : 0);
				this.i2++;
				this.currentChar = this.ch2;
				this.currentState = 3;
				this.mCrc.UpdateCRC(this.ch2);
				return;
			}
			this.EndBlock();
			this.InitBlock();
			this.SetupBlock();
		}

		// Token: 0x0600407A RID: 16506 RVA: 0x00160FA4 File Offset: 0x00160FA4
		private void SetupNoRandPartA()
		{
			if (this.i2 <= this.last)
			{
				this.chPrev = this.ch2;
				this.ch2 = (int)this.ll8[this.tPos];
				this.tPos = this.tt[this.tPos];
				this.i2++;
				this.currentChar = this.ch2;
				this.currentState = 6;
				this.mCrc.UpdateCRC(this.ch2);
				return;
			}
			this.EndBlock();
			this.InitBlock();
			this.SetupBlock();
		}

		// Token: 0x0600407B RID: 16507 RVA: 0x00161040 File Offset: 0x00161040
		private void SetupRandPartB()
		{
			if (this.ch2 != this.chPrev)
			{
				this.currentState = 2;
				this.count = 1;
				this.SetupRandPartA();
				return;
			}
			this.count++;
			if (this.count >= 4)
			{
				this.z = this.ll8[this.tPos];
				this.tPos = this.tt[this.tPos];
				if (this.rNToGo == 0)
				{
					this.rNToGo = BZip2Constants.rNums[this.rTPos];
					this.rTPos++;
					if (this.rTPos == 512)
					{
						this.rTPos = 0;
					}
				}
				this.rNToGo--;
				this.z ^= ((this.rNToGo == 1) ? '\u0001' : '\0');
				this.j2 = 0;
				this.currentState = 4;
				this.SetupRandPartC();
				return;
			}
			this.currentState = 2;
			this.SetupRandPartA();
		}

		// Token: 0x0600407C RID: 16508 RVA: 0x0016114C File Offset: 0x0016114C
		private void SetupRandPartC()
		{
			if (this.j2 < (int)this.z)
			{
				this.currentChar = this.ch2;
				this.mCrc.UpdateCRC(this.ch2);
				this.j2++;
				return;
			}
			this.currentState = 2;
			this.i2++;
			this.count = 0;
			this.SetupRandPartA();
		}

		// Token: 0x0600407D RID: 16509 RVA: 0x001611BC File Offset: 0x001611BC
		private void SetupNoRandPartB()
		{
			if (this.ch2 != this.chPrev)
			{
				this.currentState = 5;
				this.count = 1;
				this.SetupNoRandPartA();
				return;
			}
			this.count++;
			if (this.count >= 4)
			{
				this.z = this.ll8[this.tPos];
				this.tPos = this.tt[this.tPos];
				this.currentState = 7;
				this.j2 = 0;
				this.SetupNoRandPartC();
				return;
			}
			this.currentState = 5;
			this.SetupNoRandPartA();
		}

		// Token: 0x0600407E RID: 16510 RVA: 0x00161258 File Offset: 0x00161258
		private void SetupNoRandPartC()
		{
			if (this.j2 < (int)this.z)
			{
				this.currentChar = this.ch2;
				this.mCrc.UpdateCRC(this.ch2);
				this.j2++;
				return;
			}
			this.currentState = 5;
			this.i2++;
			this.count = 0;
			this.SetupNoRandPartA();
		}

		// Token: 0x0600407F RID: 16511 RVA: 0x001612C8 File Offset: 0x001612C8
		private void SetDecompressStructureSizes(int newSize100k)
		{
			if (0 <= newSize100k && newSize100k <= 9 && 0 <= this.blockSize100k)
			{
				int num = this.blockSize100k;
			}
			this.blockSize100k = newSize100k;
			if (newSize100k == 0)
			{
				return;
			}
			int num2 = 100000 * newSize100k;
			this.ll8 = new char[num2];
			this.tt = new int[num2];
		}

		// Token: 0x06004080 RID: 16512 RVA: 0x0016132C File Offset: 0x0016132C
		public override void Flush()
		{
		}

		// Token: 0x06004081 RID: 16513 RVA: 0x00161330 File Offset: 0x00161330
		public override int Read(byte[] buffer, int offset, int count)
		{
			int i;
			for (i = 0; i < count; i++)
			{
				int num = this.ReadByte();
				if (num == -1)
				{
					break;
				}
				buffer[i + offset] = (byte)num;
			}
			return i;
		}

		// Token: 0x06004082 RID: 16514 RVA: 0x00161368 File Offset: 0x00161368
		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x0016136C File Offset: 0x0016136C
		public override void SetLength(long value)
		{
		}

		// Token: 0x06004084 RID: 16516 RVA: 0x00161370 File Offset: 0x00161370
		public override void Write(byte[] buffer, int offset, int count)
		{
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06004085 RID: 16517 RVA: 0x00161374 File Offset: 0x00161374
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06004086 RID: 16518 RVA: 0x00161378 File Offset: 0x00161378
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06004087 RID: 16519 RVA: 0x0016137C File Offset: 0x0016137C
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06004088 RID: 16520 RVA: 0x00161380 File Offset: 0x00161380
		public override long Length
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06004089 RID: 16521 RVA: 0x00161384 File Offset: 0x00161384
		// (set) Token: 0x0600408A RID: 16522 RVA: 0x00161388 File Offset: 0x00161388
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

		// Token: 0x040020EF RID: 8431
		private const int START_BLOCK_STATE = 1;

		// Token: 0x040020F0 RID: 8432
		private const int RAND_PART_A_STATE = 2;

		// Token: 0x040020F1 RID: 8433
		private const int RAND_PART_B_STATE = 3;

		// Token: 0x040020F2 RID: 8434
		private const int RAND_PART_C_STATE = 4;

		// Token: 0x040020F3 RID: 8435
		private const int NO_RAND_PART_A_STATE = 5;

		// Token: 0x040020F4 RID: 8436
		private const int NO_RAND_PART_B_STATE = 6;

		// Token: 0x040020F5 RID: 8437
		private const int NO_RAND_PART_C_STATE = 7;

		// Token: 0x040020F6 RID: 8438
		private int last;

		// Token: 0x040020F7 RID: 8439
		private int origPtr;

		// Token: 0x040020F8 RID: 8440
		private int blockSize100k;

		// Token: 0x040020F9 RID: 8441
		private bool blockRandomised;

		// Token: 0x040020FA RID: 8442
		private int bsBuff;

		// Token: 0x040020FB RID: 8443
		private int bsLive;

		// Token: 0x040020FC RID: 8444
		private CRC mCrc = new CRC();

		// Token: 0x040020FD RID: 8445
		private bool[] inUse = new bool[256];

		// Token: 0x040020FE RID: 8446
		private int nInUse;

		// Token: 0x040020FF RID: 8447
		private char[] seqToUnseq = new char[256];

		// Token: 0x04002100 RID: 8448
		private char[] unseqToSeq = new char[256];

		// Token: 0x04002101 RID: 8449
		private char[] selector = new char[18002];

		// Token: 0x04002102 RID: 8450
		private char[] selectorMtf = new char[18002];

		// Token: 0x04002103 RID: 8451
		private int[] tt;

		// Token: 0x04002104 RID: 8452
		private char[] ll8;

		// Token: 0x04002105 RID: 8453
		private int[] unzftab = new int[256];

		// Token: 0x04002106 RID: 8454
		private int[][] limit = CBZip2InputStream.InitIntArray(6, 258);

		// Token: 0x04002107 RID: 8455
		private int[][] basev = CBZip2InputStream.InitIntArray(6, 258);

		// Token: 0x04002108 RID: 8456
		private int[][] perm = CBZip2InputStream.InitIntArray(6, 258);

		// Token: 0x04002109 RID: 8457
		private int[] minLens = new int[6];

		// Token: 0x0400210A RID: 8458
		private Stream bsStream;

		// Token: 0x0400210B RID: 8459
		private bool streamEnd = false;

		// Token: 0x0400210C RID: 8460
		private int currentChar = -1;

		// Token: 0x0400210D RID: 8461
		private int currentState = 1;

		// Token: 0x0400210E RID: 8462
		private int storedBlockCRC;

		// Token: 0x0400210F RID: 8463
		private int storedCombinedCRC;

		// Token: 0x04002110 RID: 8464
		private int computedBlockCRC;

		// Token: 0x04002111 RID: 8465
		private int computedCombinedCRC;

		// Token: 0x04002112 RID: 8466
		private int i2;

		// Token: 0x04002113 RID: 8467
		private int count;

		// Token: 0x04002114 RID: 8468
		private int chPrev;

		// Token: 0x04002115 RID: 8469
		private int ch2;

		// Token: 0x04002116 RID: 8470
		private int i;

		// Token: 0x04002117 RID: 8471
		private int tPos;

		// Token: 0x04002118 RID: 8472
		private int rNToGo = 0;

		// Token: 0x04002119 RID: 8473
		private int rTPos = 0;

		// Token: 0x0400211A RID: 8474
		private int j2;

		// Token: 0x0400211B RID: 8475
		private char z;
	}
}
