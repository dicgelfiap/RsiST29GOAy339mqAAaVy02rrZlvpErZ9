using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x0200030F RID: 783
	public class Pkcs5Scheme2PbeKey : CmsPbeKey
	{
		// Token: 0x060017AE RID: 6062 RVA: 0x0007B548 File Offset: 0x0007B548
		[Obsolete("Use version taking 'char[]' instead")]
		public Pkcs5Scheme2PbeKey(string password, byte[] salt, int iterationCount) : this(password.ToCharArray(), salt, iterationCount)
		{
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x0007B558 File Offset: 0x0007B558
		[Obsolete("Use version taking 'char[]' instead")]
		public Pkcs5Scheme2PbeKey(string password, AlgorithmIdentifier keyDerivationAlgorithm) : this(password.ToCharArray(), keyDerivationAlgorithm)
		{
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x0007B568 File Offset: 0x0007B568
		public Pkcs5Scheme2PbeKey(char[] password, byte[] salt, int iterationCount) : base(password, salt, iterationCount)
		{
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x0007B574 File Offset: 0x0007B574
		public Pkcs5Scheme2PbeKey(char[] password, AlgorithmIdentifier keyDerivationAlgorithm) : base(password, keyDerivationAlgorithm)
		{
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x0007B580 File Offset: 0x0007B580
		internal override KeyParameter GetEncoded(string algorithmOid)
		{
			Pkcs5S2ParametersGenerator pkcs5S2ParametersGenerator = new Pkcs5S2ParametersGenerator();
			pkcs5S2ParametersGenerator.Init(PbeParametersGenerator.Pkcs5PasswordToBytes(this.password), this.salt, this.iterationCount);
			return (KeyParameter)pkcs5S2ParametersGenerator.GenerateDerivedParameters(algorithmOid, CmsEnvelopedHelper.Instance.GetKeySize(algorithmOid));
		}
	}
}
