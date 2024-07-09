using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000238 RID: 568
	public abstract class Asn1Generator
	{
		// Token: 0x0600125C RID: 4700 RVA: 0x00067A5C File Offset: 0x00067A5C
		protected Asn1Generator(Stream outStream)
		{
			this._out = outStream;
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x00067A6C File Offset: 0x00067A6C
		protected Stream Out
		{
			get
			{
				return this._out;
			}
		}

		// Token: 0x0600125E RID: 4702
		public abstract void AddObject(Asn1Encodable obj);

		// Token: 0x0600125F RID: 4703
		public abstract Stream GetRawOutputStream();

		// Token: 0x06001260 RID: 4704
		public abstract void Close();

		// Token: 0x04000D5B RID: 3419
		private Stream _out;
	}
}
