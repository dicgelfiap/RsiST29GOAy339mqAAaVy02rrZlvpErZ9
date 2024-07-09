using System;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x0200049C RID: 1180
	public class ECNRSigner : IDsaExt, IDsa
	{
		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06002451 RID: 9297 RVA: 0x000CA5F8 File Offset: 0x000CA5F8
		public virtual string AlgorithmName
		{
			get
			{
				return "ECNR";
			}
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x000CA600 File Offset: 0x000CA600
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			this.forSigning = forSigning;
			if (forSigning)
			{
				if (parameters is ParametersWithRandom)
				{
					ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
					this.random = parametersWithRandom.Random;
					parameters = parametersWithRandom.Parameters;
				}
				else
				{
					this.random = new SecureRandom();
				}
				if (!(parameters is ECPrivateKeyParameters))
				{
					throw new InvalidKeyException("EC private key required for signing");
				}
				this.key = (ECPrivateKeyParameters)parameters;
				return;
			}
			else
			{
				if (!(parameters is ECPublicKeyParameters))
				{
					throw new InvalidKeyException("EC public key required for verification");
				}
				this.key = (ECPublicKeyParameters)parameters;
				return;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06002453 RID: 9299 RVA: 0x000CA69C File Offset: 0x000CA69C
		public virtual BigInteger Order
		{
			get
			{
				return this.key.Parameters.N;
			}
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x000CA6B0 File Offset: 0x000CA6B0
		public virtual BigInteger[] GenerateSignature(byte[] message)
		{
			if (!this.forSigning)
			{
				throw new InvalidOperationException("not initialised for signing");
			}
			BigInteger order = this.Order;
			int bitLength = order.BitLength;
			BigInteger bigInteger = new BigInteger(1, message);
			int bitLength2 = bigInteger.BitLength;
			ECPrivateKeyParameters ecprivateKeyParameters = (ECPrivateKeyParameters)this.key;
			if (bitLength2 > bitLength)
			{
				throw new DataLengthException("input too large for ECNR key.");
			}
			AsymmetricCipherKeyPair asymmetricCipherKeyPair;
			BigInteger bigInteger3;
			do
			{
				ECKeyPairGenerator eckeyPairGenerator = new ECKeyPairGenerator();
				eckeyPairGenerator.Init(new ECKeyGenerationParameters(ecprivateKeyParameters.Parameters, this.random));
				asymmetricCipherKeyPair = eckeyPairGenerator.GenerateKeyPair();
				ECPublicKeyParameters ecpublicKeyParameters = (ECPublicKeyParameters)asymmetricCipherKeyPair.Public;
				BigInteger bigInteger2 = ecpublicKeyParameters.Q.AffineXCoord.ToBigInteger();
				bigInteger3 = bigInteger2.Add(bigInteger).Mod(order);
			}
			while (bigInteger3.SignValue == 0);
			BigInteger d = ecprivateKeyParameters.D;
			BigInteger d2 = ((ECPrivateKeyParameters)asymmetricCipherKeyPair.Private).D;
			BigInteger bigInteger4 = d2.Subtract(bigInteger3.Multiply(d)).Mod(order);
			return new BigInteger[]
			{
				bigInteger3,
				bigInteger4
			};
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x000CA7D0 File Offset: 0x000CA7D0
		public virtual bool VerifySignature(byte[] message, BigInteger r, BigInteger s)
		{
			if (this.forSigning)
			{
				throw new InvalidOperationException("not initialised for verifying");
			}
			ECPublicKeyParameters ecpublicKeyParameters = (ECPublicKeyParameters)this.key;
			BigInteger n = ecpublicKeyParameters.Parameters.N;
			int bitLength = n.BitLength;
			BigInteger bigInteger = new BigInteger(1, message);
			int bitLength2 = bigInteger.BitLength;
			if (bitLength2 > bitLength)
			{
				throw new DataLengthException("input too large for ECNR key.");
			}
			if (r.CompareTo(BigInteger.One) < 0 || r.CompareTo(n) >= 0)
			{
				return false;
			}
			if (s.CompareTo(BigInteger.Zero) < 0 || s.CompareTo(n) >= 0)
			{
				return false;
			}
			ECPoint g = ecpublicKeyParameters.Parameters.G;
			ECPoint q = ecpublicKeyParameters.Q;
			ECPoint ecpoint = ECAlgorithms.SumOfTwoMultiplies(g, s, q, r).Normalize();
			if (ecpoint.IsInfinity)
			{
				return false;
			}
			BigInteger n2 = ecpoint.AffineXCoord.ToBigInteger();
			BigInteger bigInteger2 = r.Subtract(n2).Mod(n);
			return bigInteger2.Equals(bigInteger);
		}

		// Token: 0x040016F1 RID: 5873
		private bool forSigning;

		// Token: 0x040016F2 RID: 5874
		private ECKeyParameters key;

		// Token: 0x040016F3 RID: 5875
		private SecureRandom random;
	}
}
