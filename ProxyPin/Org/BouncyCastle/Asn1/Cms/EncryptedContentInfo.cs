using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000107 RID: 263
	public class EncryptedContentInfo : Asn1Encodable
	{
		// Token: 0x0600097B RID: 2427 RVA: 0x000459E4 File Offset: 0x000459E4
		public EncryptedContentInfo(DerObjectIdentifier contentType, AlgorithmIdentifier contentEncryptionAlgorithm, Asn1OctetString encryptedContent)
		{
			this.contentType = contentType;
			this.contentEncryptionAlgorithm = contentEncryptionAlgorithm;
			this.encryptedContent = encryptedContent;
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00045A04 File Offset: 0x00045A04
		public EncryptedContentInfo(Asn1Sequence seq)
		{
			this.contentType = (DerObjectIdentifier)seq[0];
			this.contentEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(seq[1]);
			if (seq.Count > 2)
			{
				this.encryptedContent = Asn1OctetString.GetInstance((Asn1TaggedObject)seq[2], false);
			}
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00045A64 File Offset: 0x00045A64
		public static EncryptedContentInfo GetInstance(object obj)
		{
			if (obj == null || obj is EncryptedContentInfo)
			{
				return (EncryptedContentInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new EncryptedContentInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid EncryptedContentInfo: " + Platform.GetTypeName(obj));
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x00045ABC File Offset: 0x00045ABC
		public DerObjectIdentifier ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x00045AC4 File Offset: 0x00045AC4
		public AlgorithmIdentifier ContentEncryptionAlgorithm
		{
			get
			{
				return this.contentEncryptionAlgorithm;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00045ACC File Offset: 0x00045ACC
		public Asn1OctetString EncryptedContent
		{
			get
			{
				return this.encryptedContent;
			}
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00045AD4 File Offset: 0x00045AD4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.contentType,
				this.contentEncryptionAlgorithm
			});
			if (this.encryptedContent != null)
			{
				asn1EncodableVector.Add(new BerTaggedObject(false, 0, this.encryptedContent));
			}
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x040006DB RID: 1755
		private DerObjectIdentifier contentType;

		// Token: 0x040006DC RID: 1756
		private AlgorithmIdentifier contentEncryptionAlgorithm;

		// Token: 0x040006DD RID: 1757
		private Asn1OctetString encryptedContent;
	}
}
