using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D3D RID: 3389
	[ComVisible(true)]
	public class VersionResource : Resource
	{
		// Token: 0x17001D54 RID: 7508
		// (get) Token: 0x060089B1 RID: 35249 RVA: 0x00295FF4 File Offset: 0x00295FF4
		public ResourceTableHeader Header
		{
			get
			{
				return this._header;
			}
		}

		// Token: 0x17001D55 RID: 7509
		// (get) Token: 0x060089B2 RID: 35250 RVA: 0x00295FFC File Offset: 0x00295FFC
		public OrderedDictionary Resources
		{
			get
			{
				return this._resources;
			}
		}

		// Token: 0x060089B3 RID: 35251 RVA: 0x00296004 File Offset: 0x00296004
		public VersionResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, ushort language, int size) : base(hModule, hResource, type, name, language, size)
		{
		}

		// Token: 0x060089B4 RID: 35252 RVA: 0x0029603C File Offset: 0x0029603C
		public VersionResource() : base(IntPtr.Zero, IntPtr.Zero, new ResourceId(Kernel32.ResourceTypes.RT_VERSION), new ResourceId(1U), ResourceUtil.USENGLISHLANGID, 0)
		{
			this._header.Header = new Kernel32.RESOURCE_HEADER(this._fixedfileinfo.Size);
		}

		// Token: 0x060089B5 RID: 35253 RVA: 0x002960B4 File Offset: 0x002960B4
		internal override IntPtr Read(IntPtr hModule, IntPtr lpRes)
		{
			this._resources.Clear();
			IntPtr lpRes2 = this._header.Read(lpRes);
			if (this._header.Header.wValueLength != 0)
			{
				this._fixedfileinfo = new FixedFileInfo();
				this._fixedfileinfo.Read(lpRes2);
			}
			IntPtr lpRes3 = ResourceUtil.Align(lpRes2.ToInt64() + (long)((ulong)this._header.Header.wValueLength));
			while (lpRes3.ToInt64() < lpRes.ToInt64() + (long)((ulong)this._header.Header.wLength))
			{
				ResourceTableHeader resourceTableHeader = new ResourceTableHeader(lpRes3);
				string key = resourceTableHeader.Key;
				if (key == "StringFileInfo")
				{
					resourceTableHeader = new StringFileInfo(lpRes3);
				}
				else
				{
					resourceTableHeader = new VarFileInfo(lpRes3);
				}
				this._resources.Add(resourceTableHeader.Key, resourceTableHeader);
				lpRes3 = ResourceUtil.Align(lpRes3.ToInt64() + (long)((ulong)resourceTableHeader.Header.wLength));
			}
			return new IntPtr(lpRes.ToInt64() + (long)((ulong)this._header.Header.wLength));
		}

		// Token: 0x17001D56 RID: 7510
		// (get) Token: 0x060089B6 RID: 35254 RVA: 0x002961CC File Offset: 0x002961CC
		// (set) Token: 0x060089B7 RID: 35255 RVA: 0x002961DC File Offset: 0x002961DC
		public string FileVersion
		{
			get
			{
				return this._fixedfileinfo.FileVersion;
			}
			set
			{
				this._fixedfileinfo.FileVersion = value;
			}
		}

		// Token: 0x17001D57 RID: 7511
		// (get) Token: 0x060089B8 RID: 35256 RVA: 0x002961EC File Offset: 0x002961EC
		// (set) Token: 0x060089B9 RID: 35257 RVA: 0x002961FC File Offset: 0x002961FC
		public uint FileFlags
		{
			get
			{
				return this._fixedfileinfo.FileFlags;
			}
			set
			{
				this._fixedfileinfo.FileFlags = value;
			}
		}

		// Token: 0x17001D58 RID: 7512
		// (get) Token: 0x060089BA RID: 35258 RVA: 0x0029620C File Offset: 0x0029620C
		// (set) Token: 0x060089BB RID: 35259 RVA: 0x0029621C File Offset: 0x0029621C
		public string ProductVersion
		{
			get
			{
				return this._fixedfileinfo.ProductVersion;
			}
			set
			{
				this._fixedfileinfo.ProductVersion = value;
			}
		}

		// Token: 0x060089BC RID: 35260 RVA: 0x0029622C File Offset: 0x0029622C
		internal override void Write(BinaryWriter w)
		{
			long position = w.BaseStream.Position;
			this._header.Write(w);
			if (this._fixedfileinfo != null)
			{
				this._fixedfileinfo.Write(w);
			}
			foreach (object obj in this._resources)
			{
				((ResourceTableHeader)((DictionaryEntry)obj).Value).Write(w);
			}
			ResourceUtil.WriteAt(w, w.BaseStream.Position - position, position);
		}

		// Token: 0x17001D59 RID: 7513
		public ResourceTableHeader this[string key]
		{
			get
			{
				return (ResourceTableHeader)this.Resources[key];
			}
			set
			{
				this.Resources[key] = value;
			}
		}

		// Token: 0x17001D5A RID: 7514
		public ResourceTableHeader this[int index]
		{
			get
			{
				return (ResourceTableHeader)this.Resources[index];
			}
			set
			{
				this.Resources[index] = value;
			}
		}

		// Token: 0x060089C1 RID: 35265 RVA: 0x00296324 File Offset: 0x00296324
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this._fixedfileinfo != null)
			{
				stringBuilder.Append(this._fixedfileinfo.ToString());
			}
			stringBuilder.AppendLine("BEGIN");
			foreach (object obj in this._resources)
			{
				stringBuilder.Append(((ResourceTableHeader)((DictionaryEntry)obj).Value).ToString(1));
			}
			stringBuilder.AppendLine("END");
			return stringBuilder.ToString();
		}

		// Token: 0x04003EDD RID: 16093
		private ResourceTableHeader _header = new ResourceTableHeader("VS_VERSION_INFO");

		// Token: 0x04003EDE RID: 16094
		private FixedFileInfo _fixedfileinfo = new FixedFileInfo();

		// Token: 0x04003EDF RID: 16095
		private OrderedDictionary _resources = new OrderedDictionary();
	}
}
