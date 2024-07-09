using System;
using System.Runtime.InteropServices;

namespace Toolbelt.Drawing.Win32
{
	// Token: 0x020000C4 RID: 196
	internal class Kernel32
	{
		// Token: 0x060007C2 RID: 1986
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LOAD_LIBRARY dwFlags);

		// Token: 0x060007C3 RID: 1987
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool FreeLibrary(IntPtr hModule);

		// Token: 0x060007C4 RID: 1988
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool EnumResourceNames(IntPtr hModule, RT type, EnumResNameProcDelegate lpEnumFunc, IntPtr lParam);

		// Token: 0x060007C5 RID: 1989
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr FindResource(IntPtr hModule, IntPtr lpszName, RT type);

		// Token: 0x060007C6 RID: 1990
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr LoadResource(IntPtr hModule, IntPtr hResInfo);

		// Token: 0x060007C7 RID: 1991
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr LockResource(IntPtr hResource);

		// Token: 0x060007C8 RID: 1992
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint SizeofResource(IntPtr hModule, IntPtr hResInfo);
	}
}
