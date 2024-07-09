using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace DarkUI.Win32
{
	// Token: 0x0200007B RID: 123
	internal sealed class Native
	{
		// Token: 0x060004AC RID: 1196
		[DllImport("user32.dll")]
		internal static extern IntPtr WindowFromPoint(Point point);

		// Token: 0x060004AD RID: 1197
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		internal static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
	}
}
