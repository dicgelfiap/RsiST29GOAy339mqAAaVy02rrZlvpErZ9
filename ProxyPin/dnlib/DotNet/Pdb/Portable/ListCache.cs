using System;
using System.Collections.Generic;
using System.Threading;

namespace dnlib.DotNet.Pdb.Portable
{
	// Token: 0x0200093B RID: 2363
	internal static class ListCache<T>
	{
		// Token: 0x06005AE1 RID: 23265 RVA: 0x001BA920 File Offset: 0x001BA920
		public static List<T> AllocList()
		{
			return Interlocked.Exchange<List<T>>(ref ListCache<T>.cachedList, null) ?? new List<T>();
		}

		// Token: 0x06005AE2 RID: 23266 RVA: 0x001BA93C File Offset: 0x001BA93C
		public static void Free(ref List<T> list)
		{
			list.Clear();
			ListCache<T>.cachedList = list;
		}

		// Token: 0x06005AE3 RID: 23267 RVA: 0x001BA950 File Offset: 0x001BA950
		public static T[] FreeAndToArray(ref List<T> list)
		{
			T[] result = list.ToArray();
			ListCache<T>.Free(ref list);
			return result;
		}

		// Token: 0x04002BED RID: 11245
		private static volatile List<T> cachedList;
	}
}
