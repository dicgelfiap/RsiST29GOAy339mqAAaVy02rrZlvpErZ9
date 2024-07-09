using System;

namespace Org.BouncyCastle.Utilities.IO.Pem
{
	// Token: 0x020006E2 RID: 1762
	public class PemHeader
	{
		// Token: 0x06003D90 RID: 15760 RVA: 0x00151034 File Offset: 0x00151034
		public PemHeader(string name, string val)
		{
			this.name = name;
			this.val = val;
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06003D91 RID: 15761 RVA: 0x0015104C File Offset: 0x0015104C
		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06003D92 RID: 15762 RVA: 0x00151054 File Offset: 0x00151054
		public virtual string Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x06003D93 RID: 15763 RVA: 0x0015105C File Offset: 0x0015105C
		public override int GetHashCode()
		{
			return this.GetHashCode(this.name) + 31 * this.GetHashCode(this.val);
		}

		// Token: 0x06003D94 RID: 15764 RVA: 0x0015107C File Offset: 0x0015107C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			if (!(obj is PemHeader))
			{
				return false;
			}
			PemHeader pemHeader = (PemHeader)obj;
			return object.Equals(this.name, pemHeader.name) && object.Equals(this.val, pemHeader.val);
		}

		// Token: 0x06003D95 RID: 15765 RVA: 0x001510D4 File Offset: 0x001510D4
		private int GetHashCode(string s)
		{
			if (s == null)
			{
				return 1;
			}
			return s.GetHashCode();
		}

		// Token: 0x04001EFA RID: 7930
		private string name;

		// Token: 0x04001EFB RID: 7931
		private string val;
	}
}
