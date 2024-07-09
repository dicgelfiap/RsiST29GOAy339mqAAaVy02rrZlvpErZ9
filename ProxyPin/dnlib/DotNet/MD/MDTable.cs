using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.DotNet.MD
{
	// Token: 0x02000994 RID: 2452
	[DebuggerDisplay("DL:{dataReader.Length} R:{numRows} RS:{tableInfo.RowSize} C:{Count} {tableInfo.Name}")]
	[ComVisible(true)]
	public sealed class MDTable : IDisposable, IFileSection
	{
		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x06005E3E RID: 24126 RVA: 0x001C5690 File Offset: 0x001C5690
		private int Count
		{
			get
			{
				return this.tableInfo.Columns.Length;
			}
		}

		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x06005E3F RID: 24127 RVA: 0x001C56A0 File Offset: 0x001C56A0
		public FileOffset StartOffset
		{
			get
			{
				return (FileOffset)this.dataReader.StartOffset;
			}
		}

		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x06005E40 RID: 24128 RVA: 0x001C56B0 File Offset: 0x001C56B0
		public FileOffset EndOffset
		{
			get
			{
				return (FileOffset)this.dataReader.EndOffset;
			}
		}

		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x06005E41 RID: 24129 RVA: 0x001C56C0 File Offset: 0x001C56C0
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x06005E42 RID: 24130 RVA: 0x001C56C8 File Offset: 0x001C56C8
		public string Name
		{
			get
			{
				return this.tableInfo.Name;
			}
		}

		// Token: 0x170013AE RID: 5038
		// (get) Token: 0x06005E43 RID: 24131 RVA: 0x001C56D8 File Offset: 0x001C56D8
		public uint Rows
		{
			get
			{
				return this.numRows;
			}
		}

		// Token: 0x170013AF RID: 5039
		// (get) Token: 0x06005E44 RID: 24132 RVA: 0x001C56E0 File Offset: 0x001C56E0
		public uint RowSize
		{
			get
			{
				return (uint)this.tableInfo.RowSize;
			}
		}

		// Token: 0x170013B0 RID: 5040
		// (get) Token: 0x06005E45 RID: 24133 RVA: 0x001C56F0 File Offset: 0x001C56F0
		public IList<ColumnInfo> Columns
		{
			get
			{
				return this.tableInfo.Columns;
			}
		}

		// Token: 0x170013B1 RID: 5041
		// (get) Token: 0x06005E46 RID: 24134 RVA: 0x001C5700 File Offset: 0x001C5700
		public bool IsEmpty
		{
			get
			{
				return this.numRows == 0U;
			}
		}

		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x06005E47 RID: 24135 RVA: 0x001C570C File Offset: 0x001C570C
		public TableInfo TableInfo
		{
			get
			{
				return this.tableInfo;
			}
		}

		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x06005E48 RID: 24136 RVA: 0x001C5714 File Offset: 0x001C5714
		// (set) Token: 0x06005E49 RID: 24137 RVA: 0x001C571C File Offset: 0x001C571C
		internal DataReader DataReader
		{
			get
			{
				return this.dataReader;
			}
			set
			{
				this.dataReader = value;
			}
		}

		// Token: 0x06005E4A RID: 24138 RVA: 0x001C5728 File Offset: 0x001C5728
		internal MDTable(Table table, uint numRows, TableInfo tableInfo)
		{
			this.table = table;
			this.numRows = numRows;
			this.tableInfo = tableInfo;
			ColumnInfo[] columns = tableInfo.Columns;
			int num = columns.Length;
			if (num > 0)
			{
				this.Column0 = columns[0];
			}
			if (num > 1)
			{
				this.Column1 = columns[1];
			}
			if (num > 2)
			{
				this.Column2 = columns[2];
			}
			if (num > 3)
			{
				this.Column3 = columns[3];
			}
			if (num > 4)
			{
				this.Column4 = columns[4];
			}
			if (num > 5)
			{
				this.Column5 = columns[5];
			}
			if (num > 6)
			{
				this.Column6 = columns[6];
			}
			if (num > 7)
			{
				this.Column7 = columns[7];
			}
			if (num > 8)
			{
				this.Column8 = columns[8];
			}
		}

		// Token: 0x06005E4B RID: 24139 RVA: 0x001C5814 File Offset: 0x001C5814
		public bool IsValidRID(uint rid)
		{
			return rid != 0U && rid <= this.numRows;
		}

		// Token: 0x06005E4C RID: 24140 RVA: 0x001C582C File Offset: 0x001C582C
		public bool IsInvalidRID(uint rid)
		{
			return rid == 0U || rid > this.numRows;
		}

		// Token: 0x06005E4D RID: 24141 RVA: 0x001C5840 File Offset: 0x001C5840
		public void Dispose()
		{
			this.numRows = 0U;
			this.tableInfo = null;
			this.dataReader = default(DataReader);
		}

		// Token: 0x04002E23 RID: 11811
		private readonly Table table;

		// Token: 0x04002E24 RID: 11812
		private uint numRows;

		// Token: 0x04002E25 RID: 11813
		private TableInfo tableInfo;

		// Token: 0x04002E26 RID: 11814
		private DataReader dataReader;

		// Token: 0x04002E27 RID: 11815
		internal readonly ColumnInfo Column0;

		// Token: 0x04002E28 RID: 11816
		internal readonly ColumnInfo Column1;

		// Token: 0x04002E29 RID: 11817
		internal readonly ColumnInfo Column2;

		// Token: 0x04002E2A RID: 11818
		internal readonly ColumnInfo Column3;

		// Token: 0x04002E2B RID: 11819
		internal readonly ColumnInfo Column4;

		// Token: 0x04002E2C RID: 11820
		internal readonly ColumnInfo Column5;

		// Token: 0x04002E2D RID: 11821
		internal readonly ColumnInfo Column6;

		// Token: 0x04002E2E RID: 11822
		internal readonly ColumnInfo Column7;

		// Token: 0x04002E2F RID: 11823
		internal readonly ColumnInfo Column8;
	}
}
