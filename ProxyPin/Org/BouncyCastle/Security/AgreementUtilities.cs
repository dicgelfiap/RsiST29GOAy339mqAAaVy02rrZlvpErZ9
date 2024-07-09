using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.EdEC;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Agreement;
using Org.BouncyCastle.Crypto.Agreement.Kdf;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Security
{
	// Token: 0x020006A8 RID: 1704
	public sealed class AgreementUtilities
	{
		// Token: 0x06003B96 RID: 15254 RVA: 0x0014448C File Offset: 0x0014448C
		private AgreementUtilities()
		{
		}

		// Token: 0x06003B97 RID: 15255 RVA: 0x00144494 File Offset: 0x00144494
		static AgreementUtilities()
		{
			AgreementUtilities.algorithms[X9ObjectIdentifiers.DHSinglePassCofactorDHSha1KdfScheme.Id] = "ECCDHWITHSHA1KDF";
			AgreementUtilities.algorithms[X9ObjectIdentifiers.DHSinglePassStdDHSha1KdfScheme.Id] = "ECDHWITHSHA1KDF";
			AgreementUtilities.algorithms[X9ObjectIdentifiers.MqvSinglePassSha1KdfScheme.Id] = "ECMQVWITHSHA1KDF";
			AgreementUtilities.algorithms[EdECObjectIdentifiers.id_X25519.Id] = "X25519";
			AgreementUtilities.algorithms[EdECObjectIdentifiers.id_X448.Id] = "X448";
		}

		// Token: 0x06003B98 RID: 15256 RVA: 0x0014452C File Offset: 0x0014452C
		public static IBasicAgreement GetBasicAgreement(DerObjectIdentifier oid)
		{
			return AgreementUtilities.GetBasicAgreement(oid.Id);
		}

		// Token: 0x06003B99 RID: 15257 RVA: 0x0014453C File Offset: 0x0014453C
		public static IBasicAgreement GetBasicAgreement(string algorithm)
		{
			string mechanism = AgreementUtilities.GetMechanism(algorithm);
			if (mechanism == "DH" || mechanism == "DIFFIEHELLMAN")
			{
				return new DHBasicAgreement();
			}
			if (mechanism == "ECDH")
			{
				return new ECDHBasicAgreement();
			}
			if (mechanism == "ECDHC" || mechanism == "ECCDH")
			{
				return new ECDHCBasicAgreement();
			}
			if (mechanism == "ECMQV")
			{
				return new ECMqvBasicAgreement();
			}
			throw new SecurityUtilityException("Basic Agreement " + algorithm + " not recognised.");
		}

		// Token: 0x06003B9A RID: 15258 RVA: 0x001445E4 File Offset: 0x001445E4
		public static IBasicAgreement GetBasicAgreementWithKdf(DerObjectIdentifier oid, string wrapAlgorithm)
		{
			return AgreementUtilities.GetBasicAgreementWithKdf(oid.Id, wrapAlgorithm);
		}

		// Token: 0x06003B9B RID: 15259 RVA: 0x001445F4 File Offset: 0x001445F4
		public static IBasicAgreement GetBasicAgreementWithKdf(string agreeAlgorithm, string wrapAlgorithm)
		{
			string mechanism = AgreementUtilities.GetMechanism(agreeAlgorithm);
			if (mechanism == "DHWITHSHA1KDF" || mechanism == "ECDHWITHSHA1KDF")
			{
				return new ECDHWithKdfBasicAgreement(wrapAlgorithm, new ECDHKekGenerator(new Sha1Digest()));
			}
			if (mechanism == "ECMQVWITHSHA1KDF")
			{
				return new ECMqvWithKdfBasicAgreement(wrapAlgorithm, new ECDHKekGenerator(new Sha1Digest()));
			}
			throw new SecurityUtilityException("Basic Agreement (with KDF) " + agreeAlgorithm + " not recognised.");
		}

		// Token: 0x06003B9C RID: 15260 RVA: 0x00144674 File Offset: 0x00144674
		public static IRawAgreement GetRawAgreement(DerObjectIdentifier oid)
		{
			return AgreementUtilities.GetRawAgreement(oid.Id);
		}

		// Token: 0x06003B9D RID: 15261 RVA: 0x00144684 File Offset: 0x00144684
		public static IRawAgreement GetRawAgreement(string algorithm)
		{
			string mechanism = AgreementUtilities.GetMechanism(algorithm);
			if (mechanism == "X25519")
			{
				return new X25519Agreement();
			}
			if (mechanism == "X448")
			{
				return new X448Agreement();
			}
			throw new SecurityUtilityException("Raw Agreement " + algorithm + " not recognised.");
		}

		// Token: 0x06003B9E RID: 15262 RVA: 0x001446E0 File Offset: 0x001446E0
		public static string GetAlgorithmName(DerObjectIdentifier oid)
		{
			return (string)AgreementUtilities.algorithms[oid.Id];
		}

		// Token: 0x06003B9F RID: 15263 RVA: 0x001446F8 File Offset: 0x001446F8
		private static string GetMechanism(string algorithm)
		{
			string text = Platform.ToUpperInvariant(algorithm);
			string text2 = (string)AgreementUtilities.algorithms[text];
			if (text2 != null)
			{
				return text2;
			}
			return text;
		}

		// Token: 0x04001E94 RID: 7828
		private static readonly IDictionary algorithms = Platform.CreateHashtable();
	}
}
