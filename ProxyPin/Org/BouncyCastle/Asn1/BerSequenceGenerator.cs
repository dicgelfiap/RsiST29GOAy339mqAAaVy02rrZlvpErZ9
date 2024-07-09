using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000259 RID: 601
	public class BerSequenceGenerator : BerGenerator
	{
		// Token: 0x06001339 RID: 4921 RVA: 0x0006A284 File Offset: 0x0006A284
		public BerSequenceGenerator(Stream outStream) : base(outStream)
		{
			base.WriteBerHeader(48);
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0006A298 File Offset: 0x0006A298
		public BerSequenceGenerator(Stream outStream, int tagNo, bool isExplicit) : base(outStream, tagNo, isExplicit)
		{
			base.WriteBerHeader(48);
		}
	}
}
