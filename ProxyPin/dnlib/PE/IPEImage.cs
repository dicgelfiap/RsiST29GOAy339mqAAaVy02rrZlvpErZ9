using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.IO;
using dnlib.W32Resources;

namespace dnlib.PE
{
	// Token: 0x02000754 RID: 1876
	[ComVisible(true)]
	public interface IPEImage : IRvaFileOffsetConverter, IDisposable
	{
		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x060041B0 RID: 16816
		bool IsFileImageLayout { get; }

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x060041B1 RID: 16817
		bool MayHaveInvalidAddresses { get; }

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x060041B2 RID: 16818
		string Filename { get; }

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x060041B3 RID: 16819
		ImageDosHeader ImageDosHeader { get; }

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x060041B4 RID: 16820
		ImageNTHeaders ImageNTHeaders { get; }

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x060041B5 RID: 16821
		IList<ImageSectionHeader> ImageSectionHeaders { get; }

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x060041B6 RID: 16822
		IList<ImageDebugDirectory> ImageDebugDirectories { get; }

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x060041B7 RID: 16823
		// (set) Token: 0x060041B8 RID: 16824
		Win32Resources Win32Resources { get; set; }

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x060041B9 RID: 16825
		DataReaderFactory DataReaderFactory { get; }

		// Token: 0x060041BA RID: 16826
		DataReader CreateReader(FileOffset offset);

		// Token: 0x060041BB RID: 16827
		DataReader CreateReader(FileOffset offset, uint length);

		// Token: 0x060041BC RID: 16828
		DataReader CreateReader(RVA rva);

		// Token: 0x060041BD RID: 16829
		DataReader CreateReader(RVA rva, uint length);

		// Token: 0x060041BE RID: 16830
		DataReader CreateReader();
	}
}
