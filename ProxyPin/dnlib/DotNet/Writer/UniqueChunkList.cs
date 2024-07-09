using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008DF RID: 2271
	[ComVisible(true)]
	public sealed class UniqueChunkList<T> : ChunkListBase<T> where T : class, IChunk
	{
		// Token: 0x0600586E RID: 22638 RVA: 0x001B2D6C File Offset: 0x001B2D6C
		public UniqueChunkList() : this(EqualityComparer<T>.Default)
		{
		}

		// Token: 0x0600586F RID: 22639 RVA: 0x001B2D7C File Offset: 0x001B2D7C
		public UniqueChunkList(IEqualityComparer<T> chunkComparer)
		{
			this.chunks = new List<ChunkListBase<T>.Elem>();
			this.dict = new Dictionary<ChunkListBase<T>.Elem, ChunkListBase<T>.Elem>(new ChunkListBase<T>.ElemEqualityComparer(chunkComparer));
		}

		// Token: 0x06005870 RID: 22640 RVA: 0x001B2DA0 File Offset: 0x001B2DA0
		public override void SetOffset(FileOffset offset, RVA rva)
		{
			this.dict = null;
			base.SetOffset(offset, rva);
		}

		// Token: 0x06005871 RID: 22641 RVA: 0x001B2DB4 File Offset: 0x001B2DB4
		public T Add(T chunk, uint alignment)
		{
			if (this.setOffsetCalled)
			{
				throw new InvalidOperationException("SetOffset() has already been called");
			}
			if (chunk == null)
			{
				return default(T);
			}
			ChunkListBase<T>.Elem elem = new ChunkListBase<T>.Elem(chunk, alignment);
			ChunkListBase<T>.Elem elem2;
			if (this.dict.TryGetValue(elem, out elem2))
			{
				return elem2.chunk;
			}
			this.dict[elem] = elem;
			this.chunks.Add(elem);
			return elem.chunk;
		}

		// Token: 0x04002ACB RID: 10955
		private Dictionary<ChunkListBase<T>.Elem, ChunkListBase<T>.Elem> dict;
	}
}
