using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000270 RID: 624
	public class DerPrintableString : DerStringBase
	{
		// Token: 0x060013DA RID: 5082 RVA: 0x0006C0C0 File Offset: 0x0006C0C0
		public static DerPrintableString GetInstance(object obj)
		{
			if (obj == null || obj is DerPrintableString)
			{
				return (DerPrintableString)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0006C0F0 File Offset: 0x0006C0F0
		public static DerPrintableString GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerPrintableString)
			{
				return DerPrintableString.GetInstance(@object);
			}
			return new DerPrintableString(Asn1OctetString.GetInstance(@object).GetOctets());
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0006C130 File Offset: 0x0006C130
		public DerPrintableString(byte[] str) : this(Strings.FromAsciiByteArray(str), false)
		{
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0006C140 File Offset: 0x0006C140
		public DerPrintableString(string str) : this(str, false)
		{
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0006C14C File Offset: 0x0006C14C
		public DerPrintableString(string str, bool validate)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (validate && !DerPrintableString.IsPrintableString(str))
			{
				throw new ArgumentException("string contains illegal characters", "str");
			}
			this.str = str;
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0006C19C File Offset: 0x0006C19C
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0006C1A4 File Offset: 0x0006C1A4
		public byte[] GetOctets()
		{
			return Strings.ToAsciiByteArray(this.str);
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0006C1B4 File Offset: 0x0006C1B4
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(19, this.GetOctets());
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0006C1C4 File Offset: 0x0006C1C4
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerPrintableString derPrintableString = asn1Object as DerPrintableString;
			return derPrintableString != null && this.str.Equals(derPrintableString.str);
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0006C1F8 File Offset: 0x0006C1F8
		public static bool IsPrintableString(string str)
		{
			int i = 0;
			while (i < str.Length)
			{
				char c = str[i];
				if (c <= '\u007f')
				{
					if (!char.IsLetterOrDigit(c))
					{
						char c2 = c;
						switch (c2)
						{
						case ' ':
						case '\'':
						case '(':
						case ')':
						case '+':
						case ',':
						case '-':
						case '.':
						case '/':
							goto IL_9E;
						case '!':
						case '"':
						case '#':
						case '$':
						case '%':
						case '&':
						case '*':
							break;
						default:
							if (c2 == ':')
							{
								goto IL_9E;
							}
							switch (c2)
							{
							case '=':
							case '?':
								goto IL_9E;
							}
							break;
						}
						return false;
					}
					IL_9E:
					i++;
					continue;
				}
				return false;
			}
			return true;
		}

		// Token: 0x04000DB8 RID: 3512
		private readonly string str;
	}
}
