using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000310 RID: 784
	public class Pkcs5Scheme2Utf8PbeKey : CmsPbeKey
	{
		// Token: 0x060017B3 RID: 6067 RVA: 0x0007B5CC File Offset: 0x0007B5CC
		[Obsolete("Use version taking 'char[]' instead")]
		public Pkcs5Scheme2Utf8PbeKey(string password, byte[] salt, int iterationCount) : this(password.ToCharArray(), salt, iterationCount)
		{
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x0007B5DC File Offset: 0x0007B5DC
		[Obsolete("Use version taking 'char[]' instead")]
		public Pkcs5Scheme2Utf8PbeKey(string password, AlgorithmIdentifier keyDerivationAlgorithm) : this(password.ToCharArray(), keyDerivationAlgorithm)
		{
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x0007B5EC File Offset: 0x0007B5EC
		public Pkcs5Scheme2Utf8PbeKey(char[] password, byte[] salt, int iterationCount) : base(password, salt, iterationCount)
		{
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x0007B5F8 File Offset: 0x0007B5F8
		public Pkcs5Scheme2Utf8PbeKey(char[] password, AlgorithmIdentifier keyDerivationAlgorithm) : base(password, keyDerivationAlgorithm)
		{
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0007B604 File Offset: 0x0007B604
		internal override KeyParameter GetEncoded(string algorithmOid)
		{
			Pkcs5S2ParametersGenerator pkcs5S2ParametersGenerator = new Pkcs5S2ParametersGenerator();
			pkcs5S2ParametersGenerator.Init(PbeParametersGenerator.Pkcs5PasswordToUtf8Bytes(this.password), this.salt, this.iterationCount);
			return (KeyParameter)pkcs5S2ParametersGenerator.GenerateDerivedParameters(algorithmOid, CmsEnvelopedHelper.Instance.GetKeySize(algorithmOid));
		}
	}
}
