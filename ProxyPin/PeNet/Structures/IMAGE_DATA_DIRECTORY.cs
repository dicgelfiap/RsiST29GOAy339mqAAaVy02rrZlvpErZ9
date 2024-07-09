using System;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BA4 RID: 2980
	[ComVisible(true)]
	public class IMAGE_DATA_DIRECTORY : AbstractStructure
	{
		// Token: 0x060077D2 RID: 30674 RVA: 0x0023B94C File Offset: 0x0023B94C
		public IMAGE_DATA_DIRECTORY(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x1700193C RID: 6460
		// (get) Token: 0x060077D3 RID: 30675 RVA: 0x0023B958 File Offset: 0x0023B958
		// (set) Token: 0x060077D4 RID: 30676 RVA: 0x0023B96C File Offset: 0x0023B96C
		public uint VirtualAddress
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset, value);
			}
		}

		// Token: 0x1700193D RID: 6461
		// (get) Token: 0x060077D5 RID: 30677 RVA: 0x0023B980 File Offset: 0x0023B980
		// (set) Token: 0x060077D6 RID: 30678 RVA: 0x0023B998 File Offset: 0x0023B998
		public uint Size
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset + 4U);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset + 4U, value);
			}
		}
	}
}
