using System;
using System.Runtime.CompilerServices;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C50 RID: 3152
	internal sealed class EnumSerializer : IProtoSerializer
	{
		// Token: 0x06007D3D RID: 32061 RVA: 0x0024CC54 File Offset: 0x0024CC54
		public EnumSerializer(Type enumType, EnumSerializer.EnumPair[] map)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			this.enumType = enumType;
			this.map = map;
			if (map != null)
			{
				for (int i = 1; i < map.Length; i++)
				{
					for (int j = 0; j < i; j++)
					{
						if (map[i].WireValue == map[j].WireValue && !object.Equals(map[i].RawValue, map[j].RawValue))
						{
							throw new ProtoException("Multiple enums with wire-value " + map[i].WireValue.ToString());
						}
						if (object.Equals(map[i].RawValue, map[j].RawValue) && map[i].WireValue != map[j].WireValue)
						{
							string str = "Multiple enums with deserialized-value ";
							object rawValue = map[i].RawValue;
							throw new ProtoException(str + ((rawValue != null) ? rawValue.ToString() : null));
						}
					}
				}
			}
		}

		// Token: 0x06007D3E RID: 32062 RVA: 0x0024CD88 File Offset: 0x0024CD88
		private ProtoTypeCode GetTypeCode()
		{
			Type underlyingType = Helpers.GetUnderlyingType(this.enumType);
			if (underlyingType == null)
			{
				underlyingType = this.enumType;
			}
			return Helpers.GetTypeCode(underlyingType);
		}

		// Token: 0x17001B1D RID: 6941
		// (get) Token: 0x06007D3F RID: 32063 RVA: 0x0024CDC0 File Offset: 0x0024CDC0
		public Type ExpectedType
		{
			get
			{
				return this.enumType;
			}
		}

		// Token: 0x17001B1E RID: 6942
		// (get) Token: 0x06007D40 RID: 32064 RVA: 0x0024CDC8 File Offset: 0x0024CDC8
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B1F RID: 6943
		// (get) Token: 0x06007D41 RID: 32065 RVA: 0x0024CDCC File Offset: 0x0024CDCC
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007D42 RID: 32066 RVA: 0x0024CDD0 File Offset: 0x0024CDD0
		private int EnumToWire(object value)
		{
			switch (this.GetTypeCode())
			{
			case ProtoTypeCode.SByte:
				return (int)((sbyte)value);
			case ProtoTypeCode.Byte:
				return (int)((byte)value);
			case ProtoTypeCode.Int16:
				return (int)((short)value);
			case ProtoTypeCode.UInt16:
				return (int)((ushort)value);
			case ProtoTypeCode.Int32:
				return (int)value;
			case ProtoTypeCode.UInt32:
				return (int)((uint)value);
			case ProtoTypeCode.Int64:
				return (int)((long)value);
			case ProtoTypeCode.UInt64:
				return (int)((ulong)value);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06007D43 RID: 32067 RVA: 0x0024CE54 File Offset: 0x0024CE54
		private object WireToEnum(int value)
		{
			switch (this.GetTypeCode())
			{
			case ProtoTypeCode.SByte:
				return Enum.ToObject(this.enumType, (sbyte)value);
			case ProtoTypeCode.Byte:
				return Enum.ToObject(this.enumType, (byte)value);
			case ProtoTypeCode.Int16:
				return Enum.ToObject(this.enumType, (short)value);
			case ProtoTypeCode.UInt16:
				return Enum.ToObject(this.enumType, (ushort)value);
			case ProtoTypeCode.Int32:
				return Enum.ToObject(this.enumType, value);
			case ProtoTypeCode.UInt32:
				return Enum.ToObject(this.enumType, (uint)value);
			case ProtoTypeCode.Int64:
				return Enum.ToObject(this.enumType, (long)value);
			case ProtoTypeCode.UInt64:
				return Enum.ToObject(this.enumType, (ulong)((long)value));
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06007D44 RID: 32068 RVA: 0x0024CF0C File Offset: 0x0024CF0C
		public object Read(object value, ProtoReader source)
		{
			int num = source.ReadInt32();
			if (this.map == null)
			{
				return this.WireToEnum(num);
			}
			for (int i = 0; i < this.map.Length; i++)
			{
				if (this.map[i].WireValue == num)
				{
					return this.map[i].TypedValue;
				}
			}
			source.ThrowEnumException(this.ExpectedType, num);
			return null;
		}

		// Token: 0x06007D45 RID: 32069 RVA: 0x0024CF84 File Offset: 0x0024CF84
		public void Write(object value, ProtoWriter dest)
		{
			if (this.map == null)
			{
				ProtoWriter.WriteInt32(this.EnumToWire(value), dest);
				return;
			}
			for (int i = 0; i < this.map.Length; i++)
			{
				if (object.Equals(this.map[i].TypedValue, value))
				{
					ProtoWriter.WriteInt32(this.map[i].WireValue, dest);
					return;
				}
			}
			ProtoWriter.ThrowEnumException(dest, value);
		}

		// Token: 0x06007D46 RID: 32070 RVA: 0x0024D000 File Offset: 0x0024D000
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ProtoTypeCode typeCode = this.GetTypeCode();
			if (this.map == null)
			{
				ctx.LoadValue(valueFrom);
				ctx.ConvertToInt32(typeCode, false);
				ctx.EmitBasicWrite("WriteInt32", null);
				return;
			}
			using (Local localWithValue = ctx.GetLocalWithValue(this.ExpectedType, valueFrom))
			{
				CodeLabel label = ctx.DefineLabel();
				for (int i = 0; i < this.map.Length; i++)
				{
					CodeLabel label2 = ctx.DefineLabel();
					CodeLabel label3 = ctx.DefineLabel();
					ctx.LoadValue(localWithValue);
					EnumSerializer.WriteEnumValue(ctx, typeCode, this.map[i].RawValue);
					ctx.BranchIfEqual(label3, true);
					ctx.Branch(label2, true);
					ctx.MarkLabel(label3);
					ctx.LoadValue(this.map[i].WireValue);
					ctx.EmitBasicWrite("WriteInt32", null);
					ctx.Branch(label, false);
					ctx.MarkLabel(label2);
				}
				ctx.LoadReaderWriter();
				ctx.LoadValue(localWithValue);
				ctx.CastToObject(this.ExpectedType);
				ctx.EmitCall(ctx.MapType(typeof(ProtoWriter)).GetMethod("ThrowEnumException"));
				ctx.MarkLabel(label);
			}
		}

		// Token: 0x06007D47 RID: 32071 RVA: 0x0024D148 File Offset: 0x0024D148
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			ProtoTypeCode typeCode = this.GetTypeCode();
			if (this.map == null)
			{
				ctx.EmitBasicRead("ReadInt32", ctx.MapType(typeof(int)));
				ctx.ConvertFromInt32(typeCode, false);
				return;
			}
			int[] array = new int[this.map.Length];
			object[] array2 = new object[this.map.Length];
			for (int i = 0; i < this.map.Length; i++)
			{
				array[i] = this.map[i].WireValue;
				array2[i] = this.map[i].RawValue;
			}
			using (Local local = new Local(ctx, this.ExpectedType))
			{
				using (Local local2 = new Local(ctx, ctx.MapType(typeof(int))))
				{
					ctx.EmitBasicRead("ReadInt32", ctx.MapType(typeof(int)));
					ctx.StoreValue(local2);
					CodeLabel codeLabel = ctx.DefineLabel();
					foreach (object obj in BasicList.GetContiguousGroups(array, array2))
					{
						BasicList.Group group = (BasicList.Group)obj;
						CodeLabel label = ctx.DefineLabel();
						int count = group.Items.Count;
						if (count == 1)
						{
							ctx.LoadValue(local2);
							ctx.LoadValue(group.First);
							CodeLabel codeLabel2 = ctx.DefineLabel();
							ctx.BranchIfEqual(codeLabel2, true);
							ctx.Branch(label, false);
							EnumSerializer.WriteEnumValue(ctx, typeCode, codeLabel2, codeLabel, group.Items[0], local);
						}
						else
						{
							ctx.LoadValue(local2);
							ctx.LoadValue(group.First);
							ctx.Subtract();
							CodeLabel[] array3 = new CodeLabel[count];
							for (int j = 0; j < count; j++)
							{
								array3[j] = ctx.DefineLabel();
							}
							ctx.Switch(array3);
							ctx.Branch(label, false);
							for (int k = 0; k < count; k++)
							{
								EnumSerializer.WriteEnumValue(ctx, typeCode, array3[k], codeLabel, group.Items[k], local);
							}
						}
						ctx.MarkLabel(label);
					}
					ctx.LoadReaderWriter();
					ctx.LoadValue(this.ExpectedType);
					ctx.LoadValue(local2);
					ctx.EmitCall(ctx.MapType(typeof(ProtoReader)).GetMethod("ThrowEnumException"));
					ctx.MarkLabel(codeLabel);
					ctx.LoadValue(local);
				}
			}
		}

		// Token: 0x06007D48 RID: 32072 RVA: 0x0024D408 File Offset: 0x0024D408
		private static void WriteEnumValue(CompilerContext ctx, ProtoTypeCode typeCode, object value)
		{
			switch (typeCode)
			{
			case ProtoTypeCode.SByte:
				ctx.LoadValue((int)((sbyte)value));
				return;
			case ProtoTypeCode.Byte:
				ctx.LoadValue((int)((byte)value));
				return;
			case ProtoTypeCode.Int16:
				ctx.LoadValue((int)((short)value));
				return;
			case ProtoTypeCode.UInt16:
				ctx.LoadValue((int)((ushort)value));
				return;
			case ProtoTypeCode.Int32:
				ctx.LoadValue((int)value);
				return;
			case ProtoTypeCode.UInt32:
				ctx.LoadValue((int)((uint)value));
				return;
			case ProtoTypeCode.Int64:
				ctx.LoadValue((long)value);
				return;
			case ProtoTypeCode.UInt64:
				ctx.LoadValue((long)((ulong)value));
				return;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06007D49 RID: 32073 RVA: 0x0024D4B4 File Offset: 0x0024D4B4
		private static void WriteEnumValue(CompilerContext ctx, ProtoTypeCode typeCode, CodeLabel handler, CodeLabel @continue, object value, Local local)
		{
			ctx.MarkLabel(handler);
			EnumSerializer.WriteEnumValue(ctx, typeCode, value);
			ctx.StoreValue(local);
			ctx.Branch(@continue, false);
		}

		// Token: 0x04003C49 RID: 15433
		private readonly Type enumType;

		// Token: 0x04003C4A RID: 15434
		private readonly EnumSerializer.EnumPair[] map;

		// Token: 0x02001173 RID: 4467
		[protobuf-net.IsReadOnly]
		public struct EnumPair
		{
			// Token: 0x06009356 RID: 37718 RVA: 0x002C2CAC File Offset: 0x002C2CAC
			public EnumPair(int wireValue, object raw, Type type)
			{
				this.WireValue = wireValue;
				this.RawValue = raw;
				this.TypedValue = (Enum)Enum.ToObject(type, raw);
			}

			// Token: 0x04004B47 RID: 19271
			public readonly object RawValue;

			// Token: 0x04004B48 RID: 19272
			public readonly Enum TypedValue;

			// Token: 0x04004B49 RID: 19273
			public readonly int WireValue;
		}
	}
}
