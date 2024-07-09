using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000104 RID: 260
	public class CompressedDataParser
	{
		// Token: 0x0600096D RID: 2413 RVA: 0x0004579C File Offset: 0x0004579C
		public CompressedDataParser(Asn1SequenceParser seq)
		{
			this._version = (DerInteger)seq.ReadObject();
			this._compressionAlgorithm = AlgorithmIdentifier.GetInstance(seq.ReadObject().ToAsn1Object());
			this._encapContentInfo = new ContentInfoParser((Asn1SequenceParser)seq.ReadObject());
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x000457F0 File Offset: 0x000457F0
		public DerInteger Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x000457F8 File Offset: 0x000457F8
		public AlgorithmIdentifier CompressionAlgorithmIdentifier
		{
			get
			{
				return this._compressionAlgorithm;
			}
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00045800 File Offset: 0x00045800
		public ContentInfoParser GetEncapContentInfo()
		{
			return this._encapContentInfo;
		}

		// Token: 0x040006D4 RID: 1748
		private DerInteger _version;

		// Token: 0x040006D5 RID: 1749
		private AlgorithmIdentifier _compressionAlgorithm;

		// Token: 0x040006D6 RID: 1750
		private ContentInfoParser _encapContentInfo;
	}
}
