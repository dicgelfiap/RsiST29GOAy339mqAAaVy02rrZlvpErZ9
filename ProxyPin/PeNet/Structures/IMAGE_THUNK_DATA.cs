using System;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BB4 RID: 2996
	[ComVisible(true)]
	public class IMAGE_THUNK_DATA : AbstractStructure
	{
		// Token: 0x06007910 RID: 30992 RVA: 0x0023E24C File Offset: 0x0023E24C
		public IMAGE_THUNK_DATA(byte[] buff, uint offset, bool is64Bit) : base(buff, offset)
		{
			this._is64Bit = is64Bit;
		}

		// Token: 0x170019D6 RID: 6614
		// (get) Token: 0x06007911 RID: 30993 RVA: 0x0023E260 File Offset: 0x0023E260
		// (set) Token: 0x06007912 RID: 30994 RVA: 0x0023E294 File Offset: 0x0023E294
		public ulong AddressOfData
		{
			get
			{
				if (!this._is64Bit)
				{
					return (ulong)this.Buff.BytesToUInt32(this.Offset);
				}
				return this.Buff.BytesToUInt64((ulong)this.Offset);
			}
			set
			{
				if (!this._is64Bit)
				{
					this.Buff.SetUInt32(this.Offset, (uint)value);
					return;
				}
				this.Buff.SetUInt64((ulong)this.Offset, value);
			}
		}

		// Token: 0x170019D7 RID: 6615
		// (get) Token: 0x06007913 RID: 30995 RVA: 0x0023E2C8 File Offset: 0x0023E2C8
		// (set) Token: 0x06007914 RID: 30996 RVA: 0x0023E2D0 File Offset: 0x0023E2D0
		public ulong Ordinal
		{
			get
			{
				return this.AddressOfData;
			}
			set
			{
				this.AddressOfData = value;
			}
		}

		// Token: 0x170019D8 RID: 6616
		// (get) Token: 0x06007915 RID: 30997 RVA: 0x0023E2DC File Offset: 0x0023E2DC
		// (set) Token: 0x06007916 RID: 30998 RVA: 0x0023E2E4 File Offset: 0x0023E2E4
		public ulong ForwarderString
		{
			get
			{
				return this.AddressOfData;
			}
			set
			{
				this.AddressOfData = value;
			}
		}

		// Token: 0x170019D9 RID: 6617
		// (get) Token: 0x06007917 RID: 30999 RVA: 0x0023E2F0 File Offset: 0x0023E2F0
		// (set) Token: 0x06007918 RID: 31000 RVA: 0x0023E2F8 File Offset: 0x0023E2F8
		public ulong Function
		{
			get
			{
				return this.AddressOfData;
			}
			set
			{
				this.AddressOfData = value;
			}
		}

		// Token: 0x04003A56 RID: 14934
		private readonly bool _is64Bit;
	}
}
