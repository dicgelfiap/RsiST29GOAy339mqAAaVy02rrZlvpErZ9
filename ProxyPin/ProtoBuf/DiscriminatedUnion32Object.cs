using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace ProtoBuf
{
	// Token: 0x02000C21 RID: 3105
	[protobuf-net.IsReadOnly]
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Explicit)]
	public struct DiscriminatedUnion32Object : ISerializable
	{
		// Token: 0x06007B6C RID: 31596 RVA: 0x00246530 File Offset: 0x00246530
		private DiscriminatedUnion32Object(int discriminator)
		{
			this = default(DiscriminatedUnion32Object);
			this._discriminator = discriminator;
		}

		// Token: 0x06007B6D RID: 31597 RVA: 0x00246540 File Offset: 0x00246540
		public bool Is(int discriminator)
		{
			return this._discriminator == discriminator;
		}

		// Token: 0x06007B6E RID: 31598 RVA: 0x0024654C File Offset: 0x0024654C
		public DiscriminatedUnion32Object(int discriminator, int value)
		{
			this = new DiscriminatedUnion32Object(discriminator);
			this.Int32 = value;
		}

		// Token: 0x06007B6F RID: 31599 RVA: 0x0024655C File Offset: 0x0024655C
		public DiscriminatedUnion32Object(int discriminator, uint value)
		{
			this = new DiscriminatedUnion32Object(discriminator);
			this.UInt32 = value;
		}

		// Token: 0x06007B70 RID: 31600 RVA: 0x0024656C File Offset: 0x0024656C
		public DiscriminatedUnion32Object(int discriminator, float value)
		{
			this = new DiscriminatedUnion32Object(discriminator);
			this.Single = value;
		}

		// Token: 0x06007B71 RID: 31601 RVA: 0x0024657C File Offset: 0x0024657C
		public DiscriminatedUnion32Object(int discriminator, bool value)
		{
			this = new DiscriminatedUnion32Object(discriminator);
			this.Boolean = value;
		}

		// Token: 0x06007B72 RID: 31602 RVA: 0x0024658C File Offset: 0x0024658C
		public DiscriminatedUnion32Object(int discriminator, object value)
		{
			this = new DiscriminatedUnion32Object((value != null) ? discriminator : 0);
			this.Object = value;
		}

		// Token: 0x06007B73 RID: 31603 RVA: 0x002465A8 File Offset: 0x002465A8
		public static void Reset(ref DiscriminatedUnion32Object value, int discriminator)
		{
			if (value.Discriminator == discriminator)
			{
				value = default(DiscriminatedUnion32Object);
			}
		}

		// Token: 0x17001AC8 RID: 6856
		// (get) Token: 0x06007B74 RID: 31604 RVA: 0x002465C0 File Offset: 0x002465C0
		public int Discriminator
		{
			get
			{
				return this._discriminator;
			}
		}

		// Token: 0x06007B75 RID: 31605 RVA: 0x002465C8 File Offset: 0x002465C8
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
			if (this.Object != null)
			{
				info.AddValue("o", this.Object);
			}
		}

		// Token: 0x06007B76 RID: 31606 RVA: 0x00246630 File Offset: 0x00246630
		private DiscriminatedUnion32Object(SerializationInfo info, StreamingContext context)
		{
			this = default(DiscriminatedUnion32Object);
			foreach (SerializationEntry serializationEntry in info)
			{
				string name = serializationEntry.Name;
				if (name != null)
				{
					if (!(name == "d"))
					{
						if (!(name == "i"))
						{
							if (name == "o")
							{
								this.Object = serializationEntry.Value;
							}
						}
						else
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

		// Token: 0x04003B9F RID: 15263
		[FieldOffset(0)]
		private readonly int _discriminator;

		// Token: 0x04003BA0 RID: 15264
		[FieldOffset(4)]
		public readonly int Int32;

		// Token: 0x04003BA1 RID: 15265
		[FieldOffset(4)]
		public readonly uint UInt32;

		// Token: 0x04003BA2 RID: 15266
		[FieldOffset(4)]
		public readonly bool Boolean;

		// Token: 0x04003BA3 RID: 15267
		[FieldOffset(4)]
		public readonly float Single;

		// Token: 0x04003BA4 RID: 15268
		[FieldOffset(8)]
		public readonly object Object;
	}
}
