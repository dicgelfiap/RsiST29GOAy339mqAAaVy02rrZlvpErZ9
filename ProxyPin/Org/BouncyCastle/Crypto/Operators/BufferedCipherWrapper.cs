using System;
using System.IO;
using Org.BouncyCastle.Crypto.IO;

namespace Org.BouncyCastle.Crypto.Operators
{
	// Token: 0x0200040F RID: 1039
	public class BufferedCipherWrapper : ICipher
	{
		// Token: 0x0600215F RID: 8543 RVA: 0x000C1F78 File Offset: 0x000C1F78
		public BufferedCipherWrapper(IBufferedCipher bufferedCipher, Stream source)
		{
			this.bufferedCipher = bufferedCipher;
			this.stream = new CipherStream(source, bufferedCipher, bufferedCipher);
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x000C1F98 File Offset: 0x000C1F98
		public int GetMaxOutputSize(int inputLen)
		{
			return this.bufferedCipher.GetOutputSize(inputLen);
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x000C1FA8 File Offset: 0x000C1FA8
		public int GetUpdateOutputSize(int inputLen)
		{
			return this.bufferedCipher.GetUpdateOutputSize(inputLen);
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06002162 RID: 8546 RVA: 0x000C1FB8 File Offset: 0x000C1FB8
		public Stream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x040015B2 RID: 5554
		private readonly IBufferedCipher bufferedCipher;

		// Token: 0x040015B3 RID: 5555
		private readonly CipherStream stream;
	}
}
