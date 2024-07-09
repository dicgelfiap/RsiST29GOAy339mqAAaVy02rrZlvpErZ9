using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace PeNet.PatternMatching
{
	// Token: 0x02000BEF RID: 3055
	[ComVisible(true)]
	public class Trie<T, TValue>
	{
		// Token: 0x06007A95 RID: 31381 RVA: 0x00241224 File Offset: 0x00241224
		public void Add(IEnumerable<T> word, TValue value)
		{
			this._isBuild = false;
			Trie<T, TValue>.Node<T, TValue>[] node = new Trie<T, TValue>.Node<T, TValue>[]
			{
				this.root
			};
			Func<T, Trie<T, TValue>.Node<T, TValue>> <>9__0;
			Func<T, Trie<T, TValue>.Node<T, TValue>> selector;
			if ((selector = <>9__0) == null)
			{
				selector = (<>9__0 = delegate(T c)
				{
					Trie<T, TValue>.Node<T, TValue> result;
					if ((result = node[0][c]) == null)
					{
						result = (node[0][c] = new Trie<T, TValue>.Node<T, TValue>(c, node[0]));
					}
					return result;
				});
			}
			foreach (Trie<T, TValue>.Node<T, TValue> node2 in word.Select(selector))
			{
				node[0] = node2;
			}
			node[0].Values.Add(value);
		}

		// Token: 0x06007A96 RID: 31382 RVA: 0x002412DC File Offset: 0x002412DC
		private void Build()
		{
			Queue<Trie<T, TValue>.Node<T, TValue>> queue = new Queue<Trie<T, TValue>.Node<T, TValue>>();
			queue.Enqueue(this.root);
			while (queue.Count > 0)
			{
				Trie<T, TValue>.Node<T, TValue> node = queue.Dequeue();
				foreach (Trie<T, TValue>.Node<T, TValue> item in node)
				{
					queue.Enqueue(item);
				}
				if (node == this.root)
				{
					this.root.Fail = this.root;
				}
				else
				{
					Trie<T, TValue>.Node<T, TValue> fail = node.Parent.Fail;
					while (fail[node.Word] == null && fail != this.root)
					{
						fail = fail.Fail;
					}
					node.Fail = (fail[node.Word] ?? this.root);
					if (node.Fail == node)
					{
						node.Fail = this.root;
					}
				}
			}
		}

		// Token: 0x06007A97 RID: 31383 RVA: 0x002413E8 File Offset: 0x002413E8
		public IEnumerable<Tuple<TValue, int>> Find(IEnumerable<T> text)
		{
			if (!this._isBuild)
			{
				this.Build();
				this._isBuild = true;
			}
			Trie<T, TValue>.Node<T, TValue> node = this.root;
			int pos = 0;
			foreach (T c in text)
			{
				while (node[c] == null && node != this.root)
				{
					node = node.Fail;
				}
				node = (node[c] ?? this.root);
				Trie<T, TValue>.Node<T, TValue> t;
				for (t = node; t != this.root; t = t.Fail)
				{
					foreach (TValue item in t.Values)
					{
						yield return new Tuple<TValue, int>(item, pos);
					}
					List<TValue>.Enumerator enumerator2 = default(List<TValue>.Enumerator);
				}
				t = null;
				int num = pos;
				pos = num + 1;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04003B0B RID: 15115
		private readonly Trie<T, TValue>.Node<T, TValue> root = new Trie<T, TValue>.Node<T, TValue>();

		// Token: 0x04003B0C RID: 15116
		private bool _isBuild;

		// Token: 0x02001164 RID: 4452
		private class Node<TNode, TNodeValue> : IEnumerable<Trie<T, TValue>.Node<TNode, TNodeValue>>, IEnumerable
		{
			// Token: 0x06009316 RID: 37654 RVA: 0x002C21B4 File Offset: 0x002C21B4
			public Node()
			{
			}

			// Token: 0x06009317 RID: 37655 RVA: 0x002C21D4 File Offset: 0x002C21D4
			public Node(TNode word, Trie<T, TValue>.Node<TNode, TNodeValue> parent)
			{
				this.Word = word;
				this.Parent = parent;
			}

			// Token: 0x17001E72 RID: 7794
			// (get) Token: 0x06009318 RID: 37656 RVA: 0x002C2200 File Offset: 0x002C2200
			public TNode Word { get; }

			// Token: 0x17001E73 RID: 7795
			// (get) Token: 0x06009319 RID: 37657 RVA: 0x002C2208 File Offset: 0x002C2208
			public Trie<T, TValue>.Node<TNode, TNodeValue> Parent { get; }

			// Token: 0x17001E74 RID: 7796
			// (get) Token: 0x0600931A RID: 37658 RVA: 0x002C2210 File Offset: 0x002C2210
			// (set) Token: 0x0600931B RID: 37659 RVA: 0x002C2218 File Offset: 0x002C2218
			public Trie<T, TValue>.Node<TNode, TNodeValue> Fail { get; set; }

			// Token: 0x17001E75 RID: 7797
			public Trie<T, TValue>.Node<TNode, TNodeValue> this[TNode c]
			{
				get
				{
					if (!this.children.ContainsKey(c))
					{
						return null;
					}
					return this.children[c];
				}
				set
				{
					this.children[c] = value;
				}
			}

			// Token: 0x17001E76 RID: 7798
			// (get) Token: 0x0600931E RID: 37662 RVA: 0x002C2258 File Offset: 0x002C2258
			public List<TNodeValue> Values { get; } = new List<TNodeValue>();

			// Token: 0x0600931F RID: 37663 RVA: 0x002C2260 File Offset: 0x002C2260
			public IEnumerator<Trie<T, TValue>.Node<TNode, TNodeValue>> GetEnumerator()
			{
				return this.children.Values.GetEnumerator();
			}

			// Token: 0x06009320 RID: 37664 RVA: 0x002C2278 File Offset: 0x002C2278
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06009321 RID: 37665 RVA: 0x002C2280 File Offset: 0x002C2280
			public override string ToString()
			{
				TNode word = this.Word;
				return word.ToString();
			}

			// Token: 0x04004B08 RID: 19208
			private readonly Dictionary<TNode, Trie<T, TValue>.Node<TNode, TNodeValue>> children = new Dictionary<TNode, Trie<T, TValue>.Node<TNode, TNodeValue>>();
		}
	}
}
