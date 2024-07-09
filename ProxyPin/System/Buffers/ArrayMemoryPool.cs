using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000CE4 RID: 3300
	internal sealed class ArrayMemoryPool<T> : MemoryPool<T>
	{
		// Token: 0x17001C99 RID: 7321
		// (get) Token: 0x06008584 RID: 34180 RVA: 0x00271DF0 File Offset: 0x00271DF0
		public sealed override int MaxBufferSize
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x06008585 RID: 34181 RVA: 0x00271DF8 File Offset: 0x00271DF8
		public sealed override IMemoryOwner<T> Rent(int minimumBufferSize = -1)
		{
			if (minimumBufferSize == -1)
			{
				minimumBufferSize = 1 + 4095 / Unsafe.SizeOf<T>();
			}
			else if (minimumBufferSize > 2147483647)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.minimumBufferSize);
			}
			return new ArrayMemoryPool<T>.ArrayMemoryPoolBuffer(minimumBufferSize);
		}

		// Token: 0x06008586 RID: 34182 RVA: 0x00271E2C File Offset: 0x00271E2C
		protected sealed override void Dispose(bool disposing)
		{
		}

		// Token: 0x04003DC8 RID: 15816
		private const int s_maxBufferSize = 2147483647;

		// Token: 0x020011C6 RID: 4550
		private sealed class ArrayMemoryPoolBuffer : IMemoryOwner<T>, IDisposable
		{
			// Token: 0x06009672 RID: 38514 RVA: 0x002CC0CC File Offset: 0x002CC0CC
			public ArrayMemoryPoolBuffer(int size)
			{
				this._array = ArrayPool<T>.Shared.Rent(size);
			}

			// Token: 0x17001F49 RID: 8009
			// (get) Token: 0x06009673 RID: 38515 RVA: 0x002CC0E8 File Offset: 0x002CC0E8
			public Memory<T> Memory
			{
				get
				{
					T[] array = this._array;
					if (array == null)
					{
						ThrowHelper.ThrowObjectDisposedException_ArrayMemoryPoolBuffer();
					}
					return new Memory<T>(array);
				}
			}

			// Token: 0x06009674 RID: 38516 RVA: 0x002CC114 File Offset: 0x002CC114
			public void Dispose()
			{
				T[] array = this._array;
				if (array != null)
				{
					this._array = null;
					ArrayPool<T>.Shared.Return(array, false);
				}
			}

			// Token: 0x04004C6C RID: 19564
			private T[] _array;
		}
	}
}
