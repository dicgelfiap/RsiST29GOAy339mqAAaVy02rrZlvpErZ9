using System;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BB3 RID: 2995
	[ComVisible(true)]
	public class IMAGE_SECTION_HEADER : AbstractStructure
	{
		// Token: 0x060078FA RID: 30970 RVA: 0x0023DF38 File Offset: 0x0023DF38
		public IMAGE_SECTION_HEADER(byte[] buff, uint offset, ulong imageBaseAddress) : base(buff, offset)
		{
			this.ImageBaseAddress = imageBaseAddress;
		}

		// Token: 0x170019CB RID: 6603
		// (get) Token: 0x060078FB RID: 30971 RVA: 0x0023DF4C File Offset: 0x0023DF4C
		public ulong ImageBaseAddress { get; }

		// Token: 0x170019CC RID: 6604
		// (get) Token: 0x060078FC RID: 30972 RVA: 0x0023DF54 File Offset: 0x0023DF54
		// (set) Token: 0x060078FD RID: 30973 RVA: 0x0023DFFC File Offset: 0x0023DFFC
		public byte[] Name
		{
			get
			{
				return new byte[]
				{
					this.Buff[(int)this.Offset],
					this.Buff[(int)(this.Offset + 1U)],
					this.Buff[(int)(this.Offset + 2U)],
					this.Buff[(int)(this.Offset + 3U)],
					this.Buff[(int)(this.Offset + 4U)],
					this.Buff[(int)(this.Offset + 5U)],
					this.Buff[(int)(this.Offset + 6U)],
					this.Buff[(int)(this.Offset + 7U)]
				};
			}
			set
			{
				this.Buff[(int)this.Offset] = value[0];
				this.Buff[(int)(this.Offset + 1U)] = value[1];
				this.Buff[(int)(this.Offset + 2U)] = value[2];
				this.Buff[(int)(this.Offset + 3U)] = value[3];
				this.Buff[(int)(this.Offset + 4U)] = value[4];
				this.Buff[(int)(this.Offset + 5U)] = value[5];
				this.Buff[(int)(this.Offset + 6U)] = value[6];
				this.Buff[(int)(this.Offset + 7U)] = value[7];
			}
		}

		// Token: 0x170019CD RID: 6605
		// (get) Token: 0x060078FE RID: 30974 RVA: 0x0023E09C File Offset: 0x0023E09C
		// (set) Token: 0x060078FF RID: 30975 RVA: 0x0023E0B4 File Offset: 0x0023E0B4
		public uint VirtualSize
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

		// Token: 0x170019CE RID: 6606
		// (get) Token: 0x06007900 RID: 30976 RVA: 0x0023E0CC File Offset: 0x0023E0CC
		// (set) Token: 0x06007901 RID: 30977 RVA: 0x0023E0E4 File Offset: 0x0023E0E4
		public uint VirtualAddress
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

		// Token: 0x170019CF RID: 6607
		// (get) Token: 0x06007902 RID: 30978 RVA: 0x0023E0FC File Offset: 0x0023E0FC
		// (set) Token: 0x06007903 RID: 30979 RVA: 0x0023E114 File Offset: 0x0023E114
		public uint SizeOfRawData
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

		// Token: 0x170019D0 RID: 6608
		// (get) Token: 0x06007904 RID: 30980 RVA: 0x0023E12C File Offset: 0x0023E12C
		// (set) Token: 0x06007905 RID: 30981 RVA: 0x0023E144 File Offset: 0x0023E144
		public uint PointerToRawData
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

		// Token: 0x170019D1 RID: 6609
		// (get) Token: 0x06007906 RID: 30982 RVA: 0x0023E15C File Offset: 0x0023E15C
		// (set) Token: 0x06007907 RID: 30983 RVA: 0x0023E174 File Offset: 0x0023E174
		public uint PointerToRelocations
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

		// Token: 0x170019D2 RID: 6610
		// (get) Token: 0x06007908 RID: 30984 RVA: 0x0023E18C File Offset: 0x0023E18C
		// (set) Token: 0x06007909 RID: 30985 RVA: 0x0023E1A4 File Offset: 0x0023E1A4
		public uint PointerToLinenumbers
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

		// Token: 0x170019D3 RID: 6611
		// (get) Token: 0x0600790A RID: 30986 RVA: 0x0023E1BC File Offset: 0x0023E1BC
		// (set) Token: 0x0600790B RID: 30987 RVA: 0x0023E1D4 File Offset: 0x0023E1D4
		public ushort NumberOfRelocations
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 32U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 32U), value);
			}
		}

		// Token: 0x170019D4 RID: 6612
		// (get) Token: 0x0600790C RID: 30988 RVA: 0x0023E1EC File Offset: 0x0023E1EC
		// (set) Token: 0x0600790D RID: 30989 RVA: 0x0023E204 File Offset: 0x0023E204
		public ushort NumberOfLinenumbers
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.Offset + 34U));
			}
			set
			{
				this.Buff.SetUInt16((ulong)(this.Offset + 34U), value);
			}
		}

		// Token: 0x170019D5 RID: 6613
		// (get) Token: 0x0600790E RID: 30990 RVA: 0x0023E21C File Offset: 0x0023E21C
		// (set) Token: 0x0600790F RID: 30991 RVA: 0x0023E234 File Offset: 0x0023E234
		public uint Characteristics
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
