using System;

namespace Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x020006F0 RID: 1776
	internal sealed class InfBlocks
	{
		// Token: 0x06003DF2 RID: 15858 RVA: 0x001540A0 File Offset: 0x001540A0
		internal InfBlocks(ZStream z, object checkfn, int w)
		{
			this.hufts = new int[4320];
			this.window = new byte[w];
			this.end = w;
			this.checkfn = checkfn;
			this.mode = 0;
			this.reset(z, null);
		}

		// Token: 0x06003DF3 RID: 15859 RVA: 0x00154120 File Offset: 0x00154120
		internal void reset(ZStream z, long[] c)
		{
			if (c != null)
			{
				c[0] = this.check;
			}
			if (this.mode != 4)
			{
				int num = this.mode;
			}
			if (this.mode == 6)
			{
				this.codes.free(z);
			}
			this.mode = 0;
			this.bitk = 0;
			this.bitb = 0;
			this.read = (this.write = 0);
			if (this.checkfn != null)
			{
				z.adler = (this.check = z._adler.adler32(0L, null, 0, 0));
			}
		}

		// Token: 0x06003DF4 RID: 15860 RVA: 0x001541BC File Offset: 0x001541BC
		internal int proc(ZStream z, int r)
		{
			int num = z.next_in_index;
			int num2 = z.avail_in;
			int num3 = this.bitb;
			int i = this.bitk;
			int num4 = this.write;
			int num5 = (num4 < this.read) ? (this.read - num4 - 1) : (this.end - num4);
			int num6;
			for (;;)
			{
				switch (this.mode)
				{
				case 0:
					while (i < 3)
					{
						if (num2 == 0)
						{
							goto IL_96;
						}
						r = 0;
						num2--;
						num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
						i += 8;
					}
					num6 = (num3 & 7);
					this.last = (num6 & 1);
					switch (num6 >> 1)
					{
					case 0:
						num3 >>= 3;
						i -= 3;
						num6 = (i & 7);
						num3 >>= num6;
						i -= num6;
						this.mode = 1;
						continue;
					case 1:
					{
						int[] array = new int[1];
						int[] array2 = new int[1];
						int[][] array3 = new int[1][];
						int[][] array4 = new int[1][];
						InfTree.inflate_trees_fixed(array, array2, array3, array4, z);
						this.codes.init(array[0], array2[0], array3[0], 0, array4[0], 0, z);
						num3 >>= 3;
						i -= 3;
						this.mode = 6;
						continue;
					}
					case 2:
						num3 >>= 3;
						i -= 3;
						this.mode = 3;
						continue;
					case 3:
						goto IL_1D3;
					default:
						continue;
					}
					break;
				case 1:
					while (i < 32)
					{
						if (num2 == 0)
						{
							goto IL_243;
						}
						r = 0;
						num2--;
						num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
						i += 8;
					}
					if ((~num3 >> 16 & 65535) != (num3 & 65535))
					{
						goto Block_8;
					}
					this.left = (num3 & 65535);
					i = (num3 = 0);
					this.mode = ((this.left != 0) ? 2 : ((this.last != 0) ? 7 : 0));
					continue;
				case 2:
					if (num2 == 0)
					{
						goto Block_11;
					}
					if (num5 == 0)
					{
						if (num4 == this.end && this.read != 0)
						{
							num4 = 0;
							num5 = ((num4 < this.read) ? (this.read - num4 - 1) : (this.end - num4));
						}
						if (num5 == 0)
						{
							this.write = num4;
							r = this.inflate_flush(z, r);
							num4 = this.write;
							num5 = ((num4 < this.read) ? (this.read - num4 - 1) : (this.end - num4));
							if (num4 == this.end && this.read != 0)
							{
								num4 = 0;
								num5 = ((num4 < this.read) ? (this.read - num4 - 1) : (this.end - num4));
							}
							if (num5 == 0)
							{
								goto Block_21;
							}
						}
					}
					r = 0;
					num6 = this.left;
					if (num6 > num2)
					{
						num6 = num2;
					}
					if (num6 > num5)
					{
						num6 = num5;
					}
					Array.Copy(z.next_in, num, this.window, num4, num6);
					num += num6;
					num2 -= num6;
					num4 += num6;
					num5 -= num6;
					if ((this.left -= num6) == 0)
					{
						this.mode = ((this.last != 0) ? 7 : 0);
						continue;
					}
					continue;
				case 3:
					while (i < 14)
					{
						if (num2 == 0)
						{
							goto IL_55B;
						}
						r = 0;
						num2--;
						num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
						i += 8;
					}
					num6 = (this.table = (num3 & 16383));
					if ((num6 & 31) > 29 || (num6 >> 5 & 31) > 29)
					{
						goto IL_5EF;
					}
					num6 = 258 + (num6 & 31) + (num6 >> 5 & 31);
					if (this.blens == null || this.blens.Length < num6)
					{
						this.blens = new int[num6];
					}
					else
					{
						for (int j = 0; j < num6; j++)
						{
							this.blens[j] = 0;
						}
					}
					num3 >>= 14;
					i -= 14;
					this.index = 0;
					this.mode = 4;
					goto IL_767;
				case 4:
					goto IL_767;
				case 5:
					goto IL_84C;
				case 6:
					goto IL_C35;
				case 7:
					goto IL_D08;
				case 8:
					goto IL_DA5;
				case 9:
					goto IL_DEB;
				}
				break;
				IL_767:
				while (this.index < 4 + (this.table >> 10))
				{
					while (i < 3)
					{
						if (num2 == 0)
						{
							goto IL_6D3;
						}
						r = 0;
						num2--;
						num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
						i += 8;
					}
					this.blens[InfBlocks.border[this.index++]] = (num3 & 7);
					num3 >>= 3;
					i -= 3;
				}
				while (this.index < 19)
				{
					this.blens[InfBlocks.border[this.index++]] = 0;
				}
				this.bb[0] = 7;
				num6 = this.inftree.inflate_trees_bits(this.blens, this.bb, this.tb, this.hufts, z);
				if (num6 != 0)
				{
					goto Block_34;
				}
				this.index = 0;
				this.mode = 5;
				for (;;)
				{
					IL_84C:
					num6 = this.table;
					if (this.index >= 258 + (num6 & 31) + (num6 >> 5 & 31))
					{
						break;
					}
					num6 = this.bb[0];
					while (i < num6)
					{
						if (num2 == 0)
						{
							goto IL_88F;
						}
						r = 0;
						num2--;
						num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
						i += 8;
					}
					int num7 = this.tb[0];
					num6 = this.hufts[(this.tb[0] + (num3 & InfBlocks.inflate_mask[num6])) * 3 + 1];
					int num8 = this.hufts[(this.tb[0] + (num3 & InfBlocks.inflate_mask[num6])) * 3 + 2];
					if (num8 < 16)
					{
						num3 >>= num6;
						i -= num6;
						this.blens[this.index++] = num8;
					}
					else
					{
						int num9 = (num8 == 18) ? 7 : (num8 - 14);
						int num10 = (num8 == 18) ? 11 : 3;
						while (i < num6 + num9)
						{
							if (num2 == 0)
							{
								goto IL_9B7;
							}
							r = 0;
							num2--;
							num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
							i += 8;
						}
						num3 >>= num6;
						i -= num6;
						num10 += (num3 & InfBlocks.inflate_mask[num9]);
						num3 >>= num9;
						i -= num9;
						num9 = this.index;
						num6 = this.table;
						if (num9 + num10 > 258 + (num6 & 31) + (num6 >> 5 & 31) || (num8 == 16 && num9 < 1))
						{
							goto IL_A8B;
						}
						num8 = ((num8 == 16) ? this.blens[num9 - 1] : 0);
						do
						{
							this.blens[num9++] = num8;
						}
						while (--num10 != 0);
						this.index = num9;
					}
				}
				this.tb[0] = -1;
				int[] array5 = new int[1];
				int[] array6 = new int[1];
				int[] array7 = new int[1];
				int[] array8 = new int[1];
				array5[0] = 9;
				array6[0] = 6;
				num6 = this.table;
				num6 = this.inftree.inflate_trees_dynamic(257 + (num6 & 31), 1 + (num6 >> 5 & 31), this.blens, array5, array6, array7, array8, this.hufts, z);
				if (num6 != 0)
				{
					goto Block_48;
				}
				this.codes.init(array5[0], array6[0], this.hufts, array7[0], this.hufts, array8[0], z);
				this.mode = 6;
				IL_C35:
				this.bitb = num3;
				this.bitk = i;
				z.avail_in = num2;
				z.total_in += (long)(num - z.next_in_index);
				z.next_in_index = num;
				this.write = num4;
				if ((r = this.codes.proc(this, z, r)) != 1)
				{
					goto Block_50;
				}
				r = 0;
				this.codes.free(z);
				num = z.next_in_index;
				num2 = z.avail_in;
				num3 = this.bitb;
				i = this.bitk;
				num4 = this.write;
				num5 = ((num4 < this.read) ? (this.read - num4 - 1) : (this.end - num4));
				if (this.last != 0)
				{
					goto IL_D01;
				}
				this.mode = 0;
			}
			r = -2;
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			IL_96:
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			IL_1D3:
			num3 >>= 3;
			i -= 3;
			this.mode = 9;
			z.msg = "invalid block type";
			r = -3;
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			IL_243:
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			Block_8:
			this.mode = 9;
			z.msg = "invalid stored block lengths";
			r = -3;
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			Block_11:
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			Block_21:
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			IL_55B:
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			IL_5EF:
			this.mode = 9;
			z.msg = "too many length or distance symbols";
			r = -3;
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			IL_6D3:
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			Block_34:
			r = num6;
			if (r == -3)
			{
				this.blens = null;
				this.mode = 9;
			}
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			IL_88F:
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			IL_9B7:
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			IL_A8B:
			this.blens = null;
			this.mode = 9;
			z.msg = "invalid bit length repeat";
			r = -3;
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			Block_48:
			if (num6 == -3)
			{
				this.blens = null;
				this.mode = 9;
			}
			r = num6;
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			Block_50:
			return this.inflate_flush(z, r);
			IL_D01:
			this.mode = 7;
			IL_D08:
			this.write = num4;
			r = this.inflate_flush(z, r);
			num4 = this.write;
			int num11 = (num4 < this.read) ? (this.read - num4 - 1) : (this.end - num4);
			if (this.read != this.write)
			{
				this.bitb = num3;
				this.bitk = i;
				z.avail_in = num2;
				z.total_in += (long)(num - z.next_in_index);
				z.next_in_index = num;
				this.write = num4;
				return this.inflate_flush(z, r);
			}
			this.mode = 8;
			IL_DA5:
			r = 1;
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
			IL_DEB:
			r = -3;
			this.bitb = num3;
			this.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			this.write = num4;
			return this.inflate_flush(z, r);
		}

		// Token: 0x06003DF5 RID: 15861 RVA: 0x00155048 File Offset: 0x00155048
		internal void free(ZStream z)
		{
			this.reset(z, null);
			this.window = null;
			this.hufts = null;
		}

		// Token: 0x06003DF6 RID: 15862 RVA: 0x00155060 File Offset: 0x00155060
		internal void set_dictionary(byte[] d, int start, int n)
		{
			Array.Copy(d, start, this.window, 0, n);
			this.write = n;
			this.read = n;
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x00155090 File Offset: 0x00155090
		internal int sync_point()
		{
			if (this.mode != 1)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06003DF8 RID: 15864 RVA: 0x001550A4 File Offset: 0x001550A4
		internal int inflate_flush(ZStream z, int r)
		{
			int num = z.next_out_index;
			int num2 = this.read;
			int num3 = ((num2 <= this.write) ? this.write : this.end) - num2;
			if (num3 > z.avail_out)
			{
				num3 = z.avail_out;
			}
			if (num3 != 0 && r == -5)
			{
				r = 0;
			}
			z.avail_out -= num3;
			z.total_out += (long)num3;
			if (this.checkfn != null)
			{
				z.adler = (this.check = z._adler.adler32(this.check, this.window, num2, num3));
			}
			Array.Copy(this.window, num2, z.next_out, num, num3);
			num += num3;
			num2 += num3;
			if (num2 == this.end)
			{
				num2 = 0;
				if (this.write == this.end)
				{
					this.write = 0;
				}
				num3 = this.write - num2;
				if (num3 > z.avail_out)
				{
					num3 = z.avail_out;
				}
				if (num3 != 0 && r == -5)
				{
					r = 0;
				}
				z.avail_out -= num3;
				z.total_out += (long)num3;
				if (this.checkfn != null)
				{
					z.adler = (this.check = z._adler.adler32(this.check, this.window, num2, num3));
				}
				Array.Copy(this.window, num2, z.next_out, num, num3);
				num += num3;
				num2 += num3;
			}
			z.next_out_index = num;
			this.read = num2;
			return r;
		}

		// Token: 0x04001F77 RID: 8055
		private const int MANY = 1440;

		// Token: 0x04001F78 RID: 8056
		private const int Z_OK = 0;

		// Token: 0x04001F79 RID: 8057
		private const int Z_STREAM_END = 1;

		// Token: 0x04001F7A RID: 8058
		private const int Z_NEED_DICT = 2;

		// Token: 0x04001F7B RID: 8059
		private const int Z_ERRNO = -1;

		// Token: 0x04001F7C RID: 8060
		private const int Z_STREAM_ERROR = -2;

		// Token: 0x04001F7D RID: 8061
		private const int Z_DATA_ERROR = -3;

		// Token: 0x04001F7E RID: 8062
		private const int Z_MEM_ERROR = -4;

		// Token: 0x04001F7F RID: 8063
		private const int Z_BUF_ERROR = -5;

		// Token: 0x04001F80 RID: 8064
		private const int Z_VERSION_ERROR = -6;

		// Token: 0x04001F81 RID: 8065
		private const int TYPE = 0;

		// Token: 0x04001F82 RID: 8066
		private const int LENS = 1;

		// Token: 0x04001F83 RID: 8067
		private const int STORED = 2;

		// Token: 0x04001F84 RID: 8068
		private const int TABLE = 3;

		// Token: 0x04001F85 RID: 8069
		private const int BTREE = 4;

		// Token: 0x04001F86 RID: 8070
		private const int DTREE = 5;

		// Token: 0x04001F87 RID: 8071
		private const int CODES = 6;

		// Token: 0x04001F88 RID: 8072
		private const int DRY = 7;

		// Token: 0x04001F89 RID: 8073
		private const int DONE = 8;

		// Token: 0x04001F8A RID: 8074
		private const int BAD = 9;

		// Token: 0x04001F8B RID: 8075
		private static readonly int[] inflate_mask = new int[]
		{
			0,
			1,
			3,
			7,
			15,
			31,
			63,
			127,
			255,
			511,
			1023,
			2047,
			4095,
			8191,
			16383,
			32767,
			65535
		};

		// Token: 0x04001F8C RID: 8076
		private static readonly int[] border = new int[]
		{
			16,
			17,
			18,
			0,
			8,
			7,
			9,
			6,
			10,
			5,
			11,
			4,
			12,
			3,
			13,
			2,
			14,
			1,
			15
		};

		// Token: 0x04001F8D RID: 8077
		internal int mode;

		// Token: 0x04001F8E RID: 8078
		internal int left;

		// Token: 0x04001F8F RID: 8079
		internal int table;

		// Token: 0x04001F90 RID: 8080
		internal int index;

		// Token: 0x04001F91 RID: 8081
		internal int[] blens;

		// Token: 0x04001F92 RID: 8082
		internal int[] bb = new int[1];

		// Token: 0x04001F93 RID: 8083
		internal int[] tb = new int[1];

		// Token: 0x04001F94 RID: 8084
		internal InfCodes codes = new InfCodes();

		// Token: 0x04001F95 RID: 8085
		private int last;

		// Token: 0x04001F96 RID: 8086
		internal int bitk;

		// Token: 0x04001F97 RID: 8087
		internal int bitb;

		// Token: 0x04001F98 RID: 8088
		internal int[] hufts;

		// Token: 0x04001F99 RID: 8089
		internal byte[] window;

		// Token: 0x04001F9A RID: 8090
		internal int end;

		// Token: 0x04001F9B RID: 8091
		internal int read;

		// Token: 0x04001F9C RID: 8092
		internal int write;

		// Token: 0x04001F9D RID: 8093
		internal object checkfn;

		// Token: 0x04001F9E RID: 8094
		internal long check;

		// Token: 0x04001F9F RID: 8095
		internal InfTree inftree = new InfTree();
	}
}
