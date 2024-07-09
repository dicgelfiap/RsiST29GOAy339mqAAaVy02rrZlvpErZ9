using System;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BAD RID: 2989
	[ComVisible(true)]
	public class IMAGE_NT_HEADERS : AbstractStructure
	{
		// Token: 0x0600788D RID: 30861 RVA: 0x0023D230 File Offset: 0x0023D230
		public IMAGE_NT_HEADERS(byte[] buff, uint offset, bool is64Bit) : base(buff, offset)
		{
			this.FileHeader = new IMAGE_FILE_HEADER(buff, offset + 4U);
			this.OptionalHeader = new IMAGE_OPTIONAL_HEADER(buff, offset + 24U, is64Bit);
		}

		// Token: 0x17001996 RID: 6550
		// (get) Token: 0x0600788E RID: 30862 RVA: 0x0023D25C File Offset: 0x0023D25C
		// (set) Token: 0x0600788F RID: 30863 RVA: 0x0023D270 File Offset: 0x0023D270
		public uint Signature
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

		// Token: 0x04003A4D RID: 14925
		public readonly IMAGE_FILE_HEADER FileHeader;

		// Token: 0x04003A4E RID: 14926
		public readonly IMAGE_OPTIONAL_HEADER OptionalHeader;
	}
}
