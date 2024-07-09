using System;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BAE RID: 2990
	[ComVisible(true)]
	public class IMAGE_OPTIONAL_HEADER : AbstractStructure
	{
		// Token: 0x06007890 RID: 30864 RVA: 0x0023D284 File Offset: 0x0023D284
		public IMAGE_OPTIONAL_HEADER(byte[] buff, uint offset, bool is64Bit) : base(buff, offset)
		{
			this._is64Bit = is64Bit;
			this.DataDirectory = new IMAGE_DATA_DIRECTORY[16];
			for (uint num = 0U; num < 16U; num += 1U)
			{
				if (!this._is64Bit)
				{
					this.DataDirectory[(int)num] = new IMAGE_DATA_DIRECTORY(buff, offset + 96U + num * 8U);
				}
				else
				{
					this.DataDirectory[(int)num] = new IMAGE_DATA_DIRECTORY(buff, offset + 112U + num * 8U);
				}
			}
		}

		// Token: 0x17001997 RID: 6551
		// (get) Token: 0x06007891 RID: 30865 RVA: 0x0023D308 File Offset: 0x0023D308
		// (set) Token: 0x06007892 RID: 30866 RVA: 0x0023D31C File Offset: 0x0023D31C
		public ushort Magic
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

		// Token: 0x17001998 RID: 6552
		// (get) Token: 0x06007893 RID: 30867 RVA: 0x0023D334 File Offset: 0x0023D334
		// (set) Token: 0x06007894 RID: 30868 RVA: 0x0023D348 File Offset: 0x0023D348
		public byte MajorLinkerVersion
		{
			get
			{
				return this.Buff[(int)(this.Offset + 2U)];
			}
			set
			{
				this.Buff[(int)(this.Offset + 2U)] = value;
			}
		}

		// Token: 0x17001999 RID: 6553
		// (get) Token: 0x06007895 RID: 30869 RVA: 0x0023D35C File Offset: 0x0023D35C
		// (set) Token: 0x06007896 RID: 30870 RVA: 0x0023D370 File Offset: 0x0023D370
		public byte MinorLinkerVersion
		{
			get
			{
				return this.Buff[(int)(this.Offset + 3U)];
			}
			set
			{
				this.Buff[(int)(this.Offset + 3U)] = value;
			}
		}

		// Token: 0x1700199A RID: 6554
		// (get) Token: 0x06007897 RID: 30871 RVA: 0x0023D384 File Offset: 0x0023D384
		// (set) Token: 0x06007898 RID: 30872 RVA: 0x0023D39C File Offset: 0x0023D39C
		public uint SizeOfCode
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

		// Token: 0x1700199B RID: 6555
		// (get) Token: 0x06007899 RID: 30873 RVA: 0x0023D3B4 File Offset: 0x0023D3B4
		// (set) Token: 0x0600789A RID: 30874 RVA: 0x0023D3CC File Offset: 0x0023D3CC
		public uint SizeOfInitializedData
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

		// Token: 0x1700199C RID: 6556
		// (get) Token: 0x0600789B RID: 30875 RVA: 0x0023D3E4 File Offset: 0x0023D3E4
		// (set) Token: 0x0600789C RID: 30876 RVA: 0x0023D3FC File Offset: 0x0023D3FC
		public uint SizeOfUninitializedData
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

		// Token: 0x1700199D RID: 6557
		// (get) Token: 0x0600789D RID: 30877 RVA: 0x0023D414 File Offset: 0x0023D414
		// (set) Token: 0x0600789E RID: 30878 RVA: 0x0023D42C File Offset: 0x0023D42C
		public uint AddressOfEntryPoint
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

		// Token: 0x1700199E RID: 6558
		// (get) Token: 0x0600789F RID: 30879 RVA: 0x0023D444 File Offset: 0x0023D444
		// (set) Token: 0x060078A0 RID: 30880 RVA: 0x0023D45C File Offset: 0x0023D45C
		public uint BaseOfCode
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

		// Token: 0x1700199F RID: 6559
		// (get) Token: 0x060078A1 RID: 30881 RVA: 0x0023D474 File Offset: 0x0023D474
		// (set) Token: 0x060078A2 RID: 30882 RVA: 0x0023D498 File Offset: 0x0023D498
		public uint BaseOfData
		{
			get
			{
				if (!this._is64Bit)
				{
					return this.Buff.BytesToUInt32(this.Offset + 24U);
				}
				return 0U;
			}
			set
			{
				if (!this._is64Bit)
				{
					this.Buff.SetUInt32(this.Offset + 24U, value);
					return;
				}
				throw new Exception("IMAGE_OPTIONAL_HEADER->BaseOfCode does not exist in 64 bit applications.");
			}
		}

		// Token: 0x170019A0 RID: 6560
		// (get) Token: 0x060078A3 RID: 30883 RVA: 0x0023D4C8 File Offset: 0x0023D4C8
		// (set) Token: 0x060078A4 RID: 30884 RVA: 0x0023D500 File Offset: 0x0023D500
		public ulong ImageBase
		{
			get
			{
				if (!this._is64Bit)
				{
					return (ulong)this.Buff.BytesToUInt32(this.Offset + 28U);
				}
				return this.Buff.BytesToUInt64((ulong)(this.Offset + 24U));
			}
			set
			{
				if (!this._is64Bit)
				{
					this.Buff.SetUInt32(this.Offset + 28U, (uint)value);
					return;
				}
				this.Buff.SetUInt64((ulong)(this.Offset + 24U), value);
			}
		}

		// Token: 0x170019A1 RID: 6561
		// (get) Token: 0x060078A5 RID: 30885 RVA: 0x0023D53C File Offset: 0x0023D53C
		// (set) Token: 0x060078A6 RID: 30886 RVA: 0x0023D554 File Offset: 0x0023D554
		public uint SectionAlignment
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

		// Token: 0x170019A2 RID: 6562
		// (get) Token: 0x060078A7 RID: 30887 RVA: 0x0023D56C File Offset: 0x0023D56C
		// (set) Token: 0x060078A8 RID: 30888 RVA: 0x0023D584 File Offset: 0x0023D584
		public uint FileAlignment
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

		// Token: 0x170019A3 RID: 6563
		// (get) Token: 0x060078A9 RID: 30889 RVA: 0x0023D59C File Offset: 0x0023D59C
		// (set) Token: 0x060078AA RID: 30890 RVA: 0x0023D5B4 File Offset: 0x0023D5B4
		public ushort MajorOperatingSystemVersion
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 40U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 40U), value);
			}
		}

		// Token: 0x170019A4 RID: 6564
		// (get) Token: 0x060078AB RID: 30891 RVA: 0x0023D5CC File Offset: 0x0023D5CC
		// (set) Token: 0x060078AC RID: 30892 RVA: 0x0023D5E4 File Offset: 0x0023D5E4
		public ushort MinorOperatingSystemVersion
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 42U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 42U), value);
			}
		}

		// Token: 0x170019A5 RID: 6565
		// (get) Token: 0x060078AD RID: 30893 RVA: 0x0023D5FC File Offset: 0x0023D5FC
		// (set) Token: 0x060078AE RID: 30894 RVA: 0x0023D614 File Offset: 0x0023D614
		public ushort MajorImageVersion
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 44U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 44U), value);
			}
		}

		// Token: 0x170019A6 RID: 6566
		// (get) Token: 0x060078AF RID: 30895 RVA: 0x0023D62C File Offset: 0x0023D62C
		// (set) Token: 0x060078B0 RID: 30896 RVA: 0x0023D644 File Offset: 0x0023D644
		public ushort MinorImageVersion
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 46U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 46U), value);
			}
		}

		// Token: 0x170019A7 RID: 6567
		// (get) Token: 0x060078B1 RID: 30897 RVA: 0x0023D65C File Offset: 0x0023D65C
		// (set) Token: 0x060078B2 RID: 30898 RVA: 0x0023D674 File Offset: 0x0023D674
		public ushort MajorSubsystemVersion
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 48U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 48U), value);
			}
		}

		// Token: 0x170019A8 RID: 6568
		// (get) Token: 0x060078B3 RID: 30899 RVA: 0x0023D68C File Offset: 0x0023D68C
		// (set) Token: 0x060078B4 RID: 30900 RVA: 0x0023D6A4 File Offset: 0x0023D6A4
		public ushort MinorSubsystemVersion
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 50U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 50U), value);
			}
		}

		// Token: 0x170019A9 RID: 6569
		// (get) Token: 0x060078B5 RID: 30901 RVA: 0x0023D6BC File Offset: 0x0023D6BC
		// (set) Token: 0x060078B6 RID: 30902 RVA: 0x0023D6D4 File Offset: 0x0023D6D4
		public uint Win32VersionValue
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset + 52U);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset + 52U, value);
			}
		}

		// Token: 0x170019AA RID: 6570
		// (get) Token: 0x060078B7 RID: 30903 RVA: 0x0023D6EC File Offset: 0x0023D6EC
		// (set) Token: 0x060078B8 RID: 30904 RVA: 0x0023D704 File Offset: 0x0023D704
		public uint SizeOfImage
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset + 56U);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset + 56U, value);
			}
		}

		// Token: 0x170019AB RID: 6571
		// (get) Token: 0x060078B9 RID: 30905 RVA: 0x0023D71C File Offset: 0x0023D71C
		// (set) Token: 0x060078BA RID: 30906 RVA: 0x0023D734 File Offset: 0x0023D734
		public uint SizeOfHeaders
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

		// Token: 0x170019AC RID: 6572
		// (get) Token: 0x060078BB RID: 30907 RVA: 0x0023D74C File Offset: 0x0023D74C
		// (set) Token: 0x060078BC RID: 30908 RVA: 0x0023D764 File Offset: 0x0023D764
		public uint CheckSum
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset + 64U);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset + 64U, value);
			}
		}

		// Token: 0x170019AD RID: 6573
		// (get) Token: 0x060078BD RID: 30909 RVA: 0x0023D77C File Offset: 0x0023D77C
		// (set) Token: 0x060078BE RID: 30910 RVA: 0x0023D794 File Offset: 0x0023D794
		public ushort Subsystem
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 68U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 68U), value);
			}
		}

		// Token: 0x170019AE RID: 6574
		// (get) Token: 0x060078BF RID: 30911 RVA: 0x0023D7AC File Offset: 0x0023D7AC
		// (set) Token: 0x060078C0 RID: 30912 RVA: 0x0023D7C4 File Offset: 0x0023D7C4
		public ushort DllCharacteristics
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 70U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 70U), value);
			}
		}

		// Token: 0x170019AF RID: 6575
		// (get) Token: 0x060078C1 RID: 30913 RVA: 0x0023D7DC File Offset: 0x0023D7DC
		// (set) Token: 0x060078C2 RID: 30914 RVA: 0x0023D814 File Offset: 0x0023D814
		public ulong SizeOfStackReserve
		{
			get
			{
				if (!this._is64Bit)
				{
					return (ulong)this.Buff.BytesToUInt32(this.Offset + 72U);
				}
				return this.Buff.BytesToUInt64((ulong)(this.Offset + 72U));
			}
			set
			{
				if (!this._is64Bit)
				{
					this.Buff.SetUInt32(this.Offset + 72U, (uint)value);
					return;
				}
				this.Buff.SetUInt64((ulong)(this.Offset + 72U), value);
			}
		}

		// Token: 0x170019B0 RID: 6576
		// (get) Token: 0x060078C3 RID: 30915 RVA: 0x0023D850 File Offset: 0x0023D850
		// (set) Token: 0x060078C4 RID: 30916 RVA: 0x0023D888 File Offset: 0x0023D888
		public ulong SizeOfStackCommit
		{
			get
			{
				if (!this._is64Bit)
				{
					return (ulong)this.Buff.BytesToUInt32(this.Offset + 76U);
				}
				return this.Buff.BytesToUInt64((ulong)(this.Offset + 80U));
			}
			set
			{
				if (!this._is64Bit)
				{
					this.Buff.SetUInt32(this.Offset + 76U, (uint)value);
					return;
				}
				this.Buff.SetUInt64((ulong)(this.Offset + 80U), value);
			}
		}

		// Token: 0x170019B1 RID: 6577
		// (get) Token: 0x060078C5 RID: 30917 RVA: 0x0023D8C4 File Offset: 0x0023D8C4
		// (set) Token: 0x060078C6 RID: 30918 RVA: 0x0023D8FC File Offset: 0x0023D8FC
		public ulong SizeOfHeapReserve
		{
			get
			{
				if (!this._is64Bit)
				{
					return (ulong)this.Buff.BytesToUInt32(this.Offset + 80U);
				}
				return this.Buff.BytesToUInt64((ulong)(this.Offset + 88U));
			}
			set
			{
				if (!this._is64Bit)
				{
					this.Buff.SetUInt32(this.Offset + 80U, (uint)value);
					return;
				}
				this.Buff.SetUInt64((ulong)(this.Offset + 88U), value);
			}
		}

		// Token: 0x170019B2 RID: 6578
		// (get) Token: 0x060078C7 RID: 30919 RVA: 0x0023D938 File Offset: 0x0023D938
		// (set) Token: 0x060078C8 RID: 30920 RVA: 0x0023D970 File Offset: 0x0023D970
		public ulong SizeOfHeapCommit
		{
			get
			{
				if (!this._is64Bit)
				{
					return (ulong)this.Buff.BytesToUInt32(this.Offset + 84U);
				}
				return this.Buff.BytesToUInt64((ulong)(this.Offset + 96U));
			}
			set
			{
				if (!this._is64Bit)
				{
					this.Buff.SetUInt32(this.Offset + 84U, (uint)value);
					return;
				}
				this.Buff.SetUInt64((ulong)(this.Offset + 96U), value);
			}
		}

		// Token: 0x170019B3 RID: 6579
		// (get) Token: 0x060078C9 RID: 30921 RVA: 0x0023D9AC File Offset: 0x0023D9AC
		// (set) Token: 0x060078CA RID: 30922 RVA: 0x0023D9E4 File Offset: 0x0023D9E4
		public uint LoaderFlags
		{
			get
			{
				if (!this._is64Bit)
				{
					return this.Buff.BytesToUInt32(this.Offset + 88U);
				}
				return this.Buff.BytesToUInt32(this.Offset + 104U);
			}
			set
			{
				if (!this._is64Bit)
				{
					this.Buff.SetUInt32(this.Offset + 88U, value);
					return;
				}
				this.Buff.SetUInt32(this.Offset + 104U, value);
			}
		}

		// Token: 0x170019B4 RID: 6580
		// (get) Token: 0x060078CB RID: 30923 RVA: 0x0023DA1C File Offset: 0x0023DA1C
		// (set) Token: 0x060078CC RID: 30924 RVA: 0x0023DA54 File Offset: 0x0023DA54
		public uint NumberOfRvaAndSizes
		{
			get
			{
				if (!this._is64Bit)
				{
					return this.Buff.BytesToUInt32(this.Offset + 92U);
				}
				return this.Buff.BytesToUInt32(this.Offset + 108U);
			}
			set
			{
				if (!this._is64Bit)
				{
					this.Buff.SetUInt32(this.Offset + 92U, value);
					return;
				}
				this.Buff.SetUInt32(this.Offset + 108U, value);
			}
		}

		// Token: 0x04003A4F RID: 14927
		private readonly bool _is64Bit;

		// Token: 0x04003A50 RID: 14928
		public readonly IMAGE_DATA_DIRECTORY[] DataDirectory;
	}
}
