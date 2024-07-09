using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002A7 RID: 679
	public class ElGamalPublicBcpgKey : BcpgObject, IBcpgKey
	{
		// Token: 0x0600151A RID: 5402 RVA: 0x000702F4 File Offset: 0x000702F4
		public ElGamalPublicBcpgKey(BcpgInputStream bcpgIn)
		{
			this.p = new MPInteger(bcpgIn);
			this.g = new MPInteger(bcpgIn);
			this.y = new MPInteger(bcpgIn);
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x00070320 File Offset: 0x00070320
		public ElGamalPublicBcpgKey(BigInteger p, BigInteger g, BigInteger y)
		{
			this.p = new MPInteger(p);
			this.g = new MPInteger(g);
			this.y = new MPInteger(y);
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x0007034C File Offset: 0x0007034C
		public string Format
		{
			get
			{
				return "PGP";
			}
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x00070354 File Offset: 0x00070354
		public override byte[] GetEncoded()
		{
			byte[] result;
			try
			{
				result = base.GetEncoded();
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x00070388 File Offset: 0x00070388
		public BigInteger P
		{
			get
			{
				return this.p.Value;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x00070398 File Offset: 0x00070398
		public BigInteger G
		{
			get
			{
				return this.g.Value;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x000703A8 File Offset: 0x000703A8
		public BigInteger Y
		{
			get
			{
				return this.y.Value;
			}
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x000703B8 File Offset: 0x000703B8
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WriteObjects(new BcpgObject[]
			{
				this.p,
				this.g,
				this.y
			});
		}

		// Token: 0x04000E2A RID: 3626
		internal MPInteger p;

		// Token: 0x04000E2B RID: 3627
		internal MPInteger g;

		// Token: 0x04000E2C RID: 3628
		internal MPInteger y;
	}
}
