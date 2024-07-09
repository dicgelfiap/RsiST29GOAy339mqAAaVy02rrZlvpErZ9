using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using dnlib.IO;
using dnlib.PE;
using dnlib.W32Resources;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008E1 RID: 2273
	[ComVisible(true)]
	public sealed class Win32ResourcesChunk : IReuseChunk, IChunk
	{
		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x0600587F RID: 22655 RVA: 0x001B3258 File Offset: 0x001B3258
		public FileOffset FileOffset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x06005880 RID: 22656 RVA: 0x001B3260 File Offset: 0x001B3260
		public RVA RVA
		{
			get
			{
				return this.rva;
			}
		}

		// Token: 0x06005881 RID: 22657 RVA: 0x001B3268 File Offset: 0x001B3268
		public Win32ResourcesChunk(Win32Resources win32Resources)
		{
			this.win32Resources = win32Resources;
		}

		// Token: 0x06005882 RID: 22658 RVA: 0x001B32E4 File Offset: 0x001B32E4
		public bool GetFileOffsetAndRvaOf(ResourceDirectoryEntry dirEntry, out FileOffset fileOffset, out RVA rva)
		{
			ResourceDirectory resourceDirectory = dirEntry as ResourceDirectory;
			if (resourceDirectory != null)
			{
				return this.GetFileOffsetAndRvaOf(resourceDirectory, out fileOffset, out rva);
			}
			ResourceData resourceData = dirEntry as ResourceData;
			if (resourceData != null)
			{
				return this.GetFileOffsetAndRvaOf(resourceData, out fileOffset, out rva);
			}
			fileOffset = (FileOffset)0U;
			rva = (RVA)0U;
			return false;
		}

		// Token: 0x06005883 RID: 22659 RVA: 0x001B332C File Offset: 0x001B332C
		public FileOffset GetFileOffset(ResourceDirectoryEntry dirEntry)
		{
			FileOffset result;
			RVA rva;
			this.GetFileOffsetAndRvaOf(dirEntry, out result, out rva);
			return result;
		}

		// Token: 0x06005884 RID: 22660 RVA: 0x001B334C File Offset: 0x001B334C
		public RVA GetRVA(ResourceDirectoryEntry dirEntry)
		{
			FileOffset fileOffset;
			RVA result;
			this.GetFileOffsetAndRvaOf(dirEntry, out fileOffset, out result);
			return result;
		}

		// Token: 0x06005885 RID: 22661 RVA: 0x001B336C File Offset: 0x001B336C
		public bool GetFileOffsetAndRvaOf(ResourceDirectory dir, out FileOffset fileOffset, out RVA rva)
		{
			uint num;
			if (dir == null || !this.dirDict.TryGetValue(dir, out num))
			{
				fileOffset = (FileOffset)0U;
				rva = (RVA)0U;
				return false;
			}
			fileOffset = this.offset + num;
			rva = this.rva + num;
			return true;
		}

		// Token: 0x06005886 RID: 22662 RVA: 0x001B33B4 File Offset: 0x001B33B4
		public FileOffset GetFileOffset(ResourceDirectory dir)
		{
			FileOffset result;
			RVA rva;
			this.GetFileOffsetAndRvaOf(dir, out result, out rva);
			return result;
		}

		// Token: 0x06005887 RID: 22663 RVA: 0x001B33D4 File Offset: 0x001B33D4
		public RVA GetRVA(ResourceDirectory dir)
		{
			FileOffset fileOffset;
			RVA result;
			this.GetFileOffsetAndRvaOf(dir, out fileOffset, out result);
			return result;
		}

		// Token: 0x06005888 RID: 22664 RVA: 0x001B33F4 File Offset: 0x001B33F4
		public bool GetFileOffsetAndRvaOf(ResourceData dataHeader, out FileOffset fileOffset, out RVA rva)
		{
			uint num;
			if (dataHeader == null || !this.dataHeaderDict.TryGetValue(dataHeader, out num))
			{
				fileOffset = (FileOffset)0U;
				rva = (RVA)0U;
				return false;
			}
			fileOffset = this.offset + num;
			rva = this.rva + num;
			return true;
		}

		// Token: 0x06005889 RID: 22665 RVA: 0x001B343C File Offset: 0x001B343C
		public FileOffset GetFileOffset(ResourceData dataHeader)
		{
			FileOffset result;
			RVA rva;
			this.GetFileOffsetAndRvaOf(dataHeader, out result, out rva);
			return result;
		}

		// Token: 0x0600588A RID: 22666 RVA: 0x001B345C File Offset: 0x001B345C
		public RVA GetRVA(ResourceData dataHeader)
		{
			FileOffset fileOffset;
			RVA result;
			this.GetFileOffsetAndRvaOf(dataHeader, out fileOffset, out result);
			return result;
		}

		// Token: 0x0600588B RID: 22667 RVA: 0x001B347C File Offset: 0x001B347C
		public bool GetFileOffsetAndRvaOf(string name, out FileOffset fileOffset, out RVA rva)
		{
			uint num;
			if (name == null || !this.stringsDict.TryGetValue(name, out num))
			{
				fileOffset = (FileOffset)0U;
				rva = (RVA)0U;
				return false;
			}
			fileOffset = this.offset + num;
			rva = this.rva + num;
			return true;
		}

		// Token: 0x0600588C RID: 22668 RVA: 0x001B34C4 File Offset: 0x001B34C4
		public FileOffset GetFileOffset(string name)
		{
			FileOffset result;
			RVA rva;
			this.GetFileOffsetAndRvaOf(name, out result, out rva);
			return result;
		}

		// Token: 0x0600588D RID: 22669 RVA: 0x001B34E4 File Offset: 0x001B34E4
		public RVA GetRVA(string name)
		{
			FileOffset fileOffset;
			RVA result;
			this.GetFileOffsetAndRvaOf(name, out fileOffset, out result);
			return result;
		}

		// Token: 0x0600588E RID: 22670 RVA: 0x001B3504 File Offset: 0x001B3504
		bool IReuseChunk.CanReuse(RVA origRva, uint origSize)
		{
			if (this.rva == (RVA)0U)
			{
				throw new InvalidOperationException();
			}
			return this.length <= origSize;
		}

		// Token: 0x0600588F RID: 22671 RVA: 0x001B3524 File Offset: 0x001B3524
		internal bool CheckValidOffset(FileOffset offset)
		{
			string text;
			Win32ResourcesChunk.GetMaxAlignment(offset, out text);
			return text == null;
		}

		// Token: 0x06005890 RID: 22672 RVA: 0x001B3544 File Offset: 0x001B3544
		private static uint GetMaxAlignment(FileOffset offset, out string error)
		{
			error = null;
			uint num = 1U;
			num = Math.Max(num, 4U);
			num = Math.Max(num, 4U);
			num = Math.Max(num, 2U);
			num = Math.Max(num, 4U);
			if ((offset & (FileOffset)(num - 1U)) != (FileOffset)0U)
			{
				error = string.Format("Win32 resources section isn't {0}-byte aligned", num);
			}
			else if (num > 8U)
			{
				error = "maxAlignment > DEFAULT_WIN32_RESOURCES_ALIGNMENT";
			}
			return num;
		}

		// Token: 0x06005891 RID: 22673 RVA: 0x001B35AC File Offset: 0x001B35AC
		public void SetOffset(FileOffset offset, RVA rva)
		{
			bool flag = this.offset == (FileOffset)0U;
			this.offset = offset;
			this.rva = rva;
			if (this.win32Resources == null)
			{
				return;
			}
			if (!flag)
			{
				this.dirDict.Clear();
				this.dirList.Clear();
				this.dataHeaderDict.Clear();
				this.dataHeaderList.Clear();
				this.stringsDict.Clear();
				this.stringsList.Clear();
				this.dataDict.Clear();
				this.dataList.Clear();
			}
			this.FindDirectoryEntries();
			uint num = 0U;
			string text;
			Win32ResourcesChunk.GetMaxAlignment(offset, out text);
			if (text != null)
			{
				throw new ModuleWriterException(text);
			}
			foreach (ResourceDirectory resourceDirectory in this.dirList)
			{
				num = Utils.AlignUp(num, 4U);
				this.dirDict[resourceDirectory] = num;
				if (resourceDirectory != this.dirList[0])
				{
					this.AddString(resourceDirectory.Name);
				}
				num += (uint)(16 + (resourceDirectory.Directories.Count + resourceDirectory.Data.Count) * 8);
			}
			foreach (ResourceData resourceData in this.dataHeaderList)
			{
				num = Utils.AlignUp(num, 4U);
				this.dataHeaderDict[resourceData] = num;
				this.AddString(resourceData.Name);
				this.AddData(resourceData);
				num += 16U;
			}
			foreach (string text2 in this.stringsList)
			{
				num = Utils.AlignUp(num, 2U);
				this.stringsDict[text2] = num;
				num += (uint)(2 + text2.Length * 2);
			}
			foreach (ResourceData resourceData2 in this.dataList)
			{
				num = Utils.AlignUp(num, 4U);
				this.dataDict[resourceData2] = num;
				num += resourceData2.CreateReader().Length;
			}
			this.length = num;
		}

		// Token: 0x06005892 RID: 22674 RVA: 0x001B3844 File Offset: 0x001B3844
		private void AddData(ResourceData data)
		{
			if (this.dataDict.ContainsKey(data))
			{
				return;
			}
			this.dataList.Add(data);
			this.dataDict.Add(data, 0U);
		}

		// Token: 0x06005893 RID: 22675 RVA: 0x001B3874 File Offset: 0x001B3874
		private void AddString(ResourceName name)
		{
			if (!name.HasName || this.stringsDict.ContainsKey(name.Name))
			{
				return;
			}
			this.stringsList.Add(name.Name);
			this.stringsDict.Add(name.Name, 0U);
		}

		// Token: 0x06005894 RID: 22676 RVA: 0x001B38D0 File Offset: 0x001B38D0
		private void FindDirectoryEntries()
		{
			this.FindDirectoryEntries(this.win32Resources.Root);
		}

		// Token: 0x06005895 RID: 22677 RVA: 0x001B38E4 File Offset: 0x001B38E4
		private void FindDirectoryEntries(ResourceDirectory dir)
		{
			if (this.dirDict.ContainsKey(dir))
			{
				return;
			}
			this.dirList.Add(dir);
			this.dirDict[dir] = 0U;
			IList<ResourceDirectory> directories = dir.Directories;
			int count = directories.Count;
			for (int i = 0; i < count; i++)
			{
				this.FindDirectoryEntries(directories[i]);
			}
			IList<ResourceData> data = dir.Data;
			count = data.Count;
			for (int j = 0; j < count; j++)
			{
				ResourceData resourceData = data[j];
				if (!this.dataHeaderDict.ContainsKey(resourceData))
				{
					this.dataHeaderList.Add(resourceData);
					this.dataHeaderDict[resourceData] = 0U;
				}
			}
		}

		// Token: 0x06005896 RID: 22678 RVA: 0x001B39A4 File Offset: 0x001B39A4
		public uint GetFileLength()
		{
			return this.length;
		}

		// Token: 0x06005897 RID: 22679 RVA: 0x001B39AC File Offset: 0x001B39AC
		public uint GetVirtualSize()
		{
			return this.GetFileLength();
		}

		// Token: 0x06005898 RID: 22680 RVA: 0x001B39B4 File Offset: 0x001B39B4
		public void WriteTo(DataWriter writer)
		{
			uint num = 0U;
			foreach (ResourceDirectory resourceDirectory in this.dirList)
			{
				uint num2 = Utils.AlignUp(num, 4U) - num;
				writer.WriteZeroes((int)num2);
				num += num2;
				if (this.dirDict[resourceDirectory] != num)
				{
					throw new ModuleWriterException("Invalid Win32 resource directory offset");
				}
				num += this.WriteTo(writer, resourceDirectory);
			}
			foreach (ResourceData resourceData in this.dataHeaderList)
			{
				uint num3 = Utils.AlignUp(num, 4U) - num;
				writer.WriteZeroes((int)num3);
				num += num3;
				if (this.dataHeaderDict[resourceData] != num)
				{
					throw new ModuleWriterException("Invalid Win32 resource data header offset");
				}
				num += this.WriteTo(writer, resourceData);
			}
			foreach (string text in this.stringsList)
			{
				uint num4 = Utils.AlignUp(num, 2U) - num;
				writer.WriteZeroes((int)num4);
				num += num4;
				if (this.stringsDict[text] != num)
				{
					throw new ModuleWriterException("Invalid Win32 resource string offset");
				}
				byte[] bytes = Encoding.Unicode.GetBytes(text);
				if (bytes.Length / 2 > 65535)
				{
					throw new ModuleWriterException("Win32 resource entry name is too long");
				}
				writer.WriteUInt16((ushort)(bytes.Length / 2));
				writer.WriteBytes(bytes);
				num += (uint)(2 + bytes.Length);
			}
			byte[] dataBuffer = new byte[8192];
			foreach (ResourceData resourceData2 in this.dataList)
			{
				uint num5 = Utils.AlignUp(num, 4U) - num;
				writer.WriteZeroes((int)num5);
				num += num5;
				if (this.dataDict[resourceData2] != num)
				{
					throw new ModuleWriterException("Invalid Win32 resource data offset");
				}
				DataReader dataReader = resourceData2.CreateReader();
				num += dataReader.BytesLeft;
				dataReader.CopyTo(writer, dataBuffer);
			}
		}

		// Token: 0x06005899 RID: 22681 RVA: 0x001B3C30 File Offset: 0x001B3C30
		private uint WriteTo(DataWriter writer, ResourceDirectory dir)
		{
			writer.WriteUInt32(dir.Characteristics);
			writer.WriteUInt32(dir.TimeDateStamp);
			writer.WriteUInt16(dir.MajorVersion);
			writer.WriteUInt16(dir.MinorVersion);
			List<ResourceDirectoryEntry> list;
			List<ResourceDirectoryEntry> list2;
			Win32ResourcesChunk.GetNamedAndIds(dir, out list, out list2);
			if (list.Count > 65535 || list2.Count > 65535)
			{
				throw new ModuleWriterException("Too many named/id Win32 resource entries");
			}
			writer.WriteUInt16((ushort)list.Count);
			writer.WriteUInt16((ushort)list2.Count);
			list.Sort((ResourceDirectoryEntry a, ResourceDirectoryEntry b) => a.Name.Name.ToUpperInvariant().CompareTo(b.Name.Name.ToUpperInvariant()));
			list2.Sort((ResourceDirectoryEntry a, ResourceDirectoryEntry b) => a.Name.Id.CompareTo(b.Name.Id));
			foreach (ResourceDirectoryEntry resourceDirectoryEntry in list)
			{
				writer.WriteUInt32(2147483648U | this.stringsDict[resourceDirectoryEntry.Name.Name]);
				writer.WriteUInt32(this.GetDirectoryEntryOffset(resourceDirectoryEntry));
			}
			foreach (ResourceDirectoryEntry resourceDirectoryEntry2 in list2)
			{
				writer.WriteInt32(resourceDirectoryEntry2.Name.Id);
				writer.WriteUInt32(this.GetDirectoryEntryOffset(resourceDirectoryEntry2));
			}
			return (uint)(16 + (list.Count + list2.Count) * 8);
		}

		// Token: 0x0600589A RID: 22682 RVA: 0x001B3DF4 File Offset: 0x001B3DF4
		private uint GetDirectoryEntryOffset(ResourceDirectoryEntry e)
		{
			if (e is ResourceData)
			{
				return this.dataHeaderDict[(ResourceData)e];
			}
			return 2147483648U | this.dirDict[(ResourceDirectory)e];
		}

		// Token: 0x0600589B RID: 22683 RVA: 0x001B3E2C File Offset: 0x001B3E2C
		private static void GetNamedAndIds(ResourceDirectory dir, out List<ResourceDirectoryEntry> named, out List<ResourceDirectoryEntry> ids)
		{
			named = new List<ResourceDirectoryEntry>();
			ids = new List<ResourceDirectoryEntry>();
			IList<ResourceDirectory> directories = dir.Directories;
			int count = directories.Count;
			for (int i = 0; i < count; i++)
			{
				ResourceDirectory resourceDirectory = directories[i];
				if (resourceDirectory.Name.HasId)
				{
					ids.Add(resourceDirectory);
				}
				else
				{
					named.Add(resourceDirectory);
				}
			}
			IList<ResourceData> data = dir.Data;
			count = data.Count;
			for (int j = 0; j < count; j++)
			{
				ResourceData resourceData = data[j];
				if (resourceData.Name.HasId)
				{
					ids.Add(resourceData);
				}
				else
				{
					named.Add(resourceData);
				}
			}
		}

		// Token: 0x0600589C RID: 22684 RVA: 0x001B3EF4 File Offset: 0x001B3EF4
		private uint WriteTo(DataWriter writer, ResourceData dataHeader)
		{
			writer.WriteUInt32((uint)(this.rva + this.dataDict[dataHeader]));
			writer.WriteUInt32(dataHeader.CreateReader().Length);
			writer.WriteUInt32(dataHeader.CodePage);
			writer.WriteUInt32(dataHeader.Reserved);
			return 16U;
		}

		// Token: 0x04002AD1 RID: 10961
		private readonly Win32Resources win32Resources;

		// Token: 0x04002AD2 RID: 10962
		private FileOffset offset;

		// Token: 0x04002AD3 RID: 10963
		private RVA rva;

		// Token: 0x04002AD4 RID: 10964
		private uint length;

		// Token: 0x04002AD5 RID: 10965
		private readonly Dictionary<ResourceDirectory, uint> dirDict = new Dictionary<ResourceDirectory, uint>();

		// Token: 0x04002AD6 RID: 10966
		private readonly List<ResourceDirectory> dirList = new List<ResourceDirectory>();

		// Token: 0x04002AD7 RID: 10967
		private readonly Dictionary<ResourceData, uint> dataHeaderDict = new Dictionary<ResourceData, uint>();

		// Token: 0x04002AD8 RID: 10968
		private readonly List<ResourceData> dataHeaderList = new List<ResourceData>();

		// Token: 0x04002AD9 RID: 10969
		private readonly Dictionary<string, uint> stringsDict = new Dictionary<string, uint>(StringComparer.Ordinal);

		// Token: 0x04002ADA RID: 10970
		private readonly List<string> stringsList = new List<string>();

		// Token: 0x04002ADB RID: 10971
		private readonly Dictionary<ResourceData, uint> dataDict = new Dictionary<ResourceData, uint>();

		// Token: 0x04002ADC RID: 10972
		private readonly List<ResourceData> dataList = new List<ResourceData>();

		// Token: 0x04002ADD RID: 10973
		private const uint RESOURCE_DIR_ALIGNMENT = 4U;

		// Token: 0x04002ADE RID: 10974
		private const uint RESOURCE_DATA_HEADER_ALIGNMENT = 4U;

		// Token: 0x04002ADF RID: 10975
		private const uint RESOURCE_STRING_ALIGNMENT = 2U;

		// Token: 0x04002AE0 RID: 10976
		private const uint RESOURCE_DATA_ALIGNMENT = 4U;
	}
}
