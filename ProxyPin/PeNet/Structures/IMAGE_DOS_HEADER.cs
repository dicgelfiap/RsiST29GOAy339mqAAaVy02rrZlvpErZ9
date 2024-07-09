using System;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BA7 RID: 2983
	[ComVisible(true)]
	public class IMAGE_DOS_HEADER : AbstractStructure
	{
		// Token: 0x060077FE RID: 30718 RVA: 0x0023BDB8 File Offset: 0x0023BDB8
		public IMAGE_DOS_HEADER(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x17001951 RID: 6481
		// (get) Token: 0x060077FF RID: 30719 RVA: 0x0023BDC4 File Offset: 0x0023BDC4
		// (set) Token: 0x06007800 RID: 30720 RVA: 0x0023BDD8 File Offset: 0x0023BDD8
		public ushort e_magic
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

		// Token: 0x17001952 RID: 6482
		// (get) Token: 0x06007801 RID: 30721 RVA: 0x0023BDF0 File Offset: 0x0023BDF0
		// (set) Token: 0x06007802 RID: 30722 RVA: 0x0023BE08 File Offset: 0x0023BE08
		public ushort e_cblp
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

		// Token: 0x17001953 RID: 6483
		// (get) Token: 0x06007803 RID: 30723 RVA: 0x0023BE20 File Offset: 0x0023BE20
		// (set) Token: 0x06007804 RID: 30724 RVA: 0x0023BE38 File Offset: 0x0023BE38
		public ushort e_cp
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 4U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 4U), value);
			}
		}

		// Token: 0x17001954 RID: 6484
		// (get) Token: 0x06007805 RID: 30725 RVA: 0x0023BE50 File Offset: 0x0023BE50
		// (set) Token: 0x06007806 RID: 30726 RVA: 0x0023BE68 File Offset: 0x0023BE68
		public ushort e_crlc
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 6U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 6U), value);
			}
		}

		// Token: 0x17001955 RID: 6485
		// (get) Token: 0x06007807 RID: 30727 RVA: 0x0023BE80 File Offset: 0x0023BE80
		// (set) Token: 0x06007808 RID: 30728 RVA: 0x0023BE98 File Offset: 0x0023BE98
		public ushort e_cparhdr
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

		// Token: 0x17001956 RID: 6486
		// (get) Token: 0x06007809 RID: 30729 RVA: 0x0023BEB0 File Offset: 0x0023BEB0
		// (set) Token: 0x0600780A RID: 30730 RVA: 0x0023BEC8 File Offset: 0x0023BEC8
		public ushort e_minalloc
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

		// Token: 0x17001957 RID: 6487
		// (get) Token: 0x0600780B RID: 30731 RVA: 0x0023BEE0 File Offset: 0x0023BEE0
		// (set) Token: 0x0600780C RID: 30732 RVA: 0x0023BEF8 File Offset: 0x0023BEF8
		public ushort e_maxalloc
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 12U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 12U), value);
			}
		}

		// Token: 0x17001958 RID: 6488
		// (get) Token: 0x0600780D RID: 30733 RVA: 0x0023BF10 File Offset: 0x0023BF10
		// (set) Token: 0x0600780E RID: 30734 RVA: 0x0023BF28 File Offset: 0x0023BF28
		public ushort e_ss
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 14U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 14U), value);
			}
		}

		// Token: 0x17001959 RID: 6489
		// (get) Token: 0x0600780F RID: 30735 RVA: 0x0023BF40 File Offset: 0x0023BF40
		// (set) Token: 0x06007810 RID: 30736 RVA: 0x0023BF58 File Offset: 0x0023BF58
		public ushort e_sp
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

		// Token: 0x1700195A RID: 6490
		// (get) Token: 0x06007811 RID: 30737 RVA: 0x0023BF70 File Offset: 0x0023BF70
		// (set) Token: 0x06007812 RID: 30738 RVA: 0x0023BF88 File Offset: 0x0023BF88
		public ushort e_csum
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

		// Token: 0x1700195B RID: 6491
		// (get) Token: 0x06007813 RID: 30739 RVA: 0x0023BFA0 File Offset: 0x0023BFA0
		// (set) Token: 0x06007814 RID: 30740 RVA: 0x0023BFB8 File Offset: 0x0023BFB8
		public ushort e_ip
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 20U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 20U), value);
			}
		}

		// Token: 0x1700195C RID: 6492
		// (get) Token: 0x06007815 RID: 30741 RVA: 0x0023BFD0 File Offset: 0x0023BFD0
		// (set) Token: 0x06007816 RID: 30742 RVA: 0x0023BFE8 File Offset: 0x0023BFE8
		public ushort e_cs
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 22U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 22U), value);
			}
		}

		// Token: 0x1700195D RID: 6493
		// (get) Token: 0x06007817 RID: 30743 RVA: 0x0023C000 File Offset: 0x0023C000
		// (set) Token: 0x06007818 RID: 30744 RVA: 0x0023C018 File Offset: 0x0023C018
		public ushort e_lfarlc
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 24U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 24U), value);
			}
		}

		// Token: 0x1700195E RID: 6494
		// (get) Token: 0x06007819 RID: 30745 RVA: 0x0023C030 File Offset: 0x0023C030
		// (set) Token: 0x0600781A RID: 30746 RVA: 0x0023C048 File Offset: 0x0023C048
		public ushort e_ovno
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 26U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 26U), value);
			}
		}

		// Token: 0x1700195F RID: 6495
		// (get) Token: 0x0600781B RID: 30747 RVA: 0x0023C060 File Offset: 0x0023C060
		// (set) Token: 0x0600781C RID: 30748 RVA: 0x0023C0D8 File Offset: 0x0023C0D8
		public ushort[] e_res
		{
			get
			{
				return new ushort[]
				{
					this.Buff.BytesToUInt16((ulong)(this.Offset + 28U)),
					this.Buff.BytesToUInt16((ulong)(this.Offset + 30U)),
					this.Buff.BytesToUInt16((ulong)(this.Offset + 32U)),
					this.Buff.BytesToUInt16((ulong)(this.Offset + 34U))
				};
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 28U), value[0]);
				this.Buff.SetUInt16((ulong)(this.Offset + 30U), value[1]);
				this.Buff.SetUInt16((ulong)(this.Offset + 32U), value[2]);
				this.Buff.SetUInt16((ulong)(this.Offset + 34U), value[3]);
			}
		}

		// Token: 0x17001960 RID: 6496
		// (get) Token: 0x0600781D RID: 30749 RVA: 0x0023C14C File Offset: 0x0023C14C
		// (set) Token: 0x0600781E RID: 30750 RVA: 0x0023C164 File Offset: 0x0023C164
		public ushort e_oemid
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 36U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 36U), value);
			}
		}

		// Token: 0x17001961 RID: 6497
		// (get) Token: 0x0600781F RID: 30751 RVA: 0x0023C17C File Offset: 0x0023C17C
		// (set) Token: 0x06007820 RID: 30752 RVA: 0x0023C194 File Offset: 0x0023C194
		public ushort e_oeminfo
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 38U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 38U), value);
			}
		}

		// Token: 0x17001962 RID: 6498
		// (get) Token: 0x06007821 RID: 30753 RVA: 0x0023C1AC File Offset: 0x0023C1AC
		// (set) Token: 0x06007822 RID: 30754 RVA: 0x0023C2B8 File Offset: 0x0023C2B8
		public ushort[] e_res2
		{
			get
			{
				return new ushort[]
				{
					this.Buff.BytesToUInt16((ulong)(this.Offset + 40U)),
					this.Buff.BytesToUInt16((ulong)(this.Offset + 42U)),
					this.Buff.BytesToUInt16((ulong)(this.Offset + 44U)),
					this.Buff.BytesToUInt16((ulong)(this.Offset + 46U)),
					this.Buff.BytesToUInt16((ulong)(this.Offset + 48U)),
					this.Buff.BytesToUInt16((ulong)(this.Offset + 50U)),
					this.Buff.BytesToUInt16((ulong)(this.Offset + 52U)),
					this.Buff.BytesToUInt16((ulong)(this.Offset + 54U)),
					this.Buff.BytesToUInt16((ulong)(this.Offset + 56U)),
					this.Buff.BytesToUInt16((ulong)(this.Offset + 58U))
				};
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 40U), value[0]);
				this.Buff.SetUInt16((ulong)(this.Offset + 42U), value[1]);
				this.Buff.SetUInt16((ulong)(this.Offset + 44U), value[2]);
				this.Buff.SetUInt16((ulong)(this.Offset + 46U), value[3]);
				this.Buff.SetUInt16((ulong)(this.Offset + 48U), value[4]);
				this.Buff.SetUInt16((ulong)(this.Offset + 50U), value[5]);
				this.Buff.SetUInt16((ulong)(this.Offset + 52U), value[6]);
				this.Buff.SetUInt16((ulong)(this.Offset + 54U), value[7]);
				this.Buff.SetUInt16((ulong)(this.Offset + 56U), value[8]);
				this.Buff.SetUInt16((ulong)(this.Offset + 58U), value[9]);
			}
		}

		// Token: 0x17001963 RID: 6499
		// (get) Token: 0x06007823 RID: 30755 RVA: 0x0023C3BC File Offset: 0x0023C3BC
		// (set) Token: 0x06007824 RID: 30756 RVA: 0x0023C3D4 File Offset: 0x0023C3D4
		public uint e_lfanew
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset + 60U);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset + 60U, value);
			}
		}
	}
}
