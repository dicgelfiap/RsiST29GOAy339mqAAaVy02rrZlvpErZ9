using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D18 RID: 3352
	[ComVisible(true)]
	public class FontDirectoryEntry
	{
		// Token: 0x17001D0D RID: 7437
		// (get) Token: 0x0600886A RID: 34922 RVA: 0x002924B0 File Offset: 0x002924B0
		// (set) Token: 0x0600886B RID: 34923 RVA: 0x002924B8 File Offset: 0x002924B8
		public ushort FontOrdinal
		{
			get
			{
				return this._fontOrdinal;
			}
			set
			{
				this._fontOrdinal = value;
			}
		}

		// Token: 0x17001D0E RID: 7438
		// (get) Token: 0x0600886C RID: 34924 RVA: 0x002924C4 File Offset: 0x002924C4
		// (set) Token: 0x0600886D RID: 34925 RVA: 0x002924CC File Offset: 0x002924CC
		public string FaceName
		{
			get
			{
				return this._faceName;
			}
			set
			{
				this._faceName = value;
			}
		}

		// Token: 0x17001D0F RID: 7439
		// (get) Token: 0x0600886E RID: 34926 RVA: 0x002924D8 File Offset: 0x002924D8
		// (set) Token: 0x0600886F RID: 34927 RVA: 0x002924E0 File Offset: 0x002924E0
		public string DeviceName
		{
			get
			{
				return this._deviceName;
			}
			set
			{
				this._deviceName = value;
			}
		}

		// Token: 0x17001D10 RID: 7440
		// (get) Token: 0x06008870 RID: 34928 RVA: 0x002924EC File Offset: 0x002924EC
		// (set) Token: 0x06008871 RID: 34929 RVA: 0x002924F4 File Offset: 0x002924F4
		public User32.FONTDIRENTRY Font
		{
			get
			{
				return this._font;
			}
			set
			{
				this._font = value;
			}
		}

		// Token: 0x06008873 RID: 34931 RVA: 0x00292508 File Offset: 0x00292508
		internal IntPtr Read(IntPtr lpRes)
		{
			this._fontOrdinal = (ushort)Marshal.ReadInt16(lpRes);
			lpRes = new IntPtr(lpRes.ToInt64() + 2L);
			this._font = (User32.FONTDIRENTRY)Marshal.PtrToStructure(lpRes, typeof(User32.FONTDIRENTRY));
			lpRes = new IntPtr(lpRes.ToInt64() + (long)Marshal.SizeOf(this._font));
			this._deviceName = Marshal.PtrToStringAnsi(lpRes);
			lpRes = new IntPtr(lpRes.ToInt64() + (long)this._deviceName.Length + 1L);
			this._faceName = Marshal.PtrToStringAnsi(lpRes);
			lpRes = new IntPtr(lpRes.ToInt64() + (long)this._faceName.Length + 1L);
			return lpRes;
		}

		// Token: 0x06008874 RID: 34932 RVA: 0x002925C8 File Offset: 0x002925C8
		public void Write(BinaryWriter w)
		{
			w.Write(this._fontOrdinal);
			w.Write(ResourceUtil.GetBytes<User32.FONTDIRENTRY>(this._font));
			if (!string.IsNullOrEmpty(this._deviceName))
			{
				w.Write(Encoding.ASCII.GetBytes(this._deviceName));
			}
			w.Write(0);
			if (!string.IsNullOrEmpty(this._faceName))
			{
				w.Write(Encoding.ASCII.GetBytes(this._faceName));
			}
			w.Write(0);
		}

		// Token: 0x04003E9E RID: 16030
		private ushort _fontOrdinal;

		// Token: 0x04003E9F RID: 16031
		private User32.FONTDIRENTRY _font;

		// Token: 0x04003EA0 RID: 16032
		private string _faceName;

		// Token: 0x04003EA1 RID: 16033
		private string _deviceName;
	}
}
