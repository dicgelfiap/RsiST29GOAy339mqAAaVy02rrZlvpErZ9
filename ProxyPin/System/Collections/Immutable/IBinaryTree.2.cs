using System;

namespace System.Collections.Immutable
{
	// Token: 0x02000C98 RID: 3224
	internal interface IBinaryTree<out T> : IBinaryTree
	{
		// Token: 0x17001BED RID: 7149
		// (get) Token: 0x060080D5 RID: 32981
		T Value { get; }

		// Token: 0x17001BEE RID: 7150
		// (get) Token: 0x060080D6 RID: 32982
		IBinaryTree<T> Left { get; }

		// Token: 0x17001BEF RID: 7151
		// (get) Token: 0x060080D7 RID: 32983
		IBinaryTree<T> Right { get; }
	}
}
