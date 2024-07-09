using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace ProtoBuf
{
	// Token: 0x02000C1F RID: 3103
	[protobuf-net.IsReadOnly]
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Explicit)]
	public struct DiscriminatedUnion64Object : ISerializable
	{
		// Token: 0x06007B51 RID: 31569 RVA: 0x00246184 File Offset: 0x00246184
		static DiscriminatedUnion64Object()
		{
			if (sizeof(DateTime) > 8)
			{
				throw new InvalidOperationException("DateTime was unexpectedly too big for DiscriminatedUnion64Object");
			}
			if (sizeof(TimeSpan) > 8)
			{
				throw new InvalidOperationException("TimeSpan was unexpectedly too big for DiscriminatedUnion64Object");
			}
		}

		// Token: 0x06007B52 RID: 31570 RVA: 0x002461B4 File Offset: 0x002461B4
		private DiscriminatedUnion64Object(int discriminator)
		{
			this = default(DiscriminatedUnion64Object);
			this._discriminator = discriminator;
		}

		// Token: 0x06007B53 RID: 31571 RVA: 0x002461C4 File Offset: 0x002461C4
		public bool Is(int discriminator)
		{
			return this._discriminator == discriminator;
		}

		// Token: 0x06007B54 RID: 31572 RVA: 0x002461D0 File Offset: 0x002461D0
		public DiscriminatedUnion64Object(int discriminator, long value)
		{
			this = new DiscriminatedUnion64Object(discriminator);
			this.Int64 = value;
		}

		// Token: 0x06007B55 RID: 31573 RVA: 0x002461E0 File Offset: 0x002461E0
		public DiscriminatedUnion64Object(int discriminator, int value)
		{
			this = new DiscriminatedUnion64Object(discriminator);
			this.Int32 = value;
		}

		// Token: 0x06007B56 RID: 31574 RVA: 0x002461F0 File Offset: 0x002461F0
		public DiscriminatedUnion64Object(int discriminator, ulong value)
		{
			this = new DiscriminatedUnion64Object(discriminator);
			this.UInt64 = value;
		}

		// Token: 0x06007B57 RID: 31575 RVA: 0x00246200 File Offset: 0x00246200
		public DiscriminatedUnion64Object(int discriminator, uint value)
		{
			this = new DiscriminatedUnion64Object(discriminator);
			this.UInt32 = value;
		}

		// Token: 0x06007B58 RID: 31576 RVA: 0x00246210 File Offset: 0x00246210
		public DiscriminatedUnion64Object(int discriminator, float value)
		{
			this = new DiscriminatedUnion64Object(discriminator);
			this.Single = value;
		}

		// Token: 0x06007B59 RID: 31577 RVA: 0x00246220 File Offset: 0x00246220
		public DiscriminatedUnion64Object(int discriminator, double value)
		{
			this = new DiscriminatedUnion64Object(discriminator);
			this.Double = value;
		}

		// Token: 0x06007B5A RID: 31578 RVA: 0x00246230 File Offset: 0x00246230
		public DiscriminatedUnion64Object(int discriminator, bool value)
		{
			this = new DiscriminatedUnion64Object(discriminator);
			this.Boolean = value;
		}

		// Token: 0x06007B5B RID: 31579 RVA: 0x00246240 File Offset: 0x00246240
		public DiscriminatedUnion64Object(int discriminator, object value)
		{
			this = new DiscriminatedUnion64Object((value != null) ? discriminator : 0);
			this.Object = value;
		}

		// Token: 0x06007B5C RID: 31580 RVA: 0x0024625C File Offset: 0x0024625C
		public DiscriminatedUnion64Object(int discriminator, DateTime? value)
		{
			this = new DiscriminatedUnion64Object((value != null) ? discriminator : 0);
			this.DateTime = value.GetValueOrDefault();
		}

		// Token: 0x06007B5D RID: 31581 RVA: 0x00246284 File Offset: 0x00246284
		public DiscriminatedUnion64Object(int discriminator, TimeSpan? value)
		{
			this = new DiscriminatedUnion64Object((value != null) ? discriminator : 0);
			this.TimeSpan = value.GetValueOrDefault();
		}

		// Token: 0x06007B5E RID: 31582 RVA: 0x002462AC File Offset: 0x002462AC
		public static void Reset(ref DiscriminatedUnion64Object value, int discriminator)
		{
			if (value.Discriminator == discriminator)
			{
				value = default(DiscriminatedUnion64Object);
			}
		}

		// Token: 0x17001AC6 RID: 6854
		// (get) Token: 0x06007B5F RID: 31583 RVA: 0x002462C4 File Offset: 0x002462C4
		public int Discriminator
		{
			get
			{
				return this._discriminator;
			}
		}

		// Token: 0x06007B60 RID: 31584 RVA: 0x002462CC File Offset: 0x002462CC
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
			if (this.Object != null)
			{
				info.AddValue("o", this.Object);
			}
		}

		// Token: 0x06007B61 RID: 31585 RVA: 0x00246334 File Offset: 0x00246334
		private DiscriminatedUnion64Object(SerializationInfo info, StreamingContext context)
		{
			this = default(DiscriminatedUnion64Object);
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

		// Token: 0x04003B8F RID: 15247
		[FieldOffset(0)]
		private readonly int _discriminator;

		// Token: 0x04003B90 RID: 15248
		[FieldOffset(8)]
		public readonly long Int64;

		// Token: 0x04003B91 RID: 15249
		[FieldOffset(8)]
		public readonly ulong UInt64;

		// Token: 0x04003B92 RID: 15250
		[FieldOffset(8)]
		public readonly int Int32;

		// Token: 0x04003B93 RID: 15251
		[FieldOffset(8)]
		public readonly uint UInt32;

		// Token: 0x04003B94 RID: 15252
		[FieldOffset(8)]
		public readonly bool Boolean;

		// Token: 0x04003B95 RID: 15253
		[FieldOffset(8)]
		public readonly float Single;

		// Token: 0x04003B96 RID: 15254
		[FieldOffset(8)]
		public readonly double Double;

		// Token: 0x04003B97 RID: 15255
		[FieldOffset(8)]
		public readonly DateTime DateTime;

		// Token: 0x04003B98 RID: 15256
		[FieldOffset(8)]
		public readonly TimeSpan TimeSpan;

		// Token: 0x04003B99 RID: 15257
		[FieldOffset(16)]
		public readonly object Object;
	}
}
