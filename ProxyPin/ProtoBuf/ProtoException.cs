using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace ProtoBuf
{
	// Token: 0x02000C33 RID: 3123
	[ComVisible(true)]
	[Serializable]
	public class ProtoException : Exception
	{
		// Token: 0x06007BE2 RID: 31714 RVA: 0x002472AC File Offset: 0x002472AC
		public ProtoException()
		{
		}

		// Token: 0x06007BE3 RID: 31715 RVA: 0x002472B4 File Offset: 0x002472B4
		public ProtoException(string message) : base(message)
		{
		}

		// Token: 0x06007BE4 RID: 31716 RVA: 0x002472C0 File Offset: 0x002472C0
		public ProtoException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007BE5 RID: 31717 RVA: 0x002472CC File Offset: 0x002472CC
		protected ProtoException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
