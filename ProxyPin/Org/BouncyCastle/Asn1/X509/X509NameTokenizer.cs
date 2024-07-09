using System;
using System.Text;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000225 RID: 549
	public class X509NameTokenizer
	{
		// Token: 0x060011D3 RID: 4563 RVA: 0x00065798 File Offset: 0x00065798
		public X509NameTokenizer(string oid) : this(oid, ',')
		{
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x000657A4 File Offset: 0x000657A4
		public X509NameTokenizer(string oid, char separator)
		{
			this.value = oid;
			this.index = -1;
			this.separator = separator;
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x000657CC File Offset: 0x000657CC
		public bool HasMoreTokens()
		{
			return this.index != this.value.Length;
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x000657E4 File Offset: 0x000657E4
		public string NextToken()
		{
			if (this.index == this.value.Length)
			{
				return null;
			}
			int num = this.index + 1;
			bool flag = false;
			bool flag2 = false;
			this.buffer.Remove(0, this.buffer.Length);
			while (num != this.value.Length)
			{
				char c = this.value[num];
				if (c == '"')
				{
					if (!flag2)
					{
						flag = !flag;
					}
					else
					{
						this.buffer.Append(c);
						flag2 = false;
					}
				}
				else if (flag2 || flag)
				{
					if (c == '#' && this.buffer[this.buffer.Length - 1] == '=')
					{
						this.buffer.Append('\\');
					}
					else if (c == '+' && this.separator != '+')
					{
						this.buffer.Append('\\');
					}
					this.buffer.Append(c);
					flag2 = false;
				}
				else if (c == '\\')
				{
					flag2 = true;
				}
				else
				{
					if (c == this.separator)
					{
						break;
					}
					this.buffer.Append(c);
				}
				num++;
			}
			this.index = num;
			return this.buffer.ToString().Trim();
		}

		// Token: 0x04000CDB RID: 3291
		private string value;

		// Token: 0x04000CDC RID: 3292
		private int index;

		// Token: 0x04000CDD RID: 3293
		private char separator;

		// Token: 0x04000CDE RID: 3294
		private StringBuilder buffer = new StringBuilder();
	}
}
