using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x0200049B RID: 1179
	public class ECGost3410Signer : IDsaExt, IDsa
	{
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x0600244A RID: 9290 RVA: 0x000CA314 File Offset: 0x000CA314
		public virtual string AlgorithmName
		{
			get
			{
				return this.key.AlgorithmName;
			}
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x000CA324 File Offset: 0x000CA324
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

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x0600244C RID: 9292 RVA: 0x000CA3C0 File Offset: 0x000CA3C0
		public virtual BigInteger Order
		{
			get
			{
				return this.key.Parameters.N;
			}
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x000CA3D4 File Offset: 0x000CA3D4
		public virtual BigInteger[] GenerateSignature(byte[] message)
		{
			if (!this.forSigning)
			{
				throw new InvalidOperationException("not initialized for signing");
			}
			byte[] bytes = Arrays.Reverse(message);
			BigInteger val = new BigInteger(1, bytes);
			ECDomainParameters parameters = this.key.Parameters;
			BigInteger n = parameters.N;
			BigInteger d = ((ECPrivateKeyParameters)this.key).D;
			ECMultiplier ecmultiplier = this.CreateBasePointMultiplier();
			BigInteger bigInteger2;
			BigInteger bigInteger3;
			for (;;)
			{
				BigInteger bigInteger = new BigInteger(n.BitLength, this.random);
				if (bigInteger.SignValue != 0)
				{
					ECPoint ecpoint = ecmultiplier.Multiply(parameters.G, bigInteger).Normalize();
					bigInteger2 = ecpoint.AffineXCoord.ToBigInteger().Mod(n);
					if (bigInteger2.SignValue != 0)
					{
						bigInteger3 = bigInteger.Multiply(val).Add(d.Multiply(bigInteger2)).Mod(n);
						if (bigInteger3.SignValue != 0)
						{
							break;
						}
					}
				}
			}
			return new BigInteger[]
			{
				bigInteger2,
				bigInteger3
			};
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x000CA4D0 File Offset: 0x000CA4D0
		public virtual bool VerifySignature(byte[] message, BigInteger r, BigInteger s)
		{
			if (this.forSigning)
			{
				throw new InvalidOperationException("not initialized for verification");
			}
			byte[] bytes = Arrays.Reverse(message);
			BigInteger bigInteger = new BigInteger(1, bytes);
			BigInteger n = this.key.Parameters.N;
			if (r.CompareTo(BigInteger.One) < 0 || r.CompareTo(n) >= 0)
			{
				return false;
			}
			if (s.CompareTo(BigInteger.One) < 0 || s.CompareTo(n) >= 0)
			{
				return false;
			}
			BigInteger val = bigInteger.ModInverse(n);
			BigInteger a = s.Multiply(val).Mod(n);
			BigInteger b = n.Subtract(r).Multiply(val).Mod(n);
			ECPoint g = this.key.Parameters.G;
			ECPoint q = ((ECPublicKeyParameters)this.key).Q;
			ECPoint ecpoint = ECAlgorithms.SumOfTwoMultiplies(g, a, q, b).Normalize();
			if (ecpoint.IsInfinity)
			{
				return false;
			}
			BigInteger bigInteger2 = ecpoint.AffineXCoord.ToBigInteger().Mod(n);
			return bigInteger2.Equals(r);
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x000CA5E8 File Offset: 0x000CA5E8
		protected virtual ECMultiplier CreateBasePointMultiplier()
		{
			return new FixedPointCombMultiplier();
		}

		// Token: 0x040016EE RID: 5870
		private ECKeyParameters key;

		// Token: 0x040016EF RID: 5871
		private SecureRandom random;

		// Token: 0x040016F0 RID: 5872
		private bool forSigning;
	}
}
