using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x02000894 RID: 2196
	[ComVisible(true)]
	public struct CustomAttributeWriter : IDisposable
	{
		// Token: 0x060053F6 RID: 21494 RVA: 0x0019947C File Offset: 0x0019947C
		public static byte[] Write(ICustomAttributeWriterHelper helper, CustomAttribute ca)
		{
			byte[] result;
			using (CustomAttributeWriter customAttributeWriter = new CustomAttributeWriter(helper))
			{
				customAttributeWriter.Write(ca);
				result = customAttributeWriter.GetResult();
			}
			return result;
		}

		// Token: 0x060053F7 RID: 21495 RVA: 0x001994C8 File Offset: 0x001994C8
		internal static byte[] Write(ICustomAttributeWriterHelper helper, CustomAttribute ca, DataWriterContext context)
		{
			byte[] result;
			using (CustomAttributeWriter customAttributeWriter = new CustomAttributeWriter(helper, context))
			{
				customAttributeWriter.Write(ca);
				result = customAttributeWriter.GetResult();
			}
			return result;
		}

		// Token: 0x060053F8 RID: 21496 RVA: 0x00199514 File Offset: 0x00199514
		internal static byte[] Write(ICustomAttributeWriterHelper helper, IList<CANamedArgument> namedArgs)
		{
			byte[] result;
			using (CustomAttributeWriter customAttributeWriter = new CustomAttributeWriter(helper))
			{
				customAttributeWriter.Write(namedArgs);
				result = customAttributeWriter.GetResult();
			}
			return result;
		}

		// Token: 0x060053F9 RID: 21497 RVA: 0x00199560 File Offset: 0x00199560
		internal static byte[] Write(ICustomAttributeWriterHelper helper, IList<CANamedArgument> namedArgs, DataWriterContext context)
		{
			byte[] result;
			using (CustomAttributeWriter customAttributeWriter = new CustomAttributeWriter(helper, context))
			{
				customAttributeWriter.Write(namedArgs);
				result = customAttributeWriter.GetResult();
			}
			return result;
		}

		// Token: 0x060053FA RID: 21498 RVA: 0x001995AC File Offset: 0x001995AC
		private CustomAttributeWriter(ICustomAttributeWriterHelper helper)
		{
			this.helper = helper;
			this.recursionCounter = default(RecursionCounter);
			this.outStream = new MemoryStream();
			this.writer = new DataWriter(this.outStream);
			this.genericArguments = null;
			this.disposeStream = true;
		}

		// Token: 0x060053FB RID: 21499 RVA: 0x001995EC File Offset: 0x001995EC
		private CustomAttributeWriter(ICustomAttributeWriterHelper helper, DataWriterContext context)
		{
			this.helper = helper;
			this.recursionCounter = default(RecursionCounter);
			this.outStream = context.OutStream;
			this.writer = context.Writer;
			this.genericArguments = null;
			this.disposeStream = false;
			this.outStream.SetLength(0L);
			this.outStream.Position = 0L;
		}

		// Token: 0x060053FC RID: 21500 RVA: 0x00199650 File Offset: 0x00199650
		private byte[] GetResult()
		{
			return this.outStream.ToArray();
		}

		// Token: 0x060053FD RID: 21501 RVA: 0x00199660 File Offset: 0x00199660
		private void Write(CustomAttribute ca)
		{
			if (ca == null)
			{
				this.helper.Error("The custom attribute is null");
				return;
			}
			if (ca.IsRawBlob)
			{
				if ((ca.ConstructorArguments != null && ca.ConstructorArguments.Count > 0) || (ca.NamedArguments != null && ca.NamedArguments.Count > 0))
				{
					this.helper.Error("Raw custom attribute contains arguments and/or named arguments");
				}
				this.writer.WriteBytes(ca.RawData);
				return;
			}
			if (ca.Constructor == null)
			{
				this.helper.Error("Custom attribute ctor is null");
				return;
			}
			MethodSig methodSig = CustomAttributeWriter.GetMethodSig(ca.Constructor);
			if (methodSig == null)
			{
				this.helper.Error("Custom attribute ctor's method signature is invalid");
				return;
			}
			if (ca.ConstructorArguments.Count != methodSig.Params.Count)
			{
				this.helper.Error("Custom attribute arguments count != method sig arguments count");
			}
			if (methodSig.ParamsAfterSentinel != null && methodSig.ParamsAfterSentinel.Count > 0)
			{
				this.helper.Error("Custom attribute ctor has parameters after the sentinel");
			}
			if (ca.NamedArguments.Count > 65535)
			{
				this.helper.Error("Custom attribute has too many named arguments");
			}
			MemberRef memberRef = ca.Constructor as MemberRef;
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
			this.writer.WriteUInt16(1);
			int num = Math.Min(methodSig.Params.Count, ca.ConstructorArguments.Count);
			for (int i = 0; i < num; i++)
			{
				this.WriteValue(this.FixTypeSig(methodSig.Params[i]), ca.ConstructorArguments[i]);
			}
			int num2 = Math.Min(65535, ca.NamedArguments.Count);
			this.writer.WriteUInt16((ushort)num2);
			for (int j = 0; j < num2; j++)
			{
				this.Write(ca.NamedArguments[j]);
			}
		}

		// Token: 0x060053FE RID: 21502 RVA: 0x001998AC File Offset: 0x001998AC
		private void Write(IList<CANamedArgument> namedArgs)
		{
			if (namedArgs == null || namedArgs.Count > 536870911)
			{
				this.helper.Error("Too many custom attribute named arguments");
				namedArgs = Array2.Empty<CANamedArgument>();
			}
			this.writer.WriteCompressedUInt32((uint)namedArgs.Count);
			for (int i = 0; i < namedArgs.Count; i++)
			{
				this.Write(namedArgs[i]);
			}
		}

		// Token: 0x060053FF RID: 21503 RVA: 0x0019991C File Offset: 0x0019991C
		private TypeSig FixTypeSig(TypeSig type)
		{
			return this.SubstituteGenericParameter(type.RemoveModifiers()).RemoveModifiers();
		}

		// Token: 0x06005400 RID: 21504 RVA: 0x00199930 File Offset: 0x00199930
		private TypeSig SubstituteGenericParameter(TypeSig type)
		{
			if (this.genericArguments == null)
			{
				return type;
			}
			return this.genericArguments.Resolve(type);
		}

		// Token: 0x06005401 RID: 21505 RVA: 0x0019994C File Offset: 0x0019994C
		private void WriteValue(TypeSig argType, CAArgument value)
		{
			if (argType == null || value.Type == null)
			{
				this.helper.Error("Custom attribute argument type is null");
				return;
			}
			if (!this.recursionCounter.Increment())
			{
				this.helper.Error("Infinite recursion");
				return;
			}
			SZArraySig szarraySig = argType as SZArraySig;
			if (szarraySig != null)
			{
				IList<CAArgument> list = value.Value as IList<CAArgument>;
				if (list == null && value.Value != null)
				{
					this.helper.Error("CAArgument.Value is not null or an array");
				}
				this.WriteArrayValue(szarraySig, list);
			}
			else
			{
				this.WriteElem(argType, value);
			}
			this.recursionCounter.Decrement();
		}

		// Token: 0x06005402 RID: 21506 RVA: 0x00199A00 File Offset: 0x00199A00
		private void WriteArrayValue(SZArraySig arrayType, IList<CAArgument> args)
		{
			if (arrayType == null)
			{
				this.helper.Error("Custom attribute: Array type is null");
				return;
			}
			if (args == null)
			{
				this.writer.WriteUInt32(uint.MaxValue);
				return;
			}
			this.writer.WriteUInt32((uint)args.Count);
			TypeSig argType = this.FixTypeSig(arrayType.Next);
			for (int i = 0; i < args.Count; i++)
			{
				this.WriteValue(argType, args[i]);
			}
		}

		// Token: 0x06005403 RID: 21507 RVA: 0x00199A7C File Offset: 0x00199A7C
		private bool VerifyTypeAndValue(CAArgument value, ElementType etype)
		{
			if (!CustomAttributeWriter.VerifyType(value.Type, etype))
			{
				this.helper.Error("Custom attribute arg type != value.Type");
				return false;
			}
			if (!CustomAttributeWriter.VerifyValue(value.Value, etype))
			{
				this.helper.Error("Custom attribute value.Value's type != value.Type");
				return false;
			}
			return true;
		}

		// Token: 0x06005404 RID: 21508 RVA: 0x00199AD8 File Offset: 0x00199AD8
		private bool VerifyTypeAndValue(CAArgument value, ElementType etype, Type valueType)
		{
			if (!CustomAttributeWriter.VerifyType(value.Type, etype))
			{
				this.helper.Error("Custom attribute arg type != value.Type");
				return false;
			}
			return value.Value == null || value.Value.GetType() == valueType;
		}

		// Token: 0x06005405 RID: 21509 RVA: 0x00199B30 File Offset: 0x00199B30
		private static bool VerifyType(TypeSig type, ElementType etype)
		{
			type = type.RemoveModifiers();
			return type != null && (etype == type.ElementType || type.ElementType == ElementType.ValueType);
		}

		// Token: 0x06005406 RID: 21510 RVA: 0x00199B5C File Offset: 0x00199B5C
		private static bool VerifyValue(object o, ElementType etype)
		{
			if (o == null)
			{
				return false;
			}
			bool result;
			switch (Type.GetTypeCode(o.GetType()))
			{
			case TypeCode.Boolean:
				result = (etype == ElementType.Boolean);
				break;
			case TypeCode.Char:
				result = (etype == ElementType.Char);
				break;
			case TypeCode.SByte:
				result = (etype == ElementType.I1);
				break;
			case TypeCode.Byte:
				result = (etype == ElementType.U1);
				break;
			case TypeCode.Int16:
				result = (etype == ElementType.I2);
				break;
			case TypeCode.UInt16:
				result = (etype == ElementType.U2);
				break;
			case TypeCode.Int32:
				result = (etype == ElementType.I4);
				break;
			case TypeCode.UInt32:
				result = (etype == ElementType.U4);
				break;
			case TypeCode.Int64:
				result = (etype == ElementType.I8);
				break;
			case TypeCode.UInt64:
				result = (etype == ElementType.U8);
				break;
			case TypeCode.Single:
				result = (etype == ElementType.R4);
				break;
			case TypeCode.Double:
				result = (etype == ElementType.R8);
				break;
			default:
				result = false;
				break;
			}
			return result;
		}

		// Token: 0x06005407 RID: 21511 RVA: 0x00199C40 File Offset: 0x00199C40
		private static ulong ToUInt64(object o)
		{
			ulong result;
			CustomAttributeWriter.ToUInt64(o, out result);
			return result;
		}

		// Token: 0x06005408 RID: 21512 RVA: 0x00199C5C File Offset: 0x00199C5C
		private static bool ToUInt64(object o, out ulong result)
		{
			if (o == null)
			{
				result = 0UL;
				return false;
			}
			switch (Type.GetTypeCode(o.GetType()))
			{
			case TypeCode.Boolean:
				result = (((bool)o) ? 1UL : 0UL);
				return true;
			case TypeCode.Char:
				result = (ulong)((char)o);
				return true;
			case TypeCode.SByte:
				result = (ulong)((long)((sbyte)o));
				return true;
			case TypeCode.Byte:
				result = (ulong)((byte)o);
				return true;
			case TypeCode.Int16:
				result = (ulong)((long)((short)o));
				return true;
			case TypeCode.UInt16:
				result = (ulong)((ushort)o);
				return true;
			case TypeCode.Int32:
				result = (ulong)((long)((int)o));
				return true;
			case TypeCode.UInt32:
				result = (ulong)((uint)o);
				return true;
			case TypeCode.Int64:
				result = (ulong)((long)o);
				return true;
			case TypeCode.UInt64:
				result = (ulong)o;
				return true;
			case TypeCode.Single:
				result = (ulong)((float)o);
				return true;
			case TypeCode.Double:
				result = (ulong)((double)o);
				return true;
			default:
				result = 0UL;
				return false;
			}
		}

		// Token: 0x06005409 RID: 21513 RVA: 0x00199D58 File Offset: 0x00199D58
		private static double ToDouble(object o)
		{
			double result;
			CustomAttributeWriter.ToDouble(o, out result);
			return result;
		}

		// Token: 0x0600540A RID: 21514 RVA: 0x00199D74 File Offset: 0x00199D74
		private static bool ToDouble(object o, out double result)
		{
			if (o == null)
			{
				result = double.NaN;
				return false;
			}
			switch (Type.GetTypeCode(o.GetType()))
			{
			case TypeCode.Boolean:
				result = (double)(((bool)o) ? 1 : 0);
				return true;
			case TypeCode.Char:
				result = (double)((char)o);
				return true;
			case TypeCode.SByte:
				result = (double)((sbyte)o);
				return true;
			case TypeCode.Byte:
				result = (double)((byte)o);
				return true;
			case TypeCode.Int16:
				result = (double)((short)o);
				return true;
			case TypeCode.UInt16:
				result = (double)((ushort)o);
				return true;
			case TypeCode.Int32:
				result = (double)((int)o);
				return true;
			case TypeCode.UInt32:
				result = (uint)o;
				return true;
			case TypeCode.Int64:
				result = (double)((long)o);
				return true;
			case TypeCode.UInt64:
				result = (ulong)o;
				return true;
			case TypeCode.Single:
				result = (double)((float)o);
				return true;
			case TypeCode.Double:
				result = (double)o;
				return true;
			default:
				result = double.NaN;
				return false;
			}
		}

		// Token: 0x0600540B RID: 21515 RVA: 0x00199E80 File Offset: 0x00199E80
		private void WriteElem(TypeSig argType, CAArgument value)
		{
			if (argType == null)
			{
				this.helper.Error("Custom attribute: Arg type is null");
				argType = value.Type;
				if (argType == null)
				{
					return;
				}
			}
			if (!this.recursionCounter.Increment())
			{
				this.helper.Error("Infinite recursion");
				return;
			}
			switch (argType.ElementType)
			{
			case ElementType.Boolean:
				if (!this.VerifyTypeAndValue(value, ElementType.Boolean))
				{
					this.writer.WriteBoolean(CustomAttributeWriter.ToUInt64(value.Value) > 0UL);
					goto IL_691;
				}
				this.writer.WriteBoolean((bool)value.Value);
				goto IL_691;
			case ElementType.Char:
				if (!this.VerifyTypeAndValue(value, ElementType.Char))
				{
					this.writer.WriteUInt16((ushort)CustomAttributeWriter.ToUInt64(value.Value));
					goto IL_691;
				}
				this.writer.WriteUInt16((ushort)((char)value.Value));
				goto IL_691;
			case ElementType.I1:
				if (!this.VerifyTypeAndValue(value, ElementType.I1))
				{
					this.writer.WriteSByte((sbyte)CustomAttributeWriter.ToUInt64(value.Value));
					goto IL_691;
				}
				this.writer.WriteSByte((sbyte)value.Value);
				goto IL_691;
			case ElementType.U1:
				if (!this.VerifyTypeAndValue(value, ElementType.U1))
				{
					this.writer.WriteByte((byte)CustomAttributeWriter.ToUInt64(value.Value));
					goto IL_691;
				}
				this.writer.WriteByte((byte)value.Value);
				goto IL_691;
			case ElementType.I2:
				if (!this.VerifyTypeAndValue(value, ElementType.I2))
				{
					this.writer.WriteInt16((short)CustomAttributeWriter.ToUInt64(value.Value));
					goto IL_691;
				}
				this.writer.WriteInt16((short)value.Value);
				goto IL_691;
			case ElementType.U2:
				if (!this.VerifyTypeAndValue(value, ElementType.U2))
				{
					this.writer.WriteUInt16((ushort)CustomAttributeWriter.ToUInt64(value.Value));
					goto IL_691;
				}
				this.writer.WriteUInt16((ushort)value.Value);
				goto IL_691;
			case ElementType.I4:
				if (!this.VerifyTypeAndValue(value, ElementType.I4))
				{
					this.writer.WriteInt32((int)CustomAttributeWriter.ToUInt64(value.Value));
					goto IL_691;
				}
				this.writer.WriteInt32((int)value.Value);
				goto IL_691;
			case ElementType.U4:
				if (!this.VerifyTypeAndValue(value, ElementType.U4))
				{
					this.writer.WriteUInt32((uint)CustomAttributeWriter.ToUInt64(value.Value));
					goto IL_691;
				}
				this.writer.WriteUInt32((uint)value.Value);
				goto IL_691;
			case ElementType.I8:
				if (!this.VerifyTypeAndValue(value, ElementType.I8))
				{
					this.writer.WriteInt64((long)CustomAttributeWriter.ToUInt64(value.Value));
					goto IL_691;
				}
				this.writer.WriteInt64((long)value.Value);
				goto IL_691;
			case ElementType.U8:
				if (!this.VerifyTypeAndValue(value, ElementType.U8))
				{
					this.writer.WriteUInt64(CustomAttributeWriter.ToUInt64(value.Value));
					goto IL_691;
				}
				this.writer.WriteUInt64((ulong)value.Value);
				goto IL_691;
			case ElementType.R4:
				if (!this.VerifyTypeAndValue(value, ElementType.R4))
				{
					this.writer.WriteSingle((float)CustomAttributeWriter.ToDouble(value.Value));
					goto IL_691;
				}
				this.writer.WriteSingle((float)value.Value);
				goto IL_691;
			case ElementType.R8:
				if (!this.VerifyTypeAndValue(value, ElementType.R8))
				{
					this.writer.WriteDouble(CustomAttributeWriter.ToDouble(value.Value));
					goto IL_691;
				}
				this.writer.WriteDouble((double)value.Value);
				goto IL_691;
			case ElementType.String:
				if (this.VerifyTypeAndValue(value, ElementType.String, typeof(UTF8String)))
				{
					this.WriteUTF8String((UTF8String)value.Value);
					goto IL_691;
				}
				if (this.VerifyTypeAndValue(value, ElementType.String, typeof(string)))
				{
					this.WriteUTF8String((string)value.Value);
					goto IL_691;
				}
				this.WriteUTF8String(UTF8String.Empty);
				goto IL_691;
			case ElementType.ValueType:
			{
				ITypeDefOrRef typeDefOrRef = ((TypeDefOrRefSig)argType).TypeDefOrRef;
				TypeSig enumUnderlyingType = CustomAttributeWriter.GetEnumUnderlyingType(argType);
				if (enumUnderlyingType != null)
				{
					this.WriteElem(enumUnderlyingType, value);
					goto IL_691;
				}
				if (!(typeDefOrRef is TypeRef) || !this.TryWriteEnumUnderlyingTypeValue(value.Value))
				{
					this.helper.Error("Custom attribute value is not an enum");
					goto IL_691;
				}
				goto IL_691;
			}
			case ElementType.Class:
			{
				ITypeDefOrRef typeDefOrRef = ((TypeDefOrRefSig)argType).TypeDefOrRef;
				if (CustomAttributeWriter.CheckCorLibType(argType, "Type"))
				{
					if (!CustomAttributeWriter.CheckCorLibType(value.Type, "Type"))
					{
						this.helper.Error("Custom attribute value type is not System.Type");
						this.WriteUTF8String(UTF8String.Empty);
						goto IL_691;
					}
					TypeSig typeSig = value.Value as TypeSig;
					if (typeSig != null)
					{
						this.WriteType(typeSig);
						goto IL_691;
					}
					if (value.Value == null)
					{
						this.WriteUTF8String(null);
						goto IL_691;
					}
					this.helper.Error("Custom attribute value is not a type");
					this.WriteUTF8String(UTF8String.Empty);
					goto IL_691;
				}
				else if (typeDefOrRef is TypeRef && this.TryWriteEnumUnderlyingTypeValue(value.Value))
				{
					goto IL_691;
				}
				break;
			}
			case ElementType.Object:
				this.WriteFieldOrPropType(value.Type);
				this.WriteElem(value.Type, value);
				goto IL_691;
			case ElementType.SZArray:
				this.WriteValue(argType, value);
				goto IL_691;
			}
			this.helper.Error("Invalid or unsupported element type in custom attribute");
			IL_691:
			this.recursionCounter.Decrement();
		}

		// Token: 0x0600540C RID: 21516 RVA: 0x0019A530 File Offset: 0x0019A530
		private bool TryWriteEnumUnderlyingTypeValue(object o)
		{
			if (o == null)
			{
				return false;
			}
			switch (Type.GetTypeCode(o.GetType()))
			{
			case TypeCode.Boolean:
				this.writer.WriteBoolean((bool)o);
				break;
			case TypeCode.Char:
				this.writer.WriteUInt16((ushort)((char)o));
				break;
			case TypeCode.SByte:
				this.writer.WriteSByte((sbyte)o);
				break;
			case TypeCode.Byte:
				this.writer.WriteByte((byte)o);
				break;
			case TypeCode.Int16:
				this.writer.WriteInt16((short)o);
				break;
			case TypeCode.UInt16:
				this.writer.WriteUInt16((ushort)o);
				break;
			case TypeCode.Int32:
				this.writer.WriteInt32((int)o);
				break;
			case TypeCode.UInt32:
				this.writer.WriteUInt32((uint)o);
				break;
			case TypeCode.Int64:
				this.writer.WriteInt64((long)o);
				break;
			case TypeCode.UInt64:
				this.writer.WriteUInt64((ulong)o);
				break;
			default:
				return false;
			}
			return true;
		}

		// Token: 0x0600540D RID: 21517 RVA: 0x0019A66C File Offset: 0x0019A66C
		private static TypeSig GetEnumUnderlyingType(TypeSig type)
		{
			TypeDef enumTypeDef = CustomAttributeWriter.GetEnumTypeDef(type);
			if (enumTypeDef == null)
			{
				return null;
			}
			return enumTypeDef.GetEnumUnderlyingType().RemoveModifiers();
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x0019A698 File Offset: 0x0019A698
		private static TypeDef GetEnumTypeDef(TypeSig type)
		{
			if (type == null)
			{
				return null;
			}
			TypeDef typeDef = CustomAttributeWriter.GetTypeDef(type);
			if (typeDef == null)
			{
				return null;
			}
			if (!typeDef.IsEnum)
			{
				return null;
			}
			return typeDef;
		}

		// Token: 0x0600540F RID: 21519 RVA: 0x0019A6D0 File Offset: 0x0019A6D0
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

		// Token: 0x06005410 RID: 21520 RVA: 0x0019A714 File Offset: 0x0019A714
		private void Write(CANamedArgument namedArg)
		{
			if (namedArg == null)
			{
				this.helper.Error("Custom attribute named arg is null");
				return;
			}
			if (!this.recursionCounter.Increment())
			{
				this.helper.Error("Infinite recursion");
				return;
			}
			if (namedArg.IsProperty)
			{
				this.writer.WriteByte(84);
			}
			else
			{
				this.writer.WriteByte(83);
			}
			this.WriteFieldOrPropType(namedArg.Type);
			this.WriteUTF8String(namedArg.Name);
			this.WriteValue(namedArg.Type, namedArg.Argument);
			this.recursionCounter.Decrement();
		}

		// Token: 0x06005411 RID: 21521 RVA: 0x0019A7BC File Offset: 0x0019A7BC
		private void WriteFieldOrPropType(TypeSig type)
		{
			type = type.RemoveModifiers();
			if (type == null)
			{
				this.helper.Error("Custom attribute: Field/property type is null");
				return;
			}
			if (!this.recursionCounter.Increment())
			{
				this.helper.Error("Infinite recursion");
				return;
			}
			switch (type.ElementType)
			{
			case ElementType.Boolean:
				this.writer.WriteByte(2);
				goto IL_2AD;
			case ElementType.Char:
				this.writer.WriteByte(3);
				goto IL_2AD;
			case ElementType.I1:
				this.writer.WriteByte(4);
				goto IL_2AD;
			case ElementType.U1:
				this.writer.WriteByte(5);
				goto IL_2AD;
			case ElementType.I2:
				this.writer.WriteByte(6);
				goto IL_2AD;
			case ElementType.U2:
				this.writer.WriteByte(7);
				goto IL_2AD;
			case ElementType.I4:
				this.writer.WriteByte(8);
				goto IL_2AD;
			case ElementType.U4:
				this.writer.WriteByte(9);
				goto IL_2AD;
			case ElementType.I8:
				this.writer.WriteByte(10);
				goto IL_2AD;
			case ElementType.U8:
				this.writer.WriteByte(11);
				goto IL_2AD;
			case ElementType.R4:
				this.writer.WriteByte(12);
				goto IL_2AD;
			case ElementType.R8:
				this.writer.WriteByte(13);
				goto IL_2AD;
			case ElementType.String:
				this.writer.WriteByte(14);
				goto IL_2AD;
			case ElementType.ValueType:
			{
				ITypeDefOrRef typeDefOrRef = ((TypeDefOrRefSig)type).TypeDefOrRef;
				if (CustomAttributeWriter.GetEnumTypeDef(type) != null || typeDefOrRef is TypeRef)
				{
					this.writer.WriteByte(85);
					this.WriteType(typeDefOrRef);
					goto IL_2AD;
				}
				this.helper.Error("Custom attribute type doesn't seem to be an enum.");
				this.writer.WriteByte(85);
				this.WriteType(typeDefOrRef);
				goto IL_2AD;
			}
			case ElementType.Class:
			{
				ITypeDefOrRef typeDefOrRef = ((TypeDefOrRefSig)type).TypeDefOrRef;
				if (CustomAttributeWriter.CheckCorLibType(type, "Type"))
				{
					this.writer.WriteByte(80);
					goto IL_2AD;
				}
				if (typeDefOrRef is TypeRef)
				{
					this.writer.WriteByte(85);
					this.WriteType(typeDefOrRef);
					goto IL_2AD;
				}
				break;
			}
			case ElementType.Object:
				this.writer.WriteByte(81);
				goto IL_2AD;
			case ElementType.SZArray:
				this.writer.WriteByte(29);
				this.WriteFieldOrPropType(type.Next);
				goto IL_2AD;
			}
			this.helper.Error("Custom attribute: Invalid type");
			this.writer.WriteByte(byte.MaxValue);
			IL_2AD:
			this.recursionCounter.Decrement();
		}

		// Token: 0x06005412 RID: 21522 RVA: 0x0019AA88 File Offset: 0x0019AA88
		private void WriteType(IType type)
		{
			if (type == null)
			{
				this.helper.Error("Custom attribute: Type is null");
				this.WriteUTF8String(UTF8String.Empty);
				return;
			}
			this.WriteUTF8String(FullNameFactory.AssemblyQualifiedName(type, this.helper, null));
		}

		// Token: 0x06005413 RID: 21523 RVA: 0x0019AAC4 File Offset: 0x0019AAC4
		private static bool CheckCorLibType(TypeSig ts, string name)
		{
			TypeDefOrRefSig typeDefOrRefSig = ts as TypeDefOrRefSig;
			return typeDefOrRefSig != null && CustomAttributeWriter.CheckCorLibType(typeDefOrRefSig.TypeDefOrRef, name);
		}

		// Token: 0x06005414 RID: 21524 RVA: 0x0019AAF0 File Offset: 0x0019AAF0
		private static bool CheckCorLibType(ITypeDefOrRef tdr, string name)
		{
			return tdr != null && tdr.DefinitionAssembly.IsCorLib() && !(tdr is TypeSpec) && tdr.TypeName == name && tdr.Namespace == "System";
		}

		// Token: 0x06005415 RID: 21525 RVA: 0x0019AB4C File Offset: 0x0019AB4C
		private static MethodSig GetMethodSig(ICustomAttributeType ctor)
		{
			if (ctor == null)
			{
				return null;
			}
			return ctor.MethodSig;
		}

		// Token: 0x06005416 RID: 21526 RVA: 0x0019AB5C File Offset: 0x0019AB5C
		private void WriteUTF8String(UTF8String s)
		{
			if (s == null || s.Data == null)
			{
				this.writer.WriteByte(byte.MaxValue);
				return;
			}
			this.writer.WriteCompressedUInt32((uint)s.Data.Length);
			this.writer.WriteBytes(s.Data);
		}

		// Token: 0x06005417 RID: 21527 RVA: 0x0019ABB4 File Offset: 0x0019ABB4
		public void Dispose()
		{
			if (!this.disposeStream)
			{
				return;
			}
			if (this.outStream != null)
			{
				this.outStream.Dispose();
			}
		}

		// Token: 0x0400285E RID: 10334
		private readonly ICustomAttributeWriterHelper helper;

		// Token: 0x0400285F RID: 10335
		private RecursionCounter recursionCounter;

		// Token: 0x04002860 RID: 10336
		private readonly MemoryStream outStream;

		// Token: 0x04002861 RID: 10337
		private readonly DataWriter writer;

		// Token: 0x04002862 RID: 10338
		private readonly bool disposeStream;

		// Token: 0x04002863 RID: 10339
		private GenericArguments genericArguments;
	}
}
