using System;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BA6 RID: 2982
	[ComVisible(true)]
	public class IMAGE_DELAY_IMPORT_DESCRIPTOR : AbstractStructure
	{
		// Token: 0x060077ED RID: 30701 RVA: 0x0023BC34 File Offset: 0x0023BC34
		public IMAGE_DELAY_IMPORT_DESCRIPTOR(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x17001949 RID: 6473
		// (get) Token: 0x060077EE RID: 30702 RVA: 0x0023BC40 File Offset: 0x0023BC40
		// (set) Token: 0x060077EF RID: 30703 RVA: 0x0023BC54 File Offset: 0x0023BC54
		public uint grAttrs
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

		// Token: 0x1700194A RID: 6474
		// (get) Token: 0x060077F0 RID: 30704 RVA: 0x0023BC68 File Offset: 0x0023BC68
		// (set) Token: 0x060077F1 RID: 30705 RVA: 0x0023BC80 File Offset: 0x0023BC80
		public uint szName
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

		// Token: 0x1700194B RID: 6475
		// (get) Token: 0x060077F2 RID: 30706 RVA: 0x0023BC98 File Offset: 0x0023BC98
		// (set) Token: 0x060077F3 RID: 30707 RVA: 0x0023BCB0 File Offset: 0x0023BCB0
		public uint phmod
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

		// Token: 0x1700194C RID: 6476
		// (get) Token: 0x060077F4 RID: 30708 RVA: 0x0023BCC8 File Offset: 0x0023BCC8
		// (set) Token: 0x060077F5 RID: 30709 RVA: 0x0023BCE0 File Offset: 0x0023BCE0
		public uint pIAT
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

		// Token: 0x1700194D RID: 6477
		// (get) Token: 0x060077F6 RID: 30710 RVA: 0x0023BCF8 File Offset: 0x0023BCF8
		// (set) Token: 0x060077F7 RID: 30711 RVA: 0x0023BD10 File Offset: 0x0023BD10
		public uint pINT
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

		// Token: 0x1700194E RID: 6478
		// (get) Token: 0x060077F8 RID: 30712 RVA: 0x0023BD28 File Offset: 0x0023BD28
		// (set) Token: 0x060077F9 RID: 30713 RVA: 0x0023BD40 File Offset: 0x0023BD40
		public uint pBoundIAT
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

		// Token: 0x1700194F RID: 6479
		// (get) Token: 0x060077FA RID: 30714 RVA: 0x0023BD58 File Offset: 0x0023BD58
		// (set) Token: 0x060077FB RID: 30715 RVA: 0x0023BD70 File Offset: 0x0023BD70
		public uint pUnloadIAT
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset + 24U);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset + 22U, value);
			}
		}

		// Token: 0x17001950 RID: 6480
		// (get) Token: 0x060077FC RID: 30716 RVA: 0x0023BD88 File Offset: 0x0023BD88
		// (set) Token: 0x060077FD RID: 30717 RVA: 0x0023BDA0 File Offset: 0x0023BDA0
		public uint dwTimeStamp
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
	}
}
