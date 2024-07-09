using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace dnlib.PE
{
	// Token: 0x0200075D RID: 1885
	internal static class ProcessorArchUtils
	{
		// Token: 0x06004201 RID: 16897 RVA: 0x00164394 File Offset: 0x00164394
		public static Machine GetProcessCpuArchitecture()
		{
			if (ProcessorArchUtils.cachedMachine == Machine.Unknown)
			{
				ProcessorArchUtils.cachedMachine = ProcessorArchUtils.GetProcessCpuArchitectureCore();
			}
			return ProcessorArchUtils.cachedMachine;
		}

		// Token: 0x06004202 RID: 16898 RVA: 0x001643B0 File Offset: 0x001643B0
		private static Machine GetProcessCpuArchitectureCore()
		{
			Machine result;
			if (ProcessorArchUtils.WindowsUtils.TryGetProcessCpuArchitecture(out result))
			{
				return result;
			}
			try
			{
				if (ProcessorArchUtils.RuntimeInformationUtils.TryGet_RuntimeInformation_Architecture(out result))
				{
					return result;
				}
			}
			catch (PlatformNotSupportedException)
			{
			}
			if (IntPtr.Size != 4)
			{
				return Machine.AMD64;
			}
			return Machine.I386;
		}

		// Token: 0x0400236D RID: 9069
		private static Machine cachedMachine;

		// Token: 0x02000FC4 RID: 4036
		private static class RuntimeInformationUtils
		{
			// Token: 0x17001DB6 RID: 7606
			// (get) Token: 0x06008D9E RID: 36254 RVA: 0x002A6F44 File Offset: 0x002A6F44
			private static Assembly RuntimeInformationAssembly
			{
				get
				{
					return typeof(object).Assembly;
				}
			}

			// Token: 0x17001DB7 RID: 7607
			// (get) Token: 0x06008D9F RID: 36255 RVA: 0x002A6F58 File Offset: 0x002A6F58
			private static Type System_Runtime_InteropServices_RuntimeInformation
			{
				get
				{
					return ProcessorArchUtils.RuntimeInformationUtils.RuntimeInformationAssembly.GetType("System.Runtime.InteropServices.RuntimeInformation", false);
				}
			}

			// Token: 0x06008DA0 RID: 36256 RVA: 0x002A6F6C File Offset: 0x002A6F6C
			public static bool TryGet_RuntimeInformation_Architecture(out Machine machine)
			{
				machine = Machine.Unknown;
				Type system_Runtime_InteropServices_RuntimeInformation = ProcessorArchUtils.RuntimeInformationUtils.System_Runtime_InteropServices_RuntimeInformation;
				MethodInfo methodInfo = (system_Runtime_InteropServices_RuntimeInformation != null) ? system_Runtime_InteropServices_RuntimeInformation.GetMethod("get_ProcessArchitecture", Array2.Empty<Type>()) : null;
				return methodInfo != null && ProcessorArchUtils.RuntimeInformationUtils.TryGetArchitecture((int)methodInfo.Invoke(null, Array2.Empty<object>()), out machine);
			}

			// Token: 0x06008DA1 RID: 36257 RVA: 0x002A6FC4 File Offset: 0x002A6FC4
			private static bool TryGetArchitecture(int architecture, out Machine machine)
			{
				switch (architecture)
				{
				case 0:
					machine = Machine.I386;
					return true;
				case 1:
					machine = Machine.AMD64;
					return true;
				case 2:
					machine = Machine.ARMNT;
					return true;
				case 3:
					machine = Machine.ARM64;
					return true;
				default:
					machine = Machine.Unknown;
					return false;
				}
			}
		}

		// Token: 0x02000FC5 RID: 4037
		private static class WindowsUtils
		{
			// Token: 0x06008DA2 RID: 36258
			[DllImport("kernel32")]
			private static extern void GetSystemInfo(out ProcessorArchUtils.WindowsUtils.SYSTEM_INFO lpSystemInfo);

			// Token: 0x06008DA3 RID: 36259 RVA: 0x002A7018 File Offset: 0x002A7018
			public static bool TryGetProcessCpuArchitecture(out Machine machine)
			{
				if (ProcessorArchUtils.WindowsUtils.canTryGetSystemInfo)
				{
					try
					{
						ProcessorArchUtils.WindowsUtils.SYSTEM_INFO system_INFO;
						ProcessorArchUtils.WindowsUtils.GetSystemInfo(out system_INFO);
						ProcessorArchUtils.WindowsUtils.ProcessorArchitecture wProcessorArchitecture = (ProcessorArchUtils.WindowsUtils.ProcessorArchitecture)system_INFO.wProcessorArchitecture;
						if (wProcessorArchitecture <= ProcessorArchUtils.WindowsUtils.ProcessorArchitecture.AMD64)
						{
							if (wProcessorArchitecture == ProcessorArchUtils.WindowsUtils.ProcessorArchitecture.INTEL)
							{
								machine = Machine.I386;
								return true;
							}
							switch (wProcessorArchitecture)
							{
							case ProcessorArchUtils.WindowsUtils.ProcessorArchitecture.ARM:
								machine = Machine.ARMNT;
								return true;
							case ProcessorArchUtils.WindowsUtils.ProcessorArchitecture.IA64:
								machine = Machine.IA64;
								return true;
							case ProcessorArchUtils.WindowsUtils.ProcessorArchitecture.AMD64:
								machine = Machine.AMD64;
								return true;
							}
						}
						else
						{
							if (wProcessorArchitecture == ProcessorArchUtils.WindowsUtils.ProcessorArchitecture.ARM64)
							{
								machine = Machine.ARM64;
								return true;
							}
							if (wProcessorArchitecture != ProcessorArchUtils.WindowsUtils.ProcessorArchitecture.UNKNOWN)
							{
							}
						}
					}
					catch (EntryPointNotFoundException)
					{
						ProcessorArchUtils.WindowsUtils.canTryGetSystemInfo = false;
					}
					catch (DllNotFoundException)
					{
						ProcessorArchUtils.WindowsUtils.canTryGetSystemInfo = false;
					}
				}
				machine = Machine.Unknown;
				return false;
			}

			// Token: 0x040042FE RID: 17150
			private static bool canTryGetSystemInfo = true;

			// Token: 0x0200120B RID: 4619
			private struct SYSTEM_INFO
			{
				// Token: 0x04004EFF RID: 20223
				public ushort wProcessorArchitecture;

				// Token: 0x04004F00 RID: 20224
				public ushort wReserved;

				// Token: 0x04004F01 RID: 20225
				public uint dwPageSize;

				// Token: 0x04004F02 RID: 20226
				public IntPtr lpMinimumApplicationAddress;

				// Token: 0x04004F03 RID: 20227
				public IntPtr lpMaximumApplicationAddress;

				// Token: 0x04004F04 RID: 20228
				public IntPtr dwActiveProcessorMask;

				// Token: 0x04004F05 RID: 20229
				public uint dwNumberOfProcessors;

				// Token: 0x04004F06 RID: 20230
				public uint dwProcessorType;

				// Token: 0x04004F07 RID: 20231
				public uint dwAllocationGranularity;

				// Token: 0x04004F08 RID: 20232
				public ushort wProcessorLevel;

				// Token: 0x04004F09 RID: 20233
				public ushort wProcessorRevision;
			}

			// Token: 0x0200120C RID: 4620
			private enum ProcessorArchitecture : ushort
			{
				// Token: 0x04004F0B RID: 20235
				INTEL,
				// Token: 0x04004F0C RID: 20236
				ARM = 5,
				// Token: 0x04004F0D RID: 20237
				IA64,
				// Token: 0x04004F0E RID: 20238
				AMD64 = 9,
				// Token: 0x04004F0F RID: 20239
				ARM64 = 12,
				// Token: 0x04004F10 RID: 20240
				UNKNOWN = 65535
			}
		}
	}
}
