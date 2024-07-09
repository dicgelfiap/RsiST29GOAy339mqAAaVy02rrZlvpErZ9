using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000869 RID: 2153
	[ComVisible(true)]
	public sealed class CorLibTypeSig : TypeDefOrRefSig
	{
		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x0600529E RID: 21150 RVA: 0x00196120 File Offset: 0x00196120
		public override ElementType ElementType
		{
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x0600529F RID: 21151 RVA: 0x00196128 File Offset: 0x00196128
		public CorLibTypeSig(ITypeDefOrRef corType, ElementType elementType) : base(corType)
		{
			if (!(corType is TypeRef) && !(corType is TypeDef))
			{
				throw new ArgumentException("corType must be a TypeDef or a TypeRef. null and TypeSpec are invalid inputs.");
			}
			this.elementType = elementType;
		}

		// Token: 0x040027CC RID: 10188
		private readonly ElementType elementType;
	}
}
