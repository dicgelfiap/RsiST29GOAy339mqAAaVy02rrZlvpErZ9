using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.TeleTrust;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002F1 RID: 753
	public class CmsSignedGenerator
	{
		// Token: 0x060016A5 RID: 5797 RVA: 0x0007595C File Offset: 0x0007595C
		protected CmsSignedGenerator() : this(new SecureRandom())
		{
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x0007596C File Offset: 0x0007596C
		protected CmsSignedGenerator(SecureRandom rand)
		{
			this.rand = rand;
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x000759C4 File Offset: 0x000759C4
		protected internal virtual IDictionary GetBaseParameters(DerObjectIdentifier contentType, AlgorithmIdentifier digAlgId, byte[] hash)
		{
			IDictionary dictionary = Platform.CreateHashtable();
			if (contentType != null)
			{
				dictionary[CmsAttributeTableParameter.ContentType] = contentType;
			}
			dictionary[CmsAttributeTableParameter.DigestAlgorithmIdentifier] = digAlgId;
			dictionary[CmsAttributeTableParameter.Digest] = hash.Clone();
			return dictionary;
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x00075A10 File Offset: 0x00075A10
		protected internal virtual Asn1Set GetAttributeSet(Org.BouncyCastle.Asn1.Cms.AttributeTable attr)
		{
			if (attr != null)
			{
				return new DerSet(attr.ToAsn1EncodableVector());
			}
			return null;
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x00075A28 File Offset: 0x00075A28
		public void AddCertificates(IX509Store certStore)
		{
			CollectionUtilities.AddRange(this._certs, CmsUtilities.GetCertificatesFromStore(certStore));
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00075A3C File Offset: 0x00075A3C
		public void AddCrls(IX509Store crlStore)
		{
			CollectionUtilities.AddRange(this._crls, CmsUtilities.GetCrlsFromStore(crlStore));
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x00075A50 File Offset: 0x00075A50
		public void AddAttributeCertificates(IX509Store store)
		{
			try
			{
				foreach (object obj in store.GetMatches(null))
				{
					IX509AttributeCertificate ix509AttributeCertificate = (IX509AttributeCertificate)obj;
					this._certs.Add(new DerTaggedObject(false, 2, AttributeCertificate.GetInstance(Asn1Object.FromByteArray(ix509AttributeCertificate.GetEncoded()))));
				}
			}
			catch (Exception e)
			{
				throw new CmsException("error processing attribute certs", e);
			}
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00075AF0 File Offset: 0x00075AF0
		public void AddSigners(SignerInformationStore signerStore)
		{
			foreach (object obj in signerStore.GetSigners())
			{
				SignerInformation signerInformation = (SignerInformation)obj;
				this._signers.Add(signerInformation);
				this.AddSignerCallback(signerInformation);
			}
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x00075B60 File Offset: 0x00075B60
		public IDictionary GetGeneratedDigests()
		{
			return Platform.CreateHashtable(this._digests);
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x00075B70 File Offset: 0x00075B70
		// (set) Token: 0x060016AF RID: 5807 RVA: 0x00075B78 File Offset: 0x00075B78
		public bool UseDerForCerts
		{
			get
			{
				return this._useDerForCerts;
			}
			set
			{
				this._useDerForCerts = value;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x00075B84 File Offset: 0x00075B84
		// (set) Token: 0x060016B1 RID: 5809 RVA: 0x00075B8C File Offset: 0x00075B8C
		public bool UseDerForCrls
		{
			get
			{
				return this._useDerForCrls;
			}
			set
			{
				this._useDerForCrls = value;
			}
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x00075B98 File Offset: 0x00075B98
		internal virtual void AddSignerCallback(SignerInformation si)
		{
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x00075B9C File Offset: 0x00075B9C
		internal static SignerIdentifier GetSignerIdentifier(X509Certificate cert)
		{
			return new SignerIdentifier(CmsUtilities.GetIssuerAndSerialNumber(cert));
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x00075BAC File Offset: 0x00075BAC
		internal static SignerIdentifier GetSignerIdentifier(byte[] subjectKeyIdentifier)
		{
			return new SignerIdentifier(new DerOctetString(subjectKeyIdentifier));
		}

		// Token: 0x04000F52 RID: 3922
		public static readonly string Data = CmsObjectIdentifiers.Data.Id;

		// Token: 0x04000F53 RID: 3923
		public static readonly string DigestSha1 = OiwObjectIdentifiers.IdSha1.Id;

		// Token: 0x04000F54 RID: 3924
		public static readonly string DigestSha224 = NistObjectIdentifiers.IdSha224.Id;

		// Token: 0x04000F55 RID: 3925
		public static readonly string DigestSha256 = NistObjectIdentifiers.IdSha256.Id;

		// Token: 0x04000F56 RID: 3926
		public static readonly string DigestSha384 = NistObjectIdentifiers.IdSha384.Id;

		// Token: 0x04000F57 RID: 3927
		public static readonly string DigestSha512 = NistObjectIdentifiers.IdSha512.Id;

		// Token: 0x04000F58 RID: 3928
		public static readonly string DigestMD5 = PkcsObjectIdentifiers.MD5.Id;

		// Token: 0x04000F59 RID: 3929
		public static readonly string DigestGost3411 = CryptoProObjectIdentifiers.GostR3411.Id;

		// Token: 0x04000F5A RID: 3930
		public static readonly string DigestRipeMD128 = TeleTrusTObjectIdentifiers.RipeMD128.Id;

		// Token: 0x04000F5B RID: 3931
		public static readonly string DigestRipeMD160 = TeleTrusTObjectIdentifiers.RipeMD160.Id;

		// Token: 0x04000F5C RID: 3932
		public static readonly string DigestRipeMD256 = TeleTrusTObjectIdentifiers.RipeMD256.Id;

		// Token: 0x04000F5D RID: 3933
		public static readonly string EncryptionRsa = PkcsObjectIdentifiers.RsaEncryption.Id;

		// Token: 0x04000F5E RID: 3934
		public static readonly string EncryptionDsa = X9ObjectIdentifiers.IdDsaWithSha1.Id;

		// Token: 0x04000F5F RID: 3935
		public static readonly string EncryptionECDsa = X9ObjectIdentifiers.ECDsaWithSha1.Id;

		// Token: 0x04000F60 RID: 3936
		public static readonly string EncryptionRsaPss = PkcsObjectIdentifiers.IdRsassaPss.Id;

		// Token: 0x04000F61 RID: 3937
		public static readonly string EncryptionGost3410 = CryptoProObjectIdentifiers.GostR3410x94.Id;

		// Token: 0x04000F62 RID: 3938
		public static readonly string EncryptionECGost3410 = CryptoProObjectIdentifiers.GostR3410x2001.Id;

		// Token: 0x04000F63 RID: 3939
		internal IList _certs = Platform.CreateArrayList();

		// Token: 0x04000F64 RID: 3940
		internal IList _crls = Platform.CreateArrayList();

		// Token: 0x04000F65 RID: 3941
		internal IList _signers = Platform.CreateArrayList();

		// Token: 0x04000F66 RID: 3942
		internal IDictionary _digests = Platform.CreateHashtable();

		// Token: 0x04000F67 RID: 3943
		internal bool _useDerForCerts = false;

		// Token: 0x04000F68 RID: 3944
		internal bool _useDerForCrls = false;

		// Token: 0x04000F69 RID: 3945
		protected readonly SecureRandom rand;
	}
}
