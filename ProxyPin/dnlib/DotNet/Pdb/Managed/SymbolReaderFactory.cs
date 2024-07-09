using System;
using System.IO;
using dnlib.DotNet.Pdb.Symbols;
using dnlib.IO;

namespace dnlib.DotNet.Pdb.Managed
{
	// Token: 0x02000955 RID: 2389
	internal static class SymbolReaderFactory
	{
		// Token: 0x06005BC6 RID: 23494 RVA: 0x001BFCCC File Offset: 0x001BFCCC
		public static SymbolReader Create(PdbReaderContext pdbContext, DataReaderFactory pdbStream)
		{
			if (pdbStream == null)
			{
				return null;
			}
			try
			{
				if (pdbContext.CodeViewDebugDirectory == null)
				{
					return null;
				}
				Guid expectedGuid;
				uint expectedAge;
				if (!pdbContext.TryGetCodeViewData(out expectedGuid, out expectedAge))
				{
					return null;
				}
				PdbReader pdbReader = new PdbReader(expectedGuid, expectedAge);
				pdbReader.Read(pdbStream.CreateReader());
				if (pdbReader.MatchesModule)
				{
					return pdbReader;
				}
				return null;
			}
			catch (PdbException)
			{
			}
			catch (IOException)
			{
			}
			finally
			{
				if (pdbStream != null)
				{
					pdbStream.Dispose();
				}
			}
			return null;
		}
	}
}
