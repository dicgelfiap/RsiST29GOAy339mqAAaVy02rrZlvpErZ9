using System;
using System.Runtime.InteropServices;

namespace dnlib.Utils
{
	// Token: 0x0200073E RID: 1854
	[ComVisible(true)]
	public interface IListListener<TListValue>
	{
		// Token: 0x060040E6 RID: 16614
		void OnLazyAdd(int index, ref TListValue value);

		// Token: 0x060040E7 RID: 16615
		void OnAdd(int index, TListValue value);

		// Token: 0x060040E8 RID: 16616
		void OnRemove(int index, TListValue value);

		// Token: 0x060040E9 RID: 16617
		void OnResize(int index);

		// Token: 0x060040EA RID: 16618
		void OnClear();
	}
}
