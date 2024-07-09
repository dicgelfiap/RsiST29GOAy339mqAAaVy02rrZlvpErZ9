using System;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008AB RID: 2219
	[ComVisible(true)]
	public interface IMDTable
	{
		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x060054DA RID: 21722
		Table Table { get; }

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x060054DB RID: 21723
		bool IsEmpty { get; }

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x060054DC RID: 21724
		int Rows { get; }

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x060054DD RID: 21725
		// (set) Token: 0x060054DE RID: 21726
		bool IsSorted { get; set; }

		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x060054DF RID: 21727
		bool IsReadOnly { get; }

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x060054E0 RID: 21728
		// (set) Token: 0x060054E1 RID: 21729
		TableInfo TableInfo { get; set; }

		// Token: 0x060054E2 RID: 21730
		void SetReadOnly();
	}
}
