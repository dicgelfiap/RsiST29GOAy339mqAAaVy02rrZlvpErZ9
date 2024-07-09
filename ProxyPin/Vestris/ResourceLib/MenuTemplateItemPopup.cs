using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D30 RID: 3376
	[ComVisible(true)]
	public class MenuTemplateItemPopup : MenuTemplateItem
	{
		// Token: 0x17001D2F RID: 7471
		// (get) Token: 0x06008917 RID: 35095 RVA: 0x00293F94 File Offset: 0x00293F94
		// (set) Token: 0x06008918 RID: 35096 RVA: 0x00293F9C File Offset: 0x00293F9C
		public MenuTemplateItemCollection SubMenuItems
		{
			get
			{
				return this._subMenuItems;
			}
			set
			{
				this._subMenuItems = value;
			}
		}

		// Token: 0x0600891A RID: 35098 RVA: 0x00293FBC File Offset: 0x00293FBC
		internal override IntPtr Read(IntPtr lpRes)
		{
			this._header = (User32.MENUITEMTEMPLATE)Marshal.PtrToStructure(lpRes, typeof(User32.MENUITEMTEMPLATE));
			lpRes = new IntPtr(lpRes.ToInt64() + (long)Marshal.SizeOf(this._header));
			lpRes = base.Read(lpRes);
			return this._subMenuItems.Read(lpRes);
		}

		// Token: 0x0600891B RID: 35099 RVA: 0x00294020 File Offset: 0x00294020
		internal override void Write(BinaryWriter w)
		{
			w.Write(this._header.mtOption);
			base.Write(w);
			this._subMenuItems.Write(w);
		}

		// Token: 0x0600891C RID: 35100 RVA: 0x00294048 File Offset: 0x00294048
		public override string ToString(int indent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("{0}POPUP \"{1}\"", new string(' ', indent), (this._menuString == null) ? string.Empty : this._menuString.Replace("\t", "\\t")));
			stringBuilder.Append(this._subMenuItems.ToString(indent));
			return stringBuilder.ToString();
		}

		// Token: 0x04003EC6 RID: 16070
		private MenuTemplateItemCollection _subMenuItems = new MenuTemplateItemCollection();
	}
}
