using System;
using dnlib.PE;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008A0 RID: 2208
	internal interface IReuseChunk : IChunk
	{
		// Token: 0x06005480 RID: 21632
		bool CanReuse(RVA origRva, uint origSize);
	}
}
