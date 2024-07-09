using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000569 RID: 1385
	public class SimpleBlockResult : IBlockResult
	{
		// Token: 0x06002AFC RID: 11004 RVA: 0x000E5230 File Offset: 0x000E5230
		public SimpleBlockResult(byte[] result)
		{
			this.result = result;
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06002AFD RID: 11005 RVA: 0x000E5240 File Offset: 0x000E5240
		public int Length
		{
			get
			{
				return this.result.Length;
			}
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x000E524C File Offset: 0x000E524C
		public byte[] Collect()
		{
			return this.result;
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x000E5254 File Offset: 0x000E5254
		public int Collect(byte[] destination, int offset)
		{
			Array.Copy(this.result, 0, destination, offset, this.result.Length);
			return this.result.Length;
		}

		// Token: 0x04001B3D RID: 6973
		private readonly byte[] result;
	}
}
