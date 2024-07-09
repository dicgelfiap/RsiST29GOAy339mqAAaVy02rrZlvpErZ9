using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008B8 RID: 2232
	[ComVisible(true)]
	public sealed class MetadataHeader : IChunk
	{
		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x06005629 RID: 22057 RVA: 0x001A58E4 File Offset: 0x001A58E4
		public FileOffset FileOffset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x0600562A RID: 22058 RVA: 0x001A58EC File Offset: 0x001A58EC
		public RVA RVA
		{
			get
			{
				return this.rva;
			}
		}

		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x0600562B RID: 22059 RVA: 0x001A58F4 File Offset: 0x001A58F4
		// (set) Token: 0x0600562C RID: 22060 RVA: 0x001A58FC File Offset: 0x001A58FC
		public IList<IHeap> Heaps
		{
			get
			{
				return this.heaps;
			}
			set
			{
				this.heaps = value;
			}
		}

		// Token: 0x0600562D RID: 22061 RVA: 0x001A5908 File Offset: 0x001A5908
		public MetadataHeader() : this(null)
		{
		}

		// Token: 0x0600562E RID: 22062 RVA: 0x001A5914 File Offset: 0x001A5914
		public MetadataHeader(MetadataHeaderOptions options)
		{
			this.options = (options ?? new MetadataHeaderOptions());
		}

		// Token: 0x0600562F RID: 22063 RVA: 0x001A5930 File Offset: 0x001A5930
		public void SetOffset(FileOffset offset, RVA rva)
		{
			this.offset = offset;
			this.rva = rva;
			this.length = 16U;
			this.length += (uint)this.GetVersionString().Length;
			this.length = Utils.AlignUp(this.length, 4U);
			this.length += 4U;
			IList<IHeap> list = this.heaps;
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				IHeap heap = list[i];
				this.length += 8U;
				this.length += (uint)this.GetAsciizName(heap.Name).Length;
				this.length = Utils.AlignUp(this.length, 4U);
			}
		}

		// Token: 0x06005630 RID: 22064 RVA: 0x001A59EC File Offset: 0x001A59EC
		public uint GetFileLength()
		{
			return this.length;
		}

		// Token: 0x06005631 RID: 22065 RVA: 0x001A59F4 File Offset: 0x001A59F4
		public uint GetVirtualSize()
		{
			return this.GetFileLength();
		}

		// Token: 0x06005632 RID: 22066 RVA: 0x001A59FC File Offset: 0x001A59FC
		public void WriteTo(DataWriter writer)
		{
			writer.WriteUInt32(this.options.Signature ?? 1112167234U);
			writer.WriteUInt16(this.options.MajorVersion ?? 1);
			writer.WriteUInt16(this.options.MinorVersion ?? 1);
			writer.WriteUInt32(this.options.Reserved1.GetValueOrDefault());
			byte[] array = this.GetVersionString();
			writer.WriteInt32(Utils.AlignUp(array.Length, 4U));
			writer.WriteBytes(array);
			writer.WriteZeroes(Utils.AlignUp(array.Length, 4U) - array.Length);
			writer.WriteByte((byte)this.options.StorageFlags.GetValueOrDefault());
			writer.WriteByte(this.options.Reserved2.GetValueOrDefault());
			IList<IHeap> list = this.heaps;
			writer.WriteUInt16((ushort)list.Count);
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				IHeap heap = list[i];
				writer.WriteUInt32(heap.FileOffset - this.offset);
				writer.WriteUInt32(heap.GetFileLength());
				writer.WriteBytes(array = this.GetAsciizName(heap.Name));
				if (array.Length > 32)
				{
					throw new ModuleWriterException("Heap name '" + heap.Name + "' is > 32 bytes");
				}
				writer.WriteZeroes(Utils.AlignUp(array.Length, 4U) - array.Length);
			}
		}

		// Token: 0x06005633 RID: 22067 RVA: 0x001A5BB0 File Offset: 0x001A5BB0
		private byte[] GetVersionString()
		{
			return Encoding.UTF8.GetBytes((this.options.VersionString ?? "v2.0.50727") + "\0");
		}

		// Token: 0x06005634 RID: 22068 RVA: 0x001A5BE0 File Offset: 0x001A5BE0
		private byte[] GetAsciizName(string s)
		{
			return Encoding.ASCII.GetBytes(s + "\0");
		}

		// Token: 0x0400294F RID: 10575
		private IList<IHeap> heaps;

		// Token: 0x04002950 RID: 10576
		private readonly MetadataHeaderOptions options;

		// Token: 0x04002951 RID: 10577
		private uint length;

		// Token: 0x04002952 RID: 10578
		private FileOffset offset;

		// Token: 0x04002953 RID: 10579
		private RVA rva;
	}
}
