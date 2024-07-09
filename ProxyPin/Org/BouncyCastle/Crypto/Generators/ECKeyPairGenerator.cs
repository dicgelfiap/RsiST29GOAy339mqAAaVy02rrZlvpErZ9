using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto.EC;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003BE RID: 958
	public class ECKeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x06001E77 RID: 7799 RVA: 0x000B246C File Offset: 0x000B246C
		public ECKeyPairGenerator() : this("EC")
		{
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x000B247C File Offset: 0x000B247C
		public ECKeyPairGenerator(string algorithm)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			this.algorithm = ECKeyParameters.VerifyAlgorithmName(algorithm);
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x000B24A4 File Offset: 0x000B24A4
		public void Init(KeyGenerationParameters parameters)
		{
			if (parameters is ECKeyGenerationParameters)
			{
				ECKeyGenerationParameters eckeyGenerationParameters = (ECKeyGenerationParameters)parameters;
				this.publicKeyParamSet = eckeyGenerationParameters.PublicKeyParamSet;
				this.parameters = eckeyGenerationParameters.DomainParameters;
			}
			else
			{
				int strength = parameters.Strength;
				DerObjectIdentifier oid;
				if (strength <= 239)
				{
					if (strength == 192)
					{
						oid = X9ObjectIdentifiers.Prime192v1;
						goto IL_DA;
					}
					if (strength == 224)
					{
						oid = SecObjectIdentifiers.SecP224r1;
						goto IL_DA;
					}
					if (strength == 239)
					{
						oid = X9ObjectIdentifiers.Prime239v1;
						goto IL_DA;
					}
				}
				else
				{
					if (strength == 256)
					{
						oid = X9ObjectIdentifiers.Prime256v1;
						goto IL_DA;
					}
					if (strength == 384)
					{
						oid = SecObjectIdentifiers.SecP384r1;
						goto IL_DA;
					}
					if (strength == 521)
					{
						oid = SecObjectIdentifiers.SecP521r1;
						goto IL_DA;
					}
				}
				throw new InvalidParameterException("unknown key size.");
				IL_DA:
				X9ECParameters x9ECParameters = ECKeyPairGenerator.FindECCurveByOid(oid);
				this.publicKeyParamSet = oid;
				this.parameters = new ECDomainParameters(x9ECParameters.Curve, x9ECParameters.G, x9ECParameters.N, x9ECParameters.H, x9ECParameters.GetSeed());
			}
			this.random = parameters.Random;
			if (this.random == null)
			{
				this.random = new SecureRandom();
			}
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x000B25E8 File Offset: 0x000B25E8
		public AsymmetricCipherKeyPair GenerateKeyPair()
		{
			BigInteger n = this.parameters.N;
			int num = n.BitLength >> 2;
			BigInteger bigInteger;
			do
			{
				bigInteger = new BigInteger(n.BitLength, this.random);
			}
			while (bigInteger.CompareTo(BigInteger.One) < 0 || bigInteger.CompareTo(n) >= 0 || WNafUtilities.GetNafWeight(bigInteger) < num);
			ECPoint q = this.CreateBasePointMultiplier().Multiply(this.parameters.G, bigInteger);
			if (this.publicKeyParamSet != null)
			{
				return new AsymmetricCipherKeyPair(new ECPublicKeyParameters(this.algorithm, q, this.publicKeyParamSet), new ECPrivateKeyParameters(this.algorithm, bigInteger, this.publicKeyParamSet));
			}
			return new AsymmetricCipherKeyPair(new ECPublicKeyParameters(this.algorithm, q, this.parameters), new ECPrivateKeyParameters(this.algorithm, bigInteger, this.parameters));
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x000B26B8 File Offset: 0x000B26B8
		protected virtual ECMultiplier CreateBasePointMultiplier()
		{
			return new FixedPointCombMultiplier();
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x000B26C0 File Offset: 0x000B26C0
		internal static X9ECParameters FindECCurveByOid(DerObjectIdentifier oid)
		{
			X9ECParameters byOid = CustomNamedCurves.GetByOid(oid);
			if (byOid == null)
			{
				byOid = ECNamedCurveTable.GetByOid(oid);
			}
			return byOid;
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x000B26E8 File Offset: 0x000B26E8
		internal static ECPublicKeyParameters GetCorrespondingPublicKey(ECPrivateKeyParameters privKey)
		{
			ECDomainParameters ecdomainParameters = privKey.Parameters;
			ECPoint q = new FixedPointCombMultiplier().Multiply(ecdomainParameters.G, privKey.D);
			if (privKey.PublicKeyParamSet != null)
			{
				return new ECPublicKeyParameters(privKey.AlgorithmName, q, privKey.PublicKeyParamSet);
			}
			return new ECPublicKeyParameters(privKey.AlgorithmName, q, ecdomainParameters);
		}

		// Token: 0x0400142B RID: 5163
		private readonly string algorithm;

		// Token: 0x0400142C RID: 5164
		private ECDomainParameters parameters;

		// Token: 0x0400142D RID: 5165
		private DerObjectIdentifier publicKeyParamSet;

		// Token: 0x0400142E RID: 5166
		private SecureRandom random;
	}
}
