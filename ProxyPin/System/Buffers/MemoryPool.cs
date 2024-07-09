using System;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x02000CE7 RID: 3303
	[ComVisible(true)]
	public abstract class MemoryPool<T> : IDisposable
	{
		// Token: 0x17001C9A RID: 7322
		// (get) Token: 0x06008592 RID: 34194 RVA: 0x00272088 File Offset: 0x00272088
		public static MemoryPool<T> Shared
		{
			get
			{
				return MemoryPool<T>.s_shared;
			}
		}

		// Token: 0x06008593 RID: 34195
		public abstract IMemoryOwner<T> Rent(int minBufferSize = -1);

		// Token: 0x17001C9B RID: 7323
		// (get) Token: 0x06008594 RID: 34196
		public abstract int MaxBufferSize { get; }

		// Token: 0x06008596 RID: 34198 RVA: 0x00272098 File Offset: 0x00272098
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06008597 RID: 34199
		protected abstract void Dispose(bool disposing);

		// Token: 0x04003DC9 RID: 15817
		private static readonly MemoryPool<T> s_shared = new ArrayMemoryPool<T>();
	}
}
