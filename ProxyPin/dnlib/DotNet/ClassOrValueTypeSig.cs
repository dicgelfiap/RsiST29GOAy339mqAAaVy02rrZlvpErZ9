using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200086A RID: 2154
	[ComVisible(true)]
	public abstract class ClassOrValueTypeSig : TypeDefOrRefSig
	{
		// Token: 0x060052A0 RID: 21152 RVA: 0x0019615C File Offset: 0x0019615C
		protected ClassOrValueTypeSig(ITypeDefOrRef typeDefOrRef) : base(typeDefOrRef)
		{
		}
	}
}
