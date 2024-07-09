using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A38 RID: 2616
	public partial class FindForm : Form
	{
		// Token: 0x06006680 RID: 26240 RVA: 0x001F24DC File Offset: 0x001F24DC
		public FindForm(FastColoredTextBox tb)
		{
			this.InitializeComponent();
			this.tb = tb;
		}

		// Token: 0x06006681 RID: 26241 RVA: 0x001F2504 File Offset: 0x001F2504
		private void btClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06006682 RID: 26242 RVA: 0x001F2510 File Offset: 0x001F2510
		private void btFindNext_Click(object sender, EventArgs e)
		{
			this.FindNext(this.tbFind.Text);
		}

		// Token: 0x06006683 RID: 26243 RVA: 0x001F2528 File Offset: 0x001F2528
		public virtual void FindNext(string pattern)
		{
			try
			{
				RegexOptions options = this.cbMatchCase.Checked ? RegexOptions.None : RegexOptions.IgnoreCase;
				bool flag = !this.cbRegex.Checked;
				if (flag)
				{
					pattern = Regex.Escape(pattern);
				}
				bool @checked = this.cbWholeWord.Checked;
				if (@checked)
				{
					pattern = "\\b" + pattern + "\\b";
				}
				Range range = this.tb.Selection.Clone();
				range.Normalize();
				bool flag2 = this.firstSearch;
				if (flag2)
				{
					this.startPlace = range.Start;
					this.firstSearch = false;
				}
				range.Start = range.End;
				bool flag3 = range.Start >= this.startPlace;
				if (flag3)
				{
					range.End = new Place(this.tb.GetLineLength(this.tb.LinesCount - 1), this.tb.LinesCount - 1);
				}
				else
				{
					range.End = this.startPlace;
				}
				using (IEnumerator<Range> enumerator = range.GetRangesByLines(pattern, options).GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						Range selection = enumerator.Current;
						this.tb.Selection = selection;
						this.tb.DoSelectionVisible();
						this.tb.Invalidate();
						return;
					}
				}
				bool flag4 = range.Start >= this.startPlace && this.startPlace > Place.Empty;
				if (flag4)
				{
					this.tb.Selection.Start = new Place(0, 0);
					this.FindNext(pattern);
				}
				else
				{
					MessageBox.Show("Not found");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06006684 RID: 26244 RVA: 0x001F2754 File Offset: 0x001F2754
		private void tbFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			bool flag = e.KeyChar == '\r';
			if (flag)
			{
				this.btFindNext.PerformClick();
				e.Handled = true;
			}
			else
			{
				bool flag2 = e.KeyChar == '\u001b';
				if (flag2)
				{
					base.Hide();
					e.Handled = true;
				}
			}
		}

		// Token: 0x06006685 RID: 26245 RVA: 0x001F27B8 File Offset: 0x001F27B8
		private void FindForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool flag = e.CloseReason == CloseReason.UserClosing;
			if (flag)
			{
				e.Cancel = true;
				base.Hide();
			}
			this.tb.Focus();
		}

		// Token: 0x06006686 RID: 26246 RVA: 0x001F27F8 File Offset: 0x001F27F8
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			bool flag = keyData == Keys.Escape;
			bool result;
			if (flag)
			{
				base.Close();
				result = true;
			}
			else
			{
				result = base.ProcessCmdKey(ref msg, keyData);
			}
			return result;
		}

		// Token: 0x06006687 RID: 26247 RVA: 0x001F2834 File Offset: 0x001F2834
		protected override void OnActivated(EventArgs e)
		{
			this.tbFind.Focus();
			this.ResetSerach();
		}

		// Token: 0x06006688 RID: 26248 RVA: 0x001F284C File Offset: 0x001F284C
		private void ResetSerach()
		{
			this.firstSearch = true;
		}

		// Token: 0x06006689 RID: 26249 RVA: 0x001F2858 File Offset: 0x001F2858
		private void cbMatchCase_CheckedChanged(object sender, EventArgs e)
		{
			this.ResetSerach();
		}

		// Token: 0x04003480 RID: 13440
		private bool firstSearch = true;

		// Token: 0x04003481 RID: 13441
		private Place startPlace;

		// Token: 0x04003482 RID: 13442
		private FastColoredTextBox tb;
	}
}
