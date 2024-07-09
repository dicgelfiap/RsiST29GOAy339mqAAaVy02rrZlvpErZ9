using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D10 RID: 3344
	[ComVisible(true)]
	public class DialogResource : Resource
	{
		// Token: 0x17001CE0 RID: 7392
		// (get) Token: 0x060087EE RID: 34798 RVA: 0x00290FB4 File Offset: 0x00290FB4
		// (set) Token: 0x060087EF RID: 34799 RVA: 0x00290FBC File Offset: 0x00290FBC
		public DialogTemplateBase Template
		{
			get
			{
				return this._dlgtemplate;
			}
			set
			{
				this._dlgtemplate = value;
			}
		}

		// Token: 0x060087F0 RID: 34800 RVA: 0x00290FC8 File Offset: 0x00290FC8
		public DialogResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, ushort language, int size) : base(hModule, hResource, type, name, language, size)
		{
		}

		// Token: 0x060087F1 RID: 34801 RVA: 0x00290FDC File Offset: 0x00290FDC
		public DialogResource() : base(IntPtr.Zero, IntPtr.Zero, new ResourceId(Kernel32.ResourceTypes.RT_DIALOG), new ResourceId(1U), ResourceUtil.NEUTRALLANGID, 0)
		{
		}

		// Token: 0x060087F2 RID: 34802 RVA: 0x00291000 File Offset: 0x00291000
		internal override IntPtr Read(IntPtr hModule, IntPtr lpRes)
		{
			uint num = (uint)Marshal.ReadInt32(lpRes) >> 16;
			if (num == 65535U)
			{
				this._dlgtemplate = new DialogExTemplate();
			}
			else
			{
				this._dlgtemplate = new DialogTemplate();
			}
			return this._dlgtemplate.Read(lpRes);
		}

		// Token: 0x060087F3 RID: 34803 RVA: 0x00291050 File Offset: 0x00291050
		internal override void Write(BinaryWriter w)
		{
			this._dlgtemplate.Write(w);
		}

		// Token: 0x060087F4 RID: 34804 RVA: 0x00291060 File Offset: 0x00291060
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0} {1}", base.Name.IsIntResource() ? base.Name.ToString() : ("\"" + base.Name.ToString() + "\""), this._dlgtemplate);
			return stringBuilder.ToString();
		}

		// Token: 0x04003E8F RID: 16015
		private DialogTemplateBase _dlgtemplate;
	}
}
