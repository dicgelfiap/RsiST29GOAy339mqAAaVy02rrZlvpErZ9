using System;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Modes.Gcm
{
	// Token: 0x020003F6 RID: 1014
	public class Tables64kGcmMultiplier : IGcmMultiplier
	{
		// Token: 0x06002036 RID: 8246 RVA: 0x000BB4D8 File Offset: 0x000BB4D8
		public void Init(byte[] H)
		{
			if (this.M == null)
			{
				this.M = new uint[16][][];
			}
			else if (Arrays.AreEqual(this.H, H))
			{
				return;
			}
			this.H = Arrays.Clone(H);
			this.M[0] = new uint[256][];
			this.M[0][0] = new uint[4];
			this.M[0][128] = GcmUtilities.AsUints(H);
			for (int i = 64; i >= 1; i >>= 1)
			{
				uint[] array = (uint[])this.M[0][i + i].Clone();
				GcmUtilities.MultiplyP(array);
				this.M[0][i] = array;
			}
			int num = 0;
			for (;;)
			{
				for (int j = 2; j < 256; j += j)
				{
					for (int k = 1; k < j; k++)
					{
						uint[] array2 = (uint[])this.M[num][j].Clone();
						GcmUtilities.Xor(array2, this.M[num][k]);
						this.M[num][j + k] = array2;
					}
				}
				if (++num == 16)
				{
					break;
				}
				this.M[num] = new uint[256][];
				this.M[num][0] = new uint[4];
				for (int l = 128; l > 0; l >>= 1)
				{
					uint[] array3 = (uint[])this.M[num - 1][l].Clone();
					GcmUtilities.MultiplyP8(array3);
					this.M[num][l] = array3;
				}
			}
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x000BB6C0 File Offset: 0x000BB6C0
		public void MultiplyH(byte[] x)
		{
			uint[] array = new uint[4];
			for (int num = 0; num != 16; num++)
			{
				uint[] array2 = this.M[num][(int)x[num]];
				uint[] array3;
				(array3 = array)[0] = (array3[0] ^ array2[0]);
				(array3 = array)[1] = (array3[1] ^ array2[1]);
				(array3 = array)[2] = (array3[2] ^ array2[2]);
				(array3 = array)[3] = (array3[3] ^ array2[3]);
			}
			Pack.UInt32_To_BE(array, x, 0);
		}

		// Token: 0x0400150E RID: 5390
		private byte[] H;

		// Token: 0x0400150F RID: 5391
		private uint[][][] M;
	}
}
