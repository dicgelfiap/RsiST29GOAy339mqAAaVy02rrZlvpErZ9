using System;
using System.Collections.Generic;
using System.Linq;
using PeNet.Parser;
using PeNet.Structures;
using PeNet.Utilities;

namespace PeNet
{
	// Token: 0x02000B8B RID: 2955
	internal class DataDirectoryParsers
	{
		// Token: 0x060076C0 RID: 30400 RVA: 0x00238548 File Offset: 0x00238548
		public DataDirectoryParsers(byte[] buff, ICollection<IMAGE_DATA_DIRECTORY> dataDirectories, ICollection<IMAGE_SECTION_HEADER> sectionHeaders, bool is32Bit)
		{
			this._buff = buff;
			this._dataDirectories = dataDirectories.ToArray<IMAGE_DATA_DIRECTORY>();
			this._sectionHeaders = sectionHeaders.ToArray<IMAGE_SECTION_HEADER>();
			this._is32Bit = is32Bit;
			this.InitAllParsers();
		}

		// Token: 0x170018C1 RID: 6337
		// (get) Token: 0x060076C1 RID: 30401 RVA: 0x00238580 File Offset: 0x00238580
		public IMAGE_EXPORT_DIRECTORY ImageExportDirectories
		{
			get
			{
				ImageExportDirectoriesParser imageExportDirectoriesParser = this._imageExportDirectoriesParser;
				if (imageExportDirectoriesParser == null)
				{
					return null;
				}
				return imageExportDirectoriesParser.GetParserTarget();
			}
		}

		// Token: 0x170018C2 RID: 6338
		// (get) Token: 0x060076C2 RID: 30402 RVA: 0x00238598 File Offset: 0x00238598
		public IMAGE_IMPORT_DESCRIPTOR[] ImageImportDescriptors
		{
			get
			{
				ImageImportDescriptorsParser imageImportDescriptorsParser = this._imageImportDescriptorsParser;
				if (imageImportDescriptorsParser == null)
				{
					return null;
				}
				return imageImportDescriptorsParser.GetParserTarget();
			}
		}

		// Token: 0x170018C3 RID: 6339
		// (get) Token: 0x060076C3 RID: 30403 RVA: 0x002385B0 File Offset: 0x002385B0
		public IMAGE_RESOURCE_DIRECTORY ImageResourceDirectory
		{
			get
			{
				ImageResourceDirectoryParser imageResourceDirectoryParser = this._imageResourceDirectoryParser;
				if (imageResourceDirectoryParser == null)
				{
					return null;
				}
				return imageResourceDirectoryParser.GetParserTarget();
			}
		}

		// Token: 0x170018C4 RID: 6340
		// (get) Token: 0x060076C4 RID: 30404 RVA: 0x002385C8 File Offset: 0x002385C8
		public IMAGE_BASE_RELOCATION[] ImageBaseRelocations
		{
			get
			{
				ImageBaseRelocationsParser imageBaseRelocationsParser = this._imageBaseRelocationsParser;
				if (imageBaseRelocationsParser == null)
				{
					return null;
				}
				return imageBaseRelocationsParser.GetParserTarget();
			}
		}

		// Token: 0x170018C5 RID: 6341
		// (get) Token: 0x060076C5 RID: 30405 RVA: 0x002385E0 File Offset: 0x002385E0
		public WIN_CERTIFICATE WinCertificate
		{
			get
			{
				WinCertificateParser winCertificateParser = this._winCertificateParser;
				if (winCertificateParser == null)
				{
					return null;
				}
				return winCertificateParser.GetParserTarget();
			}
		}

		// Token: 0x170018C6 RID: 6342
		// (get) Token: 0x060076C6 RID: 30406 RVA: 0x002385F8 File Offset: 0x002385F8
		public IMAGE_DEBUG_DIRECTORY[] ImageDebugDirectory
		{
			get
			{
				ImageDebugDirectoryParser imageDebugDirectoryParser = this._imageDebugDirectoryParser;
				if (imageDebugDirectoryParser == null)
				{
					return null;
				}
				return imageDebugDirectoryParser.GetParserTarget();
			}
		}

		// Token: 0x170018C7 RID: 6343
		// (get) Token: 0x060076C7 RID: 30407 RVA: 0x00238610 File Offset: 0x00238610
		public RUNTIME_FUNCTION[] RuntimeFunctions
		{
			get
			{
				RuntimeFunctionsParser runtimeFunctionsParser = this._runtimeFunctionsParser;
				if (runtimeFunctionsParser == null)
				{
					return null;
				}
				return runtimeFunctionsParser.GetParserTarget();
			}
		}

