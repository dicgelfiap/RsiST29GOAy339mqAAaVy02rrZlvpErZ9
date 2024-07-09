using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A00 RID: 2560
	public class DocumentMap : Control
	{
		// Token: 0x170014B1 RID: 5297
		// (get) Token: 0x06006276 RID: 25206 RVA: 0x001D5A14 File Offset: 0x001D5A14
		// (set) Token: 0x06006277 RID: 25207 RVA: 0x001D5A34 File Offset: 0x001D5A34
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
				bool flag2 = value != null;
				if (flag2)
				{
					this.Subscribe(this.target);
				}
				this.OnTargetChanged();
			}
		}

		// Token: 0x170014B2 RID: 5298
		// (get) Token: 0x06006278 RID: 25208 RVA: 0x001D5A8C File Offset: 0x001D5A8C
		// (set) Token: 0x06006279 RID: 25209 RVA: 0x001D5AAC File Offset: 0x001D5AAC
		[Description("Scale")]
		[DefaultValue(0.3f)]
		public new float Scale
		{
			get
			{
				return this.scale;
			}
			set
			{
				this.scale = value;
				this.NeedRepaint();
			}
		}

		// Token: 0x170014B3 RID: 5299
		// (get) Token: 0x0600627A RID: 25210 RVA: 0x001D5AC0 File Offset: 0x001D5AC0
		// (set) Token: 0x0600627B RID: 25211 RVA: 0x001D5AE0 File Offset: 0x001D5AE0
		[Description("Scrollbar visibility")]
		[DefaultValue(true)]
		public bool ScrollbarVisible
		{
			get
			{
				return this.scrollbarVisible;
			}
			set
			{
				this.scrollbarVisible = value;
				this.NeedRepaint();
			}
		}

		// Token: 0x0600627C RID: 25212 RVA: 0x001D5AF4 File Offset: 0x001D5AF4
		public DocumentMap()
		{
			this.ForeColor = Color.Maroon;
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			Application.Idle += this.Application_Idle;
		}

		// Token: 0x0600627D RID: 25213 RVA: 0x001D5B5C File Offset: 0x001D5B5C
		private void Application_Idle(object sender, EventArgs e)
		{
			bool flag = this.needRepaint;
			if (flag)
			{
				base.Invalidate();
			}
		}

		// Token: 0x0600627E RID: 25214 RVA: 0x001D5B84 File Offset: 0x001D5B84
		protected virtual void OnTargetChanged()
		{
			this.NeedRepaint();
			bool flag = this.TargetChanged != null;
			if (flag)
			{
				this.TargetChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600627F RID: 25215 RVA: 0x001D5BC0 File Offset: 0x001D5BC0
		protected virtual void UnSubscribe(FastColoredTextBox target)
		{
			target.Scroll -= this.Target_Scroll;
			target.SelectionChangedDelayed -= this.Target_SelectionChanged;
			target.VisibleRangeChanged -= this.Target_VisibleRangeChanged;
		}

		// Token: 0x06006280 RID: 25216 RVA: 0x001D5C00 File Offset: 0x001D5C00
		protected virtual void Subscribe(FastColoredTextBox target)
		{
			target.Scroll += this.Target_Scroll;
			target.SelectionChangedDelayed += this.Target_SelectionChanged;
			target.VisibleRangeChanged += this.Target_VisibleRangeChanged;
		}

		// Token: 0x06006281 RID: 25217 RVA: 0x001D5C40 File Offset: 0x001D5C40
		protected virtual void Target_VisibleRangeChanged(object sender, EventArgs e)
		{
			this.NeedRepaint();
		}

		// Token: 0x06006282 RID: 25218 RVA: 0x001D5C4C File Offset: 0x001D5C4C
		protected virtual void Target_SelectionChanged(object sender, EventArgs e)
		{
			this.NeedRepaint();
		}

		// Token: 0x06006283 RID: 25219 RVA: 0x001D5C58 File Offset: 0x001D5C58
		protected virtual void Target_Scroll(object sender, ScrollEventArgs e)
		{
			this.NeedRepaint();
		}

		// Token: 0x06006284 RID: 25220 RVA: 0x001D5C64 File Offset: 0x001D5C64
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.NeedRepaint();
		}

		// Token: 0x06006285 RID: 25221 RVA: 0x001D5C78 File Offset: 0x001D5C78
		public void NeedRepaint()
		{
			this.needRepaint = true;
		}

		// Token: 0x06006286 RID: 25222 RVA: 0x001D5C84 File Offset: 0x001D5C84
		protected override void OnPaint(PaintEventArgs e)
		{
			bool flag = this.target == null;
			if (!flag)
			{
				float num = this.Scale * 100f / (float)this.target.Zoom;
				bool flag2 = num <= float.Epsilon;
				if (!flag2)
				{
					Range visibleRange = this.target.VisibleRange;
					bool flag3 = this.startPlace.iLine > visibleRange.Start.iLine;
					if (flag3)
					{
						this.startPlace.iLine = visibleRange.Start.iLine;
					}
					else
					{
						Point point = this.target.PlaceToPoint(visibleRange.End);
						point.Offset(0, -(int)((float)base.ClientSize.Height / num) + this.target.CharHeight);
						Place place = this.target.PointToPlace(point);
						bool flag4 = place.iLine > this.startPlace.iLine;
						if (flag4)
						{
							this.startPlace.iLine = place.iLine;
						}
					}
					this.startPlace.iChar = 0;
					int count = this.target.Lines.Count;
					float num2 = (float)visibleRange.Start.iLine / (float)count;
					float num3 = (float)visibleRange.End.iLine / (float)count;
					e.Graphics.ScaleTransform(num, num);
					SizeF sizeF = new SizeF((float)base.ClientSize.Width / num, (float)base.ClientSize.Height / num);
					this.target.DrawText(e.Graphics, this.startPlace, sizeF.ToSize());
					Point point2 = this.target.PlaceToPoint(this.startPlace);
					Point point3 = this.target.PlaceToPoint(visibleRange.Start);
					Point point4 = this.target.PlaceToPoint(visibleRange.End);
					int num4 = point3.Y - point2.Y;
					int num5 = point4.Y + this.target.CharHeight - point2.Y;
					e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
					using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(50, this.ForeColor)))
					{
						using (Pen pen = new Pen(solidBrush, 1f / num))
						{
							Rectangle rect = new Rectangle(0, num4, (int)((float)(base.ClientSize.Width - 1) / num), num5 - num4);
							e.Graphics.FillRectangle(solidBrush, rect);
							e.Graphics.DrawRectangle(pen, rect);
						}
					}
					bool flag5 = this.scrollbarVisible;
					if (flag5)
					{
						e.Graphics.ResetTransform();
						e.Graphics.SmoothingMode = SmoothingMode.None;
						using (SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(200, this.ForeColor)))
						{
							RectangleF rect2 = new RectangleF((float)(base.ClientSize.Width - 3), (float)base.ClientSize.Height * num2, 2f, (float)base.ClientSize.Height * (num3 - num2));
							e.Graphics.FillRectangle(solidBrush2, rect2);
						}
					}
					this.needRepaint = false;
				}
			}
		}

		// Token: 0x06006287 RID: 25223 RVA: 0x001D601C File Offset: 0x001D601C
		protected override void OnMouseDown(MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				this.Scroll(e.Location);
			}
			base.OnMouseDown(e);
		}

		// Token: 0x06006288 RID: 25224 RVA: 0x001D6058 File Offset: 0x001D6058
		protected override void OnMouseMove(MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				this.Scroll(e.Location);
			}
			base.OnMouseMove(e);
		}

		// Token: 0x06006289 RID: 25225 RVA: 0x001D6094 File Offset: 0x001D6094
		private void Scroll(Point point)
		{
			bool flag = this.target == null;
			if (!flag)
			{
				float num = this.Scale * 100f / (float)this.target.Zoom;
				bool flag2 = num <= float.Epsilon;
				if (!flag2)
				{
					Point point2 = this.target.PlaceToPoint(this.startPlace);
					point2 = new Point(0, point2.Y + (int)((float)point.Y / num));
					Place place = this.target.PointToPlace(point2);
					this.target.DoRangeVisible(new Range(this.target, place, place), true);
					base.BeginInvoke(new MethodInvoker(this.OnScroll));
				}
			}
		}

		// Token: 0x0600628A RID: 25226 RVA: 0x001D6158 File Offset: 0x001D6158
		private void OnScroll()
		{
			this.Refresh();
			this.target.Refresh();
		}

		// Token: 0x0600628B RID: 25227 RVA: 0x001D6170 File Offset: 0x001D6170
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Application.Idle -= this.Application_Idle;
				bool flag = this.target != null;
				if (flag)
				{
					this.UnSubscribe(this.target);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x04003239 RID: 12857
		public EventHandler TargetChanged;

		// Token: 0x0400323A RID: 12858
		private FastColoredTextBox target;

		// Token: 0x0400323B RID: 12859
		private float scale = 0.3f;

		// Token: 0x0400323C RID: 12860
		private bool needRepaint = true;

		// Token: 0x0400323D RID: 12861
		private Place startPlace = Place.Empty;

		// Token: 0x0400323E RID: 12862
		private bool scrollbarVisible = true;
	}
}
