using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace dnlib.DotNet
{
	// Token: 0x0200079C RID: 1948
	[ComVisible(true)]
	[Serializable]
	public class CABlobParserException : Exception
	{
		// Token: 0x060045A3 RID: 17827 RVA: 0x0016DF0C File Offset: 0x0016DF0C
		public CABlobParserException()
		{
		}

		// Token: 0x060045A4 RID: 17828 RVA: 0x0016DF14 File Offset: 0x0016DF14
		public CABlobParserException(string message) : base(message)
		{
		}

		// Token: 0x060045A5 RID: 17829 RVA: 0x0016DF20 File Offset: 0x0016DF20
		public CABlobParserException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060045A6 RID: 17830 RVA: 0x0016DF2C File Offset: 0x0016DF2C
		protected CABlobParserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
