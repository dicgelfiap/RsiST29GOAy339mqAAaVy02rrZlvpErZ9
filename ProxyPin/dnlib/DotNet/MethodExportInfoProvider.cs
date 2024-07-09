using System;
using System.Collections.Generic;
using System.IO;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet
{
	// Token: 0x02000812 RID: 2066
	internal sealed class MethodExportInfoProvider
	{
		// Token: 0x06004B6E RID: 19310 RVA: 0x0017DF38 File Offset: 0x0017DF38
		public MethodExportInfoProvider(ModuleDefMD module)
		{
			this.toInfo = new Dictionary<uint, MethodExportInfo>();
			try
			{
				this.Initialize(module);
			}
			catch (OutOfMemoryException)
			{
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x06004B6F RID: 19311 RVA: 0x0017DF8C File Offset: 0x0017DF8C
		private void Initialize(ModuleDefMD module)
		{
			ImageDataDirectory vtableFixups = module.Metadata.ImageCor20Header.VTableFixups;
			if (vtableFixups.VirtualAddress == (RVA)0U || vtableFixups.Size == 0U)
			{
				return;
			}
			IPEImage peimage = module.Metadata.PEImage;
			ImageDataDirectory imageDataDirectory = peimage.ImageNTHeaders.OptionalHeader.DataDirectories[0];
			if (imageDataDirectory.VirtualAddress == (RVA)0U || imageDataDirectory.Size < 40U)
			{
				return;
			}
			CpuArch cpuArch;
			if (!CpuArch.TryGetCpuArch(peimage.ImageNTHeaders.FileHeader.Machine, out cpuArch))
			{
				return;
			}
			DataReader dataReader = peimage.CreateReader();
			Dictionary<uint, MethodExportInfo> offsetToExportInfoDictionary = MethodExportInfoProvider.GetOffsetToExportInfoDictionary(ref dataReader, peimage, imageDataDirectory, cpuArch);
			dataReader.Position = (uint)peimage.ToFileOffset(vtableFixups.VirtualAddress);
			ulong num = (ulong)dataReader.Position + (ulong)vtableFixups.Size;
			while ((ulong)dataReader.Position + 8UL <= num && dataReader.CanRead(8U))
			{
				RVA rva = (RVA)dataReader.ReadUInt32();
				int num2 = (int)dataReader.ReadUInt16();
				ushort num3 = dataReader.ReadUInt16();
				bool flag = (num3 & 2) > 0;
				MethodExportInfoOptions options = MethodExportInfoProvider.ToMethodExportInfoOptions((VTableFlags)num3);
				uint position = dataReader.Position;
				dataReader.Position = (uint)peimage.ToFileOffset(rva);
				uint num4 = flag ? 8U : 4U;
				while (num2-- > 0 && dataReader.CanRead(num4))
				{
					uint position2 = dataReader.Position;
					uint key = dataReader.ReadUInt32();
					MethodExportInfo methodExportInfo;
					if (offsetToExportInfoDictionary.TryGetValue(position2, out methodExportInfo))
					{
						this.toInfo[key] = new MethodExportInfo(methodExportInfo.Name, methodExportInfo.Ordinal, options);
					}
					if (num4 == 8U)
					{
						dataReader.ReadUInt32();
					}
				}
				dataReader.Position = position;
			}
		}

		// Token: 0x06004B70 RID: 19312 RVA: 0x0017E148 File Offset: 0x0017E148
		private static MethodExportInfoOptions ToMethodExportInfoOptions(VTableFlags flags)
		{
			MethodExportInfoOptions methodExportInfoOptions = MethodExportInfoOptions.None;
			if ((flags & VTableFlags.FromUnmanaged) != (VTableFlags)0)
			{
				methodExportInfoOptions |= MethodExportInfoOptions.FromUnmanaged;
			}
			if ((flags & VTableFlags.FromUnmanagedRetainAppDomain) != (VTableFlags)0)
			{
				methodExportInfoOptions |= MethodExportInfoOptions.FromUnmanagedRetainAppDomain;
			}
			if ((flags & VTableFlags.CallMostDerived) != (VTableFlags)0)
			{
				methodExportInfoOptions |= MethodExportInfoOptions.CallMostDerived;
			}
			return methodExportInfoOptions;
		}

		// Token: 0x06004B71 RID: 19313 RVA: 0x0017E184 File Offset: 0x0017E184
		private static Dictionary<uint, MethodExportInfo> GetOffsetToExportInfoDictionary(ref DataReader reader, IPEImage peImage, ImageDataDirectory exportHdr, CpuArch cpuArch)
		{
			reader.Position = (uint)peImage.ToFileOffset(exportHdr.VirtualAddress);
			reader.Position += 16U;
			uint num = reader.ReadUInt32();
			int num2 = reader.ReadInt32();
			int numNames = reader.ReadInt32();
			uint position = (uint)peImage.ToFileOffset((RVA)reader.ReadUInt32());
			uint offsetOfNames = (uint)peImage.ToFileOffset((RVA)reader.ReadUInt32());
			uint offsetOfNameIndexes = (uint)peImage.ToFileOffset((RVA)reader.ReadUInt32());
			MethodExportInfoProvider.NameAndIndex[] array = MethodExportInfoProvider.ReadNames(ref reader, peImage, numNames, offsetOfNames, offsetOfNameIndexes);
			reader.Position = position;
			MethodExportInfo[] array2 = new MethodExportInfo[num2];
			Dictionary<uint, MethodExportInfo> dictionary = new Dictionary<uint, MethodExportInfo>(num2);
			for (int i = 0; i < array2.Length; i++)
			{
				uint position2 = reader.Position + 4U;
				uint rva = 0U;
				RVA rva2 = (RVA)reader.ReadUInt32();
				reader.Position = (uint)peImage.ToFileOffset(rva2);
				uint num3 = (uint)((rva2 != (RVA)0U && cpuArch.TryGetExportedRvaFromStub(ref reader, peImage, out rva)) ? peImage.ToFileOffset((RVA)rva) : ((FileOffset)0U));
				MethodExportInfo methodExportInfo = new MethodExportInfo((ushort)(num + (uint)i));
				if (num3 != 0U)
				{
					dictionary[num3] = methodExportInfo;
				}
				array2[i] = methodExportInfo;
				reader.Position = position2;
			}
			foreach (MethodExportInfoProvider.NameAndIndex nameAndIndex in array)
			{
				int index = nameAndIndex.Index;
				if (index < num2)
				{
					array2[index].Ordinal = null;
					array2[index].Name = nameAndIndex.Name;
				}
			}
			return dictionary;
		}

		// Token: 0x06004B72 RID: 19314 RVA: 0x0017E318 File Offset: 0x0017E318
		private static MethodExportInfoProvider.NameAndIndex[] ReadNames(ref DataReader reader, IPEImage peImage, int numNames, uint offsetOfNames, uint offsetOfNameIndexes)
		{
			MethodExportInfoProvider.NameAndIndex[] array = new MethodExportInfoProvider.NameAndIndex[numNames];
			reader.Position = offsetOfNameIndexes;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Index = (int)reader.ReadUInt16();
			}
			uint num = offsetOfNames;
			int j = 0;
			while (j < array.Length)
			{
				reader.Position = num;
				uint offset = (uint)peImage.ToFileOffset((RVA)reader.ReadUInt32());
				array[j].Name = MethodExportInfoProvider.ReadMethodNameASCIIZ(ref reader, offset);
				j++;
				num += 4U;
			}
			return array;
		}

		// Token: 0x06004B73 RID: 19315 RVA: 0x0017E39C File Offset: 0x0017E39C
		private static string ReadMethodNameASCIIZ(ref DataReader reader, uint offset)
		{
			reader.Position = offset;
			return reader.TryReadZeroTerminatedUtf8String() ?? string.Empty;
		}

		// Token: 0x06004B74 RID: 19316 RVA: 0x0017E3B8 File Offset: 0x0017E3B8
		public MethodExportInfo GetMethodExportInfo(uint token)
		{
			if (this.toInfo.Count == 0)
			{
				return null;
			}
			MethodExportInfo methodExportInfo;
			if (this.toInfo.TryGetValue(token, out methodExportInfo))
			{
				return new MethodExportInfo(methodExportInfo.Name, methodExportInfo.Ordinal, methodExportInfo.Options);
			}
			return null;
		}

		// Token: 0x040025AF RID: 9647
		private readonly Dictionary<uint, MethodExportInfo> toInfo;

		// Token: 0x02000FEB RID: 4075
		private struct NameAndIndex
		{
			// Token: 0x040043DC RID: 17372
			public string Name;

			// Token: 0x040043DD RID: 17373
			public int Index;
		}
	}
}
