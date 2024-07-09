using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
	// Token: 0x020009F9 RID: 2553
	[ToolboxItem(false)]
	public class AutocompleteListView : UserControl, IDisposable
	{
		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06006201 RID: 25089 RVA: 0x001D3A10 File Offset: 0x001D3A10
		// (remove) Token: 0x06006202 RID: 25090 RVA: 0x001D3A4C File Offset: 0x001D3A4C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler FocussedItemIndexChanged;

		// Token: 0x17001496 RID: 5270
		// (get) Token: 0x06006203 RID: 25091 RVA: 0x001D3A88 File Offset: 0x001D3A88
		private int ItemHeight
		{
			get
			{
				return this.Font.Height + 2;
			}
		}

		// Token: 0x17001497 RID: 5271
		// (get) Token: 0x06006204 RID: 25092 RVA: 0x001D3AB0 File Offset: 0x001D3AB0
		private AutocompleteMenu Menu
		{
			get
			{
				return base.Parent as AutocompleteMenu;
			}
		}

		// Token: 0x17001498 RID: 5272
		// (get) Token: 0x06006205 RID: 25093 RVA: 0x001D3AD4 File Offset: 0x001D3AD4
		// (set) Token: 0x06006206 RID: 25094 RVA: 0x001D3ADC File Offset: 0x001D3ADC
		internal bool AllowTabKey { get; set; }

		// Token: 0x17001499 RID: 5273
		// (get) Token: 0x06006207 RID: 25095 RVA: 0x001D3AE8 File Offset: 0x001D3AE8
		// (set) Token: 0x06006208 RID: 25096 RVA: 0x001D3AF0 File Offset: 0x001D3AF0
		public ImageList ImageList { get; set; }

		// Token: 0x1700149A RID: 5274
		// (get) Token: 0x06006209 RID: 25097 RVA: 0x001D3AFC File Offset: 0x001D3AFC
		// (set) Token: 0x0600620A RID: 25098 RVA: 0x001D3B20 File Offset: 0x001D3B20
		internal int AppearInterval
		{
			get
			{
				return this.timer.Interval;
			}
			set
			{
				this.timer.Interval = value;
			}
		}

		// Token: 0x1700149B RID: 5275
		// (get) Token: 0x0600620B RID: 25099 RVA: 0x001D3B30 File Offset: 0x001D3B30
		// (set) Token: 0x0600620C RID: 25100 RVA: 0x001D3B38 File Offset: 0x001D3B38
		internal int ToolTipDuration { get; set; }

		// Token: 0x1700149C RID: 5276
		// (get) Token: 0x0600620D RID: 25101 RVA: 0x001D3B44 File Offset: 0x001D3B44
		// (set) Token: 0x0600620E RID: 25102 RVA: 0x001D3B4C File Offset: 0x001D3B4C
		internal Size MaxToolTipSize { get; set; }

		// Token: 0x1700149D RID: 5277
		// (get) Token: 0x0600620F RID: 25103 RVA: 0x001D3B58 File Offset: 0x001D3B58
		// (set) Token: 0x06006210 RID: 25104 RVA: 0x001D3B7C File Offset: 0x001D3B7C
		internal bool AlwaysShowTooltip
		{
			get
			{
				return this.toolTip.ShowAlways;
			}
			set
			{
				this.toolTip.ShowAlways = value;
			}
		}

		// Token: 0x1700149E RID: 5278
		// (get) Token: 0x06006211 RID: 25105 RVA: 0x001D3B8C File Offset: 0x001D3B8C
		// (set) Token: 0x06006212 RID: 25106 RVA: 0x001D3B94 File Offset: 0x001D3B94
		public Color SelectedColor { get; set; }

		// Token: 0x1700149F RID: 5279
		// (get) Token: 0x06006213 RID: 25107 RVA: 0x001D3BA0 File Offset: 0x001D3BA0
		// (set) Token: 0x06006214 RID: 25108 RVA: 0x001D3BA8 File Offset: 0x001D3BA8
		public Color HoveredColor { get; set; }

		// Token: 0x170014A0 RID: 5280
		// (get) Token: 0x06006215 RID: 25109 RVA: 0x001D3BB4 File Offset: 0x001D3BB4
		// (set) Token: 0x06006216 RID: 25110 RVA: 0x001D3BD4 File Offset: 0x001D3BD4
		public int FocussedItemIndex
		{
			get
			{
				return this.focussedItemIndex;
			}
			set
			{
				bool flag = this.focussedItemIndex != value;
				if (flag)
				{
					this.focussedItemIndex = value;
					bool flag2 = this.FocussedItemIndexChanged != null;
					if (flag2)
					{
						this.FocussedItemIndexChanged(this, EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x170014A1 RID: 5281
		// (get) Token: 0x06006217 RID: 25111 RVA: 0x001D3C24 File Offset: 0x001D3C24
		// (set) Token: 0x06006218 RID: 25112 RVA: 0x001D3C84 File Offset: 0x001D3C84
		public AutocompleteItem FocussedItem
		{
			get
			{
				bool flag = this.FocussedItemIndex >= 0 && this.focussedItemIndex < this.visibleItems.Count;
				AutocompleteItem result;
				if (flag)
				{
					result = this.visibleItems[this.focussedItemIndex];
				}
				else
				{
					result = null;
				}
				return result;
			}
			set
			{
				this.FocussedItemIndex = this.visibleItems.IndexOf(value);
			}
		}

		// Token: 0x06006219 RID: 25113 RVA: 0x001D3C9C File Offset: 0x001D3C9C
		internal AutocompleteListView(FastColoredTextBox tb)
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			base.Font = new Font(FontFamily.GenericSansSerif, 9f);
			this.visibleItems = new List<AutocompleteItem>();
			base.VerticalScroll.SmallChange = this.ItemHeight;
			this.MaximumSize = new Size(base.Size.Width, 180);
			this.toolTip.ShowAlways = false;
			this.AppearInterval = 500;
			this.timer.Tick += this.timer_Tick;
			this.SelectedColor = Color.Orange;
			this.HoveredColor = Color.Red;
			this.ToolTipDuration = 3000;
			this.toolTip.Popup += this.ToolTip_Popup;
			this.tb = tb;
			tb.KeyDown += this.tb_KeyDown;
			tb.SelectionChanged += this.tb_SelectionChanged;
			tb.KeyPressed += this.tb_KeyPressed;
			Form form = tb.FindForm();
			bool flag = form != null;
			if (flag)
			{
				form.LocationChanged += delegate(object <sender>, EventArgs <e>)
				{
					this.SafetyClose();
				};
				form.ResizeBegin += delegate(object <sender>, EventArgs <e>)
				{
					this.SafetyClose();
				};
				form.FormClosing += delegate(object <sender>, FormClosingEventArgs <e>)
				{
					this.SafetyClose();
				};
				form.LostFocus += delegate(object <sender>, EventArgs <e>)
				{
					this.SafetyClose();
				};
			}
			tb.LostFocus += delegate(object o, EventArgs e)
			{
				bool flag2 = this.Menu != null && !this.Menu.IsDisposed;
				if (flag2)
				{
					bool flag3 = !this.Menu.Focused;
					if (flag3)
					{
						this.SafetyClose();
					}
				}
			};
			tb.Scroll += delegate(object <sender>, ScrollEventArgs <e>)
			{
				this.SafetyClose();
			};
			base.VisibleChanged += delegate(object o, EventArgs e)
			{
				bool visible = base.Visible;
				if (visible)
				{
					this.DoSelectedVisible();
				}
			};
		}

		// Token: 0x0600621A RID: 25114 RVA: 0x001D3E90 File Offset: 0x001D3E90
		private void ToolTip_Popup(object sender, PopupEventArgs e)
		{
			bool flag = this.MaxToolTipSize.Height > 0 && this.MaxToolTipSize.Width > 0;
			if (flag)
			{
				e.ToolTipSize = this.MaxToolTipSize;
			}
		}

		// Token: 0x0600621B RID: 25115 RVA: 0x001D3EE4 File Offset: 0x001D3EE4
		protected override void Dispose(bool disposing)
		{
			bool flag = this.toolTip != null;
			if (flag)
			{
				this.toolTip.Popup -= this.ToolTip_Popup;
				this.toolTip.Dispose();
			}
			bool flag2 = this.tb != null;
			if (flag2)
			{
				this.tb.KeyDown -= this.tb_KeyDown;
				this.tb.KeyPress -= this.tb_KeyPressed;
				this.tb.SelectionChanged -= this.tb_SelectionChanged;
			}
			bool flag3 = this.timer != null;
			if (flag3)
			{
				this.timer.Stop();
				this.timer.Tick -= this.timer_Tick;
				this.timer.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600621C RID: 25116 RVA: 0x001D3FD0 File Offset: 0x001D3FD0
		private void SafetyClose()
		{
			bool flag = this.Menu != null && !this.Menu.IsDisposed;
			if (flag)
			{
				this.Menu.Close();
			}
		}

		// Token: 0x0600621D RID: 25117 RVA: 0x001D4014 File Offset: 0x001D4014
		private void tb_KeyPressed(object sender, KeyPressEventArgs e)
		{
			bool flag = e.KeyChar == '\b' || e.KeyChar == 'ÿ';
			bool flag2 = this.Menu.Visible && !flag;
			if (flag2)
			{
				this.DoAutocomplete(false);
			}
			else
			{
				this.ResetTimer(this.timer);
			}
		}

		// Token: 0x0600621E RID: 25118 RVA: 0x001D4084 File Offset: 0x001D4084
		private void timer_Tick(object sender, EventArgs e)
		{
			this.timer.Stop();
			this.DoAutocomplete(false);
		}

		// Token: 0x0600621F RID: 25119 RVA: 0x001D409C File Offset: 0x001D409C
		private void ResetTimer(Timer timer)
		{
			timer.Stop();
			timer.Start();
		}

		// Token: 0x06006220 RID: 25120 RVA: 0x001D40B0 File Offset: 0x001D40B0
		internal void DoAutocomplete()
		{
			this.DoAutocomplete(false);
		}

		// Token: 0x06006221 RID: 25121 RVA: 0x001D40BC File Offset: 0x001D40BC
		internal void DoAutocomplete(bool forced)
		{
			bool flag = !this.Menu.Enabled;
			if (flag)
			{
				this.Menu.Close();
			}
			else
			{
				this.visibleItems.Clear();
				this.FocussedItemIndex = 0;
				base.VerticalScroll.Value = 0;
				base.AutoScrollMinSize -= new Size(1, 0);
				base.AutoScrollMinSize += new Size(1, 0);
				Range fragment = this.tb.Selection.GetFragment(this.Menu.SearchPattern);
				string text = fragment.Text;
				Point position = this.tb.PlaceToPoint(fragment.End);
				position.Offset(2, this.tb.CharHeight);
				bool flag2 = forced || (text.Length >= this.Menu.MinFragmentLength && this.tb.Selection.IsEmpty && (this.tb.Selection.Start > fragment.Start || text.Length == 0));
				if (flag2)
				{
					this.Menu.Fragment = fragment;
					bool flag3 = false;
					foreach (AutocompleteItem autocompleteItem in this.sourceItems)
					{
						autocompleteItem.Parent = this.Menu;
						CompareResult compareResult = autocompleteItem.Compare(text);
						bool flag4 = compareResult > CompareResult.Hidden;
						if (flag4)
						{
							this.visibleItems.Add(autocompleteItem);
						}
						bool flag5 = compareResult == CompareResult.VisibleAndSelected && !flag3;
						if (flag5)
						{
							flag3 = true;
							this.FocussedItemIndex = this.visibleItems.Count - 1;
						}
					}
					bool flag6 = flag3;
					if (flag6)
					{
						this.AdjustScroll();
						this.DoSelectedVisible();
					}
				}
				bool flag7 = this.Count > 0;
				if (flag7)
				{
					bool flag8 = !this.Menu.Visible;
					if (flag8)
					{
						CancelEventArgs cancelEventArgs = new CancelEventArgs();
						this.Menu.OnOpening(cancelEventArgs);
						bool flag9 = !cancelEventArgs.Cancel;
						if (flag9)
						{
							this.Menu.Show(this.tb, position);
						}
					}
					this.DoSelectedVisible();
					base.Invalidate();
				}
				else
				{
					this.Menu.Close();
				}
			}
		}

		// Token: 0x06006222 RID: 25122 RVA: 0x001D436C File Offset: 0x001D436C
		private void tb_SelectionChanged(object sender, EventArgs e)
		{
			bool visible = this.Menu.Visible;
			if (visible)
			{
				bool flag = false;
				bool flag2 = !this.tb.Selection.IsEmpty;
				if (flag2)
				{
					flag = true;
				}
				else
				{
					bool flag3 = !this.Menu.Fragment.Contains(this.tb.Selection.Start);
					if (flag3)
					{
						bool flag4 = this.tb.Selection.Start.iLine == this.Menu.Fragment.End.iLine && this.tb.Selection.Start.iChar == this.Menu.Fragment.End.iChar + 1;
						if (flag4)
						{
							bool flag5 = !Regex.IsMatch(this.tb.Selection.CharBeforeStart.ToString(), this.Menu.SearchPattern);
							if (flag5)
							{
								flag = true;
							}
						}
						else
						{
							flag = true;
						}
					}
				}
				bool flag6 = flag;
				if (flag6)
				{
					this.Menu.Close();
				}
			}
		}

		// Token: 0x06006223 RID: 25123 RVA: 0x001D44A4 File Offset: 0x001D44A4
		private void tb_KeyDown(object sender, KeyEventArgs e)
		{
			FastColoredTextBox fastColoredTextBox = sender as FastColoredTextBox;
			bool visible = this.Menu.Visible;
			if (visible)
			{
				bool flag = this.ProcessKey(e.KeyCode, e.Modifiers);
				if (flag)
				{
					e.Handled = true;
				}
			}
			bool flag2 = !this.Menu.Visible;
			if (flag2)
			{
				bool flag3 = fastColoredTextBox.HotkeysMapping.ContainsKey(e.KeyData) && fastColoredTextBox.HotkeysMapping[e.KeyData] == FCTBAction.AutocompleteMenu;
				if (flag3)
				{
					this.DoAutocomplete();
					e.Handled = true;
				}
				else
				{
					bool flag4 = e.KeyCode == Keys.Escape && this.timer.Enabled;
					if (flag4)
					{
						this.timer.Stop();
					}
				}
			}
		}

		// Token: 0x06006224 RID: 25124 RVA: 0x001D458C File Offset: 0x001D458C
		private void AdjustScroll()
		{
			bool flag = this.oldItemCount == this.visibleItems.Count;
			if (!flag)
			{
				int num = this.ItemHeight * this.visibleItems.Count + 1;
				base.Height = Math.Min(num, this.MaximumSize.Height);
				this.Menu.CalcSize();
				base.AutoScrollMinSize = new Size(0, num);
				this.oldItemCount = this.visibleItems.Count;
			}
		}

		// Token: 0x06006225 RID: 25125 RVA: 0x001D4618 File Offset: 0x001D4618
		protected override void OnPaint(PaintEventArgs e)
		{
			this.AdjustScroll();
			int itemHeight = this.ItemHeight;
			int num = base.VerticalScroll.Value / itemHeight - 1;
			int num2 = (base.VerticalScroll.Value + base.ClientSize.Height) / itemHeight + 1;
			num = Math.Max(num, 0);
			num2 = Math.Min(num2, this.visibleItems.Count);
			int num3 = 0;
			int num4 = 18;
			for (int i = num; i < num2; i++)
			{
				num3 = i * itemHeight - base.VerticalScroll.Value;
				AutocompleteItem autocompleteItem = this.visibleItems[i];
				bool flag = autocompleteItem.BackColor != Color.Transparent;
				if (flag)
				{
					using (SolidBrush solidBrush = new SolidBrush(autocompleteItem.BackColor))
					{
						e.Graphics.FillRectangle(solidBrush, 1, num3, base.ClientSize.Width - 1 - 1, itemHeight - 1);
					}
				}
				bool flag2 = this.ImageList != null && this.visibleItems[i].ImageIndex >= 0;
				if (flag2)
				{
					e.Graphics.DrawImage(this.ImageList.Images[autocompleteItem.ImageIndex], 1, num3);
				}
				bool flag3 = i == this.FocussedItemIndex;
				if (flag3)
				{
					using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Point(0, num3 - 3), new Point(0, num3 + itemHeight), Color.Transparent, this.SelectedColor))
					{
						using (Pen pen = new Pen(this.SelectedColor))
						{
							e.Graphics.FillRectangle(linearGradientBrush, num4, num3, base.ClientSize.Width - 1 - num4, itemHeight - 1);
							e.Graphics.DrawRectangle(pen, num4, num3, base.ClientSize.Width - 1 - num4, itemHeight - 1);
						}
					}
				}
				bool flag4 = i == this.hoveredItemIndex;
				if (flag4)
				{
					using (Pen pen2 = new Pen(this.HoveredColor))
					{
						e.Graphics.DrawRectangle(pen2, num4, num3, base.ClientSize.Width - 1 - num4, itemHeight - 1);
					}
				}
				using (SolidBrush solidBrush2 = new SolidBrush((autocompleteItem.ForeColor != Color.Transparent) ? autocompleteItem.ForeColor : this.ForeColor))
				{
					e.Graphics.DrawString(autocompleteItem.ToString(), this.Font, solidBrush2, (float)num4, (float)num3);
				}
			}
		}

		// Token: 0x06006226 RID: 25126 RVA: 0x001D4944 File Offset: 0x001D4944
		protected override void OnScroll(ScrollEventArgs se)
		{
			base.OnScroll(se);
			base.Invalidate();
		}

		// Token: 0x06006227 RID: 25127 RVA: 0x001D4958 File Offset: 0x001D4958
		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				this.FocussedItemIndex = this.PointToItemIndex(e.Location);
				this.DoSelectedVisible();
				base.Invalidate();
			}
		}

		// Token: 0x06006228 RID: 25128 RVA: 0x001D49AC File Offset: 0x001D49AC
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			this.FocussedItemIndex = this.PointToItemIndex(e.Location);
			base.Invalidate();
			this.OnSelecting();
		}

		// Token: 0x06006229 RID: 25129 RVA: 0x001D49E8 File Offset: 0x001D49E8
		internal virtual void OnSelecting()
		{
			bool flag = this.FocussedItemIndex < 0 || this.FocussedItemIndex >= this.visibleItems.Count;
			if (!flag)
			{
				this.tb.TextSource.Manager.BeginAutoUndoCommands();
				try
				{
					AutocompleteItem focussedItem = this.FocussedItem;
					SelectingEventArgs selectingEventArgs = new SelectingEventArgs
					{
						Item = focussedItem,
						SelectedIndex = this.FocussedItemIndex
					};
					this.Menu.OnSelecting(selectingEventArgs);
					bool cancel = selectingEventArgs.Cancel;
					if (cancel)
					{
						this.FocussedItemIndex = selectingEventArgs.SelectedIndex;
						base.Invalidate();
					}
					else
					{
						bool flag2 = !selectingEventArgs.Handled;
						if (flag2)
						{
							Range fragment = this.Menu.Fragment;
							this.DoAutocomplete(focussedItem, fragment);
						}
						this.Menu.Close();
						SelectedEventArgs selectedEventArgs = new SelectedEventArgs
						{
							Item = focussedItem,
							Tb = this.Menu.Fragment.tb
						};
						focussedItem.OnSelected(this.Menu, selectedEventArgs);
						this.Menu.OnSelected(selectedEventArgs);
					}
				}
				finally
				{
					this.tb.TextSource.Manager.EndAutoUndoCommands();
				}
			}
		}

		// Token: 0x0600622A RID: 25130 RVA: 0x001D4B40 File Offset: 0x001D4B40
		private void DoAutocomplete(AutocompleteItem item, Range fragment)
		{
			string textForReplace = item.GetTextForReplace();
			FastColoredTextBox fastColoredTextBox = fragment.tb;
			fastColoredTextBox.BeginAutoUndo();
			fastColoredTextBox.TextSource.Manager.ExecuteCommand(new SelectCommand(fastColoredTextBox.TextSource));
			bool columnSelectionMode = fastColoredTextBox.Selection.ColumnSelectionMode;
			if (columnSelectionMode)
			{
				Place start = fastColoredTextBox.Selection.Start;
				Place end = fastColoredTextBox.Selection.End;
				start.iChar = fragment.Start.iChar;
				end.iChar = fragment.End.iChar;
				fastColoredTextBox.Selection.Start = start;
				fastColoredTextBox.Selection.End = end;
			}
			else
			{
				fastColoredTextBox.Selection.Start = fragment.Start;
				fastColoredTextBox.Selection.End = fragment.End;
			}
			fastColoredTextBox.InsertText(textForReplace);
			fastColoredTextBox.TextSource.Manager.ExecuteCommand(new SelectCommand(fastColoredTextBox.TextSource));
			fastColoredTextBox.EndAutoUndo();
			fastColoredTextBox.Focus();
		}

		// Token: 0x0600622B RID: 25131 RVA: 0x001D4C4C File Offset: 0x001D4C4C
		private int PointToItemIndex(Point p)
		{
			return (p.Y + base.VerticalScroll.Value) / this.ItemHeight;
		}

		// Token: 0x0600622C RID: 25132 RVA: 0x001D4C80 File Offset: 0x001D4C80
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			this.ProcessKey(keyData, Keys.None);
			return base.ProcessCmdKey(ref msg, keyData);
		}

		// Token: 0x0600622D RID: 25133 RVA: 0x001D4CAC File Offset: 0x001D4CAC
		private bool ProcessKey(Keys keyData, Keys keyModifiers)
		{
			bool flag = keyModifiers == Keys.None;
			if (flag)
			{
				if (keyData <= Keys.Escape)
				{
					if (keyData != Keys.Tab)
					{
						if (keyData == Keys.Return)
						{
							this.OnSelecting();
							return true;
						}
						if (keyData == Keys.Escape)
						{
							this.Menu.Close();
							return true;
						}
					}
					else
					{
						bool flag2 = !this.AllowTabKey;
						if (!flag2)
						{
							this.OnSelecting();
							return true;
						}
					}
				}
				else if (keyData <= Keys.Next)
				{
					if (keyData == Keys.Prior)
					{
						this.SelectNext(-10);
						return true;
					}
					if (keyData == Keys.Next)
					{
						this.SelectNext(10);
						return true;
					}
				}
				else
				{
					if (keyData == Keys.Up)
					{
						this.SelectNext(-1);
						return true;
					}
					if (keyData == Keys.Down)
					{
						this.SelectNext(1);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600622E RID: 25134 RVA: 0x001D4DC0 File Offset: 0x001D4DC0
		public void SelectNext(int shift)
		{
			this.FocussedItemIndex = Math.Max(0, Math.Min(this.FocussedItemIndex + shift, this.visibleItems.Count - 1));
			this.DoSelectedVisible();
			base.Invalidate();
		}

		// Token: 0x0600622F RID: 25135 RVA: 0x001D4E08 File Offset: 0x001D4E08
		private void DoSelectedVisible()
		{
			bool flag = this.FocussedItem != null;
			if (flag)
			{
				this.SetToolTip(this.FocussedItem);
			}
			int num = this.FocussedItemIndex * this.ItemHeight - base.VerticalScroll.Value;
			bool flag2 = num < 0;
			if (flag2)
			{
				base.VerticalScroll.Value = this.FocussedItemIndex * this.ItemHeight;
			}
			bool flag3 = num > base.ClientSize.Height - this.ItemHeight;
			if (flag3)
			{
				base.VerticalScroll.Value = Math.Min(base.VerticalScroll.Maximum, this.FocussedItemIndex * this.ItemHeight - base.ClientSize.Height + this.ItemHeight);
			}
			base.AutoScrollMinSize -= new Size(1, 0);
			base.AutoScrollMinSize += new Size(1, 0);
		}

		// Token: 0x06006230 RID: 25136 RVA: 0x001D4F08 File Offset: 0x001D4F08
		private void SetToolTip(AutocompleteItem autocompleteItem)
		{
			string toolTipTitle = autocompleteItem.ToolTipTitle;
			string toolTipText = autocompleteItem.ToolTipText;
			bool flag = string.IsNullOrEmpty(toolTipTitle);
			if (flag)
			{
				this.toolTip.ToolTipTitle = null;
				this.toolTip.SetToolTip(this, null);
			}
			else
			{
				bool flag2 = base.Parent != null;
				if (flag2)
				{
					IWin32Window window = base.Parent ?? this;
					bool flag3 = base.PointToScreen(base.Location).X + this.MaxToolTipSize.Width + 105 < Screen.FromControl(base.Parent).WorkingArea.Right;
					Point point;
					if (flag3)
					{
						point = new Point(base.Right + 5, 0);
					}
					else
					{
						point = new Point(base.Left - 105 - this.MaximumSize.Width, 0);
					}
					bool flag4 = string.IsNullOrEmpty(toolTipText);
					if (flag4)
					{
						this.toolTip.ToolTipTitle = null;
						this.toolTip.Show(toolTipTitle, window, point.X, point.Y, this.ToolTipDuration);
					}
					else
					{
						this.toolTip.ToolTipTitle = toolTipTitle;
						this.toolTip.Show(toolTipText, window, point.X, point.Y, this.ToolTipDuration);
					}
				}
			}
		}

		// Token: 0x170014A2 RID: 5282
		// (get) Token: 0x06006231 RID: 25137 RVA: 0x001D5074 File Offset: 0x001D5074
		public int Count
		{
			get
			{
				return this.visibleItems.Count;
			}
		}

		// Token: 0x06006232 RID: 25138 RVA: 0x001D5098 File Offset: 0x001D5098
		public void SetAutocompleteItems(ICollection<string> items)
		{
			List<AutocompleteItem> list = new List<AutocompleteItem>(items.Count);
			foreach (string text in items)
			{
				list.Add(new AutocompleteItem(text));
			}
			this.SetAutocompleteItems(list);
		}

		// Token: 0x06006233 RID: 25139 RVA: 0x001D5108 File Offset: 0x001D5108
		public void SetAutocompleteItems(IEnumerable<AutocompleteItem> items)
		{
			this.sourceItems = items;
		}

		// Token: 0x0400321C RID: 12828
		internal List<AutocompleteItem> visibleItems;

		// Token: 0x0400321D RID: 12829
		private IEnumerable<AutocompleteItem> sourceItems = new List<AutocompleteItem>();

		// Token: 0x0400321E RID: 12830
		private int focussedItemIndex = 0;

		// Token: 0x0400321F RID: 12831
		private int hoveredItemIndex = -1;

		// Token: 0x04003220 RID: 12832
		private int oldItemCount = 0;

		// Token: 0x04003221 RID: 12833
		private FastColoredTextBox tb;

		// Token: 0x04003222 RID: 12834
		internal ToolTip toolTip = new ToolTip();

		// Token: 0x04003223 RID: 12835
		private Timer timer = new Timer();
	}
}
