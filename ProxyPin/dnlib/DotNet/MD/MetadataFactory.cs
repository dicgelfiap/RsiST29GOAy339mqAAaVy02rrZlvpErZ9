using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.MD
{
	// Token: 0x02000997 RID: 2455
	[ComVisible(true)]
	public static class MetadataFactory
	{
		// Token: 0x06005EBE RID: 24254 RVA: 0x001C6B4C File Offset: 0x001C6B4C
		internal static MetadataBase Load(string fileName, CLRRuntimeReaderKind runtime)
		{
			IPEImage ipeimage = null;
			MetadataBase result;
			try
			{
				result = MetadataFactory.Load(ipeimage = new PEImage(fileName), runtime);
			}
			catch
			{
				if (ipeimage != null)
				{
					ipeimage.Dispose();
				}
				throw;
			}
			return result;
		}

		// Token: 0x06005EBF RID: 24255 RVA: 0x001C6B90 File Offset: 0x001C6B90
		internal static MetadataBase Load(byte[] data, CLRRuntimeReaderKind runtime)
		{
			IPEImage ipeimage = null;
			MetadataBase result;
			try
			{
				result = MetadataFactory.Load(ipeimage = new PEImage(data), runtime);
			}
			catch
			{
				if (ipeimage != null)
				{
					ipeimage.Dispose();
				}
				throw;
			}
			return result;
		}

		// Token: 0x06005EC0 RID: 24256 RVA: 0x001C6BD4 File Offset: 0x001C6BD4
		internal static MetadataBase Load(IntPtr addr, CLRRuntimeReaderKind runtime)
		{
			IPEImage ipeimage = null;
			try
			{
				return MetadataFactory.Load(ipeimage = new PEImage(addr, ImageLayout.Memory, true), runtime);
			}
			catch
			{
				if (ipeimage != null)
				{
					ipeimage.Dispose();
				}
				ipeimage = null;
			}
			MetadataBase result;
			try
			{
				result = MetadataFactory.Load(ipeimage = new PEImage(addr, ImageLayout.File, true), runtime);
			}
			catch
			{
				if (ipeimage != null)
				{
					ipeimage.Dispose();
				}
				throw;
			}
			return result;
		}

		// Token: 0x06005EC1 RID: 24257 RVA: 0x001C6C50 File Offset: 0x001C6C50
		internal static MetadataBase Load(IntPtr addr, ImageLayout imageLayout, CLRRuntimeReaderKind runtime)
		{
			IPEImage ipeimage = null;
			MetadataBase result;
			try
			{
				result = MetadataFactory.Load(ipeimage = new PEImage(addr, imageLayout, true), runtime);
			}
			catch
			{
				if (ipeimage != null)
				{
					ipeimage.Dispose();
				}
				throw;
			}
			return result;
		}

		// Token: 0x06005EC2 RID: 24258 RVA: 0x001C6C98 File Offset: 0x001C6C98
		internal static MetadataBase Load(IPEImage peImage, CLRRuntimeReaderKind runtime)
		{
			return MetadataFactory.Create(peImage, runtime, true);
		}

		// Token: 0x06005EC3 RID: 24259 RVA: 0x001C6CA4 File Offset: 0x001C6CA4
		public static Metadata CreateMetadata(IPEImage peImage)
		{
			return MetadataFactory.CreateMetadata(peImage, CLRRuntimeReaderKind.CLR);
		}

		// Token: 0x06005EC4 RID: 24260 RVA: 0x001C6CB0 File Offset: 0x001C6CB0
		public static Metadata CreateMetadata(IPEImage peImage, CLRRuntimeReaderKind runtime)
		{
			return MetadataFactory.Create(peImage, runtime, true);
		}

		// Token: 0x06005EC5 RID: 24261 RVA: 0x001C6CBC File Offset: 0x001C6CBC
		public static Metadata CreateMetadata(IPEImage peImage, bool verify)
		{
			return MetadataFactory.CreateMetadata(peImage, CLRRuntimeReaderKind.CLR, verify);
		}

		// Token: 0x06005EC6 RID: 24262 RVA: 0x001C6CC8 File Offset: 0x001C6CC8
		public static Metadata CreateMetadata(IPEImage peImage, CLRRuntimeReaderKind runtime, bool verify)
		{
			return MetadataFactory.Create(peImage, runtime, verify);
		}

		// Token: 0x06005EC7 RID: 24263 RVA: 0x001C6CD4 File Offset: 0x001C6CD4
		private static MetadataBase Create(IPEImage peImage, CLRRuntimeReaderKind runtime, bool verify)
		{
			MetadataBase metadataBase = null;
			MetadataBase metadataBase2;
			try
			{
				ImageDataDirectory imageDataDirectory = peImage.ImageNTHeaders.OptionalHeader.DataDirectories[14];
				if (imageDataDirectory.VirtualAddress == (RVA)0U)
				{
					throw new BadImageFormatException(".NET data directory RVA is 0");
				}
				DataReader dataReader = peImage.CreateReader(imageDataDirectory.VirtualAddress, 72U);
				ImageCor20Header imageCor20Header = new ImageCor20Header(ref dataReader, verify && runtime == CLRRuntimeReaderKind.CLR);
				if (imageCor20Header.Metadata.VirtualAddress == (RVA)0U)
				{
					throw new BadImageFormatException(".NET metadata RVA is 0");
				}
				RVA virtualAddress = imageCor20Header.Metadata.VirtualAddress;
				DataReader dataReader2 = peImage.CreateReader(virtualAddress);
				MetadataHeader metadataHeader = new MetadataHeader(ref dataReader2, runtime, verify);
				if (verify)
				{
					foreach (StreamHeader streamHeader in metadataHeader.StreamHeaders)
					{
						if ((ulong)streamHeader.Offset + (ulong)streamHeader.StreamSize > (ulong)dataReader2.EndOffset)
						{
							throw new BadImageFormatException("Invalid stream header");
						}
					}
				}
				MetadataFactory.MetadataType metadataType = MetadataFactory.GetMetadataType(metadataHeader.StreamHeaders, runtime);
				if (metadataType != MetadataFactory.MetadataType.Compressed)
				{
					if (metadataType != MetadataFactory.MetadataType.ENC)
					{
						throw new BadImageFormatException("No #~ or #- stream found");
					}
					metadataBase2 = new ENCMetadata(peImage, imageCor20Header, metadataHeader, runtime);
				}
				else
				{
					metadataBase2 = new CompressedMetadata(peImage, imageCor20Header, metadataHeader, runtime);
				}
				metadataBase = metadataBase2;
				metadataBase.Initialize(null);
				metadataBase2 = metadataBase;
			}
			catch
			{
				if (metadataBase != null)
				{
					metadataBase.Dispose();
				}
				throw;
			}
			return metadataBase2;
		}

		// Token: 0x06005EC8 RID: 24264 RVA: 0x001C6E88 File Offset: 0x001C6E88
		internal static MetadataBase CreateStandalonePortablePDB(DataReaderFactory mdReaderFactory, bool verify)
		{
			MetadataBase metadataBase = null;
			MetadataBase metadataBase2;
			try
			{
				DataReader dataReader = mdReaderFactory.CreateReader();
				MetadataHeader metadataHeader = new MetadataHeader(ref dataReader, CLRRuntimeReaderKind.CLR, verify);
				if (verify)
				{
					foreach (StreamHeader streamHeader in metadataHeader.StreamHeaders)
					{
						if (streamHeader.Offset + streamHeader.StreamSize < streamHeader.Offset || streamHeader.Offset + streamHeader.StreamSize > dataReader.Length)
						{
							throw new BadImageFormatException("Invalid stream header");
						}
					}
				}
				MetadataFactory.MetadataType metadataType = MetadataFactory.GetMetadataType(metadataHeader.StreamHeaders, CLRRuntimeReaderKind.CLR);
				if (metadataType != MetadataFactory.MetadataType.Compressed)
				{
					if (metadataType != MetadataFactory.MetadataType.ENC)
					{
						throw new BadImageFormatException("No #~ or #- stream found");
					}
					metadataBase2 = new ENCMetadata(metadataHeader, true, CLRRuntimeReaderKind.CLR);
				}
				else
				{
					metadataBase2 = new CompressedMetadata(metadataHeader, true, CLRRuntimeReaderKind.CLR);
				}
				metadataBase = metadataBase2;
				metadataBase.Initialize(mdReaderFactory);
				metadataBase2 = metadataBase;
			}
			catch
			{
				if (metadataBase != null)
				{
					metadataBase.Dispose();
				}
				throw;
			}
			return metadataBase2;
		}

		// Token: 0x06005EC9 RID: 24265 RVA: 0x001C6FB0 File Offset: 0x001C6FB0
		private static MetadataFactory.MetadataType GetMetadataType(IList<StreamHeader> streamHeaders, CLRRuntimeReaderKind runtime)
		{
			MetadataFactory.MetadataType? metadataType = null;
			if (runtime == CLRRuntimeReaderKind.CLR)
			{
				using (IEnumerator<StreamHeader> enumerator = streamHeaders.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						StreamHeader streamHeader = enumerator.Current;
						if (metadataType == null)
						{
							if (streamHeader.Name == "#~")
							{
								metadataType = new MetadataFactory.MetadataType?(MetadataFactory.MetadataType.Compressed);
							}
							else if (streamHeader.Name == "#-")
							{
								metadataType = new MetadataFactory.MetadataType?(MetadataFactory.MetadataType.ENC);
							}
						}
						if (streamHeader.Name == "#Schema")
						{
							metadataType = new MetadataFactory.MetadataType?(MetadataFactory.MetadataType.ENC);
						}
					}
					goto IL_121;
				}
			}
			if (runtime == CLRRuntimeReaderKind.Mono)
			{
				using (IEnumerator<StreamHeader> enumerator = streamHeaders.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						StreamHeader streamHeader2 = enumerator.Current;
						if (streamHeader2.Name == "#~")
						{
							metadataType = new MetadataFactory.MetadataType?(MetadataFactory.MetadataType.Compressed);
						}
						else if (streamHeader2.Name == "#-")
						{
							metadataType = new MetadataFactory.MetadataType?(MetadataFactory.MetadataType.ENC);
							break;
						}
					}
					goto IL_121;
				}
			}
			throw new ArgumentOutOfRangeException("runtime");
			IL_121:
			if (metadataType == null)
			{
				return MetadataFactory.MetadataType.Unknown;
			}
			return metadataType.Value;
		}

		// Token: 0x0200103F RID: 4159
		private enum MetadataType
		{
			// Token: 0x0400452D RID: 17709
			Unknown,
			// Token: 0x0400452E RID: 17710
			Compressed,
			// Token: 0x0400452F RID: 17711
			ENC
		}
	}
}
