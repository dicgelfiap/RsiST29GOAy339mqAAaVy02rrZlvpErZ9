using System;
using System.Collections;

namespace Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x020006D0 RID: 1744
	public abstract class UnmodifiableList : IList, ICollection, IEnumerable
	{
		// Token: 0x06003D0F RID: 15631 RVA: 0x0014FB98 File Offset: 0x0014FB98
		public virtual int Add(object o)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003D10 RID: 15632 RVA: 0x0014FBA0 File Offset: 0x0014FBA0
		public virtual void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003D11 RID: 15633
		public abstract bool Contains(object o);

		// Token: 0x06003D12 RID: 15634
		public abstract void CopyTo(Array array, int index);

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06003D13 RID: 15635
		public abstract int Count { get; }

		// Token: 0x06003D14 RID: 15636
		public abstract IEnumerator GetEnumerator();

		// Token: 0x06003D15 RID: 15637
		public abstract int IndexOf(object o);

		// Token: 0x06003D16 RID: 15638 RVA: 0x0014FBA8 File Offset: 0x0014FBA8
		public virtual void Insert(int i, object o)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06003D17 RID: 15639
		public abstract bool IsFixedSize { get; }

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06003D18 RID: 15640 RVA: 0x0014FBB0 File Offset: 0x0014FBB0
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06003D19 RID: 15641
		public abstract bool IsSynchronized { get; }

		// Token: 0x06003D1A RID: 15642 RVA: 0x0014FBB4 File Offset: 0x0014FBB4
		public virtual void Remove(object o)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x0014FBBC File Offset: 0x0014FBBC
		public virtual void RemoveAt(int i)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06003D1C RID: 15644
		public abstract object SyncRoot { get; }

		// Token: 0x17000A70 RID: 2672
		public virtual object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06003D1F RID: 15647
		protected abstract object GetValue(int i);
	}
}
