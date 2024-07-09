using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000AD7 RID: 2775
	public class DiagnosticsTraceWriter : ITraceWriter
	{
		// Token: 0x170016D8 RID: 5848
		// (get) Token: 0x06006E86 RID: 28294 RVA: 0x00217BF0 File Offset: 0x00217BF0
		// (set) Token: 0x06006E87 RID: 28295 RVA: 0x00217BF8 File Offset: 0x00217BF8
		public TraceLevel LevelFilter { get; set; }

		// Token: 0x06006E88 RID: 28296 RVA: 0x00217C04 File Offset: 0x00217C04
		private TraceEventType GetTraceEventType(TraceLevel level)
		{
			switch (level)
			{
			case TraceLevel.Error:
				return TraceEventType.Error;
			case TraceLevel.Warning:
				return TraceEventType.Warning;
			case TraceLevel.Info:
				return TraceEventType.Information;
			case TraceLevel.Verbose:
				return TraceEventType.Verbose;
			default:
				throw new ArgumentOutOfRangeException("level");
			}
		}

		// Token: 0x06006E89 RID: 28297 RVA: 0x00217C38 File Offset: 0x00217C38
		[NullableContext(1)]
		public void Trace(TraceLevel level, string message, [Nullable(2)] Exception ex)
		{
			if (level == TraceLevel.Off)
			{
				return;
			}
			TraceEventCache eventCache = new TraceEventCache();
			TraceEventType traceEventType = this.GetTraceEventType(level);
			foreach (object obj in System.Diagnostics.Trace.Listeners)
			{
				TraceListener traceListener = (TraceListener)obj;
				if (!traceListener.IsThreadSafe)
				{
					TraceListener obj2 = traceListener;
					lock (obj2)
					{
						traceListener.TraceEvent(eventCache, "Newtonsoft.Json", traceEventType, 0, message);
						goto IL_7D;
					}
					goto IL_6E;
				}
				goto IL_6E;
				IL_7D:
				if (System.Diagnostics.Trace.AutoFlush)
				{
					traceListener.Flush();
					continue;
				}
				continue;
				IL_6E:
				traceListener.TraceEvent(eventCache, "Newtonsoft.Json", traceEventType, 0, message);
				goto IL_7D;
			}
		}
	}
}
