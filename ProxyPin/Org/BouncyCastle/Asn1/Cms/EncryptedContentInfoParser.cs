using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000108 RID: 264
	public class EncryptedContentInfoParser
	{
		// Token: 0x06000982 RID: 2434 RVA: 0x00045B34 File Offset: 0x00045B34
		public EncryptedContentInfoParser(Asn1SequenceParser seq)
		{
			this._contentType = (DerObjectIdentifier)seq.ReadObject();
			this._contentEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(seq.ReadObject().ToAsn1Object());
			this._encryptedContent = (Asn1TaggedObjectParser)seq.ReadObject();
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x00045B84 File Offset: 0x00045B84
		public DerObjectIdentifier ContentType
		{
			get
			{
				return this._contentType;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x00045B8C File Offset: 0x00045B8C
		public AlgorithmIdentifier ContentEncryptionAlgorithm
		{
			get
			{
				return this._contentEncryptionAlgorithm;
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00045B94 File Offset: 0x00045B94
		public IAsn1Convertible GetEncryptedContent(int tag)
		{
			return this._encryptedContent.GetObjectParser(tag, false);
		}

		// Token: 0x040006DE RID: 1758
		private DerObjectIdentifier _contentType;

		// Token: 0x040006DF RID: 1759
		private AlgorithmIdentifier _contentEncryptionAlgorithm;

		// Token: 0x040006E0 RID: 1760
		private Asn1TaggedObjectParser _encryptedContent;
	}
}
