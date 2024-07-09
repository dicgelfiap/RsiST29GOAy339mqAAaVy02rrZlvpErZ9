using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200027F RID: 639
	public class OidTokenizer
	{
		// Token: 0x06001442 RID: 5186 RVA: 0x0006D11C File Offset: 0x0006D11C
		public OidTokenizer(string oid)
		{
			this.oid = oid;
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x0006D12C File Offset: 0x0006D12C
		public bool HasMoreTokens
		{
			get
			{
				return this.index != -1;
			}
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0006D13C File Offset: 0x0006D13C
		public string NextToken()
		{
			if (this.index == -1)
			{
				return null;
			}
			int num = this.oid.IndexOf('.', this.index);
			if (num == -1)
			{
				string result = this.oid.Substring(this.index);
				this.index = -1;
				return result;
			}
			string result2 = this.oid.Substring(this.index, num - this.index);
			this.index = num + 1;
			return result2;
		}

		// Token: 0x04000DC8 RID: 3528
		private string oid;

		// Token: 0x04000DC9 RID: 3529
		private int index;
	}
}
