using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002B7 RID: 695
	public class RsaSecretBcpgKey : BcpgObject, IBcpgKey
	{
		// Token: 0x06001560 RID: 5472 RVA: 0x00070EFC File Offset: 0x00070EFC
		public RsaSecretBcpgKey(BcpgInputStream bcpgIn)
		{
			this.d = new MPInteger(bcpgIn);
			this.p = new MPInteger(bcpgIn);
			this.q = new MPInteger(bcpgIn);
			this.u = new MPInteger(bcpgIn);
			this.expP = this.d.Value.Remainder(this.p.Value.Subtract(BigInteger.One));
			this.expQ = this.d.Value.Remainder(this.q.Value.Subtract(BigInteger.One));
			this.crt = this.q.Value.ModInverse(this.p.Value);
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x00070FBC File Offset: 0x00070FBC
		public RsaSecretBcpgKey(BigInteger d, BigInteger p, BigInteger q)
		{
			int num = p.CompareTo(q);
			if (num >= 0)
			{
				if (num == 0)
				{
					throw new ArgumentException("p and q cannot be equal");
				}
				BigInteger bigInteger = p;
				p = q;
				q = bigInteger;
			}
			this.d = new MPInteger(d);
			this.p = new MPInteger(p);
			this.q = new MPInteger(q);
			this.u = new MPInteger(p.ModInverse(q));
			this.expP = d.Remainder(p.Subtract(BigInteger.One));
			this.expQ = d.Remainder(q.Subtract(BigInteger.One));
			this.crt = q.ModInverse(p);
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x0007106C File Offset: 0x0007106C
		public BigInteger Modulus
		{
			get
			{
				return this.p.Value.Multiply(this.q.Value);
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x0007108C File Offset: 0x0007108C
		public BigInteger PrivateExponent
		{
			get
			{
				return this.d.Value;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x0007109C File Offset: 0x0007109C
		public BigInteger PrimeP
		{
			get
			{
				return this.p.Value;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x000710AC File Offset: 0x000710AC
		public BigInteger PrimeQ
		{
			get
			{
				return this.q.Value;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x000710BC File Offset: 0x000710BC
		public BigInteger PrimeExponentP
		{
			get
			{
				return this.expP;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x000710C4 File Offset: 0x000710C4
		public BigInteger PrimeExponentQ
		{
			get
			{
				return this.expQ;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x000710CC File Offset: 0x000710CC
		public BigInteger CrtCoefficient
		{
			get
			{
				return this.crt;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x000710D4 File Offset: 0x000710D4
		public string Format
		{
			get
			{
				return "PGP";
			}
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x000710DC File Offset: 0x000710DC
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

		// Token: 0x0600156B RID: 5483 RVA: 0x00071110 File Offset: 0x00071110
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WriteObjects(new BcpgObject[]
			{
				this.d,
				this.p,
				this.q,
				this.u
			});
		}

		// Token: 0x04000E81 RID: 3713
		private readonly MPInteger d;

		// Token: 0x04000E82 RID: 3714
		private readonly MPInteger p;

		// Token: 0x04000E83 RID: 3715
		private readonly MPInteger q;

		// Token: 0x04000E84 RID: 3716
		private readonly MPInteger u;

		// Token: 0x04000E85 RID: 3717
		private readonly BigInteger expP;

		// Token: 0x04000E86 RID: 3718
		private readonly BigInteger expQ;

		// Token: 0x04000E87 RID: 3719
		private readonly BigInteger crt;
	}
}
