using System;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x02000195 RID: 405
	public class OcspResponseStatus : DerEnumerated
	{
		// Token: 0x06000D54 RID: 3412 RVA: 0x00053CCC File Offset: 0x00053CCC
		public OcspResponseStatus(int value) : base(value)
		{
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00053CD8 File Offset: 0x00053CD8
		public OcspResponseStatus(DerEnumerated value) : base(value.IntValueExact)
		{
		}

		// Token: 0x04000998 RID: 2456
		public const int Successful = 0;

		// Token: 0x04000999 RID: 2457
		public const int MalformedRequest = 1;

		// Token: 0x0400099A RID: 2458
		public const int InternalError = 2;

		// Token: 0x0400099B RID: 2459
		public const int TryLater = 3;

		// Token: 0x0400099C RID: 2460
		public const int SignatureRequired = 5;

		// Token: 0x0400099D RID: 2461
		public const int Unauthorized = 6;
	}
}
