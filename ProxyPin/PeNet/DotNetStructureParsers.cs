using System;
using System.Linq;
using PeNet.Parser;
using PeNet.Structures;
using PeNet.Utilities;

namespace PeNet
{
	// Token: 0x02000B8D RID: 2957
	internal class DotNetStructureParsers
	{
		// Token: 0x170018CF RID: 6351
		// (get) Token: 0x060076DE RID: 30430 RVA: 0x00238BD8 File Offset: 0x00238BD8
		public METADATAHDR MetaDataHdr
		{
			get
			{
				MetaDataHdrParser metaDataHdrParser = this._metaDataHdrParser;
				if (metaDataHdrParser == null)
				{
					return null;
				}
				return metaDataHdrParser.GetParserTarget();
			}
		}

		// Token: 0x170018D0 RID: 6352
		// (get) Token: 0x060076DF RID: 30431 RVA: 0x00238BF0 File Offset: 0x00238BF0
		public IMETADATASTREAM_STRING MetaDataStreamString
		{
			get
			{
				MetaDataStreamStringParser metaDataStreamStringParser = this._metaDataStreamStringParser;
				if (metaDataStreamStringParser == null)
				{
					return null;
				}
				return metaDataStreamStringParser.GetParserTarget();
			}
		}

		// Token: 0x170018D1 RID: 6353
		// (get) Token: 0x060076E0 RID: 30432 RVA: 0x00238C08 File Offset: 0x00238C08
		public IMETADATASTREAM_US MetaDataStreamUS
		{
			get
			{
				MetaDataStreamUSParser metaDataStreamUSParser = this._metaDataStreamUSParser;
				if (metaDataStreamUSParser == null)
				{
					return null;
				}
				return metaDataStreamUSParser.GetParserTarget();
			}
		}

		// Token: 0x170018D2 RID: 6354
		// (get) Token: 0x060076E1 RID: 30433 RVA: 0x00238C20 File Offset: 0x00238C20
		public IMETADATASTREAM_GUID MetaDataStreamGUID
		{
			get
			{
				MetaDataStreamGUIDParser metaDataStreamGuidParser = this._metaDataStreamGuidParser;
				if (metaDataStreamGuidParser == null)
				{
					return null;
				}
				return metaDataStreamGuidParser.GetParserTarget();
			}
		}

		// Token: 0x170018D3 RID: 6355
		// (get) Token: 0x060076E2 RID: 30434 RVA: 0x00238C38 File Offset: 0x00238C38
		public byte[] MetaDataStreamBlob
		{
			get
			{
				MetaDataStreamBlobParser metaDataStreamBlobParser = this._metaDataStreamBlobParser;
				if (metaDataStreamBlobParser == null)
				{
					return null;
				}
				return metaDataStreamBlobParser.GetParserTarget();
			}
		}

		// Token: 0x170018D4 RID: 6356
		// (get) Token: 0x060076E3 RID: 30435 RVA: 0x00238C50 File Offset: 0x00238C50
		public METADATATABLESHDR MetaDataStreamTablesHeader
		{
			get
			{
				MetaDataStreamTablesHeaderParser metaDataStreamTablesHeaderParser = this._metaDataStreamTablesHeaderParser;
				if (metaDataStreamTablesHeaderParser == null)
				{
					return null;
				}
				return metaDataStreamTablesHeaderParser.GetParserTarget();
			}
		}

		// Token: 0x060076E4 RID: 30436 RVA: 0x00238C68 File Offset: 0x00238C68
		public DotNetStructureParsers(byte[] buff, IMAGE_COR20_HEADER imageCor20Header, IMAGE_SECTION_HEADER[] sectionHeaders)
		{
			this._buff = buff;
			this._sectionHeaders = sectionHeaders;
			this._imageCor20Header = imageCor20Header;
			this.InitAllParsers();
		}

