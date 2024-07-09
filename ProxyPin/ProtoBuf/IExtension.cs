using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ProtoBuf
{
	// Token: 0x02000C27 RID: 3111
	[ComVisible(true)]
	public interface IExtension
	{
		// Token: 0x06007BAB RID: 31659
		Stream BeginAppend();

		// Token: 0x06007BAC RID: 31660
		void EndAppend(Stream stream, bool commit);

		// Token: 0x06007BAD RID: 31661
		Stream BeginQuery();

		// Token: 0x06007BAE RID: 31662
		void EndQuery(Stream stream);

		// Token: 0x06007BAF RID: 31663
		int GetLength();
	}
}
