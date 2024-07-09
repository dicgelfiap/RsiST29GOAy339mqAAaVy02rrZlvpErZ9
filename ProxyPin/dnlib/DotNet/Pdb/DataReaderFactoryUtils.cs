using System;
using System.IO;
using System.Security;
using dnlib.IO;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x020008F1 RID: 2289
	internal static class DataReaderFactoryUtils
	{
		// Token: 0x06005903 RID: 22787 RVA: 0x001B5698 File Offset: 0x001B5698
		public static DataReaderFactory TryCreateDataReaderFactory(string filename)
		{
			try
			{
				if (!File.Exists(filename))
				{
					return null;
				}
				return ByteArrayDataReaderFactory.Create(File.ReadAllBytes(filename), filename);
			}
			catch (IOException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch (SecurityException)
			{
			}
			return null;
		}
	}
}
