using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;
using dnlib.IO;
using dnlib.PE;
using dnlib.W32Resources;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008CA RID: 2250
	[ComVisible(true)]
	public sealed class NativeModuleWriter : ModuleWriterBase
	{
		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x06005738 RID: 22328 RVA: 0x001A9B6C File Offset: 0x001A9B6C
		public ModuleDefMD ModuleDefMD
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x06005739 RID: 22329 RVA: 0x001A9B74 File Offset: 0x001A9B74
		public override ModuleDef Module
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x0600573A RID: 22330 RVA: 0x001A9B7C File Offset: 0x001A9B7C
		public override ModuleWriterOptionsBase TheOptions
		{
			get
			{
				return this.Options;
			}
		}

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x0600573B RID: 22331 RVA: 0x001A9B84 File Offset: 0x001A9B84
		// (set) Token: 0x0600573C RID: 22332 RVA: 0x001A9BB8 File Offset: 0x001A9BB8
		public NativeModuleWriterOptions Options
		{
			get
			{
				NativeModuleWriterOptions result;
				if ((result = this.options) == null)
				{
					result = (this.options = new NativeModuleWriterOptions(this.module, true));
				}
				return result;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x0600573D RID: 22333 RVA: 0x001A9BC4 File Offset: 0x001A9BC4
		public override List<PESection> Sections
		{
			get
			{
				return this.sections;
			}
		}

		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x0600573E RID: 22334 RVA: 0x001A9BCC File Offset: 0x001A9BCC
		public List<NativeModuleWriter.OrigSection> OrigSections
		{
			get
			{
				return this.origSections;
			}
		}

		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x0600573F RID: 22335 RVA: 0x001A9BD4 File Offset: 0x001A9BD4
		public override PESection TextSection
		{
			get
			{
				return this.textSection;
			}
		}

		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x06005740 RID: 22336 RVA: 0x001A9BDC File Offset: 0x001A9BDC
		public override PESection RsrcSection
		{
			get
			{
				return this.rsrcSection;
			}
		}

		// Token: 0x06005741 RID: 22337 RVA: 0x001A9BE4 File Offset: 0x001A9BE4
		public NativeModuleWriter(ModuleDefMD module, NativeModuleWriterOptions options)
		{
			this.module = module;
			this.options = options;
			this.peImage = module.Metadata.PEImage;
			this.reusedChunks = new List<NativeModuleWriter.ReusedChunkInfo>();
		}

		// Token: 0x06005742 RID: 22338 RVA: 0x001A9C18 File Offset: 0x001A9C18
		protected override long WriteImpl()
		{
			long result;
			try
			{
				result = this.Write();
			}
			finally
			{
				if (this.origSections != null)
				{
					foreach (NativeModuleWriter.OrigSection origSection in this.origSections)
					{
						origSection.Dispose();
					}
				}
			}
			return result;
		}

		// Token: 0x06005743 RID: 22339 RVA: 0x001A9C94 File Offset: 0x001A9C94
		private long Write()
		{
			this.Initialize();
			this.metadata.KeepFieldRVA = true;
			this.metadata.CreateTables();
			return this.WriteFile();
		}

		// Token: 0x06005744 RID: 22340 RVA: 0x001A9CC8 File Offset: 0x001A9CC8
		private void Initialize()
		{
			this.CreateSections();
			base.OnWriterEvent(ModuleWriterEvent.PESectionsCreated);
			this.CreateChunks();
			base.OnWriterEvent(ModuleWriterEvent.ChunksCreated);
			this.AddChunksToSections();
			base.OnWriterEvent(ModuleWriterEvent.ChunksAddedToSections);
		}

		// Token: 0x06005745 RID: 22341 RVA: 0x001A9D00 File Offset: 0x001A9D00
		private void CreateSections()
		{
			this.CreatePESections();
			this.CreateRawSections();
			this.CreateExtraData();
		}

		// Token: 0x06005746 RID: 22342 RVA: 0x001A9D14 File Offset: 0x001A9D14
		private void CreateChunks()
		{
			base.CreateMetadataChunks(this.module);
			this.methodBodies.CanReuseOldBodyLocation = this.Options.OptimizeImageSize;
			base.CreateDebugDirectory();
			this.imageCor20Header = new ByteArrayChunk(new byte[72]);
			base.CreateStrongNameSignature();
		}

		// Token: 0x06005747 RID: 22343 RVA: 0x001A9D68 File Offset: 0x001A9D68
		private void AddChunksToSections()
		{
			this.textSection.Add(this.imageCor20Header, 4U);
			this.textSection.Add(this.strongNameSignature, 4U);
			this.textSection.Add(this.constants, 8U);
			this.textSection.Add(this.methodBodies, 4U);
			this.textSection.Add(this.netResources, 4U);
			this.textSection.Add(this.metadata, 4U);
			this.textSection.Add(this.debugDirectory, 4U);
			if (this.rsrcSection != null)
			{
				this.rsrcSection.Add(this.win32Resources, 8U);
			}
		}

		// Token: 0x06005748 RID: 22344 RVA: 0x001A9E14 File Offset: 0x001A9E14
		protected override Win32Resources GetWin32Resources()
		{
			if (this.Options.KeepWin32Resources)
			{
				return null;
			}
			if (this.Options.NoWin32Resources)
			{
				return null;
			}
			return this.Options.Win32Resources ?? this.module.Win32Resources;
		}

		// Token: 0x06005749 RID: 22345 RVA: 0x001A9E68 File Offset: 0x001A9E68
		private void CreatePESections()
		{
			this.sections = new List<PESection>();
			this.sections.Add(this.textSection = new PESection(".text", 1610612768U));
			if (this.GetWin32Resources() != null)
			{
				this.sections.Add(this.rsrcSection = new PESection(".rsrc", 1073741888U));
			}
		}

		// Token: 0x0600574A RID: 22346 RVA: 0x001A9ED8 File Offset: 0x001A9ED8
		private void CreateRawSections()
		{
			uint fileAlignment = this.peImage.ImageNTHeaders.OptionalHeader.FileAlignment;
			this.origSections = new List<NativeModuleWriter.OrigSection>(this.peImage.ImageSectionHeaders.Count);
			foreach (ImageSectionHeader imageSectionHeader in this.peImage.ImageSectionHeaders)
			{
				NativeModuleWriter.OrigSection origSection = new NativeModuleWriter.OrigSection(imageSectionHeader);
				this.origSections.Add(origSection);
				uint length = Utils.AlignUp(imageSectionHeader.SizeOfRawData, fileAlignment);
				origSection.Chunk = new DataReaderChunk(this.peImage.CreateReader(imageSectionHeader.VirtualAddress, length), imageSectionHeader.VirtualSize);
			}
		}

		// Token: 0x0600574B RID: 22347 RVA: 0x001A9FA4 File Offset: 0x001A9FA4
		private DataReaderChunk CreateHeaderSection(out IChunk extraHeaderData)
		{
			uint num = this.GetOffsetAfterLastSectionHeader() + (uint)(this.sections.Count * 40);
			uint num2 = Math.Min(this.GetFirstRawDataFileOffset(), this.peImage.ImageNTHeaders.OptionalHeader.SectionAlignment);
			uint num3 = num;
			if (num2 > num3)
			{
				num3 = num2;
			}
			num3 = Utils.AlignUp(num3, this.peImage.ImageNTHeaders.OptionalHeader.FileAlignment);
			if (num3 <= this.peImage.ImageNTHeaders.OptionalHeader.SectionAlignment)
			{
				uint sizeOfHeaders = this.peImage.ImageNTHeaders.OptionalHeader.SizeOfHeaders;
				uint num4;
				if (num3 <= sizeOfHeaders)
				{
					num4 = 0U;
				}
				else
				{
					num4 = num3 - sizeOfHeaders;
					num3 = sizeOfHeaders;
				}
				if (num4 > 0U)
				{
					extraHeaderData = new ByteArrayChunk(new byte[num4]);
				}
				else
				{
					extraHeaderData = null;
				}
				return new DataReaderChunk(this.peImage.CreateReader((FileOffset)0U, num3));
			}
			throw new ModuleWriterException("Could not create header");
		}

		// Token: 0x0600574C RID: 22348 RVA: 0x001AA090 File Offset: 0x001AA090
		private uint GetOffsetAfterLastSectionHeader()
		{
			return (uint)this.peImage.ImageSectionHeaders[this.peImage.ImageSectionHeaders.Count - 1].EndOffset;
		}

		// Token: 0x0600574D RID: 22349 RVA: 0x001AA0C8 File Offset: 0x001AA0C8
		private uint GetFirstRawDataFileOffset()
		{
			uint num = uint.MaxValue;
			foreach (ImageSectionHeader imageSectionHeader in this.peImage.ImageSectionHeaders)
			{
				num = Math.Min(num, imageSectionHeader.PointerToRawData);
			}
			return num;
		}

		// Token: 0x0600574E RID: 22350 RVA: 0x001AA12C File Offset: 0x001AA12C
		private void CreateExtraData()
		{
			if (!this.Options.KeepExtraPEData)
			{
				return;
			}
			uint lastFileSectionOffset = this.GetLastFileSectionOffset();
			this.extraData = new DataReaderChunk(this.peImage.CreateReader((FileOffset)lastFileSectionOffset));
			if (this.extraData.CreateReader().Length == 0U)
			{
				this.extraData = null;
			}
		}

		// Token: 0x0600574F RID: 22351 RVA: 0x001AA18C File Offset: 0x001AA18C
		private uint GetLastFileSectionOffset()
		{
			uint num = 0U;
			foreach (NativeModuleWriter.OrigSection origSection in this.origSections)
			{
				num = Math.Max(num, (uint)(origSection.PESection.VirtualAddress + origSection.PESection.SizeOfRawData));
			}
			return (uint)(this.peImage.ToFileOffset((RVA)(num - 1U)) + 1U);
		}

		// Token: 0x06005750 RID: 22352 RVA: 0x001AA210 File Offset: 0x001AA210
		private void ReuseIfPossible(PESection section, IReuseChunk chunk, RVA origRva, uint origSize, uint requiredAlignment)
		{
			if (origRva == (RVA)0U || origSize == 0U)
			{
				return;
			}
			if (chunk == null)
			{
				return;
			}
			if (!chunk.CanReuse(origRva, origSize))
			{
				return;
			}
			if ((origRva & (RVA)(requiredAlignment - 1U)) != (RVA)0U)
			{
				return;
			}
			if (section.Remove(chunk) == null)
			{
				throw new InvalidOperationException();
			}
			this.reusedChunks.Add(new NativeModuleWriter.ReusedChunkInfo(chunk, origRva));
		}

		// Token: 0x06005751 RID: 22353 RVA: 0x001AA280 File Offset: 0x001AA280
		private FileOffset GetNewFileOffset(RVA rva)
		{
			foreach (NativeModuleWriter.OrigSection origSection in this.origSections)
			{
				ImageSectionHeader pesection = origSection.PESection;
				if (pesection.VirtualAddress <= rva && rva < pesection.VirtualAddress + Math.Max(pesection.VirtualSize, pesection.SizeOfRawData))
				{
					return origSection.Chunk.FileOffset + (rva - pesection.VirtualAddress);
				}
			}
			return (FileOffset)rva;
		}

		// Token: 0x06005752 RID: 22354 RVA: 0x001AA320 File Offset: 0x001AA320
		private long WriteFile()
		{
			uint entryPointToken;
			bool entryPoint = this.GetEntryPoint(out entryPointToken);
			base.OnWriterEvent(ModuleWriterEvent.BeginWritePdb);
			base.WritePdbFile();
			base.OnWriterEvent(ModuleWriterEvent.EndWritePdb);
			this.metadata.OnBeforeSetOffset();
			base.OnWriterEvent(ModuleWriterEvent.BeginCalculateRvasAndFileOffsets);
			if (this.Options.OptimizeImageSize)
			{
				ImageDataDirectory metadata = this.module.Metadata.ImageCor20Header.Metadata;
				this.metadata.SetOffset(this.peImage.ToFileOffset(metadata.VirtualAddress), metadata.VirtualAddress);
				this.ReuseIfPossible(this.textSection, this.metadata, metadata.VirtualAddress, metadata.Size, 4U);
				ImageDataDirectory imageDataDirectory = this.peImage.ImageNTHeaders.OptionalHeader.DataDirectories[2];
				if (this.win32Resources != null && imageDataDirectory.VirtualAddress != (RVA)0U && imageDataDirectory.Size != 0U)
				{
					FileOffset offset = this.peImage.ToFileOffset(imageDataDirectory.VirtualAddress);
					if (this.win32Resources.CheckValidOffset(offset))
					{
						this.win32Resources.SetOffset(offset, imageDataDirectory.VirtualAddress);
						this.ReuseIfPossible(this.rsrcSection, this.win32Resources, imageDataDirectory.VirtualAddress, imageDataDirectory.Size, 8U);
					}
				}
				this.ReuseIfPossible(this.textSection, this.imageCor20Header, this.module.Metadata.PEImage.ImageNTHeaders.OptionalHeader.DataDirectories[14].VirtualAddress, this.module.Metadata.PEImage.ImageNTHeaders.OptionalHeader.DataDirectories[14].Size, 4U);
				if ((this.module.Metadata.ImageCor20Header.Flags & ComImageFlags.StrongNameSigned) != (ComImageFlags)0U)
				{
					this.ReuseIfPossible(this.textSection, this.strongNameSignature, this.module.Metadata.ImageCor20Header.StrongNameSignature.VirtualAddress, this.module.Metadata.ImageCor20Header.StrongNameSignature.Size, 4U);
				}
				this.ReuseIfPossible(this.textSection, this.netResources, this.module.Metadata.ImageCor20Header.Resources.VirtualAddress, this.module.Metadata.ImageCor20Header.Resources.Size, 4U);
				if (this.methodBodies.ReusedAllMethodBodyLocations)
				{
					this.textSection.Remove(this.methodBodies);
				}
				ImageDataDirectory imageDataDirectory2 = this.peImage.ImageNTHeaders.OptionalHeader.DataDirectories[6];
				uint origSize;
				if (imageDataDirectory2.VirtualAddress != (RVA)0U && imageDataDirectory2.Size != 0U && NativeModuleWriter.TryGetRealDebugDirectorySize(this.peImage, out origSize))
				{
					this.ReuseIfPossible(this.textSection, this.debugDirectory, imageDataDirectory2.VirtualAddress, origSize, 4U);
				}
			}
			if (this.constants.IsEmpty)
			{
				this.textSection.Remove(this.constants);
			}
			if (this.netResources.IsEmpty)
			{
				this.textSection.Remove(this.netResources);
			}
			if (this.textSection.IsEmpty)
			{
				this.sections.Remove(this.textSection);
			}
			if (this.rsrcSection != null && this.rsrcSection.IsEmpty)
			{
				this.sections.Remove(this.rsrcSection);
				this.rsrcSection = null;
			}
			IChunk chunk;
			DataReaderChunk dataReaderChunk = this.CreateHeaderSection(out chunk);
			List<IChunk> list = new List<IChunk>();
			uint headerLen;
			if (chunk != null)
			{
				ChunkList<IChunk> chunkList = new ChunkList<IChunk>();
				chunkList.Add(dataReaderChunk, 1U);
				chunkList.Add(chunk, 1U);
				list.Add(chunkList);
				headerLen = dataReaderChunk.GetVirtualSize() + chunk.GetVirtualSize();
			}
			else
			{
				list.Add(dataReaderChunk);
				headerLen = dataReaderChunk.GetVirtualSize();
			}
			foreach (NativeModuleWriter.OrigSection origSection in this.origSections)
			{
				list.Add(origSection.Chunk);
			}
			foreach (PESection item in this.sections)
			{
				list.Add(item);
			}
			if (this.extraData != null)
			{
				list.Add(this.extraData);
			}
			base.CalculateRvasAndFileOffsets(list, (FileOffset)0U, (RVA)0U, this.peImage.ImageNTHeaders.OptionalHeader.FileAlignment, this.peImage.ImageNTHeaders.OptionalHeader.SectionAlignment);
			if (this.reusedChunks.Count > 0 || this.methodBodies.HasReusedMethods)
			{
				this.methodBodies.InitializeReusedMethodBodies(new Func<RVA, FileOffset>(this.GetNewFileOffset));
				foreach (NativeModuleWriter.ReusedChunkInfo reusedChunkInfo in this.reusedChunks)
				{
					FileOffset newFileOffset = this.GetNewFileOffset(reusedChunkInfo.RVA);
					reusedChunkInfo.Chunk.SetOffset(newFileOffset, reusedChunkInfo.RVA);
				}
			}
			this.metadata.UpdateMethodAndFieldRvas();
			foreach (NativeModuleWriter.OrigSection origSection2 in this.origSections)
			{
				if (origSection2.Chunk.RVA != origSection2.PESection.VirtualAddress)
				{
					throw new ModuleWriterException("Invalid section RVA");
				}
			}
			base.OnWriterEvent(ModuleWriterEvent.EndCalculateRvasAndFileOffsets);
			base.OnWriterEvent(ModuleWriterEvent.BeginWriteChunks);
			DataWriter dataWriter = new DataWriter(this.destStream);
			base.WriteChunks(dataWriter, list, (FileOffset)0U, this.peImage.ImageNTHeaders.OptionalHeader.FileAlignment);
			long num = dataWriter.Position - this.destStreamBaseOffset;
			if (this.reusedChunks.Count > 0 || this.methodBodies.HasReusedMethods)
			{
				long position = dataWriter.Position;
				foreach (NativeModuleWriter.ReusedChunkInfo reusedChunkInfo2 in this.reusedChunks)
				{
					if (reusedChunkInfo2.Chunk.RVA != reusedChunkInfo2.RVA)
					{
						throw new InvalidOperationException();
					}
					dataWriter.Position = this.destStreamBaseOffset + (long)((ulong)reusedChunkInfo2.Chunk.FileOffset);
					reusedChunkInfo2.Chunk.VerifyWriteTo(dataWriter);
				}
				this.methodBodies.WriteReusedMethodBodies(dataWriter, this.destStreamBaseOffset);
				dataWriter.Position = position;
			}
			SectionSizes sectionSizes = new SectionSizes(this.peImage.ImageNTHeaders.OptionalHeader.FileAlignment, this.peImage.ImageNTHeaders.OptionalHeader.SectionAlignment, headerLen, new Func<IEnumerable<SectionSizeInfo>>(this.GetSectionSizeInfos));
			this.UpdateHeaderFields(dataWriter, entryPoint, entryPointToken, ref sectionSizes);
			base.OnWriterEvent(ModuleWriterEvent.EndWriteChunks);
			base.OnWriterEvent(ModuleWriterEvent.BeginStrongNameSign);
			if (this.Options.StrongNameKey != null)
			{
				base.StrongNameSign((long)((ulong)this.strongNameSignature.FileOffset));
			}
			base.OnWriterEvent(ModuleWriterEvent.EndStrongNameSign);
			base.OnWriterEvent(ModuleWriterEvent.BeginWritePEChecksum);
			if (this.Options.AddCheckSum)
			{
				this.destStream.Position = this.destStreamBaseOffset;
				uint value = this.destStream.CalculatePECheckSum(num, this.checkSumOffset);
				dataWriter.Position = this.checkSumOffset;
				dataWriter.WriteUInt32(value);
			}
			base.OnWriterEvent(ModuleWriterEvent.EndWritePEChecksum);
			return num;
		}

		// Token: 0x06005753 RID: 22355 RVA: 0x001AAAF4 File Offset: 0x001AAAF4
		private static bool TryGetRealDebugDirectorySize(IPEImage peImage, out uint realSize)
		{
			realSize = 0U;
			if (peImage.ImageDebugDirectories.Count == 0)
			{
				return false;
			}
			List<ImageDebugDirectory> list = new List<ImageDebugDirectory>(peImage.ImageDebugDirectories);
			list.Sort((ImageDebugDirectory a, ImageDebugDirectory b) => a.AddressOfRawData.CompareTo(b.AddressOfRawData));
			ImageDataDirectory imageDataDirectory = peImage.ImageNTHeaders.OptionalHeader.DataDirectories[6];
			uint num = (uint)(imageDataDirectory.VirtualAddress + imageDataDirectory.Size);
			for (int i = 0; i < list.Count; i++)
			{
				uint num2 = num + 3U & 4294967292U;
				ImageDebugDirectory imageDebugDirectory = list[i];
				if (imageDebugDirectory.AddressOfRawData != (RVA)0U && imageDebugDirectory.SizeOfData != 0U)
				{
					if (num > (uint)imageDebugDirectory.AddressOfRawData || imageDebugDirectory.AddressOfRawData > (RVA)num2)
					{
						return false;
					}
					num = (uint)(imageDebugDirectory.AddressOfRawData + imageDebugDirectory.SizeOfData);
				}
			}
			realSize = num - (uint)imageDataDirectory.VirtualAddress;
			return true;
		}

		// Token: 0x06005754 RID: 22356 RVA: 0x001AABE8 File Offset: 0x001AABE8
		private bool Is64Bit()
		{
			return this.peImage.ImageNTHeaders.OptionalHeader is ImageOptionalHeader64;
		}

		// Token: 0x06005755 RID: 22357 RVA: 0x001AAC04 File Offset: 0x001AAC04
		private Characteristics GetCharacteristics()
		{
			Characteristics characteristics = this.module.Characteristics;
			if (this.Is64Bit())
			{
				characteristics &= ~Characteristics.Bit32Machine;
			}
			else
			{
				characteristics |= Characteristics.Bit32Machine;
			}
			if (this.Options.IsExeFile)
			{
				characteristics &= ~Characteristics.Dll;
			}
			else
			{
				characteristics |= Characteristics.Dll;
			}
			return characteristics;
		}

		// Token: 0x06005756 RID: 22358 RVA: 0x001AAC68 File Offset: 0x001AAC68
		private void UpdateHeaderFields(DataWriter writer, bool entryPointIsManagedOrNoEntryPoint, uint entryPointToken, ref SectionSizes sectionSizes)
		{
			long position = this.destStreamBaseOffset + (long)((ulong)this.peImage.ImageNTHeaders.FileHeader.StartOffset);
			long position2 = this.destStreamBaseOffset + (long)((ulong)this.peImage.ImageNTHeaders.OptionalHeader.StartOffset);
			long position3 = this.destStreamBaseOffset + (long)((ulong)this.peImage.ImageSectionHeaders[0].StartOffset);
			long num = this.destStreamBaseOffset + (long)((ulong)this.peImage.ImageNTHeaders.OptionalHeader.EndOffset) - 128L;
			long position4 = this.destStreamBaseOffset + (long)((ulong)this.imageCor20Header.FileOffset);
			PEHeadersOptions peheadersOptions = this.Options.PEHeadersOptions;
			writer.Position = position;
			writer.WriteUInt16((ushort)(peheadersOptions.Machine ?? this.module.Machine));
			writer.WriteUInt16((ushort)(this.origSections.Count + this.sections.Count));
			NativeModuleWriter.WriteUInt32(writer, peheadersOptions.TimeDateStamp);
			NativeModuleWriter.WriteUInt32(writer, peheadersOptions.PointerToSymbolTable);
			NativeModuleWriter.WriteUInt32(writer, peheadersOptions.NumberOfSymbols);
			writer.Position += 2L;
			writer.WriteUInt16((ushort)(peheadersOptions.Characteristics ?? this.GetCharacteristics()));
			writer.Position = position2;
			if (this.peImage.ImageNTHeaders.OptionalHeader is ImageOptionalHeader32)
			{
				writer.Position += 2L;
				NativeModuleWriter.WriteByte(writer, peheadersOptions.MajorLinkerVersion);
				NativeModuleWriter.WriteByte(writer, peheadersOptions.MinorLinkerVersion);
				writer.WriteUInt32(sectionSizes.SizeOfCode);
				writer.WriteUInt32(sectionSizes.SizeOfInitdData);
				writer.WriteUInt32(sectionSizes.SizeOfUninitdData);
				writer.Position += 4L;
				writer.WriteUInt32(sectionSizes.BaseOfCode);
				writer.WriteUInt32(sectionSizes.BaseOfData);
				NativeModuleWriter.WriteUInt32(writer, peheadersOptions.ImageBase);
				writer.Position += 8L;
				NativeModuleWriter.WriteUInt16(writer, peheadersOptions.MajorOperatingSystemVersion);
				NativeModuleWriter.WriteUInt16(writer, peheadersOptions.MinorOperatingSystemVersion);
				NativeModuleWriter.WriteUInt16(writer, peheadersOptions.MajorImageVersion);
				NativeModuleWriter.WriteUInt16(writer, peheadersOptions.MinorImageVersion);
				NativeModuleWriter.WriteUInt16(writer, peheadersOptions.MajorSubsystemVersion);
				NativeModuleWriter.WriteUInt16(writer, peheadersOptions.MinorSubsystemVersion);
				NativeModuleWriter.WriteUInt32(writer, peheadersOptions.Win32VersionValue);
				writer.WriteUInt32(sectionSizes.SizeOfImage);
				writer.WriteUInt32(sectionSizes.SizeOfHeaders);
				this.checkSumOffset = writer.Position;
				writer.WriteInt32(0);
				NativeModuleWriter.WriteUInt16(writer, peheadersOptions.Subsystem);
				NativeModuleWriter.WriteUInt16(writer, peheadersOptions.DllCharacteristics);
				NativeModuleWriter.WriteUInt32(writer, peheadersOptions.SizeOfStackReserve);
				NativeModuleWriter.WriteUInt32(writer, peheadersOptions.SizeOfStackCommit);
				NativeModuleWriter.WriteUInt32(writer, peheadersOptions.SizeOfHeapReserve);
				NativeModuleWriter.WriteUInt32(writer, peheadersOptions.SizeOfHeapCommit);
				NativeModuleWriter.WriteUInt32(writer, peheadersOptions.LoaderFlags);
				NativeModuleWriter.WriteUInt32(writer, peheadersOptions.NumberOfRvaAndSizes);
			}
			else
			{
				writer.Position += 2L;
				NativeModuleWriter.WriteByte(writer, peheadersOptions.MajorLinkerVersion);
				NativeModuleWriter.WriteByte(writer, peheadersOptions.MinorLinkerVersion);
				writer.WriteUInt32(sectionSizes.SizeOfCode);
				writer.WriteUInt32(sectionSizes.SizeOfInitdData);
				writer.WriteUInt32(sectionSizes.SizeOfUninitdData);
				writer.Position += 4L;
				writer.WriteUInt32(sectionSizes.BaseOfCode);
				NativeModuleWriter.WriteUInt64(writer, peheadersOptions.ImageBase);
				writer.Position += 8L;
				NativeModuleWriter.WriteUInt16(writer, peheadersOptions.MajorOperatingSystemVersion);
				NativeModuleWriter.WriteUInt16(writer, peheadersOptions.MinorOperatingSystemVersion);
				NativeModuleWriter.WriteUInt16(writer, peheadersOptions.MajorImageVersion);
				NativeModuleWriter.WriteUInt16(writer, peheadersOptions.MinorImageVersion);
				NativeModuleWriter.WriteUInt16(writer, peheadersOptions.MajorSubsystemVersion);
				NativeModuleWriter.WriteUInt16(writer, peheadersOptions.MinorSubsystemVersion);
				NativeModuleWriter.WriteUInt32(writer, peheadersOptions.Win32VersionValue);
				writer.WriteUInt32(sectionSizes.SizeOfImage);
				writer.WriteUInt32(sectionSizes.SizeOfHeaders);
				this.checkSumOffset = writer.Position;
				writer.WriteInt32(0);
				NativeModuleWriter.WriteUInt16(writer, new Subsystem?(peheadersOptions.Subsystem ?? this.GetSubsystem()));
				NativeModuleWriter.WriteUInt16(writer, new DllCharacteristics?(peheadersOptions.DllCharacteristics ?? this.module.DllCharacteristics));
				NativeModuleWriter.WriteUInt64(writer, peheadersOptions.SizeOfStackReserve);
				NativeModuleWriter.WriteUInt64(writer, peheadersOptions.SizeOfStackCommit);
				NativeModuleWriter.WriteUInt64(writer, peheadersOptions.SizeOfHeapReserve);
				NativeModuleWriter.WriteUInt64(writer, peheadersOptions.SizeOfHeapCommit);
				NativeModuleWriter.WriteUInt32(writer, peheadersOptions.LoaderFlags);
				NativeModuleWriter.WriteUInt32(writer, peheadersOptions.NumberOfRvaAndSizes);
			}
			if (this.win32Resources != null)
			{
				writer.Position = num + 16L;
				writer.WriteDataDirectory(this.win32Resources);
			}
			writer.Position = num + 32L;
			writer.WriteDataDirectory(null);
			writer.Position = num + 48L;
			writer.WriteDebugDirectory(this.debugDirectory);
			writer.Position = num + 112L;
			writer.WriteDataDirectory(this.imageCor20Header);
			writer.Position = position3;
			foreach (NativeModuleWriter.OrigSection origSection in this.origSections)
			{
				writer.Position += 20L;
				writer.WriteUInt32((uint)origSection.Chunk.FileOffset);
				writer.Position += 16L;
			}
			foreach (PESection pesection in this.sections)
			{
				pesection.WriteHeaderTo(writer, this.peImage.ImageNTHeaders.OptionalHeader.FileAlignment, this.peImage.ImageNTHeaders.OptionalHeader.SectionAlignment, (uint)pesection.RVA);
			}
			writer.Position = position4;
			writer.WriteInt32(72);
			NativeModuleWriter.WriteUInt16(writer, this.Options.Cor20HeaderOptions.MajorRuntimeVersion);
			NativeModuleWriter.WriteUInt16(writer, this.Options.Cor20HeaderOptions.MinorRuntimeVersion);
			writer.WriteDataDirectory(this.metadata);
			writer.WriteUInt32((uint)this.GetComImageFlags(entryPointIsManagedOrNoEntryPoint));
			writer.WriteUInt32(entryPointToken);
			writer.WriteDataDirectory(this.netResources);
			writer.WriteDataDirectory(this.strongNameSignature);
			NativeModuleWriter.WriteDataDirectory(writer, this.module.Metadata.ImageCor20Header.CodeManagerTable);
			NativeModuleWriter.WriteDataDirectory(writer, this.module.Metadata.ImageCor20Header.VTableFixups);
			NativeModuleWriter.WriteDataDirectory(writer, this.module.Metadata.ImageCor20Header.ExportAddressTableJumps);
			NativeModuleWriter.WriteDataDirectory(writer, this.module.Metadata.ImageCor20Header.ManagedNativeHeader);
			this.UpdateVTableFixups(writer);
		}

		// Token: 0x06005757 RID: 22359 RVA: 0x001AB38C File Offset: 0x001AB38C
		private static void WriteDataDirectory(DataWriter writer, ImageDataDirectory dataDir)
		{
			writer.WriteUInt32((uint)dataDir.VirtualAddress);
			writer.WriteUInt32(dataDir.Size);
		}

		// Token: 0x06005758 RID: 22360 RVA: 0x001AB3B8 File Offset: 0x001AB3B8
		private static void WriteByte(DataWriter writer, byte? value)
		{
			if (value == null)
			{
				long position = writer.Position;
				writer.Position = position + 1L;
				return;
			}
			writer.WriteByte(value.Value);
		}

		// Token: 0x06005759 RID: 22361 RVA: 0x001AB3F4 File Offset: 0x001AB3F4
		private static void WriteUInt16(DataWriter writer, ushort? value)
		{
			if (value == null)
			{
				writer.Position += 2L;
				return;
			}
			writer.WriteUInt16(value.Value);
		}

		// Token: 0x0600575A RID: 22362 RVA: 0x001AB430 File Offset: 0x001AB430
		private static void WriteUInt16(DataWriter writer, Subsystem? value)
		{
			if (value == null)
			{
				writer.Position += 2L;
				return;
			}
			writer.WriteUInt16((ushort)value.Value);
		}

		// Token: 0x0600575B RID: 22363 RVA: 0x001AB46C File Offset: 0x001AB46C
		private static void WriteUInt16(DataWriter writer, DllCharacteristics? value)
		{
			if (value == null)
			{
				writer.Position += 2L;
				return;
			}
			writer.WriteUInt16((ushort)value.Value);
		}

		// Token: 0x0600575C RID: 22364 RVA: 0x001AB4A8 File Offset: 0x001AB4A8
		private static void WriteUInt32(DataWriter writer, uint? value)
		{
			if (value == null)
			{
				writer.Position += 4L;
				return;
			}
			writer.WriteUInt32(value.Value);
		}

		// Token: 0x0600575D RID: 22365 RVA: 0x001AB4E4 File Offset: 0x001AB4E4
		private static void WriteUInt32(DataWriter writer, ulong? value)
		{
			if (value == null)
			{
				writer.Position += 4L;
				return;
			}
			writer.WriteUInt32((uint)value.Value);
		}

		// Token: 0x0600575E RID: 22366 RVA: 0x001AB520 File Offset: 0x001AB520
		private static void WriteUInt64(DataWriter writer, ulong? value)
		{
			if (value == null)
			{
				writer.Position += 8L;
				return;
			}
			writer.WriteUInt64(value.Value);
		}

		// Token: 0x0600575F RID: 22367 RVA: 0x001AB55C File Offset: 0x001AB55C
		private ComImageFlags GetComImageFlags(bool isManagedEntryPoint)
		{
			ComImageFlags comImageFlags = this.Options.Cor20HeaderOptions.Flags ?? this.module.Cor20HeaderFlags;
			uint? entryPoint = this.Options.Cor20HeaderOptions.EntryPoint;
			if (entryPoint != null)
			{
				return comImageFlags;
			}
			if (isManagedEntryPoint)
			{
				return comImageFlags & ~ComImageFlags.NativeEntryPoint;
			}
			return comImageFlags | ComImageFlags.NativeEntryPoint;
		}

		// Token: 0x06005760 RID: 22368 RVA: 0x001AB5D0 File Offset: 0x001AB5D0
		private Subsystem GetSubsystem()
		{
			if (this.module.Kind == ModuleKind.Windows)
			{
				return Subsystem.WindowsGui;
			}
			return Subsystem.WindowsCui;
		}

		// Token: 0x06005761 RID: 22369 RVA: 0x001AB5E8 File Offset: 0x001AB5E8
		private long ToWriterOffset(RVA rva)
		{
			if (rva == (RVA)0U)
			{
				return 0L;
			}
			foreach (NativeModuleWriter.OrigSection origSection in this.origSections)
			{
				ImageSectionHeader pesection = origSection.PESection;
				if (pesection.VirtualAddress <= rva && rva < pesection.VirtualAddress + Math.Max(pesection.VirtualSize, pesection.SizeOfRawData))
				{
					return this.destStreamBaseOffset + (long)((ulong)origSection.Chunk.FileOffset) + (long)((ulong)(rva - pesection.VirtualAddress));
				}
			}
			return 0L;
		}

		// Token: 0x06005762 RID: 22370 RVA: 0x001AB69C File Offset: 0x001AB69C
		private IEnumerable<SectionSizeInfo> GetSectionSizeInfos()
		{
			foreach (NativeModuleWriter.OrigSection origSection in this.origSections)
			{
				yield return new SectionSizeInfo(origSection.Chunk.GetVirtualSize(), origSection.PESection.Characteristics);
			}
			List<NativeModuleWriter.OrigSection>.Enumerator enumerator = default(List<NativeModuleWriter.OrigSection>.Enumerator);
			foreach (PESection pesection in this.sections)
			{
				yield return new SectionSizeInfo(pesection.GetVirtualSize(), pesection.Characteristics);
			}
			List<PESection>.Enumerator enumerator2 = default(List<PESection>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06005763 RID: 22371 RVA: 0x001AB6AC File Offset: 0x001AB6AC
		private void UpdateVTableFixups(DataWriter writer)
		{
			VTableFixups vtableFixups = this.module.VTableFixups;
			if (vtableFixups == null || vtableFixups.VTables.Count == 0)
			{
				return;
			}
			writer.Position = this.ToWriterOffset(vtableFixups.RVA);
			if (writer.Position == 0L)
			{
				base.Error("Could not convert RVA to file offset", new object[0]);
				return;
			}
			foreach (VTable vtable in vtableFixups)
			{
				if (vtable.Methods.Count > 65535)
				{
					throw new ModuleWriterException("Too many methods in vtable");
				}
				writer.WriteUInt32((uint)vtable.RVA);
				writer.WriteUInt16((ushort)vtable.Methods.Count);
				writer.WriteUInt16((ushort)vtable.Flags);
				long position = writer.Position;
				writer.Position = this.ToWriterOffset(vtable.RVA);
				if (writer.Position == 0L)
				{
					if (vtable.RVA != (RVA)0U || vtable.Methods.Count > 0)
					{
						base.Error("Could not convert RVA to file offset", new object[0]);
					}
				}
				else
				{
					IList<IMethod> methods = vtable.Methods;
					int count = methods.Count;
					for (int i = 0; i < count; i++)
					{
						IMethod method = methods[i];
						writer.WriteUInt32(this.GetMethodToken(method));
						if (vtable.Is64Bit)
						{
							writer.WriteInt32(0);
						}
					}
				}
				writer.Position = position;
			}
		}

		// Token: 0x06005764 RID: 22372 RVA: 0x001AB850 File Offset: 0x001AB850
		private uint GetMethodToken(IMethod method)
		{
			MethodDef methodDef = method as MethodDef;
			if (methodDef != null)
			{
				return new MDToken(Table.Method, this.metadata.GetRid(methodDef)).Raw;
			}
			MemberRef memberRef = method as MemberRef;
			if (memberRef != null)
			{
				return new MDToken(Table.MemberRef, this.metadata.GetRid(memberRef)).Raw;
			}
			MethodSpec methodSpec = method as MethodSpec;
			if (methodSpec != null)
			{
				return new MDToken(Table.MethodSpec, this.metadata.GetRid(methodSpec)).Raw;
			}
			if (method == null)
			{
				return 0U;
			}
			base.Error("Invalid VTable method type: {0}", new object[]
			{
				method.GetType()
			});
			return 0U;
		}

		// Token: 0x06005765 RID: 22373 RVA: 0x001AB900 File Offset: 0x001AB900
		private bool GetEntryPoint(out uint ep)
		{
			uint? entryPoint = this.Options.Cor20HeaderOptions.EntryPoint;
			if (entryPoint != null)
			{
				ep = entryPoint.Value;
				return ep == 0U || (this.Options.Cor20HeaderOptions.Flags.GetValueOrDefault() & ComImageFlags.NativeEntryPoint) == (ComImageFlags)0U;
			}
			MethodDef methodDef = this.module.ManagedEntryPoint as MethodDef;
			if (methodDef != null)
			{
				ep = new MDToken(Table.Method, this.metadata.GetRid(methodDef)).Raw;
				return true;
			}
			FileDef fileDef = this.module.ManagedEntryPoint as FileDef;
			if (fileDef != null)
			{
				ep = new MDToken(Table.File, this.metadata.GetRid(fileDef)).Raw;
				return true;
			}
			ep = (uint)this.module.NativeEntryPoint;
			return ep == 0U;
		}

		// Token: 0x040029E4 RID: 10724
		private readonly ModuleDefMD module;

		// Token: 0x040029E5 RID: 10725
		private NativeModuleWriterOptions options;

		// Token: 0x040029E6 RID: 10726
		private DataReaderChunk extraData;

		// Token: 0x040029E7 RID: 10727
		private List<NativeModuleWriter.OrigSection> origSections;

		// Token: 0x040029E8 RID: 10728
		private List<NativeModuleWriter.ReusedChunkInfo> reusedChunks;

		// Token: 0x040029E9 RID: 10729
		private readonly IPEImage peImage;

		// Token: 0x040029EA RID: 10730
		private List<PESection> sections;

		// Token: 0x040029EB RID: 10731
		private PESection textSection;

		// Token: 0x040029EC RID: 10732
		private ByteArrayChunk imageCor20Header;

		// Token: 0x040029ED RID: 10733
		private PESection rsrcSection;

		// Token: 0x040029EE RID: 10734
		private long checkSumOffset;

		// Token: 0x02001020 RID: 4128
		private readonly struct ReusedChunkInfo
		{
			// Token: 0x17001E04 RID: 7684
			// (get) Token: 0x06008F68 RID: 36712 RVA: 0x002AC0E4 File Offset: 0x002AC0E4
			public IReuseChunk Chunk { get; }

			// Token: 0x17001E05 RID: 7685
			// (get) Token: 0x06008F69 RID: 36713 RVA: 0x002AC0EC File Offset: 0x002AC0EC
			public RVA RVA { get; }

			// Token: 0x06008F6A RID: 36714 RVA: 0x002AC0F4 File Offset: 0x002AC0F4
			public ReusedChunkInfo(IReuseChunk chunk, RVA rva)
			{
				this.Chunk = chunk;
				this.RVA = rva;
			}
		}

		// Token: 0x02001021 RID: 4129
		public sealed class OrigSection : IDisposable
		{
			// Token: 0x06008F6B RID: 36715 RVA: 0x002AC104 File Offset: 0x002AC104
			public OrigSection(ImageSectionHeader peSection)
			{
				this.PESection = peSection;
			}

			// Token: 0x06008F6C RID: 36716 RVA: 0x002AC114 File Offset: 0x002AC114
			public void Dispose()
			{
				this.Chunk = null;
				this.PESection = null;
			}

			// Token: 0x06008F6D RID: 36717 RVA: 0x002AC124 File Offset: 0x002AC124
			public override string ToString()
			{
				uint startOffset = this.Chunk.CreateReader().StartOffset;
				return string.Format("{0} FO:{1:X8} L:{2:X8}", this.PESection.DisplayName, startOffset, this.Chunk.CreateReader().Length);
			}

			// Token: 0x040044BC RID: 17596
			public ImageSectionHeader PESection;

			// Token: 0x040044BD RID: 17597
			public DataReaderChunk Chunk;
		}
	}
}
