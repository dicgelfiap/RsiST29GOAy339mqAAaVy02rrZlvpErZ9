using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007EB RID: 2027
	[ComVisible(true)]
	public abstract class ImportMapper
	{
		// Token: 0x06004905 RID: 18693 RVA: 0x001776A8 File Offset: 0x001776A8
		public virtual ITypeDefOrRef Map(ITypeDefOrRef source)
		{
			return null;
		}

		// Token: 0x06004906 RID: 18694 RVA: 0x001776AC File Offset: 0x001776AC
		public virtual IField Map(FieldDef source)
		{
			return null;
		}

		// Token: 0x06004907 RID: 18695 RVA: 0x001776B0 File Offset: 0x001776B0
		public virtual IMethod Map(MethodDef source)
		{
			return null;
		}

		// Token: 0x06004908 RID: 18696 RVA: 0x001776B4 File Offset: 0x001776B4
		public virtual MemberRef Map(MemberRef source)
		{
			return null;
		}

		// Token: 0x06004909 RID: 18697 RVA: 0x001776B8 File Offset: 0x001776B8
		public virtual TypeRef Map(Type source)
		{
			return null;
		}
	}
}
