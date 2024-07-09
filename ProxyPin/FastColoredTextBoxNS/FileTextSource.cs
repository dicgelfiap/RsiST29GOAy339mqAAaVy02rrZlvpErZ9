using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A4D RID: 2637
	public class FileTextSource : TextSource, IDisposable
	{
		// Token: 0x14000052 RID: 82
		// (add) Token: 0x060067A5 RID: 26533 RVA: 0x001F8EDC File Offset: 0x001F8EDC
		// (remove) Token: 0x060067A6 RID: 26534 RVA: 0x001F8F18 File Offset: 0x001F8F18
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<LineNeededEventArgs> LineNeeded;

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x060067A7 RID: 26535 RVA: 0x001F8F54 File Offset: 0x001F8F54
		// (remove) Token: 0x060067A8 RID: 26536 RVA: 0x001F8F90 File Offset: 0x001F8F90
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<LinePushedEventArgs> LinePushed;

		// Token: 0x060067A9 RID: 26537 RVA: 0x001F8FCC File Offset: 0x001F8FCC
		public FileTextSource(FastColoredTextBox currentTB) : base(currentTB)
		{
			this.timer.Interval = 10000;
			this.timer.Tick += this.timer_Tick;
			this.timer.Enabled = true;
			this.SaveEOL = Environment.NewLine;
		}

		// Token: 0x060067AA RID: 26538 RVA: 0x001F9040 File Offset: 0x001F9040
		private void timer_Tick(object sender, EventArgs e)
		{
			this.timer.Enabled = false;
			try
			{
				this.UnloadUnusedLines();
			}
			finally
			{
				this.timer.Enabled = true;
			}
		}

		// Token: 0x060067AB RID: 26539 RVA: 0x001F908C File Offset: 0x001F908C
		private void UnloadUnusedLines()
		{
			int iLine = base.CurrentTB.VisibleRange.Start.iLine;
			int iLine2 = base.CurrentTB.VisibleRange.End.iLine;
			int num = 0;
			for (int i = 0; i < this.Count; i++)
			{
				bool flag = this.lines[i] != null && !this.lines[i].IsChanged && Math.Abs(i - iLine2) > 2000;
				if (flag)
				{
					this.lines[i] = null;
					num++;
				}
			}
		}

		// Token: 0x060067AC RID: 26540 RVA: 0x001F9140 File Offset: 0x001F9140
		public void OpenFile(string fileName, Encoding enc)
		{
			this.Clear();
			bool flag = this.fs != null;
			if (flag)
			{
				this.fs.Dispose();
			}
			this.SaveEOL = Environment.NewLine;
			this.fs = new FileStream(fileName, FileMode.Open);
			long length = this.fs.Length;
			enc = FileTextSource.DefineEncoding(enc, this.fs);
			int num = this.DefineShift(enc);
			this.sourceFileLinePositions.Add((int)this.fs.Position);
			this.lines.Add(null);
			this.sourceFileLinePositions.Capacity = (int)(length / 7L + 1000L);
			int num2 = 0;
			int item = 0;
			BinaryReader binaryReader = new BinaryReader(this.fs, enc);
			while (this.fs.Position < length)
			{
				item = (int)this.fs.Position;
				char c = binaryReader.ReadChar();
				bool flag2 = c == '\n';
				if (flag2)
				{
					this.sourceFileLinePositions.Add((int)this.fs.Position);
					this.lines.Add(null);
				}
				else
				{
					bool flag3 = num2 == 13;
					if (flag3)
					{
						this.sourceFileLinePositions.Add(item);
						this.lines.Add(null);
						this.SaveEOL = "\r";
					}
				}
				num2 = (int)c;
			}
			bool flag4 = num2 == 13;
			if (flag4)
			{
				this.sourceFileLinePositions.Add(item);
				this.lines.Add(null);
			}
			bool flag5 = length > 2000000L;
			if (flag5)
			{
				GC.Collect();
			}
			Line[] array = new Line[100];
			int count = this.lines.Count;
			this.lines.AddRange(array);
			this.lines.TrimExcess();
			this.lines.RemoveRange(count, array.Length);
			int[] collection = new int[100];
			count = this.lines.Count;
			this.sourceFileLinePositions.AddRange(collection);
			this.sourceFileLinePositions.TrimExcess();
			this.sourceFileLinePositions.RemoveRange(count, array.Length);
			this.fileEncoding = enc;
			this.OnLineInserted(0, this.Count);
			int num3 = Math.Min(this.lines.Count, base.CurrentTB.ClientRectangle.Height / base.CurrentTB.CharHeight);
			for (int i = 0; i < num3; i++)
			{
				this.LoadLineFromSourceFile(i);
			}
			this.NeedRecalc(new TextSource.TextChangedEventArgs(0, num3 - 1));
			bool wordWrap = base.CurrentTB.WordWrap;
			if (wordWrap)
			{
				this.OnRecalcWordWrap(new TextSource.TextChangedEventArgs(0, num3 - 1));
			}
		}

		// Token: 0x060067AD RID: 26541 RVA: 0x001F9414 File Offset: 0x001F9414
		private int DefineShift(Encoding enc)
		{
			bool isSingleByte = enc.IsSingleByte;
			int result;
			if (isSingleByte)
			{
				result = 0;
			}
			else
			{
				bool flag = enc.HeaderName == "unicodeFFFE";
				if (flag)
				{
					result = 0;
				}
				else
				{
					bool flag2 = enc.HeaderName == "utf-16";
					if (flag2)
					{
						result = 1;
					}
					else
					{
						bool flag3 = enc.HeaderName == "utf-32BE";
						if (flag3)
						{
							result = 0;
						}
						else
						{
							bool flag4 = enc.HeaderName == "utf-32";
							if (flag4)
							{
								result = 3;
							}
							else
							{
								result = 0;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060067AE RID: 26542 RVA: 0x001F94C0 File Offset: 0x001F94C0
		private static Encoding DefineEncoding(Encoding enc, FileStream fs)
		{
			int num = 0;
			byte[] array = new byte[4];
			int num2 = fs.Read(array, 0, 4);
			bool flag = array[0] == byte.MaxValue && array[1] == 254 && array[2] == 0 && array[3] == 0 && num2 >= 4;
			if (flag)
			{
				enc = Encoding.UTF32;
				num = 4;
			}
			else
			{
				bool flag2 = array[0] == 0 && array[1] == 0 && array[2] == 254 && array[3] == byte.MaxValue;
				if (flag2)
				{
					enc = new UTF32Encoding(true, true);
					num = 4;
				}
				else
				{
					bool flag3 = array[0] == 239 && array[1] == 187 && array[2] == 191;
					if (flag3)
					{
						enc = Encoding.UTF8;
						num = 3;
					}
					else
					{
						bool flag4 = array[0] == 254 && array[1] == byte.MaxValue;
						if (flag4)
						{
							enc = Encoding.BigEndianUnicode;
							num = 2;
						}
						else
						{
							bool flag5 = array[0] == byte.MaxValue && array[1] == 254;
							if (flag5)
							{
								enc = Encoding.Unicode;
								num = 2;
							}
						}
					}
				}
			}
			fs.Seek((long)num, SeekOrigin.Begin);
			return enc;
		}

		// Token: 0x060067AF RID: 26543 RVA: 0x001F963C File Offset: 0x001F963C
		public void CloseFile()
		{
			bool flag = this.fs != null;
			if (flag)
			{
				try
				{
					this.fs.Dispose();
				}
				catch
				{
				}
			}
			this.fs = null;
		}

		// Token: 0x170015DC RID: 5596
		// (get) Token: 0x060067B0 RID: 26544 RVA: 0x001F9690 File Offset: 0x001F9690
		// (set) Token: 0x060067B1 RID: 26545 RVA: 0x001F9698 File Offset: 0x001F9698
		public string SaveEOL { get; set; }

		// Token: 0x060067B2 RID: 26546 RVA: 0x001F96A4 File Offset: 0x001F96A4
		public override void SaveToFile(string fileName, Encoding enc)
		{
			List<int> list = new List<int>(this.Count);
			string directoryName = Path.GetDirectoryName(fileName);
			string text = Path.Combine(directoryName, Path.GetFileNameWithoutExtension(fileName) + ".tmp");
			StreamReader streamReader = new StreamReader(this.fs, this.fileEncoding);
			using (FileStream fileStream = new FileStream(text, FileMode.Create))
			{
				using (StreamWriter streamWriter = new StreamWriter(fileStream, enc))
				{
					streamWriter.Flush();
					for (int i = 0; i < this.Count; i++)
					{
						list.Add((int)fileStream.Length);
						string text2 = this.ReadLine(streamReader, i);
						bool flag = this.lines[i] != null && this.lines[i].IsChanged;
						bool flag2 = flag;
						string text3;
						if (flag2)
						{
							text3 = this.lines[i].Text;
						}
						else
						{
							text3 = text2;
						}
						bool flag3 = this.LinePushed != null;
						if (flag3)
						{
							LinePushedEventArgs linePushedEventArgs = new LinePushedEventArgs(text2, i, flag ? text3 : null);
							this.LinePushed(this, linePushedEventArgs);
							bool flag4 = linePushedEventArgs.SavedText != null;
							if (flag4)
							{
								text3 = linePushedEventArgs.SavedText;
							}
						}
						streamWriter.Write(text3);
						bool flag5 = i < this.Count - 1;
						if (flag5)
						{
							streamWriter.Write(this.SaveEOL);
						}
						streamWriter.Flush();
					}
				}
			}
			for (int j = 0; j < this.Count; j++)
			{
				this.lines[j] = null;
			}
			streamReader.Dispose();
			this.fs.Dispose();
			bool flag6 = File.Exists(fileName);
			if (flag6)
			{
				File.Delete(fileName);
			}
			File.Move(text, fileName);
			this.sourceFileLinePositions = list;
			this.fs = new FileStream(fileName, FileMode.Open);
			this.fileEncoding = enc;
		}

		// Token: 0x060067B3 RID: 26547 RVA: 0x001F9904 File Offset: 0x001F9904
		private string ReadLine(StreamReader sr, int i)
		{
			int num = this.sourceFileLinePositions[i];
			bool flag = num < 0;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				this.fs.Seek((long)num, SeekOrigin.Begin);
				sr.DiscardBufferedData();
				string text = sr.ReadLine();
				result = text;
			}
			return result;
		}

		// Token: 0x060067B4 RID: 26548 RVA: 0x001F9960 File Offset: 0x001F9960
		public override void ClearIsChanged()
		{
			foreach (Line line in this.lines)
			{
				bool flag = line != null;
				if (flag)
				{
					line.IsChanged = false;
				}
			}
		}

		// Token: 0x170015DD RID: 5597
		public override Line this[int i]
		{
			get
			{
				bool flag = this.lines[i] != null;
				Line result;
				if (flag)
				{
					result = this.lines[i];
				}
				else
				{
					this.LoadLineFromSourceFile(i);
					result = this.lines[i];
				}
				return result;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060067B7 RID: 26551 RVA: 0x001F9A28 File Offset: 0x001F9A28
		private void LoadLineFromSourceFile(int i)
		{
			Line line = this.CreateLine();
			this.fs.Seek((long)this.sourceFileLinePositions[i], SeekOrigin.Begin);
			StreamReader streamReader = new StreamReader(this.fs, this.fileEncoding);
			string text = streamReader.ReadLine();
			bool flag = text == null;
			if (flag)
			{
				text = "";
			}
			bool flag2 = this.LineNeeded != null;
			if (flag2)
			{
				LineNeededEventArgs lineNeededEventArgs = new LineNeededEventArgs(text, i);
				this.LineNeeded(this, lineNeededEventArgs);
				text = lineNeededEventArgs.DisplayedLineText;
				bool flag3 = text == null;
				if (flag3)
				{
					return;
				}
			}
			foreach (char c in text)
			{
				line.Add(new Char(c));
			}
			this.lines[i] = line;
			bool wordWrap = base.CurrentTB.WordWrap;
			if (wordWrap)
			{
				this.OnRecalcWordWrap(new TextSource.TextChangedEventArgs(i, i));
			}
		}

		// Token: 0x060067B8 RID: 26552 RVA: 0x001F9B34 File Offset: 0x001F9B34
		public override void InsertLine(int index, Line line)
		{
			this.sourceFileLinePositions.Insert(index, -1);
			base.InsertLine(index, line);
		}

		// Token: 0x060067B9 RID: 26553 RVA: 0x001F9B50 File Offset: 0x001F9B50
		public override void RemoveLine(int index, int count)
		{
			this.sourceFileLinePositions.RemoveRange(index, count);
			base.RemoveLine(index, count);
		}

		// Token: 0x060067BA RID: 26554 RVA: 0x001F9B6C File Offset: 0x001F9B6C
		public override void Clear()
		{
			base.Clear();
		}

		// Token: 0x060067BB RID: 26555 RVA: 0x001F9B78 File Offset: 0x001F9B78
		public override int GetLineLength(int i)
		{
			bool flag = this.lines[i] == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = this.lines[i].Count;
			}
			return result;
		}

		// Token: 0x060067BC RID: 26556 RVA: 0x001F9BC0 File Offset: 0x001F9BC0
		public override bool LineHasFoldingStartMarker(int iLine)
		{
			bool flag = this.lines[iLine] == null;
			return !flag && !string.IsNullOrEmpty(this.lines[iLine].FoldingStartMarker);
		}

		// Token: 0x060067BD RID: 26557 RVA: 0x001F9C10 File Offset: 0x001F9C10
		public override bool LineHasFoldingEndMarker(int iLine)
		{
			bool flag = this.lines[iLine] == null;
			return !flag && !string.IsNullOrEmpty(this.lines[iLine].FoldingEndMarker);
		}

		// Token: 0x060067BE RID: 26558 RVA: 0x001F9C60 File Offset: 0x001F9C60
		public override void Dispose()
		{
			bool flag = this.fs != null;
			if (flag)
			{
				this.fs.Dispose();
			}
			this.timer.Dispose();
		}

		// Token: 0x060067BF RID: 26559 RVA: 0x001F9C9C File Offset: 0x001F9C9C
		internal void UnloadLine(int iLine)
		{
			bool flag = this.lines[iLine] != null && !this.lines[iLine].IsChanged;
			if (flag)
			{
				this.lines[iLine] = null;
			}
		}

		// Token: 0x040034CD RID: 13517
		private List<int> sourceFileLinePositions = new List<int>();

		// Token: 0x040034CE RID: 13518
		private FileStream fs;

		// Token: 0x040034CF RID: 13519
		private Encoding fileEncoding;

		// Token: 0x040034D0 RID: 13520
		private Timer timer = new Timer();
	}
}
