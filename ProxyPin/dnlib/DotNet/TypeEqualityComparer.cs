using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000845 RID: 2117
	[ComVisible(true)]
	public sealed class TypeEqualityComparer : IEqualityComparer<IType>, IEqualityComparer<ITypeDefOrRef>, IEqualityComparer<TypeRef>, IEqualityComparer<TypeDef>, IEqualityComparer<TypeSpec>, IEqualityComparer<TypeSig>, IEqualityComparer<ExportedType>
	{
		// Token: 0x06004F07 RID: 20231 RVA: 0x00187978 File Offset: 0x00187978
		public TypeEqualityComparer(SigComparerOptions options)
		{
			this.options = options;
		}

		// Token: 0x06004F08 RID: 20232 RVA: 0x00187988 File Offset: 0x00187988
		public bool Equals(IType x, IType y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F09 RID: 20233 RVA: 0x001879B0 File Offset: 0x001879B0
		public int GetHashCode(IType obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x06004F0A RID: 20234 RVA: 0x001879D8 File Offset: 0x001879D8
		public bool Equals(ITypeDefOrRef x, ITypeDefOrRef y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F0B RID: 20235 RVA: 0x00187A00 File Offset: 0x00187A00
		public int GetHashCode(ITypeDefOrRef obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x06004F0C RID: 20236 RVA: 0x00187A28 File Offset: 0x00187A28
		public bool Equals(TypeRef x, TypeRef y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F0D RID: 20237 RVA: 0x00187A50 File Offset: 0x00187A50
		public int GetHashCode(TypeRef obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x06004F0E RID: 20238 RVA: 0x00187A78 File Offset: 0x00187A78
		public bool Equals(TypeDef x, TypeDef y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F0F RID: 20239 RVA: 0x00187AA0 File Offset: 0x00187AA0
		public int GetHashCode(TypeDef obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x06004F10 RID: 20240 RVA: 0x00187AC8 File Offset: 0x00187AC8
		public bool Equals(TypeSpec x, TypeSpec y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F11 RID: 20241 RVA: 0x00187AF0 File Offset: 0x00187AF0
		public int GetHashCode(TypeSpec obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x06004F12 RID: 20242 RVA: 0x00187B18 File Offset: 0x00187B18
		public bool Equals(TypeSig x, TypeSig y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F13 RID: 20243 RVA: 0x00187B40 File Offset: 0x00187B40
		public int GetHashCode(TypeSig obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x06004F14 RID: 20244 RVA: 0x00187B68 File Offset: 0x00187B68
		public bool Equals(ExportedType x, ExportedType y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F15 RID: 20245 RVA: 0x00187B90 File Offset: 0x00187B90
		public int GetHashCode(ExportedType obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x04002703 RID: 9987
		private readonly SigComparerOptions options;

		// Token: 0x04002704 RID: 9988
		public static readonly TypeEqualityComparer Instance = new TypeEqualityComparer((SigComparerOptions)0U);

		// Token: 0x04002705 RID: 9989
		public static readonly TypeEqualityComparer CaseInsensitive = new TypeEqualityComparer(SigComparerOptions.CaseInsensitiveAll);
	}
}
