using System;
using System.IO;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004F1 RID: 1265
	internal class DigestInputBuffer : MemoryStream
	{
		// Token: 0x060026D9 RID: 9945 RVA: 0x000D20B4 File Offset: 0x000D20B4
		internal void UpdateDigest(IDigest d)
		{
			Streams.WriteBufTo(this, new DigestInputBuffer.DigStream(d));
		}

		// Token: 0x02000E19 RID: 3609
		private class DigStream : BaseOutputStream
		{
			// Token: 0x06008C40 RID: 35904 RVA: 0x002A21E4 File Offset: 0x002A21E4
			internal DigStream(IDigest d)
			{
				this.d = d;
			}

			// Token: 0x06008C41 RID: 35905 RVA: 0x002A21F4 File Offset: 0x002A21F4
			public override void WriteByte(byte b)
			{
				this.d.Update(b);
			}

			// Token: 0x06008C42 RID: 35906 RVA: 0x002A2204 File Offset: 0x002A2204
			public override void Write(byte[] buf, int off, int len)
			{
				this.d.BlockUpdate(buf, off, len);
			}

			// Token: 0x0400415B RID: 16731
			private readonly IDigest d;
		}
	}
}
