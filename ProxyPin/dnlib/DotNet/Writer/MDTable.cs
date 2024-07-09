using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008AC RID: 2220
	[ComVisible(true)]
	public sealed class MDTable<TRow> : IMDTable where TRow : struct
	{
		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x060054E3 RID: 21731 RVA: 0x0019D968 File Offset: 0x0019D968
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x060054E4 RID: 21732 RVA: 0x0019D970 File Offset: 0x0019D970
		public bool IsEmpty
		{
			get
			{
				return this.cached.Count == 0;
			}
		}

		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x060054E5 RID: 21733 RVA: 0x0019D980 File Offset: 0x0019D980
		public int Rows
		{
			get
			{
				return this.cached.Count;
			}
		}

		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x060054E6 RID: 21734 RVA: 0x0019D990 File Offset: 0x0019D990
		// (set) Token: 0x060054E7 RID: 21735 RVA: 0x0019D998 File Offset: 0x0019D998
		public bool IsSorted
		{
			get
			{
				return this.isSorted;
			}
			set
			{
				this.isSorted = value;
			}
		}

		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x060054E8 RID: 21736 RVA: 0x0019D9A4 File Offset: 0x0019D9A4
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x060054E9 RID: 21737 RVA: 0x0019D9AC File Offset: 0x0019D9AC
		// (set) Token: 0x060054EA RID: 21738 RVA: 0x0019D9B4 File Offset: 0x0019D9B4
		public TableInfo TableInfo
		{
			get
			{
				return this.tableInfo;
			}
			set
			{
				this.tableInfo = value;
			}
		}

		// Token: 0x1700118B RID: 4491
		public TRow this[uint rid]
		{
			get
			{
				return this.cached[(int)(rid - 1U)];
			}
			set
			{
				this.cached[(int)(rid - 1U)] = value;
			}
		}

		// Token: 0x060054ED RID: 21741 RVA: 0x0019D9E4 File Offset: 0x0019D9E4
		public MDTable(Table table, IEqualityComparer<TRow> equalityComparer)
		{
			this.table = table;
			this.cachedDict = new Dictionary<TRow, uint>(equalityComparer);
			this.cached = new List<TRow>();
		}

		// Token: 0x060054EE RID: 21742 RVA: 0x0019DA0C File Offset: 0x0019DA0C
		public void SetReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x060054EF RID: 21743 RVA: 0x0019DA18 File Offset: 0x0019DA18
		public uint Add(TRow row)
		{
			if (this.isReadOnly)
			{
				throw new ModuleWriterException(string.Format("Trying to modify table {0} after it's been set to read-only", this.table));
			}
			uint result;
			if (this.cachedDict.TryGetValue(row, out result))
			{
				return result;
			}
			return this.Create(row);
		}

		// Token: 0x060054F0 RID: 21744 RVA: 0x0019DA6C File Offset: 0x0019DA6C
		public uint Create(TRow row)
		{
			if (this.isReadOnly)
			{
				throw new ModuleWriterException(string.Format("Trying to modify table {0} after it's been set to read-only", this.table));
			}
			uint num = (uint)(this.cached.Count + 1);
			if (!this.cachedDict.ContainsKey(row))
			{
				this.cachedDict[row] = num;
			}
			this.cached.Add(row);
			return num;
		}

		// Token: 0x060054F1 RID: 21745 RVA: 0x0019DADC File Offset: 0x0019DADC
		public void ReAddRows()
		{
			if (this.isReadOnly)
			{
				throw new ModuleWriterException(string.Format("Trying to modify table {0} after it's been set to read-only", this.table));
			}
			this.cachedDict.Clear();
			for (int i = 0; i < this.cached.Count; i++)
			{
				uint value = (uint)(i + 1);
				TRow key = this.cached[i];
				if (!this.cachedDict.ContainsKey(key))
				{
					this.cachedDict[key] = value;
				}
			}
		}

		// Token: 0x060054F2 RID: 21746 RVA: 0x0019DB68 File Offset: 0x0019DB68
		public void Reset()
		{
			if (this.isReadOnly)
			{
				throw new ModuleWriterException(string.Format("Trying to modify table {0} after it's been set to read-only", this.table));
			}
			this.cachedDict.Clear();
			this.cached.Clear();
		}

		// Token: 0x040028C0 RID: 10432
		private readonly Table table;

		// Token: 0x040028C1 RID: 10433
		private readonly Dictionary<TRow, uint> cachedDict;

		// Token: 0x040028C2 RID: 10434
		private readonly List<TRow> cached;

		// Token: 0x040028C3 RID: 10435
		private TableInfo tableInfo;

		// Token: 0x040028C4 RID: 10436
		private bool isSorted;

		// Token: 0x040028C5 RID: 10437
		private bool isReadOnly;
	}
}
