using System;
using PeNet.Structures;

namespace PeNet
{
	// Token: 0x02000B93 RID: 2963
	internal static class PeValidator
	{
		// Token: 0x0600775C RID: 30556 RVA: 0x00239BB4 File Offset: 0x00239BB4
		public static bool HasMagicHeader(byte[] buff)
		{
			bool result;
			try
			{
				result = (new IMAGE_DOS_HEADER(buff, 0U).e_magic == 23117);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600775D RID: 30557 RVA: 0x00239BF4 File Offset: 0x00239BF4
		public static bool IsPeFileParseable(byte[] buff)
		{
			bool result;
			try
			{
				new PeFile(buff);
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600775E RID: 30558 RVA: 0x00239C28 File Offset: 0x00239C28
		public static bool HasValidNumberOfDirectories(byte[] buff)
		{
			bool result;
			try
			{
				result = (new PeFile(buff).ImageNtHeaders.OptionalHeader.NumberOfRvaAndSizes <= 16U);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600775F RID: 30559 RVA: 0x00239C70 File Offset: 0x00239C70
		public static bool IsPeValidPeFile(byte[] buff)
		{
			return PeValidator.HasMagicHeader(buff) && PeValidator.IsPeFileParseable(buff) && PeValidator.HasValidNumberOfDirectories(buff);
		}
	}
}
