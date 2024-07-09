using System;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200065A RID: 1626
	public sealed class PgpPad
	{
		// Token: 0x06003865 RID: 14437 RVA: 0x0012EE10 File Offset: 0x0012EE10
		private PgpPad()
		{
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x0012EE18 File Offset: 0x0012EE18
		public static byte[] PadSessionData(byte[] sessionInfo)
		{
			byte[] array = new byte[40];
			Array.Copy(sessionInfo, 0, array, 0, sessionInfo.Length);
			byte b = (byte)(array.Length - sessionInfo.Length);
			for (int num = sessionInfo.Length; num != array.Length; num++)
			{
				array[num] = b;
			}
			return array;
		}

		// Token: 0x06003867 RID: 14439 RVA: 0x0012EE60 File Offset: 0x0012EE60
		public static byte[] UnpadSessionData(byte[] encoded)
		{
			byte b = encoded[encoded.Length - 1];
			for (int num = encoded.Length - (int)b; num != encoded.Length; num++)
			{
				if (encoded[num] != b)
				{
					throw new PgpException("bad padding found in session data");
				}
			}
			byte[] array = new byte[encoded.Length - (int)b];
			Array.Copy(encoded, 0, array, 0, array.Length);
			return array;
		}
	}
}
