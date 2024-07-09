using System;

namespace Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x020006F1 RID: 1777
	internal sealed class InfCodes
	{
		// Token: 0x06003DFA RID: 15866 RVA: 0x00155274 File Offset: 0x00155274
		internal InfCodes()
		{
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x00155284 File Offset: 0x00155284
		internal void init(int bl, int bd, int[] tl, int tl_index, int[] td, int td_index, ZStream z)
		{
			this.mode = 0;
			this.lbits = (byte)bl;
			this.dbits = (byte)bd;
			this.ltree = tl;
			this.ltree_index = tl_index;
			this.dtree = td;
			this.dtree_index = td_index;
			this.tree = null;
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x001552C4 File Offset: 0x001552C4
		internal int proc(InfBlocks s, ZStream z, int r)
		{
			int num = z.next_in_index;
			int num2 = z.avail_in;
			int num3 = s.bitb;
			int i = s.bitk;
			int num4 = s.write;
			int num5 = (num4 < s.read) ? (s.read - num4 - 1) : (s.end - num4);
			for (;;)
			{
				int num6;
				switch (this.mode)
				{
				case 0:
					if (num5 >= 258 && num2 >= 10)
					{
						s.bitb = num3;
						s.bitk = i;
						z.avail_in = num2;
						z.total_in += (long)(num - z.next_in_index);
						z.next_in_index = num;
						s.write = num4;
						r = this.inflate_fast((int)this.lbits, (int)this.dbits, this.ltree, this.ltree_index, this.dtree, this.dtree_index, s, z);
						num = z.next_in_index;
						num2 = z.avail_in;
						num3 = s.bitb;
						i = s.bitk;
						num4 = s.write;
						num5 = ((num4 < s.read) ? (s.read - num4 - 1) : (s.end - num4));
						if (r != 0)
						{
							this.mode = ((r == 1) ? 7 : 9);
							continue;
						}
					}
					this.need = (int)this.lbits;
					this.tree = this.ltree;
					this.tree_index = this.ltree_index;
					this.mode = 1;
					goto IL_1A1;
				case 1:
					goto IL_1A1;
				case 2:
					num6 = this.get;
					while (i < num6)
					{
						if (num2 == 0)
						{
							goto IL_373;
						}
						r = 0;
						num2--;
						num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
						i += 8;
					}
					this.len += (num3 & InfCodes.inflate_mask[num6]);
					num3 >>= num6;
					i -= num6;
					this.need = (int)this.dbits;
					this.tree = this.dtree;
					this.tree_index = this.dtree_index;
					this.mode = 3;
					goto IL_42C;
				case 3:
					goto IL_42C;
				case 4:
					num6 = this.get;
					while (i < num6)
					{
						if (num2 == 0)
						{
							goto IL_5C4;
						}
						r = 0;
						num2--;
						num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
						i += 8;
					}
					this.dist += (num3 & InfCodes.inflate_mask[num6]);
					num3 >>= num6;
					i -= num6;
					this.mode = 5;
					goto IL_659;
				case 5:
					goto IL_659;
				case 6:
					if (num5 == 0)
					{
						if (num4 == s.end && s.read != 0)
						{
							num4 = 0;
							num5 = ((num4 < s.read) ? (s.read - num4 - 1) : (s.end - num4));
						}
						if (num5 == 0)
						{
							s.write = num4;
							r = s.inflate_flush(z, r);
							num4 = s.write;
							num5 = ((num4 < s.read) ? (s.read - num4 - 1) : (s.end - num4));
							if (num4 == s.end && s.read != 0)
							{
								num4 = 0;
								num5 = ((num4 < s.read) ? (s.read - num4 - 1) : (s.end - num4));
							}
							if (num5 == 0)
							{
								goto Block_44;
							}
						}
					}
					r = 0;
					s.window[num4++] = (byte)this.lit;
					num5--;
					this.mode = 0;
					continue;
				case 7:
					goto IL_93F;
				case 8:
					goto IL_9EF;
				case 9:
					goto IL_A35;
				}
				break;
				IL_1A1:
				num6 = this.need;
				while (i < num6)
				{
					if (num2 == 0)
					{
						goto IL_1BC;
					}
					r = 0;
					num2--;
					num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
					i += 8;
				}
				int num7 = (this.tree_index + (num3 & InfCodes.inflate_mask[num6])) * 3;
				num3 >>= this.tree[num7 + 1];
				i -= this.tree[num7 + 1];
				int num8 = this.tree[num7];
				if (num8 == 0)
				{
					this.lit = this.tree[num7 + 2];
					this.mode = 6;
					continue;
				}
				if ((num8 & 16) != 0)
				{
					this.get = (num8 & 15);
					this.len = this.tree[num7 + 2];
					this.mode = 2;
					continue;
				}
				if ((num8 & 64) == 0)
				{
					this.need = num8;
					this.tree_index = num7 / 3 + this.tree[num7 + 2];
					continue;
				}
				if ((num8 & 32) != 0)
				{
					this.mode = 7;
					continue;
				}
				goto IL_2FE;
				IL_42C:
				num6 = this.need;
				while (i < num6)
				{
					if (num2 == 0)
					{
						goto IL_447;
					}
					r = 0;
					num2--;
					num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
					i += 8;
				}
				num7 = (this.tree_index + (num3 & InfCodes.inflate_mask[num6])) * 3;
				num3 >>= this.tree[num7 + 1];
				i -= this.tree[num7 + 1];
				num8 = this.tree[num7];
				if ((num8 & 16) != 0)
				{
					this.get = (num8 & 15);
					this.dist = this.tree[num7 + 2];
					this.mode = 4;
					continue;
				}
				if ((num8 & 64) == 0)
				{
					this.need = num8;
					this.tree_index = num7 / 3 + this.tree[num7 + 2];
					continue;
				}
				goto IL_54F;
				IL_659:
				int j;
				for (j = num4 - this.dist; j < 0; j += s.end)
				{
				}
				while (this.len != 0)
				{
					if (num5 == 0)
					{
						if (num4 == s.end && s.read != 0)
						{
							num4 = 0;
							num5 = ((num4 < s.read) ? (s.read - num4 - 1) : (s.end - num4));
						}
						if (num5 == 0)
						{
							s.write = num4;
							r = s.inflate_flush(z, r);
							num4 = s.write;
							num5 = ((num4 < s.read) ? (s.read - num4 - 1) : (s.end - num4));
							if (num4 == s.end && s.read != 0)
							{
								num4 = 0;
								num5 = ((num4 < s.read) ? (s.read - num4 - 1) : (s.end - num4));
							}
							if (num5 == 0)
							{
								goto Block_32;
							}
						}
					}
					s.window[num4++] = s.window[j++];
					num5--;
					if (j == s.end)
					{
						j = 0;
					}
					this.len--;
				}
				this.mode = 0;
			}
			r = -2;
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return s.inflate_flush(z, r);
			IL_1BC:
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return s.inflate_flush(z, r);
			IL_2FE:
			this.mode = 9;
			z.msg = "invalid literal/length code";
			r = -3;
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return s.inflate_flush(z, r);
			IL_373:
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return s.inflate_flush(z, r);
			IL_447:
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return s.inflate_flush(z, r);
			IL_54F:
			this.mode = 9;
			z.msg = "invalid distance code";
			r = -3;
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return s.inflate_flush(z, r);
			IL_5C4:
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return s.inflate_flush(z, r);
			Block_32:
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return s.inflate_flush(z, r);
			Block_44:
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return s.inflate_flush(z, r);
			IL_93F:
			if (i > 7)
			{
				i -= 8;
				num2++;
				num--;
			}
			s.write = num4;
			r = s.inflate_flush(z, r);
			num4 = s.write;
			int num9 = (num4 < s.read) ? (s.read - num4 - 1) : (s.end - num4);
			if (s.read != s.write)
			{
				s.bitb = num3;
				s.bitk = i;
				z.avail_in = num2;
				z.total_in += (long)(num - z.next_in_index);
				z.next_in_index = num;
				s.write = num4;
				return s.inflate_flush(z, r);
			}
			this.mode = 8;
			IL_9EF:
			r = 1;
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return s.inflate_flush(z, r);
			IL_A35:
			r = -3;
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return s.inflate_flush(z, r);
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x00155D98 File Offset: 0x00155D98
		internal void free(ZStream z)
		{
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x00155D9C File Offset: 0x00155D9C
		internal int inflate_fast(int bl, int bd, int[] tl, int tl_index, int[] td, int td_index, InfBlocks s, ZStream z)
		{
			int num = z.next_in_index;
			int num2 = z.avail_in;
			int num3 = s.bitb;
			int i = s.bitk;
			int num4 = s.write;
			int num5 = (num4 < s.read) ? (s.read - num4 - 1) : (s.end - num4);
			int num6 = InfCodes.inflate_mask[bl];
			int num7 = InfCodes.inflate_mask[bd];
			int num10;
			int num11;
			for (;;)
			{
				if (i >= 20)
				{
					int num8 = num3 & num6;
					int num9 = (tl_index + num8) * 3;
					if ((num10 = tl[num9]) == 0)
					{
						num3 >>= tl[num9 + 1];
						i -= tl[num9 + 1];
						s.window[num4++] = (byte)tl[num9 + 2];
						num5--;
					}
					else
					{
						for (;;)
						{
							num3 >>= tl[num9 + 1];
							i -= tl[num9 + 1];
							if ((num10 & 16) != 0)
							{
								break;
							}
							if ((num10 & 64) != 0)
							{
								goto IL_4E2;
							}
							num8 += tl[num9 + 2];
							num8 += (num3 & InfCodes.inflate_mask[num10]);
							num9 = (tl_index + num8) * 3;
							if ((num10 = tl[num9]) == 0)
							{
								goto Block_20;
							}
						}
						num10 &= 15;
						num11 = tl[num9 + 2] + (num3 & InfCodes.inflate_mask[num10]);
						num3 >>= num10;
						for (i -= num10; i < 15; i += 8)
						{
							num2--;
							num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
						}
						num8 = (num3 & num7);
						num9 = (td_index + num8) * 3;
						num10 = td[num9];
						for (;;)
						{
							num3 >>= td[num9 + 1];
							i -= td[num9 + 1];
							if ((num10 & 16) != 0)
							{
								break;
							}
							if ((num10 & 64) != 0)
							{
								goto IL_3EC;
							}
							num8 += td[num9 + 2];
							num8 += (num3 & InfCodes.inflate_mask[num10]);
							num9 = (td_index + num8) * 3;
							num10 = td[num9];
						}
						num10 &= 15;
						while (i < num10)
						{
							num2--;
							num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
							i += 8;
						}
						int num12 = td[num9 + 2] + (num3 & InfCodes.inflate_mask[num10]);
						num3 >>= num10;
						i -= num10;
						num5 -= num11;
						int num13;
						if (num4 >= num12)
						{
							num13 = num4 - num12;
							if (num4 - num13 > 0 && 2 > num4 - num13)
							{
								s.window[num4++] = s.window[num13++];
								s.window[num4++] = s.window[num13++];
								num11 -= 2;
							}
							else
							{
								Array.Copy(s.window, num13, s.window, num4, 2);
								num4 += 2;
								num13 += 2;
								num11 -= 2;
							}
						}
						else
						{
							num13 = num4 - num12;
							do
							{
								num13 += s.end;
							}
							while (num13 < 0);
							num10 = s.end - num13;
							if (num11 > num10)
							{
								num11 -= num10;
								if (num4 - num13 > 0 && num10 > num4 - num13)
								{
									do
									{
										s.window[num4++] = s.window[num13++];
									}
									while (--num10 != 0);
								}
								else
								{
									Array.Copy(s.window, num13, s.window, num4, num10);
									num4 += num10;
									num13 += num10;
								}
								num13 = 0;
							}
						}
						if (num4 - num13 > 0 && num11 > num4 - num13)
						{
							do
							{
								s.window[num4++] = s.window[num13++];
							}
							while (--num11 != 0);
							goto IL_5E3;
						}
						Array.Copy(s.window, num13, s.window, num4, num11);
						num4 += num11;
						num13 += num11;
						goto IL_5E3;
						Block_20:
						num3 >>= tl[num9 + 1];
						i -= tl[num9 + 1];
						s.window[num4++] = (byte)tl[num9 + 2];
						num5--;
					}
					IL_5E3:
					if (num5 < 258 || num2 < 10)
					{
						goto IL_5F7;
					}
				}
				else
				{
					num2--;
					num3 |= (int)(z.next_in[num++] & byte.MaxValue) << i;
					i += 8;
				}
			}
			IL_3EC:
			z.msg = "invalid distance code";
			num11 = z.avail_in - num2;
			num11 = ((i >> 3 < num11) ? (i >> 3) : num11);
			num2 += num11;
			num -= num11;
			i -= num11 << 3;
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return -3;
			IL_4E2:
			if ((num10 & 32) != 0)
			{
				num11 = z.avail_in - num2;
				num11 = ((i >> 3 < num11) ? (i >> 3) : num11);
				num2 += num11;
				num -= num11;
				i -= num11 << 3;
				s.bitb = num3;
				s.bitk = i;
				z.avail_in = num2;
				z.total_in += (long)(num - z.next_in_index);
				z.next_in_index = num;
				s.write = num4;
				return 1;
			}
			z.msg = "invalid literal/length code";
			num11 = z.avail_in - num2;
			num11 = ((i >> 3 < num11) ? (i >> 3) : num11);
			num2 += num11;
			num -= num11;
			i -= num11 << 3;
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return -3;
			IL_5F7:
			num11 = z.avail_in - num2;
			num11 = ((i >> 3 < num11) ? (i >> 3) : num11);
			num2 += num11;
			num -= num11;
			i -= num11 << 3;
			s.bitb = num3;
			s.bitk = i;
			z.avail_in = num2;
			z.total_in += (long)(num - z.next_in_index);
			z.next_in_index = num;
			s.write = num4;
			return 0;
		}

		// Token: 0x04001FA0 RID: 8096
		private const int Z_OK = 0;

		// Token: 0x04001FA1 RID: 8097
		private const int Z_STREAM_END = 1;

		// Token: 0x04001FA2 RID: 8098
		private const int Z_NEED_DICT = 2;

		// Token: 0x04001FA3 RID: 8099
		private const int Z_ERRNO = -1;

		// Token: 0x04001FA4 RID: 8100
		private const int Z_STREAM_ERROR = -2;

		// Token: 0x04001FA5 RID: 8101
		private const int Z_DATA_ERROR = -3;

		// Token: 0x04001FA6 RID: 8102
		private const int Z_MEM_ERROR = -4;

		// Token: 0x04001FA7 RID: 8103
		private const int Z_BUF_ERROR = -5;

		// Token: 0x04001FA8 RID: 8104
		private const int Z_VERSION_ERROR = -6;

		// Token: 0x04001FA9 RID: 8105
		private const int START = 0;

		// Token: 0x04001FAA RID: 8106
		private const int LEN = 1;

		// Token: 0x04001FAB RID: 8107
		private const int LENEXT = 2;

		// Token: 0x04001FAC RID: 8108
		private const int DIST = 3;

		// Token: 0x04001FAD RID: 8109
		private const int DISTEXT = 4;

		// Token: 0x04001FAE RID: 8110
		private const int COPY = 5;

		// Token: 0x04001FAF RID: 8111
		private const int LIT = 6;

		// Token: 0x04001FB0 RID: 8112
		private const int WASH = 7;

		// Token: 0x04001FB1 RID: 8113
		private const int END = 8;

		// Token: 0x04001FB2 RID: 8114
		private const int BADCODE = 9;

		// Token: 0x04001FB3 RID: 8115
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

		// Token: 0x04001FB4 RID: 8116
		private int mode;

		// Token: 0x04001FB5 RID: 8117
		private int len;

		// Token: 0x04001FB6 RID: 8118
		private int[] tree;

		// Token: 0x04001FB7 RID: 8119
		private int tree_index = 0;

		// Token: 0x04001FB8 RID: 8120
		private int need;

		// Token: 0x04001FB9 RID: 8121
		private int lit;

		// Token: 0x04001FBA RID: 8122
		private int get;

		// Token: 0x04001FBB RID: 8123
		private int dist;

		// Token: 0x04001FBC RID: 8124
		private byte lbits;

		// Token: 0x04001FBD RID: 8125
		private byte dbits;

		// Token: 0x04001FBE RID: 8126
		private int[] ltree;

		// Token: 0x04001FBF RID: 8127
		private int ltree_index;

		// Token: 0x04001FC0 RID: 8128
		private int[] dtree;

		// Token: 0x04001FC1 RID: 8129
		private int dtree_index;
	}
}
