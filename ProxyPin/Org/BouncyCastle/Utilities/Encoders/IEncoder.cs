using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x020006D7 RID: 1751
	public interface IEncoder
	{
		// Token: 0x06003D54 RID: 15700
		int Encode(byte[] data, int off, int length, Stream outStream);

		// Token: 0x06003D55 RID: 15701
		int Decode(byte[] data, int off, int length, Stream outStream);

		// Token: 0x06003D56 RID: 15702
		int DecodeString(string data, Stream outStream);
	}
}
