using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;

namespace Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x02000343 RID: 835
	public class ECDHBasicAgreement : IBasicAgreement
	{
		// Token: 0x060018ED RID: 6381 RVA: 0x0008020C File Offset: 0x0008020C
		public virtual void Init(ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			this.privKey = (ECPrivateKeyParameters)parameters;
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x00080234 File Offset: 0x00080234
		public virtual int GetFieldSize()
		{
			return (this.privKey.Parameters.Curve.FieldSize + 7) / 8;
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x00080250 File Offset: 0x00080250
		public virtual BigInteger CalculateAgreement(ICipherParameters pubKey)
		{
			ECPublicKeyParameters ecpublicKeyParameters = (ECPublicKeyParameters)pubKey;
			ECDomainParameters parameters = this.privKey.Parameters;
			if (!parameters.Equals(ecpublicKeyParameters.Parameters))
			{
				throw new InvalidOperationException("ECDH public key has wrong domain parameters");
			}
			BigInteger bigInteger = this.privKey.D;
			ECPoint ecpoint = ECAlgorithms.CleanPoint(parameters.Curve, ecpublicKeyParameters.Q);
			if (ecpoint.IsInfinity)
			{
				throw new InvalidOperationException("Infinity is not a valid public key for ECDH");
			}
			BigInteger h = parameters.H;
			if (!h.Equals(BigInteger.One))
			{
				bigInteger = parameters.HInv.Multiply(bigInteger).Mod(parameters.N);
				ecpoint = ECAlgorithms.ReferenceMultiply(ecpoint, h);
			}
			ECPoint ecpoint2 = ecpoint.Multiply(bigInteger).Normalize();
			if (ecpoint2.IsInfinity)
			{
				throw new InvalidOperationException("Infinity is not a valid agreement value for ECDH");
			}
			return ecpoint2.AffineXCoord.ToBigInteger();
		}

		// Token: 0x040010C7 RID: 4295
		protected internal ECPrivateKeyParameters privKey;
	}
}
