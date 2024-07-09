using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C58 RID: 3160
	internal interface IProtoTypeSerializer : IProtoSerializer
	{
		// Token: 0x06007D84 RID: 32132
		bool HasCallbacks(TypeModel.CallbackType callbackType);

		// Token: 0x06007D85 RID: 32133
		bool CanCreateInstance();

		// Token: 0x06007D86 RID: 32134
		object CreateInstance(ProtoReader source);

		// Token: 0x06007D87 RID: 32135
		void Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context);

		// Token: 0x06007D88 RID: 32136
		void EmitCallback(CompilerContext ctx, Local valueFrom, TypeModel.CallbackType callbackType);

		// Token: 0x06007D89 RID: 32137
		void EmitCreateInstance(CompilerContext ctx);
	}
}
