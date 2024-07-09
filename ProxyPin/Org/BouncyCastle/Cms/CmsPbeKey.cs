using System;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002EA RID: 746
	public abstract class CmsPbeKey : ICipherParameters
	{
		// Token: 0x0600166E RID: 5742 RVA: 0x000750DC File Offset: 0x000750DC
		[Obsolete("Use version taking 'char[]' instead")]
		public CmsPbeKey(string password, byte[] salt, int iterationCount) : this(password.ToCharArray(), salt, iterationCount)
		{
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x000750EC File Offset: 0x000750EC
		[Obsolete("Use version taking 'char[]' instead")]
		public CmsPbeKey(string password, AlgorithmIdentifier keyDerivationAlgorithm) : this(password.ToCharArray(), keyDerivationAlgorithm)
		{
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x000750FC File Offset: 0x000750FC
		public CmsPbeKey(char[] password, byte[] salt, int iterationCount)
		{
			this.password = (char[])password.Clone();
			this.salt = Arrays.Clone(salt);
			this.iterationCount = iterationCount;
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x00075128 File Offset: 0x00075128
		public CmsPbeKey(char[] password, AlgorithmIdentifier keyDerivationAlgorithm)
		{
			if (!keyDerivationAlgorithm.Algorithm.Equals(PkcsObjectIdentifiers.IdPbkdf2))
			{
				throw new ArgumentException("Unsupported key derivation algorithm: " + keyDerivationAlgorithm.Algorithm);
			}
			Pbkdf2Params instance = Pbkdf2Params.GetInstance(keyDerivationAlgorithm.Parameters.ToAsn1Object());
			this.password = (char[])password.Clone();
			this.salt = instance.GetSalt();
			this.iterationCount = instance.IterationCount.IntValue;
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x000751AC File Offset: 0x000751AC
		~CmsPbeKey()
		{
			Array.Clear(this.password, 0, this.password.Length);
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x000751EC File Offset: 0x000751EC
		[Obsolete("Will be removed")]
		public string Password
		{
			get
			{
				return new string(this.password);
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x000751FC File Offset: 0x000751FC
		public byte[] Salt
		{
			get
			{
				return Arrays.Clone(this.salt);
			}
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x0007520C File Offset: 0x0007520C
		[Obsolete("Use 'Salt' property instead")]
		public byte[] GetSalt()
		{
			return this.Salt;
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001676 RID: 5750 RVA: 0x00075214 File Offset: 0x00075214
		public int IterationCount
		{
			get
			{
				return this.iterationCount;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x0007521C File Offset: 0x0007521C
		public string Algorithm
		{
			get
			{
				return "PKCS5S2";
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x00075224 File Offset: 0x00075224
		public string Format
		{
			get
			{
				return "RAW";
			}
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x0007522C File Offset: 0x0007522C
		public byte[] GetEncoded()
		{
			return null;
		}

		// Token: 0x0600167A RID: 5754
		internal abstract KeyParameter GetEncoded(string algorithmOid);

		// Token: 0x04000F3F RID: 3903
		internal readonly char[] password;

		// Token: 0x04000F40 RID: 3904
		internal readonly byte[] salt;

		// Token: 0x04000F41 RID: 3905
		internal readonly int iterationCount;
	}
}
