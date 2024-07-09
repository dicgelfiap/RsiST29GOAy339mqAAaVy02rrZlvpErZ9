using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x02000991 RID: 2449
	[ComVisible(true)]
	public interface IRowReader<TRow> where TRow : struct
	{
		// Token: 0x06005E3D RID: 24125
		bool TryReadRow(uint rid, out TRow row);
	}
}
