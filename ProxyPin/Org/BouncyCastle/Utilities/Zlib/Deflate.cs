using System;

namespace Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x020006EF RID: 1775
	public sealed class Deflate
	{
		// Token: 0x06003DCA RID: 15818 RVA: 0x00151A90 File Offset: 0x00151A90
		static Deflate()
		{
			Deflate.config_table = new Deflate.Config[10];
			Deflate.config_table[0] = new Deflate.Config(0, 0, 0, 0, 0);
			Deflate.config_table[1] = new Deflate.Config(4, 4, 8, 4, 1);
			Deflate.config_table[2] = new Deflate.Config(4, 5, 16, 8, 1);
			Deflate.config_table[3] = new Deflate.Config(4, 6, 32, 32, 1);
			Deflate.config_table[4] = new Deflate.Config(4, 4, 16, 16, 2);
			Deflate.config_table[5] = new Deflate.Config(8, 16, 32, 32, 2);
			Deflate.config_table[6] = new Deflate.Config(8, 16, 128, 128, 2);
			Deflate.config_table[7] = new Deflate.Config(8, 32, 128, 256, 2);
			Deflate.config_table[8] = new Deflate.Config(32, 128, 258, 1024, 2);
			Deflate.config_table[9] = new Deflate.Config(32, 258, 258, 4096, 2);
		}

		// Token: 0x06003DCB RID: 15819 RVA: 0x00151C3C File Offset: 0x00151C3C
		internal Deflate()
		{
			this.dyn_ltree = new short[1146];
			this.dyn_dtree = new short[122];
			this.bl_tree = new short[78];
		}

		// Token: 0x06003DCC RID: 15820 RVA: 0x00151CCC File Offset: 0x00151CCC
		internal void lm_init()
		{
			this.window_size = 2 * this.w_size;
			this.head[this.hash_size - 1] = 0;
			for (int i = 0; i < this.hash_size - 1; i++)
			{
				this.head[i] = 0;
			}
			this.max_lazy_match = Deflate.config_table[this.level].max_lazy;
			this.good_match = Deflate.config_table[this.level].good_length;
			this.nice_match = Deflate.config_table[this.level].nice_length;
			this.max_chain_length = Deflate.config_table[this.level].max_chain;
			this.strstart = 0;
			this.block_start = 0;
			this.lookahead = 0;
			this.match_length = (this.prev_length = 2);
			this.match_available = 0;
			this.ins_h = 0;
		}

		// Token: 0x06003DCD RID: 15821 RVA: 0x00151DBC File Offset: 0x00151DBC
		internal void tr_init()
		{
			this.l_desc.dyn_tree = this.dyn_ltree;
			this.l_desc.stat_desc = StaticTree.static_l_desc;
			this.d_desc.dyn_tree = this.dyn_dtree;
			this.d_desc.stat_desc = StaticTree.static_d_desc;
			this.bl_desc.dyn_tree = this.bl_tree;
			this.bl_desc.stat_desc = StaticTree.static_bl_desc;
			this.bi_buf = 0U;
			this.bi_valid = 0;
			this.last_eob_len = 8;
			this.init_block();
		}

		// Token: 0x06003DCE RID: 15822 RVA: 0x00151E4C File Offset: 0x00151E4C
		internal void init_block()
		{
			for (int i = 0; i < 286; i++)
			{
				this.dyn_ltree[i * 2] = 0;
			}
			for (int j = 0; j < 30; j++)
			{
				this.dyn_dtree[j * 2] = 0;
			}
			for (int k = 0; k < 19; k++)
			{
				this.bl_tree[k * 2] = 0;
			}
			this.dyn_ltree[512] = 1;
			this.opt_len = (this.static_len = 0);
			this.last_lit = (this.matches = 0);
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x00151EE0 File Offset: 0x00151EE0
		internal void pqdownheap(short[] tree, int k)
		{
			int num = this.heap[k];
			for (int i = k << 1; i <= this.heap_len; i <<= 1)
			{
				if (i < this.heap_len && Deflate.smaller(tree, this.heap[i + 1], this.heap[i], this.depth))
				{
					i++;
				}
				if (Deflate.smaller(tree, num, this.heap[i], this.depth))
				{
					break;
				}
				this.heap[k] = this.heap[i];
				k = i;
			}
			this.heap[k] = num;
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x00151F7C File Offset: 0x00151F7C
		internal static bool smaller(short[] tree, int n, int m, byte[] depth)
		{
			short num = tree[n * 2];
			short num2 = tree[m * 2];
			return num < num2 || (num == num2 && depth[n] <= depth[m]);
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x00151FB8 File Offset: 0x00151FB8
		internal void scan_tree(short[] tree, int max_code)
		{
			int num = -1;
			int num2 = (int)tree[1];
			int num3 = 0;
			int num4 = 7;
			int num5 = 4;
			if (num2 == 0)
			{
				num4 = 138;
				num5 = 3;
			}
			tree[(max_code + 1) * 2 + 1] = -1;
			for (int i = 0; i <= max_code; i++)
			{
				int num6 = num2;
				num2 = (int)tree[(i + 1) * 2 + 1];
				if (++num3 >= num4 || num6 != num2)
				{
					if (num3 < num5)
					{
						short[] array;
						IntPtr intPtr;
						(array = this.bl_tree)[(int)(intPtr = (IntPtr)(num6 * 2))] = array[(int)intPtr] + (short)num3;
					}
					else if (num6 != 0)
					{
						short[] array;
						if (num6 != num)
						{
							IntPtr intPtr;
							(array = this.bl_tree)[(int)(intPtr = (IntPtr)(num6 * 2))] = array[(int)intPtr] + 1;
						}
						(array = this.bl_tree)[32] = array[32] + 1;
					}
					else if (num3 <= 10)
					{
						short[] array;
						(array = this.bl_tree)[34] = array[34] + 1;
					}
					else
					{
						short[] array;
						(array = this.bl_tree)[36] = array[36] + 1;
					}
					num3 = 0;
					num = num6;
					if (num2 == 0)
					{
						num4 = 138;
						num5 = 3;
					}
					else if (num6 == num2)
					{
						num4 = 6;
						num5 = 3;
					}
					else
					{
						num4 = 7;
						num5 = 4;
					}
				}
			}
		}

		// Token: 0x06003DD2 RID: 15826 RVA: 0x001520F4 File Offset: 0x001520F4
		internal int build_bl_tree()
		{
			this.scan_tree(this.dyn_ltree, this.l_desc.max_code);
			this.scan_tree(this.dyn_dtree, this.d_desc.max_code);
			this.bl_desc.build_tree(this);
			int num = 18;
			while (num >= 3 && this.bl_tree[(int)(Tree.bl_order[num] * 2 + 1)] == 0)
			{
				num--;
			}
			this.opt_len += 3 * (num + 1) + 5 + 5 + 4;
			return num;
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x00152180 File Offset: 0x00152180
		internal void send_all_trees(int lcodes, int dcodes, int blcodes)
		{
			this.send_bits(lcodes - 257, 5);
			this.send_bits(dcodes - 1, 5);
			this.send_bits(blcodes - 4, 4);
			for (int i = 0; i < blcodes; i++)
			{
				this.send_bits((int)this.bl_tree[(int)(Tree.bl_order[i] * 2 + 1)], 3);
			}
			this.send_tree(this.dyn_ltree, lcodes - 1);
			this.send_tree(this.dyn_dtree, dcodes - 1);
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x001521FC File Offset: 0x001521FC
		internal void send_tree(short[] tree, int max_code)
		{
			int num = -1;
			int num2 = (int)tree[1];
			int num3 = 0;
			int num4 = 7;
			int num5 = 4;
			if (num2 == 0)
			{
				num4 = 138;
				num5 = 3;
			}
			for (int i = 0; i <= max_code; i++)
			{
				int num6 = num2;
				num2 = (int)tree[(i + 1) * 2 + 1];
				if (++num3 >= num4 || num6 != num2)
				{
					if (num3 < num5)
					{
						do
						{
							this.send_code(num6, this.bl_tree);
						}
						while (--num3 != 0);
					}
					else if (num6 != 0)
					{
						if (num6 != num)
						{
							this.send_code(num6, this.bl_tree);
							num3--;
						}
						this.send_code(16, this.bl_tree);
						this.send_bits(num3 - 3, 2);
					}
					else if (num3 <= 10)
					{
						this.send_code(17, this.bl_tree);
						this.send_bits(num3 - 3, 3);
					}
					else
					{
						this.send_code(18, this.bl_tree);
						this.send_bits(num3 - 11, 7);
					}
					num3 = 0;
					num = num6;
					if (num2 == 0)
					{
						num4 = 138;
						num5 = 3;
					}
					else if (num6 == num2)
					{
						num4 = 6;
						num5 = 3;
					}
					else
					{
						num4 = 7;
						num5 = 4;
					}
				}
			}
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x00152330 File Offset: 0x00152330
		internal void put_byte(byte[] p, int start, int len)
		{
			Array.Copy(p, start, this.pending_buf, this.pending, len);
			this.pending += len;
		}

		// Token: 0x06003DD6 RID: 15830 RVA: 0x00152354 File Offset: 0x00152354
		internal void put_byte(byte c)
		{
			this.pending_buf[this.pending++] = c;
		}

		// Token: 0x06003DD7 RID: 15831 RVA: 0x00152380 File Offset: 0x00152380
		internal void put_short(int w)
		{
			this.pending_buf[this.pending++] = (byte)w;
			this.pending_buf[this.pending++] = (byte)(w >> 8);
		}

		// Token: 0x06003DD8 RID: 15832 RVA: 0x001523C8 File Offset: 0x001523C8
		internal void putShortMSB(int b)
		{
			this.pending_buf[this.pending++] = (byte)(b >> 8);
			this.pending_buf[this.pending++] = (byte)b;
		}

		// Token: 0x06003DD9 RID: 15833 RVA: 0x00152410 File Offset: 0x00152410
		internal void send_code(int c, short[] tree)
		{
			int num = c * 2;
			this.send_bits((int)tree[num] & 65535, (int)tree[num + 1] & 65535);
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x00152440 File Offset: 0x00152440
		internal void send_bits(int val, int length)
		{
			if (this.bi_valid > 16 - length)
			{
				this.bi_buf |= (uint)((uint)val << this.bi_valid);
				this.pending_buf[this.pending++] = (byte)this.bi_buf;
				this.pending_buf[this.pending++] = (byte)(this.bi_buf >> 8);
				this.bi_buf = (uint)val >> 16 - this.bi_valid;
				this.bi_valid += length - 16;
				return;
			}
			this.bi_buf |= (uint)((uint)val << this.bi_valid);
			this.bi_valid += length;
		}

		// Token: 0x06003DDB RID: 15835 RVA: 0x00152504 File Offset: 0x00152504
		internal void _tr_align()
		{
			this.send_bits(2, 3);
			this.send_code(256, StaticTree.static_ltree);
			this.bi_flush();
			if (1 + this.last_eob_len + 10 - this.bi_valid < 9)
			{
				this.send_bits(2, 3);
				this.send_code(256, StaticTree.static_ltree);
				this.bi_flush();
			}
			this.last_eob_len = 7;
		}

		// Token: 0x06003DDC RID: 15836 RVA: 0x00152574 File Offset: 0x00152574
		internal bool _tr_tally(int dist, int lc)
		{
			this.pending_buf[this.d_buf + this.last_lit * 2] = (byte)(dist >> 8);
			this.pending_buf[this.d_buf + this.last_lit * 2 + 1] = (byte)dist;
			this.pending_buf[this.l_buf + this.last_lit] = (byte)lc;
			this.last_lit++;
			if (dist == 0)
			{
				short[] array;
				IntPtr intPtr;
				(array = this.dyn_ltree)[(int)(intPtr = (IntPtr)(lc * 2))] = array[(int)intPtr] + 1;
			}
			else
			{
				this.matches++;
				dist--;
				short[] array;
				IntPtr intPtr;
				(array = this.dyn_ltree)[(int)(intPtr = ((IntPtr)Tree._length_code[lc] + 256 + 1) * 2)] = array[(int)intPtr] + 1;
				(array = this.dyn_dtree)[(int)(intPtr = (IntPtr)(Tree.d_code(dist) * 2))] = array[(int)intPtr] + 1;
			}
			if ((this.last_lit & 8191) == 0 && this.level > 2)
			{
				int num = this.last_lit * 8;
				int num2 = this.strstart - this.block_start;
				for (int i = 0; i < 30; i++)
				{
					num += (int)((long)this.dyn_dtree[i * 2] * (5L + (long)Tree.extra_dbits[i]));
				}
				num >>= 3;
				if (this.matches < this.last_lit / 2 && num < num2 / 2)
				{
					return true;
				}
			}
			return this.last_lit == this.lit_bufsize - 1;
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x001526E4 File Offset: 0x001526E4
		internal void compress_block(short[] ltree, short[] dtree)
		{
			int num = 0;
			if (this.last_lit != 0)
			{
				do
				{
					int num2 = ((int)this.pending_buf[this.d_buf + num * 2] << 8 & 65280) | (int)(this.pending_buf[this.d_buf + num * 2 + 1] & byte.MaxValue);
					int num3 = (int)(this.pending_buf[this.l_buf + num] & byte.MaxValue);
					num++;
					if (num2 == 0)
					{
						this.send_code(num3, ltree);
					}
					else
					{
						int num4 = (int)Tree._length_code[num3];
						this.send_code(num4 + 256 + 1, ltree);
						int num5 = Tree.extra_lbits[num4];
						if (num5 != 0)
						{
							num3 -= Tree.base_length[num4];
							this.send_bits(num3, num5);
						}
						num2--;
						num4 = Tree.d_code(num2);
						this.send_code(num4, dtree);
						num5 = Tree.extra_dbits[num4];
						if (num5 != 0)
						{
							num2 -= Tree.base_dist[num4];
							this.send_bits(num2, num5);
						}
					}
				}
				while (num < this.last_lit);
			}
			this.send_code(256, ltree);
			this.last_eob_len = (int)ltree[513];
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x001527FC File Offset: 0x001527FC
		internal void set_data_type()
		{
			int i = 0;
			int num = 0;
			int num2 = 0;
			while (i < 7)
			{
				num2 += (int)this.dyn_ltree[i * 2];
				i++;
			}
			while (i < 128)
			{
				num += (int)this.dyn_ltree[i * 2];
				i++;
			}
			while (i < 256)
			{
				num2 += (int)this.dyn_ltree[i * 2];
				i++;
			}
			this.data_type = ((num2 > num >> 2) ? 0 : 1);
		}

		// Token: 0x06003DDF RID: 15839 RVA: 0x00152880 File Offset: 0x00152880
		internal void bi_flush()
		{
			if (this.bi_valid == 16)
			{
				this.pending_buf[this.pending++] = (byte)this.bi_buf;
				this.pending_buf[this.pending++] = (byte)(this.bi_buf >> 8);
				this.bi_buf = 0U;
				this.bi_valid = 0;
				return;
			}
			if (this.bi_valid >= 8)
			{
				this.pending_buf[this.pending++] = (byte)this.bi_buf;
				this.bi_buf >>= 8;
				this.bi_buf &= 255U;
				this.bi_valid -= 8;
			}
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x00152948 File Offset: 0x00152948
		internal void bi_windup()
		{
			if (this.bi_valid > 8)
			{
				this.pending_buf[this.pending++] = (byte)this.bi_buf;
				this.pending_buf[this.pending++] = (byte)(this.bi_buf >> 8);
			}
			else if (this.bi_valid > 0)
			{
				this.pending_buf[this.pending++] = (byte)this.bi_buf;
			}
			this.bi_buf = 0U;
			this.bi_valid = 0;
		}

		// Token: 0x06003DE1 RID: 15841 RVA: 0x001529E4 File Offset: 0x001529E4
		internal void copy_block(int buf, int len, bool header)
		{
			this.bi_windup();
			this.last_eob_len = 8;
			if (header)
			{
				this.put_short((int)((short)len));
				this.put_short((int)((short)(~(short)len)));
			}
			this.put_byte(this.window, buf, len);
		}

		// Token: 0x06003DE2 RID: 15842 RVA: 0x00152A18 File Offset: 0x00152A18
		internal void flush_block_only(bool eof)
		{
			this._tr_flush_block((this.block_start >= 0) ? this.block_start : -1, this.strstart - this.block_start, eof);
			this.block_start = this.strstart;
			this.strm.flush_pending();
		}

		// Token: 0x06003DE3 RID: 15843 RVA: 0x00152A6C File Offset: 0x00152A6C
		internal int deflate_stored(int flush)
		{
			int num = Math.Min(65535, this.pending_buf.Length - 5);
			for (;;)
			{
				if (this.lookahead <= 1)
				{
					this.fill_window();
					if (this.lookahead == 0 && flush == 0)
					{
						break;
					}
					if (this.lookahead == 0)
					{
						goto IL_E4;
					}
				}
				this.strstart += this.lookahead;
				this.lookahead = 0;
				int num2 = this.block_start + num;
				if (this.strstart == 0 || this.strstart >= num2)
				{
					this.lookahead = this.strstart - num2;
					this.strstart = num2;
					this.flush_block_only(false);
					if (this.strm.avail_out == 0)
					{
						return 0;
					}
				}
				if (this.strstart - this.block_start >= this.w_size - 262)
				{
					this.flush_block_only(false);
					if (this.strm.avail_out == 0)
					{
						return 0;
					}
				}
			}
			return 0;
			IL_E4:
			this.flush_block_only(flush == 4);
			if (this.strm.avail_out == 0)
			{
				if (flush != 4)
				{
					return 0;
				}
				return 2;
			}
			else
			{
				if (flush != 4)
				{
					return 1;
				}
				return 3;
			}
		}

		// Token: 0x06003DE4 RID: 15844 RVA: 0x00152B90 File Offset: 0x00152B90
		internal void _tr_stored_block(int buf, int stored_len, bool eof)
		{
			this.send_bits(eof ? 1 : 0, 3);
			this.copy_block(buf, stored_len, true);
		}

		// Token: 0x06003DE5 RID: 15845 RVA: 0x00152BB0 File Offset: 0x00152BB0
		internal void _tr_flush_block(int buf, int stored_len, bool eof)
		{
			int num = 0;
			int num2;
			int num3;
			if (this.level > 0)
			{
				if (this.data_type == 2)
				{
					this.set_data_type();
				}
				this.l_desc.build_tree(this);
				this.d_desc.build_tree(this);
				num = this.build_bl_tree();
				num2 = this.opt_len + 3 + 7 >> 3;
				num3 = this.static_len + 3 + 7 >> 3;
				if (num3 <= num2)
				{
					num2 = num3;
				}
			}
			else
			{
				num3 = (num2 = stored_len + 5);
			}
			if (stored_len + 4 <= num2 && buf != -1)
			{
				this._tr_stored_block(buf, stored_len, eof);
			}
			else if (num3 == num2)
			{
				this.send_bits(2 + (eof ? 1 : 0), 3);
				this.compress_block(StaticTree.static_ltree, StaticTree.static_dtree);
			}
			else
			{
				this.send_bits(4 + (eof ? 1 : 0), 3);
				this.send_all_trees(this.l_desc.max_code + 1, this.d_desc.max_code + 1, num + 1);
				this.compress_block(this.dyn_ltree, this.dyn_dtree);
			}
			this.init_block();
			if (eof)
			{
				this.bi_windup();
			}
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x00152CDC File Offset: 0x00152CDC
		internal void fill_window()
		{
			for (;;)
			{
				int num = this.window_size - this.lookahead - this.strstart;
				int num2;
				if (num == 0 && this.strstart == 0 && this.lookahead == 0)
				{
					num = this.w_size;
				}
				else if (num == -1)
				{
					num--;
				}
				else if (this.strstart >= this.w_size + this.w_size - 262)
				{
					Array.Copy(this.window, this.w_size, this.window, 0, this.w_size);
					this.match_start -= this.w_size;
					this.strstart -= this.w_size;
					this.block_start -= this.w_size;
					num2 = this.hash_size;
					int num3 = num2;
					do
					{
						int num4 = (int)this.head[--num3] & 65535;
						this.head[num3] = (short)((num4 >= this.w_size) ? (num4 - this.w_size) : 0);
					}
					while (--num2 != 0);
					num2 = this.w_size;
					num3 = num2;
					do
					{
						int num4 = (int)this.prev[--num3] & 65535;
						this.prev[num3] = (short)((num4 >= this.w_size) ? (num4 - this.w_size) : 0);
					}
					while (--num2 != 0);
					num += this.w_size;
				}
				if (this.strm.avail_in == 0)
				{
					break;
				}
				num2 = this.strm.read_buf(this.window, this.strstart + this.lookahead, num);
				this.lookahead += num2;
				if (this.lookahead >= 3)
				{
					this.ins_h = (int)(this.window[this.strstart] & byte.MaxValue);
					this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[this.strstart + 1] & byte.MaxValue)) & this.hash_mask);
				}
				if (this.lookahead >= 262 || this.strm.avail_in == 0)
				{
					return;
				}
			}
		}

		// Token: 0x06003DE7 RID: 15847 RVA: 0x00152EFC File Offset: 0x00152EFC
		internal int deflate_fast(int flush)
		{
			int num = 0;
			for (;;)
			{
				if (this.lookahead < 262)
				{
					this.fill_window();
					if (this.lookahead < 262 && flush == 0)
					{
						break;
					}
					if (this.lookahead == 0)
					{
						goto IL_2DE;
					}
				}
				if (this.lookahead >= 3)
				{
					this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[this.strstart + 2] & byte.MaxValue)) & this.hash_mask);
					num = ((int)this.head[this.ins_h] & 65535);
					this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
					this.head[this.ins_h] = (short)this.strstart;
				}
				if ((long)num != 0L && (this.strstart - num & 65535) <= this.w_size - 262 && this.strategy != 2)
				{
					this.match_length = this.longest_match(num);
				}
				bool flag;
				if (this.match_length >= 3)
				{
					flag = this._tr_tally(this.strstart - this.match_start, this.match_length - 3);
					this.lookahead -= this.match_length;
					if (this.match_length <= this.max_lazy_match && this.lookahead >= 3)
					{
						this.match_length--;
						do
						{
							this.strstart++;
							this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[this.strstart + 2] & byte.MaxValue)) & this.hash_mask);
							num = ((int)this.head[this.ins_h] & 65535);
							this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
							this.head[this.ins_h] = (short)this.strstart;
						}
						while (--this.match_length != 0);
						this.strstart++;
					}
					else
					{
						this.strstart += this.match_length;
						this.match_length = 0;
						this.ins_h = (int)(this.window[this.strstart] & byte.MaxValue);
						this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[this.strstart + 1] & byte.MaxValue)) & this.hash_mask);
					}
				}
				else
				{
					flag = this._tr_tally(0, (int)(this.window[this.strstart] & byte.MaxValue));
					this.lookahead--;
					this.strstart++;
				}
				if (flag)
				{
					this.flush_block_only(false);
					if (this.strm.avail_out == 0)
					{
						return 0;
					}
				}
			}
			return 0;
			IL_2DE:
			this.flush_block_only(flush == 4);
			if (this.strm.avail_out == 0)
			{
				if (flush == 4)
				{
					return 2;
				}
				return 0;
			}
			else
			{
				if (flush != 4)
				{
					return 1;
				}
				return 3;
			}
		}

		// Token: 0x06003DE8 RID: 15848 RVA: 0x0015321C File Offset: 0x0015321C
		internal int deflate_slow(int flush)
		{
			int num = 0;
			for (;;)
			{
				if (this.lookahead < 262)
				{
					this.fill_window();
					if (this.lookahead < 262 && flush == 0)
					{
						break;
					}
					if (this.lookahead == 0)
					{
						goto IL_350;
					}
				}
				if (this.lookahead >= 3)
				{
					this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[this.strstart + 2] & byte.MaxValue)) & this.hash_mask);
					num = ((int)this.head[this.ins_h] & 65535);
					this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
					this.head[this.ins_h] = (short)this.strstart;
				}
				this.prev_length = this.match_length;
				this.prev_match = this.match_start;
				this.match_length = 2;
				if (num != 0 && this.prev_length < this.max_lazy_match && (this.strstart - num & 65535) <= this.w_size - 262)
				{
					if (this.strategy != 2)
					{
						this.match_length = this.longest_match(num);
					}
					if (this.match_length <= 5 && (this.strategy == 1 || (this.match_length == 3 && this.strstart - this.match_start > 4096)))
					{
						this.match_length = 2;
					}
				}
				if (this.prev_length >= 3 && this.match_length <= this.prev_length)
				{
					int num2 = this.strstart + this.lookahead - 3;
					bool flag = this._tr_tally(this.strstart - 1 - this.prev_match, this.prev_length - 3);
					this.lookahead -= this.prev_length - 1;
					this.prev_length -= 2;
					do
					{
						if (++this.strstart <= num2)
						{
							this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[this.strstart + 2] & byte.MaxValue)) & this.hash_mask);
							num = ((int)this.head[this.ins_h] & 65535);
							this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
							this.head[this.ins_h] = (short)this.strstart;
						}
					}
					while (--this.prev_length != 0);
					this.match_available = 0;
					this.match_length = 2;
					this.strstart++;
					if (flag)
					{
						this.flush_block_only(false);
						if (this.strm.avail_out == 0)
						{
							return 0;
						}
					}
				}
				else if (this.match_available != 0)
				{
					bool flag = this._tr_tally(0, (int)(this.window[this.strstart - 1] & byte.MaxValue));
					if (flag)
					{
						this.flush_block_only(false);
					}
					this.strstart++;
					this.lookahead--;
					if (this.strm.avail_out == 0)
					{
						return 0;
					}
				}
				else
				{
					this.match_available = 1;
					this.strstart++;
					this.lookahead--;
				}
			}
			return 0;
			IL_350:
			if (this.match_available != 0)
			{
				bool flag = this._tr_tally(0, (int)(this.window[this.strstart - 1] & byte.MaxValue));
				this.match_available = 0;
			}
			this.flush_block_only(flush == 4);
			if (this.strm.avail_out == 0)
			{
				if (flush == 4)
				{
					return 2;
				}
				return 0;
			}
			else
			{
				if (flush != 4)
				{
					return 1;
				}
				return 3;
			}
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x001535DC File Offset: 0x001535DC
		internal int longest_match(int cur_match)
		{
			int num = this.max_chain_length;
			int num2 = this.strstart;
			int num3 = this.prev_length;
			int num4 = (this.strstart > this.w_size - 262) ? (this.strstart - (this.w_size - 262)) : 0;
			int num5 = this.nice_match;
			int num6 = this.w_mask;
			int num7 = this.strstart + 258;
			byte b = this.window[num2 + num3 - 1];
			byte b2 = this.window[num2 + num3];
			if (this.prev_length >= this.good_match)
			{
				num >>= 2;
			}
			if (num5 > this.lookahead)
			{
				num5 = this.lookahead;
			}
			do
			{
				int num8 = cur_match;
				if (this.window[num8 + num3] == b2 && this.window[num8 + num3 - 1] == b && this.window[num8] == this.window[num2] && this.window[++num8] == this.window[num2 + 1])
				{
					num2 += 2;
					num8++;
					while (this.window[++num2] == this.window[++num8] && this.window[++num2] == this.window[++num8] && this.window[++num2] == this.window[++num8] && this.window[++num2] == this.window[++num8] && this.window[++num2] == this.window[++num8] && this.window[++num2] == this.window[++num8] && this.window[++num2] == this.window[++num8] && this.window[++num2] == this.window[++num8] && num2 < num7)
					{
					}
					int num9 = 258 - (num7 - num2);
					num2 = num7 - 258;
					if (num9 > num3)
					{
						this.match_start = cur_match;
						num3 = num9;
						if (num9 >= num5)
						{
							break;
						}
						b = this.window[num2 + num3 - 1];
						b2 = this.window[num2 + num3];
					}
				}
			}
			while ((cur_match = ((int)this.prev[cur_match & num6] & 65535)) > num4 && --num != 0);
			if (num3 <= this.lookahead)
			{
				return num3;
			}
			return this.lookahead;
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x00153880 File Offset: 0x00153880
		internal int deflateInit(ZStream strm, int level, int bits)
		{
			return this.deflateInit2(strm, level, 8, bits, 8, 0);
		}

		// Token: 0x06003DEB RID: 15851 RVA: 0x00153890 File Offset: 0x00153890
		internal int deflateInit(ZStream strm, int level)
		{
			return this.deflateInit(strm, level, 15);
		}

		// Token: 0x06003DEC RID: 15852 RVA: 0x0015389C File Offset: 0x0015389C
		internal int deflateInit2(ZStream strm, int level, int method, int windowBits, int memLevel, int strategy)
		{
			int num = 0;
			strm.msg = null;
			if (level == -1)
			{
				level = 6;
			}
			if (windowBits < 0)
			{
				num = 1;
				windowBits = -windowBits;
			}
			if (memLevel < 1 || memLevel > 9 || method != 8 || windowBits < 9 || windowBits > 15 || level < 0 || level > 9 || strategy < 0 || strategy > 2)
			{
				return -2;
			}
			strm.dstate = this;
			this.noheader = num;
			this.w_bits = windowBits;
			this.w_size = 1 << this.w_bits;
			this.w_mask = this.w_size - 1;
			this.hash_bits = memLevel + 7;
			this.hash_size = 1 << this.hash_bits;
			this.hash_mask = this.hash_size - 1;
			this.hash_shift = (this.hash_bits + 3 - 1) / 3;
			this.window = new byte[this.w_size * 2];
			this.prev = new short[this.w_size];
			this.head = new short[this.hash_size];
			this.lit_bufsize = 1 << memLevel + 6;
			this.pending_buf = new byte[this.lit_bufsize * 4];
			this.d_buf = this.lit_bufsize;
			this.l_buf = 3 * this.lit_bufsize;
			this.level = level;
			this.strategy = strategy;
			this.method = (byte)method;
			return this.deflateReset(strm);
		}

		// Token: 0x06003DED RID: 15853 RVA: 0x00153A1C File Offset: 0x00153A1C
		internal int deflateReset(ZStream strm)
		{
			strm.total_in = (strm.total_out = 0L);
			strm.msg = null;
			strm.data_type = 2;
			this.pending = 0;
			this.pending_out = 0;
			if (this.noheader < 0)
			{
				this.noheader = 0;
			}
			this.status = ((this.noheader != 0) ? 113 : 42);
			strm.adler = strm._adler.adler32(0L, null, 0, 0);
			this.last_flush = 0;
			this.tr_init();
			this.lm_init();
			return 0;
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x00153AB4 File Offset: 0x00153AB4
		internal int deflateEnd()
		{
			if (this.status != 42 && this.status != 113 && this.status != 666)
			{
				return -2;
			}
			this.pending_buf = null;
			this.head = null;
			this.prev = null;
			this.window = null;
			if (this.status != 113)
			{
				return 0;
			}
			return -3;
		}

		// Token: 0x06003DEF RID: 15855 RVA: 0x00153B20 File Offset: 0x00153B20
		internal int deflateParams(ZStream strm, int _level, int _strategy)
		{
			int result = 0;
			if (_level == -1)
			{
				_level = 6;
			}
			if (_level < 0 || _level > 9 || _strategy < 0 || _strategy > 2)
			{
				return -2;
			}
			if (Deflate.config_table[this.level].func != Deflate.config_table[_level].func && strm.total_in != 0L)
			{
				result = strm.deflate(1);
			}
			if (this.level != _level)
			{
				this.level = _level;
				this.max_lazy_match = Deflate.config_table[this.level].max_lazy;
				this.good_match = Deflate.config_table[this.level].good_length;
				this.nice_match = Deflate.config_table[this.level].nice_length;
				this.max_chain_length = Deflate.config_table[this.level].max_chain;
			}
			this.strategy = _strategy;
			return result;
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x00153C24 File Offset: 0x00153C24
		internal int deflateSetDictionary(ZStream strm, byte[] dictionary, int dictLength)
		{
			int num = dictLength;
			int sourceIndex = 0;
			if (dictionary == null || this.status != 42)
			{
				return -2;
			}
			strm.adler = strm._adler.adler32(strm.adler, dictionary, 0, dictLength);
			if (num < 3)
			{
				return 0;
			}
			if (num > this.w_size - 262)
			{
				num = this.w_size - 262;
				sourceIndex = dictLength - num;
			}
			Array.Copy(dictionary, sourceIndex, this.window, 0, num);
			this.strstart = num;
			this.block_start = num;
			this.ins_h = (int)(this.window[0] & byte.MaxValue);
			this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[1] & byte.MaxValue)) & this.hash_mask);
			for (int i = 0; i <= num - 3; i++)
			{
				this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[i + 2] & byte.MaxValue)) & this.hash_mask);
				this.prev[i & this.w_mask] = this.head[this.ins_h];
				this.head[this.ins_h] = (short)i;
			}
			return 0;
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x00153D60 File Offset: 0x00153D60
		internal int deflate(ZStream strm, int flush)
		{
			if (flush > 4 || flush < 0)
			{
				return -2;
			}
			if (strm.next_out == null || (strm.next_in == null && strm.avail_in != 0) || (this.status == 666 && flush != 4))
			{
				strm.msg = Deflate.z_errmsg[4];
				return -2;
			}
			if (strm.avail_out == 0)
			{
				strm.msg = Deflate.z_errmsg[7];
				return -5;
			}
			this.strm = strm;
			int num = this.last_flush;
			this.last_flush = flush;
			if (this.status == 42)
			{
				int num2 = 8 + (this.w_bits - 8 << 4) << 8;
				int num3 = (this.level - 1 & 255) >> 1;
				if (num3 > 3)
				{
					num3 = 3;
				}
				num2 |= num3 << 6;
				if (this.strstart != 0)
				{
					num2 |= 32;
				}
				num2 += 31 - num2 % 31;
				this.status = 113;
				this.putShortMSB(num2);
				if (this.strstart != 0)
				{
					this.putShortMSB((int)(strm.adler >> 16));
					this.putShortMSB((int)(strm.adler & 65535L));
				}
				strm.adler = strm._adler.adler32(0L, null, 0, 0);
			}
			if (this.pending != 0)
			{
				strm.flush_pending();
				if (strm.avail_out == 0)
				{
					this.last_flush = -1;
					return 0;
				}
			}
			else if (strm.avail_in == 0 && flush <= num && flush != 4)
			{
				strm.msg = Deflate.z_errmsg[7];
				return -5;
			}
			if (this.status == 666 && strm.avail_in != 0)
			{
				strm.msg = Deflate.z_errmsg[7];
				return -5;
			}
			if (strm.avail_in != 0 || this.lookahead != 0 || (flush != 0 && this.status != 666))
			{
				int num4 = -1;
				switch (Deflate.config_table[this.level].func)
				{
				case 0:
					num4 = this.deflate_stored(flush);
					break;
				case 1:
					num4 = this.deflate_fast(flush);
					break;
				case 2:
					num4 = this.deflate_slow(flush);
					break;
				}
				if (num4 == 2 || num4 == 3)
				{
					this.status = 666;
				}
				if (num4 == 0 || num4 == 2)
				{
					if (strm.avail_out == 0)
					{
						this.last_flush = -1;
					}
					return 0;
				}
				if (num4 == 1)
				{
					if (flush == 1)
					{
						this._tr_align();
					}
					else
					{
						this._tr_stored_block(0, 0, false);
						if (flush == 3)
						{
							for (int i = 0; i < this.hash_size; i++)
							{
								this.head[i] = 0;
							}
						}
					}
					strm.flush_pending();
					if (strm.avail_out == 0)
					{
						this.last_flush = -1;
						return 0;
					}
				}
			}
			if (flush != 4)
			{
				return 0;
			}
			if (this.noheader != 0)
			{
				return 1;
			}
			this.putShortMSB((int)(strm.adler >> 16));
			this.putShortMSB((int)(strm.adler & 65535L));
			strm.flush_pending();
			this.noheader = -1;
			if (this.pending == 0)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x04001F07 RID: 7943
		private const int MAX_MEM_LEVEL = 9;

		// Token: 0x04001F08 RID: 7944
		private const int Z_DEFAULT_COMPRESSION = -1;

		// Token: 0x04001F09 RID: 7945
		private const int MAX_WBITS = 15;

		// Token: 0x04001F0A RID: 7946
		private const int DEF_MEM_LEVEL = 8;

		// Token: 0x04001F0B RID: 7947
		private const int STORED = 0;

		// Token: 0x04001F0C RID: 7948
		private const int FAST = 1;

		// Token: 0x04001F0D RID: 7949
		private const int SLOW = 2;

		// Token: 0x04001F0E RID: 7950
		private const int NeedMore = 0;

		// Token: 0x04001F0F RID: 7951
		private const int BlockDone = 1;

		// Token: 0x04001F10 RID: 7952
		private const int FinishStarted = 2;

		// Token: 0x04001F11 RID: 7953
		private const int FinishDone = 3;

		// Token: 0x04001F12 RID: 7954
		private const int PRESET_DICT = 32;

		// Token: 0x04001F13 RID: 7955
		private const int Z_FILTERED = 1;

		// Token: 0x04001F14 RID: 7956
		private const int Z_HUFFMAN_ONLY = 2;

		// Token: 0x04001F15 RID: 7957
		private const int Z_DEFAULT_STRATEGY = 0;

		// Token: 0x04001F16 RID: 7958
		private const int Z_NO_FLUSH = 0;

		// Token: 0x04001F17 RID: 7959
		private const int Z_PARTIAL_FLUSH = 1;

		// Token: 0x04001F18 RID: 7960
		private const int Z_SYNC_FLUSH = 2;

		// Token: 0x04001F19 RID: 7961
		private const int Z_FULL_FLUSH = 3;

		// Token: 0x04001F1A RID: 7962
		private const int Z_FINISH = 4;

		// Token: 0x04001F1B RID: 7963
		private const int Z_OK = 0;

		// Token: 0x04001F1C RID: 7964
		private const int Z_STREAM_END = 1;

		// Token: 0x04001F1D RID: 7965
		private const int Z_NEED_DICT = 2;

		// Token: 0x04001F1E RID: 7966
		private const int Z_ERRNO = -1;

		// Token: 0x04001F1F RID: 7967
		private const int Z_STREAM_ERROR = -2;

		// Token: 0x04001F20 RID: 7968
		private const int Z_DATA_ERROR = -3;

		// Token: 0x04001F21 RID: 7969
		private const int Z_MEM_ERROR = -4;

		// Token: 0x04001F22 RID: 7970
		private const int Z_BUF_ERROR = -5;

		// Token: 0x04001F23 RID: 7971
		private const int Z_VERSION_ERROR = -6;

		// Token: 0x04001F24 RID: 7972
		private const int INIT_STATE = 42;

		// Token: 0x04001F25 RID: 7973
		private const int BUSY_STATE = 113;

		// Token: 0x04001F26 RID: 7974
		private const int FINISH_STATE = 666;

		// Token: 0x04001F27 RID: 7975
		private const int Z_DEFLATED = 8;

		// Token: 0x04001F28 RID: 7976
		private const int STORED_BLOCK = 0;

		// Token: 0x04001F29 RID: 7977
		private const int STATIC_TREES = 1;

		// Token: 0x04001F2A RID: 7978
		private const int DYN_TREES = 2;

		// Token: 0x04001F2B RID: 7979
		private const int Z_BINARY = 0;

		// Token: 0x04001F2C RID: 7980
		private const int Z_ASCII = 1;

		// Token: 0x04001F2D RID: 7981
		private const int Z_UNKNOWN = 2;

		// Token: 0x04001F2E RID: 7982
		private const int Buf_size = 16;

		// Token: 0x04001F2F RID: 7983
		private const int REP_3_6 = 16;

		// Token: 0x04001F30 RID: 7984
		private const int REPZ_3_10 = 17;

		// Token: 0x04001F31 RID: 7985
		private const int REPZ_11_138 = 18;

		// Token: 0x04001F32 RID: 7986
		private const int MIN_MATCH = 3;

		// Token: 0x04001F33 RID: 7987
		private const int MAX_MATCH = 258;

		// Token: 0x04001F34 RID: 7988
		private const int MIN_LOOKAHEAD = 262;

		// Token: 0x04001F35 RID: 7989
		private const int MAX_BITS = 15;

		// Token: 0x04001F36 RID: 7990
		private const int D_CODES = 30;

		// Token: 0x04001F37 RID: 7991
		private const int BL_CODES = 19;

		// Token: 0x04001F38 RID: 7992
		private const int LENGTH_CODES = 29;

		// Token: 0x04001F39 RID: 7993
		private const int LITERALS = 256;

		// Token: 0x04001F3A RID: 7994
		private const int L_CODES = 286;

		// Token: 0x04001F3B RID: 7995
		private const int HEAP_SIZE = 573;

		// Token: 0x04001F3C RID: 7996
		private const int END_BLOCK = 256;

		// Token: 0x04001F3D RID: 7997
		private static readonly Deflate.Config[] config_table;

		// Token: 0x04001F3E RID: 7998
		private static readonly string[] z_errmsg = new string[]
		{
			"need dictionary",
			"stream end",
			"",
			"file error",
			"stream error",
			"data error",
			"insufficient memory",
			"buffer error",
			"incompatible version",
			""
		};

		// Token: 0x04001F3F RID: 7999
		internal ZStream strm;

		// Token: 0x04001F40 RID: 8000
		internal int status;

		// Token: 0x04001F41 RID: 8001
		internal byte[] pending_buf;

		// Token: 0x04001F42 RID: 8002
		internal int pending_out;

		// Token: 0x04001F43 RID: 8003
		internal int pending;

		// Token: 0x04001F44 RID: 8004
		internal int noheader;

		// Token: 0x04001F45 RID: 8005
		internal byte data_type;

		// Token: 0x04001F46 RID: 8006
		internal byte method;

		// Token: 0x04001F47 RID: 8007
		internal int last_flush;

		// Token: 0x04001F48 RID: 8008
		internal int w_size;

		// Token: 0x04001F49 RID: 8009
		internal int w_bits;

		// Token: 0x04001F4A RID: 8010
		internal int w_mask;

		// Token: 0x04001F4B RID: 8011
		internal byte[] window;

		// Token: 0x04001F4C RID: 8012
		internal int window_size;

		// Token: 0x04001F4D RID: 8013
		internal short[] prev;

		// Token: 0x04001F4E RID: 8014
		internal short[] head;

		// Token: 0x04001F4F RID: 8015
		internal int ins_h;

		// Token: 0x04001F50 RID: 8016
		internal int hash_size;

		// Token: 0x04001F51 RID: 8017
		internal int hash_bits;

		// Token: 0x04001F52 RID: 8018
		internal int hash_mask;

		// Token: 0x04001F53 RID: 8019
		internal int hash_shift;

		// Token: 0x04001F54 RID: 8020
		internal int block_start;

		// Token: 0x04001F55 RID: 8021
		internal int match_length;

		// Token: 0x04001F56 RID: 8022
		internal int prev_match;

		// Token: 0x04001F57 RID: 8023
		internal int match_available;

		// Token: 0x04001F58 RID: 8024
		internal int strstart;

		// Token: 0x04001F59 RID: 8025
		internal int match_start;

		// Token: 0x04001F5A RID: 8026
		internal int lookahead;

		// Token: 0x04001F5B RID: 8027
		internal int prev_length;

		// Token: 0x04001F5C RID: 8028
		internal int max_chain_length;

		// Token: 0x04001F5D RID: 8029
		internal int max_lazy_match;

		// Token: 0x04001F5E RID: 8030
		internal int level;

		// Token: 0x04001F5F RID: 8031
		internal int strategy;

		// Token: 0x04001F60 RID: 8032
		internal int good_match;

		// Token: 0x04001F61 RID: 8033
		internal int nice_match;

		// Token: 0x04001F62 RID: 8034
		internal short[] dyn_ltree;

		// Token: 0x04001F63 RID: 8035
		internal short[] dyn_dtree;

		// Token: 0x04001F64 RID: 8036
		internal short[] bl_tree;

		// Token: 0x04001F65 RID: 8037
		internal Tree l_desc = new Tree();

		// Token: 0x04001F66 RID: 8038
		internal Tree d_desc = new Tree();

		// Token: 0x04001F67 RID: 8039
		internal Tree bl_desc = new Tree();

		// Token: 0x04001F68 RID: 8040
		internal short[] bl_count = new short[16];

		// Token: 0x04001F69 RID: 8041
		internal int[] heap = new int[573];

		// Token: 0x04001F6A RID: 8042
		internal int heap_len;

		// Token: 0x04001F6B RID: 8043
		internal int heap_max;

		// Token: 0x04001F6C RID: 8044
		internal byte[] depth = new byte[573];

		// Token: 0x04001F6D RID: 8045
		internal int l_buf;

		// Token: 0x04001F6E RID: 8046
		internal int lit_bufsize;

		// Token: 0x04001F6F RID: 8047
		internal int last_lit;

		// Token: 0x04001F70 RID: 8048
		internal int d_buf;

		// Token: 0x04001F71 RID: 8049
		internal int opt_len;

		// Token: 0x04001F72 RID: 8050
		internal int static_len;

		// Token: 0x04001F73 RID: 8051
		internal int matches;

		// Token: 0x04001F74 RID: 8052
		internal int last_eob_len;

		// Token: 0x04001F75 RID: 8053
		internal uint bi_buf;

		// Token: 0x04001F76 RID: 8054
		internal int bi_valid;

		// Token: 0x02000E71 RID: 3697
		internal class Config
		{
			// Token: 0x06008D7A RID: 36218 RVA: 0x002A6AC0 File Offset: 0x002A6AC0
			internal Config(int good_length, int max_lazy, int nice_length, int max_chain, int func)
			{
				this.good_length = good_length;
				this.max_lazy = max_lazy;
				this.nice_length = nice_length;
				this.max_chain = max_chain;
				this.func = func;
			}

			// Token: 0x040042E9 RID: 17129
			internal int good_length;

			// Token: 0x040042EA RID: 17130
			internal int max_lazy;

			// Token: 0x040042EB RID: 17131
			internal int nice_length;

			// Token: 0x040042EC RID: 17132
			internal int max_chain;

			// Token: 0x040042ED RID: 17133
			internal int func;
		}
	}
}
