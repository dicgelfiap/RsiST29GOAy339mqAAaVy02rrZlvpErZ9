using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D2D RID: 3373
	[ComVisible(true)]
	public abstract class MenuTemplateItem
	{
		// Token: 0x17001D2C RID: 7468
		// (get) Token: 0x06008904 RID: 35076 RVA: 0x00293BB4 File Offset: 0x00293BB4
		// (set) Token: 0x06008905 RID: 35077 RVA: 0x00293BBC File Offset: 0x00293BBC
		public string MenuString
		{
			get
			{
				return this._menuString;
			}
			set
			{
				this._menuString = value;
			}
		}

		// Token: 0x06008906 RID: 35078 RVA: 0x00293BC8 File Offset: 0x00293BC8
		internal virtual IntPtr Read(IntPtr lpRes)
		{
			if ((ushort)Marshal.ReadInt16(lpRes) == 0)
			{
				lpRes = new IntPtr(lpRes.ToInt64() + 2L);
			}
			else
			{
				this._menuString = Marshal.PtrToStringUni(lpRes);
				lpRes = new IntPtr(lpRes.ToInt64() + (long)((this._menuString.Length + 1) * Marshal.SystemDefaultCharSize));
			}
			return lpRes;
		}

		// Token: 0x06008907 RID: 35079 RVA: 0x00293C30 File Offset: 0x00293C30
		internal virtual void Write(BinaryWriter w)
		{
			if (this._menuString == null)
			{
				w.Write(0);
				return;
			}
			w.Write(Encoding.Unicode.GetBytes(this._menuString));
			w.Write(0);
			ResourceUtil.PadToWORD(w);
		}

		// Token: 0x06008908 RID: 35080
		public abstract string ToString(int indent);

		// Token: 0x06008909 RID: 35081 RVA: 0x00293C6C File Offset: 0x00293C6C
		public override string ToString()
		{
			return this.ToString(0);
		}

		// Token: 0x04003EC3 RID: 16067
		protected User32.MENUITEMTEMPLATE _header;

		// Token: 0x04003EC4 RID: 16068
		protected string _menuString;
	}
}
