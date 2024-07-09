using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Agreement.Srp
{
	// Token: 0x0200033B RID: 827
	public class Srp6Server
	{
		// Token: 0x060018BF RID: 6335 RVA: 0x0007F504 File Offset: 0x0007F504
		public virtual void Init(BigInteger N, BigInteger g, BigInteger v, IDigest digest, SecureRandom random)
		{
			this.N = N;
			this.g = g;
			this.v = v;
			this.random = random;
			this.digest = digest;
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0007F52C File Offset: 0x0007F52C
		public virtual void Init(Srp6GroupParameters group, BigInteger v, IDigest digest, SecureRandom random)
		{
			this.Init(group.N, group.G, v, digest, random);
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0007F554 File Offset: 0x0007F554
		public virtual BigInteger GenerateServerCredentials()
		{
			BigInteger bigInteger = Srp6Utilities.CalculateK(this.digest, this.N, this.g);
			this.privB = this.SelectPrivateValue();
			this.pubB = bigInteger.Multiply(this.v).Mod(this.N).Add(this.g.ModPow(this.privB, this.N)).Mod(this.N);
			return this.pubB;
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0007F5D4 File Offset: 0x0007F5D4
		public virtual BigInteger CalculateSecret(BigInteger clientA)
		{
			this.A = Srp6Utilities.ValidatePublicValue(this.N, clientA);
			this.u = Srp6Utilities.CalculateU(this.digest, this.N, this.A, this.pubB);
			this.S = this.CalculateS();
			return this.S;
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x0007F62C File Offset: 0x0007F62C
		protected virtual BigInteger SelectPrivateValue()
		{
			return Srp6Utilities.GeneratePrivateValue(this.digest, this.N, this.g, this.random);
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0007F64C File Offset: 0x0007F64C
		private BigInteger CalculateS()
		{
			return this.v.ModPow(this.u, this.N).Multiply(this.A).Mod(this.N).ModPow(this.privB, this.N);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0007F69C File Offset: 0x0007F69C
		public virtual bool VerifyClientEvidenceMessage(BigInteger clientM1)
		{
			if (this.A == null || this.pubB == null || this.S == null)
			{
				throw new CryptoException("Impossible to compute and verify M1: some data are missing from the previous operations (A,B,S)");
			}
			BigInteger bigInteger = Srp6Utilities.CalculateM1(this.digest, this.N, this.A, this.pubB, this.S);
			if (bigInteger.Equals(clientM1))
			{
				this.M1 = clientM1;
				return true;
			}
			return false;
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0007F714 File Offset: 0x0007F714
		public virtual BigInteger CalculateServerEvidenceMessage()
		{
			if (this.A == null || this.M1 == null || this.S == null)
			{
				throw new CryptoException("Impossible to compute M2: some data are missing from the previous operations (A,M1,S)");
			}
			this.M2 = Srp6Utilities.CalculateM2(this.digest, this.N, this.A, this.M1, this.S);
			return this.M2;
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x0007F780 File Offset: 0x0007F780
		public virtual BigInteger CalculateSessionKey()
		{
			if (this.S == null || this.M1 == null || this.M2 == null)
			{
				throw new CryptoException("Impossible to compute Key: some data are missing from the previous operations (S,M1,M2)");
			}
			this.Key = Srp6Utilities.CalculateKey(this.digest, this.N, this.S);
			return this.Key;
		}

		// Token: 0x0400106A RID: 4202
		protected BigInteger N;

		// Token: 0x0400106B RID: 4203
		protected BigInteger g;

		// Token: 0x0400106C RID: 4204
		protected BigInteger v;

		// Token: 0x0400106D RID: 4205
		protected SecureRandom random;

		// Token: 0x0400106E RID: 4206
		protected IDigest digest;

		// Token: 0x0400106F RID: 4207
		protected BigInteger A;

		// Token: 0x04001070 RID: 4208
		protected BigInteger privB;

		// Token: 0x04001071 RID: 4209
		protected BigInteger pubB;

		// Token: 0x04001072 RID: 4210
		protected BigInteger u;

		// Token: 0x04001073 RID: 4211
		protected BigInteger S;

		// Token: 0x04001074 RID: 4212
		protected BigInteger M1;

		// Token: 0x04001075 RID: 4213
		protected BigInteger M2;

		// Token: 0x04001076 RID: 4214
		protected BigInteger Key;
	}
}
