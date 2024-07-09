using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Paddings
{
	// Token: 0x0200042C RID: 1068
	public class X923Padding : IBlockCipherPadding
	{
		// Token: 0x060021CA RID: 8650 RVA: 0x000C3454 File Offset: 0x000C3454
		public void Init(SecureRandom random)
		{
			this.random = random;
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060021CB RID: 8651 RVA: 0x000C3460 File Offset: 0x000C3460
		public string PaddingName
		{
			get
			{
				return "X9.23";
			}
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x000C3468 File Offset: 0x000C3468
		public int AddPadding(byte[] input, int inOff)
		{
			byte b = (byte)(input.Length - inOff);
			while (inOff < input.Length - 1)
			{
				if (this.random == null)
				{
					input[inOff] = 0;
				}
				else
				{
					input[inOff] = (byte)this.random.NextInt();
				}
				inOff++;
			}
			input[inOff] = b;
			return (int)b;
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x000C34BC File Offset: 0x000C34BC
		public int PadCount(byte[] input)
		{
			int num = (int)(input[input.Length - 1] & byte.MaxValue);
			if (num > input.Length)
			{
				throw new InvalidCipherTextException("pad block corrupted");
			}
			return num;
		}

		// Token: 0x040015D2 RID: 5586
		private SecureRandom random;
	}
}
