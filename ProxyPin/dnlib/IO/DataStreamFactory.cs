using System;
using System.Runtime.InteropServices;
using dnlib.PE;

namespace dnlib.IO
{
	// Token: 0x02000769 RID: 1897
	[ComVisible(true)]
	public static class DataStreamFactory
	{
		// Token: 0x06004295 RID: 17045 RVA: 0x00165BF8 File Offset: 0x00165BF8
		private static bool CalculateSupportsUnalignedAccesses()
		{
			Machine processCpuArchitecture = ProcessorArchUtils.GetProcessCpuArchitecture();
			if (processCpuArchitecture <= Machine.ARMNT)
			{
				if (processCpuArchitecture != Machine.I386)
				{
					if (processCpuArchitecture != Machine.ARMNT)
					{
						return true;
					}
					return false;
				}
			}
			else if (processCpuArchitecture != Machine.AMD64)
			{
				if (processCpuArchitecture != Machine.ARM64)
				{
					return true;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06004296 RID: 17046 RVA: 0x00165C58 File Offset: 0x00165C58
		public unsafe static DataStream Create(byte* data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (DataStreamFactory.supportsUnalignedAccesses)
			{
				return new UnalignedNativeMemoryDataStream(data);
			}
			return new AlignedNativeMemoryDataStream(data);
		}

		// Token: 0x06004297 RID: 17047 RVA: 0x00165C84 File Offset: 0x00165C84
		public static DataStream Create(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (DataStreamFactory.supportsUnalignedAccesses)
			{
				return new UnalignedByteArrayDataStream(data);
			}
			return new AlignedByteArrayDataStream(data);
		}

		// Token: 0x0400238B RID: 9099
		private static bool supportsUnalignedAccesses = DataStreamFactory.CalculateSupportsUnalignedAccesses();
	}
}
