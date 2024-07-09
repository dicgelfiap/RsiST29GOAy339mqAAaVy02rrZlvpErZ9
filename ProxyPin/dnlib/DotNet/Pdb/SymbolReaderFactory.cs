using System;
using System.IO;
using System.Text;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb.Dss;
using dnlib.DotNet.Pdb.Managed;
using dnlib.DotNet.Pdb.Portable;
using dnlib.DotNet.Pdb.Symbols;
using dnlib.IO;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000926 RID: 2342
	internal static class SymbolReaderFactory
	{
		// Token: 0x06005A5A RID: 23130 RVA: 0x001B7660 File Offset: 0x001B7660
		public static SymbolReader CreateFromAssemblyFile(PdbReaderOptions options, Metadata metadata, string assemblyFileName)
		{
			PdbReaderContext pdbReaderContext = new PdbReaderContext(metadata.PEImage, options);
			if (!pdbReaderContext.HasDebugInfo)
			{
				return null;
			}
			Guid guid;
			uint num;
			string text;
			if (!pdbReaderContext.TryGetCodeViewData(out guid, out num, out text))
			{
				return null;
			}
			int num2 = text.LastIndexOfAny(dnlib.DotNet.Pdb.SymbolReaderFactory.windowsPathSepChars);
			string text2;
			if (num2 >= 0)
			{
				text2 = text.Substring(num2 + 1);
			}
			else
			{
				text2 = text;
			}
			string text3;
			try
			{
				text3 = ((assemblyFileName == string.Empty) ? text2 : Path.Combine(Path.GetDirectoryName(assemblyFileName), text2));
				if (!File.Exists(text3))
				{
					string text4 = Path.GetExtension(text2);
					if (string.IsNullOrEmpty(text4))
					{
						text4 = "pdb";
					}
					text3 = Path.ChangeExtension(assemblyFileName, text4);
				}
			}
			catch (ArgumentException)
			{
				return null;
			}
			return dnlib.DotNet.Pdb.SymbolReaderFactory.Create(options, metadata, text3);
		}

		// Token: 0x06005A5B RID: 23131 RVA: 0x001B774C File Offset: 0x001B774C
		public static SymbolReader Create(PdbReaderOptions options, Metadata metadata, string pdbFileName)
		{
			PdbReaderContext pdbContext = new PdbReaderContext(metadata.PEImage, options);
			if (!pdbContext.HasDebugInfo)
			{
				return null;
			}
			return dnlib.DotNet.Pdb.SymbolReaderFactory.CreateCore(pdbContext, metadata, DataReaderFactoryUtils.TryCreateDataReaderFactory(pdbFileName));
		}

		// Token: 0x06005A5C RID: 23132 RVA: 0x001B7788 File Offset: 0x001B7788
		public static SymbolReader Create(PdbReaderOptions options, Metadata metadata, byte[] pdbData)
		{
			PdbReaderContext pdbContext = new PdbReaderContext(metadata.PEImage, options);
			if (!pdbContext.HasDebugInfo)
			{
				return null;
			}
			return dnlib.DotNet.Pdb.SymbolReaderFactory.CreateCore(pdbContext, metadata, ByteArrayDataReaderFactory.Create(pdbData, null));
		}

		// Token: 0x06005A5D RID: 23133 RVA: 0x001B77C4 File Offset: 0x001B77C4
		public static SymbolReader Create(PdbReaderOptions options, Metadata metadata, DataReaderFactory pdbStream)
		{
			return dnlib.DotNet.Pdb.SymbolReaderFactory.CreateCore(new PdbReaderContext(metadata.PEImage, options), metadata, pdbStream);
		}

		// Token: 0x06005A5E RID: 23134 RVA: 0x001B77DC File Offset: 0x001B77DC
		private static SymbolReader CreateCore(PdbReaderContext pdbContext, Metadata metadata, DataReaderFactory pdbStream)
		{
			SymbolReader symbolReader = null;
			bool flag = true;
			try
			{
				if (!pdbContext.HasDebugInfo)
				{
					return null;
				}
				if ((pdbContext.Options & PdbReaderOptions.MicrosoftComReader) != PdbReaderOptions.None && pdbStream != null && dnlib.DotNet.Pdb.SymbolReaderFactory.IsWindowsPdb(pdbStream.CreateReader()))
				{
					symbolReader = SymbolReaderWriterFactory.Create(pdbContext, metadata, pdbStream);
				}
				else
				{
					symbolReader = dnlib.DotNet.Pdb.SymbolReaderFactory.CreateManaged(pdbContext, metadata, pdbStream);
				}
				if (symbolReader != null)
				{
					flag = false;
					return symbolReader;
				}
			}
			catch (IOException)
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
					if (symbolReader != null)
					{
						symbolReader.Dispose();
					}
				}
			}
			return null;
		}

		// Token: 0x06005A5F RID: 23135 RVA: 0x001B7894 File Offset: 0x001B7894
		private static bool IsWindowsPdb(DataReader reader)
		{
			return reader.CanRead("Microsoft C/C++ MSF 7.00\r\n\u001aDS\0".Length) && reader.ReadString("Microsoft C/C++ MSF 7.00\r\n\u001aDS\0".Length, Encoding.ASCII) == "Microsoft C/C++ MSF 7.00\r\n\u001aDS\0";
		}

		// Token: 0x06005A60 RID: 23136 RVA: 0x001B78D0 File Offset: 0x001B78D0
		public static SymbolReader TryCreateEmbeddedPdbReader(PdbReaderOptions options, Metadata metadata)
		{
			PdbReaderContext pdbContext = new PdbReaderContext(metadata.PEImage, options);
			if (!pdbContext.HasDebugInfo)
			{
				return null;
			}
			return dnlib.DotNet.Pdb.SymbolReaderFactory.TryCreateEmbeddedPortablePdbReader(pdbContext, metadata);
		}

		// Token: 0x06005A61 RID: 23137 RVA: 0x001B7904 File Offset: 0x001B7904
		private static SymbolReader CreateManaged(PdbReaderContext pdbContext, Metadata metadata, DataReaderFactory pdbStream)
		{
			SymbolReader result;
			try
			{
				SymbolReader symbolReader = dnlib.DotNet.Pdb.SymbolReaderFactory.TryCreateEmbeddedPortablePdbReader(pdbContext, metadata);
				if (symbolReader != null)
				{
					if (pdbStream != null)
					{
						pdbStream.Dispose();
					}
					result = symbolReader;
				}
				else
				{
					result = dnlib.DotNet.Pdb.SymbolReaderFactory.CreateManagedCore(pdbContext, pdbStream);
				}
			}
			catch
			{
				if (pdbStream != null)
				{
					pdbStream.Dispose();
				}
				throw;
			}
			return result;
		}

		// Token: 0x06005A62 RID: 23138 RVA: 0x001B7960 File Offset: 0x001B7960
		private static SymbolReader CreateManagedCore(PdbReaderContext pdbContext, DataReaderFactory pdbStream)
		{
			if (pdbStream == null)
			{
				return null;
			}
			try
			{
				DataReader dataReader = pdbStream.CreateReader();
				if (dataReader.Length >= 4U)
				{
					if (dataReader.ReadUInt32() == 1112167234U)
					{
						return dnlib.DotNet.Pdb.Portable.SymbolReaderFactory.TryCreate(pdbContext, pdbStream, false);
					}
					return dnlib.DotNet.Pdb.Managed.SymbolReaderFactory.Create(pdbContext, pdbStream);
				}
			}
			catch (IOException)
			{
			}
			if (pdbStream != null)
			{
				pdbStream.Dispose();
			}
			return null;
		}

		// Token: 0x06005A63 RID: 23139 RVA: 0x001B79E0 File Offset: 0x001B79E0
		private static SymbolReader TryCreateEmbeddedPortablePdbReader(PdbReaderContext pdbContext, Metadata metadata)
		{
			return dnlib.DotNet.Pdb.Portable.SymbolReaderFactory.TryCreateEmbeddedPortablePdbReader(pdbContext, metadata);
		}

		// Token: 0x04002BB5 RID: 11189
		private static readonly char[] windowsPathSepChars = new char[]
		{
			'\\',
			'/'
		};
	}
}
