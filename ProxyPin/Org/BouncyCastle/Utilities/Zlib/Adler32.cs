using System;

namespace Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x020006EE RID: 1774
	internal sealed class Adler32
	{
		// Token: 0x06003DC8 RID: 15816 RVA: 0x00151880 File Offset: 0x00151880
		internal long adler32(long adler, byte[] buf, int index, int len)
		{
			if (buf == null)
			{
				return 1L;
			}
			long num = adler & 65535L;
			long num2 = adler >> 16 & 65535L;
			while (len > 0)
			{
				int i = (len < 5552) ? len : 5552;
				len -= i;
				while (i >= 16)
				{
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					i -= 16;
				}
				if (i != 0)
				{
					do
					{
						num += (long)(buf[index++] & byte.MaxValue);
						num2 += num;
					}
					while (--i != 0);
				}
				num %= 65521L;
				num2 %= 65521L;
			}
			return num2 << 16 | num;
		}

		// Token: 0x04001F05 RID: 7941
		private const int BASE = 65521;

		// Token: 0x04001F06 RID: 7942
		private const int NMAX = 5552;
	}
}
