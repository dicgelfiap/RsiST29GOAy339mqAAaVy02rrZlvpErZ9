using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200029C RID: 668
	public class CompressedDataPacket : InputStreamPacket
	{
		// Token: 0x060014E6 RID: 5350 RVA: 0x0006FCD8 File Offset: 0x0006FCD8
		internal CompressedDataPacket(BcpgInputStream bcpgIn) : base(bcpgIn)
		{
			this.algorithm = (CompressionAlgorithmTag)bcpgIn.ReadByte();
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x0006FCF0 File Offset: 0x0006FCF0
		public CompressionAlgorithmTag Algorithm
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x04000E16 RID: 3606
		private readonly CompressionAlgorithmTag algorithm;
	}
}
