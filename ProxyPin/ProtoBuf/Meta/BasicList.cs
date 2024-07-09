using System;
using System.Collections;

namespace ProtoBuf.Meta
{
	// Token: 0x02000C72 RID: 3186
	internal class BasicList : IEnumerable
	{
		// Token: 0x06007E98 RID: 32408 RVA: 0x00253E44 File Offset: 0x00253E44
		public void CopyTo(Array array, int offset)
		{
			this.head.CopyTo(array, offset);
		}

		// Token: 0x06007E99 RID: 32409 RVA: 0x00253E54 File Offset: 0x00253E54
		public int Add(object value)
		{
			return (this.head = this.head.Append(value)).Length - 1;
		}

		// Token: 0x17001B83 RID: 7043
		public object this[int index]
		{
			get
			{
				return this.head[index];
			}
		}

		// Token: 0x06007E9B RID: 32411 RVA: 0x00253E94 File Offset: 0x00253E94
		public void Trim()
		{
			this.head = this.head.Trim();
		}

		// Token: 0x17001B84 RID: 7044
		// (get) Token: 0x06007E9C RID: 32412 RVA: 0x00253EA8 File Offset: 0x00253EA8
		public int Count
		{
			get
			{
				return this.head.Length;
			}
		}

		// Token: 0x06007E9D RID: 32413 RVA: 0x00253EB8 File Offset: 0x00253EB8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new BasicList.NodeEnumerator(this.head);
		}

		// Token: 0x06007E9E RID: 32414 RVA: 0x00253ECC File Offset: 0x00253ECC
		public BasicList.NodeEnumerator GetEnumerator()
		{
			return new BasicList.NodeEnumerator(this.head);
		}

		// Token: 0x06007E9F RID: 32415 RVA: 0x00253EDC File Offset: 0x00253EDC
		internal int IndexOf(BasicList.MatchPredicate predicate, object ctx)
		{
			return this.head.IndexOf(predicate, ctx);
		}

		// Token: 0x06007EA0 RID: 32416 RVA: 0x00253EEC File Offset: 0x00253EEC
		internal int IndexOfString(string value)
		{
			return this.head.IndexOfString(value);
		}

		// Token: 0x06007EA1 RID: 32417 RVA: 0x00253EFC File Offset: 0x00253EFC
		internal int IndexOfReference(object instance)
		{
			return this.head.IndexOfReference(instance);
		}

