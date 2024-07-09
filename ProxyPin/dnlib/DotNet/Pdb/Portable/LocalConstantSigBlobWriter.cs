using System;
using System.Text;
using dnlib.DotNet.MD;
using dnlib.DotNet.Writer;

namespace dnlib.DotNet.Pdb.Portable
{
	// Token: 0x0200093D RID: 2365
	internal readonly struct LocalConstantSigBlobWriter
	{
		// Token: 0x06005AED RID: 23277 RVA: 0x001BB2BC File Offset: 0x001BB2BC
		private LocalConstantSigBlobWriter(IWriterError helper, dnlib.DotNet.Writer.Metadata systemMetadata)
		{
			this.helper = helper;
			this.systemMetadata = systemMetadata;
		}

		// Token: 0x06005AEE RID: 23278 RVA: 0x001BB2CC File Offset: 0x001BB2CC
		public static void Write(IWriterError helper, dnlib.DotNet.Writer.Metadata systemMetadata, DataWriter writer, TypeSig type, object value)
		{
			LocalConstantSigBlobWriter localConstantSigBlobWriter = new LocalConstantSigBlobWriter(helper, systemMetadata);
			localConstantSigBlobWriter.Write(writer, type, value);
		}

		// Token: 0x06005AEF RID: 23279 RVA: 0x001BB2F4 File Offset: 0x001BB2F4
		private void Write(DataWriter writer, TypeSig type, object value)
		{
			while (type != null)
			{
				ElementType elementType = type.ElementType;
				writer.WriteByte((byte)elementType);
				switch (elementType)
				{
				case ElementType.Boolean:
				case ElementType.Char:
				case ElementType.I1:
				case ElementType.U1:
				case ElementType.I2:
				case ElementType.U2:
				case ElementType.I4:
				case ElementType.U4:
				case ElementType.I8:
				case ElementType.U8:
					this.WritePrimitiveValue(writer, elementType, value);
					return;
				case ElementType.R4:
					if (value is float)
					{
						writer.WriteSingle((float)value);
						return;
					}
					this.helper.Error("Expected a Single constant");
					writer.WriteSingle(0f);
					return;
				case ElementType.R8:
					if (value is double)
					{
						writer.WriteDouble((double)value);
						return;
					}
					this.helper.Error("Expected a Double constant");
					writer.WriteDouble(0.0);
					return;
				case ElementType.String:
					if (value == null)
					{
						writer.WriteByte(byte.MaxValue);
						return;
					}
					if (value is string)
					{
						writer.WriteBytes(Encoding.Unicode.GetBytes((string)value));
						return;
					}
					this.helper.Error("Expected a String constant");
					return;
				case ElementType.Ptr:
				case ElementType.ByRef:
					this.WriteTypeDefOrRef(writer, new TypeSpecUser(type));
					return;
				case ElementType.ValueType:
				{
					ITypeDefOrRef typeDefOrRef = ((ValueTypeSig)type).TypeDefOrRef;
					TypeDef typeDef = typeDefOrRef.ResolveTypeDef();
					if (typeDef == null)
					{
						this.helper.Error(string.Format("Couldn't resolve type 0x{0:X8}", (typeDefOrRef != null) ? typeDefOrRef.MDToken.Raw : 0U));
						return;
					}
					if (!typeDef.IsEnum)
					{
						this.WriteTypeDefOrRef(writer, typeDefOrRef);
						bool flag = false;
						UTF8String left;
						UTF8String left2;
						if (LocalConstantSigBlobWriter.GetName(typeDefOrRef, out left, out left2) && left == LocalConstantSigBlobWriter.stringSystem && typeDefOrRef.DefinitionAssembly.IsCorLib())
						{
							if (left2 == LocalConstantSigBlobWriter.stringDecimal)
							{
								if (value is decimal)
								{
									int[] bits = decimal.GetBits((decimal)value);
									writer.WriteByte((byte)((uint)bits[3] >> 31 << 7 | ((uint)bits[3] >> 16 & 127U)));
									writer.WriteInt32(bits[0]);
									writer.WriteInt32(bits[1]);
									writer.WriteInt32(bits[2]);
								}
								else
								{
									this.helper.Error("Expected a Decimal constant");
									writer.WriteBytes(new byte[13]);
								}
								flag = true;
							}
							else if (left2 == LocalConstantSigBlobWriter.stringDateTime)
							{
								if (value is DateTime)
								{
									writer.WriteInt64(((DateTime)value).Ticks);
								}
								else
								{
									this.helper.Error("Expected a DateTime constant");
									writer.WriteInt64(0L);
								}
								flag = true;
							}
						}
						if (!flag)
						{
							if (value is byte[])
							{
								writer.WriteBytes((byte[])value);
								return;
							}
							if (value != null)
							{
								this.helper.Error("Unsupported constant: " + value.GetType().FullName);
								return;
							}
						}
						return;
					}
					TypeSig a = typeDef.GetEnumUnderlyingType().RemovePinnedAndModifiers();
					ElementType elementType2 = a.GetElementType();
					if (elementType2 - ElementType.Boolean <= 9)
					{
						long position = writer.Position;
						writer.Position = position - 1L;
						writer.WriteByte((byte)a.GetElementType());
						this.WritePrimitiveValue(writer, a.GetElementType(), value);
						this.WriteTypeDefOrRef(writer, typeDefOrRef);
						return;
					}
					this.helper.Error("Invalid enum underlying type");
					return;
				}
				case ElementType.Class:
					this.WriteTypeDefOrRef(writer, ((ClassSig)type).TypeDefOrRef);
					if (value is byte[])
					{
						writer.WriteBytes((byte[])value);
						return;
					}
					if (value != null)
					{
						this.helper.Error("Expected a null constant");
					}
					return;
				case ElementType.Var:
				case ElementType.Array:
				case ElementType.GenericInst:
				case ElementType.TypedByRef:
				case ElementType.I:
				case ElementType.U:
				case ElementType.FnPtr:
				case ElementType.SZArray:
				case ElementType.MVar:
					this.WriteTypeDefOrRef(writer, new TypeSpecUser(type));
					return;
				case ElementType.Object:
					return;
				case ElementType.CModReqd:
				case ElementType.CModOpt:
					this.WriteTypeDefOrRef(writer, ((ModifierSig)type).Modifier);
					type = type.Next;
					continue;
				}
				this.helper.Error("Unsupported element type in LocalConstant sig blob: " + elementType.ToString());
				return;
			}
		}

		// Token: 0x06005AF0 RID: 23280 RVA: 0x001BB7C0 File Offset: 0x001BB7C0
		private static bool GetName(ITypeDefOrRef tdr, out UTF8String @namespace, out UTF8String name)
		{
			TypeRef typeRef = tdr as TypeRef;
			if (typeRef != null)
			{
				@namespace = typeRef.Namespace;
				name = typeRef.Name;
				return true;
			}
			TypeDef typeDef = tdr as TypeDef;
			if (typeDef != null)
			{
				@namespace = typeDef.Namespace;
				name = typeDef.Name;
				return true;
			}
			@namespace = null;
			name = null;
			return false;
		}

		// Token: 0x06005AF1 RID: 23281 RVA: 0x001BB818 File Offset: 0x001BB818
		private void WritePrimitiveValue(DataWriter writer, ElementType et, object value)
		{
			switch (et)
			{
			case ElementType.Boolean:
				if (value is bool)
				{
					writer.WriteBoolean((bool)value);
					return;
				}
				this.helper.Error("Expected a Boolean constant");
				writer.WriteBoolean(false);
				return;
			case ElementType.Char:
				if (value is char)
				{
					writer.WriteUInt16((ushort)((char)value));
					return;
				}
				this.helper.Error("Expected a Char constant");
				writer.WriteUInt16(0);
				return;
			case ElementType.I1:
				if (value is sbyte)
				{
					writer.WriteSByte((sbyte)value);
					return;
				}
				this.helper.Error("Expected a SByte constant");
				writer.WriteSByte(0);
				return;
			case ElementType.U1:
				if (value is byte)
				{
					writer.WriteByte((byte)value);
					return;
				}
				this.helper.Error("Expected a Byte constant");
				writer.WriteByte(0);
				return;
			case ElementType.I2:
				if (value is short)
				{
					writer.WriteInt16((short)value);
					return;
				}
				this.helper.Error("Expected an Int16 constant");
				writer.WriteInt16(0);
				return;
			case ElementType.U2:
				if (value is ushort)
				{
					writer.WriteUInt16((ushort)value);
					return;
				}
				this.helper.Error("Expected a UInt16 constant");
				writer.WriteUInt16(0);
				return;
			case ElementType.I4:
				if (value is int)
				{
					writer.WriteInt32((int)value);
					return;
				}
				this.helper.Error("Expected an Int32 constant");
				writer.WriteInt32(0);
				return;
			case ElementType.U4:
				if (value is uint)
				{
					writer.WriteUInt32((uint)value);
					return;
				}
				this.helper.Error("Expected a UInt32 constant");
				writer.WriteUInt32(0U);
				return;
			case ElementType.I8:
				if (value is long)
				{
					writer.WriteInt64((long)value);
					return;
				}
				this.helper.Error("Expected an Int64 constant");
				writer.WriteInt64(0L);
				return;
			case ElementType.U8:
				if (value is ulong)
				{
					writer.WriteUInt64((ulong)value);
					return;
				}
				this.helper.Error("Expected a UInt64 constant");
				writer.WriteUInt64(0UL);
				return;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06005AF2 RID: 23282 RVA: 0x001BBA48 File Offset: 0x001BBA48
		private void WriteTypeDefOrRef(DataWriter writer, ITypeDefOrRef tdr)
		{
			uint value;
			if (!CodedToken.TypeDefOrRef.Encode(this.systemMetadata.GetToken(tdr), out value))
			{
				this.helper.Error("Couldn't encode a TypeDefOrRef");
				return;
			}
			writer.WriteCompressedUInt32(value);
		}

		// Token: 0x04002BF5 RID: 11253
		private readonly IWriterError helper;

		// Token: 0x04002BF6 RID: 11254
		private readonly dnlib.DotNet.Writer.Metadata systemMetadata;

		// Token: 0x04002BF7 RID: 11255
		private static readonly UTF8String stringSystem = new UTF8String("System");

		// Token: 0x04002BF8 RID: 11256
		private static readonly UTF8String stringDecimal = new UTF8String("Decimal");

		// Token: 0x04002BF9 RID: 11257
		private static readonly UTF8String stringDateTime = new UTF8String("DateTime");
	}
}