		// Token: 0x170018C8 RID: 6344
		// (get) Token: 0x060076C8 RID: 30408 RVA: 0x00238628 File Offset: 0x00238628
		public ExportFunction[] ExportFunctions
		{
			get
			{
				ExportedFunctionsParser exportedFunctionsParser = this._exportedFunctionsParser;
				if (exportedFunctionsParser == null)
				{
					return null;
				}
				return exportedFunctionsParser.GetParserTarget();
			}
		}

		// Token: 0x170018C9 RID: 6345
		// (get) Token: 0x060076C9 RID: 30409 RVA: 0x00238640 File Offset: 0x00238640
		public ImportFunction[] ImportFunctions
		{
			get
			{
				ImportedFunctionsParser importedFunctionsParser = this._importedFunctionsParser;
				if (importedFunctionsParser == null)
				{
					return null;
				}
				return importedFunctionsParser.GetParserTarget();
			}
		}

		// Token: 0x170018CA RID: 6346
		// (get) Token: 0x060076CA RID: 30410 RVA: 0x00238658 File Offset: 0x00238658
		public IMAGE_BOUND_IMPORT_DESCRIPTOR ImageBoundImportDescriptor
		{
			get
			{
				ImageBoundImportDescriptorParser imageBoundImportDescriptorParser = this._imageBoundImportDescriptorParser;
				if (imageBoundImportDescriptorParser == null)
				{
					return null;
				}
				return imageBoundImportDescriptorParser.GetParserTarget();
			}
		}

		// Token: 0x170018CB RID: 6347
		// (get) Token: 0x060076CB RID: 30411 RVA: 0x00238670 File Offset: 0x00238670
		public IMAGE_TLS_DIRECTORY ImageTlsDirectory
		{
			get
			{
				ImageTlsDirectoryParser imageTlsDirectoryParser = this._imageTlsDirectoryParser;
				if (imageTlsDirectoryParser == null)
				{
					return null;
				}
				return imageTlsDirectoryParser.GetParserTarget();
			}
		}

		// Token: 0x170018CC RID: 6348
		// (get) Token: 0x060076CC RID: 30412 RVA: 0x00238688 File Offset: 0x00238688
		public IMAGE_DELAY_IMPORT_DESCRIPTOR ImageDelayImportDescriptor
		{
			get
			{
				ImageDelayImportDescriptorParser imageDelayImportDescriptorParser = this._imageDelayImportDescriptorParser;
				if (imageDelayImportDescriptorParser == null)
				{
					return null;
				}
				return imageDelayImportDescriptorParser.GetParserTarget();
			}
		}

		// Token: 0x170018CD RID: 6349
		// (get) Token: 0x060076CD RID: 30413 RVA: 0x002386A0 File Offset: 0x002386A0
		public IMAGE_LOAD_CONFIG_DIRECTORY ImageLoadConfigDirectory
		{
			get
			{
				ImageLoadConfigDirectoryParser imageLoadConfigDirectoryParser = this._imageLoadConfigDirectoryParser;
				if (imageLoadConfigDirectoryParser == null)
				{
					return null;
				}
				return imageLoadConfigDirectoryParser.GetParserTarget();
			}
		}

		// Token: 0x170018CE RID: 6350
		// (get) Token: 0x060076CE RID: 30414 RVA: 0x002386B8 File Offset: 0x002386B8
		public IMAGE_COR20_HEADER ImageComDescriptor
		{
			get
			{
				ImageCor20HeaderParser imageCor20HeaderParser = this._imageCor20HeaderParser;
				if (imageCor20HeaderParser == null)
				{
					return null;
				}
				return imageCor20HeaderParser.GetParserTarget();
			}
		}

		// Token: 0x060076CF RID: 30415 RVA: 0x002386D0 File Offset: 0x002386D0
		private void InitAllParsers()
		{
			this._imageExportDirectoriesParser = this.InitImageExportDirectoryParser();
			this._runtimeFunctionsParser = this.InitRuntimeFunctionsParser();
			this._imageImportDescriptorsParser = this.InitImageImportDescriptorsParser();
			this._imageBaseRelocationsParser = this.InitImageBaseRelocationsParser();
			this._imageResourceDirectoryParser = this.InitImageResourceDirectoryParser();
			this._imageDebugDirectoryParser = this.InitImageDebugDirectoryParser();
			this._winCertificateParser = this.InitWinCertificateParser();
			this._exportedFunctionsParser = this.InitExportFunctionParser();
			this._importedFunctionsParser = this.InitImportedFunctionsParser();
			this._imageBoundImportDescriptorParser = this.InitBoundImportDescriptorParser();
			this._imageTlsDirectoryParser = this.InitImageTlsDirectoryParser();
			this._imageDelayImportDescriptorParser = this.InitImageDelayImportDescriptorParser();
			this._imageLoadConfigDirectoryParser = this.InitImageLoadConfigDirectoryParser();
			this._imageCor20HeaderParser = this.InitImageComDescriptorParser();
		}

