using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Win32;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A18 RID: 2584
	public class FastColoredTextBox : UserControl, ISupportInitialize
	{
		// Token: 0x060063BA RID: 25530 RVA: 0x001DEEB8 File Offset: 0x001DEEB8
		public FastColoredTextBox()
		{
			TypeDescriptionProvider provider = TypeDescriptor.GetProvider(base.GetType());
			object value = provider.GetType().GetField("Provider", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(provider);
			bool flag = value.GetType() != typeof(FCTBDescriptionProvider);
			if (flag)
			{
				TypeDescriptor.AddProvider(new FCTBDescriptionProvider(base.GetType()), base.GetType());
			}
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.Font = new Font(FontFamily.GenericMonospace, 9.75f);
			this.InitTextSource(this.CreateTextSource());
			bool flag2 = this.lines.Count == 0;
			if (flag2)
			{
				this.lines.InsertLine(0, this.lines.CreateLine());
			}
			this.selection = new Range(this)
			{
				Start = new Place(0, 0)
			};
			this.Cursor = Cursors.IBeam;
			this.BackColor = Color.White;
			this.LineNumberColor = Color.Teal;
			this.IndentBackColor = Color.WhiteSmoke;
			this.ServiceLinesColor = Color.Silver;
			this.FoldingIndicatorColor = Color.Green;
			this.CurrentLineColor = Color.Transparent;
			this.ChangedLineColor = Color.Transparent;
			this.HighlightFoldingIndicator = true;
			this.ShowLineNumbers = true;
			this.TabLength = 4;
			this.FoldedBlockStyle = new FoldedBlockStyle(Brushes.Gray, null, FontStyle.Regular);
			this.SelectionColor = Color.Blue;
			this.BracketsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(80, Color.Lime)));
			this.BracketsStyle2 = new MarkerStyle(new SolidBrush(Color.FromArgb(60, Color.Red)));
			this.DelayedEventsInterval = 100;
			this.DelayedTextChangedInterval = 100;
			this.AllowSeveralTextStyleDrawing = false;
			this.LeftBracket = '\0';
			this.RightBracket = '\0';
			this.LeftBracket2 = '\0';
			this.RightBracket2 = '\0';
			this.SyntaxHighlighter = new SyntaxHighlighter(this);
			this.language = Language.Custom;
			this.PreferredLineWidth = 0;
			this.needRecalc = true;
			this.lastNavigatedDateTime = DateTime.Now;
			this.AutoIndent = true;
			this.AutoIndentExistingLines = true;
			this.CommentPrefix = "//";
			this.lineNumberStartValue = 1U;
			this.multiline = true;
			this.scrollBars = true;
			this.AcceptsTab = true;
			this.AcceptsReturn = true;
			this.caretVisible = true;
			this.CaretColor = Color.Black;
			this.WideCaret = false;
			this.Paddings = new Padding(0, 0, 0, 0);
			this.PaddingBackColor = Color.Transparent;
			this.DisabledColor = Color.FromArgb(100, 180, 180, 180);
			this.needRecalcFoldingLines = true;
			this.AllowDrop = true;
			this.FindEndOfFoldingBlockStrategy = FindEndOfFoldingBlockStrategy.Strategy1;
			this.VirtualSpace = false;
			this.bookmarks = new Bookmarks(this);
			this.BookmarkColor = Color.PowderBlue;
			this.ToolTip = new ToolTip();
			this.timer3.Interval = 500;
			this.hints = new Hints(this);
			this.SelectionHighlightingForLineBreaksEnabled = true;
			this.textAreaBorder = TextAreaBorderType.None;
			this.textAreaBorderColor = Color.Black;
			this.macrosManager = new MacrosManager(this);
			this.HotkeysMapping = new HotkeysMapping();
			this.HotkeysMapping.InitDefault();
			this.WordWrapAutoIndent = true;
			this.FoldedBlocks = new Dictionary<int, int>();
			this.AutoCompleteBrackets = false;
			this.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+\\s*(?<range>=)\\s*(?<range>[^;]+);";
			this.AutoIndentChars = true;
			this.CaretBlinking = true;
			this.ServiceColors = new ServiceColors();
			base.AutoScroll = true;
			this.timer.Tick += this.timer_Tick;
			this.timer2.Tick += this.timer2_Tick;
			this.timer3.Tick += this.timer3_Tick;
			this.middleClickScrollingTimer.Tick += this.middleClickScrollingTimer_Tick;
		}

		// Token: 0x170014FC RID: 5372
		// (get) Token: 0x060063BB RID: 25531 RVA: 0x001DF368 File Offset: 0x001DF368
		// (set) Token: 0x060063BC RID: 25532 RVA: 0x001DF388 File Offset: 0x001DF388
		public char[] AutoCompleteBracketsList
		{
			get
			{
				return this.autoCompleteBracketsList;
			}
			set
			{
				this.autoCompleteBracketsList = value;
			}
		}

		// Token: 0x170014FD RID: 5373
		// (get) Token: 0x060063BD RID: 25533 RVA: 0x001DF394 File Offset: 0x001DF394
		// (set) Token: 0x060063BE RID: 25534 RVA: 0x001DF39C File Offset: 0x001DF39C
		[DefaultValue(false)]
		[Description("AutoComplete brackets.")]
		public bool AutoCompleteBrackets { get; set; }

		// Token: 0x170014FE RID: 5374
		// (get) Token: 0x060063BF RID: 25535 RVA: 0x001DF3A8 File Offset: 0x001DF3A8
		// (set) Token: 0x060063C0 RID: 25536 RVA: 0x001DF3B0 File Offset: 0x001DF3B0
		[Browsable(true)]
		[Description("Colors of some service visual markers.")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public ServiceColors ServiceColors { get; set; }

		// Token: 0x170014FF RID: 5375
		// (get) Token: 0x060063C1 RID: 25537 RVA: 0x001DF3BC File Offset: 0x001DF3BC
		// (set) Token: 0x060063C2 RID: 25538 RVA: 0x001DF3C4 File Offset: 0x001DF3C4
		[Browsable(false)]
		public Dictionary<int, int> FoldedBlocks { get; private set; }

		// Token: 0x17001500 RID: 5376
		// (get) Token: 0x060063C3 RID: 25539 RVA: 0x001DF3D0 File Offset: 0x001DF3D0
		// (set) Token: 0x060063C4 RID: 25540 RVA: 0x001DF3D8 File Offset: 0x001DF3D8
		[DefaultValue(typeof(BracketsHighlightStrategy), "Strategy1")]
		[Description("Strategy of search of brackets to highlighting.")]
		public BracketsHighlightStrategy BracketsHighlightStrategy { get; set; }

		// Token: 0x17001501 RID: 5377
		// (get) Token: 0x060063C5 RID: 25541 RVA: 0x001DF3E4 File Offset: 0x001DF3E4
		// (set) Token: 0x060063C6 RID: 25542 RVA: 0x001DF3EC File Offset: 0x001DF3EC
		[DefaultValue(true)]
		[Description("Automatically shifts secondary wordwrap lines on the shift amount of the first line.")]
		public bool WordWrapAutoIndent { get; set; }

		// Token: 0x17001502 RID: 5378
		// (get) Token: 0x060063C7 RID: 25543 RVA: 0x001DF3F8 File Offset: 0x001DF3F8
		// (set) Token: 0x060063C8 RID: 25544 RVA: 0x001DF400 File Offset: 0x001DF400
		[DefaultValue(0)]
		[Description("Indent of secondary wordwrap lines (in chars).")]
		public int WordWrapIndent { get; set; }

		// Token: 0x17001503 RID: 5379
		// (get) Token: 0x060063C9 RID: 25545 RVA: 0x001DF40C File Offset: 0x001DF40C
		[Browsable(false)]
		public MacrosManager MacrosManager
		{
			get
			{
				return this.macrosManager;
			}
		}

		// Token: 0x17001504 RID: 5380
		// (get) Token: 0x060063CA RID: 25546 RVA: 0x001DF42C File Offset: 0x001DF42C
		// (set) Token: 0x060063CB RID: 25547 RVA: 0x001DF44C File Offset: 0x001DF44C
		[DefaultValue(true)]
		[Description("Allows drag and drop")]
		public override bool AllowDrop
		{
			get
			{
				return base.AllowDrop;
			}
			set
			{
				base.AllowDrop = value;
			}
		}

		// Token: 0x17001505 RID: 5381
		// (get) Token: 0x060063CC RID: 25548 RVA: 0x001DF458 File Offset: 0x001DF458
		// (set) Token: 0x060063CD RID: 25549 RVA: 0x001DF478 File Offset: 0x001DF478
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Hints Hints
		{
			get
			{
				return this.hints;
			}
			set
			{
				this.hints = value;
			}
		}

		// Token: 0x17001506 RID: 5382
		// (get) Token: 0x060063CE RID: 25550 RVA: 0x001DF484 File Offset: 0x001DF484
		// (set) Token: 0x060063CF RID: 25551 RVA: 0x001DF4A8 File Offset: 0x001DF4A8
		[Browsable(true)]
		[DefaultValue(500)]
		[Description("Delay(ms) of ToolTip.")]
		public int ToolTipDelay
		{
			get
			{
				return this.timer3.Interval;
			}
			set
			{
				this.timer3.Interval = value;
			}
		}

		// Token: 0x17001507 RID: 5383
		// (get) Token: 0x060063D0 RID: 25552 RVA: 0x001DF4B8 File Offset: 0x001DF4B8
		// (set) Token: 0x060063D1 RID: 25553 RVA: 0x001DF4C0 File Offset: 0x001DF4C0
		[Browsable(true)]
		[Description("ToolTip component.")]
		public ToolTip ToolTip { get; set; }

		// Token: 0x17001508 RID: 5384
		// (get) Token: 0x060063D2 RID: 25554 RVA: 0x001DF4CC File Offset: 0x001DF4CC
		// (set) Token: 0x060063D3 RID: 25555 RVA: 0x001DF4D4 File Offset: 0x001DF4D4
		[Browsable(true)]
		[DefaultValue(typeof(Color), "PowderBlue")]
		[Description("Color of bookmarks.")]
		public Color BookmarkColor { get; set; }

		// Token: 0x17001509 RID: 5385
		// (get) Token: 0x060063D4 RID: 25556 RVA: 0x001DF4E0 File Offset: 0x001DF4E0
		// (set) Token: 0x060063D5 RID: 25557 RVA: 0x001DF500 File Offset: 0x001DF500
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public BaseBookmarks Bookmarks
		{
			get
			{
				return this.bookmarks;
			}
			set
			{
				this.bookmarks = value;
			}
		}

		// Token: 0x1700150A RID: 5386
		// (get) Token: 0x060063D6 RID: 25558 RVA: 0x001DF50C File Offset: 0x001DF50C
		// (set) Token: 0x060063D7 RID: 25559 RVA: 0x001DF514 File Offset: 0x001DF514
		[DefaultValue(false)]
		[Description("Enables virtual spaces.")]
		public bool VirtualSpace { get; set; }

		// Token: 0x1700150B RID: 5387
		// (get) Token: 0x060063D8 RID: 25560 RVA: 0x001DF520 File Offset: 0x001DF520
		// (set) Token: 0x060063D9 RID: 25561 RVA: 0x001DF528 File Offset: 0x001DF528
		[DefaultValue(FindEndOfFoldingBlockStrategy.Strategy1)]
		[Description("Strategy of search of end of folding block.")]
		public FindEndOfFoldingBlockStrategy FindEndOfFoldingBlockStrategy { get; set; }

		// Token: 0x1700150C RID: 5388
		// (get) Token: 0x060063DA RID: 25562 RVA: 0x001DF534 File Offset: 0x001DF534
		// (set) Token: 0x060063DB RID: 25563 RVA: 0x001DF53C File Offset: 0x001DF53C
		[DefaultValue(true)]
		[Description("Indicates if tab characters are accepted as input.")]
		public bool AcceptsTab { get; set; }

		// Token: 0x1700150D RID: 5389
		// (get) Token: 0x060063DC RID: 25564 RVA: 0x001DF548 File Offset: 0x001DF548
		// (set) Token: 0x060063DD RID: 25565 RVA: 0x001DF550 File Offset: 0x001DF550
		[DefaultValue(true)]
		[Description("Indicates if return characters are accepted as input.")]
		public bool AcceptsReturn { get; set; }

		// Token: 0x1700150E RID: 5390
		// (get) Token: 0x060063DE RID: 25566 RVA: 0x001DF55C File Offset: 0x001DF55C
		// (set) Token: 0x060063DF RID: 25567 RVA: 0x001DF57C File Offset: 0x001DF57C
		[DefaultValue(true)]
		[Description("Shows or hides the caret")]
		public bool CaretVisible
		{
			get
			{
				return this.caretVisible;
			}
			set
			{
				this.caretVisible = value;
				this.Invalidate();
			}
		}

		// Token: 0x1700150F RID: 5391
		// (get) Token: 0x060063E0 RID: 25568 RVA: 0x001DF590 File Offset: 0x001DF590
		// (set) Token: 0x060063E1 RID: 25569 RVA: 0x001DF598 File Offset: 0x001DF598
		[DefaultValue(true)]
		[Description("Enables caret blinking")]
		public bool CaretBlinking { get; set; }

		// Token: 0x17001510 RID: 5392
		// (get) Token: 0x060063E2 RID: 25570 RVA: 0x001DF5A4 File Offset: 0x001DF5A4
		// (set) Token: 0x060063E3 RID: 25571 RVA: 0x001DF5AC File Offset: 0x001DF5AC
		[DefaultValue(false)]
		public bool ShowCaretWhenInactive { get; set; }

		// Token: 0x17001511 RID: 5393
		// (get) Token: 0x060063E4 RID: 25572 RVA: 0x001DF5B8 File Offset: 0x001DF5B8
		// (set) Token: 0x060063E5 RID: 25573 RVA: 0x001DF5D8 File Offset: 0x001DF5D8
		[DefaultValue(typeof(Color), "Black")]
		[Description("Color of border of text area")]
		public Color TextAreaBorderColor
		{
			get
			{
				return this.textAreaBorderColor;
			}
			set
			{
				this.textAreaBorderColor = value;
				this.Invalidate();
			}
		}

		// Token: 0x17001512 RID: 5394
		// (get) Token: 0x060063E6 RID: 25574 RVA: 0x001DF5EC File Offset: 0x001DF5EC
		// (set) Token: 0x060063E7 RID: 25575 RVA: 0x001DF60C File Offset: 0x001DF60C
		[DefaultValue(typeof(TextAreaBorderType), "None")]
		[Description("Type of border of text area")]
		public TextAreaBorderType TextAreaBorder
		{
			get
			{
				return this.textAreaBorder;
			}
			set
			{
				this.textAreaBorder = value;
				this.Invalidate();
			}
		}

		// Token: 0x17001513 RID: 5395
		// (get) Token: 0x060063E8 RID: 25576 RVA: 0x001DF620 File Offset: 0x001DF620
		// (set) Token: 0x060063E9 RID: 25577 RVA: 0x001DF640 File Offset: 0x001DF640
		[DefaultValue(typeof(Color), "Transparent")]
		[Description("Background color for current line. Set to Color.Transparent to hide current line highlighting")]
		public Color CurrentLineColor
		{
			get
			{
				return this.currentLineColor;
			}
			set
			{
				this.currentLineColor = value;
				this.Invalidate();
			}
		}

		// Token: 0x17001514 RID: 5396
		// (get) Token: 0x060063EA RID: 25578 RVA: 0x001DF654 File Offset: 0x001DF654
		// (set) Token: 0x060063EB RID: 25579 RVA: 0x001DF674 File Offset: 0x001DF674
		[DefaultValue(typeof(Color), "Transparent")]
		[Description("Background color for highlighting of changed lines. Set to Color.Transparent to hide changed line highlighting")]
		public Color ChangedLineColor
		{
			get
			{
				return this.changedLineColor;
			}
			set
			{
				this.changedLineColor = value;
				this.Invalidate();
			}
		}

		// Token: 0x17001515 RID: 5397
		// (get) Token: 0x060063EC RID: 25580 RVA: 0x001DF688 File Offset: 0x001DF688
		// (set) Token: 0x060063ED RID: 25581 RVA: 0x001DF6A8 File Offset: 0x001DF6A8
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
				this.lines.InitDefaultStyle();
				this.Invalidate();
			}
		}

		// Token: 0x17001516 RID: 5398
		// (get) Token: 0x060063EE RID: 25582 RVA: 0x001DF6C8 File Offset: 0x001DF6C8
		// (set) Token: 0x060063EF RID: 25583 RVA: 0x001DF6E8 File Offset: 0x001DF6E8
		[Browsable(false)]
		public int CharHeight
		{
			get
			{
				return this.charHeight;
			}
			set
			{
				this.charHeight = value;
				this.NeedRecalc();
				this.OnCharSizeChanged();
			}
		}

		// Token: 0x17001517 RID: 5399
		// (get) Token: 0x060063F0 RID: 25584 RVA: 0x001DF700 File Offset: 0x001DF700
		// (set) Token: 0x060063F1 RID: 25585 RVA: 0x001DF720 File Offset: 0x001DF720
		[Description("Interval between lines in pixels")]
		[DefaultValue(0)]
		public int LineInterval
		{
			get
			{
				return this.lineInterval;
			}
			set
			{
				this.lineInterval = value;
				this.SetFont(this.Font);
				this.Invalidate();
			}
		}

		// Token: 0x17001518 RID: 5400
		// (get) Token: 0x060063F2 RID: 25586 RVA: 0x001DF740 File Offset: 0x001DF740
		// (set) Token: 0x060063F3 RID: 25587 RVA: 0x001DF748 File Offset: 0x001DF748
		[Browsable(false)]
		public int CharWidth { get; set; }

		// Token: 0x17001519 RID: 5401
		// (get) Token: 0x060063F4 RID: 25588 RVA: 0x001DF754 File Offset: 0x001DF754
		// (set) Token: 0x060063F5 RID: 25589 RVA: 0x001DF75C File Offset: 0x001DF75C
		[DefaultValue(4)]
		[Description("Spaces count for tab")]
		public int TabLength { get; set; }

		// Token: 0x1700151A RID: 5402
		// (get) Token: 0x060063F6 RID: 25590 RVA: 0x001DF768 File Offset: 0x001DF768
		// (set) Token: 0x060063F7 RID: 25591 RVA: 0x001DF788 File Offset: 0x001DF788
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsChanged
		{
			get
			{
				return this.isChanged;
			}
			set
			{
				bool flag = !value;
				if (flag)
				{
					this.lines.ClearIsChanged();
				}
				this.isChanged = value;
			}
		}

		// Token: 0x1700151B RID: 5403
		// (get) Token: 0x060063F8 RID: 25592 RVA: 0x001DF7B8 File Offset: 0x001DF7B8
		// (set) Token: 0x060063F9 RID: 25593 RVA: 0x001DF7C0 File Offset: 0x001DF7C0
		[Browsable(false)]
		public int TextVersion { get; private set; }

		// Token: 0x1700151C RID: 5404
		// (get) Token: 0x060063FA RID: 25594 RVA: 0x001DF7CC File Offset: 0x001DF7CC
		// (set) Token: 0x060063FB RID: 25595 RVA: 0x001DF7D4 File Offset: 0x001DF7D4
		[DefaultValue(false)]
		public bool ReadOnly { get; set; }

		// Token: 0x1700151D RID: 5405
		// (get) Token: 0x060063FC RID: 25596 RVA: 0x001DF7E0 File Offset: 0x001DF7E0
		// (set) Token: 0x060063FD RID: 25597 RVA: 0x001DF800 File Offset: 0x001DF800
		[DefaultValue(true)]
		[Description("Shows line numbers.")]
		public bool ShowLineNumbers
		{
			get
			{
				return this.showLineNumbers;
			}
			set
			{
				this.showLineNumbers = value;
				this.NeedRecalc();
				this.Invalidate();
			}
		}

		// Token: 0x1700151E RID: 5406
		// (get) Token: 0x060063FE RID: 25598 RVA: 0x001DF818 File Offset: 0x001DF818
		// (set) Token: 0x060063FF RID: 25599 RVA: 0x001DF838 File Offset: 0x001DF838
		[DefaultValue(false)]
		[Description("Shows vertical lines between folding start line and folding end line.")]
		public bool ShowFoldingLines
		{
			get
			{
				return this.showFoldingLines;
			}
			set
			{
				this.showFoldingLines = value;
				this.Invalidate();
			}
		}

		// Token: 0x1700151F RID: 5407
		// (get) Token: 0x06006400 RID: 25600 RVA: 0x001DF84C File Offset: 0x001DF84C
		[Browsable(false)]
		public Rectangle TextAreaRect
		{
			get
			{
				int num = this.LeftIndent + this.maxLineLength * this.CharWidth + this.Paddings.Left + 1;
				num = Math.Max(base.ClientSize.Width - this.Paddings.Right, num);
				int num2 = this.TextHeight + this.Paddings.Top;
				num2 = Math.Max(base.ClientSize.Height - this.Paddings.Bottom, num2);
				int top = Math.Max(0, this.Paddings.Top - 1) - base.VerticalScroll.Value;
				int left = this.LeftIndent - base.HorizontalScroll.Value - 2 + Math.Max(0, this.Paddings.Left - 1);
				return Rectangle.FromLTRB(left, top, num - base.HorizontalScroll.Value, num2 - base.VerticalScroll.Value);
			}
		}

		// Token: 0x17001520 RID: 5408
		// (get) Token: 0x06006401 RID: 25601 RVA: 0x001DF964 File Offset: 0x001DF964
		// (set) Token: 0x06006402 RID: 25602 RVA: 0x001DF984 File Offset: 0x001DF984
		[DefaultValue(typeof(Color), "Teal")]
		[Description("Color of line numbers.")]
		public Color LineNumberColor
		{
			get
			{
				return this.lineNumberColor;
			}
			set
			{
				this.lineNumberColor = value;
				this.Invalidate();
			}
		}

		// Token: 0x17001521 RID: 5409
		// (get) Token: 0x06006403 RID: 25603 RVA: 0x001DF998 File Offset: 0x001DF998
		// (set) Token: 0x06006404 RID: 25604 RVA: 0x001DF9B8 File Offset: 0x001DF9B8
		[DefaultValue(typeof(uint), "1")]
		[Description("Start value of first line number.")]
		public uint LineNumberStartValue
		{
			get
			{
				return this.lineNumberStartValue;
			}
			set
			{
				this.lineNumberStartValue = value;
				this.needRecalc = true;
				this.Invalidate();
			}
		}

		// Token: 0x17001522 RID: 5410
		// (get) Token: 0x06006405 RID: 25605 RVA: 0x001DF9D0 File Offset: 0x001DF9D0
		// (set) Token: 0x06006406 RID: 25606 RVA: 0x001DF9F0 File Offset: 0x001DF9F0
		[DefaultValue(typeof(Color), "WhiteSmoke")]
		[Description("Background color of indent area")]
		public Color IndentBackColor
		{
			get
			{
				return this.indentBackColor;
			}
			set
			{
				this.indentBackColor = value;
				this.Invalidate();
			}
		}

		// Token: 0x17001523 RID: 5411
		// (get) Token: 0x06006407 RID: 25607 RVA: 0x001DFA04 File Offset: 0x001DFA04
		// (set) Token: 0x06006408 RID: 25608 RVA: 0x001DFA24 File Offset: 0x001DFA24
		[DefaultValue(typeof(Color), "Transparent")]
		[Description("Background color of padding area")]
		public Color PaddingBackColor
		{
			get
			{
				return this.paddingBackColor;
			}
			set
			{
				this.paddingBackColor = value;
				this.Invalidate();
			}
		}

		// Token: 0x17001524 RID: 5412
		// (get) Token: 0x06006409 RID: 25609 RVA: 0x001DFA38 File Offset: 0x001DFA38
		// (set) Token: 0x0600640A RID: 25610 RVA: 0x001DFA40 File Offset: 0x001DFA40
		[DefaultValue(typeof(Color), "100;180;180;180")]
		[Description("Color of disabled component")]
		public Color DisabledColor { get; set; }

		// Token: 0x17001525 RID: 5413
		// (get) Token: 0x0600640B RID: 25611 RVA: 0x001DFA4C File Offset: 0x001DFA4C
		// (set) Token: 0x0600640C RID: 25612 RVA: 0x001DFA54 File Offset: 0x001DFA54
		[DefaultValue(typeof(Color), "Black")]
		[Description("Color of caret.")]
		public Color CaretColor { get; set; }

		// Token: 0x17001526 RID: 5414
		// (get) Token: 0x0600640D RID: 25613 RVA: 0x001DFA60 File Offset: 0x001DFA60
		// (set) Token: 0x0600640E RID: 25614 RVA: 0x001DFA68 File Offset: 0x001DFA68
		[DefaultValue(false)]
		[Description("Wide caret.")]
		public bool WideCaret { get; set; }

		// Token: 0x17001527 RID: 5415
		// (get) Token: 0x0600640F RID: 25615 RVA: 0x001DFA74 File Offset: 0x001DFA74
		// (set) Token: 0x06006410 RID: 25616 RVA: 0x001DFA94 File Offset: 0x001DFA94
		[DefaultValue(typeof(Color), "Silver")]
		[Description("Color of service lines (folding lines, borders of blocks etc.)")]
		public Color ServiceLinesColor
		{
			get
			{
				return this.serviceLinesColor;
			}
			set
			{
				this.serviceLinesColor = value;
				this.Invalidate();
			}
		}

		// Token: 0x17001528 RID: 5416
		// (get) Token: 0x06006411 RID: 25617 RVA: 0x001DFAA8 File Offset: 0x001DFAA8
		// (set) Token: 0x06006412 RID: 25618 RVA: 0x001DFAB0 File Offset: 0x001DFAB0
		[Browsable(true)]
		[Description("Paddings of text area.")]
		public Padding Paddings { get; set; }

		// Token: 0x17001529 RID: 5417
		// (get) Token: 0x06006413 RID: 25619 RVA: 0x001DFABC File Offset: 0x001DFABC
		// (set) Token: 0x06006414 RID: 25620 RVA: 0x001DFAC4 File Offset: 0x001DFAC4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Padding Padding
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700152A RID: 5418
		// (get) Token: 0x06006415 RID: 25621 RVA: 0x001DFACC File Offset: 0x001DFACC
		// (set) Token: 0x06006416 RID: 25622 RVA: 0x001DFAD4 File Offset: 0x001DFAD4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool RightToLeft
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700152B RID: 5419
		// (get) Token: 0x06006417 RID: 25623 RVA: 0x001DFADC File Offset: 0x001DFADC
		// (set) Token: 0x06006418 RID: 25624 RVA: 0x001DFAFC File Offset: 0x001DFAFC
		[DefaultValue(typeof(Color), "Green")]
		[Description("Color of folding area indicator.")]
		public Color FoldingIndicatorColor
		{
			get
			{
				return this.foldingIndicatorColor;
			}
			set
			{
				this.foldingIndicatorColor = value;
				this.Invalidate();
			}
		}

		// Token: 0x1700152C RID: 5420
		// (get) Token: 0x06006419 RID: 25625 RVA: 0x001DFB10 File Offset: 0x001DFB10
		// (set) Token: 0x0600641A RID: 25626 RVA: 0x001DFB30 File Offset: 0x001DFB30
		[DefaultValue(true)]
		[Description("Enables folding indicator (left vertical line between folding bounds)")]
		public bool HighlightFoldingIndicator
		{
			get
			{
				return this.highlightFoldingIndicator;
			}
			set
			{
				this.highlightFoldingIndicator = value;
				this.Invalidate();
			}
		}

		// Token: 0x1700152D RID: 5421
		// (get) Token: 0x0600641B RID: 25627 RVA: 0x001DFB44 File Offset: 0x001DFB44
		// (set) Token: 0x0600641C RID: 25628 RVA: 0x001DFB4C File Offset: 0x001DFB4C
		[Browsable(false)]
		[Description("Left distance to text beginning.")]
		public int LeftIndent { get; private set; }

		// Token: 0x1700152E RID: 5422
		// (get) Token: 0x0600641D RID: 25629 RVA: 0x001DFB58 File Offset: 0x001DFB58
		// (set) Token: 0x0600641E RID: 25630 RVA: 0x001DFB78 File Offset: 0x001DFB78
		[DefaultValue(0)]
		[Description("Width of left service area (in pixels)")]
		public int LeftPadding
		{
			get
			{
				return this.leftPadding;
			}
			set
			{
				this.leftPadding = value;
				this.Invalidate();
			}
		}

		// Token: 0x1700152F RID: 5423
		// (get) Token: 0x0600641F RID: 25631 RVA: 0x001DFB8C File Offset: 0x001DFB8C
		// (set) Token: 0x06006420 RID: 25632 RVA: 0x001DFBAC File Offset: 0x001DFBAC
		[DefaultValue(0)]
		[Description("This property draws vertical line after defined char position. Set to 0 for disable drawing of vertical line.")]
		public int PreferredLineWidth
		{
			get
			{
				return this.preferredLineWidth;
			}
			set
			{
				this.preferredLineWidth = value;
				this.Invalidate();
			}
		}

		// Token: 0x17001530 RID: 5424
		// (get) Token: 0x06006421 RID: 25633 RVA: 0x001DFBC0 File Offset: 0x001DFBC0
		[Browsable(false)]
		public Style[] Styles
		{
			get
			{
				return this.lines.Styles;
			}
		}

		// Token: 0x17001531 RID: 5425
		// (get) Token: 0x06006422 RID: 25634 RVA: 0x001DFBE4 File Offset: 0x001DFBE4
		// (set) Token: 0x06006423 RID: 25635 RVA: 0x001DFC08 File Offset: 0x001DFC08
		[Description("Here you can change hotkeys for FastColoredTextBox.")]
		[Editor(typeof(HotkeysEditor), typeof(UITypeEditor))]
		[DefaultValue("Tab=IndentIncrease, Escape=ClearHints, PgUp=GoPageUp, PgDn=GoPageDown, End=GoEnd, Home=GoHome, Left=GoLeft, Up=GoUp, Right=GoRight, Down=GoDown, Ins=ReplaceMode, Del=DeleteCharRight, F3=FindNext, Shift+Tab=IndentDecrease, Shift+PgUp=GoPageUpWithSelection, Shift+PgDn=GoPageDownWithSelection, Shift+End=GoEndWithSelection, Shift+Home=GoHomeWithSelection, Shift+Left=GoLeftWithSelection, Shift+Up=GoUpWithSelection, Shift+Right=GoRightWithSelection, Shift+Down=GoDownWithSelection, Shift+Ins=Paste, Shift+Del=Cut, Ctrl+Back=ClearWordLeft, Ctrl+Space=AutocompleteMenu, Ctrl+End=GoLastLine, Ctrl+Home=GoFirstLine, Ctrl+Left=GoWordLeft, Ctrl+Up=ScrollUp, Ctrl+Right=GoWordRight, Ctrl+Down=ScrollDown, Ctrl+Ins=Copy, Ctrl+Del=ClearWordRight, Ctrl+0=ZoomNormal, Ctrl+A=SelectAll, Ctrl+B=BookmarkLine, Ctrl+C=Copy, Ctrl+E=MacroExecute, Ctrl+F=FindDialog, Ctrl+G=GoToDialog, Ctrl+H=ReplaceDialog, Ctrl+I=AutoIndentChars, Ctrl+M=MacroRecord, Ctrl+N=GoNextBookmark, Ctrl+R=Redo, Ctrl+U=UpperCase, Ctrl+V=Paste, Ctrl+X=Cut, Ctrl+Z=Undo, Ctrl+Add=ZoomIn, Ctrl+Subtract=ZoomOut, Ctrl+OemMinus=NavigateBackward, Ctrl+Shift+End=GoLastLineWithSelection, Ctrl+Shift+Home=GoFirstLineWithSelection, Ctrl+Shift+Left=GoWordLeftWithSelection, Ctrl+Shift+Right=GoWordRightWithSelection, Ctrl+Shift+B=UnbookmarkLine, Ctrl+Shift+C=CommentSelected, Ctrl+Shift+N=GoPrevBookmark, Ctrl+Shift+U=LowerCase, Ctrl+Shift+OemMinus=NavigateForward, Alt+Back=Undo, Alt+Up=MoveSelectedLinesUp, Alt+Down=MoveSelectedLinesDown, Alt+F=FindChar, Alt+Shift+Left=GoLeft_ColumnSelectionMode, Alt+Shift+Up=GoUp_ColumnSelectionMode, Alt+Shift+Right=GoRight_ColumnSelectionMode, Alt+Shift+Down=GoDown_ColumnSelectionMode")]
		public string Hotkeys
		{
			get
			{
				return this.HotkeysMapping.ToString();
			}
			set
			{
				this.HotkeysMapping = HotkeysMapping.Parse(value);
			}
		}

		// Token: 0x17001532 RID: 5426
		// (get) Token: 0x06006424 RID: 25636 RVA: 0x001DFC18 File Offset: 0x001DFC18
		// (set) Token: 0x06006425 RID: 25637 RVA: 0x001DFC20 File Offset: 0x001DFC20
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HotkeysMapping HotkeysMapping { get; set; }

		// Token: 0x17001533 RID: 5427
		// (get) Token: 0x06006426 RID: 25638 RVA: 0x001DFC2C File Offset: 0x001DFC2C
		// (set) Token: 0x06006427 RID: 25639 RVA: 0x001DFC50 File Offset: 0x001DFC50
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TextStyle DefaultStyle
		{
			get
			{
				return this.lines.DefaultStyle;
			}
			set
			{
				this.lines.DefaultStyle = value;
			}
		}

		// Token: 0x17001534 RID: 5428
		// (get) Token: 0x06006428 RID: 25640 RVA: 0x001DFC60 File Offset: 0x001DFC60
		// (set) Token: 0x06006429 RID: 25641 RVA: 0x001DFC68 File Offset: 0x001DFC68
		[Browsable(false)]
		public SelectionStyle SelectionStyle { get; set; }

		// Token: 0x17001535 RID: 5429
		// (get) Token: 0x0600642A RID: 25642 RVA: 0x001DFC74 File Offset: 0x001DFC74
		// (set) Token: 0x0600642B RID: 25643 RVA: 0x001DFC7C File Offset: 0x001DFC7C
		[Browsable(false)]
		public TextStyle FoldedBlockStyle { get; set; }

		// Token: 0x17001536 RID: 5430
		// (get) Token: 0x0600642C RID: 25644 RVA: 0x001DFC88 File Offset: 0x001DFC88
		// (set) Token: 0x0600642D RID: 25645 RVA: 0x001DFC90 File Offset: 0x001DFC90
		[Browsable(false)]
		public MarkerStyle BracketsStyle { get; set; }

		// Token: 0x17001537 RID: 5431
		// (get) Token: 0x0600642E RID: 25646 RVA: 0x001DFC9C File Offset: 0x001DFC9C
		// (set) Token: 0x0600642F RID: 25647 RVA: 0x001DFCA4 File Offset: 0x001DFCA4
		[Browsable(false)]
		public MarkerStyle BracketsStyle2 { get; set; }

		// Token: 0x17001538 RID: 5432
		// (get) Token: 0x06006430 RID: 25648 RVA: 0x001DFCB0 File Offset: 0x001DFCB0
		// (set) Token: 0x06006431 RID: 25649 RVA: 0x001DFCB8 File Offset: 0x001DFCB8
		[DefaultValue('\0')]
		[Description("Opening bracket for brackets highlighting. Set to '\\x0' for disable brackets highlighting.")]
		public char LeftBracket { get; set; }

		// Token: 0x17001539 RID: 5433
		// (get) Token: 0x06006432 RID: 25650 RVA: 0x001DFCC4 File Offset: 0x001DFCC4
		// (set) Token: 0x06006433 RID: 25651 RVA: 0x001DFCCC File Offset: 0x001DFCCC
		[DefaultValue('\0')]
		[Description("Closing bracket for brackets highlighting. Set to '\\x0' for disable brackets highlighting.")]
		public char RightBracket { get; set; }

		// Token: 0x1700153A RID: 5434
		// (get) Token: 0x06006434 RID: 25652 RVA: 0x001DFCD8 File Offset: 0x001DFCD8
		// (set) Token: 0x06006435 RID: 25653 RVA: 0x001DFCE0 File Offset: 0x001DFCE0
		[DefaultValue('\0')]
		[Description("Alternative opening bracket for brackets highlighting. Set to '\\x0' for disable brackets highlighting.")]
		public char LeftBracket2 { get; set; }

		// Token: 0x1700153B RID: 5435
		// (get) Token: 0x06006436 RID: 25654 RVA: 0x001DFCEC File Offset: 0x001DFCEC
		// (set) Token: 0x06006437 RID: 25655 RVA: 0x001DFCF4 File Offset: 0x001DFCF4
		[DefaultValue('\0')]
		[Description("Alternative closing bracket for brackets highlighting. Set to '\\x0' for disable brackets highlighting.")]
		public char RightBracket2 { get; set; }

		// Token: 0x1700153C RID: 5436
		// (get) Token: 0x06006438 RID: 25656 RVA: 0x001DFD00 File Offset: 0x001DFD00
		// (set) Token: 0x06006439 RID: 25657 RVA: 0x001DFD08 File Offset: 0x001DFD08
		[DefaultValue("//")]
		[Description("Comment line prefix.")]
		public string CommentPrefix { get; set; }

		// Token: 0x1700153D RID: 5437
		// (get) Token: 0x0600643A RID: 25658 RVA: 0x001DFD14 File Offset: 0x001DFD14
		// (set) Token: 0x0600643B RID: 25659 RVA: 0x001DFD1C File Offset: 0x001DFD1C
		[DefaultValue(typeof(HighlightingRangeType), "ChangedRange")]
		[Description("This property specifies which part of the text will be highlighted as you type.")]
		public HighlightingRangeType HighlightingRangeType { get; set; }

		// Token: 0x1700153E RID: 5438
		// (get) Token: 0x0600643C RID: 25660 RVA: 0x001DFD28 File Offset: 0x001DFD28
		// (set) Token: 0x0600643D RID: 25661 RVA: 0x001DFDA4 File Offset: 0x001DFDA4
		[Browsable(false)]
		public bool IsReplaceMode
		{
			get
			{
				return this.isReplaceMode && this.Selection.IsEmpty && !this.Selection.ColumnSelectionMode && this.Selection.Start.iChar < this.lines[this.Selection.Start.iLine].Count;
			}
			set
			{
				this.isReplaceMode = value;
			}
		}

		// Token: 0x1700153F RID: 5439
		// (get) Token: 0x0600643E RID: 25662 RVA: 0x001DFDB0 File Offset: 0x001DFDB0
		// (set) Token: 0x0600643F RID: 25663 RVA: 0x001DFDB8 File Offset: 0x001DFDB8
		[Browsable(true)]
		[DefaultValue(false)]
		[Description("Allows text rendering several styles same time.")]
		public bool AllowSeveralTextStyleDrawing { get; set; }

		// Token: 0x17001540 RID: 5440
		// (get) Token: 0x06006440 RID: 25664 RVA: 0x001DFDC4 File Offset: 0x001DFDC4
		// (set) Token: 0x06006441 RID: 25665 RVA: 0x001DFDE8 File Offset: 0x001DFDE8
		[Browsable(true)]
		[DefaultValue(true)]
		[Description("Allows to record macros.")]
		public bool AllowMacroRecording
		{
			get
			{
				return this.macrosManager.AllowMacroRecordingByUser;
			}
			set
			{
				this.macrosManager.AllowMacroRecordingByUser = value;
			}
		}

		// Token: 0x17001541 RID: 5441
		// (get) Token: 0x06006442 RID: 25666 RVA: 0x001DFDF8 File Offset: 0x001DFDF8
		// (set) Token: 0x06006443 RID: 25667 RVA: 0x001DFE00 File Offset: 0x001DFE00
		[DefaultValue(true)]
		[Description("Allows auto indent. Inserts spaces before line chars.")]
		public bool AutoIndent { get; set; }

		// Token: 0x17001542 RID: 5442
		// (get) Token: 0x06006444 RID: 25668 RVA: 0x001DFE0C File Offset: 0x001DFE0C
		// (set) Token: 0x06006445 RID: 25669 RVA: 0x001DFE14 File Offset: 0x001DFE14
		[DefaultValue(true)]
		[Description("Does autoindenting in existing lines. It works only if AutoIndent is True.")]
		public bool AutoIndentExistingLines { get; set; }

		// Token: 0x17001543 RID: 5443
		// (get) Token: 0x06006446 RID: 25670 RVA: 0x001DFE20 File Offset: 0x001DFE20
		// (set) Token: 0x06006447 RID: 25671 RVA: 0x001DFE44 File Offset: 0x001DFE44
		[Browsable(true)]
		[DefaultValue(100)]
		[Description("Minimal delay(ms) for delayed events (except TextChangedDelayed).")]
		public int DelayedEventsInterval
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

		// Token: 0x17001544 RID: 5444
		// (get) Token: 0x06006448 RID: 25672 RVA: 0x001DFE54 File Offset: 0x001DFE54
		// (set) Token: 0x06006449 RID: 25673 RVA: 0x001DFE78 File Offset: 0x001DFE78
		[Browsable(true)]
		[DefaultValue(100)]
		[Description("Minimal delay(ms) for TextChangedDelayed event.")]
		public int DelayedTextChangedInterval
		{
			get
			{
				return this.timer2.Interval;
			}
			set
			{
				this.timer2.Interval = value;
			}
		}

		// Token: 0x17001545 RID: 5445
		// (get) Token: 0x0600644A RID: 25674 RVA: 0x001DFE88 File Offset: 0x001DFE88
		// (set) Token: 0x0600644B RID: 25675 RVA: 0x001DFEA8 File Offset: 0x001DFEA8
		[Browsable(true)]
		[DefaultValue(typeof(Language), "Custom")]
		[Description("Language for highlighting by built-in highlighter.")]
		public Language Language
		{
			get
			{
				return this.language;
			}
			set
			{
				this.language = value;
				bool flag = this.SyntaxHighlighter != null;
				if (flag)
				{
					this.SyntaxHighlighter.InitStyleSchema(this.language);
				}
				this.Invalidate();
			}
		}

		// Token: 0x17001546 RID: 5446
		// (get) Token: 0x0600644C RID: 25676 RVA: 0x001DFEEC File Offset: 0x001DFEEC
		// (set) Token: 0x0600644D RID: 25677 RVA: 0x001DFEF4 File Offset: 0x001DFEF4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SyntaxHighlighter SyntaxHighlighter { get; set; }

		// Token: 0x17001547 RID: 5447
		// (get) Token: 0x0600644E RID: 25678 RVA: 0x001DFF00 File Offset: 0x001DFF00
		// (set) Token: 0x0600644F RID: 25679 RVA: 0x001DFF20 File Offset: 0x001DFF20
		[Browsable(true)]
		[DefaultValue(null)]
		[Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
		[Description("XML file with description of syntax highlighting. This property works only with Language == Language.Custom.")]
		public string DescriptionFile
		{
			get
			{
				return this.descriptionFile;
			}
			set
			{
				this.descriptionFile = value;
				this.Invalidate();
			}
		}

		// Token: 0x17001548 RID: 5448
		// (get) Token: 0x06006450 RID: 25680 RVA: 0x001DFF34 File Offset: 0x001DFF34
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Range LeftBracketPosition
		{
			get
			{
				return this.leftBracketPosition;
			}
		}

		// Token: 0x17001549 RID: 5449
		// (get) Token: 0x06006451 RID: 25681 RVA: 0x001DFF54 File Offset: 0x001DFF54
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Range RightBracketPosition
		{
			get
			{
				return this.rightBracketPosition;
			}
		}

		// Token: 0x1700154A RID: 5450
		// (get) Token: 0x06006452 RID: 25682 RVA: 0x001DFF74 File Offset: 0x001DFF74
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Range LeftBracketPosition2
		{
			get
			{
				return this.leftBracketPosition2;
			}
		}

		// Token: 0x1700154B RID: 5451
		// (get) Token: 0x06006453 RID: 25683 RVA: 0x001DFF94 File Offset: 0x001DFF94
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Range RightBracketPosition2
		{
			get
			{
				return this.rightBracketPosition2;
			}
		}

		// Token: 0x1700154C RID: 5452
		// (get) Token: 0x06006454 RID: 25684 RVA: 0x001DFFB4 File Offset: 0x001DFFB4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int StartFoldingLine
		{
			get
			{
				return this.startFoldingLine;
			}
		}

		// Token: 0x1700154D RID: 5453
		// (get) Token: 0x06006455 RID: 25685 RVA: 0x001DFFD4 File Offset: 0x001DFFD4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int EndFoldingLine
		{
			get
			{
				return this.endFoldingLine;
			}
		}

		// Token: 0x1700154E RID: 5454
		// (get) Token: 0x06006456 RID: 25686 RVA: 0x001DFFF4 File Offset: 0x001DFFF4
		// (set) Token: 0x06006457 RID: 25687 RVA: 0x001E0014 File Offset: 0x001E0014
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TextSource TextSource
		{
			get
			{
				return this.lines;
			}
			set
			{
				this.InitTextSource(value);
			}
		}

		// Token: 0x1700154F RID: 5455
		// (get) Token: 0x06006458 RID: 25688 RVA: 0x001E0020 File Offset: 0x001E0020
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool HasSourceTextBox
		{
			get
			{
				return this.SourceTextBox != null;
			}
		}

		// Token: 0x17001550 RID: 5456
		// (get) Token: 0x06006459 RID: 25689 RVA: 0x001E0044 File Offset: 0x001E0044
		// (set) Token: 0x0600645A RID: 25690 RVA: 0x001E0064 File Offset: 0x001E0064
		[Browsable(true)]
		[DefaultValue(null)]
		[Description("Allows to get text from other FastColoredTextBox.")]
		public FastColoredTextBox SourceTextBox
		{
			get
			{
				return this.sourceTextBox;
			}
			set
			{
				bool flag = value == this.sourceTextBox;
				if (!flag)
				{
					this.sourceTextBox = value;
					bool flag2 = this.sourceTextBox == null;
					if (flag2)
					{
						this.InitTextSource(this.CreateTextSource());
						this.lines.InsertLine(0, this.TextSource.CreateLine());
						this.IsChanged = false;
					}
					else
					{
						this.InitTextSource(this.SourceTextBox.TextSource);
						this.isChanged = false;
					}
					this.Invalidate();
				}
			}
		}

		// Token: 0x17001551 RID: 5457
		// (get) Token: 0x0600645B RID: 25691 RVA: 0x001E00F8 File Offset: 0x001E00F8
		[Browsable(false)]
		public Range VisibleRange
		{
			get
			{
				bool flag = this.visibleRange != null;
				Range range;
				if (flag)
				{
					range = this.visibleRange;
				}
				else
				{
					range = this.GetRange(this.PointToPlace(new Point(this.LeftIndent, 0)), this.PointToPlace(new Point(base.ClientSize.Width, base.ClientSize.Height)));
				}
				return range;
			}
		}

		// Token: 0x17001552 RID: 5458
		// (get) Token: 0x0600645C RID: 25692 RVA: 0x001E016C File Offset: 0x001E016C
		// (set) Token: 0x0600645D RID: 25693 RVA: 0x001E018C File Offset: 0x001E018C
		[Browsable(false)]
		public Range Selection
		{
			get
			{
				return this.selection;
			}
			set
			{
				bool flag = value == this.selection;
				if (!flag)
				{
					this.selection.BeginUpdate();
					this.selection.Start = value.Start;
					this.selection.End = value.End;
					this.selection.EndUpdate();
					this.Invalidate();
				}
			}
		}

		// Token: 0x17001553 RID: 5459
		// (get) Token: 0x0600645E RID: 25694 RVA: 0x001E01F8 File Offset: 0x001E01F8
		// (set) Token: 0x0600645F RID: 25695 RVA: 0x001E0218 File Offset: 0x001E0218
		[DefaultValue(typeof(Color), "White")]
		[Description("Background color.")]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		// Token: 0x17001554 RID: 5460
		// (get) Token: 0x06006460 RID: 25696 RVA: 0x001E0224 File Offset: 0x001E0224
		// (set) Token: 0x06006461 RID: 25697 RVA: 0x001E0244 File Offset: 0x001E0244
		[Browsable(false)]
		public Brush BackBrush
		{
			get
			{
				return this.backBrush;
			}
			set
			{
				this.backBrush = value;
				this.Invalidate();
			}
		}

		// Token: 0x17001555 RID: 5461
		// (get) Token: 0x06006462 RID: 25698 RVA: 0x001E0258 File Offset: 0x001E0258
		// (set) Token: 0x06006463 RID: 25699 RVA: 0x001E0278 File Offset: 0x001E0278
		[Browsable(true)]
		[DefaultValue(true)]
		[Description("Scollbars visibility.")]
		public bool ShowScrollBars
		{
			get
			{
				return this.scrollBars;
			}
			set
			{
				bool flag = value == this.scrollBars;
				if (!flag)
				{
					this.scrollBars = value;
					this.needRecalc = true;
					this.Invalidate();
				}
			}
		}

		// Token: 0x17001556 RID: 5462
		// (get) Token: 0x06006464 RID: 25700 RVA: 0x001E02B4 File Offset: 0x001E02B4
		// (set) Token: 0x06006465 RID: 25701 RVA: 0x001E02D4 File Offset: 0x001E02D4
		[Browsable(true)]
		[DefaultValue(true)]
		[Description("Multiline mode.")]
		public bool Multiline
		{
			get
			{
				return this.multiline;
			}
			set
			{
				bool flag = this.multiline == value;
				if (!flag)
				{
					this.multiline = value;
					this.needRecalc = true;
					bool flag2 = this.multiline;
					if (flag2)
					{
						base.AutoScroll = true;
						this.ShowScrollBars = true;
					}
					else
					{
						base.AutoScroll = false;
						this.ShowScrollBars = false;
						bool flag3 = this.lines.Count > 1;
						if (flag3)
						{
							this.lines.RemoveLine(1, this.lines.Count - 1);
						}
						this.lines.Manager.ClearHistory();
					}
					this.Invalidate();
				}
			}
		}

		// Token: 0x17001557 RID: 5463
		// (get) Token: 0x06006466 RID: 25702 RVA: 0x001E0388 File Offset: 0x001E0388
		// (set) Token: 0x06006467 RID: 25703 RVA: 0x001E03A8 File Offset: 0x001E03A8
		[Browsable(true)]
		[DefaultValue(false)]
		[Description("WordWrap.")]
		public bool WordWrap
		{
			get
			{
				return this.wordWrap;
			}
			set
			{
				bool flag = this.wordWrap == value;
				if (!flag)
				{
					this.wordWrap = value;
					bool flag2 = this.wordWrap;
					if (flag2)
					{
						this.Selection.ColumnSelectionMode = false;
					}
					this.NeedRecalc(false, true);
					this.Invalidate();
				}
			}
		}

		// Token: 0x17001558 RID: 5464
		// (get) Token: 0x06006468 RID: 25704 RVA: 0x001E0400 File Offset: 0x001E0400
		// (set) Token: 0x06006469 RID: 25705 RVA: 0x001E0420 File Offset: 0x001E0420
		[Browsable(true)]
		[DefaultValue(typeof(WordWrapMode), "WordWrapControlWidth")]
		[Description("WordWrap mode.")]
		public WordWrapMode WordWrapMode
		{
			get
			{
				return this.wordWrapMode;
			}
			set
			{
				bool flag = this.wordWrapMode == value;
				if (!flag)
				{
					this.wordWrapMode = value;
					this.NeedRecalc(false, true);
					this.Invalidate();
				}
			}
		}

		// Token: 0x17001559 RID: 5465
		// (get) Token: 0x0600646A RID: 25706 RVA: 0x001E0460 File Offset: 0x001E0460
		// (set) Token: 0x0600646B RID: 25707 RVA: 0x001E0480 File Offset: 0x001E0480
		[DefaultValue(true)]
		[Description("If enabled then line ends included into the selection will be selected too. Then line ends will be shown as selected blank character.")]
		public bool SelectionHighlightingForLineBreaksEnabled
		{
			get
			{
				return this.selectionHighlightingForLineBreaksEnabled;
			}
			set
			{
				this.selectionHighlightingForLineBreaksEnabled = value;
				this.Invalidate();
			}
		}

		// Token: 0x1700155A RID: 5466
		// (get) Token: 0x0600646C RID: 25708 RVA: 0x001E0494 File Offset: 0x001E0494
		// (set) Token: 0x0600646D RID: 25709 RVA: 0x001E049C File Offset: 0x001E049C
		[Browsable(false)]
		public FindForm findForm { get; private set; }

		// Token: 0x1700155B RID: 5467
		// (get) Token: 0x0600646E RID: 25710 RVA: 0x001E04A8 File Offset: 0x001E04A8
		// (set) Token: 0x0600646F RID: 25711 RVA: 0x001E04B0 File Offset: 0x001E04B0
		[Browsable(false)]
		public ReplaceForm replaceForm { get; private set; }

		// Token: 0x1700155C RID: 5468
		// (get) Token: 0x06006470 RID: 25712 RVA: 0x001E04BC File Offset: 0x001E04BC
		// (set) Token: 0x06006471 RID: 25713 RVA: 0x001E04DC File Offset: 0x001E04DC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoScroll
		{
			get
			{
				return base.AutoScroll;
			}
			set
			{
			}
		}

		// Token: 0x1700155D RID: 5469
		// (get) Token: 0x06006472 RID: 25714 RVA: 0x001E04E0 File Offset: 0x001E04E0
		[Browsable(false)]
		public int LinesCount
		{
			get
			{
				return this.lines.Count;
			}
		}

		// Token: 0x1700155E RID: 5470
		public Char this[Place place]
		{
			get
			{
				return this.lines[place.iLine][place.iChar];
			}
			set
			{
				this.lines[place.iLine][place.iChar] = value;
			}
		}

		// Token: 0x1700155F RID: 5471
		public Line this[int iLine]
		{
			get
			{
				return this.lines[iLine];
			}
		}

		// Token: 0x17001560 RID: 5472
		// (get) Token: 0x06006476 RID: 25718 RVA: 0x001E0588 File Offset: 0x001E0588
		// (set) Token: 0x06006477 RID: 25719 RVA: 0x001E05D0 File Offset: 0x001E05D0
		[Browsable(true)]
		[Localizable(true)]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SettingsBindable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Text of the control.")]
		[Bindable(true)]
		public override string Text
		{
			get
			{
				bool flag = this.LinesCount == 0;
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					Range range = new Range(this);
					range.SelectAll();
					result = range.Text;
				}
				return result;
			}
			set
			{
				bool flag = value == this.Text && value != "";
				if (!flag)
				{
					this.SetAsCurrentTB();
					this.Selection.ColumnSelectionMode = false;
					this.Selection.BeginUpdate();
					try
					{
						this.Selection.SelectAll();
						this.InsertText(value);
						this.GoHome();
					}
					finally
					{
						this.Selection.EndUpdate();
					}
				}
			}
		}

		// Token: 0x17001561 RID: 5473
		// (get) Token: 0x06006478 RID: 25720 RVA: 0x001E0670 File Offset: 0x001E0670
		public int TextLength
		{
			get
			{
				bool flag = this.LinesCount == 0;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					Range range = new Range(this);
					range.SelectAll();
					result = range.Length;
				}
				return result;
			}
		}

		// Token: 0x17001562 RID: 5474
		// (get) Token: 0x06006479 RID: 25721 RVA: 0x001E06B4 File Offset: 0x001E06B4
		[Browsable(false)]
		public IList<string> Lines
		{
			get
			{
				return this.lines.GetLines();
			}
		}

		// Token: 0x17001563 RID: 5475
		// (get) Token: 0x0600647A RID: 25722 RVA: 0x001E06D8 File Offset: 0x001E06D8
		[Browsable(false)]
		public string Html
		{
			get
			{
				return "<pre>" + new ExportToHTML
				{
					UseNbsp = false,
					UseStyleTag = false,
					UseBr = false
				}.GetHtml(this) + "</pre>";
			}
		}

		// Token: 0x17001564 RID: 5476
		// (get) Token: 0x0600647B RID: 25723 RVA: 0x001E0728 File Offset: 0x001E0728
		[Browsable(false)]
		public string Rtf
		{
			get
			{
				ExportToRTF exportToRTF = new ExportToRTF();
				return exportToRTF.GetRtf(this);
			}
		}

		// Token: 0x17001565 RID: 5477
		// (get) Token: 0x0600647C RID: 25724 RVA: 0x001E0750 File Offset: 0x001E0750
		// (set) Token: 0x0600647D RID: 25725 RVA: 0x001E0774 File Offset: 0x001E0774
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string SelectedText
		{
			get
			{
				return this.Selection.Text;
			}
			set
			{
				this.InsertText(value);
			}
		}

		// Token: 0x17001566 RID: 5478
		// (get) Token: 0x0600647E RID: 25726 RVA: 0x001E0780 File Offset: 0x001E0780
		// (set) Token: 0x0600647F RID: 25727 RVA: 0x001E07C0 File Offset: 0x001E07C0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int SelectionStart
		{
			get
			{
				return Math.Min(this.PlaceToPosition(this.Selection.Start), this.PlaceToPosition(this.Selection.End));
			}
			set
			{
				this.Selection.Start = this.PositionToPlace(value);
			}
		}

		// Token: 0x17001567 RID: 5479
		// (get) Token: 0x06006480 RID: 25728 RVA: 0x001E07D8 File Offset: 0x001E07D8
		// (set) Token: 0x06006481 RID: 25729 RVA: 0x001E07FC File Offset: 0x001E07FC
		[Browsable(false)]
		[DefaultValue(0)]
		public int SelectionLength
		{
			get
			{
				return this.Selection.Length;
			}
			set
			{
				bool flag = value > 0;
				if (flag)
				{
					this.Selection.End = this.PositionToPlace(this.SelectionStart + value);
				}
			}
		}

		// Token: 0x17001568 RID: 5480
		// (get) Token: 0x06006482 RID: 25730 RVA: 0x001E0834 File Offset: 0x001E0834
		// (set) Token: 0x06006483 RID: 25731 RVA: 0x001E0854 File Offset: 0x001E0854
		[DefaultValue(typeof(Font), "Courier New, 9.75")]
		public override Font Font
		{
			get
			{
				return this.BaseFont;
			}
			set
			{
				this.originalFont = (Font)value.Clone();
				this.SetFont(value);
			}
		}

		// Token: 0x17001569 RID: 5481
		// (get) Token: 0x06006484 RID: 25732 RVA: 0x001E0870 File Offset: 0x001E0870
		// (set) Token: 0x06006485 RID: 25733 RVA: 0x001E0890 File Offset: 0x001E0890
		[DefaultValue(typeof(Font), "Courier New, 9.75")]
		private Font BaseFont
		{
			get
			{
				return this.baseFont;
			}
			set
			{
				this.baseFont = value;
			}
		}

		// Token: 0x06006486 RID: 25734 RVA: 0x001E089C File Offset: 0x001E089C
		private void SetFont(Font newFont)
		{
			this.BaseFont = newFont;
			SizeF charSize = FastColoredTextBox.GetCharSize(this.BaseFont, 'M');
			SizeF charSize2 = FastColoredTextBox.GetCharSize(this.BaseFont, '.');
			bool flag = charSize != charSize2;
			if (flag)
			{
				this.BaseFont = new Font("Courier New", this.BaseFont.SizeInPoints, FontStyle.Regular, GraphicsUnit.Point);
			}
			SizeF charSize3 = FastColoredTextBox.GetCharSize(this.BaseFont, 'M');
			this.CharWidth = (int)Math.Round((double)(charSize3.Width * 1f)) - 1;
			this.CharHeight = this.lineInterval + (int)Math.Round((double)(charSize3.Height * 1f)) - 1;
			this.NeedRecalc(false, this.wordWrap);
			this.Invalidate();
		}

		// Token: 0x1700156A RID: 5482
		// (get) Token: 0x06006488 RID: 25736 RVA: 0x001E0A90 File Offset: 0x001E0A90
		// (set) Token: 0x06006487 RID: 25735 RVA: 0x001E0964 File Offset: 0x001E0964
		public new Size AutoScrollMinSize
		{
			get
			{
				bool flag = this.scrollBars;
				Size autoScrollMinSize;
				if (flag)
				{
					autoScrollMinSize = base.AutoScrollMinSize;
				}
				else
				{
					autoScrollMinSize = this.localAutoScrollMinSize;
				}
				return autoScrollMinSize;
			}
			set
			{
				bool flag = this.scrollBars;
				if (flag)
				{
					bool flag2 = !base.AutoScroll;
					if (flag2)
					{
						base.AutoScroll = true;
					}
					Size autoScrollMinSize = value;
					bool flag3 = this.WordWrap && this.WordWrapMode != WordWrapMode.Custom;
					if (flag3)
					{
						int maxLineWordWrapedWidth = this.GetMaxLineWordWrapedWidth();
						autoScrollMinSize = new Size(Math.Min(autoScrollMinSize.Width, maxLineWordWrapedWidth), autoScrollMinSize.Height);
					}
					base.AutoScrollMinSize = autoScrollMinSize;
				}
				else
				{
					bool autoScroll = base.AutoScroll;
					if (autoScroll)
					{
						base.AutoScroll = false;
					}
					base.AutoScrollMinSize = new Size(0, 0);
					base.VerticalScroll.Visible = false;
					base.HorizontalScroll.Visible = false;
					base.VerticalScroll.Maximum = Math.Max(0, value.Height - base.ClientSize.Height);
					base.HorizontalScroll.Maximum = Math.Max(0, value.Width - base.ClientSize.Width);
					this.localAutoScrollMinSize = value;
				}
			}
		}

		// Token: 0x1700156B RID: 5483
		// (get) Token: 0x06006489 RID: 25737 RVA: 0x001E0AC8 File Offset: 0x001E0AC8
		[Browsable(false)]
		public bool ImeAllowed
		{
			get
			{
				return base.ImeMode != ImeMode.Disable && base.ImeMode != ImeMode.Off && base.ImeMode > ImeMode.NoControl;
			}
		}

		// Token: 0x1700156C RID: 5484
		// (get) Token: 0x0600648A RID: 25738 RVA: 0x001E0B08 File Offset: 0x001E0B08
		[Browsable(false)]
		public bool UndoEnabled
		{
			get
			{
				return this.lines.Manager.UndoEnabled;
			}
		}

		// Token: 0x1700156D RID: 5485
		// (get) Token: 0x0600648B RID: 25739 RVA: 0x001E0B34 File Offset: 0x001E0B34
		[Browsable(false)]
		public bool RedoEnabled
		{
			get
			{
				return this.lines.Manager.RedoEnabled;
			}
		}

		// Token: 0x1700156E RID: 5486
		// (get) Token: 0x0600648C RID: 25740 RVA: 0x001E0B60 File Offset: 0x001E0B60
		private int LeftIndentLine
		{
			get
			{
				return this.LeftIndent - 4 - 3;
			}
		}

		// Token: 0x1700156F RID: 5487
		// (get) Token: 0x0600648D RID: 25741 RVA: 0x001E0B84 File Offset: 0x001E0B84
		[Browsable(false)]
		public Range Range
		{
			get
			{
				return new Range(this, new Place(0, 0), new Place(this.lines[this.lines.Count - 1].Count, this.lines.Count - 1));
			}
		}

		// Token: 0x17001570 RID: 5488
		// (get) Token: 0x0600648E RID: 25742 RVA: 0x001E0BDC File Offset: 0x001E0BDC
		// (set) Token: 0x0600648F RID: 25743 RVA: 0x001E0BFC File Offset: 0x001E0BFC
		[DefaultValue(typeof(Color), "Blue")]
		[Description("Color of selected area.")]
		public virtual Color SelectionColor
		{
			get
			{
				return this.selectionColor;
			}
			set
			{
				this.selectionColor = value;
				bool flag = this.selectionColor.A == byte.MaxValue;
				if (flag)
				{
					this.selectionColor = Color.FromArgb(60, this.selectionColor);
				}
				this.SelectionStyle = new SelectionStyle(new SolidBrush(this.selectionColor), null);
				this.Invalidate();
			}
		}

		// Token: 0x17001571 RID: 5489
		// (get) Token: 0x06006490 RID: 25744 RVA: 0x001E0C60 File Offset: 0x001E0C60
		// (set) Token: 0x06006491 RID: 25745 RVA: 0x001E0C80 File Offset: 0x001E0C80
		public override Cursor Cursor
		{
			get
			{
				return base.Cursor;
			}
			set
			{
				this.defaultCursor = value;
				base.Cursor = value;
			}
		}

		// Token: 0x17001572 RID: 5490
		// (get) Token: 0x06006492 RID: 25746 RVA: 0x001E0C94 File Offset: 0x001E0C94
		// (set) Token: 0x06006493 RID: 25747 RVA: 0x001E0CB4 File Offset: 0x001E0CB4
		[DefaultValue(1)]
		[Description("Reserved space for line number characters. If smaller than needed (e. g. line count >= 10 and this value set to 1) this value will have no impact. If you want to reserve space, e. g. for line numbers >= 10 or >= 100, than you can set this value to 2 or 3 or higher.")]
		public int ReservedCountOfLineNumberChars
		{
			get
			{
				return this.reservedCountOfLineNumberChars;
			}
			set
			{
				this.reservedCountOfLineNumberChars = value;
				this.NeedRecalc();
				this.Invalidate();
			}
		}

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06006494 RID: 25748 RVA: 0x001E0CCC File Offset: 0x001E0CCC
		// (remove) Token: 0x06006495 RID: 25749 RVA: 0x001E0D08 File Offset: 0x001E0D08
		[Browsable(true)]
		[Description("Occurs when mouse is moving over text and tooltip is needed.")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ToolTipNeededEventArgs> ToolTipNeeded;

		// Token: 0x06006496 RID: 25750 RVA: 0x001E0D44 File Offset: 0x001E0D44
		public void ClearHints()
		{
			bool flag = this.Hints != null;
			if (flag)
			{
				this.Hints.Clear();
			}
		}

		// Token: 0x06006497 RID: 25751 RVA: 0x001E0D74 File Offset: 0x001E0D74
		public virtual Hint AddHint(Range range, Control innerControl, bool scrollToHint, bool inline, bool dock)
		{
			Hint hint = new Hint(range, innerControl, inline, dock);
			this.Hints.Add(hint);
			if (scrollToHint)
			{
				hint.DoVisible();
			}
			return hint;
		}

		// Token: 0x06006498 RID: 25752 RVA: 0x001E0DB8 File Offset: 0x001E0DB8
		public Hint AddHint(Range range, Control innerControl)
		{
			return this.AddHint(range, innerControl, true, true, true);
		}

		// Token: 0x06006499 RID: 25753 RVA: 0x001E0DDC File Offset: 0x001E0DDC
		public virtual Hint AddHint(Range range, string text, bool scrollToHint, bool inline, bool dock)
		{
			Hint hint = new Hint(range, text, inline, dock);
			this.Hints.Add(hint);
			if (scrollToHint)
			{
				hint.DoVisible();
			}
			return hint;
		}

		// Token: 0x0600649A RID: 25754 RVA: 0x001E0E20 File Offset: 0x001E0E20
		public Hint AddHint(Range range, string text)
		{
			return this.AddHint(range, text, true, true, true);
		}

		// Token: 0x0600649B RID: 25755 RVA: 0x001E0E44 File Offset: 0x001E0E44
		public virtual void OnHintClick(Hint hint)
		{
			bool flag = this.HintClick != null;
			if (flag)
			{
				this.HintClick(this, new HintClickEventArgs(hint));
			}
		}

		// Token: 0x0600649C RID: 25756 RVA: 0x001E0E7C File Offset: 0x001E0E7C
		private void timer3_Tick(object sender, EventArgs e)
		{
			this.timer3.Stop();
			this.OnToolTip();
		}

		// Token: 0x0600649D RID: 25757 RVA: 0x001E0E94 File Offset: 0x001E0E94
		protected virtual void OnToolTip()
		{
			bool flag = this.ToolTip == null;
			if (!flag)
			{
				bool flag2 = this.ToolTipNeeded == null;
				if (!flag2)
				{
					Place place = this.PointToPlace(this.lastMouseCoord);
					Point point = this.PlaceToPoint(place);
					bool flag3 = Math.Abs(point.X - this.lastMouseCoord.X) > this.CharWidth * 2 || Math.Abs(point.Y - this.lastMouseCoord.Y) > this.CharHeight * 2;
					if (!flag3)
					{
						Range range = new Range(this, place, place);
						string text = range.GetFragment("[a-zA-Z]").Text;
						ToolTipNeededEventArgs toolTipNeededEventArgs = new ToolTipNeededEventArgs(place, text);
						this.ToolTipNeeded(this, toolTipNeededEventArgs);
						bool flag4 = toolTipNeededEventArgs.ToolTipText != null;
						if (flag4)
						{
							this.ToolTip.ToolTipTitle = toolTipNeededEventArgs.ToolTipTitle;
							this.ToolTip.ToolTipIcon = toolTipNeededEventArgs.ToolTipIcon;
							this.ToolTip.Show(toolTipNeededEventArgs.ToolTipText, this, new Point(this.lastMouseCoord.X, this.lastMouseCoord.Y + this.CharHeight));
						}
					}
				}
			}
		}

		// Token: 0x0600649E RID: 25758 RVA: 0x001E0FE8 File Offset: 0x001E0FE8
		public virtual void OnVisibleRangeChanged()
		{
			this.needRecalcFoldingLines = true;
			this.needRiseVisibleRangeChangedDelayed = true;
			this.ResetTimer(this.timer);
			bool flag = this.VisibleRangeChanged != null;
			if (flag)
			{
				this.VisibleRangeChanged(this, new EventArgs());
			}
		}

		// Token: 0x0600649F RID: 25759 RVA: 0x001E1038 File Offset: 0x001E1038
		public new void Invalidate()
		{
			bool invokeRequired = base.InvokeRequired;
			if (invokeRequired)
			{
				base.BeginInvoke(new MethodInvoker(this.Invalidate));
			}
			else
			{
				base.Invalidate();
			}
		}

		// Token: 0x060064A0 RID: 25760 RVA: 0x001E1078 File Offset: 0x001E1078
		protected virtual void OnCharSizeChanged()
		{
			base.VerticalScroll.SmallChange = this.charHeight;
			base.VerticalScroll.LargeChange = 10 * this.charHeight;
			base.HorizontalScroll.SmallChange = this.CharWidth;
		}

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x060064A1 RID: 25761 RVA: 0x001E10C4 File Offset: 0x001E10C4
		// (remove) Token: 0x060064A2 RID: 25762 RVA: 0x001E1100 File Offset: 0x001E1100
		[Browsable(true)]
		[Description("It occurs if user click on the hint.")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<HintClickEventArgs> HintClick;

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x060064A3 RID: 25763 RVA: 0x001E113C File Offset: 0x001E113C
		// (remove) Token: 0x060064A4 RID: 25764 RVA: 0x001E1178 File Offset: 0x001E1178
		[Browsable(true)]
		[Description("It occurs after insert, delete, clear, undo and redo operations.")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public new event EventHandler<TextChangedEventArgs> TextChanged;

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x060064A5 RID: 25765 RVA: 0x001E11B4 File Offset: 0x001E11B4
		// (remove) Token: 0x060064A6 RID: 25766 RVA: 0x001E11F0 File Offset: 0x001E11F0
		[Browsable(false)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event EventHandler BindingTextChanged;

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x060064A7 RID: 25767 RVA: 0x001E122C File Offset: 0x001E122C
		// (remove) Token: 0x060064A8 RID: 25768 RVA: 0x001E1268 File Offset: 0x001E1268
		[Description("Occurs when user paste text from clipboard")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<TextChangingEventArgs> Pasting;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x060064A9 RID: 25769 RVA: 0x001E12A4 File Offset: 0x001E12A4
		// (remove) Token: 0x060064AA RID: 25770 RVA: 0x001E12E0 File Offset: 0x001E12E0
		[Browsable(true)]
		[Description("It occurs before insert, delete, clear, undo and redo operations.")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<TextChangingEventArgs> TextChanging;

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x060064AB RID: 25771 RVA: 0x001E131C File Offset: 0x001E131C
		// (remove) Token: 0x060064AC RID: 25772 RVA: 0x001E1358 File Offset: 0x001E1358
		[Browsable(true)]
		[Description("It occurs after changing of selection.")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler SelectionChanged;

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x060064AD RID: 25773 RVA: 0x001E1394 File Offset: 0x001E1394
		// (remove) Token: 0x060064AE RID: 25774 RVA: 0x001E13D0 File Offset: 0x001E13D0
		[Browsable(true)]
		[Description("It occurs after changing of visible range.")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler VisibleRangeChanged;

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x060064AF RID: 25775 RVA: 0x001E140C File Offset: 0x001E140C
		// (remove) Token: 0x060064B0 RID: 25776 RVA: 0x001E1448 File Offset: 0x001E1448
		[Browsable(true)]
		[Description("It occurs after insert, delete, clear, undo and redo operations. This event occurs with a delay relative to TextChanged, and fires only once.")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<TextChangedEventArgs> TextChangedDelayed;

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x060064B1 RID: 25777 RVA: 0x001E1484 File Offset: 0x001E1484
		// (remove) Token: 0x060064B2 RID: 25778 RVA: 0x001E14C0 File Offset: 0x001E14C0
		[Browsable(true)]
		[Description("It occurs after changing of selection. This event occurs with a delay relative to SelectionChanged, and fires only once.")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler SelectionChangedDelayed;

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x060064B3 RID: 25779 RVA: 0x001E14FC File Offset: 0x001E14FC
		// (remove) Token: 0x060064B4 RID: 25780 RVA: 0x001E1538 File Offset: 0x001E1538
		[Browsable(true)]
		[Description("It occurs after changing of visible range. This event occurs with a delay relative to VisibleRangeChanged, and fires only once.")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler VisibleRangeChangedDelayed;

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x060064B5 RID: 25781 RVA: 0x001E1574 File Offset: 0x001E1574
		// (remove) Token: 0x060064B6 RID: 25782 RVA: 0x001E15B0 File Offset: 0x001E15B0
		[Browsable(true)]
		[Description("It occurs when user click on VisualMarker.")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<VisualMarkerEventArgs> VisualMarkerClick;

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x060064B7 RID: 25783 RVA: 0x001E15EC File Offset: 0x001E15EC
		// (remove) Token: 0x060064B8 RID: 25784 RVA: 0x001E1628 File Offset: 0x001E1628
		[Browsable(true)]
		[Description("It occurs when visible char is enetering (alphabetic, digit, punctuation, DEL, BACKSPACE).")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event KeyPressEventHandler KeyPressing;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x060064B9 RID: 25785 RVA: 0x001E1664 File Offset: 0x001E1664
		// (remove) Token: 0x060064BA RID: 25786 RVA: 0x001E16A0 File Offset: 0x001E16A0
		[Browsable(true)]
		[Description("It occurs when visible char is enetered (alphabetic, digit, punctuation, DEL, BACKSPACE).")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event KeyPressEventHandler KeyPressed;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x060064BB RID: 25787 RVA: 0x001E16DC File Offset: 0x001E16DC
		// (remove) Token: 0x060064BC RID: 25788 RVA: 0x001E1718 File Offset: 0x001E1718
		[Browsable(true)]
		[Description("It occurs when calculates AutoIndent for new line.")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<AutoIndentEventArgs> AutoIndentNeeded;

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x060064BD RID: 25789 RVA: 0x001E1754 File Offset: 0x001E1754
		// (remove) Token: 0x060064BE RID: 25790 RVA: 0x001E1790 File Offset: 0x001E1790
		[Browsable(true)]
		[Description("It occurs when line background is painting.")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<PaintLineEventArgs> PaintLine;

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x060064BF RID: 25791 RVA: 0x001E17CC File Offset: 0x001E17CC
		// (remove) Token: 0x060064C0 RID: 25792 RVA: 0x001E1808 File Offset: 0x001E1808
		[Browsable(true)]
		[Description("Occurs when line was inserted/added.")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<LineInsertedEventArgs> LineInserted;

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x060064C1 RID: 25793 RVA: 0x001E1844 File Offset: 0x001E1844
		// (remove) Token: 0x060064C2 RID: 25794 RVA: 0x001E1880 File Offset: 0x001E1880
		[Browsable(true)]
		[Description("Occurs when line was removed.")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<LineRemovedEventArgs> LineRemoved;

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x060064C3 RID: 25795 RVA: 0x001E18BC File Offset: 0x001E18BC
		// (remove) Token: 0x060064C4 RID: 25796 RVA: 0x001E18F8 File Offset: 0x001E18F8
		[Browsable(true)]
		[Description("Occurs when current highlighted folding area is changed.")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<EventArgs> FoldingHighlightChanged;

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x060064C5 RID: 25797 RVA: 0x001E1934 File Offset: 0x001E1934
		// (remove) Token: 0x060064C6 RID: 25798 RVA: 0x001E1970 File Offset: 0x001E1970
		[Browsable(true)]
		[Description("Occurs when undo/redo stack is changed.")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<EventArgs> UndoRedoStateChanged;

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x060064C7 RID: 25799 RVA: 0x001E19AC File Offset: 0x001E19AC
		// (remove) Token: 0x060064C8 RID: 25800 RVA: 0x001E19E8 File Offset: 0x001E19E8
		[Browsable(true)]
		[Description("Occurs when component was zoomed.")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler ZoomChanged;

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x060064C9 RID: 25801 RVA: 0x001E1A24 File Offset: 0x001E1A24
		// (remove) Token: 0x060064CA RID: 25802 RVA: 0x001E1A60 File Offset: 0x001E1A60
		[Browsable(true)]
		[Description("Occurs when user pressed key, that specified as CustomAction.")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<CustomActionEventArgs> CustomAction;

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x060064CB RID: 25803 RVA: 0x001E1A9C File Offset: 0x001E1A9C
		// (remove) Token: 0x060064CC RID: 25804 RVA: 0x001E1AD8 File Offset: 0x001E1AD8
		[Browsable(true)]
		[Description("Occurs when scroolbars are updated.")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler ScrollbarsUpdated;

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x060064CD RID: 25805 RVA: 0x001E1B14 File Offset: 0x001E1B14
		// (remove) Token: 0x060064CE RID: 25806 RVA: 0x001E1B50 File Offset: 0x001E1B50
		[Browsable(true)]
		[Description("Occurs when custom wordwrap is needed.")]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<WordWrapNeededEventArgs> WordWrapNeeded;

		// Token: 0x060064CF RID: 25807 RVA: 0x001E1B8C File Offset: 0x001E1B8C
		public List<Style> GetStylesOfChar(Place place)
		{
			List<Style> list = new List<Style>();
			bool flag = place.iLine < this.LinesCount && place.iChar < this[place.iLine].Count;
			if (flag)
			{
				ushort style = (ushort)this[place].style;
				for (int i = 0; i < 16; i++)
				{
					bool flag2 = ((int)style & 1 << i) != 0;
					if (flag2)
					{
						list.Add(this.Styles[i]);
					}
				}
			}
			return list;
		}

		// Token: 0x060064D0 RID: 25808 RVA: 0x001E1C34 File Offset: 0x001E1C34
		protected virtual TextSource CreateTextSource()
		{
			return new TextSource(this);
		}

		// Token: 0x060064D1 RID: 25809 RVA: 0x001E1C54 File Offset: 0x001E1C54
		private void SetAsCurrentTB()
		{
			this.TextSource.CurrentTB = this;
		}

		// Token: 0x060064D2 RID: 25810 RVA: 0x001E1C64 File Offset: 0x001E1C64
		protected virtual void InitTextSource(TextSource ts)
		{
			bool flag = this.lines != null;
			if (flag)
			{
				this.lines.LineInserted -= this.ts_LineInserted;
				this.lines.LineRemoved -= this.ts_LineRemoved;
				this.lines.TextChanged -= this.ts_TextChanged;
				this.lines.RecalcNeeded -= this.ts_RecalcNeeded;
				this.lines.RecalcWordWrap -= this.ts_RecalcWordWrap;
				this.lines.TextChanging -= this.ts_TextChanging;
				this.lines.Dispose();
			}
			this.LineInfos.Clear();
			this.ClearHints();
			bool flag2 = this.Bookmarks != null;
			if (flag2)
			{
				this.Bookmarks.Clear();
			}
			this.lines = ts;
			bool flag3 = ts != null;
			if (flag3)
			{
				ts.LineInserted += this.ts_LineInserted;
				ts.LineRemoved += this.ts_LineRemoved;
				ts.TextChanged += this.ts_TextChanged;
				ts.RecalcNeeded += this.ts_RecalcNeeded;
				ts.RecalcWordWrap += this.ts_RecalcWordWrap;
				ts.TextChanging += this.ts_TextChanging;
				while (this.LineInfos.Count < ts.Count)
				{
					this.LineInfos.Add(new LineInfo(-1));
				}
			}
			this.isChanged = false;
			this.needRecalc = true;
		}

		// Token: 0x060064D3 RID: 25811 RVA: 0x001E1E18 File Offset: 0x001E1E18
		private void ts_RecalcWordWrap(object sender, TextSource.TextChangedEventArgs e)
		{
			this.RecalcWordWrap(e.iFromLine, e.iToLine);
		}

		// Token: 0x060064D4 RID: 25812 RVA: 0x001E1E30 File Offset: 0x001E1E30
		private void ts_TextChanging(object sender, TextChangingEventArgs e)
		{
			bool flag = this.TextSource.CurrentTB == this;
			if (flag)
			{
				string insertingText = e.InsertingText;
				this.OnTextChanging(ref insertingText);
				e.InsertingText = insertingText;
			}
		}

		// Token: 0x060064D5 RID: 25813 RVA: 0x001E1E74 File Offset: 0x001E1E74
		private void ts_RecalcNeeded(object sender, TextSource.TextChangedEventArgs e)
		{
			bool flag = e.iFromLine == e.iToLine && !this.WordWrap && this.lines.Count > 100000;
			if (flag)
			{
				this.RecalcScrollByOneLine(e.iFromLine);
			}
			else
			{
				this.NeedRecalc(false, this.WordWrap);
			}
		}

		// Token: 0x060064D6 RID: 25814 RVA: 0x001E1EE4 File Offset: 0x001E1EE4
		public void NeedRecalc()
		{
			this.NeedRecalc(false);
		}

		// Token: 0x060064D7 RID: 25815 RVA: 0x001E1EF0 File Offset: 0x001E1EF0
		public void NeedRecalc(bool forced)
		{
			this.NeedRecalc(forced, false);
		}

		// Token: 0x060064D8 RID: 25816 RVA: 0x001E1EFC File Offset: 0x001E1EFC
		public void NeedRecalc(bool forced, bool wordWrapRecalc)
		{
			this.needRecalc = true;
			if (wordWrapRecalc)
			{
				this.needRecalcWordWrapInterval = new Point(0, this.LinesCount - 1);
				this.needRecalcWordWrap = true;
			}
			if (forced)
			{
				this.Recalc();
			}
		}

		// Token: 0x060064D9 RID: 25817 RVA: 0x001E1F4C File Offset: 0x001E1F4C
		private void ts_TextChanged(object sender, TextSource.TextChangedEventArgs e)
		{
			bool flag = e.iFromLine == e.iToLine && !this.WordWrap;
			if (flag)
			{
				this.RecalcScrollByOneLine(e.iFromLine);
			}
			else
			{
				this.needRecalc = true;
			}
			this.Invalidate();
			bool flag2 = this.TextSource.CurrentTB == this;
			if (flag2)
			{
				this.OnTextChanged(e.iFromLine, e.iToLine);
			}
		}

		// Token: 0x060064DA RID: 25818 RVA: 0x001E1FD0 File Offset: 0x001E1FD0
		private void ts_LineRemoved(object sender, LineRemovedEventArgs e)
		{
			this.LineInfos.RemoveRange(e.Index, e.Count);
			this.OnLineRemoved(e.Index, e.Count, e.RemovedLineUniqueIds);
		}

		// Token: 0x060064DB RID: 25819 RVA: 0x001E2014 File Offset: 0x001E2014
		private void ts_LineInserted(object sender, LineInsertedEventArgs e)
		{
			VisibleState visibleState = VisibleState.Visible;
			bool flag = e.Index >= 0 && e.Index < this.LineInfos.Count && this.LineInfos[e.Index].VisibleState == VisibleState.Hidden;
			if (flag)
			{
				visibleState = VisibleState.Hidden;
			}
			bool flag2 = e.Count > 100000;
			if (flag2)
			{
				this.LineInfos.Capacity = this.LineInfos.Count + e.Count + 1000;
			}
			LineInfo[] array = new LineInfo[e.Count];
			for (int i = 0; i < e.Count; i++)
			{
				array[i].startY = -1;
				array[i].VisibleState = visibleState;
			}
			this.LineInfos.InsertRange(e.Index, array);
			bool flag3 = e.Count > 1000000;
			if (flag3)
			{
				GC.Collect();
			}
			this.OnLineInserted(e.Index, e.Count);
		}

		// Token: 0x060064DC RID: 25820 RVA: 0x001E2134 File Offset: 0x001E2134
		public bool NavigateForward()
		{
			DateTime t = DateTime.Now;
			int num = -1;
			for (int i = 0; i < this.LinesCount; i++)
			{
				bool flag = this.lines.IsLineLoaded(i);
				if (flag)
				{
					bool flag2 = this.lines[i].LastVisit > this.lastNavigatedDateTime && this.lines[i].LastVisit < t;
					if (flag2)
					{
						t = this.lines[i].LastVisit;
						num = i;
					}
				}
			}
			bool flag3 = num >= 0;
			bool result;
			if (flag3)
			{
				this.Navigate(num);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060064DD RID: 25821 RVA: 0x001E2208 File Offset: 0x001E2208
		public bool NavigateBackward()
		{
			DateTime t = default(DateTime);
			int num = -1;
			for (int i = 0; i < this.LinesCount; i++)
			{
				bool flag = this.lines.IsLineLoaded(i);
				if (flag)
				{
					bool flag2 = this.lines[i].LastVisit < this.lastNavigatedDateTime && this.lines[i].LastVisit > t;
					if (flag2)
					{
						t = this.lines[i].LastVisit;
						num = i;
					}
				}
			}
			bool flag3 = num >= 0;
			bool result;
			if (flag3)
			{
				this.Navigate(num);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060064DE RID: 25822 RVA: 0x001E22DC File Offset: 0x001E22DC
		public void Navigate(int iLine)
		{
			bool flag = iLine >= this.LinesCount;
			if (!flag)
			{
				this.lastNavigatedDateTime = this.lines[iLine].LastVisit;
				this.Selection.Start = new Place(0, iLine);
				this.DoSelectionVisible();
			}
		}

		// Token: 0x060064DF RID: 25823 RVA: 0x001E2338 File Offset: 0x001E2338
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.m_hImc = FastColoredTextBox.ImmGetContext(base.Handle);
		}

		// Token: 0x060064E0 RID: 25824 RVA: 0x001E2354 File Offset: 0x001E2354
		private void timer2_Tick(object sender, EventArgs e)
		{
			this.timer2.Enabled = false;
			bool flag = this.needRiseTextChangedDelayed;
			if (flag)
			{
				this.needRiseTextChangedDelayed = false;
				bool flag2 = this.delayedTextChangedRange == null;
				if (!flag2)
				{
					this.delayedTextChangedRange = this.Range.GetIntersectionWith(this.delayedTextChangedRange);
					this.delayedTextChangedRange.Expand();
					this.OnTextChangedDelayed(this.delayedTextChangedRange);
					this.delayedTextChangedRange = null;
				}
			}
		}

		// Token: 0x060064E1 RID: 25825 RVA: 0x001E23D8 File Offset: 0x001E23D8
		public void AddVisualMarker(VisualMarker marker)
		{
			this.visibleMarkers.Add(marker);
		}

		// Token: 0x060064E2 RID: 25826 RVA: 0x001E23E8 File Offset: 0x001E23E8
		private void timer_Tick(object sender, EventArgs e)
		{
			this.timer.Enabled = false;
			bool flag = this.needRiseSelectionChangedDelayed;
			if (flag)
			{
				this.needRiseSelectionChangedDelayed = false;
				this.OnSelectionChangedDelayed();
			}
			bool flag2 = this.needRiseVisibleRangeChangedDelayed;
			if (flag2)
			{
				this.needRiseVisibleRangeChangedDelayed = false;
				this.OnVisibleRangeChangedDelayed();
			}
		}

		// Token: 0x060064E3 RID: 25827 RVA: 0x001E2444 File Offset: 0x001E2444
		public virtual void OnTextChangedDelayed(Range changedRange)
		{
			bool flag = this.TextChangedDelayed != null;
			if (flag)
			{
				this.TextChangedDelayed(this, new TextChangedEventArgs(changedRange));
			}
		}

		// Token: 0x060064E4 RID: 25828 RVA: 0x001E247C File Offset: 0x001E247C
		public virtual void OnSelectionChangedDelayed()
		{
			this.RecalcScrollByOneLine(this.Selection.Start.iLine);
			this.ClearBracketsPositions();
			bool flag = this.LeftBracket != '\0' && this.RightBracket > '\0';
			if (flag)
			{
				this.HighlightBrackets(this.LeftBracket, this.RightBracket, ref this.leftBracketPosition, ref this.rightBracketPosition);
			}
			bool flag2 = this.LeftBracket2 != '\0' && this.RightBracket2 > '\0';
			if (flag2)
			{
				this.HighlightBrackets(this.LeftBracket2, this.RightBracket2, ref this.leftBracketPosition2, ref this.rightBracketPosition2);
			}
			bool flag3 = this.Selection.IsEmpty && this.Selection.Start.iLine < this.LinesCount;
			if (flag3)
			{
				bool flag4 = this.lastNavigatedDateTime != this.lines[this.Selection.Start.iLine].LastVisit;
				if (flag4)
				{
					this.lines[this.Selection.Start.iLine].LastVisit = DateTime.Now;
					this.lastNavigatedDateTime = this.lines[this.Selection.Start.iLine].LastVisit;
				}
			}
			bool flag5 = this.SelectionChangedDelayed != null;
			if (flag5)
			{
				this.SelectionChangedDelayed(this, new EventArgs());
			}
		}

		// Token: 0x060064E5 RID: 25829 RVA: 0x001E2608 File Offset: 0x001E2608
		public virtual void OnVisibleRangeChangedDelayed()
		{
			bool flag = this.VisibleRangeChangedDelayed != null;
			if (flag)
			{
				this.VisibleRangeChangedDelayed(this, new EventArgs());
			}
		}

		// Token: 0x060064E6 RID: 25830 RVA: 0x001E263C File Offset: 0x001E263C
		private void ResetTimer(System.Windows.Forms.Timer timer)
		{
			bool invokeRequired = base.InvokeRequired;
			if (invokeRequired)
			{
				base.BeginInvoke(new MethodInvoker(delegate()
				{
					this.ResetTimer(timer);
				}));
			}
			else
			{
				timer.Stop();
				bool isHandleCreated = base.IsHandleCreated;
				if (isHandleCreated)
				{
					timer.Start();
				}
				else
				{
					this.timersToReset[timer] = timer;
				}
			}
		}

		// Token: 0x060064E7 RID: 25831 RVA: 0x001E26CC File Offset: 0x001E26CC
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			foreach (System.Windows.Forms.Timer timer in new List<System.Windows.Forms.Timer>(this.timersToReset.Keys))
			{
				this.ResetTimer(timer);
			}
			this.timersToReset.Clear();
			this.OnScrollbarsUpdated();
		}

		// Token: 0x060064E8 RID: 25832 RVA: 0x001E2750 File Offset: 0x001E2750
		public int AddStyle(Style style)
		{
			bool flag = style == null;
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				int num = this.GetStyleIndex(style);
				bool flag2 = num >= 0;
				if (flag2)
				{
					result = num;
				}
				else
				{
					num = this.CheckStylesBufferSize();
					this.Styles[num] = style;
					result = num;
				}
			}
			return result;
		}

		// Token: 0x060064E9 RID: 25833 RVA: 0x001E27B0 File Offset: 0x001E27B0
		public int CheckStylesBufferSize()
		{
			int i;
			for (i = this.Styles.Length - 1; i >= 0; i--)
			{
				bool flag = this.Styles[i] != null;
				if (flag)
				{
					break;
				}
			}
			i++;
			bool flag2 = i >= this.Styles.Length;
			if (flag2)
			{
				throw new Exception("Maximum count of Styles is exceeded.");
			}
			return i;
		}

		// Token: 0x060064EA RID: 25834 RVA: 0x001E282C File Offset: 0x001E282C
		public virtual void ShowFindDialog()
		{
			this.ShowFindDialog(null);
		}

		// Token: 0x060064EB RID: 25835 RVA: 0x001E2838 File Offset: 0x001E2838
		public virtual void ShowFindDialog(string findText)
		{
			bool flag = this.findForm == null;
			if (flag)
			{
				this.findForm = new FindForm(this);
			}
			bool flag2 = findText != null;
			if (flag2)
			{
				this.findForm.tbFind.Text = findText;
			}
			else
			{
				bool flag3 = !this.Selection.IsEmpty && this.Selection.Start.iLine == this.Selection.End.iLine;
				if (flag3)
				{
					this.findForm.tbFind.Text = this.Selection.Text;
				}
			}
			this.findForm.tbFind.SelectAll();
			this.findForm.Show();
			this.findForm.Focus();
		}

		// Token: 0x060064EC RID: 25836 RVA: 0x001E2910 File Offset: 0x001E2910
		public virtual void ShowReplaceDialog()
		{
			this.ShowReplaceDialog(null);
		}

		// Token: 0x060064ED RID: 25837 RVA: 0x001E291C File Offset: 0x001E291C
		public virtual void ShowReplaceDialog(string findText)
		{
			bool readOnly = this.ReadOnly;
			if (!readOnly)
			{
				bool flag = this.replaceForm == null;
				if (flag)
				{
					this.replaceForm = new ReplaceForm(this);
				}
				bool flag2 = findText != null;
				if (flag2)
				{
					this.replaceForm.tbFind.Text = findText;
				}
				else
				{
					bool flag3 = !this.Selection.IsEmpty && this.Selection.Start.iLine == this.Selection.End.iLine;
					if (flag3)
					{
						this.replaceForm.tbFind.Text = this.Selection.Text;
					}
				}
				this.replaceForm.tbFind.SelectAll();
				this.replaceForm.Show();
				this.replaceForm.Focus();
			}
		}

		// Token: 0x060064EE RID: 25838 RVA: 0x001E2A04 File Offset: 0x001E2A04
		public int GetLineLength(int iLine)
		{
			bool flag = iLine < 0 || iLine >= this.lines.Count;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("Line index out of range");
			}
			return this.lines[iLine].Count;
		}

		// Token: 0x060064EF RID: 25839 RVA: 0x001E2A60 File Offset: 0x001E2A60
		public Range GetLine(int iLine)
		{
			bool flag = iLine < 0 || iLine >= this.lines.Count;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("Line index out of range");
			}
			return new Range(this)
			{
				Start = new Place(0, iLine),
				End = new Place(this.lines[iLine].Count, iLine)
			};
		}

		// Token: 0x060064F0 RID: 25840 RVA: 0x001E2AE0 File Offset: 0x001E2AE0
		public virtual void Copy()
		{
			bool isEmpty = this.Selection.IsEmpty;
			if (isEmpty)
			{
				this.Selection.Expand();
			}
			bool flag = !this.Selection.IsEmpty;
			if (flag)
			{
				DataObject data = new DataObject();
				this.OnCreateClipboardData(data);
				Thread thread = new Thread(delegate()
				{
					this.SetClipboard(data);
				});
				thread.SetApartmentState(ApartmentState.STA);
				thread.Start();
				thread.Join();
			}
		}

		// Token: 0x060064F1 RID: 25841 RVA: 0x001E2B74 File Offset: 0x001E2B74
		protected virtual void OnCreateClipboardData(DataObject data)
		{
			string html = "<pre>" + new ExportToHTML
			{
				UseBr = false,
				UseNbsp = false,
				UseStyleTag = true
			}.GetHtml(this.Selection.Clone()) + "</pre>";
			data.SetData(DataFormats.UnicodeText, true, this.Selection.Text);
			data.SetData(DataFormats.Html, FastColoredTextBox.PrepareHtmlForClipboard(html));
			data.SetData(DataFormats.Rtf, new ExportToRTF().GetRtf(this.Selection.Clone()));
		}

		// Token: 0x060064F2 RID: 25842
		[DllImport("user32.dll")]
		private static extern IntPtr GetOpenClipboardWindow();

		// Token: 0x060064F3 RID: 25843
		[DllImport("user32.dll")]
		private static extern IntPtr CloseClipboard();

		// Token: 0x060064F4 RID: 25844 RVA: 0x001E2C10 File Offset: 0x001E2C10
		protected void SetClipboard(DataObject data)
		{
			try
			{
				FastColoredTextBox.CloseClipboard();
				Clipboard.SetDataObject(data, true, 5, 100);
			}
			catch (ExternalException)
			{
			}
		}

		// Token: 0x060064F5 RID: 25845 RVA: 0x001E2C50 File Offset: 0x001E2C50
		public static MemoryStream PrepareHtmlForClipboard(string html)
		{
			Encoding utf = Encoding.UTF8;
			string format = "Version:0.9\r\nStartHTML:{0:000000}\r\nEndHTML:{1:000000}\r\nStartFragment:{2:000000}\r\nEndFragment:{3:000000}\r\n";
			string text = "<html>\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=" + utf.WebName + "\">\r\n<title>HTML clipboard</title>\r\n</head>\r\n<body>\r\n<!--StartFragment-->";
			string text2 = "<!--EndFragment-->\r\n</body>\r\n</html>\r\n";
			string s = string.Format(format, new object[]
			{
				0,
				0,
				0,
				0
			});
			int byteCount = utf.GetByteCount(s);
			int byteCount2 = utf.GetByteCount(text);
			int byteCount3 = utf.GetByteCount(html);
			int byteCount4 = utf.GetByteCount(text2);
			string s2 = string.Format(format, new object[]
			{
				byteCount,
				byteCount + byteCount2 + byteCount3 + byteCount4,
				byteCount + byteCount2,
				byteCount + byteCount2 + byteCount3
			}) + text + html + text2;
			return new MemoryStream(utf.GetBytes(s2));
		}

		// Token: 0x060064F6 RID: 25846 RVA: 0x001E2D48 File Offset: 0x001E2D48
		public virtual void Cut()
		{
			bool flag = !this.Selection.IsEmpty;
			if (flag)
			{
				this.Copy();
				this.ClearSelected();
			}
			else
			{
				bool flag2 = this.LinesCount == 1;
				if (flag2)
				{
					this.Selection.SelectAll();
					this.Copy();
					this.ClearSelected();
				}
				else
				{
					DataObject data = new DataObject();
					this.OnCreateClipboardData(data);
					Thread thread = new Thread(delegate()
					{
						this.SetClipboard(data);
					});
					thread.SetApartmentState(ApartmentState.STA);
					thread.Start();
					thread.Join();
					bool flag3 = this.Selection.Start.iLine >= 0 && this.Selection.Start.iLine < this.LinesCount;
					if (flag3)
					{
						int iLine = this.Selection.Start.iLine;
						this.RemoveLines(new List<int>
						{
							iLine
						});
						this.Selection.Start = new Place(0, Math.Max(0, Math.Min(iLine, this.LinesCount - 1)));
					}
				}
			}
		}

		// Token: 0x060064F7 RID: 25847 RVA: 0x001E2E94 File Offset: 0x001E2E94
		public virtual void Paste()
		{
			string text = null;
			Thread thread = new Thread(delegate()
			{
				bool flag3 = Clipboard.ContainsText();
				if (flag3)
				{
					text = Clipboard.GetText();
				}
			});
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
			thread.Join();
			bool flag = this.Pasting != null;
			if (flag)
			{
				TextChangingEventArgs textChangingEventArgs = new TextChangingEventArgs
				{
					Cancel = false,
					InsertingText = text
				};
				this.Pasting(this, textChangingEventArgs);
				bool cancel = textChangingEventArgs.Cancel;
				if (cancel)
				{
					text = string.Empty;
				}
				else
				{
					text = textChangingEventArgs.InsertingText;
				}
			}
			bool flag2 = !string.IsNullOrEmpty(text);
			if (flag2)
			{
				this.InsertText(text);
			}
		}

		// Token: 0x060064F8 RID: 25848 RVA: 0x001E2F68 File Offset: 0x001E2F68
		public void SelectAll()
		{
			this.Selection.SelectAll();
		}

		// Token: 0x060064F9 RID: 25849 RVA: 0x001E2F78 File Offset: 0x001E2F78
		public void GoEnd()
		{
			bool flag = this.lines.Count > 0;
			if (flag)
			{
				this.Selection.Start = new Place(this.lines[this.lines.Count - 1].Count, this.lines.Count - 1);
			}
			else
			{
				this.Selection.Start = new Place(0, 0);
			}
			this.DoCaretVisible();
		}

		// Token: 0x060064FA RID: 25850 RVA: 0x001E2FFC File Offset: 0x001E2FFC
		public void GoHome()
		{
			this.Selection.Start = new Place(0, 0);
			this.DoCaretVisible();
		}

		// Token: 0x060064FB RID: 25851 RVA: 0x001E301C File Offset: 0x001E301C
		public virtual void Clear()
		{
			this.Selection.BeginUpdate();
			try
			{
				this.Selection.SelectAll();
				this.ClearSelected();
				this.lines.Manager.ClearHistory();
				this.Invalidate();
			}
			finally
			{
				this.Selection.EndUpdate();
			}
		}

		// Token: 0x060064FC RID: 25852 RVA: 0x001E3088 File Offset: 0x001E3088
		public void ClearStylesBuffer()
		{
			for (int i = 0; i < this.Styles.Length; i++)
			{
				this.Styles[i] = null;
			}
		}

		// Token: 0x060064FD RID: 25853 RVA: 0x001E30C4 File Offset: 0x001E30C4
		public void ClearStyle(StyleIndex styleIndex)
		{
			foreach (Line line in this.lines)
			{
				line.ClearStyle(styleIndex);
			}
			for (int i = 0; i < this.LineInfos.Count; i++)
			{
				this.SetVisibleState(i, VisibleState.Visible);
			}
			this.Invalidate();
		}

		// Token: 0x060064FE RID: 25854 RVA: 0x001E314C File Offset: 0x001E314C
		public void ClearUndo()
		{
			this.lines.Manager.ClearHistory();
		}

		// Token: 0x060064FF RID: 25855 RVA: 0x001E3160 File Offset: 0x001E3160
		public virtual void InsertText(string text)
		{
			this.InsertText(text, true);
		}

		// Token: 0x06006500 RID: 25856 RVA: 0x001E316C File Offset: 0x001E316C
		public virtual void InsertText(string text, bool jumpToCaret)
		{
			bool flag = text == null;
			if (!flag)
			{
				bool flag2 = text == "\r";
				if (flag2)
				{
					text = "\n";
				}
				this.lines.Manager.BeginAutoUndoCommands();
				try
				{
					bool flag3 = !this.Selection.IsEmpty;
					if (flag3)
					{
						this.lines.Manager.ExecuteCommand(new ClearSelectedCommand(this.TextSource));
					}
					bool flag4 = this.TextSource.Count > 0;
					if (flag4)
					{
						bool flag5 = this.Selection.IsEmpty && this.Selection.Start.iChar > this.GetLineLength(this.Selection.Start.iLine) && this.VirtualSpace;
						if (flag5)
						{
							this.InsertVirtualSpaces();
						}
					}
					this.lines.Manager.ExecuteCommand(new InsertTextCommand(this.TextSource, text));
					bool flag6 = this.updating <= 0 && jumpToCaret;
					if (flag6)
					{
						this.DoCaretVisible();
					}
				}
				finally
				{
					this.lines.Manager.EndAutoUndoCommands();
				}
				this.Invalidate();
			}
		}

		// Token: 0x06006501 RID: 25857 RVA: 0x001E32C0 File Offset: 0x001E32C0
		public virtual Range InsertText(string text, Style style)
		{
			return this.InsertText(text, style, true);
		}

		// Token: 0x06006502 RID: 25858 RVA: 0x001E32E4 File Offset: 0x001E32E4
		public virtual Range InsertText(string text, Style style, bool jumpToCaret)
		{
			bool flag = text == null;
			Range result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Place start = (this.Selection.Start > this.Selection.End) ? this.Selection.End : this.Selection.Start;
				this.InsertText(text, jumpToCaret);
				Range range = new Range(this, start, this.Selection.Start)
				{
					ColumnSelectionMode = this.Selection.ColumnSelectionMode
				};
				range = range.GetIntersectionWith(this.Range);
				range.SetStyle(style);
				result = range;
			}
			return result;
		}

		// Token: 0x06006503 RID: 25859 RVA: 0x001E3390 File Offset: 0x001E3390
		public virtual Range InsertTextAndRestoreSelection(Range replaceRange, string text, Style style)
		{
			bool flag = text == null;
			Range result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = this.PlaceToPosition(this.Selection.Start);
				int num2 = this.PlaceToPosition(this.Selection.End);
				int num3 = replaceRange.Text.Length;
				int num4 = this.PlaceToPosition(replaceRange.Start);
				this.Selection.BeginUpdate();
				this.Selection = replaceRange;
				Range range = this.InsertText(text, style);
				num3 = range.Text.Length - num3;
				this.Selection.Start = this.PositionToPlace(num + ((num >= num4) ? num3 : 0));
				this.Selection.End = this.PositionToPlace(num2 + ((num2 >= num4) ? num3 : 0));
				this.Selection.EndUpdate();
				result = range;
			}
			return result;
		}

		// Token: 0x06006504 RID: 25860 RVA: 0x001E3480 File Offset: 0x001E3480
		public virtual void AppendText(string text)
		{
			this.AppendText(text, null);
		}

		// Token: 0x06006505 RID: 25861 RVA: 0x001E348C File Offset: 0x001E348C
		public virtual void AppendText(string text, Style style)
		{
			bool flag = text == null;
			if (!flag)
			{
				this.Selection.ColumnSelectionMode = false;
				Place start = this.Selection.Start;
				Place end = this.Selection.End;
				this.Selection.BeginUpdate();
				this.lines.Manager.BeginAutoUndoCommands();
				try
				{
					bool flag2 = this.lines.Count > 0;
					if (flag2)
					{
						this.Selection.Start = new Place(this.lines[this.lines.Count - 1].Count, this.lines.Count - 1);
					}
					else
					{
						this.Selection.Start = new Place(0, 0);
					}
					Place start2 = this.Selection.Start;
					this.lines.Manager.ExecuteCommand(new InsertTextCommand(this.TextSource, text));
					bool flag3 = style != null;
					if (flag3)
					{
						new Range(this, start2, this.Selection.Start).SetStyle(style);
					}
				}
				finally
				{
					this.lines.Manager.EndAutoUndoCommands();
					this.Selection.Start = start;
					this.Selection.End = end;
					this.Selection.EndUpdate();
				}
				this.Invalidate();
			}
		}

		// Token: 0x06006506 RID: 25862 RVA: 0x001E3600 File Offset: 0x001E3600
		public int GetStyleIndex(Style style)
		{
			return Array.IndexOf<Style>(this.Styles, style);
		}

		// Token: 0x06006507 RID: 25863 RVA: 0x001E3628 File Offset: 0x001E3628
		public StyleIndex GetStyleIndexMask(Style[] styles)
		{
			StyleIndex styleIndex = StyleIndex.None;
			foreach (Style style in styles)
			{
				int styleIndex2 = this.GetStyleIndex(style);
				bool flag = styleIndex2 >= 0;
				if (flag)
				{
					styleIndex |= Range.ToStyleIndex(styleIndex2);
				}
			}
			return styleIndex;
		}

		// Token: 0x06006508 RID: 25864 RVA: 0x001E3688 File Offset: 0x001E3688
		internal int GetOrSetStyleLayerIndex(Style style)
		{
			int num = this.GetStyleIndex(style);
			bool flag = num < 0;
			if (flag)
			{
				num = this.AddStyle(style);
			}
			return num;
		}

		// Token: 0x06006509 RID: 25865 RVA: 0x001E36C0 File Offset: 0x001E36C0
		public static SizeF GetCharSize(Font font, char c)
		{
			Size size = TextRenderer.MeasureText("<" + c.ToString() + ">", font);
			Size size2 = TextRenderer.MeasureText("<>", font);
			return new SizeF((float)(size.Width - size2.Width + 1), (float)font.Height);
		}

		// Token: 0x0600650A RID: 25866
		[DllImport("Imm32.dll")]
		public static extern IntPtr ImmGetContext(IntPtr hWnd);

		// Token: 0x0600650B RID: 25867
		[DllImport("Imm32.dll")]
		public static extern IntPtr ImmAssociateContext(IntPtr hWnd, IntPtr hIMC);

		// Token: 0x0600650C RID: 25868 RVA: 0x001E3720 File Offset: 0x001E3720
		protected override void WndProc(ref Message m)
		{
			bool flag = m.Msg == 276 || m.Msg == 277;
			if (flag)
			{
				bool flag2 = m.WParam.ToInt32() != 8;
				if (flag2)
				{
					this.Invalidate();
				}
			}
			base.WndProc(ref m);
			bool imeAllowed = this.ImeAllowed;
			if (imeAllowed)
			{
				bool flag3 = m.Msg == 641 && m.WParam.ToInt32() == 1;
				if (flag3)
				{
					FastColoredTextBox.ImmAssociateContext(base.Handle, this.m_hImc);
				}
			}
		}

		// Token: 0x0600650D RID: 25869 RVA: 0x001E37D8 File Offset: 0x001E37D8
		private void HideHints()
		{
			bool flag = !this.ShowScrollBars && this.Hints.Count > 0;
			if (flag)
			{
				base.SuspendLayout();
				foreach (object obj in base.Controls)
				{
					Control item = (Control)obj;
					this.tempHintsList.Add(item);
				}
				base.Controls.Clear();
			}
		}

		// Token: 0x0600650E RID: 25870 RVA: 0x001E3880 File Offset: 0x001E3880
		private void RestoreHints()
		{
			bool flag = !this.ShowScrollBars && this.Hints.Count > 0;
			if (flag)
			{
				foreach (Control value in this.tempHintsList)
				{
					base.Controls.Add(value);
				}
				this.tempHintsList.Clear();
				base.ResumeLayout(false);
				bool flag2 = !this.Focused;
				if (flag2)
				{
					base.Focus();
				}
			}
		}

		// Token: 0x0600650F RID: 25871 RVA: 0x001E3938 File Offset: 0x001E3938
		public void OnScroll(ScrollEventArgs se, bool alignByLines)
		{
			this.HideHints();
			bool flag = se.ScrollOrientation == ScrollOrientation.VerticalScroll;
			if (flag)
			{
				int num = se.NewValue;
				if (alignByLines)
				{
					num = (int)(Math.Ceiling(1.0 * (double)num / (double)this.CharHeight) * (double)this.CharHeight);
				}
				base.VerticalScroll.Value = Math.Max(base.VerticalScroll.Minimum, Math.Min(base.VerticalScroll.Maximum, num));
			}
			bool flag2 = se.ScrollOrientation == ScrollOrientation.HorizontalScroll;
			if (flag2)
			{
				base.HorizontalScroll.Value = Math.Max(base.HorizontalScroll.Minimum, Math.Min(base.HorizontalScroll.Maximum, se.NewValue));
			}
			this.UpdateScrollbars();
			this.RestoreHints();
			this.Invalidate();
			base.OnScroll(se);
			this.OnVisibleRangeChanged();
		}

		// Token: 0x06006510 RID: 25872 RVA: 0x001E3A28 File Offset: 0x001E3A28
		protected override void OnScroll(ScrollEventArgs se)
		{
			this.OnScroll(se, true);
		}

		// Token: 0x06006511 RID: 25873 RVA: 0x001E3A34 File Offset: 0x001E3A34
		protected virtual void InsertChar(char c)
		{
			this.lines.Manager.BeginAutoUndoCommands();
			try
			{
				bool flag = !this.Selection.IsEmpty;
				if (flag)
				{
					this.lines.Manager.ExecuteCommand(new ClearSelectedCommand(this.TextSource));
				}
				bool flag2 = this.Selection.IsEmpty && this.Selection.Start.iChar > this.GetLineLength(this.Selection.Start.iLine) && this.VirtualSpace;
				if (flag2)
				{
					this.InsertVirtualSpaces();
				}
				this.lines.Manager.ExecuteCommand(new InsertCharCommand(this.TextSource, c));
			}
			finally
			{
				this.lines.Manager.EndAutoUndoCommands();
			}
			this.Invalidate();
		}

		// Token: 0x06006512 RID: 25874 RVA: 0x001E3B28 File Offset: 0x001E3B28
		private void InsertVirtualSpaces()
		{
			int lineLength = this.GetLineLength(this.Selection.Start.iLine);
			int count = this.Selection.Start.iChar - lineLength;
			this.Selection.BeginUpdate();
			try
			{
				this.Selection.Start = new Place(lineLength, this.Selection.Start.iLine);
				this.lines.Manager.ExecuteCommand(new InsertTextCommand(this.TextSource, new string(' ', count)));
			}
			finally
			{
				this.Selection.EndUpdate();
			}
		}

		// Token: 0x06006513 RID: 25875 RVA: 0x001E3BD8 File Offset: 0x001E3BD8
		public virtual void ClearSelected()
		{
			bool flag = !this.Selection.IsEmpty;
			if (flag)
			{
				this.lines.Manager.ExecuteCommand(new ClearSelectedCommand(this.TextSource));
				this.Invalidate();
			}
		}

		// Token: 0x06006514 RID: 25876 RVA: 0x001E3C24 File Offset: 0x001E3C24
		public void ClearCurrentLine()
		{
			this.Selection.Expand();
			this.lines.Manager.ExecuteCommand(new ClearSelectedCommand(this.TextSource));
			bool flag = this.Selection.Start.iLine == 0;
			if (flag)
			{
				bool flag2 = !this.Selection.GoRightThroughFolded();
				if (flag2)
				{
					return;
				}
			}
			bool flag3 = this.Selection.Start.iLine > 0;
			if (flag3)
			{
				this.lines.Manager.ExecuteCommand(new InsertCharCommand(this.TextSource, '\b'));
			}
			this.Invalidate();
		}

		// Token: 0x06006515 RID: 25877 RVA: 0x001E3CD0 File Offset: 0x001E3CD0
		private void Recalc()
		{
			bool flag = !this.needRecalc;
			if (!flag)
			{
				this.needRecalc = false;
				this.LeftIndent = this.LeftPadding;
				long num = (long)this.LinesCount + (long)((ulong)this.lineNumberStartValue) - 1L;
				int num2 = 2 + ((num > 0L) ? ((int)Math.Log10((double)num)) : 0);
				bool flag2 = this.ReservedCountOfLineNumberChars + 1 > num2;
				if (flag2)
				{
					num2 = this.ReservedCountOfLineNumberChars + 1;
				}
				bool created = base.Created;
				if (created)
				{
					bool flag3 = this.ShowLineNumbers;
					if (flag3)
					{
						this.LeftIndent += num2 * this.CharWidth + 8 + 1;
					}
					bool flag4 = this.needRecalcWordWrap;
					if (flag4)
					{
						this.RecalcWordWrap(this.needRecalcWordWrapInterval.X, this.needRecalcWordWrapInterval.Y);
						this.needRecalcWordWrap = false;
					}
				}
				else
				{
					this.needRecalc = true;
				}
				this.TextHeight = 0;
				this.maxLineLength = this.RecalcMaxLineLength();
				int width;
				this.CalcMinAutosizeWidth(out width, ref this.maxLineLength);
				this.AutoScrollMinSize = new Size(width, this.TextHeight + this.Paddings.Top + this.Paddings.Bottom);
				this.UpdateScrollbars();
			}
		}

		// Token: 0x06006516 RID: 25878 RVA: 0x001E3E2C File Offset: 0x001E3E2C
		private void CalcMinAutosizeWidth(out int minWidth, ref int maxLineLength)
		{
			minWidth = this.LeftIndent + maxLineLength * this.CharWidth + 2 + this.Paddings.Left + this.Paddings.Right;
			bool flag = this.wordWrap;
			if (flag)
			{
				switch (this.WordWrapMode)
				{
				case WordWrapMode.WordWrapControlWidth:
				case WordWrapMode.CharWrapControlWidth:
					maxLineLength = Math.Min(maxLineLength, (base.ClientSize.Width - this.LeftIndent - this.Paddings.Left - this.Paddings.Right) / this.CharWidth);
					minWidth = 0;
					break;
				case WordWrapMode.WordWrapPreferredWidth:
				case WordWrapMode.CharWrapPreferredWidth:
					maxLineLength = Math.Min(maxLineLength, this.PreferredLineWidth);
					minWidth = this.LeftIndent + this.PreferredLineWidth * this.CharWidth + 2 + this.Paddings.Left + this.Paddings.Right;
					break;
				}
			}
		}

		// Token: 0x06006517 RID: 25879 RVA: 0x001E3F34 File Offset: 0x001E3F34
		private void RecalcScrollByOneLine(int iLine)
		{
			bool flag = iLine >= this.lines.Count;
			if (!flag)
			{
				int count = this.lines[iLine].Count;
				bool flag2 = this.maxLineLength < count && !this.WordWrap;
				if (flag2)
				{
					this.maxLineLength = count;
				}
				int num;
				this.CalcMinAutosizeWidth(out num, ref count);
				bool flag3 = this.AutoScrollMinSize.Width < num;
				if (flag3)
				{
					this.AutoScrollMinSize = new Size(num, this.AutoScrollMinSize.Height);
				}
			}
		}

		// Token: 0x06006518 RID: 25880 RVA: 0x001E3FE0 File Offset: 0x001E3FE0
		private int RecalcMaxLineLength()
		{
			int num = 0;
			TextSource textSource = this.lines;
			int count = textSource.Count;
			int num2 = this.CharHeight;
			int top = this.Paddings.Top;
			this.TextHeight = top;
			for (int i = 0; i < count; i++)
			{
				int lineLength = textSource.GetLineLength(i);
				LineInfo lineInfo = this.LineInfos[i];
				bool flag = lineLength > num && lineInfo.VisibleState == VisibleState.Visible;
				if (flag)
				{
					num = lineLength;
				}
				lineInfo.startY = this.TextHeight;
				this.TextHeight += lineInfo.WordWrapStringsCount * num2 + lineInfo.bottomPadding;
				this.LineInfos[i] = lineInfo;
			}
			this.TextHeight -= top;
			return num;
		}

		// Token: 0x06006519 RID: 25881 RVA: 0x001E40D4 File Offset: 0x001E40D4
		private int GetMaxLineWordWrapedWidth()
		{
			bool flag = this.wordWrap;
			if (flag)
			{
				switch (this.wordWrapMode)
				{
				case WordWrapMode.WordWrapControlWidth:
				case WordWrapMode.CharWrapControlWidth:
					return base.ClientSize.Width;
				case WordWrapMode.WordWrapPreferredWidth:
				case WordWrapMode.CharWrapPreferredWidth:
					return this.LeftIndent + this.PreferredLineWidth * this.CharWidth + 2 + this.Paddings.Left + this.Paddings.Right;
				}
			}
			return int.MaxValue;
		}

		// Token: 0x0600651A RID: 25882 RVA: 0x001E4174 File Offset: 0x001E4174
		private void RecalcWordWrap(int fromLine, int toLine)
		{
			int num = 0;
			bool charWrap = false;
			toLine = Math.Min(this.LinesCount - 1, toLine);
			switch (this.WordWrapMode)
			{
			case WordWrapMode.WordWrapControlWidth:
				num = (base.ClientSize.Width - this.LeftIndent - this.Paddings.Left - this.Paddings.Right) / this.CharWidth;
				break;
			case WordWrapMode.WordWrapPreferredWidth:
				num = this.PreferredLineWidth;
				break;
			case WordWrapMode.CharWrapControlWidth:
				num = (base.ClientSize.Width - this.LeftIndent - this.Paddings.Left - this.Paddings.Right) / this.CharWidth;
				charWrap = true;
				break;
			case WordWrapMode.CharWrapPreferredWidth:
				num = this.PreferredLineWidth;
				charWrap = true;
				break;
			}
			for (int i = fromLine; i <= toLine; i++)
			{
				bool flag = this.lines.IsLineLoaded(i);
				if (flag)
				{
					bool flag2 = !this.wordWrap;
					if (flag2)
					{
						this.LineInfos[i].CutOffPositions.Clear();
					}
					else
					{
						LineInfo lineInfo = this.LineInfos[i];
						lineInfo.wordWrapIndent = (this.WordWrapAutoIndent ? (this.lines[i].StartSpacesCount + this.WordWrapIndent) : this.WordWrapIndent);
						bool flag3 = this.WordWrapMode == WordWrapMode.Custom;
						if (flag3)
						{
							bool flag4 = this.WordWrapNeeded != null;
							if (flag4)
							{
								this.WordWrapNeeded(this, new WordWrapNeededEventArgs(lineInfo.CutOffPositions, this.ImeAllowed, this.lines[i]));
							}
						}
						else
						{
							FastColoredTextBox.CalcCutOffs(lineInfo.CutOffPositions, num, num - lineInfo.wordWrapIndent, this.ImeAllowed, charWrap, this.lines[i]);
						}
						this.LineInfos[i] = lineInfo;
					}
				}
			}
			this.needRecalc = true;
		}

		// Token: 0x0600651B RID: 25883 RVA: 0x001E43A8 File Offset: 0x001E43A8
		public static void CalcCutOffs(List<int> cutOffPositions, int maxCharsPerLine, int maxCharsPerSecondaryLine, bool allowIME, bool charWrap, Line line)
		{
			bool flag = maxCharsPerSecondaryLine < 1;
			if (flag)
			{
				maxCharsPerSecondaryLine = 1;
			}
			bool flag2 = maxCharsPerLine < 1;
			if (flag2)
			{
				maxCharsPerLine = 1;
			}
			int num = 0;
			int num2 = 0;
			cutOffPositions.Clear();
			for (int i = 0; i < line.Count - 1; i++)
			{
				char c = line[i].c;
				if (charWrap)
				{
					num2 = i + 1;
				}
				else
				{
					bool flag3 = allowIME && FastColoredTextBox.IsCJKLetter(c);
					if (flag3)
					{
						num2 = i;
					}
					else
					{
						bool flag4 = !char.IsLetterOrDigit(c) && c != '_' && c != '\'' && c != '\u00a0' && ((c != '.' && c != ',') || !char.IsDigit(line[i + 1].c));
						if (flag4)
						{
							num2 = Math.Min(i + 1, line.Count - 1);
						}
					}
				}
				num++;
				bool flag5 = num == maxCharsPerLine;
				if (flag5)
				{
					bool flag6 = num2 == 0 || (cutOffPositions.Count > 0 && num2 == cutOffPositions[cutOffPositions.Count - 1]);
					if (flag6)
					{
						num2 = i + 1;
					}
					cutOffPositions.Add(num2);
					num = 1 + i - num2;
					maxCharsPerLine = maxCharsPerSecondaryLine;
				}
			}
		}

		// Token: 0x0600651C RID: 25884 RVA: 0x001E4534 File Offset: 0x001E4534
		public static bool IsCJKLetter(char c)
		{
			int num = Convert.ToInt32(c);
			return (num >= 13056 && num <= 13311) || (num >= 65072 && num <= 65103) || (num >= 63744 && num <= 64255) || (num >= 11904 && num <= 12031) || (num >= 12736 && num <= 12783) || (num >= 19968 && num <= 40959) || (num >= 13312 && num <= 19903) || (num >= 12800 && num <= 13055) || (num >= 9312 && num <= 9471) || (num >= 12352 && num <= 12447) || (num >= 12032 && num <= 12255) || (num >= 12704 && num <= 12735) || (num >= 19904 && num <= 19967) || (num >= 12544 && num <= 12591) || (num >= 12448 && num <= 12543) || (num >= 12784 && num <= 12799) || (num >= 12272 && num <= 12287) || (num >= 4352 && num <= 4607) || (num >= 43360 && num <= 43391) || (num >= 55216 && num <= 55295) || (num >= 12592 && num <= 12687) || (num >= 44032 && num <= 55215);
		}

		// Token: 0x0600651D RID: 25885 RVA: 0x001E4744 File Offset: 0x001E4744
		protected override void OnClientSizeChanged(EventArgs e)
		{
			base.OnClientSizeChanged(e);
			bool flag = this.WordWrap;
			if (flag)
			{
				this.NeedRecalc(false, true);
				this.Invalidate();
			}
			this.OnVisibleRangeChanged();
			this.UpdateScrollbars();
		}

		// Token: 0x0600651E RID: 25886 RVA: 0x001E478C File Offset: 0x001E478C
		internal void DoVisibleRectangle(Rectangle rect)
		{
			this.HideHints();
			int value = base.VerticalScroll.Value;
			int num = base.VerticalScroll.Value;
			int num2 = base.HorizontalScroll.Value;
			bool flag = rect.Bottom > base.ClientRectangle.Height;
			if (flag)
			{
				num += rect.Bottom - base.ClientRectangle.Height;
			}
			else
			{
				bool flag2 = rect.Top < 0;
				if (flag2)
				{
					num += rect.Top;
				}
			}
			bool flag3 = rect.Right > base.ClientRectangle.Width;
			if (flag3)
			{
				num2 += rect.Right - base.ClientRectangle.Width;
			}
			else
			{
				bool flag4 = rect.Left < this.LeftIndent;
				if (flag4)
				{
					num2 += rect.Left - this.LeftIndent;
				}
			}
			bool flag5 = !this.Multiline;
			if (flag5)
			{
				num = 0;
			}
			num = Math.Max(base.VerticalScroll.Minimum, num);
			num2 = Math.Max(base.HorizontalScroll.Minimum, num2);
			try
			{
				bool flag6 = base.VerticalScroll.Visible || !this.ShowScrollBars;
				if (flag6)
				{
					base.VerticalScroll.Value = Math.Min(num, base.VerticalScroll.Maximum);
				}
				bool flag7 = base.HorizontalScroll.Visible || !this.ShowScrollBars;
				if (flag7)
				{
					base.HorizontalScroll.Value = Math.Min(num2, base.HorizontalScroll.Maximum);
				}
			}
			catch (ArgumentOutOfRangeException)
			{
			}
			this.UpdateScrollbars();
			this.RestoreHints();
			bool flag8 = value != base.VerticalScroll.Value;
			if (flag8)
			{
				this.OnVisibleRangeChanged();
			}
		}

		// Token: 0x0600651F RID: 25887 RVA: 0x001E4998 File Offset: 0x001E4998
		public void UpdateScrollbars()
		{
			bool showScrollBars = this.ShowScrollBars;
			if (showScrollBars)
			{
				base.AutoScrollMinSize -= new Size(1, 0);
				base.AutoScrollMinSize += new Size(1, 0);
			}
			else
			{
				base.PerformLayout();
			}
			bool isHandleCreated = base.IsHandleCreated;
			if (isHandleCreated)
			{
				base.BeginInvoke(new MethodInvoker(this.OnScrollbarsUpdated));
			}
		}

		// Token: 0x06006520 RID: 25888 RVA: 0x001E4A18 File Offset: 0x001E4A18
		protected virtual void OnScrollbarsUpdated()
		{
			bool flag = this.ScrollbarsUpdated != null;
			if (flag)
			{
				this.ScrollbarsUpdated(this, EventArgs.Empty);
			}
		}

		// Token: 0x06006521 RID: 25889 RVA: 0x001E4A4C File Offset: 0x001E4A4C
		public void DoCaretVisible()
		{
			this.Invalidate();
			this.Recalc();
			Point location = this.PlaceToPoint(this.Selection.Start);
			location.Offset(-this.CharWidth, 0);
			this.DoVisibleRectangle(new Rectangle(location, new Size(2 * this.CharWidth, 2 * this.CharHeight)));
		}

		// Token: 0x06006522 RID: 25890 RVA: 0x001E4AB0 File Offset: 0x001E4AB0
		public void ScrollLeft()
		{
			this.Invalidate();
			base.HorizontalScroll.Value = 0;
			this.AutoScrollMinSize -= new Size(1, 0);
			this.AutoScrollMinSize += new Size(1, 0);
		}

		// Token: 0x06006523 RID: 25891 RVA: 0x001E4B08 File Offset: 0x001E4B08
		public void DoSelectionVisible()
		{
			bool flag = this.LineInfos[this.Selection.End.iLine].VisibleState > VisibleState.Visible;
			if (flag)
			{
				this.ExpandBlock(this.Selection.End.iLine);
			}
			bool flag2 = this.LineInfos[this.Selection.Start.iLine].VisibleState > VisibleState.Visible;
			if (flag2)
			{
				this.ExpandBlock(this.Selection.Start.iLine);
			}
			this.Recalc();
			this.DoVisibleRectangle(new Rectangle(this.PlaceToPoint(new Place(0, this.Selection.End.iLine)), new Size(2 * this.CharWidth, 2 * this.CharHeight)));
			Point location = this.PlaceToPoint(this.Selection.Start);
			Point point = this.PlaceToPoint(this.Selection.End);
			location.Offset(-this.CharWidth, -base.ClientSize.Height / 2);
			this.DoVisibleRectangle(new Rectangle(location, new Size(Math.Abs(point.X - location.X), base.ClientSize.Height)));
			this.Invalidate();
		}

		// Token: 0x06006524 RID: 25892 RVA: 0x001E4C64 File Offset: 0x001E4C64
		public void DoRangeVisible(Range range)
		{
			this.DoRangeVisible(range, false);
		}

		// Token: 0x06006525 RID: 25893 RVA: 0x001E4C70 File Offset: 0x001E4C70
		public void DoRangeVisible(Range range, bool tryToCentre)
		{
			range = range.Clone();
			range.Normalize();
			range.End = new Place(range.End.iChar, Math.Min(range.End.iLine, range.Start.iLine + base.ClientSize.Height / this.CharHeight));
			bool flag = this.LineInfos[range.End.iLine].VisibleState > VisibleState.Visible;
			if (flag)
			{
				this.ExpandBlock(range.End.iLine);
			}
			bool flag2 = this.LineInfos[range.Start.iLine].VisibleState > VisibleState.Visible;
			if (flag2)
			{
				this.ExpandBlock(range.Start.iLine);
			}
			this.Recalc();
			int height = (1 + range.End.iLine - range.Start.iLine) * this.CharHeight;
			Point location = this.PlaceToPoint(new Place(0, range.Start.iLine));
			if (tryToCentre)
			{
				location.Offset(0, -base.ClientSize.Height / 2);
				height = base.ClientSize.Height;
			}
			this.DoVisibleRectangle(new Rectangle(location, new Size(2 * this.CharWidth, height)));
			this.Invalidate();
		}

		// Token: 0x06006526 RID: 25894 RVA: 0x001E4DE0 File Offset: 0x001E4DE0
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			bool flag = e.KeyCode == Keys.ShiftKey;
			if (flag)
			{
				this.lastModifiers &= ~Keys.Shift;
			}
			bool flag2 = e.KeyCode == Keys.Alt;
			if (flag2)
			{
				this.lastModifiers &= ~Keys.Alt;
			}
			bool flag3 = e.KeyCode == Keys.ControlKey;
			if (flag3)
			{
				this.lastModifiers &= ~Keys.Control;
			}
		}

		// Token: 0x06006527 RID: 25895 RVA: 0x001E4E68 File Offset: 0x001E4E68
		protected override void OnKeyDown(KeyEventArgs e)
		{
			bool flag = this.middleClickScrollingActivated;
			if (!flag)
			{
				base.OnKeyDown(e);
				bool focused = this.Focused;
				if (focused)
				{
					this.lastModifiers = e.Modifiers;
				}
				this.handledChar = false;
				bool handled = e.Handled;
				if (handled)
				{
					this.handledChar = true;
				}
				else
				{
					bool flag2 = this.ProcessKey(e.KeyData);
					if (!flag2)
					{
						e.Handled = true;
						this.DoCaretVisible();
						this.Invalidate();
					}
				}
			}
		}

		// Token: 0x06006528 RID: 25896 RVA: 0x001E4EFC File Offset: 0x001E4EFC
		protected override bool ProcessDialogKey(Keys keyData)
		{
			bool flag = (keyData & Keys.Alt) > Keys.None;
			if (flag)
			{
				bool flag2 = this.HotkeysMapping.ContainsKey(keyData);
				if (flag2)
				{
					this.ProcessKey(keyData);
					return true;
				}
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x06006529 RID: 25897 RVA: 0x001E4F54 File Offset: 0x001E4F54
		public virtual bool ProcessKey(Keys keyData)
		{
			KeyEventArgs keyEventArgs = new KeyEventArgs(keyData);
			bool flag = keyEventArgs.KeyCode == Keys.Tab && !this.AcceptsTab;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.macrosManager != null;
				if (flag2)
				{
					bool flag3 = !this.HotkeysMapping.ContainsKey(keyData) || (this.HotkeysMapping[keyData] != FCTBAction.MacroExecute && this.HotkeysMapping[keyData] != FCTBAction.MacroRecord);
					if (flag3)
					{
						this.macrosManager.ProcessKey(keyData);
					}
				}
				bool flag4 = this.HotkeysMapping.ContainsKey(keyData);
				if (flag4)
				{
					FCTBAction fctbaction = this.HotkeysMapping[keyData];
					this.DoAction(fctbaction);
					bool flag5 = FastColoredTextBox.scrollActions.ContainsKey(fctbaction);
					if (flag5)
					{
						return true;
					}
					bool flag6 = keyData == Keys.Tab || keyData == (Keys.LButton | Keys.Back | Keys.Shift);
					if (flag6)
					{
						this.handledChar = true;
						return true;
					}
				}
				else
				{
					bool flag7 = keyEventArgs.KeyCode == Keys.Alt;
					if (flag7)
					{
						return true;
					}
					bool flag8 = (keyEventArgs.Modifiers & Keys.Control) > Keys.None;
					if (flag8)
					{
						return true;
					}
					bool flag9 = (keyEventArgs.Modifiers & Keys.Alt) > Keys.None;
					if (flag9)
					{
						bool flag10 = (Control.MouseButtons & MouseButtons.Left) > MouseButtons.None;
						if (flag10)
						{
							this.CheckAndChangeSelectionType();
						}
						return true;
					}
					bool flag11 = keyEventArgs.KeyCode == Keys.ShiftKey;
					if (flag11)
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x0600652A RID: 25898 RVA: 0x001E511C File Offset: 0x001E511C
		private void DoAction(FCTBAction action)
		{
			switch (action)
			{
			case FCTBAction.AutoIndentChars:
			{
				bool flag = !this.Selection.ReadOnly;
				if (flag)
				{
					this.DoAutoIndentChars(this.Selection.Start.iLine);
				}
				break;
			}
			case FCTBAction.BookmarkLine:
				this.BookmarkLine(this.Selection.Start.iLine);
				break;
			case FCTBAction.ClearHints:
			{
				this.ClearHints();
				bool flag2 = this.MacrosManager != null;
				if (flag2)
				{
					this.MacrosManager.IsRecording = false;
				}
				break;
			}
			case FCTBAction.ClearWordLeft:
			{
				bool flag3 = this.OnKeyPressing('\b');
				if (!flag3)
				{
					bool flag4 = !this.Selection.ReadOnly;
					if (flag4)
					{
						bool flag5 = !this.Selection.IsEmpty;
						if (flag5)
						{
							this.ClearSelected();
						}
						this.Selection.GoWordLeft(true);
						bool flag6 = !this.Selection.ReadOnly;
						if (flag6)
						{
							this.ClearSelected();
						}
					}
					this.OnKeyPressed('\b');
				}
				break;
			}
			case FCTBAction.ClearWordRight:
			{
				bool flag7 = this.OnKeyPressing('ÿ');
				if (!flag7)
				{
					bool flag8 = !this.Selection.ReadOnly;
					if (flag8)
					{
						bool flag9 = !this.Selection.IsEmpty;
						if (flag9)
						{
							this.ClearSelected();
						}
						this.Selection.GoWordRight(true, false);
						bool flag10 = !this.Selection.ReadOnly;
						if (flag10)
						{
							this.ClearSelected();
						}
					}
					this.OnKeyPressed('ÿ');
				}
				break;
			}
			case FCTBAction.CommentSelected:
				this.CommentSelected();
				break;
			case FCTBAction.Copy:
				this.Copy();
				break;
			case FCTBAction.Cut:
			{
				bool flag11 = !this.Selection.ReadOnly;
				if (flag11)
				{
					this.Cut();
				}
				break;
			}
			case FCTBAction.DeleteCharRight:
			{
				bool flag12 = !this.Selection.ReadOnly;
				if (flag12)
				{
					bool flag13 = this.OnKeyPressing('ÿ');
					if (!flag13)
					{
						bool flag14 = !this.Selection.IsEmpty;
						if (flag14)
						{
							this.ClearSelected();
						}
						else
						{
							bool flag15 = this[this.Selection.Start.iLine].StartSpacesCount == this[this.Selection.Start.iLine].Count;
							if (flag15)
							{
								this.RemoveSpacesAfterCaret();
							}
							bool flag16 = !this.Selection.IsReadOnlyRightChar();
							if (flag16)
							{
								bool flag17 = this.Selection.GoRightThroughFolded();
								if (flag17)
								{
									int iLine = this.Selection.Start.iLine;
									this.InsertChar('\b');
									bool flag18 = iLine != this.Selection.Start.iLine && this.AutoIndent;
									if (flag18)
									{
										bool flag19 = this.Selection.Start.iChar > 0;
										if (flag19)
										{
											this.RemoveSpacesAfterCaret();
										}
									}
								}
							}
						}
						bool autoIndentChars = this.AutoIndentChars;
						if (autoIndentChars)
						{
							this.DoAutoIndentChars(this.Selection.Start.iLine);
						}
						this.OnKeyPressed('ÿ');
					}
				}
				break;
			}
			case FCTBAction.FindChar:
				this.findCharMode = true;
				break;
			case FCTBAction.FindDialog:
				this.ShowFindDialog();
				break;
			case FCTBAction.FindNext:
			{
				bool flag20 = this.findForm == null || this.findForm.tbFind.Text == "";
				if (flag20)
				{
					this.ShowFindDialog();
				}
				else
				{
					this.findForm.FindNext(this.findForm.tbFind.Text);
				}
				break;
			}
			case FCTBAction.GoDown:
				this.Selection.GoDown(false);
				this.ScrollLeft();
				break;
			case FCTBAction.GoDownWithSelection:
				this.Selection.GoDown(true);
				this.ScrollLeft();
				break;
			case FCTBAction.GoDown_ColumnSelectionMode:
			{
				this.CheckAndChangeSelectionType();
				bool columnSelectionMode = this.Selection.ColumnSelectionMode;
				if (columnSelectionMode)
				{
					this.Selection.GoDown_ColumnSelectionMode();
				}
				this.Invalidate();
				break;
			}
			case FCTBAction.GoEnd:
				this.Selection.GoEnd(false);
				break;
			case FCTBAction.GoEndWithSelection:
				this.Selection.GoEnd(true);
				break;
			case FCTBAction.GoFirstLine:
				this.Selection.GoFirst(false);
				break;
			case FCTBAction.GoFirstLineWithSelection:
				this.Selection.GoFirst(true);
				break;
			case FCTBAction.GoHome:
				this.GoHome(false);
				this.ScrollLeft();
				break;
			case FCTBAction.GoHomeWithSelection:
				this.GoHome(true);
				this.ScrollLeft();
				break;
			case FCTBAction.GoLastLine:
				this.Selection.GoLast(false);
				break;
			case FCTBAction.GoLastLineWithSelection:
				this.Selection.GoLast(true);
				break;
			case FCTBAction.GoLeft:
				this.Selection.GoLeft(false);
				break;
			case FCTBAction.GoLeftWithSelection:
				this.Selection.GoLeft(true);
				break;
			case FCTBAction.GoLeft_ColumnSelectionMode:
			{
				this.CheckAndChangeSelectionType();
				bool columnSelectionMode2 = this.Selection.ColumnSelectionMode;
				if (columnSelectionMode2)
				{
					this.Selection.GoLeft_ColumnSelectionMode();
				}
				this.Invalidate();
				break;
			}
			case FCTBAction.GoPageDown:
				this.Selection.GoPageDown(false);
				this.ScrollLeft();
				break;
			case FCTBAction.GoPageDownWithSelection:
				this.Selection.GoPageDown(true);
				this.ScrollLeft();
				break;
			case FCTBAction.GoPageUp:
				this.Selection.GoPageUp(false);
				this.ScrollLeft();
				break;
			case FCTBAction.GoPageUpWithSelection:
				this.Selection.GoPageUp(true);
				this.ScrollLeft();
				break;
			case FCTBAction.GoRight:
				this.Selection.GoRight(false);
				break;
			case FCTBAction.GoRightWithSelection:
				this.Selection.GoRight(true);
				break;
			case FCTBAction.GoRight_ColumnSelectionMode:
			{
				this.CheckAndChangeSelectionType();
				bool columnSelectionMode3 = this.Selection.ColumnSelectionMode;
				if (columnSelectionMode3)
				{
					this.Selection.GoRight_ColumnSelectionMode();
				}
				this.Invalidate();
				break;
			}
			case FCTBAction.GoToDialog:
				this.ShowGoToDialog();
				break;
			case FCTBAction.GoNextBookmark:
				this.GotoNextBookmark(this.Selection.Start.iLine);
				break;
			case FCTBAction.GoPrevBookmark:
				this.GotoPrevBookmark(this.Selection.Start.iLine);
				break;
			case FCTBAction.GoUp:
				this.Selection.GoUp(false);
				this.ScrollLeft();
				break;
			case FCTBAction.GoUpWithSelection:
				this.Selection.GoUp(true);
				this.ScrollLeft();
				break;
			case FCTBAction.GoUp_ColumnSelectionMode:
			{
				this.CheckAndChangeSelectionType();
				bool columnSelectionMode4 = this.Selection.ColumnSelectionMode;
				if (columnSelectionMode4)
				{
					this.Selection.GoUp_ColumnSelectionMode();
				}
				this.Invalidate();
				break;
			}
			case FCTBAction.GoWordLeft:
				this.Selection.GoWordLeft(false);
				break;
			case FCTBAction.GoWordLeftWithSelection:
				this.Selection.GoWordLeft(true);
				break;
			case FCTBAction.GoWordRight:
				this.Selection.GoWordRight(false, true);
				break;
			case FCTBAction.GoWordRightWithSelection:
				this.Selection.GoWordRight(true, true);
				break;
			case FCTBAction.IndentIncrease:
			{
				bool flag21 = !this.Selection.ReadOnly;
				if (flag21)
				{
					Range range = this.Selection.Clone();
					bool flag22 = range.Start > range.End;
					range.Normalize();
					int startSpacesCount = this[range.Start.iLine].StartSpacesCount;
					bool flag23 = range.Start.iLine != range.End.iLine || (range.Start.iChar <= startSpacesCount && range.End.iChar == this[range.Start.iLine].Count) || range.End.iChar <= startSpacesCount;
					if (flag23)
					{
						this.IncreaseIndent();
						bool flag24 = range.Start.iLine == range.End.iLine && !range.IsEmpty;
						if (flag24)
						{
							this.Selection = new Range(this, this[range.Start.iLine].StartSpacesCount, range.End.iLine, this[range.Start.iLine].Count, range.End.iLine);
							bool flag25 = flag22;
							if (flag25)
							{
								this.Selection.Inverse();
							}
						}
					}
					else
					{
						this.ProcessKey('\t', Keys.None);
					}
				}
				break;
			}
			case FCTBAction.IndentDecrease:
			{
				bool flag26 = !this.Selection.ReadOnly;
				if (flag26)
				{
					Range range2 = this.Selection.Clone();
					bool flag27 = range2.Start.iLine == range2.End.iLine;
					if (flag27)
					{
						Line line = this[range2.Start.iLine];
						bool flag28 = range2.Start.iChar == 0 && range2.End.iChar == line.Count;
						if (flag28)
						{
							this.Selection = new Range(this, line.StartSpacesCount, range2.Start.iLine, line.Count, range2.Start.iLine);
						}
						else
						{
							bool flag29 = range2.Start.iChar == line.Count && range2.End.iChar == 0;
							if (flag29)
							{
								this.Selection = new Range(this, line.Count, range2.Start.iLine, line.StartSpacesCount, range2.Start.iLine);
							}
						}
					}
					this.DecreaseIndent();
				}
				break;
			}
			case FCTBAction.LowerCase:
			{
				bool flag30 = !this.Selection.ReadOnly;
				if (flag30)
				{
					this.LowerCase();
				}
				break;
			}
			case FCTBAction.MacroExecute:
			{
				bool flag31 = this.MacrosManager != null;
				if (flag31)
				{
					this.MacrosManager.IsRecording = false;
					this.MacrosManager.ExecuteMacros();
				}
				break;
			}
			case FCTBAction.MacroRecord:
			{
				bool flag32 = this.MacrosManager != null;
				if (flag32)
				{
					bool allowMacroRecordingByUser = this.MacrosManager.AllowMacroRecordingByUser;
					if (allowMacroRecordingByUser)
					{
						this.MacrosManager.IsRecording = !this.MacrosManager.IsRecording;
					}
					bool isRecording = this.MacrosManager.IsRecording;
					if (isRecording)
					{
						this.MacrosManager.ClearMacros();
					}
				}
				break;
			}
			case FCTBAction.MoveSelectedLinesDown:
			{
				bool flag33 = !this.Selection.ColumnSelectionMode;
				if (flag33)
				{
					this.MoveSelectedLinesDown();
				}
				break;
			}
			case FCTBAction.MoveSelectedLinesUp:
			{
				bool flag34 = !this.Selection.ColumnSelectionMode;
				if (flag34)
				{
					this.MoveSelectedLinesUp();
				}
				break;
			}
			case FCTBAction.NavigateBackward:
				this.NavigateBackward();
				break;
			case FCTBAction.NavigateForward:
				this.NavigateForward();
				break;
			case FCTBAction.Paste:
			{
				bool flag35 = !this.Selection.ReadOnly;
				if (flag35)
				{
					this.Paste();
				}
				break;
			}
			case FCTBAction.Redo:
			{
				bool flag36 = !this.ReadOnly;
				if (flag36)
				{
					this.Redo();
				}
				break;
			}
			case FCTBAction.ReplaceDialog:
				this.ShowReplaceDialog();
				break;
			case FCTBAction.ReplaceMode:
			{
				bool flag37 = !this.ReadOnly;
				if (flag37)
				{
					this.isReplaceMode = !this.isReplaceMode;
				}
				break;
			}
			case FCTBAction.ScrollDown:
				this.DoScrollVertical(1, -1);
				break;
			case FCTBAction.ScrollUp:
				this.DoScrollVertical(1, 1);
				break;
			case FCTBAction.SelectAll:
				this.Selection.SelectAll();
				break;
			case FCTBAction.UnbookmarkLine:
				this.UnbookmarkLine(this.Selection.Start.iLine);
				break;
			case FCTBAction.Undo:
			{
				bool flag38 = !this.ReadOnly;
				if (flag38)
				{
					this.Undo();
				}
				break;
			}
			case FCTBAction.UpperCase:
			{
				bool flag39 = !this.Selection.ReadOnly;
				if (flag39)
				{
					this.UpperCase();
				}
				break;
			}
			case FCTBAction.ZoomIn:
				this.ChangeFontSize(2);
				break;
			case FCTBAction.ZoomNormal:
				this.RestoreFontSize();
				break;
			case FCTBAction.ZoomOut:
				this.ChangeFontSize(-2);
				break;
			case FCTBAction.CustomAction1:
			case FCTBAction.CustomAction2:
			case FCTBAction.CustomAction3:
			case FCTBAction.CustomAction4:
			case FCTBAction.CustomAction5:
			case FCTBAction.CustomAction6:
			case FCTBAction.CustomAction7:
			case FCTBAction.CustomAction8:
			case FCTBAction.CustomAction9:
			case FCTBAction.CustomAction10:
			case FCTBAction.CustomAction11:
			case FCTBAction.CustomAction12:
			case FCTBAction.CustomAction13:
			case FCTBAction.CustomAction14:
			case FCTBAction.CustomAction15:
			case FCTBAction.CustomAction16:
			case FCTBAction.CustomAction17:
			case FCTBAction.CustomAction18:
			case FCTBAction.CustomAction19:
			case FCTBAction.CustomAction20:
				this.OnCustomAction(new CustomActionEventArgs(action));
				break;
			}
		}

		// Token: 0x0600652B RID: 25899 RVA: 0x001E5E5C File Offset: 0x001E5E5C
		protected virtual void OnCustomAction(CustomActionEventArgs e)
		{
			bool flag = this.CustomAction != null;
			if (flag)
			{
				this.CustomAction(this, e);
			}
		}

		// Token: 0x0600652C RID: 25900 RVA: 0x001E5E8C File Offset: 0x001E5E8C
		private void RestoreFontSize()
		{
			this.Zoom = 100;
		}

		// Token: 0x0600652D RID: 25901 RVA: 0x001E5E98 File Offset: 0x001E5E98
		public bool GotoNextBookmark(int iLine)
		{
			Bookmark bookmark = null;
			int num = int.MaxValue;
			Bookmark bookmark2 = null;
			int num2 = int.MaxValue;
			foreach (Bookmark bookmark3 in this.bookmarks)
			{
				bool flag = bookmark3.LineIndex < num2;
				if (flag)
				{
					num2 = bookmark3.LineIndex;
					bookmark2 = bookmark3;
				}
				bool flag2 = bookmark3.LineIndex > iLine && bookmark3.LineIndex < num;
				if (flag2)
				{
					num = bookmark3.LineIndex;
					bookmark = bookmark3;
				}
			}
			bool flag3 = bookmark != null;
			bool result;
			if (flag3)
			{
				bookmark.DoVisible();
				result = true;
			}
			else
			{
				bool flag4 = bookmark2 != null;
				if (flag4)
				{
					bookmark2.DoVisible();
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600652E RID: 25902 RVA: 0x001E5F9C File Offset: 0x001E5F9C
		public bool GotoPrevBookmark(int iLine)
		{
			Bookmark bookmark = null;
			int num = -1;
			Bookmark bookmark2 = null;
			int num2 = -1;
			foreach (Bookmark bookmark3 in this.bookmarks)
			{
				bool flag = bookmark3.LineIndex > num2;
				if (flag)
				{
					num2 = bookmark3.LineIndex;
					bookmark2 = bookmark3;
				}
				bool flag2 = bookmark3.LineIndex < iLine && bookmark3.LineIndex > num;
				if (flag2)
				{
					num = bookmark3.LineIndex;
					bookmark = bookmark3;
				}
			}
			bool flag3 = bookmark != null;
			bool result;
			if (flag3)
			{
				bookmark.DoVisible();
				result = true;
			}
			else
			{
				bool flag4 = bookmark2 != null;
				if (flag4)
				{
					bookmark2.DoVisible();
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600652F RID: 25903 RVA: 0x001E6098 File Offset: 0x001E6098
		public virtual void BookmarkLine(int iLine)
		{
			bool flag = !this.bookmarks.Contains(iLine);
			if (flag)
			{
				this.bookmarks.Add(iLine);
			}
		}

		// Token: 0x06006530 RID: 25904 RVA: 0x001E60D0 File Offset: 0x001E60D0
		public virtual void UnbookmarkLine(int iLine)
		{
			this.bookmarks.Remove(iLine);
		}

		// Token: 0x06006531 RID: 25905 RVA: 0x001E60E0 File Offset: 0x001E60E0
		public virtual void MoveSelectedLinesDown()
		{
			Range range = this.Selection.Clone();
			this.Selection.Expand();
			bool flag = !this.Selection.ReadOnly;
			if (flag)
			{
				int iLine = this.Selection.Start.iLine;
				bool flag2 = this.Selection.End.iLine >= this.LinesCount - 1;
				if (flag2)
				{
					this.Selection = range;
				}
				else
				{
					string selectedText = this.SelectedText;
					List<int> list = new List<int>();
					for (int i = this.Selection.Start.iLine; i <= this.Selection.End.iLine; i++)
					{
						list.Add(i);
					}
					this.RemoveLines(list);
					this.Selection.Start = new Place(this.GetLineLength(iLine), iLine);
					this.SelectedText = "\n" + selectedText;
					this.Selection.Start = new Place(range.Start.iChar, range.Start.iLine + 1);
					this.Selection.End = new Place(range.End.iChar, range.End.iLine + 1);
				}
			}
			else
			{
				this.Selection = range;
			}
		}

		// Token: 0x06006532 RID: 25906 RVA: 0x001E624C File Offset: 0x001E624C
		public virtual void MoveSelectedLinesUp()
		{
			Range range = this.Selection.Clone();
			this.Selection.Expand();
			bool flag = !this.Selection.ReadOnly;
			if (flag)
			{
				int iLine = this.Selection.Start.iLine;
				bool flag2 = iLine == 0;
				if (flag2)
				{
					this.Selection = range;
				}
				else
				{
					string selectedText = this.SelectedText;
					List<int> list = new List<int>();
					for (int i = this.Selection.Start.iLine; i <= this.Selection.End.iLine; i++)
					{
						list.Add(i);
					}
					this.RemoveLines(list);
					this.Selection.Start = new Place(0, iLine - 1);
					this.SelectedText = selectedText + "\n";
					this.Selection.Start = new Place(range.Start.iChar, range.Start.iLine - 1);
					this.Selection.End = new Place(range.End.iChar, range.End.iLine - 1);
				}
			}
			else
			{
				this.Selection = range;
			}
		}

		// Token: 0x06006533 RID: 25907 RVA: 0x001E639C File Offset: 0x001E639C
		private void GoHome(bool shift)
		{
			this.Selection.BeginUpdate();
			try
			{
				int iLine = this.Selection.Start.iLine;
				int startSpacesCount = this[iLine].StartSpacesCount;
				bool flag = this.Selection.Start.iChar <= startSpacesCount;
				if (flag)
				{
					this.Selection.GoHome(shift);
				}
				else
				{
					this.Selection.GoHome(shift);
					for (int i = 0; i < startSpacesCount; i++)
					{
						this.Selection.GoRight(shift);
					}
				}
			}
			finally
			{
				this.Selection.EndUpdate();
			}
		}

		// Token: 0x06006534 RID: 25908 RVA: 0x001E645C File Offset: 0x001E645C
		public virtual void UpperCase()
		{
			Range range = this.Selection.Clone();
			this.SelectedText = this.SelectedText.ToUpper();
			this.Selection.Start = range.Start;
			this.Selection.End = range.End;
		}

		// Token: 0x06006535 RID: 25909 RVA: 0x001E64B0 File Offset: 0x001E64B0
		public virtual void LowerCase()
		{
			Range range = this.Selection.Clone();
			this.SelectedText = this.SelectedText.ToLower();
			this.Selection.Start = range.Start;
			this.Selection.End = range.End;
		}

		// Token: 0x06006536 RID: 25910 RVA: 0x001E6504 File Offset: 0x001E6504
		public virtual void TitleCase()
		{
			Range range = this.Selection.Clone();
			this.SelectedText = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(this.SelectedText.ToLower());
			this.Selection.Start = range.Start;
			this.Selection.End = range.End;
		}

		// Token: 0x06006537 RID: 25911 RVA: 0x001E656C File Offset: 0x001E656C
		public virtual void SentenceCase()
		{
			Range range = this.Selection.Clone();
			string input = this.SelectedText.ToLower();
			Regex regex = new Regex("(^\\S)|[\\.\\?!:]\\s+(\\S)", RegexOptions.ExplicitCapture);
			this.SelectedText = regex.Replace(input, (Match s) => s.Value.ToUpper());
			this.Selection.Start = range.Start;
			this.Selection.End = range.End;
		}

		// Token: 0x06006538 RID: 25912 RVA: 0x001E65F8 File Offset: 0x001E65F8
		public void CommentSelected()
		{
			this.CommentSelected(this.CommentPrefix);
		}

		// Token: 0x06006539 RID: 25913 RVA: 0x001E6608 File Offset: 0x001E6608
		public virtual void CommentSelected(string commentPrefix)
		{
			bool flag = string.IsNullOrEmpty(commentPrefix);
			if (!flag)
			{
				this.Selection.Normalize();
				bool flag2 = this.lines[this.Selection.Start.iLine].Text.TrimStart(new char[0]).StartsWith(commentPrefix);
				bool flag3 = flag2;
				if (flag3)
				{
					this.RemoveLinePrefix(commentPrefix);
				}
				else
				{
					this.InsertLinePrefix(commentPrefix);
				}
			}
		}

		// Token: 0x0600653A RID: 25914 RVA: 0x001E6688 File Offset: 0x001E6688
		public void OnKeyPressing(KeyPressEventArgs args)
		{
			bool flag = this.KeyPressing != null;
			if (flag)
			{
				this.KeyPressing(this, args);
			}
		}

		// Token: 0x0600653B RID: 25915 RVA: 0x001E66B8 File Offset: 0x001E66B8
		private bool OnKeyPressing(char c)
		{
			bool flag = this.findCharMode;
			bool result;
			if (flag)
			{
				this.findCharMode = false;
				this.FindChar(c);
				result = true;
			}
			else
			{
				KeyPressEventArgs keyPressEventArgs = new KeyPressEventArgs(c);
				this.OnKeyPressing(keyPressEventArgs);
				result = keyPressEventArgs.Handled;
			}
			return result;
		}

		// Token: 0x0600653C RID: 25916 RVA: 0x001E670C File Offset: 0x001E670C
		public void OnKeyPressed(char c)
		{
			KeyPressEventArgs e = new KeyPressEventArgs(c);
			bool flag = this.KeyPressed != null;
			if (flag)
			{
				this.KeyPressed(this, e);
			}
		}

		// Token: 0x0600653D RID: 25917 RVA: 0x001E6744 File Offset: 0x001E6744
		protected override bool ProcessMnemonic(char charCode)
		{
			bool flag = this.middleClickScrollingActivated;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool focused = this.Focused;
				result = (focused && (this.ProcessKey(charCode, this.lastModifiers) || base.ProcessMnemonic(charCode)));
			}
			return result;
		}

		// Token: 0x0600653E RID: 25918 RVA: 0x001E67A4 File Offset: 0x001E67A4
		protected override bool ProcessKeyMessage(ref Message m)
		{
			bool flag = m.Msg == 258;
			if (flag)
			{
				this.ProcessMnemonic(Convert.ToChar(m.WParam.ToInt32()));
			}
			return base.ProcessKeyMessage(ref m);
		}

		// Token: 0x0600653F RID: 25919 RVA: 0x001E67F4 File Offset: 0x001E67F4
		public virtual bool ProcessKey(char c, Keys modifiers)
		{
			bool flag = this.handledChar;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = this.macrosManager != null;
				if (flag2)
				{
					this.macrosManager.ProcessKey(c, modifiers);
				}
				bool flag3 = c == '\b' && (modifiers == Keys.None || modifiers == Keys.Shift || (modifiers & Keys.Alt) > Keys.None);
				if (flag3)
				{
					bool flag4 = this.ReadOnly || !base.Enabled;
					if (flag4)
					{
						result = false;
					}
					else
					{
						bool flag5 = this.OnKeyPressing(c);
						if (flag5)
						{
							result = true;
						}
						else
						{
							bool readOnly = this.Selection.ReadOnly;
							if (readOnly)
							{
								result = false;
							}
							else
							{
								bool flag6 = !this.Selection.IsEmpty;
								if (flag6)
								{
									this.ClearSelected();
								}
								else
								{
									bool flag7 = !this.Selection.IsReadOnlyLeftChar();
									if (flag7)
									{
										this.InsertChar('\b');
									}
								}
								bool autoIndentChars = this.AutoIndentChars;
								if (autoIndentChars)
								{
									this.DoAutoIndentChars(this.Selection.Start.iLine);
								}
								this.OnKeyPressed('\b');
								result = true;
							}
						}
					}
				}
				else
				{
					bool flag8 = char.IsControl(c) && c != '\r' && c != '\t';
					if (flag8)
					{
						result = false;
					}
					else
					{
						bool flag9 = this.ReadOnly || !base.Enabled;
						if (flag9)
						{
							result = false;
						}
						else
						{
							bool flag10 = modifiers != Keys.None && modifiers != Keys.Shift && modifiers != (Keys.Control | Keys.Alt) && modifiers != (Keys.Shift | Keys.Control | Keys.Alt) && (modifiers != Keys.Alt || char.IsLetterOrDigit(c));
							if (flag10)
							{
								result = false;
							}
							else
							{
								char c2 = c;
								bool flag11 = this.OnKeyPressing(c2);
								if (flag11)
								{
									result = true;
								}
								else
								{
									bool readOnly2 = this.Selection.ReadOnly;
									if (readOnly2)
									{
										result = false;
									}
									else
									{
										bool flag12 = c == '\r' && !this.AcceptsReturn;
										if (flag12)
										{
											result = false;
										}
										else
										{
											bool flag13 = c == '\r';
											if (flag13)
											{
												c = '\n';
											}
											bool flag14 = this.IsReplaceMode;
											if (flag14)
											{
												this.Selection.GoRight(true);
												this.Selection.Inverse();
											}
											bool flag15 = !this.Selection.ReadOnly;
											if (flag15)
											{
												bool flag16 = !this.DoAutocompleteBrackets(c);
												if (flag16)
												{
													this.InsertChar(c);
												}
											}
											bool flag17 = c == '\n' || this.AutoIndentExistingLines;
											if (flag17)
											{
												this.DoAutoIndentIfNeed();
											}
											bool autoIndentChars2 = this.AutoIndentChars;
											if (autoIndentChars2)
											{
												this.DoAutoIndentChars(this.Selection.Start.iLine);
											}
											this.DoCaretVisible();
											this.Invalidate();
											this.OnKeyPressed(c2);
											result = true;
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x17001573 RID: 5491
		// (get) Token: 0x06006540 RID: 25920 RVA: 0x001E6B28 File Offset: 0x001E6B28
		// (set) Token: 0x06006541 RID: 25921 RVA: 0x001E6B30 File Offset: 0x001E6B30
		[Description("Enables AutoIndentChars mode")]
		[DefaultValue(true)]
		public bool AutoIndentChars { get; set; }

		// Token: 0x17001574 RID: 5492
		// (get) Token: 0x06006542 RID: 25922 RVA: 0x001E6B3C File Offset: 0x001E6B3C
		// (set) Token: 0x06006543 RID: 25923 RVA: 0x001E6B44 File Offset: 0x001E6B44
		[Description("Regex patterns for AutoIndentChars (one regex per line)")]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("^\\s*[\\w\\.]+\\s*(?<range>=)\\s*(?<range>[^;]+);")]
		public string AutoIndentCharsPatterns { get; set; }

		// Token: 0x06006544 RID: 25924 RVA: 0x001E6B50 File Offset: 0x001E6B50
		public void DoAutoIndentChars(int iLine)
		{
			string[] array = this.AutoIndentCharsPatterns.Split(new char[]
			{
				'\r',
				'\n'
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string pattern in array)
			{
				Match match = Regex.Match(this[iLine].Text, pattern);
				bool success = match.Success;
				if (success)
				{
					this.DoAutoIndentChars(iLine, new Regex(pattern));
					break;
				}
			}
		}

		// Token: 0x06006545 RID: 25925 RVA: 0x001E6BD8 File Offset: 0x001E6BD8
		protected void DoAutoIndentChars(int iLine, Regex regex)
		{
			Range range = this.Selection.Clone();
			SortedDictionary<int, CaptureCollection> sortedDictionary = new SortedDictionary<int, CaptureCollection>();
			SortedDictionary<int, string> sortedDictionary2 = new SortedDictionary<int, string>();
			int num = 0;
			int startSpacesCount = this[iLine].StartSpacesCount;
			for (int i = iLine; i >= 0; i--)
			{
				bool flag = startSpacesCount != this[i].StartSpacesCount;
				if (flag)
				{
					break;
				}
				string text = this[i].Text;
				Match match = regex.Match(text);
				bool success = match.Success;
				if (!success)
				{
					break;
				}
				sortedDictionary[i] = match.Groups["range"].Captures;
				sortedDictionary2[i] = text;
				bool flag2 = sortedDictionary[i].Count > num;
				if (flag2)
				{
					num = sortedDictionary[i].Count;
				}
			}
			for (int j = iLine + 1; j < this.LinesCount; j++)
			{
				bool flag3 = startSpacesCount != this[j].StartSpacesCount;
				if (flag3)
				{
					break;
				}
				string text2 = this[j].Text;
				Match match2 = regex.Match(text2);
				bool success2 = match2.Success;
				if (!success2)
				{
					break;
				}
				sortedDictionary[j] = match2.Groups["range"].Captures;
				sortedDictionary2[j] = text2;
				bool flag4 = sortedDictionary[j].Count > num;
				if (flag4)
				{
					num = sortedDictionary[j].Count;
				}
			}
			Dictionary<int, bool> dictionary = new Dictionary<int, bool>();
			bool flag5 = false;
			for (int k = num - 1; k >= 0; k--)
			{
				int num2 = 0;
				foreach (int key in sortedDictionary.Keys)
				{
					CaptureCollection captureCollection = sortedDictionary[key];
					bool flag6 = captureCollection.Count <= k;
					if (!flag6)
					{
						Capture capture = captureCollection[k];
						int num3 = capture.Index;
						string text3 = sortedDictionary2[key];
						while (num3 > 0 && text3[num3 - 1] == ' ')
						{
							num3--;
						}
						bool flag7 = k == 0;
						int num4;
						if (flag7)
						{
							num4 = num3;
						}
						else
						{
							num4 = num3 - captureCollection[k - 1].Index - 1;
						}
						bool flag8 = num4 > num2;
						if (flag8)
						{
							num2 = num4;
						}
					}
				}
				foreach (int num5 in new List<int>(sortedDictionary2.Keys))
				{
					bool flag9 = sortedDictionary[num5].Count <= k;
					if (!flag9)
					{
						Capture capture2 = sortedDictionary[num5][k];
						bool flag10 = k == 0;
						int num6;
						if (flag10)
						{
							num6 = capture2.Index;
						}
						else
						{
							num6 = capture2.Index - sortedDictionary[num5][k - 1].Index - 1;
						}
						int num7 = num2 - num6 + 1;
						bool flag11 = num7 == 0;
						if (!flag11)
						{
							bool flag12 = range.Start.iLine == num5 && range.Start.iChar > capture2.Index;
							if (flag12)
							{
								range.Start = new Place(range.Start.iChar + num7, num5);
							}
							bool flag13 = num7 > 0;
							if (flag13)
							{
								sortedDictionary2[num5] = sortedDictionary2[num5].Insert(capture2.Index, new string(' ', num7));
							}
							else
							{
								sortedDictionary2[num5] = sortedDictionary2[num5].Remove(capture2.Index + num7, -num7);
							}
							dictionary[num5] = true;
							flag5 = true;
						}
					}
				}
			}
			bool flag14 = flag5;
			if (flag14)
			{
				this.Selection.BeginUpdate();
				this.BeginAutoUndo();
				this.BeginUpdate();
				this.TextSource.Manager.ExecuteCommand(new SelectCommand(this.TextSource));
				foreach (int num8 in sortedDictionary2.Keys)
				{
					bool flag15 = dictionary.ContainsKey(num8);
					if (flag15)
					{
						this.Selection = new Range(this, 0, num8, this[num8].Count, num8);
						bool flag16 = !this.Selection.ReadOnly;
						if (flag16)
						{
							this.InsertText(sortedDictionary2[num8]);
						}
					}
				}
				this.Selection = range;
				this.EndUpdate();
				this.EndAutoUndo();
				this.Selection.EndUpdate();
			}
		}

		// Token: 0x06006546 RID: 25926 RVA: 0x001E719C File Offset: 0x001E719C
		private bool DoAutocompleteBrackets(char c)
		{
			bool autoCompleteBrackets = this.AutoCompleteBrackets;
			if (autoCompleteBrackets)
			{
				bool flag = !this.Selection.ColumnSelectionMode;
				if (flag)
				{
					for (int i = 1; i < this.autoCompleteBracketsList.Length; i += 2)
					{
						bool flag2 = c == this.autoCompleteBracketsList[i] && c == this.Selection.CharAfterStart;
						if (flag2)
						{
							this.Selection.GoRight();
							return true;
						}
					}
				}
				for (int j = 0; j < this.autoCompleteBracketsList.Length; j += 2)
				{
					bool flag3 = c == this.autoCompleteBracketsList[j];
					if (flag3)
					{
						this.InsertBrackets(this.autoCompleteBracketsList[j], this.autoCompleteBracketsList[j + 1]);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06006547 RID: 25927 RVA: 0x001E7290 File Offset: 0x001E7290
		private bool InsertBrackets(char left, char right)
		{
			bool columnSelectionMode = this.Selection.ColumnSelectionMode;
			if (columnSelectionMode)
			{
				Range range = this.Selection.Clone();
				range.Normalize();
				this.Selection.BeginUpdate();
				this.BeginAutoUndo();
				this.Selection = new Range(this, range.Start.iChar, range.Start.iLine, range.Start.iChar, range.End.iLine)
				{
					ColumnSelectionMode = true
				};
				this.InsertChar(left);
				this.Selection = new Range(this, range.End.iChar + 1, range.Start.iLine, range.End.iChar + 1, range.End.iLine)
				{
					ColumnSelectionMode = true
				};
				this.InsertChar(right);
				bool isEmpty = range.IsEmpty;
				if (isEmpty)
				{
					this.Selection = new Range(this, range.End.iChar + 1, range.Start.iLine, range.End.iChar + 1, range.End.iLine)
					{
						ColumnSelectionMode = true
					};
				}
				this.EndAutoUndo();
				this.Selection.EndUpdate();
			}
			else
			{
				bool isEmpty2 = this.Selection.IsEmpty;
				if (isEmpty2)
				{
					this.InsertText(left.ToString() + right.ToString());
					this.Selection.GoLeft();
				}
				else
				{
					this.InsertText(left.ToString() + this.SelectedText + right.ToString());
				}
			}
			return true;
		}

		// Token: 0x06006548 RID: 25928 RVA: 0x001E7448 File Offset: 0x001E7448
		protected virtual void FindChar(char c)
		{
			bool flag = c == '\r';
			if (flag)
			{
				c = '\n';
			}
			Range range = this.Selection.Clone();
			while (range.GoRight())
			{
				bool flag2 = range.CharBeforeStart == c;
				if (flag2)
				{
					this.Selection = range;
					this.DoCaretVisible();
					break;
				}
			}
		}

		// Token: 0x06006549 RID: 25929 RVA: 0x001E74AC File Offset: 0x001E74AC
		public virtual void DoAutoIndentIfNeed()
		{
			bool columnSelectionMode = this.Selection.ColumnSelectionMode;
			if (!columnSelectionMode)
			{
				bool autoIndent = this.AutoIndent;
				if (autoIndent)
				{
					this.DoCaretVisible();
					int num = this.CalcAutoIndent(this.Selection.Start.iLine);
					bool flag = this[this.Selection.Start.iLine].AutoIndentSpacesNeededCount != num;
					if (flag)
					{
						this.DoAutoIndent(this.Selection.Start.iLine);
						this[this.Selection.Start.iLine].AutoIndentSpacesNeededCount = num;
					}
				}
			}
		}

		// Token: 0x0600654A RID: 25930 RVA: 0x001E7560 File Offset: 0x001E7560
		private void RemoveSpacesAfterCaret()
		{
			bool flag = !this.Selection.IsEmpty;
			if (!flag)
			{
				Place start = this.Selection.Start;
				while (this.Selection.CharAfterStart == ' ')
				{
					this.Selection.GoRight(true);
				}
				this.ClearSelected();
			}
		}

		// Token: 0x0600654B RID: 25931 RVA: 0x001E75C4 File Offset: 0x001E75C4
		public virtual void DoAutoIndent(int iLine)
		{
			bool columnSelectionMode = this.Selection.ColumnSelectionMode;
			if (!columnSelectionMode)
			{
				Place start = this.Selection.Start;
				int num = this.CalcAutoIndent(iLine);
				int startSpacesCount = this.lines[iLine].StartSpacesCount;
				int num2 = num - startSpacesCount;
				bool flag = num2 < 0;
				if (flag)
				{
					num2 = -Math.Min(-num2, startSpacesCount);
				}
				bool flag2 = num2 == 0;
				if (!flag2)
				{
					this.Selection.Start = new Place(0, iLine);
					bool flag3 = num2 > 0;
					if (flag3)
					{
						this.InsertText(new string(' ', num2));
					}
					else
					{
						this.Selection.Start = new Place(0, iLine);
						this.Selection.End = new Place(-num2, iLine);
						this.ClearSelected();
					}
					this.Selection.Start = new Place(Math.Min(this.lines[iLine].Count, Math.Max(0, start.iChar + num2)), iLine);
				}
			}
		}

		// Token: 0x0600654C RID: 25932 RVA: 0x001E76E4 File Offset: 0x001E76E4
		public virtual int CalcAutoIndent(int iLine)
		{
			bool flag = iLine < 0 || iLine >= this.LinesCount;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				EventHandler<AutoIndentEventArgs> eventHandler = this.AutoIndentNeeded;
				bool flag2 = eventHandler == null;
				if (flag2)
				{
					bool flag3 = this.Language != Language.Custom && this.SyntaxHighlighter != null;
					if (flag3)
					{
						eventHandler = new EventHandler<AutoIndentEventArgs>(this.SyntaxHighlighter.AutoIndentNeeded);
					}
					else
					{
						eventHandler = new EventHandler<AutoIndentEventArgs>(this.CalcAutoIndentShiftByCodeFolding);
					}
				}
				Stack<AutoIndentEventArgs> stack = new Stack<AutoIndentEventArgs>();
				int i;
				for (i = iLine - 1; i >= 0; i--)
				{
					AutoIndentEventArgs autoIndentEventArgs = new AutoIndentEventArgs(i, this.lines[i].Text, (i > 0) ? this.lines[i - 1].Text : "", this.TabLength, 0);
					eventHandler(this, autoIndentEventArgs);
					stack.Push(autoIndentEventArgs);
					bool flag4 = autoIndentEventArgs.Shift == 0 && autoIndentEventArgs.AbsoluteIndentation == 0 && autoIndentEventArgs.LineText.Trim() != "";
					if (flag4)
					{
						break;
					}
				}
				int num = this.lines[(i >= 0) ? i : 0].StartSpacesCount;
				while (stack.Count != 0)
				{
					AutoIndentEventArgs autoIndentEventArgs2 = stack.Pop();
					bool flag5 = autoIndentEventArgs2.AbsoluteIndentation != 0;
					if (flag5)
					{
						num = autoIndentEventArgs2.AbsoluteIndentation + autoIndentEventArgs2.ShiftNextLines;
					}
					else
					{
						num += autoIndentEventArgs2.ShiftNextLines;
					}
				}
				AutoIndentEventArgs autoIndentEventArgs3 = new AutoIndentEventArgs(iLine, this.lines[iLine].Text, (iLine > 0) ? this.lines[iLine - 1].Text : "", this.TabLength, num);
				eventHandler(this, autoIndentEventArgs3);
				int num2 = autoIndentEventArgs3.AbsoluteIndentation + autoIndentEventArgs3.Shift;
				result = num2;
			}
			return result;
		}

		// Token: 0x0600654D RID: 25933 RVA: 0x001E7924 File Offset: 0x001E7924
		internal virtual void CalcAutoIndentShiftByCodeFolding(object sender, AutoIndentEventArgs args)
		{
			bool flag = string.IsNullOrEmpty(this.lines[args.iLine].FoldingEndMarker) && !string.IsNullOrEmpty(this.lines[args.iLine].FoldingStartMarker);
			if (flag)
			{
				args.ShiftNextLines = this.TabLength;
			}
			else
			{
				bool flag2 = !string.IsNullOrEmpty(this.lines[args.iLine].FoldingEndMarker) && string.IsNullOrEmpty(this.lines[args.iLine].FoldingStartMarker);
				if (flag2)
				{
					args.Shift = -this.TabLength;
					args.ShiftNextLines = -this.TabLength;
				}
			}
		}

		// Token: 0x0600654E RID: 25934 RVA: 0x001E7A00 File Offset: 0x001E7A00
		protected int GetMinStartSpacesCount(int fromLine, int toLine)
		{
			bool flag = fromLine > toLine;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				int num = int.MaxValue;
				for (int i = fromLine; i <= toLine; i++)
				{
					int startSpacesCount = this.lines[i].StartSpacesCount;
					bool flag2 = startSpacesCount < num;
					if (flag2)
					{
						num = startSpacesCount;
					}
				}
				result = num;
			}
			return result;
		}

		// Token: 0x0600654F RID: 25935 RVA: 0x001E7A70 File Offset: 0x001E7A70
		protected int GetMaxStartSpacesCount(int fromLine, int toLine)
		{
			bool flag = fromLine > toLine;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				int num = 0;
				for (int i = fromLine; i <= toLine; i++)
				{
					int startSpacesCount = this.lines[i].StartSpacesCount;
					bool flag2 = startSpacesCount > num;
					if (flag2)
					{
						num = startSpacesCount;
					}
				}
				result = num;
			}
			return result;
		}

		// Token: 0x06006550 RID: 25936 RVA: 0x001E7ADC File Offset: 0x001E7ADC
		public virtual void Undo()
		{
			this.lines.Manager.Undo();
			this.DoCaretVisible();
			this.Invalidate();
		}

		// Token: 0x06006551 RID: 25937 RVA: 0x001E7B00 File Offset: 0x001E7B00
		public virtual void Redo()
		{
			this.lines.Manager.Redo();
			this.DoCaretVisible();
			this.Invalidate();
		}

		// Token: 0x06006552 RID: 25938 RVA: 0x001E7B24 File Offset: 0x001E7B24
		protected override bool IsInputKey(Keys keyData)
		{
			bool flag = keyData == Keys.Tab && !this.AcceptsTab;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = keyData == Keys.Return && !this.AcceptsReturn;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = (keyData & Keys.Alt) == Keys.None;
					if (flag3)
					{
						Keys keys = keyData & Keys.KeyCode;
						bool flag4 = keys == Keys.Return;
						if (flag4)
						{
							return true;
						}
					}
					bool flag5 = (keyData & Keys.Alt) != Keys.Alt;
					if (flag5)
					{
						Keys keys2 = keyData & Keys.KeyCode;
						if (keys2 == Keys.Tab)
						{
							return (keyData & Keys.Control) == Keys.None;
						}
						switch (keys2)
						{
						case Keys.Escape:
							return false;
						case Keys.Prior:
						case Keys.Next:
						case Keys.End:
						case Keys.Home:
						case Keys.Left:
						case Keys.Up:
						case Keys.Right:
						case Keys.Down:
							return true;
						}
					}
					result = base.IsInputKey(keyData);
				}
			}
			return result;
		}

		// Token: 0x06006553 RID: 25939
		[DllImport("user32.dll")]
		private static extern bool CreateCaret(IntPtr hWnd, int hBitmap, int nWidth, int nHeight);

		// Token: 0x06006554 RID: 25940
		[DllImport("user32.dll")]
		private static extern bool SetCaretPos(int x, int y);

		// Token: 0x06006555 RID: 25941
		[DllImport("user32.dll")]
		private static extern bool DestroyCaret();

		// Token: 0x06006556 RID: 25942
		[DllImport("user32.dll")]
		private static extern bool ShowCaret(IntPtr hWnd);

		// Token: 0x06006557 RID: 25943
		[DllImport("user32.dll")]
		private static extern bool HideCaret(IntPtr hWnd);

		// Token: 0x06006558 RID: 25944 RVA: 0x001E7C58 File Offset: 0x001E7C58
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			bool flag = this.BackBrush == null;
			if (flag)
			{
				base.OnPaintBackground(e);
			}
			else
			{
				e.Graphics.FillRectangle(this.BackBrush, base.ClientRectangle);
			}
		}

		// Token: 0x06006559 RID: 25945 RVA: 0x001E7CA0 File Offset: 0x001E7CA0
		public void DrawText(Graphics gr, Place start, Size size)
		{
			bool flag = this.needRecalc;
			if (flag)
			{
				this.Recalc();
			}
			bool flag2 = this.needRecalcFoldingLines;
			if (flag2)
			{
				this.RecalcFoldingLines();
			}
			Point point = this.PlaceToPoint(start);
			int num = point.Y + base.VerticalScroll.Value;
			int num2 = point.X + base.HorizontalScroll.Value - this.LeftIndent - this.Paddings.Left;
			int iChar = start.iChar;
			int lastChar = (num2 + size.Width) / this.CharWidth;
			int iLine = start.iLine;
			for (int i = iLine; i < this.lines.Count; i++)
			{
				Line line = this.lines[i];
				LineInfo lineInfo = this.LineInfos[i];
				bool flag3 = lineInfo.startY > num + size.Height;
				if (flag3)
				{
					break;
				}
				bool flag4 = lineInfo.startY + lineInfo.WordWrapStringsCount * this.CharHeight < num;
				if (!flag4)
				{
					bool flag5 = lineInfo.VisibleState == VisibleState.Hidden;
					if (!flag5)
					{
						int y = lineInfo.startY - num;
						gr.SmoothingMode = SmoothingMode.None;
						bool flag6 = lineInfo.VisibleState == VisibleState.Visible;
						if (flag6)
						{
							bool flag7 = line.BackgroundBrush != null;
							if (flag7)
							{
								gr.FillRectangle(line.BackgroundBrush, new Rectangle(0, y, size.Width, this.CharHeight * lineInfo.WordWrapStringsCount));
							}
						}
						gr.SmoothingMode = SmoothingMode.AntiAlias;
						for (int j = 0; j < lineInfo.WordWrapStringsCount; j++)
						{
							y = lineInfo.startY + j * this.CharHeight - num;
							int num3 = (j == 0) ? 0 : (lineInfo.wordWrapIndent * this.CharWidth);
							this.DrawLineChars(gr, iChar, lastChar, i, j, -num2 + num3, y);
						}
					}
				}
			}
		}

		// Token: 0x0600655A RID: 25946 RVA: 0x001E7EC4 File Offset: 0x001E7EC4
		protected override void OnPaint(PaintEventArgs e)
		{
			bool flag = this.needRecalc;
			if (flag)
			{
				this.Recalc();
			}
			bool flag2 = this.needRecalcFoldingLines;
			if (flag2)
			{
				this.RecalcFoldingLines();
			}
			this.visibleMarkers.Clear();
			e.Graphics.SmoothingMode = SmoothingMode.None;
			Pen pen = new Pen(this.ServiceLinesColor);
			Brush brush = new SolidBrush(this.ChangedLineColor);
			Brush brush2 = new SolidBrush(this.IndentBackColor);
			Brush brush3 = new SolidBrush(this.PaddingBackColor);
			Brush brush4 = new SolidBrush(Color.FromArgb((int)((this.CurrentLineColor.A == byte.MaxValue) ? 50 : this.CurrentLineColor.A), this.CurrentLineColor));
			Rectangle textAreaRect = this.TextAreaRect;
			e.Graphics.FillRectangle(brush3, 0, -base.VerticalScroll.Value, base.ClientSize.Width, Math.Max(0, this.Paddings.Top - 1));
			e.Graphics.FillRectangle(brush3, 0, textAreaRect.Bottom, base.ClientSize.Width, base.ClientSize.Height);
			e.Graphics.FillRectangle(brush3, textAreaRect.Right, 0, base.ClientSize.Width, base.ClientSize.Height);
			e.Graphics.FillRectangle(brush3, this.LeftIndentLine, 0, this.LeftIndent - this.LeftIndentLine - 1, base.ClientSize.Height);
			bool flag3 = base.HorizontalScroll.Value <= this.Paddings.Left;
			if (flag3)
			{
				e.Graphics.FillRectangle(brush3, this.LeftIndent - base.HorizontalScroll.Value - 2, 0, Math.Max(0, this.Paddings.Left - 1), base.ClientSize.Height);
			}
			int num = Math.Max(this.LeftIndent, this.LeftIndent + this.Paddings.Left - base.HorizontalScroll.Value);
			int width = textAreaRect.Width;
			e.Graphics.FillRectangle(brush2, 0, 0, this.LeftIndentLine, base.ClientSize.Height);
			bool flag4 = this.LeftIndent > 8;
			if (flag4)
			{
				e.Graphics.DrawLine(pen, this.LeftIndentLine, 0, this.LeftIndentLine, base.ClientSize.Height);
			}
			bool flag5 = this.PreferredLineWidth > 0;
			if (flag5)
			{
				e.Graphics.DrawLine(pen, new Point(this.LeftIndent + this.Paddings.Left + this.PreferredLineWidth * this.CharWidth - base.HorizontalScroll.Value + 1, textAreaRect.Top + 1), new Point(this.LeftIndent + this.Paddings.Left + this.PreferredLineWidth * this.CharWidth - base.HorizontalScroll.Value + 1, textAreaRect.Bottom - 1));
			}
			this.DrawTextAreaBorder(e.Graphics);
			int num2 = Math.Max(0, base.HorizontalScroll.Value - this.Paddings.Left) / this.CharWidth;
			int lastChar = (base.HorizontalScroll.Value + base.ClientSize.Width) / this.CharWidth;
			int num3 = this.LeftIndent + this.Paddings.Left - base.HorizontalScroll.Value;
			bool flag6 = num3 < this.LeftIndent;
			if (flag6)
			{
				num2++;
			}
			Dictionary<int, Bookmark> dictionary = new Dictionary<int, Bookmark>();
			foreach (Bookmark bookmark in this.bookmarks)
			{
				dictionary[bookmark.LineIndex] = bookmark;
			}
			int num4 = this.YtoLineIndex(base.VerticalScroll.Value);
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			int i;
			for (i = num4; i < this.lines.Count; i++)
			{
				Line line = this.lines[i];
				LineInfo lineInfo = this.LineInfos[i];
				bool flag7 = lineInfo.startY > base.VerticalScroll.Value + base.ClientSize.Height;
				if (flag7)
				{
					break;
				}
				bool flag8 = lineInfo.startY + lineInfo.WordWrapStringsCount * this.CharHeight < base.VerticalScroll.Value;
				if (!flag8)
				{
					bool flag9 = lineInfo.VisibleState == VisibleState.Hidden;
					if (!flag9)
					{
						int num5 = lineInfo.startY - base.VerticalScroll.Value;
						e.Graphics.SmoothingMode = SmoothingMode.None;
						bool flag10 = lineInfo.VisibleState == VisibleState.Visible;
						if (flag10)
						{
							bool flag11 = line.BackgroundBrush != null;
							if (flag11)
							{
								e.Graphics.FillRectangle(line.BackgroundBrush, new Rectangle(textAreaRect.Left, num5, textAreaRect.Width, this.CharHeight * lineInfo.WordWrapStringsCount));
							}
						}
						bool flag12 = this.CurrentLineColor != Color.Transparent && i == this.Selection.Start.iLine;
						if (flag12)
						{
							bool isEmpty = this.Selection.IsEmpty;
							if (isEmpty)
							{
								e.Graphics.FillRectangle(brush4, new Rectangle(textAreaRect.Left, num5, textAreaRect.Width, this.CharHeight));
							}
						}
						bool flag13 = this.ChangedLineColor != Color.Transparent && line.IsChanged;
						if (flag13)
						{
							e.Graphics.FillRectangle(brush, new RectangleF(-10f, (float)num5, (float)(this.LeftIndent - 8 - 2 + 10), (float)(this.CharHeight + 1)));
						}
						e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
						bool flag14 = dictionary.ContainsKey(i);
						if (flag14)
						{
							dictionary[i].Paint(e.Graphics, new Rectangle(this.LeftIndent, num5, base.Width, this.CharHeight * lineInfo.WordWrapStringsCount));
						}
						bool flag15 = lineInfo.VisibleState == VisibleState.Visible;
						if (flag15)
						{
							this.OnPaintLine(new PaintLineEventArgs(i, new Rectangle(this.LeftIndent, num5, base.Width, this.CharHeight * lineInfo.WordWrapStringsCount), e.Graphics, e.ClipRectangle));
						}
						bool flag16 = this.ShowLineNumbers;
						if (flag16)
						{
							using (SolidBrush solidBrush = new SolidBrush(this.LineNumberColor))
							{
								e.Graphics.DrawString(((long)i + (long)((ulong)this.lineNumberStartValue)).ToString(), this.Font, solidBrush, new RectangleF(-10f, (float)num5, (float)(this.LeftIndent - 8 - 2 + 10), (float)(this.CharHeight + (int)((float)this.lineInterval * 0.5f))), new StringFormat(StringFormatFlags.DirectionRightToLeft)
								{
									LineAlignment = StringAlignment.Center
								});
							}
						}
						bool flag17 = lineInfo.VisibleState == VisibleState.StartOfHiddenBlock;
						if (flag17)
						{
							this.visibleMarkers.Add(new ExpandFoldingMarker(i, new Rectangle(this.LeftIndentLine - 4, num5 + this.CharHeight / 2 - 3, 8, 8)));
						}
						bool flag18 = !string.IsNullOrEmpty(line.FoldingStartMarker) && lineInfo.VisibleState == VisibleState.Visible && string.IsNullOrEmpty(line.FoldingEndMarker);
						if (flag18)
						{
							this.visibleMarkers.Add(new CollapseFoldingMarker(i, new Rectangle(this.LeftIndentLine - 4, num5 + this.CharHeight / 2 - 3, 8, 8)));
						}
						bool flag19 = lineInfo.VisibleState == VisibleState.Visible && !string.IsNullOrEmpty(line.FoldingEndMarker) && string.IsNullOrEmpty(line.FoldingStartMarker);
						if (flag19)
						{
							e.Graphics.DrawLine(pen, this.LeftIndentLine, num5 + this.CharHeight * lineInfo.WordWrapStringsCount - 1, this.LeftIndentLine + 4, num5 + this.CharHeight * lineInfo.WordWrapStringsCount - 1);
						}
						for (int j = 0; j < lineInfo.WordWrapStringsCount; j++)
						{
							num5 = lineInfo.startY + j * this.CharHeight - base.VerticalScroll.Value;
							bool flag20 = num5 > base.VerticalScroll.Value + base.ClientSize.Height;
							if (flag20)
							{
								break;
							}
							bool flag21 = lineInfo.startY + j * this.CharHeight < base.VerticalScroll.Value;
							if (!flag21)
							{
								int num6 = (j == 0) ? 0 : (lineInfo.wordWrapIndent * this.CharWidth);
								this.DrawLineChars(e.Graphics, num2, lastChar, i, j, num3 + num6, num5);
							}
						}
					}
				}
			}
			int endLine = i - 1;
			bool flag22 = this.ShowFoldingLines;
			if (flag22)
			{
				this.DrawFoldingLines(e, num4, endLine);
			}
			bool columnSelectionMode = this.Selection.ColumnSelectionMode;
			if (columnSelectionMode)
			{
				bool flag23 = this.SelectionStyle.BackgroundBrush is SolidBrush;
				if (flag23)
				{
					Color color = ((SolidBrush)this.SelectionStyle.BackgroundBrush).Color;
					Point point = this.PlaceToPoint(this.Selection.Start);
					Point point2 = this.PlaceToPoint(this.Selection.End);
					using (Pen pen2 = new Pen(color))
					{
						e.Graphics.DrawRectangle(pen2, Rectangle.FromLTRB(Math.Min(point.X, point2.X) - 1, Math.Min(point.Y, point2.Y), Math.Max(point.X, point2.X), Math.Max(point.Y, point2.Y) + this.CharHeight));
					}
				}
			}
			bool flag24 = this.BracketsStyle != null && this.leftBracketPosition != null && this.rightBracketPosition != null;
			if (flag24)
			{
				this.BracketsStyle.Draw(e.Graphics, this.PlaceToPoint(this.leftBracketPosition.Start), this.leftBracketPosition);
				this.BracketsStyle.Draw(e.Graphics, this.PlaceToPoint(this.rightBracketPosition.Start), this.rightBracketPosition);
			}
			bool flag25 = this.BracketsStyle2 != null && this.leftBracketPosition2 != null && this.rightBracketPosition2 != null;
			if (flag25)
			{
				this.BracketsStyle2.Draw(e.Graphics, this.PlaceToPoint(this.leftBracketPosition2.Start), this.leftBracketPosition2);
				this.BracketsStyle2.Draw(e.Graphics, this.PlaceToPoint(this.rightBracketPosition2.Start), this.rightBracketPosition2);
			}
			e.Graphics.SmoothingMode = SmoothingMode.None;
			bool flag26 = (this.startFoldingLine >= 0 || this.endFoldingLine >= 0) && this.Selection.Start == this.Selection.End;
			if (flag26)
			{
				bool flag27 = this.endFoldingLine < this.LineInfos.Count;
				if (flag27)
				{
					int y = ((this.startFoldingLine >= 0) ? this.LineInfos[this.startFoldingLine].startY : 0) - base.VerticalScroll.Value + this.CharHeight / 2;
					int y2 = ((this.endFoldingLine >= 0) ? (this.LineInfos[this.endFoldingLine].startY + (this.LineInfos[this.endFoldingLine].WordWrapStringsCount - 1) * this.CharHeight) : (this.TextHeight + this.CharHeight)) - base.VerticalScroll.Value + this.CharHeight;
					using (Pen pen3 = new Pen(Color.FromArgb(100, this.FoldingIndicatorColor), 4f))
					{
						e.Graphics.DrawLine(pen3, this.LeftIndent - 5, y, this.LeftIndent - 5, y2);
					}
				}
			}
			this.PaintHintBrackets(e.Graphics);
			this.DrawMarkers(e, pen);
			Point point3 = this.PlaceToPoint(this.Selection.Start);
			int num7 = this.CharHeight - this.lineInterval;
			point3.Offset(0, this.lineInterval / 2);
			bool flag28 = (this.Focused || this.IsDragDrop || this.ShowCaretWhenInactive) && point3.X >= this.LeftIndent && this.CaretVisible;
			if (flag28)
			{
				int num8 = (this.IsReplaceMode || this.WideCaret) ? this.CharWidth : 1;
				bool wideCaret = this.WideCaret;
				if (wideCaret)
				{
					using (SolidBrush solidBrush2 = new SolidBrush(this.CaretColor))
					{
						e.Graphics.FillRectangle(solidBrush2, point3.X, point3.Y, num8, num7 + 1);
					}
				}
				else
				{
					using (Pen pen4 = new Pen(this.CaretColor))
					{
						e.Graphics.DrawLine(pen4, point3.X, point3.Y, point3.X, point3.Y + num7);
					}
				}
				Rectangle right = new Rectangle(base.HorizontalScroll.Value + point3.X, base.VerticalScroll.Value + point3.Y, num8, num7 + 1);
				bool caretBlinking = this.CaretBlinking;
				if (caretBlinking)
				{
					bool flag29 = this.prevCaretRect != right || !this.ShowScrollBars;
					if (flag29)
					{
						FastColoredTextBox.CreateCaret(base.Handle, 0, num8, num7 + 1);
						FastColoredTextBox.SetCaretPos(point3.X, point3.Y);
						FastColoredTextBox.ShowCaret(base.Handle);
					}
				}
				this.prevCaretRect = right;
			}
			else
			{
				FastColoredTextBox.HideCaret(base.Handle);
				this.prevCaretRect = Rectangle.Empty;
			}
			bool flag30 = !base.Enabled;
			if (flag30)
			{
				using (SolidBrush solidBrush3 = new SolidBrush(this.DisabledColor))
				{
					e.Graphics.FillRectangle(solidBrush3, base.ClientRectangle);
				}
			}
			bool isRecording = this.MacrosManager.IsRecording;
			if (isRecording)
			{
				this.DrawRecordingHint(e.Graphics);
			}
			bool flag31 = this.middleClickScrollingActivated;
			if (flag31)
			{
				this.DrawMiddleClickScrolling(e.Graphics);
			}
			pen.Dispose();
			brush.Dispose();
			brush2.Dispose();
			brush4.Dispose();
			brush3.Dispose();
			base.OnPaint(e);
		}

		// Token: 0x0600655B RID: 25947 RVA: 0x001E8F14 File Offset: 0x001E8F14
		private void DrawMarkers(PaintEventArgs e, Pen servicePen)
		{
			foreach (VisualMarker visualMarker in this.visibleMarkers)
			{
				bool flag = visualMarker is CollapseFoldingMarker;
				if (flag)
				{
					using (SolidBrush solidBrush = new SolidBrush(this.ServiceColors.CollapseMarkerBackColor))
					{
						using (Pen pen = new Pen(this.ServiceColors.CollapseMarkerForeColor))
						{
							using (Pen pen2 = new Pen(this.ServiceColors.CollapseMarkerBorderColor))
							{
								(visualMarker as CollapseFoldingMarker).Draw(e.Graphics, pen2, solidBrush, pen);
							}
						}
					}
				}
				else
				{
					bool flag2 = visualMarker is ExpandFoldingMarker;
					if (flag2)
					{
						using (SolidBrush solidBrush2 = new SolidBrush(this.ServiceColors.ExpandMarkerBackColor))
						{
							using (Pen pen3 = new Pen(this.ServiceColors.ExpandMarkerForeColor))
							{
								using (Pen pen4 = new Pen(this.ServiceColors.ExpandMarkerBorderColor))
								{
									(visualMarker as ExpandFoldingMarker).Draw(e.Graphics, pen4, solidBrush2, pen3);
								}
							}
						}
					}
					else
					{
						visualMarker.Draw(e.Graphics, servicePen);
					}
				}
			}
		}

		// Token: 0x0600655C RID: 25948 RVA: 0x001E915C File Offset: 0x001E915C
		private void DrawRecordingHint(Graphics graphics)
		{
			Rectangle rect = new Rectangle(base.ClientRectangle.Right - 75, base.ClientRectangle.Bottom - 13, 75, 13);
			Rectangle rect2 = new Rectangle(-3, -3, 6, 6);
			GraphicsState gstate = graphics.Save();
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.TranslateTransform((float)(rect.Left + 6), (float)(rect.Top + 6));
			TimeSpan timeSpan = new TimeSpan(DateTime.Now.Ticks);
			graphics.RotateTransform(180f * ((float)DateTime.Now.Millisecond / 1000f));
			using (Pen pen = new Pen(Color.Red, 2f))
			{
				graphics.DrawArc(pen, rect2, 0f, 90f);
				graphics.DrawArc(pen, rect2, 180f, 90f);
			}
			graphics.DrawEllipse(Pens.Red, rect2);
			graphics.Restore(gstate);
			using (Font font = new Font(FontFamily.GenericSansSerif, 8f))
			{
				graphics.DrawString("Recording...", font, Brushes.Red, new PointF((float)(rect.Left + 13), (float)rect.Top));
			}
			System.Threading.Timer tm = null;
			tm = new System.Threading.Timer(delegate(object o)
			{
				this.Invalidate(rect);
				tm.Dispose();
			}, null, 200, -1);
		}

		// Token: 0x0600655D RID: 25949 RVA: 0x001E9320 File Offset: 0x001E9320
		private void DrawTextAreaBorder(Graphics graphics)
		{
			bool flag = this.TextAreaBorder == TextAreaBorderType.None;
			if (!flag)
			{
				Rectangle textAreaRect = this.TextAreaRect;
				bool flag2 = this.TextAreaBorder == TextAreaBorderType.Shadow;
				if (flag2)
				{
					Rectangle rect = new Rectangle(textAreaRect.Left + 4, textAreaRect.Bottom, textAreaRect.Width - 4, 4);
					Rectangle rect2 = new Rectangle(textAreaRect.Right, textAreaRect.Bottom, 4, 4);
					Rectangle rect3 = new Rectangle(textAreaRect.Right, textAreaRect.Top + 4, 4, textAreaRect.Height - 4);
					using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(80, this.TextAreaBorderColor)))
					{
						graphics.FillRectangle(solidBrush, rect);
						graphics.FillRectangle(solidBrush, rect3);
						graphics.FillRectangle(solidBrush, rect2);
					}
				}
				using (Pen pen = new Pen(this.TextAreaBorderColor))
				{
					graphics.DrawRectangle(pen, textAreaRect);
				}
			}
		}

		// Token: 0x0600655E RID: 25950 RVA: 0x001E9448 File Offset: 0x001E9448
		private void PaintHintBrackets(Graphics gr)
		{
			foreach (Hint hint in this.hints)
			{
				Range range = hint.Range.Clone();
				range.Normalize();
				Point point = this.PlaceToPoint(range.Start);
				Point point2 = this.PlaceToPoint(range.End);
				bool flag = this.GetVisibleState(range.Start.iLine) != VisibleState.Visible || this.GetVisibleState(range.End.iLine) > VisibleState.Visible;
				if (!flag)
				{
					using (Pen pen = new Pen(hint.BorderColor))
					{
						pen.DashStyle = DashStyle.Dash;
						bool isEmpty = range.IsEmpty;
						if (isEmpty)
						{
							point.Offset(1, -1);
							gr.DrawLines(pen, new Point[]
							{
								point,
								new Point(point.X, point.Y + this.charHeight + 2)
							});
						}
						else
						{
							point.Offset(-1, -1);
							point2.Offset(1, -1);
							gr.DrawLines(pen, new Point[]
							{
								new Point(point.X + this.CharWidth / 2, point.Y),
								point,
								new Point(point.X, point.Y + this.charHeight + 2),
								new Point(point.X + this.CharWidth / 2, point.Y + this.charHeight + 2)
							});
							gr.DrawLines(pen, new Point[]
							{
								new Point(point2.X - this.CharWidth / 2, point2.Y),
								point2,
								new Point(point2.X, point2.Y + this.charHeight + 2),
								new Point(point2.X - this.CharWidth / 2, point2.Y + this.charHeight + 2)
							});
						}
					}
				}
			}
		}

		// Token: 0x0600655F RID: 25951 RVA: 0x001E96E0 File Offset: 0x001E96E0
		protected virtual void DrawFoldingLines(PaintEventArgs e, int startLine, int endLine)
		{
			e.Graphics.SmoothingMode = SmoothingMode.None;
			using (Pen pen = new Pen(Color.FromArgb(200, this.ServiceLinesColor))
			{
				DashStyle = DashStyle.Dot
			})
			{
				foreach (KeyValuePair<int, int> keyValuePair in this.foldingPairs)
				{
					bool flag = keyValuePair.Key < endLine && keyValuePair.Value > startLine;
					if (flag)
					{
						Line line = this.lines[keyValuePair.Key];
						int num = this.LineInfos[keyValuePair.Key].startY - base.VerticalScroll.Value + this.CharHeight;
						num += num % 2;
						bool flag2 = keyValuePair.Value >= this.LinesCount;
						int num2;
						if (flag2)
						{
							num2 = this.LineInfos[this.LinesCount - 1].startY + this.CharHeight - base.VerticalScroll.Value;
						}
						else
						{
							bool flag3 = this.LineInfos[keyValuePair.Value].VisibleState == VisibleState.Visible;
							if (!flag3)
							{
								continue;
							}
							int num3 = 0;
							int startSpacesCount = line.StartSpacesCount;
							bool flag4 = this.lines[keyValuePair.Value].Count <= startSpacesCount || this.lines[keyValuePair.Value][startSpacesCount].c == ' ';
							if (flag4)
							{
								num3 = this.CharHeight;
							}
							num2 = this.LineInfos[keyValuePair.Value].startY - base.VerticalScroll.Value + num3;
						}
						int num4 = this.LeftIndent + this.Paddings.Left + line.StartSpacesCount * this.CharWidth - base.HorizontalScroll.Value;
						bool flag5 = num4 >= this.LeftIndent + this.Paddings.Left;
						if (flag5)
						{
							e.Graphics.DrawLine(pen, num4, (num >= 0) ? num : 0, num4, (num2 < base.ClientSize.Height) ? num2 : base.ClientSize.Height);
						}
					}
				}
			}
		}

		// Token: 0x06006560 RID: 25952 RVA: 0x001E99B8 File Offset: 0x001E99B8
		private void DrawLineChars(Graphics gr, int firstChar, int lastChar, int iLine, int iWordWrapLine, int startX, int y)
		{
			Line line = this.lines[iLine];
			LineInfo lineInfo = this.LineInfos[iLine];
			int wordWrapStringStartPosition = lineInfo.GetWordWrapStringStartPosition(iWordWrapLine);
			int wordWrapStringFinishPosition = lineInfo.GetWordWrapStringFinishPosition(iWordWrapLine, line);
			lastChar = Math.Min(wordWrapStringFinishPosition - wordWrapStringStartPosition, lastChar);
			gr.SmoothingMode = SmoothingMode.AntiAlias;
			bool flag = lineInfo.VisibleState == VisibleState.StartOfHiddenBlock;
			if (flag)
			{
				this.FoldedBlockStyle.Draw(gr, new Point(startX + firstChar * this.CharWidth, y), new Range(this, wordWrapStringStartPosition + firstChar, iLine, wordWrapStringStartPosition + lastChar + 1, iLine));
			}
			else
			{
				StyleIndex styleIndex = StyleIndex.None;
				int num = firstChar - 1;
				for (int i = firstChar; i <= lastChar; i++)
				{
					StyleIndex style = line[wordWrapStringStartPosition + i].style;
					bool flag2 = styleIndex != style;
					if (flag2)
					{
						this.FlushRendering(gr, styleIndex, new Point(startX + (num + 1) * this.CharWidth, y), new Range(this, wordWrapStringStartPosition + num + 1, iLine, wordWrapStringStartPosition + i, iLine));
						num = i - 1;
						styleIndex = style;
					}
				}
				this.FlushRendering(gr, styleIndex, new Point(startX + (num + 1) * this.CharWidth, y), new Range(this, wordWrapStringStartPosition + num + 1, iLine, wordWrapStringStartPosition + lastChar + 1, iLine));
			}
			bool flag3 = this.SelectionHighlightingForLineBreaksEnabled && iWordWrapLine == lineInfo.WordWrapStringsCount - 1;
			if (flag3)
			{
				lastChar++;
			}
			bool flag4 = !this.Selection.IsEmpty && lastChar >= firstChar;
			if (flag4)
			{
				gr.SmoothingMode = SmoothingMode.None;
				Range range = new Range(this, wordWrapStringStartPosition + firstChar, iLine, wordWrapStringStartPosition + lastChar + 1, iLine);
				range = this.Selection.GetIntersectionWith(range);
				bool flag5 = range != null && this.SelectionStyle != null;
				if (flag5)
				{
					this.SelectionStyle.Draw(gr, new Point(startX + (range.Start.iChar - wordWrapStringStartPosition) * this.CharWidth, 1 + y), range);
				}
			}
		}

		// Token: 0x06006561 RID: 25953 RVA: 0x001E9BE8 File Offset: 0x001E9BE8
		private void FlushRendering(Graphics gr, StyleIndex styleIndex, Point pos, Range range)
		{
			bool flag = range.End > range.Start;
			if (flag)
			{
				int num = 1;
				bool flag2 = false;
				for (int i = 0; i < this.Styles.Length; i++)
				{
					bool flag3 = this.Styles[i] != null && ((int)styleIndex & num) != 0;
					if (flag3)
					{
						Style style = this.Styles[i];
						bool flag4 = style is TextStyle;
						bool flag5 = !flag2 || !flag4 || this.AllowSeveralTextStyleDrawing;
						if (flag5)
						{
							style.Draw(gr, pos, range);
						}
						flag2 = (flag2 || flag4);
					}
					num <<= 1;
				}
				bool flag6 = !flag2;
				if (flag6)
				{
					this.DefaultStyle.Draw(gr, pos, range);
				}
			}
		}

		// Token: 0x06006562 RID: 25954 RVA: 0x001E9CD4 File Offset: 0x001E9CD4
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			this.mouseIsDrag = false;
			this.mouseIsDragDrop = false;
			this.draggedRange = null;
		}

		// Token: 0x06006563 RID: 25955 RVA: 0x001E9CF4 File Offset: 0x001E9CF4
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			this.isLineSelect = false;
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				bool flag2 = this.mouseIsDragDrop;
				if (flag2)
				{
					this.OnMouseClickText(e);
				}
			}
		}

		// Token: 0x06006564 RID: 25956 RVA: 0x001E9D40 File Offset: 0x001E9D40
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			bool flag = this.middleClickScrollingActivated;
			if (flag)
			{
				this.DeactivateMiddleClickScrollingMode();
				this.mouseIsDrag = false;
				bool flag2 = e.Button == MouseButtons.Middle;
				if (flag2)
				{
					this.RestoreScrollsAfterMiddleClickScrollingMode();
				}
			}
			else
			{
				this.MacrosManager.IsRecording = false;
				base.Select();
				base.ActiveControl = null;
				bool flag3 = e.Button == MouseButtons.Left;
				if (flag3)
				{
					VisualMarker visualMarker = this.FindVisualMarkerForPoint(e.Location);
					bool flag4 = visualMarker != null;
					if (flag4)
					{
						this.mouseIsDrag = false;
						this.mouseIsDragDrop = false;
						this.draggedRange = null;
						this.OnMarkerClick(e, visualMarker);
					}
					else
					{
						this.mouseIsDrag = true;
						this.mouseIsDragDrop = false;
						this.draggedRange = null;
						this.isLineSelect = (e.Location.X < this.LeftIndentLine);
						bool flag5 = !this.isLineSelect;
						if (flag5)
						{
							Place place = this.PointToPlace(e.Location);
							bool flag6 = e.Clicks == 2;
							if (flag6)
							{
								this.mouseIsDrag = false;
								this.mouseIsDragDrop = false;
								this.draggedRange = null;
								this.SelectWord(place);
							}
							else
							{
								bool flag7 = this.Selection.IsEmpty || !this.Selection.Contains(place) || this[place.iLine].Count <= place.iChar || this.ReadOnly;
								if (flag7)
								{
									this.OnMouseClickText(e);
								}
								else
								{
									this.mouseIsDragDrop = true;
									this.mouseIsDrag = false;
								}
							}
						}
						else
						{
							this.CheckAndChangeSelectionType();
							this.Selection.BeginUpdate();
							int iLine = this.PointToPlaceSimple(e.Location).iLine;
							this.lineSelectFrom = iLine;
							this.Selection.Start = new Place(0, iLine);
							this.Selection.End = new Place(this.GetLineLength(iLine), iLine);
							this.Selection.EndUpdate();
							this.Invalidate();
						}
					}
				}
				else
				{
					bool flag8 = e.Button == MouseButtons.Middle;
					if (flag8)
					{
						this.ActivateMiddleClickScrollingMode(e);
					}
				}
			}
		}

		// Token: 0x06006565 RID: 25957 RVA: 0x001E9FA0 File Offset: 0x001E9FA0
		private void OnMouseClickText(MouseEventArgs e)
		{
			Place end = this.Selection.End;
			this.Selection.BeginUpdate();
			bool columnSelectionMode = this.Selection.ColumnSelectionMode;
			if (columnSelectionMode)
			{
				this.Selection.Start = this.PointToPlaceSimple(e.Location);
				this.Selection.ColumnSelectionMode = true;
			}
			else
			{
				bool virtualSpace = this.VirtualSpace;
				if (virtualSpace)
				{
					this.Selection.Start = this.PointToPlaceSimple(e.Location);
				}
				else
				{
					this.Selection.Start = this.PointToPlace(e.Location);
				}
			}
			bool flag = (this.lastModifiers & Keys.Shift) > Keys.None;
			if (flag)
			{
				this.Selection.End = end;
			}
			this.CheckAndChangeSelectionType();
			this.Selection.EndUpdate();
			this.Invalidate();
		}

		// Token: 0x06006566 RID: 25958 RVA: 0x001EA090 File Offset: 0x001EA090
		protected virtual void CheckAndChangeSelectionType()
		{
			bool flag = (Control.ModifierKeys & Keys.Alt) != Keys.None && !this.WordWrap;
			if (flag)
			{
				this.Selection.ColumnSelectionMode = true;
			}
			else
			{
				this.Selection.ColumnSelectionMode = false;
			}
		}

		// Token: 0x06006567 RID: 25959 RVA: 0x001EA0EC File Offset: 0x001EA0EC
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			this.Invalidate();
			bool flag = this.lastModifiers == Keys.Control;
			if (flag)
			{
				this.ChangeFontSize(2 * Math.Sign(e.Delta));
				((HandledMouseEventArgs)e).Handled = true;
			}
			else
			{
				bool flag2 = base.VerticalScroll.Visible || !this.ShowScrollBars;
				if (flag2)
				{
					int controlPanelWheelScrollLinesValue = FastColoredTextBox.GetControlPanelWheelScrollLinesValue();
					this.DoScrollVertical(controlPanelWheelScrollLinesValue, e.Delta);
					((HandledMouseEventArgs)e).Handled = true;
				}
			}
			this.DeactivateMiddleClickScrollingMode();
		}

		// Token: 0x06006568 RID: 25960 RVA: 0x001EA194 File Offset: 0x001EA194
		private void DoScrollVertical(int countLines, int direction)
		{
			bool flag = base.VerticalScroll.Visible || !this.ShowScrollBars;
			if (flag)
			{
				int num = base.ClientSize.Height / this.CharHeight;
				bool flag2 = countLines == -1 || countLines > num;
				int num2;
				if (flag2)
				{
					num2 = this.CharHeight * num;
				}
				else
				{
					num2 = this.CharHeight * countLines;
				}
				int newValue = base.VerticalScroll.Value - Math.Sign(direction) * num2;
				ScrollEventArgs se = new ScrollEventArgs((direction > 0) ? ScrollEventType.SmallDecrement : ScrollEventType.SmallIncrement, base.VerticalScroll.Value, newValue, ScrollOrientation.VerticalScroll);
				this.OnScroll(se);
			}
		}

		// Token: 0x06006569 RID: 25961 RVA: 0x001EA258 File Offset: 0x001EA258
		private static int GetControlPanelWheelScrollLinesValue()
		{
			int result;
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", false))
				{
					result = Convert.ToInt32(registryKey.GetValue("WheelScrollLines"));
				}
			}
			catch
			{
				result = 1;
			}
			return result;
		}

		// Token: 0x0600656A RID: 25962 RVA: 0x001EA2C4 File Offset: 0x001EA2C4
		public void ChangeFontSize(int step)
		{
			float sizeInPoints = this.Font.SizeInPoints;
			using (Graphics graphics = Graphics.FromHwnd(base.Handle))
			{
				float dpiY = graphics.DpiY;
				float num = sizeInPoints + (float)step * 72f / dpiY;
				bool flag = num < 1f;
				if (!flag)
				{
					float num2 = num / this.originalFont.SizeInPoints;
					this.Zoom = (int)(100f * num2);
				}
			}
		}

		// Token: 0x17001575 RID: 5493
		// (get) Token: 0x0600656B RID: 25963 RVA: 0x001EA358 File Offset: 0x001EA358
		// (set) Token: 0x0600656C RID: 25964 RVA: 0x001EA378 File Offset: 0x001EA378
		[Browsable(false)]
		public int Zoom
		{
			get
			{
				return this.zoom;
			}
			set
			{
				this.zoom = value;
				this.DoZoom((float)this.zoom / 100f);
				this.OnZoomChanged();
			}
		}

		// Token: 0x0600656D RID: 25965 RVA: 0x001EA3A0 File Offset: 0x001EA3A0
		protected virtual void OnZoomChanged()
		{
			bool flag = this.ZoomChanged != null;
			if (flag)
			{
				this.ZoomChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600656E RID: 25966 RVA: 0x001EA3D4 File Offset: 0x001EA3D4
		private void DoZoom(float koeff)
		{
			int num = this.YtoLineIndex(base.VerticalScroll.Value);
			float num2 = this.originalFont.SizeInPoints;
			num2 *= koeff;
			bool flag = num2 < 1f || num2 > 300f;
			if (!flag)
			{
				Font font = this.Font;
				this.SetFont(new Font(this.Font.FontFamily, num2, this.Font.Style, GraphicsUnit.Point));
				font.Dispose();
				this.NeedRecalc(true);
				bool flag2 = num < this.LinesCount;
				if (flag2)
				{
					base.VerticalScroll.Value = Math.Min(base.VerticalScroll.Maximum, this.LineInfos[num].startY - this.Paddings.Top);
				}
				this.UpdateScrollbars();
				this.Invalidate();
				this.OnVisibleRangeChanged();
			}
		}

		// Token: 0x0600656F RID: 25967 RVA: 0x001EA4CC File Offset: 0x001EA4CC
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.CancelToolTip();
		}

		// Token: 0x06006570 RID: 25968 RVA: 0x001EA4E0 File Offset: 0x001EA4E0
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			bool flag = this.middleClickScrollingActivated;
			if (!flag)
			{
				bool flag2 = this.lastMouseCoord != e.Location;
				if (flag2)
				{
					this.CancelToolTip();
					this.timer3.Start();
				}
				this.lastMouseCoord = e.Location;
				bool flag3 = e.Button == MouseButtons.Left && this.mouseIsDragDrop;
				if (flag3)
				{
					this.draggedRange = this.Selection.Clone();
					base.DoDragDrop(this.SelectedText, DragDropEffects.Copy);
					this.draggedRange = null;
				}
				else
				{
					bool flag4 = e.Button == MouseButtons.Left && this.mouseIsDrag;
					if (flag4)
					{
						bool flag5 = this.Selection.ColumnSelectionMode || this.VirtualSpace;
						Place place;
						if (flag5)
						{
							place = this.PointToPlaceSimple(e.Location);
						}
						else
						{
							place = this.PointToPlace(e.Location);
						}
						bool flag6 = this.isLineSelect;
						if (flag6)
						{
							this.Selection.BeginUpdate();
							int iLine = place.iLine;
							bool flag7 = iLine < this.lineSelectFrom;
							if (flag7)
							{
								this.Selection.Start = new Place(0, iLine);
								this.Selection.End = new Place(this.GetLineLength(this.lineSelectFrom), this.lineSelectFrom);
							}
							else
							{
								this.Selection.Start = new Place(this.GetLineLength(iLine), iLine);
								this.Selection.End = new Place(0, this.lineSelectFrom);
							}
							this.Selection.EndUpdate();
							this.DoCaretVisible();
							base.HorizontalScroll.Value = 0;
							this.UpdateScrollbars();
							this.Invalidate();
						}
						else
						{
							bool flag8 = place != this.Selection.Start;
							if (flag8)
							{
								Place end = this.Selection.End;
								this.Selection.BeginUpdate();
								bool columnSelectionMode = this.Selection.ColumnSelectionMode;
								if (columnSelectionMode)
								{
									this.Selection.Start = place;
									this.Selection.ColumnSelectionMode = true;
								}
								else
								{
									this.Selection.Start = place;
								}
								this.Selection.End = end;
								this.Selection.EndUpdate();
								this.DoCaretVisible();
								this.Invalidate();
								return;
							}
						}
					}
					VisualMarker visualMarker = this.FindVisualMarkerForPoint(e.Location);
					bool flag9 = visualMarker != null;
					if (flag9)
					{
						base.Cursor = visualMarker.Cursor;
					}
					else
					{
						bool flag10 = e.Location.X < this.LeftIndentLine || this.isLineSelect;
						if (flag10)
						{
							base.Cursor = Cursors.Arrow;
						}
						else
						{
							base.Cursor = this.defaultCursor;
						}
					}
				}
			}
		}

		// Token: 0x06006571 RID: 25969 RVA: 0x001EA804 File Offset: 0x001EA804
		private void CancelToolTip()
		{
			this.timer3.Stop();
			bool flag = this.ToolTip != null && !string.IsNullOrEmpty(this.ToolTip.GetToolTip(this));
			if (flag)
			{
				this.ToolTip.Hide(this);
				this.ToolTip.SetToolTip(this, null);
			}
		}

		// Token: 0x06006572 RID: 25970 RVA: 0x001EA86C File Offset: 0x001EA86C
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			VisualMarker visualMarker = this.FindVisualMarkerForPoint(e.Location);
			bool flag = visualMarker != null;
			if (flag)
			{
				this.OnMarkerDoubleClick(visualMarker);
			}
		}

		// Token: 0x06006573 RID: 25971 RVA: 0x001EA8A8 File Offset: 0x001EA8A8
		private void SelectWord(Place p)
		{
			int iEndChar = p.iChar;
			int iStartChar = p.iChar;
			for (int i = p.iChar; i < this.lines[p.iLine].Count; i++)
			{
				char c = this.lines[p.iLine][i].c;
				bool flag = char.IsLetterOrDigit(c) || c == '_';
				if (!flag)
				{
					break;
				}
				iStartChar = i + 1;
			}
			for (int j = p.iChar - 1; j >= 0; j--)
			{
				char c2 = this.lines[p.iLine][j].c;
				bool flag2 = char.IsLetterOrDigit(c2) || c2 == '_';
				if (!flag2)
				{
					break;
				}
				iEndChar = j;
			}
			this.Selection = new Range(this, iStartChar, p.iLine, iEndChar, p.iLine);
		}

		// Token: 0x06006574 RID: 25972 RVA: 0x001EA9D0 File Offset: 0x001EA9D0
		public int YtoLineIndex(int y)
		{
			int num = this.LineInfos.BinarySearch(new LineInfo(-10), new FastColoredTextBox.LineYComparer(y));
			num = ((num < 0) ? (-num - 2) : num);
			bool flag = num < 0;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = num > this.lines.Count - 1;
				if (flag2)
				{
					result = this.lines.Count - 1;
				}
				else
				{
					result = num;
				}
			}
			return result;
		}

		// Token: 0x06006575 RID: 25973 RVA: 0x001EAA54 File Offset: 0x001EAA54
		public Place PointToPlace(Point point)
		{
			point.Offset(base.HorizontalScroll.Value, base.VerticalScroll.Value);
			point.Offset(-this.LeftIndent - this.Paddings.Left, 0);
			int i = this.YtoLineIndex(point.Y);
			bool flag = i < 0;
			Place result;
			if (flag)
			{
				result = Place.Empty;
			}
			else
			{
				int num = 0;
				while (i < this.lines.Count)
				{
					num = this.LineInfos[i].startY + this.LineInfos[i].WordWrapStringsCount * this.CharHeight;
					bool flag2 = num > point.Y && this.LineInfos[i].VisibleState == VisibleState.Visible;
					if (flag2)
					{
						break;
					}
					i++;
				}
				bool flag3 = i >= this.lines.Count;
				if (flag3)
				{
					i = this.lines.Count - 1;
				}
				bool flag4 = this.LineInfos[i].VisibleState > VisibleState.Visible;
				if (flag4)
				{
					i = this.FindPrevVisibleLine(i);
				}
				int num2 = this.LineInfos[i].WordWrapStringsCount;
				bool flag5 = num > point.Y;
				if (flag5)
				{
					int num3 = (num - point.Y - this.CharHeight) / this.CharHeight;
					num -= num3 * this.CharHeight;
					num2 -= num3;
				}
				do
				{
					num2--;
					num -= this.CharHeight;
				}
				while (num > point.Y);
				bool flag6 = num2 < 0;
				if (flag6)
				{
					num2 = 0;
				}
				int wordWrapStringStartPosition = this.LineInfos[i].GetWordWrapStringStartPosition(num2);
				int wordWrapStringFinishPosition = this.LineInfos[i].GetWordWrapStringFinishPosition(num2, this.lines[i]);
				int num4 = (int)Math.Round((double)((float)point.X / (float)this.CharWidth));
				bool flag7 = num2 > 0;
				if (flag7)
				{
					num4 -= this.LineInfos[i].wordWrapIndent;
				}
				num4 = ((num4 < 0) ? wordWrapStringStartPosition : (wordWrapStringStartPosition + num4));
				bool flag8 = num4 > wordWrapStringFinishPosition;
				if (flag8)
				{
					num4 = wordWrapStringFinishPosition + 1;
				}
				bool flag9 = num4 > this.lines[i].Count;
				if (flag9)
				{
					num4 = this.lines[i].Count;
				}
				result = new Place(num4, i);
			}
			return result;
		}

		// Token: 0x06006576 RID: 25974 RVA: 0x001EAD14 File Offset: 0x001EAD14
		private Place PointToPlaceSimple(Point point)
		{
			point.Offset(base.HorizontalScroll.Value, base.VerticalScroll.Value);
			point.Offset(-this.LeftIndent - this.Paddings.Left, 0);
			int iLine = this.YtoLineIndex(point.Y);
			int num = (int)Math.Round((double)((float)point.X / (float)this.CharWidth));
			bool flag = num < 0;
			if (flag)
			{
				num = 0;
			}
			return new Place(num, iLine);
		}

		// Token: 0x06006577 RID: 25975 RVA: 0x001EADA8 File Offset: 0x001EADA8
		public int PointToPosition(Point point)
		{
			return this.PlaceToPosition(this.PointToPlace(point));
		}

		// Token: 0x06006578 RID: 25976 RVA: 0x001EADD0 File Offset: 0x001EADD0
		public virtual void OnTextChanging(ref string text)
		{
			this.ClearBracketsPositions();
			bool flag = this.TextChanging != null;
			if (flag)
			{
				TextChangingEventArgs textChangingEventArgs = new TextChangingEventArgs
				{
					InsertingText = text
				};
				this.TextChanging(this, textChangingEventArgs);
				text = textChangingEventArgs.InsertingText;
				bool cancel = textChangingEventArgs.Cancel;
				if (cancel)
				{
					text = string.Empty;
				}
			}
		}

		// Token: 0x06006579 RID: 25977 RVA: 0x001EAE34 File Offset: 0x001EAE34
		public virtual void OnTextChanging()
		{
			string text = null;
			this.OnTextChanging(ref text);
		}

		// Token: 0x0600657A RID: 25978 RVA: 0x001EAE54 File Offset: 0x001EAE54
		public virtual void OnTextChanged()
		{
			Range range = new Range(this);
			range.SelectAll();
			this.OnTextChanged(new TextChangedEventArgs(range));
		}

		// Token: 0x0600657B RID: 25979 RVA: 0x001EAE84 File Offset: 0x001EAE84
		public virtual void OnTextChanged(int fromLine, int toLine)
		{
			this.OnTextChanged(new TextChangedEventArgs(new Range(this)
			{
				Start = new Place(0, Math.Min(fromLine, toLine)),
				End = new Place(this.lines[Math.Max(fromLine, toLine)].Count, Math.Max(fromLine, toLine))
			}));
		}

		// Token: 0x0600657C RID: 25980 RVA: 0x001EAEE8 File Offset: 0x001EAEE8
		public virtual void OnTextChanged(Range r)
		{
			this.OnTextChanged(new TextChangedEventArgs(r));
		}

		// Token: 0x0600657D RID: 25981 RVA: 0x001EAEF8 File Offset: 0x001EAEF8
		public void BeginUpdate()
		{
			bool flag = this.updating == 0;
			if (flag)
			{
				this.updatingRange = null;
			}
			this.updating++;
		}

		// Token: 0x0600657E RID: 25982 RVA: 0x001EAF30 File Offset: 0x001EAF30
		public void EndUpdate()
		{
			this.updating--;
			bool flag = this.updating == 0 && this.updatingRange != null;
			if (flag)
			{
				this.updatingRange.Expand();
				this.OnTextChanged(this.updatingRange);
			}
		}

		// Token: 0x0600657F RID: 25983 RVA: 0x001EAF8C File Offset: 0x001EAF8C
		protected virtual void OnTextChanged(TextChangedEventArgs args)
		{
			args.ChangedRange.Normalize();
			bool flag = this.updating > 0;
			if (flag)
			{
				bool flag2 = this.updatingRange == null;
				if (flag2)
				{
					this.updatingRange = args.ChangedRange.Clone();
				}
				else
				{
					bool flag3 = this.updatingRange.Start.iLine > args.ChangedRange.Start.iLine;
					if (flag3)
					{
						this.updatingRange.Start = new Place(0, args.ChangedRange.Start.iLine);
					}
					bool flag4 = this.updatingRange.End.iLine < args.ChangedRange.End.iLine;
					if (flag4)
					{
						this.updatingRange.End = new Place(this.lines[args.ChangedRange.End.iLine].Count, args.ChangedRange.End.iLine);
					}
					this.updatingRange = this.updatingRange.GetIntersectionWith(this.Range);
				}
			}
			else
			{
				this.CancelToolTip();
				this.ClearHints();
				this.IsChanged = true;
				int textVersion = this.TextVersion;
				this.TextVersion = textVersion + 1;
				this.MarkLinesAsChanged(args.ChangedRange);
				this.ClearFoldingState(args.ChangedRange);
				bool flag5 = this.wordWrap;
				if (flag5)
				{
					this.RecalcWordWrap(args.ChangedRange.Start.iLine, args.ChangedRange.End.iLine);
				}
				base.OnTextChanged(args);
				bool flag6 = this.delayedTextChangedRange == null;
				if (flag6)
				{
					this.delayedTextChangedRange = args.ChangedRange.Clone();
				}
				else
				{
					this.delayedTextChangedRange = this.delayedTextChangedRange.GetUnionWith(args.ChangedRange);
				}
				this.needRiseTextChangedDelayed = true;
				this.ResetTimer(this.timer2);
				this.OnSyntaxHighlight(args);
				bool flag7 = this.TextChanged != null;
				if (flag7)
				{
					this.TextChanged(this, args);
				}
				bool flag8 = this.BindingTextChanged != null;
				if (flag8)
				{
					this.BindingTextChanged(this, EventArgs.Empty);
				}
				base.OnTextChanged(EventArgs.Empty);
				this.OnVisibleRangeChanged();
			}
		}

		// Token: 0x06006580 RID: 25984 RVA: 0x001EB1E8 File Offset: 0x001EB1E8
		private void ClearFoldingState(Range range)
		{
			for (int i = range.Start.iLine; i <= range.End.iLine; i++)
			{
				bool flag = i >= 0 && i < this.lines.Count;
				if (flag)
				{
					this.FoldedBlocks.Remove(this[i].UniqueId);
				}
			}
		}

		// Token: 0x06006581 RID: 25985 RVA: 0x001EB260 File Offset: 0x001EB260
		private void MarkLinesAsChanged(Range range)
		{
			for (int i = range.Start.iLine; i <= range.End.iLine; i++)
			{
				bool flag = i >= 0 && i < this.lines.Count;
				if (flag)
				{
					this.lines[i].IsChanged = true;
				}
			}
		}

		// Token: 0x06006582 RID: 25986 RVA: 0x001EB2D4 File Offset: 0x001EB2D4
		public virtual void OnSelectionChanged()
		{
			bool flag = this.HighlightFoldingIndicator;
			if (flag)
			{
				this.HighlightFoldings();
			}
			this.needRiseSelectionChangedDelayed = true;
			this.ResetTimer(this.timer);
			bool flag2 = this.SelectionChanged != null;
			if (flag2)
			{
				this.SelectionChanged(this, new EventArgs());
			}
		}

		// Token: 0x06006583 RID: 25987 RVA: 0x001EB330 File Offset: 0x001EB330
		private void HighlightFoldings()
		{
			bool flag = this.LinesCount == 0;
			if (!flag)
			{
				int num = this.startFoldingLine;
				int num2 = this.endFoldingLine;
				this.startFoldingLine = -1;
				this.endFoldingLine = -1;
				int num3 = 0;
				for (int i = this.Selection.Start.iLine; i >= Math.Max(this.Selection.Start.iLine - 3000, 0); i--)
				{
					bool flag2 = this.lines.LineHasFoldingStartMarker(i);
					bool flag3 = this.lines.LineHasFoldingEndMarker(i);
					bool flag4 = flag3 && flag2;
					if (!flag4)
					{
						bool flag5 = flag2;
						if (flag5)
						{
							num3--;
							bool flag6 = num3 == -1;
							if (flag6)
							{
								this.startFoldingLine = i;
								break;
							}
						}
						bool flag7 = flag3 && i != this.Selection.Start.iLine;
						if (flag7)
						{
							num3++;
						}
					}
				}
				bool flag8 = this.startFoldingLine >= 0;
				if (flag8)
				{
					this.endFoldingLine = this.FindEndOfFoldingBlock(this.startFoldingLine, 3000);
					bool flag9 = this.endFoldingLine == this.startFoldingLine;
					if (flag9)
					{
						this.endFoldingLine = -1;
					}
				}
				bool flag10 = this.startFoldingLine != num || this.endFoldingLine != num2;
				if (flag10)
				{
					this.OnFoldingHighlightChanged();
				}
			}
		}

		// Token: 0x06006584 RID: 25988 RVA: 0x001EB4C8 File Offset: 0x001EB4C8
		protected virtual void OnFoldingHighlightChanged()
		{
			bool flag = this.FoldingHighlightChanged != null;
			if (flag)
			{
				this.FoldingHighlightChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06006585 RID: 25989 RVA: 0x001EB4FC File Offset: 0x001EB4FC
		protected override void OnGotFocus(EventArgs e)
		{
			this.SetAsCurrentTB();
			base.OnGotFocus(e);
			this.Invalidate();
		}

		// Token: 0x06006586 RID: 25990 RVA: 0x001EB518 File Offset: 0x001EB518
		protected override void OnLostFocus(EventArgs e)
		{
			this.lastModifiers = Keys.None;
			this.DeactivateMiddleClickScrollingMode();
			base.OnLostFocus(e);
			this.Invalidate();
		}

		// Token: 0x06006587 RID: 25991 RVA: 0x001EB538 File Offset: 0x001EB538
		public int PlaceToPosition(Place point)
		{
			bool flag = point.iLine < 0 || point.iLine >= this.lines.Count || point.iChar >= this.lines[point.iLine].Count + Environment.NewLine.Length;
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				int num = 0;
				for (int i = 0; i < point.iLine; i++)
				{
					num += this.lines[i].Count + Environment.NewLine.Length;
				}
				num += point.iChar;
				result = num;
			}
			return result;
		}

		// Token: 0x06006588 RID: 25992 RVA: 0x001EB5F8 File Offset: 0x001EB5F8
		public Place PositionToPlace(int pos)
		{
			bool flag = pos < 0;
			Place result;
			if (flag)
			{
				result = new Place(0, 0);
			}
			else
			{
				for (int i = 0; i < this.lines.Count; i++)
				{
					int num = this.lines[i].Count + Environment.NewLine.Length;
					bool flag2 = pos < this.lines[i].Count;
					if (flag2)
					{
						return new Place(pos, i);
					}
					bool flag3 = pos < num;
					if (flag3)
					{
						return new Place(this.lines[i].Count, i);
					}
					pos -= num;
				}
				bool flag4 = this.lines.Count > 0;
				if (flag4)
				{
					result = new Place(this.lines[this.lines.Count - 1].Count, this.lines.Count - 1);
				}
				else
				{
					result = new Place(0, 0);
				}
			}
			return result;
		}

		// Token: 0x06006589 RID: 25993 RVA: 0x001EB718 File Offset: 0x001EB718
		public Point PositionToPoint(int pos)
		{
			return this.PlaceToPoint(this.PositionToPlace(pos));
		}

		// Token: 0x0600658A RID: 25994 RVA: 0x001EB740 File Offset: 0x001EB740
		public Point PlaceToPoint(Place place)
		{
			bool flag = place.iLine >= this.LineInfos.Count;
			Point result;
			if (flag)
			{
				result = default(Point);
			}
			else
			{
				int num = this.LineInfos[place.iLine].startY;
				int wordWrapStringIndex = this.LineInfos[place.iLine].GetWordWrapStringIndex(place.iChar);
				num += wordWrapStringIndex * this.CharHeight;
				int num2 = (place.iChar - this.LineInfos[place.iLine].GetWordWrapStringStartPosition(wordWrapStringIndex)) * this.CharWidth;
				bool flag2 = wordWrapStringIndex > 0;
				if (flag2)
				{
					num2 += this.LineInfos[place.iLine].wordWrapIndent * this.CharWidth;
				}
				num -= base.VerticalScroll.Value;
				num2 = this.LeftIndent + this.Paddings.Left + num2 - base.HorizontalScroll.Value;
				result = new Point(num2, num);
			}
			return result;
		}

		// Token: 0x0600658B RID: 25995 RVA: 0x001EB864 File Offset: 0x001EB864
		public Range GetRange(int fromPos, int toPos)
		{
			return new Range(this)
			{
				Start = this.PositionToPlace(fromPos),
				End = this.PositionToPlace(toPos)
			};
		}

		// Token: 0x0600658C RID: 25996 RVA: 0x001EB8A4 File Offset: 0x001EB8A4
		public Range GetRange(Place fromPlace, Place toPlace)
		{
			return new Range(this, fromPlace, toPlace);
		}

		// Token: 0x0600658D RID: 25997 RVA: 0x001EB8C8 File Offset: 0x001EB8C8
		public IEnumerable<Range> GetRanges(string regexPattern)
		{
			Range range = new Range(this);
			range.SelectAll();
			foreach (Range r in range.GetRanges(regexPattern, RegexOptions.None))
			{
				yield return r;
				r = null;
			}
			IEnumerator<Range> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600658E RID: 25998 RVA: 0x001EB8E0 File Offset: 0x001EB8E0
		public IEnumerable<Range> GetRanges(string regexPattern, RegexOptions options)
		{
			Range range = new Range(this);
			range.SelectAll();
			foreach (Range r in range.GetRanges(regexPattern, options))
			{
				yield return r;
				r = null;
			}
			IEnumerator<Range> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600658F RID: 25999 RVA: 0x001EB900 File Offset: 0x001EB900
		public string GetLineText(int iLine)
		{
			bool flag = iLine < 0 || iLine >= this.lines.Count;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("Line index out of range");
			}
			StringBuilder stringBuilder = new StringBuilder(this.lines[iLine].Count);
			foreach (Char @char in this.lines[iLine])
			{
				stringBuilder.Append(@char.c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006590 RID: 26000 RVA: 0x001EB9BC File Offset: 0x001EB9BC
		public virtual void ExpandFoldedBlock(int iLine)
		{
			bool flag = iLine < 0 || iLine >= this.lines.Count;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("Line index out of range");
			}
			int i;
			for (i = iLine; i < this.LinesCount - 1; i++)
			{
				bool flag2 = this.LineInfos[i + 1].VisibleState != VisibleState.Hidden;
				if (flag2)
				{
					break;
				}
			}
			this.ExpandBlock(iLine, i);
			this.FoldedBlocks.Remove(this[iLine].UniqueId);
			this.AdjustFolding();
		}

		// Token: 0x06006591 RID: 26001 RVA: 0x001EBA68 File Offset: 0x001EBA68
		public virtual void AdjustFolding()
		{
			for (int i = 0; i < this.LinesCount; i++)
			{
				bool flag = this.LineInfos[i].VisibleState == VisibleState.Visible;
				if (flag)
				{
					bool flag2 = this.FoldedBlocks.ContainsKey(this[i].UniqueId);
					if (flag2)
					{
						this.CollapseFoldingBlock(i);
					}
				}
			}
		}

		// Token: 0x06006592 RID: 26002 RVA: 0x001EBAD4 File Offset: 0x001EBAD4
		public virtual void ExpandBlock(int fromLine, int toLine)
		{
			int num = Math.Min(fromLine, toLine);
			int num2 = Math.Max(fromLine, toLine);
			for (int i = num; i <= num2; i++)
			{
				this.SetVisibleState(i, VisibleState.Visible);
			}
			this.needRecalc = true;
			this.Invalidate();
			this.OnVisibleRangeChanged();
		}

		// Token: 0x06006593 RID: 26003 RVA: 0x001EBB2C File Offset: 0x001EBB2C
		public void ExpandBlock(int iLine)
		{
			bool flag = this.LineInfos[iLine].VisibleState == VisibleState.Visible;
			if (!flag)
			{
				for (int i = iLine; i < this.LinesCount; i++)
				{
					bool flag2 = this.LineInfos[i].VisibleState == VisibleState.Visible;
					if (flag2)
					{
						break;
					}
					this.SetVisibleState(i, VisibleState.Visible);
					this.needRecalc = true;
				}
				for (int j = iLine - 1; j >= 0; j--)
				{
					bool flag3 = this.LineInfos[j].VisibleState == VisibleState.Visible;
					if (flag3)
					{
						break;
					}
					this.SetVisibleState(j, VisibleState.Visible);
					this.needRecalc = true;
				}
				this.Invalidate();
				this.OnVisibleRangeChanged();
			}
		}

		// Token: 0x06006594 RID: 26004 RVA: 0x001EBC0C File Offset: 0x001EBC0C
		public virtual void CollapseAllFoldingBlocks()
		{
			for (int i = 0; i < this.LinesCount; i++)
			{
				bool flag = this.lines.LineHasFoldingStartMarker(i);
				if (flag)
				{
					int num = this.FindEndOfFoldingBlock(i);
					bool flag2 = num >= 0;
					if (flag2)
					{
						this.CollapseBlock(i, num);
						i = num;
					}
				}
			}
			this.OnVisibleRangeChanged();
			this.UpdateScrollbars();
		}

		// Token: 0x06006595 RID: 26005 RVA: 0x001EBC80 File Offset: 0x001EBC80
		public virtual void ExpandAllFoldingBlocks()
		{
			for (int i = 0; i < this.LinesCount; i++)
			{
				this.SetVisibleState(i, VisibleState.Visible);
			}
			this.FoldedBlocks.Clear();
			this.OnVisibleRangeChanged();
			this.Invalidate();
			this.UpdateScrollbars();
		}

		// Token: 0x06006596 RID: 26006 RVA: 0x001EBCD4 File Offset: 0x001EBCD4
		public virtual void CollapseFoldingBlock(int iLine)
		{
			bool flag = iLine < 0 || iLine >= this.lines.Count;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("Line index out of range");
			}
			bool flag2 = string.IsNullOrEmpty(this.lines[iLine].FoldingStartMarker);
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("This line is not folding start line");
			}
			int num = this.FindEndOfFoldingBlock(iLine);
			bool flag3 = num >= 0;
			if (flag3)
			{
				this.CollapseBlock(iLine, num);
				int uniqueId = this[iLine].UniqueId;
				this.FoldedBlocks[uniqueId] = uniqueId;
			}
		}

		// Token: 0x06006597 RID: 26007 RVA: 0x001EBD80 File Offset: 0x001EBD80
		private int FindEndOfFoldingBlock(int iStartLine)
		{
			return this.FindEndOfFoldingBlock(iStartLine, int.MaxValue);
		}

		// Token: 0x06006598 RID: 26008 RVA: 0x001EBDA8 File Offset: 0x001EBDA8
		protected virtual int FindEndOfFoldingBlock(int iStartLine, int maxLines)
		{
			string foldingStartMarker = this.lines[iStartLine].FoldingStartMarker;
			Stack<string> stack = new Stack<string>();
			FindEndOfFoldingBlockStrategy findEndOfFoldingBlockStrategy = this.FindEndOfFoldingBlockStrategy;
			if (findEndOfFoldingBlockStrategy != FindEndOfFoldingBlockStrategy.Strategy1)
			{
				if (findEndOfFoldingBlockStrategy == FindEndOfFoldingBlockStrategy.Strategy2)
				{
					for (int i = iStartLine; i < this.LinesCount; i++)
					{
						bool flag = this.lines.LineHasFoldingEndMarker(i);
						if (flag)
						{
							string foldingEndMarker = this.lines[i].FoldingEndMarker;
							while (stack.Count > 0 && stack.Pop() != foldingEndMarker)
							{
							}
							bool flag2 = stack.Count == 0;
							if (flag2)
							{
								return i;
							}
						}
						bool flag3 = this.lines.LineHasFoldingStartMarker(i);
						if (flag3)
						{
							stack.Push(this.lines[i].FoldingStartMarker);
						}
						maxLines--;
						bool flag4 = maxLines < 0;
						if (flag4)
						{
							return i;
						}
					}
				}
			}
			else
			{
				for (int i = iStartLine; i < this.LinesCount; i++)
				{
					bool flag5 = this.lines.LineHasFoldingStartMarker(i);
					if (flag5)
					{
						stack.Push(this.lines[i].FoldingStartMarker);
					}
					bool flag6 = this.lines.LineHasFoldingEndMarker(i);
					if (flag6)
					{
						string foldingEndMarker2 = this.lines[i].FoldingEndMarker;
						while (stack.Count > 0 && stack.Pop() != foldingEndMarker2)
						{
						}
						bool flag7 = stack.Count == 0;
						if (flag7)
						{
							return i;
						}
					}
					maxLines--;
					bool flag8 = maxLines < 0;
					if (flag8)
					{
						return i;
					}
				}
			}
			return this.LinesCount - 1;
		}

		// Token: 0x06006599 RID: 26009 RVA: 0x001EBFB8 File Offset: 0x001EBFB8
		public string GetLineFoldingStartMarker(int iLine)
		{
			bool flag = this.lines.LineHasFoldingStartMarker(iLine);
			string result;
			if (flag)
			{
				result = this.lines[iLine].FoldingStartMarker;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600659A RID: 26010 RVA: 0x001EBFFC File Offset: 0x001EBFFC
		public string GetLineFoldingEndMarker(int iLine)
		{
			bool flag = this.lines.LineHasFoldingEndMarker(iLine);
			string result;
			if (flag)
			{
				result = this.lines[iLine].FoldingEndMarker;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600659B RID: 26011 RVA: 0x001EC040 File Offset: 0x001EC040
		protected virtual void RecalcFoldingLines()
		{
			bool flag = !this.needRecalcFoldingLines;
			if (!flag)
			{
				this.needRecalcFoldingLines = false;
				bool flag2 = !this.ShowFoldingLines;
				if (!flag2)
				{
					this.foldingPairs.Clear();
					Range range = this.VisibleRange;
					int num = Math.Max(range.Start.iLine - 3000, 0);
					int num2 = Math.Min(range.End.iLine + 3000, Math.Max(range.End.iLine, this.LinesCount - 1));
					Stack<int> stack = new Stack<int>();
					for (int i = num; i <= num2; i++)
					{
						bool flag3 = this.lines.LineHasFoldingStartMarker(i);
						bool flag4 = this.lines.LineHasFoldingEndMarker(i);
						bool flag5 = flag4 && flag3;
						if (!flag5)
						{
							bool flag6 = flag3;
							if (flag6)
							{
								stack.Push(i);
							}
							bool flag7 = flag4;
							if (flag7)
							{
								string foldingEndMarker = this.lines[i].FoldingEndMarker;
								while (stack.Count > 0)
								{
									int num3 = stack.Pop();
									this.foldingPairs[num3] = i;
									bool flag8 = foldingEndMarker == this.lines[num3].FoldingStartMarker;
									if (flag8)
									{
										break;
									}
								}
							}
						}
					}
					while (stack.Count > 0)
					{
						this.foldingPairs[stack.Pop()] = num2 + 1;
					}
				}
			}
		}

		// Token: 0x0600659C RID: 26012 RVA: 0x001EC1F4 File Offset: 0x001EC1F4
		public virtual void CollapseBlock(int fromLine, int toLine)
		{
			int i = Math.Min(fromLine, toLine);
			int num = Math.Max(fromLine, toLine);
			bool flag = i == num;
			if (!flag)
			{
				while (i <= num)
				{
					bool flag2 = this.GetLineText(i).Trim().Length > 0;
					if (flag2)
					{
						for (int j = i + 1; j <= num; j++)
						{
							this.SetVisibleState(j, VisibleState.Hidden);
						}
						this.SetVisibleState(i, VisibleState.StartOfHiddenBlock);
						this.Invalidate();
						break;
					}
					i++;
				}
				i = Math.Min(fromLine, toLine);
				num = Math.Max(fromLine, toLine);
				int num2 = this.FindNextVisibleLine(num);
				bool flag3 = num2 == num;
				if (flag3)
				{
					num2 = this.FindPrevVisibleLine(i);
				}
				this.Selection.Start = new Place(0, num2);
				this.needRecalc = true;
				this.Invalidate();
				this.OnVisibleRangeChanged();
			}
		}

		// Token: 0x0600659D RID: 26013 RVA: 0x001EC2F0 File Offset: 0x001EC2F0
		internal int FindNextVisibleLine(int iLine)
		{
			bool flag = iLine >= this.lines.Count - 1;
			int result;
			if (flag)
			{
				result = iLine;
			}
			else
			{
				int num = iLine;
				do
				{
					iLine++;
				}
				while (iLine < this.lines.Count - 1 && this.LineInfos[iLine].VisibleState > VisibleState.Visible);
				bool flag2 = this.LineInfos[iLine].VisibleState > VisibleState.Visible;
				if (flag2)
				{
					result = num;
				}
				else
				{
					result = iLine;
				}
			}
			return result;
		}

		// Token: 0x0600659E RID: 26014 RVA: 0x001EC388 File Offset: 0x001EC388
		internal int FindPrevVisibleLine(int iLine)
		{
			bool flag = iLine <= 0;
			int result;
			if (flag)
			{
				result = iLine;
			}
			else
			{
				int num = iLine;
				do
				{
					iLine--;
				}
				while (iLine > 0 && this.LineInfos[iLine].VisibleState > VisibleState.Visible);
				bool flag2 = this.LineInfos[iLine].VisibleState > VisibleState.Visible;
				if (flag2)
				{
					result = num;
				}
				else
				{
					result = iLine;
				}
			}
			return result;
		}

		// Token: 0x0600659F RID: 26015 RVA: 0x001EC408 File Offset: 0x001EC408
		private VisualMarker FindVisualMarkerForPoint(Point p)
		{
			foreach (VisualMarker visualMarker in this.visibleMarkers)
			{
				bool flag = visualMarker.rectangle.Contains(p);
				if (flag)
				{
					return visualMarker;
				}
			}
			return null;
		}

		// Token: 0x060065A0 RID: 26016 RVA: 0x001EC488 File Offset: 0x001EC488
		public virtual void IncreaseIndent()
		{
			bool flag = this.Selection.Start == this.Selection.End;
			if (flag)
			{
				bool flag2 = !this.Selection.ReadOnly;
				if (flag2)
				{
					this.Selection.Start = new Place(this[this.Selection.Start.iLine].StartSpacesCount, this.Selection.Start.iLine);
					int num = this.TabLength - this.Selection.Start.iChar % this.TabLength;
					bool flag3 = this.IsReplaceMode;
					if (flag3)
					{
						for (int i = 0; i < num; i++)
						{
							this.Selection.GoRight(true);
						}
						this.Selection.Inverse();
					}
					this.InsertText(new string(' ', num));
				}
			}
			else
			{
				bool flag4 = this.Selection.Start > this.Selection.End && !this.Selection.ColumnSelectionMode;
				int iChar = 0;
				bool columnSelectionMode = this.Selection.ColumnSelectionMode;
				if (columnSelectionMode)
				{
					iChar = Math.Min(this.Selection.End.iChar, this.Selection.Start.iChar);
				}
				this.BeginUpdate();
				this.Selection.BeginUpdate();
				this.lines.Manager.BeginAutoUndoCommands();
				Range range = this.Selection.Clone();
				this.lines.Manager.ExecuteCommand(new SelectCommand(this.TextSource));
				this.Selection.Normalize();
				Range range2 = this.Selection.Clone();
				int iLine = this.Selection.Start.iLine;
				int num2 = this.Selection.End.iLine;
				bool flag5 = !this.Selection.ColumnSelectionMode;
				if (flag5)
				{
					bool flag6 = this.Selection.End.iChar == 0;
					if (flag6)
					{
						num2--;
					}
				}
				for (int j = iLine; j <= num2; j++)
				{
					bool flag7 = this.lines[j].Count == 0;
					if (!flag7)
					{
						this.Selection.Start = new Place(iChar, j);
						this.lines.Manager.ExecuteCommand(new InsertTextCommand(this.TextSource, new string(' ', this.TabLength)));
					}
				}
				bool flag8 = !this.Selection.ColumnSelectionMode;
				if (flag8)
				{
					int iChar2 = range2.Start.iChar + this.TabLength;
					int iChar3 = range2.End.iChar + ((range2.End.iLine == num2) ? this.TabLength : 0);
					this.Selection.Start = new Place(iChar2, range2.Start.iLine);
					this.Selection.End = new Place(iChar3, range2.End.iLine);
				}
				else
				{
					this.Selection = range;
				}
				this.lines.Manager.EndAutoUndoCommands();
				bool flag9 = flag4;
				if (flag9)
				{
					this.Selection.Inverse();
				}
				this.needRecalc = true;
				this.Selection.EndUpdate();
				this.EndUpdate();
				this.Invalidate();
			}
		}

		// Token: 0x060065A1 RID: 26017 RVA: 0x001EC830 File Offset: 0x001EC830
		public virtual void DecreaseIndent()
		{
			bool flag = this.Selection.Start.iLine == this.Selection.End.iLine;
			if (flag)
			{
				this.DecreaseIndentOfSingleLine();
			}
			else
			{
				int num = 0;
				bool columnSelectionMode = this.Selection.ColumnSelectionMode;
				if (columnSelectionMode)
				{
					num = Math.Min(this.Selection.End.iChar, this.Selection.Start.iChar);
				}
				this.BeginUpdate();
				this.Selection.BeginUpdate();
				this.lines.Manager.BeginAutoUndoCommands();
				Range range = this.Selection.Clone();
				this.lines.Manager.ExecuteCommand(new SelectCommand(this.TextSource));
				Range range2 = this.Selection.Clone();
				this.Selection.Normalize();
				int iLine = this.Selection.Start.iLine;
				int num2 = this.Selection.End.iLine;
				bool flag2 = !this.Selection.ColumnSelectionMode;
				if (flag2)
				{
					bool flag3 = this.Selection.End.iChar == 0;
					if (flag3)
					{
						num2--;
					}
				}
				int num3 = 0;
				int num4 = 0;
				for (int i = iLine; i <= num2; i++)
				{
					bool flag4 = num > this.lines[i].Count;
					if (!flag4)
					{
						int num5 = Math.Min(this.lines[i].Count, num + this.TabLength);
						string text = this.lines[i].Text.Substring(num, num5 - num);
						num5 = Math.Min(num5, num + text.Length - text.TrimStart(new char[0]).Length);
						this.Selection = new Range(this, new Place(num, i), new Place(num5, i));
						int num6 = num5 - num;
						bool flag5 = i == range2.Start.iLine;
						if (flag5)
						{
							num3 = num6;
						}
						bool flag6 = i == range2.End.iLine;
						if (flag6)
						{
							num4 = num6;
						}
						bool flag7 = !this.Selection.IsEmpty;
						if (flag7)
						{
							this.ClearSelected();
						}
					}
				}
				bool flag8 = !this.Selection.ColumnSelectionMode;
				if (flag8)
				{
					int iChar = Math.Max(0, range2.Start.iChar - num3);
					int iChar2 = Math.Max(0, range2.End.iChar - num4);
					this.Selection.Start = new Place(iChar, range2.Start.iLine);
					this.Selection.End = new Place(iChar2, range2.End.iLine);
				}
				else
				{
					this.Selection = range;
				}
				this.lines.Manager.EndAutoUndoCommands();
				this.needRecalc = true;
				this.Selection.EndUpdate();
				this.EndUpdate();
				this.Invalidate();
			}
		}

		// Token: 0x060065A2 RID: 26018 RVA: 0x001ECB6C File Offset: 0x001ECB6C
		protected virtual void DecreaseIndentOfSingleLine()
		{
			bool flag = this.Selection.Start.iLine != this.Selection.End.iLine;
			if (!flag)
			{
				Range range = this.Selection.Clone();
				int iLine = this.Selection.Start.iLine;
				int num = Math.Min(this.Selection.Start.iChar, this.Selection.End.iChar);
				string text = this.lines[iLine].Text;
				Match match = new Regex("\\s*", RegexOptions.RightToLeft).Match(text, num);
				int index = match.Index;
				int length = match.Length;
				int num2 = 0;
				bool flag2 = length > 0;
				if (flag2)
				{
					int num3 = (this.TabLength > 0) ? (num % this.TabLength) : 0;
					num2 = ((num3 != 0) ? Math.Min(num3, length) : Math.Min(this.TabLength, length));
				}
				bool flag3 = num2 > 0;
				if (flag3)
				{
					this.BeginUpdate();
					this.Selection.BeginUpdate();
					this.lines.Manager.BeginAutoUndoCommands();
					this.lines.Manager.ExecuteCommand(new SelectCommand(this.TextSource));
					this.Selection.Start = new Place(index, iLine);
					this.Selection.End = new Place(index + num2, iLine);
					this.ClearSelected();
					int iChar = range.Start.iChar - num2;
					int iChar2 = range.End.iChar - num2;
					this.Selection.Start = new Place(iChar, iLine);
					this.Selection.End = new Place(iChar2, iLine);
					this.lines.Manager.ExecuteCommand(new SelectCommand(this.TextSource));
					this.lines.Manager.EndAutoUndoCommands();
					this.Selection.EndUpdate();
					this.EndUpdate();
				}
				this.Invalidate();
			}
		}

		// Token: 0x060065A3 RID: 26019 RVA: 0x001ECD94 File Offset: 0x001ECD94
		public virtual void DoAutoIndent()
		{
			bool columnSelectionMode = this.Selection.ColumnSelectionMode;
			if (!columnSelectionMode)
			{
				Range range = this.Selection.Clone();
				range.Normalize();
				this.BeginUpdate();
				this.Selection.BeginUpdate();
				this.lines.Manager.BeginAutoUndoCommands();
				for (int i = range.Start.iLine; i <= range.End.iLine; i++)
				{
					this.DoAutoIndent(i);
				}
				this.lines.Manager.EndAutoUndoCommands();
				this.Selection.Start = range.Start;
				this.Selection.End = range.End;
				this.Selection.Expand();
				this.Selection.EndUpdate();
				this.EndUpdate();
			}
		}

		// Token: 0x060065A4 RID: 26020 RVA: 0x001ECE7C File Offset: 0x001ECE7C
		public virtual void InsertLinePrefix(string prefix)
		{
			Range range = this.Selection.Clone();
			int num = Math.Min(this.Selection.Start.iLine, this.Selection.End.iLine);
			int num2 = Math.Max(this.Selection.Start.iLine, this.Selection.End.iLine);
			this.BeginUpdate();
			this.Selection.BeginUpdate();
			this.lines.Manager.BeginAutoUndoCommands();
			this.lines.Manager.ExecuteCommand(new SelectCommand(this.TextSource));
			int minStartSpacesCount = this.GetMinStartSpacesCount(num, num2);
			for (int i = num; i <= num2; i++)
			{
				this.Selection.Start = new Place(minStartSpacesCount, i);
				this.lines.Manager.ExecuteCommand(new InsertTextCommand(this.TextSource, prefix));
			}
			this.Selection.Start = new Place(0, num);
			this.Selection.End = new Place(this.lines[num2].Count, num2);
			this.needRecalc = true;
			this.lines.Manager.EndAutoUndoCommands();
			this.Selection.EndUpdate();
			this.EndUpdate();
			this.Invalidate();
		}

		// Token: 0x060065A5 RID: 26021 RVA: 0x001ECFE8 File Offset: 0x001ECFE8
		public virtual void RemoveLinePrefix(string prefix)
		{
			Range range = this.Selection.Clone();
			int num = Math.Min(this.Selection.Start.iLine, this.Selection.End.iLine);
			int num2 = Math.Max(this.Selection.Start.iLine, this.Selection.End.iLine);
			this.BeginUpdate();
			this.Selection.BeginUpdate();
			this.lines.Manager.BeginAutoUndoCommands();
			this.lines.Manager.ExecuteCommand(new SelectCommand(this.TextSource));
			for (int i = num; i <= num2; i++)
			{
				string text = this.lines[i].Text;
				string text2 = text.TrimStart(new char[0]);
				bool flag = text2.StartsWith(prefix);
				if (flag)
				{
					int num3 = text.Length - text2.Length;
					this.Selection.Start = new Place(num3, i);
					this.Selection.End = new Place(num3 + prefix.Length, i);
					this.ClearSelected();
				}
			}
			this.Selection.Start = new Place(0, num);
			this.Selection.End = new Place(this.lines[num2].Count, num2);
			this.needRecalc = true;
			this.lines.Manager.EndAutoUndoCommands();
			this.Selection.EndUpdate();
			this.EndUpdate();
		}

		// Token: 0x060065A6 RID: 26022 RVA: 0x001ED18C File Offset: 0x001ED18C
		public void BeginAutoUndo()
		{
			this.lines.Manager.BeginAutoUndoCommands();
		}

		// Token: 0x060065A7 RID: 26023 RVA: 0x001ED1A0 File Offset: 0x001ED1A0
		public void EndAutoUndo()
		{
			this.lines.Manager.EndAutoUndoCommands();
		}

		// Token: 0x060065A8 RID: 26024 RVA: 0x001ED1B4 File Offset: 0x001ED1B4
		public virtual void OnVisualMarkerClick(MouseEventArgs args, StyleVisualMarker marker)
		{
			bool flag = this.VisualMarkerClick != null;
			if (flag)
			{
				this.VisualMarkerClick(this, new VisualMarkerEventArgs(marker.Style, marker, args));
			}
			marker.Style.OnVisualMarkerClick(this, new VisualMarkerEventArgs(marker.Style, marker, args));
		}

		// Token: 0x060065A9 RID: 26025 RVA: 0x001ED20C File Offset: 0x001ED20C
		protected virtual void OnMarkerClick(MouseEventArgs args, VisualMarker marker)
		{
			bool flag = marker is StyleVisualMarker;
			if (flag)
			{
				this.OnVisualMarkerClick(args, marker as StyleVisualMarker);
			}
			else
			{
				bool flag2 = marker is CollapseFoldingMarker;
				if (flag2)
				{
					this.CollapseFoldingBlock((marker as CollapseFoldingMarker).iLine);
				}
				else
				{
					bool flag3 = marker is ExpandFoldingMarker;
					if (flag3)
					{
						this.ExpandFoldedBlock((marker as ExpandFoldingMarker).iLine);
					}
					else
					{
						bool flag4 = marker is FoldedAreaMarker;
						if (flag4)
						{
							int iLine = (marker as FoldedAreaMarker).iLine;
							int num = this.FindEndOfFoldingBlock(iLine);
							bool flag5 = num < 0;
							if (!flag5)
							{
								this.Selection.BeginUpdate();
								this.Selection.Start = new Place(0, iLine);
								this.Selection.End = new Place(this.lines[num].Count, num);
								this.Selection.EndUpdate();
								this.Invalidate();
							}
						}
					}
				}
			}
		}

		// Token: 0x060065AA RID: 26026 RVA: 0x001ED32C File Offset: 0x001ED32C
		protected virtual void OnMarkerDoubleClick(VisualMarker marker)
		{
			bool flag = marker is FoldedAreaMarker;
			if (flag)
			{
				this.ExpandFoldedBlock((marker as FoldedAreaMarker).iLine);
				this.Invalidate();
			}
		}

		// Token: 0x060065AB RID: 26027 RVA: 0x001ED370 File Offset: 0x001ED370
		private void ClearBracketsPositions()
		{
			this.leftBracketPosition = null;
			this.rightBracketPosition = null;
			this.leftBracketPosition2 = null;
			this.rightBracketPosition2 = null;
		}

		// Token: 0x060065AC RID: 26028 RVA: 0x001ED390 File Offset: 0x001ED390
		private void HighlightBrackets(char LeftBracket, char RightBracket, ref Range leftBracketPosition, ref Range rightBracketPosition)
		{
			BracketsHighlightStrategy bracketsHighlightStrategy = this.BracketsHighlightStrategy;
			if (bracketsHighlightStrategy != BracketsHighlightStrategy.Strategy1)
			{
				if (bracketsHighlightStrategy == BracketsHighlightStrategy.Strategy2)
				{
					this.HighlightBrackets2(LeftBracket, RightBracket, ref leftBracketPosition, ref rightBracketPosition);
				}
			}
			else
			{
				this.HighlightBrackets1(LeftBracket, RightBracket, ref leftBracketPosition, ref rightBracketPosition);
			}
		}

		// Token: 0x060065AD RID: 26029 RVA: 0x001ED3E4 File Offset: 0x001ED3E4
		private void HighlightBrackets1(char LeftBracket, char RightBracket, ref Range leftBracketPosition, ref Range rightBracketPosition)
		{
			bool flag = !this.Selection.IsEmpty;
			if (!flag)
			{
				bool flag2 = this.LinesCount == 0;
				if (!flag2)
				{
					Range range = leftBracketPosition;
					Range range2 = rightBracketPosition;
					Range bracketsRange = this.GetBracketsRange(this.Selection.Start, LeftBracket, RightBracket, true);
					bool flag3 = bracketsRange != null;
					if (flag3)
					{
						leftBracketPosition = new Range(this, bracketsRange.Start, new Place(bracketsRange.Start.iChar + 1, bracketsRange.Start.iLine));
						rightBracketPosition = new Range(this, new Place(bracketsRange.End.iChar - 1, bracketsRange.End.iLine), bracketsRange.End);
					}
					bool flag4 = range != leftBracketPosition || range2 != rightBracketPosition;
					if (flag4)
					{
						this.Invalidate();
					}
				}
			}
		}

		// Token: 0x060065AE RID: 26030 RVA: 0x001ED4D8 File Offset: 0x001ED4D8
		public Range GetBracketsRange(Place placeInsideBrackets, char leftBracket, char rightBracket, bool includeBrackets)
		{
			Range range = new Range(this, placeInsideBrackets, placeInsideBrackets);
			Range range2 = range.Clone();
			Range range3 = null;
			Range range4 = null;
			int num = 0;
			int num2 = 1000;
			while (range2.GoLeftThroughFolded())
			{
				bool flag = range2.CharAfterStart == leftBracket;
				if (flag)
				{
					num++;
				}
				bool flag2 = range2.CharAfterStart == rightBracket;
				if (flag2)
				{
					num--;
				}
				bool flag3 = num == 1;
				if (flag3)
				{
					range2.Start = new Place(range2.Start.iChar + ((!includeBrackets) ? 1 : 0), range2.Start.iLine);
					range3 = range2;
					break;
				}
				num2--;
				bool flag4 = num2 <= 0;
				if (flag4)
				{
					break;
				}
			}
			range2 = range.Clone();
			num = 0;
			num2 = 1000;
			for (;;)
			{
				bool flag5 = range2.CharAfterStart == leftBracket;
				if (flag5)
				{
					num++;
				}
				bool flag6 = range2.CharAfterStart == rightBracket;
				if (flag6)
				{
					num--;
				}
				bool flag7 = num == -1;
				if (flag7)
				{
					break;
				}
				num2--;
				bool flag8 = num2 <= 0;
				if (flag8)
				{
					goto Block_10;
				}
				if (!range2.GoRightThroughFolded())
				{
					goto IL_17D;
				}
			}
			range2.End = new Place(range2.Start.iChar + (includeBrackets ? 1 : 0), range2.Start.iLine);
			range4 = range2;
			Block_10:
			IL_17D:
			bool flag9 = range3 != null && range4 != null;
			Range result;
			if (flag9)
			{
				result = new Range(this, range3.Start, range4.End);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060065AF RID: 26031 RVA: 0x001ED6A4 File Offset: 0x001ED6A4
		private void HighlightBrackets2(char LeftBracket, char RightBracket, ref Range leftBracketPosition, ref Range rightBracketPosition)
		{
			bool flag = !this.Selection.IsEmpty;
			if (!flag)
			{
				bool flag2 = this.LinesCount == 0;
				if (!flag2)
				{
					Range range = leftBracketPosition;
					Range range2 = rightBracketPosition;
					Range range3 = this.Selection.Clone();
					bool flag3 = false;
					int num = 0;
					int num2 = 1000;
					bool flag4 = range3.CharBeforeStart == RightBracket;
					if (flag4)
					{
						rightBracketPosition = new Range(this, range3.Start.iChar - 1, range3.Start.iLine, range3.Start.iChar, range3.Start.iLine);
						while (range3.GoLeftThroughFolded())
						{
							bool flag5 = range3.CharAfterStart == LeftBracket;
							if (flag5)
							{
								num++;
							}
							bool flag6 = range3.CharAfterStart == RightBracket;
							if (flag6)
							{
								num--;
							}
							bool flag7 = num == 0;
							if (flag7)
							{
								range3.End = new Place(range3.Start.iChar + 1, range3.Start.iLine);
								leftBracketPosition = range3;
								flag3 = true;
								break;
							}
							num2--;
							bool flag8 = num2 <= 0;
							if (flag8)
							{
								break;
							}
						}
					}
					range3 = this.Selection.Clone();
					num = 0;
					num2 = 1000;
					bool flag9 = !flag3;
					if (flag9)
					{
						bool flag10 = range3.CharAfterStart == LeftBracket;
						if (flag10)
						{
							leftBracketPosition = new Range(this, range3.Start.iChar, range3.Start.iLine, range3.Start.iChar + 1, range3.Start.iLine);
							for (;;)
							{
								bool flag11 = range3.CharAfterStart == LeftBracket;
								if (flag11)
								{
									num++;
								}
								bool flag12 = range3.CharAfterStart == RightBracket;
								if (flag12)
								{
									num--;
								}
								bool flag13 = num == 0;
								if (flag13)
								{
									break;
								}
								num2--;
								bool flag14 = num2 <= 0;
								if (flag14)
								{
									goto Block_13;
								}
								if (!range3.GoRightThroughFolded())
								{
									goto IL_25E;
								}
							}
							range3.End = new Place(range3.Start.iChar + 1, range3.Start.iLine);
							rightBracketPosition = range3;
							Block_13:
							IL_25E:;
						}
					}
					bool flag15 = range != leftBracketPosition || range2 != rightBracketPosition;
					if (flag15)
					{
						this.Invalidate();
					}
				}
			}
		}

		// Token: 0x060065B0 RID: 26032 RVA: 0x001ED93C File Offset: 0x001ED93C
		public bool SelectNext(string regexPattern, bool backward = false, RegexOptions options = RegexOptions.None)
		{
			Range range = this.Selection.Clone();
			range.Normalize();
			Range range2 = backward ? new Range(this, this.Range.Start, range.Start) : new Range(this, range.End, this.Range.End);
			Range range3 = null;
			foreach (Range range4 in range2.GetRanges(regexPattern, options))
			{
				range3 = range4;
				bool flag = !backward;
				if (flag)
				{
					break;
				}
			}
			bool flag2 = range3 == null;
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				this.Selection = range3;
				this.Invalidate();
				result = true;
			}
			return result;
		}

		// Token: 0x060065B1 RID: 26033 RVA: 0x001EDA28 File Offset: 0x001EDA28
		public virtual void OnSyntaxHighlight(TextChangedEventArgs args)
		{
			HighlightingRangeType highlightingRangeType = this.HighlightingRangeType;
			Range range;
			if (highlightingRangeType != HighlightingRangeType.VisibleRange)
			{
				if (highlightingRangeType != HighlightingRangeType.AllTextRange)
				{
					range = args.ChangedRange;
				}
				else
				{
					range = this.Range;
				}
			}
			else
			{
				range = this.VisibleRange.GetUnionWith(args.ChangedRange);
			}
			bool flag = this.SyntaxHighlighter != null;
			if (flag)
			{
				bool flag2 = this.Language == Language.Custom && !string.IsNullOrEmpty(this.DescriptionFile);
				if (flag2)
				{
					this.SyntaxHighlighter.HighlightSyntax(this.DescriptionFile, range);
				}
				else
				{
					this.SyntaxHighlighter.HighlightSyntax(this.Language, range);
				}
			}
		}

		// Token: 0x060065B2 RID: 26034 RVA: 0x001EDAEC File Offset: 0x001EDAEC
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.Name = "FastColoredTextBox";
			base.ResumeLayout(false);
		}

		// Token: 0x060065B3 RID: 26035 RVA: 0x001EDB0C File Offset: 0x001EDB0C
		public virtual void Print(Range range, PrintDialogSettings settings)
		{
			ExportToHTML exportToHTML = new ExportToHTML();
			exportToHTML.UseBr = true;
			exportToHTML.UseForwardNbsp = true;
			exportToHTML.UseNbsp = true;
			exportToHTML.UseStyleTag = false;
			exportToHTML.IncludeLineNumbers = settings.IncludeLineNumbers;
			bool flag = range == null;
			if (flag)
			{
				range = this.Range;
			}
			bool flag2 = range.Text == string.Empty;
			if (!flag2)
			{
				this.visibleRange = range;
				try
				{
					bool flag3 = this.VisibleRangeChanged != null;
					if (flag3)
					{
						this.VisibleRangeChanged(this, new EventArgs());
					}
					bool flag4 = this.VisibleRangeChangedDelayed != null;
					if (flag4)
					{
						this.VisibleRangeChangedDelayed(this, new EventArgs());
					}
				}
				finally
				{
					this.visibleRange = null;
				}
				string text = exportToHTML.GetHtml(range);
				text = string.Concat(new string[]
				{
					"<META HTTP-EQUIV=\"Content-Type\" CONTENT=\"text/html; charset=UTF-8\"><head><title>",
					this.PrepareHtmlText(settings.Title),
					"</title></head>",
					text,
					"<br>",
					this.SelectHTMLRangeScript()
				});
				string text2 = Path.GetTempPath() + "fctb.html";
				File.WriteAllText(text2, text);
				FastColoredTextBox.SetPageSetupSettings(settings);
				WebBrowser webBrowser = new WebBrowser();
				webBrowser.Tag = settings;
				webBrowser.Visible = false;
				webBrowser.Location = new Point(-1000, -1000);
				webBrowser.Parent = this;
				webBrowser.StatusTextChanged += this.wb_StatusTextChanged;
				webBrowser.Navigate(text2);
			}
		}

		// Token: 0x060065B4 RID: 26036 RVA: 0x001EDCB0 File Offset: 0x001EDCB0
		protected virtual string PrepareHtmlText(string s)
		{
			return s.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;");
		}

		// Token: 0x060065B5 RID: 26037 RVA: 0x001EDCF8 File Offset: 0x001EDCF8
		private void wb_StatusTextChanged(object sender, EventArgs e)
		{
			WebBrowser webBrowser = sender as WebBrowser;
			bool flag = webBrowser.StatusText.Contains("#print");
			if (flag)
			{
				PrintDialogSettings printDialogSettings = webBrowser.Tag as PrintDialogSettings;
				try
				{
					bool showPrintPreviewDialog = printDialogSettings.ShowPrintPreviewDialog;
					if (showPrintPreviewDialog)
					{
						webBrowser.ShowPrintPreviewDialog();
					}
					else
					{
						bool showPageSetupDialog = printDialogSettings.ShowPageSetupDialog;
						if (showPageSetupDialog)
						{
							webBrowser.ShowPageSetupDialog();
						}
						bool showPrintDialog = printDialogSettings.ShowPrintDialog;
						if (showPrintDialog)
						{
							webBrowser.ShowPrintDialog();
						}
						else
						{
							webBrowser.Print();
						}
					}
				}
				finally
				{
					webBrowser.Parent = null;
					webBrowser.Dispose();
				}
			}
		}

		// Token: 0x060065B6 RID: 26038 RVA: 0x001EDDB0 File Offset: 0x001EDDB0
		public void Print(PrintDialogSettings settings)
		{
			this.Print(this.Range, settings);
		}

		// Token: 0x060065B7 RID: 26039 RVA: 0x001EDDC4 File Offset: 0x001EDDC4
		public void Print()
		{
			this.Print(this.Range, new PrintDialogSettings
			{
				ShowPageSetupDialog = false,
				ShowPrintDialog = false,
				ShowPrintPreviewDialog = false
			});
		}

		// Token: 0x060065B8 RID: 26040 RVA: 0x001EDE00 File Offset: 0x001EDE00
		private string SelectHTMLRangeScript()
		{
			Range range = this.Selection.Clone();
			range.Normalize();
			int num = this.PlaceToPosition(range.Start) - range.Start.iLine;
			int num2 = range.Text.Length - (range.End.iLine - range.Start.iLine);
			return string.Format("<script type=\"text/javascript\">\r\ntry{{\r\n    var sel = document.selection;\r\n    var rng = sel.createRange();\r\n    rng.moveStart(\"character\", {0});\r\n    rng.moveEnd(\"character\", {1});\r\n    rng.select();\r\n}}catch(ex){{}}\r\nwindow.status = \"#print\";\r\n</script>", num, num2);
		}

		// Token: 0x060065B9 RID: 26041 RVA: 0x001EDE80 File Offset: 0x001EDE80
		private static void SetPageSetupSettings(PrintDialogSettings settings)
		{
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\PageSetup", true);
			bool flag = registryKey != null;
			if (flag)
			{
				registryKey.SetValue("footer", settings.Footer);
				registryKey.SetValue("header", settings.Header);
			}
		}

		// Token: 0x060065BA RID: 26042 RVA: 0x001EDED4 File Offset: 0x001EDED4
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				bool flag = this.SyntaxHighlighter != null;
				if (flag)
				{
					this.SyntaxHighlighter.Dispose();
				}
				this.timer.Dispose();
				this.timer2.Dispose();
				this.middleClickScrollingTimer.Dispose();
				bool flag2 = this.findForm != null;
				if (flag2)
				{
					this.findForm.Dispose();
				}
				bool flag3 = this.replaceForm != null;
				if (flag3)
				{
					this.replaceForm.Dispose();
				}
				bool flag4 = this.TextSource != null;
				if (flag4)
				{
					this.TextSource.Dispose();
				}
				bool flag5 = this.ToolTip != null;
				if (flag5)
				{
					this.ToolTip.Dispose();
				}
			}
		}

		// Token: 0x060065BB RID: 26043 RVA: 0x001EDFAC File Offset: 0x001EDFAC
		protected virtual void OnPaintLine(PaintLineEventArgs e)
		{
			bool flag = this.PaintLine != null;
			if (flag)
			{
				this.PaintLine(this, e);
			}
		}

		// Token: 0x060065BC RID: 26044 RVA: 0x001EDFDC File Offset: 0x001EDFDC
		internal void OnLineInserted(int index)
		{
			this.OnLineInserted(index, 1);
		}

		// Token: 0x060065BD RID: 26045 RVA: 0x001EDFE8 File Offset: 0x001EDFE8
		internal void OnLineInserted(int index, int count)
		{
			bool flag = this.LineInserted != null;
			if (flag)
			{
				this.LineInserted(this, new LineInsertedEventArgs(index, count));
			}
		}

		// Token: 0x060065BE RID: 26046 RVA: 0x001EE020 File Offset: 0x001EE020
		internal void OnLineRemoved(int index, int count, List<int> removedLineIds)
		{
			bool flag = count > 0;
			if (flag)
			{
				bool flag2 = this.LineRemoved != null;
				if (flag2)
				{
					this.LineRemoved(this, new LineRemovedEventArgs(index, count, removedLineIds));
				}
			}
		}

		// Token: 0x060065BF RID: 26047 RVA: 0x001EE064 File Offset: 0x001EE064
		public void OpenFile(string fileName, Encoding enc)
		{
			TextSource ts = this.CreateTextSource();
			try
			{
				this.InitTextSource(ts);
				this.Text = File.ReadAllText(fileName, enc);
				this.ClearUndo();
				this.IsChanged = false;
				this.OnVisibleRangeChanged();
			}
			catch
			{
				this.InitTextSource(this.CreateTextSource());
				this.lines.InsertLine(0, this.TextSource.CreateLine());
				this.IsChanged = false;
				throw;
			}
			this.Selection.Start = Place.Empty;
			this.DoSelectionVisible();
		}

		// Token: 0x060065C0 RID: 26048 RVA: 0x001EE108 File Offset: 0x001EE108
		public void OpenFile(string fileName)
		{
			try
			{
				Encoding encoding = EncodingDetector.DetectTextFileEncoding(fileName);
				bool flag = encoding != null;
				if (flag)
				{
					this.OpenFile(fileName, encoding);
				}
				else
				{
					this.OpenFile(fileName, Encoding.Default);
				}
			}
			catch
			{
				this.InitTextSource(this.CreateTextSource());
				this.lines.InsertLine(0, this.TextSource.CreateLine());
				this.IsChanged = false;
				throw;
			}
		}

		// Token: 0x060065C1 RID: 26049 RVA: 0x001EE18C File Offset: 0x001EE18C
		public void OpenBindingFile(string fileName, Encoding enc)
		{
			FileTextSource fileTextSource = new FileTextSource(this);
			try
			{
				this.InitTextSource(fileTextSource);
				fileTextSource.OpenFile(fileName, enc);
				this.IsChanged = false;
				this.OnVisibleRangeChanged();
			}
			catch
			{
				fileTextSource.CloseFile();
				this.InitTextSource(this.CreateTextSource());
				this.lines.InsertLine(0, this.TextSource.CreateLine());
				this.IsChanged = false;
				throw;
			}
			this.Invalidate();
		}

		// Token: 0x060065C2 RID: 26050 RVA: 0x001EE218 File Offset: 0x001EE218
		public void CloseBindingFile()
		{
			bool flag = this.lines is FileTextSource;
			if (flag)
			{
				FileTextSource fileTextSource = this.lines as FileTextSource;
				fileTextSource.CloseFile();
				this.InitTextSource(this.CreateTextSource());
				this.lines.InsertLine(0, this.TextSource.CreateLine());
				this.IsChanged = false;
				this.Invalidate();
			}
		}

		// Token: 0x060065C3 RID: 26051 RVA: 0x001EE288 File Offset: 0x001EE288
		public void SaveToFile(string fileName, Encoding enc)
		{
			this.lines.SaveToFile(fileName, enc);
			this.IsChanged = false;
			this.OnVisibleRangeChanged();
			this.UpdateScrollbars();
		}

		// Token: 0x060065C4 RID: 26052 RVA: 0x001EE2B0 File Offset: 0x001EE2B0
		public void SetVisibleState(int iLine, VisibleState state)
		{
			LineInfo value = this.LineInfos[iLine];
			value.VisibleState = state;
			this.LineInfos[iLine] = value;
			this.needRecalc = true;
		}

		// Token: 0x060065C5 RID: 26053 RVA: 0x001EE2EC File Offset: 0x001EE2EC
		public VisibleState GetVisibleState(int iLine)
		{
			return this.LineInfos[iLine].VisibleState;
		}

		// Token: 0x060065C6 RID: 26054 RVA: 0x001EE318 File Offset: 0x001EE318
		public void ShowGoToDialog()
		{
			GoToForm goToForm = new GoToForm();
			goToForm.TotalLineCount = this.LinesCount;
			goToForm.SelectedLineNumber = this.Selection.Start.iLine + 1;
			bool flag = goToForm.ShowDialog() == DialogResult.OK;
			if (flag)
			{
				int num = Math.Min(this.LinesCount - 1, Math.Max(0, goToForm.SelectedLineNumber - 1));
				this.Selection = new Range(this, 0, num, 0, num);
				this.DoSelectionVisible();
			}
		}

		// Token: 0x060065C7 RID: 26055 RVA: 0x001EE39C File Offset: 0x001EE39C
		public void OnUndoRedoStateChanged()
		{
			bool flag = this.UndoRedoStateChanged != null;
			if (flag)
			{
				this.UndoRedoStateChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x060065C8 RID: 26056 RVA: 0x001EE3D0 File Offset: 0x001EE3D0
		public List<int> FindLines(string searchPattern, RegexOptions options)
		{
			List<int> list = new List<int>();
			foreach (Range range in this.Range.GetRangesByLines(searchPattern, options))
			{
				list.Add(range.Start.iLine);
			}
			return list;
		}

		// Token: 0x060065C9 RID: 26057 RVA: 0x001EE44C File Offset: 0x001EE44C
		public void RemoveLines(List<int> iLines)
		{
			this.TextSource.Manager.ExecuteCommand(new RemoveLinesCommand(this.TextSource, iLines));
			bool flag = iLines.Count > 0;
			if (flag)
			{
				this.IsChanged = true;
			}
			bool flag2 = this.LinesCount == 0;
			if (flag2)
			{
				this.Text = "";
			}
			this.NeedRecalc();
			this.Invalidate();
		}

		// Token: 0x060065CA RID: 26058 RVA: 0x001EE4C0 File Offset: 0x001EE4C0
		void ISupportInitialize.BeginInit()
		{
		}

		// Token: 0x060065CB RID: 26059 RVA: 0x001EE4C4 File Offset: 0x001EE4C4
		void ISupportInitialize.EndInit()
		{
			this.OnTextChanged();
			this.Selection.Start = Place.Empty;
			this.DoCaretVisible();
			this.IsChanged = false;
			this.ClearUndo();
		}

		// Token: 0x17001576 RID: 5494
		// (get) Token: 0x060065CC RID: 26060 RVA: 0x001EE504 File Offset: 0x001EE504
		// (set) Token: 0x060065CD RID: 26061 RVA: 0x001EE50C File Offset: 0x001EE50C
		private bool IsDragDrop { get; set; }

		// Token: 0x060065CE RID: 26062 RVA: 0x001EE518 File Offset: 0x001EE518
		protected override void OnDragEnter(DragEventArgs e)
		{
			bool flag = e.Data.GetDataPresent(DataFormats.Text) && this.AllowDrop;
			if (flag)
			{
				e.Effect = DragDropEffects.Copy;
				this.IsDragDrop = true;
			}
			base.OnDragEnter(e);
		}

		// Token: 0x060065CF RID: 26063 RVA: 0x001EE56C File Offset: 0x001EE56C
		protected override void OnDragDrop(DragEventArgs e)
		{
			bool flag = this.ReadOnly || !this.AllowDrop;
			if (flag)
			{
				this.IsDragDrop = false;
			}
			else
			{
				bool dataPresent = e.Data.GetDataPresent(DataFormats.Text);
				if (dataPresent)
				{
					bool flag2 = base.ParentForm != null;
					if (flag2)
					{
						base.ParentForm.Activate();
					}
					base.Focus();
					Point point = base.PointToClient(new Point(e.X, e.Y));
					string text = e.Data.GetData(DataFormats.Text).ToString();
					Place place = this.PointToPlace(point);
					this.DoDragDrop(place, text);
					this.IsDragDrop = false;
				}
				base.OnDragDrop(e);
			}
		}

		// Token: 0x060065D0 RID: 26064 RVA: 0x001EE63C File Offset: 0x001EE63C
		private void DoDragDrop_old(Place place, string text)
		{
			Range range = new Range(this, place, place);
			bool readOnly = range.ReadOnly;
			if (!readOnly)
			{
				bool flag = this.draggedRange != null && this.draggedRange.Contains(place);
				if (!flag)
				{
					bool flag2 = this.draggedRange == null || this.draggedRange.ReadOnly || (Control.ModifierKeys & Keys.Control) > Keys.None;
					bool flag3 = this.draggedRange == null;
					if (flag3)
					{
						this.Selection.BeginUpdate();
						this.Selection.Start = place;
						this.InsertText(text);
						this.Selection = new Range(this, place, this.Selection.Start);
						this.Selection.EndUpdate();
					}
					else
					{
						this.BeginAutoUndo();
						this.Selection.BeginUpdate();
						this.Selection = this.draggedRange;
						this.lines.Manager.ExecuteCommand(new SelectCommand(this.lines));
						bool columnSelectionMode = this.draggedRange.ColumnSelectionMode;
						if (columnSelectionMode)
						{
							this.draggedRange.Normalize();
							range = new Range(this, place, new Place(place.iChar, place.iLine + this.draggedRange.End.iLine - this.draggedRange.Start.iLine))
							{
								ColumnSelectionMode = true
							};
							for (int i = this.LinesCount; i <= range.End.iLine; i++)
							{
								this.Selection.GoLast(false);
								this.InsertChar('\n');
							}
						}
						bool flag4 = !range.ReadOnly;
						if (flag4)
						{
							bool flag5 = place < this.draggedRange.Start;
							Place start;
							if (flag5)
							{
								bool flag6 = !flag2;
								if (flag6)
								{
									this.Selection = this.draggedRange;
									this.ClearSelected();
								}
								this.Selection = range;
								this.Selection.ColumnSelectionMode = range.ColumnSelectionMode;
								this.InsertText(text);
								start = this.Selection.Start;
							}
							else
							{
								this.Selection = range;
								this.Selection.ColumnSelectionMode = range.ColumnSelectionMode;
								this.InsertText(text);
								start = this.Selection.Start;
								int count = this[start.iLine].Count;
								bool flag7 = !flag2;
								if (flag7)
								{
									this.Selection = this.draggedRange;
									this.ClearSelected();
								}
								int num = count - this[start.iLine].Count;
								start.iChar -= num;
								place.iChar -= num;
							}
							bool flag8 = !this.draggedRange.ColumnSelectionMode;
							if (flag8)
							{
								this.Selection = new Range(this, place, start);
							}
							else
							{
								this.draggedRange.Normalize();
								this.Selection = new Range(this, place, new Place(place.iChar + this.draggedRange.End.iChar - this.draggedRange.Start.iChar, place.iLine + this.draggedRange.End.iLine - this.draggedRange.Start.iLine))
								{
									ColumnSelectionMode = true
								};
							}
						}
						this.Selection.EndUpdate();
						this.EndAutoUndo();
						this.draggedRange = null;
					}
				}
			}
		}

		// Token: 0x060065D1 RID: 26065 RVA: 0x001EE9EC File Offset: 0x001EE9EC
		protected virtual void DoDragDrop(Place place, string text)
		{
			Range range = new Range(this, place, place);
			bool readOnly = range.ReadOnly;
			if (!readOnly)
			{
				bool flag = this.draggedRange != null && this.draggedRange.Contains(place);
				if (!flag)
				{
					bool flag2 = this.draggedRange == null || this.draggedRange.ReadOnly || (Control.ModifierKeys & Keys.Control) > Keys.None;
					bool flag3 = this.draggedRange == null;
					if (flag3)
					{
						this.Selection.BeginUpdate();
						this.Selection.Start = place;
						this.InsertText(text);
						this.Selection = new Range(this, place, this.Selection.Start);
						this.Selection.EndUpdate();
					}
					else
					{
						bool flag4 = !this.draggedRange.Contains(place);
						if (flag4)
						{
							this.BeginAutoUndo();
							this.Selection = this.draggedRange;
							this.lines.Manager.ExecuteCommand(new SelectCommand(this.lines));
							bool columnSelectionMode = this.draggedRange.ColumnSelectionMode;
							if (columnSelectionMode)
							{
								this.draggedRange.Normalize();
								range = new Range(this, place, new Place(place.iChar, place.iLine + this.draggedRange.End.iLine - this.draggedRange.Start.iLine))
								{
									ColumnSelectionMode = true
								};
								for (int i = this.LinesCount; i <= range.End.iLine; i++)
								{
									this.Selection.GoLast(false);
									this.InsertChar('\n');
								}
							}
							bool flag5 = !range.ReadOnly;
							if (flag5)
							{
								bool flag6 = place < this.draggedRange.Start;
								if (flag6)
								{
									bool flag7 = !flag2;
									if (flag7)
									{
										this.Selection = this.draggedRange;
										this.ClearSelected();
									}
									this.Selection = range;
									this.Selection.ColumnSelectionMode = range.ColumnSelectionMode;
									this.InsertText(text);
								}
								else
								{
									this.Selection = range;
									this.Selection.ColumnSelectionMode = range.ColumnSelectionMode;
									this.InsertText(text);
									bool flag8 = !flag2;
									if (flag8)
									{
										this.Selection = this.draggedRange;
										this.ClearSelected();
									}
								}
							}
							Place start = place;
							Place start2 = this.Selection.Start;
							Range range2 = (this.draggedRange.End > this.draggedRange.Start) ? this.GetRange(this.draggedRange.Start, this.draggedRange.End) : this.GetRange(this.draggedRange.End, this.draggedRange.Start);
							bool flag9 = place > this.draggedRange.Start && !flag2;
							if (flag9)
							{
								bool flag10 = !this.draggedRange.ColumnSelectionMode;
								if (flag10)
								{
									bool flag11 = range2.Start.iLine != range2.End.iLine;
									int iChar;
									int iChar2;
									if (flag11)
									{
										iChar = ((range2.End.iLine != place.iLine) ? place.iChar : (range2.Start.iChar + (place.iChar - range2.End.iChar)));
										iChar2 = range2.End.iChar;
									}
									else
									{
										bool flag12 = range2.End.iLine == place.iLine;
										if (flag12)
										{
											iChar = place.iChar - range2.Text.Length;
											iChar2 = place.iChar;
										}
										else
										{
											iChar = place.iChar;
											iChar2 = place.iChar + range2.Text.Length;
										}
									}
									bool flag13 = range2.End.iLine != place.iLine;
									int iLine;
									int iLine2;
									if (flag13)
									{
										iLine = place.iLine - (range2.End.iLine - range2.Start.iLine);
										iLine2 = place.iLine;
									}
									else
									{
										iLine = range2.Start.iLine;
										iLine2 = range2.End.iLine;
									}
									start = new Place(iChar, iLine);
									start2 = new Place(iChar2, iLine2);
								}
							}
							bool flag14 = !this.draggedRange.ColumnSelectionMode;
							if (flag14)
							{
								this.Selection = new Range(this, start, start2);
							}
							else
							{
								bool flag15 = !flag2 && place.iLine >= range2.Start.iLine && place.iLine <= range2.End.iLine && place.iChar >= range2.End.iChar;
								int iChar;
								int iChar2;
								if (flag15)
								{
									iChar = place.iChar - (range2.End.iChar - range2.Start.iChar);
									iChar2 = place.iChar;
								}
								else
								{
									iChar = place.iChar;
									iChar2 = place.iChar + (range2.End.iChar - range2.Start.iChar);
								}
								int iLine = place.iLine;
								int iLine2 = place.iLine + (range2.End.iLine - range2.Start.iLine);
								start = new Place(iChar, iLine);
								start2 = new Place(iChar2, iLine2);
								this.Selection = new Range(this, start, start2)
								{
									ColumnSelectionMode = true
								};
							}
							this.EndAutoUndo();
						}
						this.selection.Inverse();
						this.OnSelectionChanged();
					}
					this.draggedRange = null;
				}
			}
		}

		// Token: 0x060065D2 RID: 26066 RVA: 0x001EF014 File Offset: 0x001EF014
		protected override void OnDragOver(DragEventArgs e)
		{
			bool dataPresent = e.Data.GetDataPresent(DataFormats.Text);
			if (dataPresent)
			{
				Point point = base.PointToClient(new Point(e.X, e.Y));
				this.Selection.Start = this.PointToPlace(point);
				bool flag = point.Y < 6 && base.VerticalScroll.Visible && base.VerticalScroll.Value > 0;
				if (flag)
				{
					base.VerticalScroll.Value = Math.Max(0, base.VerticalScroll.Value - this.charHeight);
				}
				this.DoCaretVisible();
				this.Invalidate();
			}
			base.OnDragOver(e);
		}

		// Token: 0x060065D3 RID: 26067 RVA: 0x001EF0DC File Offset: 0x001EF0DC
		protected override void OnDragLeave(EventArgs e)
		{
			this.IsDragDrop = false;
			base.OnDragLeave(e);
		}

		// Token: 0x060065D4 RID: 26068 RVA: 0x001EF0F0 File Offset: 0x001EF0F0
		private void ActivateMiddleClickScrollingMode(MouseEventArgs e)
		{
			bool flag = !this.middleClickScrollingActivated;
			if (flag)
			{
				bool flag2 = !base.HorizontalScroll.Visible && !base.VerticalScroll.Visible;
				if (flag2)
				{
					bool showScrollBars = this.ShowScrollBars;
					if (showScrollBars)
					{
						return;
					}
				}
				this.middleClickScrollingActivated = true;
				this.middleClickScrollingOriginPoint = e.Location;
				this.middleClickScrollingOriginScroll = new Point(base.HorizontalScroll.Value, base.VerticalScroll.Value);
				this.middleClickScrollingTimer.Interval = 50;
				this.middleClickScrollingTimer.Enabled = true;
				base.Capture = true;
				this.Refresh();
				FastColoredTextBox.SendMessage(base.Handle, 11, 0, 0);
			}
		}

		// Token: 0x060065D5 RID: 26069 RVA: 0x001EF1C0 File Offset: 0x001EF1C0
		private void DeactivateMiddleClickScrollingMode()
		{
			bool flag = this.middleClickScrollingActivated;
			if (flag)
			{
				this.middleClickScrollingActivated = false;
				this.middleClickScrollingTimer.Enabled = false;
				base.Capture = false;
				base.Cursor = this.defaultCursor;
				FastColoredTextBox.SendMessage(base.Handle, 11, 1, 0);
				this.Invalidate();
			}
		}

		// Token: 0x060065D6 RID: 26070 RVA: 0x001EF224 File Offset: 0x001EF224
		private void RestoreScrollsAfterMiddleClickScrollingMode()
		{
			ScrollEventArgs se = new ScrollEventArgs(ScrollEventType.ThumbPosition, base.HorizontalScroll.Value, this.middleClickScrollingOriginScroll.X, ScrollOrientation.HorizontalScroll);
			this.OnScroll(se);
			ScrollEventArgs se2 = new ScrollEventArgs(ScrollEventType.ThumbPosition, base.VerticalScroll.Value, this.middleClickScrollingOriginScroll.Y, ScrollOrientation.VerticalScroll);
			this.OnScroll(se2);
		}

		// Token: 0x060065D7 RID: 26071
		[DllImport("user32.dll")]
		private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

		// Token: 0x060065D8 RID: 26072 RVA: 0x001EF284 File Offset: 0x001EF284
		private void middleClickScrollingTimer_Tick(object sender, EventArgs e)
		{
			bool isDisposed = base.IsDisposed;
			if (!isDisposed)
			{
				bool flag = !this.middleClickScrollingActivated;
				if (!flag)
				{
					Point point = base.PointToClient(Cursor.Position);
					base.Capture = true;
					int num = this.middleClickScrollingOriginPoint.X - point.X;
					int num2 = this.middleClickScrollingOriginPoint.Y - point.Y;
					bool flag2 = !base.VerticalScroll.Visible && this.ShowScrollBars;
					if (flag2)
					{
						num2 = 0;
					}
					bool flag3 = !base.HorizontalScroll.Visible && this.ShowScrollBars;
					if (flag3)
					{
						num = 0;
					}
					double num3 = 180.0 - Math.Atan2((double)num2, (double)num) * 180.0 / 3.141592653589793;
					double num4 = Math.Sqrt(Math.Pow((double)num, 2.0) + Math.Pow((double)num2, 2.0));
					bool flag4 = num4 > 10.0;
					if (flag4)
					{
						bool flag5 = num3 >= 325.0 || num3 <= 35.0;
						if (flag5)
						{
							this.middleClickScollDirection = ScrollDirection.Right;
						}
						else
						{
							bool flag6 = num3 <= 55.0;
							if (flag6)
							{
								this.middleClickScollDirection = (ScrollDirection.Right | ScrollDirection.Up);
							}
							else
							{
								bool flag7 = num3 <= 125.0;
								if (flag7)
								{
									this.middleClickScollDirection = ScrollDirection.Up;
								}
								else
								{
									bool flag8 = num3 <= 145.0;
									if (flag8)
									{
										this.middleClickScollDirection = (ScrollDirection.Left | ScrollDirection.Up);
									}
									else
									{
										bool flag9 = num3 <= 215.0;
										if (flag9)
										{
											this.middleClickScollDirection = ScrollDirection.Left;
										}
										else
										{
											bool flag10 = num3 <= 235.0;
											if (flag10)
											{
												this.middleClickScollDirection = (ScrollDirection.Left | ScrollDirection.Down);
											}
											else
											{
												bool flag11 = num3 <= 305.0;
												if (flag11)
												{
													this.middleClickScollDirection = ScrollDirection.Down;
												}
												else
												{
													this.middleClickScollDirection = (ScrollDirection.Right | ScrollDirection.Down);
												}
											}
										}
									}
								}
							}
						}
					}
					else
					{
						this.middleClickScollDirection = ScrollDirection.None;
					}
					switch (this.middleClickScollDirection)
					{
					case ScrollDirection.Left:
						base.Cursor = Cursors.PanWest;
						goto IL_327;
					case ScrollDirection.Right:
						base.Cursor = Cursors.PanEast;
						goto IL_327;
					case ScrollDirection.Up:
						base.Cursor = Cursors.PanNorth;
						goto IL_327;
					case ScrollDirection.Left | ScrollDirection.Up:
						base.Cursor = Cursors.PanNW;
						goto IL_327;
					case ScrollDirection.Right | ScrollDirection.Up:
						base.Cursor = Cursors.PanNE;
						goto IL_327;
					case ScrollDirection.Down:
						base.Cursor = Cursors.PanSouth;
						goto IL_327;
					case ScrollDirection.Left | ScrollDirection.Down:
						base.Cursor = Cursors.PanSW;
						goto IL_327;
					case ScrollDirection.Right | ScrollDirection.Down:
						base.Cursor = Cursors.PanSE;
						goto IL_327;
					}
					base.Cursor = this.defaultCursor;
					return;
					IL_327:
					int num5 = (int)((double)(-(double)num) / 5.0);
					int num6 = (int)((double)(-(double)num2) / 5.0);
					ScrollEventArgs se = new ScrollEventArgs((num5 < 0) ? ScrollEventType.SmallIncrement : ScrollEventType.SmallDecrement, base.HorizontalScroll.Value, base.HorizontalScroll.Value + num5, ScrollOrientation.HorizontalScroll);
					ScrollEventArgs se2 = new ScrollEventArgs((num6 < 0) ? ScrollEventType.SmallDecrement : ScrollEventType.SmallIncrement, base.VerticalScroll.Value, base.VerticalScroll.Value + num6, ScrollOrientation.VerticalScroll);
					bool flag12 = (this.middleClickScollDirection & (ScrollDirection.Up | ScrollDirection.Down)) > ScrollDirection.None;
					if (flag12)
					{
						this.OnScroll(se2, false);
					}
					bool flag13 = (this.middleClickScollDirection & (ScrollDirection.Left | ScrollDirection.Right)) > ScrollDirection.None;
					if (flag13)
					{
						this.OnScroll(se);
					}
					FastColoredTextBox.SendMessage(base.Handle, 11, 1, 0);
					this.Refresh();
					FastColoredTextBox.SendMessage(base.Handle, 11, 0, 0);
				}
			}
		}

		// Token: 0x060065D9 RID: 26073 RVA: 0x001EF6A0 File Offset: 0x001EF6A0
		private void DrawMiddleClickScrolling(Graphics gr)
		{
			bool flag = base.VerticalScroll.Visible || !this.ShowScrollBars;
			bool flag2 = base.HorizontalScroll.Visible || !this.ShowScrollBars;
			Color color = Color.FromArgb(100, (int)(~this.BackColor.R), (int)(~this.BackColor.G), (int)(~this.BackColor.B));
			using (SolidBrush solidBrush = new SolidBrush(color))
			{
				Point point = this.middleClickScrollingOriginPoint;
				GraphicsState gstate = gr.Save();
				gr.SmoothingMode = SmoothingMode.HighQuality;
				gr.TranslateTransform((float)point.X, (float)point.Y);
				gr.FillEllipse(solidBrush, -2, -2, 4, 4);
				bool flag3 = flag;
				if (flag3)
				{
					this.DrawTriangle(gr, solidBrush);
				}
				gr.RotateTransform(90f);
				bool flag4 = flag2;
				if (flag4)
				{
					this.DrawTriangle(gr, solidBrush);
				}
				gr.RotateTransform(90f);
				bool flag5 = flag;
				if (flag5)
				{
					this.DrawTriangle(gr, solidBrush);
				}
				gr.RotateTransform(90f);
				bool flag6 = flag2;
				if (flag6)
				{
					this.DrawTriangle(gr, solidBrush);
				}
				gr.Restore(gstate);
			}
		}

		// Token: 0x060065DA RID: 26074 RVA: 0x001EF810 File Offset: 0x001EF810
		private void DrawTriangle(Graphics g, Brush brush)
		{
			Point[] points = new Point[]
			{
				new Point(5, 10),
				new Point(0, 15),
				new Point(-5, 10)
			};
			g.FillPolygon(brush, points);
		}

		// Token: 0x0400336A RID: 13162
		internal const int minLeftIndent = 8;

		// Token: 0x0400336B RID: 13163
		private const int maxBracketSearchIterations = 1000;

		// Token: 0x0400336C RID: 13164
		private const int maxLinesForFolding = 3000;

		// Token: 0x0400336D RID: 13165
		private const int minLinesForAccuracy = 100000;

		// Token: 0x0400336E RID: 13166
		private const int WM_IME_SETCONTEXT = 641;

		// Token: 0x0400336F RID: 13167
		private const int WM_HSCROLL = 276;

		// Token: 0x04003370 RID: 13168
		private const int WM_VSCROLL = 277;

		// Token: 0x04003371 RID: 13169
		private const int SB_ENDSCROLL = 8;

		// Token: 0x04003372 RID: 13170
		public readonly List<LineInfo> LineInfos = new List<LineInfo>();

		// Token: 0x04003373 RID: 13171
		private readonly Range selection;

		// Token: 0x04003374 RID: 13172
		private readonly System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

		// Token: 0x04003375 RID: 13173
		private readonly System.Windows.Forms.Timer timer2 = new System.Windows.Forms.Timer();

		// Token: 0x04003376 RID: 13174
		private readonly System.Windows.Forms.Timer timer3 = new System.Windows.Forms.Timer();

		// Token: 0x04003377 RID: 13175
		private readonly List<VisualMarker> visibleMarkers = new List<VisualMarker>();

		// Token: 0x04003378 RID: 13176
		public int TextHeight;

		// Token: 0x04003379 RID: 13177
		public bool AllowInsertRemoveLines = true;

		// Token: 0x0400337A RID: 13178
		private Brush backBrush;

		// Token: 0x0400337B RID: 13179
		private BaseBookmarks bookmarks;

		// Token: 0x0400337C RID: 13180
		private bool caretVisible;

		// Token: 0x0400337D RID: 13181
		private Color changedLineColor;

		// Token: 0x0400337E RID: 13182
		private int charHeight;

		// Token: 0x0400337F RID: 13183
		private Color currentLineColor;

		// Token: 0x04003380 RID: 13184
		private Cursor defaultCursor;

		// Token: 0x04003381 RID: 13185
		private Range delayedTextChangedRange;

		// Token: 0x04003382 RID: 13186
		private string descriptionFile;

		// Token: 0x04003383 RID: 13187
		private int endFoldingLine = -1;

		// Token: 0x04003384 RID: 13188
		private Color foldingIndicatorColor;

		// Token: 0x04003385 RID: 13189
		protected Dictionary<int, int> foldingPairs = new Dictionary<int, int>();

		// Token: 0x04003386 RID: 13190
		private bool handledChar;

		// Token: 0x04003387 RID: 13191
		private bool highlightFoldingIndicator;

		// Token: 0x04003388 RID: 13192
		private Hints hints;

		// Token: 0x04003389 RID: 13193
		private Color indentBackColor;

		// Token: 0x0400338A RID: 13194
		private bool isChanged;

		// Token: 0x0400338B RID: 13195
		private bool isLineSelect;

		// Token: 0x0400338C RID: 13196
		private bool isReplaceMode;

		// Token: 0x0400338D RID: 13197
		private Language language;

		// Token: 0x0400338E RID: 13198
		private Keys lastModifiers;

		// Token: 0x0400338F RID: 13199
		private Point lastMouseCoord;

		// Token: 0x04003390 RID: 13200
		private DateTime lastNavigatedDateTime;

		// Token: 0x04003391 RID: 13201
		private Range leftBracketPosition;

		// Token: 0x04003392 RID: 13202
		private Range leftBracketPosition2;

		// Token: 0x04003393 RID: 13203
		private int leftPadding;

		// Token: 0x04003394 RID: 13204
		private int lineInterval;

		// Token: 0x04003395 RID: 13205
		private Color lineNumberColor;

		// Token: 0x04003396 RID: 13206
		private uint lineNumberStartValue;

		// Token: 0x04003397 RID: 13207
		private int lineSelectFrom;

		// Token: 0x04003398 RID: 13208
		private TextSource lines;

		// Token: 0x04003399 RID: 13209
		private IntPtr m_hImc;

		// Token: 0x0400339A RID: 13210
		private int maxLineLength;

		// Token: 0x0400339B RID: 13211
		private bool mouseIsDrag;

		// Token: 0x0400339C RID: 13212
		private bool mouseIsDragDrop;

		// Token: 0x0400339D RID: 13213
		private bool multiline;

		// Token: 0x0400339E RID: 13214
		protected bool needRecalc;

		// Token: 0x0400339F RID: 13215
		protected bool needRecalcWordWrap;

		// Token: 0x040033A0 RID: 13216
		private Point needRecalcWordWrapInterval;

		// Token: 0x040033A1 RID: 13217
		private bool needRecalcFoldingLines;

		// Token: 0x040033A2 RID: 13218
		private bool needRiseSelectionChangedDelayed;

		// Token: 0x040033A3 RID: 13219
		private bool needRiseTextChangedDelayed;

		// Token: 0x040033A4 RID: 13220
		private bool needRiseVisibleRangeChangedDelayed;

		// Token: 0x040033A5 RID: 13221
		private Color paddingBackColor;

		// Token: 0x040033A6 RID: 13222
		private int preferredLineWidth;

		// Token: 0x040033A7 RID: 13223
		private Range rightBracketPosition;

		// Token: 0x040033A8 RID: 13224
		private Range rightBracketPosition2;

		// Token: 0x040033A9 RID: 13225
		private bool scrollBars;

		// Token: 0x040033AA RID: 13226
		private Color selectionColor;

		// Token: 0x040033AB RID: 13227
		private Color serviceLinesColor;

		// Token: 0x040033AC RID: 13228
		private bool showFoldingLines;

		// Token: 0x040033AD RID: 13229
		private bool showLineNumbers;

		// Token: 0x040033AE RID: 13230
		private FastColoredTextBox sourceTextBox;

		// Token: 0x040033AF RID: 13231
		private int startFoldingLine = -1;

		// Token: 0x040033B0 RID: 13232
		private int updating;

		// Token: 0x040033B1 RID: 13233
		private Range updatingRange;

		// Token: 0x040033B2 RID: 13234
		private Range visibleRange;

		// Token: 0x040033B3 RID: 13235
		private bool wordWrap;

		// Token: 0x040033B4 RID: 13236
		private WordWrapMode wordWrapMode = WordWrapMode.WordWrapControlWidth;

		// Token: 0x040033B5 RID: 13237
		private int reservedCountOfLineNumberChars = 1;

		// Token: 0x040033B6 RID: 13238
		private int zoom = 100;

		// Token: 0x040033B7 RID: 13239
		private Size localAutoScrollMinSize;

		// Token: 0x040033B8 RID: 13240
		private char[] autoCompleteBracketsList = new char[]
		{
			'(',
			')',
			'{',
			'}',
			'[',
			']',
			'"',
			'"',
			'\'',
			'\''
		};

		// Token: 0x040033BF RID: 13247
		private MacrosManager macrosManager;

		// Token: 0x040033C8 RID: 13256
		private Color textAreaBorderColor;

		// Token: 0x040033C9 RID: 13257
		private TextAreaBorderType textAreaBorder;

		// Token: 0x040033E2 RID: 13282
		private bool selectionHighlightingForLineBreaksEnabled;

		// Token: 0x040033E5 RID: 13285
		private Font baseFont;

		// Token: 0x040033FE RID: 13310
		private Dictionary<System.Windows.Forms.Timer, System.Windows.Forms.Timer> timersToReset = new Dictionary<System.Windows.Forms.Timer, System.Windows.Forms.Timer>();

		// Token: 0x040033FF RID: 13311
		private List<Control> tempHintsList = new List<Control>();

		// Token: 0x04003400 RID: 13312
		private bool findCharMode;

		// Token: 0x04003401 RID: 13313
		private static Dictionary<FCTBAction, bool> scrollActions = new Dictionary<FCTBAction, bool>
		{
			{
				FCTBAction.ScrollDown,
				true
			},
			{
				FCTBAction.ScrollUp,
				true
			},
			{
				FCTBAction.ZoomOut,
				true
			},
			{
				FCTBAction.ZoomIn,
				true
			},
			{
				FCTBAction.ZoomNormal,
				true
			}
		};

		// Token: 0x04003402 RID: 13314
		private Font originalFont;

		// Token: 0x04003403 RID: 13315
		private const int WM_CHAR = 258;

		// Token: 0x04003406 RID: 13318
		private Rectangle prevCaretRect;

		// Token: 0x04003407 RID: 13319
		protected Range draggedRange;

		// Token: 0x04003409 RID: 13321
		private bool middleClickScrollingActivated;

		// Token: 0x0400340A RID: 13322
		private Point middleClickScrollingOriginPoint;

		// Token: 0x0400340B RID: 13323
		private Point middleClickScrollingOriginScroll;

		// Token: 0x0400340C RID: 13324
		private readonly System.Windows.Forms.Timer middleClickScrollingTimer = new System.Windows.Forms.Timer();

		// Token: 0x0400340D RID: 13325
		private ScrollDirection middleClickScollDirection = ScrollDirection.None;

		// Token: 0x0400340E RID: 13326
		private const int WM_SETREDRAW = 11;

		// Token: 0x02001052 RID: 4178
		private class LineYComparer : IComparer<LineInfo>
		{
			// Token: 0x06008FEA RID: 36842 RVA: 0x002ADC94 File Offset: 0x002ADC94
			public LineYComparer(int Y)
			{
				this.Y = Y;
			}

			// Token: 0x06008FEB RID: 36843 RVA: 0x002ADCA8 File Offset: 0x002ADCA8
			public int Compare(LineInfo x, LineInfo y)
			{
				bool flag = x.startY == -10;
				int result;
				if (flag)
				{
					result = -y.startY.CompareTo(this.Y);
				}
				else
				{
					result = x.startY.CompareTo(this.Y);
				}
				return result;
			}

			// Token: 0x0400455C RID: 17756
			private readonly int Y;
		}
	}
}
