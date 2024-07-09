using System;
using System.Reflection;
using System.Runtime.InteropServices;
using dnlib.DotNet.Writer;

namespace dnlib.DotNet
{
	// Token: 0x020007E6 RID: 2022
	[ComVisible(true)]
	public sealed class DummyLogger : ILogger
	{
		// Token: 0x060048D0 RID: 18640 RVA: 0x001771A0 File Offset: 0x001771A0
		private DummyLogger()
		{
		}

		// Token: 0x060048D1 RID: 18641 RVA: 0x001771A8 File Offset: 0x001771A8
		public DummyLogger(Type exceptionToThrow)
		{
			if (exceptionToThrow != null)
			{
				if (!exceptionToThrow.IsSubclassOf(typeof(Exception)))
				{
					throw new ArgumentException(string.Format("Not a System.Exception sub class: {0}", exceptionToThrow.GetType()));
				}
				this.ctor = exceptionToThrow.GetConstructor(new Type[]
				{
					typeof(string)
				});
				if (this.ctor == null)
				{
					throw new ArgumentException(string.Format("Exception type {0} doesn't have a public constructor that takes a string as the only argument", exceptionToThrow.GetType()));
				}
			}
		}

		// Token: 0x060048D2 RID: 18642 RVA: 0x00177230 File Offset: 0x00177230
		public void Log(object sender, LoggerEvent loggerEvent, string format, params object[] args)
		{
			if (loggerEvent == LoggerEvent.Error && this.ctor != null)
			{
				throw (Exception)this.ctor.Invoke(new object[]
				{
					string.Format(format, args)
				});
			}
		}

		// Token: 0x060048D3 RID: 18643 RVA: 0x00177268 File Offset: 0x00177268
		public bool IgnoresEvent(LoggerEvent loggerEvent)
		{
			return this.ctor == null || loggerEvent > LoggerEvent.Error;
		}

		// Token: 0x04002517 RID: 9495
		private ConstructorInfo ctor;

		// Token: 0x04002518 RID: 9496
		public static readonly DummyLogger NoThrowInstance = new DummyLogger();

		// Token: 0x04002519 RID: 9497
		public static readonly DummyLogger ThrowModuleWriterExceptionOnErrorInstance = new DummyLogger(typeof(ModuleWriterException));
	}
}
