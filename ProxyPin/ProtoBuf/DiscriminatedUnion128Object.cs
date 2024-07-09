using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace ProtoBuf
{
	// Token: 0x02000C1D RID: 3101
	[protobuf-net.IsReadOnly]
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Explicit)]
	public struct DiscriminatedUnion128Object : ISerializable
	{
		// Token: 0x06007B2E RID: 31534 RVA: 0x00245BE4 File Offset: 0x00245BE4
		static DiscriminatedUnion128Object()
		{
			if (sizeof(DateTime) > 16)
			{
				throw new InvalidOperationException("DateTime was unexpectedly too big for DiscriminatedUnion128Object");
			}
			if (sizeof(TimeSpan) > 16)
			{
				throw new InvalidOperationException("TimeSpan was unexpectedly too big for DiscriminatedUnion128Object");
			}
			if (sizeof(Guid) > 16)
			{
				throw new InvalidOperationException("Guid was unexpectedly too big for DiscriminatedUnion128Object");
			}
		}

		// Token: 0x06007B2F RID: 31535 RVA: 0x00245C40 File Offset: 0x00245C40
		private DiscriminatedUnion128Object(int discriminator)
		{
			this = default(DiscriminatedUnion128Object);
			this._discriminator = discriminator;
		}

		// Token: 0x06007B30 RID: 31536 RVA: 0x00245C50 File Offset: 0x00245C50
		public bool Is(int discriminator)
		{
			return this._discriminator == discriminator;
		}

		// Token: 0x06007B31 RID: 31537 RVA: 0x00245C5C File Offset: 0x00245C5C
		public DiscriminatedUnion128Object(int discriminator, long value)
		{
			this = new DiscriminatedUnion128Object(discriminator);
			this.Int64 = value;
		}

		// Token: 0x06007B32 RID: 31538 RVA: 0x00245C6C File Offset: 0x00245C6C
		public DiscriminatedUnion128Object(int discriminator, int value)
		{
			this = new DiscriminatedUnion128Object(discriminator);
			this.Int32 = value;
		}

		// Token: 0x06007B33 RID: 31539 RVA: 0x00245C7C File Offset: 0x00245C7C
		public DiscriminatedUnion128Object(int discriminator, ulong value)
		{
			this = new DiscriminatedUnion128Object(discriminator);
			this.UInt64 = value;
		}

		// Token: 0x06007B34 RID: 31540 RVA: 0x00245C8C File Offset: 0x00245C8C
		public DiscriminatedUnion128Object(int discriminator, uint value)
		{
			this = new DiscriminatedUnion128Object(discriminator);
			this.UInt32 = value;
		}

		// Token: 0x06007B35 RID: 31541 RVA: 0x00245C9C File Offset: 0x00245C9C
		public DiscriminatedUnion128Object(int discriminator, float value)
		{
			this = new DiscriminatedUnion128Object(discriminator);
			this.Single = value;
		}

		// Token: 0x06007B36 RID: 31542 RVA: 0x00245CAC File Offset: 0x00245CAC
		public DiscriminatedUnion128Object(int discriminator, double value)
		{
			this = new DiscriminatedUnion128Object(discriminator);
			this.Double = value;
		}

		// Token: 0x06007B37 RID: 31543 RVA: 0x00245CBC File Offset: 0x00245CBC
		public DiscriminatedUnion128Object(int discriminator, bool value)
		{
			this = new DiscriminatedUnion128Object(discriminator);
			this.Boolean = value;
		}

		// Token: 0x06007B38 RID: 31544 RVA: 0x00245CCC File Offset: 0x00245CCC
		public DiscriminatedUnion128Object(int discriminator, object value)
		{
			this = new DiscriminatedUnion128Object((value != null) ? discriminator : 0);
			this.Object = value;
		}

		// Token: 0x06007B39 RID: 31545 RVA: 0x00245CE8 File Offset: 0x00245CE8
		public DiscriminatedUnion128Object(int discriminator, DateTime? value)
		{
			this = new DiscriminatedUnion128Object((value != null) ? discriminator : 0);
			this.DateTime = value.GetValueOrDefault();
		}

		// Token: 0x06007B3A RID: 31546 RVA: 0x00245D10 File Offset: 0x00245D10
		public DiscriminatedUnion128Object(int discriminator, TimeSpan? value)
		{
			this = new DiscriminatedUnion128Object((value != null) ? discriminator : 0);
			this.TimeSpan = value.GetValueOrDefault();
		}

		// Token: 0x06007B3B RID: 31547 RVA: 0x00245D38 File Offset: 0x00245D38
		public DiscriminatedUnion128Object(int discriminator, Guid? value)
		{
			this = new DiscriminatedUnion128Object((value != null) ? discriminator : 0);
			this.Guid = value.GetValueOrDefault();
		}

		// Token: 0x06007B3C RID: 31548 RVA: 0x00245D60 File Offset: 0x00245D60
		public static void Reset(ref DiscriminatedUnion128Object value, int discriminator)
		{
			if (value.Discriminator == discriminator)
			{
				value = default(DiscriminatedUnion128Object);
			}
		}

		// Token: 0x17001AC4 RID: 6852
		// (get) Token: 0x06007B3D RID: 31549 RVA: 0x00245D78 File Offset: 0x00245D78
		public int Discriminator
		{
			get
			{
				return this._discriminator;
			}
		}

		// Token: 0x06007B3E RID: 31550 RVA: 0x00245D80 File Offset: 0x00245D80
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
			if (this.Object != null)
			{
				info.AddValue("o", this.Object);
			}
		}

		// Token: 0x06007B3F RID: 31551 RVA: 0x00245E04 File Offset: 0x00245E04
		private DiscriminatedUnion128Object(SerializationInfo info, StreamingContext context)
		{
			this = default(DiscriminatedUnion128Object);
			foreach (SerializationEntry serializationEntry in info)
			{
				string name = serializationEntry.Name;
				if (name != null)
				{
					if (!(name == "d"))
					{
						if (!(name == "l"))
						{
							if (!(name == "h"))
							{
								if (name == "o")
								{
									this.Object = serializationEntry.Value;
								}
							}
							else
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

		// Token: 0x04003B74 RID: 15220
		[FieldOffset(0)]
		private readonly int _discriminator;

		// Token: 0x04003B75 RID: 15221
		[FieldOffset(8)]
		public readonly long Int64;

		// Token: 0x04003B76 RID: 15222
		[FieldOffset(8)]
		public readonly ulong UInt64;

		// Token: 0x04003B77 RID: 15223
		[FieldOffset(8)]
		public readonly int Int32;

		// Token: 0x04003B78 RID: 15224
		[FieldOffset(8)]
		public readonly uint UInt32;

		// Token: 0x04003B79 RID: 15225
		[FieldOffset(8)]
		public readonly bool Boolean;

		// Token: 0x04003B7A RID: 15226
		[FieldOffset(8)]
		public readonly float Single;

		// Token: 0x04003B7B RID: 15227
		[FieldOffset(8)]
		public readonly double Double;

		// Token: 0x04003B7C RID: 15228
		[FieldOffset(8)]
		public readonly DateTime DateTime;

		// Token: 0x04003B7D RID: 15229
		[FieldOffset(8)]
		public readonly TimeSpan TimeSpan;

		// Token: 0x04003B7E RID: 15230
		[FieldOffset(8)]
		public readonly Guid Guid;

		// Token: 0x04003B7F RID: 15231
		[FieldOffset(24)]
		public readonly object Object;

		// Token: 0x04003B80 RID: 15232
		[FieldOffset(8)]
		private readonly long _lo;

		// Token: 0x04003B81 RID: 15233
		[FieldOffset(16)]
		private readonly long _hi;
	}
}
