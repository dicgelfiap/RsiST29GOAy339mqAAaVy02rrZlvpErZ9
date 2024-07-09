using System;
using Org.BouncyCastle.Utilities.Date;

namespace Org.BouncyCastle.Utilities
{
	// Token: 0x020006FD RID: 1789
	internal abstract class Enums
	{
		// Token: 0x06003EAD RID: 16045 RVA: 0x00159950 File Offset: 0x00159950
		internal static Enum GetEnumValue(Type enumType, string s)
		{
			if (!Enums.IsEnumType(enumType))
			{
				throw new ArgumentException("Not an enumeration type", "enumType");
			}
			if (s.Length > 0 && char.IsLetter(s[0]) && s.IndexOf(',') < 0)
			{
				s = s.Replace('-', '_');
				s = s.Replace('/', '_');
				return (Enum)Enum.Parse(enumType, s, false);
			}
			throw new ArgumentException();
		}

		// Token: 0x06003EAE RID: 16046 RVA: 0x001599D4 File Offset: 0x001599D4
		internal static Array GetEnumValues(Type enumType)
		{
			if (!Enums.IsEnumType(enumType))
			{
				throw new ArgumentException("Not an enumeration type", "enumType");
			}
			return Enum.GetValues(enumType);
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x001599F8 File Offset: 0x001599F8
		internal static Enum GetArbitraryValue(Type enumType)
		{
			Array enumValues = Enums.GetEnumValues(enumType);
			int index = (int)(DateTimeUtilities.CurrentUnixMs() & 2147483647L) % enumValues.Length;
			return (Enum)enumValues.GetValue(index);
		}

		// Token: 0x06003EB0 RID: 16048 RVA: 0x00159A34 File Offset: 0x00159A34
		internal static bool IsEnumType(Type t)
		{
			return t.IsEnum;
		}
	}
}
