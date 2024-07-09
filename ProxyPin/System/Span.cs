using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000CD9 RID: 3289
	[System.Memory.IsReadOnly]
	[DebuggerTypeProxy(typeof(SpanDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	[DebuggerTypeProxy(typeof(SpanDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	[ComVisible(true)]
	public ref struct Span<T>
	{
		// Token: 0x17001C80 RID: 7296
		// (get) Token: 0x060084F9 RID: 34041 RVA: 0x0026CC90 File Offset: 0x0026CC90
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x17001C81 RID: 7297
		// (get) Token: 0x060084FA RID: 34042 RVA: 0x0026CC98 File Offset: 0x0026CC98
		public bool IsEmpty
		{
			get
			{
				return this._length == 0;
			}
		}

		// Token: 0x060084FB RID: 34043 RVA: 0x0026CCA4 File Offset: 0x0026CCA4
		public static bool operator !=(Span<T> left, Span<T> right)
		{
			return !(left == right);
		}

		// Token: 0x060084FC RID: 34044 RVA: 0x0026CCB0 File Offset: 0x0026CCB0
		[Obsolete("Equals() on Span will always throw an exception. Use == instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			throw new NotSupportedException(System.Memory2565708.SR.NotSupported_CannotCallEqualsOnSpan);
		}

		// Token: 0x060084FD RID: 34045 RVA: 0x0026CCBC File Offset: 0x0026CCBC
		[Obsolete("GetHashCode() on Span will always throw an exception.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			throw new NotSupportedException(System.Memory2565708.SR.NotSupported_CannotCallGetHashCodeOnSpan);
		}

		// Token: 0x060084FE RID: 34046 RVA: 0x0026CCC8 File Offset: 0x0026CCC8
		public static implicit operator Span<T>(T[] array)
		{
			return new Span<T>(array);
		}

		// Token: 0x060084FF RID: 34047 RVA: 0x0026CCD0 File Offset: 0x0026CCD0
		public static implicit operator Span<T>(ArraySegment<T> segment)
		{
			return new Span<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x17001C82 RID: 7298
		// (get) Token: 0x06008500 RID: 34048 RVA: 0x0026CCFC File Offset: 0x0026CCFC
		public static Span<T> Empty
		{
			get
			{
				return default(Span<T>);
			}
		}

		// Token: 0x06008501 RID: 34049 RVA: 0x0026CD18 File Offset: 0x0026CD18
		public Span<T>.Enumerator GetEnumerator()
		{
			return new Span<T>.Enumerator(this);
		}

		// Token: 0x06008502 RID: 34050 RVA: 0x0026CD28 File Offset: 0x0026CD28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span(T[] array)
		{
			if (array == null)
			{
				this = default(Span<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			this._length = array.Length;
			this._pinnable = Unsafe.As<Pinnable<T>>(array);
			this._byteOffset = SpanHelpers.PerTypeValues<T>.ArrayAdjustment;
		}

		// Token: 0x06008503 RID: 34051 RVA: 0x0026CD9C File Offset: 0x0026CD9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Span<T> Create(T[] array, int start)
		{
			if (array == null)
			{
				if (start != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				return default(Span<T>);
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			IntPtr byteOffset = SpanHelpers.PerTypeValues<T>.ArrayAdjustment.Add(start);
			int length = array.Length - start;
			return new Span<T>(Unsafe.As<Pinnable<T>>(array), byteOffset, length);
		}

		// Token: 0x06008504 RID: 34052 RVA: 0x0026CE2C File Offset: 0x0026CE2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span(T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				this = default(Span<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			this._length = length;
			this._pinnable = Unsafe.As<Pinnable<T>>(array);
			this._byteOffset = SpanHelpers.PerTypeValues<T>.ArrayAdjustment.Add(start);
		}

		// Token: 0x06008505 RID: 34053 RVA: 0x0026CED0 File Offset: 0x0026CED0
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe Span(void* pointer, int length)
		{
			if (SpanHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowArgumentException_InvalidTypeWithPointersNotSupported(typeof(T));
			}
			if (length < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			this._length = length;
			this._pinnable = null;
			this._byteOffset = new IntPtr(pointer);
		}

		// Token: 0x06008506 RID: 34054 RVA: 0x0026CF24 File Offset: 0x0026CF24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Span(Pinnable<T> pinnable, IntPtr byteOffset, int length)
		{
			this._length = length;
			this._pinnable = pinnable;
			this._byteOffset = byteOffset;
		}

		// Token: 0x17001C83 RID: 7299
		public T this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (index >= this._length)
				{
					ThrowHelper.ThrowIndexOutOfRangeException();
				}
				if (this._pinnable == null)
				{
					return Unsafe.Add<T>(Unsafe.AsRef<T>(this._byteOffset.ToPointer()), index);
				}
				return Unsafe.Add<T>(Unsafe.AddByteOffset<T>(ref this._pinnable.Data, this._byteOffset), index);
			}
		}

		// Token: 0x06008508 RID: 34056 RVA: 0x0026CFA0 File Offset: 0x0026CFA0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ref T GetPinnableReference()
		{
			if (this._length == 0)
			{
				return Unsafe.AsRef<T>(null);
			}
			if (this._pinnable == null)
			{
				return Unsafe.AsRef<T>(this._byteOffset.ToPointer());
			}
			return Unsafe.AddByteOffset<T>(ref this._pinnable.Data, this._byteOffset);
		}

		// Token: 0x06008509 RID: 34057 RVA: 0x0026CFFC File Offset: 0x0026CFFC
		public unsafe void Clear()
		{
			int length = this._length;
			if (length == 0)
			{
				return;
			}
			UIntPtr byteLength = (UIntPtr)((ulong)length * (ulong)((long)Unsafe.SizeOf<T>()));
			if ((Unsafe.SizeOf<T>() & sizeof(IntPtr) - 1) != 0)
			{
				if (this._pinnable == null)
				{
					byte* ptr = (byte*)this._byteOffset.ToPointer();
					SpanHelpers.ClearLessThanPointerSized(ptr, byteLength);
					return;
				}
				ref byte b = ref Unsafe.As<T, byte>(Unsafe.AddByteOffset<T>(ref this._pinnable.Data, this._byteOffset));
				SpanHelpers.ClearLessThanPointerSized(ref b, byteLength);
				return;
			}
			else
			{
				if (SpanHelpers.IsReferenceOrContainsReferences<T>())
				{
					UIntPtr pointerSizeLength = (UIntPtr)((ulong)((long)(length * Unsafe.SizeOf<T>() / sizeof(IntPtr))));
					ref IntPtr ip = ref Unsafe.As<T, IntPtr>(this.DangerousGetPinnableReference());
					SpanHelpers.ClearPointerSizedWithReferences(ref ip, pointerSizeLength);
					return;
				}
				ref byte b2 = ref Unsafe.As<T, byte>(this.DangerousGetPinnableReference());
				SpanHelpers.ClearPointerSizedWithoutReferences(ref b2, byteLength);
				return;
			}
		}

		// Token: 0x0600850A RID: 34058 RVA: 0x0026D0D4 File Offset: 0x0026D0D4
		public unsafe void Fill(T value)
		{
			int length = this._length;
			if (length == 0)
			{
				return;
			}
			if (Unsafe.SizeOf<T>() != 1)
			{
				ref T source = ref this.DangerousGetPinnableReference();
				int i;
				for (i = 0; i < (length & -8); i += 8)
				{
					*Unsafe.Add<T>(ref source, i) = value;
					*Unsafe.Add<T>(ref source, i + 1) = value;
					*Unsafe.Add<T>(ref source, i + 2) = value;
					*Unsafe.Add<T>(ref source, i + 3) = value;
					*Unsafe.Add<T>(ref source, i + 4) = value;
					*Unsafe.Add<T>(ref source, i + 5) = value;
					*Unsafe.Add<T>(ref source, i + 6) = value;
					*Unsafe.Add<T>(ref source, i + 7) = value;
				}
				if (i < (length & -4))
				{
					*Unsafe.Add<T>(ref source, i) = value;
					*Unsafe.Add<T>(ref source, i + 1) = value;
					*Unsafe.Add<T>(ref source, i + 2) = value;
					*Unsafe.Add<T>(ref source, i + 3) = value;
					i += 4;
				}
				while (i < length)
				{
					*Unsafe.Add<T>(ref source, i) = value;
					i++;
				}
				return;
			}
			byte value2 = *Unsafe.As<T, byte>(ref value);
			if (this._pinnable == null)
			{
				Unsafe.InitBlockUnaligned(this._byteOffset.ToPointer(), value2, (uint)length);
				return;
			}
			ref byte startAddress = ref Unsafe.As<T, byte>(Unsafe.AddByteOffset<T>(ref this._pinnable.Data, this._byteOffset));
			Unsafe.InitBlockUnaligned(ref startAddress, value2, (uint)length);
		}

		// Token: 0x0600850B RID: 34059 RVA: 0x0026D268 File Offset: 0x0026D268
		public void CopyTo(Span<T> destination)
		{
			if (!this.TryCopyTo(destination))
			{
				ThrowHelper.ThrowArgumentException_DestinationTooShort();
			}
		}

		// Token: 0x0600850C RID: 34060 RVA: 0x0026D27C File Offset: 0x0026D27C
		public bool TryCopyTo(Span<T> destination)
		{
			int length = this._length;
			int length2 = destination._length;
			if (length == 0)
			{
				return true;
			}
			if (length > length2)
			{
				return false;
			}
			ref T src = ref this.DangerousGetPinnableReference();
			ref T dst = ref destination.DangerousGetPinnableReference();
			SpanHelpers.CopyTo<T>(ref dst, length2, ref src, length);
			return true;
		}

		// Token: 0x0600850D RID: 34061 RVA: 0x0026D2C8 File Offset: 0x0026D2C8
		public static bool operator ==(Span<T> left, Span<T> right)
		{
			return left._length == right._length && Unsafe.AreSame<T>(left.DangerousGetPinnableReference(), right.DangerousGetPinnableReference());
		}

		// Token: 0x0600850E RID: 34062 RVA: 0x0026D2F0 File Offset: 0x0026D2F0
		public static implicit operator ReadOnlySpan<T>(Span<T> span)
		{
			return new ReadOnlySpan<T>(span._pinnable, span._byteOffset, span._length);
		}

		// Token: 0x0600850F RID: 34063 RVA: 0x0026D30C File Offset: 0x0026D30C
		public unsafe override string ToString()
		{
			if (typeof(T) == typeof(char))
			{
				fixed (char* ptr = Unsafe.As<T, char>(this.DangerousGetPinnableReference()))
				{
					char* value = ptr;
					return new string(value, 0, this._length);
				}
			}
			return string.Format("System.Span<{0}>[{1}]", typeof(T).Name, this._length);
		}

		// Token: 0x06008510 RID: 34064 RVA: 0x0026D37C File Offset: 0x0026D37C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<T> Slice(int start)
		{
			if (start > this._length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			IntPtr byteOffset = this._byteOffset.Add(start);
			int length = this._length - start;
			return new Span<T>(this._pinnable, byteOffset, length);
		}

		// Token: 0x06008511 RID: 34065 RVA: 0x0026D3C4 File Offset: 0x0026D3C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<T> Slice(int start, int length)
		{
			if (start > this._length || length > this._length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			IntPtr byteOffset = this._byteOffset.Add(start);
			return new Span<T>(this._pinnable, byteOffset, length);
		}

		// Token: 0x06008512 RID: 34066 RVA: 0x0026D410 File Offset: 0x0026D410
		public T[] ToArray()
		{
			if (this._length == 0)
			{
				return SpanHelpers.PerTypeValues<T>.EmptyArray;
			}
			T[] array = new T[this._length];
			this.CopyTo(array);
			return array;
		}

		// Token: 0x06008513 RID: 34067 RVA: 0x0026D44C File Offset: 0x0026D44C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref T DangerousGetPinnableReference()
		{
			if (this._pinnable == null)
			{
				return Unsafe.AsRef<T>(this._byteOffset.ToPointer());
			}
			return Unsafe.AddByteOffset<T>(ref this._pinnable.Data, this._byteOffset);
		}

		// Token: 0x17001C84 RID: 7300
		// (get) Token: 0x06008514 RID: 34068 RVA: 0x0026D494 File Offset: 0x0026D494
		internal Pinnable<T> Pinnable
		{
			get
			{
				return this._pinnable;
			}
		}

		// Token: 0x17001C85 RID: 7301
		// (get) Token: 0x06008515 RID: 34069 RVA: 0x0026D49C File Offset: 0x0026D49C
		internal IntPtr ByteOffset
		{
			get
			{
				return this._byteOffset;
			}
		}

		// Token: 0x04003DB6 RID: 15798
		private readonly Pinnable<T> _pinnable;

		// Token: 0x04003DB7 RID: 15799
		private readonly IntPtr _byteOffset;

		// Token: 0x04003DB8 RID: 15800
		private readonly int _length;

		// Token: 0x020011BF RID: 4543
		public ref struct Enumerator
		{
			// Token: 0x06009663 RID: 38499 RVA: 0x002CBEAC File Offset: 0x002CBEAC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal Enumerator(Span<T> span)
			{
				this._span = span;
				this._index = -1;
			}

			// Token: 0x06009664 RID: 38500 RVA: 0x002CBEBC File Offset: 0x002CBEBC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				int num = this._index + 1;
				if (num < this._span.Length)
				{
					this._index = num;
					return true;
				}
				return false;
			}

			// Token: 0x17001F46 RID: 8006
			// (get) Token: 0x06009665 RID: 38501 RVA: 0x002CBEF4 File Offset: 0x002CBEF4
			public ref T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this._span[this._index];
				}
			}

			// Token: 0x04004C5F RID: 19551
			private readonly Span<T> _span;

			// Token: 0x04004C60 RID: 19552
			private int _index;
		}
	}
}
