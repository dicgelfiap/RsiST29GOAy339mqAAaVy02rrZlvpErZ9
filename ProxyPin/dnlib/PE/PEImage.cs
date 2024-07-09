using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using dnlib.IO;
using dnlib.Threading;
using dnlib.Utils;
using dnlib.W32Resources;

namespace dnlib.PE
{
	// Token: 0x0200075B RID: 1883
	[ComVisible(true)]
	public sealed class PEImage : IInternalPEImage, IPEImage, IRvaFileOffsetConverter, IDisposable
	{
		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x060041CE RID: 16846 RVA: 0x00163A98 File Offset: 0x00163A98
		public bool IsFileImageLayout
		{
			get
			{
				return this.peType is PEImage.FilePEType;
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x060041CF RID: 16847 RVA: 0x00163AA8 File Offset: 0x00163AA8
		public bool MayHaveInvalidAddresses
		{
			get
			{
				return !this.IsFileImageLayout;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x060041D0 RID: 16848 RVA: 0x00163AB4 File Offset: 0x00163AB4
		public string Filename
		{
			get
			{
				return this.dataReaderFactory.Filename;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x060041D1 RID: 16849 RVA: 0x00163AC4 File Offset: 0x00163AC4
		public ImageDosHeader ImageDosHeader
		{
			get
			{
				return this.peInfo.ImageDosHeader;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x060041D2 RID: 16850 RVA: 0x00163AD4 File Offset: 0x00163AD4
		public ImageNTHeaders ImageNTHeaders
		{
			get
			{
				return this.peInfo.ImageNTHeaders;
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x060041D3 RID: 16851 RVA: 0x00163AE4 File Offset: 0x00163AE4
		public IList<ImageSectionHeader> ImageSectionHeaders
		{
			get
			{
				return this.peInfo.ImageSectionHeaders;
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x060041D4 RID: 16852 RVA: 0x00163AF4 File Offset: 0x00163AF4
		public IList<ImageDebugDirectory> ImageDebugDirectories
		{
			get
			{
				if (this.imageDebugDirectories == null)
				{
					this.imageDebugDirectories = this.ReadImageDebugDirectories();
				}
				return this.imageDebugDirectories;
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x060041D5 RID: 16853 RVA: 0x00163B14 File Offset: 0x00163B14
		public DataReaderFactory DataReaderFactory
		{
			get
			{
				return this.dataReaderFactory;
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x060041D6 RID: 16854 RVA: 0x00163B1C File Offset: 0x00163B1C
		// (set) Token: 0x060041D7 RID: 16855 RVA: 0x00163B2C File Offset: 0x00163B2C
		public Win32Resources Win32Resources
		{
			get
			{
				return this.win32Resources.Value;
			}
			set
			{
				IDisposable disposable = null;
				if (this.win32Resources.IsValueInitialized)
				{
					disposable = this.win32Resources.Value;
					if (disposable == value)
					{
						return;
					}
				}
				this.win32Resources.Value = value;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x060041D8 RID: 16856 RVA: 0x00163B7C File Offset: 0x00163B7C
		public PEImage(DataReaderFactory dataReaderFactory, ImageLayout imageLayout, bool verify)
		{
			try
			{
				this.dataReaderFactory = dataReaderFactory;
				this.peType = PEImage.ConvertImageLayout(imageLayout);
				DataReader dataReader = dataReaderFactory.CreateReader();
				this.peInfo = new PEInfo(ref dataReader, verify);
				this.Initialize();
			}
			catch
			{
				this.Dispose();
				throw;
			}
		}

		// Token: 0x060041D9 RID: 16857 RVA: 0x00163BE8 File Offset: 0x00163BE8
		private void Initialize()
		{
			this.win32Resources.ReadOriginalValue = delegate()
			{
				ImageDataDirectory imageDataDirectory = this.peInfo.ImageNTHeaders.OptionalHeader.DataDirectories[2];
				if (imageDataDirectory.VirtualAddress == (RVA)0U || imageDataDirectory.Size == 0U)
				{
					return null;
				}
				return new Win32ResourcesPE(this);
			};
			this.win32Resources.Lock = this.theLock;
		}

		// Token: 0x060041DA RID: 16858 RVA: 0x00163C14 File Offset: 0x00163C14
		private static IPEType ConvertImageLayout(ImageLayout imageLayout)
		{
			IPEType result;
			if (imageLayout != ImageLayout.File)
			{
				if (imageLayout != ImageLayout.Memory)
				{
					throw new ArgumentException("imageLayout");
				}
				result = PEImage.MemoryLayout;
			}
			else
			{
				result = PEImage.FileLayout;
			}
			return result;
		}

		// Token: 0x060041DB RID: 16859 RVA: 0x00163C5C File Offset: 0x00163C5C
		internal PEImage(string filename, bool mapAsImage, bool verify) : this(DataReaderFactoryFactory.Create(filename, mapAsImage), mapAsImage ? ImageLayout.Memory : ImageLayout.File, verify)
		{
			try
			{
				if (mapAsImage && this.dataReaderFactory is MemoryMappedDataReaderFactory)
				{
					((MemoryMappedDataReaderFactory)this.dataReaderFactory).SetLength(this.peInfo.GetImageSize());
				}
			}
			catch
			{
				this.Dispose();
				throw;
			}
		}

		// Token: 0x060041DC RID: 16860 RVA: 0x00163CD4 File Offset: 0x00163CD4
		public PEImage(string filename, bool verify) : this(filename, false, verify)
		{
		}

		// Token: 0x060041DD RID: 16861 RVA: 0x00163CE0 File Offset: 0x00163CE0
		public PEImage(string filename) : this(filename, true)
		{
		}

		// Token: 0x060041DE RID: 16862 RVA: 0x00163CEC File Offset: 0x00163CEC
		public PEImage(byte[] data, string filename, ImageLayout imageLayout, bool verify) : this(ByteArrayDataReaderFactory.Create(data, filename), imageLayout, verify)
		{
		}

		// Token: 0x060041DF RID: 16863 RVA: 0x00163D00 File Offset: 0x00163D00
		public PEImage(byte[] data, ImageLayout imageLayout, bool verify) : this(data, null, imageLayout, verify)
		{
		}

		// Token: 0x060041E0 RID: 16864 RVA: 0x00163D0C File Offset: 0x00163D0C
		public PEImage(byte[] data, bool verify) : this(data, null, ImageLayout.File, verify)
		{
		}

		// Token: 0x060041E1 RID: 16865 RVA: 0x00163D18 File Offset: 0x00163D18
		public PEImage(byte[] data, string filename, bool verify) : this(data, filename, ImageLayout.File, verify)
		{
		}

		// Token: 0x060041E2 RID: 16866 RVA: 0x00163D24 File Offset: 0x00163D24
		public PEImage(byte[] data) : this(data, null, true)
		{
		}

		// Token: 0x060041E3 RID: 16867 RVA: 0x00163D30 File Offset: 0x00163D30
		public PEImage(byte[] data, string filename) : this(data, filename, true)
		{
		}

		// Token: 0x060041E4 RID: 16868 RVA: 0x00163D3C File Offset: 0x00163D3C
		public unsafe PEImage(IntPtr baseAddr, uint length, ImageLayout imageLayout, bool verify) : this(NativeMemoryDataReaderFactory.Create((byte*)((void*)baseAddr), length, null), imageLayout, verify)
		{
		}

		// Token: 0x060041E5 RID: 16869 RVA: 0x00163D54 File Offset: 0x00163D54
		public PEImage(IntPtr baseAddr, uint length, bool verify) : this(baseAddr, length, ImageLayout.Memory, verify)
		{
		}

		// Token: 0x060041E6 RID: 16870 RVA: 0x00163D60 File Offset: 0x00163D60
		public PEImage(IntPtr baseAddr, uint length) : this(baseAddr, length, true)
		{
		}

		// Token: 0x060041E7 RID: 16871 RVA: 0x00163D6C File Offset: 0x00163D6C
		public unsafe PEImage(IntPtr baseAddr, ImageLayout imageLayout, bool verify) : this(NativeMemoryDataReaderFactory.Create((byte*)((void*)baseAddr), 65536U, null), imageLayout, verify)
		{
			try
			{
				((NativeMemoryDataReaderFactory)this.dataReaderFactory).SetLength(this.peInfo.GetImageSize());
			}
			catch
			{
				this.Dispose();
				throw;
			}
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x00163DCC File Offset: 0x00163DCC
		public PEImage(IntPtr baseAddr, bool verify) : this(baseAddr, ImageLayout.Memory, verify)
		{
		}

		// Token: 0x060041E9 RID: 16873 RVA: 0x00163DD8 File Offset: 0x00163DD8
		public PEImage(IntPtr baseAddr) : this(baseAddr, true)
		{
		}

		// Token: 0x060041EA RID: 16874 RVA: 0x00163DE4 File Offset: 0x00163DE4
		public RVA ToRVA(FileOffset offset)
		{
			return this.peType.ToRVA(this.peInfo, offset);
		}

		// Token: 0x060041EB RID: 16875 RVA: 0x00163DF8 File Offset: 0x00163DF8
		public FileOffset ToFileOffset(RVA rva)
		{
			return this.peType.ToFileOffset(this.peInfo, rva);
		}

		// Token: 0x060041EC RID: 16876 RVA: 0x00163E0C File Offset: 0x00163E0C
		public void Dispose()
		{
			IDisposable value;
			if (this.win32Resources.IsValueInitialized && (value = this.win32Resources.Value) != null)
			{
				value.Dispose();
			}
			DataReaderFactory dataReaderFactory = this.dataReaderFactory;
			if (dataReaderFactory != null)
			{
				dataReaderFactory.Dispose();
			}
			this.win32Resources.Value = null;
			this.dataReaderFactory = null;
			this.peType = null;
			this.peInfo = null;
		}

		// Token: 0x060041ED RID: 16877 RVA: 0x00163E80 File Offset: 0x00163E80
		public DataReader CreateReader(FileOffset offset)
		{
			return this.DataReaderFactory.CreateReader((uint)offset, this.DataReaderFactory.Length - (uint)offset);
		}

		// Token: 0x060041EE RID: 16878 RVA: 0x00163EAC File Offset: 0x00163EAC
		public DataReader CreateReader(FileOffset offset, uint length)
		{
			return this.DataReaderFactory.CreateReader((uint)offset, length);
		}

		// Token: 0x060041EF RID: 16879 RVA: 0x00163EBC File Offset: 0x00163EBC
		public DataReader CreateReader(RVA rva)
		{
			return this.CreateReader(this.ToFileOffset(rva));
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x00163ECC File Offset: 0x00163ECC
		public DataReader CreateReader(RVA rva, uint length)
		{
			return this.CreateReader(this.ToFileOffset(rva), length);
		}

		// Token: 0x060041F1 RID: 16881 RVA: 0x00163EDC File Offset: 0x00163EDC
		public DataReader CreateReader()
		{
			return this.DataReaderFactory.CreateReader();
		}

		// Token: 0x060041F2 RID: 16882 RVA: 0x00163EEC File Offset: 0x00163EEC
		void IInternalPEImage.UnsafeDisableMemoryMappedIO()
		{
			MemoryMappedDataReaderFactory memoryMappedDataReaderFactory = this.dataReaderFactory as MemoryMappedDataReaderFactory;
			if (memoryMappedDataReaderFactory != null)
			{
				memoryMappedDataReaderFactory.UnsafeDisableMemoryMappedIO();
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x060041F3 RID: 16883 RVA: 0x00163F18 File Offset: 0x00163F18
		bool IInternalPEImage.IsMemoryMappedIO
		{
			get
			{
				MemoryMappedDataReaderFactory memoryMappedDataReaderFactory = this.dataReaderFactory as MemoryMappedDataReaderFactory;
				return memoryMappedDataReaderFactory != null && memoryMappedDataReaderFactory.IsMemoryMappedIO;
			}
		}

		// Token: 0x060041F4 RID: 16884 RVA: 0x00163F44 File Offset: 0x00163F44
		private ImageDebugDirectory[] ReadImageDebugDirectories()
		{
			try
			{
				ImageDataDirectory imageDataDirectory = this.ImageNTHeaders.OptionalHeader.DataDirectories[6];
				if (imageDataDirectory.VirtualAddress == (RVA)0U)
				{
					return Array2.Empty<ImageDebugDirectory>();
				}
				DataReader dataReader = this.DataReaderFactory.CreateReader();
				if (imageDataDirectory.Size > dataReader.Length)
				{
					return Array2.Empty<ImageDebugDirectory>();
				}
				int num = (int)(imageDataDirectory.Size / 28U);
				if (num == 0)
				{
					return Array2.Empty<ImageDebugDirectory>();
				}
				dataReader.CurrentOffset = (uint)this.ToFileOffset(imageDataDirectory.VirtualAddress);
				if ((ulong)dataReader.CurrentOffset + (ulong)imageDataDirectory.Size > (ulong)dataReader.Length)
				{
					return Array2.Empty<ImageDebugDirectory>();
				}
				ImageDebugDirectory[] array = new ImageDebugDirectory[num];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new ImageDebugDirectory(ref dataReader, true);
				}
				return array;
			}
			catch (IOException)
			{
			}
			return Array2.Empty<ImageDebugDirectory>();
		}

		// Token: 0x04002361 RID: 9057
		private const bool USE_MEMORY_LAYOUT_WITH_MAPPED_FILES = false;

		// Token: 0x04002362 RID: 9058
		private static readonly IPEType MemoryLayout = new PEImage.MemoryPEType();

		// Token: 0x04002363 RID: 9059
		private static readonly IPEType FileLayout = new PEImage.FilePEType();

		// Token: 0x04002364 RID: 9060
		private DataReaderFactory dataReaderFactory;

		// Token: 0x04002365 RID: 9061
		private IPEType peType;

		// Token: 0x04002366 RID: 9062
		private PEInfo peInfo;

		// Token: 0x04002367 RID: 9063
		private UserValue<Win32Resources> win32Resources;

		// Token: 0x04002368 RID: 9064
		private readonly Lock theLock = Lock.Create();

		// Token: 0x04002369 RID: 9065
		private ImageDebugDirectory[] imageDebugDirectories;

		// Token: 0x02000FC2 RID: 4034
		private sealed class FilePEType : IPEType
		{
			// Token: 0x06008D98 RID: 36248 RVA: 0x002A6F14 File Offset: 0x002A6F14
			public RVA ToRVA(PEInfo peInfo, FileOffset offset)
			{
				return peInfo.ToRVA(offset);
			}

			// Token: 0x06008D99 RID: 36249 RVA: 0x002A6F20 File Offset: 0x002A6F20
			public FileOffset ToFileOffset(PEInfo peInfo, RVA rva)
			{
				return peInfo.ToFileOffset(rva);
			}
		}

		// Token: 0x02000FC3 RID: 4035
		private sealed class MemoryPEType : IPEType
		{
			// Token: 0x06008D9B RID: 36251 RVA: 0x002A6F34 File Offset: 0x002A6F34
			public RVA ToRVA(PEInfo peInfo, FileOffset offset)
			{
				return (RVA)offset;
			}

			// Token: 0x06008D9C RID: 36252 RVA: 0x002A6F38 File Offset: 0x002A6F38
			public FileOffset ToFileOffset(PEInfo peInfo, RVA rva)
			{
				return (FileOffset)rva;
			}
		}
	}
}
