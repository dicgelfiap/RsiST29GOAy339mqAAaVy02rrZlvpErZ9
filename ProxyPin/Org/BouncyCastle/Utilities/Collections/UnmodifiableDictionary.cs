using System;
using System.Collections;

namespace Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x020006CE RID: 1742
	public abstract class UnmodifiableDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06003CF2 RID: 15602 RVA: 0x0014FAA8 File Offset: 0x0014FAA8
		public virtual void Add(object k, object v)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003CF3 RID: 15603 RVA: 0x0014FAB0 File Offset: 0x0014FAB0
		public virtual void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003CF4 RID: 15604
		public abstract bool Contains(object k);

		// Token: 0x06003CF5 RID: 15605
		public abstract void CopyTo(Array array, int index);

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06003CF6 RID: 15606
		public abstract int Count { get; }

		// Token: 0x06003CF7 RID: 15607 RVA: 0x0014FAB8 File Offset: 0x0014FAB8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06003CF8 RID: 15608
		public abstract IDictionaryEnumerator GetEnumerator();

		// Token: 0x06003CF9 RID: 15609 RVA: 0x0014FAC0 File Offset: 0x0014FAC0
		public virtual void Remove(object k)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06003CFA RID: 15610
		public abstract bool IsFixedSize { get; }

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06003CFB RID: 15611 RVA: 0x0014FAC8 File Offset: 0x0014FAC8
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06003CFC RID: 15612
		public abstract bool IsSynchronized { get; }

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06003CFD RID: 15613
		public abstract object SyncRoot { get; }

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06003CFE RID: 15614
		public abstract ICollection Keys { get; }

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06003CFF RID: 15615
		public abstract ICollection Values { get; }

		// Token: 0x17000A64 RID: 2660
		public virtual object this[object k]
		{
			get
			{
				return this.GetValue(k);
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06003D02 RID: 15618
		protected abstract object GetValue(object k);
	}
}
