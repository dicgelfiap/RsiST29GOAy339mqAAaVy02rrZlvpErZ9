using System;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BB5 RID: 2997
	[ComVisible(true)]
	public class IMAGE_TLS_CALLBACK : AbstractStructure
	{
		// Token: 0x06007919 RID: 31001 RVA: 0x0023E304 File Offset: 0x0023E304
		public IMAGE_TLS_CALLBACK(byte[] buff, uint offset, bool is64Bit) : base(buff, offset)
		{
			this._is64Bit = is64Bit;
		}

		// Token: 0x170019DA RID: 6618
		// (get) Token: 0x0600791A RID: 31002 RVA: 0x0023E318 File Offset: 0x0023E318
		// (set) Token: 0x0600791B RID: 31003 RVA: 0x0023E34C File Offset: 0x0023E34C
		public ulong Callback
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

		// Token: 0x04003A57 RID: 14935
		private readonly bool _is64Bit;
	}
}
