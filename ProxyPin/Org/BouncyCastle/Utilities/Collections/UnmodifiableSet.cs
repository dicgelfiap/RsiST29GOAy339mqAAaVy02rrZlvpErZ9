using System;
using System.Collections;

namespace Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x020006D2 RID: 1746
	public abstract class UnmodifiableSet : ISet, ICollection, IEnumerable
	{
		// Token: 0x06003D2B RID: 15659 RVA: 0x0014FC80 File Offset: 0x0014FC80
		public virtual void Add(object o)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003D2C RID: 15660 RVA: 0x0014FC88 File Offset: 0x0014FC88
		public virtual void AddAll(IEnumerable e)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003D2D RID: 15661 RVA: 0x0014FC90 File Offset: 0x0014FC90
		public virtual void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003D2E RID: 15662
		public abstract bool Contains(object o);

		// Token: 0x06003D2F RID: 15663
		public abstract void CopyTo(Array array, int index);

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06003D30 RID: 15664
		public abstract int Count { get; }

		// Token: 0x06003D31 RID: 15665
		public abstract IEnumerator GetEnumerator();

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06003D32 RID: 15666
		public abstract bool IsEmpty { get; }

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06003D33 RID: 15667
		public abstract bool IsFixedSize { get; }

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06003D34 RID: 15668 RVA: 0x0014FC98 File Offset: 0x0014FC98
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06003D35 RID: 15669
		public abstract bool IsSynchronized { get; }

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06003D36 RID: 15670
		public abstract object SyncRoot { get; }

		// Token: 0x06003D37 RID: 15671 RVA: 0x0014FC9C File Offset: 0x0014FC9C
		public virtual void Remove(object o)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003D38 RID: 15672 RVA: 0x0014FCA4 File Offset: 0x0014FCA4
		public virtual void RemoveAll(IEnumerable e)
		{
			throw new NotSupportedException();
		}
	}
}
