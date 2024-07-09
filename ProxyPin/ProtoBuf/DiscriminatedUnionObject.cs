using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace ProtoBuf
{
	// Token: 0x02000C1B RID: 3099
	[protobuf-net.IsReadOnly]
	[ComVisible(true)]
	[Serializable]
	public struct DiscriminatedUnionObject : ISerializable
	{
		// Token: 0x06007B18 RID: 31512 RVA: 0x002458E8 File Offset: 0x002458E8
		public bool Is(int discriminator)
		{
			return this.Discriminator == discriminator;
		}

		// Token: 0x06007B19 RID: 31513 RVA: 0x002458F4 File Offset: 0x002458F4
		public DiscriminatedUnionObject(int discriminator, object value)
		{
			this.Discriminator = discriminator;
			this.Object = value;
		}

		// Token: 0x06007B1A RID: 31514 RVA: 0x00245904 File Offset: 0x00245904
		public static void Reset(ref DiscriminatedUnionObject value, int discriminator)
		{
			if (value.Discriminator == discriminator)
			{
				value = default(DiscriminatedUnionObject);
			}
		}

		// Token: 0x17001AC2 RID: 6850
		// (get) Token: 0x06007B1B RID: 31515 RVA: 0x0024591C File Offset: 0x0024591C
		public int Discriminator { get; }

		// Token: 0x06007B1C RID: 31516 RVA: 0x00245924 File Offset: 0x00245924
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (this.Discriminator != 0)
			{
				info.AddValue("d", this.Discriminator);
			}
			if (this.Object != null)
			{
				info.AddValue("o", this.Object);
			}
		}

		// Token: 0x06007B1D RID: 31517 RVA: 0x00245960 File Offset: 0x00245960
		private DiscriminatedUnionObject(SerializationInfo info, StreamingContext context)
		{
			this = default(DiscriminatedUnionObject);
			foreach (SerializationEntry serializationEntry in info)
			{
				string name = serializationEntry.Name;
				if (name != null)
				{
					if (!(name == "d"))
					{
						if (name == "o")
						{
							this.Object = serializationEntry.Value;
						}
					}
					else
					{
						this.Discriminator = (int)serializationEntry.Value;
					}
				}
			}
		}

		// Token: 0x04003B68 RID: 15208
		public readonly object Object;
	}
}
