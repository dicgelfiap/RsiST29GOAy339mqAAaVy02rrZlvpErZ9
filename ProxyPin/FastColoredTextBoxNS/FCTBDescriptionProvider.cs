using System;
using System.ComponentModel;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A4A RID: 2634
	internal class FCTBDescriptionProvider : TypeDescriptionProvider
	{
		// Token: 0x06006799 RID: 26521 RVA: 0x001F8CF8 File Offset: 0x001F8CF8
		public FCTBDescriptionProvider(Type type) : base(FCTBDescriptionProvider.GetDefaultTypeProvider(type))
		{
		}

		// Token: 0x0600679A RID: 26522 RVA: 0x001F8D08 File Offset: 0x001F8D08
		private static TypeDescriptionProvider GetDefaultTypeProvider(Type type)
		{
			return TypeDescriptor.GetProvider(type);
		}

		// Token: 0x0600679B RID: 26523 RVA: 0x001F8D28 File Offset: 0x001F8D28
		public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
		{
			ICustomTypeDescriptor typeDescriptor = base.GetTypeDescriptor(objectType, instance);
			return new FCTBTypeDescriptor(typeDescriptor, instance);
		}
	}
}
