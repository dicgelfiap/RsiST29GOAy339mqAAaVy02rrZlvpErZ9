using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace ProtoBuf
{
	// Token: 0x02000C1C RID: 3100
	[protobuf-net.IsReadOnly]
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Explicit)]
	public struct DiscriminatedUnion64 : ISerializable
	{
		// Token: 0x06007B1E RID: 31518 RVA: 0x002459EC File Offset: 0x002459EC
		static DiscriminatedUnion64()
		{
			if (sizeof(DateTime) > 8)
			{
				throw new InvalidOperationException("DateTime was unexpectedly too big for DiscriminatedUnion64");
			}
			if (sizeof(TimeSpan) > 8)
			{
				throw new InvalidOperationException("TimeSpan was unexpectedly too big for DiscriminatedUnion64");
			}
		}

		// Token: 0x06007B1F RID: 31519 RVA: 0x00245A1C File Offset: 0x00245A1C
		private DiscriminatedUnion64(int discriminator)
		{
			this = default(DiscriminatedUnion64);
			this._discriminator = discriminator;
		}

		// Token: 0x06007B20 RID: 31520 RVA: 0x00245A2C File Offset: 0x00245A2C
		public bool Is(int discriminator)
		{
			return this._discriminator == discriminator;
		}

		// Token: 0x06007B21 RID: 31521 RVA: 0x00245A38 File Offset: 0x00245A38
		public DiscriminatedUnion64(int discriminator, long value)
		{
			this = new DiscriminatedUnion64(discriminator);
			this.Int64 = value;
		}

		// Token: 0x06007B22 RID: 31522 RVA: 0x00245A48 File Offset: 0x00245A48
		public DiscriminatedUnion64(int discriminator, int value)
		{
			this = new DiscriminatedUnion64(discriminator);
			this.Int32 = value;
		}

		// Token: 0x06007B23 RID: 31523 RVA: 0x00245A58 File Offset: 0x00245A58
		public DiscriminatedUnion64(int discriminator, ulong value)
		{
			this = new DiscriminatedUnion64(discriminator);
			this.UInt64 = value;
		}

		// Token: 0x06007B24 RID: 31524 RVA: 0x00245A68 File Offset: 0x00245A68
		public DiscriminatedUnion64(int discriminator, uint value)
		{
			this = new DiscriminatedUnion64(discriminator);
			this.UInt32 = value;
		}

		// Token: 0x06007B25 RID: 31525 RVA: 0x00245A78 File Offset: 0x00245A78
		public DiscriminatedUnion64(int discriminator, float value)
		{
			this = new DiscriminatedUnion64(discriminator);
			this.Single = value;
		}

		// Token: 0x06007B26 RID: 31526 RVA: 0x00245A88 File Offset: 0x00245A88
		public DiscriminatedUnion64(int discriminator, double value)
		{
			this = new DiscriminatedUnion64(discriminator);
			this.Double = value;
		}

		// Token: 0x06007B27 RID: 31527 RVA: 0x00245A98 File Offset: 0x00245A98
		public DiscriminatedUnion64(int discriminator, bool value)
		{
			this = new DiscriminatedUnion64(discriminator);
			this.Boolean = value;
		}

		// Token: 0x06007B28 RID: 31528 RVA: 0x00245AA8 File Offset: 0x00245AA8
		public DiscriminatedUnion64(int discriminator, DateTime? value)
		{
			this = new DiscriminatedUnion64((value != null) ? discriminator : 0);
			this.DateTime = value.GetValueOrDefault();
		}

		// Token: 0x06007B29 RID: 31529 RVA: 0x00245AD0 File Offset: 0x00245AD0
		public DiscriminatedUnion64(int discriminator, TimeSpan? value)
		{
			this = new DiscriminatedUnion64((value != null) ? discriminator : 0);
			this.TimeSpan = value.GetValueOrDefault();
		}

		// Token: 0x06007B2A RID: 31530 RVA: 0x00245AF8 File Offset: 0x00245AF8
		public static void Reset(ref DiscriminatedUnion64 value, int discriminator)
		{
			if (value.Discriminator == discriminator)
			{
				value = default(DiscriminatedUnion64);
			}
		}

		// Token: 0x17001AC3 RID: 6851
		// (get) Token: 0x06007B2B RID: 31531 RVA: 0x00245B10 File Offset: 0x00245B10
		public int Discriminator
		{
			get
			{
				return this._discriminator;
			}
		}

		// Token: 0x06007B2C RID: 31532 RVA: 0x00245B18 File Offset: 0x00245B18
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (this._discriminator != 0)
			{
				info.AddValue("d", this._discriminator);
			}
			if (this.Int64 != 0L)
			{
				info.AddValue("i", this.Int64);
			}
		}

		// Token: 0x06007B2D RID: 31533 RVA: 0x00245B54 File Offset: 0x00245B54
		private DiscriminatedUnion64(SerializationInfo info, StreamingContext context)
		{
			this = default(DiscriminatedUnion64);
			foreach (SerializationEntry serializationEntry in info)
			{
				string name = serializationEntry.Name;
				if (name != null)
				{
					if (!(name == "d"))
					{
						if (name == "i")
						{
							this.Int64 = (long)serializationEntry.Value;
						}
					}
					else
					{
						this._discriminator = (int)serializationEntry.Value;
					}
				}
			}
		}

		// Token: 0x04003B6A RID: 15210
		[FieldOffset(0)]
		private readonly int _discriminator;

		// Token: 0x04003B6B RID: 15211
		[FieldOffset(8)]
		public readonly long Int64;

		// Token: 0x04003B6C RID: 15212
		[FieldOffset(8)]
		public readonly ulong UInt64;

		// Token: 0x04003B6D RID: 15213
		[FieldOffset(8)]
		public readonly int Int32;

		// Token: 0x04003B6E RID: 15214
		[FieldOffset(8)]
		public readonly uint UInt32;

		// Token: 0x04003B6F RID: 15215
		[FieldOffset(8)]
		public readonly bool Boolean;

		// Token: 0x04003B70 RID: 15216
		[FieldOffset(8)]
		public readonly float Single;

		// Token: 0x04003B71 RID: 15217
		[FieldOffset(8)]
		public readonly double Double;

		// Token: 0x04003B72 RID: 15218
		[FieldOffset(8)]
		public readonly DateTime DateTime;

		// Token: 0x04003B73 RID: 15219
		[FieldOffset(8)]
		public readonly TimeSpan TimeSpan;
	}
}
