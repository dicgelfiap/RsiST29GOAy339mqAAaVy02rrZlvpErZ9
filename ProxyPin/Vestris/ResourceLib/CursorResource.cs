using System;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D0C RID: 3340
	[ComVisible(true)]
	public class CursorResource : IconImageResource
	{
		// Token: 0x060087AA RID: 34730 RVA: 0x0029007C File Offset: 0x0029007C
		internal CursorResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, ushort language, int size) : base(hModule, hResource, type, name, language, size)
		{
		}

		// Token: 0x060087AB RID: 34731 RVA: 0x00290090 File Offset: 0x00290090
		public CursorResource() : base(new ResourceId(Kernel32.ResourceTypes.RT_CURSOR))
		{
		}

		// Token: 0x060087AC RID: 34732 RVA: 0x002900A0 File Offset: 0x002900A0
		public CursorResource(IconFileIcon icon, ResourceId id, ushort language) : base(icon, new ResourceId(Kernel32.ResourceTypes.RT_CURSOR), id, language)
		{
		}

		// Token: 0x17001CC2 RID: 7362
		// (get) Token: 0x060087AD RID: 34733 RVA: 0x002900B4 File Offset: 0x002900B4
		// (set) Token: 0x060087AE RID: 34734 RVA: 0x002900BC File Offset: 0x002900BC
		public ushort HotspotX
		{
			get
			{
				return this._hotspotx;
			}
			set
			{
				this._hotspotx = value;
			}
		}

		// Token: 0x17001CC3 RID: 7363
		// (get) Token: 0x060087AF RID: 34735 RVA: 0x002900C8 File Offset: 0x002900C8
		// (set) Token: 0x060087B0 RID: 34736 RVA: 0x002900D0 File Offset: 0x002900D0
		public ushort HotspotY
		{
			get
			{
				return this._hotspoty;
			}
			set
			{
				this._hotspoty = value;
			}
		}

		// Token: 0x060087B1 RID: 34737 RVA: 0x002900DC File Offset: 0x002900DC
		public override void SaveIconTo(string filename)
		{
			byte[] array = new byte[base.Image.Data.Length + 4];
			Buffer.BlockCopy(base.Image.Data, 0, array, 4, base.Image.Data.Length);
			array[0] = (byte)(this.HotspotX & 255);
			array[1] = (byte)(this.HotspotX >> 8);
			array[2] = (byte)(this.HotspotY & 255);
			array[3] = (byte)(this.HotspotY >> 8);
			Resource.SaveTo(filename, this._type, new ResourceId((uint)this._header.nID), this._language, array);
		}

		// Token: 0x060087B2 RID: 34738 RVA: 0x00290180 File Offset: 0x00290180
		internal override void ReadImage(IntPtr dibBits, uint size)
		{
			this._hotspotx = (ushort)Marshal.ReadInt16(dibBits);
			dibBits = new IntPtr(dibBits.ToInt64() + 2L);
			this._hotspoty = (ushort)Marshal.ReadInt16(dibBits);
			dibBits = new IntPtr(dibBits.ToInt64() + 2L);
			base.ReadImage(dibBits, size - 4U);
		}

		// Token: 0x04003E83 RID: 16003
		private ushort _hotspotx;

		// Token: 0x04003E84 RID: 16004
		private ushort _hotspoty;
	}
}