		// Token: 0x060076D0 RID: 30416 RVA: 0x0023878C File Offset: 0x0023878C
		private ImageCor20HeaderParser InitImageComDescriptorParser()
		{
			uint? num = this._dataDirectories[14].VirtualAddress.SafeRVAtoFileMapping(this._sectionHeaders);
			if (num != null)
			{
				return new ImageCor20HeaderParser(this._buff, num.Value);
			}
			return null;
		}

		// Token: 0x060076D1 RID: 30417 RVA: 0x002387DC File Offset: 0x002387DC
		private ImageLoadConfigDirectoryParser InitImageLoadConfigDirectoryParser()
		{
			uint? num = this._dataDirectories[10].VirtualAddress.SafeRVAtoFileMapping(this._sectionHeaders);
			if (num != null)
			{
				return new ImageLoadConfigDirectoryParser(this._buff, num.Value, !this._is32Bit);
			}
			return null;
		}

		// Token: 0x060076D2 RID: 30418 RVA: 0x00238834 File Offset: 0x00238834
		private ImageDelayImportDescriptorParser InitImageDelayImportDescriptorParser()
		{
			uint? num = this._dataDirectories[13].VirtualAddress.SafeRVAtoFileMapping(this._sectionHeaders);
			if (num != null)
			{
				return new ImageDelayImportDescriptorParser(this._buff, num.Value);
			}
			return null;
		}

		// Token: 0x060076D3 RID: 30419 RVA: 0x00238884 File Offset: 0x00238884
		private ImageTlsDirectoryParser InitImageTlsDirectoryParser()
		{
			uint? num = this._dataDirectories[9].VirtualAddress.SafeRVAtoFileMapping(this._sectionHeaders);
			if (num != null)
			{
				return new ImageTlsDirectoryParser(this._buff, num.Value, !this._is32Bit, this._sectionHeaders);
			}
			return null;
		}

		// Token: 0x060076D4 RID: 30420 RVA: 0x002388E4 File Offset: 0x002388E4
		private ImageBoundImportDescriptorParser InitBoundImportDescriptorParser()
		{
			uint? num = this._dataDirectories[11].VirtualAddress.SafeRVAtoFileMapping(this._sectionHeaders);
			if (num != null)
			{
				return new ImageBoundImportDescriptorParser(this._buff, num.Value);
			}
			return null;
		}

		// Token: 0x060076D5 RID: 30421 RVA: 0x00238934 File Offset: 0x00238934
		private ImportedFunctionsParser InitImportedFunctionsParser()
		{
			return new ImportedFunctionsParser(this._buff, this.ImageImportDescriptors, this._sectionHeaders, !this._is32Bit);
		}

		// Token: 0x060076D6 RID: 30422 RVA: 0x00238958 File Offset: 0x00238958
		private ExportedFunctionsParser InitExportFunctionParser()
		{
			return new ExportedFunctionsParser(this._buff, this.ImageExportDirectories, this._sectionHeaders, this._dataDirectories[0]);
		}

		// Token: 0x060076D7 RID: 30423 RVA: 0x00238980 File Offset: 0x00238980
		private WinCertificateParser InitWinCertificateParser()
		{
			uint virtualAddress = this._dataDirectories[4].VirtualAddress;
			if (virtualAddress == 0U)
			{
				return null;
			}
			return new WinCertificateParser(this._buff, virtualAddress);
		}

		// Token: 0x060076D8 RID: 30424 RVA: 0x002389B8 File Offset: 0x002389B8
		private ImageDebugDirectoryParser InitImageDebugDirectoryParser()
		{
			uint? num = this._dataDirectories[6].VirtualAddress.SafeRVAtoFileMapping(this._sectionHeaders);
			uint size = this._dataDirectories[6].Size;
			if (num != null)
			{
				return new ImageDebugDirectoryParser(this._buff, num.Value, size);
			}
			return null;
		}

