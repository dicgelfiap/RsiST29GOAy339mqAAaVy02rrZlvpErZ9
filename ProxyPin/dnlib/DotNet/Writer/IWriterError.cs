using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008A7 RID: 2215
	[ComVisible(true)]
	public interface IWriterError
	{
		// Token: 0x060054B7 RID: 21687
		void Error(string message);
	}
}
