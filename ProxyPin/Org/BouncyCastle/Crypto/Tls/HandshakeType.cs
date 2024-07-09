using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000504 RID: 1284
	public abstract class HandshakeType
	{
		// Token: 0x040019A1 RID: 6561
		public const byte hello_request = 0;

		// Token: 0x040019A2 RID: 6562
		public const byte client_hello = 1;

		// Token: 0x040019A3 RID: 6563
		public const byte server_hello = 2;

		// Token: 0x040019A4 RID: 6564
		public const byte certificate = 11;

		// Token: 0x040019A5 RID: 6565
		public const byte server_key_exchange = 12;

		// Token: 0x040019A6 RID: 6566
		public const byte certificate_request = 13;

		// Token: 0x040019A7 RID: 6567
		public const byte server_hello_done = 14;

		// Token: 0x040019A8 RID: 6568
		public const byte certificate_verify = 15;

		// Token: 0x040019A9 RID: 6569
		public const byte client_key_exchange = 16;

		// Token: 0x040019AA RID: 6570
		public const byte finished = 20;

		// Token: 0x040019AB RID: 6571
		public const byte certificate_url = 21;

		// Token: 0x040019AC RID: 6572
		public const byte certificate_status = 22;

		// Token: 0x040019AD RID: 6573
		public const byte hello_verify_request = 3;

		// Token: 0x040019AE RID: 6574
		public const byte supplemental_data = 23;

		// Token: 0x040019AF RID: 6575
		public const byte session_ticket = 4;
	}
}
