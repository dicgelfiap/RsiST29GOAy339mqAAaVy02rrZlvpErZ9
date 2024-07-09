using System;
using System.Diagnostics;
using System.Threading;

namespace System.Buffers
{
	// Token: 0x02000C88 RID: 3208
	internal sealed class DefaultArrayPool<T> : ArrayPool<T>
	{
		// Token: 0x06008077 RID: 32887 RVA: 0x00260630 File Offset: 0x00260630
		internal DefaultArrayPool() : this(1048576, 50)
		{
		}

		// Token: 0x06008078 RID: 32888 RVA: 0x00260640 File Offset: 0x00260640
		internal DefaultArrayPool(int maxArrayLength, int maxArraysPerBucket)
		{
			if (maxArrayLength <= 0)
			{
				throw new ArgumentOutOfRangeException("maxArrayLength");
			}
			if (maxArraysPerBucket <= 0)
			{
				throw new ArgumentOutOfRangeException("maxArraysPerBucket");
			}
			if (maxArrayLength > 1073741824)
			{
				maxArrayLength = 1073741824;
			}
			else if (maxArrayLength < 16)
			{
				maxArrayLength = 16;
			}
			int id = this.Id;
			int num = Utilities.SelectBucketIndex(maxArrayLength);
			DefaultArrayPool<T>.Bucket[] array = new DefaultArrayPool<T>.Bucket[num + 1];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new DefaultArrayPool<T>.Bucket(Utilities.GetMaxSizeForBucket(i), maxArraysPerBucket, id);
			}
			this._buckets = array;
		}

		// Token: 0x17001BD6 RID: 7126
		// (get) Token: 0x06008079 RID: 32889 RVA: 0x002606E4 File Offset: 0x002606E4
		private int Id
		{
			get
			{
				return this.GetHashCode();
			}
		}

		// Token: 0x0600807A RID: 32890 RVA: 0x002606EC File Offset: 0x002606EC
		public override T[] Rent(int minimumLength)
		{
			if (minimumLength < 0)
			{
				throw new ArgumentOutOfRangeException("minimumLength");
			}
			if (minimumLength == 0)
			{
				T[] result;
				if ((result = DefaultArrayPool<T>.s_emptyArray) == null)
				{
					result = (DefaultArrayPool<T>.s_emptyArray = new T[0]);
				}
				return result;
			}
			ArrayPoolEventSource log = ArrayPoolEventSource.Log;
			int num = Utilities.SelectBucketIndex(minimumLength);
			T[] array;
			if (num < this._buckets.Length)
			{
				int num2 = num;
				for (;;)
				{
					array = this._buckets[num2].Rent();
					if (array != null)
					{
						break;
					}
					if (++num2 >= this._buckets.Length || num2 == num + 2)
					{
						goto IL_B3;
					}
				}
				if (log.IsEnabled())
				{
					log.BufferRented(array.GetHashCode(), array.Length, this.Id, this._buckets[num2].Id);
				}
				return array;
				IL_B3:
				array = new T[this._buckets[num]._bufferLength];
			}
			else
			{
				array = new T[minimumLength];
			}
			if (log.IsEnabled())
			{
				int hashCode = array.GetHashCode();
				int bucketId = -1;
				log.BufferRented(hashCode, array.Length, this.Id, bucketId);
				log.BufferAllocated(hashCode, array.Length, this.Id, bucketId, (num >= this._buckets.Length) ? ArrayPoolEventSource.BufferAllocatedReason.OverMaximumSize : ArrayPoolEventSource.BufferAllocatedReason.PoolExhausted);
			}
			return array;
		}

		// Token: 0x0600807B RID: 32891 RVA: 0x00260828 File Offset: 0x00260828
		public override void Return(T[] array, bool clearArray = false)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Length == 0)
			{
				return;
			}
			int num = Utilities.SelectBucketIndex(array.Length);
			if (num < this._buckets.Length)
			{
				if (clearArray)
				{
					Array.Clear(array, 0, array.Length);
				}
				this._buckets[num].Return(array);
			}
			ArrayPoolEventSource log = ArrayPoolEventSource.Log;
			if (log.IsEnabled())
			{
				log.BufferReturned(array.GetHashCode(), array.Length, this.Id);
			}
		}

		// Token: 0x04003D16 RID: 15638
		private const int DefaultMaxArrayLength = 1048576;

		// Token: 0x04003D17 RID: 15639
		private const int DefaultMaxNumberOfArraysPerBucket = 50;

		// Token: 0x04003D18 RID: 15640
		private static T[] s_emptyArray;

		// Token: 0x04003D19 RID: 15641
		private readonly DefaultArrayPool<T>.Bucket[] _buckets;

		// Token: 0x0200118C RID: 4492
		private sealed class Bucket
		{
			// Token: 0x060093B3 RID: 37811 RVA: 0x002C3AF0 File Offset: 0x002C3AF0
			internal Bucket(int bufferLength, int numberOfBuffers, int poolId)
			{
				this._lock = new SpinLock(Debugger.IsAttached);
				this._buffers = new T[numberOfBuffers][];
				this._bufferLength = bufferLength;
				this._poolId = poolId;
			}

			// Token: 0x17001E98 RID: 7832
			// (get) Token: 0x060093B4 RID: 37812 RVA: 0x002C3B24 File Offset: 0x002C3B24
			internal int Id
			{
				get
				{
					return this.GetHashCode();
				}
			}

			// Token: 0x060093B5 RID: 37813 RVA: 0x002C3B2C File Offset: 0x002C3B2C
			internal T[] Rent()
			{
				T[][] buffers = this._buffers;
				T[] array = null;
				bool flag = false;
				bool flag2 = false;
				try
				{
					this._lock.Enter(ref flag);
					if (this._index < buffers.Length)
					{
						array = buffers[this._index];
						T[][] array2 = buffers;
						int index = this._index;
						this._index = index + 1;
						array2[index] = null;
						flag2 = (array == null);
					}
				}
				finally
				{
					if (flag)
					{
						this._lock.Exit(false);
					}
				}
				if (flag2)
				{
					array = new T[this._bufferLength];
					ArrayPoolEventSource log = ArrayPoolEventSource.Log;
					if (log.IsEnabled())
					{
						log.BufferAllocated(array.GetHashCode(), this._bufferLength, this._poolId, this.Id, ArrayPoolEventSource.BufferAllocatedReason.Pooled);
					}
				}
				return array;
			}

			// Token: 0x060093B6 RID: 37814 RVA: 0x002C3BF8 File Offset: 0x002C3BF8
			internal void Return(T[] array)
			{
				if (array.Length != this._bufferLength)
				{
					throw new ArgumentException(SR.ArgumentException_BufferNotFromPool, "array");
				}
				bool flag = false;
				try
				{
					this._lock.Enter(ref flag);
					if (this._index != 0)
					{
						T[][] buffers = this._buffers;
						int num = this._index - 1;
						this._index = num;
						buffers[num] = array;
					}
				}
				finally
				{
					if (flag)
					{
						this._lock.Exit(false);
					}
				}
			}

			// Token: 0x04004B9C RID: 19356
			internal readonly int _bufferLength;

			// Token: 0x04004B9D RID: 19357
			private readonly T[][] _buffers;

			// Token: 0x04004B9E RID: 19358
			private readonly int _poolId;

			// Token: 0x04004B9F RID: 19359
			private SpinLock _lock;

			// Token: 0x04004BA0 RID: 19360
			private int _index;
		}
	}
}
