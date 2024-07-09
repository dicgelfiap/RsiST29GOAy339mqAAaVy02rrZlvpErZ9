using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using dnlib.Threading;

namespace dnlib.Utils
{
	// Token: 0x0200073F RID: 1855
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(CollectionDebugView<>))]
	[ComVisible(true)]
	public class LazyList<TValue> : ILazyList<TValue>, IList<TValue>, ICollection<TValue>, IEnumerable<TValue>, IEnumerable where TValue : class
	{
		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x060040EB RID: 16619 RVA: 0x0016208C File Offset: 0x0016208C
		public int Count
		{
			get
			{
				this.theLock.EnterReadLock();
				int count_NoLock;
				try
				{
					count_NoLock = this.Count_NoLock;
				}
				finally
				{
					this.theLock.ExitReadLock();
				}
				return count_NoLock;
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x060040EC RID: 16620 RVA: 0x001620D0 File Offset: 0x001620D0
		internal int Count_NoLock
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x060040ED RID: 16621 RVA: 0x001620E0 File Offset: 0x001620E0
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AF9 RID: 2809
		public TValue this[int index]
		{
			get
			{
				this.theLock.EnterWriteLock();
				TValue result;
				try
				{
					result = this.Get_NoLock(index);
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
				return result;
			}
			set
			{
				this.theLock.EnterWriteLock();
				try
				{
					this.Set_NoLock(index, value);
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x060040F0 RID: 16624 RVA: 0x0016216C File Offset: 0x0016216C
		internal TValue Get_NoLock(int index)
		{
			return this.list[index].GetValue_NoLock(index);
		}

		// Token: 0x060040F1 RID: 16625 RVA: 0x00162180 File Offset: 0x00162180
		private void Set_NoLock(int index, TValue value)
		{
			if (this.listener != null)
			{
				this.listener.OnRemove(index, this.list[index].GetValue_NoLock(index));
				this.listener.OnAdd(index, value);
			}
			this.list[index].SetValue_NoLock(index, value);
			this.id++;
		}

		// Token: 0x060040F2 RID: 16626 RVA: 0x001621E8 File Offset: 0x001621E8
		public LazyList() : this(null)
		{
		}

		// Token: 0x060040F3 RID: 16627 RVA: 0x001621F4 File Offset: 0x001621F4
		public LazyList(IListListener<TValue> listener)
		{
			this.theLock = Lock.Create();
			base..ctor();
			this.listener = listener;
			this.list = new List<LazyList<TValue>.Element>();
		}

		// Token: 0x060040F4 RID: 16628 RVA: 0x0016221C File Offset: 0x0016221C
		private protected LazyList(int length, IListListener<TValue> listener)
		{
			this.theLock = Lock.Create();
			base..ctor();
			this.listener = listener;
			this.list = new List<LazyList<TValue>.Element>(length);
		}

		// Token: 0x060040F5 RID: 16629 RVA: 0x00162244 File Offset: 0x00162244
		public int IndexOf(TValue item)
		{
			this.theLock.EnterWriteLock();
			int result;
			try
			{
				result = this.IndexOf_NoLock(item);
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
			return result;
		}

		// Token: 0x060040F6 RID: 16630 RVA: 0x00162288 File Offset: 0x00162288
		private int IndexOf_NoLock(TValue item)
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				if (this.list[i].GetValue_NoLock(i) == item)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060040F7 RID: 16631 RVA: 0x001622D8 File Offset: 0x001622D8
		public void Insert(int index, TValue item)
		{
			this.theLock.EnterWriteLock();
			try
			{
				this.Insert_NoLock(index, item);
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x060040F8 RID: 16632 RVA: 0x0016231C File Offset: 0x0016231C
		private void Insert_NoLock(int index, TValue item)
		{
			if (this.listener != null)
			{
				this.listener.OnAdd(index, item);
			}
			this.list.Insert(index, new LazyList<TValue>.Element(item));
			if (this.listener != null)
			{
				this.listener.OnResize(index);
			}
			this.id++;
		}

		// Token: 0x060040F9 RID: 16633 RVA: 0x0016237C File Offset: 0x0016237C
		public void RemoveAt(int index)
		{
			this.theLock.EnterWriteLock();
			try
			{
				this.RemoveAt_NoLock(index);
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x001623BC File Offset: 0x001623BC
		private void RemoveAt_NoLock(int index)
		{
			if (this.listener != null)
			{
				this.listener.OnRemove(index, this.list[index].GetValue_NoLock(index));
			}
			this.list.RemoveAt(index);
			if (this.listener != null)
			{
				this.listener.OnResize(index);
			}
			this.id++;
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x00162428 File Offset: 0x00162428
		public void Add(TValue item)
		{
			this.theLock.EnterWriteLock();
			try
			{
				this.Add_NoLock(item);
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x00162468 File Offset: 0x00162468
		private void Add_NoLock(TValue item)
		{
			int count = this.list.Count;
			if (this.listener != null)
			{
				this.listener.OnAdd(count, item);
			}
			this.list.Add(new LazyList<TValue>.Element(item));
			if (this.listener != null)
			{
				this.listener.OnResize(count);
			}
			this.id++;
		}

		// Token: 0x060040FD RID: 16637 RVA: 0x001624D4 File Offset: 0x001624D4
		public void Clear()
		{
			this.theLock.EnterWriteLock();
			try
			{
				this.Clear_NoLock();
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x060040FE RID: 16638 RVA: 0x00162514 File Offset: 0x00162514
		private void Clear_NoLock()
		{
			if (this.listener != null)
			{
				this.listener.OnClear();
			}
			this.list.Clear();
			if (this.listener != null)
			{
				this.listener.OnResize(0);
			}
			this.id++;
		}

		// Token: 0x060040FF RID: 16639 RVA: 0x0016256C File Offset: 0x0016256C
		public bool Contains(TValue item)
		{
			return this.IndexOf(item) >= 0;
		}

		// Token: 0x06004100 RID: 16640 RVA: 0x0016257C File Offset: 0x0016257C
		public void CopyTo(TValue[] array, int arrayIndex)
		{
			this.theLock.EnterWriteLock();
			try
			{
				this.CopyTo_NoLock(array, arrayIndex);
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x06004101 RID: 16641 RVA: 0x001625C0 File Offset: 0x001625C0
		private void CopyTo_NoLock(TValue[] array, int arrayIndex)
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				array[arrayIndex + i] = this.list[i].GetValue_NoLock(i);
			}
		}

		// Token: 0x06004102 RID: 16642 RVA: 0x00162608 File Offset: 0x00162608
		public bool Remove(TValue item)
		{
			this.theLock.EnterWriteLock();
			bool result;
			try
			{
				result = this.Remove_NoLock(item);
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
			return result;
		}

		// Token: 0x06004103 RID: 16643 RVA: 0x0016264C File Offset: 0x0016264C
		private bool Remove_NoLock(TValue item)
		{
			int num = this.IndexOf_NoLock(item);
			if (num < 0)
			{
				return false;
			}
			this.RemoveAt_NoLock(num);
			return true;
		}

		// Token: 0x06004104 RID: 16644 RVA: 0x00162678 File Offset: 0x00162678
		internal bool IsInitialized(int index)
		{
			this.theLock.EnterReadLock();
			bool result;
			try
			{
				result = this.IsInitialized_NoLock(index);
			}
			finally
			{
				this.theLock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x06004105 RID: 16645 RVA: 0x001626BC File Offset: 0x001626BC
		private bool IsInitialized_NoLock(int index)
		{
			return index < this.list.Count && this.list[index].IsInitialized_NoLock;
		}

		// Token: 0x06004106 RID: 16646 RVA: 0x001626E4 File Offset: 0x001626E4
		public LazyList<TValue>.Enumerator GetEnumerator()
		{
			return new LazyList<TValue>.Enumerator(this);
		}

		// Token: 0x06004107 RID: 16647 RVA: 0x001626EC File Offset: 0x001626EC
		IEnumerator<TValue> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06004108 RID: 16648 RVA: 0x001626FC File Offset: 0x001626FC
		internal IEnumerable<TValue> GetEnumerable_NoLock()
		{
			int id2 = this.id;
			int num;
			for (int i = 0; i < this.list.Count; i = num + 1)
			{
				if (this.id != id2)
				{
					throw new InvalidOperationException("List was modified");
				}
				yield return this.list[i].GetValue_NoLock(i);
				num = i;
			}
			yield break;
		}

		// Token: 0x06004109 RID: 16649 RVA: 0x0016270C File Offset: 0x0016270C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04002289 RID: 8841
		private protected readonly List<LazyList<TValue>.Element> list;

		// Token: 0x0400228A RID: 8842
		private int id;

		// Token: 0x0400228B RID: 8843
		private protected readonly IListListener<TValue> listener;

		// Token: 0x0400228C RID: 8844
		private readonly Lock theLock;

		// Token: 0x02000FBE RID: 4030
		internal class Element
		{
			// Token: 0x17001DB0 RID: 7600
			// (get) Token: 0x06008D7E RID: 36222 RVA: 0x002A6B34 File Offset: 0x002A6B34
			public virtual bool IsInitialized_NoLock
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06008D7F RID: 36223 RVA: 0x002A6B38 File Offset: 0x002A6B38
			protected Element()
			{
			}

			// Token: 0x06008D80 RID: 36224 RVA: 0x002A6B40 File Offset: 0x002A6B40
			public Element(TValue data)
			{
				this.value = data;
			}

			// Token: 0x06008D81 RID: 36225 RVA: 0x002A6B50 File Offset: 0x002A6B50
			public virtual TValue GetValue_NoLock(int index)
			{
				return this.value;
			}

			// Token: 0x06008D82 RID: 36226 RVA: 0x002A6B58 File Offset: 0x002A6B58
			public virtual void SetValue_NoLock(int index, TValue value)
			{
				this.value = value;
			}

			// Token: 0x06008D83 RID: 36227 RVA: 0x002A6B64 File Offset: 0x002A6B64
			public override string ToString()
			{
				TValue tvalue = this.value;
				return ((tvalue != null) ? tvalue.ToString() : null) ?? string.Empty;
			}

			// Token: 0x040042F1 RID: 17137
			protected TValue value;
		}

		// Token: 0x02000FBF RID: 4031
		public struct Enumerator : IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06008D84 RID: 36228 RVA: 0x002A6B90 File Offset: 0x002A6B90
			internal Enumerator(LazyList<TValue> list)
			{
				this.list = list;
				this.index = 0;
				this.current = default(TValue);
				list.theLock.EnterReadLock();
				try
				{
					this.id = list.id;
				}
				finally
				{
					list.theLock.ExitReadLock();
				}
			}

			// Token: 0x17001DB1 RID: 7601
			// (get) Token: 0x06008D85 RID: 36229 RVA: 0x002A6BF0 File Offset: 0x002A6BF0
			public TValue Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x17001DB2 RID: 7602
			// (get) Token: 0x06008D86 RID: 36230 RVA: 0x002A6BF8 File Offset: 0x002A6BF8
			object IEnumerator.Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x06008D87 RID: 36231 RVA: 0x002A6C08 File Offset: 0x002A6C08
			public bool MoveNext()
			{
				this.list.theLock.EnterWriteLock();
				bool result;
				try
				{
					if (this.list.id == this.id && this.index < this.list.Count_NoLock)
					{
						this.current = this.list.list[this.index].GetValue_NoLock(this.index);
						this.index++;
						result = true;
					}
					else
					{
						result = this.MoveNextDoneOrThrow_NoLock();
					}
				}
				finally
				{
					this.list.theLock.ExitWriteLock();
				}
				return result;
			}

			// Token: 0x06008D88 RID: 36232 RVA: 0x002A6CBC File Offset: 0x002A6CBC
			private bool MoveNextDoneOrThrow_NoLock()
			{
				if (this.list.id != this.id)
				{
					throw new InvalidOperationException("List was modified");
				}
				this.current = default(TValue);
				return false;
			}

			// Token: 0x06008D89 RID: 36233 RVA: 0x002A6CEC File Offset: 0x002A6CEC
			public void Dispose()
			{
			}

			// Token: 0x06008D8A RID: 36234 RVA: 0x002A6CF0 File Offset: 0x002A6CF0
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x040042F2 RID: 17138
			private readonly LazyList<TValue> list;

			// Token: 0x040042F3 RID: 17139
			private readonly int id;

			// Token: 0x040042F4 RID: 17140
			private int index;

			// Token: 0x040042F5 RID: 17141
			private TValue current;
		}
	}
}