		// Token: 0x060076D9 RID: 30425 RVA: 0x00238A1C File Offset: 0x00238A1C
		private ImageResourceDirectoryParser InitImageResourceDirectoryParser()
		{
			uint? num = this._dataDirectories[2].VirtualAddress.SafeRVAtoFileMapping(this._sectionHeaders);
			if (num != null)
			{
				return new ImageResourceDirectoryParser(this._buff, num.Value);
			}
			return null;
		}

		// Token: 0x060076DA RID: 30426 RVA: 0x00238A6C File Offset: 0x00238A6C
		private ImageBaseRelocationsParser InitImageBaseRelocationsParser()
		{
			uint? num = this._dataDirectories[5].VirtualAddress.SafeRVAtoFileMapping(this._sectionHeaders);
			if (num == null)
			{
				return null;
			}
			return new ImageBaseRelocationsParser(this._buff, num.Value, this._dataDirectories[5].Size);
		}

		// Token: 0x060076DB RID: 30427 RVA: 0x00238ACC File Offset: 0x00238ACC
		private ImageExportDirectoriesParser InitImageExportDirectoryParser()
		{
			uint? num = this._dataDirectories[0].VirtualAddress.SafeRVAtoFileMapping(this._sectionHeaders);
			if (num == null)
			{
				return null;
			}
			return new ImageExportDirectoriesParser(this._buff, num.Value);
		}

		// Token: 0x060076DC RID: 30428 RVA: 0x00238B1C File Offset: 0x00238B1C
		private RuntimeFunctionsParser InitRuntimeFunctionsParser()
		{
			uint? num = this._dataDirectories[3].VirtualAddress.SafeRVAtoFileMapping(this._sectionHeaders);
			if (num == null)
			{
				return null;
			}
			return new RuntimeFunctionsParser(this._buff, num.Value, this._is32Bit, this._dataDirectories[3].Size, this._sectionHeaders);
		}

		// Token: 0x060076DD RID: 30429 RVA: 0x00238B88 File Offset: 0x00238B88
		private ImageImportDescriptorsParser InitImageImportDescriptorsParser()
		{
			uint? num = this._dataDirectories[1].VirtualAddress.SafeRVAtoFileMapping(this._sectionHeaders);
			if (num != null)
			{
				return new ImageImportDescriptorsParser(this._buff, num.Value);
			}
			return null;
		}

		// Token: 0x040039B4 RID: 14772
		private readonly byte[] _buff;

		// Token: 0x040039B5 RID: 14773
		private readonly IMAGE_DATA_DIRECTORY[] _dataDirectories;

		// Token: 0x040039B6 RID: 14774
		private readonly bool _is32Bit;

		// Token: 0x040039B7 RID: 14775
		private readonly IMAGE_SECTION_HEADER[] _sectionHeaders;

		// Token: 0x040039B8 RID: 14776
		private ExportedFunctionsParser _exportedFunctionsParser;

		// Token: 0x040039B9 RID: 14777
		private ImageBaseRelocationsParser _imageBaseRelocationsParser;

		// Token: 0x040039BA RID: 14778
		private ImageDebugDirectoryParser _imageDebugDirectoryParser;

		// Token: 0x040039BB RID: 14779
		private ImageBoundImportDescriptorParser _imageBoundImportDescriptorParser;

		// Token: 0x040039BC RID: 14780
		private ImageExportDirectoriesParser _imageExportDirectoriesParser;

		// Token: 0x040039BD RID: 14781
		private ImageImportDescriptorsParser _imageImportDescriptorsParser;

		// Token: 0x040039BE RID: 14782
		private ImageResourceDirectoryParser _imageResourceDirectoryParser;

		// Token: 0x040039BF RID: 14783
		private ImportedFunctionsParser _importedFunctionsParser;

		// Token: 0x040039C0 RID: 14784
		private RuntimeFunctionsParser _runtimeFunctionsParser;

		// Token: 0x040039C1 RID: 14785
		private WinCertificateParser _winCertificateParser;

		// Token: 0x040039C2 RID: 14786
		private ImageTlsDirectoryParser _imageTlsDirectoryParser;

		// Token: 0x040039C3 RID: 14787
		private ImageDelayImportDescriptorParser _imageDelayImportDescriptorParser;

		// Token: 0x040039C4 RID: 14788
		private ImageLoadConfigDirectoryParser _imageLoadConfigDirectoryParser;

		// Token: 0x040039C5 RID: 14789
		private ImageCor20HeaderParser _imageCor20HeaderParser;
	}
}
