using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D29 RID: 3369
	[ComVisible(true)]
	public class MenuExTemplateItemPopup : MenuExTemplateItem
	{
		// Token: 0x17001D29 RID: 7465
		// (get) Token: 0x060088EE RID: 35054 RVA: 0x00293888 File Offset: 0x00293888
		// (set) Token: 0x060088EF RID: 35055 RVA: 0x00293890 File Offset: 0x00293890
		public MenuExTemplateItemCollection SubMenuItems
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

		// Token: 0x060088F1 RID: 35057 RVA: 0x002938B0 File Offset: 0x002938B0
		internal override IntPtr Read(IntPtr lpRes)
		{
			lpRes = base.Read(lpRes);
			lpRes = ResourceUtil.Align(lpRes);
			this._dwHelpId = (uint)Marshal.ReadInt32(lpRes);
			lpRes = new IntPtr(lpRes.ToInt64() + 4L);
			return this._subMenuItems.Read(lpRes);
		}

		// Token: 0x060088F2 RID: 35058 RVA: 0x002938FC File Offset: 0x002938FC
		internal override void Write(BinaryWriter w)
		{
			base.Write(w);
			ResourceUtil.PadToDWORD(w);
			w.Write(this._dwHelpId);
			this._subMenuItems.Write(w);
		}

		// Token: 0x060088F3 RID: 35059 RVA: 0x00293924 File Offset: 0x00293924
		public override string ToString(int indent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("{0}POPUP \"{1}\"", new string(' ', indent), (this._menuString == null) ? string.Empty : this._menuString.Replace("\t", "\\t")));
			stringBuilder.Append(this._subMenuItems.ToString(indent));
			return stringBuilder.ToString();
		}

		// Token: 0x04003EBE RID: 16062
		private uint _dwHelpId;

		// Token: 0x04003EBF RID: 16063
		private MenuExTemplateItemCollection _subMenuItems = new MenuExTemplateItemCollection();
	}
}
