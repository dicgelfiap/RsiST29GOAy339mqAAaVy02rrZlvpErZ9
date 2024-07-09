using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace ProtoBuf
{
	// Token: 0x02000C20 RID: 3104
	[protobuf-net.IsReadOnly]
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Explicit)]
	public struct DiscriminatedUnion32 : ISerializable
	{
		// Token: 0x06007B62 RID: 31586 RVA: 0x002463E8 File Offset: 0x002463E8
		private DiscriminatedUnion32(int discriminator)
		{
			this = default(DiscriminatedUnion32);
			this._discriminator = discriminator;
		}

		// Token: 0x06007B63 RID: 31587 RVA: 0x002463F8 File Offset: 0x002463F8
		public bool Is(int discriminator)
		{
			return this._discriminator == discriminator;
		}

		// Token: 0x06007B64 RID: 31588 RVA: 0x00246404 File Offset: 0x00246404
		public DiscriminatedUnion32(int discriminator, int value)
		{
			this = new DiscriminatedUnion32(discriminator);
			this.Int32 = value;
		}

		// Token: 0x06007B65 RID: 31589 RVA: 0x00246414 File Offset: 0x00246414
		public DiscriminatedUnion32(int discriminator, uint value)
		{
			this = new DiscriminatedUnion32(discriminator);
			this.UInt32 = value;
		}

		// Token: 0x06007B66 RID: 31590 RVA: 0x00246424 File Offset: 0x00246424
		public DiscriminatedUnion32(int discriminator, float value)
		{
			this = new DiscriminatedUnion32(discriminator);
			this.Single = value;
		}

		// Token: 0x06007B67 RID: 31591 RVA: 0x00246434 File Offset: 0x00246434
		public DiscriminatedUnion32(int discriminator, bool value)
		{
			this = new DiscriminatedUnion32(discriminator);
			this.Boolean = value;
		}

		// Token: 0x06007B68 RID: 31592 RVA: 0x00246444 File Offset: 0x00246444
		public static void Reset(ref DiscriminatedUnion32 value, int discriminator)
		{
			if (value.Discriminator == discriminator)
			{
				value = default(DiscriminatedUnion32);
			}
		}

		// Token: 0x17001AC7 RID: 6855
		// (get) Token: 0x06007B69 RID: 31593 RVA: 0x0024645C File Offset: 0x0024645C
		public int Discriminator
		{
			get
			{
				return this._discriminator;
			}
		}

		// Token: 0x06007B6A RID: 31594 RVA: 0x00246464 File Offset: 0x00246464
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (this._discriminator != 0)
			{
				info.AddValue("d", this._discriminator);
			}
			if (this.Int32 != 0)
			{
				info.AddValue("i", this.Int32);
			}
		}

		// Token: 0x06007B6B RID: 31595 RVA: 0x002464A0 File Offset: 0x002464A0
		private DiscriminatedUnion32(SerializationInfo info, StreamingContext context)
		{
			this = default(DiscriminatedUnion32);
			foreach (SerializationEntry serializationEntry in info)
			{
				string name = serializationEntry.Name;
				if (name != null)
				{
					if (!(name == "d"))
					{
						if (name == "i")
						{
							this.Int32 = (int)serializationEntry.Value;
						}
					}
					else
					{
						this._discriminator = (int)serializationEntry.Value;
					}
				}
			}
		}

		// Token: 0x04003B9A RID: 15258
		[FieldOffset(0)]
		private readonly int _discriminator;

		// Token: 0x04003B9B RID: 15259
		[FieldOffset(4)]
		public readonly int Int32;

		// Token: 0x04003B9C RID: 15260
		[FieldOffset(4)]
		public readonly uint UInt32;

		// Token: 0x04003B9D RID: 15261
		[FieldOffset(4)]
		public readonly bool Boolean;

		// Token: 0x04003B9E RID: 15262
		[FieldOffset(4)]
		public readonly float Single;
	}
}
