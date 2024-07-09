using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D26 RID: 3366
	[ComVisible(true)]
	public abstract class MenuExTemplateItem
	{
		// Token: 0x17001D27 RID: 7463
		// (get) Token: 0x060088DF RID: 35039 RVA: 0x002934B8 File Offset: 0x002934B8
		// (set) Token: 0x060088E0 RID: 35040 RVA: 0x002934C0 File Offset: 0x002934C0
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

		// Token: 0x060088E1 RID: 35041 RVA: 0x002934CC File Offset: 0x002934CC
		internal virtual IntPtr Read(IntPtr lpRes)
		{
			this._header = (User32.MENUEXITEMTEMPLATE)Marshal.PtrToStructure(lpRes, typeof(User32.MENUEXITEMTEMPLATE));
			lpRes = new IntPtr(lpRes.ToInt64() + (long)Marshal.SizeOf(this._header));
			uint num = (uint)Marshal.ReadInt32(lpRes);
			if (num != 0U)
			{
				this._menuString = Marshal.PtrToStringUni(lpRes);
				lpRes = new IntPtr(lpRes.ToInt64() + (long)((this._menuString.Length + 1) * Marshal.SystemDefaultCharSize));
			}
			return lpRes;
		}

		// Token: 0x060088E2 RID: 35042 RVA: 0x00293558 File Offset: 0x00293558
		internal virtual void Write(BinaryWriter w)
		{
			w.Write(this._header.dwType);
			w.Write(this._header.dwState);
			w.Write(this._header.dwMenuId);
			w.Write(this._header.bResInfo);
			if (this._menuString != null)
			{
				w.Write(Encoding.Unicode.GetBytes(this._menuString));
				w.Write(0);
				ResourceUtil.PadToDWORD(w);
			}
		}

		// Token: 0x060088E3 RID: 35043
		public abstract string ToString(int indent);

		// Token: 0x060088E4 RID: 35044 RVA: 0x002935DC File Offset: 0x002935DC
		public override string ToString()
		{
			return this.ToString(0);
		}

		// Token: 0x04003EBC RID: 16060
		protected User32.MENUEXITEMTEMPLATE _header;

		// Token: 0x04003EBD RID: 16061
		protected string _menuString;
	}
}
