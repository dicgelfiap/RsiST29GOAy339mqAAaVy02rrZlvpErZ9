using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001F1 RID: 497
	public class CrlNumber : DerInteger
	{
		// Token: 0x06001008 RID: 4104 RVA: 0x0005E4B0 File Offset: 0x0005E4B0
		public CrlNumber(BigInteger number) : base(number)
		{
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x0005E4BC File Offset: 0x0005E4BC
		public BigInteger Number
		{
			get
			{
				return base.PositiveValue;
			}
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0005E4C4 File Offset: 0x0005E4C4
		public override string ToString()
		{
			return "CRLNumber: " + this.Number;
		}
	}
}
