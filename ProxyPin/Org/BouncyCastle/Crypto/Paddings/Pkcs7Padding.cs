using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Paddings
{
	// Token: 0x0200042A RID: 1066
	public class Pkcs7Padding : IBlockCipherPadding
	{
		// Token: 0x060021C0 RID: 8640 RVA: 0x000C32F4 File Offset: 0x000C32F4
		public void Init(SecureRandom random)
		{
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060021C1 RID: 8641 RVA: 0x000C32F8 File Offset: 0x000C32F8
		public string PaddingName
		{
			get
			{
				return "PKCS7";
			}
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x000C3300 File Offset: 0x000C3300
		public int AddPadding(byte[] input, int inOff)
		{
			byte b = (byte)(input.Length - inOff);
			while (inOff < input.Length)
			{
				input[inOff] = b;
				inOff++;
			}
			return (int)b;
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x000C3330 File Offset: 0x000C3330
		public int PadCount(byte[] input)
		{
			byte b = input[input.Length - 1];
			int num = (int)b;
			if (num < 1 || num > input.Length)
			{
				throw new InvalidCipherTextException("pad block corrupted");
			}
			for (int i = 2; i <= num; i++)
			{
				if (input[input.Length - i] != b)
				{
					throw new InvalidCipherTextException("pad block corrupted");
				}
			}
			return num;
		}
	}
}
