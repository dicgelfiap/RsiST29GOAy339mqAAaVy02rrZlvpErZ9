using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000423 RID: 1059
	public interface IVerifier
	{
		// Token: 0x060021A0 RID: 8608
		bool IsVerified(byte[] data);

		// Token: 0x060021A1 RID: 8609
		bool IsVerified(byte[] source, int off, int length);
	}
}
