using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using dnlib.IO;

namespace dnlib.DotNet.Resources
{
	// Token: 0x020008EA RID: 2282
	[ComVisible(true)]
	public struct ResourceReader
	{
		// Token: 0x060058E0 RID: 22752 RVA: 0x001B4878 File Offset: 0x001B4878
		private ResourceReader(ModuleDef module, ref DataReader reader, CreateResourceDataDelegate createResourceDataDelegate)
		{
			this.reader = reader;
			this.resourceDataFactory = new ResourceDataFactory(module);
			this.createResourceDataDelegate = createResourceDataDelegate;
			this.baseFileOffset = reader.StartOffset;
		}

		// Token: 0x060058E1 RID: 22753 RVA: 0x001B48A8 File Offset: 0x001B48A8
		public static bool CouldBeResourcesFile(DataReader reader)
		{
			return reader.CanRead(4U) && reader.ReadUInt32() == 3203386062U;
		}

		// Token: 0x060058E2 RID: 22754 RVA: 0x001B48C8 File Offset: 0x001B48C8
		public static ResourceElementSet Read(ModuleDef module, DataReader reader)
		{
			return ResourceReader.Read(module, reader, null);
		}

		// Token: 0x060058E3 RID: 22755 RVA: 0x001B48D4 File Offset: 0x001B48D4
		public static ResourceElementSet Read(ModuleDef module, DataReader reader, CreateResourceDataDelegate createResourceDataDelegate)
		{
			return new ResourceReader(module, ref reader, createResourceDataDelegate).Read();
		}

		// Token: 0x060058E4 RID: 22756 RVA: 0x001B48F8 File Offset: 0x001B48F8
		private ResourceElementSet Read()
		{
			ResourceElementSet resourceElementSet = new ResourceElementSet();
			uint num = this.reader.ReadUInt32();
			if (num != 3203386062U)
			{
				throw new ResourceReaderException(string.Format("Invalid resource sig: {0:X8}", num));
			}
			if (!this.CheckReaders())
			{
				throw new ResourceReaderException("Invalid resource reader");
			}
			int num2 = this.reader.ReadInt32();
			if (num2 != 2)
			{
				throw new ResourceReaderException(string.Format("Invalid resource version: {0}", num2));
			}
			int num3 = this.reader.ReadInt32();
			if (num3 < 0)
			{
				throw new ResourceReaderException(string.Format("Invalid number of resources: {0}", num3));
			}
			int num4 = this.reader.ReadInt32();
			if (num4 < 0)
			{
				throw new ResourceReaderException(string.Format("Invalid number of user types: {0}", num4));
			}
			List<UserResourceType> list = new List<UserResourceType>();
			for (int i = 0; i < num4; i++)
			{
				list.Add(new UserResourceType(this.reader.ReadSerializedString(), ResourceTypeCode.UserTypes + i));
			}
			this.reader.Position = (this.reader.Position + 7U & 4294967288U);
			int[] array = new int[num3];
			for (int j = 0; j < num3; j++)
			{
				array[j] = this.reader.ReadInt32();
			}
			int[] array2 = new int[num3];
			for (int k = 0; k < num3; k++)
			{
				array2[k] = this.reader.ReadInt32();
			}
			uint position = this.reader.Position;
			long num5 = (long)this.reader.ReadInt32();
			long num6 = (long)((ulong)this.reader.Position);
			long num7 = (long)((ulong)this.reader.Length);
			List<ResourceReader.ResourceInfo> list2 = new List<ResourceReader.ResourceInfo>(num3);
			for (int l = 0; l < num3; l++)
			{
				this.reader.Position = (uint)(num6 + (long)array2[l]);
				string name = this.reader.ReadSerializedString(Encoding.Unicode);
				long offset = num5 + (long)this.reader.ReadInt32();
				list2.Add(new ResourceReader.ResourceInfo(name, offset));
			}
			list2.Sort((ResourceReader.ResourceInfo a, ResourceReader.ResourceInfo b) => a.offset.CompareTo(b.offset));
			for (int m = 0; m < list2.Count; m++)
			{
				ResourceReader.ResourceInfo resourceInfo = list2[m];
				ResourceElement resourceElement = new ResourceElement();
				resourceElement.Name = resourceInfo.name;
				this.reader.Position = (uint)resourceInfo.offset;
				int size = (int)(((m == list2.Count - 1) ? num7 : list2[m + 1].offset) - resourceInfo.offset);
				resourceElement.ResourceData = this.ReadResourceData(list, size);
				resourceElement.ResourceData.StartOffset = this.baseFileOffset + (FileOffset)resourceInfo.offset;
				resourceElement.ResourceData.EndOffset = (FileOffset)(this.baseFileOffset + this.reader.Position);
				resourceElementSet.Add(resourceElement);
			}
			return resourceElementSet;
		}

