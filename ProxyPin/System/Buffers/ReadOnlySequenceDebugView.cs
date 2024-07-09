using System;
using System.Diagnostics;

namespace System.Buffers
{
	// Token: 0x02000CEB RID: 3307
	internal sealed class ReadOnlySequenceDebugView<T>
	{
		// Token: 0x060085CB RID: 34251 RVA: 0x00273174 File Offset: 0x00273174
		public ReadOnlySequenceDebugView(ReadOnlySequence<T> sequence)
		{
			this._array = ref sequence.ToArray<T>();
			int num = 0;
			foreach (ReadOnlyMemory<T> readOnlyMemory in sequence)
			{
				num++;
			}
			ReadOnlyMemory<T>[] array = new ReadOnlyMemory<T>[num];
			int num2 = 0;
			foreach (ReadOnlyMemory<T> readOnlyMemory2 in sequence)
			{
				array[num2] = readOnlyMemory2;
				num2++;
			}
			this._segments = new ReadOnlySequenceDebugView<T>.ReadOnlySequenceDebugViewSegments
			{
				Segments = array
			};
		}

		// Token: 0x17001CA2 RID: 7330
		// (get) Token: 0x060085CC RID: 34252 RVA: 0x00273210 File Offset: 0x00273210
		public ReadOnlySequenceDebugView<T>.ReadOnlySequenceDebugViewSegments BufferSegments
		{
			get
			{
				return this._segments;
			}
		}

		// Token: 0x17001CA3 RID: 7331
		// (get) Token: 0x060085CD RID: 34253 RVA: 0x00273218 File Offset: 0x00273218
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._array;
			}
		}

		// Token: 0x04003DDC RID: 15836
		private readonly T[] _array;

		// Token: 0x04003DDD RID: 15837
		private readonly ReadOnlySequenceDebugView<T>.ReadOnlySequenceDebugViewSegments _segments;

		// Token: 0x020011C9 RID: 4553
		[DebuggerDisplay("Count: {Segments.Length}", Name = "Segments")]
		public struct ReadOnlySequenceDebugViewSegments
		{
			// Token: 0x17001F4B RID: 8011
			// (get) Token: 0x06009678 RID: 38520 RVA: 0x002CC1A4 File Offset: 0x002CC1A4
			// (set) Token: 0x06009679 RID: 38521 RVA: 0x002CC1AC File Offset: 0x002CC1AC
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public ReadOnlyMemory<T>[] Segments { get; set; }
		}
	}
}
