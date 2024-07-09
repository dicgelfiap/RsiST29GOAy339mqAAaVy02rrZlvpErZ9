using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A41 RID: 2625
	public abstract class Style : IDisposable
	{
		// Token: 0x170015C9 RID: 5577
		// (get) Token: 0x0600672D RID: 26413 RVA: 0x001F72D0 File Offset: 0x001F72D0
		// (set) Token: 0x0600672E RID: 26414 RVA: 0x001F72D8 File Offset: 0x001F72D8
		public virtual bool IsExportable { get; set; }

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x0600672F RID: 26415 RVA: 0x001F72E4 File Offset: 0x001F72E4
		// (remove) Token: 0x06006730 RID: 26416 RVA: 0x001F7320 File Offset: 0x001F7320
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<VisualMarkerEventArgs> VisualMarkerClick;

		// Token: 0x06006731 RID: 26417 RVA: 0x001F735C File Offset: 0x001F735C
		public Style()
		{
			this.IsExportable = true;
		}

		// Token: 0x06006732 RID: 26418
		public abstract void Draw(Graphics gr, Point position, Range range);

		// Token: 0x06006733 RID: 26419 RVA: 0x001F7370 File Offset: 0x001F7370
		public virtual void OnVisualMarkerClick(FastColoredTextBox tb, VisualMarkerEventArgs args)
		{
			bool flag = this.VisualMarkerClick != null;
			if (flag)
			{
				this.VisualMarkerClick(tb, args);
			}
		}

		// Token: 0x06006734 RID: 26420 RVA: 0x001F73A0 File Offset: 0x001F73A0
		protected virtual void AddVisualMarker(FastColoredTextBox tb, StyleVisualMarker marker)
		{
			tb.AddVisualMarker(marker);
		}

		// Token: 0x06006735 RID: 26421 RVA: 0x001F73AC File Offset: 0x001F73AC
		public static Size GetSizeOfRange(Range range)
		{
			return new Size((range.End.iChar - range.Start.iChar) * range.tb.CharWidth, range.tb.CharHeight);
		}

		// Token: 0x06006736 RID: 26422 RVA: 0x001F73F8 File Offset: 0x001F73F8
		public static GraphicsPath GetRoundedRectangle(Rectangle rect, int d)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddArc(rect.X, rect.Y, d, d, 180f, 90f);
			graphicsPath.AddArc(rect.X + rect.Width - d, rect.Y, d, d, 270f, 90f);
			graphicsPath.AddArc(rect.X + rect.Width - d, rect.Y + rect.Height - d, d, d, 0f, 90f);
			graphicsPath.AddArc(rect.X, rect.Y + rect.Height - d, d, d, 90f, 90f);
			graphicsPath.AddLine(rect.X, rect.Y + rect.Height - d, rect.X, rect.Y + d / 2);
			return graphicsPath;
		}

		// Token: 0x06006737 RID: 26423 RVA: 0x001F74F8 File Offset: 0x001F74F8
		public virtual void Dispose()
		{
		}

		// Token: 0x06006738 RID: 26424 RVA: 0x001F74FC File Offset: 0x001F74FC
		public virtual string GetCSS()
		{
			return "";
		}

		// Token: 0x06006739 RID: 26425 RVA: 0x001F751C File Offset: 0x001F751C
		public virtual RTFStyleDescriptor GetRTF()
		{
			return new RTFStyleDescriptor();
		}
	}
}
