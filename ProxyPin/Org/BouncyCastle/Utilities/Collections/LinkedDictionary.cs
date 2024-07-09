using System;
using System.Collections;

namespace Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x020006CC RID: 1740
	public class LinkedDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06003CD9 RID: 15577 RVA: 0x0014F7A4 File Offset: 0x0014F7A4
		public virtual void Add(object k, object v)
		{
			this.hash.Add(k, v);
			this.keys.Add(k);
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x0014F7C0 File Offset: 0x0014F7C0
		public virtual void Clear()
		{
			this.hash.Clear();
			this.keys.Clear();
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x0014F7D8 File Offset: 0x0014F7D8
		public virtual bool Contains(object k)
		{
			return this.hash.Contains(k);
		}

		// Token: 0x06003CDC RID: 15580 RVA: 0x0014F7E8 File Offset: 0x0014F7E8
		public virtual void CopyTo(Array array, int index)
		{
			foreach (object key in this.keys)
			{
				array.SetValue(this.hash[key], index++);
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06003CDD RID: 15581 RVA: 0x0014F858 File Offset: 0x0014F858
		public virtual int Count
		{
			get
			{
				return this.hash.Count;
			}
		}

		// Token: 0x06003CDE RID: 15582 RVA: 0x0014F868 File Offset: 0x0014F868
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x0014F870 File Offset: 0x0014F870
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new LinkedDictionaryEnumerator(this);
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x0014F878 File Offset: 0x0014F878
		public virtual void Remove(object k)
		{
			this.hash.Remove(k);
			this.keys.Remove(k);
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06003CE1 RID: 15585 RVA: 0x0014F894 File Offset: 0x0014F894
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06003CE2 RID: 15586 RVA: 0x0014F898 File Offset: 0x0014F898
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06003CE3 RID: 15587 RVA: 0x0014F89C File Offset: 0x0014F89C
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06003CE4 RID: 15588 RVA: 0x0014F8A0 File Offset: 0x0014F8A0
		public virtual object SyncRoot
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06003CE5 RID: 15589 RVA: 0x0014F8A8 File Offset: 0x0014F8A8
		public virtual ICollection Keys
		{
			get
			{
				return Platform.CreateArrayList(this.keys);
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06003CE6 RID: 15590 RVA: 0x0014F8B8 File Offset: 0x0014F8B8
		public virtual ICollection Values
		{
			get
			{
				IList list = Platform.CreateArrayList(this.keys.Count);
				foreach (object key in this.keys)
				{
					list.Add(this.hash[key]);
				}
				return list;
			}
		}

		// Token: 0x17000A57 RID: 2647
		public virtual object this[object k]
		{
			get
			{
				return this.hash[k];
			}
			set
			{
				if (!this.hash.Contains(k))
				{
					this.keys.Add(k);
				}
				this.hash[k] = value;
			}
		}

		// Token: 0x04001EE3 RID: 7907
		internal readonly IDictionary hash = Platform.CreateHashtable();

		// Token: 0x04001EE4 RID: 7908
		internal readonly IList keys = Platform.CreateArrayList();
	}
}
