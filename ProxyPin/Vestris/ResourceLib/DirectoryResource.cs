using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D16 RID: 3350
	[ComVisible(true)]
	public class DirectoryResource<ImageResourceType> : Resource where ImageResourceType : IconImageResource, new()
	{
		// Token: 0x17001D06 RID: 7430
		// (get) Token: 0x06008856 RID: 34902 RVA: 0x00291D50 File Offset: 0x00291D50
		public Kernel32.ResourceTypes ResourceType
		{
			get
			{
				ushort wType = this._header.wType;
				if (wType == 1)
				{
					return Kernel32.ResourceTypes.RT_ICON;
				}
				if (wType != 2)
				{
					throw new NotSupportedException();
				}
				return Kernel32.ResourceTypes.RT_CURSOR;
			}
		}

		// Token: 0x17001D07 RID: 7431
		// (get) Token: 0x06008857 RID: 34903 RVA: 0x00291D8C File Offset: 0x00291D8C
		// (set) Token: 0x06008858 RID: 34904 RVA: 0x00291D94 File Offset: 0x00291D94
		public List<ImageResourceType> Icons
		{
			get
			{
				return this._icons;
			}
			set
			{
				this._icons = value;
			}
		}

		// Token: 0x06008859 RID: 34905 RVA: 0x00291DA0 File Offset: 0x00291DA0
		internal DirectoryResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, ushort language, int size) : base(hModule, hResource, type, name, language, size)
		{
		}

		// Token: 0x0600885A RID: 34906 RVA: 0x00291DBC File Offset: 0x00291DBC
		public DirectoryResource(Kernel32.ResourceTypes resourceType) : base(IntPtr.Zero, IntPtr.Zero, new ResourceId(resourceType), new ResourceId(1U), ResourceUtil.NEUTRALLANGID, Marshal.SizeOf(typeof(Kernel32.GRPICONDIR)))
		{
			if (resourceType == Kernel32.ResourceTypes.RT_GROUP_CURSOR)
			{
				this._header.wType = 2;
				return;
			}
			if (resourceType != Kernel32.ResourceTypes.RT_GROUP_ICON)
			{
				throw new NotSupportedException();
			}
			this._header.wType = 1;
		}

		// Token: 0x0600885B RID: 34907 RVA: 0x00291E3C File Offset: 0x00291E3C
		public override void SaveTo(string filename)
		{
			base.SaveTo(filename);
			foreach (ImageResourceType imageResourceType in this._icons)
			{
				imageResourceType.SaveIconTo(filename);
			}
		}

		// Token: 0x0600885C RID: 34908 RVA: 0x00291EA0 File Offset: 0x00291EA0
		internal override IntPtr Read(IntPtr hModule, IntPtr lpRes)
		{
			this._icons.Clear();
			this._header = (Kernel32.GRPICONDIR)Marshal.PtrToStructure(lpRes, typeof(Kernel32.GRPICONDIR));
			IntPtr intPtr = new IntPtr(lpRes.ToInt64() + (long)Marshal.SizeOf(this._header));
			for (ushort num = 0; num < this._header.wImageCount; num += 1)
			{
				ImageResourceType imageResourceType = Activator.CreateInstance<ImageResourceType>();
				intPtr = imageResourceType.Read(hModule, intPtr);
				this._icons.Add(imageResourceType);
			}
			return intPtr;
		}

		// Token: 0x0600885D RID: 34909 RVA: 0x00291F34 File Offset: 0x00291F34
		internal override void Write(BinaryWriter w)
		{
			w.Write(this._header.wReserved);
			w.Write(this._header.wType);
			w.Write((ushort)this._icons.Count);
			ResourceUtil.PadToWORD(w);
			foreach (ImageResourceType imageResourceType in this._icons)
			{
				imageResourceType.Write(w);
			}
		}

		// Token: 0x04003E9B RID: 16027
		private Kernel32.GRPICONDIR _header;

		// Token: 0x04003E9C RID: 16028
		private List<ImageResourceType> _icons = new List<ImageResourceType>();
	}
}
