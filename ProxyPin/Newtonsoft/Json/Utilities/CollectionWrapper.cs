using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000AA4 RID: 2724
	[NullableContext(1)]
	[Nullable(0)]
	internal class CollectionWrapper<[Nullable(2)] T> : ICollection<T>, IEnumerable<!0>, IEnumerable, IWrappedCollection, IList, ICollection
	{
		// Token: 0x06006C6F RID: 27759 RVA: 0x0020B7F4 File Offset: 0x0020B7F4
		public CollectionWrapper(IList list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			ICollection<T> collection = list as ICollection<T>;
			if (collection != null)
			{
				this._genericCollection = collection;
				return;
			}
			this._list = list;
		}

		// Token: 0x06006C70 RID: 27760 RVA: 0x0020B834 File Offset: 0x0020B834
		public CollectionWrapper(ICollection<T> list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			this._genericCollection = list;
		}

		// Token: 0x06006C71 RID: 27761 RVA: 0x0020B850 File Offset: 0x0020B850
		public virtual void Add(T item)
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.Add(item);
				return;
			}
			this._list.Add(item);
		}

		// Token: 0x06006C72 RID: 27762 RVA: 0x0020B87C File Offset: 0x0020B87C
		public virtual void Clear()
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.Clear();
				return;
			}
			this._list.Clear();
		}

		// Token: 0x06006C73 RID: 27763 RVA: 0x0020B8A0 File Offset: 0x0020B8A0
		public virtual bool Contains(T item)
		{
			if (this._genericCollection != null)
			{
				return this._genericCollection.Contains(item);
			}
			return this._list.Contains(item);
		}

		// Token: 0x06006C74 RID: 27764 RVA: 0x0020B8CC File Offset: 0x0020B8CC
		public virtual void CopyTo(T[] array, int arrayIndex)
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.CopyTo(array, arrayIndex);
				return;
			}
			this._list.CopyTo(array, arrayIndex);
		}

		// Token: 0x1700169D RID: 5789
		// (get) Token: 0x06006C75 RID: 27765 RVA: 0x0020B8F4 File Offset: 0x0020B8F4
		public virtual int Count
		{
			get
			{
				if (this._genericCollection != null)
				{
					return this._genericCollection.Count;
				}
				return this._list.Count;
			}
		}

		// Token: 0x1700169E RID: 5790
		// (get) Token: 0x06006C76 RID: 27766 RVA: 0x0020B918 File Offset: 0x0020B918
		public virtual bool IsReadOnly
		{
			get
			{
				if (this._genericCollection != null)
				{
					return this._genericCollection.IsReadOnly;
				}
				return this._list.IsReadOnly;
			}
		}

		// Token: 0x06006C77 RID: 27767 RVA: 0x0020B93C File Offset: 0x0020B93C
		public virtual bool Remove(T item)
		{
			if (this._genericCollection != null)
			{
				return this._genericCollection.Remove(item);
			}
			bool flag = this._list.Contains(item);
			if (flag)
			{
				this._list.Remove(item);
			}
			return flag;
		}

		// Token: 0x06006C78 RID: 27768 RVA: 0x0020B990 File Offset: 0x0020B990
		public virtual IEnumerator<T> GetEnumerator()
		{
			IEnumerable<T> genericCollection = this._genericCollection;
			return (genericCollection ?? this._list.Cast<T>()).GetEnumerator();
		}

		// Token: 0x06006C79 RID: 27769 RVA: 0x0020B9C0 File Offset: 0x0020B9C0
		IEnumerator IEnumerable.GetEnumerator()
		{
			IEnumerable genericCollection = this._genericCollection;
			return (genericCollection ?? this._list).GetEnumerator();
		}

		// Token: 0x06006C7A RID: 27770 RVA: 0x0020B9EC File Offset: 0x0020B9EC
		int IList.Add(object value)
		{
			CollectionWrapper<T>.VerifyValueType(value);
			this.Add((T)((object)value));
			return this.Count - 1;
		}

		// Token: 0x06006C7B RID: 27771 RVA: 0x0020BA08 File Offset: 0x0020BA08
		bool IList.Contains(object value)
		{
			return CollectionWrapper<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		// Token: 0x06006C7C RID: 27772 RVA: 0x0020BA24 File Offset: 0x0020BA24
		int IList.IndexOf(object value)
		{
			if (this._genericCollection != null)
			{
				throw new InvalidOperationException("Wrapped ICollection<T> does not support IndexOf.");
			}
			if (CollectionWrapper<T>.IsCompatibleObject(value))
			{
				return this._list.IndexOf((T)((object)value));
			}
			return -1;
		}

		// Token: 0x06006C7D RID: 27773 RVA: 0x0020BA60 File Offset: 0x0020BA60
		void IList.RemoveAt(int index)
		{
			if (this._genericCollection != null)
			{
				throw new InvalidOperationException("Wrapped ICollection<T> does not support RemoveAt.");
			}
			this._list.RemoveAt(index);
		}

		// Token: 0x06006C7E RID: 27774 RVA: 0x0020BA84 File Offset: 0x0020BA84
		void IList.Insert(int index, object value)
		{
			if (this._genericCollection != null)
			{
				throw new InvalidOperationException("Wrapped ICollection<T> does not support Insert.");
			}
			CollectionWrapper<T>.VerifyValueType(value);
			this._list.Insert(index, (T)((object)value));
		}

		// Token: 0x1700169F RID: 5791
		// (get) Token: 0x06006C7F RID: 27775 RVA: 0x0020BABC File Offset: 0x0020BABC
		bool IList.IsFixedSize
		{
			get
			{
				if (this._genericCollection != null)
				{
					return this._genericCollection.IsReadOnly;
				}
				return this._list.IsFixedSize;
			}
		}

		// Token: 0x06006C80 RID: 27776 RVA: 0x0020BAE0 File Offset: 0x0020BAE0
		void IList.Remove(object value)
		{
			if (CollectionWrapper<T>.IsCompatibleObject(value))
			{
				this.Remove((T)((object)value));
			}
		}

		// Token: 0x170016A0 RID: 5792
		object IList.this[int index]
		{
			get
			{
				if (this._genericCollection != null)
				{
					throw new InvalidOperationException("Wrapped ICollection<T> does not support indexer.");
				}
				return this._list[index];
			}
			set
			{
				if (this._genericCollection != null)
				{
					throw new InvalidOperationException("Wrapped ICollection<T> does not support indexer.");
				}
				CollectionWrapper<T>.VerifyValueType(value);
				this._list[index] = (T)((object)value);
			}
		}

		// Token: 0x06006C83 RID: 27779 RVA: 0x0020BB58 File Offset: 0x0020BB58
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			this.CopyTo((T[])array, arrayIndex);
		}

		// Token: 0x170016A1 RID: 5793
		// (get) Token: 0x06006C84 RID: 27780 RVA: 0x0020BB68 File Offset: 0x0020BB68
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170016A2 RID: 5794
		// (get) Token: 0x06006C85 RID: 27781 RVA: 0x0020BB6C File Offset: 0x0020BB6C
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x06006C86 RID: 27782 RVA: 0x0020BB94 File Offset: 0x0020BB94
		private static void VerifyValueType(object value)
		{
			if (!CollectionWrapper<T>.IsCompatibleObject(value))
			{
				throw new ArgumentException("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.".FormatWith(CultureInfo.InvariantCulture, value, typeof(T)), "value");
			}
		}

		// Token: 0x06006C87 RID: 27783 RVA: 0x0020BBC8 File Offset: 0x0020BBC8
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && (!typeof(T).IsValueType() || ReflectionUtils.IsNullableType(typeof(T))));
		}

		// Token: 0x170016A3 RID: 5795
		// (get) Token: 0x06006C88 RID: 27784 RVA: 0x0020BC08 File Offset: 0x0020BC08
		public object UnderlyingCollection
		{
			get
			{
				return this._genericCollection ?? this._list;
			}
		}

		// Token: 0x04003657 RID: 13911
		[Nullable(2)]
		private readonly IList _list;

		// Token: 0x04003658 RID: 13912
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private readonly ICollection<T> _genericCollection;

		// Token: 0x04003659 RID: 13913
		[Nullable(2)]
		private object _syncRoot;
	}
}
