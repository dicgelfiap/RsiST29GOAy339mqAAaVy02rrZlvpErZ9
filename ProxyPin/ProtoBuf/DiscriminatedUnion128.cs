using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace ProtoBuf
{
	// Token: 0x02000C1E RID: 3102
	[protobuf-net.IsReadOnly]
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Explicit)]
	public struct DiscriminatedUnion128 : ISerializable
	{
		// Token: 0x06007B40 RID: 31552 RVA: 0x00245EE0 File Offset: 0x00245EE0
		static DiscriminatedUnion128()
		{
			if (sizeof(DateTime) > 16)
			{
				throw new InvalidOperationException("DateTime was unexpectedly too big for DiscriminatedUnion128");
			}
			if (sizeof(TimeSpan) > 16)
			{
				throw new InvalidOperationException("TimeSpan was unexpectedly too big for DiscriminatedUnion128");
			}
			if (sizeof(Guid) > 16)
			{
				throw new InvalidOperationException("Guid was unexpectedly too big for DiscriminatedUnion128");
			}
		}

		// Token: 0x06007B41 RID: 31553 RVA: 0x00245F3C File Offset: 0x00245F3C
		private DiscriminatedUnion128(int discriminator)
		{
			this = default(DiscriminatedUnion128);
			this._discriminator = discriminator;
		}

		// Token: 0x06007B42 RID: 31554 RVA: 0x00245F4C File Offset: 0x00245F4C
		public bool Is(int discriminator)
		{
			return this._discriminator == discriminator;
		}

		// Token: 0x06007B43 RID: 31555 RVA: 0x00245F58 File Offset: 0x00245F58
		public DiscriminatedUnion128(int discriminator, long value)
		{
			this = new DiscriminatedUnion128(discriminator);
			this.Int64 = value;
		}

		// Token: 0x06007B44 RID: 31556 RVA: 0x00245F68 File Offset: 0x00245F68
		public DiscriminatedUnion128(int discriminator, int value)
		{
			this = new DiscriminatedUnion128(discriminator);
			this.Int32 = value;
		}

		// Token: 0x06007B45 RID: 31557 RVA: 0x00245F78 File Offset: 0x00245F78
		public DiscriminatedUnion128(int discriminator, ulong value)
		{
			this = new DiscriminatedUnion128(discriminator);
			this.UInt64 = value;
		}

		// Token: 0x06007B46 RID: 31558 RVA: 0x00245F88 File Offset: 0x00245F88
		public DiscriminatedUnion128(int discriminator, uint value)
		{
			this = new DiscriminatedUnion128(discriminator);
			this.UInt32 = value;
		}

		// Token: 0x06007B47 RID: 31559 RVA: 0x00245F98 File Offset: 0x00245F98
		public DiscriminatedUnion128(int discriminator, float value)
		{
			this = new DiscriminatedUnion128(discriminator);
			this.Single = value;
		}

		// Token: 0x06007B48 RID: 31560 RVA: 0x00245FA8 File Offset: 0x00245FA8
		public DiscriminatedUnion128(int discriminator, double value)
		{
			this = new DiscriminatedUnion128(discriminator);
			this.Double = value;
		}

		// Token: 0x06007B49 RID: 31561 RVA: 0x00245FB8 File Offset: 0x00245FB8
		public DiscriminatedUnion128(int discriminator, bool value)
		{
			this = new DiscriminatedUnion128(discriminator);
			this.Boolean = value;
		}

		// Token: 0x06007B4A RID: 31562 RVA: 0x00245FC8 File Offset: 0x00245FC8
		public DiscriminatedUnion128(int discriminator, DateTime? value)
		{
			this = new DiscriminatedUnion128((value != null) ? discriminator : 0);
			this.DateTime = value.GetValueOrDefault();
		}

		// Token: 0x06007B4B RID: 31563 RVA: 0x00245FF0 File Offset: 0x00245FF0
		public DiscriminatedUnion128(int discriminator, TimeSpan? value)
		{
			this = new DiscriminatedUnion128((value != null) ? discriminator : 0);
			this.TimeSpan = value.GetValueOrDefault();
		}

		// Token: 0x06007B4C RID: 31564 RVA: 0x00246018 File Offset: 0x00246018
		public DiscriminatedUnion128(int discriminator, Guid? value)
		{
			this = new DiscriminatedUnion128((value != null) ? discriminator : 0);
			this.Guid = value.GetValueOrDefault();
		}

		// Token: 0x06007B4D RID: 31565 RVA: 0x00246040 File Offset: 0x00246040
		public static void Reset(ref DiscriminatedUnion128 value, int discriminator)
		{
			if (value.Discriminator == discriminator)
			{
				value = default(DiscriminatedUnion128);
			}
		}

		// Token: 0x17001AC5 RID: 6853
		// (get) Token: 0x06007B4E RID: 31566 RVA: 0x00246058 File Offset: 0x00246058
		public int Discriminator
		{
			get
			{
				return this._discriminator;
			}
		}

		// Token: 0x06007B4F RID: 31567 RVA: 0x00246060 File Offset: 0x00246060
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (this._discriminator != 0)
			{
				info.AddValue("d", this._discriminator);
			}
			if (this._lo != 0L)
			{
				info.AddValue("l", this._lo);
			}
			if (this._hi != 0L)
			{
				info.AddValue("h", this._hi);
			}
		}

		// Token: 0x06007B50 RID: 31568 RVA: 0x002460C8 File Offset: 0x002460C8
		private DiscriminatedUnion128(SerializationInfo info, StreamingContext context)
		{
			this = default(DiscriminatedUnion128);
			foreach (SerializationEntry serializationEntry in info)
			{
				string name = serializationEntry.Name;
				if (name != null)
				{
					if (!(name == "d"))
					{
						if (!(name == "l"))
						{
							if (name == "h")
							{
								this._hi = (long)serializationEntry.Value;
							}
						}
						else
						{
							this._lo = (long)serializationEntry.Value;
						}
					}
					else
					{
						this._discriminator = (int)serializationEntry.Value;
					}
				}
			}
		}

		// Token: 0x04003B82 RID: 15234
		[FieldOffset(0)]
		private readonly int _discriminator;

		// Token: 0x04003B83 RID: 15235
		[FieldOffset(8)]
		public readonly long Int64;

		// Token: 0x04003B84 RID: 15236
		[FieldOffset(8)]
		public readonly ulong UInt64;

		// Token: 0x04003B85 RID: 15237
		[FieldOffset(8)]
		public readonly int Int32;

		// Token: 0x04003B86 RID: 15238
		[FieldOffset(8)]
		public readonly uint UInt32;

		// Token: 0x04003B87 RID: 15239
		[FieldOffset(8)]
		public readonly bool Boolean;

		// Token: 0x04003B88 RID: 15240
		[FieldOffset(8)]
		public readonly float Single;

		// Token: 0x04003B89 RID: 15241
		[FieldOffset(8)]
		public readonly double Double;

		// Token: 0x04003B8A RID: 15242
		[FieldOffset(8)]
		public readonly DateTime DateTime;

		// Token: 0x04003B8B RID: 15243
		[FieldOffset(8)]
		public readonly TimeSpan TimeSpan;

		// Token: 0x04003B8C RID: 15244
		[FieldOffset(8)]
		public readonly Guid Guid;

		// Token: 0x04003B8D RID: 15245
		[FieldOffset(8)]
		private readonly long _lo;

		// Token: 0x04003B8E RID: 15246
		[FieldOffset(16)]
		private readonly long _hi;
	}
}
