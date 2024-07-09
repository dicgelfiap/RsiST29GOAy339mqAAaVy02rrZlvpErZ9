using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using dnlib.Utils;

namespace dnlib.DotNet.Emit
{
	// Token: 0x020009E2 RID: 2530
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(LocalList_CollectionDebugView))]
	[ComVisible(true)]
	public sealed class LocalList : IListListener<Local>, IList<Local>, ICollection<Local>, IEnumerable<Local>, IEnumerable
	{
		// Token: 0x17001462 RID: 5218
		// (get) Token: 0x060060F7 RID: 24823 RVA: 0x001CE980 File Offset: 0x001CE980
		public int Count
		{
			get
			{
				return this.locals.Count;
			}
		}

		// Token: 0x17001463 RID: 5219
		// (get) Token: 0x060060F8 RID: 24824 RVA: 0x001CE990 File Offset: 0x001CE990
		public IList<Local> Locals
		{
			get
			{
				return this.locals;
			}
		}

		// Token: 0x17001464 RID: 5220
		public Local this[int index]
		{
			get
			{
				return this.locals[index];
			}
			set
			{
				this.locals[index] = value;
			}
		}

		// Token: 0x060060FB RID: 24827 RVA: 0x001CE9B8 File Offset: 0x001CE9B8
		public LocalList()
		{
			this.locals = new LazyList<Local>(this);
		}

		// Token: 0x060060FC RID: 24828 RVA: 0x001CE9CC File Offset: 0x001CE9CC
		public LocalList(IList<Local> locals)
		{
			this.locals = new LazyList<Local>(this);
			for (int i = 0; i < locals.Count; i++)
			{
				this.locals.Add(locals[i]);
			}
		}

		// Token: 0x060060FD RID: 24829 RVA: 0x001CEA18 File Offset: 0x001CEA18
		public Local Add(Local local)
		{
			this.locals.Add(local);
			return local;
		}

		// Token: 0x060060FE RID: 24830 RVA: 0x001CEA28 File Offset: 0x001CEA28
		void IListListener<Local>.OnLazyAdd(int index, ref Local value)
		{
		}

		// Token: 0x060060FF RID: 24831 RVA: 0x001CEA2C File Offset: 0x001CEA2C
		void IListListener<Local>.OnAdd(int index, Local value)
		{
			value.Index = index;
		}

		// Token: 0x06006100 RID: 24832 RVA: 0x001CEA38 File Offset: 0x001CEA38
		void IListListener<Local>.OnRemove(int index, Local value)
		{
			value.Index = -1;
		}

		// Token: 0x06006101 RID: 24833 RVA: 0x001CEA44 File Offset: 0x001CEA44
		void IListListener<Local>.OnResize(int index)
		{
			for (int i = index; i < this.locals.Count_NoLock; i++)
			{
				this.locals.Get_NoLock(i).Index = i;
			}
		}

		// Token: 0x06006102 RID: 24834 RVA: 0x001CEA80 File Offset: 0x001CEA80
		void IListListener<Local>.OnClear()
		{
			foreach (Local local in this.locals.GetEnumerable_NoLock())
			{
				local.Index = -1;
			}
		}

		// Token: 0x06006103 RID: 24835 RVA: 0x001CEADC File Offset: 0x001CEADC
		public int IndexOf(Local item)
		{
			return this.locals.IndexOf(item);
		}

		// Token: 0x06006104 RID: 24836 RVA: 0x001CEAEC File Offset: 0x001CEAEC
		public void Insert(int index, Local item)
		{
			this.locals.Insert(index, item);
		}

		// Token: 0x06006105 RID: 24837 RVA: 0x001CEAFC File Offset: 0x001CEAFC
		public void RemoveAt(int index)
		{
			this.locals.RemoveAt(index);
		}

		// Token: 0x06006106 RID: 24838 RVA: 0x001CEB0C File Offset: 0x001CEB0C
		void ICollection<Local>.Add(Local item)
		{
			this.locals.Add(item);
		}

		// Token: 0x06006107 RID: 24839 RVA: 0x001CEB1C File Offset: 0x001CEB1C
		public void Clear()
		{
			this.locals.Clear();
		}

		// Token: 0x06006108 RID: 24840 RVA: 0x001CEB2C File Offset: 0x001CEB2C
		public bool Contains(Local item)
		{
			return this.locals.Contains(item);
		}

		// Token: 0x06006109 RID: 24841 RVA: 0x001CEB3C File Offset: 0x001CEB3C
		public void CopyTo(Local[] array, int arrayIndex)
		{
			this.locals.CopyTo(array, arrayIndex);
		}

		// Token: 0x17001465 RID: 5221
		// (get) Token: 0x0600610A RID: 24842 RVA: 0x001CEB4C File Offset: 0x001CEB4C
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600610B RID: 24843 RVA: 0x001CEB50 File Offset: 0x001CEB50
		public bool Remove(Local item)
		{
			return this.locals.Remove(item);
		}

		// Token: 0x0600610C RID: 24844 RVA: 0x001CEB60 File Offset: 0x001CEB60
		public LazyList<Local>.Enumerator GetEnumerator()
		{
			return this.locals.GetEnumerator();
		}

		// Token: 0x0600610D RID: 24845 RVA: 0x001CEB70 File Offset: 0x001CEB70
		IEnumerator<Local> IEnumerable<Local>.GetEnumerator()
		{
			return this.locals.GetEnumerator();
		}

		// Token: 0x0600610E RID: 24846 RVA: 0x001CEB84 File Offset: 0x001CEB84
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<Local>)this).GetEnumerator();
		}

		// Token: 0x040030A0 RID: 12448
		private readonly LazyList<Local> locals;
	}
}
