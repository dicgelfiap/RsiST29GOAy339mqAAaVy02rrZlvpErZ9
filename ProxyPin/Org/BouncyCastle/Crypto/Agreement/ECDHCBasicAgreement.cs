using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;

namespace Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x02000344 RID: 836
	public class ECDHCBasicAgreement : IBasicAgreement
	{
		// Token: 0x060018F1 RID: 6385 RVA: 0x00080338 File Offset: 0x00080338
		public virtual void Init(ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			this.privKey = (ECPrivateKeyParameters)parameters;
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x00080360 File Offset: 0x00080360
		public virtual int GetFieldSize()
		{
			return (this.privKey.Parameters.Curve.FieldSize + 7) / 8;
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x0008037C File Offset: 0x0008037C
		public virtual BigInteger CalculateAgreement(ICipherParameters pubKey)
		{
			ECPublicKeyParameters ecpublicKeyParameters = (ECPublicKeyParameters)pubKey;
			ECDomainParameters parameters = this.privKey.Parameters;
			if (!parameters.Equals(ecpublicKeyParameters.Parameters))
			{
				throw new InvalidOperationException("ECDHC public key has wrong domain parameters");
			}
			BigInteger b = parameters.H.Multiply(this.privKey.D).Mod(parameters.N);
			ECPoint ecpoint = ECAlgorithms.CleanPoint(parameters.Curve, ecpublicKeyParameters.Q);
			if (ecpoint.IsInfinity)
			{
				throw new InvalidOperationException("Infinity is not a valid public key for ECDHC");
			}
			ECPoint ecpoint2 = ecpoint.Multiply(b).Normalize();
			if (ecpoint2.IsInfinity)
			{
				throw new InvalidOperationException("Infinity is not a valid agreement value for ECDHC");
			}
			return ecpoint2.AffineXCoord.ToBigInteger();
		}

		// Token: 0x040010C8 RID: 4296
		private ECPrivateKeyParameters privKey;
	}
}
