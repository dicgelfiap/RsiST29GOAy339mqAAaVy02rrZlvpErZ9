using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D31 RID: 3377
	[ComVisible(true)]
	public abstract class Resource
	{
		// Token: 0x17001D30 RID: 7472
		// (get) Token: 0x0600891D RID: 35101 RVA: 0x002940BC File Offset: 0x002940BC
		public int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17001D31 RID: 7473
		// (get) Token: 0x0600891E RID: 35102 RVA: 0x002940C4 File Offset: 0x002940C4
		// (set) Token: 0x0600891F RID: 35103 RVA: 0x002940CC File Offset: 0x002940CC
		public ushort Language
		{
			get
			{
				return this._language;
			}
			set
			{
				this._language = value;
			}
		}

		// Token: 0x17001D32 RID: 7474
		// (get) Token: 0x06008920 RID: 35104 RVA: 0x002940D8 File Offset: 0x002940D8
		public ResourceId Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17001D33 RID: 7475
		// (get) Token: 0x06008921 RID: 35105 RVA: 0x002940E0 File Offset: 0x002940E0
		public string TypeName
		{
			get
			{
				return this._type.TypeName;
			}
		}

		// Token: 0x17001D34 RID: 7476
		// (get) Token: 0x06008922 RID: 35106 RVA: 0x002940F0 File Offset: 0x002940F0
		// (set) Token: 0x06008923 RID: 35107 RVA: 0x002940F8 File Offset: 0x002940F8
		public ResourceId Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x06008924 RID: 35108 RVA: 0x00294104 File Offset: 0x00294104
		internal Resource()
		{
		}

		// Token: 0x06008925 RID: 35109 RVA: 0x00294124 File Offset: 0x00294124
		internal Resource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, ushort language, int size)
		{
			this._hModule = hModule;
			this._type = type;
			this._name = name;
			this._language = language;
			this._hResource = hResource;
			this._size = size;
			this.LockAndReadResource(hModule, hResource);
		}

		// Token: 0x06008926 RID: 35110 RVA: 0x00294188 File Offset: 0x00294188
		internal void LockAndReadResource(IntPtr hModule, IntPtr hResource)
		{
			if (hResource == IntPtr.Zero)
			{
				return;
			}
			IntPtr intPtr = Kernel32.LockResource(hResource);
			if (intPtr == IntPtr.Zero)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			using (Aligned aligned = new Aligned(intPtr, this._size))
			{
				this.Read(hModule, aligned.Ptr);
			}
		}

		// Token: 0x06008927 RID: 35111 RVA: 0x00294204 File Offset: 0x00294204
		public virtual void LoadFrom(string filename)
		{
			this.LoadFrom(filename, this._type, this._name, this._language);
		}

		// Token: 0x06008928 RID: 35112 RVA: 0x00294220 File Offset: 0x00294220
		internal void LoadFrom(string filename, ResourceId type, ResourceId name, ushort lang)
		{
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Kernel32.LoadLibraryEx(filename, IntPtr.Zero, 3U);
				this.LoadFrom(intPtr, type, name, lang);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Kernel32.FreeLibrary(intPtr);
				}
			}
		}

		// Token: 0x06008929 RID: 35113 RVA: 0x00294278 File Offset: 0x00294278
		internal void LoadFrom(IntPtr hModule, ResourceId type, ResourceId name, ushort lang)
		{
			if (IntPtr.Zero == hModule)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			IntPtr intPtr = Kernel32.FindResourceEx(hModule, type.Id, name.Id, lang);
			if (IntPtr.Zero == intPtr)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			IntPtr intPtr2 = Kernel32.LoadResource(hModule, intPtr);
			if (IntPtr.Zero == intPtr2)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			IntPtr intPtr3 = Kernel32.LockResource(intPtr2);
			if (intPtr3 == IntPtr.Zero)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			this._size = Kernel32.SizeofResource(hModule, intPtr);
			if (this._size <= 0)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			using (Aligned aligned = new Aligned(intPtr3, this._size))
			{
				this._type = type;
				this._name = name;
				this._language = lang;
				this.Read(hModule, aligned.Ptr);
			}
		}

		// Token: 0x0600892A RID: 35114
		internal abstract IntPtr Read(IntPtr hModule, IntPtr lpRes);

		// Token: 0x0600892B RID: 35115
		internal abstract void Write(BinaryWriter w);

		// Token: 0x0600892C RID: 35116 RVA: 0x0029438C File Offset: 0x0029438C
		public byte[] WriteAndGetBytes()
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream, Encoding.Default);
			this.Write(binaryWriter);
			binaryWriter.Close();
			return memoryStream.ToArray();
		}

		// Token: 0x0600892D RID: 35117 RVA: 0x002943C0 File Offset: 0x002943C0
		public virtual void SaveTo(string filename)
		{
			this.SaveTo(filename, this._type, this._name, this._language);
		}

		// Token: 0x0600892E RID: 35118 RVA: 0x002943DC File Offset: 0x002943DC
		internal void SaveTo(string filename, ResourceId type, ResourceId name, ushort langid)
		{
			byte[] data = this.WriteAndGetBytes();
			Resource.SaveTo(filename, type, name, langid, data);
		}

		// Token: 0x0600892F RID: 35119 RVA: 0x00294400 File Offset: 0x00294400
		public virtual void DeleteFrom(string filename)
		{
			Resource.Delete(filename, this._type, this._name, this._language);
		}

		// Token: 0x06008930 RID: 35120 RVA: 0x0029441C File Offset: 0x0029441C
		internal static void Delete(string filename, ResourceId type, ResourceId name, ushort lang)
		{
			Resource.SaveTo(filename, type, name, lang, null);
		}

		// Token: 0x06008931 RID: 35121 RVA: 0x00294428 File Offset: 0x00294428
		internal static void SaveTo(string filename, ResourceId type, ResourceId name, ushort lang, byte[] data)
		{
			IntPtr intPtr = Kernel32.BeginUpdateResource(filename, false);
			if (intPtr == IntPtr.Zero)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			try
			{
				if (data != null && data.Length == 0)
				{
					data = null;
				}
				if (!Kernel32.UpdateResource(intPtr, type.Id, name.Id, lang, data, (uint)((data == null) ? 0 : data.Length)))
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
			}
			catch
			{
				Kernel32.EndUpdateResource(intPtr, true);
				throw;
			}
			if (!Kernel32.EndUpdateResource(intPtr, false))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x06008932 RID: 35122 RVA: 0x002944D8 File Offset: 0x002944D8
		public static void Save(string filename, IEnumerable<Resource> resources)
		{
			IntPtr intPtr = Kernel32.BeginUpdateResource(filename, false);
			if (intPtr == IntPtr.Zero)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			try
			{
				foreach (Resource resource in resources)
				{
					IconImageResource iconImageResource = resource as IconImageResource;
					if (iconImageResource != null)
					{
						byte[] array = (iconImageResource.Image == null) ? null : iconImageResource.Image.Data;
						if (!Kernel32.UpdateResource(intPtr, iconImageResource.Type.Id, new IntPtr((int)iconImageResource.Id), iconImageResource.Language, array, (uint)((array == null) ? 0 : array.Length)))
						{
							throw new Win32Exception(Marshal.GetLastWin32Error());
						}
					}
					else
					{
						byte[] array2 = resource.WriteAndGetBytes();
						if (array2 != null && array2.Length == 0)
						{
							array2 = null;
						}
						if (!Kernel32.UpdateResource(intPtr, resource.Type.Id, resource.Name.Id, resource.Language, array2, (uint)((array2 == null) ? 0 : array2.Length)))
						{
							throw new Win32Exception(Marshal.GetLastWin32Error());
						}
					}
				}
			}
			catch
			{
				Kernel32.EndUpdateResource(intPtr, true);
				throw;
			}
			if (!Kernel32.EndUpdateResource(intPtr, false))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x04003EC7 RID: 16071
		protected ResourceId _type;

		// Token: 0x04003EC8 RID: 16072
		protected ResourceId _name;

		// Token: 0x04003EC9 RID: 16073
		protected ushort _language;

		// Token: 0x04003ECA RID: 16074
		protected IntPtr _hModule = IntPtr.Zero;

		// Token: 0x04003ECB RID: 16075
		protected IntPtr _hResource = IntPtr.Zero;

		// Token: 0x04003ECC RID: 16076
		protected int _size;
	}
}
