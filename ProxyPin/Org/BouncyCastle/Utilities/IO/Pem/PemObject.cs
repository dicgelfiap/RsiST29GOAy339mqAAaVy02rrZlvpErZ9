using System;
using System.Collections;

namespace Org.BouncyCastle.Utilities.IO.Pem
{
	// Token: 0x020006E3 RID: 1763
	public class PemObject : PemObjectGenerator
	{
		// Token: 0x06003D96 RID: 15766 RVA: 0x001510E4 File Offset: 0x001510E4
		public PemObject(string type, byte[] content) : this(type, Platform.CreateArrayList(), content)
		{
		}

		// Token: 0x06003D97 RID: 15767 RVA: 0x001510F4 File Offset: 0x001510F4
		public PemObject(string type, IList headers, byte[] content)
		{
			this.type = type;
			this.headers = Platform.CreateArrayList(headers);
			this.content = content;
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06003D98 RID: 15768 RVA: 0x00151118 File Offset: 0x00151118
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06003D99 RID: 15769 RVA: 0x00151120 File Offset: 0x00151120
		public IList Headers
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06003D9A RID: 15770 RVA: 0x00151128 File Offset: 0x00151128
		public byte[] Content
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x06003D9B RID: 15771 RVA: 0x00151130 File Offset: 0x00151130
		public PemObject Generate()
		{
			return this;
		}

		// Token: 0x04001EFC RID: 7932
		private string type;

		// Token: 0x04001EFD RID: 7933
		private IList headers;

		// Token: 0x04001EFE RID: 7934
		private byte[] content;
	}
}
