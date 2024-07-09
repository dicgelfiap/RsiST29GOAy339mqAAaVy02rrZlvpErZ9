using System;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BAF RID: 2991
	[ComVisible(true)]
	public class IMAGE_RESOURCE_DATA_ENTRY : AbstractStructure
	{
		// Token: 0x060078CD RID: 30925 RVA: 0x0023DA8C File Offset: 0x0023DA8C
		public IMAGE_RESOURCE_DATA_ENTRY(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x170019B5 RID: 6581
		// (get) Token: 0x060078CE RID: 30926 RVA: 0x0023DA98 File Offset: 0x0023DA98
		// (set) Token: 0x060078CF RID: 30927 RVA: 0x0023DAAC File Offset: 0x0023DAAC
		public uint OffsetToData
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

		// Token: 0x170019B6 RID: 6582
		// (get) Token: 0x060078D0 RID: 30928 RVA: 0x0023DAC0 File Offset: 0x0023DAC0
		// (set) Token: 0x060078D1 RID: 30929 RVA: 0x0023DAD8 File Offset: 0x0023DAD8
		public uint Size1
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

		// Token: 0x170019B7 RID: 6583
		// (get) Token: 0x060078D2 RID: 30930 RVA: 0x0023DAF0 File Offset: 0x0023DAF0
		// (set) Token: 0x060078D3 RID: 30931 RVA: 0x0023DB08 File Offset: 0x0023DB08
		public uint CodePage
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

		// Token: 0x170019B8 RID: 6584
		// (get) Token: 0x060078D4 RID: 30932 RVA: 0x0023DB20 File Offset: 0x0023DB20
		// (set) Token: 0x060078D5 RID: 30933 RVA: 0x0023DB38 File Offset: 0x0023DB38
		public uint Reserved
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
	}
}
