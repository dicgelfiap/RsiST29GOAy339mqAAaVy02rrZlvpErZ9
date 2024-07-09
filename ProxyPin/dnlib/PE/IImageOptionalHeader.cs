using System;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.PE
{
	// Token: 0x02000749 RID: 1865
	[ComVisible(true)]
	public interface IImageOptionalHeader : IFileSection
	{
		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06004126 RID: 16678
		ushort Magic { get; }

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06004127 RID: 16679
		byte MajorLinkerVersion { get; }

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06004128 RID: 16680
		byte MinorLinkerVersion { get; }

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06004129 RID: 16681
		uint SizeOfCode { get; }

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x0600412A RID: 16682
		uint SizeOfInitializedData { get; }

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x0600412B RID: 16683
		uint SizeOfUninitializedData { get; }

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x0600412C RID: 16684
		RVA AddressOfEntryPoint { get; }

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x0600412D RID: 16685
		RVA BaseOfCode { get; }

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x0600412E RID: 16686
		RVA BaseOfData { get; }

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x0600412F RID: 16687
		ulong ImageBase { get; }

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06004130 RID: 16688
		uint SectionAlignment { get; }

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06004131 RID: 16689
		uint FileAlignment { get; }

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06004132 RID: 16690
		ushort MajorOperatingSystemVersion { get; }

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06004133 RID: 16691
		ushort MinorOperatingSystemVersion { get; }

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06004134 RID: 16692
		ushort MajorImageVersion { get; }

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06004135 RID: 16693
		ushort MinorImageVersion { get; }

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06004136 RID: 16694
		ushort MajorSubsystemVersion { get; }

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06004137 RID: 16695
		ushort MinorSubsystemVersion { get; }

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06004138 RID: 16696
		uint Win32VersionValue { get; }

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06004139 RID: 16697
		uint SizeOfImage { get; }

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x0600413A RID: 16698
		uint SizeOfHeaders { get; }

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x0600413B RID: 16699
		uint CheckSum { get; }

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x0600413C RID: 16700
		Subsystem Subsystem { get; }

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x0600413D RID: 16701
		DllCharacteristics DllCharacteristics { get; }

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x0600413E RID: 16702
		ulong SizeOfStackReserve { get; }

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x0600413F RID: 16703
		ulong SizeOfStackCommit { get; }

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06004140 RID: 16704
		ulong SizeOfHeapReserve { get; }

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06004141 RID: 16705
		ulong SizeOfHeapCommit { get; }

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06004142 RID: 16706
		uint LoaderFlags { get; }

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06004143 RID: 16707
		uint NumberOfRvaAndSizes { get; }

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06004144 RID: 16708
		ImageDataDirectory[] DataDirectories { get; }
	}
}
