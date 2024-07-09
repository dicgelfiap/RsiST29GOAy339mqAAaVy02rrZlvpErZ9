using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using IP2Region.Models;

namespace IP2Region
{
	// Token: 0x02000A59 RID: 2649
	[ComVisible(true)]
	public class DbSearcher : IDisposable
	{
		// Token: 0x060067ED RID: 26605 RVA: 0x001FA2C8 File Offset: 0x001FA2C8
		private DataBlock GetByIndexPtr(long ptr)
		{
			this._raf.Seek(ptr, SeekOrigin.Begin);
			byte[] array = new byte[12];
			this._raf.Read(array, 0, array.Length);
			long intLong = Utils.GetIntLong(array, 8);
			int num = (int)(intLong >> 24 & 255L);
			int num2 = (int)(intLong & 16777215L);
			this._raf.Seek((long)num2, SeekOrigin.Begin);
			byte[] array2 = new byte[num];
			this._raf.Read(array2, 0, array2.Length);
			int city_id = (int)Utils.GetIntLong(array2, 0);
			string @string = Encoding.UTF8.GetString(array2, 4, array2.Length - 4);
			return new DataBlock(city_id, @string, num2);
		}

		// Token: 0x060067EE RID: 26606 RVA: 0x001FA368 File Offset: 0x001FA368
		public DbSearcher(DbConfig dbConfig, string dbFile)
		{
			if (this._dbConfig == null)
			{
				this._dbConfig = dbConfig;
			}
			this._raf = new FileStream(dbFile, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		// Token: 0x060067EF RID: 26607 RVA: 0x001FA394 File Offset: 0x001FA394
		public DbSearcher(string dbFile) : this(null, dbFile)
		{
		}

		// Token: 0x060067F0 RID: 26608 RVA: 0x001FA3A0 File Offset: 0x001FA3A0
		private DataBlock MemorySearch(long ip)
		{
			int num = 12;
			if (this._dbBinStr == null)
			{
				this._dbBinStr = new byte[(int)this._raf.Length];
				this._raf.Seek(0L, SeekOrigin.Begin);
				this._raf.Read(this._dbBinStr, 0, this._dbBinStr.Length);
				this._firstIndexPtr = Utils.GetIntLong(this._dbBinStr, 0);
				this._lastIndexPtr = Utils.GetIntLong(this._dbBinStr, 4);
				this._totalIndexBlocks = (int)((this._lastIndexPtr - this._firstIndexPtr) / (long)num) + 1;
			}
			int i = 0;
			int num2 = this._totalIndexBlocks;
			long num3 = 0L;
			while (i <= num2)
			{
				int num4 = i + num2 >> 1;
				int num5 = (int)(this._firstIndexPtr + (long)(num4 * num));
				num3 = Utils.GetIntLong(this._dbBinStr, num5);
				if (ip < num3)
				{
					num2 = num4 - 1;
				}
				else
				{
					num3 = Utils.GetIntLong(this._dbBinStr, num5 + 4);
					if (ip <= num3)
					{
						num3 = Utils.GetIntLong(this._dbBinStr, num5 + 8);
						break;
					}
					i = num4 + 1;
				}
			}
			if (num3 == 0L)
			{
				return null;
			}
			int num6 = (int)(num3 >> 24 & 255L);
			int num7 = (int)(num3 & 16777215L);
			int city_id = (int)Utils.GetIntLong(this._dbBinStr, num7);
			string @string = Encoding.UTF8.GetString(this._dbBinStr, num7 + 4, num6 - 4);
			return new DataBlock(city_id, @string, num7);
		}

		// Token: 0x060067F1 RID: 26609 RVA: 0x001FA510 File Offset: 0x001FA510
		public DataBlock MemorySearch(string ip)
		{
			return this.MemorySearch(Utils.Ip2long(ip));
		}

		// Token: 0x060067F2 RID: 26610 RVA: 0x001FA520 File Offset: 0x001FA520
		private DataBlock BtreeSearch(long ip)
		{
			if (this._headerSip == null)
			{
				this._raf.Seek(8L, SeekOrigin.Begin);
				byte[] array = new byte[4096];
				this._raf.Read(array, 0, array.Length);
				int num = array.Length >> 3;
				int num2 = 0;
				this._headerSip = new long[num];
				this._headerPtr = new int[num];
				for (int i = 0; i < array.Length; i += 8)
				{
					long intLong = Utils.GetIntLong(array, i);
					long intLong2 = Utils.GetIntLong(array, i + 4);
					if (intLong2 == 0L)
					{
						break;
					}
					this._headerSip[num2] = intLong;
					this._headerPtr[num2] = (int)intLong2;
					num2++;
				}
				this._headerLength = num2;
			}
			if (ip == this._headerSip[0])
			{
				return this.GetByIndexPtr((long)this._headerPtr[0]);
			}
			if (ip == (long)this._headerPtr[this._headerLength - 1])
			{
				return this.GetByIndexPtr((long)this._headerPtr[this._headerLength - 1]);
			}
			int j = 0;
			int num3 = this._headerLength;
			int num4 = 0;
			int num5 = 0;
			while (j <= num3)
			{
				int num6 = j + num3 >> 1;
				if (ip == this._headerSip[num6])
				{
					if (num6 > 0)
					{
						num4 = this._headerPtr[num6 - 1];
						num5 = this._headerPtr[num6];
					}
					else
					{
						num4 = this._headerPtr[num6];
						num5 = this._headerPtr[num6 + 1];
					}
				}
				else if (ip < this._headerSip[num6])
				{
					if (num6 == 0)
					{
						num4 = this._headerPtr[num6];
						num5 = this._headerPtr[num6 + 1];
						break;
					}
					if (ip > this._headerSip[num6 - 1])
					{
						num4 = this._headerPtr[num6 - 1];
						num5 = this._headerPtr[num6];
						break;
					}
					num3 = num6 - 1;
				}
				else
				{
					if (num6 == this._headerLength - 1)
					{
						num4 = this._headerPtr[num6 - 1];
						num5 = this._headerPtr[num6];
						break;
					}
					if (ip <= this._headerSip[num6 + 1])
					{
						num4 = this._headerPtr[num6];
						num5 = this._headerPtr[num6 + 1];
						break;
					}
					j = num6 + 1;
				}
			}
			if (num4 == 0)
			{
				return null;
			}
			int num7 = num5 - num4;
			int num8 = 12;
			byte[] array2 = new byte[num7 + num8];
			this._raf.Seek((long)num4, SeekOrigin.Begin);
			this._raf.Read(array2, 0, array2.Length);
			j = 0;
			num3 = num7 / num8;
			long num9 = 0L;
			while (j <= num3)
			{
				int num6 = j + num3 >> 1;
				int num10 = num6 * num8;
				num9 = Utils.GetIntLong(array2, num10);
				if (ip < num9)
				{
					num3 = num6 - 1;
				}
				else
				{
					num9 = Utils.GetIntLong(array2, num10 + 4);
					if (ip <= num9)
					{
						num9 = Utils.GetIntLong(array2, num10 + 8);
						break;
					}
					j = num6 + 1;
				}
			}
			if (num9 == 0L)
			{
				return null;
			}
			int num11 = (int)(num9 >> 24 & 255L);
			int num12 = (int)(num9 & 16777215L);
			this._raf.Seek((long)num12, SeekOrigin.Begin);
			byte[] array3 = new byte[num11];
			this._raf.Read(array3, 0, array3.Length);
			int city_id = (int)Utils.GetIntLong(array3, 0);
			string @string = Encoding.UTF8.GetString(array3, 4, array3.Length - 4);
			return new DataBlock(city_id, @string, num12);
		}

		// Token: 0x060067F3 RID: 26611 RVA: 0x001FA894 File Offset: 0x001FA894
		public DataBlock BtreeSearch(string ip)
		{
			return this.BtreeSearch(Utils.Ip2long(ip));
		}

		// Token: 0x060067F4 RID: 26612 RVA: 0x001FA8A4 File Offset: 0x001FA8A4
		private DataBlock BinarySearch(long ip)
		{
			int num = 12;
			if (this._totalIndexBlocks == 0)
			{
				this._raf.Seek(0L, SeekOrigin.Begin);
				byte[] array = new byte[8];
				this._raf.Read(array, 0, array.Length);
				this._firstIndexPtr = Utils.GetIntLong(array, 0);
				this._lastIndexPtr = Utils.GetIntLong(array, 4);
				this._totalIndexBlocks = (int)((this._lastIndexPtr - this._firstIndexPtr) / (long)num) + 1;
			}
			int i = 0;
			int num2 = this._totalIndexBlocks;
			byte[] array2 = new byte[num];
			long num3 = 0L;
			while (i <= num2)
			{
				int num4 = i + num2 >> 1;
				this._raf.Seek(this._firstIndexPtr + (long)(num4 * num), SeekOrigin.Begin);
				this._raf.Read(array2, 0, array2.Length);
				num3 = Utils.GetIntLong(array2, 0);
				if (ip < num3)
				{
					num2 = num4 - 1;
				}
				else
				{
					num3 = Utils.GetIntLong(array2, 4);
					if (ip <= num3)
					{
						num3 = Utils.GetIntLong(array2, 8);
						break;
					}
					i = num4 + 1;
				}
			}
			if (num3 == 0L)
			{
				return null;
			}
			int num5 = (int)(num3 >> 24 & 255L);
			int num6 = (int)(num3 & 16777215L);
			this._raf.Seek((long)num6, SeekOrigin.Begin);
			byte[] array3 = new byte[num5];
			this._raf.Read(array3, 0, array3.Length);
			int city_id = (int)Utils.GetIntLong(array3, 0);
			string @string = Encoding.UTF8.GetString(array3, 4, array3.Length - 4);
			return new DataBlock(city_id, @string, num6);
		}

		// Token: 0x060067F5 RID: 26613 RVA: 0x001FAA28 File Offset: 0x001FAA28
		public DataBlock BinarySearch(string ip)
		{
			return this.BinarySearch(Utils.Ip2long(ip));
		}

		// Token: 0x060067F6 RID: 26614 RVA: 0x001FAA38 File Offset: 0x001FAA38
		public async Task<DataBlock> MemorySearchAsync(string ip)
		{
			return await Task.FromResult<DataBlock>(this.MemorySearch(ip));
		}

		// Token: 0x060067F7 RID: 26615 RVA: 0x001FAA8C File Offset: 0x001FAA8C
		public async Task<DataBlock> BtreeSearchAsync(string ip)
		{
			return await Task.FromResult<DataBlock>(this.BtreeSearch(ip));
		}

		// Token: 0x060067F8 RID: 26616 RVA: 0x001FAAE0 File Offset: 0x001FAAE0
		public async Task<DataBlock> BinarySearchAsync(string ip)
		{
			return await Task.FromResult<DataBlock>(this.BinarySearch(ip));
		}

		// Token: 0x060067F9 RID: 26617 RVA: 0x001FAB34 File Offset: 0x001FAB34
		public void Close()
		{
			this._headerSip = null;
			this._headerPtr = null;
			this._dbBinStr = null;
			this._raf.Close();
		}

		// Token: 0x060067FA RID: 26618 RVA: 0x001FAB58 File Offset: 0x001FAB58
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x040034E7 RID: 13543
		private const int BTREE_ALGORITHM = 1;

		// Token: 0x040034E8 RID: 13544
		private const int BINARY_ALGORITHM = 2;

		// Token: 0x040034E9 RID: 13545
		private const int MEMORY_ALGORITYM = 3;

		// Token: 0x040034EA RID: 13546
		private DbConfig _dbConfig;

		// Token: 0x040034EB RID: 13547
		private FileStream _raf;

		// Token: 0x040034EC RID: 13548
		private long[] _headerSip;

		// Token: 0x040034ED RID: 13549
		private int[] _headerPtr;

		// Token: 0x040034EE RID: 13550
		private int _headerLength;

		// Token: 0x040034EF RID: 13551
		private long _firstIndexPtr;

		// Token: 0x040034F0 RID: 13552
		private long _lastIndexPtr;

		// Token: 0x040034F1 RID: 13553
		private int _totalIndexBlocks;

		// Token: 0x040034F2 RID: 13554
		private byte[] _dbBinStr;
	}
}
