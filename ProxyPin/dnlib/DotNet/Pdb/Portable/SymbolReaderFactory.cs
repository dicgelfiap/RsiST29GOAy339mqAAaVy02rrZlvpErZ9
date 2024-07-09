using System;
using System.IO;
using System.IO.Compression;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb.Symbols;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Pdb.Portable
{
	// Token: 0x02000945 RID: 2373
	internal static class SymbolReaderFactory
	{
		// Token: 0x06005B3A RID: 23354 RVA: 0x001BD728 File Offset: 0x001BD728
		public static SymbolReader TryCreate(PdbReaderContext pdbContext, DataReaderFactory pdbStream, bool isEmbeddedPortablePdb)
		{
			bool flag = true;
			try
			{
				if (!pdbContext.HasDebugInfo)
				{
					return null;
				}
				if (pdbStream == null)
				{
					return null;
				}
				if (pdbStream.Length < 4U)
				{
					return null;
				}
				if (pdbStream.CreateReader().ReadUInt32() != 1112167234U)
				{
					return null;
				}
				ImageDebugDirectory codeViewDebugDirectory = pdbContext.CodeViewDebugDirectory;
				if (codeViewDebugDirectory == null)
				{
					return null;
				}
				Guid pdbGuid;
				uint age;
				if (!pdbContext.TryGetCodeViewData(out pdbGuid, out age))
				{
					return null;
				}
				PortablePdbReader portablePdbReader = new PortablePdbReader(pdbStream, isEmbeddedPortablePdb ? PdbFileKind.EmbeddedPortablePDB : PdbFileKind.PortablePDB);
				if (!portablePdbReader.MatchesModule(pdbGuid, codeViewDebugDirectory.TimeDateStamp, age))
				{
					return null;
				}
				flag = false;
				return portablePdbReader;
			}
			catch (IOException)
			{
			}
			finally
			{
				if (flag && pdbStream != null)
				{
					pdbStream.Dispose();
				}
			}
			return null;
		}

		// Token: 0x06005B3B RID: 23355 RVA: 0x001BD82C File Offset: 0x001BD82C
		public static SymbolReader TryCreateEmbeddedPortablePdbReader(PdbReaderContext pdbContext, Metadata metadata)
		{
			if (metadata == null)
			{
				return null;
			}
			try
			{
				if (!pdbContext.HasDebugInfo)
				{
					return null;
				}
				ImageDebugDirectory imageDebugDirectory = pdbContext.TryGetDebugDirectoryEntry(ImageDebugType.EmbeddedPortablePdb);
				if (imageDebugDirectory == null)
				{
					return null;
				}
				DataReader dataReader = pdbContext.CreateReader(imageDebugDirectory.AddressOfRawData, imageDebugDirectory.SizeOfData);
				if (dataReader.Length < 8U)
				{
					return null;
				}
				if (dataReader.ReadUInt32() != 1111773261U)
				{
					return null;
				}
				uint num = dataReader.ReadUInt32();
				if ((num & 2147483648U) > 0U)
				{
					return null;
				}
				byte[] array = new byte[num];
				using (DeflateStream deflateStream = new DeflateStream(dataReader.AsStream(), CompressionMode.Decompress))
				{
					int i;
					int num2;
					for (i = 0; i < array.Length; i += num2)
					{
						num2 = deflateStream.Read(array, i, array.Length - i);
						if (num2 == 0)
						{
							break;
						}
					}
					if (i != array.Length)
					{
						return null;
					}
					ByteArrayDataReaderFactory pdbStream = ByteArrayDataReaderFactory.Create(array, null);
					return SymbolReaderFactory.TryCreate(pdbContext, pdbStream, true);
				}
			}
			catch (IOException)
			{
			}
			return null;
		}
	}
}
