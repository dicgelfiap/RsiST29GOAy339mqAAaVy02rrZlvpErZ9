using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using PeNet.Authenticode;
using PeNet.ImpHash;
using PeNet.Structures;
using PeNet.Utilities;

namespace PeNet
{
	// Token: 0x02000B92 RID: 2962
	[ComVisible(true)]
	public class PeFile : AbstractStructure
	{
		// Token: 0x170018F2 RID: 6386
		// (get) Token: 0x06007723 RID: 30499 RVA: 0x00239498 File Offset: 0x00239498
		[JsonIgnore]
		public new byte[] Buff
		{
			get
			{
				return this.Buff;
			}
		}

		// Token: 0x170018F3 RID: 6387
		// (get) Token: 0x06007724 RID: 30500 RVA: 0x002394A0 File Offset: 0x002394A0
		[JsonIgnore]
		public Stream Stream
		{
			get
			{
				Stream result;
				if ((result = this._stream) == null)
				{
					result = (this._stream = new MemoryStream(this.Buff));
				}
				return result;
			}
		}

		// Token: 0x06007725 RID: 30501 RVA: 0x002394D4 File Offset: 0x002394D4
		public PeFile(byte[] buff) : base(buff, 0U)
		{
			this._nativeStructureParsers = new NativeStructureParsers(this.Buff);
			byte[] buff2 = this.Buff;
			IMAGE_NT_HEADERS imageNtHeaders = this.ImageNtHeaders;
			ICollection<IMAGE_DATA_DIRECTORY> dataDirectories;
			if (imageNtHeaders == null)
			{
				dataDirectories = null;
			}
			else
			{
				IMAGE_OPTIONAL_HEADER optionalHeader = imageNtHeaders.OptionalHeader;
				dataDirectories = ((optionalHeader != null) ? optionalHeader.DataDirectory : null);
			}
			this._dataDirectoryParsers = new DataDirectoryParsers(buff2, dataDirectories, this.ImageSectionHeaders, this.Is32Bit);
			this._dotNetStructureParsers = new DotNetStructureParsers(this.Buff, this.ImageComDescriptor, this.ImageSectionHeaders);
			this._authenticodeParser = new AuthenticodeParser(this);
		}

		// Token: 0x06007726 RID: 30502 RVA: 0x00239570 File Offset: 0x00239570
		public PeFile(string peFile) : this(File.ReadAllBytes(peFile))
		{
			this.FileLocation = peFile;
		}

		// Token: 0x06007727 RID: 30503 RVA: 0x00239588 File Offset: 0x00239588
		public void SaveAs(string path)
		{
			File.WriteAllBytes(path, this.Buff);
		}

		// Token: 0x170018F4 RID: 6388
		// (get) Token: 0x06007728 RID: 30504 RVA: 0x00239598 File Offset: 0x00239598
		public bool HasValidExportDir
		{
			get
			{
				return this.ImageExportDirectory != null;
			}
		}

		// Token: 0x170018F5 RID: 6389
		// (get) Token: 0x06007729 RID: 30505 RVA: 0x002395A4 File Offset: 0x002395A4
		public bool HasValidImportDir
		{
			get
			{
				return this.ImageImportDescriptors != null;
			}
		}

		// Token: 0x170018F6 RID: 6390
		// (get) Token: 0x0600772A RID: 30506 RVA: 0x002395B0 File Offset: 0x002395B0
		public bool HasValidResourceDir
		{
			get
			{
				return this.ImageResourceDirectory != null;
			}
		}

		// Token: 0x170018F7 RID: 6391
		// (get) Token: 0x0600772B RID: 30507 RVA: 0x002395BC File Offset: 0x002395BC
		public bool HasValidDir
		{
			get
			{
				return this.ExceptionDirectory != null;
			}
		}

		// Token: 0x170018F8 RID: 6392
		// (get) Token: 0x0600772C RID: 30508 RVA: 0x002395C8 File Offset: 0x002395C8
		public bool HasValidSecurityDir
		{
			get
			{
				return this.WinCertificate != null;
			}
		}

		// Token: 0x170018F9 RID: 6393
		// (get) Token: 0x0600772D RID: 30509 RVA: 0x002395D4 File Offset: 0x002395D4
		public bool HasValidRelocDir
		{
			get
			{
				return this.ImageRelocationDirectory != null;
			}
		}

