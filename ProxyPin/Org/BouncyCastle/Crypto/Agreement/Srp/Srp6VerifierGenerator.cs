using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Agreement.Srp
{
	// Token: 0x0200033E RID: 830
	public class Srp6VerifierGenerator
	{
		// Token: 0x060018D9 RID: 6361 RVA: 0x0007FB50 File Offset: 0x0007FB50
		public virtual void Init(BigInteger N, BigInteger g, IDigest digest)
		{
			this.N = N;
			this.g = g;
			this.digest = digest;
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0007FB68 File Offset: 0x0007FB68
		public virtual void Init(Srp6GroupParameters group, IDigest digest)
		{
			this.Init(group.N, group.G, digest);
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x0007FB8C File Offset: 0x0007FB8C
		public virtual BigInteger GenerateVerifier(byte[] salt, byte[] identity, byte[] password)
		{
			BigInteger e = Srp6Utilities.CalculateX(this.digest, this.N, salt, identity, password);
			return this.g.ModPow(e, this.N);
		}

		// Token: 0x0400108C RID: 4236
		protected BigInteger N;

		// Token: 0x0400108D RID: 4237
		protected BigInteger g;

		// Token: 0x0400108E RID: 4238
		protected IDigest digest;
	}
}
