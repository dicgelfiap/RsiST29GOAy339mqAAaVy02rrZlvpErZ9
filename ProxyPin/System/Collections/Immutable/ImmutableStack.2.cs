using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000CBC RID: 3260
	[DebuggerDisplay("IsEmpty = {IsEmpty}; Top = {_head}")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	[ComVisible(true)]
	public sealed class ImmutableStack<T> : IImmutableStack<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x060083B2 RID: 33714 RVA: 0x00268228 File Offset: 0x00268228
		private ImmutableStack()
		{
		}

		// Token: 0x060083B3 RID: 33715 RVA: 0x00268230 File Offset: 0x00268230
		private ImmutableStack(T head, ImmutableStack<T> tail)
		{
			this._head = head;
			this._tail = tail;
		}

		// Token: 0x17001C5A RID: 7258
		// (get) Token: 0x060083B4 RID: 33716 RVA: 0x00268248 File Offset: 0x00268248
		public static ImmutableStack<T> Empty
		{
			get
			{
				return ImmutableStack<T>.s_EmptyField;
			}
		}

		// Token: 0x060083B5 RID: 33717 RVA: 0x00268250 File Offset: 0x00268250
		public ImmutableStack<T> Clear()
		{
			return ImmutableStack<T>.Empty;
		}

		// Token: 0x060083B6 RID: 33718 RVA: 0x00268258 File Offset: 0x00268258
		IImmutableStack<T> IImmutableStack<!0>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17001C5B RID: 7259
		// (get) Token: 0x060083B7 RID: 33719 RVA: 0x00268260 File Offset: 0x00268260
		public bool IsEmpty
		{
			get
			{
				return this._tail == null;
			}
		}

		// Token: 0x060083B8 RID: 33720 RVA: 0x0026826C File Offset: 0x0026826C
		public T Peek()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(System.Collections.Immutable2448884.SR.InvalidEmptyOperation);
			}
			return this._head;
		}

		// Token: 0x060083B9 RID: 33721 RVA: 0x0026828C File Offset: 0x0026828C
		[return: System.Collections.Immutable.IsReadOnly]
		public ref T PeekRef()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(System.Collections.Immutable2448884.SR.InvalidEmptyOperation);
			}
			return ref this._head;
		}

		// Token: 0x060083BA RID: 33722 RVA: 0x002682AC File Offset: 0x002682AC
		public ImmutableStack<T> Push(T value)
		{
			return new ImmutableStack<T>(value, this);
		}

		// Token: 0x060083BB RID: 33723 RVA: 0x002682B8 File Offset: 0x002682B8
		IImmutableStack<T> IImmutableStack<!0>.Push(T value)
		{
			return this.Push(value);
		}

		// Token: 0x060083BC RID: 33724 RVA: 0x002682C4 File Offset: 0x002682C4
		public ImmutableStack<T> Pop()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(System.Collections.Immutable2448884.SR.InvalidEmptyOperation);
			}
			return this._tail;
		}

		// Token: 0x060083BD RID: 33725 RVA: 0x002682E4 File Offset: 0x002682E4
		public ImmutableStack<T> Pop(out T value)
		{
			value = this.Peek();
			return this.Pop();
		}

		// Token: 0x060083BE RID: 33726 RVA: 0x002682F8 File Offset: 0x002682F8
		IImmutableStack<T> IImmutableStack<!0>.Pop()
		{
			return this.Pop();
		}

		// Token: 0x060083BF RID: 33727 RVA: 0x00268300 File Offset: 0x00268300
		public ImmutableStack<T>.Enumerator GetEnumerator()
		{
			return new ImmutableStack<T>.Enumerator(this);
		}

		// Token: 0x060083C0 RID: 33728 RVA: 0x00268308 File Offset: 0x00268308
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return new ImmutableStack<T>.EnumeratorObject(this);
			}
			return Enumerable.Empty<T>().GetEnumerator();
		}

		// Token: 0x060083C1 RID: 33729 RVA: 0x00268338 File Offset: 0x00268338
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ImmutableStack<T>.EnumeratorObject(this);
		}

		// Token: 0x060083C2 RID: 33730 RVA: 0x00268340 File Offset: 0x00268340
		internal ImmutableStack<T> Reverse()
		{
			ImmutableStack<T> immutableStack = this.Clear();
			ImmutableStack<T> immutableStack2 = this;
			while (!immutableStack2.IsEmpty)
			{
				immutableStack = immutableStack.Push(immutableStack2.Peek());
				immutableStack2 = immutableStack2.Pop();
			}
			return immutableStack;
		}

		// Token: 0x04003D4B RID: 15691
		private static readonly ImmutableStack<T> s_EmptyField = new ImmutableStack<T>();

		// Token: 0x04003D4C RID: 15692
		private readonly T _head;

		// Token: 0x04003D4D RID: 15693
		private readonly ImmutableStack<T> _tail;

		// Token: 0x020011B9 RID: 4537
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator
		{
			// Token: 0x06009640 RID: 38464 RVA: 0x002CB864 File Offset: 0x002CB864
			internal Enumerator(ImmutableStack<T> stack)
			{
				Requires.NotNull<ImmutableStack<T>>(stack, "stack");
				this._originalStack = stack;
				this._remainingStack = null;
			}

			// Token: 0x17001F3D RID: 7997
			// (get) Token: 0x06009641 RID: 38465 RVA: 0x002CB880 File Offset: 0x002CB880
			public T Current
			{
				get
				{
					if (this._remainingStack == null || this._remainingStack.IsEmpty)
					{
						throw new InvalidOperationException();
					}
					return this._remainingStack.Peek();
				}
			}

			// Token: 0x06009642 RID: 38466 RVA: 0x002CB8B0 File Offset: 0x002CB8B0
			public bool MoveNext()
			{
				if (this._remainingStack == null)
				{
					this._remainingStack = this._originalStack;
				}
				else if (!this._remainingStack.IsEmpty)
				{
					this._remainingStack = this._remainingStack.Pop();
				}
				return !this._remainingStack.IsEmpty;
			}

			// Token: 0x04004C4E RID: 19534
			private readonly ImmutableStack<T> _originalStack;

			// Token: 0x04004C4F RID: 19535
			private ImmutableStack<T> _remainingStack;
		}

		// Token: 0x020011BA RID: 4538
		private class EnumeratorObject : IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06009643 RID: 38467 RVA: 0x002CB90C File Offset: 0x002CB90C
			internal EnumeratorObject(ImmutableStack<T> stack)
			{
				Requires.NotNull<ImmutableStack<T>>(stack, "stack");
				this._originalStack = stack;
			}

			// Token: 0x17001F3E RID: 7998
			// (get) Token: 0x06009644 RID: 38468 RVA: 0x002CB928 File Offset: 0x002CB928
			public T Current
			{
				get
				{
					this.ThrowIfDisposed();
					if (this._remainingStack == null || this._remainingStack.IsEmpty)
					{
						throw new InvalidOperationException();
					}
					return this._remainingStack.Peek();
				}
			}

			// Token: 0x17001F3F RID: 7999
			// (get) Token: 0x06009645 RID: 38469 RVA: 0x002CB95C File Offset: 0x002CB95C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06009646 RID: 38470 RVA: 0x002CB96C File Offset: 0x002CB96C
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				if (this._remainingStack == null)
				{
					this._remainingStack = this._originalStack;
				}
				else if (!this._remainingStack.IsEmpty)
				{
					this._remainingStack = this._remainingStack.Pop();
				}
				return !this._remainingStack.IsEmpty;
			}

			// Token: 0x06009647 RID: 38471 RVA: 0x002CB9D0 File Offset: 0x002CB9D0
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._remainingStack = null;
			}

			// Token: 0x06009648 RID: 38472 RVA: 0x002CB9E0 File Offset: 0x002CB9E0
			public void Dispose()
			{
				this._disposed = true;
			}

			// Token: 0x06009649 RID: 38473 RVA: 0x002CB9EC File Offset: 0x002CB9EC
			private void ThrowIfDisposed()
			{
				if (this._disposed)
				{
					Requires.FailObjectDisposed<ImmutableStack<T>.EnumeratorObject>(this);
				}
			}

			// Token: 0x04004C50 RID: 19536
			private readonly ImmutableStack<T> _originalStack;

			// Token: 0x04004C51 RID: 19537
			private ImmutableStack<T> _remainingStack;

			// Token: 0x04004C52 RID: 19538
			private bool _disposed;
		}
	}
}
