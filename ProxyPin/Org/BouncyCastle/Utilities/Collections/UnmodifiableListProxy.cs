using System;
using System.Collections;

namespace Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x020006D1 RID: 1745
	public class UnmodifiableListProxy : UnmodifiableList
	{
		// Token: 0x06003D20 RID: 15648 RVA: 0x0014FBD8 File Offset: 0x0014FBD8
		public UnmodifiableListProxy(IList l)
		{
			this.l = l;
		}

		// Token: 0x06003D21 RID: 15649 RVA: 0x0014FBE8 File Offset: 0x0014FBE8
		public override bool Contains(object o)
		{
			return this.l.Contains(o);
		}

		// Token: 0x06003D22 RID: 15650 RVA: 0x0014FBF8 File Offset: 0x0014FBF8
		public override void CopyTo(Array array, int index)
		{
			this.l.CopyTo(array, index);
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06003D23 RID: 15651 RVA: 0x0014FC08 File Offset: 0x0014FC08
		public override int Count
		{
			get
			{
				return this.l.Count;
			}
		}

		// Token: 0x06003D24 RID: 15652 RVA: 0x0014FC18 File Offset: 0x0014FC18
		public override IEnumerator GetEnumerator()
		{
			return this.l.GetEnumerator();
		}

		// Token: 0x06003D25 RID: 15653 RVA: 0x0014FC28 File Offset: 0x0014FC28
		public override int IndexOf(object o)
		{
			return this.l.IndexOf(o);
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06003D26 RID: 15654 RVA: 0x0014FC38 File Offset: 0x0014FC38
		public override bool IsFixedSize
		{
			get
			{
				return this.l.IsFixedSize;
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06003D27 RID: 15655 RVA: 0x0014FC48 File Offset: 0x0014FC48
		public override bool IsSynchronized
		{
			get
			{
				return this.l.IsSynchronized;
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06003D28 RID: 15656 RVA: 0x0014FC58 File Offset: 0x0014FC58
		public override object SyncRoot
		{
			get
			{
				return this.l.SyncRoot;
			}
		}

		// Token: 0x06003D29 RID: 15657 RVA: 0x0014FC68 File Offset: 0x0014FC68
		protected override object GetValue(int i)
		{
			return this.l[i];
		}

		// Token: 0x04001EE8 RID: 7912
		private readonly IList l;
	}
}
