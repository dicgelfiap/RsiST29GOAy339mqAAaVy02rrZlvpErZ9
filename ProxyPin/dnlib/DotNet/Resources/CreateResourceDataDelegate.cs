using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Resources
{
	// Token: 0x020008E9 RID: 2281
	// (Invoke) Token: 0x060058DD RID: 22749
	[ComVisible(true)]
	public delegate IResourceData CreateResourceDataDelegate(ResourceDataFactory resourceDataFactory, UserResourceType type, byte[] serializedData);
}
