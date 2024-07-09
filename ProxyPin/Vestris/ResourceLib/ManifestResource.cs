using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D24 RID: 3364
	[ComVisible(true)]
	public class ManifestResource : Resource
	{
		// Token: 0x17001D24 RID: 7460
		// (get) Token: 0x060088CE RID: 35022 RVA: 0x002930F0 File Offset: 0x002930F0
		// (set) Token: 0x060088CF RID: 35023 RVA: 0x002931C0 File Offset: 0x002931C0
		public XmlDocument Manifest
		{
			get
			{
				if (this._manifest == null && this._data != null)
				{
					bool flag = this._data.Length >= 3 && this._data[0] == ManifestResource.utf8_bom[0] && this._data[1] == ManifestResource.utf8_bom[1] && this._data[2] == ManifestResource.utf8_bom[2];
					string @string = Encoding.UTF8.GetString(this._data, flag ? 3 : 0, flag ? (this._data.Length - 3) : this._data.Length);
					this._manifest = new XmlDocument();
					this._manifest.LoadXml(@string);
				}
				return this._manifest;
			}
			set
			{
				this._manifest = value;
				this._data = null;
				this._size = Encoding.UTF8.GetBytes(this._manifest.OuterXml).Length;
			}
		}

		// Token: 0x17001D25 RID: 7461
		// (get) Token: 0x060088D0 RID: 35024 RVA: 0x002931F0 File Offset: 0x002931F0
		// (set) Token: 0x060088D1 RID: 35025 RVA: 0x00293204 File Offset: 0x00293204
		public Kernel32.ManifestType ManifestType
		{
			get
			{
				return (Kernel32.ManifestType)((int)this._name.Id);
			}
			set
			{
				this._name = new ResourceId((IntPtr)((int)value));
			}
		}

		// Token: 0x060088D2 RID: 35026 RVA: 0x00293218 File Offset: 0x00293218
		public ManifestResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, ushort language, int size) : base(hModule, hResource, type, name, language, size)
		{
		}

		// Token: 0x060088D3 RID: 35027 RVA: 0x0029322C File Offset: 0x0029322C
		public ManifestResource() : this(Kernel32.ManifestType.CreateProcess)
		{
		}

		// Token: 0x060088D4 RID: 35028 RVA: 0x00293238 File Offset: 0x00293238
		public ManifestResource(Kernel32.ManifestType manifestType) : base(IntPtr.Zero, IntPtr.Zero, new ResourceId(Kernel32.ResourceTypes.RT_MANIFEST), new ResourceId((uint)manifestType), 0, 0)
		{
			this._manifest = new XmlDocument();
			this._manifest.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><assembly xmlns=\"urn:schemas-microsoft-com:asm.v1\" manifestVersion=\"1.0\" />");
			this._size = Encoding.UTF8.GetBytes(this._manifest.OuterXml).Length;
		}

		// Token: 0x060088D5 RID: 35029 RVA: 0x002932A0 File Offset: 0x002932A0
		internal override IntPtr Read(IntPtr hModule, IntPtr lpRes)
		{
			if (this._size > 0)
			{
				this._manifest = null;
				this._data = new byte[this._size];
				Marshal.Copy(lpRes, this._data, 0, this._data.Length);
			}
			return new IntPtr(lpRes.ToInt64() + (long)this._size);
		}

		// Token: 0x060088D6 RID: 35030 RVA: 0x00293300 File Offset: 0x00293300
		internal override void Write(BinaryWriter w)
		{
			if (this._manifest != null)
			{
				w.Write(Encoding.UTF8.GetBytes(this._manifest.OuterXml));
				return;
			}
			if (this._data != null)
			{
				w.Write(this._data);
			}
		}

		// Token: 0x060088D7 RID: 35031 RVA: 0x00293340 File Offset: 0x00293340
		public void LoadFrom(string filename, Kernel32.ManifestType manifestType)
		{
			base.LoadFrom(filename, new ResourceId(Kernel32.ResourceTypes.RT_MANIFEST), new ResourceId((uint)manifestType), 0);
		}

		// Token: 0x04003EB7 RID: 16055
		private static byte[] utf8_bom = new byte[]
		{
			239,
			187,
			191
		};

		// Token: 0x04003EB8 RID: 16056
		private byte[] _data;

		// Token: 0x04003EB9 RID: 16057
		private XmlDocument _manifest;
	}
}
