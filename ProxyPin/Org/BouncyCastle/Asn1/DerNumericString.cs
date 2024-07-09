using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200026E RID: 622
	public class DerNumericString : DerStringBase
	{
		// Token: 0x060013CD RID: 5069 RVA: 0x0006BED0 File Offset: 0x0006BED0
		public static DerNumericString GetInstance(object obj)
		{
			if (obj == null || obj is DerNumericString)
			{
				return (DerNumericString)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0006BF00 File Offset: 0x0006BF00
		public static DerNumericString GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerNumericString)
			{
				return DerNumericString.GetInstance(@object);
			}
			return new DerNumericString(Asn1OctetString.GetInstance(@object).GetOctets());
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0006BF40 File Offset: 0x0006BF40
		public DerNumericString(byte[] str) : this(Strings.FromAsciiByteArray(str), false)
		{
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0006BF50 File Offset: 0x0006BF50
		public DerNumericString(string str) : this(str, false)
		{
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0006BF5C File Offset: 0x0006BF5C
		public DerNumericString(string str, bool validate)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (validate && !DerNumericString.IsNumericString(str))
			{
				throw new ArgumentException("string contains illegal characters", "str");
			}
			this.str = str;
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0006BFAC File Offset: 0x0006BFAC
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0006BFB4 File Offset: 0x0006BFB4
		public byte[] GetOctets()
		{
			return Strings.ToAsciiByteArray(this.str);
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0006BFC4 File Offset: 0x0006BFC4
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(18, this.GetOctets());
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0006BFD4 File Offset: 0x0006BFD4
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerNumericString derNumericString = asn1Object as DerNumericString;
			return derNumericString != null && this.str.Equals(derNumericString.str);
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0006C008 File Offset: 0x0006C008
		public static bool IsNumericString(string str)
		{
			foreach (char c in str)
			{
				if (c > '\u007f' || (c != ' ' && !char.IsDigit(c)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000DB6 RID: 3510
		private readonly string str;
	}
}