		// Token: 0x060058E5 RID: 22757 RVA: 0x001B4C10 File Offset: 0x001B4C10
		private IResourceData ReadResourceData(List<UserResourceType> userTypes, int size)
		{
			uint num = this.reader.Position + (uint)size;
			uint num2 = ResourceReader.ReadUInt32(ref this.reader);
			switch (num2)
			{
			case 0U:
				return this.resourceDataFactory.CreateNull();
			case 1U:
				return this.resourceDataFactory.Create(this.reader.ReadSerializedString());
			case 2U:
				return this.resourceDataFactory.Create(this.reader.ReadBoolean());
			case 3U:
				return this.resourceDataFactory.Create(this.reader.ReadChar());
			case 4U:
				return this.resourceDataFactory.Create(this.reader.ReadByte());
			case 5U:
				return this.resourceDataFactory.Create(this.reader.ReadSByte());
			case 6U:
				return this.resourceDataFactory.Create(this.reader.ReadInt16());
			case 7U:
				return this.resourceDataFactory.Create(this.reader.ReadUInt16());
			case 8U:
				return this.resourceDataFactory.Create(this.reader.ReadInt32());
			case 9U:
				return this.resourceDataFactory.Create(this.reader.ReadUInt32());
			case 10U:
				return this.resourceDataFactory.Create(this.reader.ReadInt64());
			case 11U:
				return this.resourceDataFactory.Create(this.reader.ReadUInt64());
			case 12U:
				return this.resourceDataFactory.Create(this.reader.ReadSingle());
			case 13U:
				return this.resourceDataFactory.Create(this.reader.ReadDouble());
			case 14U:
				return this.resourceDataFactory.Create(this.reader.ReadDecimal());
			case 15U:
				return this.resourceDataFactory.Create(DateTime.FromBinary(this.reader.ReadInt64()));
			case 16U:
				return this.resourceDataFactory.Create(new TimeSpan(this.reader.ReadInt64()));
			case 32U:
				return this.resourceDataFactory.Create(this.reader.ReadBytes(this.reader.ReadInt32()));
			case 33U:
				return this.resourceDataFactory.CreateStream(this.reader.ReadBytes(this.reader.ReadInt32()));
			}
			int num3 = (int)(num2 - 64U);
			if (num3 < 0 || num3 >= userTypes.Count)
			{
				throw new ResourceReaderException(string.Format("Invalid resource data code: {0}", num2));
			}
			UserResourceType type = userTypes[num3];
			byte[] array = this.reader.ReadBytes((int)(num - this.reader.Position));
			if (this.createResourceDataDelegate != null)
			{
				IResourceData resourceData = this.createResourceDataDelegate(this.resourceDataFactory, type, array);
				if (resourceData != null)
				{
					return resourceData;
				}
			}
			return this.resourceDataFactory.CreateSerialized(array, type);
		}

		// Token: 0x060058E6 RID: 22758 RVA: 0x001B4F28 File Offset: 0x001B4F28
		private static uint ReadUInt32(ref DataReader reader)
		{
			uint result;
			try
			{
				result = reader.Read7BitEncodedUInt32();
			}
			catch
			{
				throw new ResourceReaderException("Invalid encoded int32");
			}
			return result;
		}

		// Token: 0x060058E7 RID: 22759 RVA: 0x001B4F60 File Offset: 0x001B4F60
		private bool CheckReaders()
		{
			bool result = false;
			int num = this.reader.ReadInt32();
			if (num < 0)
			{
				throw new ResourceReaderException(string.Format("Invalid number of readers: {0}", num));
			}
			int num2 = this.reader.ReadInt32();
			if (num2 < 0)
			{
				throw new ResourceReaderException(string.Format("Invalid readers size: {0:X8}", num2));
			}
			for (int i = 0; i < num; i++)
			{
				string input = this.reader.ReadSerializedString();
				this.reader.ReadSerializedString();
				if (Regex.IsMatch(input, "^System\\.Resources\\.ResourceReader,\\s*mscorlib,"))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x04002AEC RID: 10988
		private DataReader reader;

		// Token: 0x04002AED RID: 10989
		private readonly uint baseFileOffset;

		// Token: 0x04002AEE RID: 10990
		private readonly ResourceDataFactory resourceDataFactory;

		// Token: 0x04002AEF RID: 10991
		private readonly CreateResourceDataDelegate createResourceDataDelegate;

		// Token: 0x02001030 RID: 4144
		private sealed class ResourceInfo
		{
			// Token: 0x06008FB4 RID: 36788 RVA: 0x002AD0CC File Offset: 0x002AD0CC
			public ResourceInfo(string name, long offset)
			{
				this.name = name;
				this.offset = offset;
			}

			// Token: 0x06008FB5 RID: 36789 RVA: 0x002AD0E4 File Offset: 0x002AD0E4
			public override string ToString()
			{
				return string.Format("{0:X8} - {1}", this.offset, this.name);
			}

			// Token: 0x040044F8 RID: 17656
			public string name;

			// Token: 0x040044F9 RID: 17657
			public long offset;
		}
	}
}
