using System;
using System.IO;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000264 RID: 612
	internal abstract class LimitedInputStream : BaseInputStream
	{
		// Token: 0x06001373 RID: 4979 RVA: 0x0006AAA8 File Offset: 0x0006AAA8
		internal LimitedInputStream(Stream inStream, int limit)
		{
			this._in = inStream;
			this._limit = limit;
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x0006AAC0 File Offset: 0x0006AAC0
		internal virtual int Limit
		{
			get
			{
				return this._limit;
			}
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0006AAC8 File Offset: 0x0006AAC8
		protected virtual void SetParentEofDetect(bool on)
		{
			if (this._in is IndefiniteLengthInputStream)
			{
				((IndefiniteLengthInputStream)this._in).SetEofOn00(on);
			}
		}

		// Token: 0x04000DA1 RID: 3489
		protected readonly Stream _in;

		// Token: 0x04000DA2 RID: 3490
		private int _limit;
	}
}
