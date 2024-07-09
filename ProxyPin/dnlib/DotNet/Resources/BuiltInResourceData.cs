using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using dnlib.IO;

namespace dnlib.DotNet.Resources
{
	// Token: 0x020008E3 RID: 2275
	[ComVisible(true)]
	public sealed class BuiltInResourceData : IResourceData, IFileSection
	{
		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x060058A0 RID: 22688 RVA: 0x001B4008 File Offset: 0x001B4008
		public object Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x060058A1 RID: 22689 RVA: 0x001B4010 File Offset: 0x001B4010
		public ResourceTypeCode Code
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x060058A2 RID: 22690 RVA: 0x001B4018 File Offset: 0x001B4018
		// (set) Token: 0x060058A3 RID: 22691 RVA: 0x001B4020 File Offset: 0x001B4020
		public FileOffset StartOffset { get; set; }

		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x060058A4 RID: 22692 RVA: 0x001B402C File Offset: 0x001B402C
		// (set) Token: 0x060058A5 RID: 22693 RVA: 0x001B4034 File Offset: 0x001B4034
		public FileOffset EndOffset { get; set; }

		// Token: 0x060058A6 RID: 22694 RVA: 0x001B4040 File Offset: 0x001B4040
		public BuiltInResourceData(ResourceTypeCode code, object data)
		{
			this.code = code;
			this.data = data;
		}

		// Token: 0x060058A7 RID: 22695 RVA: 0x001B4058 File Offset: 0x001B4058
		public void WriteData(BinaryWriter writer, IFormatter formatter)
		{
			switch (this.code)
			{
			case ResourceTypeCode.Null:
				return;
			case ResourceTypeCode.String:
				writer.Write((string)this.data);
				return;
			case ResourceTypeCode.Boolean:
				writer.Write((bool)this.data);
				return;
			case ResourceTypeCode.Char:
				writer.Write((ushort)((char)this.data));
				return;
			case ResourceTypeCode.Byte:
				writer.Write((byte)this.data);
				return;
			case ResourceTypeCode.SByte:
				writer.Write((sbyte)this.data);
				return;
			case ResourceTypeCode.Int16:
				writer.Write((short)this.data);
				return;
			case ResourceTypeCode.UInt16:
				writer.Write((ushort)this.data);
				return;
			case ResourceTypeCode.Int32:
				writer.Write((int)this.data);
				return;
			case ResourceTypeCode.UInt32:
				writer.Write((uint)this.data);
				return;
			case ResourceTypeCode.Int64:
				writer.Write((long)this.data);
				return;
			case ResourceTypeCode.UInt64:
				writer.Write((ulong)this.data);
				return;
			case ResourceTypeCode.Single:
				writer.Write((float)this.data);
				return;
			case ResourceTypeCode.Double:
				writer.Write((double)this.data);
				return;
			case ResourceTypeCode.Decimal:
				writer.Write((decimal)this.data);
				return;
			case ResourceTypeCode.DateTime:
				writer.Write(((DateTime)this.data).ToBinary());
				return;
			case ResourceTypeCode.TimeSpan:
				writer.Write(((TimeSpan)this.data).Ticks);
				return;
			case ResourceTypeCode.ByteArray:
			case ResourceTypeCode.Stream:
			{
				byte[] array = (byte[])this.data;
				writer.Write(array.Length);
				writer.Write(array);
				return;
			}
			}
			throw new InvalidOperationException("Unknown resource type code");
		}

		// Token: 0x060058A8 RID: 22696 RVA: 0x001B425C File Offset: 0x001B425C
		public override string ToString()
		{
			switch (this.code)
			{
			case ResourceTypeCode.Null:
				return "null";
			case ResourceTypeCode.String:
			case ResourceTypeCode.Boolean:
			case ResourceTypeCode.Char:
			case ResourceTypeCode.Byte:
			case ResourceTypeCode.SByte:
			case ResourceTypeCode.Int16:
			case ResourceTypeCode.UInt16:
			case ResourceTypeCode.Int32:
			case ResourceTypeCode.UInt32:
			case ResourceTypeCode.Int64:
			case ResourceTypeCode.UInt64:
			case ResourceTypeCode.Single:
			case ResourceTypeCode.Double:
			case ResourceTypeCode.Decimal:
			case ResourceTypeCode.DateTime:
			case ResourceTypeCode.TimeSpan:
				return string.Format("{0}: '{1}'", this.code, this.data);
			case ResourceTypeCode.ByteArray:
			case ResourceTypeCode.Stream:
			{
				byte[] array = this.data as byte[];
				if (array != null)
				{
					return string.Format("{0}: Length: {1}", this.code, array.Length);
				}
				return string.Format("{0}: '{1}'", this.code, this.data);
			}
			}
			return string.Format("{0}: '{1}'", this.code, this.data);
		}

		// Token: 0x04002AE1 RID: 10977
		private readonly ResourceTypeCode code;

		// Token: 0x04002AE2 RID: 10978
		private readonly object data;
	}
}
