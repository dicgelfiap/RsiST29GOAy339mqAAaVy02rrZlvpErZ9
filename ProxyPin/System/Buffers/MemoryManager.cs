using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x02000CF1 RID: 3313
	[ComVisible(true)]
	public abstract class MemoryManager<T> : IMemoryOwner<T>, IDisposable, IPinnable
	{
		// Token: 0x17001CAD RID: 7341
		// (get) Token: 0x060085E9 RID: 34281 RVA: 0x00273590 File Offset: 0x00273590
		public virtual Memory<T> Memory
		{
			get
			{
				return new Memory<T>(this, this.GetSpan().Length);
			}
		}

		// Token: 0x060085EA RID: 34282
		public abstract Span<T> GetSpan();

		// Token: 0x060085EB RID: 34283
		public abstract MemoryHandle Pin(int elementIndex = 0);

		// Token: 0x060085EC RID: 34284
		public abstract void Unpin();

		// Token: 0x060085ED RID: 34285 RVA: 0x002735B8 File Offset: 0x002735B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected Memory<T> CreateMemory(int length)
		{
			return new Memory<T>(this, length);
		}

		// Token: 0x060085EE RID: 34286 RVA: 0x002735C4 File Offset: 0x002735C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected Memory<T> CreateMemory(int start, int length)
		{
			return new Memory<T>(this, start, length);
		}

		// Token: 0x060085EF RID: 34287 RVA: 0x002735D0 File Offset: 0x002735D0
		protected internal virtual bool TryGetArray(out ArraySegment<T> segment)
		{
			segment = default(ArraySegment<T>);
			return false;
		}

		// Token: 0x060085F0 RID: 34288 RVA: 0x002735DC File Offset: 0x002735DC
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060085F1 RID: 34289
		protected abstract void Dispose(bool disposing);
	}
}
