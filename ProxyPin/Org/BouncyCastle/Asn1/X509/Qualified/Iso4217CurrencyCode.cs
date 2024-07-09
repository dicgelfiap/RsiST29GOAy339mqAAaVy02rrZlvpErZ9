using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x020001D9 RID: 473
	public class Iso4217CurrencyCode : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06000F3D RID: 3901 RVA: 0x0005BBA4 File Offset: 0x0005BBA4
		public static Iso4217CurrencyCode GetInstance(object obj)
		{
			if (obj == null || obj is Iso4217CurrencyCode)
			{
				return (Iso4217CurrencyCode)obj;
			}
			if (obj is DerInteger)
			{
				DerInteger instance = DerInteger.GetInstance(obj);
				int intValueExact = instance.IntValueExact;
				return new Iso4217CurrencyCode(intValueExact);
			}
			if (obj is DerPrintableString)
			{
				DerPrintableString instance2 = DerPrintableString.GetInstance(obj);
				return new Iso4217CurrencyCode(instance2.GetString());
			}
			throw new ArgumentException("unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0005BC28 File Offset: 0x0005BC28
		public Iso4217CurrencyCode(int numeric)
		{
			if (numeric > 999 || numeric < 1)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"wrong size in numeric code : not in (",
					1,
					"..",
					999,
					")"
				}));
			}
			this.obj = new DerInteger(numeric);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0005BCA0 File Offset: 0x0005BCA0
		public Iso4217CurrencyCode(string alphabetic)
		{
			if (alphabetic.Length > 3)
			{
				throw new ArgumentException("wrong size in alphabetic code : max size is " + 3);
			}
			this.obj = new DerPrintableString(alphabetic);
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x0005BCD8 File Offset: 0x0005BCD8
		public bool IsAlphabetic
		{
			get
			{
				return this.obj is DerPrintableString;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x0005BCE8 File Offset: 0x0005BCE8
		public string Alphabetic
		{
			get
			{
				return ((DerPrintableString)this.obj).GetString();
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x0005BCFC File Offset: 0x0005BCFC
		public int Numeric
		{
			get
			{
				return ((DerInteger)this.obj).IntValueExact;
			}
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0005BD10 File Offset: 0x0005BD10
		public override Asn1Object ToAsn1Object()
		{
			return this.obj.ToAsn1Object();
		}

		// Token: 0x04000B77 RID: 2935
		internal const int AlphabeticMaxSize = 3;

		// Token: 0x04000B78 RID: 2936
		internal const int NumericMinSize = 1;

		// Token: 0x04000B79 RID: 2937
		internal const int NumericMaxSize = 999;

		// Token: 0x04000B7A RID: 2938
		internal Asn1Encodable obj;
	}
}
