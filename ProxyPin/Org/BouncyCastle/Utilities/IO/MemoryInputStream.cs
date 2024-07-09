using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.IO
{
	// Token: 0x020006E5 RID: 1765
	public class MemoryInputStream : MemoryStream
	{
		// Token: 0x06003D9D RID: 15773 RVA: 0x00151134 File Offset: 0x00151134
		public MemoryInputStream(byte[] buffer) : base(buffer, false)
		{
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06003D9E RID: 15774 RVA: 0x00151140 File Offset: 0x00151140
		public sealed override bool CanWrite
		{
			get
			{
				return false;
			}
		}
	}
}