		// Token: 0x170018FA RID: 6394
		// (get) Token: 0x0600772E RID: 30510 RVA: 0x002395E0 File Offset: 0x002395E0
		public bool HasValidComDescriptor
		{
			get
			{
				return this.ImageComDescriptor != null;
			}
		}

		// Token: 0x170018FB RID: 6395
		// (get) Token: 0x0600772F RID: 30511 RVA: 0x002395EC File Offset: 0x002395EC
		public bool IsDLL
		{
			get
			{
				return (this.ImageNtHeaders.FileHeader.Characteristics & 8192) > 0;
			}
		}

		// Token: 0x170018FC RID: 6396
		// (get) Token: 0x06007730 RID: 30512 RVA: 0x00239608 File Offset: 0x00239608
		public bool IsEXE
		{
			get
			{
				return (this.ImageNtHeaders.FileHeader.Characteristics & 2) > 0;
			}
		}

		// Token: 0x170018FD RID: 6397
		// (get) Token: 0x06007731 RID: 30513 RVA: 0x00239620 File Offset: 0x00239620
		public bool IsDriver
		{
			get
			{
				if (this.ImageNtHeaders.OptionalHeader.Subsystem == 1)
				{
					return this.ImportedFunctions.FirstOrDefault((ImportFunction i) => i.DLL == "ntoskrnl.exe") != null;
				}
				return false;
			}
		}

		// Token: 0x170018FE RID: 6398
		// (get) Token: 0x06007732 RID: 30514 RVA: 0x0023967C File Offset: 0x0023967C
		public bool IsSigned
		{
			get
			{
				return this.PKCS7 != null;
			}
		}

		// Token: 0x170018FF RID: 6399
		// (get) Token: 0x06007733 RID: 30515 RVA: 0x00239688 File Offset: 0x00239688
		public bool IsSignatureValid
		{
			get
			{
				AuthenticodeInfo authenticode = this.Authenticode;
				return authenticode != null && authenticode.IsAuthenticodeValid;
			}
		}

		// Token: 0x17001900 RID: 6400
		// (get) Token: 0x06007734 RID: 30516 RVA: 0x002396A0 File Offset: 0x002396A0
		public AuthenticodeInfo Authenticode
		{
			get
			{
				return this._authenticodeParser.GetParserTarget();
			}
		}

