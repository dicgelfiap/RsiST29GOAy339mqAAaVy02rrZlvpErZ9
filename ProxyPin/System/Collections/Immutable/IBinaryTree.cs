using System;

namespace System.Collections.Immutable
{
	// Token: 0x02000C97 RID: 3223
	internal interface IBinaryTree
	{
		// Token: 0x17001BE8 RID: 7144
		// (get) Token: 0x060080D0 RID: 32976
		int Height { get; }

		// Token: 0x17001BE9 RID: 7145
		// (get) Token: 0x060080D1 RID: 32977
		bool IsEmpty { get; }

		// Token: 0x17001BEA RID: 7146
		// (get) Token: 0x060080D2 RID: 32978
		int Count { get; }

		// Token: 0x17001BEB RID: 7147
		// (get) Token: 0x060080D3 RID: 32979
		IBinaryTree Left { get; }

		// Token: 0x17001BEC RID: 7148
		// (get) Token: 0x060080D4 RID: 32980
		IBinaryTree Right { get; }
	}
}
