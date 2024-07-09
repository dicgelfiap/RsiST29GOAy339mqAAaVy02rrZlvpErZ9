using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000848 RID: 2120
	[ComVisible(true)]
	public sealed class PropertyEqualityComparer : IEqualityComparer<PropertyDef>
	{
		// Token: 0x06004F2B RID: 20267 RVA: 0x00187EE4 File Offset: 0x00187EE4
		public PropertyEqualityComparer(SigComparerOptions options)
		{
			this.options = options;
		}

		// Token: 0x06004F2C RID: 20268 RVA: 0x00187EF4 File Offset: 0x00187EF4
		public bool Equals(PropertyDef x, PropertyDef y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F2D RID: 20269 RVA: 0x00187F1C File Offset: 0x00187F1C
		public int GetHashCode(PropertyDef obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x04002710 RID: 10000
		private readonly SigComparerOptions options;

		// Token: 0x04002711 RID: 10001
		public static readonly PropertyEqualityComparer CompareDeclaringTypes = new PropertyEqualityComparer(SigComparerOptions.ComparePropertyDeclaringType);

		// Token: 0x04002712 RID: 10002
		public static readonly PropertyEqualityComparer DontCompareDeclaringTypes = new PropertyEqualityComparer((SigComparerOptions)0U);

		// Token: 0x04002713 RID: 10003
		public static readonly PropertyEqualityComparer CaseInsensitiveCompareDeclaringTypes = new PropertyEqualityComparer(SigComparerOptions.ComparePropertyDeclaringType | SigComparerOptions.CaseInsensitiveTypeNamespaces | SigComparerOptions.CaseInsensitiveTypeNames | SigComparerOptions.CaseInsensitiveMethodFieldNames | SigComparerOptions.CaseInsensitivePropertyNames | SigComparerOptions.CaseInsensitiveEventNames);

		// Token: 0x04002714 RID: 10004
		public static readonly PropertyEqualityComparer CaseInsensitiveDontCompareDeclaringTypes = new PropertyEqualityComparer(SigComparerOptions.CaseInsensitiveAll);
	}
}
