using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace System.Collections.Immutable
{
	// Token: 0x02000CC5 RID: 3269
	[DebuggerDisplay("{_key} = {_value}")]
	internal sealed class SortedInt32KeyNode<TValue> : IBinaryTree
	{
		// Token: 0x060083E4 RID: 33764 RVA: 0x00268714 File Offset: 0x00268714
		private SortedInt32KeyNode()
		{
			this._frozen = true;
		}

		// Token: 0x060083E5 RID: 33765 RVA: 0x00268724 File Offset: 0x00268724
		private SortedInt32KeyNode(int key, TValue value, SortedInt32KeyNode<TValue> left, SortedInt32KeyNode<TValue> right, bool frozen = false)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(left, "left");
			Requires.NotNull<SortedInt32KeyNode<TValue>>(right, "right");
			this._key = key;
			this._value = value;
			this._left = left;
			this._right = right;
			this._frozen = frozen;
			this._height = checked(1 + Math.Max(left._height, right._height));
		}

		// Token: 0x17001C63 RID: 7267
		// (get) Token: 0x060083E6 RID: 33766 RVA: 0x00268794 File Offset: 0x00268794
		public bool IsEmpty
		{
			get
			{
				return this._left == null;
			}
		}

		// Token: 0x17001C64 RID: 7268
		// (get) Token: 0x060083E7 RID: 33767 RVA: 0x002687A0 File Offset: 0x002687A0
		public int Height
		{
			get
			{
				return (int)this._height;
			}
		}

		// Token: 0x17001C65 RID: 7269
		// (get) Token: 0x060083E8 RID: 33768 RVA: 0x002687A8 File Offset: 0x002687A8
		public SortedInt32KeyNode<TValue> Left
		{
			get
			{
				return this._left;
			}
		}

		// Token: 0x17001C66 RID: 7270
		// (get) Token: 0x060083E9 RID: 33769 RVA: 0x002687B0 File Offset: 0x002687B0
		public SortedInt32KeyNode<TValue> Right
		{
			get
			{
				return this._right;
			}
		}

		// Token: 0x17001C67 RID: 7271
		// (get) Token: 0x060083EA RID: 33770 RVA: 0x002687B8 File Offset: 0x002687B8
		IBinaryTree IBinaryTree.Left
		{
			get
			{
				return this._left;
			}
		}

		// Token: 0x17001C68 RID: 7272
		// (get) Token: 0x060083EB RID: 33771 RVA: 0x002687C0 File Offset: 0x002687C0
		IBinaryTree IBinaryTree.Right
		{
			get
			{
				return this._right;
			}
		}

		// Token: 0x17001C69 RID: 7273
		// (get) Token: 0x060083EC RID: 33772 RVA: 0x002687C8 File Offset: 0x002687C8
		int IBinaryTree.Count
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001C6A RID: 7274
		// (get) Token: 0x060083ED RID: 33773 RVA: 0x002687D0 File Offset: 0x002687D0
		public KeyValuePair<int, TValue> Value
		{
			get
			{
				return new KeyValuePair<int, TValue>(this._key, this._value);
			}
		}

		// Token: 0x17001C6B RID: 7275
		// (get) Token: 0x060083EE RID: 33774 RVA: 0x002687E4 File Offset: 0x002687E4
		internal IEnumerable<TValue> Values
		{
			get
			{
				foreach (KeyValuePair<int, TValue> keyValuePair in this)
				{
					yield return keyValuePair.Value;
				}
				SortedInt32KeyNode<TValue>.Enumerator enumerator = default(SortedInt32KeyNode<TValue>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x060083EF RID: 33775 RVA: 0x00268808 File Offset: 0x00268808
		public SortedInt32KeyNode<TValue>.Enumerator GetEnumerator()
		{
			return new SortedInt32KeyNode<TValue>.Enumerator(this);
		}

		// Token: 0x060083F0 RID: 33776 RVA: 0x00268810 File Offset: 0x00268810
		internal SortedInt32KeyNode<TValue> SetItem(int key, TValue value, IEqualityComparer<TValue> valueComparer, out bool replacedExistingValue, out bool mutated)
		{
			Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
			return this.SetOrAdd(key, value, valueComparer, true, out replacedExistingValue, out mutated);
		}

		// Token: 0x060083F1 RID: 33777 RVA: 0x0026882C File Offset: 0x0026882C
		internal SortedInt32KeyNode<TValue> Remove(int key, out bool mutated)
		{
			return this.RemoveRecursive(key, out mutated);
		}

		// Token: 0x060083F2 RID: 33778 RVA: 0x00268838 File Offset: 0x00268838
		internal TValue GetValueOrDefault(int key)
		{
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = this;
			while (!sortedInt32KeyNode.IsEmpty)
			{
				if (key == sortedInt32KeyNode._key)
				{
					return sortedInt32KeyNode._value;
				}
				if (key > sortedInt32KeyNode._key)
				{
					sortedInt32KeyNode = sortedInt32KeyNode._right;
				}
				else
				{
					sortedInt32KeyNode = sortedInt32KeyNode._left;
				}
			}
			return default(TValue);
		}

		// Token: 0x060083F3 RID: 33779 RVA: 0x00268890 File Offset: 0x00268890
		internal bool TryGetValue(int key, out TValue value)
		{
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = this;
			while (!sortedInt32KeyNode.IsEmpty)
			{
				if (key == sortedInt32KeyNode._key)
				{
					value = sortedInt32KeyNode._value;
					return true;
				}
				if (key > sortedInt32KeyNode._key)
				{
					sortedInt32KeyNode = sortedInt32KeyNode._right;
				}
				else
				{
					sortedInt32KeyNode = sortedInt32KeyNode._left;
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x060083F4 RID: 33780 RVA: 0x002688F0 File Offset: 0x002688F0
		internal void Freeze(Action<KeyValuePair<int, TValue>> freezeAction = null)
		{
			if (!this._frozen)
			{
				if (freezeAction != null)
				{
					freezeAction(new KeyValuePair<int, TValue>(this._key, this._value));
				}
				this._left.Freeze(freezeAction);
				this._right.Freeze(freezeAction);
				this._frozen = true;
			}
		}

		// Token: 0x060083F5 RID: 33781 RVA: 0x00268948 File Offset: 0x00268948
		private static SortedInt32KeyNode<TValue> RotateLeft(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (tree._right.IsEmpty)
			{
				return tree;
			}
			SortedInt32KeyNode<TValue> right = tree._right;
			return right.Mutate(tree.Mutate(null, right._left), null);
		}

		// Token: 0x060083F6 RID: 33782 RVA: 0x00268994 File Offset: 0x00268994
		private static SortedInt32KeyNode<TValue> RotateRight(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (tree._left.IsEmpty)
			{
				return tree;
			}
			SortedInt32KeyNode<TValue> left = tree._left;
			return left.Mutate(null, tree.Mutate(left._right, null));
		}

		// Token: 0x060083F7 RID: 33783 RVA: 0x002689E0 File Offset: 0x002689E0
		private static SortedInt32KeyNode<TValue> DoubleLeft(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (tree._right.IsEmpty)
			{
				return tree;
			}
			SortedInt32KeyNode<TValue> tree2 = tree.Mutate(null, SortedInt32KeyNode<TValue>.RotateRight(tree._right));
			return SortedInt32KeyNode<TValue>.RotateLeft(tree2);
		}

		// Token: 0x060083F8 RID: 33784 RVA: 0x00268A28 File Offset: 0x00268A28
		private static SortedInt32KeyNode<TValue> DoubleRight(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (tree._left.IsEmpty)
			{
				return tree;
			}
			SortedInt32KeyNode<TValue> tree2 = tree.Mutate(SortedInt32KeyNode<TValue>.RotateLeft(tree._left), null);
			return SortedInt32KeyNode<TValue>.RotateRight(tree2);
		}

		// Token: 0x060083F9 RID: 33785 RVA: 0x00268A70 File Offset: 0x00268A70
		private static int Balance(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			return (int)(tree._right._height - tree._left._height);
		}

		// Token: 0x060083FA RID: 33786 RVA: 0x00268A94 File Offset: 0x00268A94
		private static bool IsRightHeavy(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			return SortedInt32KeyNode<TValue>.Balance(tree) >= 2;
		}

		// Token: 0x060083FB RID: 33787 RVA: 0x00268AB0 File Offset: 0x00268AB0
		private static bool IsLeftHeavy(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			return SortedInt32KeyNode<TValue>.Balance(tree) <= -2;
		}

		// Token: 0x060083FC RID: 33788 RVA: 0x00268ACC File Offset: 0x00268ACC
		private static SortedInt32KeyNode<TValue> MakeBalanced(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (SortedInt32KeyNode<TValue>.IsRightHeavy(tree))
			{
				if (SortedInt32KeyNode<TValue>.Balance(tree._right) >= 0)
				{
					return SortedInt32KeyNode<TValue>.RotateLeft(tree);
				}
				return SortedInt32KeyNode<TValue>.DoubleLeft(tree);
			}
			else
			{
				if (!SortedInt32KeyNode<TValue>.IsLeftHeavy(tree))
				{
					return tree;
				}
				if (SortedInt32KeyNode<TValue>.Balance(tree._left) <= 0)
				{
					return SortedInt32KeyNode<TValue>.RotateRight(tree);
				}
				return SortedInt32KeyNode<TValue>.DoubleRight(tree);
			}
		}

		// Token: 0x060083FD RID: 33789 RVA: 0x00268B40 File Offset: 0x00268B40
		private SortedInt32KeyNode<TValue> SetOrAdd(int key, TValue value, IEqualityComparer<TValue> valueComparer, bool overwriteExistingValue, out bool replacedExistingValue, out bool mutated)
		{
			replacedExistingValue = false;
			if (this.IsEmpty)
			{
				mutated = true;
				return new SortedInt32KeyNode<TValue>(key, value, this, this, false);
			}
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = this;
			if (key > this._key)
			{
				SortedInt32KeyNode<TValue> right = this._right.SetOrAdd(key, value, valueComparer, overwriteExistingValue, out replacedExistingValue, out mutated);
				if (mutated)
				{
					sortedInt32KeyNode = this.Mutate(null, right);
				}
			}
			else if (key < this._key)
			{
				SortedInt32KeyNode<TValue> left = this._left.SetOrAdd(key, value, valueComparer, overwriteExistingValue, out replacedExistingValue, out mutated);
				if (mutated)
				{
					sortedInt32KeyNode = this.Mutate(left, null);
				}
			}
			else
			{
				if (valueComparer.Equals(this._value, value))
				{
					mutated = false;
					return this;
				}
				if (!overwriteExistingValue)
				{
					throw new ArgumentException(System.Collections.Immutable2448884.SR.Format(System.Collections.Immutable2448884.SR.DuplicateKey, key));
				}
				mutated = true;
				replacedExistingValue = true;
				sortedInt32KeyNode = new SortedInt32KeyNode<TValue>(key, value, this._left, this._right, false);
			}
			if (!mutated)
			{
				return sortedInt32KeyNode;
			}
			return SortedInt32KeyNode<TValue>.MakeBalanced(sortedInt32KeyNode);
		}

		// Token: 0x060083FE RID: 33790 RVA: 0x00268C48 File Offset: 0x00268C48
		private SortedInt32KeyNode<TValue> RemoveRecursive(int key, out bool mutated)
		{
			if (this.IsEmpty)
			{
				mutated = false;
				return this;
			}
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = this;
			if (key == this._key)
			{
				mutated = true;
				if (this._right.IsEmpty && this._left.IsEmpty)
				{
					sortedInt32KeyNode = SortedInt32KeyNode<TValue>.EmptyNode;
				}
				else if (this._right.IsEmpty && !this._left.IsEmpty)
				{
					sortedInt32KeyNode = this._left;
				}
				else if (!this._right.IsEmpty && this._left.IsEmpty)
				{
					sortedInt32KeyNode = this._right;
				}
				else
				{
					SortedInt32KeyNode<TValue> sortedInt32KeyNode2 = this._right;
					while (!sortedInt32KeyNode2._left.IsEmpty)
					{
						sortedInt32KeyNode2 = sortedInt32KeyNode2._left;
					}
					bool flag;
					SortedInt32KeyNode<TValue> right = this._right.Remove(sortedInt32KeyNode2._key, out flag);
					sortedInt32KeyNode = sortedInt32KeyNode2.Mutate(this._left, right);
				}
			}
			else if (key < this._key)
			{
				SortedInt32KeyNode<TValue> left = this._left.Remove(key, out mutated);
				if (mutated)
				{
					sortedInt32KeyNode = this.Mutate(left, null);
				}
			}
			else
			{
				SortedInt32KeyNode<TValue> right2 = this._right.Remove(key, out mutated);
				if (mutated)
				{
					sortedInt32KeyNode = this.Mutate(null, right2);
				}
			}
			if (!sortedInt32KeyNode.IsEmpty)
			{
				return SortedInt32KeyNode<TValue>.MakeBalanced(sortedInt32KeyNode);
			}
			return sortedInt32KeyNode;
		}

		// Token: 0x060083FF RID: 33791 RVA: 0x00268DA8 File Offset: 0x00268DA8
		private SortedInt32KeyNode<TValue> Mutate(SortedInt32KeyNode<TValue> left = null, SortedInt32KeyNode<TValue> right = null)
		{
			if (this._frozen)
			{
				return new SortedInt32KeyNode<TValue>(this._key, this._value, left ?? this._left, right ?? this._right, false);
			}
			if (left != null)
			{
				this._left = left;
			}
			if (right != null)
			{
				this._right = right;
			}
			this._height = checked(1 + Math.Max(this._left._height, this._right._height));
			return this;
		}

		// Token: 0x04003D55 RID: 15701
		internal static readonly SortedInt32KeyNode<TValue> EmptyNode = new SortedInt32KeyNode<TValue>();

		// Token: 0x04003D56 RID: 15702
		private readonly int _key;

		// Token: 0x04003D57 RID: 15703
		private readonly TValue _value;

		// Token: 0x04003D58 RID: 15704
		private bool _frozen;

		// Token: 0x04003D59 RID: 15705
		private byte _height;

		// Token: 0x04003D5A RID: 15706
		private SortedInt32KeyNode<TValue> _left;

		// Token: 0x04003D5B RID: 15707
		private SortedInt32KeyNode<TValue> _right;

		// Token: 0x020011BB RID: 4539
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator : IEnumerator<KeyValuePair<int, TValue>>, IDisposable, IEnumerator, ISecurePooledObjectUser
		{
			// Token: 0x0600964A RID: 38474 RVA: 0x002CBA00 File Offset: 0x002CBA00
			internal Enumerator(SortedInt32KeyNode<TValue> root)
			{
				Requires.NotNull<SortedInt32KeyNode<TValue>>(root, "root");
				this._root = root;
				this._current = null;
				this._poolUserId = SecureObjectPool.NewId();
				this._stack = null;
				if (!this._root.IsEmpty)
				{
					if (!SortedInt32KeyNode<TValue>.Enumerator.s_enumeratingStacks.TryTake(this, out this._stack))
					{
						this._stack = SortedInt32KeyNode<TValue>.Enumerator.s_enumeratingStacks.PrepNew(this, new Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>(root.Height));
					}
					this.PushLeft(this._root);
				}
			}

			// Token: 0x17001F40 RID: 8000
			// (get) Token: 0x0600964B RID: 38475 RVA: 0x002CBA94 File Offset: 0x002CBA94
			public KeyValuePair<int, TValue> Current
			{
				get
				{
					this.ThrowIfDisposed();
					if (this._current != null)
					{
						return this._current.Value;
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17001F41 RID: 8001
			// (get) Token: 0x0600964C RID: 38476 RVA: 0x002CBAB8 File Offset: 0x002CBAB8
			int ISecurePooledObjectUser.PoolUserId
			{
				get
				{
					return this._poolUserId;
				}
			}

			// Token: 0x17001F42 RID: 8002
			// (get) Token: 0x0600964D RID: 38477 RVA: 0x002CBAC0 File Offset: 0x002CBAC0
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600964E RID: 38478 RVA: 0x002CBAD0 File Offset: 0x002CBAD0
			public void Dispose()
			{
				this._root = null;
				this._current = null;
				Stack<RefAsValueType<SortedInt32KeyNode<TValue>>> stack;
				if (this._stack != null && this._stack.TryUse<SortedInt32KeyNode<TValue>.Enumerator>(ref this, out stack))
				{
					stack.ClearFastWhenEmpty<RefAsValueType<SortedInt32KeyNode<TValue>>>();
					SortedInt32KeyNode<TValue>.Enumerator.s_enumeratingStacks.TryAdd(this, this._stack);
				}
				this._stack = null;
			}

			// Token: 0x0600964F RID: 38479 RVA: 0x002CBB30 File Offset: 0x002CBB30
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				if (this._stack != null)
				{
					Stack<RefAsValueType<SortedInt32KeyNode<TValue>>> stack = this._stack.Use<SortedInt32KeyNode<TValue>.Enumerator>(ref this);
					if (stack.Count > 0)
					{
						SortedInt32KeyNode<TValue> value = stack.Pop().Value;
						this._current = value;
						this.PushLeft(value.Right);
						return true;
					}
				}
				this._current = null;
				return false;
			}

			// Token: 0x06009650 RID: 38480 RVA: 0x002CBB94 File Offset: 0x002CBB94
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._current = null;
				if (this._stack != null)
				{
					Stack<RefAsValueType<SortedInt32KeyNode<TValue>>> stack = this._stack.Use<SortedInt32KeyNode<TValue>.Enumerator>(ref this);
					stack.ClearFastWhenEmpty<RefAsValueType<SortedInt32KeyNode<TValue>>>();
					this.PushLeft(this._root);
				}
			}

			// Token: 0x06009651 RID: 38481 RVA: 0x002CBBDC File Offset: 0x002CBBDC
			internal void ThrowIfDisposed()
			{
				if (this._root == null || (this._stack != null && !this._stack.IsOwned<SortedInt32KeyNode<TValue>.Enumerator>(ref this)))
				{
					Requires.FailObjectDisposed<SortedInt32KeyNode<TValue>.Enumerator>(this);
				}
			}

			// Token: 0x06009652 RID: 38482 RVA: 0x002CBC10 File Offset: 0x002CBC10
			private void PushLeft(SortedInt32KeyNode<TValue> node)
			{
				Requires.NotNull<SortedInt32KeyNode<TValue>>(node, "node");
				Stack<RefAsValueType<SortedInt32KeyNode<TValue>>> stack = this._stack.Use<SortedInt32KeyNode<TValue>.Enumerator>(ref this);
				while (!node.IsEmpty)
				{
					stack.Push(new RefAsValueType<SortedInt32KeyNode<TValue>>(node));
					node = node.Left;
				}
			}

			// Token: 0x04004C53 RID: 19539
			private static readonly SecureObjectPool<Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>, SortedInt32KeyNode<TValue>.Enumerator> s_enumeratingStacks = new SecureObjectPool<Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>, SortedInt32KeyNode<TValue>.Enumerator>();

			// Token: 0x04004C54 RID: 19540
			private readonly int _poolUserId;

			// Token: 0x04004C55 RID: 19541
			private SortedInt32KeyNode<TValue> _root;

			// Token: 0x04004C56 RID: 19542
			private SecurePooledObject<Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>> _stack;

			// Token: 0x04004C57 RID: 19543
			private SortedInt32KeyNode<TValue> _current;
		}
	}
}
