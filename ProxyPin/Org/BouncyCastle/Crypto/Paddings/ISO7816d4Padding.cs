using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Paddings
{
	// Token: 0x02000428 RID: 1064
	public class ISO7816d4Padding : IBlockCipherPadding
	{
		// Token: 0x060021B3 RID: 8627 RVA: 0x000C2EB4 File Offset: 0x000C2EB4
		public void Init(SecureRandom random)
		{
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x060021B4 RID: 8628 RVA: 0x000C2EB8 File Offset: 0x000C2EB8
		public string PaddingName
		{
			get
			{
				return "ISO7816-4";
			}
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x000C2EC0 File Offset: 0x000C2EC0
		public int AddPadding(byte[] input, int inOff)
		{
			int result = input.Length - inOff;
			input[inOff] = 128;
			for (inOff++; inOff < input.Length; inOff++)
			{
				input[inOff] = 0;
			}
			return result;
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x000C2EFC File Offset: 0x000C2EFC
		public int PadCount(byte[] input)
		{
			int num = input.Length - 1;
			while (num > 0 && input[num] == 0)
			{
				num--;
			}
			if (input[num] != 128)
			{
				throw new InvalidCipherTextException("pad block corrupted");
			}
			return input.Length - num;
		}
	}
}
