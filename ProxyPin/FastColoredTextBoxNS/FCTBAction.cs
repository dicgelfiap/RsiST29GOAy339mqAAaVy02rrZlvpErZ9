using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A0F RID: 2575
	public enum FCTBAction
	{
		// Token: 0x0400327E RID: 12926
		None,
		// Token: 0x0400327F RID: 12927
		AutocompleteMenu,
		// Token: 0x04003280 RID: 12928
		AutoIndentChars,
		// Token: 0x04003281 RID: 12929
		BookmarkLine,
		// Token: 0x04003282 RID: 12930
		ClearHints,
		// Token: 0x04003283 RID: 12931
		ClearWordLeft,
		// Token: 0x04003284 RID: 12932
		ClearWordRight,
		// Token: 0x04003285 RID: 12933
		CommentSelected,
		// Token: 0x04003286 RID: 12934
		Copy,
		// Token: 0x04003287 RID: 12935
		Cut,
		// Token: 0x04003288 RID: 12936
		DeleteCharRight,
		// Token: 0x04003289 RID: 12937
		FindChar,
		// Token: 0x0400328A RID: 12938
		FindDialog,
		// Token: 0x0400328B RID: 12939
		FindNext,
		// Token: 0x0400328C RID: 12940
		GoDown,
		// Token: 0x0400328D RID: 12941
		GoDownWithSelection,
		// Token: 0x0400328E RID: 12942
		GoDown_ColumnSelectionMode,
		// Token: 0x0400328F RID: 12943
		GoEnd,
		// Token: 0x04003290 RID: 12944
		GoEndWithSelection,
		// Token: 0x04003291 RID: 12945
		GoFirstLine,
		// Token: 0x04003292 RID: 12946
		GoFirstLineWithSelection,
		// Token: 0x04003293 RID: 12947
		GoHome,
		// Token: 0x04003294 RID: 12948
		GoHomeWithSelection,
		// Token: 0x04003295 RID: 12949
		GoLastLine,
		// Token: 0x04003296 RID: 12950
		GoLastLineWithSelection,
		// Token: 0x04003297 RID: 12951
		GoLeft,
		// Token: 0x04003298 RID: 12952
		GoLeftWithSelection,
		// Token: 0x04003299 RID: 12953
		GoLeft_ColumnSelectionMode,
		// Token: 0x0400329A RID: 12954
		GoPageDown,
		// Token: 0x0400329B RID: 12955
		GoPageDownWithSelection,
		// Token: 0x0400329C RID: 12956
		GoPageUp,
		// Token: 0x0400329D RID: 12957
		GoPageUpWithSelection,
		// Token: 0x0400329E RID: 12958
		GoRight,
		// Token: 0x0400329F RID: 12959
		GoRightWithSelection,
		// Token: 0x040032A0 RID: 12960
		GoRight_ColumnSelectionMode,
		// Token: 0x040032A1 RID: 12961
		GoToDialog,
		// Token: 0x040032A2 RID: 12962
		GoNextBookmark,
		// Token: 0x040032A3 RID: 12963
		GoPrevBookmark,
		// Token: 0x040032A4 RID: 12964
		GoUp,
		// Token: 0x040032A5 RID: 12965
		GoUpWithSelection,
		// Token: 0x040032A6 RID: 12966
		GoUp_ColumnSelectionMode,
		// Token: 0x040032A7 RID: 12967
		GoWordLeft,
		// Token: 0x040032A8 RID: 12968
		GoWordLeftWithSelection,
		// Token: 0x040032A9 RID: 12969
		GoWordRight,
		// Token: 0x040032AA RID: 12970
		GoWordRightWithSelection,
		// Token: 0x040032AB RID: 12971
		IndentIncrease,
		// Token: 0x040032AC RID: 12972
		IndentDecrease,
		// Token: 0x040032AD RID: 12973
		LowerCase,
		// Token: 0x040032AE RID: 12974
		MacroExecute,
		// Token: 0x040032AF RID: 12975
		MacroRecord,
		// Token: 0x040032B0 RID: 12976
		MoveSelectedLinesDown,
		// Token: 0x040032B1 RID: 12977
		MoveSelectedLinesUp,
		// Token: 0x040032B2 RID: 12978
		NavigateBackward,
		// Token: 0x040032B3 RID: 12979
		NavigateForward,
		// Token: 0x040032B4 RID: 12980
		Paste,
		// Token: 0x040032B5 RID: 12981
		Redo,
		// Token: 0x040032B6 RID: 12982
		ReplaceDialog,
		// Token: 0x040032B7 RID: 12983
		ReplaceMode,
		// Token: 0x040032B8 RID: 12984
		ScrollDown,
		// Token: 0x040032B9 RID: 12985
		ScrollUp,
		// Token: 0x040032BA RID: 12986
		SelectAll,
		// Token: 0x040032BB RID: 12987
		UnbookmarkLine,
		// Token: 0x040032BC RID: 12988
		Undo,
		// Token: 0x040032BD RID: 12989
		UpperCase,
		// Token: 0x040032BE RID: 12990
		ZoomIn,
		// Token: 0x040032BF RID: 12991
		ZoomNormal,
		// Token: 0x040032C0 RID: 12992
		ZoomOut,
		// Token: 0x040032C1 RID: 12993
		CustomAction1,
		// Token: 0x040032C2 RID: 12994
		CustomAction2,
		// Token: 0x040032C3 RID: 12995
		CustomAction3,
		// Token: 0x040032C4 RID: 12996
		CustomAction4,
		// Token: 0x040032C5 RID: 12997
		CustomAction5,
		// Token: 0x040032C6 RID: 12998
		CustomAction6,
		// Token: 0x040032C7 RID: 12999
		CustomAction7,
		// Token: 0x040032C8 RID: 13000
		CustomAction8,
		// Token: 0x040032C9 RID: 13001
		CustomAction9,
		// Token: 0x040032CA RID: 13002
		CustomAction10,
		// Token: 0x040032CB RID: 13003
		CustomAction11,
		// Token: 0x040032CC RID: 13004
		CustomAction12,
		// Token: 0x040032CD RID: 13005
		CustomAction13,
		// Token: 0x040032CE RID: 13006
		CustomAction14,
		// Token: 0x040032CF RID: 13007
		CustomAction15,
		// Token: 0x040032D0 RID: 13008
		CustomAction16,
		// Token: 0x040032D1 RID: 13009
		CustomAction17,
		// Token: 0x040032D2 RID: 13010
		CustomAction18,
		// Token: 0x040032D3 RID: 13011
		CustomAction19,
		// Token: 0x040032D4 RID: 13012
		CustomAction20
	}
}
