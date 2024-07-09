using System;
using System.Buffers;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000CD4 RID: 3284
	[System.Memory.IsReadOnly]
	[DebuggerTypeProxy(typeof(MemoryDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	[ComVisible(true)]
	public struct Memory<T>
	{
		// Token: 0x0600845A RID: 33882 RVA: 0x00269D88 File Offset: 0x00269D88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Memory(T[] array)
		{
			if (array == null)
			{
				this = default(Memory<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			this._object = array;
			this._index = 0;
			this._length = array.Length;
		}

		// Token: 0x0600845B RID: 33883 RVA: 0x00269DF0 File Offset: 0x00269DF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Memory(T[] array, int start)
		{
			if (array == null)
			{
				if (start != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this = default(Memory<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._object = array;
			this._index = start;
			this._length = array.Length - start;
		}

		// Token: 0x0600845C RID: 33884 RVA: 0x00269E74 File Offset: 0x00269E74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Memory(T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this = default(Memory<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._object = array;
			this._index = start;
			this._length = length;
		}

		// Token: 0x0600845D RID: 33885 RVA: 0x00269F04 File Offset: 0x00269F04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Memory(MemoryManager<T> manager, int length)
		{
			if (length < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._object = manager;
			this._index = int.MinValue;
			this._length = length;
		}

		// Token: 0x0600845E RID: 33886 RVA: 0x00269F2C File Offset: 0x00269F2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Memory(MemoryManager<T> manager, int start, int length)
		{
			if (length < 0 || start < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._object = manager;
			this._index = (start | int.MinValue);
			this._length = length;
		}

		// Token: 0x0600845F RID: 33887 RVA: 0x00269F5C File Offset: 0x00269F5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Memory(object obj, int start, int length)
		{
			this._object = obj;
			this._index = start;
			this._length = length;
		}

		// Token: 0x06008460 RID: 33888 RVA: 0x00269F74 File Offset: 0x00269F74
		public static implicit operator Memory<T>(T[] array)
		{
			return new Memory<T>(array);
		}

		// Token: 0x06008461 RID: 33889 RVA: 0x00269F7C File Offset: 0x00269F7C
		public static implicit operator Memory<T>(ArraySegment<T> segment)
		{
			return new Memory<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x06008462 RID: 33890 RVA: 0x00269FA8 File Offset: 0x00269FA8
		public unsafe static implicit operator ReadOnlyMemory<T>(Memory<T> memory)
		{
			return *Unsafe.As<Memory<T>, ReadOnlyMemory<T>>(ref memory);
		}

		// Token: 0x17001C71 RID: 7281
		// (get) Token: 0x06008463 RID: 33891 RVA: 0x00269FB8 File Offset: 0x00269FB8
		public static Memory<T> Empty
		{
			get
			{
				return default(Memory<T>);
			}
		}

		// Token: 0x17001C72 RID: 7282
		// (get) Token: 0x06008464 RID: 33892 RVA: 0x00269FD4 File Offset: 0x00269FD4
		public int Length
		{
			get
			{
				return this._length & int.MaxValue;
			}
		}

		// Token: 0x17001C73 RID: 7283
		// (get) Token: 0x06008465 RID: 33893 RVA: 0x00269FE4 File Offset: 0x00269FE4
		public bool IsEmpty
		{
			get
			{
				return (this._length & int.MaxValue) == 0;
			}
		}

		// Token: 0x06008466 RID: 33894 RVA: 0x00269FF8 File Offset: 0x00269FF8
		public override string ToString()
		{
			if (!(typeof(T) == typeof(char)))
			{
				return string.Format("System.Memory<{0}>[{1}]", typeof(T).Name, this._length & int.MaxValue);
			}
			string text;
			if ((text = (this._object as string)) == null)
			{
				return this.Span.ToString();
			}
			return text.Substring(this._index, this._length & int.MaxValue);
		}

		// Token: 0x06008467 RID: 33895 RVA: 0x0026A094 File Offset: 0x0026A094
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Memory<T> Slice(int start)
		{
			int length = this._length;
			int num = length & int.MaxValue;
			if (start > num)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new Memory<T>(this._object, this._index + start, length - start);
		}

		// Token: 0x06008468 RID: 33896 RVA: 0x0026A0D8 File Offset: 0x0026A0D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Memory<T> Slice(int start, int length)
		{
			int length2 = this._length;
			int num = length2 & int.MaxValue;
			if (start > num || length > num - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Memory<T>(this._object, this._index + start, length | (length2 & int.MinValue));
		}

		// Token: 0x17001C74 RID: 7284
		// (get) Token: 0x06008469 RID: 33897 RVA: 0x0026A12C File Offset: 0x0026A12C
		public Span<T> Span
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (this._index < 0)
				{
					return ((MemoryManager<T>)this._object).GetSpan().Slice(this._index & int.MaxValue, this._length);
				}
				string text;
				if (typeof(T) == typeof(char) && (text = (this._object as string)) != null)
				{
					return new Span<T>(Unsafe.As<Pinnable<T>>(text), MemoryExtensions.StringAdjustment, text.Length).Slice(this._index, this._length);
				}
				if (this._object != null)
				{
					return new Span<T>((T[])this._object, this._index, this._length & int.MaxValue);
				}
				return default(Span<T>);
			}
		}

		// Token: 0x0600846A RID: 33898 RVA: 0x0026A208 File Offset: 0x0026A208
		public void CopyTo(Memory<T> destination)
		{
			this.Span.CopyTo(destination.Span);
		}

		// Token: 0x0600846B RID: 33899 RVA: 0x0026A230 File Offset: 0x0026A230
		public bool TryCopyTo(Memory<T> destination)
		{
			return this.Span.TryCopyTo(destination.Span);
		}

		// Token: 0x0600846C RID: 33900 RVA: 0x0026A258 File Offset: 0x0026A258
		public unsafe MemoryHandle Pin()
		{
			if (this._index < 0)
			{
				return ((MemoryManager<T>)this._object).Pin(this._index & int.MaxValue);
			}
			string value;
			if (typeof(T) == typeof(char) && (value = (this._object as string)) != null)
			{
				GCHandle handle = GCHandle.Alloc(value, GCHandleType.Pinned);
				void* pointer = Unsafe.Add<T>((void*)handle.AddrOfPinnedObject(), this._index);
				return new MemoryHandle(pointer, handle, null);
			}
			T[] array;
			if ((array = (this._object as T[])) == null)
			{
				return default(MemoryHandle);
			}
			if (this._length < 0)
			{
				void* pointer2 = Unsafe.Add<T>(Unsafe.AsPointer<T>(MemoryMarshal.GetReference<T>(array)), this._index);
				return new MemoryHandle(pointer2, default(GCHandle), null);
			}
			GCHandle handle2 = GCHandle.Alloc(array, GCHandleType.Pinned);
			void* pointer3 = Unsafe.Add<T>((void*)handle2.AddrOfPinnedObject(), this._index);
			return new MemoryHandle(pointer3, handle2, null);
		}

		// Token: 0x0600846D RID: 33901 RVA: 0x0026A370 File Offset: 0x0026A370
		public T[] ToArray()
		{
			return this.Span.ToArray();
		}

		// Token: 0x0600846E RID: 33902 RVA: 0x0026A390 File Offset: 0x0026A390
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			if (obj is ReadOnlyMemory<T>)
			{
				return ((ReadOnlyMemory<T>)obj).Equals(this);
			}
			if (obj is Memory<T>)
			{
				Memory<T> other = (Memory<T>)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x0600846F RID: 33903 RVA: 0x0026A3E4 File Offset: 0x0026A3E4
		public bool Equals(Memory<T> other)
		{
			return this._object == other._object && this._index == other._index && this._length == other._length;
		}

		// Token: 0x06008470 RID: 33904 RVA: 0x0026A418 File Offset: 0x0026A418
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			if (this._object == null)
			{
				return 0;
			}
			return Memory<T>.CombineHashCodes(this._object.GetHashCode(), this._index.GetHashCode(), this._length.GetHashCode());
		}

		// Token: 0x06008471 RID: 33905 RVA: 0x0026A464 File Offset: 0x0026A464
		private static int CombineHashCodes(int left, int right)
		{
			return (left << 5) + left ^ right;
		}

		// Token: 0x06008472 RID: 33906 RVA: 0x0026A470 File Offset: 0x0026A470
		private static int CombineHashCodes(int h1, int h2, int h3)
		{
			return Memory<T>.CombineHashCodes(Memory<T>.CombineHashCodes(h1, h2), h3);
		}

		// Token: 0x04003DA9 RID: 15785
		private readonly object _object;

		// Token: 0x04003DAA RID: 15786
		private readonly int _index;

		// Token: 0x04003DAB RID: 15787
		private readonly int _length;

		// Token: 0x04003DAC RID: 15788
		private const int RemoveFlagsBitMask = 2147483647;
	}
}
