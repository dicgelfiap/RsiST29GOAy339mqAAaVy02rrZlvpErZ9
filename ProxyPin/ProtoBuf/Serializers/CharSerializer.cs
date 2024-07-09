using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C4A RID: 3146
	internal sealed class CharSerializer : UInt16Serializer
	{
		// Token: 0x06007D04 RID: 32004 RVA: 0x0024C2E0 File Offset: 0x0024C2E0
		public CharSerializer(TypeModel model) : base(model)
		{
		}

		// Token: 0x17001B0D RID: 6925
		// (get) Token: 0x06007D05 RID: 32005 RVA: 0x0024C2EC File Offset: 0x0024C2EC
		public override Type ExpectedType
		{
			get
			{
				return CharSerializer.expectedType;
			}
		}

		// Token: 0x06007D06 RID: 32006 RVA: 0x0024C2F4 File Offset: 0x0024C2F4
		public override void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteUInt16((ushort)((char)value), dest);
		}

		// Token: 0x06007D07 RID: 32007 RVA: 0x0024C304 File Offset: 0x0024C304
		public override object Read(object value, ProtoReader source)
		{
			return (char)source.ReadUInt16();
		}

		// Token: 0x04003C3F RID: 15423
		private static readonly Type expectedType = typeof(char);
	}
}
