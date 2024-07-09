using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000CB4 RID: 3252
	[DebuggerDisplay("IsEmpty = {IsEmpty}")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	[ComVisible(true)]
	public sealed class ImmutableQueue<T> : IImmutableQueue<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x060082EF RID: 33519 RVA: 0x00266758 File Offset: 0x00266758
		internal ImmutableQueue(ImmutableStack<T> forwards, ImmutableStack<T> backwards)
		{
			this._forwards = forwards;
			this._backwards = backwards;
		}

		// Token: 0x060082F0 RID: 33520 RVA: 0x00266770 File Offset: 0x00266770
		public ImmutableQueue<T> Clear()
		{
			return ImmutableQueue<T>.Empty;
		}

		// Token: 0x17001C34 RID: 7220
		// (get) Token: 0x060082F1 RID: 33521 RVA: 0x00266778 File Offset: 0x00266778
		public bool IsEmpty
		{
			get
			{
				return this._forwards.IsEmpty;
			}
		}

		// Token: 0x17001C35 RID: 7221
		// (get) Token: 0x060082F2 RID: 33522 RVA: 0x00266788 File Offset: 0x00266788
		public static ImmutableQueue<T> Empty
		{
			get
			{
				return ImmutableQueue<T>.s_EmptyField;
			}
		}

		// Token: 0x060082F3 RID: 33523 RVA: 0x00266790 File Offset: 0x00266790
		IImmutableQueue<T> IImmutableQueue<!0>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17001C36 RID: 7222
		// (get) Token: 0x060082F4 RID: 33524 RVA: 0x00266798 File Offset: 0x00266798
		private ImmutableStack<T> BackwardsReversed
		{
			get
			{
				if (this._backwardsReversed == null)
				{
					this._backwardsReversed = this._backwards.Reverse();
				}
				return this._backwardsReversed;
			}
		}

		// Token: 0x060082F5 RID: 33525 RVA: 0x002667BC File Offset: 0x002667BC
		public T Peek()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(System.Collections.Immutable2448884.SR.InvalidEmptyOperation);
			}
			return this._forwards.Peek();
		}

		// Token: 0x060082F6 RID: 33526 RVA: 0x002667E0 File Offset: 0x002667E0
		[return: System.Collections.Immutable.IsReadOnly]
		public ref T PeekRef()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(System.Collections.Immutable2448884.SR.InvalidEmptyOperation);
			}
			return this._forwards.PeekRef();
		}

		// Token: 0x060082F7 RID: 33527 RVA: 0x00266804 File Offset: 0x00266804
		public ImmutableQueue<T> Enqueue(T value)
		{
			if (this.IsEmpty)
			{
				return new ImmutableQueue<T>(ImmutableStack.Create<T>(value), ImmutableStack<T>.Empty);
			}
			return new ImmutableQueue<T>(this._forwards, this._backwards.Push(value));
		}

		// Token: 0x060082F8 RID: 33528 RVA: 0x0026683C File Offset: 0x0026683C
		IImmutableQueue<T> IImmutableQueue<!0>.Enqueue(T value)
		{
			return this.Enqueue(value);
		}

		// Token: 0x060082F9 RID: 33529 RVA: 0x00266848 File Offset: 0x00266848
		public ImmutableQueue<T> Dequeue()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(System.Collections.Immutable2448884.SR.InvalidEmptyOperation);
			}
			ImmutableStack<T> immutableStack = this._forwards.Pop();
			if (!immutableStack.IsEmpty)
			{
				return new ImmutableQueue<T>(immutableStack, this._backwards);
			}
			if (this._backwards.IsEmpty)
			{
				return ImmutableQueue<T>.Empty;
			}
			return new ImmutableQueue<T>(this.BackwardsReversed, ImmutableStack<T>.Empty);
		}

		// Token: 0x060082FA RID: 33530 RVA: 0x002668BC File Offset: 0x002668BC
		public ImmutableQueue<T> Dequeue(out T value)
		{
			value = this.Peek();
			return this.Dequeue();
		}

		// Token: 0x060082FB RID: 33531 RVA: 0x002668D0 File Offset: 0x002668D0
		IImmutableQueue<T> IImmutableQueue<!0>.Dequeue()
		{
			return this.Dequeue();
		}

		// Token: 0x060082FC RID: 33532 RVA: 0x002668D8 File Offset: 0x002668D8
		public ImmutableQueue<T>.Enumerator GetEnumerator()
		{
			return new ImmutableQueue<T>.Enumerator(this);
		}

		// Token: 0x060082FD RID: 33533 RVA: 0x002668E0 File Offset: 0x002668E0
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return new ImmutableQueue<T>.EnumeratorObject(this);
			}
			return Enumerable.Empty<T>().GetEnumerator();
		}

		// Token: 0x060082FE RID: 33534 RVA: 0x00266910 File Offset: 0x00266910
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ImmutableQueue<T>.EnumeratorObject(this);
		}

		// Token: 0x04003D3B RID: 15675
		private static readonly ImmutableQueue<T> s_EmptyField = new ImmutableQueue<T>(ImmutableStack<T>.Empty, ImmutableStack<T>.Empty);

		// Token: 0x04003D3C RID: 15676
		private readonly ImmutableStack<T> _backwards;

		// Token: 0x04003D3D RID: 15677
		private readonly ImmutableStack<T> _forwards;

		// Token: 0x04003D3E RID: 15678
		private ImmutableStack<T> _backwardsReversed;

		// Token: 0x020011AF RID: 4527
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator
		{
			// Token: 0x06009570 RID: 38256 RVA: 0x002C8E50 File Offset: 0x002C8E50
			internal Enumerator(ImmutableQueue<T> queue)
			{
				this._originalQueue = queue;
				this._remainingForwardsStack = null;
				this._remainingBackwardsStack = null;
			}

			// Token: 0x17001EFE RID: 7934
			// (get) Token: 0x06009571 RID: 38257 RVA: 0x002C8E68 File Offset: 0x002C8E68
			public T Current
			{
				get
				{
					if (this._remainingForwardsStack == null)
					{
						throw new InvalidOperationException();
					}
					if (!this._remainingForwardsStack.IsEmpty)
					{
						return this._remainingForwardsStack.Peek();
					}
					if (!this._remainingBackwardsStack.IsEmpty)
					{
						return this._remainingBackwardsStack.Peek();
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x06009572 RID: 38258 RVA: 0x002C8EC8 File Offset: 0x002C8EC8
			public bool MoveNext()
			{
				if (this._remainingForwardsStack == null)
				{
					this._remainingForwardsStack = this._originalQueue._forwards;
					this._remainingBackwardsStack = this._originalQueue.BackwardsReversed;
				}
				else if (!this._remainingForwardsStack.IsEmpty)
				{
					this._remainingForwardsStack = this._remainingForwardsStack.Pop();
				}
				else if (!this._remainingBackwardsStack.IsEmpty)
				{
					this._remainingBackwardsStack = this._remainingBackwardsStack.Pop();
				}
				return !this._remainingForwardsStack.IsEmpty || !this._remainingBackwardsStack.IsEmpty;
			}

			// Token: 0x04004C1B RID: 19483
			private readonly ImmutableQueue<T> _originalQueue;

			// Token: 0x04004C1C RID: 19484
			private ImmutableStack<T> _remainingForwardsStack;

			// Token: 0x04004C1D RID: 19485
			private ImmutableStack<T> _remainingBackwardsStack;
		}

		// Token: 0x020011B0 RID: 4528
		private class EnumeratorObject : IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06009573 RID: 38259 RVA: 0x002C8F74 File Offset: 0x002C8F74
			internal EnumeratorObject(ImmutableQueue<T> queue)
			{
				this._originalQueue = queue;
			}

			// Token: 0x17001EFF RID: 7935
			// (get) Token: 0x06009574 RID: 38260 RVA: 0x002C8F84 File Offset: 0x002C8F84
			public T Current
			{
				get
				{
					this.ThrowIfDisposed();
					if (this._remainingForwardsStack == null)
					{
						throw new InvalidOperationException();
					}
					if (!this._remainingForwardsStack.IsEmpty)
					{
						return this._remainingForwardsStack.Peek();
					}
					if (!this._remainingBackwardsStack.IsEmpty)
					{
						return this._remainingBackwardsStack.Peek();
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17001F00 RID: 7936
			// (get) Token: 0x06009575 RID: 38261 RVA: 0x002C8FEC File Offset: 0x002C8FEC
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06009576 RID: 38262 RVA: 0x002C8FFC File Offset: 0x002C8FFC
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				if (this._remainingForwardsStack == null)
				{
					this._remainingForwardsStack = this._originalQueue._forwards;
					this._remainingBackwardsStack = this._originalQueue.BackwardsReversed;
				}
				else if (!this._remainingForwardsStack.IsEmpty)
				{
					this._remainingForwardsStack = this._remainingForwardsStack.Pop();
				}
				else if (!this._remainingBackwardsStack.IsEmpty)
				{
					this._remainingBackwardsStack = this._remainingBackwardsStack.Pop();
				}
				return !this._remainingForwardsStack.IsEmpty || !this._remainingBackwardsStack.IsEmpty;
			}

			// Token: 0x06009577 RID: 38263 RVA: 0x002C90AC File Offset: 0x002C90AC
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._remainingBackwardsStack = null;
				this._remainingForwardsStack = null;
			}

			// Token: 0x06009578 RID: 38264 RVA: 0x002C90C4 File Offset: 0x002C90C4
			public void Dispose()
			{
				this._disposed = true;
			}

			// Token: 0x06009579 RID: 38265 RVA: 0x002C90D0 File Offset: 0x002C90D0
			private void ThrowIfDisposed()
			{
				if (this._disposed)
				{
					Requires.FailObjectDisposed<ImmutableQueue<T>.EnumeratorObject>(this);
				}
			}

			// Token: 0x04004C1E RID: 19486
			private readonly ImmutableQueue<T> _originalQueue;

			// Token: 0x04004C1F RID: 19487
			private ImmutableStack<T> _remainingForwardsStack;

			// Token: 0x04004C20 RID: 19488
			private ImmutableStack<T> _remainingBackwardsStack;

			// Token: 0x04004C21 RID: 19489
			private bool _disposed;
		}
	}
}
