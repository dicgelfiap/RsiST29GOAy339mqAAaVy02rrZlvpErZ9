using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.IO;
using dnlib.PE;
using dnlib.Utils;

namespace dnlib.W32Resources
{
	// Token: 0x02000733 RID: 1843
	[ComVisible(true)]
	public sealed class ResourceDirectoryPE : ResourceDirectory
	{
		// Token: 0x060040AC RID: 16556 RVA: 0x0016166C File Offset: 0x0016166C
		public ResourceDirectoryPE(uint depth, ResourceName name, Win32ResourcesPE resources, ref DataReader reader) : base(name)
		{
			this.resources = resources;
			this.depth = depth;
			this.Initialize(ref reader);
		}

		// Token: 0x060040AD RID: 16557 RVA: 0x0016168C File Offset: 0x0016168C
		private void Initialize(ref DataReader reader)
		{
			if (this.depth > 10U || !reader.CanRead(16U))
			{
				this.InitializeDefault();
				return;
			}
			this.characteristics = reader.ReadUInt32();
			this.timeDateStamp = reader.ReadUInt32();
			this.majorVersion = reader.ReadUInt16();
			this.minorVersion = reader.ReadUInt16();
			int num = (int)reader.ReadUInt16();
			ushort num2 = reader.ReadUInt16();
			int num3 = num + (int)num2;
			if (!reader.CanRead((uint)(num3 * 8)))
			{
				this.InitializeDefault();
				return;
			}
			this.dataInfos = new List<ResourceDirectoryPE.EntryInfo>();
			this.dirInfos = new List<ResourceDirectoryPE.EntryInfo>();
			uint num4 = reader.Position;
			int j = 0;
			while (j < num3)
			{
				reader.Position = num4;
				uint num5 = reader.ReadUInt32();
				uint num6 = reader.ReadUInt32();
				ResourceName name;
				if ((num5 & 2147483648U) != 0U)
				{
					name = new ResourceName(ResourceDirectoryPE.ReadString(ref reader, num5 & 2147483647U) ?? string.Empty);
				}
				else
				{
					name = new ResourceName((int)num5);
				}
				if ((num6 & 2147483648U) == 0U)
				{
					this.dataInfos.Add(new ResourceDirectoryPE.EntryInfo(name, num6));
				}
				else
				{
					this.dirInfos.Add(new ResourceDirectoryPE.EntryInfo(name, num6 & 2147483647U));
				}
				j++;
				num4 += 8U;
			}
			this.directories = new LazyList<ResourceDirectory, object>(this.dirInfos.Count, null, (object ctx, int i) => this.ReadResourceDirectory(i));
			this.data = new LazyList<ResourceData, object>(this.dataInfos.Count, null, (object ctx, int i) => this.ReadResourceData(i));
		}

		// Token: 0x060040AE RID: 16558 RVA: 0x00161820 File Offset: 0x00161820
		private static string ReadString(ref DataReader reader, uint offset)
		{
			reader.Position = offset;
			if (!reader.CanRead(2U))
			{
				return null;
			}
			int num = (int)(reader.ReadUInt16() * 2);
			if (!reader.CanRead((uint)num))
			{
				return null;
			}
			string result;
			try
			{
				result = reader.ReadUtf16String(num / 2);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060040AF RID: 16559 RVA: 0x00161884 File Offset: 0x00161884
		private ResourceDirectory ReadResourceDirectory(int i)
		{
			ResourceDirectoryPE.EntryInfo entryInfo = this.dirInfos[i];
			DataReader resourceReader = this.resources.GetResourceReader();
			resourceReader.Position = Math.Min(resourceReader.Length, entryInfo.offset);
			return new ResourceDirectoryPE(this.depth + 1U, entryInfo.name, this.resources, ref resourceReader);
		}

		// Token: 0x060040B0 RID: 16560 RVA: 0x001618E4 File Offset: 0x001618E4
		private ResourceData ReadResourceData(int i)
		{
			ResourceDirectoryPE.EntryInfo entryInfo = this.dataInfos[i];
			DataReader resourceReader = this.resources.GetResourceReader();
			resourceReader.Position = Math.Min(resourceReader.Length, entryInfo.offset);
			ResourceData result;
			if (resourceReader.CanRead(16U))
			{
				RVA rva = (RVA)resourceReader.ReadUInt32();
				uint size = resourceReader.ReadUInt32();
				uint codePage = resourceReader.ReadUInt32();
				uint reserved = resourceReader.ReadUInt32();
				DataReaderFactory dataReaderFactory;
				uint offset;
				uint length;
				this.resources.GetDataReaderInfo(rva, size, out dataReaderFactory, out offset, out length);
				result = new ResourceData(entryInfo.name, dataReaderFactory, offset, length, codePage, reserved);
			}
			else
			{
				result = new ResourceData(entryInfo.name);
			}
			return result;
		}

		// Token: 0x060040B1 RID: 16561 RVA: 0x00161998 File Offset: 0x00161998
		private void InitializeDefault()
		{
			this.directories = new LazyList<ResourceDirectory>();
			this.data = new LazyList<ResourceData>();
		}

		// Token: 0x04002274 RID: 8820
		private const uint MAX_DIR_DEPTH = 10U;

		// Token: 0x04002275 RID: 8821
		private readonly Win32ResourcesPE resources;

		// Token: 0x04002276 RID: 8822
		private uint depth;

		// Token: 0x04002277 RID: 8823
		private List<ResourceDirectoryPE.EntryInfo> dataInfos;

		// Token: 0x04002278 RID: 8824
		private List<ResourceDirectoryPE.EntryInfo> dirInfos;

		// Token: 0x02000FBD RID: 4029
		private readonly struct EntryInfo
		{
			// Token: 0x06008D7C RID: 36220 RVA: 0x002A6B00 File Offset: 0x002A6B00
			public EntryInfo(ResourceName name, uint offset)
			{
				this.name = name;
				this.offset = offset;
			}

			// Token: 0x06008D7D RID: 36221 RVA: 0x002A6B10 File Offset: 0x002A6B10
			public override string ToString()
			{
				return string.Format("{0:X8} {1}", this.offset, this.name);
			}

			// Token: 0x040042EF RID: 17135
			public readonly ResourceName name;

			// Token: 0x040042F0 RID: 17136
			public readonly uint offset;
		}
	}
}
