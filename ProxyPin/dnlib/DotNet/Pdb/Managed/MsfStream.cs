using System;
using dnlib.IO;

namespace dnlib.DotNet.Pdb.Managed
{
	// Token: 0x0200094F RID: 2383
	internal sealed class MsfStream
	{
		// Token: 0x06005B96 RID: 23446 RVA: 0x001BEC18 File Offset: 0x001BEC18
		public MsfStream(DataReader[] pages, uint length)
		{
			byte[] array = new byte[length];
			int num = 0;
			foreach (DataReader dataReader in pages)
			{
				dataReader.Position = 0U;
				int num2 = Math.Min((int)dataReader.Length, (int)((ulong)length - (ulong)((long)num)));
				dataReader.ReadBytes(array, num, num2);
				num += num2;
			}
			this.Content = ByteArrayDataReaderFactory.CreateReader(array);
		}

		// Token: 0x04002C5A RID: 11354
		public DataReader Content;
	}
}
