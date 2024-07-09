using System;
using System.Collections;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020004AB RID: 1195
	public class IsoTrailers
	{
		// Token: 0x060024C4 RID: 9412 RVA: 0x000CCF64 File Offset: 0x000CCF64
		private static IDictionary CreateTrailerMap()
		{
			IDictionary dictionary = Platform.CreateHashtable();
			dictionary.Add("RIPEMD128", 13004);
			dictionary.Add("RIPEMD160", 12748);
			dictionary.Add("SHA-1", 13260);
			dictionary.Add("SHA-224", 14540);
			dictionary.Add("SHA-256", 13516);
			dictionary.Add("SHA-384", 14028);
			dictionary.Add("SHA-512", 13772);
			dictionary.Add("SHA-512/224", 14796);
			dictionary.Add("SHA-512/256", 16588);
			dictionary.Add("Whirlpool", 14284);
			return CollectionUtilities.ReadOnly(dictionary);
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000CD054 File Offset: 0x000CD054
		public static int GetTrailer(IDigest digest)
		{
			return (int)IsoTrailers.trailerMap[digest.AlgorithmName];
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000CD06C File Offset: 0x000CD06C
		public static bool NoTrailerAvailable(IDigest digest)
		{
			return !IsoTrailers.trailerMap.Contains(digest.AlgorithmName);
		}

		// Token: 0x04001746 RID: 5958
		public const int TRAILER_IMPLICIT = 188;

		// Token: 0x04001747 RID: 5959
		public const int TRAILER_RIPEMD160 = 12748;

		// Token: 0x04001748 RID: 5960
		public const int TRAILER_RIPEMD128 = 13004;

		// Token: 0x04001749 RID: 5961
		public const int TRAILER_SHA1 = 13260;

		// Token: 0x0400174A RID: 5962
		public const int TRAILER_SHA256 = 13516;

		// Token: 0x0400174B RID: 5963
		public const int TRAILER_SHA512 = 13772;

		// Token: 0x0400174C RID: 5964
		public const int TRAILER_SHA384 = 14028;

		// Token: 0x0400174D RID: 5965
		public const int TRAILER_WHIRLPOOL = 14284;

		// Token: 0x0400174E RID: 5966
		public const int TRAILER_SHA224 = 14540;

		// Token: 0x0400174F RID: 5967
		public const int TRAILER_SHA512_224 = 14796;

		// Token: 0x04001750 RID: 5968
		public const int TRAILER_SHA512_256 = 16588;

		// Token: 0x04001751 RID: 5969
		private static readonly IDictionary trailerMap = IsoTrailers.CreateTrailerMap();
	}
}
