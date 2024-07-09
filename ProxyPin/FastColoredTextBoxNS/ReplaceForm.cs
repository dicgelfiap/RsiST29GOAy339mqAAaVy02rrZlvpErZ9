using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A17 RID: 2583
	public partial class ReplaceForm : Form
	{
		// Token: 0x060063AB RID: 25515 RVA: 0x001DE080 File Offset: 0x001DE080
		public ReplaceForm(FastColoredTextBox tb)
		{
			this.InitializeComponent();
			this.tb = tb;
		}

		// Token: 0x060063AC RID: 25516 RVA: 0x001DE0A8 File Offset: 0x001DE0A8
		private void btClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060063AD RID: 25517 RVA: 0x001DE0B4 File Offset: 0x001DE0B4
		private void btFindNext_Click(object sender, EventArgs e)
		{
			try
			{
				bool flag = !this.Find(this.tbFind.Text);
				if (flag)
				{
					MessageBox.Show("Not found");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x060063AE RID: 25518 RVA: 0x001DE114 File Offset: 0x001DE114
		public List<Range> FindAll(string pattern)
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
			Range range = this.tb.Selection.IsEmpty ? this.tb.Range.Clone() : this.tb.Selection.Clone();
			List<Range> list = new List<Range>();
			foreach (Range item in range.GetRangesByLines(pattern, options))
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x060063AF RID: 25519 RVA: 0x001DE220 File Offset: 0x001DE220
		public bool Find(string pattern)
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
					Range range2 = enumerator.Current;
					this.tb.Selection.Start = range2.Start;
					this.tb.Selection.End = range2.End;
					this.tb.DoSelectionVisible();
					this.tb.Invalidate();
					return true;
				}
			}
			bool flag4 = range.Start >= this.startPlace && this.startPlace > Place.Empty;
			bool result;
			if (flag4)
			{
				this.tb.Selection.Start = new Place(0, 0);
				result = this.Find(pattern);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060063B0 RID: 25520 RVA: 0x001DE430 File Offset: 0x001DE430
		private void tbFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			bool flag = e.KeyChar == '\r';
			if (flag)
			{
				this.btFindNext_Click(sender, null);
			}
			bool flag2 = e.KeyChar == '\u001b';
			if (flag2)
			{
				base.Hide();
			}
		}

		// Token: 0x060063B1 RID: 25521 RVA: 0x001DE474 File Offset: 0x001DE474
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

		// Token: 0x060063B2 RID: 25522 RVA: 0x001DE4B0 File Offset: 0x001DE4B0
		private void ReplaceForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool flag = e.CloseReason == CloseReason.UserClosing;
			if (flag)
			{
				e.Cancel = true;
				base.Hide();
			}
			this.tb.Focus();
		}

		// Token: 0x060063B3 RID: 25523 RVA: 0x001DE4F0 File Offset: 0x001DE4F0
		private void btReplace_Click(object sender, EventArgs e)
		{
			try
			{
				bool flag = this.tb.SelectionLength != 0;
				if (flag)
				{
					bool flag2 = !this.tb.Selection.ReadOnly;
					if (flag2)
					{
						this.tb.InsertText(this.tbReplace.Text);
					}
				}
				this.btFindNext_Click(sender, null);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x060063B4 RID: 25524 RVA: 0x001DE578 File Offset: 0x001DE578
		private void btReplaceAll_Click(object sender, EventArgs e)
		{
			try
			{
				this.tb.Selection.BeginUpdate();
				List<Range> list = this.FindAll(this.tbFind.Text);
				bool flag = false;
				foreach (Range range in list)
				{
					bool readOnly = range.ReadOnly;
					if (readOnly)
					{
						flag = true;
						break;
					}
				}
				bool flag2 = !flag;
				if (flag2)
				{
					bool flag3 = list.Count > 0;
					if (flag3)
					{
						this.tb.TextSource.Manager.ExecuteCommand(new ReplaceTextCommand(this.tb.TextSource, list, this.tbReplace.Text));
						this.tb.Selection.Start = new Place(0, 0);
					}
				}
				this.tb.Invalidate();
				MessageBox.Show(list.Count + " occurrence(s) replaced");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			this.tb.Selection.EndUpdate();
		}

		// Token: 0x060063B5 RID: 25525 RVA: 0x001DE6E8 File Offset: 0x001DE6E8
		protected override void OnActivated(EventArgs e)
		{
			this.tbFind.Focus();
			this.ResetSerach();
		}

		// Token: 0x060063B6 RID: 25526 RVA: 0x001DE700 File Offset: 0x001DE700
		private void ResetSerach()
		{
			this.firstSearch = true;
		}

		// Token: 0x060063B7 RID: 25527 RVA: 0x001DE70C File Offset: 0x001DE70C
		private void cbMatchCase_CheckedChanged(object sender, EventArgs e)
		{
			this.ResetSerach();
		}

		// Token: 0x0400335B RID: 13147
		private FastColoredTextBox tb;

		// Token: 0x0400335C RID: 13148
		private bool firstSearch = true;

		// Token: 0x0400335D RID: 13149
		private Place startPlace;
	}
}
