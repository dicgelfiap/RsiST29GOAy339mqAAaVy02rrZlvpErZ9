using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.DotNet
{
	// Token: 0x0200079D RID: 1949
	[ComVisible(true)]
	public struct CustomAttributeReader
	{
		// Token: 0x060045A7 RID: 17831 RVA: 0x0016DF38 File Offset: 0x0016DF38
		public static CustomAttribute Read(ModuleDefMD readerModule, ICustomAttributeType ctor, uint offset)
		{
			return CustomAttributeReader.Read(readerModule, ctor, offset, default(GenericParamContext));
		}

		// Token: 0x060045A8 RID: 17832 RVA: 0x0016DF5C File Offset: 0x0016DF5C
		public static CustomAttribute Read(ModuleDefMD readerModule, ICustomAttributeType ctor, uint offset, GenericParamContext gpContext)
		{
			CustomAttributeReader customAttributeReader = new CustomAttributeReader(readerModule, offset, gpContext);
			CustomAttribute result;
			try
			{
				if (ctor == null)
				{
					result = customAttributeReader.CreateRaw(ctor);
				}
				else
				{
					result = customAttributeReader.Read(ctor);
				}
			}
			catch (CABlobParserException)
			{
				result = customAttributeReader.CreateRaw(ctor);
			}
			catch (IOException)
			{
				result = customAttributeReader.CreateRaw(ctor);
			}
			return result;
		}

		// Token: 0x060045A9 RID: 17833 RVA: 0x0016DFD0 File Offset: 0x0016DFD0
		private CustomAttribute CreateRaw(ICustomAttributeType ctor)
		{
			return new CustomAttribute(ctor, this.GetRawBlob());
		}

		// Token: 0x060045AA RID: 17834 RVA: 0x0016DFE0 File Offset: 0x0016DFE0
		public static CustomAttribute Read(ModuleDef module, byte[] caBlob, ICustomAttributeType ctor)
		{
			return CustomAttributeReader.Read(module, ByteArrayDataReaderFactory.CreateReader(caBlob), ctor, default(GenericParamContext));
		}

		// Token: 0x060045AB RID: 17835 RVA: 0x0016E008 File Offset: 0x0016E008
		public static CustomAttribute Read(ModuleDef module, DataReader reader, ICustomAttributeType ctor)
		{
			return CustomAttributeReader.Read(module, ref reader, ctor, default(GenericParamContext));
		}

		// Token: 0x060045AC RID: 17836 RVA: 0x0016E02C File Offset: 0x0016E02C
		public static CustomAttribute Read(ModuleDef module, byte[] caBlob, ICustomAttributeType ctor, GenericParamContext gpContext)
		{
			return CustomAttributeReader.Read(module, ByteArrayDataReaderFactory.CreateReader(caBlob), ctor, gpContext);
		}

		// Token: 0x060045AD RID: 17837 RVA: 0x0016E03C File Offset: 0x0016E03C
		public static CustomAttribute Read(ModuleDef module, DataReader reader, ICustomAttributeType ctor, GenericParamContext gpContext)
		{
			return CustomAttributeReader.Read(module, ref reader, ctor, gpContext);
		}

		// Token: 0x060045AE RID: 17838 RVA: 0x0016E048 File Offset: 0x0016E048
		private static CustomAttribute Read(ModuleDef module, ref DataReader reader, ICustomAttributeType ctor, GenericParamContext gpContext)
		{
			CustomAttributeReader customAttributeReader = new CustomAttributeReader(module, ref reader, gpContext);
			CustomAttribute result;
			try
			{
				if (ctor == null)
				{
					result = customAttributeReader.CreateRaw(ctor);
				}
				else
				{
					result = customAttributeReader.Read(ctor);
				}
			}
			catch (CABlobParserException)
			{
				result = customAttributeReader.CreateRaw(ctor);
			}
			catch (IOException)
			{
				result = customAttributeReader.CreateRaw(ctor);
			}
			return result;
		}

		// Token: 0x060045AF RID: 17839 RVA: 0x0016E0BC File Offset: 0x0016E0BC
		internal static List<CANamedArgument> ReadNamedArguments(ModuleDef module, ref DataReader reader, int numNamedArgs, GenericParamContext gpContext)
		{
			List<CANamedArgument> result;
			try
			{
				CustomAttributeReader customAttributeReader = new CustomAttributeReader(module, ref reader, gpContext);
				List<CANamedArgument> list = customAttributeReader.ReadNamedArguments(numNamedArgs);
				reader.CurrentOffset = customAttributeReader.reader.CurrentOffset;
				result = list;
			}
			catch (CABlobParserException)
			{
				result = null;
			}
			catch (IOException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060045B0 RID: 17840 RVA: 0x0016E120 File Offset: 0x0016E120
		private CustomAttributeReader(ModuleDefMD readerModule, uint offset, GenericParamContext gpContext)
		{
			this.module = readerModule;
			this.caBlobOffset = offset;
			this.reader = readerModule.BlobStream.CreateReader(offset);
			this.genericArguments = null;
			this.recursionCounter = default(RecursionCounter);
			this.verifyReadAllBytes = false;
			this.gpContext = gpContext;
		}

		// Token: 0x060045B1 RID: 17841 RVA: 0x0016E174 File Offset: 0x0016E174
		private CustomAttributeReader(ModuleDef module, ref DataReader reader, GenericParamContext gpContext)
		{
			this.module = module;
			this.caBlobOffset = 0U;
			this.reader = reader;
			this.genericArguments = null;
			this.recursionCounter = default(RecursionCounter);
			this.verifyReadAllBytes = false;
			this.gpContext = gpContext;
		}

		// Token: 0x060045B2 RID: 17842 RVA: 0x0016E1B4 File Offset: 0x0016E1B4
		private byte[] GetRawBlob()
		{
			return this.reader.ToArray();
		}

		// Token: 0x060045B3 RID: 17843 RVA: 0x0016E1C4 File Offset: 0x0016E1C4
		private CustomAttribute Read(ICustomAttributeType ctor)
		{
			MethodSig methodSig = (ctor != null) ? ctor.MethodSig : null;
			if (methodSig == null)
			{
				throw new CABlobParserException("ctor is null or not a method");
			}
			MemberRef memberRef = ctor as MemberRef;
			if (memberRef != null)
			{
				TypeSpec typeSpec = memberRef.Class as TypeSpec;
				if (typeSpec != null)
				{
					GenericInstSig genericInstSig = typeSpec.TypeSig as GenericInstSig;
					if (genericInstSig != null)
					{
						this.genericArguments = new GenericArguments();
						this.genericArguments.PushTypeArgs(genericInstSig.GenericArguments);
					}
				}
			}
			IList<TypeSig> @params = methodSig.Params;
			if ((@params.Count != 0 || this.reader.Position != this.reader.Length) && this.reader.ReadUInt16() != 1)
			{
				throw new CABlobParserException("Invalid CA blob prolog");
			}
			List<CAArgument> list = new List<CAArgument>(@params.Count);
			int count = @params.Count;
			for (int i = 0; i < count; i++)
			{
				list.Add(this.ReadFixedArg(this.FixTypeSig(@params[i])));
			}
			int numNamedArgs = (int)((this.reader.Position == this.reader.Length) ? 0 : this.reader.ReadUInt16());
			List<CANamedArgument> namedArguments = this.ReadNamedArguments(numNamedArgs);
			if (this.verifyReadAllBytes && this.reader.Position != this.reader.Length)
			{
				throw new CABlobParserException("Not all CA blob bytes were read");
			}
			return new CustomAttribute(ctor, list, namedArguments, this.caBlobOffset);
		}

		// Token: 0x060045B4 RID: 17844 RVA: 0x0016E354 File Offset: 0x0016E354
		private List<CANamedArgument> ReadNamedArguments(int numNamedArgs)
		{
			List<CANamedArgument> list = new List<CANamedArgument>(numNamedArgs);
			int num = 0;
			while (num < numNamedArgs && this.reader.Position != this.reader.Length)
			{
				list.Add(this.ReadNamedArgument());
				num++;
			}
			return list;
		}

		// Token: 0x060045B5 RID: 17845 RVA: 0x0016E3A4 File Offset: 0x0016E3A4
		private TypeSig FixTypeSig(TypeSig type)
		{
			return this.SubstituteGenericParameter(type.RemoveModifiers()).RemoveModifiers();
		}

		// Token: 0x060045B6 RID: 17846 RVA: 0x0016E3B8 File Offset: 0x0016E3B8
		private TypeSig SubstituteGenericParameter(TypeSig type)
		{
			if (this.genericArguments == null)
			{
				return type;
			}
			return this.genericArguments.Resolve(type);
		}

		// Token: 0x060045B7 RID: 17847 RVA: 0x0016E3D4 File Offset: 0x0016E3D4
		private CAArgument ReadFixedArg(TypeSig argType)
		{
			if (!this.recursionCounter.Increment())
			{
				throw new CABlobParserException("Too much recursion");
			}
			if (argType == null)
			{
				throw new CABlobParserException("null argType");
			}
			SZArraySig szarraySig = argType as SZArraySig;
			CAArgument result;
			if (szarraySig != null)
			{
				result = this.ReadArrayArgument(szarraySig);
			}
			else
			{
				result = this.ReadElem(argType);
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x060045B8 RID: 17848 RVA: 0x0016E440 File Offset: 0x0016E440
		private CAArgument ReadElem(TypeSig argType)
		{
			if (argType == null)
			{
				throw new CABlobParserException("null argType");
			}
			TypeSig typeSig;
			object obj = this.ReadValue((SerializationType)argType.ElementType, argType, out typeSig);
			if (typeSig == null)
			{
				throw new CABlobParserException("Invalid arg type");
			}
			if (obj is CAArgument)
			{
				return (CAArgument)obj;
			}
			return new CAArgument(typeSig, obj);
		}

		// Token: 0x060045B9 RID: 17849 RVA: 0x0016E49C File Offset: 0x0016E49C
		private object ReadValue(SerializationType etype, TypeSig argType, out TypeSig realArgType)
		{
			if (!this.recursionCounter.Increment())
			{
				throw new CABlobParserException("Too much recursion");
			}
			object result;
			if (etype <= SerializationType.Type)
			{
				switch (etype)
				{
				case SerializationType.Boolean:
					realArgType = this.module.CorLibTypes.Boolean;
					result = (this.reader.ReadByte() > 0);
					goto IL_407;
				case SerializationType.Char:
					realArgType = this.module.CorLibTypes.Char;
					result = this.reader.ReadChar();
					goto IL_407;
				case SerializationType.I1:
					realArgType = this.module.CorLibTypes.SByte;
					result = this.reader.ReadSByte();
					goto IL_407;
				case SerializationType.U1:
					realArgType = this.module.CorLibTypes.Byte;
					result = this.reader.ReadByte();
					goto IL_407;
				case SerializationType.I2:
					realArgType = this.module.CorLibTypes.Int16;
					result = this.reader.ReadInt16();
					goto IL_407;
				case SerializationType.U2:
					realArgType = this.module.CorLibTypes.UInt16;
					result = this.reader.ReadUInt16();
					goto IL_407;
				case SerializationType.I4:
					realArgType = this.module.CorLibTypes.Int32;
					result = this.reader.ReadInt32();
					goto IL_407;
				case SerializationType.U4:
					realArgType = this.module.CorLibTypes.UInt32;
					result = this.reader.ReadUInt32();
					goto IL_407;
				case SerializationType.I8:
					realArgType = this.module.CorLibTypes.Int64;
					result = this.reader.ReadInt64();
					goto IL_407;
				case SerializationType.U8:
					realArgType = this.module.CorLibTypes.UInt64;
					result = this.reader.ReadUInt64();
					goto IL_407;
				case SerializationType.R4:
					realArgType = this.module.CorLibTypes.Single;
					result = this.reader.ReadSingle();
					goto IL_407;
				case SerializationType.R8:
					realArgType = this.module.CorLibTypes.Double;
					result = this.reader.ReadDouble();
					goto IL_407;
				case SerializationType.String:
					realArgType = this.module.CorLibTypes.String;
					result = this.ReadUTF8String();
					goto IL_407;
				case (SerializationType)15:
				case (SerializationType)16:
				case (SerializationType)19:
				case (SerializationType)20:
				case (SerializationType)21:
				case (SerializationType)22:
				case (SerializationType)23:
				case (SerializationType)24:
				case (SerializationType)25:
				case (SerializationType)26:
				case (SerializationType)27:
					goto IL_3FC;
				case (SerializationType)17:
					if (argType == null)
					{
						throw new CABlobParserException("Invalid element type");
					}
					realArgType = argType;
					result = this.ReadEnumValue(CustomAttributeReader.GetEnumUnderlyingType(argType));
					goto IL_407;
				case (SerializationType)18:
				{
					TypeDefOrRefSig typeDefOrRefSig = argType as TypeDefOrRefSig;
					if (typeDefOrRefSig != null && typeDefOrRefSig.DefinitionAssembly.IsCorLib() && typeDefOrRefSig.Namespace == "System")
					{
						if (typeDefOrRefSig.TypeName == "Type")
						{
							result = this.ReadValue(SerializationType.Type, typeDefOrRefSig, out realArgType);
							goto IL_407;
						}
						if (typeDefOrRefSig.TypeName == "String")
						{
							result = this.ReadValue(SerializationType.String, typeDefOrRefSig, out realArgType);
							goto IL_407;
						}
						if (typeDefOrRefSig.TypeName == "Object")
						{
							result = this.ReadValue(SerializationType.TaggedObject, typeDefOrRefSig, out realArgType);
							goto IL_407;
						}
					}
					realArgType = argType;
					result = this.ReadEnumValue(null);
					goto IL_407;
				}
				case (SerializationType)28:
					break;
				default:
					if (etype != SerializationType.Type)
					{
						goto IL_3FC;
					}
					realArgType = argType;
					result = this.ReadType(true);
					goto IL_407;
				}
			}
			else if (etype != SerializationType.TaggedObject)
			{
				if (etype != SerializationType.Enum)
				{
					goto IL_3FC;
				}
				realArgType = this.ReadType(false);
				result = this.ReadEnumValue(CustomAttributeReader.GetEnumUnderlyingType(realArgType));
				goto IL_407;
			}
			realArgType = this.ReadFieldOrPropType();
			SZArraySig szarraySig = realArgType as SZArraySig;
			if (szarraySig != null)
			{
				result = this.ReadArrayArgument(szarraySig);
				goto IL_407;
			}
			TypeSig typeSig;
			result = this.ReadValue((SerializationType)realArgType.ElementType, realArgType, out typeSig);
			goto IL_407;
			IL_3FC:
			throw new CABlobParserException("Invalid element type");
			IL_407:
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x060045BA RID: 17850 RVA: 0x0016E8C0 File Offset: 0x0016E8C0
		private object ReadEnumValue(TypeSig underlyingType)
		{
			if (underlyingType == null)
			{
				this.verifyReadAllBytes = true;
				return this.reader.ReadInt32();
			}
			if (underlyingType.ElementType < ElementType.Boolean || underlyingType.ElementType > ElementType.U8)
			{
				throw new CABlobParserException("Invalid enum underlying type");
			}
			TypeSig typeSig;
			return this.ReadValue((SerializationType)underlyingType.ElementType, underlyingType, out typeSig);
		}

		// Token: 0x060045BB RID: 17851 RVA: 0x0016E924 File Offset: 0x0016E924
		private TypeSig ReadType(bool canReturnNull)
		{
			UTF8String utf8String = this.ReadUTF8String();
			if (canReturnNull && utf8String == null)
			{
				return null;
			}
			CAAssemblyRefFinder typeNameParserHelper = new CAAssemblyRefFinder(this.module);
			TypeSig typeSig = TypeNameParser.ParseAsTypeSigReflection(this.module, UTF8String.ToSystemStringOrEmpty(utf8String), typeNameParserHelper, this.gpContext);
			if (typeSig == null)
			{
				throw new CABlobParserException("Could not parse type");
			}
			return typeSig;
		}

		// Token: 0x060045BC RID: 17852 RVA: 0x0016E980 File Offset: 0x0016E980
		private static TypeSig GetEnumUnderlyingType(TypeSig type)
		{
			if (type == null)
			{
				throw new CABlobParserException("null enum type");
			}
			TypeDef typeDef = CustomAttributeReader.GetTypeDef(type);
			if (typeDef == null)
			{
				return null;
			}
			if (!typeDef.IsEnum)
			{
				throw new CABlobParserException("Not an enum");
			}
			return typeDef.GetEnumUnderlyingType().RemoveModifiers();
		}

		// Token: 0x060045BD RID: 17853 RVA: 0x0016E9D4 File Offset: 0x0016E9D4
		private static TypeDef GetTypeDef(TypeSig type)
		{
			TypeDefOrRefSig typeDefOrRefSig = type as TypeDefOrRefSig;
			if (typeDefOrRefSig != null)
			{
				TypeDef typeDef = typeDefOrRefSig.TypeDef;
				if (typeDef != null)
				{
					return typeDef;
				}
				TypeRef typeRef = typeDefOrRefSig.TypeRef;
				if (typeRef != null)
				{
					return typeRef.Resolve();
				}
			}
			return null;
		}

		// Token: 0x060045BE RID: 17854 RVA: 0x0016EA18 File Offset: 0x0016EA18
		private CAArgument ReadArrayArgument(SZArraySig arrayType)
		{
			if (!this.recursionCounter.Increment())
			{
				throw new CABlobParserException("Too much recursion");
			}
			CAArgument result = new CAArgument(arrayType);
			int num = this.reader.ReadInt32();
			if (num != -1)
			{
				if (num < 0)
				{
					throw new CABlobParserException("Array is too big");
				}
				List<CAArgument> list = new List<CAArgument>(num);
				result.Value = list;
				for (int i = 0; i < num; i++)
				{
					list.Add(this.ReadFixedArg(this.FixTypeSig(arrayType.Next)));
				}
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x060045BF RID: 17855 RVA: 0x0016EAB4 File Offset: 0x0016EAB4
		private CANamedArgument ReadNamedArgument()
		{
			SerializationType serializationType = (SerializationType)this.reader.ReadByte();
			bool flag;
			if (serializationType != SerializationType.Field)
			{
				if (serializationType != SerializationType.Property)
				{
					throw new CABlobParserException("Named argument is not a field/property");
				}
				flag = false;
			}
			else
			{
				flag = true;
			}
			bool isField = flag;
			TypeSig typeSig = this.ReadFieldOrPropType();
			UTF8String name = this.ReadUTF8String();
			CAArgument argument = this.ReadFixedArg(typeSig);
			return new CANamedArgument(isField, typeSig, name, argument);
		}

		// Token: 0x060045C0 RID: 17856 RVA: 0x0016EB1C File Offset: 0x0016EB1C
		private TypeSig ReadFieldOrPropType()
		{
			if (!this.recursionCounter.Increment())
			{
				throw new CABlobParserException("Too much recursion");
			}
			SerializationType serializationType = (SerializationType)this.reader.ReadByte();
			TypeSig typeSig;
			if (serializationType <= SerializationType.SZArray)
			{
				switch (serializationType)
				{
				case SerializationType.Boolean:
					typeSig = this.module.CorLibTypes.Boolean;
					goto IL_217;
				case SerializationType.Char:
					typeSig = this.module.CorLibTypes.Char;
					goto IL_217;
				case SerializationType.I1:
					typeSig = this.module.CorLibTypes.SByte;
					goto IL_217;
				case SerializationType.U1:
					typeSig = this.module.CorLibTypes.Byte;
					goto IL_217;
				case SerializationType.I2:
					typeSig = this.module.CorLibTypes.Int16;
					goto IL_217;
				case SerializationType.U2:
					typeSig = this.module.CorLibTypes.UInt16;
					goto IL_217;
				case SerializationType.I4:
					typeSig = this.module.CorLibTypes.Int32;
					goto IL_217;
				case SerializationType.U4:
					typeSig = this.module.CorLibTypes.UInt32;
					goto IL_217;
				case SerializationType.I8:
					typeSig = this.module.CorLibTypes.Int64;
					goto IL_217;
				case SerializationType.U8:
					typeSig = this.module.CorLibTypes.UInt64;
					goto IL_217;
				case SerializationType.R4:
					typeSig = this.module.CorLibTypes.Single;
					goto IL_217;
				case SerializationType.R8:
					typeSig = this.module.CorLibTypes.Double;
					goto IL_217;
				case SerializationType.String:
					typeSig = this.module.CorLibTypes.String;
					goto IL_217;
				default:
					if (serializationType == SerializationType.SZArray)
					{
						typeSig = new SZArraySig(this.ReadFieldOrPropType());
						goto IL_217;
					}
					break;
				}
			}
			else
			{
				if (serializationType == SerializationType.Type)
				{
					typeSig = new ClassSig(this.module.CorLibTypes.GetTypeRef("System", "Type"));
					goto IL_217;
				}
				if (serializationType == SerializationType.TaggedObject)
				{
					typeSig = this.module.CorLibTypes.Object;
					goto IL_217;
				}
				if (serializationType == SerializationType.Enum)
				{
					typeSig = this.ReadType(false);
					goto IL_217;
				}
			}
			throw new CABlobParserException("Invalid type");
			IL_217:
			TypeSig result = typeSig;
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x060045C1 RID: 17857 RVA: 0x0016ED50 File Offset: 0x0016ED50
		private UTF8String ReadUTF8String()
		{
			if (this.reader.ReadByte() == 255)
			{
				return null;
			}
			uint position = this.reader.Position;
			this.reader.Position = position - 1U;
			uint num;
			if (!this.reader.TryReadCompressedUInt32(out num))
			{
				throw new CABlobParserException("Could not read compressed UInt32");
			}
			if (num == 0U)
			{
				return UTF8String.Empty;
			}
			return new UTF8String(this.reader.ReadBytes((int)num));
		}

		// Token: 0x0400244F RID: 9295
		private readonly ModuleDef module;

		// Token: 0x04002450 RID: 9296
		private DataReader reader;

		// Token: 0x04002451 RID: 9297
		private readonly uint caBlobOffset;

		// Token: 0x04002452 RID: 9298
		private readonly GenericParamContext gpContext;

		// Token: 0x04002453 RID: 9299
		private GenericArguments genericArguments;

		// Token: 0x04002454 RID: 9300
		private RecursionCounter recursionCounter;

		// Token: 0x04002455 RID: 9301
		private bool verifyReadAllBytes;
	}
}
