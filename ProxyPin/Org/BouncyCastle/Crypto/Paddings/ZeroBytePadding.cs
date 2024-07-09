using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Paddings
{
	// Token: 0x0200042D RID: 1069
	public class ZeroBytePadding : IBlockCipherPadding
	{
		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060021CF RID: 8655 RVA: 0x000C34F8 File Offset: 0x000C34F8
		public string PaddingName
		{
			get
			{
				return "ZeroBytePadding";
			}
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x000C3500 File Offset: 0x000C3500
		public void Init(SecureRandom random)
		{
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x000C3504 File Offset: 0x000C3504
		public int AddPadding(byte[] input, int inOff)
		{
			int result = input.Length - inOff;
			while (inOff < input.Length)
			{
				input[inOff] = 0;
				inOff++;
			}
			return result;
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x000C3530 File Offset: 0x000C3530
		public int PadCount(byte[] input)
		{
			int num = input.Length;
			while (num > 0 && input[num - 1] == 0)
			{
				num--;
			}
			return input.Length - num;
		}
	}
}
