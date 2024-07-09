using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002A1 RID: 673
	public class DsaPublicBcpgKey : BcpgObject, IBcpgKey
	{
		// Token: 0x060014F1 RID: 5361 RVA: 0x0006FDC4 File Offset: 0x0006FDC4
		public DsaPublicBcpgKey(BcpgInputStream bcpgIn)
		{
			this.p = new MPInteger(bcpgIn);
			this.q = new MPInteger(bcpgIn);
			this.g = new MPInteger(bcpgIn);
			this.y = new MPInteger(bcpgIn);
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0006FDFC File Offset: 0x0006FDFC
		public DsaPublicBcpgKey(BigInteger p, BigInteger q, BigInteger g, BigInteger y)
		{
			this.p = new MPInteger(p);
			this.q = new MPInteger(q);
			this.g = new MPInteger(g);
			this.y = new MPInteger(y);
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x0006FE38 File Offset: 0x0006FE38
		public string Format
		{
			get
			{
				return "PGP";
			}
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0006FE40 File Offset: 0x0006FE40
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

		// Token: 0x060014F5 RID: 5365 RVA: 0x0006FE74 File Offset: 0x0006FE74
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WriteObjects(new BcpgObject[]
			{
				this.p,
				this.q,
				this.g,
				this.y
			});
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x0006FEC8 File Offset: 0x0006FEC8
		public BigInteger G
		{
			get
			{
				return this.g.Value;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x0006FED8 File Offset: 0x0006FED8
		public BigInteger P
		{
			get
			{
				return this.p.Value;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x0006FEE8 File Offset: 0x0006FEE8
		public BigInteger Q
		{
			get
			{
				return this.q.Value;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x0006FEF8 File Offset: 0x0006FEF8
		public BigInteger Y
		{
			get
			{
				return this.y.Value;
			}
		}

		// Token: 0x04000E1F RID: 3615
		private readonly MPInteger p;

		// Token: 0x04000E20 RID: 3616
		private readonly MPInteger q;

		// Token: 0x04000E21 RID: 3617
		private readonly MPInteger g;

		// Token: 0x04000E22 RID: 3618
		private readonly MPInteger y;
	}
}
