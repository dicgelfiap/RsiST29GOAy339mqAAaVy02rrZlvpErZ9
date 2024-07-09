using System;
using PeNet.Parser;
using PeNet.Structures;
using PeNet.Utilities;

namespace PeNet
{
	// Token: 0x02000B91 RID: 2961
	internal class NativeStructureParsers
	{
		// Token: 0x06007719 RID: 30489 RVA: 0x00239334 File Offset: 0x00239334
		internal NativeStructureParsers(byte[] buff)
		{
			this._buff = buff;
			this.InitAllParsers();
		}

		// Token: 0x170018EE RID: 6382
		// (get) Token: 0x0600771A RID: 30490 RVA: 0x0023934C File Offset: 0x0023934C
		public IMAGE_DOS_HEADER ImageDosHeader
		{
			get
			{
				ImageDosHeaderParser imageDosHeaderParser = this._imageDosHeaderParser;
				if (imageDosHeaderParser == null)
				{
					return null;
				}
				return imageDosHeaderParser.GetParserTarget();
			}
		}

		// Token: 0x170018EF RID: 6383
		// (get) Token: 0x0600771B RID: 30491 RVA: 0x00239364 File Offset: 0x00239364
		public IMAGE_NT_HEADERS ImageNtHeaders
		{
			get
			{
				ImageNtHeadersParser imageNtHeadersParser = this._imageNtHeadersParser;
				if (imageNtHeadersParser == null)
				{
					return null;
				}
				return imageNtHeadersParser.GetParserTarget();
			}
		}

		// Token: 0x170018F0 RID: 6384
		// (get) Token: 0x0600771C RID: 30492 RVA: 0x0023937C File Offset: 0x0023937C
		public IMAGE_SECTION_HEADER[] ImageSectionHeaders
		{
			get
			{
				ImageSectionHeadersParser imageSectionHeadersParser = this._imageSectionHeadersParser;
				if (imageSectionHeadersParser == null)
				{
					return null;
				}
				return imageSectionHeadersParser.GetParserTarget();
			}
		}

		// Token: 0x170018F1 RID: 6385
		// (get) Token: 0x0600771D RID: 30493 RVA: 0x00239394 File Offset: 0x00239394
		private bool Is64Bit
		{
			get
			{
				return this._buff.BytesToUInt16((ulong)(this.ImageDosHeader.e_lfanew + 4U)) == 34404;
			}
		}

		// Token: 0x0600771E RID: 30494 RVA: 0x002393B8 File Offset: 0x002393B8
		private void InitAllParsers()
		{
			this._imageDosHeaderParser = this.InitImageDosHeaderParser();
			this._imageNtHeadersParser = this.InitNtHeadersParser();
			this._imageSectionHeadersParser = this.InitImageSectionHeadersParser();
		}

		// Token: 0x0600771F RID: 30495 RVA: 0x002393E0 File Offset: 0x002393E0
		private ImageSectionHeadersParser InitImageSectionHeadersParser()
		{
			return new ImageSectionHeadersParser(this._buff, this.GetSecHeaderOffset(), this.ImageNtHeaders.FileHeader.NumberOfSections, this.ImageNtHeaders.OptionalHeader.ImageBase);
		}

		// Token: 0x06007720 RID: 30496 RVA: 0x00239424 File Offset: 0x00239424
		private ImageNtHeadersParser InitNtHeadersParser()
		{
			return new ImageNtHeadersParser(this._buff, this.ImageDosHeader.e_lfanew, this.Is64Bit);
		}

		// Token: 0x06007721 RID: 30497 RVA: 0x00239454 File Offset: 0x00239454
		private ImageDosHeaderParser InitImageDosHeaderParser()
		{
			return new ImageDosHeaderParser(this._buff, 0U);
		}

		// Token: 0x06007722 RID: 30498 RVA: 0x00239464 File Offset: 0x00239464
		private uint GetSecHeaderOffset()
		{
			uint num = (uint)(this.ImageNtHeaders.FileHeader.SizeOfOptionalHeader + 24);
			return this.ImageDosHeader.e_lfanew + num;
		}

		// Token: 0x040039E5 RID: 14821
		private readonly byte[] _buff;

		// Token: 0x040039E6 RID: 14822
		private ImageDosHeaderParser _imageDosHeaderParser;

		// Token: 0x040039E7 RID: 14823
		private ImageNtHeadersParser _imageNtHeadersParser;

		// Token: 0x040039E8 RID: 14824
		private ImageSectionHeadersParser _imageSectionHeadersParser;
	}
}
