using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D33 RID: 3379
	[ComVisible(true)]
	public class ResourceInfo : IEnumerable<Resource>, IEnumerable, IDisposable
	{
		// Token: 0x17001D39 RID: 7481
		// (get) Token: 0x06008943 RID: 35139 RVA: 0x00294854 File Offset: 0x00294854
		public Dictionary<ResourceId, List<Resource>> Resources
		{
			get
			{
				return this._resources;
			}
		}

		// Token: 0x17001D3A RID: 7482
		// (get) Token: 0x06008944 RID: 35140 RVA: 0x0029485C File Offset: 0x0029485C
		public List<ResourceId> ResourceTypes
		{
			get
			{
				return this._resourceTypes;
			}
		}

		// Token: 0x06008946 RID: 35142 RVA: 0x00294878 File Offset: 0x00294878
		public void Unload()
		{
			if (this._hModule != IntPtr.Zero)
			{
				Kernel32.FreeLibrary(this._hModule);
				this._hModule = IntPtr.Zero;
			}
			this._innerException = null;
		}

		// Token: 0x06008947 RID: 35143 RVA: 0x002948B0 File Offset: 0x002948B0
		public void Load(string filename)
		{
			this.Unload();
			this._resourceTypes = new List<ResourceId>();
			this._resources = new Dictionary<ResourceId, List<Resource>>();
			this._hModule = Kernel32.LoadLibraryEx(filename, IntPtr.Zero, 3U);
			if (IntPtr.Zero == this._hModule)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			try
			{
				if (!Kernel32.EnumResourceTypes(this._hModule, new Kernel32.EnumResourceTypesDelegate(this.EnumResourceTypesImpl), IntPtr.Zero))
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
			}
			catch (Exception outerException)
			{
				throw new LoadException(string.Format("Error loading '{0}'.", filename), this._innerException, outerException);
			}
		}

		// Token: 0x06008948 RID: 35144 RVA: 0x00294968 File Offset: 0x00294968
		private bool EnumResourceTypesImpl(IntPtr hModule, IntPtr lpszType, IntPtr lParam)
		{
			ResourceId item = new ResourceId(lpszType);
			this._resourceTypes.Add(item);
			if (!Kernel32.EnumResourceNames(hModule, lpszType, new Kernel32.EnumResourceNamesDelegate(this.EnumResourceNamesImpl), IntPtr.Zero))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			return true;
		}

		// Token: 0x06008949 RID: 35145 RVA: 0x002949B8 File Offset: 0x002949B8
		private bool EnumResourceNamesImpl(IntPtr hModule, IntPtr lpszType, IntPtr lpszName, IntPtr lParam)
		{
			if (!Kernel32.EnumResourceLanguages(hModule, lpszType, lpszName, new Kernel32.EnumResourceLanguagesDelegate(this.EnumResourceLanguages), IntPtr.Zero))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			return true;
		}

		// Token: 0x0600894A RID: 35146 RVA: 0x002949E4 File Offset: 0x002949E4
		protected Resource CreateResource(IntPtr hModule, IntPtr hResourceGlobal, ResourceId type, ResourceId name, ushort wIDLanguage, int size)
		{
			if (type.IsIntResource())
			{
				Kernel32.ResourceTypes resourceType = type.ResourceType;
				switch (resourceType)
				{
				case Kernel32.ResourceTypes.RT_BITMAP:
					return new BitmapResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
				case Kernel32.ResourceTypes.RT_ICON:
				case Kernel32.ResourceTypes.RT_RCDATA:
				case Kernel32.ResourceTypes.RT_MESSAGETABLE:
				case (Kernel32.ResourceTypes)13:
				case (Kernel32.ResourceTypes)15:
					break;
				case Kernel32.ResourceTypes.RT_MENU:
					return new MenuResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
				case Kernel32.ResourceTypes.RT_DIALOG:
					return new DialogResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
				case Kernel32.ResourceTypes.RT_STRING:
					return new StringResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
				case Kernel32.ResourceTypes.RT_FONTDIR:
					return new FontDirectoryResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
				case Kernel32.ResourceTypes.RT_FONT:
					return new FontResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
				case Kernel32.ResourceTypes.RT_ACCELERATOR:
					return new AcceleratorResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
				case Kernel32.ResourceTypes.RT_GROUP_CURSOR:
					return new CursorDirectoryResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
				case Kernel32.ResourceTypes.RT_GROUP_ICON:
					return new IconDirectoryResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
				case Kernel32.ResourceTypes.RT_VERSION:
					return new VersionResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
				default:
					if (resourceType == Kernel32.ResourceTypes.RT_MANIFEST)
					{
						return new ManifestResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
					}
					break;
				}
			}
			return new GenericResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
		}

		// Token: 0x0600894B RID: 35147 RVA: 0x00294B0C File Offset: 0x00294B0C
		private bool EnumResourceLanguages(IntPtr hModule, IntPtr lpszType, IntPtr lpszName, ushort wIDLanguage, IntPtr lParam)
		{
			List<Resource> list = null;
			ResourceId resourceId = new ResourceId(lpszType);
			if (!this._resources.TryGetValue(resourceId, out list))
			{
				list = new List<Resource>();
				this._resources[resourceId] = list;
			}
			ResourceId resourceId2 = new ResourceId(lpszName);
			IntPtr intPtr = Kernel32.FindResourceEx(hModule, lpszType, lpszName, wIDLanguage);
			IntPtr lp = Kernel32.LoadResource(hModule, intPtr);
			int size = Kernel32.SizeofResource(hModule, intPtr);
			using (Aligned aligned = new Aligned(lp, size))
			{
				try
				{
					list.Add(this.CreateResource(hModule, aligned.Ptr, resourceId, resourceId2, wIDLanguage, size));
				}
				catch (Exception ex)
				{
					this._innerException = new Exception(string.Format("Error loading resource '{0}' {1} ({2}).", resourceId2, resourceId.TypeName, wIDLanguage), ex);
					throw ex;
				}
			}
			return true;
		}

		// Token: 0x0600894C RID: 35148 RVA: 0x00294BE8 File Offset: 0x00294BE8
		public void Save(string filename)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600894D RID: 35149 RVA: 0x00294BF0 File Offset: 0x00294BF0
		public void Dispose()
		{
			this.Unload();
		}

		// Token: 0x17001D3B RID: 7483
		public List<Resource> this[Kernel32.ResourceTypes type]
		{
			get
			{
				return this._resources[new ResourceId(type)];
			}
			set
			{
				this._resources[new ResourceId(type)] = value;
			}
		}

		// Token: 0x17001D3C RID: 7484
		public List<Resource> this[string type]
		{
			get
			{
				return this._resources[new ResourceId(type)];
			}
			set
			{
				this._resources[new ResourceId(type)] = value;
			}
		}

		// Token: 0x06008952 RID: 35154 RVA: 0x00294C48 File Offset: 0x00294C48
		public IEnumerator<Resource> GetEnumerator()
		{
			foreach (KeyValuePair<ResourceId, List<Resource>> keyValuePair in this._resources)
			{
				foreach (Resource resource in keyValuePair.Value)
				{
					yield return resource;
				}
				List<Resource>.Enumerator resourceEnumerator = default(List<Resource>.Enumerator);
			}
			yield break;
		}

		// Token: 0x06008953 RID: 35155 RVA: 0x00294C58 File Offset: 0x00294C58
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04003ECE RID: 16078
		private Exception _innerException;

		// Token: 0x04003ECF RID: 16079
		private IntPtr _hModule = IntPtr.Zero;

		// Token: 0x04003ED0 RID: 16080
		private Dictionary<ResourceId, List<Resource>> _resources;

		// Token: 0x04003ED1 RID: 16081
		private List<ResourceId> _resourceTypes;
	}
}
