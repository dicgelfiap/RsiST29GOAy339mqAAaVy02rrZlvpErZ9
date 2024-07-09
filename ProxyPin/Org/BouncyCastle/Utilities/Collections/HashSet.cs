using System;
using System.Collections;

namespace Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x020006CB RID: 1739
	public class HashSet : ISet, ICollection, IEnumerable
	{
		// Token: 0x06003CC8 RID: 15560 RVA: 0x0014F58C File Offset: 0x0014F58C
		public HashSet()
		{
		}

		// Token: 0x06003CC9 RID: 15561 RVA: 0x0014F5A0 File Offset: 0x0014F5A0
		public HashSet(IEnumerable s)
		{
			foreach (object o in s)
			{
				this.Add(o);
			}
		}

		// Token: 0x06003CCA RID: 15562 RVA: 0x0014F60C File Offset: 0x0014F60C
		public virtual void Add(object o)
		{
			this.impl[o] = null;
		}

		// Token: 0x06003CCB RID: 15563 RVA: 0x0014F61C File Offset: 0x0014F61C
		public virtual void AddAll(IEnumerable e)
		{
			foreach (object o in e)
			{
				this.Add(o);
			}
		}

		// Token: 0x06003CCC RID: 15564 RVA: 0x0014F674 File Offset: 0x0014F674
		public virtual void Clear()
		{
			this.impl.Clear();
		}

		// Token: 0x06003CCD RID: 15565 RVA: 0x0014F684 File Offset: 0x0014F684
		public virtual bool Contains(object o)
		{
			return this.impl.Contains(o);
		}

		// Token: 0x06003CCE RID: 15566 RVA: 0x0014F694 File Offset: 0x0014F694
		public virtual void CopyTo(Array array, int index)
		{
			this.impl.Keys.CopyTo(array, index);
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06003CCF RID: 15567 RVA: 0x0014F6A8 File Offset: 0x0014F6A8
		public virtual int Count
		{
			get
			{
				return this.impl.Count;
			}
		}

		// Token: 0x06003CD0 RID: 15568 RVA: 0x0014F6B8 File Offset: 0x0014F6B8
		public virtual IEnumerator GetEnumerator()
		{
			return this.impl.Keys.GetEnumerator();
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06003CD1 RID: 15569 RVA: 0x0014F6CC File Offset: 0x0014F6CC
		public virtual bool IsEmpty
		{
			get
			{
				return this.impl.Count == 0;
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06003CD2 RID: 15570 RVA: 0x0014F6DC File Offset: 0x0014F6DC
		public virtual bool IsFixedSize
		{
			get
			{
				return this.impl.IsFixedSize;
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06003CD3 RID: 15571 RVA: 0x0014F6EC File Offset: 0x0014F6EC
		public virtual bool IsReadOnly
		{
			get
			{
				return this.impl.IsReadOnly;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06003CD4 RID: 15572 RVA: 0x0014F6FC File Offset: 0x0014F6FC
		public virtual bool IsSynchronized
		{
			get
			{
				return this.impl.IsSynchronized;
			}
		}

		// Token: 0x06003CD5 RID: 15573 RVA: 0x0014F70C File Offset: 0x0014F70C
		public virtual void Remove(object o)
		{
			this.impl.Remove(o);
		}

		// Token: 0x06003CD6 RID: 15574 RVA: 0x0014F71C File Offset: 0x0014F71C
		public virtual void RemoveAll(IEnumerable e)
		{
			foreach (object o in e)
			{
				this.Remove(o);
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06003CD7 RID: 15575 RVA: 0x0014F774 File Offset: 0x0014F774
		public virtual object SyncRoot
		{
			get
			{
				return this.impl.SyncRoot;
			}
		}

		// Token: 0x04001EE2 RID: 7906
		private readonly IDictionary impl = Platform.CreateHashtable();
	}
}
