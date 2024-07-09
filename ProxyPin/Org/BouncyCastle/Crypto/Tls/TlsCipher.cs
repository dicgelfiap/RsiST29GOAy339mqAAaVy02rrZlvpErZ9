using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004D8 RID: 1240
	public interface TlsCipher
	{
		// Token: 0x06002642 RID: 9794
		int GetPlaintextLimit(int ciphertextLimit);

		// Token: 0x06002643 RID: 9795
		byte[] EncodePlaintext(long seqNo, byte type, byte[] plaintext, int offset, int len);

		// Token: 0x06002644 RID: 9796
		byte[] DecodeCiphertext(long seqNo, byte type, byte[] ciphertext, int offset, int len);
	}
}
