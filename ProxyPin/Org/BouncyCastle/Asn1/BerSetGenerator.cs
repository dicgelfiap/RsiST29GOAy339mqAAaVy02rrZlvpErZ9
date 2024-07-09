using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200025D RID: 605
	public class BerSetGenerator : BerGenerator
	{
		// Token: 0x0600134F RID: 4943 RVA: 0x0006A500 File Offset: 0x0006A500
		public BerSetGenerator(Stream outStream) : base(outStream)
		{
			base.WriteBerHeader(49);
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0006A514 File Offset: 0x0006A514
		public BerSetGenerator(Stream outStream, int tagNo, bool isExplicit) : base(outStream, tagNo, isExplicit)
		{
			base.WriteBerHeader(49);
		}
	}
}
