using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000103 RID: 259
	public class CompressedData : Asn1Encodable
	{
		// Token: 0x06000965 RID: 2405 RVA: 0x00045660 File Offset: 0x00045660
		public CompressedData(AlgorithmIdentifier compressionAlgorithm, ContentInfo encapContentInfo)
		{
			this.version = new DerInteger(0);
			this.compressionAlgorithm = compressionAlgorithm;
			this.encapContentInfo = encapContentInfo;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00045684 File Offset: 0x00045684
		public CompressedData(Asn1Sequence seq)
		{
			this.version = (DerInteger)seq[0];
			this.compressionAlgorithm = AlgorithmIdentifier.GetInstance(seq[1]);
			this.encapContentInfo = ContentInfo.GetInstance(seq[2]);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x000456D4 File Offset: 0x000456D4
		public static CompressedData GetInstance(Asn1TaggedObject ato, bool explicitly)
		{
			return CompressedData.GetInstance(Asn1Sequence.GetInstance(ato, explicitly));
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x000456E4 File Offset: 0x000456E4
		public static CompressedData GetInstance(object obj)
		{
			if (obj == null || obj is CompressedData)
			{
				return (CompressedData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CompressedData((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid CompressedData: " + Platform.GetTypeName(obj));
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x0004573C File Offset: 0x0004573C
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x00045744 File Offset: 0x00045744
		public AlgorithmIdentifier CompressionAlgorithmIdentifier
		{
			get
			{
				return this.compressionAlgorithm;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x0004574C File Offset: 0x0004574C
		public ContentInfo EncapContentInfo
		{
			get
			{
				return this.encapContentInfo;
			}
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00045754 File Offset: 0x00045754
		public override Asn1Object ToAsn1Object()
		{
			return new BerSequence(new Asn1Encodable[]
			{
				this.version,
				this.compressionAlgorithm,
				this.encapContentInfo
			});
		}

		// Token: 0x040006D1 RID: 1745
		private DerInteger version;

		// Token: 0x040006D2 RID: 1746
		private AlgorithmIdentifier compressionAlgorithm;

		// Token: 0x040006D3 RID: 1747
		private ContentInfo encapContentInfo;
	}
}
