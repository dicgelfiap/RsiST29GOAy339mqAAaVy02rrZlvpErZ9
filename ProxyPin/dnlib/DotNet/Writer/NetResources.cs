using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008CB RID: 2251
	[ComVisible(true)]
	public sealed class NetResources : IReuseChunk, IChunk
	{
		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x06005766 RID: 22374 RVA: 0x001AB9DC File Offset: 0x001AB9DC
		internal bool IsEmpty
		{
			get
			{
				return this.resources.Count == 0;
			}
		}

		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x06005767 RID: 22375 RVA: 0x001AB9EC File Offset: 0x001AB9EC
		public FileOffset FileOffset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x06005768 RID: 22376 RVA: 0x001AB9F4 File Offset: 0x001AB9F4
		public RVA RVA
		{
			get
			{
				return this.rva;
			}
		}

		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x06005769 RID: 22377 RVA: 0x001AB9FC File Offset: 0x001AB9FC
		public uint NextOffset
		{
			get
			{
				return Utils.AlignUp(this.length, this.alignment);
			}
		}

		// Token: 0x0600576A RID: 22378 RVA: 0x001ABA10 File Offset: 0x001ABA10
		public NetResources(uint alignment)
		{
			this.alignment = alignment;
		}

		// Token: 0x0600576B RID: 22379 RVA: 0x001ABA2C File Offset: 0x001ABA2C
		public DataReaderChunk Add(DataReader reader)
		{
			if (this.setOffsetCalled)
			{
				throw new InvalidOperationException("SetOffset() has already been called");
			}
			this.length = this.NextOffset + 4U + reader.Length;
			DataReaderChunk dataReaderChunk = new DataReaderChunk(ref reader);
			this.resources.Add(dataReaderChunk);
			return dataReaderChunk;
		}

		// Token: 0x0600576C RID: 22380 RVA: 0x001ABA80 File Offset: 0x001ABA80
		bool IReuseChunk.CanReuse(RVA origRva, uint origSize)
		{
			return this.length <= origSize;
		}

		// Token: 0x0600576D RID: 22381 RVA: 0x001ABA90 File Offset: 0x001ABA90
		public void SetOffset(FileOffset offset, RVA rva)
		{
			this.setOffsetCalled = true;
			this.offset = offset;
			this.rva = rva;
			foreach (DataReaderChunk dataReaderChunk in this.resources)
			{
				offset = offset.AlignUp(this.alignment);
				rva = rva.AlignUp(this.alignment);
				dataReaderChunk.SetOffset(offset + 4U, rva + 4U);
				uint num = 4U + dataReaderChunk.GetFileLength();
				offset += num;
				rva += num;
			}
		}

		// Token: 0x0600576E RID: 22382 RVA: 0x001ABB34 File Offset: 0x001ABB34
		public uint GetFileLength()
		{
			return this.length;
		}

		// Token: 0x0600576F RID: 22383 RVA: 0x001ABB3C File Offset: 0x001ABB3C
		public uint GetVirtualSize()
		{
			return this.GetFileLength();
		}

		// Token: 0x06005770 RID: 22384 RVA: 0x001ABB44 File Offset: 0x001ABB44
		public void WriteTo(DataWriter writer)
		{
			RVA rva = this.rva;
			foreach (DataReaderChunk dataReaderChunk in this.resources)
			{
				int num = (int)(rva.AlignUp(this.alignment) - rva);
				writer.WriteZeroes(num);
				rva += (uint)num;
				writer.WriteUInt32(dataReaderChunk.GetFileLength());
				dataReaderChunk.VerifyWriteTo(writer);
				rva += 4U + dataReaderChunk.GetFileLength();
			}
		}

		// Token: 0x040029EF RID: 10735
		private readonly List<DataReaderChunk> resources = new List<DataReaderChunk>();

		// Token: 0x040029F0 RID: 10736
		private readonly uint alignment;

		// Token: 0x040029F1 RID: 10737
		private uint length;

		// Token: 0x040029F2 RID: 10738
		private bool setOffsetCalled;

		// Token: 0x040029F3 RID: 10739
		private FileOffset offset;

		// Token: 0x040029F4 RID: 10740
		private RVA rva;
	}
}
