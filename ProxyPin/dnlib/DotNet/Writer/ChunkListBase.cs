using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Writer
{
	// Token: 0x02000892 RID: 2194
	[ComVisible(true)]
	public abstract class ChunkListBase<T> : IChunk where T : IChunk
	{
		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x060053EE RID: 21486 RVA: 0x00199250 File Offset: 0x00199250
		internal bool IsEmpty
		{
			get
			{
				return this.chunks.Count == 0;
			}
		}

		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x060053EF RID: 21487 RVA: 0x00199260 File Offset: 0x00199260
		public FileOffset FileOffset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x060053F0 RID: 21488 RVA: 0x00199268 File Offset: 0x00199268
		public RVA RVA
		{
			get
			{
				return this.rva;
			}
		}

		// Token: 0x060053F1 RID: 21489 RVA: 0x00199270 File Offset: 0x00199270
		public virtual void SetOffset(FileOffset offset, RVA rva)
		{
			this.setOffsetCalled = true;
			this.offset = offset;
			this.rva = rva;
			this.length = 0U;
			this.virtualSize = 0U;
			foreach (ChunkListBase<T>.Elem elem in this.chunks)
			{
				uint num = offset.AlignUp(elem.alignment) - offset;
				uint num2 = rva.AlignUp(elem.alignment) - rva;
				offset += num;
				rva += num2;
				T chunk = elem.chunk;
				chunk.SetOffset(offset, rva);
				chunk = elem.chunk;
				if (chunk.GetVirtualSize() == 0U)
				{
					offset -= num;
					rva -= num2;
				}
				else
				{
					chunk = elem.chunk;
					uint fileLength = chunk.GetFileLength();
					chunk = elem.chunk;
					uint num3 = chunk.GetVirtualSize();
					offset += fileLength;
					rva += num3;
					this.length += num + fileLength;
					this.virtualSize += num2 + num3;
				}
			}
		}

		// Token: 0x060053F2 RID: 21490 RVA: 0x001993AC File Offset: 0x001993AC
		public uint GetFileLength()
		{
			return this.length;
		}

		// Token: 0x060053F3 RID: 21491 RVA: 0x001993B4 File Offset: 0x001993B4
		public uint GetVirtualSize()
		{
			return this.virtualSize;
		}

		// Token: 0x060053F4 RID: 21492 RVA: 0x001993BC File Offset: 0x001993BC
		public void WriteTo(DataWriter writer)
		{
			FileOffset fileOffset = this.offset;
			foreach (ChunkListBase<T>.Elem elem in this.chunks)
			{
				T chunk = elem.chunk;
				if (chunk.GetVirtualSize() != 0U)
				{
					int num = (int)(fileOffset.AlignUp(elem.alignment) - fileOffset);
					writer.WriteZeroes(num);
					elem.chunk.VerifyWriteTo(writer);
					FileOffset fileOffset2 = fileOffset;
					uint num2 = (uint)num;
					chunk = elem.chunk;
					fileOffset = fileOffset2 + (num2 + chunk.GetFileLength());
				}
			}
		}

		// Token: 0x04002858 RID: 10328
		protected List<ChunkListBase<T>.Elem> chunks;

		// Token: 0x04002859 RID: 10329
		private uint length;

		// Token: 0x0400285A RID: 10330
		private uint virtualSize;

		// Token: 0x0400285B RID: 10331
		protected bool setOffsetCalled;

		// Token: 0x0400285C RID: 10332
		private FileOffset offset;

		// Token: 0x0400285D RID: 10333
		private RVA rva;

		// Token: 0x0200100D RID: 4109
		protected readonly struct Elem
		{
			// Token: 0x06008F05 RID: 36613 RVA: 0x002AB3B8 File Offset: 0x002AB3B8
			public Elem(T chunk, uint alignment)
			{
				this.chunk = chunk;
				this.alignment = alignment;
			}

			// Token: 0x04004464 RID: 17508
			public readonly T chunk;

			// Token: 0x04004465 RID: 17509
			public readonly uint alignment;
		}

		// Token: 0x0200100E RID: 4110
		protected sealed class ElemEqualityComparer : IEqualityComparer<ChunkListBase<T>.Elem>
		{
			// Token: 0x06008F06 RID: 36614 RVA: 0x002AB3C8 File Offset: 0x002AB3C8
			public ElemEqualityComparer(IEqualityComparer<T> chunkComparer)
			{
				this.chunkComparer = chunkComparer;
			}

			// Token: 0x06008F07 RID: 36615 RVA: 0x002AB3D8 File Offset: 0x002AB3D8
			public bool Equals(ChunkListBase<T>.Elem x, ChunkListBase<T>.Elem y)
			{
				return x.alignment == y.alignment && this.chunkComparer.Equals(x.chunk, y.chunk);
			}

			// Token: 0x06008F08 RID: 36616 RVA: 0x002AB404 File Offset: 0x002AB404
			public int GetHashCode(ChunkListBase<T>.Elem obj)
			{
				return (int)(obj.alignment + (uint)this.chunkComparer.GetHashCode(obj.chunk));
			}

			// Token: 0x04004466 RID: 17510
			private IEqualityComparer<T> chunkComparer;
		}
	}
}
