using System;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BA2 RID: 2978
	[ComVisible(true)]
	public class IMAGE_BOUND_IMPORT_DESCRIPTOR : AbstractStructure
	{
		// Token: 0x060077B6 RID: 30646 RVA: 0x0023B5F8 File Offset: 0x0023B5F8
		public IMAGE_BOUND_IMPORT_DESCRIPTOR(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x1700192C RID: 6444
		// (get) Token: 0x060077B7 RID: 30647 RVA: 0x0023B604 File Offset: 0x0023B604
		// (set) Token: 0x060077B8 RID: 30648 RVA: 0x0023B618 File Offset: 0x0023B618
		public uint TimeDateStamp
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

		// Token: 0x1700192D RID: 6445
		// (get) Token: 0x060077B9 RID: 30649 RVA: 0x0023B62C File Offset: 0x0023B62C
		// (set) Token: 0x060077BA RID: 30650 RVA: 0x0023B644 File Offset: 0x0023B644
		public ushort OffsetModuleName
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 4U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 2U), value);
			}
		}

		// Token: 0x1700192E RID: 6446
		// (get) Token: 0x060077BB RID: 30651 RVA: 0x0023B65C File Offset: 0x0023B65C
		// (set) Token: 0x060077BC RID: 30652 RVA: 0x0023B674 File Offset: 0x0023B674
		public ushort NumberOfModuleForwarderRefs
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 6U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 4U), value);
			}
		}
	}
}
