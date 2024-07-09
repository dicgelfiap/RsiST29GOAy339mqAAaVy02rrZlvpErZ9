using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D28 RID: 3368
	[ComVisible(true)]
	public class MenuExTemplateItemCommand : MenuExTemplateItem
	{
		// Token: 0x17001D28 RID: 7464
		// (get) Token: 0x060088EC RID: 35052 RVA: 0x00293784 File Offset: 0x00293784
		public bool IsSeparator
		{
			get
			{
				return this._header.dwType == 2048U || ((this._header.bResInfo == ushort.MaxValue || this._header.bResInfo == 0) && this._header.dwMenuId == 0U && this._menuString == null);
			}
		}

		// Token: 0x060088ED RID: 35053 RVA: 0x002937EC File Offset: 0x002937EC
		public override string ToString(int indent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.IsSeparator)
			{
				stringBuilder.AppendLine(string.Format("{0}MENUITEM SEPARATOR", new string(' ', indent)));
			}
			else
			{
				stringBuilder.AppendLine(string.Format("{0}MENUITEM \"{1}\", {2}", new string(' ', indent), (this._menuString == null) ? string.Empty : this._menuString.Replace("\t", "\\t"), this._header.dwMenuId));
			}
			return stringBuilder.ToString();
		}
	}
}
