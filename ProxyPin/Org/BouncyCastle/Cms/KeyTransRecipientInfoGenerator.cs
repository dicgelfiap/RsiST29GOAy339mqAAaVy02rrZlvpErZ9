using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000306 RID: 774
	public class KeyTransRecipientInfoGenerator : RecipientInfoGenerator
	{
		// Token: 0x06001762 RID: 5986 RVA: 0x0007A570 File Offset: 0x0007A570
		internal KeyTransRecipientInfoGenerator()
		{
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0007A578 File Offset: 0x0007A578
		protected KeyTransRecipientInfoGenerator(IssuerAndSerialNumber issuerAndSerialNumber)
		{
			this.issuerAndSerialNumber = issuerAndSerialNumber;
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x0007A588 File Offset: 0x0007A588
		protected KeyTransRecipientInfoGenerator(byte[] subjectKeyIdentifier)
		{
			this.subjectKeyIdentifier = new DerOctetString(subjectKeyIdentifier);
		}

		// Token: 0x1700052A RID: 1322
		// (set) Token: 0x06001765 RID: 5989 RVA: 0x0007A59C File Offset: 0x0007A59C
		internal X509Certificate RecipientCert
		{
			set
			{
				this.recipientTbsCert = CmsUtilities.GetTbsCertificateStructure(value);
				this.recipientPublicKey = value.GetPublicKey();
				this.info = this.recipientTbsCert.SubjectPublicKeyInfo;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (set) Token: 0x06001766 RID: 5990 RVA: 0x0007A5C8 File Offset: 0x0007A5C8
		internal AsymmetricKeyParameter RecipientPublicKey
		{
			set
			{
				this.recipientPublicKey = value;
				try
				{
					this.info = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(this.recipientPublicKey);
				}
				catch (IOException)
				{
					throw new ArgumentException("can't extract key algorithm from this key");
				}
			}
		}

		// Token: 0x1700052C RID: 1324
		// (set) Token: 0x06001767 RID: 5991 RVA: 0x0007A610 File Offset: 0x0007A610
		internal Asn1OctetString SubjectKeyIdentifier
		{
			set
			{
				this.subjectKeyIdentifier = value;
			}
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x0007A61C File Offset: 0x0007A61C
		public RecipientInfo Generate(KeyParameter contentEncryptionKey, SecureRandom random)
		{
			AlgorithmIdentifier algorithmDetails = this.AlgorithmDetails;
			this.random = random;
			byte[] str = this.GenerateWrappedKey(contentEncryptionKey);
			RecipientIdentifier rid;
			if (this.recipientTbsCert != null)
			{
				IssuerAndSerialNumber id = new IssuerAndSerialNumber(this.recipientTbsCert.Issuer, this.recipientTbsCert.SerialNumber.Value);
				rid = new RecipientIdentifier(id);
			}
			else
			{
				rid = new RecipientIdentifier(this.subjectKeyIdentifier);
			}
			return new RecipientInfo(new KeyTransRecipientInfo(rid, algorithmDetails, new DerOctetString(str)));
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x0007A69C File Offset: 0x0007A69C
		protected virtual AlgorithmIdentifier AlgorithmDetails
		{
			get
			{
				return this.info.AlgorithmID;
			}
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0007A6AC File Offset: 0x0007A6AC
		protected virtual byte[] GenerateWrappedKey(KeyParameter contentEncryptionKey)
		{
			byte[] key = contentEncryptionKey.GetKey();
			AlgorithmIdentifier algorithmID = this.info.AlgorithmID;
			IWrapper wrapper = KeyTransRecipientInfoGenerator.Helper.CreateWrapper(algorithmID.Algorithm.Id);
			wrapper.Init(true, new ParametersWithRandom(this.recipientPublicKey, this.random));
			return wrapper.Wrap(key, 0, key.Length);
		}

		// Token: 0x04000FB5 RID: 4021
		private static readonly CmsEnvelopedHelper Helper = CmsEnvelopedHelper.Instance;

		// Token: 0x04000FB6 RID: 4022
		private TbsCertificateStructure recipientTbsCert;

		// Token: 0x04000FB7 RID: 4023
		private AsymmetricKeyParameter recipientPublicKey;

		// Token: 0x04000FB8 RID: 4024
		private Asn1OctetString subjectKeyIdentifier;

		// Token: 0x04000FB9 RID: 4025
		private SubjectPublicKeyInfo info;

		// Token: 0x04000FBA RID: 4026
		private IssuerAndSerialNumber issuerAndSerialNumber;

		// Token: 0x04000FBB RID: 4027
		private SecureRandom random;
	}
}
