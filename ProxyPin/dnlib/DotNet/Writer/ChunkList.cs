using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x02000891 RID: 2193
	[ComVisible(true)]
	public class ChunkList<T> : ChunkListBase<T> where T : class, IChunk
	{
		// Token: 0x060053EB RID: 21483 RVA: 0x00199178 File Offset: 0x00199178
		public ChunkList()
		{
			this.chunks = new List<ChunkListBase<T>.Elem>();
		}

		// Token: 0x060053EC RID: 21484 RVA: 0x0019918C File Offset: 0x0019918C
		public void Add(T chunk, uint alignment)
		{
			if (this.setOffsetCalled)
			{
				throw new InvalidOperationException("SetOffset() has already been called");
			}
			if (chunk != null)
			{
				this.chunks.Add(new ChunkListBase<T>.Elem(chunk, alignment));
			}
		}

		// Token: 0x060053ED RID: 21485 RVA: 0x001991C4 File Offset: 0x001991C4
		public uint? Remove(T chunk)
		{
			if (this.setOffsetCalled)
			{
				throw new InvalidOperationException("SetOffset() has already been called");
			}
			if (chunk != null)
			{
				List<ChunkListBase<T>.Elem> chunks = this.chunks;
				for (int i = 0; i < chunks.Count; i++)
				{
					if (chunks[i].chunk == chunk)
					{
						uint alignment = chunks[i].alignment;
						chunks.RemoveAt(i);
						return new uint?(alignment);
					}
				}
			}
			return null;
		}
	}
}
