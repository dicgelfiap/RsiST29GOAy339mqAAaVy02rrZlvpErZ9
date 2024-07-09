using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000847 RID: 2119
	[ComVisible(true)]
	public sealed class MethodEqualityComparer : IEqualityComparer<IMethod>, IEqualityComparer<IMethodDefOrRef>, IEqualityComparer<MethodDef>, IEqualityComparer<MemberRef>, IEqualityComparer<MethodSpec>
	{
		// Token: 0x06004F1F RID: 20255 RVA: 0x00187D0C File Offset: 0x00187D0C
		public MethodEqualityComparer(SigComparerOptions options)
		{
			this.options = options;
		}

		// Token: 0x06004F20 RID: 20256 RVA: 0x00187D1C File Offset: 0x00187D1C
		public bool Equals(IMethod x, IMethod y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F21 RID: 20257 RVA: 0x00187D44 File Offset: 0x00187D44
		public int GetHashCode(IMethod obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x06004F22 RID: 20258 RVA: 0x00187D6C File Offset: 0x00187D6C
		public bool Equals(IMethodDefOrRef x, IMethodDefOrRef y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F23 RID: 20259 RVA: 0x00187D94 File Offset: 0x00187D94
		public int GetHashCode(IMethodDefOrRef obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x06004F24 RID: 20260 RVA: 0x00187DBC File Offset: 0x00187DBC
		public bool Equals(MethodDef x, MethodDef y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F25 RID: 20261 RVA: 0x00187DE4 File Offset: 0x00187DE4
		public int GetHashCode(MethodDef obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x06004F26 RID: 20262 RVA: 0x00187E0C File Offset: 0x00187E0C
		public bool Equals(MemberRef x, MemberRef y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F27 RID: 20263 RVA: 0x00187E34 File Offset: 0x00187E34
		public int GetHashCode(MemberRef obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x06004F28 RID: 20264 RVA: 0x00187E5C File Offset: 0x00187E5C
		public bool Equals(MethodSpec x, MethodSpec y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F29 RID: 20265 RVA: 0x00187E84 File Offset: 0x00187E84
		public int GetHashCode(MethodSpec obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x0400270B RID: 9995
		private readonly SigComparerOptions options;

		// Token: 0x0400270C RID: 9996
		public static readonly MethodEqualityComparer CompareDeclaringTypes = new MethodEqualityComparer(SigComparerOptions.CompareMethodFieldDeclaringType);

		// Token: 0x0400270D RID: 9997
		public static readonly MethodEqualityComparer DontCompareDeclaringTypes = new MethodEqualityComparer((SigComparerOptions)0U);

		// Token: 0x0400270E RID: 9998
		public static readonly MethodEqualityComparer CaseInsensitiveCompareDeclaringTypes = new MethodEqualityComparer(SigComparerOptions.CompareMethodFieldDeclaringType | SigComparerOptions.CaseInsensitiveTypeNamespaces | SigComparerOptions.CaseInsensitiveTypeNames | SigComparerOptions.CaseInsensitiveMethodFieldNames | SigComparerOptions.CaseInsensitivePropertyNames | SigComparerOptions.CaseInsensitiveEventNames);

		// Token: 0x0400270F RID: 9999
		public static readonly MethodEqualityComparer CaseInsensitiveDontCompareDeclaringTypes = new MethodEqualityComparer(SigComparerOptions.CaseInsensitiveAll);
	}
}
