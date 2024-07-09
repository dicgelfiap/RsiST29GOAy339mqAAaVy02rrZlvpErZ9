using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000849 RID: 2121
	[ComVisible(true)]
	public sealed class EventEqualityComparer : IEqualityComparer<EventDef>
	{
		// Token: 0x06004F2F RID: 20271 RVA: 0x00187F7C File Offset: 0x00187F7C
		public EventEqualityComparer(SigComparerOptions options)
		{
			this.options = options;
		}

		// Token: 0x06004F30 RID: 20272 RVA: 0x00187F8C File Offset: 0x00187F8C
		public bool Equals(EventDef x, EventDef y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F31 RID: 20273 RVA: 0x00187FB4 File Offset: 0x00187FB4
		public int GetHashCode(EventDef obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x04002715 RID: 10005
		private readonly SigComparerOptions options;

		// Token: 0x04002716 RID: 10006
		public static readonly EventEqualityComparer CompareDeclaringTypes = new EventEqualityComparer(SigComparerOptions.CompareEventDeclaringType);

		// Token: 0x04002717 RID: 10007
		public static readonly EventEqualityComparer DontCompareDeclaringTypes = new EventEqualityComparer((SigComparerOptions)0U);

		// Token: 0x04002718 RID: 10008
		public static readonly EventEqualityComparer CaseInsensitiveCompareDeclaringTypes = new EventEqualityComparer(SigComparerOptions.CompareEventDeclaringType | SigComparerOptions.CaseInsensitiveTypeNamespaces | SigComparerOptions.CaseInsensitiveTypeNames | SigComparerOptions.CaseInsensitiveMethodFieldNames | SigComparerOptions.CaseInsensitivePropertyNames | SigComparerOptions.CaseInsensitiveEventNames);

		// Token: 0x04002719 RID: 10009
		public static readonly EventEqualityComparer CaseInsensitiveDontCompareDeclaringTypes = new EventEqualityComparer(SigComparerOptions.CaseInsensitiveAll);
	}
}
