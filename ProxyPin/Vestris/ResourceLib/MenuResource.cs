using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D2A RID: 3370
	[ComVisible(true)]
	public class MenuResource : Resource
	{
		// Token: 0x17001D2A RID: 7466
		// (get) Token: 0x060088F4 RID: 35060 RVA: 0x00293998 File Offset: 0x00293998
		// (set) Token: 0x060088F5 RID: 35061 RVA: 0x002939A0 File Offset: 0x002939A0
		public MenuTemplateBase Menu
		{
			get
			{
				return this._menu;
			}
			set
			{
				this._menu = value;
			}
		}

		// Token: 0x060088F6 RID: 35062 RVA: 0x002939AC File Offset: 0x002939AC
		public MenuResource() : base(IntPtr.Zero, IntPtr.Zero, new ResourceId(Kernel32.ResourceTypes.RT_MENU), null, ResourceUtil.NEUTRALLANGID, 0)
		{
		}

		// Token: 0x060088F7 RID: 35063 RVA: 0x002939CC File Offset: 0x002939CC
		public MenuResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, ushort language, int size) : base(hModule, hResource, type, name, language, size)
		{
		}

		// Token: 0x060088F8 RID: 35064 RVA: 0x002939E0 File Offset: 0x002939E0
		internal override IntPtr Read(IntPtr hModule, IntPtr lpRes)
		{
			ushort num = (ushort)Marshal.ReadInt16(lpRes);
			if (num != 0)
			{
				if (num != 1)
				{
					throw new NotSupportedException(string.Format("Unexpected menu header version {0}", num));
				}
				this._menu = new MenuExTemplate();
			}
			else
			{
				this._menu = new MenuTemplate();
			}
			return this._menu.Read(lpRes);
		}

		// Token: 0x060088F9 RID: 35065 RVA: 0x00293A50 File Offset: 0x00293A50
		internal override void Write(BinaryWriter w)
		{
			this._menu.Write(w);
		}

		// Token: 0x060088FA RID: 35066 RVA: 0x00293A60 File Offset: 0x00293A60
		public override string ToString()
		{
			return string.Format("{0} {1}", base.Name, this.Menu.ToString());
		}

		// Token: 0x04003EC0 RID: 16064
		private MenuTemplateBase _menu;
	}
}
