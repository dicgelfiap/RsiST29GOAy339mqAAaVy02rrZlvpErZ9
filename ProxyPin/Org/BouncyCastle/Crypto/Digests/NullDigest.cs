using System;
using System.IO;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x0200035C RID: 860
	public class NullDigest : IDigest
	{
		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001A0F RID: 6671 RVA: 0x00088754 File Offset: 0x00088754
		public string AlgorithmName
		{
			get
			{
				return "NULL";
			}
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x0008875C File Offset: 0x0008875C
		public int GetByteLength()
		{
			return 0;
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x00088760 File Offset: 0x00088760
		public int GetDigestSize()
		{
			return (int)this.bOut.Length;
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x00088770 File Offset: 0x00088770
		public void Update(byte b)
		{
			this.bOut.WriteByte(b);
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x00088780 File Offset: 0x00088780
		public void BlockUpdate(byte[] inBytes, int inOff, int len)
		{
			this.bOut.Write(inBytes, inOff, len);
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x00088790 File Offset: 0x00088790
		public int DoFinal(byte[] outBytes, int outOff)
		{
			int result;
			try
			{
				result = Streams.WriteBufTo(this.bOut, outBytes, outOff);
			}
			finally
			{
				this.Reset();
			}
			return result;
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x000887C8 File Offset: 0x000887C8
		public void Reset()
		{
			this.bOut.SetLength(0L);
		}

		// Token: 0x0400117C RID: 4476
		private readonly MemoryStream bOut = new MemoryStream();
	}
}
