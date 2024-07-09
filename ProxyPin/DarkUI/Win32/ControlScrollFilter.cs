using System;
using System.Drawing;
using System.Windows.Forms;

namespace DarkUI.Win32
{
	// Token: 0x02000078 RID: 120
	public class ControlScrollFilter : IMessageFilter
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x00030B44 File Offset: 0x00030B44
		public bool PreFilterMessage(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 522 && msg != 526)
			{
				return false;
			}
			IntPtr intPtr = Native.WindowFromPoint(new Point((int)m.LParam));
			if (intPtr == m.HWnd)
			{
				return false;
			}
			Native.SendMessage(intPtr, (uint)m.Msg, m.WParam, m.LParam);
			return true;
		}
	}
}
