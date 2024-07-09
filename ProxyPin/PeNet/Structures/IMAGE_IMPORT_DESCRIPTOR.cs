using System;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BAB RID: 2987
	[ComVisible(true)]
	public class IMAGE_IMPORT_DESCRIPTOR : AbstractStructure
	{
		// Token: 0x0600784F RID: 30799 RVA: 0x0023C7A8 File Offset: 0x0023C7A8
		public IMAGE_IMPORT_DESCRIPTOR(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x17001978 RID: 6520
		// (get) Token: 0x06007850 RID: 30800 RVA: 0x0023C7B4 File Offset: 0x0023C7B4
		// (set) Token: 0x06007851 RID: 30801 RVA: 0x0023C7C8 File Offset: 0x0023C7C8
		public uint OriginalFirstThunk
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

		// Token: 0x17001979 RID: 6521
		// (get) Token: 0x06007852 RID: 30802 RVA: 0x0023C7DC File Offset: 0x0023C7DC
		// (set) Token: 0x06007853 RID: 30803 RVA: 0x0023C7F4 File Offset: 0x0023C7F4
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

		// Token: 0x1700197A RID: 6522
		// (get) Token: 0x06007854 RID: 30804 RVA: 0x0023C80C File Offset: 0x0023C80C
		// (set) Token: 0x06007855 RID: 30805 RVA: 0x0023C824 File Offset: 0x0023C824
		public uint ForwarderChain
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

		// Token: 0x1700197B RID: 6523
		// (get) Token: 0x06007856 RID: 30806 RVA: 0x0023C83C File Offset: 0x0023C83C
		// (set) Token: 0x06007857 RID: 30807 RVA: 0x0023C854 File Offset: 0x0023C854
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

		// Token: 0x1700197C RID: 6524
		// (get) Token: 0x06007858 RID: 30808 RVA: 0x0023C86C File Offset: 0x0023C86C
		// (set) Token: 0x06007859 RID: 30809 RVA: 0x0023C884 File Offset: 0x0023C884
		public uint FirstThunk
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
	}
}
