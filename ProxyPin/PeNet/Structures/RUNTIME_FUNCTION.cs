using System;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BC3 RID: 3011
	[ComVisible(true)]
	public class RUNTIME_FUNCTION : AbstractStructure
	{
		// Token: 0x0600797D RID: 31101 RVA: 0x0023F7E8 File Offset: 0x0023F7E8
		public RUNTIME_FUNCTION(byte[] buff, uint offset, IMAGE_SECTION_HEADER[] sh) : base(buff, offset)
		{
			this._sectionHeaders = sh;
		}

		// Token: 0x17001A06 RID: 6662
		// (get) Token: 0x0600797E RID: 31102 RVA: 0x0023F7FC File Offset: 0x0023F7FC
		// (set) Token: 0x0600797F RID: 31103 RVA: 0x0023F810 File Offset: 0x0023F810
		public uint FunctionStart
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

		// Token: 0x17001A07 RID: 6663
		// (get) Token: 0x06007980 RID: 31104 RVA: 0x0023F824 File Offset: 0x0023F824
		// (set) Token: 0x06007981 RID: 31105 RVA: 0x0023F83C File Offset: 0x0023F83C
		public uint FunctionEnd
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

		// Token: 0x17001A08 RID: 6664
		// (get) Token: 0x06007982 RID: 31106 RVA: 0x0023F854 File Offset: 0x0023F854
		// (set) Token: 0x06007983 RID: 31107 RVA: 0x0023F86C File Offset: 0x0023F86C
		public uint UnwindInfo
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

		// Token: 0x17001A09 RID: 6665
		// (get) Token: 0x06007984 RID: 31108 RVA: 0x0023F884 File Offset: 0x0023F884
		public UNWIND_INFO ResolvedUnwindInfo
		{
			get
			{
				if (this._resolvedUnwindInfo != null)
				{
					return this._resolvedUnwindInfo;
				}
				this._resolvedUnwindInfo = this.GetUnwindInfo(this._sectionHeaders);
				return this._resolvedUnwindInfo;
			}
		}

		// Token: 0x06007985 RID: 31109 RVA: 0x0023F8B0 File Offset: 0x0023F8B0
		private UNWIND_INFO GetUnwindInfo(IMAGE_SECTION_HEADER[] sh)
		{
			uint rva = ((this.UnwindInfo & 1U) == 1U) ? (this.UnwindInfo & 65534U) : this.UnwindInfo;
			return new UNWIND_INFO(this.Buff, rva.RVAtoFileMapping(sh));
		}

		// Token: 0x04003A69 RID: 14953
		private UNWIND_INFO _resolvedUnwindInfo;

		// Token: 0x04003A6A RID: 14954
		private readonly IMAGE_SECTION_HEADER[] _sectionHeaders;
	}
}
