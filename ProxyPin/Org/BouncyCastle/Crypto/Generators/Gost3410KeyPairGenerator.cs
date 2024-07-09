using System;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003C3 RID: 963
	public class Gost3410KeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x06001E8A RID: 7818 RVA: 0x000B28C4 File Offset: 0x000B28C4
		public void Init(KeyGenerationParameters parameters)
		{
			if (parameters is Gost3410KeyGenerationParameters)
			{
				this.param = (Gost3410KeyGenerationParameters)parameters;
				return;
			}
			Gost3410KeyGenerationParameters gost3410KeyGenerationParameters = new Gost3410KeyGenerationParameters(parameters.Random, CryptoProObjectIdentifiers.GostR3410x94CryptoProA);
			int strength = parameters.Strength;
			int num = gost3410KeyGenerationParameters.Parameters.P.BitLength - 1;
			this.param = gost3410KeyGenerationParameters;
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x000B2920 File Offset: 0x000B2920
		public AsymmetricCipherKeyPair GenerateKeyPair()
		{
			SecureRandom random = this.param.Random;
			Gost3410Parameters parameters = this.param.Parameters;
			BigInteger q = parameters.Q;
			int num = 64;
			BigInteger bigInteger;
			do
			{
				bigInteger = new BigInteger(256, random);
			}
			while (bigInteger.SignValue < 1 || bigInteger.CompareTo(q) >= 0 || WNafUtilities.GetNafWeight(bigInteger) < num);
			BigInteger p = parameters.P;
			BigInteger a = parameters.A;
			BigInteger y = a.ModPow(bigInteger, p);
			if (this.param.PublicKeyParamSet != null)
			{
				return new AsymmetricCipherKeyPair(new Gost3410PublicKeyParameters(y, this.param.PublicKeyParamSet), new Gost3410PrivateKeyParameters(bigInteger, this.param.PublicKeyParamSet));
			}
			return new AsymmetricCipherKeyPair(new Gost3410PublicKeyParameters(y, parameters), new Gost3410PrivateKeyParameters(bigInteger, parameters));
		}

		// Token: 0x04001435 RID: 5173
		private Gost3410KeyGenerationParameters param;
	}
}
