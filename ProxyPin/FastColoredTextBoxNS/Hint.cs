using System;
using System.Drawing;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A07 RID: 2567
	public class Hint
	{
		// Token: 0x170014C3 RID: 5315
		// (get) Token: 0x060062D2 RID: 25298 RVA: 0x001D89E4 File Offset: 0x001D89E4
		// (set) Token: 0x060062D3 RID: 25299 RVA: 0x001D8A08 File Offset: 0x001D8A08
		public string Text
		{
			get
			{
				return this.HostPanel.Text;
			}
			set
			{
				this.HostPanel.Text = value;
			}
		}

		// Token: 0x170014C4 RID: 5316
		// (get) Token: 0x060062D4 RID: 25300 RVA: 0x001D8A18 File Offset: 0x001D8A18
		// (set) Token: 0x060062D5 RID: 25301 RVA: 0x001D8A20 File Offset: 0x001D8A20
		public Range Range { get; set; }

		// Token: 0x170014C5 RID: 5317
		// (get) Token: 0x060062D6 RID: 25302 RVA: 0x001D8A2C File Offset: 0x001D8A2C
		// (set) Token: 0x060062D7 RID: 25303 RVA: 0x001D8A50 File Offset: 0x001D8A50
		public Color BackColor
		{
			get
			{
				return this.HostPanel.BackColor;
			}
			set
			{
				this.HostPanel.BackColor = value;
			}
		}

		// Token: 0x170014C6 RID: 5318
		// (get) Token: 0x060062D8 RID: 25304 RVA: 0x001D8A60 File Offset: 0x001D8A60
		// (set) Token: 0x060062D9 RID: 25305 RVA: 0x001D8A84 File Offset: 0x001D8A84
		public Color BackColor2
		{
			get
			{
				return this.HostPanel.BackColor2;
			}
			set
			{
				this.HostPanel.BackColor2 = value;
			}
		}

		// Token: 0x170014C7 RID: 5319
		// (get) Token: 0x060062DA RID: 25306 RVA: 0x001D8A94 File Offset: 0x001D8A94
		// (set) Token: 0x060062DB RID: 25307 RVA: 0x001D8AB8 File Offset: 0x001D8AB8
		public Color BorderColor
		{
			get
			{
				return this.HostPanel.BorderColor;
			}
			set
			{
				this.HostPanel.BorderColor = value;
			}
		}

		// Token: 0x170014C8 RID: 5320
		// (get) Token: 0x060062DC RID: 25308 RVA: 0x001D8AC8 File Offset: 0x001D8AC8
		// (set) Token: 0x060062DD RID: 25309 RVA: 0x001D8AEC File Offset: 0x001D8AEC
		public Color ForeColor
		{
			get
			{
				return this.HostPanel.ForeColor;
			}
			set
			{
				this.HostPanel.ForeColor = value;
			}
		}

		// Token: 0x170014C9 RID: 5321
		// (get) Token: 0x060062DE RID: 25310 RVA: 0x001D8AFC File Offset: 0x001D8AFC
		// (set) Token: 0x060062DF RID: 25311 RVA: 0x001D8B20 File Offset: 0x001D8B20
		public StringAlignment TextAlignment
		{
			get
			{
				return this.HostPanel.TextAlignment;
			}
			set
			{
				this.HostPanel.TextAlignment = value;
			}
		}

		// Token: 0x170014CA RID: 5322
		// (get) Token: 0x060062E0 RID: 25312 RVA: 0x001D8B30 File Offset: 0x001D8B30
		// (set) Token: 0x060062E1 RID: 25313 RVA: 0x001D8B54 File Offset: 0x001D8B54
		public Font Font
		{
			get
			{
				return this.HostPanel.Font;
			}
			set
			{
				this.HostPanel.Font = value;
			}
		}

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x060062E2 RID: 25314 RVA: 0x001D8B64 File Offset: 0x001D8B64
		// (remove) Token: 0x060062E3 RID: 25315 RVA: 0x001D8B74 File Offset: 0x001D8B74
		public event EventHandler Click
		{
			add
			{
				this.HostPanel.Click += value;
			}
			remove
			{
				this.HostPanel.Click -= value;
			}
		}

		// Token: 0x170014CB RID: 5323
		// (get) Token: 0x060062E4 RID: 25316 RVA: 0x001D8B84 File Offset: 0x001D8B84
		// (set) Token: 0x060062E5 RID: 25317 RVA: 0x001D8B8C File Offset: 0x001D8B8C
		public Control InnerControl { get; set; }

		// Token: 0x170014CC RID: 5324
		// (get) Token: 0x060062E6 RID: 25318 RVA: 0x001D8B98 File Offset: 0x001D8B98
		// (set) Token: 0x060062E7 RID: 25319 RVA: 0x001D8BA0 File Offset: 0x001D8BA0
		public DockStyle Dock { get; set; }

		// Token: 0x170014CD RID: 5325
		// (get) Token: 0x060062E8 RID: 25320 RVA: 0x001D8BAC File Offset: 0x001D8BAC
		// (set) Token: 0x060062E9 RID: 25321 RVA: 0x001D8BD0 File Offset: 0x001D8BD0
		public int Width
		{
			get
			{
				return this.HostPanel.Width;
			}
			set
			{
				this.HostPanel.Width = value;
			}
		}

		// Token: 0x170014CE RID: 5326
		// (get) Token: 0x060062EA RID: 25322 RVA: 0x001D8BE0 File Offset: 0x001D8BE0
		// (set) Token: 0x060062EB RID: 25323 RVA: 0x001D8C04 File Offset: 0x001D8C04
		public int Height
		{
			get
			{
				return this.HostPanel.Height;
			}
			set
			{
				this.HostPanel.Height = value;
			}
		}

		// Token: 0x170014CF RID: 5327
		// (get) Token: 0x060062EC RID: 25324 RVA: 0x001D8C14 File Offset: 0x001D8C14
		// (set) Token: 0x060062ED RID: 25325 RVA: 0x001D8C1C File Offset: 0x001D8C1C
		public UnfocusablePanel HostPanel { get; private set; }

		// Token: 0x170014D0 RID: 5328
		// (get) Token: 0x060062EE RID: 25326 RVA: 0x001D8C28 File Offset: 0x001D8C28
		// (set) Token: 0x060062EF RID: 25327 RVA: 0x001D8C30 File Offset: 0x001D8C30
		internal int TopPadding { get; set; }

		// Token: 0x170014D1 RID: 5329
		// (get) Token: 0x060062F0 RID: 25328 RVA: 0x001D8C3C File Offset: 0x001D8C3C
		// (set) Token: 0x060062F1 RID: 25329 RVA: 0x001D8C44 File Offset: 0x001D8C44
		public object Tag { get; set; }

		// Token: 0x170014D2 RID: 5330
		// (get) Token: 0x060062F2 RID: 25330 RVA: 0x001D8C50 File Offset: 0x001D8C50
		// (set) Token: 0x060062F3 RID: 25331 RVA: 0x001D8C74 File Offset: 0x001D8C74
		public Cursor Cursor
		{
			get
			{
				return this.HostPanel.Cursor;
			}
			set
			{
				this.HostPanel.Cursor = value;
			}
		}

		// Token: 0x170014D3 RID: 5331
		// (get) Token: 0x060062F4 RID: 25332 RVA: 0x001D8C84 File Offset: 0x001D8C84
		// (set) Token: 0x060062F5 RID: 25333 RVA: 0x001D8C8C File Offset: 0x001D8C8C
		public bool Inline { get; set; }

		// Token: 0x060062F6 RID: 25334 RVA: 0x001D8C98 File Offset: 0x001D8C98
		public virtual void DoVisible()
		{
			this.Range.tb.DoRangeVisible(this.Range, true);
			this.Range.tb.DoVisibleRectangle(this.HostPanel.Bounds);
			this.Range.tb.Invalidate();
		}

		// Token: 0x060062F7 RID: 25335 RVA: 0x001D8CF0 File Offset: 0x001D8CF0
		private Hint(Range range, Control innerControl, string text, bool inline, bool dock)
		{
			this.Range = range;
			this.Inline = inline;
			this.InnerControl = innerControl;
			this.Init();
			this.Dock = (dock ? DockStyle.Fill : DockStyle.None);
			this.Text = text;
		}

		// Token: 0x060062F8 RID: 25336 RVA: 0x001D8D48 File Offset: 0x001D8D48
		public Hint(Range range, string text, bool inline, bool dock) : this(range, null, text, inline, dock)
		{
		}

		// Token: 0x060062F9 RID: 25337 RVA: 0x001D8D58 File Offset: 0x001D8D58
		public Hint(Range range, string text) : this(range, null, text, true, true)
		{
		}

		// Token: 0x060062FA RID: 25338 RVA: 0x001D8D68 File Offset: 0x001D8D68
		public Hint(Range range, Control innerControl, bool inline, bool dock) : this(range, innerControl, null, inline, dock)
		{
		}

		// Token: 0x060062FB RID: 25339 RVA: 0x001D8D78 File Offset: 0x001D8D78
		public Hint(Range range, Control innerControl) : this(range, innerControl, null, true, true)
		{
		}

		// Token: 0x060062FC RID: 25340 RVA: 0x001D8D88 File Offset: 0x001D8D88
		protected virtual void Init()
		{
			this.HostPanel = new UnfocusablePanel();
			this.HostPanel.Click += this.OnClick;
			this.Cursor = Cursors.Default;
			this.BorderColor = Color.Silver;
			this.BackColor2 = Color.White;
			this.BackColor = ((this.InnerControl == null) ? Color.Silver : SystemColors.Control);
			this.ForeColor = Color.Black;
			this.TextAlignment = StringAlignment.Near;
			this.Font = ((this.Range.tb.Parent == null) ? this.Range.tb.Font : this.Range.tb.Parent.Font);
			bool flag = this.InnerControl != null;
			if (flag)
			{
				this.HostPanel.Controls.Add(this.InnerControl);
				Size preferredSize = this.InnerControl.GetPreferredSize(this.InnerControl.Size);
				this.HostPanel.Width = preferredSize.Width + 2;
				this.HostPanel.Height = preferredSize.Height + 2;
				this.InnerControl.Dock = DockStyle.Fill;
				this.InnerControl.Visible = true;
				this.BackColor = SystemColors.Control;
			}
			else
			{
				this.HostPanel.Height = this.Range.tb.CharHeight + 5;
			}
		}

		// Token: 0x060062FD RID: 25341 RVA: 0x001D8F14 File Offset: 0x001D8F14
		protected virtual void OnClick(object sender, EventArgs e)
		{
			this.Range.tb.OnHintClick(this);
		}
	}
}
