using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D0F RID: 3343
	[ComVisible(true)]
	public class DialogExTemplateControl : DialogTemplateControlBase
	{
		// Token: 0x17001CD9 RID: 7385
		// (get) Token: 0x060087DC RID: 34780 RVA: 0x00290CC0 File Offset: 0x00290CC0
		// (set) Token: 0x060087DD RID: 34781 RVA: 0x00290CD0 File Offset: 0x00290CD0
		public override short x
		{
			get
			{
				return this._header.x;
			}
			set
			{
				this._header.x = value;
			}
		}

		// Token: 0x17001CDA RID: 7386
		// (get) Token: 0x060087DE RID: 34782 RVA: 0x00290CE0 File Offset: 0x00290CE0
		// (set) Token: 0x060087DF RID: 34783 RVA: 0x00290CF0 File Offset: 0x00290CF0
		public override short y
		{
			get
			{
				return this._header.y;
			}
			set
			{
				this._header.y = value;
			}
		}

		// Token: 0x17001CDB RID: 7387
		// (get) Token: 0x060087E0 RID: 34784 RVA: 0x00290D00 File Offset: 0x00290D00
		// (set) Token: 0x060087E1 RID: 34785 RVA: 0x00290D10 File Offset: 0x00290D10
		public override short cx
		{
			get
			{
				return this._header.cx;
			}
			set
			{
				this._header.cx = value;
			}
		}

		// Token: 0x17001CDC RID: 7388
		// (get) Token: 0x060087E2 RID: 34786 RVA: 0x00290D20 File Offset: 0x00290D20
		// (set) Token: 0x060087E3 RID: 34787 RVA: 0x00290D30 File Offset: 0x00290D30
		public override short cy
		{
			get
			{
				return this._header.cy;
			}
			set
			{
				this._header.cy = value;
			}
		}

		// Token: 0x17001CDD RID: 7389
		// (get) Token: 0x060087E4 RID: 34788 RVA: 0x00290D40 File Offset: 0x00290D40
		// (set) Token: 0x060087E5 RID: 34789 RVA: 0x00290D50 File Offset: 0x00290D50
		public override uint Style
		{
			get
			{
				return this._header.style;
			}
			set
			{
				this._header.style = value;
			}
		}

		// Token: 0x17001CDE RID: 7390
		// (get) Token: 0x060087E6 RID: 34790 RVA: 0x00290D60 File Offset: 0x00290D60
		// (set) Token: 0x060087E7 RID: 34791 RVA: 0x00290D70 File Offset: 0x00290D70
		public override uint ExtendedStyle
		{
			get
			{
				return this._header.exStyle;
			}
			set
			{
				this._header.exStyle = value;
			}
		}

		// Token: 0x17001CDF RID: 7391
		// (get) Token: 0x060087E8 RID: 34792 RVA: 0x00290D80 File Offset: 0x00290D80
		// (set) Token: 0x060087E9 RID: 34793 RVA: 0x00290D90 File Offset: 0x00290D90
		public int Id
		{
			get
			{
				return this._header.id;
			}
			set
			{
				this._header.id = value;
			}
		}

		// Token: 0x060087EB RID: 34795 RVA: 0x00290DA8 File Offset: 0x00290DA8
		internal override IntPtr Read(IntPtr lpRes)
		{
			this._header = (User32.DIALOGEXITEMTEMPLATE)Marshal.PtrToStructure(lpRes, typeof(User32.DIALOGEXITEMTEMPLATE));
			lpRes = new IntPtr(lpRes.ToInt64() + (long)Marshal.SizeOf(this._header));
			return base.Read(lpRes);
		}

		// Token: 0x060087EC RID: 34796 RVA: 0x00290DFC File Offset: 0x00290DFC
		public override void Write(BinaryWriter w)
		{
			w.Write(this._header.helpID);
			w.Write(this._header.exStyle);
			w.Write(this._header.style);
			w.Write(this._header.x);
			w.Write(this._header.y);
			w.Write(this._header.cx);
			w.Write(this._header.cy);
			w.Write(this._header.id);
			base.Write(w);
		}

		// Token: 0x060087ED RID: 34797 RVA: 0x00290E9C File Offset: 0x00290E9C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0} \"{1}\" {2}, {3}, {4}, {5}, {6}, {7}, {8}", new object[]
			{
				base.ControlClassId,
				base.CaptionId,
				this.Id,
				base.ControlClassId,
				this.x,
				this.y,
				this.cx,
				this.cy,
				DialogTemplateUtil.StyleToString<User32.WindowStyles, User32.StaticControlStyles>(this.Style, this.ExtendedStyle)
			});
			if (base.ControlClassId.IsIntResource())
			{
				User32.DialogItemClass controlClass = base.ControlClass;
				if (controlClass != User32.DialogItemClass.Button)
				{
					if (controlClass == User32.DialogItemClass.Edit)
					{
						stringBuilder.AppendFormat("| {0}", DialogTemplateUtil.StyleToString<User32.EditControlStyles>(this.Style & 65535U));
					}
				}
				else
				{
					stringBuilder.AppendFormat("| {0}", (User32.ButtonControlStyles)(this.Style & 65535U));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04003E8E RID: 16014
		private User32.DIALOGEXITEMTEMPLATE _header;
	}
}
