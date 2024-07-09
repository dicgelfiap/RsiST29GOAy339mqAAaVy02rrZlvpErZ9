using System;
using System.IO;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008A9 RID: 2217
	[ComVisible(true)]
	public readonly struct MarshalBlobWriter : IDisposable, IFullNameFactoryHelper
	{
		// Token: 0x060054C9 RID: 21705 RVA: 0x0019D1B4 File Offset: 0x0019D1B4
		public static byte[] Write(ModuleDef module, MarshalType marshalType, IWriterError helper)
		{
			return MarshalBlobWriter.Write(module, marshalType, helper, false);
		}

		// Token: 0x060054CA RID: 21706 RVA: 0x0019D1C0 File Offset: 0x0019D1C0
		public static byte[] Write(ModuleDef module, MarshalType marshalType, IWriterError helper, bool optimizeCustomAttributeSerializedTypeNames)
		{
			byte[] result;
			using (MarshalBlobWriter marshalBlobWriter = new MarshalBlobWriter(module, helper, optimizeCustomAttributeSerializedTypeNames))
			{
				result = marshalBlobWriter.Write(marshalType);
			}
			return result;
		}

		// Token: 0x060054CB RID: 21707 RVA: 0x0019D204 File Offset: 0x0019D204
		private MarshalBlobWriter(ModuleDef module, IWriterError helper, bool optimizeCustomAttributeSerializedTypeNames)
		{
			this.module = module;
			this.outStream = new MemoryStream();
			this.writer = new DataWriter(this.outStream);
			this.helper = helper;
			this.optimizeCustomAttributeSerializedTypeNames = optimizeCustomAttributeSerializedTypeNames;
		}

		// Token: 0x060054CC RID: 21708 RVA: 0x0019D238 File Offset: 0x0019D238
		private byte[] Write(MarshalType marshalType)
		{
			if (marshalType == null)
			{
				return null;
			}
			NativeType nativeType = marshalType.NativeType;
			if (nativeType != (NativeType)4294967295U)
			{
				if (nativeType > (NativeType)255U)
				{
					this.helper.Error("Invalid MarshalType.NativeType");
				}
				this.writer.WriteByte((byte)nativeType);
			}
			bool flag = true;
			if (nativeType <= NativeType.Array)
			{
				switch (nativeType)
				{
				case NativeType.FixedSysString:
				{
					FixedSysStringMarshalType fixedSysStringMarshalType = (FixedSysStringMarshalType)marshalType;
					if (fixedSysStringMarshalType.IsSizeValid)
					{
						this.WriteCompressedUInt32((uint)fixedSysStringMarshalType.Size);
					}
					break;
				}
				case NativeType.ObjectRef:
				case NativeType.Struct:
					break;
				case NativeType.IUnknown:
				case NativeType.IDispatch:
				case NativeType.IntF:
				{
					InterfaceMarshalType interfaceMarshalType = (InterfaceMarshalType)marshalType;
					if (interfaceMarshalType.IsIidParamIndexValid)
					{
						this.WriteCompressedUInt32((uint)interfaceMarshalType.IidParamIndex);
					}
					break;
				}
				case NativeType.SafeArray:
				{
					SafeArrayMarshalType safeArrayMarshalType = (SafeArrayMarshalType)marshalType;
					if (this.UpdateCanWrite(safeArrayMarshalType.IsVariantTypeValid, "VariantType", ref flag))
					{
						this.WriteCompressedUInt32((uint)safeArrayMarshalType.VariantType);
					}
					if (this.UpdateCanWrite(safeArrayMarshalType.IsUserDefinedSubTypeValid, "UserDefinedSubType", ref flag))
					{
						this.Write(safeArrayMarshalType.UserDefinedSubType.AssemblyQualifiedName);
					}
					break;
				}
				case NativeType.FixedArray:
				{
					FixedArrayMarshalType fixedArrayMarshalType = (FixedArrayMarshalType)marshalType;
					if (this.UpdateCanWrite(fixedArrayMarshalType.IsSizeValid, "Size", ref flag))
					{
						this.WriteCompressedUInt32((uint)fixedArrayMarshalType.Size);
					}
					if (this.UpdateCanWrite(fixedArrayMarshalType.IsElementTypeValid, "ElementType", ref flag))
					{
						this.WriteCompressedUInt32((uint)fixedArrayMarshalType.ElementType);
					}
					break;
				}
				default:
					if (nativeType == NativeType.Array)
					{
						ArrayMarshalType arrayMarshalType = (ArrayMarshalType)marshalType;
						if (this.UpdateCanWrite(arrayMarshalType.IsElementTypeValid, "ElementType", ref flag))
						{
							this.WriteCompressedUInt32((uint)arrayMarshalType.ElementType);
						}
						if (this.UpdateCanWrite(arrayMarshalType.IsParamNumberValid, "ParamNumber", ref flag))
						{
							this.WriteCompressedUInt32((uint)arrayMarshalType.ParamNumber);
						}
						if (this.UpdateCanWrite(arrayMarshalType.IsSizeValid, "Size", ref flag))
						{
							this.WriteCompressedUInt32((uint)arrayMarshalType.Size);
						}
						if (this.UpdateCanWrite(arrayMarshalType.IsFlagsValid, "Flags", ref flag))
						{
							this.WriteCompressedUInt32((uint)arrayMarshalType.Flags);
						}
					}
					break;
				}
			}
			else if (nativeType != NativeType.CustomMarshaler)
			{
				if (nativeType == (NativeType)4294967295U)
				{
					byte[] data = ((RawMarshalType)marshalType).Data;
					if (data != null)
					{
						this.writer.WriteBytes(data);
					}
				}
			}
			else
			{
				CustomMarshalType customMarshalType = (CustomMarshalType)marshalType;
				this.Write(customMarshalType.Guid);
				this.Write(customMarshalType.NativeTypeName);
				ITypeDefOrRef customMarshaler = customMarshalType.CustomMarshaler;
				string s = (customMarshaler == null) ? string.Empty : FullNameFactory.AssemblyQualifiedName(customMarshaler, this, null);
				this.Write(s);
				this.Write(customMarshalType.Cookie);
			}
			return this.outStream.ToArray();
		}

		// Token: 0x060054CD RID: 21709 RVA: 0x0019D528 File Offset: 0x0019D528
		private bool UpdateCanWrite(bool isValid, string field, ref bool canWriteMore)
		{
			if (!canWriteMore)
			{
				if (isValid)
				{
					this.helper.Error("MarshalType field " + field + " is valid even though a previous field was invalid");
				}
				return canWriteMore;
			}
			if (!isValid)
			{
				canWriteMore = false;
			}
			return canWriteMore;
		}

		// Token: 0x060054CE RID: 21710 RVA: 0x0019D560 File Offset: 0x0019D560
		private uint WriteCompressedUInt32(uint value)
		{
			return this.writer.WriteCompressedUInt32(this.helper, value);
		}

		// Token: 0x060054CF RID: 21711 RVA: 0x0019D574 File Offset: 0x0019D574
		private void Write(UTF8String s)
		{
			this.writer.Write(this.helper, s);
		}

		// Token: 0x060054D0 RID: 21712 RVA: 0x0019D588 File Offset: 0x0019D588
		public void Dispose()
		{
			MemoryStream memoryStream = this.outStream;
			if (memoryStream == null)
			{
				return;
			}
			memoryStream.Dispose();
		}

		// Token: 0x060054D1 RID: 21713 RVA: 0x0019D5A0 File Offset: 0x0019D5A0
		bool IFullNameFactoryHelper.MustUseAssemblyName(IType type)
		{
			return FullNameFactory.MustUseAssemblyName(this.module, type, this.optimizeCustomAttributeSerializedTypeNames);
		}

		// Token: 0x040028B6 RID: 10422
		private readonly ModuleDef module;

		// Token: 0x040028B7 RID: 10423
		private readonly MemoryStream outStream;

		// Token: 0x040028B8 RID: 10424
		private readonly DataWriter writer;

		// Token: 0x040028B9 RID: 10425
		private readonly IWriterError helper;

		// Token: 0x040028BA RID: 10426
		private readonly bool optimizeCustomAttributeSerializedTypeNames;
	}
}
