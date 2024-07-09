using System;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D22 RID: 3362
	[ComVisible(true)]
	public abstract class Kernel32
	{
		// Token: 0x060088BC RID: 35004
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "LoadLibraryExW", SetLastError = true)]
		internal static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, uint dwFlags);

		// Token: 0x060088BD RID: 35005
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool FreeLibrary(IntPtr hModule);

		// Token: 0x060088BE RID: 35006
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "EnumResourceTypesW", SetLastError = true)]
		internal static extern bool EnumResourceTypes(IntPtr hModule, Kernel32.EnumResourceTypesDelegate lpEnumFunc, IntPtr lParam);

		// Token: 0x060088BF RID: 35007
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "EnumResourceNamesW", SetLastError = true)]
		internal static extern bool EnumResourceNames(IntPtr hModule, IntPtr lpszType, Kernel32.EnumResourceNamesDelegate lpEnumFunc, IntPtr lParam);

		// Token: 0x060088C0 RID: 35008
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "EnumResourceLanguagesW", SetLastError = true)]
		internal static extern bool EnumResourceLanguages(IntPtr hModule, IntPtr lpszType, IntPtr lpszName, Kernel32.EnumResourceLanguagesDelegate lpEnumFunc, IntPtr lParam);

		// Token: 0x060088C1 RID: 35009
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "FindResourceExW", SetLastError = true)]
		internal static extern IntPtr FindResourceEx(IntPtr hModule, IntPtr lpszType, IntPtr lpszName, ushort wLanguage);

		// Token: 0x060088C2 RID: 35010
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr LockResource(IntPtr hResData);

		// Token: 0x060088C3 RID: 35011
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr LoadResource(IntPtr hModule, IntPtr hResData);

		// Token: 0x060088C4 RID: 35012
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern int SizeofResource(IntPtr hInstance, IntPtr hResInfo);

		// Token: 0x060088C5 RID: 35013
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool CloseHandle(IntPtr hHandle);

		// Token: 0x060088C6 RID: 35014
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "BeginUpdateResourceW", ExactSpelling = true, SetLastError = true)]
		internal static extern IntPtr BeginUpdateResource(string pFileName, bool bDeleteExistingResources);

		// Token: 0x060088C7 RID: 35015
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "UpdateResourceW", ExactSpelling = true, SetLastError = true)]
		internal static extern bool UpdateResource(IntPtr hUpdate, IntPtr lpType, IntPtr lpName, ushort wLanguage, byte[] lpData, uint cbData);

		// Token: 0x060088C8 RID: 35016
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "EndUpdateResourceW", ExactSpelling = true, SetLastError = true)]
		internal static extern bool EndUpdateResource(IntPtr hUpdate, bool fDiscard);

		// Token: 0x060088C9 RID: 35017
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "RtlMoveMemory", ExactSpelling = true, SetLastError = true)]
		internal static extern void MoveMemory(IntPtr dest, IntPtr src, uint count);

		// Token: 0x04003EAB RID: 16043
		internal const uint LOAD_LIBRARY_AS_DATAFILE = 2U;

		// Token: 0x04003EAC RID: 16044
		internal const uint DONT_RESOLVE_DLL_REFERENCES = 1U;

		// Token: 0x04003EAD RID: 16045
		internal const uint LOAD_WITH_ALTERED_SEARCH_PATH = 8U;

		// Token: 0x04003EAE RID: 16046
		internal const uint LOAD_IGNORE_CODE_AUTHZ_LEVEL = 16U;

		// Token: 0x04003EAF RID: 16047
		public const ushort LANG_NEUTRAL = 0;

		// Token: 0x04003EB0 RID: 16048
		public const ushort LANG_ENGLISH = 9;

		// Token: 0x04003EB1 RID: 16049
		public const ushort SUBLANG_NEUTRAL = 0;

		// Token: 0x04003EB2 RID: 16050
		public const ushort SUBLANG_ENGLISH_US = 1;

		// Token: 0x04003EB3 RID: 16051
		public const ushort CREATEPROCESS_MANIFEST_RESOURCE_ID = 1;

		// Token: 0x04003EB4 RID: 16052
		public const ushort ISOLATIONAWARE_MANIFEST_RESOURCE_ID = 2;

		// Token: 0x04003EB5 RID: 16053
		public const ushort ISOLATIONAWARE_NOSTATICIMPORT_MANIFEST_RESOURCE_ID = 3;

		// Token: 0x020011E0 RID: 4576
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct RESOURCE_HEADER
		{
			// Token: 0x0600967F RID: 38527 RVA: 0x002CC52C File Offset: 0x002CC52C
			public RESOURCE_HEADER(ushort valueLength)
			{
				this.wLength = 0;
				this.wValueLength = valueLength;
				this.wType = 0;
			}

			// Token: 0x04004CC0 RID: 19648
			public ushort wLength;

			// Token: 0x04004CC1 RID: 19649
			public ushort wValueLength;

			// Token: 0x04004CC2 RID: 19650
			public ushort wType;
		}

		// Token: 0x020011E1 RID: 4577
		public enum RESOURCE_HEADER_TYPE
		{
			// Token: 0x04004CC4 RID: 19652
			BinaryData,
			// Token: 0x04004CC5 RID: 19653
			StringData
		}

		// Token: 0x020011E2 RID: 4578
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct VAR_HEADER
		{
			// Token: 0x04004CC6 RID: 19654
			public ushort wLanguageIDMS;

			// Token: 0x04004CC7 RID: 19655
			public ushort wCodePageIBM;
		}

		// Token: 0x020011E3 RID: 4579
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		public struct VS_FIXEDFILEINFO
		{
			// Token: 0x06009680 RID: 38528 RVA: 0x002CC544 File Offset: 0x002CC544
			public static Kernel32.VS_FIXEDFILEINFO GetWindowsDefault()
			{
				return new Kernel32.VS_FIXEDFILEINFO
				{
					dwSignature = 4277077181U,
					dwStrucVersion = 65536U,
					dwFileFlagsMask = 63U,
					dwFileOS = 4U,
					dwFileSubtype = 0U,
					dwFileType = 2U
				};
			}

			// Token: 0x04004CC8 RID: 19656
			public uint dwSignature;

			// Token: 0x04004CC9 RID: 19657
			public uint dwStrucVersion;

			// Token: 0x04004CCA RID: 19658
			public uint dwFileVersionMS;

			// Token: 0x04004CCB RID: 19659
			public uint dwFileVersionLS;

			// Token: 0x04004CCC RID: 19660
			public uint dwProductVersionMS;

			// Token: 0x04004CCD RID: 19661
			public uint dwProductVersionLS;

			// Token: 0x04004CCE RID: 19662
			public uint dwFileFlagsMask;

			// Token: 0x04004CCF RID: 19663
			public uint dwFileFlags;

			// Token: 0x04004CD0 RID: 19664
			public uint dwFileOS;

			// Token: 0x04004CD1 RID: 19665
			public uint dwFileType;

			// Token: 0x04004CD2 RID: 19666
			public uint dwFileSubtype;

			// Token: 0x04004CD3 RID: 19667
			public uint dwFileDateMS;

			// Token: 0x04004CD4 RID: 19668
			public uint dwFileDateLS;
		}

		// Token: 0x020011E4 RID: 4580
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		public struct GRPICONDIR
		{
			// Token: 0x04004CD5 RID: 19669
			public ushort wReserved;

			// Token: 0x04004CD6 RID: 19670
			public ushort wType;

			// Token: 0x04004CD7 RID: 19671
			public ushort wImageCount;
		}

		// Token: 0x020011E5 RID: 4581
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		public struct GRPICONDIRENTRY
		{
			// Token: 0x04004CD8 RID: 19672
			public byte bWidth;

			// Token: 0x04004CD9 RID: 19673
			public byte bHeight;

			// Token: 0x04004CDA RID: 19674
			public byte bColors;

			// Token: 0x04004CDB RID: 19675
			public byte bReserved;

			// Token: 0x04004CDC RID: 19676
			public ushort wPlanes;

			// Token: 0x04004CDD RID: 19677
			public ushort wBitsPerPixel;

			// Token: 0x04004CDE RID: 19678
			public uint dwImageSize;

			// Token: 0x04004CDF RID: 19679
			public ushort nID;
		}

		// Token: 0x020011E6 RID: 4582
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		public struct FILEGRPICONDIRENTRY
		{
			// Token: 0x04004CE0 RID: 19680
			public byte bWidth;

			// Token: 0x04004CE1 RID: 19681
			public byte bHeight;

			// Token: 0x04004CE2 RID: 19682
			public byte bColors;

			// Token: 0x04004CE3 RID: 19683
			public byte bReserved;

			// Token: 0x04004CE4 RID: 19684
			public ushort wPlanes;

			// Token: 0x04004CE5 RID: 19685
			public ushort wBitsPerPixel;

			// Token: 0x04004CE6 RID: 19686
			public uint dwImageSize;

			// Token: 0x04004CE7 RID: 19687
			public uint dwFileOffset;
		}

		// Token: 0x020011E7 RID: 4583
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		public struct FILEGRPICONDIR
		{
			// Token: 0x04004CE8 RID: 19688
			public ushort wReserved;

			// Token: 0x04004CE9 RID: 19689
			public ushort wType;

			// Token: 0x04004CEA RID: 19690
			public ushort wCount;
		}

		// Token: 0x020011E8 RID: 4584
		public enum ResourceTypes
		{
			// Token: 0x04004CEC RID: 19692
			RT_CURSOR = 1,
			// Token: 0x04004CED RID: 19693
			RT_BITMAP,
			// Token: 0x04004CEE RID: 19694
			RT_ICON,
			// Token: 0x04004CEF RID: 19695
			RT_MENU,
			// Token: 0x04004CF0 RID: 19696
			RT_DIALOG,
			// Token: 0x04004CF1 RID: 19697
			RT_STRING,
			// Token: 0x04004CF2 RID: 19698
			RT_FONTDIR,
			// Token: 0x04004CF3 RID: 19699
			RT_FONT,
			// Token: 0x04004CF4 RID: 19700
			RT_ACCELERATOR,
			// Token: 0x04004CF5 RID: 19701
			RT_RCDATA,
			// Token: 0x04004CF6 RID: 19702
			RT_MESSAGETABLE,
			// Token: 0x04004CF7 RID: 19703
			RT_GROUP_CURSOR,
			// Token: 0x04004CF8 RID: 19704
			RT_GROUP_ICON = 14,
			// Token: 0x04004CF9 RID: 19705
			RT_VERSION = 16,
			// Token: 0x04004CFA RID: 19706
			RT_DLGINCLUDE,
			// Token: 0x04004CFB RID: 19707
			RT_PLUGPLAY = 19,
			// Token: 0x04004CFC RID: 19708
			RT_VXD,
			// Token: 0x04004CFD RID: 19709
			RT_ANICURSOR,
			// Token: 0x04004CFE RID: 19710
			RT_ANIICON,
			// Token: 0x04004CFF RID: 19711
			RT_HTML,
			// Token: 0x04004D00 RID: 19712
			RT_MANIFEST
		}

		// Token: 0x020011E9 RID: 4585
		// (Invoke) Token: 0x06009682 RID: 38530
		internal delegate bool EnumResourceTypesDelegate(IntPtr hModule, IntPtr lpszType, IntPtr lParam);

		// Token: 0x020011EA RID: 4586
		// (Invoke) Token: 0x06009686 RID: 38534
		internal delegate bool EnumResourceNamesDelegate(IntPtr hModule, IntPtr lpszType, IntPtr lpszName, IntPtr lParam);

		// Token: 0x020011EB RID: 4587
		// (Invoke) Token: 0x0600968A RID: 38538
		internal delegate bool EnumResourceLanguagesDelegate(IntPtr hModule, IntPtr lpszType, IntPtr lpszName, ushort wIDLanguage, IntPtr lParam);

		// Token: 0x020011EC RID: 4588
		public enum ManifestType
		{
			// Token: 0x04004D02 RID: 19714
			CreateProcess = 1,
			// Token: 0x04004D03 RID: 19715
			IsolationAware,
			// Token: 0x04004D04 RID: 19716
			IsolationAwareNonstaticImport
		}
	}
}
