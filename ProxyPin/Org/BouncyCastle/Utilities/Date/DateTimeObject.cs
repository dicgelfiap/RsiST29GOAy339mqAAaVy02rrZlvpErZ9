using System;

namespace Org.BouncyCastle.Utilities.Date
{
	// Token: 0x020006D4 RID: 1748
	public sealed class DateTimeObject
	{
		// Token: 0x06003D42 RID: 15682 RVA: 0x0014FD3C File Offset: 0x0014FD3C
		public DateTimeObject(DateTime dt)
		{
			this.dt = dt;
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06003D43 RID: 15683 RVA: 0x0014FD4C File Offset: 0x0014FD4C
		public DateTime Value
		{
			get
			{
				return this.dt;
			}
		}

		// Token: 0x06003D44 RID: 15684 RVA: 0x0014FD54 File Offset: 0x0014FD54
		public override string ToString()
		{
			return this.dt.ToString();
		}

		// Token: 0x04001EEA RID: 7914
		private readonly DateTime dt;
	}
}
