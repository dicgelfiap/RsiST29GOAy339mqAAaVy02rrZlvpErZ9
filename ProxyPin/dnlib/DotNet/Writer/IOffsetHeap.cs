using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008A6 RID: 2214
	[ComVisible(true)]
	public interface IOffsetHeap<TValue>
	{
		// Token: 0x060054B4 RID: 21684
		int GetRawDataSize(TValue data);

		// Token: 0x060054B5 RID: 21685
		void SetRawData(uint offset, byte[] rawData);

		// Token: 0x060054B6 RID: 21686
		IEnumerable<KeyValuePair<uint, byte[]>> GetAllRawData();
	}
}
