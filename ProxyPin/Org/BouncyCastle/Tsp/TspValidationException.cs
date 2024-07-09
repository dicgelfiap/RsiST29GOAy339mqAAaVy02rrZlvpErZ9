using System;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x020006C5 RID: 1733
	[Serializable]
	public class TspValidationException : TspException
	{
		// Token: 0x06003CAA RID: 15530 RVA: 0x0014F370 File Offset: 0x0014F370
		public TspValidationException(string message) : base(message)
		{
			this.failureCode = -1;
		}

		// Token: 0x06003CAB RID: 15531 RVA: 0x0014F380 File Offset: 0x0014F380
		public TspValidationException(string message, int failureCode) : base(message)
		{
			this.failureCode = failureCode;
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06003CAC RID: 15532 RVA: 0x0014F390 File Offset: 0x0014F390
		public int FailureCode
		{
			get
			{
				return this.failureCode;
			}
		}

		// Token: 0x04001EDE RID: 7902
		private int failureCode;
	}
}
