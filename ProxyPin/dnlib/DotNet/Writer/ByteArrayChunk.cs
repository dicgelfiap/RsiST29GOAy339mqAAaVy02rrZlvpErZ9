using System;
using System.Runtime.InteropServices;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Writer
{
	// Token: 0x0200088F RID: 2191
	[ComVisible(true)]
	public sealed class ByteArrayChunk : IReuseChunk, IChunk
	{
		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x060053E0 RID: 21472 RVA: 0x001990BC File Offset: 0x001990BC
		public FileOffset FileOffset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x060053E1 RID: 21473 RVA: 0x001990C4 File Offset: 0x001990C4
		public RVA RVA
		{
			get
			{
				return this.rva;
			}
		}

		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x060053E2 RID: 21474 RVA: 0x001990CC File Offset: 0x001990CC
		public byte[] Data
		{
			get
			{
				return this.array;
			}
		}

		// Token: 0x060053E3 RID: 21475 RVA: 0x001990D4 File Offset: 0x001990D4
		public ByteArrayChunk(byte[] array)
		{
			this.array = (array ?? Array2.Empty<byte>());
		}

		// Token: 0x060053E4 RID: 21476 RVA: 0x001990F0 File Offset: 0x001990F0
		bool IReuseChunk.CanReuse(RVA origRva, uint origSize)
		{
			return this.array.Length <= (int)origSize;
		}

		// Token: 0x060053E5 RID: 21477 RVA: 0x00199100 File Offset: 0x00199100
		public void SetOffset(FileOffset offset, RVA rva)
		{
			this.offset = offset;
			this.rva = rva;
		}

		// Token: 0x060053E6 RID: 21478 RVA: 0x00199110 File Offset: 0x00199110
		public uint GetFileLength()
		{
			return (uint)this.array.Length;
		}

		// Token: 0x060053E7 RID: 21479 RVA: 0x0019911C File Offset: 0x0019911C
		public uint GetVirtualSize()
		{
			return this.GetFileLength();
		}

		// Token: 0x060053E8 RID: 21480 RVA: 0x00199124 File Offset: 0x00199124
		public void WriteTo(DataWriter writer)
		{
			writer.WriteBytes(this.array);
		}

		// Token: 0x060053E9 RID: 21481 RVA: 0x00199134 File Offset: 0x00199134
		public override int GetHashCode()
		{
			return Utils.GetHashCode(this.array);
		}

		// Token: 0x060053EA RID: 21482 RVA: 0x00199144 File Offset: 0x00199144
		public override bool Equals(object obj)
		{
			ByteArrayChunk byteArrayChunk = obj as ByteArrayChunk;
			return byteArrayChunk != null && Utils.Equals(this.array, byteArrayChunk.array);
		}

		// Token: 0x04002850 RID: 10320
		private readonly byte[] array;

		// Token: 0x04002851 RID: 10321
		private FileOffset offset;

		// Token: 0x04002852 RID: 10322
		private RVA rva;
	}
}
