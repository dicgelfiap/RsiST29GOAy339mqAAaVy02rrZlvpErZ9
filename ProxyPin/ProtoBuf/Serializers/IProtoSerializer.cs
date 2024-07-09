using System;
using ProtoBuf.Compiler;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C57 RID: 3159
	internal interface IProtoSerializer
	{
		// Token: 0x17001B30 RID: 6960
		// (get) Token: 0x06007D7D RID: 32125
		Type ExpectedType { get; }

		// Token: 0x06007D7E RID: 32126
		void Write(object value, ProtoWriter dest);

		// Token: 0x06007D7F RID: 32127
		object Read(object value, ProtoReader source);

		// Token: 0x17001B31 RID: 6961
		// (get) Token: 0x06007D80 RID: 32128
		bool RequiresOldValue { get; }

		// Token: 0x17001B32 RID: 6962
		// (get) Token: 0x06007D81 RID: 32129
		bool ReturnsValue { get; }

		// Token: 0x06007D82 RID: 32130
		void EmitWrite(CompilerContext ctx, Local valueFrom);

		// Token: 0x06007D83 RID: 32131
		void EmitRead(CompilerContext ctx, Local entity);
	}
}
