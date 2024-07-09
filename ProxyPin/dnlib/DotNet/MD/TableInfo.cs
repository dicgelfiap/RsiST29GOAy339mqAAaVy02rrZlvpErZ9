using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009D5 RID: 2517
	[DebuggerDisplay("{rowSize} {name}")]
	[ComVisible(true)]
	public sealed class TableInfo
	{
		// Token: 0x17001418 RID: 5144
		// (get) Token: 0x06005FCE RID: 24526 RVA: 0x001C9F9C File Offset: 0x001C9F9C
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x17001419 RID: 5145
		// (get) Token: 0x06005FCF RID: 24527 RVA: 0x001C9FA4 File Offset: 0x001C9FA4
		// (set) Token: 0x06005FD0 RID: 24528 RVA: 0x001C9FAC File Offset: 0x001C9FAC
		public int RowSize
		{
			get
			{
				return this.rowSize;
			}
			internal set
			{
				this.rowSize = value;
			}
		}

		// Token: 0x1700141A RID: 5146
		// (get) Token: 0x06005FD1 RID: 24529 RVA: 0x001C9FB8 File Offset: 0x001C9FB8
		public ColumnInfo[] Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x1700141B RID: 5147
		// (get) Token: 0x06005FD2 RID: 24530 RVA: 0x001C9FC0 File Offset: 0x001C9FC0
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06005FD3 RID: 24531 RVA: 0x001C9FC8 File Offset: 0x001C9FC8
		public TableInfo(Table table, string name, ColumnInfo[] columns)
		{
			this.table = table;
			this.name = name;
			this.columns = columns;
		}

		// Token: 0x06005FD4 RID: 24532 RVA: 0x001C9FE8 File Offset: 0x001C9FE8
		public TableInfo(Table table, string name, ColumnInfo[] columns, int rowSize)
		{
			this.table = table;
			this.name = name;
			this.columns = columns;
			this.rowSize = rowSize;
		}

		// Token: 0x04002F33 RID: 12083
		private readonly Table table;

		// Token: 0x04002F34 RID: 12084
		private int rowSize;

		// Token: 0x04002F35 RID: 12085
		private readonly ColumnInfo[] columns;

		// Token: 0x04002F36 RID: 12086
		private readonly string name;
	}
}
