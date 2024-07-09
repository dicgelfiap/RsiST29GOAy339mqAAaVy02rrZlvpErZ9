using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020004A4 RID: 1188
	public class Gost3410Signer : IDsaExt, IDsa
	{
		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x0600248F RID: 9359 RVA: 0x000CB3A8 File Offset: 0x000CB3A8
		public virtual string AlgorithmName
		{
			get
			{
				return "GOST3410";
			}
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x000CB3B0 File Offset: 0x000CB3B0
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
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
				if (!(parameters is Gost3410PrivateKeyParameters))
				{
					throw new InvalidKeyException("GOST3410 private key required for signing");
				}
				this.key = (Gost3410PrivateKeyParameters)parameters;
				return;
			}
			else
			{
				if (!(parameters is Gost3410PublicKeyParameters))
				{
					throw new InvalidKeyException("GOST3410 public key required for signing");
				}
				this.key = (Gost3410PublicKeyParameters)parameters;
				return;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06002491 RID: 9361 RVA: 0x000CB444 File Offset: 0x000CB444
		public virtual BigInteger Order
		{
			get
			{
				return this.key.Parameters.Q;
			}
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x000CB458 File Offset: 0x000CB458
		public virtual BigInteger[] GenerateSignature(byte[] message)
		{
			byte[] bytes = Arrays.Reverse(message);
			BigInteger val = new BigInteger(1, bytes);
			Gost3410Parameters parameters = this.key.Parameters;
			BigInteger bigInteger;
			do
			{
				bigInteger = new BigInteger(parameters.Q.BitLength, this.random);
			}
			while (bigInteger.CompareTo(parameters.Q) >= 0);
			BigInteger bigInteger2 = parameters.A.ModPow(bigInteger, parameters.P).Mod(parameters.Q);
			BigInteger bigInteger3 = bigInteger.Multiply(val).Add(((Gost3410PrivateKeyParameters)this.key).X.Multiply(bigInteger2)).Mod(parameters.Q);
			return new BigInteger[]
			{
				bigInteger2,
				bigInteger3
			};
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x000CB518 File Offset: 0x000CB518
		public virtual bool VerifySignature(byte[] message, BigInteger r, BigInteger s)
		{
			byte[] bytes = Arrays.Reverse(message);
			BigInteger bigInteger = new BigInteger(1, bytes);
			Gost3410Parameters parameters = this.key.Parameters;
			if (r.SignValue < 0 || parameters.Q.CompareTo(r) <= 0)
			{
				return false;
			}
			if (s.SignValue < 0 || parameters.Q.CompareTo(s) <= 0)
			{
				return false;
			}
			BigInteger val = bigInteger.ModPow(parameters.Q.Subtract(BigInteger.Two), parameters.Q);
			BigInteger bigInteger2 = s.Multiply(val).Mod(parameters.Q);
			BigInteger bigInteger3 = parameters.Q.Subtract(r).Multiply(val).Mod(parameters.Q);
			bigInteger2 = parameters.A.ModPow(bigInteger2, parameters.P);
			bigInteger3 = ((Gost3410PublicKeyParameters)this.key).Y.ModPow(bigInteger3, parameters.P);
			BigInteger bigInteger4 = bigInteger2.Multiply(bigInteger3).Mod(parameters.P).Mod(parameters.Q);
			return bigInteger4.Equals(r);
		}

		// Token: 0x04001714 RID: 5908
		private Gost3410KeyParameters key;

		// Token: 0x04001715 RID: 5909
		private SecureRandom random;
	}
}
