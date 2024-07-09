using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000561 RID: 1377
	internal class Check
	{
		// Token: 0x06002AE2 RID: 10978 RVA: 0x000E4FF0 File Offset: 0x000E4FF0
		internal static void DataLength(bool condition, string msg)
		{
			if (condition)
			{
				throw new DataLengthException(msg);
			}
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x000E5000 File Offset: 0x000E5000
		internal static void DataLength(byte[] buf, int off, int len, string msg)
		{
			if (off > buf.Length - len)
			{
				throw new DataLengthException(msg);
			}
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x000E5014 File Offset: 0x000E5014
		internal static void OutputLength(byte[] buf, int off, int len, string msg)
		{
			if (off > buf.Length - len)
			{
				throw new OutputLengthException(msg);
			}
		}
	}
}
