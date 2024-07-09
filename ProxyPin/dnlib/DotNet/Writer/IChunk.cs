using System;
using System.Runtime.InteropServices;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Writer
{
	// Token: 0x0200089F RID: 2207
	[ComVisible(true)]
	public interface IChunk
	{
		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x0600547A RID: 21626
		FileOffset FileOffset { get; }

		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x0600547B RID: 21627
		RVA RVA { get; }

		// Token: 0x0600547C RID: 21628
		void SetOffset(FileOffset offset, RVA rva);

		// Token: 0x0600547D RID: 21629
		uint GetFileLength();

		// Token: 0x0600547E RID: 21630
		uint GetVirtualSize();

		// Token: 0x0600547F RID: 21631
		void WriteTo(DataWriter writer);
	}
}
