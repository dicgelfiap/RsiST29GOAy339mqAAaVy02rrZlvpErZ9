using System;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D3A RID: 3386
	[ComVisible(true)]
	public abstract class User32
	{
		// Token: 0x0600899C RID: 35228
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern IntPtr GetDC(IntPtr hWnd);

		// Token: 0x0600899D RID: 35229
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

		// Token: 0x020011EE RID: 4590
		internal struct ICONINFO
		{
			// Token: 0x04004D0A RID: 19722
			public bool IsIcon;

			// Token: 0x04004D0B RID: 19723
			public int xHotspot;

			// Token: 0x04004D0C RID: 19724
			public int yHotspot;

			// Token: 0x04004D0D RID: 19725
			public IntPtr MaskBitmap;

			// Token: 0x04004D0E RID: 19726
			public IntPtr ColorBitmap;
		}

		// Token: 0x020011EF RID: 4591
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		public struct DIALOGTEMPLATE
		{
			// Token: 0x04004D0F RID: 19727
			public uint style;

			// Token: 0x04004D10 RID: 19728
			public uint dwExtendedStyle;

			// Token: 0x04004D11 RID: 19729
			public ushort cdit;

			// Token: 0x04004D12 RID: 19730
			public short x;

			// Token: 0x04004D13 RID: 19731
			public short y;

			// Token: 0x04004D14 RID: 19732
			public short cx;

			// Token: 0x04004D15 RID: 19733
			public short cy;
		}

		// Token: 0x020011F0 RID: 4592
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		public struct DIALOGITEMTEMPLATE
		{
			// Token: 0x04004D16 RID: 19734
			public uint style;

			// Token: 0x04004D17 RID: 19735
			public uint dwExtendedStyle;

			// Token: 0x04004D18 RID: 19736
			public short x;

			// Token: 0x04004D19 RID: 19737
			public short y;

			// Token: 0x04004D1A RID: 19738
			public short cx;

			// Token: 0x04004D1B RID: 19739
			public short cy;

			// Token: 0x04004D1C RID: 19740
			public short id;
		}

		// Token: 0x020011F1 RID: 4593
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		public struct DIALOGEXTEMPLATE
		{
			// Token: 0x04004D1D RID: 19741
			public ushort dlgVer;

			// Token: 0x04004D1E RID: 19742
			public ushort signature;

			// Token: 0x04004D1F RID: 19743
			public uint helpID;

			// Token: 0x04004D20 RID: 19744
			public uint exStyle;

			// Token: 0x04004D21 RID: 19745
			public uint style;

			// Token: 0x04004D22 RID: 19746
			public ushort cDlgItems;

			// Token: 0x04004D23 RID: 19747
			public short x;

			// Token: 0x04004D24 RID: 19748
			public short y;

			// Token: 0x04004D25 RID: 19749
			public short cx;

			// Token: 0x04004D26 RID: 19750
			public short cy;
		}

		// Token: 0x020011F2 RID: 4594
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		public struct DIALOGEXITEMTEMPLATE
		{
			// Token: 0x04004D27 RID: 19751
			public uint helpID;

			// Token: 0x04004D28 RID: 19752
			public uint exStyle;

			// Token: 0x04004D29 RID: 19753
			public uint style;

			// Token: 0x04004D2A RID: 19754
			public short x;

			// Token: 0x04004D2B RID: 19755
			public short y;

			// Token: 0x04004D2C RID: 19756
			public short cx;

			// Token: 0x04004D2D RID: 19757
			public short cy;

			// Token: 0x04004D2E RID: 19758
			public int id;
		}

		// Token: 0x020011F3 RID: 4595
		public enum WindowStyles : uint
		{
			// Token: 0x04004D30 RID: 19760
			WS_OVERLAPPED,
			// Token: 0x04004D31 RID: 19761
			WS_POPUP = 2147483648U,
			// Token: 0x04004D32 RID: 19762
			WS_CHILD = 1073741824U,
			// Token: 0x04004D33 RID: 19763
			WS_MINIMIZE = 536870912U,
			// Token: 0x04004D34 RID: 19764
			WS_VISIBLE = 268435456U,
			// Token: 0x04004D35 RID: 19765
			WS_DISABLED = 134217728U,
			// Token: 0x04004D36 RID: 19766
			WS_CLIPSIBLINGS = 67108864U,
			// Token: 0x04004D37 RID: 19767
			WS_CLIPCHILDREN = 33554432U,
			// Token: 0x04004D38 RID: 19768
			WS_MAXIMIZE = 16777216U,
			// Token: 0x04004D39 RID: 19769
			WS_CAPTION = 12582912U,
			// Token: 0x04004D3A RID: 19770
			WS_BORDER = 8388608U,
			// Token: 0x04004D3B RID: 19771
			WS_DLGFRAME = 4194304U,
			// Token: 0x04004D3C RID: 19772
			WS_VSCROLL = 2097152U,
			// Token: 0x04004D3D RID: 19773
			WS_HSCROLL = 1048576U,
			// Token: 0x04004D3E RID: 19774
			WS_SYSMENU = 524288U,
			// Token: 0x04004D3F RID: 19775
			WS_THICKFRAME = 262144U,
			// Token: 0x04004D40 RID: 19776
			WS_GROUP = 131072U,
			// Token: 0x04004D41 RID: 19777
			WS_TABSTOP = 65536U
		}

		// Token: 0x020011F4 RID: 4596
		public enum DialogStyles : uint
		{
			// Token: 0x04004D43 RID: 19779
			DS_ABSALIGN = 1U,
			// Token: 0x04004D44 RID: 19780
			DS_SYSMODAL,
			// Token: 0x04004D45 RID: 19781
			DS_LOCALEDIT = 32U,
			// Token: 0x04004D46 RID: 19782
			DS_SETFONT = 64U,
			// Token: 0x04004D47 RID: 19783
			DS_MODALFRAME = 128U,
			// Token: 0x04004D48 RID: 19784
			DS_NOIDLEMSG = 256U,
			// Token: 0x04004D49 RID: 19785
			DS_SETFOREGROUND = 512U,
			// Token: 0x04004D4A RID: 19786
			DS_3DLOOK = 4U,
			// Token: 0x04004D4B RID: 19787
			DS_FIXEDSYS = 8U,
			// Token: 0x04004D4C RID: 19788
			DS_NOFAILCREATE = 16U,
			// Token: 0x04004D4D RID: 19789
			DS_CONTROL = 1024U,
			// Token: 0x04004D4E RID: 19790
			DS_CENTER = 2048U,
			// Token: 0x04004D4F RID: 19791
			DS_CENTERMOUSE = 4096U,
			// Token: 0x04004D50 RID: 19792
			DS_CONTEXTHELP = 8192U,
			// Token: 0x04004D51 RID: 19793
			DS_SHELLFONT = 72U,
			// Token: 0x04004D52 RID: 19794
			DS_USEPIXELS = 32768U
		}

		// Token: 0x020011F5 RID: 4597
		public enum ExtendedDialogStyles : uint
		{
			// Token: 0x04004D54 RID: 19796
			WS_EX_DLGMODALFRAME = 1U,
			// Token: 0x04004D55 RID: 19797
			WS_EX_NOPARENTNOTIFY = 4U,
			// Token: 0x04004D56 RID: 19798
			WS_EX_TOPMOST = 8U,
			// Token: 0x04004D57 RID: 19799
			WS_EX_ACCEPTFILES = 16U,
			// Token: 0x04004D58 RID: 19800
			WS_EX_TRANSPARENT = 32U,
			// Token: 0x04004D59 RID: 19801
			WS_EX_MDICHILD = 64U,
			// Token: 0x04004D5A RID: 19802
			WS_EX_TOOLWINDOW = 128U,
			// Token: 0x04004D5B RID: 19803
			WS_EX_WINDOWEDGE = 256U,
			// Token: 0x04004D5C RID: 19804
			WS_EX_CLIENTEDGE = 512U,
			// Token: 0x04004D5D RID: 19805
			WS_EX_CONTEXTHELP = 1024U,
			// Token: 0x04004D5E RID: 19806
			WS_EX_RIGHT = 4096U,
			// Token: 0x04004D5F RID: 19807
			WS_EX_LEFT = 0U,
			// Token: 0x04004D60 RID: 19808
			WS_EX_RTLREADING = 8192U,
			// Token: 0x04004D61 RID: 19809
			WS_EX_LTRREADING = 0U,
			// Token: 0x04004D62 RID: 19810
			WS_EX_LEFTSCROLLBAR = 16384U,
			// Token: 0x04004D63 RID: 19811
			WS_EX_RIGHTSCROLLBAR = 0U,
			// Token: 0x04004D64 RID: 19812
			WS_EX_CONTROLPARENT = 65536U,
			// Token: 0x04004D65 RID: 19813
			WS_EX_STATICEDGE = 131072U,
			// Token: 0x04004D66 RID: 19814
			WS_EX_APPWINDOW = 262144U,
			// Token: 0x04004D67 RID: 19815
			WS_EX_OVERLAPPEDWINDOW = 768U,
			// Token: 0x04004D68 RID: 19816
			WS_EX_PALETTEWINDOW = 392U,
			// Token: 0x04004D69 RID: 19817
			WS_EX_LAYERED = 524288U,
			// Token: 0x04004D6A RID: 19818
			WS_EX_NOINHERITLAYOUT = 1048576U,
			// Token: 0x04004D6B RID: 19819
			WS_EX_LAYOUTRTL = 4194304U,
			// Token: 0x04004D6C RID: 19820
			WS_EX_COMPOSITED = 33554432U,
			// Token: 0x04004D6D RID: 19821
			WS_EX_NOACTIVATE = 134217728U
		}

		// Token: 0x020011F6 RID: 4598
		public enum DialogItemClass : uint
		{
			// Token: 0x04004D6F RID: 19823
			Button = 128U,
			// Token: 0x04004D70 RID: 19824
			Edit,
			// Token: 0x04004D71 RID: 19825
			Static,
			// Token: 0x04004D72 RID: 19826
			ListBox,
			// Token: 0x04004D73 RID: 19827
			ScrollBar,
			// Token: 0x04004D74 RID: 19828
			ComboBox
		}

		// Token: 0x020011F7 RID: 4599
		public enum StaticControlStyles : uint
		{
			// Token: 0x04004D76 RID: 19830
			SS_LEFT,
			// Token: 0x04004D77 RID: 19831
			SS_CENTER,
			// Token: 0x04004D78 RID: 19832
			SS_RIGHT,
			// Token: 0x04004D79 RID: 19833
			SS_ICON,
			// Token: 0x04004D7A RID: 19834
			SS_BLACKRECT,
			// Token: 0x04004D7B RID: 19835
			SS_GRAYRECT,
			// Token: 0x04004D7C RID: 19836
			SS_WHITERECT,
			// Token: 0x04004D7D RID: 19837
			SS_BLACKFRAME,
			// Token: 0x04004D7E RID: 19838
			SS_GRAYFRAME,
			// Token: 0x04004D7F RID: 19839
			SS_WHITEFRAME,
			// Token: 0x04004D80 RID: 19840
			SS_USERITEM,
			// Token: 0x04004D81 RID: 19841
			SS_SIMPLE,
			// Token: 0x04004D82 RID: 19842
			SS_LEFTNOWORDWRAP,
			// Token: 0x04004D83 RID: 19843
			SS_OWNERDRAW,
			// Token: 0x04004D84 RID: 19844
			SS_BITMAP,
			// Token: 0x04004D85 RID: 19845
			SS_ENHMETAFILE,
			// Token: 0x04004D86 RID: 19846
			SS_ETCHEDHORZ,
			// Token: 0x04004D87 RID: 19847
			SS_ETCHEDVERT,
			// Token: 0x04004D88 RID: 19848
			SS_ETCHEDFRAME,
			// Token: 0x04004D89 RID: 19849
			SS_TYPEMASK = 31U,
			// Token: 0x04004D8A RID: 19850
			SS_REALSIZECONTROL = 64U,
			// Token: 0x04004D8B RID: 19851
			SS_NOPREFIX = 128U,
			// Token: 0x04004D8C RID: 19852
			SS_NOTIFY = 256U,
			// Token: 0x04004D8D RID: 19853
			SS_CENTERIMAGE = 512U,
			// Token: 0x04004D8E RID: 19854
			SS_RIGHTJUST = 1024U,
			// Token: 0x04004D8F RID: 19855
			SS_REALSIZEIMAGE = 2048U,
			// Token: 0x04004D90 RID: 19856
			SS_SUNKEN = 4096U,
			// Token: 0x04004D91 RID: 19857
			SS_EDITCONTROL = 8192U,
			// Token: 0x04004D92 RID: 19858
			SS_ENDELLIPSIS = 16384U,
			// Token: 0x04004D93 RID: 19859
			SS_PATHELLIPSIS = 32768U,
			// Token: 0x04004D94 RID: 19860
			SS_WORDELLIPSIS = 49152U,
			// Token: 0x04004D95 RID: 19861
			SS_ELLIPSISMASK = 49152U
		}

		// Token: 0x020011F8 RID: 4600
		public enum ButtonControlStyles : uint
		{
			// Token: 0x04004D97 RID: 19863
			BS_PUSHBUTTON,
			// Token: 0x04004D98 RID: 19864
			BS_DEFPUSHBUTTON,
			// Token: 0x04004D99 RID: 19865
			BS_CHECKBOX,
			// Token: 0x04004D9A RID: 19866
			BS_AUTOCHECKBOX,
			// Token: 0x04004D9B RID: 19867
			BS_RADIOBUTTON,
			// Token: 0x04004D9C RID: 19868
			BS_3STATE,
			// Token: 0x04004D9D RID: 19869
			BS_AUTO3STATE,
			// Token: 0x04004D9E RID: 19870
			BS_GROUPBOX,
			// Token: 0x04004D9F RID: 19871
			BS_USERBUTTON,
			// Token: 0x04004DA0 RID: 19872
			BS_AUTORADIOBUTTON,
			// Token: 0x04004DA1 RID: 19873
			BS_PUSHBOX,
			// Token: 0x04004DA2 RID: 19874
			BS_OWNERDRAW,
			// Token: 0x04004DA3 RID: 19875
			BS_TYPEMASK = 15U,
			// Token: 0x04004DA4 RID: 19876
			BS_LEFTTEXT = 32U,
			// Token: 0x04004DA5 RID: 19877
			BS_TEXT = 0U,
			// Token: 0x04004DA6 RID: 19878
			BS_ICON = 64U,
			// Token: 0x04004DA7 RID: 19879
			BS_BITMAP = 128U,
			// Token: 0x04004DA8 RID: 19880
			BS_LEFT = 256U,
			// Token: 0x04004DA9 RID: 19881
			BS_RIGHT = 512U,
			// Token: 0x04004DAA RID: 19882
			BS_CENTER = 768U,
			// Token: 0x04004DAB RID: 19883
			BS_TOP = 1024U,
			// Token: 0x04004DAC RID: 19884
			BS_BOTTOM = 2048U,
			// Token: 0x04004DAD RID: 19885
			BS_VCENTER = 3072U,
			// Token: 0x04004DAE RID: 19886
			BS_PUSHLIKE = 4096U,
			// Token: 0x04004DAF RID: 19887
			BS_MULTILINE = 8192U,
			// Token: 0x04004DB0 RID: 19888
			BS_NOTIFY = 16384U,
			// Token: 0x04004DB1 RID: 19889
			BS_FLAT = 32768U,
			// Token: 0x04004DB2 RID: 19890
			BS_DEFSPLITBUTTON = 13U,
			// Token: 0x04004DB3 RID: 19891
			BS_COMMANDLINK,
			// Token: 0x04004DB4 RID: 19892
			BS_DEFCOMMANDLINK
		}

		// Token: 0x020011F9 RID: 4601
		public enum EditControlStyles : uint
		{
			// Token: 0x04004DB6 RID: 19894
			ES_LEFT,
			// Token: 0x04004DB7 RID: 19895
			ES_CENTER,
			// Token: 0x04004DB8 RID: 19896
			ES_RIGHT,
			// Token: 0x04004DB9 RID: 19897
			ES_MULTILINE = 4U,
			// Token: 0x04004DBA RID: 19898
			ES_UPPERCASE = 8U,
			// Token: 0x04004DBB RID: 19899
			ES_LOWERCASE = 16U,
			// Token: 0x04004DBC RID: 19900
			ES_PASSWORD = 32U,
			// Token: 0x04004DBD RID: 19901
			ES_AUTOVSCROLL = 64U,
			// Token: 0x04004DBE RID: 19902
			ES_AUTOHSCROLL = 128U,
			// Token: 0x04004DBF RID: 19903
			ES_NOHIDESEL = 256U,
			// Token: 0x04004DC0 RID: 19904
			ES_OEMCONVERT = 1024U,
			// Token: 0x04004DC1 RID: 19905
			ES_READONLY = 2048U,
			// Token: 0x04004DC2 RID: 19906
			ES_WANTRETURN = 4096U,
			// Token: 0x04004DC3 RID: 19907
			ES_NUMBER = 8192U
		}

		// Token: 0x020011FA RID: 4602
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		internal struct MENUTEMPLATE
		{
			// Token: 0x04004DC4 RID: 19908
			public ushort wVersion;

			// Token: 0x04004DC5 RID: 19909
			public ushort wOffset;
		}

		// Token: 0x020011FB RID: 4603
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		public struct MENUITEMTEMPLATE
		{
			// Token: 0x04004DC6 RID: 19910
			public ushort mtOption;
		}

		// Token: 0x020011FC RID: 4604
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct MENUEXTEMPLATE
		{
			// Token: 0x04004DC7 RID: 19911
			public ushort wVersion;

			// Token: 0x04004DC8 RID: 19912
			public ushort wOffset;
		}

		// Token: 0x020011FD RID: 4605
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct MENUEXITEMTEMPLATE
		{
			// Token: 0x04004DC9 RID: 19913
			public uint dwType;

			// Token: 0x04004DCA RID: 19914
			public uint dwState;

			// Token: 0x04004DCB RID: 19915
			public uint dwMenuId;

			// Token: 0x04004DCC RID: 19916
			public ushort bResInfo;
		}

		// Token: 0x020011FE RID: 4606
		public enum MenuFlags : uint
		{
			// Token: 0x04004DCE RID: 19918
			MF_INSERT,
			// Token: 0x04004DCF RID: 19919
			MF_CHANGE = 128U,
			// Token: 0x04004DD0 RID: 19920
			MF_APPEND = 256U,
			// Token: 0x04004DD1 RID: 19921
			MF_DELETE = 512U,
			// Token: 0x04004DD2 RID: 19922
			MF_REMOVE = 4096U,
			// Token: 0x04004DD3 RID: 19923
			MF_BYCOMMAND = 0U,
			// Token: 0x04004DD4 RID: 19924
			MF_BYPOSITION = 1024U,
			// Token: 0x04004DD5 RID: 19925
			MF_SEPARATOR = 2048U,
			// Token: 0x04004DD6 RID: 19926
			MF_ENABLED = 0U,
			// Token: 0x04004DD7 RID: 19927
			MF_GRAYED,
			// Token: 0x04004DD8 RID: 19928
			MF_DISABLED,
			// Token: 0x04004DD9 RID: 19929
			MF_UNCHECKED = 0U,
			// Token: 0x04004DDA RID: 19930
			MF_CHECKED = 8U,
			// Token: 0x04004DDB RID: 19931
			MF_USECHECKBITMAPS = 512U,
			// Token: 0x04004DDC RID: 19932
			MF_STRING = 0U,
			// Token: 0x04004DDD RID: 19933
			MF_BITMAP = 4U,
			// Token: 0x04004DDE RID: 19934
			MF_OWNERDRAW = 256U,
			// Token: 0x04004DDF RID: 19935
			MF_POPUP = 16U,
			// Token: 0x04004DE0 RID: 19936
			MF_MENUBARBREAK = 32U,
			// Token: 0x04004DE1 RID: 19937
			MF_MENUBREAK = 64U,
			// Token: 0x04004DE2 RID: 19938
			MF_UNHILITE = 0U,
			// Token: 0x04004DE3 RID: 19939
			MF_HILITE = 128U,
			// Token: 0x04004DE4 RID: 19940
			MF_DEFAULT = 4096U,
			// Token: 0x04004DE5 RID: 19941
			MF_SYSMENU = 8192U,
			// Token: 0x04004DE6 RID: 19942
			MF_HELP = 16384U,
			// Token: 0x04004DE7 RID: 19943
			MF_RIGHTJUSTIFY = 16384U,
			// Token: 0x04004DE8 RID: 19944
			MF_MOUSESELECT = 32768U,
			// Token: 0x04004DE9 RID: 19945
			MF_END = 128U,
			// Token: 0x04004DEA RID: 19946
			MFT_STRING = 0U,
			// Token: 0x04004DEB RID: 19947
			MFT_BITMAP = 4U,
			// Token: 0x04004DEC RID: 19948
			MFT_MENUBARBREAK = 32U,
			// Token: 0x04004DED RID: 19949
			MFT_MENUBREAK = 64U,
			// Token: 0x04004DEE RID: 19950
			MFT_OWNERDRAW = 256U,
			// Token: 0x04004DEF RID: 19951
			MFT_RADIOCHECK = 512U,
			// Token: 0x04004DF0 RID: 19952
			MFT_SEPARATOR = 2048U,
			// Token: 0x04004DF1 RID: 19953
			MFT_RIGHTORDER = 8192U,
			// Token: 0x04004DF2 RID: 19954
			MFT_RIGHTJUSTIFY = 16384U,
			// Token: 0x04004DF3 RID: 19955
			MFS_GRAYED = 3U,
			// Token: 0x04004DF4 RID: 19956
			MFS_DISABLED = 3U,
			// Token: 0x04004DF5 RID: 19957
			MFS_CHECKED = 8U,
			// Token: 0x04004DF6 RID: 19958
			MFS_HILITE = 128U,
			// Token: 0x04004DF7 RID: 19959
			MFS_ENABLED = 0U,
			// Token: 0x04004DF8 RID: 19960
			MFS_UNCHECKED = 0U,
			// Token: 0x04004DF9 RID: 19961
			MFS_UNHILITE = 0U,
			// Token: 0x04004DFA RID: 19962
			MFS_DEFAULT = 4096U
		}

		// Token: 0x020011FF RID: 4607
		public enum MenuResourceType
		{
			// Token: 0x04004DFC RID: 19964
			Last = 128,
			// Token: 0x04004DFD RID: 19965
			Sub = 1
		}

		// Token: 0x02001200 RID: 4608
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		public struct ACCEL
		{
			// Token: 0x04004DFE RID: 19966
			public ushort fVirt;

			// Token: 0x04004DFF RID: 19967
			public ushort key;

			// Token: 0x04004E00 RID: 19968
			public uint cmd;
		}

		// Token: 0x02001201 RID: 4609
		public enum AcceleratorVirtualKey : uint
		{
			// Token: 0x04004E02 RID: 19970
			VIRTKEY = 1U,
			// Token: 0x04004E03 RID: 19971
			NOINVERT,
			// Token: 0x04004E04 RID: 19972
			SHIFT = 4U,
			// Token: 0x04004E05 RID: 19973
			CONTROL = 8U,
			// Token: 0x04004E06 RID: 19974
			ALT = 16U
		}

		// Token: 0x02001202 RID: 4610
		public enum VirtualKeys : uint
		{
			// Token: 0x04004E08 RID: 19976
			VK_LBUTTON = 1U,
			// Token: 0x04004E09 RID: 19977
			VK_RBUTTON,
			// Token: 0x04004E0A RID: 19978
			VK_CANCEL,
			// Token: 0x04004E0B RID: 19979
			VK_MBUTTON,
			// Token: 0x04004E0C RID: 19980
			VK_XBUTTON1,
			// Token: 0x04004E0D RID: 19981
			VK_XBUTTON2,
			// Token: 0x04004E0E RID: 19982
			VK_BACK = 8U,
			// Token: 0x04004E0F RID: 19983
			VK_TAB,
			// Token: 0x04004E10 RID: 19984
			VK_CLEAR = 12U,
			// Token: 0x04004E11 RID: 19985
			VK_RETURN,
			// Token: 0x04004E12 RID: 19986
			VK_SHIFT = 16U,
			// Token: 0x04004E13 RID: 19987
			VK_CONTROL,
			// Token: 0x04004E14 RID: 19988
			VK_MENU,
			// Token: 0x04004E15 RID: 19989
			VK_PAUSE,
			// Token: 0x04004E16 RID: 19990
			VK_CAPITAL,
			// Token: 0x04004E17 RID: 19991
			VK_KANA,
			// Token: 0x04004E18 RID: 19992
			VK_JUNJA = 23U,
			// Token: 0x04004E19 RID: 19993
			VK_FINAL,
			// Token: 0x04004E1A RID: 19994
			VK_KANJI,
			// Token: 0x04004E1B RID: 19995
			VK_ESCAPE = 27U,
			// Token: 0x04004E1C RID: 19996
			VK_CONVERT,
			// Token: 0x04004E1D RID: 19997
			VK_NONCONVERT,
			// Token: 0x04004E1E RID: 19998
			VK_ACCEPT,
			// Token: 0x04004E1F RID: 19999
			VK_MODECHANGE,
			// Token: 0x04004E20 RID: 20000
			VK_SPACE,
			// Token: 0x04004E21 RID: 20001
			VK_PRIOR,
			// Token: 0x04004E22 RID: 20002
			VK_NEXT,
			// Token: 0x04004E23 RID: 20003
			VK_END,
			// Token: 0x04004E24 RID: 20004
			VK_HOME,
			// Token: 0x04004E25 RID: 20005
			VK_LEFT,
			// Token: 0x04004E26 RID: 20006
			VK_UP,
			// Token: 0x04004E27 RID: 20007
			VK_RIGHT,
			// Token: 0x04004E28 RID: 20008
			VK_DOWN,
			// Token: 0x04004E29 RID: 20009
			VK_SELECT,
			// Token: 0x04004E2A RID: 20010
			VK_PRINT,
			// Token: 0x04004E2B RID: 20011
			VK_EXECUTE,
			// Token: 0x04004E2C RID: 20012
			VK_SNAPSHOT,
			// Token: 0x04004E2D RID: 20013
			VK_INSERT,
			// Token: 0x04004E2E RID: 20014
			VK_DELETE,
			// Token: 0x04004E2F RID: 20015
			VK_HELP,
			// Token: 0x04004E30 RID: 20016
			VK_LWIN = 91U,
			// Token: 0x04004E31 RID: 20017
			VK_RWIN,
			// Token: 0x04004E32 RID: 20018
			VK_APPS,
			// Token: 0x04004E33 RID: 20019
			VK_SLEEP = 95U,
			// Token: 0x04004E34 RID: 20020
			VK_NUMPAD0,
			// Token: 0x04004E35 RID: 20021
			VK_NUMPAD1,
			// Token: 0x04004E36 RID: 20022
			VK_NUMPAD2,
			// Token: 0x04004E37 RID: 20023
			VK_NUMPAD3,
			// Token: 0x04004E38 RID: 20024
			VK_NUMPAD4,
			// Token: 0x04004E39 RID: 20025
			VK_NUMPAD5,
			// Token: 0x04004E3A RID: 20026
			VK_NUMPAD6,
			// Token: 0x04004E3B RID: 20027
			VK_NUMPAD7,
			// Token: 0x04004E3C RID: 20028
			VK_NUMPAD8,
			// Token: 0x04004E3D RID: 20029
			VK_NUMPAD9,
			// Token: 0x04004E3E RID: 20030
			VK_MULTIPLY,
			// Token: 0x04004E3F RID: 20031
			VK_ADD,
			// Token: 0x04004E40 RID: 20032
			VK_SEPARATOR,
			// Token: 0x04004E41 RID: 20033
			VK_SUBTRACT,
			// Token: 0x04004E42 RID: 20034
			VK_DECIMAL,
			// Token: 0x04004E43 RID: 20035
			VK_DIVIDE,
			// Token: 0x04004E44 RID: 20036
			VK_F1,
			// Token: 0x04004E45 RID: 20037
			VK_F2,
			// Token: 0x04004E46 RID: 20038
			VK_F3,
			// Token: 0x04004E47 RID: 20039
			VK_F4,
			// Token: 0x04004E48 RID: 20040
			VK_F5,
			// Token: 0x04004E49 RID: 20041
			VK_F6,
			// Token: 0x04004E4A RID: 20042
			VK_F7,
			// Token: 0x04004E4B RID: 20043
			VK_F8,
			// Token: 0x04004E4C RID: 20044
			VK_F9,
			// Token: 0x04004E4D RID: 20045
			VK_F10,
			// Token: 0x04004E4E RID: 20046
			VK_F11,
			// Token: 0x04004E4F RID: 20047
			VK_F12,
			// Token: 0x04004E50 RID: 20048
			VK_F13,
			// Token: 0x04004E51 RID: 20049
			VK_F14,
			// Token: 0x04004E52 RID: 20050
			VK_F15,
			// Token: 0x04004E53 RID: 20051
			VK_F16,
			// Token: 0x04004E54 RID: 20052
			VK_F17,
			// Token: 0x04004E55 RID: 20053
			VK_F18,
			// Token: 0x04004E56 RID: 20054
			VK_F19,
			// Token: 0x04004E57 RID: 20055
			VK_F20,
			// Token: 0x04004E58 RID: 20056
			VK_F21,
			// Token: 0x04004E59 RID: 20057
			VK_F22,
			// Token: 0x04004E5A RID: 20058
			VK_F23,
			// Token: 0x04004E5B RID: 20059
			VK_F24,
			// Token: 0x04004E5C RID: 20060
			VK_NUMLOCK = 144U,
			// Token: 0x04004E5D RID: 20061
			VK_SCROLL,
			// Token: 0x04004E5E RID: 20062
			VK_OEM_NEC_EQUAL,
			// Token: 0x04004E5F RID: 20063
			VK_OEM_FJ_JISHO = 146U,
			// Token: 0x04004E60 RID: 20064
			VK_OEM_FJ_MASSHOU,
			// Token: 0x04004E61 RID: 20065
			VK_OEM_FJ_TOUROKU,
			// Token: 0x04004E62 RID: 20066
			VK_OEM_FJ_LOYA,
			// Token: 0x04004E63 RID: 20067
			VK_OEM_FJ_ROYA,
			// Token: 0x04004E64 RID: 20068
			VK_LSHIFT = 160U,
			// Token: 0x04004E65 RID: 20069
			VK_RSHIFT,
			// Token: 0x04004E66 RID: 20070
			VK_LCONTROL,
			// Token: 0x04004E67 RID: 20071
			VK_RCONTROL,
			// Token: 0x04004E68 RID: 20072
			VK_LMENU,
			// Token: 0x04004E69 RID: 20073
			VK_RMENU,
			// Token: 0x04004E6A RID: 20074
			VK_BROWSER_BACK,
			// Token: 0x04004E6B RID: 20075
			VK_BROWSER_FORWARD,
			// Token: 0x04004E6C RID: 20076
			VK_BROWSER_REFRESH,
			// Token: 0x04004E6D RID: 20077
			VK_BROWSER_STOP,
			// Token: 0x04004E6E RID: 20078
			VK_BROWSER_SEARCH,
			// Token: 0x04004E6F RID: 20079
			VK_BROWSER_FAVORITES,
			// Token: 0x04004E70 RID: 20080
			VK_BROWSER_HOME,
			// Token: 0x04004E71 RID: 20081
			VK_VOLUME_MUTE,
			// Token: 0x04004E72 RID: 20082
			VK_VOLUME_DOWN,
			// Token: 0x04004E73 RID: 20083
			VK_VOLUME_UP,
			// Token: 0x04004E74 RID: 20084
			VK_MEDIA_NEXT_TRACK,
			// Token: 0x04004E75 RID: 20085
			VK_MEDIA_PREV_TRACK,
			// Token: 0x04004E76 RID: 20086
			VK_MEDIA_STOP,
			// Token: 0x04004E77 RID: 20087
			VK_MEDIA_PLAY_PAUSE,
			// Token: 0x04004E78 RID: 20088
			VK_LAUNCH_MAIL,
			// Token: 0x04004E79 RID: 20089
			VK_LAUNCH_MEDIA_SELECT,
			// Token: 0x04004E7A RID: 20090
			VK_LAUNCH_APP1,
			// Token: 0x04004E7B RID: 20091
			VK_LAUNCH_APP2,
			// Token: 0x04004E7C RID: 20092
			VK_OEM_1 = 186U,
			// Token: 0x04004E7D RID: 20093
			VK_OEM_PLUS,
			// Token: 0x04004E7E RID: 20094
			VK_OEM_COMMA,
			// Token: 0x04004E7F RID: 20095
			VK_OEM_MINUS,
			// Token: 0x04004E80 RID: 20096
			VK_OEM_PERIOD,
			// Token: 0x04004E81 RID: 20097
			VK_OEM_2,
			// Token: 0x04004E82 RID: 20098
			VK_OEM_3,
			// Token: 0x04004E83 RID: 20099
			VK_OEM_4 = 219U,
			// Token: 0x04004E84 RID: 20100
			VK_OEM_5,
			// Token: 0x04004E85 RID: 20101
			VK_OEM_6,
			// Token: 0x04004E86 RID: 20102
			VK_OEM_7,
			// Token: 0x04004E87 RID: 20103
			VK_OEM_8,
			// Token: 0x04004E88 RID: 20104
			VK_OEM_AX = 225U,
			// Token: 0x04004E89 RID: 20105
			VK_OEM_102,
			// Token: 0x04004E8A RID: 20106
			VK_ICO_HELP,
			// Token: 0x04004E8B RID: 20107
			VK_ICO_00,
			// Token: 0x04004E8C RID: 20108
			VK_PROCESSKEY,
			// Token: 0x04004E8D RID: 20109
			VK_ICO_CLEAR,
			// Token: 0x04004E8E RID: 20110
			VK_PACKET,
			// Token: 0x04004E8F RID: 20111
			VK_OEM_RESET = 233U,
			// Token: 0x04004E90 RID: 20112
			VK_OEM_JUMP,
			// Token: 0x04004E91 RID: 20113
			VK_OEM_PA1,
			// Token: 0x04004E92 RID: 20114
			VK_OEM_PA2,
			// Token: 0x04004E93 RID: 20115
			VK_OEM_PA3,
			// Token: 0x04004E94 RID: 20116
			VK_OEM_WSCTRL,
			// Token: 0x04004E95 RID: 20117
			VK_OEM_CUSEL,
			// Token: 0x04004E96 RID: 20118
			VK_OEM_ATTN,
			// Token: 0x04004E97 RID: 20119
			VK_OEM_FINISH,
			// Token: 0x04004E98 RID: 20120
			VK_OEM_COPY,
			// Token: 0x04004E99 RID: 20121
			VK_OEM_AUTO,
			// Token: 0x04004E9A RID: 20122
			VK_OEM_ENLW,
			// Token: 0x04004E9B RID: 20123
			VK_OEM_BACKTAB,
			// Token: 0x04004E9C RID: 20124
			VK_ATTN,
			// Token: 0x04004E9D RID: 20125
			VK_CRSEL,
			// Token: 0x04004E9E RID: 20126
			VK_EXSEL,
			// Token: 0x04004E9F RID: 20127
			VK_EREOF,
			// Token: 0x04004EA0 RID: 20128
			VK_PLAY,
			// Token: 0x04004EA1 RID: 20129
			VK_ZOOM,
			// Token: 0x04004EA2 RID: 20130
			VK_NONAME,
			// Token: 0x04004EA3 RID: 20131
			VK_PA1,
			// Token: 0x04004EA4 RID: 20132
			VK_OEM_CLEAR
		}

		// Token: 0x02001203 RID: 4611
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct FONTDIRENTRY
		{
			// Token: 0x04004EA5 RID: 20133
			public ushort dfVersion;

			// Token: 0x04004EA6 RID: 20134
			public uint dfSize;

			// Token: 0x04004EA7 RID: 20135
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 60)]
			public string dfCopyright;

			// Token: 0x04004EA8 RID: 20136
			public ushort dfType;

			// Token: 0x04004EA9 RID: 20137
			public ushort dfPoints;

			// Token: 0x04004EAA RID: 20138
			public ushort dfVertRes;

			// Token: 0x04004EAB RID: 20139
			public ushort dfHorizRes;

			// Token: 0x04004EAC RID: 20140
			public ushort dfAscent;

			// Token: 0x04004EAD RID: 20141
			public ushort dfInternalLeading;

			// Token: 0x04004EAE RID: 20142
			public ushort dfExternalLeading;

			// Token: 0x04004EAF RID: 20143
			public byte dfItalic;

			// Token: 0x04004EB0 RID: 20144
			public byte dfUnderline;

			// Token: 0x04004EB1 RID: 20145
			public byte dfStrikeOut;

			// Token: 0x04004EB2 RID: 20146
			public ushort dfWeight;

			// Token: 0x04004EB3 RID: 20147
			public byte dfCharSet;

			// Token: 0x04004EB4 RID: 20148
			public ushort dfPixWidth;

			// Token: 0x04004EB5 RID: 20149
			public ushort dfPixHeight;

			// Token: 0x04004EB6 RID: 20150
			public byte dfPitchAndFamily;

			// Token: 0x04004EB7 RID: 20151
			public ushort dfAvgWidth;

			// Token: 0x04004EB8 RID: 20152
			public ushort dfMaxWidth;

			// Token: 0x04004EB9 RID: 20153
			public byte dfFirstChar;

			// Token: 0x04004EBA RID: 20154
			public byte dfLastChar;

			// Token: 0x04004EBB RID: 20155
			public byte dfDefaultChar;

			// Token: 0x04004EBC RID: 20156
			public byte dfBreakChar;

			// Token: 0x04004EBD RID: 20157
			public ushort dfWidthBytes;

			// Token: 0x04004EBE RID: 20158
			public uint dfDevice;

			// Token: 0x04004EBF RID: 20159
			public uint dfFace;

			// Token: 0x04004EC0 RID: 20160
			public uint dfReserved;
		}
	}
}
