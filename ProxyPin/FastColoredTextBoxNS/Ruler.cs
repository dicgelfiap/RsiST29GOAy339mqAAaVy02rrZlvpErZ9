using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A11 RID: 2577
	public class Ruler : UserControl
	{
		// Token: 0x170014DF RID: 5343
		// (get) Token: 0x0600633B RID: 25403 RVA: 0x001DAD00 File Offset: 0x001DAD00
		// (set) Token: 0x0600633C RID: 25404 RVA: 0x001DAD08 File Offset: 0x001DAD08
		[DefaultValue(typeof(Color), "ControlLight")]
		public Color BackColor2 { get; set; }

		// Token: 0x170014E0 RID: 5344
		// (get) Token: 0x0600633D RID: 25405 RVA: 0x001DAD14 File Offset: 0x001DAD14
		// (set) Token: 0x0600633E RID: 25406 RVA: 0x001DAD1C File Offset: 0x001DAD1C
		[DefaultValue(typeof(Color), "DarkGray")]
		public Color TickColor { get; set; }

		// Token: 0x170014E1 RID: 5345
		// (get) Token: 0x0600633F RID: 25407 RVA: 0x001DAD28 File Offset: 0x001DAD28
		// (set) Token: 0x06006340 RID: 25408 RVA: 0x001DAD30 File Offset: 0x001DAD30
		[DefaultValue(typeof(Color), "Black")]
		public Color CaretTickColor { get; set; }

		// Token: 0x170014E2 RID: 5346
		// (get) Token: 0x06006341 RID: 25409 RVA: 0x001DAD3C File Offset: 0x001DAD3C
		// (set) Token: 0x06006342 RID: 25410 RVA: 0x001DAD5C File Offset: 0x001DAD5C
		[Description("Target FastColoredTextBox")]
		public FastColoredTextBox Target
		{
			get
			{
				return this.target;
			}
			set
			{
				bool flag = this.target != null;
				if (flag)
				{
					this.UnSubscribe(this.target);
				}
				this.target = value;
				this.Subscribe(this.target);
				this.OnTargetChanged();
			}
		}

		// Token: 0x06006343 RID: 25411 RVA: 0x001DADA8 File Offset: 0x001DADA8
		public Ruler()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.MinimumSize = new Size(0, 24);
			this.MaximumSize = new Size(1073741823, 24);
			this.BackColor2 = SystemColors.ControlLight;
			this.TickColor = Color.DarkGray;
			this.CaretTickColor = Color.Black;
		}

		// Token: 0x06006344 RID: 25412 RVA: 0x001DAE24 File Offset: 0x001DAE24
		protected virtual void OnTargetChanged()
		{
			bool flag = this.TargetChanged != null;
			if (flag)
			{
				this.TargetChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06006345 RID: 25413 RVA: 0x001DAE58 File Offset: 0x001DAE58
		protected virtual void UnSubscribe(FastColoredTextBox target)
		{
			target.Scroll -= this.target_Scroll;
			target.SelectionChanged -= this.target_SelectionChanged;
			target.VisibleRangeChanged -= this.target_VisibleRangeChanged;
		}

		// Token: 0x06006346 RID: 25414 RVA: 0x001DAE98 File Offset: 0x001DAE98
		protected virtual void Subscribe(FastColoredTextBox target)
		{
			target.Scroll += this.target_Scroll;
			target.SelectionChanged += this.target_SelectionChanged;
			target.VisibleRangeChanged += this.target_VisibleRangeChanged;
		}

		// Token: 0x06006347 RID: 25415 RVA: 0x001DAED8 File Offset: 0x001DAED8
		private void target_VisibleRangeChanged(object sender, EventArgs e)
		{
			base.Invalidate();
		}

		// Token: 0x06006348 RID: 25416 RVA: 0x001DAEE4 File Offset: 0x001DAEE4
		private void target_SelectionChanged(object sender, EventArgs e)
		{
			base.Invalidate();
		}

		// Token: 0x06006349 RID: 25417 RVA: 0x001DAEF0 File Offset: 0x001DAEF0
		protected virtual void target_Scroll(object sender, ScrollEventArgs e)
		{
			base.Invalidate();
		}

		// Token: 0x0600634A RID: 25418 RVA: 0x001DAEFC File Offset: 0x001DAEFC
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			base.Invalidate();
		}

		// Token: 0x0600634B RID: 25419 RVA: 0x001DAF10 File Offset: 0x001DAF10
		protected override void OnPaint(PaintEventArgs e)
		{
			bool flag = this.target == null;
			if (!flag)
			{
				Point point = base.PointToClient(this.target.PointToScreen(this.target.PlaceToPoint(this.target.Selection.Start)));
				Size size = TextRenderer.MeasureText("W", this.Font);
				int num = 0;
				e.Graphics.FillRectangle(new LinearGradientBrush(new Rectangle(0, 0, base.Width, base.Height), this.BackColor, this.BackColor2, 270f), new Rectangle(0, 0, base.Width, base.Height));
				float num2 = (float)this.target.CharWidth;
				StringFormat stringFormat = new StringFormat();
				stringFormat.Alignment = StringAlignment.Center;
				stringFormat.LineAlignment = StringAlignment.Near;
				Point p = this.target.PositionToPoint(0);
				p = base.PointToClient(this.target.PointToScreen(p));
				using (Pen pen = new Pen(this.TickColor))
				{
					using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
					{
						float num3 = (float)p.X;
						while (num3 < (float)base.Right)
						{
							bool flag2 = num % 10 == 0;
							if (flag2)
							{
								e.Graphics.DrawString(num.ToString(), this.Font, solidBrush, num3, 0f, stringFormat);
							}
							e.Graphics.DrawLine(pen, (int)num3, size.Height + ((num % 5 == 0) ? 1 : 3), (int)num3, base.Height - 4);
							num3 += num2;
							num++;
						}
					}
				}
				using (Pen pen2 = new Pen(this.TickColor))
				{
					e.Graphics.DrawLine(pen2, new Point(point.X - 3, base.Height - 3), new Point(point.X + 3, base.Height - 3));
				}
				using (Pen pen3 = new Pen(this.CaretTickColor))
				{
					e.Graphics.DrawLine(pen3, new Point(point.X - 2, size.Height + 3), new Point(point.X - 2, base.Height - 4));
					e.Graphics.DrawLine(pen3, new Point(point.X, size.Height + 1), new Point(point.X, base.Height - 4));
					e.Graphics.DrawLine(pen3, new Point(point.X + 2, size.Height + 3), new Point(point.X + 2, base.Height - 4));
				}
			}
		}

		// Token: 0x0600634C RID: 25420 RVA: 0x001DB244 File Offset: 0x001DB244
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600634D RID: 25421 RVA: 0x001DB288 File Offset: 0x001DB288
		private void InitializeComponent()
		{
			this.components = new Container();
			base.AutoScaleMode = AutoScaleMode.Font;
		}

		// Token: 0x040032D5 RID: 13013
		public EventHandler TargetChanged;

		// Token: 0x040032D9 RID: 13017
		private FastColoredTextBox target;

		// Token: 0x040032DA RID: 13018
		private IContainer components = null;
	}
}
