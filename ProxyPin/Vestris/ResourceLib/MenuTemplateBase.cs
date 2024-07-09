using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D2C RID: 3372
	[ComVisible(true)]
	public abstract class MenuTemplateBase
	{
		// Token: 0x06008901 RID: 35073
		internal abstract IntPtr Read(IntPtr lpRes);

		// Token: 0x06008902 RID: 35074
		internal abstract void Write(BinaryWriter w);
	}
}
