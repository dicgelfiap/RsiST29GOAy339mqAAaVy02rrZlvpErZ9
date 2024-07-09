using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Paddings
{
	// Token: 0x02000426 RID: 1062
	public interface IBlockCipherPadding
	{
		// Token: 0x060021AA RID: 8618
		void Init(SecureRandom random);

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x060021AB RID: 8619
		string PaddingName { get; }

		// Token: 0x060021AC RID: 8620
		int AddPadding(byte[] input, int inOff);

		// Token: 0x060021AD RID: 8621
		int PadCount(byte[] input);
	}
}
