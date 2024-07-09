using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A0E RID: 2574
	public class HotkeysMapping : SortedDictionary<Keys, FCTBAction>
	{
		// Token: 0x06006334 RID: 25396 RVA: 0x001DA6D0 File Offset: 0x001DA6D0
		public virtual void InitDefault()
		{
			base[(Keys)131143] = FCTBAction.GoToDialog;
			base[(Keys)131142] = FCTBAction.FindDialog;
			base[(Keys)262214] = FCTBAction.FindChar;
			base[System.Windows.Forms.Keys.F3] = FCTBAction.FindNext;
			base[(Keys)131144] = FCTBAction.ReplaceDialog;
			base[(Keys)131139] = FCTBAction.Copy;
			base[(Keys)196675] = FCTBAction.CommentSelected;
			base[(Keys)131160] = FCTBAction.Cut;
			base[(Keys)131158] = FCTBAction.Paste;
			base[(Keys)131137] = FCTBAction.SelectAll;
			base[(Keys)131162] = FCTBAction.Undo;
			base[(Keys)131154] = FCTBAction.Redo;
			base[(Keys)131157] = FCTBAction.UpperCase;
			base[(Keys)196693] = FCTBAction.LowerCase;
			base[System.Windows.Forms.Keys.LButton | System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.Back | System.Windows.Forms.Keys.ShiftKey | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.F17 | System.Windows.Forms.Keys.Control] = FCTBAction.NavigateBackward;
			base[System.Windows.Forms.Keys.LButton | System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.Back | System.Windows.Forms.Keys.ShiftKey | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.F17 | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control] = FCTBAction.NavigateForward;
			base[(Keys)131138] = FCTBAction.BookmarkLine;
			base[(Keys)196674] = FCTBAction.UnbookmarkLine;
			base[(Keys)131150] = FCTBAction.GoNextBookmark;
			base[(Keys)196686] = FCTBAction.GoPrevBookmark;
			base[System.Windows.Forms.Keys.Back | System.Windows.Forms.Keys.Alt] = FCTBAction.Undo;
			base[System.Windows.Forms.Keys.Back | System.Windows.Forms.Keys.Control] = FCTBAction.ClearWordLeft;
			base[System.Windows.Forms.Keys.Insert] = FCTBAction.ReplaceMode;
			base[System.Windows.Forms.Keys.LButton | System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.Back | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Control] = FCTBAction.Copy;
			base[System.Windows.Forms.Keys.LButton | System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.Back | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Shift] = FCTBAction.Paste;
			base[System.Windows.Forms.Keys.Delete] = FCTBAction.DeleteCharRight;
			base[System.Windows.Forms.Keys.RButton | System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.Back | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Control] = FCTBAction.ClearWordRight;
			base[System.Windows.Forms.Keys.RButton | System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.Back | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Shift] = FCTBAction.Cut;
			base[System.Windows.Forms.Keys.Left] = FCTBAction.GoLeft;
			base[System.Windows.Forms.Keys.LButton | System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Shift] = FCTBAction.GoLeftWithSelection;
			base[System.Windows.Forms.Keys.LButton | System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Control] = FCTBAction.GoWordLeft;
			base[System.Windows.Forms.Keys.LButton | System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control] = FCTBAction.GoWordLeftWithSelection;
			base[System.Windows.Forms.Keys.LButton | System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Alt] = FCTBAction.GoLeft_ColumnSelectionMode;
			base[System.Windows.Forms.Keys.Right] = FCTBAction.GoRight;
			base[System.Windows.Forms.Keys.LButton | System.Windows.Forms.Keys.RButton | System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Shift] = FCTBAction.GoRightWithSelection;
			base[System.Windows.Forms.Keys.LButton | System.Windows.Forms.Keys.RButton | System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Control] = FCTBAction.GoWordRight;
			base[System.Windows.Forms.Keys.LButton | System.Windows.Forms.Keys.RButton | System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control] = FCTBAction.GoWordRightWithSelection;
			base[System.Windows.Forms.Keys.LButton | System.Windows.Forms.Keys.RButton | System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Alt] = FCTBAction.GoRight_ColumnSelectionMode;
			base[System.Windows.Forms.Keys.Up] = FCTBAction.GoUp;
			base[System.Windows.Forms.Keys.RButton | System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Shift] = FCTBAction.GoUpWithSelection;
			base[System.Windows.Forms.Keys.RButton | System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Alt] = FCTBAction.GoUp_ColumnSelectionMode;
			base[System.Windows.Forms.Keys.RButton | System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Alt] = FCTBAction.MoveSelectedLinesUp;
			base[System.Windows.Forms.Keys.RButton | System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Control] = FCTBAction.ScrollUp;
			base[System.Windows.Forms.Keys.Down] = FCTBAction.GoDown;
			base[System.Windows.Forms.Keys.Back | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Shift] = FCTBAction.GoDownWithSelection;
			base[System.Windows.Forms.Keys.Back | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Alt] = FCTBAction.GoDown_ColumnSelectionMode;
			base[System.Windows.Forms.Keys.Back | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Alt] = FCTBAction.MoveSelectedLinesDown;
			base[System.Windows.Forms.Keys.Back | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Control] = FCTBAction.ScrollDown;
			base[System.Windows.Forms.Keys.Prior] = FCTBAction.GoPageUp;
			base[System.Windows.Forms.Keys.LButton | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Shift] = FCTBAction.GoPageUpWithSelection;
			base[System.Windows.Forms.Keys.Next] = FCTBAction.GoPageDown;
			base[System.Windows.Forms.Keys.RButton | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Shift] = FCTBAction.GoPageDownWithSelection;
			base[System.Windows.Forms.Keys.Home] = FCTBAction.GoHome;
			base[System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Shift] = FCTBAction.GoHomeWithSelection;
			base[System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Control] = FCTBAction.GoFirstLine;
			base[System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control] = FCTBAction.GoFirstLineWithSelection;
			base[System.Windows.Forms.Keys.End] = FCTBAction.GoEnd;
			base[System.Windows.Forms.Keys.LButton | System.Windows.Forms.Keys.RButton | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Shift] = FCTBAction.GoEndWithSelection;
			base[System.Windows.Forms.Keys.LButton | System.Windows.Forms.Keys.RButton | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Control] = FCTBAction.GoLastLine;
			base[System.Windows.Forms.Keys.LButton | System.Windows.Forms.Keys.RButton | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control] = FCTBAction.GoLastLineWithSelection;
			base[System.Windows.Forms.Keys.Escape] = FCTBAction.ClearHints;
			base[(Keys)131149] = FCTBAction.MacroRecord;
			base[(Keys)131141] = FCTBAction.MacroExecute;
			base[System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Control] = FCTBAction.AutocompleteMenu;
			base[System.Windows.Forms.Keys.Tab] = FCTBAction.IndentIncrease;
			base[System.Windows.Forms.Keys.LButton | System.Windows.Forms.Keys.Back | System.Windows.Forms.Keys.Shift] = FCTBAction.IndentDecrease;
			base[(Keys)131181] = FCTBAction.ZoomOut;
			base[(Keys)131179] = FCTBAction.ZoomIn;
			base[System.Windows.Forms.Keys.ShiftKey | System.Windows.Forms.Keys.Space | System.Windows.Forms.Keys.Control] = FCTBAction.ZoomNormal;
			base[(Keys)131145] = FCTBAction.AutoIndentChars;
		}

		// Token: 0x06006335 RID: 25397 RVA: 0x001DAA88 File Offset: 0x001DAA88
		public override string ToString()
		{
			CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
			Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
			StringBuilder stringBuilder = new StringBuilder();
			KeysConverter keysConverter = new KeysConverter();
			foreach (KeyValuePair<Keys, FCTBAction> keyValuePair in this)
			{
				stringBuilder.AppendFormat("{0}={1}, ", keysConverter.ConvertToString(keyValuePair.Key), keyValuePair.Value);
			}
			bool flag = stringBuilder.Length > 1;
			if (flag)
			{
				stringBuilder.Remove(stringBuilder.Length - 2, 2);
			}
			Thread.CurrentThread.CurrentUICulture = currentUICulture;
			return stringBuilder.ToString();
		}

		// Token: 0x06006336 RID: 25398 RVA: 0x001DAB6C File Offset: 0x001DAB6C
		public static HotkeysMapping Parse(string s)
		{
			HotkeysMapping hotkeysMapping = new HotkeysMapping();
			hotkeysMapping.Clear();
			CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
			Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
			KeysConverter keysConverter = new KeysConverter();
			foreach (string text in s.Split(new char[]
			{
				','
			}))
			{
				string[] array2 = text.Split(new char[]
				{
					'='
				});
				Keys key = (Keys)keysConverter.ConvertFromString(array2[0].Trim());
				FCTBAction value = (FCTBAction)Enum.Parse(typeof(FCTBAction), array2[1].Trim());
				hotkeysMapping[key] = value;
			}
			Thread.CurrentThread.CurrentUICulture = currentUICulture;
			return hotkeysMapping;
		}
	}
}
