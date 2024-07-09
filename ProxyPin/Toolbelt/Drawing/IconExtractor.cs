using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Toolbelt.Drawing.Win32;

namespace Toolbelt.Drawing
{
	// Token: 0x020000BC RID: 188
	public class IconExtractor
	{
		// Token: 0x060007B8 RID: 1976 RVA: 0x0003FF04 File Offset: 0x0003FF04
		public static void Extract1stIconTo(string sourceFile, Stream stream)
		{
			IntPtr hModule = Kernel32.LoadLibraryEx(sourceFile, IntPtr.Zero, LOAD_LIBRARY.AS_DATAFILE);
			try
			{
				Kernel32.EnumResourceNames(hModule, RT.GROUP_ICON, delegate(IntPtr _hModule, RT type, IntPtr lpszName, IntPtr lParam)
				{
					ICONRESINF[] iconResInfo = IconExtractor.GetIconResInfo(_hModule, lpszName);
					IconExtractor.WriteIconData(hModule, iconResInfo, stream);
					return false;
				}, IntPtr.Zero);
			}
			finally
			{
				Kernel32.FreeLibrary(hModule);
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0003FF74 File Offset: 0x0003FF74
		private static ICONRESINF[] GetIconResInfo(IntPtr hModule, IntPtr lpszName)
		{
			IntPtr hResInfo = Kernel32.FindResource(hModule, lpszName, RT.GROUP_ICON);
			IntPtr hResource = Kernel32.LoadResource(hModule, hResInfo);
			IntPtr ptrResource = Kernel32.LockResource(hResource);
			ICONRESHEAD iconreshead = (ICONRESHEAD)Marshal.PtrToStructure(ptrResource, typeof(ICONRESHEAD));
			int s1 = Marshal.SizeOf(typeof(ICONRESHEAD));
			int s2 = Marshal.SizeOf(typeof(ICONRESINF));
			return (from i in Enumerable.Range(0, (int)iconreshead.Count)
			select (ICONRESINF)Marshal.PtrToStructure(ptrResource + s1 + s2 * i, typeof(ICONRESINF))).ToArray<ICONRESINF>();
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00040010 File Offset: 0x00040010
		private static void WriteIconData(IntPtr hModule, ICONRESINF[] iconResInfos, Stream stream)
		{
			int num = Marshal.SizeOf(typeof(ICONFILEHEAD));
			int num2 = Marshal.SizeOf(typeof(ICONFILEINF));
			int address = num + num2 * iconResInfos.Length;
			List<IconExtractor.<>f__AnonymousType0<byte[], ICONFILEINF>> list = iconResInfos.Select(delegate(ICONRESINF iconResInf)
			{
				byte[] resourceBytes = IconExtractor.GetResourceBytes(hModule, (IntPtr)((int)iconResInf.ID), RT.ICON);
				ICONFILEINF iconFileInf = new ICONFILEINF
				{
					Cx = iconResInf.Cx,
					Cy = iconResInf.Cy,
					ColorCount = iconResInf.ColorCount,
					Planes = iconResInf.Planes,
					BitCount = iconResInf.BitCount,
					Size = iconResInf.Size,
					Address = (uint)address
				};
				address += resourceBytes.Length;
				return new IconExtractor.<>f__AnonymousType0<byte[], ICONFILEINF>(resourceBytes, iconFileInf);
			}).ToList<IconExtractor.<>f__AnonymousType0<byte[], ICONFILEINF>>();
			byte[] array = IconExtractor.StructureToBytes(new ICONFILEHEAD
			{
				Type = 1,
				Count = (ushort)iconResInfos.Length
			});
			stream.Write(array, 0, array.Length);
			list.ForEach(delegate(IconExtractor.<>f__AnonymousType0<byte[], ICONFILEINF> iconFile)
			{
				byte[] array2 = IconExtractor.StructureToBytes(iconFile.iconFileInf);
				stream.Write(array2, 0, array2.Length);
			});
			list.ForEach(delegate(IconExtractor.<>f__AnonymousType0<byte[], ICONFILEINF> iconFile)
			{
				stream.Write(iconFile.iconBytes, 0, iconFile.iconBytes.Length);
			});
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x000400D8 File Offset: 0x000400D8
		private static byte[] GetResourceBytes(IntPtr hModule, IntPtr lpszName, RT type)
		{
			IntPtr hResInfo = Kernel32.FindResource(hModule, lpszName, type);
			IntPtr source = Kernel32.LockResource(Kernel32.LoadResource(hModule, hResInfo));
			byte[] array = new byte[Kernel32.SizeofResource(hModule, hResInfo)];
			Marshal.Copy(source, array, 0, array.Length);
			return array;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00040118 File Offset: 0x00040118
		private static byte[] StructureToBytes(object obj)
		{
			byte[] array = new byte[Marshal.SizeOf(obj)];
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			Marshal.StructureToPtr(obj, gchandle.AddrOfPinnedObject(), false);
			gchandle.Free();
			return array;
		}
	}
}
