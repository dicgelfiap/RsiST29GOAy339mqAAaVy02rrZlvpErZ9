using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D0E RID: 3342
	[ComVisible(true)]
	public class DialogExTemplate : DialogTemplateBase
	{
		// Token: 0x17001CCF RID: 7375
		// (get) Token: 0x060087C4 RID: 34756 RVA: 0x00290940 File Offset: 0x00290940
		// (set) Token: 0x060087C5 RID: 34757 RVA: 0x00290948 File Offset: 0x00290948
		public byte CharacterSet
		{
			get
			{
				return this._characterSet;
			}
			set
			{
				this._characterSet = value;
			}
		}

		// Token: 0x17001CD0 RID: 7376
		// (get) Token: 0x060087C6 RID: 34758 RVA: 0x00290954 File Offset: 0x00290954
		// (set) Token: 0x060087C7 RID: 34759 RVA: 0x00290964 File Offset: 0x00290964
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

		// Token: 0x17001CD1 RID: 7377
		// (get) Token: 0x060087C8 RID: 34760 RVA: 0x00290974 File Offset: 0x00290974
		// (set) Token: 0x060087C9 RID: 34761 RVA: 0x00290984 File Offset: 0x00290984
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

		// Token: 0x17001CD2 RID: 7378
		// (get) Token: 0x060087CA RID: 34762 RVA: 0x00290994 File Offset: 0x00290994
		// (set) Token: 0x060087CB RID: 34763 RVA: 0x002909A4 File Offset: 0x002909A4
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

		// Token: 0x17001CD3 RID: 7379
		// (get) Token: 0x060087CC RID: 34764 RVA: 0x002909B4 File Offset: 0x002909B4
		// (set) Token: 0x060087CD RID: 34765 RVA: 0x002909C4 File Offset: 0x002909C4
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

		// Token: 0x17001CD4 RID: 7380
		// (get) Token: 0x060087CE RID: 34766 RVA: 0x002909D4 File Offset: 0x002909D4
		// (set) Token: 0x060087CF RID: 34767 RVA: 0x002909E4 File Offset: 0x002909E4
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

		// Token: 0x17001CD5 RID: 7381
		// (get) Token: 0x060087D0 RID: 34768 RVA: 0x002909F4 File Offset: 0x002909F4
		// (set) Token: 0x060087D1 RID: 34769 RVA: 0x00290A04 File Offset: 0x00290A04
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

		// Token: 0x17001CD6 RID: 7382
		// (get) Token: 0x060087D2 RID: 34770 RVA: 0x00290A14 File Offset: 0x00290A14
		// (set) Token: 0x060087D3 RID: 34771 RVA: 0x00290A1C File Offset: 0x00290A1C
		public ushort Weight
		{
			get
			{
				return this._weight;
			}
			set
			{
				this._weight = value;
			}
		}

		// Token: 0x17001CD7 RID: 7383
		// (get) Token: 0x060087D4 RID: 34772 RVA: 0x00290A28 File Offset: 0x00290A28
		// (set) Token: 0x060087D5 RID: 34773 RVA: 0x00290A30 File Offset: 0x00290A30
		public bool Italic
		{
			get
			{
				return this._italic;
			}
			set
			{
				this._italic = value;
			}
		}

		// Token: 0x17001CD8 RID: 7384
		// (get) Token: 0x060087D6 RID: 34774 RVA: 0x00290A3C File Offset: 0x00290A3C
		public override ushort ControlCount
		{
			get
			{
				return this._header.cDlgItems;
			}
		}

		// Token: 0x060087D8 RID: 34776 RVA: 0x00290A54 File Offset: 0x00290A54
		internal override IntPtr Read(IntPtr lpRes)
		{
			this._header = (User32.DIALOGEXTEMPLATE)Marshal.PtrToStructure(lpRes, typeof(User32.DIALOGEXTEMPLATE));
			lpRes = base.Read(new IntPtr(lpRes.ToInt64() + 26L));
			if ((this.Style & 64U) > 0U || (this.Style & 72U) > 0U)
			{
				this.Weight = (ushort)Marshal.ReadInt16(lpRes);
				lpRes = new IntPtr(lpRes.ToInt64() + 2L);
				this.Italic = (Marshal.ReadByte(lpRes) > 0);
				lpRes = new IntPtr(lpRes.ToInt64() + 1L);
				this.CharacterSet = Marshal.ReadByte(lpRes);
				lpRes = new IntPtr(lpRes.ToInt64() + 1L);
				base.TypeFace = Marshal.PtrToStringUni(lpRes);
				lpRes = new IntPtr(lpRes.ToInt64() + (long)((base.TypeFace.Length + 1) * Marshal.SystemDefaultCharSize));
			}
			return base.ReadControls(lpRes);
		}

		// Token: 0x060087D9 RID: 34777 RVA: 0x00290B48 File Offset: 0x00290B48
		internal override IntPtr AddControl(IntPtr lpRes)
		{
			DialogExTemplateControl dialogExTemplateControl = new DialogExTemplateControl();
			base.Controls.Add(dialogExTemplateControl);
			return dialogExTemplateControl.Read(lpRes);
		}

		// Token: 0x060087DA RID: 34778 RVA: 0x00290B74 File Offset: 0x00290B74
		public override void Write(BinaryWriter w)
		{
			w.Write(this._header.dlgVer);
			w.Write(this._header.signature);
			w.Write(this._header.helpID);
			w.Write(this._header.exStyle);
			w.Write(this._header.style);
			w.Write((ushort)base.Controls.Count);
			w.Write(this._header.x);
			w.Write(this._header.y);
			w.Write(this._header.cx);
			w.Write(this._header.cy);
			base.Write(w);
			if ((this.Style & 64U) > 0U || (this.Style & 72U) > 0U)
			{
				w.Write(this.Weight);
				w.Write(this.Italic ? 1 : 0);
				w.Write(this.CharacterSet);
				w.Write(Encoding.Unicode.GetBytes(base.TypeFace));
				w.Write(0);
			}
			base.WriteControls(w);
		}

		// Token: 0x060087DB RID: 34779 RVA: 0x00290CAC File Offset: 0x00290CAC
		public override string ToString()
		{
			return string.Format("DIALOGEX {0}", base.ToString());
		}

		// Token: 0x04003E8A RID: 16010
		private User32.DIALOGEXTEMPLATE _header;

		// Token: 0x04003E8B RID: 16011
		private byte _characterSet;

		// Token: 0x04003E8C RID: 16012
		private ushort _weight;

		// Token: 0x04003E8D RID: 16013
		private bool _italic;
	}
}
