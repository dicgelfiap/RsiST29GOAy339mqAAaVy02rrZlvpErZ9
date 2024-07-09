using System;

namespace Org.BouncyCastle.Crmf
{
	// Token: 0x02000321 RID: 801
	public interface IEncryptedValuePadder
	{
		// Token: 0x0600182F RID: 6191
		byte[] GetPaddedData(byte[] data);

		// Token: 0x06001830 RID: 6192
		byte[] GetUnpaddedData(byte[] paddedData);
	}
}
