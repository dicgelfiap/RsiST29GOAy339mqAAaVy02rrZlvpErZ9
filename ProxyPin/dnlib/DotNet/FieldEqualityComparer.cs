using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000846 RID: 2118
	[ComVisible(true)]
	public sealed class FieldEqualityComparer : IEqualityComparer<IField>, IEqualityComparer<FieldDef>, IEqualityComparer<MemberRef>
	{
		// Token: 0x06004F17 RID: 20247 RVA: 0x00187BD4 File Offset: 0x00187BD4
		public FieldEqualityComparer(SigComparerOptions options)
		{
			this.options = options;
		}

		// Token: 0x06004F18 RID: 20248 RVA: 0x00187BE4 File Offset: 0x00187BE4
		public bool Equals(IField x, IField y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F19 RID: 20249 RVA: 0x00187C0C File Offset: 0x00187C0C
		public int GetHashCode(IField obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x06004F1A RID: 20250 RVA: 0x00187C34 File Offset: 0x00187C34
		public bool Equals(FieldDef x, FieldDef y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F1B RID: 20251 RVA: 0x00187C5C File Offset: 0x00187C5C
		public int GetHashCode(FieldDef obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x06004F1C RID: 20252 RVA: 0x00187C84 File Offset: 0x00187C84
		public bool Equals(MemberRef x, MemberRef y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F1D RID: 20253 RVA: 0x00187CAC File Offset: 0x00187CAC
		public int GetHashCode(MemberRef obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x04002706 RID: 9990
		private readonly SigComparerOptions options;

		// Token: 0x04002707 RID: 9991
		public static readonly FieldEqualityComparer CompareDeclaringTypes = new FieldEqualityComparer(SigComparerOptions.CompareMethodFieldDeclaringType);

		// Token: 0x04002708 RID: 9992
		public static readonly FieldEqualityComparer DontCompareDeclaringTypes = new FieldEqualityComparer((SigComparerOptions)0U);

		// Token: 0x04002709 RID: 9993
		public static readonly FieldEqualityComparer CaseInsensitiveCompareDeclaringTypes = new FieldEqualityComparer(SigComparerOptions.CompareMethodFieldDeclaringType | SigComparerOptions.CaseInsensitiveTypeNamespaces | SigComparerOptions.CaseInsensitiveTypeNames | SigComparerOptions.CaseInsensitiveMethodFieldNames | SigComparerOptions.CaseInsensitivePropertyNames | SigComparerOptions.CaseInsensitiveEventNames);

		// Token: 0x0400270A RID: 9994
		public static readonly FieldEqualityComparer CaseInsensitiveDontCompareDeclaringTypes = new FieldEqualityComparer(SigComparerOptions.CaseInsensitiveAll);
	}
}
