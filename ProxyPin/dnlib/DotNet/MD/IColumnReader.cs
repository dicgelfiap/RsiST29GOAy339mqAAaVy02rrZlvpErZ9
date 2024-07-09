using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x02000990 RID: 2448
	[ComVisible(true)]
	public interface IColumnReader
	{
		// Token: 0x06005E3C RID: 24124
		bool ReadColumn(MDTable table, uint rid, ColumnInfo column, out uint value);
	}
}