		// Token: 0x17001901 RID: 6401
		// (get) Token: 0x06007735 RID: 30517 RVA: 0x002396B0 File Offset: 0x002396B0
		public bool Is64Bit
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)(this.ImageDosHeader.e_lfanew + 4U)) == 34404;
			}
		}

		// Token: 0x17001902 RID: 6402
		// (get) Token: 0x06007736 RID: 30518 RVA: 0x002396E4 File Offset: 0x002396E4
		public bool Is32Bit
		{
			get
			{
				return !this.Is64Bit;
			}
		}

		// Token: 0x17001903 RID: 6403
		// (get) Token: 0x06007737 RID: 30519 RVA: 0x002396F0 File Offset: 0x002396F0
		public IMAGE_DOS_HEADER ImageDosHeader
		{
			get
			{
				return this._nativeStructureParsers.ImageDosHeader;
			}
		}

		// Token: 0x17001904 RID: 6404
		// (get) Token: 0x06007738 RID: 30520 RVA: 0x00239700 File Offset: 0x00239700
		public IMAGE_NT_HEADERS ImageNtHeaders
		{
			get
			{
				return this._nativeStructureParsers.ImageNtHeaders;
			}
		}

		// Token: 0x17001905 RID: 6405
		// (get) Token: 0x06007739 RID: 30521 RVA: 0x00239710 File Offset: 0x00239710
		public IMAGE_SECTION_HEADER[] ImageSectionHeaders
		{
			get
			{
				return this._nativeStructureParsers.ImageSectionHeaders;
			}
		}

		// Token: 0x17001906 RID: 6406
		// (get) Token: 0x0600773A RID: 30522 RVA: 0x00239720 File Offset: 0x00239720
		public IMAGE_EXPORT_DIRECTORY ImageExportDirectory
		{
			get
			{
				return this._dataDirectoryParsers.ImageExportDirectories;
			}
		}

		// Token: 0x17001907 RID: 6407
		// (get) Token: 0x0600773B RID: 30523 RVA: 0x00239730 File Offset: 0x00239730
		public IMAGE_IMPORT_DESCRIPTOR[] ImageImportDescriptors
		{
			get
			{
				return this._dataDirectoryParsers.ImageImportDescriptors;
			}
		}

		// Token: 0x17001908 RID: 6408
		// (get) Token: 0x0600773C RID: 30524 RVA: 0x00239740 File Offset: 0x00239740
		public IMAGE_BASE_RELOCATION[] ImageRelocationDirectory
		{
			get
			{
				return this._dataDirectoryParsers.ImageBaseRelocations;
			}
		}

		// Token: 0x17001909 RID: 6409
		// (get) Token: 0x0600773D RID: 30525 RVA: 0x00239750 File Offset: 0x00239750
		public IMAGE_DEBUG_DIRECTORY[] ImageDebugDirectory
		{
			get
			{
				return this._dataDirectoryParsers.ImageDebugDirectory;
			}
		}

		// Token: 0x1700190A RID: 6410
		// (get) Token: 0x0600773E RID: 30526 RVA: 0x00239760 File Offset: 0x00239760
		public ExportFunction[] ExportedFunctions
		{
			get
			{
				return this._dataDirectoryParsers.ExportFunctions;
			}
		}

		// Token: 0x1700190B RID: 6411
		// (get) Token: 0x0600773F RID: 30527 RVA: 0x00239770 File Offset: 0x00239770
		public ImportFunction[] ImportedFunctions
		{
			get
			{
				return this._dataDirectoryParsers.ImportFunctions;
			}
		}

		// Token: 0x1700190C RID: 6412
		// (get) Token: 0x06007740 RID: 30528 RVA: 0x00239780 File Offset: 0x00239780
		public IMAGE_RESOURCE_DIRECTORY ImageResourceDirectory
		{
			get
			{
				return this._dataDirectoryParsers.ImageResourceDirectory;
			}
		}

		// Token: 0x1700190D RID: 6413
		// (get) Token: 0x06007741 RID: 30529 RVA: 0x00239790 File Offset: 0x00239790
		public RUNTIME_FUNCTION[] ExceptionDirectory
		{
			get
			{
				return this._dataDirectoryParsers.RuntimeFunctions;
			}
		}

		// Token: 0x1700190E RID: 6414
		// (get) Token: 0x06007742 RID: 30530 RVA: 0x002397A0 File Offset: 0x002397A0
		public WIN_CERTIFICATE WinCertificate
		{
			get
			{
				return this._dataDirectoryParsers.WinCertificate;
			}
		}

		// Token: 0x1700190F RID: 6415
		// (get) Token: 0x06007743 RID: 30531 RVA: 0x002397B0 File Offset: 0x002397B0
		public IMAGE_BOUND_IMPORT_DESCRIPTOR ImageBoundImportDescriptor
		{
			get
			{
				return this._dataDirectoryParsers.ImageBoundImportDescriptor;
			}
		}

		// Token: 0x17001910 RID: 6416
		// (get) Token: 0x06007744 RID: 30532 RVA: 0x002397C0 File Offset: 0x002397C0
		public IMAGE_TLS_DIRECTORY ImageTlsDirectory
		{
			get
			{
				return this._dataDirectoryParsers.ImageTlsDirectory;
			}
		}

		// Token: 0x17001911 RID: 6417
		// (get) Token: 0x06007745 RID: 30533 RVA: 0x002397D0 File Offset: 0x002397D0
		public IMAGE_DELAY_IMPORT_DESCRIPTOR ImageDelayImportDescriptor
		{
			get
			{
				return this._dataDirectoryParsers.ImageDelayImportDescriptor;
			}
		}

		// Token: 0x17001912 RID: 6418
		// (get) Token: 0x06007746 RID: 30534 RVA: 0x002397E0 File Offset: 0x002397E0
		public IMAGE_LOAD_CONFIG_DIRECTORY ImageLoadConfigDirectory
		{
			get
			{
				return this._dataDirectoryParsers.ImageLoadConfigDirectory;
			}
		}

		// Token: 0x17001913 RID: 6419
		// (get) Token: 0x06007747 RID: 30535 RVA: 0x002397F0 File Offset: 0x002397F0
		public IMAGE_COR20_HEADER ImageComDescriptor
		{
			get
			{
				return this._dataDirectoryParsers.ImageComDescriptor;
			}
		}

		// Token: 0x17001914 RID: 6420
		// (get) Token: 0x06007748 RID: 30536 RVA: 0x00239800 File Offset: 0x00239800
		public X509Certificate2 PKCS7
		{
			get
			{
				AuthenticodeInfo authenticode = this.Authenticode;
				if (authenticode == null)
				{
					return null;
				}
				return authenticode.SigningCertificate;
			}
		}

		// Token: 0x17001915 RID: 6421
		// (get) Token: 0x06007749 RID: 30537 RVA: 0x00239818 File Offset: 0x00239818
		public METADATAHDR MetaDataHdr
		{
			get
			{
				return this._dotNetStructureParsers.MetaDataHdr;
			}
		}

		// Token: 0x17001916 RID: 6422
		// (get) Token: 0x0600774A RID: 30538 RVA: 0x00239828 File Offset: 0x00239828
		public IMETADATASTREAM_STRING MetaDataStreamString
		{
			get
			{
				return this._dotNetStructureParsers.MetaDataStreamString;
			}
		}

		// Token: 0x17001917 RID: 6423
		// (get) Token: 0x0600774B RID: 30539 RVA: 0x00239838 File Offset: 0x00239838
		public IMETADATASTREAM_US MetaDataStreamUS
		{
			get
			{
				return this._dotNetStructureParsers.MetaDataStreamUS;
			}
		}

		// Token: 0x17001918 RID: 6424
		// (get) Token: 0x0600774C RID: 30540 RVA: 0x00239848 File Offset: 0x00239848
		public IMETADATASTREAM_GUID MetaDataStreamGUID
		{
			get
			{
				return this._dotNetStructureParsers.MetaDataStreamGUID;
			}
		}

		// Token: 0x17001919 RID: 6425
		// (get) Token: 0x0600774D RID: 30541 RVA: 0x00239858 File Offset: 0x00239858
		public byte[] MetaDataStreamBlob
		{
			get
			{
				return this._dotNetStructureParsers.MetaDataStreamBlob;
			}
		}

		// Token: 0x1700191A RID: 6426
		// (get) Token: 0x0600774E RID: 30542 RVA: 0x00239868 File Offset: 0x00239868
		public METADATATABLESHDR MetaDataStreamTablesHeader
		{
			get
			{
				return this._dotNetStructureParsers.MetaDataStreamTablesHeader;
			}
		}

		// Token: 0x1700191B RID: 6427
		// (get) Token: 0x0600774F RID: 30543 RVA: 0x00239878 File Offset: 0x00239878
		public string SHA256
		{
			get
			{
				string result;
				if ((result = this._sha256) == null)
				{
					result = (this._sha256 = Hashes.Sha256(this.Buff));
				}
				return result;
			}
		}

		// Token: 0x1700191C RID: 6428
		// (get) Token: 0x06007750 RID: 30544 RVA: 0x002398AC File Offset: 0x002398AC
		public string SHA1
		{
			get
			{
				string result;
				if ((result = this._sha1) == null)
				{
					result = (this._sha1 = Hashes.Sha1(this.Buff));
				}
				return result;
			}
		}

		// Token: 0x1700191D RID: 6429
		// (get) Token: 0x06007751 RID: 30545 RVA: 0x002398E0 File Offset: 0x002398E0
		public string MD5
		{
			get
			{
				string result;
				if ((result = this._md5) == null)
				{
					result = (this._md5 = Hashes.MD5(this.Buff));
				}
				return result;
			}
		}

		// Token: 0x1700191E RID: 6430
		// (get) Token: 0x06007752 RID: 30546 RVA: 0x00239914 File Offset: 0x00239914
		public string ImpHash
		{
			get
			{
				string result;
				if ((result = this._impHash) == null)
				{
					result = (this._impHash = new ImportHash(this.ImportedFunctions).ImpHash);
				}
				return result;
			}
		}

		// Token: 0x1700191F RID: 6431
		// (get) Token: 0x06007753 RID: 30547 RVA: 0x0023994C File Offset: 0x0023994C
		public int FileSize
		{
			get
			{
				return this.Buff.Length;
			}
		}

		// Token: 0x17001920 RID: 6432
		// (get) Token: 0x06007754 RID: 30548 RVA: 0x00239958 File Offset: 0x00239958
		// (set) Token: 0x06007755 RID: 30549 RVA: 0x00239960 File Offset: 0x00239960
		public string FileLocation { get; private set; }

		// Token: 0x06007756 RID: 30550 RVA: 0x0023996C File Offset: 0x0023996C
		public bool IsValidCertChain(bool online)
		{
			return this.IsSigned && SignatureInformation.IsValidCertChain(this.PKCS7, online);
		}

		// Token: 0x06007757 RID: 30551 RVA: 0x00239988 File Offset: 0x00239988
		public CrlUrlList GetCrlUrlList()
		{
			if (this.PKCS7 == null)
			{
				return null;
			}
			CrlUrlList result;
			try
			{
				result = new CrlUrlList(this.PKCS7);
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06007758 RID: 30552 RVA: 0x002399D0 File Offset: 0x002399D0
		public static bool IsPEFile(string file)
		{
			byte[] array = new byte[2];
			using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
			{
				fileStream.Read(array, 0, array.Length);
			}
			return array[1] == 90 && array[0] == 77;
		}

		// Token: 0x06007759 RID: 30553 RVA: 0x00239A30 File Offset: 0x00239A30
		public static bool Is64BitPeFile(string file)
		{
			byte[] buff = File.ReadAllBytes(file);
			IMAGE_DOS_HEADER image_DOS_HEADER;
			bool flag;
			try
			{
				image_DOS_HEADER = new IMAGE_DOS_HEADER(buff, 0U);
				flag = (buff.BytesToUInt16((ulong)(image_DOS_HEADER.e_lfanew + 4U)) == 34404);
			}
			catch (Exception)
			{
				return false;
			}
			return image_DOS_HEADER.e_magic == 23117 && flag;
		}

		// Token: 0x0600775A RID: 30554 RVA: 0x00239A94 File Offset: 0x00239A94
		public static bool Is32BitPeFile(string file)
		{
			byte[] buff = File.ReadAllBytes(file);
			IMAGE_DOS_HEADER image_DOS_HEADER;
			bool flag;
			try
			{
				image_DOS_HEADER = new IMAGE_DOS_HEADER(buff, 0U);
				flag = (buff.BytesToUInt16((ulong)(image_DOS_HEADER.e_lfanew + 4U)) == 332);
			}
			catch (Exception)
			{
				return false;
			}
			return image_DOS_HEADER.e_magic == 23117 && flag;
		}

		// Token: 0x0600775B RID: 30555 RVA: 0x00239AF8 File Offset: 0x00239AF8
		public string GetFileType()
		{
			ushort machine = this.ImageNtHeaders.FileHeader.Machine;
			string text;
			if (machine != 332)
			{
				if (machine != 34404)
				{
					text = "UNKNOWN";
				}
				else
				{
					text = "AMD64";
				}
			}
			else
			{
				text = "I386";
			}
			if ((this.ImageNtHeaders.FileHeader.Characteristics & 8192) != 0)
			{
				text += "_DLL";
			}
			else if ((this.ImageNtHeaders.FileHeader.Characteristics & 2) != 0)
			{
				text += "_EXE";
			}
			else
			{
				text += "_UNKNOWN";
			}
			return text;
		}

		// Token: 0x040039E9 RID: 14825
		private readonly DataDirectoryParsers _dataDirectoryParsers;

		// Token: 0x040039EA RID: 14826
		private readonly NativeStructureParsers _nativeStructureParsers;

		// Token: 0x040039EB RID: 14827
		private readonly DotNetStructureParsers _dotNetStructureParsers;

		// Token: 0x040039EC RID: 14828
		private readonly AuthenticodeParser _authenticodeParser;

		// Token: 0x040039ED RID: 14829
		private Stream _stream;

		// Token: 0x040039EE RID: 14830
		private string _impHash;

		// Token: 0x040039EF RID: 14831
		private string _md5;

		// Token: 0x040039F0 RID: 14832
		private string _sha1;

		// Token: 0x040039F1 RID: 14833
		private string _sha256;
	}
}
