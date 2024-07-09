using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BA1 RID: 2977
	[ComVisible(true)]
	public class IMAGE_BASE_RELOCATION : AbstractStructure
	{
		// Token: 0x060077AE RID: 30638 RVA: 0x0023B4E0 File Offset: 0x0023B4E0
		public IMAGE_BASE_RELOCATION(byte[] buff, uint offset, uint relocSize) : base(buff, offset)
		{
			if (this.SizeOfBlock > relocSize)
			{
				throw new ArgumentOutOfRangeException("relocSize", "SizeOfBlock cannot be bigger than size of the Relocation Directory.");
			}
			if (this.SizeOfBlock < 8U)
			{
				throw new Exception("SizeOfBlock cannot be smaller than 8.");
			}
			this.ParseTypeOffsets();
		}

		// Token: 0x17001929 RID: 6441
		// (get) Token: 0x060077AF RID: 30639 RVA: 0x0023B534 File Offset: 0x0023B534
		// (set) Token: 0x060077B0 RID: 30640 RVA: 0x0023B548 File Offset: 0x0023B548
		public uint VirtualAddress
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

		// Token: 0x1700192A RID: 6442
		// (get) Token: 0x060077B1 RID: 30641 RVA: 0x0023B55C File Offset: 0x0023B55C
		// (set) Token: 0x060077B2 RID: 30642 RVA: 0x0023B574 File Offset: 0x0023B574
		public uint SizeOfBlock
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

		// Token: 0x1700192B RID: 6443
		// (get) Token: 0x060077B3 RID: 30643 RVA: 0x0023B58C File Offset: 0x0023B58C
		// (set) Token: 0x060077B4 RID: 30644 RVA: 0x0023B594 File Offset: 0x0023B594
		public IMAGE_BASE_RELOCATION.TypeOffset[] TypeOffsets { get; private set; }

		// Token: 0x060077B5 RID: 30645 RVA: 0x0023B5A0 File Offset: 0x0023B5A0
		private void ParseTypeOffsets()
		{
			List<IMAGE_BASE_RELOCATION.TypeOffset> list = new List<IMAGE_BASE_RELOCATION.TypeOffset>();
			for (uint num = 0U; num < (this.SizeOfBlock - 8U) / 2U; num += 1U)
			{
				list.Add(new IMAGE_BASE_RELOCATION.TypeOffset(this.Buff, this.Offset + 8U + num * 2U));
			}
			this.TypeOffsets = list.ToArray();
		}

		// Token: 0x0200115B RID: 4443
		public class TypeOffset
		{
			// Token: 0x060092F8 RID: 37624 RVA: 0x002C204C File Offset: 0x002C204C
			public TypeOffset(byte[] buff, uint offset)
			{
				this._buff = buff;
				this._offset = offset;
			}

			// Token: 0x17001E6C RID: 7788
			// (get) Token: 0x060092F9 RID: 37625 RVA: 0x002C2064 File Offset: 0x002C2064
			public byte Type
			{
				get
				{
					return (byte)(this._buff.BytesToUInt16((ulong)this._offset) >> 12);
				}
			}

			// Token: 0x17001E6D RID: 7789
			// (get) Token: 0x060092FA RID: 37626 RVA: 0x002C207C File Offset: 0x002C207C
			public ushort Offset
			{
				get
				{
					return this._buff.BytesToUInt16((ulong)this._offset) & 4095;
				}
			}

			// Token: 0x04004AF7 RID: 19191
			private readonly byte[] _buff;

			// Token: 0x04004AF8 RID: 19192
			private readonly uint _offset;
		}
	}
}
