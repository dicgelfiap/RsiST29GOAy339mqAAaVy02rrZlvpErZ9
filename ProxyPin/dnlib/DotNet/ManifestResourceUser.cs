using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007FA RID: 2042
	[ComVisible(true)]
	public class ManifestResourceUser : ManifestResource
	{
		// Token: 0x06004990 RID: 18832 RVA: 0x00179CB4 File Offset: 0x00179CB4
		public ManifestResourceUser()
		{
		}

		// Token: 0x06004991 RID: 18833 RVA: 0x00179CBC File Offset: 0x00179CBC
		public ManifestResourceUser(UTF8String name, IImplementation implementation) : this(name, implementation, (ManifestResourceAttributes)0U)
		{
		}

		// Token: 0x06004992 RID: 18834 RVA: 0x00179CC8 File Offset: 0x00179CC8
		public ManifestResourceUser(UTF8String name, IImplementation implementation, ManifestResourceAttributes flags) : this(name, implementation, flags, 0U)
		{
		}

		// Token: 0x06004993 RID: 18835 RVA: 0x00179CD4 File Offset: 0x00179CD4
		public ManifestResourceUser(UTF8String name, IImplementation implementation, ManifestResourceAttributes flags, uint offset)
		{
			this.name = name;
			this.implementation = implementation;
			this.attributes = (int)flags;
			this.offset = offset;
		}
	}
}
