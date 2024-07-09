using System;
using System.IO;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200051F RID: 1311
	internal class SignerInputBuffer : MemoryStream
	{
		// Token: 0x060027EE RID: 10222 RVA: 0x000D6FCC File Offset: 0x000D6FCC
		internal void UpdateSigner(ISigner s)
		{
			Streams.WriteBufTo(this, new SignerInputBuffer.SigStream(s));
		}

		// Token: 0x02000E24 RID: 3620
		private class SigStream : BaseOutputStream
		{
			// Token: 0x06008C63 RID: 35939 RVA: 0x002A2664 File Offset: 0x002A2664
			internal SigStream(ISigner s)
			{
				this.s = s;
			}

			// Token: 0x06008C64 RID: 35940 RVA: 0x002A2674 File Offset: 0x002A2674
			public override void WriteByte(byte b)
			{
				this.s.Update(b);
			}

			// Token: 0x06008C65 RID: 35941 RVA: 0x002A2684 File Offset: 0x002A2684
			public override void Write(byte[] buf, int off, int len)
			{
				this.s.BlockUpdate(buf, off, len);
			}

			// Token: 0x04004191 RID: 16785
			private readonly ISigner s;
		}
	}
}
