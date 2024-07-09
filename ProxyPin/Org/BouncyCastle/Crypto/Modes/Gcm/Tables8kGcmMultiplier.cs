using System;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Modes.Gcm
{
	// Token: 0x020003F7 RID: 1015
	public class Tables8kGcmMultiplier : IGcmMultiplier
	{
		// Token: 0x06002039 RID: 8249 RVA: 0x000BB740 File Offset: 0x000BB740
		public void Init(byte[] H)
		{
			if (this.M == null)
			{
				this.M = new uint[32][][];
			}
			else if (Arrays.AreEqual(this.H, H))
			{
				return;
			}
			this.H = Arrays.Clone(H);
			this.M[0] = new uint[16][];
			this.M[1] = new uint[16][];
			this.M[0][0] = new uint[4];
			this.M[1][0] = new uint[4];
			this.M[1][8] = GcmUtilities.AsUints(H);
			for (int i = 4; i >= 1; i >>= 1)
			{
				uint[] array = (uint[])this.M[1][i + i].Clone();
				GcmUtilities.MultiplyP(array);
				this.M[1][i] = array;
			}
			uint[] array2 = (uint[])this.M[1][1].Clone();
			GcmUtilities.MultiplyP(array2);
			this.M[0][8] = array2;
			for (int j = 4; j >= 1; j >>= 1)
			{
				uint[] array3 = (uint[])this.M[0][j + j].Clone();
				GcmUtilities.MultiplyP(array3);
				this.M[0][j] = array3;
			}
			int num = 0;
			for (;;)
			{
				for (int k = 2; k < 16; k += k)
				{
					for (int l = 1; l < k; l++)
					{
						uint[] array4 = (uint[])this.M[num][k].Clone();
						GcmUtilities.Xor(array4, this.M[num][l]);
						this.M[num][k + l] = array4;
					}
				}
				if (++num == 32)
				{
					break;
				}
				if (num > 1)
				{
					this.M[num] = new uint[16][];
					this.M[num][0] = new uint[4];
					for (int m = 8; m > 0; m >>= 1)
					{
						uint[] array5 = (uint[])this.M[num - 2][m].Clone();
						GcmUtilities.MultiplyP8(array5);
						this.M[num][m] = array5;
					}
				}
			}
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x000BB9DC File Offset: 0x000BB9DC
		public void MultiplyH(byte[] x)
		{
			uint[] array = new uint[4];
			for (int i = 15; i >= 0; i--)
			{
				uint[] array2 = this.M[i + i][(int)(x[i] & 15)];
				uint[] array3;
				(array3 = array)[0] = (array3[0] ^ array2[0]);
				(array3 = array)[1] = (array3[1] ^ array2[1]);
				(array3 = array)[2] = (array3[2] ^ array2[2]);
				(array3 = array)[3] = (array3[3] ^ array2[3]);
				array2 = this.M[i + i + 1][(x[i] & 240) >> 4];
				(array3 = array)[0] = (array3[0] ^ array2[0]);
				(array3 = array)[1] = (array3[1] ^ array2[1]);
				(array3 = array)[2] = (array3[2] ^ array2[2]);
				(array3 = array)[3] = (array3[3] ^ array2[3]);
			}
			Pack.UInt32_To_BE(array, x, 0);
		}

		// Token: 0x04001510 RID: 5392
		private byte[] H;

		// Token: 0x04001511 RID: 5393
		private uint[][][] M;
	}
}
