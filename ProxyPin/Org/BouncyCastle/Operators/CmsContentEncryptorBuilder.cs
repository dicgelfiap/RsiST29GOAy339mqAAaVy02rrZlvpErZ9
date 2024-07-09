using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Ntt;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Operators
{
	// Token: 0x0200041E RID: 1054
	public class CmsContentEncryptorBuilder
	{
		// Token: 0x0600218E RID: 8590 RVA: 0x000C2B2C File Offset: 0x000C2B2C
		static CmsContentEncryptorBuilder()
		{
			CmsContentEncryptorBuilder.KeySizes[NistObjectIdentifiers.IdAes128Cbc] = 128;
			CmsContentEncryptorBuilder.KeySizes[NistObjectIdentifiers.IdAes192Cbc] = 192;
			CmsContentEncryptorBuilder.KeySizes[NistObjectIdentifiers.IdAes256Cbc] = 256;
			CmsContentEncryptorBuilder.KeySizes[NttObjectIdentifiers.IdCamellia128Cbc] = 128;
			CmsContentEncryptorBuilder.KeySizes[NttObjectIdentifiers.IdCamellia192Cbc] = 192;
			CmsContentEncryptorBuilder.KeySizes[NttObjectIdentifiers.IdCamellia256Cbc] = 256;
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x000C2BE0 File Offset: 0x000C2BE0
		private static int GetKeySize(DerObjectIdentifier oid)
		{
			if (CmsContentEncryptorBuilder.KeySizes.Contains(oid))
			{
				return (int)CmsContentEncryptorBuilder.KeySizes[oid];
			}
			return -1;
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x000C2C08 File Offset: 0x000C2C08
		public CmsContentEncryptorBuilder(DerObjectIdentifier encryptionOID) : this(encryptionOID, CmsContentEncryptorBuilder.GetKeySize(encryptionOID))
		{
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x000C2C18 File Offset: 0x000C2C18
		public CmsContentEncryptorBuilder(DerObjectIdentifier encryptionOID, int keySize)
		{
			this.encryptionOID = encryptionOID;
			this.keySize = keySize;
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x000C2C3C File Offset: 0x000C2C3C
		public ICipherBuilderWithKey Build()
		{
			return new Asn1CipherBuilderWithKey(this.encryptionOID, this.keySize, null);
		}

		// Token: 0x040015C5 RID: 5573
		private static readonly IDictionary KeySizes = Platform.CreateHashtable();

		// Token: 0x040015C6 RID: 5574
		private readonly DerObjectIdentifier encryptionOID;

		// Token: 0x040015C7 RID: 5575
		private readonly int keySize;

		// Token: 0x040015C8 RID: 5576
		private readonly EnvelopedDataHelper helper = new EnvelopedDataHelper();
	}
}
