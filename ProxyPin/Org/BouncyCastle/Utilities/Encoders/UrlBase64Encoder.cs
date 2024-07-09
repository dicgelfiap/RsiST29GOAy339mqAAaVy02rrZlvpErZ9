using System;

namespace Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x020006E0 RID: 1760
	public class UrlBase64Encoder : Base64Encoder
	{
		// Token: 0x06003D8C RID: 15756 RVA: 0x00150FD8 File Offset: 0x00150FD8
		public UrlBase64Encoder()
		{
			this.encodingTable[this.encodingTable.Length - 2] = 45;
			this.encodingTable[this.encodingTable.Length - 1] = 95;
			this.padding = 46;
			base.InitialiseDecodingTable();
		}
	}
}
