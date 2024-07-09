using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x0200049A RID: 1178
	public class ECDsaSigner : IDsaExt, IDsa
	{
		// Token: 0x0600243E RID: 9278 RVA: 0x000C9EC0 File Offset: 0x000C9EC0
		public ECDsaSigner()
		{
			this.kCalculator = new RandomDsaKCalculator();
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x000C9EE4 File Offset: 0x000C9EE4
		public ECDsaSigner(IDsaKCalculator kCalculator)
		{
			this.kCalculator = kCalculator;
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06002440 RID: 9280 RVA: 0x000C9F04 File Offset: 0x000C9F04
		public virtual string AlgorithmName
		{
			get
			{
				return "ECDSA";
			}
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x000C9F0C File Offset: 0x000C9F0C
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
				if (!(parameters is ECPrivateKeyParameters))
				{
					throw new InvalidKeyException("EC private key required for signing");
				}
				this.key = (ECPrivateKeyParameters)parameters;
			}
			else
			{
				if (!(parameters is ECPublicKeyParameters))
				{
					throw new InvalidKeyException("EC public key required for verification");
				}
				this.key = (ECPublicKeyParameters)parameters;
			}
			this.random = this.InitSecureRandom(forSigning && !this.kCalculator.IsDeterministic, provided);
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06002442 RID: 9282 RVA: 0x000C9FB8 File Offset: 0x000C9FB8
		public virtual BigInteger Order
		{
			get
			{
				return this.key.Parameters.N;
			}
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x000C9FCC File Offset: 0x000C9FCC
		public virtual BigInteger[] GenerateSignature(byte[] message)
		{
			ECDomainParameters parameters = this.key.Parameters;
			BigInteger n = parameters.N;
			BigInteger bigInteger = this.CalculateE(n, message);
			BigInteger d = ((ECPrivateKeyParameters)this.key).D;
			if (this.kCalculator.IsDeterministic)
			{
				this.kCalculator.Init(n, d, message);
			}
			else
			{
				this.kCalculator.Init(n, this.random);
			}
			ECMultiplier ecmultiplier = this.CreateBasePointMultiplier();
			BigInteger bigInteger3;
			BigInteger bigInteger4;
			for (;;)
			{
				BigInteger bigInteger2 = this.kCalculator.NextK();
				ECPoint ecpoint = ecmultiplier.Multiply(parameters.G, bigInteger2).Normalize();
				bigInteger3 = ecpoint.AffineXCoord.ToBigInteger().Mod(n);
				if (bigInteger3.SignValue != 0)
				{
					bigInteger4 = bigInteger2.ModInverse(n).Multiply(bigInteger.Add(d.Multiply(bigInteger3))).Mod(n);
					if (bigInteger4.SignValue != 0)
					{
						break;
					}
				}
			}
			return new BigInteger[]
			{
				bigInteger3,
				bigInteger4
			};
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x000CA0D4 File Offset: 0x000CA0D4
		public virtual bool VerifySignature(byte[] message, BigInteger r, BigInteger s)
		{
			BigInteger n = this.key.Parameters.N;
			if (r.SignValue < 1 || s.SignValue < 1 || r.CompareTo(n) >= 0 || s.CompareTo(n) >= 0)
			{
				return false;
			}
			BigInteger bigInteger = this.CalculateE(n, message);
			BigInteger val = s.ModInverse(n);
			BigInteger a = bigInteger.Multiply(val).Mod(n);
			BigInteger b = r.Multiply(val).Mod(n);
			ECPoint g = this.key.Parameters.G;
			ECPoint q = ((ECPublicKeyParameters)this.key).Q;
			ECPoint ecpoint = ECAlgorithms.SumOfTwoMultiplies(g, a, q, b);
			if (ecpoint.IsInfinity)
			{
				return false;
			}
			ECCurve curve = ecpoint.Curve;
			if (curve != null)
			{
				BigInteger cofactor = curve.Cofactor;
				if (cofactor != null && cofactor.CompareTo(ECDsaSigner.Eight) <= 0)
				{
					ECFieldElement denominator = this.GetDenominator(curve.CoordinateSystem, ecpoint);
					if (denominator != null && !denominator.IsZero)
					{
						ECFieldElement xcoord = ecpoint.XCoord;
						while (curve.IsValidFieldElement(r))
						{
							ECFieldElement ecfieldElement = curve.FromBigInteger(r).Multiply(denominator);
							if (ecfieldElement.Equals(xcoord))
							{
								return true;
							}
							r = r.Add(n);
						}
						return false;
					}
				}
			}
			BigInteger bigInteger2 = ecpoint.Normalize().AffineXCoord.ToBigInteger().Mod(n);
			return bigInteger2.Equals(r);
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x000CA254 File Offset: 0x000CA254
		protected virtual BigInteger CalculateE(BigInteger n, byte[] message)
		{
			int num = message.Length * 8;
			BigInteger bigInteger = new BigInteger(1, message);
			if (n.BitLength < num)
			{
				bigInteger = bigInteger.ShiftRight(num - n.BitLength);
			}
			return bigInteger;
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x000CA290 File Offset: 0x000CA290
		protected virtual ECMultiplier CreateBasePointMultiplier()
		{
			return new FixedPointCombMultiplier();
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x000CA298 File Offset: 0x000CA298
		protected virtual ECFieldElement GetDenominator(int coordinateSystem, ECPoint p)
		{
			switch (coordinateSystem)
			{
			case 1:
			case 6:
			case 7:
				return p.GetZCoord(0);
			case 2:
			case 3:
			case 4:
				return p.GetZCoord(0).Square();
			}
			return null;
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x000CA2EC File Offset: 0x000CA2EC
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

		// Token: 0x040016EA RID: 5866
		private static readonly BigInteger Eight = BigInteger.ValueOf(8L);

		// Token: 0x040016EB RID: 5867
		protected readonly IDsaKCalculator kCalculator;

		// Token: 0x040016EC RID: 5868
		protected ECKeyParameters key = null;

		// Token: 0x040016ED RID: 5869
		protected SecureRandom random = null;
	}
}
