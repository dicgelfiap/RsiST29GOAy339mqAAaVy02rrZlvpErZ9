using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007AC RID: 1964
	[ComVisible(true)]
	public class FieldDefUser : FieldDef
	{
		// Token: 0x0600470F RID: 18191 RVA: 0x00171004 File Offset: 0x00171004
		public FieldDefUser()
		{
		}

		// Token: 0x06004710 RID: 18192 RVA: 0x0017100C File Offset: 0x0017100C
		public FieldDefUser(UTF8String name) : this(name, null)
		{
		}

		// Token: 0x06004711 RID: 18193 RVA: 0x00171018 File Offset: 0x00171018
		public FieldDefUser(UTF8String name, FieldSig signature) : this(name, signature, FieldAttributes.PrivateScope)
		{
		}

		// Token: 0x06004712 RID: 18194 RVA: 0x00171024 File Offset: 0x00171024
		public FieldDefUser(UTF8String name, FieldSig signature, FieldAttributes attributes)
		{
			this.name = name;
			this.signature = signature;
			this.attributes = (int)attributes;
		}
	}
}
