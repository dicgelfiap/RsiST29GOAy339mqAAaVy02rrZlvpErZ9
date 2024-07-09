using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000AE0 RID: 2784
	[NullableContext(1)]
	public interface ITraceWriter
	{
		// Token: 0x170016E1 RID: 5857
		// (get) Token: 0x06006EA6 RID: 28326
		TraceLevel LevelFilter { get; }

		// Token: 0x06006EA7 RID: 28327
		void Trace(TraceLevel level, string message, [Nullable(2)] Exception ex);
	}
}
