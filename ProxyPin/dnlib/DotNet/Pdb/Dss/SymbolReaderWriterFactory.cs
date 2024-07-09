using System;
using System.IO;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb.Symbols;
using dnlib.DotNet.Pdb.WindowsPdb;
using dnlib.DotNet.Writer;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x0200097E RID: 2430
	internal static class SymbolReaderWriterFactory
	{
		// Token: 0x06005D9C RID: 23964
		[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories | DllImportSearchPath.AssemblyDirectory)]
		[DllImport("Microsoft.DiaSymReader.Native.x86.dll", EntryPoint = "CreateSymReader")]
		private static extern void CreateSymReader_x86(ref Guid id, [MarshalAs(UnmanagedType.IUnknown)] out object symReader);

		// Token: 0x06005D9D RID: 23965
		[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories | DllImportSearchPath.AssemblyDirectory)]
		[DllImport("Microsoft.DiaSymReader.Native.amd64.dll", EntryPoint = "CreateSymReader")]
		private static extern void CreateSymReader_x64(ref Guid id, [MarshalAs(UnmanagedType.IUnknown)] out object symReader);

		// Token: 0x06005D9E RID: 23966
		[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories | DllImportSearchPath.AssemblyDirectory)]
		[DllImport("Microsoft.DiaSymReader.Native.arm.dll", EntryPoint = "CreateSymReader")]
		private static extern void CreateSymReader_arm(ref Guid id, [MarshalAs(UnmanagedType.IUnknown)] out object symReader);

		// Token: 0x06005D9F RID: 23967
		[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories | DllImportSearchPath.AssemblyDirectory)]
		[DllImport("Microsoft.DiaSymReader.Native.x86.dll", EntryPoint = "CreateSymWriter")]
		private static extern void CreateSymWriter_x86(ref Guid guid, [MarshalAs(UnmanagedType.IUnknown)] out object symWriter);

		// Token: 0x06005DA0 RID: 23968
		[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories | DllImportSearchPath.AssemblyDirectory)]
		[DllImport("Microsoft.DiaSymReader.Native.amd64.dll", EntryPoint = "CreateSymWriter")]
		private static extern void CreateSymWriter_x64(ref Guid guid, [MarshalAs(UnmanagedType.IUnknown)] out object symWriter);

		// Token: 0x06005DA1 RID: 23969
		[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories | DllImportSearchPath.AssemblyDirectory)]
		[DllImport("Microsoft.DiaSymReader.Native.arm.dll", EntryPoint = "CreateSymWriter")]
		private static extern void CreateSymWriter_arm(ref Guid guid, [MarshalAs(UnmanagedType.IUnknown)] out object symWriter);

		// Token: 0x06005DA2 RID: 23970 RVA: 0x001C184C File Offset: 0x001C184C
		public static SymbolReader Create(PdbReaderContext pdbContext, dnlib.DotNet.MD.Metadata metadata, DataReaderFactory pdbStream)
		{
			ISymUnmanagedReader symUnmanagedReader = null;
			SymbolReaderImpl symbolReaderImpl = null;
			ReaderMetaDataImport readerMetaDataImport = null;
			DataReaderIStream dataReaderIStream = null;
			bool flag = true;
			try
			{
				if (pdbStream == null)
				{
					return null;
				}
				ImageDebugDirectory codeViewDebugDirectory = pdbContext.CodeViewDebugDirectory;
				if (codeViewDebugDirectory == null)
				{
					return null;
				}
				Guid pdbId;
				uint age;
				if (!pdbContext.TryGetCodeViewData(out pdbId, out age))
				{
					return null;
				}
				symUnmanagedReader = SymbolReaderWriterFactory.CreateSymUnmanagedReader(pdbContext.Options);
				if (symUnmanagedReader == null)
				{
					return null;
				}
				readerMetaDataImport = new ReaderMetaDataImport(metadata);
				dataReaderIStream = new DataReaderIStream(pdbStream);
				if (symUnmanagedReader.Initialize(readerMetaDataImport, null, null, dataReaderIStream) < 0)
				{
					return null;
				}
				symbolReaderImpl = new SymbolReaderImpl(symUnmanagedReader, new object[]
				{
					pdbStream,
					readerMetaDataImport,
					dataReaderIStream
				});
				if (!symbolReaderImpl.MatchesModule(pdbId, codeViewDebugDirectory.TimeDateStamp, age))
				{
					return null;
				}
				flag = false;
				return symbolReaderImpl;
			}
			catch (IOException)
			{
			}
			catch (InvalidCastException)
			{
			}
			catch (COMException)
			{
			}
			finally
			{
				if (flag)
				{
					if (pdbStream != null)
					{
						pdbStream.Dispose();
					}
					if (symbolReaderImpl != null)
					{
						symbolReaderImpl.Dispose();
					}
					if (readerMetaDataImport != null)
					{
						readerMetaDataImport.Dispose();
					}
					if (dataReaderIStream != null)
					{
						dataReaderIStream.Dispose();
					}
					ISymUnmanagedDispose symUnmanagedDispose = symUnmanagedReader as ISymUnmanagedDispose;
					if (symUnmanagedDispose != null)
					{
						symUnmanagedDispose.Destroy();
					}
				}
			}
			return null;
		}

		// Token: 0x06005DA3 RID: 23971 RVA: 0x001C19C0 File Offset: 0x001C19C0
		private static ISymUnmanagedReader CreateSymUnmanagedReader(PdbReaderOptions options)
		{
			bool flag = (options & PdbReaderOptions.NoDiaSymReader) == PdbReaderOptions.None;
			bool flag2 = (options & PdbReaderOptions.NoOldDiaSymReader) == PdbReaderOptions.None;
			if (flag && SymbolReaderWriterFactory.canTry_Microsoft_DiaSymReader_Native)
			{
				try
				{
					Guid clsid_CorSymReader_SxS = SymbolReaderWriterFactory.CLSID_CorSymReader_SxS;
					Machine processCpuArchitecture = ProcessorArchUtils.GetProcessCpuArchitecture();
					object obj;
					if (processCpuArchitecture != Machine.I386)
					{
						if (processCpuArchitecture != Machine.ARMNT)
						{
							if (processCpuArchitecture == Machine.AMD64)
							{
								SymbolReaderWriterFactory.CreateSymReader_x64(ref clsid_CorSymReader_SxS, out obj);
							}
							else
							{
								obj = null;
							}
						}
						else
						{
							SymbolReaderWriterFactory.CreateSymReader_arm(ref clsid_CorSymReader_SxS, out obj);
						}
					}
					else
					{
						SymbolReaderWriterFactory.CreateSymReader_x86(ref clsid_CorSymReader_SxS, out obj);
					}
					ISymUnmanagedReader symUnmanagedReader = obj as ISymUnmanagedReader;
					if (symUnmanagedReader != null)
					{
						return symUnmanagedReader;
					}
				}
				catch (DllNotFoundException)
				{
				}
				catch
				{
				}
				SymbolReaderWriterFactory.canTry_Microsoft_DiaSymReader_Native = false;
			}
			if (flag2)
			{
				Type type;
				if ((type = SymbolReaderWriterFactory.CorSymReader_Type) == null)
				{
					type = (SymbolReaderWriterFactory.CorSymReader_Type = Type.GetTypeFromCLSID(SymbolReaderWriterFactory.CLSID_CorSymReader_SxS));
				}
				return (ISymUnmanagedReader)Activator.CreateInstance(type);
			}
			return null;
		}

		// Token: 0x06005DA4 RID: 23972 RVA: 0x001C1AC4 File Offset: 0x001C1AC4
		private static ISymUnmanagedWriter2 CreateSymUnmanagedWriter2(PdbWriterOptions options)
		{
			bool flag = (options & PdbWriterOptions.NoDiaSymReader) == PdbWriterOptions.None;
			bool flag2 = (options & PdbWriterOptions.NoOldDiaSymReader) == PdbWriterOptions.None;
			if (flag && SymbolReaderWriterFactory.canTry_Microsoft_DiaSymReader_Native)
			{
				try
				{
					Guid clsid_CorSymWriter_SxS = SymbolReaderWriterFactory.CLSID_CorSymWriter_SxS;
					Machine processCpuArchitecture = ProcessorArchUtils.GetProcessCpuArchitecture();
					object obj;
					if (processCpuArchitecture != Machine.I386)
					{
						if (processCpuArchitecture != Machine.ARMNT)
						{
							if (processCpuArchitecture == Machine.AMD64)
							{
								SymbolReaderWriterFactory.CreateSymWriter_x64(ref clsid_CorSymWriter_SxS, out obj);
							}
							else
							{
								obj = null;
							}
						}
						else
						{
							SymbolReaderWriterFactory.CreateSymWriter_arm(ref clsid_CorSymWriter_SxS, out obj);
						}
					}
					else
					{
						SymbolReaderWriterFactory.CreateSymWriter_x86(ref clsid_CorSymWriter_SxS, out obj);
					}
					ISymUnmanagedWriter2 symUnmanagedWriter = obj as ISymUnmanagedWriter2;
					if (symUnmanagedWriter != null)
					{
						return symUnmanagedWriter;
					}
				}
				catch (DllNotFoundException)
				{
				}
				catch
				{
				}
				SymbolReaderWriterFactory.canTry_Microsoft_DiaSymReader_Native = false;
			}
			if (flag2)
			{
				Type type;
				if ((type = SymbolReaderWriterFactory.CorSymWriterType) == null)
				{
					type = (SymbolReaderWriterFactory.CorSymWriterType = Type.GetTypeFromCLSID(SymbolReaderWriterFactory.CLSID_CorSymWriter_SxS));
				}
				return (ISymUnmanagedWriter2)Activator.CreateInstance(type);
			}
			return null;
		}

		// Token: 0x06005DA5 RID: 23973 RVA: 0x001C1BC8 File Offset: 0x001C1BC8
		public static SymbolWriter Create(PdbWriterOptions options, string pdbFileName)
		{
			if (File.Exists(pdbFileName))
			{
				File.Delete(pdbFileName);
			}
			return new SymbolWriterImpl(SymbolReaderWriterFactory.CreateSymUnmanagedWriter2(options), pdbFileName, File.Create(pdbFileName), options, true);
		}

		// Token: 0x06005DA6 RID: 23974 RVA: 0x001C1BF0 File Offset: 0x001C1BF0
		public static SymbolWriter Create(PdbWriterOptions options, Stream pdbStream, string pdbFileName)
		{
			return new SymbolWriterImpl(SymbolReaderWriterFactory.CreateSymUnmanagedWriter2(options), pdbFileName, pdbStream, options, false);
		}

		// Token: 0x04002D6A RID: 11626
		private static readonly Guid CLSID_CorSymReader_SxS = new Guid("0A3976C5-4529-4ef8-B0B0-42EED37082CD");

		// Token: 0x04002D6B RID: 11627
		private static Type CorSymReader_Type;

		// Token: 0x04002D6C RID: 11628
		private static readonly Guid CLSID_CorSymWriter_SxS = new Guid(182640304U, 63745, 18315, 187, 159, 136, 30, 232, 6, 103, 136);

		// Token: 0x04002D6D RID: 11629
		private static Type CorSymWriterType;

		// Token: 0x04002D6E RID: 11630
		private static volatile bool canTry_Microsoft_DiaSymReader_Native = true;
	}
}
