using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000430 RID: 1072
	public class KeyParameter : ICipherParameters
	{
		// Token: 0x060021DB RID: 8667 RVA: 0x000C35D0 File Offset: 0x000C35D0
		public KeyParameter(byte[] key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.key = (byte[])key.Clone();
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x000C35FC File Offset: 0x000C35FC
		public KeyParameter(byte[] key, int keyOff, int keyLen)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (keyOff < 0 || keyOff > key.Length)
			{
				throw new ArgumentOutOfRangeException("keyOff");
			}
			if (keyLen < 0 || keyLen > key.Length - keyOff)
			{
				throw new ArgumentOutOfRangeException("keyLen");
			}
			this.key = new byte[keyLen];
			Array.Copy(key, keyOff, this.key, 0, keyLen);
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x000C3678 File Offset: 0x000C3678
		public byte[] GetKey()
		{
			return (byte[])this.key.Clone();
		}

		// Token: 0x040015D7 RID: 5591
		private readonly byte[] key;
	}
}
