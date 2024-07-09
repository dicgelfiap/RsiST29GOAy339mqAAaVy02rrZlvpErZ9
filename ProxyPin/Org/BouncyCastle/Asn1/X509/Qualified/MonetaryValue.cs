using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x020001DA RID: 474
	public class MonetaryValue : Asn1Encodable
	{
		// Token: 0x06000F44 RID: 3908 RVA: 0x0005BD20 File Offset: 0x0005BD20
		public static MonetaryValue GetInstance(object obj)
		{
			if (obj == null || obj is MonetaryValue)
			{
				return (MonetaryValue)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new MonetaryValue(Asn1Sequence.GetInstance(obj));
			}
			throw new ArgumentException("unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0005BD7C File Offset: 0x0005BD7C
		private MonetaryValue(Asn1Sequence seq)
		{
			if (seq.Count != 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.currency = Iso4217CurrencyCode.GetInstance(seq[0]);
			this.amount = DerInteger.GetInstance(seq[1]);
			this.exponent = DerInteger.GetInstance(seq[2]);
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0005BDF8 File Offset: 0x0005BDF8
		public MonetaryValue(Iso4217CurrencyCode currency, int amount, int exponent)
		{
			this.currency = currency;
			this.amount = new DerInteger(amount);
			this.exponent = new DerInteger(exponent);
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x0005BE20 File Offset: 0x0005BE20
		public Iso4217CurrencyCode Currency
		{
			get
			{
				return this.currency;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x0005BE28 File Offset: 0x0005BE28
		public BigInteger Amount
		{
			get
			{
				return this.amount.Value;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x0005BE38 File Offset: 0x0005BE38
		public BigInteger Exponent
		{
			get
			{
				return this.exponent.Value;
			}
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0005BE48 File Offset: 0x0005BE48
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.currency,
				this.amount,
				this.exponent
			});
		}

		// Token: 0x04000B7B RID: 2939
		internal Iso4217CurrencyCode currency;

		// Token: 0x04000B7C RID: 2940
		internal DerInteger amount;

		// Token: 0x04000B7D RID: 2941
		internal DerInteger exponent;
	}
}
