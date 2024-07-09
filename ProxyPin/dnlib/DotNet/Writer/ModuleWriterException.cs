using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008C8 RID: 2248
	[ComVisible(true)]
	[Serializable]
	public class ModuleWriterException : Exception
	{
		// Token: 0x0600572E RID: 22318 RVA: 0x001A9ABC File Offset: 0x001A9ABC
		public ModuleWriterException()
		{
		}

		// Token: 0x0600572F RID: 22319 RVA: 0x001A9AC4 File Offset: 0x001A9AC4
		public ModuleWriterException(string message) : base(message)
		{
		}

		// Token: 0x06005730 RID: 22320 RVA: 0x001A9AD0 File Offset: 0x001A9AD0
		public ModuleWriterException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06005731 RID: 22321 RVA: 0x001A9ADC File Offset: 0x001A9ADC
		protected ModuleWriterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
