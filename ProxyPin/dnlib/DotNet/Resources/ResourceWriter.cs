using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace dnlib.DotNet.Resources
{
	// Token: 0x020008EC RID: 2284
	[ComVisible(true)]
	public sealed class ResourceWriter
	{
		// Token: 0x060058E8 RID: 22760 RVA: 0x001B5000 File Offset: 0x001B5000
		private ResourceWriter(ModuleDef module, Stream stream, ResourceElementSet resources)
		{
			this.module = module;
			this.typeCreator = new ResourceDataFactory(module);
			this.writer = new BinaryWriter(stream);
			this.resources = resources;
		}

		// Token: 0x060058E9 RID: 22761 RVA: 0x001B503C File Offset: 0x001B503C
		public static void Write(ModuleDef module, Stream stream, ResourceElementSet resources)
		{
			new ResourceWriter(module, stream, resources).Write();
		}

		// Token: 0x060058EA RID: 22762 RVA: 0x001B504C File Offset: 0x001B504C
		private void Write()
		{
			this.InitializeUserTypes();
			this.writer.Write(3203386062U);
			this.writer.Write(1);
			this.WriteReaderType();
			this.writer.Write(2);
			this.writer.Write(this.resources.Count);
			this.writer.Write(this.typeCreator.Count);
			foreach (UserResourceType userResourceType in this.typeCreator.GetSortedTypes())
			{
				this.writer.Write(userResourceType.Name);
			}
			int num = 8 - ((int)this.writer.BaseStream.Position & 7);
			if (num != 8)
			{
				for (int i = 0; i < num; i++)
				{
					this.writer.Write(88);
				}
			}
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream, Encoding.Unicode);
			MemoryStream memoryStream2 = new MemoryStream();
			BinaryWriter binaryWriter2 = new BinaryWriter(memoryStream2);
			int[] array = new int[this.resources.Count];
			int[] array2 = new int[this.resources.Count];
			BinaryFormatter formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.File | StreamingContextStates.Persistence));
			int num2 = 0;
			foreach (ResourceElement resourceElement in this.resources.ResourceElements)
			{
				array2[num2] = (int)binaryWriter.BaseStream.Position;
				array[num2] = (int)ResourceWriter.Hash(resourceElement.Name);
				num2++;
				binaryWriter.Write(resourceElement.Name);
				binaryWriter.Write((int)binaryWriter2.BaseStream.Position);
				this.WriteData(binaryWriter2, resourceElement, formatter);
			}
			Array.Sort<int, int>(array, array2);
			foreach (int value in array)
			{
				this.writer.Write(value);
			}
			foreach (int value2 in array2)
			{
				this.writer.Write(value2);
			}
			this.writer.Write((int)this.writer.BaseStream.Position + (int)memoryStream.Length + 4);
			this.writer.Write(memoryStream.ToArray());
			this.writer.Write(memoryStream2.ToArray());
		}

		// Token: 0x060058EB RID: 22763 RVA: 0x001B52F8 File Offset: 0x001B52F8
		private void WriteData(BinaryWriter writer, ResourceElement info, IFormatter formatter)
		{
			ResourceTypeCode resourceType = this.GetResourceType(info.ResourceData);
			ResourceWriter.WriteUInt32(writer, (uint)resourceType);
			info.ResourceData.WriteData(writer, formatter);
		}

		// Token: 0x060058EC RID: 22764 RVA: 0x001B532C File Offset: 0x001B532C
		private static void WriteUInt32(BinaryWriter writer, uint value)
		{
			while (value >= 128U)
			{
				writer.Write((byte)(value | 128U));
				value >>= 7;
			}
			writer.Write((byte)value);
		}

		// Token: 0x060058ED RID: 22765 RVA: 0x001B5358 File Offset: 0x001B5358
		private ResourceTypeCode GetResourceType(IResourceData data)
		{
			if (data is BuiltInResourceData)
			{
				return data.Code;
			}
			UserResourceData key = (UserResourceData)data;
			return this.dataToNewType[key].Code;
		}

		// Token: 0x060058EE RID: 22766 RVA: 0x001B5394 File Offset: 0x001B5394
		private static uint Hash(string key)
		{
			uint num = 5381U;
			foreach (char c in key)
			{
				num = ((num << 5) + num ^ (uint)c);
			}
			return num;
		}

		// Token: 0x060058EF RID: 22767 RVA: 0x001B53D4 File Offset: 0x001B53D4
		private void InitializeUserTypes()
		{
			foreach (ResourceElement resourceElement in this.resources.ResourceElements)
			{
				UserResourceData userResourceData = resourceElement.ResourceData as UserResourceData;
				if (userResourceData != null)
				{
					UserResourceType value = this.typeCreator.CreateUserResourceType(userResourceData.TypeName);
					this.dataToNewType[userResourceData] = value;
				}
			}
		}

		// Token: 0x060058F0 RID: 22768 RVA: 0x001B5458 File Offset: 0x001B5458
		private void WriteReaderType()
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			string mscorlibFullname = this.GetMscorlibFullname();
			binaryWriter.Write("System.Resources.ResourceReader, " + mscorlibFullname);
			binaryWriter.Write("System.Resources.RuntimeResourceSet");
			this.writer.Write((int)memoryStream.Position);
			this.writer.Write(memoryStream.ToArray());
		}

		// Token: 0x060058F1 RID: 22769 RVA: 0x001B54BC File Offset: 0x001B54BC
		private string GetMscorlibFullname()
		{
			if (this.module.CorLibTypes.AssemblyRef.Name == "mscorlib")
			{
				return this.module.CorLibTypes.AssemblyRef.FullName;
			}
			return "mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
		}

		// Token: 0x04002B05 RID: 11013
		private ModuleDef module;

		// Token: 0x04002B06 RID: 11014
		private BinaryWriter writer;

		// Token: 0x04002B07 RID: 11015
		private ResourceElementSet resources;

		// Token: 0x04002B08 RID: 11016
		private ResourceDataFactory typeCreator;

		// Token: 0x04002B09 RID: 11017
		private Dictionary<UserResourceData, UserResourceType> dataToNewType = new Dictionary<UserResourceData, UserResourceType>();
	}
}
