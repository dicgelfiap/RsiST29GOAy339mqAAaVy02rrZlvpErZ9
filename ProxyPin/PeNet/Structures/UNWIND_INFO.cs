using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BC5 RID: 3013
	[ComVisible(true)]
	public class UNWIND_INFO : AbstractStructure
	{
		// Token: 0x0600798D RID: 31117 RVA: 0x0023F984 File Offset: 0x0023F984
		public UNWIND_INFO(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x17001A0E RID: 6670
		// (get) Token: 0x0600798E RID: 31118 RVA: 0x0023F998 File Offset: 0x0023F998
		public byte Version
		{
			get
			{
				return (byte)(this.Buff[(int)this.Offset] >> 5);
			}
		}

		// Token: 0x17001A0F RID: 6671
		// (get) Token: 0x0600798F RID: 31119 RVA: 0x0023F9AC File Offset: 0x0023F9AC
		public byte Flags
		{
			get
			{
				return this.Buff[(int)this.Offset] & 31;
			}
		}

		// Token: 0x17001A10 RID: 6672
		// (get) Token: 0x06007990 RID: 31120 RVA: 0x0023F9C0 File Offset: 0x0023F9C0
		// (set) Token: 0x06007991 RID: 31121 RVA: 0x0023F9D4 File Offset: 0x0023F9D4
		public byte SizeOfProlog
		{
			get
			{
				return this.Buff[(int)(this.Offset + 1U)];
			}
			set
			{
				this.Buff[(int)(this.Offset + 1U)] = value;
			}
		}

		// Token: 0x17001A11 RID: 6673
		// (get) Token: 0x06007992 RID: 31122 RVA: 0x0023F9E8 File Offset: 0x0023F9E8
		// (set) Token: 0x06007993 RID: 31123 RVA: 0x0023F9FC File Offset: 0x0023F9FC
		public byte CountOfCodes
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

		// Token: 0x17001A12 RID: 6674
		// (get) Token: 0x06007994 RID: 31124 RVA: 0x0023FA10 File Offset: 0x0023FA10
		public byte FrameRegister
		{
			get
			{
				return (byte)(this.Buff[(int)(this.Offset + 3U)] >> 4);
			}
		}

		// Token: 0x17001A13 RID: 6675
		// (get) Token: 0x06007995 RID: 31125 RVA: 0x0023FA24 File Offset: 0x0023FA24
		public byte FrameOffset
		{
			get
			{
				return this.Buff[(int)(this.Offset + 3U)] & 15;
			}
		}

		// Token: 0x17001A14 RID: 6676
		// (get) Token: 0x06007996 RID: 31126 RVA: 0x0023FA3C File Offset: 0x0023FA3C
		public UNWIND_CODE[] UnwindCode
		{
			get
			{
				return this.ParseUnwindCodes(this.Buff, this.Offset + 4U);
			}
		}

		// Token: 0x17001A15 RID: 6677
		// (get) Token: 0x06007997 RID: 31127 RVA: 0x0023FA54 File Offset: 0x0023FA54
		// (set) Token: 0x06007998 RID: 31128 RVA: 0x0023FA8C File Offset: 0x0023FA8C
		public uint ExceptionHandler
		{
			get
			{
				uint offset = (uint)((ulong)(this.Offset + 4U) + (ulong)((long)(this.sizeOfUnwindeCode * (int)this.CountOfCodes)));
				return this.Buff.BytesToUInt32(offset);
			}
			set
			{
				uint offset = (uint)((ulong)(this.Offset + 4U) + (ulong)((long)(this.sizeOfUnwindeCode * (int)this.CountOfCodes)));
				this.Buff.SetUInt32(offset, value);
			}
		}

		// Token: 0x17001A16 RID: 6678
		// (get) Token: 0x06007999 RID: 31129 RVA: 0x0023FAC4 File Offset: 0x0023FAC4
		// (set) Token: 0x0600799A RID: 31130 RVA: 0x0023FACC File Offset: 0x0023FACC
		public uint FunctionEntry
		{
			get
			{
				return this.ExceptionHandler;
			}
			set
			{
				this.ExceptionHandler = value;
			}
		}

		// Token: 0x17001A17 RID: 6679
		// (get) Token: 0x0600799B RID: 31131 RVA: 0x0023FAD8 File Offset: 0x0023FAD8
		public uint[] ExceptionData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600799C RID: 31132 RVA: 0x0023FADC File Offset: 0x0023FADC
		private UNWIND_CODE[] ParseUnwindCodes(byte[] buff, uint offset)
		{
			List<UNWIND_CODE> list = new List<UNWIND_CODE>();
			int i = 0;
			uint num = 2U;
			uint num2 = offset;
			while (i < (int)this.CountOfCodes)
			{
				UNWIND_CODE unwind_CODE = new UNWIND_CODE(buff, num2);
				num2 += num;
				switch (unwind_CODE.UnwindOp)
				{
				case 1:
					num2 += ((unwind_CODE.Opinfo == 0) ? 2U : 4U);
					break;
				case 4:
					num2 += 2U;
					break;
				case 5:
					num2 += 4U;
					break;
				case 8:
					num2 += 2U;
					break;
				case 9:
					num2 += 4U;
					break;
				}
				int num3;
				if ((unwind_CODE.UnwindOp == 1 && unwind_CODE.Opinfo == 0) || unwind_CODE.UnwindOp == 4 || unwind_CODE.UnwindOp == 8)
				{
					num3 = 2;
				}
				else if ((unwind_CODE.UnwindOp == 1 && unwind_CODE.Opinfo == 1) || unwind_CODE.UnwindOp == 5 || unwind_CODE.UnwindOp == 9)
				{
					num3 = 3;
				}
				else
				{
					num3 = 1;
				}
				i += num3;
				list.Add(unwind_CODE);
			}
			return list.ToArray();
		}

		// Token: 0x04003A6B RID: 14955
		private readonly int sizeOfUnwindeCode = 4;
	}
}
