using System;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace ProtoBuf.Compiler
{
	// Token: 0x02000C7F RID: 3199
	[protobuf-net.IsReadOnly]
	internal struct CodeLabel
	{
		// Token: 0x06007FFE RID: 32766 RVA: 0x0025DFC8 File Offset: 0x0025DFC8
		public CodeLabel(Label value, int index)
		{
			this.Value = value;
			this.Index = index;
		}

		// Token: 0x04003CFE RID: 15614
		public readonly Label Value;

		// Token: 0x04003CFF RID: 15615
		public readonly int Index;
	}
}
