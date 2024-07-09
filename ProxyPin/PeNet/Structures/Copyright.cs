using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PeNet.Structures
{
	// Token: 0x02000B9F RID: 2975
	[ComVisible(true)]
	public class Copyright : AbstractStructure
	{
		// Token: 0x060077A6 RID: 30630 RVA: 0x0023B424 File Offset: 0x0023B424
		public Copyright(byte[] buff, uint offset, uint size) : base(buff, offset)
		{
			this.CopyrightString = this.ParseCopyrightString(buff, offset, size);
		}

		// Token: 0x17001925 RID: 6437
		// (get) Token: 0x060077A7 RID: 30631 RVA: 0x0023B44C File Offset: 0x0023B44C
		// (set) Token: 0x060077A8 RID: 30632 RVA: 0x0023B454 File Offset: 0x0023B454
		public string CopyrightString { get; private set; }

		// Token: 0x060077A9 RID: 30633 RVA: 0x0023B460 File Offset: 0x0023B460
		private string ParseCopyrightString(byte[] buff, uint offset, uint size)
		{
			return Encoding.ASCII.GetString(buff, (int)offset, (int)size);
		}
	}
}
