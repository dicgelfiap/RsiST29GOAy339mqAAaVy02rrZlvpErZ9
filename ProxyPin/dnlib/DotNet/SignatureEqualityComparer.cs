using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200084A RID: 2122
	[ComVisible(true)]
	public sealed class SignatureEqualityComparer : IEqualityComparer<CallingConventionSig>, IEqualityComparer<MethodBaseSig>, IEqualityComparer<MethodSig>, IEqualityComparer<PropertySig>, IEqualityComparer<FieldSig>, IEqualityComparer<LocalSig>, IEqualityComparer<GenericInstMethodSig>
	{
		// Token: 0x06004F33 RID: 20275 RVA: 0x00188014 File Offset: 0x00188014
		public SignatureEqualityComparer(SigComparerOptions options)
		{
			this.options = options;
		}

		// Token: 0x06004F34 RID: 20276 RVA: 0x00188024 File Offset: 0x00188024
		public bool Equals(CallingConventionSig x, CallingConventionSig y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F35 RID: 20277 RVA: 0x0018804C File Offset: 0x0018804C
		public int GetHashCode(CallingConventionSig obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x06004F36 RID: 20278 RVA: 0x00188074 File Offset: 0x00188074
		public bool Equals(MethodBaseSig x, MethodBaseSig y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F37 RID: 20279 RVA: 0x0018809C File Offset: 0x0018809C
		public int GetHashCode(MethodBaseSig obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x06004F38 RID: 20280 RVA: 0x001880C4 File Offset: 0x001880C4
		public bool Equals(MethodSig x, MethodSig y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F39 RID: 20281 RVA: 0x001880EC File Offset: 0x001880EC
		public int GetHashCode(MethodSig obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x06004F3A RID: 20282 RVA: 0x00188114 File Offset: 0x00188114
		public bool Equals(PropertySig x, PropertySig y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F3B RID: 20283 RVA: 0x0018813C File Offset: 0x0018813C
		public int GetHashCode(PropertySig obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x06004F3C RID: 20284 RVA: 0x00188164 File Offset: 0x00188164
		public bool Equals(FieldSig x, FieldSig y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F3D RID: 20285 RVA: 0x0018818C File Offset: 0x0018818C
		public int GetHashCode(FieldSig obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x06004F3E RID: 20286 RVA: 0x001881B4 File Offset: 0x001881B4
		public bool Equals(LocalSig x, LocalSig y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F3F RID: 20287 RVA: 0x001881DC File Offset: 0x001881DC
		public int GetHashCode(LocalSig obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x06004F40 RID: 20288 RVA: 0x00188204 File Offset: 0x00188204
		public bool Equals(GenericInstMethodSig x, GenericInstMethodSig y)
		{
			return new SigComparer(this.options).Equals(x, y);
		}

		// Token: 0x06004F41 RID: 20289 RVA: 0x0018822C File Offset: 0x0018822C
		public int GetHashCode(GenericInstMethodSig obj)
		{
			return new SigComparer(this.options).GetHashCode(obj);
		}

		// Token: 0x0400271A RID: 10010
		private readonly SigComparerOptions options;

		// Token: 0x0400271B RID: 10011
		public static readonly SignatureEqualityComparer Instance = new SignatureEqualityComparer((SigComparerOptions)0U);

		// Token: 0x0400271C RID: 10012
		public static readonly SignatureEqualityComparer CaseInsensitive = new SignatureEqualityComparer(SigComparerOptions.CaseInsensitiveAll);
	}
}
