using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A3F RID: 2623
	public class Range : IEnumerable<Place>, IEnumerable
	{
		// Token: 0x060066CD RID: 26317 RVA: 0x001F39A4 File Offset: 0x001F39A4
		public Range(FastColoredTextBox tb)
		{
			this.tb = tb;
		}

		// Token: 0x170015B8 RID: 5560
		// (get) Token: 0x060066CE RID: 26318 RVA: 0x001F39CC File Offset: 0x001F39CC
		public virtual bool IsEmpty
		{
			get
			{
				bool flag = this.ColumnSelectionMode;
				bool result;
				if (flag)
				{
					result = (this.Start.iChar == this.End.iChar);
				}
				else
				{
					result = (this.Start == this.End);
				}
				return result;
			}
		}

		// Token: 0x170015B9 RID: 5561
		// (get) Token: 0x060066CF RID: 26319 RVA: 0x001F3A24 File Offset: 0x001F3A24
		// (set) Token: 0x060066D0 RID: 26320 RVA: 0x001F3A44 File Offset: 0x001F3A44
		public bool ColumnSelectionMode
		{
			get
			{
				return this.columnSelectionMode;
			}
			set
			{
				this.columnSelectionMode = value;
			}
		}

		// Token: 0x060066D1 RID: 26321 RVA: 0x001F3A50 File Offset: 0x001F3A50
		public Range(FastColoredTextBox tb, int iStartChar, int iStartLine, int iEndChar, int iEndLine) : this(tb)
		{
			this.start = new Place(iStartChar, iStartLine);
			this.end = new Place(iEndChar, iEndLine);
		}

		// Token: 0x060066D2 RID: 26322 RVA: 0x001F3A78 File Offset: 0x001F3A78
		public Range(FastColoredTextBox tb, Place start, Place end) : this(tb)
		{
			this.start = start;
			this.end = end;
		}

		// Token: 0x060066D3 RID: 26323 RVA: 0x001F3A94 File Offset: 0x001F3A94
		public Range(FastColoredTextBox tb, int iLine) : this(tb)
		{
			this.start = new Place(0, iLine);
			this.end = new Place(tb[iLine].Count, iLine);
		}

		// Token: 0x060066D4 RID: 26324 RVA: 0x001F3AD4 File Offset: 0x001F3AD4
		public bool Contains(Place place)
		{
			bool flag = place.iLine < Math.Min(this.start.iLine, this.end.iLine);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = place.iLine > Math.Max(this.start.iLine, this.end.iLine);
				if (flag2)
				{
					result = false;
				}
				else
				{
					Place place2 = this.start;
					Place place3 = this.end;
					bool flag3 = place2.iLine > place3.iLine || (place2.iLine == place3.iLine && place2.iChar > place3.iChar);
					if (flag3)
					{
						Place place4 = place2;
						place2 = place3;
						place3 = place4;
					}
					bool flag4 = this.columnSelectionMode;
					if (flag4)
					{
						bool flag5 = place.iChar < place2.iChar || place.iChar > place3.iChar;
						if (flag5)
						{
							return false;
						}
					}
					else
					{
						bool flag6 = place.iLine == place2.iLine && place.iChar < place2.iChar;
						if (flag6)
						{
							return false;
						}
						bool flag7 = place.iLine == place3.iLine && place.iChar > place3.iChar;
						if (flag7)
						{
							return false;
						}
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060066D5 RID: 26325 RVA: 0x001F3C70 File Offset: 0x001F3C70
		public virtual Range GetIntersectionWith(Range range)
		{
			bool flag = this.ColumnSelectionMode;
			Range result;
			if (flag)
			{
				result = this.GetIntersectionWith_ColumnSelectionMode(range);
			}
			else
			{
				Range range2 = this.Clone();
				Range range3 = range.Clone();
				range2.Normalize();
				range3.Normalize();
				Place place = (range2.Start > range3.Start) ? range2.Start : range3.Start;
				Place place2 = (range2.End < range3.End) ? range2.End : range3.End;
				bool flag2 = place2 < place;
				if (flag2)
				{
					result = new Range(this.tb, this.start, this.start);
				}
				else
				{
					result = this.tb.GetRange(place, place2);
				}
			}
			return result;
		}

		// Token: 0x060066D6 RID: 26326 RVA: 0x001F3D50 File Offset: 0x001F3D50
		public Range GetUnionWith(Range range)
		{
			Range range2 = this.Clone();
			Range range3 = range.Clone();
			range2.Normalize();
			range3.Normalize();
			Place fromPlace = (range2.Start < range3.Start) ? range2.Start : range3.Start;
			Place toPlace = (range2.End > range3.End) ? range2.End : range3.End;
			return this.tb.GetRange(fromPlace, toPlace);
		}

		// Token: 0x060066D7 RID: 26327 RVA: 0x001F3DE4 File Offset: 0x001F3DE4
		public void SelectAll()
		{
			this.ColumnSelectionMode = false;
			this.Start = new Place(0, 0);
			bool flag = this.tb.LinesCount == 0;
			if (flag)
			{
				this.Start = new Place(0, 0);
			}
			else
			{
				this.end = new Place(0, 0);
				this.start = new Place(this.tb[this.tb.LinesCount - 1].Count, this.tb.LinesCount - 1);
			}
			bool flag2 = this == this.tb.Selection;
			if (flag2)
			{
				this.tb.Invalidate();
			}
		}

		// Token: 0x170015BA RID: 5562
		// (get) Token: 0x060066D8 RID: 26328 RVA: 0x001F3E9C File Offset: 0x001F3E9C
		// (set) Token: 0x060066D9 RID: 26329 RVA: 0x001F3EBC File Offset: 0x001F3EBC
		public Place Start
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = value;
				this.end = value;
				this.preferedPos = -1;
				this.OnSelectionChanged();
			}
		}

		// Token: 0x170015BB RID: 5563
		// (get) Token: 0x060066DA RID: 26330 RVA: 0x001F3EEC File Offset: 0x001F3EEC
		// (set) Token: 0x060066DB RID: 26331 RVA: 0x001F3F0C File Offset: 0x001F3F0C
		public Place End
		{
			get
			{
				return this.end;
			}
			set
			{
				this.end = value;
				this.OnSelectionChanged();
			}
		}

		// Token: 0x170015BC RID: 5564
		// (get) Token: 0x060066DC RID: 26332 RVA: 0x001F3F20 File Offset: 0x001F3F20
		public virtual string Text
		{
			get
			{
				bool flag = this.ColumnSelectionMode;
				string result;
				if (flag)
				{
					result = this.Text_ColumnSelectionMode;
				}
				else
				{
					int num = Math.Min(this.end.iLine, this.start.iLine);
					int num2 = Math.Max(this.end.iLine, this.start.iLine);
					int fromX = this.FromX;
					int toX = this.ToX;
					bool flag2 = num < 0;
					if (flag2)
					{
						result = null;
					}
					else
					{
						StringBuilder stringBuilder = new StringBuilder();
						for (int i = num; i <= num2; i++)
						{
							int num3 = (i == num) ? fromX : 0;
							int num4 = (i == num2) ? Math.Min(this.tb[i].Count - 1, toX - 1) : (this.tb[i].Count - 1);
							for (int j = num3; j <= num4; j++)
							{
								stringBuilder.Append(this.tb[i][j].c);
							}
							bool flag3 = i != num2 && num != num2;
							if (flag3)
							{
								stringBuilder.AppendLine();
							}
						}
						result = stringBuilder.ToString();
					}
				}
				return result;
			}
		}

		// Token: 0x170015BD RID: 5565
		// (get) Token: 0x060066DD RID: 26333 RVA: 0x001F4094 File Offset: 0x001F4094
		public int Length
		{
			get
			{
				bool flag = this.ColumnSelectionMode;
				int result;
				if (flag)
				{
					result = this.Length_ColumnSelectionMode(false);
				}
				else
				{
					int num = Math.Min(this.end.iLine, this.start.iLine);
					int num2 = Math.Max(this.end.iLine, this.start.iLine);
					int num3 = 0;
					bool flag2 = num < 0;
					if (flag2)
					{
						result = 0;
					}
					else
					{
						for (int i = num; i <= num2; i++)
						{
							int num4 = (i == num) ? this.FromX : 0;
							int num5 = (i == num2) ? Math.Min(this.tb[i].Count - 1, this.ToX - 1) : (this.tb[i].Count - 1);
							num3 += num5 - num4 + 1;
							bool flag3 = i != num2 && num != num2;
							if (flag3)
							{
								num3 += Environment.NewLine.Length;
							}
						}
						result = num3;
					}
				}
				return result;
			}
		}

		// Token: 0x170015BE RID: 5566
		// (get) Token: 0x060066DE RID: 26334 RVA: 0x001F41CC File Offset: 0x001F41CC
		public int TextLength
		{
			get
			{
				bool flag = this.ColumnSelectionMode;
				int result;
				if (flag)
				{
					result = this.Length_ColumnSelectionMode(true);
				}
				else
				{
					result = this.Length;
				}
				return result;
			}
		}

		// Token: 0x060066DF RID: 26335 RVA: 0x001F4208 File Offset: 0x001F4208
		internal void GetText(out string text, out List<Place> charIndexToPlace)
		{
			bool flag = this.tb.TextVersion == this.cachedTextVersion;
			if (flag)
			{
				text = this.cachedText;
				charIndexToPlace = this.cachedCharIndexToPlace;
			}
			else
			{
				int num = Math.Min(this.end.iLine, this.start.iLine);
				int num2 = Math.Max(this.end.iLine, this.start.iLine);
				int fromX = this.FromX;
				int toX = this.ToX;
				StringBuilder stringBuilder = new StringBuilder((num2 - num) * 50);
				charIndexToPlace = new List<Place>(stringBuilder.Capacity);
				bool flag2 = num >= 0;
				if (flag2)
				{
					for (int i = num; i <= num2; i++)
					{
						int num3 = (i == num) ? fromX : 0;
						int num4 = (i == num2) ? Math.Min(toX - 1, this.tb[i].Count - 1) : (this.tb[i].Count - 1);
						for (int j = num3; j <= num4; j++)
						{
							stringBuilder.Append(this.tb[i][j].c);
							charIndexToPlace.Add(new Place(j, i));
						}
						bool flag3 = i != num2 && num != num2;
						if (flag3)
						{
							foreach (char value in Environment.NewLine)
							{
								stringBuilder.Append(value);
								charIndexToPlace.Add(new Place(this.tb[i].Count, i));
							}
						}
					}
				}
				text = stringBuilder.ToString();
				charIndexToPlace.Add((this.End > this.Start) ? this.End : this.Start);
				this.cachedText = text;
				this.cachedCharIndexToPlace = charIndexToPlace;
				this.cachedTextVersion = this.tb.TextVersion;
			}
		}

		// Token: 0x170015BF RID: 5567
		// (get) Token: 0x060066E0 RID: 26336 RVA: 0x001F4454 File Offset: 0x001F4454
		public char CharAfterStart
		{
			get
			{
				bool flag = this.Start.iChar >= this.tb[this.Start.iLine].Count;
				char result;
				if (flag)
				{
					result = '\n';
				}
				else
				{
					result = this.tb[this.Start.iLine][this.Start.iChar].c;
				}
				return result;
			}
		}

		// Token: 0x170015C0 RID: 5568
		// (get) Token: 0x060066E1 RID: 26337 RVA: 0x001F44D4 File Offset: 0x001F44D4
		public char CharBeforeStart
		{
			get
			{
				bool flag = this.Start.iChar > this.tb[this.Start.iLine].Count;
				char result;
				if (flag)
				{
					result = '\n';
				}
				else
				{
					bool flag2 = this.Start.iChar <= 0;
					if (flag2)
					{
						result = '\n';
					}
					else
					{
						result = this.tb[this.Start.iLine][this.Start.iChar - 1].c;
					}
				}
				return result;
			}
		}

		// Token: 0x060066E2 RID: 26338 RVA: 0x001F4574 File Offset: 0x001F4574
		public string GetCharsBeforeStart(int charsCount)
		{
			int num = this.tb.PlaceToPosition(this.Start) - charsCount;
			bool flag = num < 0;
			if (flag)
			{
				num = 0;
			}
			return new Range(this.tb, this.tb.PositionToPlace(num), this.Start).Text;
		}

		// Token: 0x060066E3 RID: 26339 RVA: 0x001F45D0 File Offset: 0x001F45D0
		public string GetCharsAfterStart(int charsCount)
		{
			return this.GetCharsBeforeStart(-charsCount);
		}

		// Token: 0x060066E4 RID: 26340 RVA: 0x001F45F4 File Offset: 0x001F45F4
		public Range Clone()
		{
			return (Range)base.MemberwiseClone();
		}

		// Token: 0x170015C1 RID: 5569
		// (get) Token: 0x060066E5 RID: 26341 RVA: 0x001F4618 File Offset: 0x001F4618
		internal int FromX
		{
			get
			{
				bool flag = this.end.iLine < this.start.iLine;
				int result;
				if (flag)
				{
					result = this.end.iChar;
				}
				else
				{
					bool flag2 = this.end.iLine > this.start.iLine;
					if (flag2)
					{
						result = this.start.iChar;
					}
					else
					{
						result = Math.Min(this.end.iChar, this.start.iChar);
					}
				}
				return result;
			}
		}

		// Token: 0x170015C2 RID: 5570
		// (get) Token: 0x060066E6 RID: 26342 RVA: 0x001F46AC File Offset: 0x001F46AC
		internal int ToX
		{
			get
			{
				bool flag = this.end.iLine < this.start.iLine;
				int result;
				if (flag)
				{
					result = this.start.iChar;
				}
				else
				{
					bool flag2 = this.end.iLine > this.start.iLine;
					if (flag2)
					{
						result = this.end.iChar;
					}
					else
					{
						result = Math.Max(this.end.iChar, this.start.iChar);
					}
				}
				return result;
			}
		}

		// Token: 0x170015C3 RID: 5571
		// (get) Token: 0x060066E7 RID: 26343 RVA: 0x001F4740 File Offset: 0x001F4740
		public int FromLine
		{
			get
			{
				return Math.Min(this.Start.iLine, this.End.iLine);
			}
		}

		// Token: 0x170015C4 RID: 5572
		// (get) Token: 0x060066E8 RID: 26344 RVA: 0x001F4774 File Offset: 0x001F4774
		public int ToLine
		{
			get
			{
				return Math.Max(this.Start.iLine, this.End.iLine);
			}
		}

		// Token: 0x060066E9 RID: 26345 RVA: 0x001F47A8 File Offset: 0x001F47A8
		public bool GoRight()
		{
			Place p = this.start;
			this.GoRight(false);
			return p != this.start;
		}

		// Token: 0x060066EA RID: 26346 RVA: 0x001F47DC File Offset: 0x001F47DC
		public virtual bool GoRightThroughFolded()
		{
			bool flag = this.ColumnSelectionMode;
			bool result;
			if (flag)
			{
				result = this.GoRightThroughFolded_ColumnSelectionMode();
			}
			else
			{
				bool flag2 = this.start.iLine >= this.tb.LinesCount - 1 && this.start.iChar >= this.tb[this.tb.LinesCount - 1].Count;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = this.start.iChar < this.tb[this.start.iLine].Count;
					if (flag3)
					{
						this.start.Offset(1, 0);
					}
					else
					{
						this.start = new Place(0, this.start.iLine + 1);
					}
					this.preferedPos = -1;
					this.end = this.start;
					this.OnSelectionChanged();
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060066EB RID: 26347 RVA: 0x001F48E4 File Offset: 0x001F48E4
		public bool GoLeft()
		{
			this.ColumnSelectionMode = false;
			Place p = this.start;
			this.GoLeft(false);
			return p != this.start;
		}

		// Token: 0x060066EC RID: 26348 RVA: 0x001F4920 File Offset: 0x001F4920
		public bool GoLeftThroughFolded()
		{
			this.ColumnSelectionMode = false;
			bool flag = this.start.iChar == 0 && this.start.iLine == 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.start.iChar > 0;
				if (flag2)
				{
					this.start.Offset(-1, 0);
				}
				else
				{
					this.start = new Place(this.tb[this.start.iLine - 1].Count, this.start.iLine - 1);
				}
				this.preferedPos = -1;
				this.end = this.start;
				this.OnSelectionChanged();
				result = true;
			}
			return result;
		}

		// Token: 0x060066ED RID: 26349 RVA: 0x001F49EC File Offset: 0x001F49EC
		public void GoLeft(bool shift)
		{
			this.ColumnSelectionMode = false;
			bool flag = !shift;
			if (flag)
			{
				bool flag2 = this.start > this.end;
				if (flag2)
				{
					this.Start = this.End;
					return;
				}
			}
			bool flag3 = this.start.iChar != 0 || this.start.iLine != 0;
			if (flag3)
			{
				bool flag4 = this.start.iChar > 0 && this.tb.LineInfos[this.start.iLine].VisibleState == VisibleState.Visible;
				if (flag4)
				{
					this.start.Offset(-1, 0);
				}
				else
				{
					int num = this.tb.FindPrevVisibleLine(this.start.iLine);
					bool flag5 = num == this.start.iLine;
					if (flag5)
					{
						return;
					}
					this.start = new Place(this.tb[num].Count, num);
				}
			}
			bool flag6 = !shift;
			if (flag6)
			{
				this.end = this.start;
			}
			this.OnSelectionChanged();
			this.preferedPos = -1;
		}

		// Token: 0x060066EE RID: 26350 RVA: 0x001F4B3C File Offset: 0x001F4B3C
		public void GoRight(bool shift)
		{
			this.ColumnSelectionMode = false;
			bool flag = !shift;
			if (flag)
			{
				bool flag2 = this.start < this.end;
				if (flag2)
				{
					this.Start = this.End;
					return;
				}
			}
			bool flag3 = this.start.iLine < this.tb.LinesCount - 1 || this.start.iChar < this.tb[this.tb.LinesCount - 1].Count;
			if (flag3)
			{
				bool flag4 = this.start.iChar < this.tb[this.start.iLine].Count && this.tb.LineInfos[this.start.iLine].VisibleState == VisibleState.Visible;
				if (flag4)
				{
					this.start.Offset(1, 0);
				}
				else
				{
					int num = this.tb.FindNextVisibleLine(this.start.iLine);
					bool flag5 = num == this.start.iLine;
					if (flag5)
					{
						return;
					}
					this.start = new Place(0, num);
				}
			}
			bool flag6 = !shift;
			if (flag6)
			{
				this.end = this.start;
			}
			this.OnSelectionChanged();
			this.preferedPos = -1;
		}

		// Token: 0x060066EF RID: 26351 RVA: 0x001F4CBC File Offset: 0x001F4CBC
		internal void GoUp(bool shift)
		{
			this.ColumnSelectionMode = false;
			bool flag = !shift;
			if (flag)
			{
				bool flag2 = this.start.iLine > this.end.iLine;
				if (flag2)
				{
					this.Start = this.End;
					return;
				}
			}
			bool flag3 = this.preferedPos < 0;
			if (flag3)
			{
				this.preferedPos = this.start.iChar - this.tb.LineInfos[this.start.iLine].GetWordWrapStringStartPosition(this.tb.LineInfos[this.start.iLine].GetWordWrapStringIndex(this.start.iChar));
			}
			int num = this.tb.LineInfos[this.start.iLine].GetWordWrapStringIndex(this.start.iChar);
			bool flag4 = num == 0;
			if (flag4)
			{
				bool flag5 = this.start.iLine <= 0;
				if (flag5)
				{
					return;
				}
				int num2 = this.tb.FindPrevVisibleLine(this.start.iLine);
				bool flag6 = num2 == this.start.iLine;
				if (flag6)
				{
					return;
				}
				this.start.iLine = num2;
				num = this.tb.LineInfos[this.start.iLine].WordWrapStringsCount;
			}
			bool flag7 = num > 0;
			if (flag7)
			{
				int wordWrapStringFinishPosition = this.tb.LineInfos[this.start.iLine].GetWordWrapStringFinishPosition(num - 1, this.tb[this.start.iLine]);
				this.start.iChar = this.tb.LineInfos[this.start.iLine].GetWordWrapStringStartPosition(num - 1) + this.preferedPos;
				bool flag8 = this.start.iChar > wordWrapStringFinishPosition + 1;
				if (flag8)
				{
					this.start.iChar = wordWrapStringFinishPosition + 1;
				}
			}
			bool flag9 = !shift;
			if (flag9)
			{
				this.end = this.start;
			}
			this.OnSelectionChanged();
		}

		// Token: 0x060066F0 RID: 26352 RVA: 0x001F4F20 File Offset: 0x001F4F20
		internal void GoPageUp(bool shift)
		{
			this.ColumnSelectionMode = false;
			bool flag = this.preferedPos < 0;
			if (flag)
			{
				this.preferedPos = this.start.iChar - this.tb.LineInfos[this.start.iLine].GetWordWrapStringStartPosition(this.tb.LineInfos[this.start.iLine].GetWordWrapStringIndex(this.start.iChar));
			}
			int num = this.tb.ClientRectangle.Height / this.tb.CharHeight - 1;
			for (int i = 0; i < num; i++)
			{
				int num2 = this.tb.LineInfos[this.start.iLine].GetWordWrapStringIndex(this.start.iChar);
				bool flag2 = num2 == 0;
				if (flag2)
				{
					bool flag3 = this.start.iLine <= 0;
					if (flag3)
					{
						break;
					}
					int num3 = this.tb.FindPrevVisibleLine(this.start.iLine);
					bool flag4 = num3 == this.start.iLine;
					if (flag4)
					{
						break;
					}
					this.start.iLine = num3;
					num2 = this.tb.LineInfos[this.start.iLine].WordWrapStringsCount;
				}
				bool flag5 = num2 > 0;
				if (flag5)
				{
					int wordWrapStringFinishPosition = this.tb.LineInfos[this.start.iLine].GetWordWrapStringFinishPosition(num2 - 1, this.tb[this.start.iLine]);
					this.start.iChar = this.tb.LineInfos[this.start.iLine].GetWordWrapStringStartPosition(num2 - 1) + this.preferedPos;
					bool flag6 = this.start.iChar > wordWrapStringFinishPosition + 1;
					if (flag6)
					{
						this.start.iChar = wordWrapStringFinishPosition + 1;
					}
				}
			}
			bool flag7 = !shift;
			if (flag7)
			{
				this.end = this.start;
			}
			this.OnSelectionChanged();
		}

		// Token: 0x060066F1 RID: 26353 RVA: 0x001F5188 File Offset: 0x001F5188
		internal void GoDown(bool shift)
		{
			this.ColumnSelectionMode = false;
			bool flag = !shift;
			if (flag)
			{
				bool flag2 = this.start.iLine < this.end.iLine;
				if (flag2)
				{
					this.Start = this.End;
					return;
				}
			}
			bool flag3 = this.preferedPos < 0;
			if (flag3)
			{
				this.preferedPos = this.start.iChar - this.tb.LineInfos[this.start.iLine].GetWordWrapStringStartPosition(this.tb.LineInfos[this.start.iLine].GetWordWrapStringIndex(this.start.iChar));
			}
			int num = this.tb.LineInfos[this.start.iLine].GetWordWrapStringIndex(this.start.iChar);
			bool flag4 = num >= this.tb.LineInfos[this.start.iLine].WordWrapStringsCount - 1;
			if (flag4)
			{
				bool flag5 = this.start.iLine >= this.tb.LinesCount - 1;
				if (flag5)
				{
					return;
				}
				int num2 = this.tb.FindNextVisibleLine(this.start.iLine);
				bool flag6 = num2 == this.start.iLine;
				if (flag6)
				{
					return;
				}
				this.start.iLine = num2;
				num = -1;
			}
			bool flag7 = num < this.tb.LineInfos[this.start.iLine].WordWrapStringsCount - 1;
			if (flag7)
			{
				int wordWrapStringFinishPosition = this.tb.LineInfos[this.start.iLine].GetWordWrapStringFinishPosition(num + 1, this.tb[this.start.iLine]);
				this.start.iChar = this.tb.LineInfos[this.start.iLine].GetWordWrapStringStartPosition(num + 1) + this.preferedPos;
				bool flag8 = this.start.iChar > wordWrapStringFinishPosition + 1;
				if (flag8)
				{
					this.start.iChar = wordWrapStringFinishPosition + 1;
				}
			}
			bool flag9 = !shift;
			if (flag9)
			{
				this.end = this.start;
			}
			this.OnSelectionChanged();
		}

		// Token: 0x060066F2 RID: 26354 RVA: 0x001F5420 File Offset: 0x001F5420
		internal void GoPageDown(bool shift)
		{
			this.ColumnSelectionMode = false;
			bool flag = this.preferedPos < 0;
			if (flag)
			{
				this.preferedPos = this.start.iChar - this.tb.LineInfos[this.start.iLine].GetWordWrapStringStartPosition(this.tb.LineInfos[this.start.iLine].GetWordWrapStringIndex(this.start.iChar));
			}
			int num = this.tb.ClientRectangle.Height / this.tb.CharHeight - 1;
			for (int i = 0; i < num; i++)
			{
				int num2 = this.tb.LineInfos[this.start.iLine].GetWordWrapStringIndex(this.start.iChar);
				bool flag2 = num2 >= this.tb.LineInfos[this.start.iLine].WordWrapStringsCount - 1;
				if (flag2)
				{
					bool flag3 = this.start.iLine >= this.tb.LinesCount - 1;
					if (flag3)
					{
						break;
					}
					int num3 = this.tb.FindNextVisibleLine(this.start.iLine);
					bool flag4 = num3 == this.start.iLine;
					if (flag4)
					{
						break;
					}
					this.start.iLine = num3;
					num2 = -1;
				}
				bool flag5 = num2 < this.tb.LineInfos[this.start.iLine].WordWrapStringsCount - 1;
				if (flag5)
				{
					int wordWrapStringFinishPosition = this.tb.LineInfos[this.start.iLine].GetWordWrapStringFinishPosition(num2 + 1, this.tb[this.start.iLine]);
					this.start.iChar = this.tb.LineInfos[this.start.iLine].GetWordWrapStringStartPosition(num2 + 1) + this.preferedPos;
					bool flag6 = this.start.iChar > wordWrapStringFinishPosition + 1;
					if (flag6)
					{
						this.start.iChar = wordWrapStringFinishPosition + 1;
					}
				}
			}
			bool flag7 = !shift;
			if (flag7)
			{
				this.end = this.start;
			}
			this.OnSelectionChanged();
		}

		// Token: 0x060066F3 RID: 26355 RVA: 0x001F56BC File Offset: 0x001F56BC
		internal void GoHome(bool shift)
		{
			this.ColumnSelectionMode = false;
			bool flag = this.start.iLine < 0;
			if (!flag)
			{
				bool flag2 = this.tb.LineInfos[this.start.iLine].VisibleState > VisibleState.Visible;
				if (!flag2)
				{
					this.start = new Place(0, this.start.iLine);
					bool flag3 = !shift;
					if (flag3)
					{
						this.end = this.start;
					}
					this.OnSelectionChanged();
					this.preferedPos = -1;
				}
			}
		}

		// Token: 0x060066F4 RID: 26356 RVA: 0x001F575C File Offset: 0x001F575C
		internal void GoEnd(bool shift)
		{
			this.ColumnSelectionMode = false;
			bool flag = this.start.iLine < 0;
			if (!flag)
			{
				bool flag2 = this.tb.LineInfos[this.start.iLine].VisibleState > VisibleState.Visible;
				if (!flag2)
				{
					this.start = new Place(this.tb[this.start.iLine].Count, this.start.iLine);
					bool flag3 = !shift;
					if (flag3)
					{
						this.end = this.start;
					}
					this.OnSelectionChanged();
					this.preferedPos = -1;
				}
			}
		}

		// Token: 0x060066F5 RID: 26357 RVA: 0x001F5818 File Offset: 0x001F5818
		public void SetStyle(Style style)
		{
			int orSetStyleLayerIndex = this.tb.GetOrSetStyleLayerIndex(style);
			this.SetStyle(Range.ToStyleIndex(orSetStyleLayerIndex));
			this.tb.Invalidate();
		}

		// Token: 0x060066F6 RID: 26358 RVA: 0x001F5850 File Offset: 0x001F5850
		public void SetStyle(Style style, string regexPattern)
		{
			StyleIndex styleLayer = Range.ToStyleIndex(this.tb.GetOrSetStyleLayerIndex(style));
			this.SetStyle(styleLayer, regexPattern, RegexOptions.None);
		}

		// Token: 0x060066F7 RID: 26359 RVA: 0x001F5880 File Offset: 0x001F5880
		public void SetStyle(Style style, Regex regex)
		{
			StyleIndex styleLayer = Range.ToStyleIndex(this.tb.GetOrSetStyleLayerIndex(style));
			this.SetStyle(styleLayer, regex);
		}

		// Token: 0x060066F8 RID: 26360 RVA: 0x001F58B0 File Offset: 0x001F58B0
		public void SetStyle(Style style, string regexPattern, RegexOptions options)
		{
			StyleIndex styleLayer = Range.ToStyleIndex(this.tb.GetOrSetStyleLayerIndex(style));
			this.SetStyle(styleLayer, regexPattern, options);
		}

		// Token: 0x060066F9 RID: 26361 RVA: 0x001F58E0 File Offset: 0x001F58E0
		public void SetStyle(StyleIndex styleLayer, string regexPattern, RegexOptions options)
		{
			bool flag = Math.Abs(this.Start.iLine - this.End.iLine) > 1000;
			if (flag)
			{
				options |= SyntaxHighlighter.RegexCompiledOption;
			}
			foreach (Range range in this.GetRanges(regexPattern, options))
			{
				range.SetStyle(styleLayer);
			}
			this.tb.Invalidate();
		}

		// Token: 0x060066FA RID: 26362 RVA: 0x001F597C File Offset: 0x001F597C
		public void SetStyle(StyleIndex styleLayer, Regex regex)
		{
			foreach (Range range in this.GetRanges(regex))
			{
				range.SetStyle(styleLayer);
			}
			this.tb.Invalidate();
		}

		// Token: 0x060066FB RID: 26363 RVA: 0x001F59E4 File Offset: 0x001F59E4
		public void SetStyle(StyleIndex styleIndex)
		{
			int num = Math.Min(this.End.iLine, this.Start.iLine);
			int num2 = Math.Max(this.End.iLine, this.Start.iLine);
			int fromX = this.FromX;
			int toX = this.ToX;
			bool flag = num < 0;
			if (!flag)
			{
				for (int i = num; i <= num2; i++)
				{
					int num3 = (i == num) ? fromX : 0;
					int num4 = (i == num2) ? Math.Min(toX - 1, this.tb[i].Count - 1) : (this.tb[i].Count - 1);
					for (int j = num3; j <= num4; j++)
					{
						Char value = this.tb[i][j];
						value.style |= styleIndex;
						this.tb[i][j] = value;
					}
				}
			}
		}

		// Token: 0x060066FC RID: 26364 RVA: 0x001F5B18 File Offset: 0x001F5B18
		public void SetFoldingMarkers(string startFoldingPattern, string finishFoldingPattern)
		{
			this.SetFoldingMarkers(startFoldingPattern, finishFoldingPattern, SyntaxHighlighter.RegexCompiledOption);
		}

		// Token: 0x060066FD RID: 26365 RVA: 0x001F5B2C File Offset: 0x001F5B2C
		public void SetFoldingMarkers(string startFoldingPattern, string finishFoldingPattern, RegexOptions options)
		{
			bool flag = startFoldingPattern == finishFoldingPattern;
			if (flag)
			{
				this.SetFoldingMarkers(startFoldingPattern, options);
			}
			else
			{
				foreach (Range range in this.GetRanges(startFoldingPattern, options))
				{
					this.tb[range.Start.iLine].FoldingStartMarker = startFoldingPattern;
				}
				foreach (Range range2 in this.GetRanges(finishFoldingPattern, options))
				{
					this.tb[range2.Start.iLine].FoldingEndMarker = startFoldingPattern;
				}
				this.tb.Invalidate();
			}
		}

		// Token: 0x060066FE RID: 26366 RVA: 0x001F5C28 File Offset: 0x001F5C28
		public void SetFoldingMarkers(string foldingPattern, RegexOptions options)
		{
			foreach (Range range in this.GetRanges(foldingPattern, options))
			{
				bool flag = range.Start.iLine > 0;
				if (flag)
				{
					this.tb[range.Start.iLine - 1].FoldingEndMarker = foldingPattern;
				}
				this.tb[range.Start.iLine].FoldingStartMarker = foldingPattern;
			}
			this.tb.Invalidate();
		}

		// Token: 0x060066FF RID: 26367 RVA: 0x001F5CDC File Offset: 0x001F5CDC
		public IEnumerable<Range> GetRanges(string regexPattern)
		{
			return this.GetRanges(regexPattern, RegexOptions.None);
		}

		// Token: 0x06006700 RID: 26368 RVA: 0x001F5D00 File Offset: 0x001F5D00
		public IEnumerable<Range> GetRanges(string regexPattern, RegexOptions options)
		{
			string text;
			List<Place> charIndexToPlace;
			this.GetText(out text, out charIndexToPlace);
			Regex regex = new Regex(regexPattern, options);
			foreach (object obj in regex.Matches(text))
			{
				Match i = (Match)obj;
				Range r = new Range(this.tb);
				Group group = i.Groups["range"];
				bool flag = !group.Success;
				if (flag)
				{
					group = i.Groups[0];
				}
				r.Start = charIndexToPlace[group.Index];
				r.End = charIndexToPlace[group.Index + group.Length];
				yield return r;
				r = null;
				group = null;
				i = null;
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06006701 RID: 26369 RVA: 0x001F5D20 File Offset: 0x001F5D20
		public IEnumerable<Range> GetRangesByLines(string regexPattern, RegexOptions options)
		{
			Regex regex = new Regex(regexPattern, options);
			foreach (Range r in this.GetRangesByLines(regex))
			{
				yield return r;
				r = null;
			}
			IEnumerator<Range> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06006702 RID: 26370 RVA: 0x001F5D40 File Offset: 0x001F5D40
		public IEnumerable<Range> GetRangesByLines(Regex regex)
		{
			this.Normalize();
			FileTextSource fts = this.tb.TextSource as FileTextSource;
			int num;
			for (int iLine = this.Start.iLine; iLine <= this.End.iLine; iLine = num + 1)
			{
				bool isLineLoaded = fts == null || fts.IsLineLoaded(iLine);
				Range r = new Range(this.tb, new Place(0, iLine), new Place(this.tb[iLine].Count, iLine));
				bool flag = iLine == this.Start.iLine || iLine == this.End.iLine;
				if (flag)
				{
					r = r.GetIntersectionWith(this);
				}
				foreach (Range foundRange in r.GetRanges(regex))
				{
					yield return foundRange;
					foundRange = null;
				}
				IEnumerator<Range> enumerator = null;
				bool flag2 = !isLineLoaded;
				if (flag2)
				{
					fts.UnloadLine(iLine);
				}
				r = null;
				num = iLine;
			}
			yield break;
			yield break;
		}

		// Token: 0x06006703 RID: 26371 RVA: 0x001F5D58 File Offset: 0x001F5D58
		public IEnumerable<Range> GetRangesByLinesReversed(string regexPattern, RegexOptions options)
		{
			this.Normalize();
			Regex regex = new Regex(regexPattern, options);
			FileTextSource fts = this.tb.TextSource as FileTextSource;
			int num;
			for (int iLine = this.End.iLine; iLine >= this.Start.iLine; iLine = num - 1)
			{
				bool isLineLoaded = fts == null || fts.IsLineLoaded(iLine);
				Range r = new Range(this.tb, new Place(0, iLine), new Place(this.tb[iLine].Count, iLine));
				bool flag = iLine == this.Start.iLine || iLine == this.End.iLine;
				if (flag)
				{
					r = r.GetIntersectionWith(this);
				}
				List<Range> list = new List<Range>();
				foreach (Range foundRange in r.GetRanges(regex))
				{
					list.Add(foundRange);
					foundRange = null;
				}
				IEnumerator<Range> enumerator = null;
				for (int i = list.Count - 1; i >= 0; i = num - 1)
				{
					yield return list[i];
					num = i;
				}
				bool flag2 = !isLineLoaded;
				if (flag2)
				{
					fts.UnloadLine(iLine);
				}
				r = null;
				list = null;
				num = iLine;
			}
			yield break;
		}

		// Token: 0x06006704 RID: 26372 RVA: 0x001F5D78 File Offset: 0x001F5D78
		public IEnumerable<Range> GetRanges(Regex regex)
		{
			string text;
			List<Place> charIndexToPlace;
			this.GetText(out text, out charIndexToPlace);
			foreach (object obj in regex.Matches(text))
			{
				Match i = (Match)obj;
				Range r = new Range(this.tb);
				Group group = i.Groups["range"];
				bool flag = !group.Success;
				if (flag)
				{
					group = i.Groups[0];
				}
				r.Start = charIndexToPlace[group.Index];
				r.End = charIndexToPlace[group.Index + group.Length];
				yield return r;
				r = null;
				group = null;
				i = null;
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06006705 RID: 26373 RVA: 0x001F5D90 File Offset: 0x001F5D90
		public void ClearStyle(params Style[] styles)
		{
			try
			{
				this.ClearStyle(this.tb.GetStyleIndexMask(styles));
			}
			catch
			{
			}
		}

		// Token: 0x06006706 RID: 26374 RVA: 0x001F5DD4 File Offset: 0x001F5DD4
		public void ClearStyle(StyleIndex styleIndex)
		{
			int num = Math.Min(this.End.iLine, this.Start.iLine);
			int num2 = Math.Max(this.End.iLine, this.Start.iLine);
			int fromX = this.FromX;
			int toX = this.ToX;
			bool flag = num < 0;
			if (!flag)
			{
				for (int i = num; i <= num2; i++)
				{
					int num3 = (i == num) ? fromX : 0;
					int num4 = (i == num2) ? Math.Min(toX - 1, this.tb[i].Count - 1) : (this.tb[i].Count - 1);
					for (int j = num3; j <= num4; j++)
					{
						Char value = this.tb[i][j];
						value.style &= ~styleIndex;
						this.tb[i][j] = value;
					}
				}
				this.tb.Invalidate();
			}
		}

		// Token: 0x06006707 RID: 26375 RVA: 0x001F5F18 File Offset: 0x001F5F18
		public void ClearFoldingMarkers()
		{
			int num = Math.Min(this.End.iLine, this.Start.iLine);
			int num2 = Math.Max(this.End.iLine, this.Start.iLine);
			bool flag = num < 0;
			if (!flag)
			{
				for (int i = num; i <= num2; i++)
				{
					this.tb[i].ClearFoldingMarkers();
				}
				this.tb.Invalidate();
			}
		}

		// Token: 0x06006708 RID: 26376 RVA: 0x001F5FA8 File Offset: 0x001F5FA8
		private void OnSelectionChanged()
		{
			this.cachedTextVersion = -1;
			this.cachedText = null;
			this.cachedCharIndexToPlace = null;
			bool flag = this.tb.Selection == this;
			if (flag)
			{
				bool flag2 = this.updating == 0;
				if (flag2)
				{
					this.tb.OnSelectionChanged();
				}
			}
		}

		// Token: 0x06006709 RID: 26377 RVA: 0x001F6000 File Offset: 0x001F6000
		public void BeginUpdate()
		{
			this.updating++;
		}

		// Token: 0x0600670A RID: 26378 RVA: 0x001F6014 File Offset: 0x001F6014
		public void EndUpdate()
		{
			this.updating--;
			bool flag = this.updating == 0;
			if (flag)
			{
				this.OnSelectionChanged();
			}
		}

		// Token: 0x0600670B RID: 26379 RVA: 0x001F604C File Offset: 0x001F604C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Start: ",
				this.Start,
				" End: ",
				this.End
			});
		}

		// Token: 0x0600670C RID: 26380 RVA: 0x001F609C File Offset: 0x001F609C
		public void Normalize()
		{
			bool flag = this.Start > this.End;
			if (flag)
			{
				this.Inverse();
			}
		}

		// Token: 0x0600670D RID: 26381 RVA: 0x001F60D0 File Offset: 0x001F60D0
		public void Inverse()
		{
			Place place = this.start;
			this.start = this.end;
			this.end = place;
		}

		// Token: 0x0600670E RID: 26382 RVA: 0x001F60FC File Offset: 0x001F60FC
		public void Expand()
		{
			this.Normalize();
			this.start = new Place(0, this.start.iLine);
			this.end = new Place(this.tb.GetLineLength(this.end.iLine), this.end.iLine);
		}

		// Token: 0x0600670F RID: 26383 RVA: 0x001F6158 File Offset: 0x001F6158
		IEnumerator<Place> IEnumerable<Place>.GetEnumerator()
		{
			bool flag = this.ColumnSelectionMode;
			if (flag)
			{
				foreach (Place p in this.GetEnumerator_ColumnSelectionMode())
				{
					yield return p;
				}
				IEnumerator<Place> enumerator = null;
				yield break;
			}
			int fromLine = Math.Min(this.end.iLine, this.start.iLine);
			int toLine = Math.Max(this.end.iLine, this.start.iLine);
			int fromChar = this.FromX;
			int toChar = this.ToX;
			bool flag2 = fromLine < 0;
			if (flag2)
			{
				yield break;
			}
			int num;
			for (int y = fromLine; y <= toLine; y = num + 1)
			{
				int fromX = (y == fromLine) ? fromChar : 0;
				int toX = (y == toLine) ? Math.Min(toChar - 1, this.tb[y].Count - 1) : (this.tb[y].Count - 1);
				for (int x = fromX; x <= toX; x = num + 1)
				{
					yield return new Place(x, y);
					num = x;
				}
				num = y;
			}
			yield break;
			yield break;
		}

		// Token: 0x06006710 RID: 26384 RVA: 0x001F6168 File Offset: 0x001F6168
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<Place>)this).GetEnumerator();
		}

		// Token: 0x170015C5 RID: 5573
		// (get) Token: 0x06006711 RID: 26385 RVA: 0x001F6188 File Offset: 0x001F6188
		public IEnumerable<Char> Chars
		{
			get
			{
				bool flag = this.ColumnSelectionMode;
				if (flag)
				{
					foreach (Place p in this.GetEnumerator_ColumnSelectionMode())
					{
						yield return this.tb[p];
					}
					IEnumerator<Place> enumerator = null;
					yield break;
				}
				int fromLine = Math.Min(this.end.iLine, this.start.iLine);
				int toLine = Math.Max(this.end.iLine, this.start.iLine);
				int fromChar = this.FromX;
				int toChar = this.ToX;
				bool flag2 = fromLine < 0;
				if (flag2)
				{
					yield break;
				}
				int num;
				for (int y = fromLine; y <= toLine; y = num + 1)
				{
					int fromX = (y == fromLine) ? fromChar : 0;
					int toX = (y == toLine) ? Math.Min(toChar - 1, this.tb[y].Count - 1) : (this.tb[y].Count - 1);
					Line line = this.tb[y];
					for (int x = fromX; x <= toX; x = num + 1)
					{
						yield return line[x];
						num = x;
					}
					line = null;
					num = y;
				}
				yield break;
				yield break;
			}
		}

		// Token: 0x06006712 RID: 26386 RVA: 0x001F61AC File Offset: 0x001F61AC
		public Range GetFragment(string allowedSymbolsPattern)
		{
			return this.GetFragment(allowedSymbolsPattern, RegexOptions.None);
		}

		// Token: 0x06006713 RID: 26387 RVA: 0x001F61D0 File Offset: 0x001F61D0
		public Range GetFragment(Style style, bool allowLineBreaks)
		{
			StyleIndex styleIndexMask = this.tb.GetStyleIndexMask(new Style[]
			{
				style
			});
			Range range = new Range(this.tb);
			range.Start = this.Start;
			while (range.GoLeftThroughFolded())
			{
				bool flag = !allowLineBreaks && range.CharAfterStart == '\n';
				if (flag)
				{
					break;
				}
				bool flag2 = range.Start.iChar < this.tb.GetLineLength(range.Start.iLine);
				if (flag2)
				{
					bool flag3 = (this.tb[range.Start].style & styleIndexMask) == StyleIndex.None;
					if (flag3)
					{
						range.GoRightThroughFolded();
						break;
					}
				}
			}
			Place place = range.Start;
			range.Start = this.Start;
			do
			{
				bool flag4 = !allowLineBreaks && range.CharAfterStart == '\n';
				if (flag4)
				{
					break;
				}
				bool flag5 = range.Start.iChar < this.tb.GetLineLength(range.Start.iLine);
				if (flag5)
				{
					bool flag6 = (this.tb[range.Start].style & styleIndexMask) == StyleIndex.None;
					if (flag6)
					{
						break;
					}
				}
			}
			while (range.GoRightThroughFolded());
			Place place2 = range.Start;
			return new Range(this.tb, place, place2);
		}

		// Token: 0x06006714 RID: 26388 RVA: 0x001F6364 File Offset: 0x001F6364
		public Range GetFragment(string allowedSymbolsPattern, RegexOptions options)
		{
			Range range = new Range(this.tb);
			range.Start = this.Start;
			Regex regex = new Regex(allowedSymbolsPattern, options);
			while (range.GoLeftThroughFolded())
			{
				bool flag = !regex.IsMatch(range.CharAfterStart.ToString());
				if (flag)
				{
					range.GoRightThroughFolded();
					break;
				}
			}
			Place place = range.Start;
			range.Start = this.Start;
			do
			{
				bool flag2 = !regex.IsMatch(range.CharAfterStart.ToString());
				if (flag2)
				{
					break;
				}
			}
			while (range.GoRightThroughFolded());
			Place place2 = range.Start;
			return new Range(this.tb, place, place2);
		}

		// Token: 0x06006715 RID: 26389 RVA: 0x001F6440 File Offset: 0x001F6440
		private bool IsIdentifierChar(char c)
		{
			return char.IsLetterOrDigit(c) || c == '_';
		}

		// Token: 0x06006716 RID: 26390 RVA: 0x001F6470 File Offset: 0x001F6470
		private bool IsSpaceChar(char c)
		{
			return c == ' ' || c == '\t';
		}

		// Token: 0x06006717 RID: 26391 RVA: 0x001F649C File Offset: 0x001F649C
		public void GoWordLeft(bool shift)
		{
			this.ColumnSelectionMode = false;
			bool flag = !shift && this.start > this.end;
			if (flag)
			{
				this.Start = this.End;
			}
			else
			{
				Range range = this.Clone();
				bool flag2 = false;
				while (this.IsSpaceChar(range.CharBeforeStart))
				{
					flag2 = true;
					range.GoLeft(shift);
				}
				bool flag3 = false;
				while (this.IsIdentifierChar(range.CharBeforeStart))
				{
					flag3 = true;
					range.GoLeft(shift);
				}
				bool flag4 = !flag3 && (!flag2 || range.CharBeforeStart != '\n');
				if (flag4)
				{
					range.GoLeft(shift);
				}
				this.Start = range.Start;
				this.End = range.End;
				bool flag5 = this.tb.LineInfos[this.Start.iLine].VisibleState > VisibleState.Visible;
				if (flag5)
				{
					this.GoRight(shift);
				}
			}
		}

		// Token: 0x06006718 RID: 26392 RVA: 0x001F65C4 File Offset: 0x001F65C4
		public void GoWordRight(bool shift, bool goToStartOfNextWord = false)
		{
			this.ColumnSelectionMode = false;
			bool flag = !shift && this.start < this.end;
			if (flag)
			{
				this.Start = this.End;
			}
			else
			{
				Range range = this.Clone();
				bool flag2 = false;
				bool flag3 = range.CharAfterStart == '\n';
				if (flag3)
				{
					range.GoRight(shift);
					flag2 = true;
				}
				bool flag4 = false;
				while (this.IsSpaceChar(range.CharAfterStart))
				{
					flag4 = true;
					range.GoRight(shift);
				}
				bool flag5 = (!flag4 && !flag2) || !goToStartOfNextWord;
				if (flag5)
				{
					bool flag6 = false;
					while (this.IsIdentifierChar(range.CharAfterStart))
					{
						flag6 = true;
						range.GoRight(shift);
					}
					bool flag7 = !flag6;
					if (flag7)
					{
						range.GoRight(shift);
					}
					bool flag8 = goToStartOfNextWord && !flag4;
					if (flag8)
					{
						while (this.IsSpaceChar(range.CharAfterStart))
						{
							range.GoRight(shift);
						}
					}
				}
				this.Start = range.Start;
				this.End = range.End;
				bool flag9 = this.tb.LineInfos[this.Start.iLine].VisibleState > VisibleState.Visible;
				if (flag9)
				{
					this.GoLeft(shift);
				}
			}
		}

		// Token: 0x06006719 RID: 26393 RVA: 0x001F673C File Offset: 0x001F673C
		internal void GoFirst(bool shift)
		{
			this.ColumnSelectionMode = false;
			this.start = new Place(0, 0);
			bool flag = this.tb.LineInfos[this.Start.iLine].VisibleState > VisibleState.Visible;
			if (flag)
			{
				this.tb.ExpandBlock(this.Start.iLine);
			}
			bool flag2 = !shift;
			if (flag2)
			{
				this.end = this.start;
			}
			this.OnSelectionChanged();
		}

		// Token: 0x0600671A RID: 26394 RVA: 0x001F67C4 File Offset: 0x001F67C4
		internal void GoLast(bool shift)
		{
			this.ColumnSelectionMode = false;
			this.start = new Place(this.tb[this.tb.LinesCount - 1].Count, this.tb.LinesCount - 1);
			bool flag = this.tb.LineInfos[this.Start.iLine].VisibleState > VisibleState.Visible;
			if (flag)
			{
				this.tb.ExpandBlock(this.Start.iLine);
			}
			bool flag2 = !shift;
			if (flag2)
			{
				this.end = this.start;
			}
			this.OnSelectionChanged();
		}

		// Token: 0x0600671B RID: 26395 RVA: 0x001F6874 File Offset: 0x001F6874
		public static StyleIndex ToStyleIndex(int i)
		{
			return (StyleIndex)(1 << i);
		}

		// Token: 0x170015C6 RID: 5574
		// (get) Token: 0x0600671C RID: 26396 RVA: 0x001F6894 File Offset: 0x001F6894
		public RangeRect Bounds
		{
			get
			{
				int iStartChar = Math.Min(this.Start.iChar, this.End.iChar);
				int iStartLine = Math.Min(this.Start.iLine, this.End.iLine);
				int iEndChar = Math.Max(this.Start.iChar, this.End.iChar);
				int iEndLine = Math.Max(this.Start.iLine, this.End.iLine);
				return new RangeRect(iStartLine, iStartChar, iEndLine, iEndChar);
			}
		}

		// Token: 0x0600671D RID: 26397 RVA: 0x001F6928 File Offset: 0x001F6928
		public IEnumerable<Range> GetSubRanges(bool includeEmpty)
		{
			bool flag = !this.ColumnSelectionMode;
			if (flag)
			{
				yield return this;
				yield break;
			}
			RangeRect rect = this.Bounds;
			int num;
			for (int y = rect.iStartLine; y <= rect.iEndLine; y = num + 1)
			{
				bool flag2 = rect.iStartChar > this.tb[y].Count && !includeEmpty;
				if (!flag2)
				{
					Range r = new Range(this.tb, rect.iStartChar, y, Math.Min(rect.iEndChar, this.tb[y].Count), y);
					yield return r;
					r = null;
				}
				num = y;
			}
			yield break;
		}

		// Token: 0x170015C7 RID: 5575
		// (get) Token: 0x0600671E RID: 26398 RVA: 0x001F6940 File Offset: 0x001F6940
		// (set) Token: 0x0600671F RID: 26399 RVA: 0x001F6C18 File Offset: 0x001F6C18
		public bool ReadOnly
		{
			get
			{
				bool readOnly = this.tb.ReadOnly;
				bool result;
				if (readOnly)
				{
					result = true;
				}
				else
				{
					ReadOnlyStyle readOnlyStyle = null;
					foreach (Style style in this.tb.Styles)
					{
						bool flag = style is ReadOnlyStyle;
						if (flag)
						{
							readOnlyStyle = (ReadOnlyStyle)style;
							break;
						}
					}
					bool flag2 = readOnlyStyle != null;
					if (flag2)
					{
						StyleIndex styleIndex = Range.ToStyleIndex(this.tb.GetStyleIndex(readOnlyStyle));
						bool isEmpty = this.IsEmpty;
						if (isEmpty)
						{
							Line line = this.tb[this.start.iLine];
							bool flag3 = this.columnSelectionMode;
							if (flag3)
							{
								foreach (Range range in this.GetSubRanges(false))
								{
									line = this.tb[range.start.iLine];
									bool flag4 = range.start.iChar < line.Count && range.start.iChar > 0;
									if (flag4)
									{
										Char @char = line[range.start.iChar - 1];
										Char char2 = line[range.start.iChar];
										bool flag5 = (@char.style & styleIndex) != StyleIndex.None && (char2.style & styleIndex) > StyleIndex.None;
										if (flag5)
										{
											return true;
										}
									}
								}
							}
							else
							{
								bool flag6 = this.start.iChar < line.Count && this.start.iChar > 0;
								if (flag6)
								{
									Char char3 = line[this.start.iChar - 1];
									Char char4 = line[this.start.iChar];
									bool flag7 = (char3.style & styleIndex) != StyleIndex.None && (char4.style & styleIndex) > StyleIndex.None;
									if (flag7)
									{
										return true;
									}
								}
							}
						}
						else
						{
							foreach (Char char5 in this.Chars)
							{
								bool flag8 = (char5.style & styleIndex) > StyleIndex.None;
								if (flag8)
								{
									return true;
								}
							}
						}
					}
					result = false;
				}
				return result;
			}
			set
			{
				ReadOnlyStyle readOnlyStyle = null;
				foreach (Style style in this.tb.Styles)
				{
					bool flag = style is ReadOnlyStyle;
					if (flag)
					{
						readOnlyStyle = (ReadOnlyStyle)style;
						break;
					}
				}
				bool flag2 = readOnlyStyle == null;
				if (flag2)
				{
					readOnlyStyle = new ReadOnlyStyle();
				}
				if (value)
				{
					this.SetStyle(readOnlyStyle);
				}
				else
				{
					this.ClearStyle(new Style[]
					{
						readOnlyStyle
					});
				}
			}
		}

		// Token: 0x06006720 RID: 26400 RVA: 0x001F6CAC File Offset: 0x001F6CAC
		public bool IsReadOnlyLeftChar()
		{
			bool readOnly = this.tb.ReadOnly;
			bool result;
			if (readOnly)
			{
				result = true;
			}
			else
			{
				Range range = this.Clone();
				range.Normalize();
				bool flag = range.start.iChar == 0;
				if (flag)
				{
					result = false;
				}
				else
				{
					bool flag2 = this.ColumnSelectionMode;
					if (flag2)
					{
						range.GoLeft_ColumnSelectionMode();
					}
					else
					{
						range.GoLeft(true);
					}
					result = range.ReadOnly;
				}
			}
			return result;
		}

		// Token: 0x06006721 RID: 26401 RVA: 0x001F6D34 File Offset: 0x001F6D34
		public bool IsReadOnlyRightChar()
		{
			bool readOnly = this.tb.ReadOnly;
			bool result;
			if (readOnly)
			{
				result = true;
			}
			else
			{
				Range range = this.Clone();
				range.Normalize();
				bool flag = range.end.iChar >= this.tb[this.end.iLine].Count;
				if (flag)
				{
					result = false;
				}
				else
				{
					bool flag2 = this.ColumnSelectionMode;
					if (flag2)
					{
						range.GoRight_ColumnSelectionMode();
					}
					else
					{
						range.GoRight(true);
					}
					result = range.ReadOnly;
				}
			}
			return result;
		}

		// Token: 0x06006722 RID: 26402 RVA: 0x001F6DD8 File Offset: 0x001F6DD8
		public IEnumerable<Place> GetPlacesCyclic(Place startPlace, bool backward = false)
		{
			if (backward)
			{
				Range r = new Range(this.tb, startPlace, startPlace);
				while (r.GoLeft() && r.start >= this.Start)
				{
					bool flag = r.Start.iChar < this.tb[r.Start.iLine].Count;
					if (flag)
					{
						yield return r.Start;
					}
				}
				r = new Range(this.tb, this.End, this.End);
				while (r.GoLeft() && r.start >= startPlace)
				{
					bool flag2 = r.Start.iChar < this.tb[r.Start.iLine].Count;
					if (flag2)
					{
						yield return r.Start;
					}
				}
				r = null;
			}
			else
			{
				Range r2 = new Range(this.tb, startPlace, startPlace);
				bool flag3 = startPlace < this.End;
				if (flag3)
				{
					do
					{
						bool flag4 = r2.Start.iChar < this.tb[r2.Start.iLine].Count;
						if (flag4)
						{
							yield return r2.Start;
						}
					}
					while (r2.GoRight());
				}
				r2 = new Range(this.tb, this.Start, this.Start);
				bool flag5 = r2.Start < startPlace;
				if (flag5)
				{
					do
					{
						bool flag6 = r2.Start.iChar < this.tb[r2.Start.iLine].Count;
						if (flag6)
						{
							yield return r2.Start;
						}
					}
					while (r2.GoRight() && r2.Start < startPlace);
				}
				r2 = null;
			}
			yield break;
		}

		// Token: 0x06006723 RID: 26403 RVA: 0x001F6DF8 File Offset: 0x001F6DF8
		private Range GetIntersectionWith_ColumnSelectionMode(Range range)
		{
			bool flag = range.Start.iLine != range.End.iLine;
			Range result;
			if (flag)
			{
				result = new Range(this.tb, this.Start, this.Start);
			}
			else
			{
				RangeRect bounds = this.Bounds;
				bool flag2 = range.Start.iLine < bounds.iStartLine || range.Start.iLine > bounds.iEndLine;
				if (flag2)
				{
					result = new Range(this.tb, this.Start, this.Start);
				}
				else
				{
					result = new Range(this.tb, bounds.iStartChar, range.Start.iLine, bounds.iEndChar, range.Start.iLine).GetIntersectionWith(range);
				}
			}
			return result;
		}

		// Token: 0x06006724 RID: 26404 RVA: 0x001F6EE0 File Offset: 0x001F6EE0
		private bool GoRightThroughFolded_ColumnSelectionMode()
		{
			RangeRect bounds = this.Bounds;
			bool flag = true;
			for (int i = bounds.iStartLine; i <= bounds.iEndLine; i++)
			{
				bool flag2 = bounds.iEndChar < this.tb[i].Count;
				if (flag2)
				{
					flag = false;
					break;
				}
			}
			bool flag3 = flag;
			bool result;
			if (flag3)
			{
				result = false;
			}
			else
			{
				Place place = this.Start;
				Place place2 = this.End;
				place.Offset(1, 0);
				place2.Offset(1, 0);
				this.BeginUpdate();
				this.Start = place;
				this.End = place2;
				this.EndUpdate();
				result = true;
			}
			return result;
		}

		// Token: 0x06006725 RID: 26405 RVA: 0x001F6FA8 File Offset: 0x001F6FA8
		private IEnumerable<Place> GetEnumerator_ColumnSelectionMode()
		{
			RangeRect bounds = this.Bounds;
			bool flag = bounds.iStartLine < 0;
			if (flag)
			{
				yield break;
			}
			int num;
			for (int y = bounds.iStartLine; y <= bounds.iEndLine; y = num + 1)
			{
				for (int x = bounds.iStartChar; x < bounds.iEndChar; x = num + 1)
				{
					bool flag2 = x < this.tb[y].Count;
					if (flag2)
					{
						yield return new Place(x, y);
					}
					num = x;
				}
				num = y;
			}
			yield break;
		}

		// Token: 0x170015C8 RID: 5576
		// (get) Token: 0x06006726 RID: 26406 RVA: 0x001F6FB8 File Offset: 0x001F6FB8
		private string Text_ColumnSelectionMode
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				RangeRect bounds = this.Bounds;
				bool flag = bounds.iStartLine < 0;
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					for (int i = bounds.iStartLine; i <= bounds.iEndLine; i++)
					{
						for (int j = bounds.iStartChar; j < bounds.iEndChar; j++)
						{
							bool flag2 = j < this.tb[i].Count;
							if (flag2)
							{
								stringBuilder.Append(this.tb[i][j].c);
							}
						}
						bool flag3 = bounds.iEndLine != bounds.iStartLine && i != bounds.iEndLine;
						if (flag3)
						{
							stringBuilder.AppendLine();
						}
					}
					result = stringBuilder.ToString();
				}
				return result;
			}
		}

		// Token: 0x06006727 RID: 26407 RVA: 0x001F70C4 File Offset: 0x001F70C4
		private int Length_ColumnSelectionMode(bool withNewLines)
		{
			RangeRect bounds = this.Bounds;
			bool flag = bounds.iStartLine < 0;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				int num = 0;
				for (int i = bounds.iStartLine; i <= bounds.iEndLine; i++)
				{
					for (int j = bounds.iStartChar; j < bounds.iEndChar; j++)
					{
						bool flag2 = j < this.tb[i].Count;
						if (flag2)
						{
							num++;
						}
					}
					bool flag3 = withNewLines && bounds.iEndLine != bounds.iStartLine && i != bounds.iEndLine;
					if (flag3)
					{
						num += Environment.NewLine.Length;
					}
				}
				result = num;
			}
			return result;
		}

		// Token: 0x06006728 RID: 26408 RVA: 0x001F71B4 File Offset: 0x001F71B4
		internal void GoDown_ColumnSelectionMode()
		{
			int iLine = this.tb.FindNextVisibleLine(this.End.iLine);
			this.End = new Place(this.End.iChar, iLine);
		}

		// Token: 0x06006729 RID: 26409 RVA: 0x001F71F8 File Offset: 0x001F71F8
		internal void GoUp_ColumnSelectionMode()
		{
			int iLine = this.tb.FindPrevVisibleLine(this.End.iLine);
			this.End = new Place(this.End.iChar, iLine);
		}

		// Token: 0x0600672A RID: 26410 RVA: 0x001F723C File Offset: 0x001F723C
		internal void GoRight_ColumnSelectionMode()
		{
			this.End = new Place(this.End.iChar + 1, this.End.iLine);
		}

		// Token: 0x0600672B RID: 26411 RVA: 0x001F7264 File Offset: 0x001F7264
		internal void GoLeft_ColumnSelectionMode()
		{
			bool flag = this.End.iChar > 0;
			if (flag)
			{
				this.End = new Place(this.End.iChar - 1, this.End.iLine);
			}
		}

		// Token: 0x040034A5 RID: 13477
		private Place start;

		// Token: 0x040034A6 RID: 13478
		private Place end;

		// Token: 0x040034A7 RID: 13479
		public readonly FastColoredTextBox tb;

		// Token: 0x040034A8 RID: 13480
		private int preferedPos = -1;

		// Token: 0x040034A9 RID: 13481
		private int updating = 0;

		// Token: 0x040034AA RID: 13482
		private string cachedText;

		// Token: 0x040034AB RID: 13483
		private List<Place> cachedCharIndexToPlace;

		// Token: 0x040034AC RID: 13484
		private int cachedTextVersion = -1;

		// Token: 0x040034AD RID: 13485
		private bool columnSelectionMode;
	}
}
