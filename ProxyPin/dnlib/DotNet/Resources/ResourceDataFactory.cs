using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace dnlib.DotNet.Resources
{
	// Token: 0x020008E5 RID: 2277
	[ComVisible(true)]
	public class ResourceDataFactory
	{
		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x060058AF RID: 22703 RVA: 0x001B4390 File Offset: 0x001B4390
		protected ModuleDef Module
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x060058B0 RID: 22704 RVA: 0x001B4398 File Offset: 0x001B4398
		public ResourceDataFactory(ModuleDef module)
		{
			this.module = module;
			this.moduleMD = (module as ModuleDefMD);
		}

		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x060058B1 RID: 22705 RVA: 0x001B43D4 File Offset: 0x001B43D4
		public int Count
		{
			get
			{
				return this.dict.Count;
			}
		}

		// Token: 0x060058B2 RID: 22706 RVA: 0x001B43E4 File Offset: 0x001B43E4
		public BuiltInResourceData CreateNull()
		{
			return new BuiltInResourceData(ResourceTypeCode.Null, null);
		}

		// Token: 0x060058B3 RID: 22707 RVA: 0x001B43F0 File Offset: 0x001B43F0
		public BuiltInResourceData Create(string value)
		{
			return new BuiltInResourceData(ResourceTypeCode.String, value);
		}

		// Token: 0x060058B4 RID: 22708 RVA: 0x001B43FC File Offset: 0x001B43FC
		public BuiltInResourceData Create(bool value)
		{
			return new BuiltInResourceData(ResourceTypeCode.Boolean, value);
		}

		// Token: 0x060058B5 RID: 22709 RVA: 0x001B440C File Offset: 0x001B440C
		public BuiltInResourceData Create(char value)
		{
			return new BuiltInResourceData(ResourceTypeCode.Char, value);
		}

		// Token: 0x060058B6 RID: 22710 RVA: 0x001B441C File Offset: 0x001B441C
		public BuiltInResourceData Create(byte value)
		{
			return new BuiltInResourceData(ResourceTypeCode.Byte, value);
		}

		// Token: 0x060058B7 RID: 22711 RVA: 0x001B442C File Offset: 0x001B442C
		public BuiltInResourceData Create(sbyte value)
		{
			return new BuiltInResourceData(ResourceTypeCode.SByte, value);
		}

		// Token: 0x060058B8 RID: 22712 RVA: 0x001B443C File Offset: 0x001B443C
		public BuiltInResourceData Create(short value)
		{
			return new BuiltInResourceData(ResourceTypeCode.Int16, value);
		}

		// Token: 0x060058B9 RID: 22713 RVA: 0x001B444C File Offset: 0x001B444C
		public BuiltInResourceData Create(ushort value)
		{
			return new BuiltInResourceData(ResourceTypeCode.UInt16, value);
		}

		// Token: 0x060058BA RID: 22714 RVA: 0x001B445C File Offset: 0x001B445C
		public BuiltInResourceData Create(int value)
		{
			return new BuiltInResourceData(ResourceTypeCode.Int32, value);
		}

		// Token: 0x060058BB RID: 22715 RVA: 0x001B446C File Offset: 0x001B446C
		public BuiltInResourceData Create(uint value)
		{
			return new BuiltInResourceData(ResourceTypeCode.UInt32, value);
		}

		// Token: 0x060058BC RID: 22716 RVA: 0x001B447C File Offset: 0x001B447C
		public BuiltInResourceData Create(long value)
		{
			return new BuiltInResourceData(ResourceTypeCode.Int64, value);
		}

		// Token: 0x060058BD RID: 22717 RVA: 0x001B448C File Offset: 0x001B448C
		public BuiltInResourceData Create(ulong value)
		{
			return new BuiltInResourceData(ResourceTypeCode.UInt64, value);
		}

		// Token: 0x060058BE RID: 22718 RVA: 0x001B449C File Offset: 0x001B449C
		public BuiltInResourceData Create(float value)
		{
			return new BuiltInResourceData(ResourceTypeCode.Single, value);
		}

		// Token: 0x060058BF RID: 22719 RVA: 0x001B44AC File Offset: 0x001B44AC
		public BuiltInResourceData Create(double value)
		{
			return new BuiltInResourceData(ResourceTypeCode.Double, value);
		}

		// Token: 0x060058C0 RID: 22720 RVA: 0x001B44BC File Offset: 0x001B44BC
		public BuiltInResourceData Create(decimal value)
		{
			return new BuiltInResourceData(ResourceTypeCode.Decimal, value);
		}

		// Token: 0x060058C1 RID: 22721 RVA: 0x001B44CC File Offset: 0x001B44CC
		public BuiltInResourceData Create(DateTime value)
		{
			return new BuiltInResourceData(ResourceTypeCode.DateTime, value);
		}

		// Token: 0x060058C2 RID: 22722 RVA: 0x001B44DC File Offset: 0x001B44DC
		public BuiltInResourceData Create(TimeSpan value)
		{
			return new BuiltInResourceData(ResourceTypeCode.TimeSpan, value);
		}

		// Token: 0x060058C3 RID: 22723 RVA: 0x001B44EC File Offset: 0x001B44EC
		public BuiltInResourceData Create(byte[] value)
		{
			return new BuiltInResourceData(ResourceTypeCode.ByteArray, value);
		}

		// Token: 0x060058C4 RID: 22724 RVA: 0x001B44F8 File Offset: 0x001B44F8
		public BuiltInResourceData CreateStream(byte[] value)
		{
			return new BuiltInResourceData(ResourceTypeCode.Stream, value);
		}

		// Token: 0x060058C5 RID: 22725 RVA: 0x001B4504 File Offset: 0x001B4504
		public BinaryResourceData CreateSerialized(byte[] value, UserResourceType type)
		{
			return new BinaryResourceData(this.CreateUserResourceType(type.Name, true), value);
		}

		// Token: 0x060058C6 RID: 22726 RVA: 0x001B451C File Offset: 0x001B451C
		public BinaryResourceData CreateSerialized(byte[] value)
		{
			string str;
			string str2;
			if (!this.GetSerializedTypeAndAssemblyName(value, out str, out str2))
			{
				throw new ApplicationException("Could not get serialized type name");
			}
			string fullName = str2 + ", " + str;
			return new BinaryResourceData(this.CreateUserResourceType(fullName), value);
		}

		// Token: 0x060058C7 RID: 22727 RVA: 0x001B4564 File Offset: 0x001B4564
		private bool GetSerializedTypeAndAssemblyName(byte[] value, out string assemblyName, out string typeName)
		{
			try
			{
				new BinaryFormatter
				{
					Binder = new ResourceDataFactory.MyBinder()
				}.Deserialize(new MemoryStream(value));
			}
			catch (ResourceDataFactory.MyBinder.OkException ex)
			{
				assemblyName = ex.AssemblyName;
				typeName = ex.TypeName;
				return true;
			}
			catch
			{
			}
			assemblyName = null;
			typeName = null;
			return false;
		}

		// Token: 0x060058C8 RID: 22728 RVA: 0x001B45D8 File Offset: 0x001B45D8
		public UserResourceType CreateUserResourceType(string fullName)
		{
			return this.CreateUserResourceType(fullName, false);
		}

		// Token: 0x060058C9 RID: 22729 RVA: 0x001B45E4 File Offset: 0x001B45E4
		private UserResourceType CreateUserResourceType(string fullName, bool useFullName)
		{
			UserResourceType userResourceType;
			if (this.dict.TryGetValue(fullName, out userResourceType))
			{
				return userResourceType;
			}
			string text = useFullName ? fullName : this.GetRealTypeFullName(fullName);
			userResourceType = new UserResourceType(text, ResourceTypeCode.UserTypes + this.dict.Count);
			this.dict[fullName] = userResourceType;
			this.dict[text] = userResourceType;
			return userResourceType;
		}

		// Token: 0x060058CA RID: 22730 RVA: 0x001B4650 File Offset: 0x001B4650
		private string GetRealTypeFullName(string fullName)
		{
			ITypeDefOrRef typeDefOrRef = TypeNameParser.ParseReflection(this.module, fullName, null);
			if (typeDefOrRef == null)
			{
				return fullName;
			}
			IAssembly definitionAssembly = typeDefOrRef.DefinitionAssembly;
			if (definitionAssembly == null)
			{
				return fullName;
			}
			string result = fullName;
			string realAssemblyName = this.GetRealAssemblyName(definitionAssembly);
			if (!string.IsNullOrEmpty(realAssemblyName))
			{
				result = typeDefOrRef.ReflectionFullName + ", " + realAssemblyName;
			}
			return result;
		}

		// Token: 0x060058CB RID: 22731 RVA: 0x001B46B0 File Offset: 0x001B46B0
		private string GetRealAssemblyName(IAssembly asm)
		{
			string fullName = asm.FullName;
			string result;
			if (!this.asmNameToAsmFullName.TryGetValue(fullName, out result))
			{
				result = (this.asmNameToAsmFullName[fullName] = this.TryGetRealAssemblyName(asm));
			}
			return result;
		}

		// Token: 0x060058CC RID: 22732 RVA: 0x001B46F4 File Offset: 0x001B46F4
		private string TryGetRealAssemblyName(IAssembly asm)
		{
			UTF8String name = asm.Name;
			if (name == this.module.CorLibTypes.AssemblyRef.Name)
			{
				return this.module.CorLibTypes.AssemblyRef.FullName;
			}
			if (this.moduleMD != null)
			{
				AssemblyRef assemblyRef = this.moduleMD.GetAssemblyRef(name);
				if (assemblyRef != null)
				{
					return assemblyRef.FullName;
				}
			}
			return this.GetAssemblyFullName(name);
		}

		// Token: 0x060058CD RID: 22733 RVA: 0x001B4774 File Offset: 0x001B4774
		protected virtual string GetAssemblyFullName(string simpleName)
		{
			return null;
		}

		// Token: 0x060058CE RID: 22734 RVA: 0x001B4778 File Offset: 0x001B4778
		public List<UserResourceType> GetSortedTypes()
		{
			List<UserResourceType> list = new List<UserResourceType>(this.dict.Values);
			list.Sort((UserResourceType a, UserResourceType b) => ((int)a.Code).CompareTo((int)b.Code));
			return list;
		}

		// Token: 0x04002AE5 RID: 10981
		private readonly ModuleDef module;

		// Token: 0x04002AE6 RID: 10982
		private readonly ModuleDefMD moduleMD;

		// Token: 0x04002AE7 RID: 10983
		private readonly Dictionary<string, UserResourceType> dict = new Dictionary<string, UserResourceType>(StringComparer.Ordinal);

		// Token: 0x04002AE8 RID: 10984
		private readonly Dictionary<string, string> asmNameToAsmFullName = new Dictionary<string, string>(StringComparer.Ordinal);

		// Token: 0x0200102E RID: 4142
		private sealed class MyBinder : SerializationBinder
		{
			// Token: 0x06008FAF RID: 36783 RVA: 0x002AD070 File Offset: 0x002AD070
			public override Type BindToType(string assemblyName, string typeName)
			{
				throw new ResourceDataFactory.MyBinder.OkException
				{
					AssemblyName = assemblyName,
					TypeName = typeName
				};
			}

			// Token: 0x02001212 RID: 4626
			public class OkException : Exception
			{
				// Token: 0x17001F52 RID: 8018
				// (get) Token: 0x060096A1 RID: 38561 RVA: 0x002CC790 File Offset: 0x002CC790
				// (set) Token: 0x060096A2 RID: 38562 RVA: 0x002CC798 File Offset: 0x002CC798
				public string AssemblyName { get; set; }

				// Token: 0x17001F53 RID: 8019
				// (get) Token: 0x060096A3 RID: 38563 RVA: 0x002CC7A4 File Offset: 0x002CC7A4
				// (set) Token: 0x060096A4 RID: 38564 RVA: 0x002CC7AC File Offset: 0x002CC7AC
				public string TypeName { get; set; }
			}
		}
	}
}
