using System;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BA9 RID: 2985
	[ComVisible(true)]
	public class IMAGE_FILE_HEADER : AbstractStructure
	{
		// Token: 0x0600783C RID: 30780 RVA: 0x0023C600 File Offset: 0x0023C600
		public IMAGE_FILE_HEADER(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x1700196F RID: 6511
		// (get) Token: 0x0600783D RID: 30781 RVA: 0x0023C60C File Offset: 0x0023C60C
		// (set) Token: 0x0600783E RID: 30782 RVA: 0x0023C620 File Offset: 0x0023C620
		public ushort Machine
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)this.Offset);
			}
			set
			{
				this.Buff.SetUInt16((ulong)this.Offset, value);
			}
		}

		// Token: 0x17001970 RID: 6512
		// (get) Token: 0x0600783F RID: 30783 RVA: 0x0023C638 File Offset: 0x0023C638
		// (set) Token: 0x06007840 RID: 30784 RVA: 0x0023C650 File Offset: 0x0023C650
		public ushort NumberOfSections
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 2U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 2U), value);
			}
		}

		// Token: 0x17001971 RID: 6513
		// (get) Token: 0x06007841 RID: 30785 RVA: 0x0023C668 File Offset: 0x0023C668
		// (set) Token: 0x06007842 RID: 30786 RVA: 0x0023C680 File Offset: 0x0023C680
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

		// Token: 0x17001972 RID: 6514
		// (get) Token: 0x06007843 RID: 30787 RVA: 0x0023C698 File Offset: 0x0023C698
		// (set) Token: 0x06007844 RID: 30788 RVA: 0x0023C6B0 File Offset: 0x0023C6B0
		public uint PointerToSymbolTable
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset + 8U);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset + 8U, value);
			}
		}

		// Token: 0x17001973 RID: 6515
		// (get) Token: 0x06007845 RID: 30789 RVA: 0x0023C6C8 File Offset: 0x0023C6C8
		// (set) Token: 0x06007846 RID: 30790 RVA: 0x0023C6E0 File Offset: 0x0023C6E0
		public uint NumberOfSymbols
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

		// Token: 0x17001974 RID: 6516
		// (get) Token: 0x06007847 RID: 30791 RVA: 0x0023C6F8 File Offset: 0x0023C6F8
		// (set) Token: 0x06007848 RID: 30792 RVA: 0x0023C710 File Offset: 0x0023C710
		public ushort SizeOfOptionalHeader
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 16U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 16U), value);
			}
		}

		// Token: 0x17001975 RID: 6517
		// (get) Token: 0x06007849 RID: 30793 RVA: 0x0023C728 File Offset: 0x0023C728
		// (set) Token: 0x0600784A RID: 30794 RVA: 0x0023C740 File Offset: 0x0023C740
		public ushort Characteristics
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 18U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 18U), value);
			}
		}
	}
}
