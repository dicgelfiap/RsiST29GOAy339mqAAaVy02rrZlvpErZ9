using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace dnlib.DotNet
{
	// Token: 0x0200085F RID: 2143
	[ComVisible(true)]
	[Serializable]
	public class TypeNameParserException : Exception
	{
		// Token: 0x060051ED RID: 20973 RVA: 0x00194728 File Offset: 0x00194728
		public TypeNameParserException()
		{
		}

		// Token: 0x060051EE RID: 20974 RVA: 0x00194730 File Offset: 0x00194730
		public TypeNameParserException(string message) : base(message)
		{
		}

		// Token: 0x060051EF RID: 20975 RVA: 0x0019473C File Offset: 0x0019473C
		public TypeNameParserException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060051F0 RID: 20976 RVA: 0x00194748 File Offset: 0x00194748
		protected TypeNameParserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
