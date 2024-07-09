using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007A8 RID: 1960
	[ComVisible(true)]
	public class ExportedTypeUser : ExportedType
	{
		// Token: 0x06004693 RID: 18067 RVA: 0x00170344 File Offset: 0x00170344
		public ExportedTypeUser(ModuleDef module)
		{
			this.module = module;
		}

		// Token: 0x06004694 RID: 18068 RVA: 0x00170354 File Offset: 0x00170354
		public ExportedTypeUser(ModuleDef module, uint typeDefId, UTF8String typeNamespace, UTF8String typeName, TypeAttributes flags, IImplementation implementation)
		{
			this.module = module;
			this.typeDefId = typeDefId;
			this.typeName = typeName;
			this.typeNamespace = typeNamespace;
			this.attributes = (int)flags;
			this.implementation = implementation;
			this.implementation_isInitialized = true;
		}
	}
}
