using System;
using System.Runtime.InteropServices;
using dnlib.Utils;

namespace dnlib.DotNet
{
	// Token: 0x020007B8 RID: 1976
	[ComVisible(true)]
	public class GenericParamUser : GenericParam
	{
		// Token: 0x06004828 RID: 18472 RVA: 0x00176B94 File Offset: 0x00176B94
		public GenericParamUser()
		{
		}

		// Token: 0x06004829 RID: 18473 RVA: 0x00176B9C File Offset: 0x00176B9C
		public GenericParamUser(ushort number) : this(number, GenericParamAttributes.NonVariant)
		{
		}

		// Token: 0x0600482A RID: 18474 RVA: 0x00176BA8 File Offset: 0x00176BA8
		public GenericParamUser(ushort number, GenericParamAttributes flags) : this(number, flags, UTF8String.Empty)
		{
		}

		// Token: 0x0600482B RID: 18475 RVA: 0x00176BB8 File Offset: 0x00176BB8
		public GenericParamUser(ushort number, GenericParamAttributes flags, UTF8String name)
		{
			this.genericParamConstraints = new LazyList<GenericParamConstraint>(this);
			this.number = number;
			this.attributes = (int)flags;
			this.name = name;
		}
	}
}
