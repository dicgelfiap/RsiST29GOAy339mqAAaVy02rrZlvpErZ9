using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002B6 RID: 694
	public class RsaPublicBcpgKey : BcpgObject, IBcpgKey
	{
		// Token: 0x06001559 RID: 5465 RVA: 0x00070E24 File Offset: 0x00070E24
		public RsaPublicBcpgKey(BcpgInputStream bcpgIn)
		{
			this.n = new MPInteger(bcpgIn);
			this.e = new MPInteger(bcpgIn);
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x00070E44 File Offset: 0x00070E44
		public RsaPublicBcpgKey(BigInteger n, BigInteger e)
		{
			this.n = new MPInteger(n);
			this.e = new MPInteger(e);
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x00070E64 File Offset: 0x00070E64
		public BigInteger PublicExponent
		{
			get
			{
				return this.e.Value;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x00070E74 File Offset: 0x00070E74
		public BigInteger Modulus
		{
			get
			{
				return this.n.Value;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x00070E84 File Offset: 0x00070E84
		public string Format
		{
			get
			{
				return "PGP";
			}
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x00070E8C File Offset: 0x00070E8C
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

		// Token: 0x0600155F RID: 5471 RVA: 0x00070EC0 File Offset: 0x00070EC0
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WriteObjects(new BcpgObject[]
			{
				this.n,
				this.e
			});
		}

		// Token: 0x04000E7F RID: 3711
		private readonly MPInteger n;

		// Token: 0x04000E80 RID: 3712
		private readonly MPInteger e;
	}
}
