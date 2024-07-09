using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Tsp;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x020006BA RID: 1722
	public class GenTimeAccuracy
	{
		// Token: 0x06003C41 RID: 15425 RVA: 0x0014D748 File Offset: 0x0014D748
		public GenTimeAccuracy(Accuracy accuracy)
		{
			this.accuracy = accuracy;
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06003C42 RID: 15426 RVA: 0x0014D758 File Offset: 0x0014D758
		public int Seconds
		{
			get
			{
				return this.GetTimeComponent(this.accuracy.Seconds);
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06003C43 RID: 15427 RVA: 0x0014D76C File Offset: 0x0014D76C
		public int Millis
		{
			get
			{
				return this.GetTimeComponent(this.accuracy.Millis);
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06003C44 RID: 15428 RVA: 0x0014D780 File Offset: 0x0014D780
		public int Micros
		{
			get
			{
				return this.GetTimeComponent(this.accuracy.Micros);
			}
		}

		// Token: 0x06003C45 RID: 15429 RVA: 0x0014D794 File Offset: 0x0014D794
		private int GetTimeComponent(DerInteger time)
		{
			if (time != null)
			{
				return time.IntValueExact;
			}
			return 0;
		}

		// Token: 0x06003C46 RID: 15430 RVA: 0x0014D7A4 File Offset: 0x0014D7A4
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.Seconds,
				".",
				this.Millis.ToString("000"),
				this.Micros.ToString("000")
			});
		}

		// Token: 0x04001EA9 RID: 7849
		private Accuracy accuracy;
	}
}
