using System;

namespace System
{
	// Token: 0x0200072E RID: 1838
	internal static class Array2
	{
		// Token: 0x06004094 RID: 16532 RVA: 0x00161438 File Offset: 0x00161438
		public static T[] Empty<T>()
		{
			return Array2.EmptyClass<T>.Empty;
		}

		// Token: 0x02000FBC RID: 4028
		private static class EmptyClass<T>
		{
			// Token: 0x040042EE RID: 17134
			public static readonly T[] Empty = new T[0];
		}
	}
}
