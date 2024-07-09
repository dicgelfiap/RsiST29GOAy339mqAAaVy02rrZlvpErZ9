using System;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x0200033F RID: 831
	public class DHAgreement
	{
		// Token: 0x060018DC RID: 6364 RVA: 0x0007FBC4 File Offset: 0x0007FBC4
		public void Init(ICipherParameters parameters)
		{
			AsymmetricKeyParameter asymmetricKeyParameter;
			if (parameters is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
				this.random = parametersWithRandom.Random;
				asymmetricKeyParameter = (AsymmetricKeyParameter)parametersWithRandom.Parameters;
			}
			else
			{
				this.random = new SecureRandom();
				asymmetricKeyParameter = (AsymmetricKeyParameter)parameters;
			}
			if (!(asymmetricKeyParameter is DHPrivateKeyParameters))
			{
				throw new ArgumentException("DHEngine expects DHPrivateKeyParameters");
			}
			this.key = (DHPrivateKeyParameters)asymmetricKeyParameter;
			this.dhParams = this.key.Parameters;
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x0007FC4C File Offset: 0x0007FC4C
		public BigInteger CalculateMessage()
		{
			DHKeyPairGenerator dhkeyPairGenerator = new DHKeyPairGenerator();
			dhkeyPairGenerator.Init(new DHKeyGenerationParameters(this.random, this.dhParams));
			AsymmetricCipherKeyPair asymmetricCipherKeyPair = dhkeyPairGenerator.GenerateKeyPair();
			this.privateValue = ((DHPrivateKeyParameters)asymmetricCipherKeyPair.Private).X;
			return ((DHPublicKeyParameters)asymmetricCipherKeyPair.Public).Y;
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0007FCA8 File Offset: 0x0007FCA8
		public BigInteger CalculateAgreement(DHPublicKeyParameters pub, BigInteger message)
		{
			if (pub == null)
			{
				throw new ArgumentNullException("pub");
			}
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			if (!pub.Parameters.Equals(this.dhParams))
			{
				throw new ArgumentException("Diffie-Hellman public key has wrong parameters.");
			}
			BigInteger p = this.dhParams.P;
			BigInteger y = pub.Y;
			if (y == null || y.CompareTo(BigInteger.One) <= 0 || y.CompareTo(p.Subtract(BigInteger.One)) >= 0)
			{
				throw new ArgumentException("Diffie-Hellman public key is weak");
			}
			BigInteger bigInteger = y.ModPow(this.privateValue, p);
			if (bigInteger.Equals(BigInteger.One))
			{
				throw new InvalidOperationException("Shared key can't be 1");
			}
			return message.ModPow(this.key.X, p).Multiply(bigInteger).Mod(p);
		}

		// Token: 0x0400108F RID: 4239
		private DHPrivateKeyParameters key;

		// Token: 0x04001090 RID: 4240
		private DHParameters dhParams;

		// Token: 0x04001091 RID: 4241
		private BigInteger privateValue;

		// Token: 0x04001092 RID: 4242
		private SecureRandom random;
	}
}
