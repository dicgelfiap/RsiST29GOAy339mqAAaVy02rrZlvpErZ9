using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using dnlib.DotNet.Writer;
using dnlib.IO;

namespace dnlib.DotNet.MD
{
	// Token: 0x02000984 RID: 2436
	[DebuggerDisplay("{offset} {size} {name}")]
	[ComVisible(true)]
	public sealed class ColumnInfo
	{
		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x06005DE3 RID: 24035 RVA: 0x001C29B4 File Offset: 0x001C29B4
		public int Index
		{
			get
			{
				return (int)this.index;
			}
		}

		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x06005DE4 RID: 24036 RVA: 0x001C29BC File Offset: 0x001C29BC
		// (set) Token: 0x06005DE5 RID: 24037 RVA: 0x001C29C4 File Offset: 0x001C29C4
		public int Offset
		{
			get
			{
				return (int)this.offset;
			}
			internal set
			{
				this.offset = (byte)value;
			}
		}

		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x06005DE6 RID: 24038 RVA: 0x001C29D0 File Offset: 0x001C29D0
		// (set) Token: 0x06005DE7 RID: 24039 RVA: 0x001C29D8 File Offset: 0x001C29D8
		public int Size
		{
			get
			{
				return (int)this.size;
			}
			internal set
			{
				this.size = (byte)value;
			}
		}

		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x06005DE8 RID: 24040 RVA: 0x001C29E4 File Offset: 0x001C29E4
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001394 RID: 5012
		// (get) Token: 0x06005DE9 RID: 24041 RVA: 0x001C29EC File Offset: 0x001C29EC
		public ColumnSize ColumnSize
		{
			get
			{
				return this.columnSize;
			}
		}

		// Token: 0x06005DEA RID: 24042 RVA: 0x001C29F4 File Offset: 0x001C29F4
		public ColumnInfo(byte index, string name, ColumnSize columnSize)
		{
			this.index = index;
			this.name = name;
			this.columnSize = columnSize;
		}

		// Token: 0x06005DEB RID: 24043 RVA: 0x001C2A14 File Offset: 0x001C2A14
		public ColumnInfo(byte index, string name, ColumnSize columnSize, byte offset, byte size)
		{
			this.index = index;
			this.name = name;
			this.columnSize = columnSize;
			this.offset = offset;
			this.size = size;
		}

		// Token: 0x06005DEC RID: 24044 RVA: 0x001C2A44 File Offset: 0x001C2A44
		public uint Read(ref DataReader reader)
		{
			switch (this.size)
			{
			case 1:
				return (uint)reader.ReadByte();
			case 2:
				return (uint)reader.ReadUInt16();
			case 4:
				return reader.ReadUInt32();
			}
			throw new InvalidOperationException("Invalid column size");
		}

		// Token: 0x06005DED RID: 24045 RVA: 0x001C2AAC File Offset: 0x001C2AAC
		internal uint Unsafe_Read24(ref DataReader reader)
		{
			if (this.size != 2)
			{
				return reader.Unsafe_ReadUInt32();
			}
			return (uint)reader.Unsafe_ReadUInt16();
		}

		// Token: 0x06005DEE RID: 24046 RVA: 0x001C2AC8 File Offset: 0x001C2AC8
		public void Write(DataWriter writer, uint value)
		{
			switch (this.size)
			{
			case 1:
				writer.WriteByte((byte)value);
				return;
			case 2:
				writer.WriteUInt16((ushort)value);
				return;
			case 4:
				writer.WriteUInt32(value);
				return;
			}
			throw new InvalidOperationException("Invalid column size");
		}

		// Token: 0x06005DEF RID: 24047 RVA: 0x001C2B24 File Offset: 0x001C2B24
		internal void Write24(DataWriter writer, uint value)
		{
			if (this.size == 2)
			{
				writer.WriteUInt16((ushort)value);
				return;
			}
			writer.WriteUInt32(value);
		}

		// Token: 0x04002D8E RID: 11662
		private readonly byte index;

		// Token: 0x04002D8F RID: 11663
		private byte offset;

		// Token: 0x04002D90 RID: 11664
		private readonly ColumnSize columnSize;

		// Token: 0x04002D91 RID: 11665
		private byte size;

		// Token: 0x04002D92 RID: 11666
		private readonly string name;
	}
}
