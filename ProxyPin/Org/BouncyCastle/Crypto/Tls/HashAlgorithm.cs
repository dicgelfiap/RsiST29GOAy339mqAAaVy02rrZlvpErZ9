using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000505 RID: 1285
	public abstract class HashAlgorithm
	{
		// Token: 0x06002758 RID: 10072 RVA: 0x000D56F8 File Offset: 0x000D56F8
		public static string GetName(byte hashAlgorithm)
		{
			switch (hashAlgorithm)
			{
			case 0:
				return "none";
			case 1:
				return "md5";
			case 2:
				return "sha1";
			case 3:
				return "sha224";
			case 4:
				return "sha256";
			case 5:
				return "sha384";
			case 6:
				return "sha512";
			default:
				return "UNKNOWN";
			}
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x000D5764 File Offset: 0x000D5764
		public static string GetText(byte hashAlgorithm)
		{
			return string.Concat(new object[]
			{
				HashAlgorithm.GetName(hashAlgorithm),
				"(",
				hashAlgorithm,
				")"
			});
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x000D57A4 File Offset: 0x000D57A4
		public static bool IsPrivate(byte hashAlgorithm)
		{
			return 224 <= hashAlgorithm && hashAlgorithm <= byte.MaxValue;
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x000D57C0 File Offset: 0x000D57C0
		public static bool IsRecognized(byte hashAlgorithm)
		{
			switch (hashAlgorithm)
			{
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x040019B0 RID: 6576
		public const byte none = 0;

		// Token: 0x040019B1 RID: 6577
		public const byte md5 = 1;

		// Token: 0x040019B2 RID: 6578
		public const byte sha1 = 2;

		// Token: 0x040019B3 RID: 6579
		public const byte sha224 = 3;

		// Token: 0x040019B4 RID: 6580
		public const byte sha256 = 4;

		// Token: 0x040019B5 RID: 6581
		public const byte sha384 = 5;

		// Token: 0x040019B6 RID: 6582
		public const byte sha512 = 6;
	}
}
