using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Utilities.Zlib;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002E2 RID: 738
	public class CmsCompressedDataParser : CmsContentInfoParser
	{
		// Token: 0x0600163F RID: 5695 RVA: 0x0007422C File Offset: 0x0007422C
		public CmsCompressedDataParser(byte[] compressedData) : this(new MemoryStream(compressedData, false))
		{
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x0007423C File Offset: 0x0007423C
		public CmsCompressedDataParser(Stream compressedData) : base(compressedData)
		{
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x00074248 File Offset: 0x00074248
		public CmsTypedStream GetContent()
		{
			CmsTypedStream result;
			try
			{
				CompressedDataParser compressedDataParser = new CompressedDataParser((Asn1SequenceParser)this.contentInfo.GetContent(16));
				ContentInfoParser encapContentInfo = compressedDataParser.GetEncapContentInfo();
				Asn1OctetStringParser asn1OctetStringParser = (Asn1OctetStringParser)encapContentInfo.GetContent(4);
				result = new CmsTypedStream(encapContentInfo.ContentType.ToString(), new ZInputStream(asn1OctetStringParser.GetOctetStream()));
			}
			catch (IOException e)
			{
				throw new CmsException("IOException reading compressed content.", e);
			}
			return result;
		}
	}
}
