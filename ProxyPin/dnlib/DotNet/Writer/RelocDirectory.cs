using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008D3 RID: 2259
	[ComVisible(true)]
	public sealed class RelocDirectory : IChunk
	{
		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x06005802 RID: 22530 RVA: 0x001B0188 File Offset: 0x001B0188
		public FileOffset FileOffset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x06005803 RID: 22531 RVA: 0x001B0190 File Offset: 0x001B0190
		public RVA RVA
		{
			get
			{
				return this.rva;
			}
		}

		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x06005804 RID: 22532 RVA: 0x001B0198 File Offset: 0x001B0198
		internal bool NeedsRelocSection
		{
			get
			{
				return this.allRelocRvas.Count != 0;
			}
		}

		// Token: 0x06005805 RID: 22533 RVA: 0x001B01A8 File Offset: 0x001B01A8
		public RelocDirectory(Machine machine)
		{
			this.machine = machine;
		}

		// Token: 0x06005806 RID: 22534 RVA: 0x001B01D0 File Offset: 0x001B01D0
		public void SetOffset(FileOffset offset, RVA rva)
		{
			this.isReadOnly = true;
			this.offset = offset;
			this.rva = rva;
			List<uint> list = new List<uint>(this.allRelocRvas.Count);
			foreach (RelocDirectory.RelocInfo relocInfo in this.allRelocRvas)
			{
				uint item;
				if (relocInfo.Chunk != null)
				{
					item = (uint)(relocInfo.Chunk.RVA + relocInfo.OffsetOrRva);
				}
				else
				{
					item = relocInfo.OffsetOrRva;
				}
				list.Add(item);
			}
			list.Sort();
			uint num = uint.MaxValue;
			List<uint> list2 = null;
			foreach (uint num2 in list)
			{
				uint num3 = num2 & 4294963200U;
				if (num3 != num)
				{
					num = num3;
					if (list2 != null)
					{
						this.totalSize += (uint)(8 + (list2.Count + 1 & -2) * 2);
					}
					list2 = new List<uint>();
					this.relocSections.Add(list2);
				}
				list2.Add(num2);
			}
			if (list2 != null)
			{
				this.totalSize += (uint)(8 + (list2.Count + 1 & -2) * 2);
			}
		}

		// Token: 0x06005807 RID: 22535 RVA: 0x001B0340 File Offset: 0x001B0340
		public uint GetFileLength()
		{
			return this.totalSize;
		}

		// Token: 0x06005808 RID: 22536 RVA: 0x001B0348 File Offset: 0x001B0348
		public uint GetVirtualSize()
		{
			return this.GetFileLength();
		}

		// Token: 0x06005809 RID: 22537 RVA: 0x001B0350 File Offset: 0x001B0350
		public void WriteTo(DataWriter writer)
		{
			uint num = this.machine.Is64Bit() ? 40960U : 12288U;
			foreach (List<uint> list in this.relocSections)
			{
				writer.WriteUInt32(list[0] & 4294963200U);
				writer.WriteUInt32((uint)(8 + (list.Count + 1 & -2) * 2));
				foreach (uint num2 in list)
				{
					writer.WriteUInt16((ushort)(num | (num2 & 4095U)));
				}
				if ((list.Count & 1) != 0)
				{
					writer.WriteUInt16(0);
				}
			}
		}

		// Token: 0x0600580A RID: 22538 RVA: 0x001B0450 File Offset: 0x001B0450
		public void Add(RVA rva)
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException("Can't add a relocation when the relocs section is read-only");
			}
			this.allRelocRvas.Add(new RelocDirectory.RelocInfo(null, (uint)rva));
		}

		// Token: 0x0600580B RID: 22539 RVA: 0x001B047C File Offset: 0x001B047C
		public void Add(IChunk chunk, uint offset)
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException("Can't add a relocation when the relocs section is read-only");
			}
			this.allRelocRvas.Add(new RelocDirectory.RelocInfo(chunk, offset));
		}

		// Token: 0x04002A50 RID: 10832
		private readonly Machine machine;

		// Token: 0x04002A51 RID: 10833
		private readonly List<RelocDirectory.RelocInfo> allRelocRvas = new List<RelocDirectory.RelocInfo>();

		// Token: 0x04002A52 RID: 10834
		private readonly List<List<uint>> relocSections = new List<List<uint>>();

		// Token: 0x04002A53 RID: 10835
		private bool isReadOnly;

		// Token: 0x04002A54 RID: 10836
		private FileOffset offset;

		// Token: 0x04002A55 RID: 10837
		private RVA rva;

		// Token: 0x04002A56 RID: 10838
		private uint totalSize;

		// Token: 0x02001028 RID: 4136
		private readonly struct RelocInfo
		{
			// Token: 0x06008F95 RID: 36757 RVA: 0x002ACB04 File Offset: 0x002ACB04
			public RelocInfo(IChunk chunk, uint offset)
			{
				this.Chunk = chunk;
				this.OffsetOrRva = offset;
			}

			// Token: 0x040044DE RID: 17630
			public readonly IChunk Chunk;

			// Token: 0x040044DF RID: 17631
			public readonly uint OffsetOrRva;
		}
	}
}
