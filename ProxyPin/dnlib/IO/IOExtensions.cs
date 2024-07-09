using System;
using System.Runtime.InteropServices;

namespace dnlib.IO
{
	// Token: 0x0200076C RID: 1900
	[ComVisible(true)]
	public static class IOExtensions
	{
		// Token: 0x060042A6 RID: 17062 RVA: 0x00165D5C File Offset: 0x00165D5C
		public static FileOffset AlignUp(this FileOffset offset, uint alignment)
		{
			return (FileOffset)(offset + alignment - (FileOffset)1U & ~(FileOffset)(alignment - 1U));
		}

		// Token: 0x060042A7 RID: 17063 RVA: 0x00165D68 File Offset: 0x00165D68
		public static FileOffset AlignUp(this FileOffset offset, int alignment)
		{
			return (FileOffset)((ulong)offset + (ulong)((long)alignment) - 1UL & (ulong)((long)(~(long)(alignment - 1))));
		}
	}
}
