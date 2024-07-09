using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace dnlib.IO
{
	// Token: 0x0200076D RID: 1901
	[DebuggerDisplay("O:{startOffset} L:{size} {GetType().Name}")]
	[ComVisible(true)]
	public class FileSection : IFileSection
	{
		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x060042A8 RID: 17064 RVA: 0x00165D7C File Offset: 0x00165D7C
		public FileOffset StartOffset
		{
			get
			{
				return this.startOffset;
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x060042A9 RID: 17065 RVA: 0x00165D84 File Offset: 0x00165D84
		public FileOffset EndOffset
		{
			get
			{
				return this.startOffset + this.size;
			}
		}

		// Token: 0x060042AA RID: 17066 RVA: 0x00165D94 File Offset: 0x00165D94
		protected void SetStartOffset(ref DataReader reader)
		{
			this.startOffset = (FileOffset)reader.CurrentOffset;
		}

		// Token: 0x060042AB RID: 17067 RVA: 0x00165DA4 File Offset: 0x00165DA4
		protected void SetEndoffset(ref DataReader reader)
		{
			this.size = reader.CurrentOffset - (uint)this.startOffset;
		}

		// Token: 0x0400238E RID: 9102
		protected FileOffset startOffset;

		// Token: 0x0400238F RID: 9103
		protected uint size;
	}
}
