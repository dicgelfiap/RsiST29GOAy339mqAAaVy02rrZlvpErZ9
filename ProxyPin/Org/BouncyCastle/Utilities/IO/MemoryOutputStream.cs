using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.IO
{
	// Token: 0x020006E6 RID: 1766
	public class MemoryOutputStream : MemoryStream
	{
		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06003D9F RID: 15775 RVA: 0x00151144 File Offset: 0x00151144
		public sealed override bool CanRead
		{
			get
			{
				return false;
			}
		}
	}
}
