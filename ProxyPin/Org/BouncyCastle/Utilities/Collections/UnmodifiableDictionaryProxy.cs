using System;
using System.Collections;

namespace Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x020006CF RID: 1743
	public class UnmodifiableDictionaryProxy : UnmodifiableDictionary
	{
		// Token: 0x06003D03 RID: 15619 RVA: 0x0014FAE0 File Offset: 0x0014FAE0
		public UnmodifiableDictionaryProxy(IDictionary d)
		{
			this.d = d;
		}

		// Token: 0x06003D04 RID: 15620 RVA: 0x0014FAF0 File Offset: 0x0014FAF0
		public override bool Contains(object k)
		{
			return this.d.Contains(k);
		}

		// Token: 0x06003D05 RID: 15621 RVA: 0x0014FB00 File Offset: 0x0014FB00
		public override void CopyTo(Array array, int index)
		{
			this.d.CopyTo(array, index);
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06003D06 RID: 15622 RVA: 0x0014FB10 File Offset: 0x0014FB10
		public override int Count
		{
			get
			{
				return this.d.Count;
			}
		}

		// Token: 0x06003D07 RID: 15623 RVA: 0x0014FB20 File Offset: 0x0014FB20
		public override IDictionaryEnumerator GetEnumerator()
		{
			return this.d.GetEnumerator();
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06003D08 RID: 15624 RVA: 0x0014FB30 File Offset: 0x0014FB30
		public override bool IsFixedSize
		{
			get
			{
				return this.d.IsFixedSize;
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06003D09 RID: 15625 RVA: 0x0014FB40 File Offset: 0x0014FB40
		public override bool IsSynchronized
		{
			get
			{
				return this.d.IsSynchronized;
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06003D0A RID: 15626 RVA: 0x0014FB50 File Offset: 0x0014FB50
		public override object SyncRoot
		{
			get
			{
				return this.d.SyncRoot;
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06003D0B RID: 15627 RVA: 0x0014FB60 File Offset: 0x0014FB60
		public override ICollection Keys
		{
			get
			{
				return this.d.Keys;
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06003D0C RID: 15628 RVA: 0x0014FB70 File Offset: 0x0014FB70
		public override ICollection Values
		{
			get
			{
				return this.d.Values;
			}
		}

		// Token: 0x06003D0D RID: 15629 RVA: 0x0014FB80 File Offset: 0x0014FB80
		protected override object GetValue(object k)
		{
			return this.d[k];
		}

		// Token: 0x04001EE7 RID: 7911
		private readonly IDictionary d;
	}
}
