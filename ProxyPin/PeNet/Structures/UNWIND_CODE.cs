using System;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BC4 RID: 3012
	[ComVisible(true)]
	public class UNWIND_CODE : AbstractStructure
	{
		// Token: 0x06007986 RID: 31110 RVA: 0x0023F8FC File Offset: 0x0023F8FC
		public UNWIND_CODE(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x17001A0A RID: 6666
		// (get) Token: 0x06007987 RID: 31111 RVA: 0x0023F908 File Offset: 0x0023F908
		// (set) Token: 0x06007988 RID: 31112 RVA: 0x0023F918 File Offset: 0x0023F918
		public byte CodeOffset
		{
			get
			{
				return this.Buff[(int)this.Offset];
			}
			set
			{
				this.Buff[(int)this.Offset] = value;
			}
		}

		// Token: 0x17001A0B RID: 6667
		// (get) Token: 0x06007989 RID: 31113 RVA: 0x0023F928 File Offset: 0x0023F928
		public byte UnwindOp
		{
			get
			{
				return (byte)(this.Buff[(int)(this.Offset + 1U)] >> 4);
			}
		}

		// Token: 0x17001A0C RID: 6668
		// (get) Token: 0x0600798A RID: 31114 RVA: 0x0023F93C File Offset: 0x0023F93C
		public byte Opinfo
		{
			get
			{
				return this.Buff[(int)(this.Offset + 1U)] & 15;
			}
		}

		// Token: 0x17001A0D RID: 6669
		// (get) Token: 0x0600798B RID: 31115 RVA: 0x0023F954 File Offset: 0x0023F954
		// (set) Token: 0x0600798C RID: 31116 RVA: 0x0023F96C File Offset: 0x0023F96C
		public ushort FrameOffset
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
	}
}