		// Token: 0x06007EA2 RID: 32418 RVA: 0x00253F0C File Offset: 0x00253F0C
		internal bool Contains(object value)
		{
			foreach (object objA in this)
			{
				if (object.Equals(objA, value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007EA3 RID: 32419 RVA: 0x00253F4C File Offset: 0x00253F4C
		internal static BasicList GetContiguousGroups(int[] keys, object[] values)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (values.Length < keys.Length)
			{
				throw new ArgumentException("Not all keys are covered by values", "values");
			}
			BasicList basicList = new BasicList();
			BasicList.Group group = null;
			for (int i = 0; i < keys.Length; i++)
			{
				if (i == 0 || keys[i] != keys[i - 1])
				{
					group = null;
				}
				if (group == null)
				{
					group = new BasicList.Group(keys[i]);
					basicList.Add(group);
				}
				group.Items.Add(values[i]);
			}
			return basicList;
		}

		// Token: 0x04003C9F RID: 15519
		private static readonly BasicList.Node nil = new BasicList.Node(null, 0);

		// Token: 0x04003CA0 RID: 15520
		protected BasicList.Node head = BasicList.nil;

		// Token: 0x02001175 RID: 4469
		public struct NodeEnumerator : IEnumerator
		{
			// Token: 0x0600935B RID: 37723 RVA: 0x002C2DA0 File Offset: 0x002C2DA0
			internal NodeEnumerator(BasicList.Node node)
			{
				this.position = -1;
				this.node = node;
			}

			// Token: 0x0600935C RID: 37724 RVA: 0x002C2DB0 File Offset: 0x002C2DB0
			void IEnumerator.Reset()
			{
				this.position = -1;
			}

			// Token: 0x17001E83 RID: 7811
			// (get) Token: 0x0600935D RID: 37725 RVA: 0x002C2DBC File Offset: 0x002C2DBC
			public object Current
			{
				get
				{
					return this.node[this.position];
				}
			}

			// Token: 0x0600935E RID: 37726 RVA: 0x002C2DD0 File Offset: 0x002C2DD0
			public bool MoveNext()
			{
				int length = this.node.Length;
				if (this.position <= length)
				{
					int num = this.position + 1;
					this.position = num;
					return num < length;
				}
				return false;
			}

			// Token: 0x04004B4B RID: 19275
			private int position;

			// Token: 0x04004B4C RID: 19276
			private readonly BasicList.Node node;
		}

		// Token: 0x02001176 RID: 4470
		internal sealed class Node
		{
			// Token: 0x17001E84 RID: 7812
			public object this[int index]
			{
				get
				{
					if (index >= 0 && index < this.length)
					{
						return this.data[index];
					}
					throw new ArgumentOutOfRangeException("index");
				}
				set
				{
					if (index >= 0 && index < this.length)
					{
						this.data[index] = value;
						return;
					}
					throw new ArgumentOutOfRangeException("index");
				}
			}

			// Token: 0x17001E85 RID: 7813
			// (get) Token: 0x06009361 RID: 37729 RVA: 0x002C2E64 File Offset: 0x002C2E64
			public int Length
			{
				get
				{
					return this.length;
				}
			}

			// Token: 0x06009362 RID: 37730 RVA: 0x002C2E6C File Offset: 0x002C2E6C
			internal Node(object[] data, int length)
			{
				this.data = data;
				this.length = length;
			}

			// Token: 0x06009363 RID: 37731 RVA: 0x002C2E84 File Offset: 0x002C2E84
			public void RemoveLastWithMutate()
			{
				if (this.length == 0)
				{
					throw new InvalidOperationException();
				}
				this.length--;
			}

			// Token: 0x06009364 RID: 37732 RVA: 0x002C2EA8 File Offset: 0x002C2EA8
			public BasicList.Node Append(object value)
			{
				int num = this.length + 1;
				object[] array;
				if (this.data == null)
				{
					array = new object[10];
				}
				else if (this.length == this.data.Length)
				{
					array = new object[this.data.Length * 2];
					Array.Copy(this.data, array, this.length);
				}
				else
				{
					array = this.data;
				}
				array[this.length] = value;
				return new BasicList.Node(array, num);
			}

			// Token: 0x06009365 RID: 37733 RVA: 0x002C2F2C File Offset: 0x002C2F2C
			public BasicList.Node Trim()
			{
				if (this.length == 0 || this.length == this.data.Length)
				{
					return this;
				}
				object[] destinationArray = new object[this.length];
				Array.Copy(this.data, destinationArray, this.length);
				return new BasicList.Node(destinationArray, this.length);
			}

			// Token: 0x06009366 RID: 37734 RVA: 0x002C2F88 File Offset: 0x002C2F88
			internal int IndexOfString(string value)
			{
				for (int i = 0; i < this.length; i++)
				{
					if (value == (string)this.data[i])
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06009367 RID: 37735 RVA: 0x002C2FC8 File Offset: 0x002C2FC8
			internal int IndexOfReference(object instance)
			{
				for (int i = 0; i < this.length; i++)
				{
					if (instance == this.data[i])
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06009368 RID: 37736 RVA: 0x002C3000 File Offset: 0x002C3000
			internal int IndexOf(BasicList.MatchPredicate predicate, object ctx)
			{
				for (int i = 0; i < this.length; i++)
				{
					if (predicate(this.data[i], ctx))
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06009369 RID: 37737 RVA: 0x002C303C File Offset: 0x002C303C
			internal void CopyTo(Array array, int offset)
			{
				if (this.length > 0)
				{
					Array.Copy(this.data, 0, array, offset, this.length);
				}
			}

			// Token: 0x0600936A RID: 37738 RVA: 0x002C3060 File Offset: 0x002C3060
			internal void Clear()
			{
				if (this.data != null)
				{
					Array.Clear(this.data, 0, this.data.Length);
				}
				this.length = 0;
			}

			// Token: 0x04004B4D RID: 19277
			private readonly object[] data;

			// Token: 0x04004B4E RID: 19278
			private int length;
		}

		// Token: 0x02001177 RID: 4471
		// (Invoke) Token: 0x0600936C RID: 37740
		internal delegate bool MatchPredicate(object value, object ctx);

		// Token: 0x02001178 RID: 4472
		internal sealed class Group
		{
			// Token: 0x0600936F RID: 37743 RVA: 0x002C3088 File Offset: 0x002C3088
			public Group(int first)
			{
				this.First = first;
				this.Items = new BasicList();
			}

			// Token: 0x04004B4F RID: 19279
			public readonly int First;

			// Token: 0x04004B50 RID: 19280
			public readonly BasicList Items;
		}
	}
}
