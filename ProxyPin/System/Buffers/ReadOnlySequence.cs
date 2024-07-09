using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x02000CE9 RID: 3305
	[System.Memory.IsReadOnly]
	[DebuggerTypeProxy(typeof(ReadOnlySequenceDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	[ComVisible(true)]
	public struct ReadOnlySequence<T>
	{
		// Token: 0x17001C9C RID: 7324
		// (get) Token: 0x06008599 RID: 34201 RVA: 0x002720B4 File Offset: 0x002720B4
		public long Length
		{
			get
			{
				return this.GetLength();
			}
		}

		// Token: 0x17001C9D RID: 7325
		// (get) Token: 0x0600859A RID: 34202 RVA: 0x002720BC File Offset: 0x002720BC
		public bool IsEmpty
		{
			get
			{
				return this.Length == 0L;
			}
		}

		// Token: 0x17001C9E RID: 7326
		// (get) Token: 0x0600859B RID: 34203 RVA: 0x002720C8 File Offset: 0x002720C8
		public bool IsSingleSegment
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this._sequenceStart.GetObject() == this._sequenceEnd.GetObject();
			}
		}

		// Token: 0x17001C9F RID: 7327
		// (get) Token: 0x0600859C RID: 34204 RVA: 0x002720E4 File Offset: 0x002720E4
		public ReadOnlyMemory<T> First
		{
			get
			{
				return this.GetFirstBuffer();
			}
		}

		// Token: 0x17001CA0 RID: 7328
		// (get) Token: 0x0600859D RID: 34205 RVA: 0x002720EC File Offset: 0x002720EC
		public SequencePosition Start
		{
			get
			{
				return this._sequenceStart;
			}
		}

		// Token: 0x17001CA1 RID: 7329
		// (get) Token: 0x0600859E RID: 34206 RVA: 0x002720F4 File Offset: 0x002720F4
		public SequencePosition End
		{
			get
			{
				return this._sequenceEnd;
			}
		}

		// Token: 0x0600859F RID: 34207 RVA: 0x002720FC File Offset: 0x002720FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ReadOnlySequence(object startSegment, int startIndexAndFlags, object endSegment, int endIndexAndFlags)
		{
			this._sequenceStart = new SequencePosition(startSegment, startIndexAndFlags);
			this._sequenceEnd = new SequencePosition(endSegment, endIndexAndFlags);
		}

		// Token: 0x060085A0 RID: 34208 RVA: 0x0027211C File Offset: 0x0027211C
		public ReadOnlySequence(ReadOnlySequenceSegment<T> startSegment, int startIndex, ReadOnlySequenceSegment<T> endSegment, int endIndex)
		{
			if (startSegment == null || endSegment == null || (startSegment != endSegment && startSegment.RunningIndex > endSegment.RunningIndex) || startSegment.Memory.Length < startIndex || endSegment.Memory.Length < endIndex || (startSegment == endSegment && endIndex < startIndex))
			{
				ThrowHelper.ThrowArgumentValidationException<T>(startSegment, startIndex, endSegment);
			}
			this._sequenceStart = new SequencePosition(startSegment, ReadOnlySequence.SegmentToSequenceStart(startIndex));
			this._sequenceEnd = new SequencePosition(endSegment, ReadOnlySequence.SegmentToSequenceEnd(endIndex));
		}

		// Token: 0x060085A1 RID: 34209 RVA: 0x002721B8 File Offset: 0x002721B8
		public ReadOnlySequence(T[] array)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			this._sequenceStart = new SequencePosition(array, ReadOnlySequence.ArrayToSequenceStart(0));
			this._sequenceEnd = new SequencePosition(array, ReadOnlySequence.ArrayToSequenceEnd(array.Length));
		}

		// Token: 0x060085A2 RID: 34210 RVA: 0x002721F0 File Offset: 0x002721F0
		public ReadOnlySequence(T[] array, int start, int length)
		{
			if (array == null || start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentValidationException(array, start);
			}
			this._sequenceStart = new SequencePosition(array, ReadOnlySequence.ArrayToSequenceStart(start));
			this._sequenceEnd = new SequencePosition(array, ReadOnlySequence.ArrayToSequenceEnd(start + length));
		}

		// Token: 0x060085A3 RID: 34211 RVA: 0x00272248 File Offset: 0x00272248
		public ReadOnlySequence(ReadOnlyMemory<T> memory)
		{
			MemoryManager<T> @object;
			int startIndex;
			int num;
			if (MemoryMarshal.TryGetMemoryManager<T, MemoryManager<T>>(memory, out @object, out startIndex, out num))
			{
				this._sequenceStart = new SequencePosition(@object, ReadOnlySequence.MemoryManagerToSequenceStart(startIndex));
				this._sequenceEnd = new SequencePosition(@object, ReadOnlySequence.MemoryManagerToSequenceEnd(num));
				return;
			}
			ArraySegment<T> arraySegment;
			if (MemoryMarshal.TryGetArray<T>(memory, out arraySegment))
			{
				T[] array = arraySegment.Array;
				int offset = arraySegment.Offset;
				this._sequenceStart = new SequencePosition(array, ReadOnlySequence.ArrayToSequenceStart(offset));
				this._sequenceEnd = new SequencePosition(array, ReadOnlySequence.ArrayToSequenceEnd(offset + arraySegment.Count));
				return;
			}
			if (typeof(T) == typeof(char))
			{
				string object2;
				int num2;
				if (!MemoryMarshal.TryGetString((ReadOnlyMemory<char>)memory, out object2, out num2, out num))
				{
					ThrowHelper.ThrowInvalidOperationException();
				}
				this._sequenceStart = new SequencePosition(object2, ReadOnlySequence.StringToSequenceStart(num2));
				this._sequenceEnd = new SequencePosition(object2, ReadOnlySequence.StringToSequenceEnd(num2 + num));
				return;
			}
			ThrowHelper.ThrowInvalidOperationException();
			this._sequenceStart = default(SequencePosition);
			this._sequenceEnd = default(SequencePosition);
		}

		// Token: 0x060085A4 RID: 34212 RVA: 0x00272368 File Offset: 0x00272368
		public ReadOnlySequence<T> Slice(long start, long length)
		{
			if (start < 0L || length < 0L)
			{
				ThrowHelper.ThrowStartOrEndArgumentValidationException(start);
			}
			int num = ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			int index = ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
			object @object = this._sequenceStart.GetObject();
			object object2 = this._sequenceEnd.GetObject();
			SequencePosition sequencePosition;
			SequencePosition endPosition;
			if (@object != object2)
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)@object;
				int num2 = readOnlySequenceSegment.Memory.Length - num;
				if ((long)num2 > start)
				{
					num += (int)start;
					sequencePosition = new SequencePosition(@object, num);
					endPosition = ReadOnlySequence<T>.GetEndPosition(readOnlySequenceSegment, @object, num, object2, index, length);
				}
				else
				{
					if (num2 < 0)
					{
						ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					}
					sequencePosition = ReadOnlySequence<T>.SeekMultiSegment(readOnlySequenceSegment.Next, object2, index, start - (long)num2, ExceptionArgument.start);
					int index2 = ReadOnlySequence<T>.GetIndex(ref sequencePosition);
					object object3 = sequencePosition.GetObject();
					if (object3 != object2)
					{
						endPosition = ReadOnlySequence<T>.GetEndPosition((ReadOnlySequenceSegment<T>)object3, object3, index2, object2, index, length);
					}
					else
					{
						if ((long)(index - index2) < length)
						{
							ThrowHelper.ThrowStartOrEndArgumentValidationException(0L);
						}
						endPosition = new SequencePosition(object3, index2 + (int)length);
					}
				}
			}
			else
			{
				if ((long)(index - num) < start)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(-1L);
				}
				num += (int)start;
				sequencePosition = new SequencePosition(@object, num);
				if ((long)(index - num) < length)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(0L);
				}
				endPosition = new SequencePosition(@object, num + (int)length);
			}
			return this.SliceImpl(ref sequencePosition, ref endPosition);
		}

		// Token: 0x060085A5 RID: 34213 RVA: 0x002724D0 File Offset: 0x002724D0
		public ReadOnlySequence<T> Slice(long start, SequencePosition end)
		{
			if (start < 0L)
			{
				ThrowHelper.ThrowStartOrEndArgumentValidationException(start);
			}
			uint index = (uint)ReadOnlySequence<T>.GetIndex(ref end);
			object @object = end.GetObject();
			uint index2 = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			object object2 = this._sequenceStart.GetObject();
			uint index3 = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
			object object3 = this._sequenceEnd.GetObject();
			if (object2 == object3)
			{
				if (!ReadOnlySequence<T>.InRange(index, index2, index3))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
				if ((ulong)(index - index2) < (ulong)start)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(-1L);
				}
			}
			else
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)object2;
				ulong num = (ulong)(readOnlySequenceSegment.RunningIndex + (long)((ulong)index2));
				ulong num2 = (ulong)(((ReadOnlySequenceSegment<T>)@object).RunningIndex + (long)((ulong)index));
				if (!ReadOnlySequence<T>.InRange(num2, num, (ulong)(((ReadOnlySequenceSegment<T>)object3).RunningIndex + (long)((ulong)index3))))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
				if (num + (ulong)start > num2)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				int num3 = readOnlySequenceSegment.Memory.Length - (int)index2;
				if ((long)num3 <= start)
				{
					if (num3 < 0)
					{
						ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					}
					SequencePosition sequencePosition = ReadOnlySequence<T>.SeekMultiSegment(readOnlySequenceSegment.Next, @object, (int)index, start - (long)num3, ExceptionArgument.start);
					return this.SliceImpl(ref sequencePosition, ref end);
				}
			}
			SequencePosition sequencePosition2 = new SequencePosition(object2, (int)(index2 + (uint)((int)start)));
			return this.SliceImpl(ref sequencePosition2, ref end);
		}

		// Token: 0x060085A6 RID: 34214 RVA: 0x0027261C File Offset: 0x0027261C
		public ReadOnlySequence<T> Slice(SequencePosition start, long length)
		{
			uint index = (uint)ReadOnlySequence<T>.GetIndex(ref start);
			object @object = start.GetObject();
			uint index2 = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			object object2 = this._sequenceStart.GetObject();
			uint index3 = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
			object object3 = this._sequenceEnd.GetObject();
			if (object2 == object3)
			{
				if (!ReadOnlySequence<T>.InRange(index, index2, index3))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
				if (length < 0L)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(0L);
				}
				if ((ulong)(index3 - index) < (ulong)length)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(0L);
				}
			}
			else
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)@object;
				ulong num = (ulong)(readOnlySequenceSegment.RunningIndex + (long)((ulong)index));
				ulong start2 = (ulong)(((ReadOnlySequenceSegment<T>)object2).RunningIndex + (long)((ulong)index2));
				ulong num2 = (ulong)(((ReadOnlySequenceSegment<T>)object3).RunningIndex + (long)((ulong)index3));
				if (!ReadOnlySequence<T>.InRange(num, start2, num2))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
				if (length < 0L)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(0L);
				}
				if (num + (ulong)length > num2)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
				}
				int num3 = readOnlySequenceSegment.Memory.Length - (int)index;
				if ((long)num3 < length)
				{
					if (num3 < 0)
					{
						ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					}
					SequencePosition sequencePosition = ReadOnlySequence<T>.SeekMultiSegment(readOnlySequenceSegment.Next, object3, (int)index3, length - (long)num3, ExceptionArgument.length);
					return this.SliceImpl(ref start, ref sequencePosition);
				}
			}
			SequencePosition sequencePosition2 = new SequencePosition(@object, (int)(index + (uint)((int)length)));
			return this.SliceImpl(ref start, ref sequencePosition2);
		}

		// Token: 0x060085A7 RID: 34215 RVA: 0x00272780 File Offset: 0x00272780
		public ReadOnlySequence<T> Slice(int start, int length)
		{
			return this.Slice((long)start, (long)length);
		}

		// Token: 0x060085A8 RID: 34216 RVA: 0x0027278C File Offset: 0x0027278C
		public ReadOnlySequence<T> Slice(int start, SequencePosition end)
		{
			return this.Slice((long)start, end);
		}

		// Token: 0x060085A9 RID: 34217 RVA: 0x00272798 File Offset: 0x00272798
		public ReadOnlySequence<T> Slice(SequencePosition start, int length)
		{
			return this.Slice(start, (long)length);
		}

		// Token: 0x060085AA RID: 34218 RVA: 0x002727A4 File Offset: 0x002727A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySequence<T> Slice(SequencePosition start, SequencePosition end)
		{
			this.BoundsCheck((uint)ReadOnlySequence<T>.GetIndex(ref start), start.GetObject(), (uint)ReadOnlySequence<T>.GetIndex(ref end), end.GetObject());
			return this.SliceImpl(ref start, ref end);
		}

		// Token: 0x060085AB RID: 34219 RVA: 0x002727E4 File Offset: 0x002727E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySequence<T> Slice(SequencePosition start)
		{
			this.BoundsCheck(ref start);
			return this.SliceImpl(ref start, ref this._sequenceEnd);
		}

		// Token: 0x060085AC RID: 34220 RVA: 0x002727FC File Offset: 0x002727FC
		public ReadOnlySequence<T> Slice(long start)
		{
			if (start < 0L)
			{
				ThrowHelper.ThrowStartOrEndArgumentValidationException(start);
			}
			if (start == 0L)
			{
				return this;
			}
			SequencePosition sequencePosition = this.Seek(ref this._sequenceStart, ref this._sequenceEnd, start, ExceptionArgument.start);
			return this.SliceImpl(ref sequencePosition, ref this._sequenceEnd);
		}

		// Token: 0x060085AD RID: 34221 RVA: 0x0027284C File Offset: 0x0027284C
		public unsafe override string ToString()
		{
			if (typeof(T) == typeof(char))
			{
				ReadOnlySequence<T> readOnlySequence = this;
				ReadOnlySequence<char> sequence = *Unsafe.As<ReadOnlySequence<T>, ReadOnlySequence<char>>(ref readOnlySequence);
				string text;
				int startIndex;
				int length;
				if (SequenceMarshal.TryGetString(sequence, out text, out startIndex, out length))
				{
					return text.Substring(startIndex, length);
				}
				if (this.Length < 2147483647L)
				{
					return new string(ref sequence.ToArray<char>());
				}
			}
			return string.Format("System.Buffers.ReadOnlySequence<{0}>[{1}]", typeof(T).Name, this.Length);
		}

		// Token: 0x060085AE RID: 34222 RVA: 0x002728EC File Offset: 0x002728EC
		public ReadOnlySequence<T>.Enumerator GetEnumerator()
		{
			return new ReadOnlySequence<T>.Enumerator(ref this);
		}

		// Token: 0x060085AF RID: 34223 RVA: 0x002728F4 File Offset: 0x002728F4
		public SequencePosition GetPosition(long offset)
		{
			return this.GetPosition(offset, this._sequenceStart);
		}

		// Token: 0x060085B0 RID: 34224 RVA: 0x00272904 File Offset: 0x00272904
		public SequencePosition GetPosition(long offset, SequencePosition origin)
		{
			if (offset < 0L)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException_OffsetOutOfRange();
			}
			return this.Seek(ref origin, ref this._sequenceEnd, offset, ExceptionArgument.offset);
		}

		// Token: 0x060085B1 RID: 34225 RVA: 0x00272924 File Offset: 0x00272924
		public bool TryGet(ref SequencePosition position, out ReadOnlyMemory<T> memory, bool advance = true)
		{
			SequencePosition sequencePosition;
			bool result = this.TryGetBuffer(ref position, out memory, out sequencePosition);
			if (advance)
			{
				position = sequencePosition;
			}
			return result;
		}

		// Token: 0x060085B2 RID: 34226 RVA: 0x00272950 File Offset: 0x00272950
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool TryGetBuffer([System.Memory.IsReadOnly] [In] ref SequencePosition position, out ReadOnlyMemory<T> memory, out SequencePosition next)
		{
			object @object = position.GetObject();
			next = default(SequencePosition);
			if (@object == null)
			{
				memory = default(ReadOnlyMemory<T>);
				return false;
			}
			ReadOnlySequence<T>.SequenceType sequenceType = this.GetSequenceType();
			object object2 = this._sequenceEnd.GetObject();
			int index = ReadOnlySequence<T>.GetIndex(ref position);
			int index2 = ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
			if (sequenceType == ReadOnlySequence<T>.SequenceType.MultiSegment)
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)@object;
				if (readOnlySequenceSegment != object2)
				{
					ReadOnlySequenceSegment<T> next2 = readOnlySequenceSegment.Next;
					if (next2 == null)
					{
						ThrowHelper.ThrowInvalidOperationException_EndPositionNotReached();
					}
					next = new SequencePosition(next2, 0);
					memory = readOnlySequenceSegment.Memory.Slice(index);
				}
				else
				{
					memory = readOnlySequenceSegment.Memory.Slice(index, index2 - index);
				}
			}
			else
			{
				if (@object != object2)
				{
					ThrowHelper.ThrowInvalidOperationException_EndPositionNotReached();
				}
				if (sequenceType == ReadOnlySequence<T>.SequenceType.Array)
				{
					memory = new ReadOnlyMemory<T>((T[])@object, index, index2 - index);
				}
				else if (typeof(T) == typeof(char) && sequenceType == ReadOnlySequence<T>.SequenceType.String)
				{
					memory = (ReadOnlyMemory<T>)((string)@object).AsMemory(index, index2 - index);
				}
				else
				{
					memory = ((MemoryManager<T>)@object).Memory.Slice(index, index2 - index);
				}
			}
			return true;
		}

		// Token: 0x060085B3 RID: 34227 RVA: 0x00272AB8 File Offset: 0x00272AB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ReadOnlyMemory<T> GetFirstBuffer()
		{
			object @object = this._sequenceStart.GetObject();
			if (@object == null)
			{
				return default(ReadOnlyMemory<T>);
			}
			int num = this._sequenceStart.GetInteger();
			int integer = this._sequenceEnd.GetInteger();
			bool flag = @object != this._sequenceEnd.GetObject();
			if (num >= 0)
			{
				if (integer < 0)
				{
					if (flag)
					{
						ThrowHelper.ThrowInvalidOperationException_EndPositionNotReached();
					}
					return new ReadOnlyMemory<T>((T[])@object, num, (integer & int.MaxValue) - num);
				}
				ReadOnlyMemory<T> memory = ((ReadOnlySequenceSegment<T>)@object).Memory;
				if (flag)
				{
					return memory.Slice(num);
				}
				return memory.Slice(num, integer - num);
			}
			else
			{
				if (flag)
				{
					ThrowHelper.ThrowInvalidOperationException_EndPositionNotReached();
				}
				if (typeof(T) == typeof(char) && integer < 0)
				{
					return (ReadOnlyMemory<T>)((string)@object).AsMemory(num & int.MaxValue, integer - num);
				}
				num &= int.MaxValue;
				return ((MemoryManager<T>)@object).Memory.Slice(num, integer - num);
			}
		}

		// Token: 0x060085B4 RID: 34228 RVA: 0x00272BE0 File Offset: 0x00272BE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private SequencePosition Seek([System.Memory.IsReadOnly] [In] ref SequencePosition start, [System.Memory.IsReadOnly] [In] ref SequencePosition end, long offset, ExceptionArgument argument)
		{
			int index = ReadOnlySequence<T>.GetIndex(ref start);
			int index2 = ReadOnlySequence<T>.GetIndex(ref end);
			object @object = start.GetObject();
			object object2 = end.GetObject();
			if (@object != object2)
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)@object;
				int num = readOnlySequenceSegment.Memory.Length - index;
				if ((long)num <= offset)
				{
					if (num < 0)
					{
						ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					}
					return ReadOnlySequence<T>.SeekMultiSegment(readOnlySequenceSegment.Next, object2, index2, offset - (long)num, argument);
				}
			}
			else if ((long)(index2 - index) < offset)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(argument);
			}
			return new SequencePosition(@object, index + (int)offset);
		}

		// Token: 0x060085B5 RID: 34229 RVA: 0x00272C78 File Offset: 0x00272C78
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static SequencePosition SeekMultiSegment(ReadOnlySequenceSegment<T> currentSegment, object endObject, int endIndex, long offset, ExceptionArgument argument)
		{
			while (currentSegment != null && currentSegment != endObject)
			{
				int length = currentSegment.Memory.Length;
				if ((long)length > offset)
				{
					IL_49:
					return new SequencePosition(currentSegment, (int)offset);
				}
				offset -= (long)length;
				currentSegment = currentSegment.Next;
			}
			if (currentSegment == null || (long)endIndex < offset)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(argument);
				goto IL_49;
			}
			goto IL_49;
		}

		// Token: 0x060085B6 RID: 34230 RVA: 0x00272CDC File Offset: 0x00272CDC
		private void BoundsCheck([System.Memory.IsReadOnly] [In] ref SequencePosition position)
		{
			uint index = (uint)ReadOnlySequence<T>.GetIndex(ref position);
			uint index2 = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			uint index3 = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
			object @object = this._sequenceStart.GetObject();
			object object2 = this._sequenceEnd.GetObject();
			if (@object == object2)
			{
				if (!ReadOnlySequence<T>.InRange(index, index2, index3))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					return;
				}
			}
			else
			{
				ulong start = (ulong)(((ReadOnlySequenceSegment<T>)@object).RunningIndex + (long)((ulong)index2));
				if (!ReadOnlySequence<T>.InRange((ulong)(((ReadOnlySequenceSegment<T>)position.GetObject()).RunningIndex + (long)((ulong)index)), start, (ulong)(((ReadOnlySequenceSegment<T>)object2).RunningIndex + (long)((ulong)index3))))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
			}
		}

		// Token: 0x060085B7 RID: 34231 RVA: 0x00272D84 File Offset: 0x00272D84
		private void BoundsCheck(uint sliceStartIndex, object sliceStartObject, uint sliceEndIndex, object sliceEndObject)
		{
			uint index = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			uint index2 = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
			object @object = this._sequenceStart.GetObject();
			object object2 = this._sequenceEnd.GetObject();
			if (@object == object2)
			{
				if (sliceStartObject != sliceEndObject || sliceStartObject != @object || sliceStartIndex > sliceEndIndex || sliceStartIndex < index || sliceEndIndex > index2)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					return;
				}
			}
			else
			{
				ulong num = (ulong)(((ReadOnlySequenceSegment<T>)sliceStartObject).RunningIndex + (long)((ulong)sliceStartIndex));
				ulong num2 = (ulong)(((ReadOnlySequenceSegment<T>)sliceEndObject).RunningIndex + (long)((ulong)sliceEndIndex));
				if (num > num2)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
				if (num < (ulong)(((ReadOnlySequenceSegment<T>)@object).RunningIndex + (long)((ulong)index)) || num2 > (ulong)(((ReadOnlySequenceSegment<T>)object2).RunningIndex + (long)((ulong)index2)))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
			}
		}

		// Token: 0x060085B8 RID: 34232 RVA: 0x00272E54 File Offset: 0x00272E54
		private static SequencePosition GetEndPosition(ReadOnlySequenceSegment<T> startSegment, object startObject, int startIndex, object endObject, int endIndex, long length)
		{
			int num = startSegment.Memory.Length - startIndex;
			if ((long)num > length)
			{
				return new SequencePosition(startObject, startIndex + (int)length);
			}
			if (num < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
			}
			return ReadOnlySequence<T>.SeekMultiSegment(startSegment.Next, endObject, endIndex, length - (long)num, ExceptionArgument.length);
		}

		// Token: 0x060085B9 RID: 34233 RVA: 0x00272EAC File Offset: 0x00272EAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ReadOnlySequence<T>.SequenceType GetSequenceType()
		{
			return (ReadOnlySequence<T>.SequenceType)(-(ReadOnlySequence<T>.SequenceType)(2 * (this._sequenceStart.GetInteger() >> 31) + (this._sequenceEnd.GetInteger() >> 31)));
		}

		// Token: 0x060085BA RID: 34234 RVA: 0x00272ED0 File Offset: 0x00272ED0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int GetIndex([System.Memory.IsReadOnly] [In] ref SequencePosition position)
		{
			return position.GetInteger() & int.MaxValue;
		}

		// Token: 0x060085BB RID: 34235 RVA: 0x00272EE0 File Offset: 0x00272EE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ReadOnlySequence<T> SliceImpl([System.Memory.IsReadOnly] [In] ref SequencePosition start, [System.Memory.IsReadOnly] [In] ref SequencePosition end)
		{
			return new ReadOnlySequence<T>(start.GetObject(), ReadOnlySequence<T>.GetIndex(ref start) | (this._sequenceStart.GetInteger() & int.MinValue), end.GetObject(), ReadOnlySequence<T>.GetIndex(ref end) | (this._sequenceEnd.GetInteger() & int.MinValue));
		}

		// Token: 0x060085BC RID: 34236 RVA: 0x00272F34 File Offset: 0x00272F34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private long GetLength()
		{
			int index = ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			int index2 = ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
			object @object = this._sequenceStart.GetObject();
			object object2 = this._sequenceEnd.GetObject();
			if (@object != object2)
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)@object;
				ReadOnlySequenceSegment<T> readOnlySequenceSegment2 = (ReadOnlySequenceSegment<T>)object2;
				return readOnlySequenceSegment2.RunningIndex + (long)index2 - (readOnlySequenceSegment.RunningIndex + (long)index);
			}
			return (long)(index2 - index);
		}

		// Token: 0x060085BD RID: 34237 RVA: 0x00272FA8 File Offset: 0x00272FA8
		internal bool TryGetReadOnlySequenceSegment(out ReadOnlySequenceSegment<T> startSegment, out int startIndex, out ReadOnlySequenceSegment<T> endSegment, out int endIndex)
		{
			object @object = this._sequenceStart.GetObject();
			if (@object == null || this.GetSequenceType() != ReadOnlySequence<T>.SequenceType.MultiSegment)
			{
				startSegment = null;
				startIndex = 0;
				endSegment = null;
				endIndex = 0;
				return false;
			}
			startSegment = (ReadOnlySequenceSegment<T>)@object;
			startIndex = ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			endSegment = (ReadOnlySequenceSegment<T>)this._sequenceEnd.GetObject();
			endIndex = ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
			return true;
		}

		// Token: 0x060085BE RID: 34238 RVA: 0x0027301C File Offset: 0x0027301C
		internal bool TryGetArray(out ArraySegment<T> segment)
		{
			if (this.GetSequenceType() != ReadOnlySequence<T>.SequenceType.Array)
			{
				segment = default(ArraySegment<T>);
				return false;
			}
			int index = ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			segment = new ArraySegment<T>((T[])this._sequenceStart.GetObject(), index, ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd) - index);
			return true;
		}

		// Token: 0x060085BF RID: 34239 RVA: 0x00273078 File Offset: 0x00273078
		internal bool TryGetString(out string text, out int start, out int length)
		{
			if (typeof(T) != typeof(char) || this.GetSequenceType() != ReadOnlySequence<T>.SequenceType.String)
			{
				start = 0;
				length = 0;
				text = null;
				return false;
			}
			start = ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			length = ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd) - start;
			text = (string)this._sequenceStart.GetObject();
			return true;
		}

		// Token: 0x060085C0 RID: 34240 RVA: 0x002730F0 File Offset: 0x002730F0
		private static bool InRange(uint value, uint start, uint end)
		{
			return value - start <= end - start;
		}

		// Token: 0x060085C1 RID: 34241 RVA: 0x00273100 File Offset: 0x00273100
		private static bool InRange(ulong value, ulong start, ulong end)
		{
			return value - start <= end - start;
		}

		// Token: 0x04003DCF RID: 15823
		private readonly SequencePosition _sequenceStart;

		// Token: 0x04003DD0 RID: 15824
		private readonly SequencePosition _sequenceEnd;

		// Token: 0x04003DD1 RID: 15825
		public static readonly ReadOnlySequence<T> Empty = new ReadOnlySequence<T>(SpanHelpers.PerTypeValues<T>.EmptyArray);

		// Token: 0x020011C7 RID: 4551
		public struct Enumerator
		{
			// Token: 0x06009675 RID: 38517 RVA: 0x002CC148 File Offset: 0x002CC148
			public Enumerator([System.Memory.IsReadOnly] [In] ref ReadOnlySequence<T> sequence)
			{
				this._currentMemory = default(ReadOnlyMemory<T>);
				this._next = sequence.Start;
				this._sequence = sequence;
			}

			// Token: 0x17001F4A RID: 8010
			// (get) Token: 0x06009676 RID: 38518 RVA: 0x002CC170 File Offset: 0x002CC170
			public ReadOnlyMemory<T> Current
			{
				get
				{
					return this._currentMemory;
				}
			}

			// Token: 0x06009677 RID: 38519 RVA: 0x002CC178 File Offset: 0x002CC178
			public bool MoveNext()
			{
				return this._next.GetObject() != null && this._sequence.TryGet(ref this._next, out this._currentMemory, true);
			}

			// Token: 0x04004C6D RID: 19565
			private readonly ReadOnlySequence<T> _sequence;

			// Token: 0x04004C6E RID: 19566
			private SequencePosition _next;

			// Token: 0x04004C6F RID: 19567
			private ReadOnlyMemory<T> _currentMemory;
		}

		// Token: 0x020011C8 RID: 4552
		private enum SequenceType
		{
			// Token: 0x04004C71 RID: 19569
			MultiSegment,
			// Token: 0x04004C72 RID: 19570
			Array,
			// Token: 0x04004C73 RID: 19571
			MemoryManager,
			// Token: 0x04004C74 RID: 19572
			String,
			// Token: 0x04004C75 RID: 19573
			Empty
		}
	}
}
