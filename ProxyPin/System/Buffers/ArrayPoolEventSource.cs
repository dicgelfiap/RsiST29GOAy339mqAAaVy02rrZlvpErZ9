using System;
using System.Diagnostics.Tracing;

namespace System.Buffers
{
	// Token: 0x02000C87 RID: 3207
	[EventSource(Name = "System.Buffers.ArrayPoolEventSource")]
	internal sealed class ArrayPoolEventSource : EventSource
	{
		// Token: 0x06008072 RID: 32882 RVA: 0x00260428 File Offset: 0x00260428
		[Event(1, Level = EventLevel.Verbose)]
		internal unsafe void BufferRented(int bufferId, int bufferSize, int poolId, int bucketId)
		{
			EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData))];
			*ptr = new EventSource.EventData
			{
				Size = 4,
				DataPointer = (IntPtr)((void*)(&bufferId))
			};
			ptr[1] = new EventSource.EventData
			{
				Size = 4,
				DataPointer = (IntPtr)((void*)(&bufferSize))
			};
			ptr[2] = new EventSource.EventData
			{
				Size = 4,
				DataPointer = (IntPtr)((void*)(&poolId))
			};
			ptr[3] = new EventSource.EventData
			{
				Size = 4,
				DataPointer = (IntPtr)((void*)(&bucketId))
			};
			base.WriteEventCore(1, 4, ptr);
		}

		// Token: 0x06008073 RID: 32883 RVA: 0x00260504 File Offset: 0x00260504
		[Event(2, Level = EventLevel.Informational)]
		internal unsafe void BufferAllocated(int bufferId, int bufferSize, int poolId, int bucketId, ArrayPoolEventSource.BufferAllocatedReason reason)
		{
			EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)5) * (UIntPtr)sizeof(EventSource.EventData))];
			*ptr = new EventSource.EventData
			{
				Size = 4,
				DataPointer = (IntPtr)((void*)(&bufferId))
			};
			ptr[1] = new EventSource.EventData
			{
				Size = 4,
				DataPointer = (IntPtr)((void*)(&bufferSize))
			};
			ptr[2] = new EventSource.EventData
			{
				Size = 4,
				DataPointer = (IntPtr)((void*)(&poolId))
			};
			ptr[3] = new EventSource.EventData
			{
				Size = 4,
				DataPointer = (IntPtr)((void*)(&bucketId))
			};
			ptr[4] = new EventSource.EventData
			{
				Size = 4,
				DataPointer = (IntPtr)((void*)(&reason))
			};
			base.WriteEventCore(2, 5, ptr);
		}

		// Token: 0x06008074 RID: 32884 RVA: 0x00260610 File Offset: 0x00260610
		[Event(3, Level = EventLevel.Verbose)]
		internal void BufferReturned(int bufferId, int bufferSize, int poolId)
		{
			base.WriteEvent(3, bufferId, bufferSize, poolId);
		}

		// Token: 0x04003D15 RID: 15637
		internal static readonly ArrayPoolEventSource Log = new ArrayPoolEventSource();

		// Token: 0x0200118B RID: 4491
		internal enum BufferAllocatedReason
		{
			// Token: 0x04004B99 RID: 19353
			Pooled,
			// Token: 0x04004B9A RID: 19354
			OverMaximumSize,
			// Token: 0x04004B9B RID: 19355
			PoolExhausted
		}
	}
}
