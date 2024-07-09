using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A06 RID: 2566
	public class Hints : ICollection<Hint>, IEnumerable<Hint>, IEnumerable, IDisposable
	{
		// Token: 0x060062C3 RID: 25283 RVA: 0x001D80DC File Offset: 0x001D80DC
		public Hints(FastColoredTextBox tb)
		{
			this.tb = tb;
			tb.TextChanged += this.OnTextBoxTextChanged;
			tb.KeyDown += this.OnTextBoxKeyDown;
			tb.VisibleRangeChanged += this.OnTextBoxVisibleRangeChanged;
		}

		// Token: 0x060062C4 RID: 25284 RVA: 0x001D8144 File Offset: 0x001D8144
		protected virtual void OnTextBoxKeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.KeyCode == Keys.Escape && e.Modifiers == Keys.None;
			if (flag)
			{
				this.Clear();
			}
		}

		// Token: 0x060062C5 RID: 25285 RVA: 0x001D8180 File Offset: 0x001D8180
		protected virtual void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
		{
			this.Clear();
		}

		// Token: 0x060062C6 RID: 25286 RVA: 0x001D818C File Offset: 0x001D818C
		public void Dispose()
		{
			this.tb.TextChanged -= this.OnTextBoxTextChanged;
			this.tb.KeyDown -= this.OnTextBoxKeyDown;
			this.tb.VisibleRangeChanged -= this.OnTextBoxVisibleRangeChanged;
		}

		// Token: 0x060062C7 RID: 25287 RVA: 0x001D81E8 File Offset: 0x001D81E8
		private void OnTextBoxVisibleRangeChanged(object sender, EventArgs e)
		{
			bool flag = this.items.Count == 0;
			if (!flag)
			{
				this.tb.NeedRecalc(true);
				foreach (Hint hint in this.items)
				{
					this.LayoutHint(hint);
					hint.HostPanel.Invalidate();
				}
			}
		}

		// Token: 0x060062C8 RID: 25288 RVA: 0x001D827C File Offset: 0x001D827C
		private void LayoutHint(Hint hint)
		{
			bool inline = hint.Inline;
			if (inline)
			{
				bool flag = hint.Range.Start.iLine < this.tb.LineInfos.Count - 1;
				if (flag)
				{
					hint.HostPanel.Top = this.tb.LineInfos[hint.Range.Start.iLine + 1].startY - hint.TopPadding - hint.HostPanel.Height - this.tb.VerticalScroll.Value;
				}
				else
				{
					hint.HostPanel.Top = this.tb.TextHeight + this.tb.Paddings.Top - hint.HostPanel.Height - this.tb.VerticalScroll.Value;
				}
			}
			else
			{
				bool flag2 = hint.Range.Start.iLine > this.tb.LinesCount - 1;
				if (flag2)
				{
					return;
				}
				bool flag3 = hint.Range.Start.iLine == this.tb.LinesCount - 1;
				if (flag3)
				{
					int num = this.tb.LineInfos[hint.Range.Start.iLine].startY - this.tb.VerticalScroll.Value + this.tb.CharHeight;
					bool flag4 = num + hint.HostPanel.Height + 1 > this.tb.ClientRectangle.Bottom;
					if (flag4)
					{
						hint.HostPanel.Top = Math.Max(0, this.tb.LineInfos[hint.Range.Start.iLine].startY - this.tb.VerticalScroll.Value - hint.HostPanel.Height);
					}
					else
					{
						hint.HostPanel.Top = num;
					}
				}
				else
				{
					hint.HostPanel.Top = this.tb.LineInfos[hint.Range.Start.iLine + 1].startY - this.tb.VerticalScroll.Value;
					bool flag5 = hint.HostPanel.Bottom > this.tb.ClientRectangle.Bottom;
					if (flag5)
					{
						hint.HostPanel.Top = this.tb.LineInfos[hint.Range.Start.iLine + 1].startY - this.tb.CharHeight - hint.TopPadding - hint.HostPanel.Height - this.tb.VerticalScroll.Value;
					}
				}
			}
			bool flag6 = hint.Dock == DockStyle.Fill;
			if (flag6)
			{
				hint.Width = this.tb.ClientSize.Width - this.tb.LeftIndent - 2;
				hint.HostPanel.Left = this.tb.LeftIndent;
			}
			else
			{
				Point point = this.tb.PlaceToPoint(hint.Range.Start);
				Point point2 = this.tb.PlaceToPoint(hint.Range.End);
				int num2 = (point.X + point2.X) / 2;
				int num3 = num2 - hint.HostPanel.Width / 2;
				hint.HostPanel.Left = Math.Max(this.tb.LeftIndent, num3);
				bool flag7 = hint.HostPanel.Right > this.tb.ClientSize.Width;
				if (flag7)
				{
					hint.HostPanel.Left = Math.Max(this.tb.LeftIndent, num3 - (hint.HostPanel.Right - this.tb.ClientSize.Width));
				}
			}
		}

		// Token: 0x060062C9 RID: 25289 RVA: 0x001D86B4 File Offset: 0x001D86B4
		public IEnumerator<Hint> GetEnumerator()
		{
			foreach (Hint item in this.items)
			{
				yield return item;
				item = null;
			}
			List<Hint>.Enumerator enumerator = default(List<Hint>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060062CA RID: 25290 RVA: 0x001D86C4 File Offset: 0x001D86C4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060062CB RID: 25291 RVA: 0x001D86E4 File Offset: 0x001D86E4
		public void Clear()
		{
			this.items.Clear();
			bool flag = this.tb.Controls.Count != 0;
			if (flag)
			{
				List<Control> list = new List<Control>();
				foreach (object obj in this.tb.Controls)
				{
					Control control = (Control)obj;
					bool flag2 = control is UnfocusablePanel;
					if (flag2)
					{
						list.Add(control);
					}
				}
				foreach (Control value in list)
				{
					this.tb.Controls.Remove(value);
				}
				for (int i = 0; i < this.tb.LineInfos.Count; i++)
				{
					LineInfo value2 = this.tb.LineInfos[i];
					value2.bottomPadding = 0;
					this.tb.LineInfos[i] = value2;
				}
				this.tb.NeedRecalc();
				this.tb.Invalidate();
				this.tb.Select();
				this.tb.ActiveControl = null;
			}
		}

		// Token: 0x060062CC RID: 25292 RVA: 0x001D8874 File Offset: 0x001D8874
		public void Add(Hint hint)
		{
			this.items.Add(hint);
			bool inline = hint.Inline;
			if (inline)
			{
				LineInfo lineInfo = this.tb.LineInfos[hint.Range.Start.iLine];
				hint.TopPadding = lineInfo.bottomPadding;
				lineInfo.bottomPadding += hint.HostPanel.Height;
				this.tb.LineInfos[hint.Range.Start.iLine] = lineInfo;
				this.tb.NeedRecalc(true);
			}
			this.LayoutHint(hint);
			this.tb.OnVisibleRangeChanged();
			hint.HostPanel.Parent = this.tb;
			this.tb.Select();
			this.tb.ActiveControl = null;
			this.tb.Invalidate();
		}

		// Token: 0x060062CD RID: 25293 RVA: 0x001D8960 File Offset: 0x001D8960
		public bool Contains(Hint item)
		{
			return this.items.Contains(item);
		}

		// Token: 0x060062CE RID: 25294 RVA: 0x001D8988 File Offset: 0x001D8988
		public void CopyTo(Hint[] array, int arrayIndex)
		{
			this.items.CopyTo(array, arrayIndex);
		}

		// Token: 0x170014C1 RID: 5313
		// (get) Token: 0x060062CF RID: 25295 RVA: 0x001D899C File Offset: 0x001D899C
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x170014C2 RID: 5314
		// (get) Token: 0x060062D0 RID: 25296 RVA: 0x001D89C0 File Offset: 0x001D89C0
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060062D1 RID: 25297 RVA: 0x001D89DC File Offset: 0x001D89DC
		public bool Remove(Hint item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04003256 RID: 12886
		private FastColoredTextBox tb;

		// Token: 0x04003257 RID: 12887
		private List<Hint> items = new List<Hint>();
	}
}
