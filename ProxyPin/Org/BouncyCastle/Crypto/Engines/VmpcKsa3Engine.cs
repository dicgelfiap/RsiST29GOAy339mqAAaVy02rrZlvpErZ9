using System;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020003AE RID: 942
	public class VmpcKsa3Engine : VmpcEngine
	{
		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001E20 RID: 7712 RVA: 0x000B0594 File Offset: 0x000B0594
		public override string AlgorithmName
		{
			get
			{
				return "VMPC-KSA3";
			}
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x000B059C File Offset: 0x000B059C
		protected override void InitKey(byte[] keyBytes, byte[] ivBytes)
		{
			this.s = 0;
			this.P = new byte[256];
			for (int i = 0; i < 256; i++)
			{
				this.P[i] = (byte)i;
			}
			for (int j = 0; j < 768; j++)
			{
				this.s = this.P[(int)(this.s + this.P[j & 255] + keyBytes[j % keyBytes.Length] & byte.MaxValue)];
				byte b = this.P[j & 255];
				this.P[j & 255] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b;
			}
			for (int k = 0; k < 768; k++)
			{
				this.s = this.P[(int)(this.s + this.P[k & 255] + ivBytes[k % ivBytes.Length] & byte.MaxValue)];
				byte b2 = this.P[k & 255];
				this.P[k & 255] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b2;
			}
			for (int l = 0; l < 768; l++)
			{
				this.s = this.P[(int)(this.s + this.P[l & 255] + keyBytes[l % keyBytes.Length] & byte.MaxValue)];
				byte b3 = this.P[l & 255];
				this.P[l & 255] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b3;
			}
			this.n = 0;
		}
	}
}
