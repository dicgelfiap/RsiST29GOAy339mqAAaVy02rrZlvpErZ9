using System;
using System.IO;

namespace dnlib.IO
{
	// Token: 0x02000766 RID: 1894
	internal static class DataReaderFactoryFactory
	{
		// Token: 0x06004277 RID: 17015 RVA: 0x00165924 File Offset: 0x00165924
		static DataReaderFactoryFactory()
		{
			int platform = (int)Environment.OSVersion.Platform;
			if (platform == 4 || platform == 6 || platform == 128)
			{
				DataReaderFactoryFactory.isUnix = true;
			}
		}

		// Token: 0x06004278 RID: 17016 RVA: 0x00165960 File Offset: 0x00165960
		public static DataReaderFactory Create(string fileName, bool mapAsImage)
		{
			DataReaderFactory dataReaderFactory = DataReaderFactoryFactory.CreateDataReaderFactory(fileName, mapAsImage);
			if (dataReaderFactory != null)
			{
				return dataReaderFactory;
			}
			return ByteArrayDataReaderFactory.Create(File.ReadAllBytes(fileName), fileName);
		}

		// Token: 0x06004279 RID: 17017 RVA: 0x00165990 File Offset: 0x00165990
		private static DataReaderFactory CreateDataReaderFactory(string fileName, bool mapAsImage)
		{
			if (!DataReaderFactoryFactory.isUnix)
			{
				return MemoryMappedDataReaderFactory.CreateWindows(fileName, mapAsImage);
			}
			return MemoryMappedDataReaderFactory.CreateUnix(fileName, mapAsImage);
		}

		// Token: 0x04002388 RID: 9096
		private static readonly bool isUnix;
	}
}
