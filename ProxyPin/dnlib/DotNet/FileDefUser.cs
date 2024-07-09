using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007B0 RID: 1968
	[ComVisible(true)]
	public class FileDefUser : FileDef
	{
		// Token: 0x0600473A RID: 18234 RVA: 0x00171570 File Offset: 0x00171570
		public FileDefUser()
		{
		}

		// Token: 0x0600473B RID: 18235 RVA: 0x00171578 File Offset: 0x00171578
		public FileDefUser(UTF8String name, FileAttributes flags, byte[] hashValue)
		{
			this.name = name;
			this.attributes = (int)flags;
			this.hashValue = hashValue;
		}
	}
}
