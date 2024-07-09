using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Asn1.X500.Style
{
	// Token: 0x020001D3 RID: 467
	public abstract class IetfUtilities
	{
		// Token: 0x06000F14 RID: 3860 RVA: 0x0005B200 File Offset: 0x0005B200
		public static string ValueToString(Asn1Encodable value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (value is IAsn1String && !(value is DerUniversalString))
			{
				string @string = ((IAsn1String)value).GetString();
				if (@string.Length > 0 && @string[0] == '#')
				{
					stringBuilder.Append('\\');
				}
				stringBuilder.Append(@string);
			}
			else
			{
				try
				{
					stringBuilder.Append('#');
					stringBuilder.Append(Hex.ToHexString(value.ToAsn1Object().GetEncoded("DER")));
				}
				catch (IOException innerException)
				{
					throw new ArgumentException("Other value has no encoded form", innerException);
				}
			}
			int num = stringBuilder.Length;
			int num2 = 0;
			if (stringBuilder.Length >= 2 && stringBuilder[0] == '\\' && stringBuilder[1] == '#')
			{
				num2 += 2;
			}
			while (num2 != num)
			{
				char c = stringBuilder[num2];
				if (c <= ',')
				{
					if (c != '"')
					{
						switch (c)
						{
						case '+':
						case ',':
							break;
						default:
							goto IL_13F;
						}
					}
				}
				else
				{
					switch (c)
					{
					case ';':
					case '<':
					case '=':
					case '>':
						break;
					default:
						if (c != '\\')
						{
							goto IL_13F;
						}
						break;
					}
				}
				stringBuilder.Insert(num2, "\\");
				num2 += 2;
				num++;
				continue;
				IL_13F:
				num2++;
			}
			int num3 = 0;
			if (stringBuilder.Length > 0)
			{
				while (stringBuilder.Length > num3 && stringBuilder[num3] == ' ')
				{
					stringBuilder.Insert(num3, "\\");
					num3 += 2;
				}
			}
			int num4 = stringBuilder.Length - 1;
			while (num4 >= 0 && stringBuilder[num4] == ' ')
			{
				stringBuilder.Insert(num4, "\\");
				num4--;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0005B3E8 File Offset: 0x0005B3E8
		public static string Canonicalize(string s)
		{
			string text = Platform.ToLowerInvariant(s);
			if (text.Length > 0 && text[0] == '#')
			{
				Asn1Object asn1Object = IetfUtilities.DecodeObject(text);
				if (asn1Object is IAsn1String)
				{
					text = Platform.ToLowerInvariant(((IAsn1String)asn1Object).GetString());
				}
			}
			if (text.Length > 1)
			{
				int num = 0;
				while (num + 1 < text.Length && text[num] == '\\' && text[num + 1] == ' ')
				{
					num += 2;
				}
				int num2 = text.Length - 1;
				while (num2 - 1 > 0 && text[num2 - 1] == '\\' && text[num2] == ' ')
				{
					num2 -= 2;
				}
				if (num > 0 || num2 < text.Length - 1)
				{
					text = text.Substring(num, num2 + 1 - num);
				}
			}
			return IetfUtilities.StripInternalSpaces(text);
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0005B4DC File Offset: 0x0005B4DC
		public static string CanonicalString(Asn1Encodable value)
		{
			return IetfUtilities.Canonicalize(IetfUtilities.ValueToString(value));
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0005B4EC File Offset: 0x0005B4EC
		private static Asn1Object DecodeObject(string oValue)
		{
			Asn1Object result;
			try
			{
				result = Asn1Object.FromByteArray(Hex.DecodeStrict(oValue, 1, oValue.Length - 1));
			}
			catch (IOException arg)
			{
				throw new InvalidOperationException("unknown encoding in name: " + arg);
			}
			return result;
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0005B538 File Offset: 0x0005B538
		public static string StripInternalSpaces(string str)
		{
			if (str.IndexOf("  ") < 0)
			{
				return str;
			}
			StringBuilder stringBuilder = new StringBuilder();
			char c = str[0];
			stringBuilder.Append(c);
			for (int i = 1; i < str.Length; i++)
			{
				char c2 = str[i];
				if (' ' != c || ' ' != c2)
				{
					stringBuilder.Append(c2);
					c = c2;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0005B5B0 File Offset: 0x0005B5B0
		public static bool RdnAreEqual(Rdn rdn1, Rdn rdn2)
		{
			if (rdn1.Count != rdn2.Count)
			{
				return false;
			}
			AttributeTypeAndValue[] typesAndValues = rdn1.GetTypesAndValues();
			AttributeTypeAndValue[] typesAndValues2 = rdn2.GetTypesAndValues();
			if (typesAndValues.Length != typesAndValues2.Length)
			{
				return false;
			}
			for (int num = 0; num != typesAndValues.Length; num++)
			{
				if (!IetfUtilities.AtvAreEqual(typesAndValues[num], typesAndValues2[num]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0005B61C File Offset: 0x0005B61C
		private static bool AtvAreEqual(AttributeTypeAndValue atv1, AttributeTypeAndValue atv2)
		{
			if (atv1 == atv2)
			{
				return true;
			}
			if (atv1 == null || atv2 == null)
			{
				return false;
			}
			DerObjectIdentifier type = atv1.Type;
			DerObjectIdentifier type2 = atv2.Type;
			if (!type.Equals(type2))
			{
				return false;
			}
			string text = IetfUtilities.CanonicalString(atv1.Value);
			string value = IetfUtilities.CanonicalString(atv2.Value);
			return text.Equals(value);
		}
	}
}
