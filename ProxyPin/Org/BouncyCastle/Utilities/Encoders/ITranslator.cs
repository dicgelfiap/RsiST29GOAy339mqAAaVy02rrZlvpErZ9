using System;

namespace Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x020006DD RID: 1757
	public interface ITranslator
	{
		// Token: 0x06003D7A RID: 15738
		int GetEncodedBlockSize();

		// Token: 0x06003D7B RID: 15739
		int Encode(byte[] input, int inOff, int length, byte[] outBytes, int outOff);

		// Token: 0x06003D7C RID: 15740
		int GetDecodedBlockSize();

		// Token: 0x06003D7D RID: 15741
		int Decode(byte[] input, int inOff, int length, byte[] outBytes, int outOff);
	}
}
