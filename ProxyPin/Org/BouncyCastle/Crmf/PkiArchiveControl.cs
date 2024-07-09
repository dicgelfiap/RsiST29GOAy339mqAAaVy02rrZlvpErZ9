using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Cms;

namespace Org.BouncyCastle.Crmf
{
	// Token: 0x02000322 RID: 802
	public class PkiArchiveControl : IControl
	{
		// Token: 0x06001831 RID: 6193 RVA: 0x0007D59C File Offset: 0x0007D59C
		public PkiArchiveControl(PkiArchiveOptions pkiArchiveOptions)
		{
			this.pkiArchiveOptions = pkiArchiveOptions;
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x0007D5AC File Offset: 0x0007D5AC
		public DerObjectIdentifier Type
		{
			get
			{
				return PkiArchiveControl.type;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001833 RID: 6195 RVA: 0x0007D5B4 File Offset: 0x0007D5B4
		public Asn1Encodable Value
		{
			get
			{
				return this.pkiArchiveOptions;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001834 RID: 6196 RVA: 0x0007D5BC File Offset: 0x0007D5BC
		public int ArchiveType
		{
			get
			{
				return this.pkiArchiveOptions.Type;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x0007D5CC File Offset: 0x0007D5CC
		public bool EnvelopedData
		{
			get
			{
				EncryptedKey instance = EncryptedKey.GetInstance(this.pkiArchiveOptions.Value);
				return !instance.IsEncryptedValue;
			}
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x0007D5F8 File Offset: 0x0007D5F8
		public CmsEnvelopedData GetEnvelopedData()
		{
			CmsEnvelopedData result;
			try
			{
				EncryptedKey instance = EncryptedKey.GetInstance(this.pkiArchiveOptions.Value);
				EnvelopedData instance2 = Org.BouncyCastle.Asn1.Cms.EnvelopedData.GetInstance(instance.Value);
				result = new CmsEnvelopedData(new ContentInfo(CmsObjectIdentifiers.EnvelopedData, instance2));
			}
			catch (CmsException ex)
			{
				throw new CrmfException("CMS parsing error: " + ex.Message, ex);
			}
			catch (Exception ex2)
			{
				throw new CrmfException("CRMF parsing error: " + ex2.Message, ex2);
			}
			return result;
		}

		// Token: 0x04001009 RID: 4105
		public static readonly int encryptedPrivKey = 0;

		// Token: 0x0400100A RID: 4106
		public static readonly int keyGenParameters = 1;

		// Token: 0x0400100B RID: 4107
		public static readonly int archiveRemGenPrivKey = 2;

		// Token: 0x0400100C RID: 4108
		private static readonly DerObjectIdentifier type = CrmfObjectIdentifiers.id_regCtrl_pkiArchiveOptions;

		// Token: 0x0400100D RID: 4109
		private readonly PkiArchiveOptions pkiArchiveOptions;
	}
}