		// Token: 0x060076E5 RID: 30437 RVA: 0x00238C8C File Offset: 0x00238C8C
		private void InitAllParsers()
		{
			this._metaDataHdrParser = this.InitMetaDataParser();
			this._metaDataStreamStringParser = this.InitMetaDataStreamStringParser();
			this._metaDataStreamUSParser = this.InitMetaDataStreamUSParser();
			this._metaDataStreamTablesHeaderParser = this.InitMetaDataStreamTablesHeaderParser();
			this._metaDataStreamGuidParser = this.InitMetaDataStreamGUIDParser();
			this._metaDataStreamBlobParser = this.InitMetaDataStreamBlobParser();
		}

		// Token: 0x060076E6 RID: 30438 RVA: 0x00238CE8 File Offset: 0x00238CE8
		private MetaDataHdrParser InitMetaDataParser()
		{
			IMAGE_COR20_HEADER imageCor20Header = this._imageCor20Header;
			uint? num = (imageCor20Header != null) ? imageCor20Header.MetaData.VirtualAddress.SafeRVAtoFileMapping(this._sectionHeaders) : null;
			if (num != null)
			{
				return new MetaDataHdrParser(this._buff, num.Value);
			}
			return null;
		}

		// Token: 0x060076E7 RID: 30439 RVA: 0x00238D4C File Offset: 0x00238D4C
		private MetaDataStreamStringParser InitMetaDataStreamStringParser()
		{
			METADATAHDR metaDataHdr = this.MetaDataHdr;
			METADATASTREAMHDR metadatastreamhdr;
			if (metaDataHdr == null)
			{
				metadatastreamhdr = null;
			}
			else
			{
				METADATASTREAMHDR[] metaDataStreamsHdrs = metaDataHdr.MetaDataStreamsHdrs;
				if (metaDataStreamsHdrs == null)
				{
					metadatastreamhdr = null;
				}
				else
				{
					metadatastreamhdr = metaDataStreamsHdrs.FirstOrDefault((METADATASTREAMHDR x) => x.streamName == "#Strings");
				}
			}
			METADATASTREAMHDR metadatastreamhdr2 = metadatastreamhdr;
			if (metadatastreamhdr2 == null)
			{
				return null;
			}
			return new MetaDataStreamStringParser(this._buff, this.MetaDataHdr.Offset + metadatastreamhdr2.offset, metadatastreamhdr2.size);
		}

		// Token: 0x060076E8 RID: 30440 RVA: 0x00238DD8 File Offset: 0x00238DD8
		private MetaDataStreamUSParser InitMetaDataStreamUSParser()
		{
			METADATAHDR metaDataHdr = this.MetaDataHdr;
			METADATASTREAMHDR metadatastreamhdr;
			if (metaDataHdr == null)
			{
				metadatastreamhdr = null;
			}
			else
			{
				METADATASTREAMHDR[] metaDataStreamsHdrs = metaDataHdr.MetaDataStreamsHdrs;
				if (metaDataStreamsHdrs == null)
				{
					metadatastreamhdr = null;
				}
				else
				{
					metadatastreamhdr = metaDataStreamsHdrs.FirstOrDefault((METADATASTREAMHDR x) => x.streamName == "#US");
				}
			}
			METADATASTREAMHDR metadatastreamhdr2 = metadatastreamhdr;
			if (metadatastreamhdr2 == null)
			{
				return null;
			}
			return new MetaDataStreamUSParser(this._buff, this.MetaDataHdr.Offset + metadatastreamhdr2.offset, metadatastreamhdr2.size);
		}

		// Token: 0x060076E9 RID: 30441 RVA: 0x00238E64 File Offset: 0x00238E64
		private MetaDataStreamTablesHeaderParser InitMetaDataStreamTablesHeaderParser()
		{
			METADATAHDR metaDataHdr = this.MetaDataHdr;
			METADATASTREAMHDR metadatastreamhdr;
			if (metaDataHdr == null)
			{
				metadatastreamhdr = null;
			}
			else
			{
				METADATASTREAMHDR[] metaDataStreamsHdrs = metaDataHdr.MetaDataStreamsHdrs;
				if (metaDataStreamsHdrs == null)
				{
					metadatastreamhdr = null;
				}
				else
				{
					metadatastreamhdr = metaDataStreamsHdrs.FirstOrDefault((METADATASTREAMHDR x) => x.streamName == "#~");
				}
			}
			METADATASTREAMHDR metadatastreamhdr2 = metadatastreamhdr;
			if (metadatastreamhdr2 == null)
			{
				return null;
			}
			return new MetaDataStreamTablesHeaderParser(this._buff, this.MetaDataHdr.Offset + metadatastreamhdr2.offset);
		}

