using System;
using System.Collections.Generic;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x0200091F RID: 2335
	internal readonly struct PdbReaderContext
	{
		// Token: 0x170012EB RID: 4843
		// (get) Token: 0x06005A18 RID: 23064 RVA: 0x001B6724 File Offset: 0x001B6724
		public bool HasDebugInfo
		{
			get
			{
				return this.codeViewDebugDir != null;
			}
		}

		// Token: 0x170012EC RID: 4844
		// (get) Token: 0x06005A19 RID: 23065 RVA: 0x001B6734 File Offset: 0x001B6734
		public ImageDebugDirectory CodeViewDebugDirectory
		{
			get
			{
				return this.codeViewDebugDir;
			}
		}

		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x06005A1A RID: 23066 RVA: 0x001B673C File Offset: 0x001B673C
		public PdbReaderOptions Options { get; }

		// Token: 0x06005A1B RID: 23067 RVA: 0x001B6744 File Offset: 0x001B6744
		public PdbReaderContext(IPEImage peImage, PdbReaderOptions options)
		{
			this.peImage = peImage;
			this.Options = options;
			this.codeViewDebugDir = PdbReaderContext.TryGetDebugDirectoryEntry(peImage, ImageDebugType.CodeView);
		}

		// Token: 0x06005A1C RID: 23068 RVA: 0x001B6764 File Offset: 0x001B6764
		public ImageDebugDirectory TryGetDebugDirectoryEntry(ImageDebugType imageDebugType)
		{
			return PdbReaderContext.TryGetDebugDirectoryEntry(this.peImage, imageDebugType);
		}

		// Token: 0x06005A1D RID: 23069 RVA: 0x001B6774 File Offset: 0x001B6774
		private static ImageDebugDirectory TryGetDebugDirectoryEntry(IPEImage peImage, ImageDebugType imageDebugType)
		{
			IList<ImageDebugDirectory> imageDebugDirectories = peImage.ImageDebugDirectories;
			int count = imageDebugDirectories.Count;
			for (int i = 0; i < count; i++)
			{
				ImageDebugDirectory imageDebugDirectory = imageDebugDirectories[i];
				if (imageDebugDirectory.Type == imageDebugType)
				{
					return imageDebugDirectory;
				}
			}
			return null;
		}

		// Token: 0x06005A1E RID: 23070 RVA: 0x001B67BC File Offset: 0x001B67BC
		public bool TryGetCodeViewData(out Guid guid, out uint age)
		{
			string text;
			return this.TryGetCodeViewData(out guid, out age, out text);
		}

		// Token: 0x06005A1F RID: 23071 RVA: 0x001B67D8 File Offset: 0x001B67D8
		public bool TryGetCodeViewData(out Guid guid, out uint age, out string pdbFilename)
		{
			guid = Guid.Empty;
			age = 0U;
			pdbFilename = null;
			DataReader codeViewDataReader = this.GetCodeViewDataReader();
			if (codeViewDataReader.Length < 25U)
			{
				return false;
			}
			if (codeViewDataReader.ReadUInt32() != 1396986706U)
			{
				return false;
			}
			guid = codeViewDataReader.ReadGuid();
			age = codeViewDataReader.ReadUInt32();
			pdbFilename = codeViewDataReader.TryReadZeroTerminatedUtf8String();
			return pdbFilename != null;
		}

		// Token: 0x06005A20 RID: 23072 RVA: 0x001B684C File Offset: 0x001B684C
		private DataReader GetCodeViewDataReader()
		{
			if (this.codeViewDebugDir == null)
			{
				return default(DataReader);
			}
			return this.CreateReader(this.codeViewDebugDir.AddressOfRawData, this.codeViewDebugDir.SizeOfData);
		}

		// Token: 0x06005A21 RID: 23073 RVA: 0x001B6890 File Offset: 0x001B6890
		public DataReader CreateReader(RVA rva, uint size)
		{
			if (rva == (RVA)0U || size == 0U)
			{
				return default(DataReader);
			}
			DataReader result = this.peImage.CreateReader(rva, size);
			if (result.Length != size)
			{
				return default(DataReader);
			}
			return result;
		}

		// Token: 0x04002B94 RID: 11156
		private readonly IPEImage peImage;

		// Token: 0x04002B95 RID: 11157
		private readonly ImageDebugDirectory codeViewDebugDir;
	}
}
