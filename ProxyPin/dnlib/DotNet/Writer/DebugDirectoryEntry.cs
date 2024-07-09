using System;
using System.Runtime.InteropServices;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet.Writer
{
	// Token: 0x02000898 RID: 2200
	[ComVisible(true)]
	public sealed class DebugDirectoryEntry
	{
		// Token: 0x06005440 RID: 21568 RVA: 0x0019B254 File Offset: 0x0019B254
		public DebugDirectoryEntry(IChunk chunk)
		{
			this.Chunk = chunk;
		}

		// Token: 0x0400286F RID: 10351
		public IMAGE_DEBUG_DIRECTORY DebugDirectory;

		// Token: 0x04002870 RID: 10352
		public readonly IChunk Chunk;
	}
}
