using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.TeleTrust;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020004AF RID: 1199
	public class RsaDigestSigner : ISigner
	{
		// Token: 0x060024E9 RID: 9449 RVA: 0x000CDAE4 File Offset: 0x000CDAE4
		static RsaDigestSigner()
		{
			RsaDigestSigner.oidMap["RIPEMD128"] = TeleTrusTObjectIdentifiers.RipeMD128;
			RsaDigestSigner.oidMap["RIPEMD160"] = TeleTrusTObjectIdentifiers.RipeMD160;
			RsaDigestSigner.oidMap["RIPEMD256"] = TeleTrusTObjectIdentifiers.RipeMD256;
			RsaDigestSigner.oidMap["SHA-1"] = X509ObjectIdentifiers.IdSha1;
			RsaDigestSigner.oidMap["SHA-224"] = NistObjectIdentifiers.IdSha224;
			RsaDigestSigner.oidMap["SHA-256"] = NistObjectIdentifiers.IdSha256;
			RsaDigestSigner.oidMap["SHA-384"] = NistObjectIdentifiers.IdSha384;
			RsaDigestSigner.oidMap["SHA-512"] = NistObjectIdentifiers.IdSha512;
			RsaDigestSigner.oidMap["MD2"] = PkcsObjectIdentifiers.MD2;
			RsaDigestSigner.oidMap["MD4"] = PkcsObjectIdentifiers.MD4;
			RsaDigestSigner.oidMap["MD5"] = PkcsObjectIdentifiers.MD5;
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x000CDBDC File Offset: 0x000CDBDC
		public RsaDigestSigner(IDigest digest) : this(digest, (DerObjectIdentifier)RsaDigestSigner.oidMap[digest.AlgorithmName])
		{
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x000CDBFC File Offset: 0x000CDBFC
		public RsaDigestSigner(IDigest digest, DerObjectIdentifier digestOid) : this(digest, new AlgorithmIdentifier(digestOid, DerNull.Instance))
		{
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x000CDC10 File Offset: 0x000CDC10
		public RsaDigestSigner(IDigest digest, AlgorithmIdentifier algId) : this(new RsaCoreEngine(), digest, algId)
		{
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x000CDC20 File Offset: 0x000CDC20
		public RsaDigestSigner(IRsa rsa, IDigest digest, DerObjectIdentifier digestOid) : this(rsa, digest, new AlgorithmIdentifier(digestOid, DerNull.Instance))
		{
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x000CDC38 File Offset: 0x000CDC38
		public RsaDigestSigner(IRsa rsa, IDigest digest, AlgorithmIdentifier algId) : this(new RsaBlindedEngine(rsa), digest, algId)
		{
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000CDC48 File Offset: 0x000CDC48
		public RsaDigestSigner(IAsymmetricBlockCipher rsaEngine, IDigest digest, AlgorithmIdentifier algId)
		{
			this.rsaEngine = new Pkcs1Encoding(rsaEngine);
			this.digest = digest;
			this.algId = algId;
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x060024F0 RID: 9456 RVA: 0x000CDC6C File Offset: 0x000CDC6C
		public virtual string AlgorithmName
		{
			get
			{
				return this.digest.AlgorithmName + "withRSA";
			}
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x000CDC84 File Offset: 0x000CDC84
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			this.forSigning = forSigning;
			AsymmetricKeyParameter asymmetricKeyParameter;
			if (parameters is ParametersWithRandom)
			{
				asymmetricKeyParameter = (AsymmetricKeyParameter)((ParametersWithRandom)parameters).Parameters;
			}
			else
			{
				asymmetricKeyParameter = (AsymmetricKeyParameter)parameters;
			}
			if (forSigning && !asymmetricKeyParameter.IsPrivate)
			{
				throw new InvalidKeyException("Signing requires private key.");
			}
			if (!forSigning && asymmetricKeyParameter.IsPrivate)
			{
				throw new InvalidKeyException("Verification requires public key.");
			}
			this.Reset();
			this.rsaEngine.Init(forSigning, parameters);
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x000CDD10 File Offset: 0x000CDD10
		public virtual void Update(byte input)
		{
			this.digest.Update(input);
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x000CDD20 File Offset: 0x000CDD20
		public virtual void BlockUpdate(byte[] input, int inOff, int length)
		{
			this.digest.BlockUpdate(input, inOff, length);
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x000CDD30 File Offset: 0x000CDD30
		public virtual byte[] GenerateSignature()
		{
			if (!this.forSigning)
			{
				throw new InvalidOperationException("RsaDigestSigner not initialised for signature generation.");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			byte[] array2 = this.DerEncode(array);
			return this.rsaEngine.ProcessBlock(array2, 0, array2.Length);
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x000CDD90 File Offset: 0x000CDD90
		public virtual bool VerifySignature(byte[] signature)
		{
			if (this.forSigning)
			{
				throw new InvalidOperationException("RsaDigestSigner not initialised for verification");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			byte[] array2;
			byte[] array3;
			try
			{
				array2 = this.rsaEngine.ProcessBlock(signature, 0, signature.Length);
				array3 = this.DerEncode(array);
			}
			catch (Exception)
			{
				return false;
			}
			if (array2.Length == array3.Length)
			{
				return Arrays.ConstantTimeAreEqual(array2, array3);
			}
			if (array2.Length == array3.Length - 2)
			{
				int num = array2.Length - array.Length - 2;
				int num2 = array3.Length - array.Length - 2;
				byte[] array4;
				(array4 = array3)[1] = array4[1] - 2;
				(array4 = array3)[3] = array4[3] - 2;
				int num3 = 0;
				for (int i = 0; i < array.Length; i++)
				{
					num3 |= (int)(array2[num + i] ^ array3[num2 + i]);
				}
				for (int j = 0; j < num; j++)
				{
					num3 |= (int)(array2[j] ^ array3[j]);
				}
				return num3 == 0;
			}
			return false;
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x000CDEB0 File Offset: 0x000CDEB0
		public virtual void Reset()
		{
			this.digest.Reset();
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x000CDEC0 File Offset: 0x000CDEC0
		private byte[] DerEncode(byte[] hash)
		{
			if (this.algId == null)
			{
				return hash;
			}
			DigestInfo digestInfo = new DigestInfo(this.algId, hash);
			return digestInfo.GetDerEncoded();
		}

		// Token: 0x04001764 RID: 5988
		private readonly IAsymmetricBlockCipher rsaEngine;

		// Token: 0x04001765 RID: 5989
		private readonly AlgorithmIdentifier algId;

		// Token: 0x04001766 RID: 5990
		private readonly IDigest digest;

		// Token: 0x04001767 RID: 5991
		private bool forSigning;

		// Token: 0x04001768 RID: 5992
		private static readonly IDictionary oidMap = Platform.CreateHashtable();
	}
}
