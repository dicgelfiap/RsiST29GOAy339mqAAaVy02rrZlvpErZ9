using System;
using System.Threading;

namespace System.Collections.Immutable
{
	// Token: 0x02000CC1 RID: 3265
	internal class SecureObjectPool
	{
		// Token: 0x060083D7 RID: 33751 RVA: 0x002685B8 File Offset: 0x002685B8
		internal static int NewId()
		{
			int num;
			do
			{
				num = Interlocked.Increment(ref SecureObjectPool.s_poolUserIdCounter);
			}
			while (num == -1);
			return num;
		}

		// Token: 0x04003D51 RID: 15697
		private static int s_poolUserIdCounter;

		// Token: 0x04003D52 RID: 15698
		internal const int UnassignedId = -1;
	}
}
