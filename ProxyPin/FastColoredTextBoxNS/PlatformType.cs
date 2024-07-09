using System;
using System.Runtime.InteropServices;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A0C RID: 2572
	public static class PlatformType
	{
		// Token: 0x06006331 RID: 25393
		[DllImport("kernel32.dll")]
		private static extern void GetNativeSystemInfo(ref PlatformType.SYSTEM_INFO lpSystemInfo);

		// Token: 0x06006332 RID: 25394
		[DllImport("kernel32.dll")]
		private static extern void GetSystemInfo(ref PlatformType.SYSTEM_INFO lpSystemInfo);

		// Token: 0x06006333 RID: 25395 RVA: 0x001DA608 File Offset: 0x001DA608
		public static Platform GetOperationSystemPlatform()
		{
			PlatformType.SYSTEM_INFO system_INFO = default(PlatformType.SYSTEM_INFO);
			bool flag = Environment.OSVersion.Version.Major > 5 || (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1);
			if (flag)
			{
				PlatformType.GetNativeSystemInfo(ref system_INFO);
			}
			else
			{
				PlatformType.GetSystemInfo(ref system_INFO);
			}
			ushort wProcessorArchitecture = system_INFO.wProcessorArchitecture;
			Platform result;
			if (wProcessorArchitecture != 0)
			{
				if (wProcessorArchitecture != 6 && wProcessorArchitecture != 9)
				{
					result = Platform.Unknown;
				}
				else
				{
					result = Platform.X64;
				}
			}
			else
			{
				result = Platform.X86;
			}
			return result;
		}

		// Token: 0x04003275 RID: 12917
		private const ushort PROCESSOR_ARCHITECTURE_INTEL = 0;

		// Token: 0x04003276 RID: 12918
		private const ushort PROCESSOR_ARCHITECTURE_IA64 = 6;

		// Token: 0x04003277 RID: 12919
		private const ushort PROCESSOR_ARCHITECTURE_AMD64 = 9;

		// Token: 0x04003278 RID: 12920
		private const ushort PROCESSOR_ARCHITECTURE_UNKNOWN = 65535;

		// Token: 0x02001050 RID: 4176
		private struct SYSTEM_INFO
		{
			// Token: 0x0400454E RID: 17742
			public ushort wProcessorArchitecture;

			// Token: 0x0400454F RID: 17743
			public ushort wReserved;

			// Token: 0x04004550 RID: 17744
			public uint dwPageSize;

			// Token: 0x04004551 RID: 17745
			public IntPtr lpMinimumApplicationAddress;

			// Token: 0x04004552 RID: 17746
			public IntPtr lpMaximumApplicationAddress;

			// Token: 0x04004553 RID: 17747
			public UIntPtr dwActiveProcessorMask;

			// Token: 0x04004554 RID: 17748
			public uint dwNumberOfProcessors;

			// Token: 0x04004555 RID: 17749
			public uint dwProcessorType;

			// Token: 0x04004556 RID: 17750
			public uint dwAllocationGranularity;

			// Token: 0x04004557 RID: 17751
			public ushort wProcessorLevel;

			// Token: 0x04004558 RID: 17752
			public ushort wProcessorRevision;
		}
	}
}
