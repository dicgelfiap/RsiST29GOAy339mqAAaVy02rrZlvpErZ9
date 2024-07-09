using System;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BB6 RID: 2998
	[ComVisible(true)]
	public class IMAGE_TLS_DIRECTORY : AbstractStructure
	{
		// Token: 0x0600791C RID: 31004 RVA: 0x0023E380 File Offset: 0x0023E380
		public IMAGE_TLS_DIRECTORY(byte[] buff, uint offset, bool is64Bit) : base(buff, offset)
		{
			this._is64Bit = is64Bit;
		}

		// Token: 0x170019DB RID: 6619
		// (get) Token: 0x0600791D RID: 31005 RVA: 0x0023E394 File Offset: 0x0023E394
		// (set) Token: 0x0600791E RID: 31006 RVA: 0x0023E3C8 File Offset: 0x0023E3C8
		public ulong StartAddressOfRawData
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
				if (this._is64Bit)
				{
					this.Buff.SetUInt64((ulong)this.Offset, value);
					return;
				}
				this.Buff.SetUInt32(this.Offset, (uint)value);
			}
		}

		// Token: 0x170019DC RID: 6620
		// (get) Token: 0x0600791F RID: 31007 RVA: 0x0023E3FC File Offset: 0x0023E3FC
		// (set) Token: 0x06007920 RID: 31008 RVA: 0x0023E434 File Offset: 0x0023E434
		public ulong EndAddressOfRawData
		{
			get
			{
				if (!this._is64Bit)
				{
					return (ulong)this.Buff.BytesToUInt32(this.Offset + 4U);
				}
				return this.Buff.BytesToUInt64((ulong)(this.Offset + 8U));
			}
			set
			{
				if (this._is64Bit)
				{
					this.Buff.SetUInt64((ulong)(this.Offset + 8U), value);
					return;
				}
				this.Buff.SetUInt32(this.Offset + 4U, (uint)value);
			}
		}

		// Token: 0x170019DD RID: 6621
		// (get) Token: 0x06007921 RID: 31009 RVA: 0x0023E46C File Offset: 0x0023E46C
		// (set) Token: 0x06007922 RID: 31010 RVA: 0x0023E4A4 File Offset: 0x0023E4A4
		public ulong AddressOfIndex
		{
			get
			{
				if (!this._is64Bit)
				{
					return (ulong)this.Buff.BytesToUInt32(this.Offset + 8U);
				}
				return this.Buff.BytesToUInt64((ulong)(this.Offset + 16U));
			}
			set
			{
				if (this._is64Bit)
				{
					this.Buff.SetUInt64((ulong)(this.Offset + 16U), value);
					return;
				}
				this.Buff.SetUInt32(this.Offset + 8U, (uint)value);
			}
		}

		// Token: 0x170019DE RID: 6622
		// (get) Token: 0x06007923 RID: 31011 RVA: 0x0023E4E0 File Offset: 0x0023E4E0
		// (set) Token: 0x06007924 RID: 31012 RVA: 0x0023E518 File Offset: 0x0023E518
		public ulong AddressOfCallBacks
		{
			get
			{
				if (!this._is64Bit)
				{
					return (ulong)this.Buff.BytesToUInt32(this.Offset + 12U);
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
				this.Buff.SetUInt32(this.Offset + 12U, (uint)value);
			}
		}

		// Token: 0x170019DF RID: 6623
		// (get) Token: 0x06007925 RID: 31013 RVA: 0x0023E554 File Offset: 0x0023E554
		// (set) Token: 0x06007926 RID: 31014 RVA: 0x0023E58C File Offset: 0x0023E58C
		public uint SizeOfZeroFill
		{
			get
			{
				if (!this._is64Bit)
				{
					return this.Buff.BytesToUInt32(this.Offset + 16U);
				}
				return this.Buff.BytesToUInt32(this.Offset + 32U);
			}
			set
			{
				if (this._is64Bit)
				{
					this.Buff.SetUInt32(this.Offset + 32U, value);
					return;
				}
				this.Buff.SetUInt32(this.Offset + 16U, value);
			}
		}

		// Token: 0x170019E0 RID: 6624
		// (get) Token: 0x06007927 RID: 31015 RVA: 0x0023E5C4 File Offset: 0x0023E5C4
		// (set) Token: 0x06007928 RID: 31016 RVA: 0x0023E5FC File Offset: 0x0023E5FC
		public uint Characteristics
		{
			get
			{
				if (!this._is64Bit)
				{
					return this.Buff.BytesToUInt32(this.Offset + 20U);
				}
				return this.Buff.BytesToUInt32(this.Offset + 36U);
			}
			set
			{
				if (this._is64Bit)
				{
					this.Buff.SetUInt32(this.Offset + 36U, value);
					return;
				}
				this.Buff.SetUInt32(this.Offset + 20U, value);
			}
		}

		// Token: 0x170019E1 RID: 6625
		// (get) Token: 0x06007929 RID: 31017 RVA: 0x0023E634 File Offset: 0x0023E634
		// (set) Token: 0x0600792A RID: 31018 RVA: 0x0023E63C File Offset: 0x0023E63C
		public IMAGE_TLS_CALLBACK[] TlsCallbacks { get; set; }

		// Token: 0x04003A58 RID: 14936
		private readonly bool _is64Bit;
	}
}
