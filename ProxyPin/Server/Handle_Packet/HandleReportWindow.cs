using System;
using System.Drawing;
using Server.Connection;
using Server.Properties;

namespace Server.Handle_Packet
{
	// Token: 0x02000048 RID: 72
	public class HandleReportWindow
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x00016B14 File Offset: 0x00016B14
		public HandleReportWindow(Clients client, string title)
		{
			new HandleLogs().Addmsg(string.Concat(new string[]
			{
				"Client ",
				client.Ip,
				" opened [",
				title,
				"]"
			}), Color.Blue);
			if (Settings.Default.Notification)
			{
				Program.form1.notifyIcon1.BalloonTipText = string.Concat(new string[]
				{
					"Client ",
					client.Ip,
					" opened [",
					title,
					"]"
				});
				Program.form1.notifyIcon1.ShowBalloonTip(100);
			}
		}
	}
}
