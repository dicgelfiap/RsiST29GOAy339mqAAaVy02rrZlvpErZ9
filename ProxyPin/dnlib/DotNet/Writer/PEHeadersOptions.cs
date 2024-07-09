using System;
using System.Runtime.InteropServices;
using dnlib.PE;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008CE RID: 2254
	[ComVisible(true)]
	public sealed class PEHeadersOptions
	{
		// Token: 0x06005791 RID: 22417 RVA: 0x001AC96C File Offset: 0x001AC96C
		public static uint CreateNewTimeDateStamp()
		{
			return (uint)(DateTime.UtcNow - PEHeadersOptions.Epoch).TotalSeconds;
		}

		// Token: 0x04002A06 RID: 10758
		public const DllCharacteristics DefaultDllCharacteristics = dnlib.PE.DllCharacteristics.DynamicBase | dnlib.PE.DllCharacteristics.NxCompat | dnlib.PE.DllCharacteristics.NoSeh | dnlib.PE.DllCharacteristics.TerminalServerAware;

		// Token: 0x04002A07 RID: 10759
		public const Subsystem DEFAULT_SUBSYSTEM = dnlib.PE.Subsystem.WindowsGui;

		// Token: 0x04002A08 RID: 10760
		public const byte DEFAULT_MAJOR_LINKER_VERSION = 11;

		// Token: 0x04002A09 RID: 10761
		public const byte DEFAULT_MINOR_LINKER_VERSION = 0;

		// Token: 0x04002A0A RID: 10762
		public Machine? Machine;

		// Token: 0x04002A0B RID: 10763
		public uint? TimeDateStamp;

		// Token: 0x04002A0C RID: 10764
		public uint? PointerToSymbolTable;

		// Token: 0x04002A0D RID: 10765
		public uint? NumberOfSymbols;

		// Token: 0x04002A0E RID: 10766
		public Characteristics? Characteristics;

		// Token: 0x04002A0F RID: 10767
		public byte? MajorLinkerVersion;

		// Token: 0x04002A10 RID: 10768
		public byte? MinorLinkerVersion;

		// Token: 0x04002A11 RID: 10769
		public ulong? ImageBase;

		// Token: 0x04002A12 RID: 10770
		public uint? SectionAlignment;

		// Token: 0x04002A13 RID: 10771
		public uint? FileAlignment;

		// Token: 0x04002A14 RID: 10772
		public ushort? MajorOperatingSystemVersion;

		// Token: 0x04002A15 RID: 10773
		public ushort? MinorOperatingSystemVersion;

		// Token: 0x04002A16 RID: 10774
		public ushort? MajorImageVersion;

		// Token: 0x04002A17 RID: 10775
		public ushort? MinorImageVersion;

		// Token: 0x04002A18 RID: 10776
		public ushort? MajorSubsystemVersion;

		// Token: 0x04002A19 RID: 10777
		public ushort? MinorSubsystemVersion;

		// Token: 0x04002A1A RID: 10778
		public uint? Win32VersionValue;

		// Token: 0x04002A1B RID: 10779
		public Subsystem? Subsystem;

		// Token: 0x04002A1C RID: 10780
		public DllCharacteristics? DllCharacteristics;

		// Token: 0x04002A1D RID: 10781
		public ulong? SizeOfStackReserve;

		// Token: 0x04002A1E RID: 10782
		public ulong? SizeOfStackCommit;

		// Token: 0x04002A1F RID: 10783
		public ulong? SizeOfHeapReserve;

		// Token: 0x04002A20 RID: 10784
		public ulong? SizeOfHeapCommit;

		// Token: 0x04002A21 RID: 10785
		public uint? LoaderFlags;

		// Token: 0x04002A22 RID: 10786
		public uint? NumberOfRvaAndSizes;

		// Token: 0x04002A23 RID: 10787
		private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	}
}
