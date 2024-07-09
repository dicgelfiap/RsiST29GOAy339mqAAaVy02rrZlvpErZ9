using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Writer
{
	// Token: 0x02000899 RID: 2201
	[ComVisible(true)]
	public sealed class DebugDirectory : IReuseChunk, IChunk
	{
		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x06005441 RID: 21569 RVA: 0x0019B264 File Offset: 0x0019B264
		internal int Count
		{
			get
			{
				return this.entries.Count;
			}
		}

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x06005442 RID: 21570 RVA: 0x0019B274 File Offset: 0x0019B274
		public FileOffset FileOffset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x06005443 RID: 21571 RVA: 0x0019B27C File Offset: 0x0019B27C
		public RVA RVA
		{
			get
			{
				return this.rva;
			}
		}

		// Token: 0x06005444 RID: 21572 RVA: 0x0019B284 File Offset: 0x0019B284
		public DebugDirectory()
		{
			this.entries = new List<DebugDirectoryEntry>();
		}

		// Token: 0x06005445 RID: 21573 RVA: 0x0019B298 File Offset: 0x0019B298
		public DebugDirectoryEntry Add(byte[] data)
		{
			return this.Add(new ByteArrayChunk(data));
		}

		// Token: 0x06005446 RID: 21574 RVA: 0x0019B2A8 File Offset: 0x0019B2A8
		public DebugDirectoryEntry Add(IChunk chunk)
		{
			if (this.isReadonly)
			{
				throw new InvalidOperationException("Can't add a new DebugDirectory entry when the DebugDirectory is read-only!");
			}
			DebugDirectoryEntry debugDirectoryEntry = new DebugDirectoryEntry(chunk);
			this.entries.Add(debugDirectoryEntry);
			return debugDirectoryEntry;
		}

		// Token: 0x06005447 RID: 21575 RVA: 0x0019B2E4 File Offset: 0x0019B2E4
		public DebugDirectoryEntry Add(byte[] data, ImageDebugType type, ushort majorVersion, ushort minorVersion, uint timeDateStamp)
		{
			return this.Add(new ByteArrayChunk(data), type, majorVersion, minorVersion, timeDateStamp);
		}

		// Token: 0x06005448 RID: 21576 RVA: 0x0019B2F8 File Offset: 0x0019B2F8
		public DebugDirectoryEntry Add(IChunk chunk, ImageDebugType type, ushort majorVersion, ushort minorVersion, uint timeDateStamp)
		{
			DebugDirectoryEntry debugDirectoryEntry = this.Add(chunk);
			debugDirectoryEntry.DebugDirectory.Type = type;
			debugDirectoryEntry.DebugDirectory.MajorVersion = majorVersion;
			debugDirectoryEntry.DebugDirectory.MinorVersion = minorVersion;
			debugDirectoryEntry.DebugDirectory.TimeDateStamp = timeDateStamp;
			return debugDirectoryEntry;
		}

		// Token: 0x06005449 RID: 21577 RVA: 0x0019B334 File Offset: 0x0019B334
		bool IReuseChunk.CanReuse(RVA origRva, uint origSize)
		{
			if (DebugDirectory.GetLength(this.entries, (FileOffset)origRva, origRva) > origSize)
			{
				return false;
			}
			this.isReadonly = true;
			return true;
		}

		// Token: 0x0600544A RID: 21578 RVA: 0x0019B354 File Offset: 0x0019B354
		public void SetOffset(FileOffset offset, RVA rva)
		{
			this.isReadonly = true;
			this.offset = offset;
			this.rva = rva;
			this.length = DebugDirectory.GetLength(this.entries, offset, rva);
		}

		// Token: 0x0600544B RID: 21579 RVA: 0x0019B380 File Offset: 0x0019B380
		private static uint GetLength(List<DebugDirectoryEntry> entries, FileOffset offset, RVA rva)
		{
			uint num = (uint)(28 * entries.Count);
			foreach (DebugDirectoryEntry debugDirectoryEntry in entries)
			{
				num = Utils.AlignUp(num, 4U);
				debugDirectoryEntry.Chunk.SetOffset(offset + num, rva + num);
				num += debugDirectoryEntry.Chunk.GetFileLength();
			}
			return num;
		}

		// Token: 0x0600544C RID: 21580 RVA: 0x0019B400 File Offset: 0x0019B400
		public uint GetFileLength()
		{
			return this.length;
		}

		// Token: 0x0600544D RID: 21581 RVA: 0x0019B408 File Offset: 0x0019B408
		public uint GetVirtualSize()
		{
			return this.GetFileLength();
		}

		// Token: 0x0600544E RID: 21582 RVA: 0x0019B410 File Offset: 0x0019B410
		public void WriteTo(DataWriter writer)
		{
			uint num = 0U;
			foreach (DebugDirectoryEntry debugDirectoryEntry in this.entries)
			{
				writer.WriteUInt32(debugDirectoryEntry.DebugDirectory.Characteristics);
				writer.WriteUInt32(debugDirectoryEntry.DebugDirectory.TimeDateStamp);
				writer.WriteUInt16(debugDirectoryEntry.DebugDirectory.MajorVersion);
				writer.WriteUInt16(debugDirectoryEntry.DebugDirectory.MinorVersion);
				writer.WriteUInt32((uint)debugDirectoryEntry.DebugDirectory.Type);
				uint fileLength = debugDirectoryEntry.Chunk.GetFileLength();
				writer.WriteUInt32(fileLength);
				writer.WriteUInt32((uint)((fileLength == 0U) ? ((RVA)0U) : debugDirectoryEntry.Chunk.RVA));
				writer.WriteUInt32((uint)((fileLength == 0U) ? ((FileOffset)0U) : debugDirectoryEntry.Chunk.FileOffset));
				num += 28U;
			}
			foreach (DebugDirectoryEntry debugDirectoryEntry2 in this.entries)
			{
				DebugDirectory.WriteAlign(writer, ref num);
				debugDirectoryEntry2.Chunk.VerifyWriteTo(writer);
				num += debugDirectoryEntry2.Chunk.GetFileLength();
			}
		}

		// Token: 0x0600544F RID: 21583 RVA: 0x0019B574 File Offset: 0x0019B574
		private static void WriteAlign(DataWriter writer, ref uint offs)
		{
			uint num = Utils.AlignUp(offs, 4U) - offs;
			offs += num;
			writer.WriteZeroes((int)num);
		}

		// Token: 0x04002871 RID: 10353
		public const uint DEFAULT_DEBUGDIRECTORY_ALIGNMENT = 4U;

		// Token: 0x04002872 RID: 10354
		internal const int HEADER_SIZE = 28;

		// Token: 0x04002873 RID: 10355
		private FileOffset offset;

		// Token: 0x04002874 RID: 10356
		private RVA rva;

		// Token: 0x04002875 RID: 10357
		private uint length;

		// Token: 0x04002876 RID: 10358
		private readonly List<DebugDirectoryEntry> entries;

		// Token: 0x04002877 RID: 10359
		private bool isReadonly;
	}
}
