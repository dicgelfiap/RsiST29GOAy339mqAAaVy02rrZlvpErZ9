using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007E5 RID: 2021
	[ComVisible(true)]
	public interface ILogger
	{
		// Token: 0x060048CE RID: 18638
		void Log(object sender, LoggerEvent loggerEvent, string format, params object[] args);

		// Token: 0x060048CF RID: 18639
		bool IgnoresEvent(LoggerEvent loggerEvent);
	}
}
