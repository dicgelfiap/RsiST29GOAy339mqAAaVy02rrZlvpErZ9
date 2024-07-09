using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto.Agreement.Kdf;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x02000345 RID: 837
	public class ECDHWithKdfBasicAgreement : ECDHBasicAgreement
	{
		// Token: 0x060018F5 RID: 6389 RVA: 0x00080440 File Offset: 0x00080440
		public ECDHWithKdfBasicAgreement(string algorithm, IDerivationFunction kdf)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (kdf == null)
			{
				throw new ArgumentNullException("kdf");
			}
			this.algorithm = algorithm;
			this.kdf = kdf;
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x00080478 File Offset: 0x00080478
		public override BigInteger CalculateAgreement(ICipherParameters pubKey)
		{
			BigInteger r = base.CalculateAgreement(pubKey);
			int defaultKeySize = GeneratorUtilities.GetDefaultKeySize(this.algorithm);
			DHKdfParameters parameters = new DHKdfParameters(new DerObjectIdentifier(this.algorithm), defaultKeySize, this.BigIntToBytes(r));
			this.kdf.Init(parameters);
			byte[] array = new byte[defaultKeySize / 8];
			this.kdf.GenerateBytes(array, 0, array.Length);
			return new BigInteger(1, array);
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x000804E4 File Offset: 0x000804E4
		private byte[] BigIntToBytes(BigInteger r)
		{
			int byteLength = X9IntegerConverter.GetByteLength(this.privKey.Parameters.Curve);
			return X9IntegerConverter.IntegerToBytes(r, byteLength);
		}

		// Token: 0x040010C9 RID: 4297
		private readonly string algorithm;

		// Token: 0x040010CA RID: 4298
		private readonly IDerivationFunction kdf;
	}
}
