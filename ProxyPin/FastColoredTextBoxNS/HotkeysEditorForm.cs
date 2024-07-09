using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A08 RID: 2568
	public partial class HotkeysEditorForm : Form
	{
		// Token: 0x060062FE RID: 25342 RVA: 0x001D8F2C File Offset: 0x001D8F2C
		public HotkeysEditorForm(HotkeysMapping hotkeys)
		{
			this.InitializeComponent();
			this.BuildWrappers(hotkeys);
			this.dgv.DataSource = this.wrappers;
		}

		// Token: 0x060062FF RID: 25343 RVA: 0x001D8F78 File Offset: 0x001D8F78
		private int CompereKeys(Keys key1, Keys key2)
		{
			int num = ((int)(key1 & (Keys)255)).CompareTo((int)(key2 & (Keys)255));
			bool flag = num == 0;
			if (flag)
			{
				num = key1.CompareTo(key2);
			}
			return num;
		}

		// Token: 0x06006300 RID: 25344 RVA: 0x001D8FC8 File Offset: 0x001D8FC8
		private void BuildWrappers(HotkeysMapping hotkeys)
		{
			List<Keys> list = new List<Keys>(hotkeys.Keys);
			list.Sort(new Comparison<Keys>(this.CompereKeys));
			this.wrappers.Clear();
			foreach (Keys keys in list)
			{
				this.wrappers.Add(new HotkeyWrapper(keys, hotkeys[keys]));
			}
		}

		// Token: 0x06006301 RID: 25345 RVA: 0x001D905C File Offset: 0x001D905C
		public HotkeysMapping GetHotkeys()
		{
			HotkeysMapping hotkeysMapping = new HotkeysMapping();
			foreach (HotkeyWrapper hotkeyWrapper in this.wrappers)
			{
				hotkeysMapping[hotkeyWrapper.ToKeyData()] = hotkeyWrapper.Action;
			}
			return hotkeysMapping;
		}

		// Token: 0x06006302 RID: 25346 RVA: 0x001D90D0 File Offset: 0x001D90D0
		private void btAdd_Click(object sender, EventArgs e)
		{
			this.wrappers.Add(new HotkeyWrapper(Keys.None, FCTBAction.None));
		}

		// Token: 0x06006303 RID: 25347 RVA: 0x001D90E8 File Offset: 0x001D90E8
		private void dgv_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			DataGridViewComboBoxCell dataGridViewComboBoxCell = this.dgv[0, e.RowIndex] as DataGridViewComboBoxCell;
			bool flag = dataGridViewComboBoxCell.Items.Count == 0;
			if (flag)
			{
				foreach (string item in new string[]
				{
					"",
					"Ctrl",
					"Ctrl + Shift",
					"Ctrl + Alt",
					"Shift",
					"Shift + Alt",
					"Alt",
					"Ctrl + Shift + Alt"
				})
				{
					dataGridViewComboBoxCell.Items.Add(item);
				}
			}
			dataGridViewComboBoxCell = (this.dgv[1, e.RowIndex] as DataGridViewComboBoxCell);
			bool flag2 = dataGridViewComboBoxCell.Items.Count == 0;
			if (flag2)
			{
				foreach (object item2 in Enum.GetValues(typeof(Keys)))
				{
					dataGridViewComboBoxCell.Items.Add(item2);
				}
			}
			dataGridViewComboBoxCell = (this.dgv[2, e.RowIndex] as DataGridViewComboBoxCell);
			bool flag3 = dataGridViewComboBoxCell.Items.Count == 0;
			if (flag3)
			{
				foreach (object item3 in Enum.GetValues(typeof(FCTBAction)))
				{
					dataGridViewComboBoxCell.Items.Add(item3);
				}
			}
		}

		// Token: 0x06006304 RID: 25348 RVA: 0x001D92C4 File Offset: 0x001D92C4
		private void btResore_Click(object sender, EventArgs e)
		{
			HotkeysMapping hotkeysMapping = new HotkeysMapping();
			hotkeysMapping.InitDefault();
			this.BuildWrappers(hotkeysMapping);
		}

		// Token: 0x06006305 RID: 25349 RVA: 0x001D92EC File Offset: 0x001D92EC
		private void btRemove_Click(object sender, EventArgs e)
		{
			for (int i = this.dgv.RowCount - 1; i >= 0; i--)
			{
				bool selected = this.dgv.Rows[i].Selected;
				if (selected)
				{
					this.dgv.Rows.RemoveAt(i);
				}
			}
		}

		// Token: 0x06006306 RID: 25350 RVA: 0x001D9350 File Offset: 0x001D9350
		private void HotkeysEditorForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool flag = base.DialogResult == DialogResult.OK;
			if (flag)
			{
				string unAssignedActions = this.GetUnAssignedActions();
				bool flag2 = !string.IsNullOrEmpty(unAssignedActions);
				if (flag2)
				{
					bool flag3 = MessageBox.Show("Some actions are not assigned!\r\nActions: " + unAssignedActions + "\r\nPress Yes to save and exit, press No to continue editing", "Some actions is not assigned", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No;
					if (flag3)
					{
						e.Cancel = true;
					}
				}
			}
		}

		// Token: 0x06006307 RID: 25351 RVA: 0x001D93BC File Offset: 0x001D93BC
		private string GetUnAssignedActions()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Dictionary<FCTBAction, FCTBAction> dictionary = new Dictionary<FCTBAction, FCTBAction>();
			foreach (HotkeyWrapper hotkeyWrapper in this.wrappers)
			{
				dictionary[hotkeyWrapper.Action] = hotkeyWrapper.Action;
			}
			foreach (object obj in Enum.GetValues(typeof(FCTBAction)))
			{
				bool flag = (FCTBAction)obj > FCTBAction.None;
				if (flag)
				{
					bool flag2 = !((FCTBAction)obj).ToString().StartsWith("CustomAction");
					if (flag2)
					{
						bool flag3 = !dictionary.ContainsKey((FCTBAction)obj);
						if (flag3)
						{
							stringBuilder.Append(obj + ", ");
						}
					}
				}
			}
			return stringBuilder.ToString().TrimEnd(new char[]
			{
				' ',
				','
			});
		}

		// Token: 0x0400325F RID: 12895
		private BindingList<HotkeyWrapper> wrappers = new BindingList<HotkeyWrapper>();
	}
}
