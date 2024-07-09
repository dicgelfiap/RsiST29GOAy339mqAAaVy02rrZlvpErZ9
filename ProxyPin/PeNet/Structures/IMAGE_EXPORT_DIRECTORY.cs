using System;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BA8 RID: 2984
	[ComVisible(true)]
	public class IMAGE_EXPORT_DIRECTORY : AbstractStructure
	{
		// Token: 0x06007825 RID: 30757 RVA: 0x0023C3EC File Offset: 0x0023C3EC
		public IMAGE_EXPORT_DIRECTORY(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x17001964 RID: 6500
		// (get) Token: 0x06007826 RID: 30758 RVA: 0x0023C3F8 File Offset: 0x0023C3F8
		// (set) Token: 0x06007827 RID: 30759 RVA: 0x0023C40C File Offset: 0x0023C40C
		public uint Characteristics
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

		// Token: 0x17001965 RID: 6501
		// (get) Token: 0x06007828 RID: 30760 RVA: 0x0023C420 File Offset: 0x0023C420
		// (set) Token: 0x06007829 RID: 30761 RVA: 0x0023C438 File Offset: 0x0023C438
		public uint TimeDateStamp
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

		// Token: 0x17001966 RID: 6502
		// (get) Token: 0x0600782A RID: 30762 RVA: 0x0023C450 File Offset: 0x0023C450
		// (set) Token: 0x0600782B RID: 30763 RVA: 0x0023C468 File Offset: 0x0023C468
		public ushort MajorVersion
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 8U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 8U), value);
			}
		}

		// Token: 0x17001967 RID: 6503
		// (get) Token: 0x0600782C RID: 30764 RVA: 0x0023C480 File Offset: 0x0023C480
		// (set) Token: 0x0600782D RID: 30765 RVA: 0x0023C498 File Offset: 0x0023C498
		public ushort MinorVersion
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 10U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 10U), value);
			}
		}

		// Token: 0x17001968 RID: 6504
		// (get) Token: 0x0600782E RID: 30766 RVA: 0x0023C4B0 File Offset: 0x0023C4B0
		// (set) Token: 0x0600782F RID: 30767 RVA: 0x0023C4C8 File Offset: 0x0023C4C8
		public uint Name
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset + 12U);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset + 12U, value);
			}
		}

		// Token: 0x17001969 RID: 6505
		// (get) Token: 0x06007830 RID: 30768 RVA: 0x0023C4E0 File Offset: 0x0023C4E0
		// (set) Token: 0x06007831 RID: 30769 RVA: 0x0023C4F8 File Offset: 0x0023C4F8
		public uint Base
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset + 16U);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset + 16U, value);
			}
		}

		// Token: 0x1700196A RID: 6506
		// (get) Token: 0x06007832 RID: 30770 RVA: 0x0023C510 File Offset: 0x0023C510
		// (set) Token: 0x06007833 RID: 30771 RVA: 0x0023C528 File Offset: 0x0023C528
		public uint NumberOfFunctions
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset + 20U);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset + 20U, value);
			}
		}

		// Token: 0x1700196B RID: 6507
		// (get) Token: 0x06007834 RID: 30772 RVA: 0x0023C540 File Offset: 0x0023C540
		// (set) Token: 0x06007835 RID: 30773 RVA: 0x0023C558 File Offset: 0x0023C558
		public uint NumberOfNames
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset + 24U);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset + 24U, value);
			}
		}

		// Token: 0x1700196C RID: 6508
		// (get) Token: 0x06007836 RID: 30774 RVA: 0x0023C570 File Offset: 0x0023C570
		// (set) Token: 0x06007837 RID: 30775 RVA: 0x0023C588 File Offset: 0x0023C588
		public uint AddressOfFunctions
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset + 28U);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset + 28U, value);
			}
		}

		// Token: 0x1700196D RID: 6509
		// (get) Token: 0x06007838 RID: 30776 RVA: 0x0023C5A0 File Offset: 0x0023C5A0
		// (set) Token: 0x06007839 RID: 30777 RVA: 0x0023C5B8 File Offset: 0x0023C5B8
		public uint AddressOfNames
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset + 32U);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset + 32U, value);
			}
		}

		// Token: 0x1700196E RID: 6510
		// (get) Token: 0x0600783A RID: 30778 RVA: 0x0023C5D0 File Offset: 0x0023C5D0
		// (set) Token: 0x0600783B RID: 30779 RVA: 0x0023C5E8 File Offset: 0x0023C5E8
		public uint AddressOfNameOrdinals
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset + 36U);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset + 36U, value);
			}
		}
	}
}
