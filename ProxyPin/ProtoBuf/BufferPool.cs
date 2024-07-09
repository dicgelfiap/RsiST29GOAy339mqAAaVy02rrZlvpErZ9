using System;

namespace ProtoBuf
{
	// Token: 0x02000C15 RID: 3093
	internal sealed class BufferPool
	{
		// Token: 0x06007B0C RID: 31500 RVA: 0x0024561C File Offset: 0x0024561C
		internal static void Flush()
		{
			BufferPool.CachedBuffer[] pool = BufferPool.Pool;
			lock (pool)
			{
				for (int i = 0; i < BufferPool.Pool.Length; i++)
				{
					BufferPool.Pool[i] = null;
				}
			}
		}

		// Token: 0x06007B0D RID: 31501 RVA: 0x0024567C File Offset: 0x0024567C
		private BufferPool()
		{
		}

		// Token: 0x06007B0E RID: 31502 RVA: 0x00245684 File Offset: 0x00245684
		internal static byte[] GetBuffer()
		{
			return BufferPool.GetBuffer(1024);
		}

		// Token: 0x06007B0F RID: 31503 RVA: 0x00245690 File Offset: 0x00245690
		internal static byte[] GetBuffer(int minSize)
		{
			byte[] cachedBuffer = BufferPool.GetCachedBuffer(minSize);
			return cachedBuffer ?? new byte[minSize];
		}

		// Token: 0x06007B10 RID: 31504 RVA: 0x002456B8 File Offset: 0x002456B8
		internal static byte[] GetCachedBuffer(int minSize)
		{
			BufferPool.CachedBuffer[] pool = BufferPool.Pool;
			byte[] result;
			lock (pool)
			{
				int num = -1;
				byte[] array = null;
				for (int i = 0; i < BufferPool.Pool.Length; i++)
				{
					BufferPool.CachedBuffer cachedBuffer = BufferPool.Pool[i];
					if (cachedBuffer != null && cachedBuffer.Size >= minSize && (array == null || array.Length >= cachedBuffer.Size))
					{
						byte[] buffer = cachedBuffer.Buffer;
						if (buffer == null)
						{
							BufferPool.Pool[i] = null;
						}
						else
						{
							array = buffer;
							num = i;
						}
					}
				}
				if (num >= 0)
				{
					BufferPool.Pool[num] = null;
				}
				result = array;
			}
			return result;
		}

		// Token: 0x06007B11 RID: 31505 RVA: 0x0024578C File Offset: 0x0024578C
		internal static void ResizeAndFlushLeft(ref byte[] buffer, int toFitAtLeastBytes, int copyFromIndex, int copyBytes)
		{
			int num = buffer.Length * 2;
			if (num < 0)
			{
				num = 2147483591;
			}
			if (num < toFitAtLeastBytes)
			{
				num = toFitAtLeastBytes;
			}
			if (copyBytes == 0)
			{
				BufferPool.ReleaseBufferToPool(ref buffer);
			}
			byte[] array = BufferPool.GetCachedBuffer(toFitAtLeastBytes) ?? new byte[num];
			if (copyBytes > 0)
			{
				Buffer.BlockCopy(buffer, copyFromIndex, array, 0, copyBytes);
				BufferPool.ReleaseBufferToPool(ref buffer);
			}
			buffer = array;
		}

		// Token: 0x06007B12 RID: 31506 RVA: 0x002457F8 File Offset: 0x002457F8
		internal static void ReleaseBufferToPool(ref byte[] buffer)
		{
			if (buffer == null)
			{
				return;
			}
			BufferPool.CachedBuffer[] pool = BufferPool.Pool;
			lock (pool)
			{
				int num = 0;
				int num2 = int.MaxValue;
				for (int i = 0; i < BufferPool.Pool.Length; i++)
				{
					BufferPool.CachedBuffer cachedBuffer = BufferPool.Pool[i];
					if (cachedBuffer == null || !cachedBuffer.IsAlive)
					{
						num = 0;
						break;
					}
					if (cachedBuffer.Size < num2)
					{
						num = i;
						num2 = cachedBuffer.Size;
					}
				}
				BufferPool.Pool[num] = new BufferPool.CachedBuffer(buffer);
			}
			buffer = null;
		}

		// Token: 0x04003B5D RID: 15197
		private const int POOL_SIZE = 20;

		// Token: 0x04003B5E RID: 15198
		internal const int BUFFER_LENGTH = 1024;

		// Token: 0x04003B5F RID: 15199
		private static readonly BufferPool.CachedBuffer[] Pool = new BufferPool.CachedBuffer[20];

		// Token: 0x04003B60 RID: 15200
		private const int MaxByteArraySize = 2147483591;

		// Token: 0x0200116C RID: 4460
		private class CachedBuffer
		{
			// Token: 0x17001E79 RID: 7801
			// (get) Token: 0x0600932E RID: 37678 RVA: 0x002C2630 File Offset: 0x002C2630
			public int Size { get; }

			// Token: 0x17001E7A RID: 7802
			// (get) Token: 0x0600932F RID: 37679 RVA: 0x002C2638 File Offset: 0x002C2638
			public bool IsAlive
			{
				get
				{
					return this._reference.IsAlive;
				}
			}

			// Token: 0x17001E7B RID: 7803
			// (get) Token: 0x06009330 RID: 37680 RVA: 0x002C2648 File Offset: 0x002C2648
			public byte[] Buffer
			{
				get
				{
					return (byte[])this._reference.Target;
				}
			}

			// Token: 0x06009331 RID: 37681 RVA: 0x002C265C File Offset: 0x002C265C
			public CachedBuffer(byte[] buffer)
			{
				this.Size = buffer.Length;
				this._reference = new WeakReference(buffer);
			}

			// Token: 0x04004B24 RID: 19236
			private readonly WeakReference _reference;
		}
	}
}
