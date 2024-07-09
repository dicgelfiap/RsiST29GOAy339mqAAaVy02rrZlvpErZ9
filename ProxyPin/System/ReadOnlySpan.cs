using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000CD8 RID: 3288
	[System.Memory.IsReadOnly]
	[DebuggerTypeProxy(typeof(SpanDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	[DebuggerTypeProxy(typeof(SpanDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	[ComVisible(true)]
	public ref struct ReadOnlySpan<T>
	{
		// Token: 0x17001C7A RID: 7290
		// (get) Token: 0x060084E0 RID: 34016 RVA: 0x0026C7C8 File Offset: 0x0026C7C8
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x17001C7B RID: 7291
		// (get) Token: 0x060084E1 RID: 34017 RVA: 0x0026C7D0 File Offset: 0x0026C7D0
		public bool IsEmpty
		{
			get
			{
				return this._length == 0;
			}
		}

		// Token: 0x060084E2 RID: 34018 RVA: 0x0026C7DC File Offset: 0x0026C7DC
		public static bool operator !=(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
		{
			return !(left == right);
		}

		// Token: 0x060084E3 RID: 34019 RVA: 0x0026C7E8 File Offset: 0x0026C7E8
		[Obsolete("Equals() on ReadOnlySpan will always throw an exception. Use == instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			throw new NotSupportedException(System.Memory2565708.SR.NotSupported_CannotCallEqualsOnSpan);
		}

		// Token: 0x060084E4 RID: 34020 RVA: 0x0026C7F4 File Offset: 0x0026C7F4
		[Obsolete("GetHashCode() on ReadOnlySpan will always throw an exception.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			throw new NotSupportedException(System.Memory2565708.SR.NotSupported_CannotCallGetHashCodeOnSpan);
		}

		// Token: 0x060084E5 RID: 34021 RVA: 0x0026C800 File Offset: 0x0026C800
		public static implicit operator ReadOnlySpan<T>(T[] array)
		{
			return new ReadOnlySpan<T>(array);
		}

		// Token: 0x060084E6 RID: 34022 RVA: 0x0026C808 File Offset: 0x0026C808
		public static implicit operator ReadOnlySpan<T>(ArraySegment<T> segment)
		{
			return new ReadOnlySpan<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x17001C7C RID: 7292
		// (get) Token: 0x060084E7 RID: 34023 RVA: 0x0026C834 File Offset: 0x0026C834
		public static ReadOnlySpan<T> Empty
		{
			get
			{
				return default(ReadOnlySpan<T>);
			}
		}

		// Token: 0x060084E8 RID: 34024 RVA: 0x0026C850 File Offset: 0x0026C850
		public ReadOnlySpan<T>.Enumerator GetEnumerator()
		{
			return new ReadOnlySpan<T>.Enumerator(this);
		}

		// Token: 0x060084E9 RID: 34025 RVA: 0x0026C860 File Offset: 0x0026C860
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan(T[] array)
		{
			if (array == null)
			{
				this = default(ReadOnlySpan<T>);
				return;
			}
			this._length = array.Length;
			this._pinnable = Unsafe.As<Pinnable<T>>(array);
			this._byteOffset = SpanHelpers.PerTypeValues<T>.ArrayAdjustment;
		}

		// Token: 0x060084EA RID: 34026 RVA: 0x0026C890 File Offset: 0x0026C890
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan(T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				this = default(ReadOnlySpan<T>);
				return;
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			this._length = length;
			this._pinnable = Unsafe.As<Pinnable<T>>(array);
			this._byteOffset = SpanHelpers.PerTypeValues<T>.ArrayAdjustment.Add(start);
		}

		// Token: 0x060084EB RID: 34027 RVA: 0x0026C900 File Offset: 0x0026C900
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ReadOnlySpan(void* pointer, int length)
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

		// Token: 0x060084EC RID: 34028 RVA: 0x0026C954 File Offset: 0x0026C954
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ReadOnlySpan(Pinnable<T> pinnable, IntPtr byteOffset, int length)
		{
			this._length = length;
			this._pinnable = pinnable;
			this._byteOffset = byteOffset;
		}

		// Token: 0x17001C7D RID: 7293
		[System.Memory.IsReadOnly]
		public T this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			[return: System.Memory.IsReadOnly]
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

		// Token: 0x060084EE RID: 34030 RVA: 0x0026C9D0 File Offset: 0x0026C9D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[return: System.Memory.IsReadOnly]
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

		// Token: 0x060084EF RID: 34031 RVA: 0x0026CA2C File Offset: 0x0026CA2C
		public void CopyTo(Span<T> destination)
		{
			if (!this.TryCopyTo(destination))
			{
				ThrowHelper.ThrowArgumentException_DestinationTooShort();
			}
		}

		// Token: 0x060084F0 RID: 34032 RVA: 0x0026CA40 File Offset: 0x0026CA40
		public bool TryCopyTo(Span<T> destination)
		{
			int length = this._length;
			int length2 = destination.Length;
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

		// Token: 0x060084F1 RID: 34033 RVA: 0x0026CA8C File Offset: 0x0026CA8C
		public static bool operator ==(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
		{
			return left._length == right._length && Unsafe.AreSame<T>(left.DangerousGetPinnableReference(), right.DangerousGetPinnableReference());
		}

		// Token: 0x060084F2 RID: 34034 RVA: 0x0026CAB4 File Offset: 0x0026CAB4
		public unsafe override string ToString()
		{
			if (typeof(T) == typeof(char))
			{
				if (this._byteOffset == MemoryExtensions.StringAdjustment)
				{
					object obj = Unsafe.As<object>(this._pinnable);
					string text;
					if ((text = (obj as string)) != null && this._length == text.Length)
					{
						return text;
					}
				}
				fixed (char* ptr = Unsafe.As<T, char>(this.DangerousGetPinnableReference()))
				{
					char* value = ptr;
					return new string(value, 0, this._length);
				}
			}
			return string.Format("System.ReadOnlySpan<{0}>[{1}]", typeof(T).Name, this._length);
		}

		// Token: 0x060084F3 RID: 34035 RVA: 0x0026CB68 File Offset: 0x0026CB68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan<T> Slice(int start)
		{
			if (start > this._length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			IntPtr byteOffset = this._byteOffset.Add(start);
			int length = this._length - start;
			return new ReadOnlySpan<T>(this._pinnable, byteOffset, length);
		}

		// Token: 0x060084F4 RID: 34036 RVA: 0x0026CBB0 File Offset: 0x0026CBB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan<T> Slice(int start, int length)
		{
			if (start > this._length || length > this._length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			IntPtr byteOffset = this._byteOffset.Add(start);
			return new ReadOnlySpan<T>(this._pinnable, byteOffset, length);
		}

		// Token: 0x060084F5 RID: 34037 RVA: 0x0026CBFC File Offset: 0x0026CBFC
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

		// Token: 0x060084F6 RID: 34038 RVA: 0x0026CC38 File Offset: 0x0026CC38
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

		// Token: 0x17001C7E RID: 7294
		// (get) Token: 0x060084F7 RID: 34039 RVA: 0x0026CC80 File Offset: 0x0026CC80
		internal Pinnable<T> Pinnable
		{
			get
			{
				return this._pinnable;
			}
		}

		// Token: 0x17001C7F RID: 7295
		// (get) Token: 0x060084F8 RID: 34040 RVA: 0x0026CC88 File Offset: 0x0026CC88
		internal IntPtr ByteOffset
		{
			get
			{
				return this._byteOffset;
			}
		}

		// Token: 0x04003DB3 RID: 15795
		private readonly Pinnable<T> _pinnable;

		// Token: 0x04003DB4 RID: 15796
		private readonly IntPtr _byteOffset;

		// Token: 0x04003DB5 RID: 15797
		private readonly int _length;

		// Token: 0x020011BE RID: 4542
		public ref struct Enumerator
		{
			// Token: 0x06009660 RID: 38496 RVA: 0x002CBE50 File Offset: 0x002CBE50
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal Enumerator(ReadOnlySpan<T> span)
			{
				this._span = span;
				this._index = -1;
			}

			// Token: 0x06009661 RID: 38497 RVA: 0x002CBE60 File Offset: 0x002CBE60
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

			// Token: 0x17001F45 RID: 8005
			// (get) Token: 0x06009662 RID: 38498 RVA: 0x002CBE98 File Offset: 0x002CBE98
			[System.Memory.IsReadOnly]
			public ref T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				[return: System.Memory.IsReadOnly]
				get
				{
					return this._span[this._index];
				}
			}

			// Token: 0x04004C5D RID: 19549
			private readonly ReadOnlySpan<T> _span;

			// Token: 0x04004C5E RID: 19550
			private int _index;
		}
	}
}
