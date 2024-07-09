using System;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BAC RID: 2988
	[ComVisible(true)]
	public class IMAGE_LOAD_CONFIG_DIRECTORY : AbstractStructure
	{
		// Token: 0x0600785A RID: 30810 RVA: 0x0023C89C File Offset: 0x0023C89C
		public IMAGE_LOAD_CONFIG_DIRECTORY(byte[] buff, uint offset, bool is64Bit) : base(buff, offset)
		{
			this._is64Bit = is64Bit;
		}

		// Token: 0x1700197D RID: 6525
		// (get) Token: 0x0600785B RID: 30811 RVA: 0x0023C8B0 File Offset: 0x0023C8B0
		// (set) Token: 0x0600785C RID: 30812 RVA: 0x0023C8C4 File Offset: 0x0023C8C4
		public uint Size
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

		// Token: 0x1700197E RID: 6526
		// (get) Token: 0x0600785D RID: 30813 RVA: 0x0023C8D8 File Offset: 0x0023C8D8
		// (set) Token: 0x0600785E RID: 30814 RVA: 0x0023C8F0 File Offset: 0x0023C8F0
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

		// Token: 0x1700197F RID: 6527
		// (get) Token: 0x0600785F RID: 30815 RVA: 0x0023C908 File Offset: 0x0023C908
		// (set) Token: 0x06007860 RID: 30816 RVA: 0x0023C920 File Offset: 0x0023C920
		public ushort MajorVesion
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

		// Token: 0x17001980 RID: 6528
		// (get) Token: 0x06007861 RID: 30817 RVA: 0x0023C938 File Offset: 0x0023C938
		// (set) Token: 0x06007862 RID: 30818 RVA: 0x0023C950 File Offset: 0x0023C950
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

		// Token: 0x17001981 RID: 6529
		// (get) Token: 0x06007863 RID: 30819 RVA: 0x0023C968 File Offset: 0x0023C968
		// (set) Token: 0x06007864 RID: 30820 RVA: 0x0023C980 File Offset: 0x0023C980
		public uint GlobalFlagsClear
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

		// Token: 0x17001982 RID: 6530
		// (get) Token: 0x06007865 RID: 30821 RVA: 0x0023C998 File Offset: 0x0023C998
		// (set) Token: 0x06007866 RID: 30822 RVA: 0x0023C9B0 File Offset: 0x0023C9B0
		public uint GlobalFlagsSet
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

		// Token: 0x17001983 RID: 6531
		// (get) Token: 0x06007867 RID: 30823 RVA: 0x0023C9C8 File Offset: 0x0023C9C8
		// (set) Token: 0x06007868 RID: 30824 RVA: 0x0023C9E0 File Offset: 0x0023C9E0
		public uint CriticalSectionDefaultTimeout
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

		// Token: 0x17001984 RID: 6532
		// (get) Token: 0x06007869 RID: 30825 RVA: 0x0023C9F8 File Offset: 0x0023C9F8
		// (set) Token: 0x0600786A RID: 30826 RVA: 0x0023CA30 File Offset: 0x0023CA30
		public ulong DeCommitFreeBlockThreshold
		{
			get
			{
				if (!this._is64Bit)
				{
					return (ulong)this.Buff.BytesToUInt32(this.Offset + 24U);
				}
				return this.Buff.BytesToUInt64((ulong)(this.Offset + 24U));
			}
			set
			{
				if (this._is64Bit)
				{
					this.Buff.SetUInt64((ulong)(this.Offset + 24U), value);
					return;
				}
				this.Buff.SetUInt32(this.Offset + 24U, (uint)value);
			}
		}

		// Token: 0x17001985 RID: 6533
		// (get) Token: 0x0600786B RID: 30827 RVA: 0x0023CA6C File Offset: 0x0023CA6C
		// (set) Token: 0x0600786C RID: 30828 RVA: 0x0023CAA4 File Offset: 0x0023CAA4
		public ulong DeCommitTotalFreeThreshold
		{
			get
			{
				if (!this._is64Bit)
				{
					return (ulong)this.Buff.BytesToUInt32(this.Offset + 28U);
				}
				return this.Buff.BytesToUInt64((ulong)(this.Offset + 32U));
			}
			set
			{
				if (this._is64Bit)
				{
					this.Buff.SetUInt64((ulong)(this.Offset + 32U), value);
					return;
				}
				this.Buff.SetUInt32(this.Offset + 28U, (uint)value);
			}
		}

		// Token: 0x17001986 RID: 6534
		// (get) Token: 0x0600786D RID: 30829 RVA: 0x0023CAE0 File Offset: 0x0023CAE0
		// (set) Token: 0x0600786E RID: 30830 RVA: 0x0023CB18 File Offset: 0x0023CB18
		public ulong LockPrefixTable
		{
			get
			{
				if (!this._is64Bit)
				{
					return (ulong)this.Buff.BytesToUInt32(this.Offset + 32U);
				}
				return this.Buff.BytesToUInt64((ulong)(this.Offset + 40U));
			}
			set
			{
				if (this._is64Bit)
				{
					this.Buff.SetUInt64((ulong)(this.Offset + 40U), value);
					return;
				}
				this.Buff.SetUInt32(this.Offset + 32U, (uint)value);
			}
		}

		// Token: 0x17001987 RID: 6535
		// (get) Token: 0x0600786F RID: 30831 RVA: 0x0023CB54 File Offset: 0x0023CB54
		// (set) Token: 0x06007870 RID: 30832 RVA: 0x0023CB8C File Offset: 0x0023CB8C
		public ulong MaximumAllocationSize
		{
			get
			{
				if (!this._is64Bit)
				{
					return (ulong)this.Buff.BytesToUInt32(this.Offset + 36U);
				}
				return this.Buff.BytesToUInt64((ulong)(this.Offset + 48U));
			}
			set
			{
				if (this._is64Bit)
				{
					this.Buff.SetUInt64((ulong)(this.Offset + 48U), value);
					return;
				}
				this.Buff.SetUInt32(this.Offset + 36U, (uint)value);
			}
		}

		// Token: 0x17001988 RID: 6536
		// (get) Token: 0x06007871 RID: 30833 RVA: 0x0023CBC8 File Offset: 0x0023CBC8
		// (set) Token: 0x06007872 RID: 30834 RVA: 0x0023CC00 File Offset: 0x0023CC00
		public ulong VirtualMemoryThershold
		{
			get
			{
				if (!this._is64Bit)
				{
					return (ulong)this.Buff.BytesToUInt32(this.Offset + 40U);
				}
				return this.Buff.BytesToUInt64((ulong)(this.Offset + 56U));
			}
			set
			{
				if (this._is64Bit)
				{
					this.Buff.SetUInt64((ulong)(this.Offset + 56U), value);
					return;
				}
				this.Buff.SetUInt32(this.Offset + 40U, (uint)value);
			}
		}

		// Token: 0x17001989 RID: 6537
		// (get) Token: 0x06007873 RID: 30835 RVA: 0x0023CC3C File Offset: 0x0023CC3C
		// (set) Token: 0x06007874 RID: 30836 RVA: 0x0023CC74 File Offset: 0x0023CC74
		public ulong ProcessAffinityMask
		{
			get
			{
				if (!this._is64Bit)
				{
					return (ulong)this.Buff.BytesToUInt32(this.Offset + 48U);
				}
				return this.Buff.BytesToUInt64((ulong)(this.Offset + 64U));
			}
			set
			{
				if (this._is64Bit)
				{
					this.Buff.SetUInt64((ulong)(this.Offset + 64U), value);
					return;
				}
				this.Buff.SetUInt32(this.Offset + 48U, (uint)value);
			}
		}

		// Token: 0x1700198A RID: 6538
		// (get) Token: 0x06007875 RID: 30837 RVA: 0x0023CCB0 File Offset: 0x0023CCB0
		// (set) Token: 0x06007876 RID: 30838 RVA: 0x0023CCE8 File Offset: 0x0023CCE8
		public uint ProcessHeapFlags
		{
			get
			{
				if (!this._is64Bit)
				{
					return this.Buff.BytesToUInt32(this.Offset + 44U);
				}
				return this.Buff.BytesToUInt32(this.Offset + 72U);
			}
			set
			{
				if (this._is64Bit)
				{
					this.Buff.SetUInt32(this.Offset + 72U, value);
					return;
				}
				this.Buff.SetUInt32(this.Offset + 44U, value);
			}
		}

		// Token: 0x1700198B RID: 6539
		// (get) Token: 0x06007877 RID: 30839 RVA: 0x0023CD20 File Offset: 0x0023CD20
		// (set) Token: 0x06007878 RID: 30840 RVA: 0x0023CD58 File Offset: 0x0023CD58
		public ushort CSDVersion
		{
			get
			{
				if (!this._is64Bit)
				{
					return this.Buff.BytesToUInt16((ulong)(this.Offset + 52U));
				}
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 76U));
			}
			set
			{
				if (this._is64Bit)
				{
					this.Buff.SetUInt16((ulong)(this.Offset + 76U), value);
					return;
				}
				this.Buff.SetUInt16((ulong)(this.Offset + 52U), value);
			}
		}

		// Token: 0x1700198C RID: 6540
		// (get) Token: 0x06007879 RID: 30841 RVA: 0x0023CD94 File Offset: 0x0023CD94
		// (set) Token: 0x0600787A RID: 30842 RVA: 0x0023CDCC File Offset: 0x0023CDCC
		public ushort Reserved1
		{
			get
			{
				if (!this._is64Bit)
				{
					return this.Buff.BytesToUInt16((ulong)(this.Offset + 54U));
				}
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 78U));
			}
			set
			{
				if (this._is64Bit)
				{
					this.Buff.SetUInt16((ulong)(this.Offset + 78U), value);
					return;
				}
				this.Buff.SetUInt16((ulong)(this.Offset + 54U), value);
			}
		}

		// Token: 0x1700198D RID: 6541
		// (get) Token: 0x0600787B RID: 30843 RVA: 0x0023CE08 File Offset: 0x0023CE08
		// (set) Token: 0x0600787C RID: 30844 RVA: 0x0023CE40 File Offset: 0x0023CE40
		public ulong EditList
		{
			get
			{
				if (!this._is64Bit)
				{
					return (ulong)this.Buff.BytesToUInt32(this.Offset + 56U);
				}
				return this.Buff.BytesToUInt64((ulong)(this.Offset + 80U));
			}
			set
			{
				if (this._is64Bit)
				{
					this.Buff.SetUInt64((ulong)(this.Offset + 80U), value);
					return;
				}
				this.Buff.SetUInt32(this.Offset + 56U, (uint)value);
			}
		}

		// Token: 0x1700198E RID: 6542
		// (get) Token: 0x0600787D RID: 30845 RVA: 0x0023CE7C File Offset: 0x0023CE7C
		// (set) Token: 0x0600787E RID: 30846 RVA: 0x0023CEB4 File Offset: 0x0023CEB4
		public ulong SecurityCoockie
		{
			get
			{
				if (!this._is64Bit)
				{
					return (ulong)this.Buff.BytesToUInt32(this.Offset + 60U);
				}
				return this.Buff.BytesToUInt64((ulong)(this.Offset + 88U));
			}
			set
			{
				if (this._is64Bit)
				{
					this.Buff.SetUInt64((ulong)(this.Offset + 88U), value);
					return;
				}
				this.Buff.SetUInt32(this.Offset + 60U, (uint)value);
			}
		}

		// Token: 0x1700198F RID: 6543
		// (get) Token: 0x0600787F RID: 30847 RVA: 0x0023CEF0 File Offset: 0x0023CEF0
		// (set) Token: 0x06007880 RID: 30848 RVA: 0x0023CF28 File Offset: 0x0023CF28
		public ulong SEHandlerTable
		{
			get
			{
				if (!this._is64Bit)
				{
					return (ulong)this.Buff.BytesToUInt32(this.Offset + 64U);
				}
				return this.Buff.BytesToUInt64((ulong)(this.Offset + 96U));
			}
			set
			{
				if (this._is64Bit)
				{
					this.Buff.SetUInt64((ulong)(this.Offset + 96U), value);
					return;
				}
				this.Buff.SetUInt32(this.Offset + 64U, (uint)value);
			}
		}

		// Token: 0x17001990 RID: 6544
		// (get) Token: 0x06007881 RID: 30849 RVA: 0x0023CF64 File Offset: 0x0023CF64
		// (set) Token: 0x06007882 RID: 30850 RVA: 0x0023CF9C File Offset: 0x0023CF9C
		public ulong SEHandlerCount
		{
			get
			{
				if (!this._is64Bit)
				{
					return (ulong)this.Buff.BytesToUInt32(this.Offset + 68U);
				}
				return this.Buff.BytesToUInt64((ulong)(this.Offset + 104U));
			}
			set
			{
				if (this._is64Bit)
				{
					this.Buff.SetUInt64((ulong)(this.Offset + 104U), value);
					return;
				}
				this.Buff.SetUInt32(this.Offset + 68U, (uint)value);
			}
		}

		// Token: 0x17001991 RID: 6545
		// (get) Token: 0x06007883 RID: 30851 RVA: 0x0023CFD8 File Offset: 0x0023CFD8
		// (set) Token: 0x06007884 RID: 30852 RVA: 0x0023D010 File Offset: 0x0023D010
		public ulong GuardCFCheckFunctionPointer
		{
			get
			{
				if (!this._is64Bit)
				{
					return (ulong)this.Buff.BytesToUInt32(this.Offset + 72U);
				}
				return this.Buff.BytesToUInt64((ulong)(this.Offset + 112U));
			}
			set
			{
				if (this._is64Bit)
				{
					this.Buff.SetUInt64((ulong)(this.Offset + 112U), value);
					return;
				}
				this.Buff.SetUInt32(this.Offset + 76U, (uint)value);
			}
		}

		// Token: 0x17001992 RID: 6546
		// (get) Token: 0x06007885 RID: 30853 RVA: 0x0023D04C File Offset: 0x0023D04C
		// (set) Token: 0x06007886 RID: 30854 RVA: 0x0023D084 File Offset: 0x0023D084
		public ulong Reserved2
		{
			get
			{
				if (!this._is64Bit)
				{
					return (ulong)this.Buff.BytesToUInt32(this.Offset + 76U);
				}
				return this.Buff.BytesToUInt64((ulong)(this.Offset + 120U));
			}
			set
			{
				if (this._is64Bit)
				{
					this.Buff.SetUInt64((ulong)(this.Offset + 120U), value);
					return;
				}
				this.Buff.SetUInt32(this.Offset + 76U, (uint)value);
			}
		}

		// Token: 0x17001993 RID: 6547
		// (get) Token: 0x06007887 RID: 30855 RVA: 0x0023D0C0 File Offset: 0x0023D0C0
		// (set) Token: 0x06007888 RID: 30856 RVA: 0x0023D0FC File Offset: 0x0023D0FC
		public ulong GuardCFFunctionTable
		{
			get
			{
				if (!this._is64Bit)
				{
					return (ulong)this.Buff.BytesToUInt32(this.Offset + 80U);
				}
				return this.Buff.BytesToUInt64((ulong)(this.Offset + 128U));
			}
			set
			{
				if (this._is64Bit)
				{
					this.Buff.SetUInt64((ulong)(this.Offset + 128U), value);
					return;
				}
				this.Buff.SetUInt32(this.Offset + 80U, (uint)value);
			}
		}

		// Token: 0x17001994 RID: 6548
		// (get) Token: 0x06007889 RID: 30857 RVA: 0x0023D13C File Offset: 0x0023D13C
		// (set) Token: 0x0600788A RID: 30858 RVA: 0x0023D178 File Offset: 0x0023D178
		public ulong GuardCFFunctionCount
		{
			get
			{
				if (!this._is64Bit)
				{
					return (ulong)this.Buff.BytesToUInt32(this.Offset + 84U);
				}
				return this.Buff.BytesToUInt64((ulong)(this.Offset + 136U));
			}
			set
			{
				if (this._is64Bit)
				{
					this.Buff.SetUInt64((ulong)(this.Offset + 136U), value);
					return;
				}
				this.Buff.SetUInt32(this.Offset + 84U, (uint)value);
			}
		}

		// Token: 0x17001995 RID: 6549
		// (get) Token: 0x0600788B RID: 30859 RVA: 0x0023D1B8 File Offset: 0x0023D1B8
		// (set) Token: 0x0600788C RID: 30860 RVA: 0x0023D1F4 File Offset: 0x0023D1F4
		public uint GuardFlags
		{
			get
			{
				if (!this._is64Bit)
				{
					return this.Buff.BytesToUInt32(this.Offset + 88U);
				}
				return this.Buff.BytesToUInt32(this.Offset + 144U);
			}
			set
			{
				if (this._is64Bit)
				{
					this.Buff.SetUInt32(this.Offset + 144U, value);
					return;
				}
				this.Buff.SetUInt32(this.Offset + 88U, value);
			}
		}

		// Token: 0x04003A4C RID: 14924
		private readonly bool _is64Bit;
	}
}
