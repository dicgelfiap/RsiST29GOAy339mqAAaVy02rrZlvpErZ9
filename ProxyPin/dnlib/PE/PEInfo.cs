using System;
using dnlib.IO;

namespace dnlib.PE
{
	// Token: 0x0200075C RID: 1884
	internal sealed class PEInfo
	{
		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x060041F7 RID: 16887 RVA: 0x001640B8 File Offset: 0x001640B8
		public ImageDosHeader ImageDosHeader
		{
			get
			{
				return this.imageDosHeader;
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x060041F8 RID: 16888 RVA: 0x001640C0 File Offset: 0x001640C0
		public ImageNTHeaders ImageNTHeaders
		{
			get
			{
				return this.imageNTHeaders;
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x060041F9 RID: 16889 RVA: 0x001640C8 File Offset: 0x001640C8
		public ImageSectionHeader[] ImageSectionHeaders
		{
			get
			{
				return this.imageSectionHeaders;
			}
		}

		// Token: 0x060041FA RID: 16890 RVA: 0x001640D0 File Offset: 0x001640D0
		public PEInfo(ref DataReader reader, bool verify)
		{
			reader.Position = 0U;
			this.imageDosHeader = new ImageDosHeader(ref reader, verify);
			if (verify && this.imageDosHeader.NTHeadersOffset == 0U)
			{
				throw new BadImageFormatException("Invalid NT headers offset");
			}
			reader.Position = this.imageDosHeader.NTHeadersOffset;
			this.imageNTHeaders = new ImageNTHeaders(ref reader, verify);
			reader.Position = (uint)(this.imageNTHeaders.OptionalHeader.StartOffset + this.imageNTHeaders.FileHeader.SizeOfOptionalHeader);
			int num = this.imageNTHeaders.FileHeader.NumberOfSections;
			if (num > 0)
			{
				DataReader dataReader = reader;
				dataReader.Position += 20U;
				uint num2 = dataReader.ReadUInt32();
				num = Math.Min(num, (int)((num2 - reader.Position) / 40U));
			}
			this.imageSectionHeaders = new ImageSectionHeader[num];
			for (int i = 0; i < this.imageSectionHeaders.Length; i++)
			{
				this.imageSectionHeaders[i] = new ImageSectionHeader(ref reader, verify);
			}
		}

		// Token: 0x060041FB RID: 16891 RVA: 0x001641E0 File Offset: 0x001641E0
		public ImageSectionHeader ToImageSectionHeader(FileOffset offset)
		{
			foreach (ImageSectionHeader imageSectionHeader in this.imageSectionHeaders)
			{
				if ((ulong)offset >= (ulong)imageSectionHeader.PointerToRawData && (ulong)offset < (ulong)(imageSectionHeader.PointerToRawData + imageSectionHeader.SizeOfRawData))
				{
					return imageSectionHeader;
				}
			}
			return null;
		}

		// Token: 0x060041FC RID: 16892 RVA: 0x00164238 File Offset: 0x00164238
		public ImageSectionHeader ToImageSectionHeader(RVA rva)
		{
			foreach (ImageSectionHeader imageSectionHeader in this.imageSectionHeaders)
			{
				if (rva >= imageSectionHeader.VirtualAddress && rva < imageSectionHeader.VirtualAddress + Math.Max(imageSectionHeader.VirtualSize, imageSectionHeader.SizeOfRawData))
				{
					return imageSectionHeader;
				}
			}
			return null;
		}

		// Token: 0x060041FD RID: 16893 RVA: 0x00164298 File Offset: 0x00164298
		public RVA ToRVA(FileOffset offset)
		{
			ImageSectionHeader imageSectionHeader = this.ToImageSectionHeader(offset);
			if (imageSectionHeader != null)
			{
				return (RVA)(offset - imageSectionHeader.PointerToRawData + (uint)imageSectionHeader.VirtualAddress);
			}
			return (RVA)offset;
		}

		// Token: 0x060041FE RID: 16894 RVA: 0x001642C8 File Offset: 0x001642C8
		public FileOffset ToFileOffset(RVA rva)
		{
			ImageSectionHeader imageSectionHeader = this.ToImageSectionHeader(rva);
			if (imageSectionHeader != null)
			{
				return (FileOffset)(rva - (uint)imageSectionHeader.VirtualAddress + imageSectionHeader.PointerToRawData);
			}
			return (FileOffset)rva;
		}

		// Token: 0x060041FF RID: 16895 RVA: 0x001642F8 File Offset: 0x001642F8
		private static ulong AlignUp(ulong val, uint alignment)
		{
			return val + (ulong)alignment - 1UL & ~(ulong)(alignment - 1U);
		}

		// Token: 0x06004200 RID: 16896 RVA: 0x00164308 File Offset: 0x00164308
		public uint GetImageSize()
		{
			IImageOptionalHeader optionalHeader = this.ImageNTHeaders.OptionalHeader;
			uint sectionAlignment = optionalHeader.SectionAlignment;
			ulong num = PEInfo.AlignUp((ulong)optionalHeader.SizeOfHeaders, sectionAlignment);
			foreach (ImageSectionHeader imageSectionHeader in this.imageSectionHeaders)
			{
				ulong num2 = PEInfo.AlignUp((ulong)imageSectionHeader.VirtualAddress + (ulong)Math.Max(imageSectionHeader.VirtualSize, imageSectionHeader.SizeOfRawData), sectionAlignment);
				if (num2 > num)
				{
					num = num2;
				}
			}
			return (uint)Math.Min(num, (ulong)-1);
		}

		// Token: 0x0400236A RID: 9066
		private readonly ImageDosHeader imageDosHeader;

		// Token: 0x0400236B RID: 9067
		private readonly ImageNTHeaders imageNTHeaders;

		// Token: 0x0400236C RID: 9068
		private readonly ImageSectionHeader[] imageSectionHeaders;
	}
}
