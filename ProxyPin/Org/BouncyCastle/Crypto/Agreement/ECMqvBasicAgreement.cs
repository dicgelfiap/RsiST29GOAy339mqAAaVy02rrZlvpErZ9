using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;

namespace Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x02000346 RID: 838
	public class ECMqvBasicAgreement : IBasicAgreement
	{
		// Token: 0x060018F8 RID: 6392 RVA: 0x00080514 File Offset: 0x00080514
		public virtual void Init(ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			this.privParams = (MqvPrivateParameters)parameters;
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x0008053C File Offset: 0x0008053C
		public virtual int GetFieldSize()
		{
			return (this.privParams.StaticPrivateKey.Parameters.Curve.FieldSize + 7) / 8;
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0008056C File Offset: 0x0008056C
		public virtual BigInteger CalculateAgreement(ICipherParameters pubKey)
		{
			MqvPublicParameters mqvPublicParameters = (MqvPublicParameters)pubKey;
			ECPrivateKeyParameters staticPrivateKey = this.privParams.StaticPrivateKey;
			ECDomainParameters parameters = staticPrivateKey.Parameters;
			if (!parameters.Equals(mqvPublicParameters.StaticPublicKey.Parameters))
			{
				throw new InvalidOperationException("ECMQV public key components have wrong domain parameters");
			}
			ECPoint ecpoint = ECMqvBasicAgreement.CalculateMqvAgreement(parameters, staticPrivateKey, this.privParams.EphemeralPrivateKey, this.privParams.EphemeralPublicKey, mqvPublicParameters.StaticPublicKey, mqvPublicParameters.EphemeralPublicKey).Normalize();
			if (ecpoint.IsInfinity)
			{
				throw new InvalidOperationException("Infinity is not a valid agreement value for MQV");
			}
			return ecpoint.AffineXCoord.ToBigInteger();
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x00080608 File Offset: 0x00080608
		private static ECPoint CalculateMqvAgreement(ECDomainParameters parameters, ECPrivateKeyParameters d1U, ECPrivateKeyParameters d2U, ECPublicKeyParameters Q2U, ECPublicKeyParameters Q1V, ECPublicKeyParameters Q2V)
		{
			BigInteger n = parameters.N;
			int num = (n.BitLength + 1) / 2;
			BigInteger m = BigInteger.One.ShiftLeft(num);
			ECCurve curve = parameters.Curve;
			ECPoint ecpoint = ECAlgorithms.CleanPoint(curve, Q2U.Q);
			ECPoint p = ECAlgorithms.CleanPoint(curve, Q1V.Q);
			ECPoint ecpoint2 = ECAlgorithms.CleanPoint(curve, Q2V.Q);
			BigInteger bigInteger = ecpoint.AffineXCoord.ToBigInteger();
			BigInteger bigInteger2 = bigInteger.Mod(m);
			BigInteger val = bigInteger2.SetBit(num);
			BigInteger val2 = d1U.D.Multiply(val).Add(d2U.D).Mod(n);
			BigInteger bigInteger3 = ecpoint2.AffineXCoord.ToBigInteger();
			BigInteger bigInteger4 = bigInteger3.Mod(m);
			BigInteger bigInteger5 = bigInteger4.SetBit(num);
			BigInteger bigInteger6 = parameters.H.Multiply(val2).Mod(n);
			return ECAlgorithms.SumOfTwoMultiplies(p, bigInteger5.Multiply(bigInteger6).Mod(n), ecpoint2, bigInteger6);
		}

		// Token: 0x040010CB RID: 4299
		protected internal MqvPrivateParameters privParams;
	}
}
