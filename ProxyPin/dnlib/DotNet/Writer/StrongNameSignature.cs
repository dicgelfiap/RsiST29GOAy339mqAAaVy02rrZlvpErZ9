using System;
using System.Runtime.InteropServices;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008DC RID: 2268
	[ComVisible(true)]
	public sealed class StrongNameSignature : IReuseChunk, IChunk
	{
		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x06005848 RID: 22600 RVA: 0x001B1A94 File Offset: 0x001B1A94
		public FileOffset FileOffset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x06005849 RID: 22601 RVA: 0x001B1A9C File Offset: 0x001B1A9C
		public RVA RVA
		{
			get
			{
				return this.rva;
			}
		}

		// Token: 0x0600584A RID: 22602 RVA: 0x001B1AA4 File Offset: 0x001B1AA4
		public StrongNameSignature(int size)
		{
			this.size = size;
		}

		// Token: 0x0600584B RID: 22603 RVA: 0x001B1AB4 File Offset: 0x001B1AB4
		bool IReuseChunk.CanReuse(RVA origRva, uint origSize)
		{
			return this.size <= (int)origSize;
		}

		// Token: 0x0600584C RID: 22604 RVA: 0x001B1AC4 File Offset: 0x001B1AC4
		public void SetOffset(FileOffset offset, RVA rva)
		{
			this.offset = offset;
			this.rva = rva;
		}

		// Token: 0x0600584D RID: 22605 RVA: 0x001B1AD4 File Offset: 0x001B1AD4
		public uint GetFileLength()
		{
			return (uint)this.size;
		}

		// Token: 0x0600584E RID: 22606 RVA: 0x001B1ADC File Offset: 0x001B1ADC
		public uint GetVirtualSize()
		{
			return this.GetFileLength();
		}

		// Token: 0x0600584F RID: 22607 RVA: 0x001B1AE4 File Offset: 0x001B1AE4
		public void WriteTo(DataWriter writer)
		{
			writer.WriteZeroes(this.size);
		}

		// Token: 0x04002A80 RID: 10880
		private FileOffset offset;

		// Token: 0x04002A81 RID: 10881
		private RVA rva;

		// Token: 0x04002A82 RID: 10882
		private int size;
	}
}
