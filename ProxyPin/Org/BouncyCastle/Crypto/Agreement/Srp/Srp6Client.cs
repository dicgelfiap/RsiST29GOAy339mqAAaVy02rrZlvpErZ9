using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Agreement.Srp
{
	// Token: 0x0200033A RID: 826
	public class Srp6Client
	{
		// Token: 0x060018B5 RID: 6325 RVA: 0x0007F208 File Offset: 0x0007F208
		public virtual void Init(BigInteger N, BigInteger g, IDigest digest, SecureRandom random)
		{
			this.N = N;
			this.g = g;
			this.digest = digest;
			this.random = random;
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0007F228 File Offset: 0x0007F228
		public virtual void Init(Srp6GroupParameters group, IDigest digest, SecureRandom random)
		{
			this.Init(group.N, group.G, digest, random);
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x0007F250 File Offset: 0x0007F250
		public virtual BigInteger GenerateClientCredentials(byte[] salt, byte[] identity, byte[] password)
		{
			this.x = Srp6Utilities.CalculateX(this.digest, this.N, salt, identity, password);
			this.privA = this.SelectPrivateValue();
			this.pubA = this.g.ModPow(this.privA, this.N);
			return this.pubA;
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x0007F2AC File Offset: 0x0007F2AC
		public virtual BigInteger CalculateSecret(BigInteger serverB)
		{
			this.B = Srp6Utilities.ValidatePublicValue(this.N, serverB);
			this.u = Srp6Utilities.CalculateU(this.digest, this.N, this.pubA, this.B);
			this.S = this.CalculateS();
			return this.S;
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x0007F304 File Offset: 0x0007F304
		protected virtual BigInteger SelectPrivateValue()
		{
			return Srp6Utilities.GeneratePrivateValue(this.digest, this.N, this.g, this.random);
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x0007F324 File Offset: 0x0007F324
		private BigInteger CalculateS()
		{
			BigInteger val = Srp6Utilities.CalculateK(this.digest, this.N, this.g);
			BigInteger e = this.u.Multiply(this.x).Add(this.privA);
			BigInteger n = this.g.ModPow(this.x, this.N).Multiply(val).Mod(this.N);
			return this.B.Subtract(n).Mod(this.N).ModPow(e, this.N);
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x0007F3B8 File Offset: 0x0007F3B8
		public virtual BigInteger CalculateClientEvidenceMessage()
		{
			if (this.pubA == null || this.B == null || this.S == null)
			{
				throw new CryptoException("Impossible to compute M1: some data are missing from the previous operations (A,B,S)");
			}
			this.M1 = Srp6Utilities.CalculateM1(this.digest, this.N, this.pubA, this.B, this.S);
			return this.M1;
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x0007F424 File Offset: 0x0007F424
		public virtual bool VerifyServerEvidenceMessage(BigInteger serverM2)
		{
			if (this.pubA == null || this.M1 == null || this.S == null)
			{
				throw new CryptoException("Impossible to compute and verify M2: some data are missing from the previous operations (A,M1,S)");
			}
			BigInteger bigInteger = Srp6Utilities.CalculateM2(this.digest, this.N, this.pubA, this.M1, this.S);
			if (bigInteger.Equals(serverM2))
			{
				this.M2 = serverM2;
				return true;
			}
			return false;
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0007F49C File Offset: 0x0007F49C
		public virtual BigInteger CalculateSessionKey()
		{
			if (this.S == null || this.M1 == null || this.M2 == null)
			{
				throw new CryptoException("Impossible to compute Key: some data are missing from the previous operations (S,M1,M2)");
			}
			this.Key = Srp6Utilities.CalculateKey(this.digest, this.N, this.S);
			return this.Key;
		}

		// Token: 0x0400105D RID: 4189
		protected BigInteger N;

		// Token: 0x0400105E RID: 4190
		protected BigInteger g;

		// Token: 0x0400105F RID: 4191
		protected BigInteger privA;

		// Token: 0x04001060 RID: 4192
		protected BigInteger pubA;

		// Token: 0x04001061 RID: 4193
		protected BigInteger B;

		// Token: 0x04001062 RID: 4194
		protected BigInteger x;

		// Token: 0x04001063 RID: 4195
		protected BigInteger u;

		// Token: 0x04001064 RID: 4196
		protected BigInteger S;

		// Token: 0x04001065 RID: 4197
		protected BigInteger M1;

		// Token: 0x04001066 RID: 4198
		protected BigInteger M2;

		// Token: 0x04001067 RID: 4199
		protected BigInteger Key;

		// Token: 0x04001068 RID: 4200
		protected IDigest digest;

		// Token: 0x04001069 RID: 4201
		protected SecureRandom random;
	}
}
