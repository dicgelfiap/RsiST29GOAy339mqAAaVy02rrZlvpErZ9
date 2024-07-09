using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Buffers
{
	// Token: 0x02000C86 RID: 3206
	[ComVisible(true)]
	public abstract class ArrayPool<T>
	{
		// Token: 0x17001BD5 RID: 7125
		// (get) Token: 0x0600806A RID: 32874 RVA: 0x002603D8 File Offset: 0x002603D8
		public static ArrayPool<T> Shared
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Volatile.Read<ArrayPool<T>>(ref ArrayPool<T>.s_sharedInstance) ?? ArrayPool<T>.EnsureSharedCreated();
			}
		}

		// Token: 0x0600806B RID: 32875 RVA: 0x002603F0 File Offset: 0x002603F0
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static ArrayPool<T> EnsureSharedCreated()
		{
			Interlocked.CompareExchange<ArrayPool<T>>(ref ArrayPool<T>.s_sharedInstance, ArrayPool<T>.Create(), null);
			return ArrayPool<T>.s_sharedInstance;
		}

		// Token: 0x0600806C RID: 32876 RVA: 0x00260408 File Offset: 0x00260408
		public static ArrayPool<T> Create()
		{
			return new DefaultArrayPool<T>();
		}

		// Token: 0x0600806D RID: 32877 RVA: 0x00260410 File Offset: 0x00260410
		public static ArrayPool<T> Create(int maxArrayLength, int maxArraysPerBucket)
		{
			return new DefaultArrayPool<T>(maxArrayLength, maxArraysPerBucket);
		}

		// Token: 0x0600806E RID: 32878
		public abstract T[] Rent(int minimumLength);

		// Token: 0x0600806F RID: 32879
		public abstract void Return(T[] array, bool clearArray = false);

		// Token: 0x04003D14 RID: 15636
		private static ArrayPool<T> s_sharedInstance;
	}
}
