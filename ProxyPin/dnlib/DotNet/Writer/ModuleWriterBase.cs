using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Text;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;
using dnlib.DotNet.Pdb.Dss;
using dnlib.DotNet.Pdb.WindowsPdb;
using dnlib.IO;
using dnlib.PE;
using dnlib.W32Resources;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008C6 RID: 2246
	[ComVisible(true)]
	public abstract class ModuleWriterBase : ILogger
	{
		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x060056F9 RID: 22265
		public abstract ModuleWriterOptionsBase TheOptions { get; }

		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x060056FA RID: 22266 RVA: 0x001A89E8 File Offset: 0x001A89E8
		public Stream DestinationStream
		{
			get
			{
				return this.destStream;
			}
		}

		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x060056FB RID: 22267 RVA: 0x001A89F0 File Offset: 0x001A89F0
		public UniqueChunkList<ByteArrayChunk> Constants
		{
			get
			{
				return this.constants;
			}
		}

		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x060056FC RID: 22268 RVA: 0x001A89F8 File Offset: 0x001A89F8
		public MethodBodyChunks MethodBodies
		{
			get
			{
				return this.methodBodies;
			}
		}

		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x060056FD RID: 22269 RVA: 0x001A8A00 File Offset: 0x001A8A00
		public NetResources NetResources
		{
			get
			{
				return this.netResources;
			}
		}

		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x060056FE RID: 22270 RVA: 0x001A8A08 File Offset: 0x001A8A08
		public Metadata Metadata
		{
			get
			{
				return this.metadata;
			}
		}

		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x060056FF RID: 22271 RVA: 0x001A8A10 File Offset: 0x001A8A10
		public Win32ResourcesChunk Win32Resources
		{
			get
			{
				return this.win32Resources;
			}
		}

		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06005700 RID: 22272 RVA: 0x001A8A18 File Offset: 0x001A8A18
		public StrongNameSignature StrongNameSignature
		{
			get
			{
				return this.strongNameSignature;
			}
		}

		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x06005701 RID: 22273
		public abstract List<PESection> Sections { get; }

		// Token: 0x06005702 RID: 22274 RVA: 0x001A8A20 File Offset: 0x001A8A20
		public virtual void AddSection(PESection section)
		{
			this.Sections.Add(section);
		}

		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x06005703 RID: 22275
		public abstract PESection TextSection { get; }

		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x06005704 RID: 22276
		public abstract PESection RsrcSection { get; }

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x06005705 RID: 22277 RVA: 0x001A8A30 File Offset: 0x001A8A30
		public DebugDirectory DebugDirectory
		{
			get
			{
				return this.debugDirectory;
			}
		}

		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x06005706 RID: 22278 RVA: 0x001A8A38 File Offset: 0x001A8A38
		public bool IsNativeWriter
		{
			get
			{
				return this is NativeModuleWriter;
			}
		}

		// Token: 0x06005707 RID: 22279 RVA: 0x001A8A44 File Offset: 0x001A8A44
		public void Write(string fileName)
		{
			using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				fileStream.SetLength(0L);
				try
				{
					this.Write(fileStream);
				}
				catch
				{
					fileStream.Close();
					ModuleWriterBase.DeleteFileNoThrow(fileName);
					throw;
				}
			}
		}

		// Token: 0x06005708 RID: 22280 RVA: 0x001A8AA8 File Offset: 0x001A8AA8
		private static void DeleteFileNoThrow(string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				return;
			}
			try
			{
				File.Delete(fileName);
			}
			catch
			{
			}
		}

		// Token: 0x06005709 RID: 22281 RVA: 0x001A8AE4 File Offset: 0x001A8AE4
		public void Write(Stream dest)
		{
			this.pdbState = ((this.TheOptions.WritePdb && this.Module.PdbState != null) ? this.Module.PdbState : null);
			if (this.TheOptions.DelaySign)
			{
				this.TheOptions.Cor20HeaderOptions.Flags &= ~ComImageFlags.StrongNameSigned;
			}
			else if (this.TheOptions.StrongNameKey != null || this.TheOptions.StrongNamePublicKey != null)
			{
				this.TheOptions.Cor20HeaderOptions.Flags |= ComImageFlags.StrongNameSigned;
			}
			this.destStream = dest;
			this.destStreamBaseOffset = this.destStream.Position;
			this.OnWriterEvent(ModuleWriterEvent.Begin);
			long num = this.WriteImpl();
			this.destStream.Position = this.destStreamBaseOffset + num;
			this.OnWriterEvent(ModuleWriterEvent.End);
		}

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x0600570A RID: 22282
		public abstract ModuleDef Module { get; }

		// Token: 0x0600570B RID: 22283
		protected abstract long WriteImpl();

		// Token: 0x0600570C RID: 22284 RVA: 0x001A8C24 File Offset: 0x001A8C24
		protected void CreateStrongNameSignature()
		{
			if (this.TheOptions.DelaySign && this.TheOptions.StrongNamePublicKey != null)
			{
				int num = this.TheOptions.StrongNamePublicKey.CreatePublicKey().Length - 32;
				this.strongNameSignature = new StrongNameSignature((num > 0) ? num : 128);
				return;
			}
			if (this.TheOptions.StrongNameKey != null)
			{
				this.strongNameSignature = new StrongNameSignature(this.TheOptions.StrongNameKey.SignatureSize);
				return;
			}
			if (this.Module.Assembly != null && !PublicKeyBase.IsNullOrEmpty2(this.Module.Assembly.PublicKey))
			{
				int num2 = this.Module.Assembly.PublicKey.Data.Length - 32;
				this.strongNameSignature = new StrongNameSignature((num2 > 0) ? num2 : 128);
				return;
			}
			if (((this.TheOptions.Cor20HeaderOptions.Flags ?? this.Module.Cor20HeaderFlags) & ComImageFlags.StrongNameSigned) != (ComImageFlags)0U)
			{
				this.strongNameSignature = new StrongNameSignature(128);
			}
		}

		// Token: 0x0600570D RID: 22285 RVA: 0x001A8D64 File Offset: 0x001A8D64
		protected void CreateMetadataChunks(ModuleDef module)
		{
			this.constants = new UniqueChunkList<ByteArrayChunk>();
			this.methodBodies = new MethodBodyChunks(this.TheOptions.ShareMethodBodies);
			this.netResources = new NetResources(4U);
			DebugMetadataKind debugKind;
			if (this.pdbState != null && (this.pdbState.PdbFileKind == PdbFileKind.PortablePDB || this.pdbState.PdbFileKind == PdbFileKind.EmbeddedPortablePDB))
			{
				debugKind = DebugMetadataKind.Standalone;
			}
			else
			{
				debugKind = DebugMetadataKind.None;
			}
			this.metadata = Metadata.Create(module, this.constants, this.methodBodies, this.netResources, this.TheOptions.MetadataOptions, debugKind);
			this.metadata.Logger = (this.TheOptions.MetadataLogger ?? this);
			this.metadata.MetadataEvent += this.Metadata_MetadataEvent;
			this.metadata.ProgressUpdated += this.Metadata_ProgressUpdated;
			StrongNamePublicKey strongNamePublicKey = this.TheOptions.StrongNamePublicKey;
			if (strongNamePublicKey != null)
			{
				this.metadata.AssemblyPublicKey = strongNamePublicKey.CreatePublicKey();
			}
			else if (this.TheOptions.StrongNameKey != null)
			{
				this.metadata.AssemblyPublicKey = this.TheOptions.StrongNameKey.PublicKey;
			}
			Win32Resources win32Resources = this.GetWin32Resources();
			if (win32Resources != null)
			{
				this.win32Resources = new Win32ResourcesChunk(win32Resources);
			}
		}

		// Token: 0x0600570E RID: 22286
		protected abstract Win32Resources GetWin32Resources();

		// Token: 0x0600570F RID: 22287 RVA: 0x001A8EBC File Offset: 0x001A8EBC
		protected void CalculateRvasAndFileOffsets(List<IChunk> chunks, FileOffset offset, RVA rva, uint fileAlignment, uint sectionAlignment)
		{
			int count = chunks.Count;
			for (int i = 0; i < count; i++)
			{
				IChunk chunk = chunks[i];
				chunk.SetOffset(offset, rva);
				if (chunk.GetVirtualSize() != 0U)
				{
					offset += chunk.GetFileLength();
					rva += chunk.GetVirtualSize();
					offset = offset.AlignUp(fileAlignment);
					rva = rva.AlignUp(sectionAlignment);
				}
			}
		}

		// Token: 0x06005710 RID: 22288 RVA: 0x001A8F28 File Offset: 0x001A8F28
		protected void WriteChunks(DataWriter writer, List<IChunk> chunks, FileOffset offset, uint fileAlignment)
		{
			int count = chunks.Count;
			for (int i = 0; i < count; i++)
			{
				IChunk chunk = chunks[i];
				chunk.VerifyWriteTo(writer);
				if (chunk.GetVirtualSize() != 0U)
				{
					offset += chunk.GetFileLength();
					FileOffset fileOffset = offset.AlignUp(fileAlignment);
					writer.WriteZeroes((int)(fileOffset - offset));
					offset = fileOffset;
				}
			}
		}

		// Token: 0x06005711 RID: 22289 RVA: 0x001A8F88 File Offset: 0x001A8F88
		protected void StrongNameSign(long snSigOffset)
		{
			StrongNameSigner strongNameSigner = new StrongNameSigner(this.destStream, this.destStreamBaseOffset);
			strongNameSigner.WriteSignature(this.TheOptions.StrongNameKey, snSigOffset);
		}

		// Token: 0x06005712 RID: 22290 RVA: 0x001A8FC0 File Offset: 0x001A8FC0
		private bool CanWritePdb()
		{
			return this.pdbState != null;
		}

		// Token: 0x06005713 RID: 22291 RVA: 0x001A8FD0 File Offset: 0x001A8FD0
		protected void CreateDebugDirectory()
		{
			if (this.CanWritePdb())
			{
				this.debugDirectory = new DebugDirectory();
			}
		}

		// Token: 0x06005714 RID: 22292 RVA: 0x001A8FE8 File Offset: 0x001A8FE8
		protected void WritePdbFile()
		{
			if (!this.CanWritePdb())
			{
				return;
			}
			if (this.debugDirectory == null)
			{
				throw new InvalidOperationException("debugDirectory is null but WritePdb is true");
			}
			if (this.pdbState == null)
			{
				this.Error("TheOptions.WritePdb is true but module has no PdbState", new object[0]);
				return;
			}
			try
			{
				switch (this.pdbState.PdbFileKind)
				{
				case PdbFileKind.WindowsPDB:
					this.WriteWindowsPdb(this.pdbState);
					break;
				case PdbFileKind.PortablePDB:
					this.WritePortablePdb(this.pdbState, false);
					break;
				case PdbFileKind.EmbeddedPortablePDB:
					this.WritePortablePdb(this.pdbState, true);
					break;
				default:
					this.Error("Invalid PDB file kind {0}", new object[]
					{
						this.pdbState.PdbFileKind
					});
					break;
				}
			}
			catch
			{
				ModuleWriterBase.DeleteFileNoThrow(this.createdPdbFileName);
				throw;
			}
		}

		// Token: 0x06005715 RID: 22293 RVA: 0x001A90D4 File Offset: 0x001A90D4
		private void AddReproduciblePdbDebugDirectoryEntry()
		{
			this.debugDirectory.Add(Array2.Empty<byte>(), ImageDebugType.Reproducible, 0, 0, 0U);
		}

		// Token: 0x06005716 RID: 22294 RVA: 0x001A90EC File Offset: 0x001A90EC
		private void AddPdbChecksumDebugDirectoryEntry(byte[] checksumBytes, ChecksumAlgorithm checksumAlgorithm)
		{
			MemoryStream memoryStream = new MemoryStream();
			DataWriter dataWriter = new DataWriter(memoryStream);
			string checksumName = Hasher.GetChecksumName(checksumAlgorithm);
			dataWriter.WriteBytes(Encoding.UTF8.GetBytes(checksumName));
			dataWriter.WriteByte(0);
			dataWriter.WriteBytes(checksumBytes);
			byte[] data = memoryStream.ToArray();
			this.debugDirectory.Add(data, ImageDebugType.PdbChecksum, 1, 0, 0U);
		}

		// Token: 0x06005717 RID: 22295 RVA: 0x001A9148 File Offset: 0x001A9148
		private void WriteWindowsPdb(PdbState pdbState)
		{
			bool flag = (this.TheOptions.PdbOptions & PdbWriterOptions.PdbChecksum) > PdbWriterOptions.None;
			string text;
			SymbolWriter windowsPdbSymbolWriter = this.GetWindowsPdbSymbolWriter(this.TheOptions.PdbOptions, out text);
			if (windowsPdbSymbolWriter == null)
			{
				this.Error("Could not create a PDB symbol writer. A Windows OS might be required.", new object[0]);
				return;
			}
			using (WindowsPdbWriter windowsPdbWriter = new WindowsPdbWriter(windowsPdbSymbolWriter, pdbState, this.metadata))
			{
				windowsPdbWriter.Logger = this.TheOptions.Logger;
				windowsPdbWriter.Write();
				uint age = 1U;
				Guid guid;
				uint timeDateStamp;
				IMAGE_DEBUG_DIRECTORY image_DEBUG_DIRECTORY;
				byte[] array;
				if (windowsPdbWriter.GetDebugInfo(this.TheOptions.PdbChecksumAlgorithm, ref age, out guid, out timeDateStamp, out image_DEBUG_DIRECTORY, out array))
				{
					this.debugDirectory.Add(ModuleWriterBase.GetCodeViewData(guid, age, this.TheOptions.PdbFileNameInDebugDirectory ?? text), ImageDebugType.CodeView, 0, 0, timeDateStamp);
				}
				else
				{
					if (array == null)
					{
						throw new InvalidOperationException();
					}
					DebugDirectoryEntry debugDirectoryEntry = this.debugDirectory.Add(array);
					debugDirectoryEntry.DebugDirectory = image_DEBUG_DIRECTORY;
					debugDirectoryEntry.DebugDirectory.TimeDateStamp = this.GetTimeDateStamp();
				}
				if (windowsPdbSymbolWriter.IsDeterministic)
				{
					this.AddReproduciblePdbDebugDirectoryEntry();
				}
			}
		}

		// Token: 0x06005718 RID: 22296 RVA: 0x001A9278 File Offset: 0x001A9278
		protected uint GetTimeDateStamp()
		{
			uint? timeDateStamp = this.TheOptions.PEHeadersOptions.TimeDateStamp;
			if (timeDateStamp != null)
			{
				return timeDateStamp.Value;
			}
			this.TheOptions.PEHeadersOptions.TimeDateStamp = new uint?(PEHeadersOptions.CreateNewTimeDateStamp());
			return this.TheOptions.PEHeadersOptions.TimeDateStamp.Value;
		}

		// Token: 0x06005719 RID: 22297 RVA: 0x001A92E0 File Offset: 0x001A92E0
		private SymbolWriter GetWindowsPdbSymbolWriter(PdbWriterOptions options, out string pdbFilename)
		{
			string pdbFileName;
			if (this.TheOptions.PdbStream != null)
			{
				Stream pdbStream = this.TheOptions.PdbStream;
				string text;
				if ((text = this.TheOptions.PdbFileName) == null)
				{
					text = (ModuleWriterBase.GetStreamName(this.TheOptions.PdbStream) ?? this.GetDefaultPdbFileName());
				}
				pdbFilename = (pdbFileName = text);
				return SymbolReaderWriterFactory.Create(options, pdbStream, pdbFileName);
			}
			if (!string.IsNullOrEmpty(this.TheOptions.PdbFileName))
			{
				pdbFilename = (pdbFileName = this.TheOptions.PdbFileName);
				this.createdPdbFileName = pdbFileName;
				return SymbolReaderWriterFactory.Create(options, this.createdPdbFileName);
			}
			pdbFilename = (pdbFileName = this.GetDefaultPdbFileName());
			this.createdPdbFileName = pdbFileName;
			if (this.createdPdbFileName == null)
			{
				return null;
			}
			return SymbolReaderWriterFactory.Create(options, this.createdPdbFileName);
		}

		// Token: 0x0600571A RID: 22298 RVA: 0x001A93AC File Offset: 0x001A93AC
		private static string GetStreamName(Stream stream)
		{
			FileStream fileStream = stream as FileStream;
			if (fileStream == null)
			{
				return null;
			}
			return fileStream.Name;
		}

		// Token: 0x0600571B RID: 22299 RVA: 0x001A93C4 File Offset: 0x001A93C4
		private static string GetModuleName(ModuleDef module)
		{
			UTF8String utf8String = module.Name ?? string.Empty;
			if (string.IsNullOrEmpty(utf8String))
			{
				return null;
			}
			if (utf8String.EndsWith(".dll", StringComparison.OrdinalIgnoreCase) || utf8String.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) || utf8String.EndsWith(".netmodule", StringComparison.OrdinalIgnoreCase))
			{
				return utf8String;
			}
			return utf8String + ".pdb";
		}

		// Token: 0x0600571C RID: 22300 RVA: 0x001A944C File Offset: 0x001A944C
		private string GetDefaultPdbFileName()
		{
			string text = ModuleWriterBase.GetStreamName(this.destStream) ?? ModuleWriterBase.GetModuleName(this.Module);
			if (string.IsNullOrEmpty(text))
			{
				this.Error("TheOptions.WritePdb is true but it's not possible to guess the default PDB file name. Set PdbFileName to the name of the PDB file.", new object[0]);
				return null;
			}
			return Path.ChangeExtension(text, "pdb");
		}

		// Token: 0x0600571D RID: 22301 RVA: 0x001A94A4 File Offset: 0x001A94A4
		private void WritePortablePdb(PdbState pdbState, bool isEmbeddedPortablePdb)
		{
			bool flag = false;
			Stream stream = null;
			try
			{
				MemoryStream portablePdbStream = null;
				if (isEmbeddedPortablePdb)
				{
					portablePdbStream = (stream = new MemoryStream());
					flag = true;
				}
				else
				{
					stream = this.GetStandalonePortablePdbStream(out flag);
				}
				if (stream == null)
				{
					throw new ModuleWriterException("Couldn't create a PDB stream");
				}
				string text;
				if ((text = this.TheOptions.PdbFileName) == null)
				{
					text = (ModuleWriterBase.GetStreamName(stream) ?? this.GetDefaultPdbFileName());
				}
				string text2 = text;
				if (isEmbeddedPortablePdb)
				{
					text2 = Path.GetFileName(text2);
				}
				uint entryPointToken;
				if (pdbState.UserEntryPoint == null)
				{
					entryPointToken = 0U;
				}
				else
				{
					entryPointToken = new MDToken(Table.Method, this.metadata.GetRid(pdbState.UserEntryPoint)).Raw;
				}
				long position;
				this.metadata.WritePortablePdb(stream, entryPointToken, out position);
				byte[] array = new byte[20];
				ArrayWriter arrayWriter = new ArrayWriter(array);
				byte[] array2;
				Guid guid;
				uint timestamp;
				if ((this.TheOptions.PdbOptions & PdbWriterOptions.Deterministic) != PdbWriterOptions.None || (this.TheOptions.PdbOptions & PdbWriterOptions.PdbChecksum) != PdbWriterOptions.None || this.TheOptions.GetPdbContentId == null)
				{
					stream.Position = 0L;
					array2 = Hasher.Hash(this.TheOptions.PdbChecksumAlgorithm, stream, stream.Length);
					if (array2.Length < 20)
					{
						throw new ModuleWriterException("Checksum bytes length < 20");
					}
					RoslynContentIdProvider.GetContentId(array2, out guid, out timestamp);
				}
				else
				{
					ContentId contentId = this.TheOptions.GetPdbContentId(stream, this.GetTimeDateStamp());
					timestamp = contentId.Timestamp;
					guid = contentId.Guid;
					array2 = null;
				}
				arrayWriter.WriteBytes(guid.ToByteArray());
				arrayWriter.WriteUInt32(timestamp);
				stream.Position = position;
				stream.Write(array, 0, array.Length);
				this.debugDirectory.Add(ModuleWriterBase.GetCodeViewData(guid, 1U, this.TheOptions.PdbFileNameInDebugDirectory ?? text2), ImageDebugType.CodeView, 256, 20557, timestamp);
				if (array2 != null)
				{
					this.AddPdbChecksumDebugDirectoryEntry(array2, this.TheOptions.PdbChecksumAlgorithm);
				}
				if ((this.TheOptions.PdbOptions & PdbWriterOptions.Deterministic) != PdbWriterOptions.None)
				{
					this.AddReproduciblePdbDebugDirectoryEntry();
				}
				if (isEmbeddedPortablePdb)
				{
					this.debugDirectory.Add(ModuleWriterBase.CreateEmbeddedPortablePdbBlob(portablePdbStream), ImageDebugType.EmbeddedPortablePdb, 256, 256, 0U);
				}
			}
			finally
			{
				if (flag && stream != null)
				{
					stream.Dispose();
				}
			}
		}

		// Token: 0x0600571E RID: 22302 RVA: 0x001A9700 File Offset: 0x001A9700
		private static byte[] CreateEmbeddedPortablePdbBlob(MemoryStream portablePdbStream)
		{
			byte[] array = ModuleWriterBase.Compress(portablePdbStream);
			byte[] array2 = new byte[8 + array.Length];
			DataWriter dataWriter = new DataWriter(new MemoryStream(array2));
			dataWriter.WriteInt32(1111773261);
			dataWriter.WriteUInt32((uint)portablePdbStream.Length);
			dataWriter.WriteBytes(array);
			return array2;
		}

		// Token: 0x0600571F RID: 22303 RVA: 0x001A974C File Offset: 0x001A974C
		private static byte[] Compress(MemoryStream sourceStream)
		{
			sourceStream.Position = 0L;
			MemoryStream memoryStream = new MemoryStream();
			using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress))
			{
				byte[] array = sourceStream.ToArray();
				deflateStream.Write(array, 0, array.Length);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06005720 RID: 22304 RVA: 0x001A97AC File Offset: 0x001A97AC
		private static byte[] GetCodeViewData(Guid guid, uint age, string filename)
		{
			MemoryStream memoryStream = new MemoryStream();
			DataWriter dataWriter = new DataWriter(memoryStream);
			dataWriter.WriteInt32(1396986706);
			dataWriter.WriteBytes(guid.ToByteArray());
			dataWriter.WriteUInt32(age);
			dataWriter.WriteBytes(Encoding.UTF8.GetBytes(filename));
			dataWriter.WriteByte(0);
			return memoryStream.ToArray();
		}

		// Token: 0x06005721 RID: 22305 RVA: 0x001A9804 File Offset: 0x001A9804
		private Stream GetStandalonePortablePdbStream(out bool ownsStream)
		{
			if (this.TheOptions.PdbStream != null)
			{
				ownsStream = false;
				return this.TheOptions.PdbStream;
			}
			if (!string.IsNullOrEmpty(this.TheOptions.PdbFileName))
			{
				this.createdPdbFileName = this.TheOptions.PdbFileName;
			}
			else
			{
				this.createdPdbFileName = this.GetDefaultPdbFileName();
			}
			if (this.createdPdbFileName == null)
			{
				ownsStream = false;
				return null;
			}
			ownsStream = true;
			return File.Create(this.createdPdbFileName);
		}

		// Token: 0x06005722 RID: 22306 RVA: 0x001A988C File Offset: 0x001A988C
		private void Metadata_MetadataEvent(object sender, MetadataWriterEventArgs e)
		{
			switch (e.Event)
			{
			case MetadataEvent.BeginCreateTables:
				this.OnWriterEvent(ModuleWriterEvent.MDBeginCreateTables);
				return;
			case MetadataEvent.AllocateTypeDefRids:
				this.OnWriterEvent(ModuleWriterEvent.MDAllocateTypeDefRids);
				return;
			case MetadataEvent.AllocateMemberDefRids:
				this.OnWriterEvent(ModuleWriterEvent.MDAllocateMemberDefRids);
				return;
			case MetadataEvent.MemberDefRidsAllocated:
				this.OnWriterEvent(ModuleWriterEvent.MDMemberDefRidsAllocated);
				return;
			case MetadataEvent.MemberDefsInitialized:
				this.OnWriterEvent(ModuleWriterEvent.MDMemberDefsInitialized);
				return;
			case MetadataEvent.BeforeSortTables:
				this.OnWriterEvent(ModuleWriterEvent.MDBeforeSortTables);
				return;
			case MetadataEvent.MostTablesSorted:
				this.OnWriterEvent(ModuleWriterEvent.MDMostTablesSorted);
				return;
			case MetadataEvent.MemberDefCustomAttributesWritten:
				this.OnWriterEvent(ModuleWriterEvent.MDMemberDefCustomAttributesWritten);
				return;
			case MetadataEvent.BeginAddResources:
				this.OnWriterEvent(ModuleWriterEvent.MDBeginAddResources);
				return;
			case MetadataEvent.EndAddResources:
				this.OnWriterEvent(ModuleWriterEvent.MDEndAddResources);
				return;
			case MetadataEvent.BeginWriteMethodBodies:
				this.OnWriterEvent(ModuleWriterEvent.MDBeginWriteMethodBodies);
				return;
			case MetadataEvent.EndWriteMethodBodies:
				this.OnWriterEvent(ModuleWriterEvent.MDEndWriteMethodBodies);
				return;
			case MetadataEvent.OnAllTablesSorted:
				this.OnWriterEvent(ModuleWriterEvent.MDOnAllTablesSorted);
				return;
			case MetadataEvent.EndCreateTables:
				this.OnWriterEvent(ModuleWriterEvent.MDEndCreateTables);
				return;
			default:
				return;
			}
		}

		// Token: 0x06005723 RID: 22307 RVA: 0x001A995C File Offset: 0x001A995C
		private void Metadata_ProgressUpdated(object sender, MetadataProgressEventArgs e)
		{
			this.RaiseProgress(ModuleWriterEvent.MDBeginCreateTables, ModuleWriterEvent.BeginWritePdb, e.Progress);
		}

		// Token: 0x06005724 RID: 22308 RVA: 0x001A9970 File Offset: 0x001A9970
		protected void OnWriterEvent(ModuleWriterEvent evt)
		{
			this.RaiseProgress(evt, 0.0);
			this.TheOptions.RaiseEvent(this, new ModuleWriterEventArgs(this, evt));
		}

		// Token: 0x06005725 RID: 22309 RVA: 0x001A99A4 File Offset: 0x001A99A4
		private void RaiseProgress(ModuleWriterEvent evt, double subProgress)
		{
			this.RaiseProgress(evt, evt + 1, subProgress);
		}

		// Token: 0x06005726 RID: 22310 RVA: 0x001A99B4 File Offset: 0x001A99B4
		private void RaiseProgress(ModuleWriterEvent evt, ModuleWriterEvent nextEvt, double subProgress)
		{
			subProgress = Math.Min(1.0, Math.Max(0.0, subProgress));
			double num = ModuleWriterBase.eventToProgress[(int)evt];
			double num2 = ModuleWriterBase.eventToProgress[(int)nextEvt];
			double num3 = num + (num2 - num) * subProgress;
			num3 = Math.Min(1.0, Math.Max(0.0, num3));
			this.TheOptions.RaiseEvent(this, new ModuleWriterProgressEventArgs(this, num3));
		}

		// Token: 0x06005727 RID: 22311 RVA: 0x001A9A30 File Offset: 0x001A9A30
		private ILogger GetLogger()
		{
			return this.TheOptions.Logger ?? DummyLogger.ThrowModuleWriterExceptionOnErrorInstance;
		}

		// Token: 0x06005728 RID: 22312 RVA: 0x001A9A4C File Offset: 0x001A9A4C
		void ILogger.Log(object sender, LoggerEvent loggerEvent, string format, params object[] args)
		{
			this.GetLogger().Log(this, loggerEvent, format, args);
		}

		// Token: 0x06005729 RID: 22313 RVA: 0x001A9A60 File Offset: 0x001A9A60
		bool ILogger.IgnoresEvent(LoggerEvent loggerEvent)
		{
			return this.GetLogger().IgnoresEvent(loggerEvent);
		}

		// Token: 0x0600572A RID: 22314 RVA: 0x001A9A70 File Offset: 0x001A9A70
		protected void Error(string format, params object[] args)
		{
			this.GetLogger().Log(this, LoggerEvent.Error, format, args);
		}

		// Token: 0x0600572B RID: 22315 RVA: 0x001A9A84 File Offset: 0x001A9A84
		protected void Warning(string format, params object[] args)
		{
			this.GetLogger().Log(this, LoggerEvent.Warning, format, args);
		}

		// Token: 0x040029AF RID: 10671
		protected internal const uint DEFAULT_CONSTANTS_ALIGNMENT = 8U;

		// Token: 0x040029B0 RID: 10672
		protected const uint DEFAULT_METHODBODIES_ALIGNMENT = 4U;

		// Token: 0x040029B1 RID: 10673
		protected const uint DEFAULT_NETRESOURCES_ALIGNMENT = 4U;

		// Token: 0x040029B2 RID: 10674
		protected const uint DEFAULT_METADATA_ALIGNMENT = 4U;

		// Token: 0x040029B3 RID: 10675
		protected internal const uint DEFAULT_WIN32_RESOURCES_ALIGNMENT = 8U;

		// Token: 0x040029B4 RID: 10676
		protected const uint DEFAULT_STRONGNAMESIG_ALIGNMENT = 4U;

		// Token: 0x040029B5 RID: 10677
		protected const uint DEFAULT_COR20HEADER_ALIGNMENT = 4U;

		// Token: 0x040029B6 RID: 10678
		protected Stream destStream;

		// Token: 0x040029B7 RID: 10679
		protected UniqueChunkList<ByteArrayChunk> constants;

		// Token: 0x040029B8 RID: 10680
		protected MethodBodyChunks methodBodies;

		// Token: 0x040029B9 RID: 10681
		protected NetResources netResources;

		// Token: 0x040029BA RID: 10682
		protected Metadata metadata;

		// Token: 0x040029BB RID: 10683
		protected Win32ResourcesChunk win32Resources;

		// Token: 0x040029BC RID: 10684
		protected long destStreamBaseOffset;

		// Token: 0x040029BD RID: 10685
		protected DebugDirectory debugDirectory;

		// Token: 0x040029BE RID: 10686
		private string createdPdbFileName;

		// Token: 0x040029BF RID: 10687
		protected StrongNameSignature strongNameSignature;

		// Token: 0x040029C0 RID: 10688
		private PdbState pdbState;

		// Token: 0x040029C1 RID: 10689
		private const uint PdbAge = 1U;

		// Token: 0x040029C2 RID: 10690
		private static readonly double[] eventToProgress = new double[]
		{
			0.0,
			0.00128048488389907,
			0.0524625293056615,
			0.0531036610555682,
			0.0535679983835939,
			0.0547784058004697,
			0.0558606342971218,
			0.120553993799033,
			0.226210300699921,
			0.236002648477671,
			0.291089703426468,
			0.449919748849947,
			0.449919985998736,
			0.452716444513587,
			0.452716681662375,
			0.924922132195272,
			0.931410404476231,
			0.931425463424305,
			0.932072998191503,
			0.932175327893773,
			0.932175446468167,
			0.954646479929387,
			0.95492263969368,
			0.980563166714175,
			0.980563403862964,
			0.980563403862964,
			0.980563522437358,
			0.999975573674777,
			1.0,
			1.0
		};
	}
}
