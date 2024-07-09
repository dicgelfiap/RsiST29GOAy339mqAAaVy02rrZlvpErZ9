using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x02000499 RID: 1177
	public class DsaSigner : IDsaExt, IDsa
	{
		// Token: 0x06002435 RID: 9269 RVA: 0x000C9BB0 File Offset: 0x000C9BB0
		public DsaSigner()
		{
			this.kCalculator = new RandomDsaKCalculator();
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x000C9BD4 File Offset: 0x000C9BD4
		public DsaSigner(IDsaKCalculator kCalculator)
		{
			this.kCalculator = kCalculator;
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06002437 RID: 9271 RVA: 0x000C9BF4 File Offset: 0x000C9BF4
		public virtual string AlgorithmName
		{
			get
			{
				return "DSA";
			}
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x000C9BFC File Offset: 0x000C9BFC
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			SecureRandom provided = null;
			if (forSigning)
			{
				if (parameters is ParametersWithRandom)
				{
					ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
					provided = parametersWithRandom.Random;
					parameters = parametersWithRandom.Parameters;
				}
				if (!(parameters is DsaPrivateKeyParameters))
				{
					throw new InvalidKeyException("DSA private key required for signing");
				}
				this.key = (DsaPrivateKeyParameters)parameters;
			}
			else
			{
				if (!(parameters is DsaPublicKeyParameters))
				{
					throw new InvalidKeyException("DSA public key required for verification");
				}
				this.key = (DsaPublicKeyParameters)parameters;
			}
			this.random = this.InitSecureRandom(forSigning && !this.kCalculator.IsDeterministic, provided);
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06002439 RID: 9273 RVA: 0x000C9CA8 File Offset: 0x000C9CA8
		public virtual BigInteger Order
		{
			get
			{
				return this.key.Parameters.Q;
			}
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x000C9CBC File Offset: 0x000C9CBC
		public virtual BigInteger[] GenerateSignature(byte[] message)
		{
			DsaParameters parameters = this.key.Parameters;
			BigInteger q = parameters.Q;
			BigInteger bigInteger = this.CalculateE(q, message);
			BigInteger x = ((DsaPrivateKeyParameters)this.key).X;
			if (this.kCalculator.IsDeterministic)
			{
				this.kCalculator.Init(q, x, message);
			}
			else
			{
				this.kCalculator.Init(q, this.random);
			}
			BigInteger bigInteger2 = this.kCalculator.NextK();
			BigInteger bigInteger3 = parameters.G.ModPow(bigInteger2, parameters.P).Mod(q);
			bigInteger2 = bigInteger2.ModInverse(q).Multiply(bigInteger.Add(x.Multiply(bigInteger3)));
			BigInteger bigInteger4 = bigInteger2.Mod(q);
			return new BigInteger[]
			{
				bigInteger3,
				bigInteger4
			};
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000C9D9C File Offset: 0x000C9D9C
		public virtual bool VerifySignature(byte[] message, BigInteger r, BigInteger s)
		{
			DsaParameters parameters = this.key.Parameters;
			BigInteger q = parameters.Q;
			BigInteger bigInteger = this.CalculateE(q, message);
			if (r.SignValue <= 0 || q.CompareTo(r) <= 0)
			{
				return false;
			}
			if (s.SignValue <= 0 || q.CompareTo(s) <= 0)
			{
				return false;
			}
			BigInteger val = s.ModInverse(q);
			BigInteger bigInteger2 = bigInteger.Multiply(val).Mod(q);
			BigInteger bigInteger3 = r.Multiply(val).Mod(q);
			BigInteger p = parameters.P;
			bigInteger2 = parameters.G.ModPow(bigInteger2, p);
			bigInteger3 = ((DsaPublicKeyParameters)this.key).Y.ModPow(bigInteger3, p);
			BigInteger bigInteger4 = bigInteger2.Multiply(bigInteger3).Mod(p).Mod(q);
			return bigInteger4.Equals(r);
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000C9E7C File Offset: 0x000C9E7C
		protected virtual BigInteger CalculateE(BigInteger n, byte[] message)
		{
			int length = Math.Min(message.Length, n.BitLength / 8);
			return new BigInteger(1, message, 0, length);
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x000C9EA8 File Offset: 0x000C9EA8
		protected virtual SecureRandom InitSecureRandom(bool needed, SecureRandom provided)
		{
			if (!needed)
			{
				return null;
			}
			if (provided == null)
			{
				return new SecureRandom();
			}
			return provided;
		}

		// Token: 0x040016E7 RID: 5863
		protected readonly IDsaKCalculator kCalculator;

		// Token: 0x040016E8 RID: 5864
		protected DsaKeyParameters key = null;

		// Token: 0x040016E9 RID: 5865
		protected SecureRandom random = null;
	}
}
