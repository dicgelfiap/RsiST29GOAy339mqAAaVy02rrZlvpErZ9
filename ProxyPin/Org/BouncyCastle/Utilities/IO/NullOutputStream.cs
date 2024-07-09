using System;

namespace Org.BouncyCastle.Utilities.IO
{
	// Token: 0x020006E7 RID: 1767
	internal class NullOutputStream : BaseOutputStream
	{
		// Token: 0x06003DA1 RID: 15777 RVA: 0x00151150 File Offset: 0x00151150
		public override void WriteByte(byte b)
		{
		}

		// Token: 0x06003DA2 RID: 15778 RVA: 0x00151154 File Offset: 0x00151154
		public override void Write(byte[] buffer, int offset, int count)
		{
		}
	}
}
