using System;
using System.IO;
using Org.BouncyCastle.Utilities.Zlib;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200052F RID: 1327
	public class TlsDeflateCompression : TlsCompression
	{
		// Token: 0x0600287B RID: 10363 RVA: 0x000DAB04 File Offset: 0x000DAB04
		public TlsDeflateCompression() : this(-1)
		{
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x000DAB10 File Offset: 0x000DAB10
		public TlsDeflateCompression(int level)
		{
			this.zIn = new ZStream();
			this.zIn.inflateInit();
			this.zOut = new ZStream();
			this.zOut.deflateInit(level);
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x000DAB48 File Offset: 0x000DAB48
		public virtual Stream Compress(Stream output)
		{
			return new TlsDeflateCompression.DeflateOutputStream(output, this.zOut, true);
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x000DAB58 File Offset: 0x000DAB58
		public virtual Stream Decompress(Stream output)
		{
			return new TlsDeflateCompression.DeflateOutputStream(output, this.zIn, false);
		}

		// Token: 0x04001AB7 RID: 6839
		public const int LEVEL_NONE = 0;

		// Token: 0x04001AB8 RID: 6840
		public const int LEVEL_FASTEST = 1;

		// Token: 0x04001AB9 RID: 6841
		public const int LEVEL_SMALLEST = 9;

		// Token: 0x04001ABA RID: 6842
		public const int LEVEL_DEFAULT = -1;

		// Token: 0x04001ABB RID: 6843
		protected readonly ZStream zIn;

		// Token: 0x04001ABC RID: 6844
		protected readonly ZStream zOut;

		// Token: 0x02000E26 RID: 3622
		protected class DeflateOutputStream : ZOutputStream
		{
			// Token: 0x06008C6A RID: 35946 RVA: 0x002A271C File Offset: 0x002A271C
			public DeflateOutputStream(Stream output, ZStream z, bool compress) : base(output, z)
			{
				this.compress = compress;
				this.FlushMode = 2;
			}

			// Token: 0x06008C6B RID: 35947 RVA: 0x002A2734 File Offset: 0x002A2734
			public override void Flush()
			{
			}
		}
	}
}
