using System;
using System.Collections;

namespace Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x020006D3 RID: 1747
	public class UnmodifiableSetProxy : UnmodifiableSet
	{
		// Token: 0x06003D39 RID: 15673 RVA: 0x0014FCAC File Offset: 0x0014FCAC
		public UnmodifiableSetProxy(ISet s)
		{
			this.s = s;
		}

		// Token: 0x06003D3A RID: 15674 RVA: 0x0014FCBC File Offset: 0x0014FCBC
		public override bool Contains(object o)
		{
			return this.s.Contains(o);
		}

		// Token: 0x06003D3B RID: 15675 RVA: 0x0014FCCC File Offset: 0x0014FCCC
		public override void CopyTo(Array array, int index)
		{
			this.s.CopyTo(array, index);
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06003D3C RID: 15676 RVA: 0x0014FCDC File Offset: 0x0014FCDC
		public override int Count
		{
			get
			{
				return this.s.Count;
			}
		}

		// Token: 0x06003D3D RID: 15677 RVA: 0x0014FCEC File Offset: 0x0014FCEC
		public override IEnumerator GetEnumerator()
		{
			return this.s.GetEnumerator();
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06003D3E RID: 15678 RVA: 0x0014FCFC File Offset: 0x0014FCFC
		public override bool IsEmpty
		{
			get
			{
				return this.s.IsEmpty;
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06003D3F RID: 15679 RVA: 0x0014FD0C File Offset: 0x0014FD0C
		public override bool IsFixedSize
		{
			get
			{
				return this.s.IsFixedSize;
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06003D40 RID: 15680 RVA: 0x0014FD1C File Offset: 0x0014FD1C
		public override bool IsSynchronized
		{
			get
			{
				return this.s.IsSynchronized;
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06003D41 RID: 15681 RVA: 0x0014FD2C File Offset: 0x0014FD2C
		public override object SyncRoot
		{
			get
			{
				return this.s.SyncRoot;
			}
		}

		// Token: 0x04001EE9 RID: 7913
		private readonly ISet s;
	}
}
