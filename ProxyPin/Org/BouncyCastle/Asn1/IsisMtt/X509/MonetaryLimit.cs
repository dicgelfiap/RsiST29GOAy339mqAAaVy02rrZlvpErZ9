using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x0200017A RID: 378
	public class MonetaryLimit : Asn1Encodable
	{
		// Token: 0x06000CB4 RID: 3252 RVA: 0x00051218 File Offset: 0x00051218
		public static MonetaryLimit GetInstance(object obj)
		{
			if (obj == null || obj is MonetaryLimit)
			{
				return (MonetaryLimit)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new MonetaryLimit(Asn1Sequence.GetInstance(obj));
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00051274 File Offset: 0x00051274
		private MonetaryLimit(Asn1Sequence seq)
		{
			if (seq.Count != 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.currency = DerPrintableString.GetInstance(seq[0]);
			this.amount = DerInteger.GetInstance(seq[1]);
			this.exponent = DerInteger.GetInstance(seq[2]);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x000512E8 File Offset: 0x000512E8
		public MonetaryLimit(string currency, int amount, int exponent)
		{
			this.currency = new DerPrintableString(currency, true);
			this.amount = new DerInteger(amount);
			this.exponent = new DerInteger(exponent);
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x00051318 File Offset: 0x00051318
		public virtual string Currency
		{
			get
			{
				return this.currency.GetString();
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x00051328 File Offset: 0x00051328
		public virtual BigInteger Amount
		{
			get
			{
				return this.amount.Value;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x00051338 File Offset: 0x00051338
		public virtual BigInteger Exponent
		{
			get
			{
				return this.exponent.Value;
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00051348 File Offset: 0x00051348
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.currency,
				this.amount,
				this.exponent
			});
		}

		// Token: 0x040008C0 RID: 2240
		private readonly DerPrintableString currency;

		// Token: 0x040008C1 RID: 2241
		private readonly DerInteger amount;

		// Token: 0x040008C2 RID: 2242
		private readonly DerInteger exponent;
	}
}
