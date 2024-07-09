using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Paddings
{
	// Token: 0x0200042B RID: 1067
	public class TbcPadding : IBlockCipherPadding
	{
		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060021C5 RID: 8645 RVA: 0x000C3398 File Offset: 0x000C3398
		public string PaddingName
		{
			get
			{
				return "TBC";
			}
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x000C33A0 File Offset: 0x000C33A0
		public virtual void Init(SecureRandom random)
		{
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x000C33A4 File Offset: 0x000C33A4
		public virtual int AddPadding(byte[] input, int inOff)
		{
			int result = input.Length - inOff;
			byte b;
			if (inOff > 0)
			{
				b = (((input[inOff - 1] & 1) == 0) ? byte.MaxValue : 0);
			}
			else
			{
				b = (((input[input.Length - 1] & 1) == 0) ? byte.MaxValue : 0);
			}
			while (inOff < input.Length)
			{
				input[inOff] = b;
				inOff++;
			}
			return result;
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x000C3410 File Offset: 0x000C3410
		public virtual int PadCount(byte[] input)
		{
			byte b = input[input.Length - 1];
			int num = input.Length - 1;
			while (num > 0 && input[num - 1] == b)
			{
				num--;
			}
			return input.Length - num;
		}
	}
}
