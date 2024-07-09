using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D2B RID: 3371
	[ComVisible(true)]
	public class MenuTemplate : MenuTemplateBase
	{
		// Token: 0x17001D2B RID: 7467
		// (get) Token: 0x060088FB RID: 35067 RVA: 0x00293A8C File Offset: 0x00293A8C
		// (set) Token: 0x060088FC RID: 35068 RVA: 0x00293A94 File Offset: 0x00293A94
		public MenuTemplateItemCollection MenuItems
		{
			get
			{
				return this._menuItems;
			}
			set
			{
				this._menuItems = value;
			}
		}

		// Token: 0x060088FD RID: 35069 RVA: 0x00293AA0 File Offset: 0x00293AA0
		internal override IntPtr Read(IntPtr lpRes)
		{
			this._header = (User32.MENUTEMPLATE)Marshal.PtrToStructure(lpRes, typeof(User32.MENUTEMPLATE));
			IntPtr lpRes2 = new IntPtr(lpRes.ToInt64() + (long)Marshal.SizeOf(this._header) + (long)((ulong)this._header.wOffset));
			return this._menuItems.Read(lpRes2);
		}

		// Token: 0x060088FE RID: 35070 RVA: 0x00293B08 File Offset: 0x00293B08
		internal override void Write(BinaryWriter w)
		{
			w.Write(this._header.wVersion);
			w.Write(this._header.wOffset);
			ResourceUtil.Pad(w, this._header.wOffset);
			this._menuItems.Write(w);
		}

		// Token: 0x060088FF RID: 35071 RVA: 0x00293B5C File Offset: 0x00293B5C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("MENU");
			stringBuilder.Append(this._menuItems.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x04003EC1 RID: 16065
		private User32.MENUTEMPLATE _header;

		// Token: 0x04003EC2 RID: 16066
		private MenuTemplateItemCollection _menuItems = new MenuTemplateItemCollection();
	}
}
