using System;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.DotNet
{
	// Token: 0x020007FD RID: 2045
	[ComVisible(true)]
	public struct MarshalBlobReader
	{
		// Token: 0x06004998 RID: 18840 RVA: 0x00179E50 File Offset: 0x00179E50
		public static MarshalType Read(ModuleDefMD module, uint sig)
		{
			return MarshalBlobReader.Read(module, module.BlobStream.CreateReader(sig), default(GenericParamContext));
		}

		// Token: 0x06004999 RID: 18841 RVA: 0x00179E7C File Offset: 0x00179E7C
		public static MarshalType Read(ModuleDefMD module, uint sig, GenericParamContext gpContext)
		{
			return MarshalBlobReader.Read(module, module.BlobStream.CreateReader(sig), gpContext);
		}

		// Token: 0x0600499A RID: 18842 RVA: 0x00179E94 File Offset: 0x00179E94
		public static MarshalType Read(ModuleDef module, byte[] data)
		{
			return MarshalBlobReader.Read(module, ByteArrayDataReaderFactory.CreateReader(data), default(GenericParamContext));
		}

		// Token: 0x0600499B RID: 18843 RVA: 0x00179EBC File Offset: 0x00179EBC
		public static MarshalType Read(ModuleDef module, byte[] data, GenericParamContext gpContext)
		{
			return MarshalBlobReader.Read(module, ByteArrayDataReaderFactory.CreateReader(data), gpContext);
		}

		// Token: 0x0600499C RID: 18844 RVA: 0x00179ECC File Offset: 0x00179ECC
		public static MarshalType Read(ModuleDef module, DataReader reader)
		{
			return MarshalBlobReader.Read(module, reader, default(GenericParamContext));
		}

		// Token: 0x0600499D RID: 18845 RVA: 0x00179EF0 File Offset: 0x00179EF0
		public static MarshalType Read(ModuleDef module, DataReader reader, GenericParamContext gpContext)
		{
			return new MarshalBlobReader(module, ref reader, gpContext).Read();
		}

		// Token: 0x0600499E RID: 18846 RVA: 0x00179F14 File Offset: 0x00179F14
		private MarshalBlobReader(ModuleDef module, ref DataReader reader, GenericParamContext gpContext)
		{
			this.module = module;
			this.reader = reader;
			this.gpContext = gpContext;
		}

		// Token: 0x0600499F RID: 18847 RVA: 0x00179F30 File Offset: 0x00179F30
		private MarshalType Read()
		{
			MarshalType result;
			try
			{
				NativeType nativeType = (NativeType)this.reader.ReadByte();
				switch (nativeType)
				{
				case NativeType.FixedSysString:
				{
					int num = (int)(this.CanRead() ? this.reader.ReadCompressedUInt32() : uint.MaxValue);
					result = new FixedSysStringMarshalType(num);
					goto IL_248;
				}
				case NativeType.ObjectRef:
				case NativeType.Struct:
					break;
				case NativeType.IUnknown:
				case NativeType.IDispatch:
				case NativeType.IntF:
				{
					int iidParamIndex = (int)(this.CanRead() ? this.reader.ReadCompressedUInt32() : uint.MaxValue);
					return new InterfaceMarshalType(nativeType, iidParamIndex);
				}
				case NativeType.SafeArray:
				{
					VariantType vt = (VariantType)(this.CanRead() ? this.reader.ReadCompressedUInt32() : uint.MaxValue);
					UTF8String utf8String = this.CanRead() ? this.ReadUTF8String() : null;
					ITypeDefOrRef userDefinedSubType = (utf8String == null) ? null : TypeNameParser.ParseReflection(this.module, UTF8String.ToSystemStringOrEmpty(utf8String), null, this.gpContext);
					result = new SafeArrayMarshalType(vt, userDefinedSubType);
					goto IL_248;
				}
				case NativeType.FixedArray:
				{
					int num = (int)(this.CanRead() ? this.reader.ReadCompressedUInt32() : uint.MaxValue);
					NativeType elementType = (NativeType)(this.CanRead() ? this.reader.ReadCompressedUInt32() : 4294967294U);
					result = new FixedArrayMarshalType(num, elementType);
					goto IL_248;
				}
				default:
					if (nativeType == NativeType.Array)
					{
						NativeType elementType = (NativeType)(this.CanRead() ? this.reader.ReadCompressedUInt32() : 4294967294U);
						int paramNum = (int)(this.CanRead() ? this.reader.ReadCompressedUInt32() : uint.MaxValue);
						int num = (int)(this.CanRead() ? this.reader.ReadCompressedUInt32() : uint.MaxValue);
						int flags = (int)(this.CanRead() ? this.reader.ReadCompressedUInt32() : uint.MaxValue);
						result = new ArrayMarshalType(elementType, paramNum, num, flags);
						goto IL_248;
					}
					if (nativeType == NativeType.CustomMarshaler)
					{
						UTF8String guid = this.ReadUTF8String();
						UTF8String nativeTypeName = this.ReadUTF8String();
						UTF8String utf8String2 = this.ReadUTF8String();
						ITypeDefOrRef custMarshaler = (utf8String2.DataLength == 0) ? null : TypeNameParser.ParseReflection(this.module, UTF8String.ToSystemStringOrEmpty(utf8String2), new CAAssemblyRefFinder(this.module), this.gpContext);
						UTF8String cookie = this.ReadUTF8String();
						result = new CustomMarshalType(guid, nativeTypeName, custMarshaler, cookie);
						goto IL_248;
					}
					break;
				}
				result = new MarshalType(nativeType);
				IL_248:;
			}
			catch
			{
				result = new RawMarshalType(this.reader.ToArray());
			}
			return result;
		}

		// Token: 0x060049A0 RID: 18848 RVA: 0x0017A1C4 File Offset: 0x0017A1C4
		private bool CanRead()
		{
			return this.reader.Position < this.reader.Length;
		}

		// Token: 0x060049A1 RID: 18849 RVA: 0x0017A1E0 File Offset: 0x0017A1E0
		private UTF8String ReadUTF8String()
		{
			uint num = this.reader.ReadCompressedUInt32();
			if (num != 0U)
			{
				return new UTF8String(this.reader.ReadBytes((int)num));
			}
			return UTF8String.Empty;
		}

		// Token: 0x0400253F RID: 9535
		private readonly ModuleDef module;

		// Token: 0x04002540 RID: 9536
		private DataReader reader;

		// Token: 0x04002541 RID: 9537
		private readonly GenericParamContext gpContext;
	}
}
