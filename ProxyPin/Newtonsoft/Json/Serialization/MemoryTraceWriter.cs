using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000AFA RID: 2810
	[NullableContext(1)]
	[Nullable(0)]
	public class MemoryTraceWriter : ITraceWriter
	{
		// Token: 0x17001758 RID: 5976
		// (get) Token: 0x0600703E RID: 28734 RVA: 0x00220A7C File Offset: 0x00220A7C
		// (set) Token: 0x0600703F RID: 28735 RVA: 0x00220A84 File Offset: 0x00220A84
		public TraceLevel LevelFilter { get; set; }

		// Token: 0x06007040 RID: 28736 RVA: 0x00220A90 File Offset: 0x00220A90
		public MemoryTraceWriter()
		{
			this.LevelFilter = TraceLevel.Verbose;
			this._traceMessages = new Queue<string>();
			this._lock = new object();
		}

		// Token: 0x06007041 RID: 28737 RVA: 0x00220AB8 File Offset: 0x00220AB8
		public void Trace(TraceLevel level, string message, [Nullable(2)] Exception ex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff", CultureInfo.InvariantCulture));
			stringBuilder.Append(" ");
			stringBuilder.Append(level.ToString("g"));
			stringBuilder.Append(" ");
			stringBuilder.Append(message);
			string item = stringBuilder.ToString();
			object @lock = this._lock;
			lock (@lock)
			{
				if (this._traceMessages.Count >= 1000)
				{
					this._traceMessages.Dequeue();
				}
				this._traceMessages.Enqueue(item);
			}
		}

		// Token: 0x06007042 RID: 28738 RVA: 0x00220B88 File Offset: 0x00220B88
		public IEnumerable<string> GetTraceMessages()
		{
			return this._traceMessages;
		}

		// Token: 0x06007043 RID: 28739 RVA: 0x00220B90 File Offset: 0x00220B90
		public override string ToString()
		{
			object @lock = this._lock;
			string result;
			lock (@lock)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string value in this._traceMessages)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.AppendLine();
					}
					stringBuilder.Append(value);
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x040037C1 RID: 14273
		private readonly Queue<string> _traceMessages;

		// Token: 0x040037C2 RID: 14274
		private readonly object _lock;
	}
}
