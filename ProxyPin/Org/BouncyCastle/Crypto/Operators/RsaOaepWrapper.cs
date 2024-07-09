using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Operators
{
	// Token: 0x02000415 RID: 1045
	internal class RsaOaepWrapper : IKeyWrapper, IKeyUnwrapper
	{
		// Token: 0x0600216F RID: 8559 RVA: 0x000C2144 File Offset: 0x000C2144
		public RsaOaepWrapper(bool forWrapping, ICipherParameters parameters, DerObjectIdentifier digestOid)
		{
			AlgorithmIdentifier algorithmIdentifier = new AlgorithmIdentifier(digestOid, DerNull.Instance);
			this.algId = new AlgorithmIdentifier(PkcsObjectIdentifiers.IdRsaesOaep, new RsaesOaepParameters(algorithmIdentifier, new AlgorithmIdentifier(PkcsObjectIdentifiers.IdMgf1, algorithmIdentifier), RsaesOaepParameters.DefaultPSourceAlgorithm));
			this.engine = new OaepEncoding(new RsaBlindedEngine(), DigestUtilities.GetDigest(digestOid));
			this.engine.Init(forWrapping, parameters);
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06002170 RID: 8560 RVA: 0x000C21B0 File Offset: 0x000C21B0
		public object AlgorithmDetails
		{
			get
			{
				return this.algId;
			}
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x000C21B8 File Offset: 0x000C21B8
		public IBlockResult Unwrap(byte[] cipherText, int offset, int length)
		{
			return new SimpleBlockResult(this.engine.ProcessBlock(cipherText, offset, length));
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x000C21D0 File Offset: 0x000C21D0
		public IBlockResult Wrap(byte[] keyData)
		{
			return new SimpleBlockResult(this.engine.ProcessBlock(keyData, 0, keyData.Length));
		}

		// Token: 0x040015B7 RID: 5559
		private readonly AlgorithmIdentifier algId;

		// Token: 0x040015B8 RID: 5560
		private readonly IAsymmetricBlockCipher engine;
	}
}
