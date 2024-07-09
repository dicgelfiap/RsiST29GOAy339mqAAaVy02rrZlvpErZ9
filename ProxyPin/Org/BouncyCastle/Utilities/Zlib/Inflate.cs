using System;

namespace Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x020006F2 RID: 1778
	internal sealed class Inflate
	{
		// Token: 0x06003E00 RID: 15872 RVA: 0x00156434 File Offset: 0x00156434
		internal int inflateReset(ZStream z)
		{
			if (z == null || z.istate == null)
			{
				return -2;
			}
			z.total_in = (z.total_out = 0L);
			z.msg = null;
			z.istate.mode = ((z.istate.nowrap != 0) ? 7 : 0);
			z.istate.blocks.reset(z, null);
			return 0;
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x001564A8 File Offset: 0x001564A8
		internal int inflateEnd(ZStream z)
		{
			if (this.blocks != null)
			{
				this.blocks.free(z);
			}
			this.blocks = null;
			return 0;
		}

		// Token: 0x06003E02 RID: 15874 RVA: 0x001564CC File Offset: 0x001564CC
		internal int inflateInit(ZStream z, int w)
		{
			z.msg = null;
			this.blocks = null;
			this.nowrap = 0;
			if (w < 0)
			{
				w = -w;
				this.nowrap = 1;
			}
			if (w < 8 || w > 15)
			{
				this.inflateEnd(z);
				return -2;
			}
			this.wbits = w;
			z.istate.blocks = new InfBlocks(z, (z.istate.nowrap != 0) ? null : this, 1 << w);
			this.inflateReset(z);
			return 0;
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x0015655C File Offset: 0x0015655C
		internal int inflate(ZStream z, int f)
		{
			if (z == null || z.istate == null || z.next_in == null)
			{
				return -2;
			}
			f = ((f == 4) ? -5 : 0);
			int num = -5;
			for (;;)
			{
				switch (z.istate.mode)
				{
				case 0:
					if (z.avail_in == 0)
					{
						return num;
					}
					num = f;
					z.avail_in--;
					z.total_in += 1L;
					if (((z.istate.method = (int)z.next_in[z.next_in_index++]) & 15) != 8)
					{
						z.istate.mode = 13;
						z.msg = "unknown compression method";
						z.istate.marker = 5;
						continue;
					}
					if ((z.istate.method >> 4) + 8 > z.istate.wbits)
					{
						z.istate.mode = 13;
						z.msg = "invalid window size";
						z.istate.marker = 5;
						continue;
					}
					z.istate.mode = 1;
					goto IL_15A;
				case 1:
					goto IL_15A;
				case 2:
					goto IL_20B;
				case 3:
					goto IL_277;
				case 4:
					goto IL_2EA;
				case 5:
					goto IL_35C;
				case 6:
					goto IL_3D9;
				case 7:
					num = z.istate.blocks.proc(z, num);
					if (num == -3)
					{
						z.istate.mode = 13;
						z.istate.marker = 0;
						continue;
					}
					if (num == 0)
					{
						num = f;
					}
					if (num != 1)
					{
						return num;
					}
					num = f;
					z.istate.blocks.reset(z, z.istate.was);
					if (z.istate.nowrap != 0)
					{
						z.istate.mode = 12;
						continue;
					}
					z.istate.mode = 8;
					goto IL_496;
				case 8:
					goto IL_496;
				case 9:
					goto IL_503;
				case 10:
					goto IL_577;
				case 11:
					goto IL_5EA;
				case 12:
					return 1;
				case 13:
					return -3;
				}
				break;
				IL_15A:
				if (z.avail_in == 0)
				{
					return num;
				}
				num = f;
				z.avail_in--;
				z.total_in += 1L;
				int num2 = (int)(z.next_in[z.next_in_index++] & byte.MaxValue);
				if (((z.istate.method << 8) + num2) % 31 != 0)
				{
					z.istate.mode = 13;
					z.msg = "incorrect header check";
					z.istate.marker = 5;
					continue;
				}
				if ((num2 & 32) == 0)
				{
					z.istate.mode = 7;
					continue;
				}
				goto IL_1FF;
				IL_5EA:
				if (z.avail_in == 0)
				{
					return num;
				}
				num = f;
				z.avail_in--;
				z.total_in += 1L;
				z.istate.need += (long)((ulong)z.next_in[z.next_in_index++] & 255UL);
				if ((int)z.istate.was[0] != (int)z.istate.need)
				{
					z.istate.mode = 13;
					z.msg = "incorrect data check";
					z.istate.marker = 5;
					continue;
				}
				goto IL_690;
				IL_577:
				if (z.avail_in == 0)
				{
					return num;
				}
				num = f;
				z.avail_in--;
				z.total_in += 1L;
				z.istate.need += ((long)((long)(z.next_in[z.next_in_index++] & byte.MaxValue) << 8) & 65280L);
				z.istate.mode = 11;
				goto IL_5EA;
				IL_503:
				if (z.avail_in == 0)
				{
					return num;
				}
				num = f;
				z.avail_in--;
				z.total_in += 1L;
				z.istate.need += ((long)((long)(z.next_in[z.next_in_index++] & byte.MaxValue) << 16) & 16711680L);
				z.istate.mode = 10;
				goto IL_577;
				IL_496:
				if (z.avail_in == 0)
				{
					return num;
				}
				num = f;
				z.avail_in--;
				z.total_in += 1L;
				z.istate.need = ((long)((long)(z.next_in[z.next_in_index++] & byte.MaxValue) << 24) & (long)((ulong)-16777216));
				z.istate.mode = 9;
				goto IL_503;
			}
			return -2;
			IL_1FF:
			z.istate.mode = 2;
			IL_20B:
			if (z.avail_in == 0)
			{
				return num;
			}
			num = f;
			z.avail_in--;
			z.total_in += 1L;
			z.istate.need = ((long)((long)(z.next_in[z.next_in_index++] & byte.MaxValue) << 24) & (long)((ulong)-16777216));
			z.istate.mode = 3;
			IL_277:
			if (z.avail_in == 0)
			{
				return num;
			}
			num = f;
			z.avail_in--;
			z.total_in += 1L;
			z.istate.need += ((long)((long)(z.next_in[z.next_in_index++] & byte.MaxValue) << 16) & 16711680L);
			z.istate.mode = 4;
			IL_2EA:
			if (z.avail_in == 0)
			{
				return num;
			}
			num = f;
			z.avail_in--;
			z.total_in += 1L;
			z.istate.need += ((long)((long)(z.next_in[z.next_in_index++] & byte.MaxValue) << 8) & 65280L);
			z.istate.mode = 5;
			IL_35C:
			if (z.avail_in == 0)
			{
				return num;
			}
			z.avail_in--;
			z.total_in += 1L;
			z.istate.need += (long)((ulong)z.next_in[z.next_in_index++] & 255UL);
			z.adler = z.istate.need;
			z.istate.mode = 6;
			return 2;
			IL_3D9:
			z.istate.mode = 13;
			z.msg = "need dictionary";
			z.istate.marker = 0;
			return -2;
			IL_690:
			z.istate.mode = 12;
			return 1;
		}

		// Token: 0x06003E04 RID: 15876 RVA: 0x00156C14 File Offset: 0x00156C14
		internal int inflateSetDictionary(ZStream z, byte[] dictionary, int dictLength)
		{
			int start = 0;
			int num = dictLength;
			if (z == null || z.istate == null || z.istate.mode != 6)
			{
				return -2;
			}
			if (z._adler.adler32(1L, dictionary, 0, dictLength) != z.adler)
			{
				return -3;
			}
			z.adler = z._adler.adler32(0L, null, 0, 0);
			if (num >= 1 << z.istate.wbits)
			{
				num = (1 << z.istate.wbits) - 1;
				start = dictLength - num;
			}
			z.istate.blocks.set_dictionary(dictionary, start, num);
			z.istate.mode = 7;
			return 0;
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x00156CD0 File Offset: 0x00156CD0
		internal int inflateSync(ZStream z)
		{
			if (z == null || z.istate == null)
			{
				return -2;
			}
			if (z.istate.mode != 13)
			{
				z.istate.mode = 13;
				z.istate.marker = 0;
			}
			int num;
			if ((num = z.avail_in) == 0)
			{
				return -5;
			}
			int num2 = z.next_in_index;
			int num3 = z.istate.marker;
			while (num != 0 && num3 < 4)
			{
				if (z.next_in[num2] == Inflate.mark[num3])
				{
					num3++;
				}
				else if (z.next_in[num2] != 0)
				{
					num3 = 0;
				}
				else
				{
					num3 = 4 - num3;
				}
				num2++;
				num--;
			}
			z.total_in += (long)(num2 - z.next_in_index);
			z.next_in_index = num2;
			z.avail_in = num;
			z.istate.marker = num3;
			if (num3 != 4)
			{
				return -3;
			}
			long total_in = z.total_in;
			long total_out = z.total_out;
			this.inflateReset(z);
			z.total_in = total_in;
			z.total_out = total_out;
			z.istate.mode = 7;
			return 0;
		}

		// Token: 0x06003E06 RID: 15878 RVA: 0x00156DFC File Offset: 0x00156DFC
		internal int inflateSyncPoint(ZStream z)
		{
			if (z == null || z.istate == null || z.istate.blocks == null)
			{
				return -2;
			}
			return z.istate.blocks.sync_point();
		}

		// Token: 0x04001FC2 RID: 8130
		private const int MAX_WBITS = 15;

		// Token: 0x04001FC3 RID: 8131
		private const int PRESET_DICT = 32;

		// Token: 0x04001FC4 RID: 8132
		internal const int Z_NO_FLUSH = 0;

		// Token: 0x04001FC5 RID: 8133
		internal const int Z_PARTIAL_FLUSH = 1;

		// Token: 0x04001FC6 RID: 8134
		internal const int Z_SYNC_FLUSH = 2;

		// Token: 0x04001FC7 RID: 8135
		internal const int Z_FULL_FLUSH = 3;

		// Token: 0x04001FC8 RID: 8136
		internal const int Z_FINISH = 4;

		// Token: 0x04001FC9 RID: 8137
		private const int Z_DEFLATED = 8;

		// Token: 0x04001FCA RID: 8138
		private const int Z_OK = 0;

		// Token: 0x04001FCB RID: 8139
		private const int Z_STREAM_END = 1;

		// Token: 0x04001FCC RID: 8140
		private const int Z_NEED_DICT = 2;

		// Token: 0x04001FCD RID: 8141
		private const int Z_ERRNO = -1;

		// Token: 0x04001FCE RID: 8142
		private const int Z_STREAM_ERROR = -2;

		// Token: 0x04001FCF RID: 8143
		private const int Z_DATA_ERROR = -3;

		// Token: 0x04001FD0 RID: 8144
		private const int Z_MEM_ERROR = -4;

		// Token: 0x04001FD1 RID: 8145
		private const int Z_BUF_ERROR = -5;

		// Token: 0x04001FD2 RID: 8146
		private const int Z_VERSION_ERROR = -6;

		// Token: 0x04001FD3 RID: 8147
		private const int METHOD = 0;

		// Token: 0x04001FD4 RID: 8148
		private const int FLAG = 1;

		// Token: 0x04001FD5 RID: 8149
		private const int DICT4 = 2;

		// Token: 0x04001FD6 RID: 8150
		private const int DICT3 = 3;

		// Token: 0x04001FD7 RID: 8151
		private const int DICT2 = 4;

		// Token: 0x04001FD8 RID: 8152
		private const int DICT1 = 5;

		// Token: 0x04001FD9 RID: 8153
		private const int DICT0 = 6;

		// Token: 0x04001FDA RID: 8154
		private const int BLOCKS = 7;

		// Token: 0x04001FDB RID: 8155
		private const int CHECK4 = 8;

		// Token: 0x04001FDC RID: 8156
		private const int CHECK3 = 9;

		// Token: 0x04001FDD RID: 8157
		private const int CHECK2 = 10;

		// Token: 0x04001FDE RID: 8158
		private const int CHECK1 = 11;

		// Token: 0x04001FDF RID: 8159
		private const int DONE = 12;

		// Token: 0x04001FE0 RID: 8160
		private const int BAD = 13;

		// Token: 0x04001FE1 RID: 8161
		internal int mode;

		// Token: 0x04001FE2 RID: 8162
		internal int method;

		// Token: 0x04001FE3 RID: 8163
		internal long[] was = new long[1];

		// Token: 0x04001FE4 RID: 8164
		internal long need;

		// Token: 0x04001FE5 RID: 8165
		internal int marker;

		// Token: 0x04001FE6 RID: 8166
		internal int nowrap;

		// Token: 0x04001FE7 RID: 8167
		internal int wbits;

		// Token: 0x04001FE8 RID: 8168
		internal InfBlocks blocks;

		// Token: 0x04001FE9 RID: 8169
		private static readonly byte[] mark = new byte[]
		{
			0,
			0,
			byte.MaxValue,
			byte.MaxValue
		};
	}
}