		// Token: 0x060076EA RID: 30442 RVA: 0x00238EE8 File Offset: 0x00238EE8
		private MetaDataStreamGUIDParser InitMetaDataStreamGUIDParser()
		{
			METADATAHDR metaDataHdr = this.MetaDataHdr;
			METADATASTREAMHDR metadatastreamhdr;
			if (metaDataHdr == null)
			{
				metadatastreamhdr = null;
			}
			else
			{
				METADATASTREAMHDR[] metaDataStreamsHdrs = metaDataHdr.MetaDataStreamsHdrs;
				if (metaDataStreamsHdrs == null)
				{
					metadatastreamhdr = null;
				}
				else
				{
					metadatastreamhdr = metaDataStreamsHdrs.FirstOrDefault((METADATASTREAMHDR x) => x.streamName == "#GUID");
				}
			}
			METADATASTREAMHDR metadatastreamhdr2 = metadatastreamhdr;
			if (metadatastreamhdr2 == null)
			{
				return null;
			}
			return new MetaDataStreamGUIDParser(this._buff, this.MetaDataHdr.Offset + metadatastreamhdr2.offset, metadatastreamhdr2.size);
		}

		// Token: 0x060076EB RID: 30443 RVA: 0x00238F74 File Offset: 0x00238F74
		private MetaDataStreamBlobParser InitMetaDataStreamBlobParser()
		{
			METADATAHDR metaDataHdr = this.MetaDataHdr;
			METADATASTREAMHDR metadatastreamhdr;
			if (metaDataHdr == null)
			{
				metadatastreamhdr = null;
			}
			else
			{
				METADATASTREAMHDR[] metaDataStreamsHdrs = metaDataHdr.MetaDataStreamsHdrs;
				if (metaDataStreamsHdrs == null)
				{
					metadatastreamhdr = null;
				}
				else
				{
					metadatastreamhdr = metaDataStreamsHdrs.FirstOrDefault((METADATASTREAMHDR x) => x.streamName == "#Blob");
				}
			}
			METADATASTREAMHDR metadatastreamhdr2 = metadatastreamhdr;
			if (metadatastreamhdr2 == null)
			{
				return null;
			}
			return new MetaDataStreamBlobParser(this._buff, this.MetaDataHdr.Offset + metadatastreamhdr2.offset, metadatastreamhdr2.size);
		}

		// Token: 0x040039C6 RID: 14790
		private readonly byte[] _buff;

		// Token: 0x040039C7 RID: 14791
		private readonly IMAGE_SECTION_HEADER[] _sectionHeaders;

		// Token: 0x040039C8 RID: 14792
		private readonly IMAGE_COR20_HEADER _imageCor20Header;

		// Token: 0x040039C9 RID: 14793
		private MetaDataHdrParser _metaDataHdrParser;

		// Token: 0x040039CA RID: 14794
		private MetaDataStreamStringParser _metaDataStreamStringParser;

		// Token: 0x040039CB RID: 14795
		private MetaDataStreamUSParser _metaDataStreamUSParser;

		// Token: 0x040039CC RID: 14796
		private MetaDataStreamTablesHeaderParser _metaDataStreamTablesHeaderParser;

		// Token: 0x040039CD RID: 14797
		private MetaDataStreamGUIDParser _metaDataStreamGuidParser;

		// Token: 0x040039CE RID: 14798
		private MetaDataStreamBlobParser _metaDataStreamBlobParser;
	}
}
