using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x02000341 RID: 833
	public class DHBasicAgreement : IBasicAgreement
	{
		// Token: 0x060018E3 RID: 6371 RVA: 0x0007FD98 File Offset: 0x0007FD98
		public virtual void Init(ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			if (!(parameters is DHPrivateKeyParameters))
			{
				throw new ArgumentException("DHEngine expects DHPrivateKeyParameters");
			}
			this.key = (DHPrivateKeyParameters)parameters;
			this.dhParams = this.key.Parameters;
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x0007FDF4 File Offset: 0x0007FDF4
		public virtual int GetFieldSize()
		{
			return (this.key.Parameters.P.BitLength + 7) / 8;
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x0007FE10 File Offset: 0x0007FE10
		public virtual BigInteger CalculateAgreement(ICipherParameters pubKey)
		{
			if (this.key == null)
			{
				throw new InvalidOperationException("Agreement algorithm not initialised");
			}
			DHPublicKeyParameters dhpublicKeyParameters = (DHPublicKeyParameters)pubKey;
			if (!dhpublicKeyParameters.Parameters.Equals(this.dhParams))
			{
				throw new ArgumentException("Diffie-Hellman public key has wrong parameters.");
			}
			BigInteger p = this.dhParams.P;
			BigInteger y = dhpublicKeyParameters.Y;
			if (y == null || y.CompareTo(BigInteger.One) <= 0 || y.CompareTo(p.Subtract(BigInteger.One)) >= 0)
			{
				throw new ArgumentException("Diffie-Hellman public key is weak");
			}
			BigInteger bigInteger = y.ModPow(this.key.X, p);
			if (bigInteger.Equals(BigInteger.One))
			{
				throw new InvalidOperationException("Shared key can't be 1");
			}
			return bigInteger;
		}

		// Token: 0x04001093 RID: 4243
		private DHPrivateKeyParameters key;

		// Token: 0x04001094 RID: 4244
		private DHParameters dhParams;
	}
}
