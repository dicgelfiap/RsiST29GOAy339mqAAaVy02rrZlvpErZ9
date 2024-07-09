using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000446 RID: 1094
	public abstract class ECKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x0600226C RID: 8812 RVA: 0x000C4A88 File Offset: 0x000C4A88
		protected ECKeyParameters(string algorithm, bool isPrivate, ECDomainParameters parameters) : base(isPrivate)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this.algorithm = ECKeyParameters.VerifyAlgorithmName(algorithm);
			this.parameters = parameters;
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x000C4AC8 File Offset: 0x000C4AC8
		protected ECKeyParameters(string algorithm, bool isPrivate, DerObjectIdentifier publicKeyParamSet) : base(isPrivate)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (publicKeyParamSet == null)
			{
				throw new ArgumentNullException("publicKeyParamSet");
			}
			this.algorithm = ECKeyParameters.VerifyAlgorithmName(algorithm);
			this.parameters = ECKeyParameters.LookupParameters(publicKeyParamSet);
			this.publicKeyParamSet = publicKeyParamSet;
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x0600226E RID: 8814 RVA: 0x000C4B24 File Offset: 0x000C4B24
		public string AlgorithmName
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x0600226F RID: 8815 RVA: 0x000C4B2C File Offset: 0x000C4B2C
		public ECDomainParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x000C4B34 File Offset: 0x000C4B34
		public DerObjectIdentifier PublicKeyParamSet
		{
			get
			{
				return this.publicKeyParamSet;
			}
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x000C4B3C File Offset: 0x000C4B3C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ECDomainParameters ecdomainParameters = obj as ECDomainParameters;
			return ecdomainParameters != null && this.Equals(ecdomainParameters);
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x000C4B6C File Offset: 0x000C4B6C
		protected bool Equals(ECKeyParameters other)
		{
			return this.parameters.Equals(other.parameters) && base.Equals(other);
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x000C4B90 File Offset: 0x000C4B90
		public override int GetHashCode()
		{
			return this.parameters.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x000C4BA4 File Offset: 0x000C4BA4
		internal ECKeyGenerationParameters CreateKeyGenerationParameters(SecureRandom random)
		{
			if (this.publicKeyParamSet != null)
			{
				return new ECKeyGenerationParameters(this.publicKeyParamSet, random);
			}
			return new ECKeyGenerationParameters(this.parameters, random);
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x000C4BCC File Offset: 0x000C4BCC
		internal static string VerifyAlgorithmName(string algorithm)
		{
			string result = Platform.ToUpperInvariant(algorithm);
			if (Array.IndexOf(ECKeyParameters.algorithms, algorithm, 0, ECKeyParameters.algorithms.Length) < 0)
			{
				throw new ArgumentException("unrecognised algorithm: " + algorithm, "algorithm");
			}
			return result;
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x000C4C14 File Offset: 0x000C4C14
		internal static ECDomainParameters LookupParameters(DerObjectIdentifier publicKeyParamSet)
		{
			if (publicKeyParamSet == null)
			{
				throw new ArgumentNullException("publicKeyParamSet");
			}
			ECDomainParameters ecdomainParameters = ECGost3410NamedCurves.GetByOid(publicKeyParamSet);
			if (ecdomainParameters == null)
			{
				X9ECParameters x9ECParameters = ECKeyPairGenerator.FindECCurveByOid(publicKeyParamSet);
				if (x9ECParameters == null)
				{
					throw new ArgumentException("OID is not a valid public key parameter set", "publicKeyParamSet");
				}
				ecdomainParameters = new ECDomainParameters(x9ECParameters.Curve, x9ECParameters.G, x9ECParameters.N, x9ECParameters.H, x9ECParameters.GetSeed());
			}
			return ecdomainParameters;
		}

		// Token: 0x0400160C RID: 5644
		private static readonly string[] algorithms = new string[]
		{
			"EC",
			"ECDSA",
			"ECDH",
			"ECDHC",
			"ECGOST3410",
			"ECMQV"
		};

		// Token: 0x0400160D RID: 5645
		private readonly string algorithm;

		// Token: 0x0400160E RID: 5646
		private readonly ECDomainParameters parameters;

		// Token: 0x0400160F RID: 5647
		private readonly DerObjectIdentifier publicKeyParamSet;
	}
}
