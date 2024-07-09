using System;

namespace System
{
	// Token: 0x02000CDE RID: 3294
	internal static class NotImplemented
	{
		// Token: 0x17001C87 RID: 7303
		// (get) Token: 0x06008550 RID: 34128 RVA: 0x0027152C File Offset: 0x0027152C
		internal static Exception ByDesign
		{
			get
			{
				return new NotImplementedException();
			}
		}

		// Token: 0x06008551 RID: 34129 RVA: 0x00271534 File Offset: 0x00271534
		internal static Exception ByDesignWithMessage(string message)
		{
			return new NotImplementedException(message);
		}

		// Token: 0x06008552 RID: 34130 RVA: 0x0027153C File Offset: 0x0027153C
		internal static Exception ActiveIssue(string issue)
		{
			return new NotImplementedException();
		}
	}
}
