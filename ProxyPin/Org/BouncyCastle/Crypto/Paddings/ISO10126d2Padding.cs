using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Paddings
{
	// Token: 0x02000427 RID: 1063
	public class ISO10126d2Padding : IBlockCipherPadding
	{
		// Token: 0x060021AE RID: 8622 RVA: 0x000C2E14 File Offset: 0x000C2E14
		public void Init(SecureRandom random)
		{
			this.random = ((random != null) ? random : new SecureRandom());
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x000C2E30 File Offset: 0x000C2E30
		public string PaddingName
		{
			get
			{
				return "ISO10126-2";
			}
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x000C2E38 File Offset: 0x000C2E38
		public int AddPadding(byte[] input, int inOff)
		{
			byte b = (byte)(input.Length - inOff);
			while (inOff < input.Length - 1)
			{
				input[inOff] = (byte)this.random.NextInt();
				inOff++;
			}
			input[inOff] = b;
			return (int)b;
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x000C2E78 File Offset: 0x000C2E78
		public int PadCount(byte[] input)
		{
			int num = (int)(input[input.Length - 1] & byte.MaxValue);
			if (num > input.Length)
			{
				throw new InvalidCipherTextException("pad block corrupted");
			}
			return num;
		}

		// Token: 0x040015D0 RID: 5584
		private SecureRandom random;
	}
}
