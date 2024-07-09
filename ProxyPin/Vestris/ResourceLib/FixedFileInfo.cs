using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D17 RID: 3351
	[ComVisible(true)]
	public class FixedFileInfo
	{
		// Token: 0x17001D08 RID: 7432
		// (get) Token: 0x0600885F RID: 34911 RVA: 0x00291FE0 File Offset: 0x00291FE0
		public Kernel32.VS_FIXEDFILEINFO Value
		{
			get
			{
				return this._fixedfileinfo;
			}
		}

		// Token: 0x06008860 RID: 34912 RVA: 0x00291FE8 File Offset: 0x00291FE8
		internal void Read(IntPtr lpRes)
		{
			this._fixedfileinfo = (Kernel32.VS_FIXEDFILEINFO)Marshal.PtrToStructure(lpRes, typeof(Kernel32.VS_FIXEDFILEINFO));
		}

		// Token: 0x17001D09 RID: 7433
		// (get) Token: 0x06008861 RID: 34913 RVA: 0x00292008 File Offset: 0x00292008
		// (set) Token: 0x06008862 RID: 34914 RVA: 0x0029208C File Offset: 0x0029208C
		public string FileVersion
		{
			get
			{
				return string.Format("{0}.{1}.{2}.{3}", new object[]
				{
					ResourceUtil.HiWord(this._fixedfileinfo.dwFileVersionMS),
					ResourceUtil.LoWord(this._fixedfileinfo.dwFileVersionMS),
					ResourceUtil.HiWord(this._fixedfileinfo.dwFileVersionLS),
					ResourceUtil.LoWord(this._fixedfileinfo.dwFileVersionLS)
				});
			}
			set
			{
				uint num = 0U;
				uint num2 = 0U;
				uint num3 = 0U;
				uint num4 = 0U;
				string[] array = value.Split(".".ToCharArray(), 4);
				if (array.Length >= 1)
				{
					num = uint.Parse(array[0]);
				}
				if (array.Length >= 2)
				{
					num2 = uint.Parse(array[1]);
				}
				if (array.Length >= 3)
				{
					num3 = uint.Parse(array[2]);
				}
				if (array.Length >= 4)
				{
					num4 = uint.Parse(array[3]);
				}
				this._fixedfileinfo.dwFileVersionMS = (num << 16) + num2;
				this._fixedfileinfo.dwFileVersionLS = (num3 << 16) + num4;
			}
		}

		// Token: 0x17001D0A RID: 7434
		// (get) Token: 0x06008863 RID: 34915 RVA: 0x0029213C File Offset: 0x0029213C
		// (set) Token: 0x06008864 RID: 34916 RVA: 0x002921C0 File Offset: 0x002921C0
		public string ProductVersion
		{
			get
			{
				return string.Format("{0}.{1}.{2}.{3}", new object[]
				{
					ResourceUtil.HiWord(this._fixedfileinfo.dwProductVersionMS),
					ResourceUtil.LoWord(this._fixedfileinfo.dwProductVersionMS),
					ResourceUtil.HiWord(this._fixedfileinfo.dwProductVersionLS),
					ResourceUtil.LoWord(this._fixedfileinfo.dwProductVersionLS)
				});
			}
			set
			{
				uint num = 0U;
				uint num2 = 0U;
				uint num3 = 0U;
				uint num4 = 0U;
				string[] array = value.Split(".".ToCharArray(), 4);
				if (array.Length >= 1)
				{
					num = uint.Parse(array[0]);
				}
				if (array.Length >= 2)
				{
					num2 = uint.Parse(array[1]);
				}
				if (array.Length >= 3)
				{
					num3 = uint.Parse(array[2]);
				}
				if (array.Length >= 4)
				{
					num4 = uint.Parse(array[3]);
				}
				this._fixedfileinfo.dwProductVersionMS = (num << 16) + num2;
				this._fixedfileinfo.dwProductVersionLS = (num3 << 16) + num4;
			}
		}

		// Token: 0x17001D0B RID: 7435
		// (get) Token: 0x06008865 RID: 34917 RVA: 0x00292270 File Offset: 0x00292270
		// (set) Token: 0x06008866 RID: 34918 RVA: 0x00292280 File Offset: 0x00292280
		public uint FileFlags
		{
			get
			{
				return this._fixedfileinfo.dwFileFlags;
			}
			set
			{
				this._fixedfileinfo.dwFileFlags = value;
			}
		}

		// Token: 0x06008867 RID: 34919 RVA: 0x00292290 File Offset: 0x00292290
		public void Write(BinaryWriter w)
		{
			w.Write(ResourceUtil.GetBytes<Kernel32.VS_FIXEDFILEINFO>(this._fixedfileinfo));
			ResourceUtil.PadToDWORD(w);
		}

		// Token: 0x17001D0C RID: 7436
		// (get) Token: 0x06008868 RID: 34920 RVA: 0x002922AC File Offset: 0x002922AC
		public ushort Size
		{
			get
			{
				return (ushort)Marshal.SizeOf(this._fixedfileinfo);
			}
		}

		// Token: 0x06008869 RID: 34921 RVA: 0x002922C0 File Offset: 0x002922C0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("FILEVERSION {0},{1},{2},{3}", new object[]
			{
				ResourceUtil.HiWord(this._fixedfileinfo.dwFileVersionMS),
				ResourceUtil.LoWord(this._fixedfileinfo.dwFileVersionMS),
				ResourceUtil.HiWord(this._fixedfileinfo.dwFileVersionLS),
				ResourceUtil.LoWord(this._fixedfileinfo.dwFileVersionLS)
			}));
			stringBuilder.AppendLine(string.Format("PRODUCTVERSION {0},{1},{2},{3}", new object[]
			{
				ResourceUtil.HiWord(this._fixedfileinfo.dwProductVersionMS),
				ResourceUtil.LoWord(this._fixedfileinfo.dwProductVersionMS),
				ResourceUtil.HiWord(this._fixedfileinfo.dwProductVersionLS),
				ResourceUtil.LoWord(this._fixedfileinfo.dwProductVersionLS)
			}));
			if (this._fixedfileinfo.dwFileFlagsMask == 63U)
			{
				stringBuilder.AppendLine("FILEFLAGSMASK VS_FFI_FILEFLAGSMASK");
			}
			else
			{
				stringBuilder.AppendLine(string.Format("FILEFLAGSMASK 0x{0:x}", this._fixedfileinfo.dwFileFlagsMask.ToString()));
			}
			stringBuilder.AppendLine(string.Format("FILEFLAGS {0}", (this._fixedfileinfo.dwFileFlags == 0U) ? "0" : ResourceUtil.FlagsToString<Winver.FileFlags>(this._fixedfileinfo.dwFileFlags)));
			stringBuilder.AppendLine(string.Format("FILEOS {0}", ResourceUtil.FlagsToString<Winver.FileOs>(this._fixedfileinfo.dwFileFlags)));
			stringBuilder.AppendLine(string.Format("FILETYPE {0}", ResourceUtil.FlagsToString<Winver.FileType>(this._fixedfileinfo.dwFileType)));
			stringBuilder.AppendLine(string.Format("FILESUBTYPE {0}", ResourceUtil.FlagsToString<Winver.FileSubType>(this._fixedfileinfo.dwFileSubtype)));
			return stringBuilder.ToString();
		}

		// Token: 0x04003E9D RID: 16029
		private Kernel32.VS_FIXEDFILEINFO _fixedfileinfo = Kernel32.VS_FIXEDFILEINFO.GetWindowsDefault();
	}
}
