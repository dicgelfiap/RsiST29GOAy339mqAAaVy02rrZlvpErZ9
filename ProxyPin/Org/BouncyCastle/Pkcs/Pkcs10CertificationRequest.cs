using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.TeleTrust;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Pkcs
{
	// Token: 0x0200067F RID: 1663
	public class Pkcs10CertificationRequest : CertificationRequest
	{
		// Token: 0x06003A09 RID: 14857 RVA: 0x001378C0 File Offset: 0x001378C0
		static Pkcs10CertificationRequest()
		{
			Pkcs10CertificationRequest.algorithms.Add("MD2WITHRSAENCRYPTION", PkcsObjectIdentifiers.MD2WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("MD2WITHRSA", PkcsObjectIdentifiers.MD2WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("MD5WITHRSAENCRYPTION", PkcsObjectIdentifiers.MD5WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("MD5WITHRSA", PkcsObjectIdentifiers.MD5WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("RSAWITHMD5", PkcsObjectIdentifiers.MD5WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("SHA1WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha1WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("SHA1WITHRSA", PkcsObjectIdentifiers.Sha1WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("SHA224WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha224WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("SHA224WITHRSA", PkcsObjectIdentifiers.Sha224WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("SHA256WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha256WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("SHA256WITHRSA", PkcsObjectIdentifiers.Sha256WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("SHA384WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha384WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("SHA384WITHRSA", PkcsObjectIdentifiers.Sha384WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("SHA512WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha512WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("SHA512WITHRSA", PkcsObjectIdentifiers.Sha512WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("SHA1WITHRSAANDMGF1", PkcsObjectIdentifiers.IdRsassaPss);
			Pkcs10CertificationRequest.algorithms.Add("SHA224WITHRSAANDMGF1", PkcsObjectIdentifiers.IdRsassaPss);
			Pkcs10CertificationRequest.algorithms.Add("SHA256WITHRSAANDMGF1", PkcsObjectIdentifiers.IdRsassaPss);
			Pkcs10CertificationRequest.algorithms.Add("SHA384WITHRSAANDMGF1", PkcsObjectIdentifiers.IdRsassaPss);
			Pkcs10CertificationRequest.algorithms.Add("SHA512WITHRSAANDMGF1", PkcsObjectIdentifiers.IdRsassaPss);
			Pkcs10CertificationRequest.algorithms.Add("RSAWITHSHA1", PkcsObjectIdentifiers.Sha1WithRsaEncryption);
			Pkcs10CertificationRequest.algorithms.Add("RIPEMD128WITHRSAENCRYPTION", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD128);
			Pkcs10CertificationRequest.algorithms.Add("RIPEMD128WITHRSA", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD128);
			Pkcs10CertificationRequest.algorithms.Add("RIPEMD160WITHRSAENCRYPTION", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD160);
			Pkcs10CertificationRequest.algorithms.Add("RIPEMD160WITHRSA", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD160);
			Pkcs10CertificationRequest.algorithms.Add("RIPEMD256WITHRSAENCRYPTION", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD256);
			Pkcs10CertificationRequest.algorithms.Add("RIPEMD256WITHRSA", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD256);
			Pkcs10CertificationRequest.algorithms.Add("SHA1WITHDSA", X9ObjectIdentifiers.IdDsaWithSha1);
			Pkcs10CertificationRequest.algorithms.Add("DSAWITHSHA1", X9ObjectIdentifiers.IdDsaWithSha1);
			Pkcs10CertificationRequest.algorithms.Add("SHA224WITHDSA", NistObjectIdentifiers.DsaWithSha224);
			Pkcs10CertificationRequest.algorithms.Add("SHA256WITHDSA", NistObjectIdentifiers.DsaWithSha256);
			Pkcs10CertificationRequest.algorithms.Add("SHA384WITHDSA", NistObjectIdentifiers.DsaWithSha384);
			Pkcs10CertificationRequest.algorithms.Add("SHA512WITHDSA", NistObjectIdentifiers.DsaWithSha512);
			Pkcs10CertificationRequest.algorithms.Add("SHA1WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha1);
			Pkcs10CertificationRequest.algorithms.Add("SHA224WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha224);
			Pkcs10CertificationRequest.algorithms.Add("SHA256WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha256);
			Pkcs10CertificationRequest.algorithms.Add("SHA384WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha384);
			Pkcs10CertificationRequest.algorithms.Add("SHA512WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha512);
			Pkcs10CertificationRequest.algorithms.Add("ECDSAWITHSHA1", X9ObjectIdentifiers.ECDsaWithSha1);
			Pkcs10CertificationRequest.algorithms.Add("GOST3411WITHGOST3410", CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x94);
			Pkcs10CertificationRequest.algorithms.Add("GOST3410WITHGOST3411", CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x94);
			Pkcs10CertificationRequest.algorithms.Add("GOST3411WITHECGOST3410", CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001);
			Pkcs10CertificationRequest.algorithms.Add("GOST3411WITHECGOST3410-2001", CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001);
			Pkcs10CertificationRequest.algorithms.Add("GOST3411WITHGOST3410-2001", CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001);
			Pkcs10CertificationRequest.oids.Add(PkcsObjectIdentifiers.Sha1WithRsaEncryption, "SHA1WITHRSA");
			Pkcs10CertificationRequest.oids.Add(PkcsObjectIdentifiers.Sha224WithRsaEncryption, "SHA224WITHRSA");
			Pkcs10CertificationRequest.oids.Add(PkcsObjectIdentifiers.Sha256WithRsaEncryption, "SHA256WITHRSA");
			Pkcs10CertificationRequest.oids.Add(PkcsObjectIdentifiers.Sha384WithRsaEncryption, "SHA384WITHRSA");
			Pkcs10CertificationRequest.oids.Add(PkcsObjectIdentifiers.Sha512WithRsaEncryption, "SHA512WITHRSA");
			Pkcs10CertificationRequest.oids.Add(CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x94, "GOST3411WITHGOST3410");
			Pkcs10CertificationRequest.oids.Add(CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001, "GOST3411WITHECGOST3410");
			Pkcs10CertificationRequest.oids.Add(PkcsObjectIdentifiers.MD5WithRsaEncryption, "MD5WITHRSA");
			Pkcs10CertificationRequest.oids.Add(PkcsObjectIdentifiers.MD2WithRsaEncryption, "MD2WITHRSA");
			Pkcs10CertificationRequest.oids.Add(X9ObjectIdentifiers.IdDsaWithSha1, "SHA1WITHDSA");
			Pkcs10CertificationRequest.oids.Add(X9ObjectIdentifiers.ECDsaWithSha1, "SHA1WITHECDSA");
			Pkcs10CertificationRequest.oids.Add(X9ObjectIdentifiers.ECDsaWithSha224, "SHA224WITHECDSA");
			Pkcs10CertificationRequest.oids.Add(X9ObjectIdentifiers.ECDsaWithSha256, "SHA256WITHECDSA");
			Pkcs10CertificationRequest.oids.Add(X9ObjectIdentifiers.ECDsaWithSha384, "SHA384WITHECDSA");
			Pkcs10CertificationRequest.oids.Add(X9ObjectIdentifiers.ECDsaWithSha512, "SHA512WITHECDSA");
			Pkcs10CertificationRequest.oids.Add(OiwObjectIdentifiers.MD5WithRsa, "MD5WITHRSA");
			Pkcs10CertificationRequest.oids.Add(OiwObjectIdentifiers.Sha1WithRsa, "SHA1WITHRSA");
			Pkcs10CertificationRequest.oids.Add(OiwObjectIdentifiers.DsaWithSha1, "SHA1WITHDSA");
			Pkcs10CertificationRequest.oids.Add(NistObjectIdentifiers.DsaWithSha224, "SHA224WITHDSA");
			Pkcs10CertificationRequest.oids.Add(NistObjectIdentifiers.DsaWithSha256, "SHA256WITHDSA");
			Pkcs10CertificationRequest.keyAlgorithms.Add(PkcsObjectIdentifiers.RsaEncryption, "RSA");
			Pkcs10CertificationRequest.keyAlgorithms.Add(X9ObjectIdentifiers.IdDsa, "DSA");
			Pkcs10CertificationRequest.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha1);
			Pkcs10CertificationRequest.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha224);
			Pkcs10CertificationRequest.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha256);
			Pkcs10CertificationRequest.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha384);
			Pkcs10CertificationRequest.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha512);
			Pkcs10CertificationRequest.noParams.Add(X9ObjectIdentifiers.IdDsaWithSha1);
			Pkcs10CertificationRequest.noParams.Add(NistObjectIdentifiers.DsaWithSha224);
			Pkcs10CertificationRequest.noParams.Add(NistObjectIdentifiers.DsaWithSha256);
			Pkcs10CertificationRequest.noParams.Add(CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x94);
			Pkcs10CertificationRequest.noParams.Add(CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001);
			AlgorithmIdentifier hashAlgId = new AlgorithmIdentifier(OiwObjectIdentifiers.IdSha1, DerNull.Instance);
			Pkcs10CertificationRequest.exParams.Add("SHA1WITHRSAANDMGF1", Pkcs10CertificationRequest.CreatePssParams(hashAlgId, 20));
			AlgorithmIdentifier hashAlgId2 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha224, DerNull.Instance);
			Pkcs10CertificationRequest.exParams.Add("SHA224WITHRSAANDMGF1", Pkcs10CertificationRequest.CreatePssParams(hashAlgId2, 28));
			AlgorithmIdentifier hashAlgId3 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha256, DerNull.Instance);
			Pkcs10CertificationRequest.exParams.Add("SHA256WITHRSAANDMGF1", Pkcs10CertificationRequest.CreatePssParams(hashAlgId3, 32));
			AlgorithmIdentifier hashAlgId4 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha384, DerNull.Instance);
			Pkcs10CertificationRequest.exParams.Add("SHA384WITHRSAANDMGF1", Pkcs10CertificationRequest.CreatePssParams(hashAlgId4, 48));
			AlgorithmIdentifier hashAlgId5 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha512, DerNull.Instance);
			Pkcs10CertificationRequest.exParams.Add("SHA512WITHRSAANDMGF1", Pkcs10CertificationRequest.CreatePssParams(hashAlgId5, 64));
		}

		// Token: 0x06003A0A RID: 14858 RVA: 0x00137F88 File Offset: 0x00137F88
		private static RsassaPssParameters CreatePssParams(AlgorithmIdentifier hashAlgId, int saltSize)
		{
			return new RsassaPssParameters(hashAlgId, new AlgorithmIdentifier(PkcsObjectIdentifiers.IdMgf1, hashAlgId), new DerInteger(saltSize), new DerInteger(1));
		}

		// Token: 0x06003A0B RID: 14859 RVA: 0x00137FA8 File Offset: 0x00137FA8
		protected Pkcs10CertificationRequest()
		{
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x00137FB0 File Offset: 0x00137FB0
		public Pkcs10CertificationRequest(byte[] encoded) : base((Asn1Sequence)Asn1Object.FromByteArray(encoded))
		{
		}

		// Token: 0x06003A0D RID: 14861 RVA: 0x00137FC4 File Offset: 0x00137FC4
		public Pkcs10CertificationRequest(Asn1Sequence seq) : base(seq)
		{
		}

		// Token: 0x06003A0E RID: 14862 RVA: 0x00137FD0 File Offset: 0x00137FD0
		public Pkcs10CertificationRequest(Stream input) : base((Asn1Sequence)Asn1Object.FromStream(input))
		{
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x00137FE4 File Offset: 0x00137FE4
		public Pkcs10CertificationRequest(string signatureAlgorithm, X509Name subject, AsymmetricKeyParameter publicKey, Asn1Set attributes, AsymmetricKeyParameter signingKey) : this(new Asn1SignatureFactory(signatureAlgorithm, signingKey), subject, publicKey, attributes)
		{
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x00137FF8 File Offset: 0x00137FF8
		[Obsolete("Use constructor without 'signingKey' parameter (ignored here)")]
		public Pkcs10CertificationRequest(ISignatureFactory signatureFactory, X509Name subject, AsymmetricKeyParameter publicKey, Asn1Set attributes, AsymmetricKeyParameter signingKey) : this(signatureFactory, subject, publicKey, attributes)
		{
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x00138008 File Offset: 0x00138008
		public Pkcs10CertificationRequest(ISignatureFactory signatureFactory, X509Name subject, AsymmetricKeyParameter publicKey, Asn1Set attributes)
		{
			if (signatureFactory == null)
			{
				throw new ArgumentNullException("signatureFactory");
			}
			if (subject == null)
			{
				throw new ArgumentNullException("subject");
			}
			if (publicKey == null)
			{
				throw new ArgumentNullException("publicKey");
			}
			if (publicKey.IsPrivate)
			{
				throw new ArgumentException("expected public key", "publicKey");
			}
			this.Init(signatureFactory, subject, publicKey, attributes);
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x00138078 File Offset: 0x00138078
		private void Init(ISignatureFactory signatureFactory, X509Name subject, AsymmetricKeyParameter publicKey, Asn1Set attributes)
		{
			this.sigAlgId = (AlgorithmIdentifier)signatureFactory.AlgorithmDetails;
			SubjectPublicKeyInfo pkInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);
			this.reqInfo = new CertificationRequestInfo(subject, pkInfo, attributes);
			IStreamCalculator streamCalculator = signatureFactory.CreateCalculator();
			byte[] derEncoded = this.reqInfo.GetDerEncoded();
			streamCalculator.Stream.Write(derEncoded, 0, derEncoded.Length);
			Platform.Dispose(streamCalculator.Stream);
			this.sigBits = new DerBitString(((IBlockResult)streamCalculator.GetResult()).Collect());
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x001380FC File Offset: 0x001380FC
		public AsymmetricKeyParameter GetPublicKey()
		{
			return PublicKeyFactory.CreateKey(this.reqInfo.SubjectPublicKeyInfo);
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x00138110 File Offset: 0x00138110
		public bool Verify()
		{
			return this.Verify(this.GetPublicKey());
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x00138120 File Offset: 0x00138120
		public bool Verify(AsymmetricKeyParameter publicKey)
		{
			return this.Verify(new Asn1VerifierFactoryProvider(publicKey));
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x00138130 File Offset: 0x00138130
		public bool Verify(IVerifierFactoryProvider verifierProvider)
		{
			return this.Verify(verifierProvider.CreateVerifierFactory(this.sigAlgId));
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x00138144 File Offset: 0x00138144
		public bool Verify(IVerifierFactory verifier)
		{
			bool result;
			try
			{
				byte[] derEncoded = this.reqInfo.GetDerEncoded();
				IStreamCalculator streamCalculator = verifier.CreateCalculator();
				streamCalculator.Stream.Write(derEncoded, 0, derEncoded.Length);
				Platform.Dispose(streamCalculator.Stream);
				result = ((IVerifier)streamCalculator.GetResult()).IsVerified(this.sigBits.GetOctets());
			}
			catch (Exception exception)
			{
				throw new SignatureException("exception encoding TBS cert request", exception);
			}
			return result;
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x001381C0 File Offset: 0x001381C0
		private void SetSignatureParameters(ISigner signature, Asn1Encodable asn1Params)
		{
			if (asn1Params != null && !(asn1Params is Asn1Null) && Platform.EndsWith(signature.AlgorithmName, "MGF1"))
			{
				throw Platform.CreateNotImplementedException("signature algorithm with MGF1");
			}
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x001381F4 File Offset: 0x001381F4
		internal static string GetSignatureName(AlgorithmIdentifier sigAlgId)
		{
			Asn1Encodable parameters = sigAlgId.Parameters;
			if (parameters != null && !(parameters is Asn1Null) && sigAlgId.Algorithm.Equals(PkcsObjectIdentifiers.IdRsassaPss))
			{
				RsassaPssParameters instance = RsassaPssParameters.GetInstance(parameters);
				return Pkcs10CertificationRequest.GetDigestAlgName(instance.HashAlgorithm.Algorithm) + "withRSAandMGF1";
			}
			return sigAlgId.Algorithm.Id;
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x00138260 File Offset: 0x00138260
		private static string GetDigestAlgName(DerObjectIdentifier digestAlgOID)
		{
			if (PkcsObjectIdentifiers.MD5.Equals(digestAlgOID))
			{
				return "MD5";
			}
			if (OiwObjectIdentifiers.IdSha1.Equals(digestAlgOID))
			{
				return "SHA1";
			}
			if (NistObjectIdentifiers.IdSha224.Equals(digestAlgOID))
			{
				return "SHA224";
			}
			if (NistObjectIdentifiers.IdSha256.Equals(digestAlgOID))
			{
				return "SHA256";
			}
			if (NistObjectIdentifiers.IdSha384.Equals(digestAlgOID))
			{
				return "SHA384";
			}
			if (NistObjectIdentifiers.IdSha512.Equals(digestAlgOID))
			{
				return "SHA512";
			}
			if (TeleTrusTObjectIdentifiers.RipeMD128.Equals(digestAlgOID))
			{
				return "RIPEMD128";
			}
			if (TeleTrusTObjectIdentifiers.RipeMD160.Equals(digestAlgOID))
			{
				return "RIPEMD160";
			}
			if (TeleTrusTObjectIdentifiers.RipeMD256.Equals(digestAlgOID))
			{
				return "RIPEMD256";
			}
			if (CryptoProObjectIdentifiers.GostR3411.Equals(digestAlgOID))
			{
				return "GOST3411";
			}
			return digestAlgOID.Id;
		}

		// Token: 0x04001E31 RID: 7729
		protected static readonly IDictionary algorithms = Platform.CreateHashtable();

		// Token: 0x04001E32 RID: 7730
		protected static readonly IDictionary exParams = Platform.CreateHashtable();

		// Token: 0x04001E33 RID: 7731
		protected static readonly IDictionary keyAlgorithms = Platform.CreateHashtable();

		// Token: 0x04001E34 RID: 7732
		protected static readonly IDictionary oids = Platform.CreateHashtable();

		// Token: 0x04001E35 RID: 7733
		protected static readonly ISet noParams = new HashSet();
	}
}
