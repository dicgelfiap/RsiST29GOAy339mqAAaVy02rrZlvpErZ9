using System;
using System.Collections.Generic;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A3B RID: 2619
	public struct LineInfo
	{
		// Token: 0x060066B9 RID: 26297 RVA: 0x001F33E8 File Offset: 0x001F33E8
		public LineInfo(int startY)
		{
			this.cutOffPositions = null;
			this.VisibleState = VisibleState.Visible;
			this.startY = startY;
			this.bottomPadding = 0;
			this.wordWrapIndent = 0;
		}

		// Token: 0x170015B5 RID: 5557
		// (get) Token: 0x060066BA RID: 26298 RVA: 0x001F3410 File Offset: 0x001F3410
		public List<int> CutOffPositions
		{
			get
			{
				bool flag = this.cutOffPositions == null;
				if (flag)
				{
					this.cutOffPositions = new List<int>();
				}
				return this.cutOffPositions;
			}
		}

		// Token: 0x170015B6 RID: 5558
		// (get) Token: 0x060066BB RID: 26299 RVA: 0x001F344C File Offset: 0x001F344C
		public int WordWrapStringsCount
		{
			get
			{
				int result;
				switch (this.VisibleState)
				{
				case VisibleState.Visible:
				{
					bool flag = this.cutOffPositions == null;
					if (flag)
					{
						result = 1;
					}
					else
					{
						result = this.cutOffPositions.Count + 1;
					}
					break;
				}
				case VisibleState.StartOfHiddenBlock:
					result = 1;
					break;
				case VisibleState.Hidden:
					result = 0;
					break;
				default:
					result = 0;
					break;
				}
				return result;
			}
		}

		// Token: 0x060066BC RID: 26300 RVA: 0x001F34BC File Offset: 0x001F34BC
		internal int GetWordWrapStringStartPosition(int iWordWrapLine)
		{
			return (iWordWrapLine == 0) ? 0 : this.CutOffPositions[iWordWrapLine - 1];
		}

		// Token: 0x060066BD RID: 26301 RVA: 0x001F34F0 File Offset: 0x001F34F0
		internal int GetWordWrapStringFinishPosition(int iWordWrapLine, Line line)
		{
			bool flag = this.WordWrapStringsCount <= 0;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = ((iWordWrapLine == this.WordWrapStringsCount - 1) ? (line.Count - 1) : (this.CutOffPositions[iWordWrapLine] - 1));
			}
			return result;
		}

		// Token: 0x060066BE RID: 26302 RVA: 0x001F354C File Offset: 0x001F354C
		public int GetWordWrapStringIndex(int iChar)
		{
			bool flag = this.cutOffPositions == null || this.cutOffPositions.Count == 0;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				for (int i = 0; i < this.cutOffPositions.Count; i++)
				{
					bool flag2 = this.cutOffPositions[i] > iChar;
					if (flag2)
					{
						return i;
					}
				}
				result = this.cutOffPositions.Count;
			}
			return result;
		}

		// Token: 0x04003496 RID: 13462
		private List<int> cutOffPositions;

		// Token: 0x04003497 RID: 13463
		internal int startY;

		// Token: 0x04003498 RID: 13464
		internal int bottomPadding;

		// Token: 0x04003499 RID: 13465
		internal int wordWrapIndent;

		// Token: 0x0400349A RID: 13466
		public VisibleState VisibleState;
	}
}
