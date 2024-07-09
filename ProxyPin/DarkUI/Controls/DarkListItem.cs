using System;
using System.Drawing;
using DarkUI.Config;

namespace DarkUI.Controls
{
	// Token: 0x020000A7 RID: 167
	public class DarkListItem
	{
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x0600069D RID: 1693 RVA: 0x0003B8D0 File Offset: 0x0003B8D0
		// (remove) Token: 0x0600069E RID: 1694 RVA: 0x0003B90C File Offset: 0x0003B90C
		public event EventHandler TextChanged;

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x0003B948 File Offset: 0x0003B948
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x0003B950 File Offset: 0x0003B950
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
				if (this.TextChanged != null)
				{
					this.TextChanged(this, new EventArgs());
				}
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0003B978 File Offset: 0x0003B978
		// (set) Token: 0x060006A2 RID: 1698 RVA: 0x0003B980 File Offset: 0x0003B980
		public Rectangle Area { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0003B98C File Offset: 0x0003B98C
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x0003B994 File Offset: 0x0003B994
		public Color TextColor { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0003B9A0 File Offset: 0x0003B9A0
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x0003B9A8 File Offset: 0x0003B9A8
		public FontStyle FontStyle { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0003B9B4 File Offset: 0x0003B9B4
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x0003B9BC File Offset: 0x0003B9BC
		public Bitmap Icon { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0003B9C8 File Offset: 0x0003B9C8
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x0003B9D0 File Offset: 0x0003B9D0
		public object Tag { get; set; }

		// Token: 0x060006AB RID: 1707 RVA: 0x0003B9DC File Offset: 0x0003B9DC
		public DarkListItem()
		{
			this.TextColor = Colors.LightText;
			this.FontStyle = FontStyle.Regular;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0003B9F8 File Offset: 0x0003B9F8
		public DarkListItem(string text) : this()
		{
			this.Text = text;
		}

		// Token: 0x040004FE RID: 1278
		private string _text;
	}
}
