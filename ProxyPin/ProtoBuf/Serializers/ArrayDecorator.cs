using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C46 RID: 3142
	internal sealed class ArrayDecorator : ProtoDecoratorBase
	{
		// Token: 0x06007CDC RID: 31964 RVA: 0x0024B828 File Offset: 0x0024B828
		public ArrayDecorator(TypeModel model, IProtoSerializer tail, int fieldNumber, bool writePacked, WireType packedWireType, Type arrayType, bool overwriteList, bool supportNull) : base(tail)
		{
			this.itemType = arrayType.GetElementType();
			Type type = supportNull ? this.itemType : (Helpers.GetUnderlyingType(this.itemType) ?? this.itemType);
			if ((writePacked || packedWireType != WireType.None) && fieldNumber <= 0)
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			if (!ListDecorator.CanPack(packedWireType))
			{
				if (writePacked)
				{
					throw new InvalidOperationException("Only simple data-types can use packed encoding");
				}
				packedWireType = WireType.None;
			}
			this.fieldNumber = fieldNumber;
			this.packedWireType = packedWireType;
			if (writePacked)
			{
				this.options |= 1;
			}
			if (overwriteList)
			{
				this.options |= 2;
			}
			if (supportNull)
			{
				this.options |= 4;
			}
			this.arrayType = arrayType;
		}

		// Token: 0x17001AFF RID: 6911
		// (get) Token: 0x06007CDD RID: 31965 RVA: 0x0024B914 File Offset: 0x0024B914
		public override Type ExpectedType
		{
			get
			{
				return this.arrayType;
			}
		}

		// Token: 0x17001B00 RID: 6912
		// (get) Token: 0x06007CDE RID: 31966 RVA: 0x0024B91C File Offset: 0x0024B91C
		public override bool RequiresOldValue
		{
			get
			{
				return this.AppendToCollection;
			}
		}

		// Token: 0x17001B01 RID: 6913
		// (get) Token: 0x06007CDF RID: 31967 RVA: 0x0024B924 File Offset: 0x0024B924
		public override bool ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007CE0 RID: 31968 RVA: 0x0024B928 File Offset: 0x0024B928
		private bool CanUsePackedPrefix()
		{
			return ArrayDecorator.CanUsePackedPrefix(this.packedWireType, this.itemType);
		}

		// Token: 0x06007CE1 RID: 31969 RVA: 0x0024B93C File Offset: 0x0024B93C
		internal static bool CanUsePackedPrefix(WireType packedWireType, Type itemType)
		{
			return (packedWireType == WireType.Fixed64 || packedWireType == WireType.Fixed32) && Helpers.IsValueType(itemType) && Helpers.GetUnderlyingType(itemType) == null;
		}

		// Token: 0x06007CE2 RID: 31970 RVA: 0x0024B968 File Offset: 0x0024B968
		protected override void EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			using (Local localWithValue = ctx.GetLocalWithValue(this.arrayType, valueFrom))
			{
				using (Local local = new Local(ctx, ctx.MapType(typeof(int))))
				{
					bool flag = (this.options & 1) > 0;
					bool flag2 = flag && this.CanUsePackedPrefix();
					using (Local local2 = (flag && !flag2) ? new Local(ctx, ctx.MapType(typeof(SubItemToken))) : null)
					{
						Type type = ctx.MapType(typeof(ProtoWriter));
						if (flag)
						{
							ctx.LoadValue(this.fieldNumber);
							ctx.LoadValue(2);
							ctx.LoadReaderWriter();
							ctx.EmitCall(type.GetMethod("WriteFieldHeader"));
							if (flag2)
							{
								ctx.LoadLength(localWithValue, false);
								ctx.LoadValue((int)this.packedWireType);
								ctx.LoadReaderWriter();
								ctx.EmitCall(type.GetMethod("WritePackedPrefix"));
							}
							else
							{
								ctx.LoadValue(localWithValue);
								ctx.LoadReaderWriter();
								ctx.EmitCall(type.GetMethod("StartSubItem"));
								ctx.StoreValue(local2);
							}
							ctx.LoadValue(this.fieldNumber);
							ctx.LoadReaderWriter();
							ctx.EmitCall(type.GetMethod("SetPackedField"));
						}
						this.EmitWriteArrayLoop(ctx, local, localWithValue);
						if (flag)
						{
							if (flag2)
							{
								ctx.LoadValue(this.fieldNumber);
								ctx.LoadReaderWriter();
								ctx.EmitCall(type.GetMethod("ClearPackedField"));
							}
							else
							{
								ctx.LoadValue(local2);
								ctx.LoadReaderWriter();
								ctx.EmitCall(type.GetMethod("EndSubItem"));
							}
						}
					}
				}
			}
		}

		// Token: 0x06007CE3 RID: 31971 RVA: 0x0024BB80 File Offset: 0x0024BB80
		private void EmitWriteArrayLoop(CompilerContext ctx, Local i, Local arr)
		{
			ctx.LoadValue(0);
			ctx.StoreValue(i);
			CodeLabel label = ctx.DefineLabel();
			CodeLabel label2 = ctx.DefineLabel();
			ctx.Branch(label, false);
			ctx.MarkLabel(label2);
			ctx.LoadArrayValue(arr, i);
			if (this.SupportNull)
			{
				this.Tail.EmitWrite(ctx, null);
			}
			else
			{
				ctx.WriteNullCheckedTail(this.itemType, this.Tail, null);
			}
			ctx.LoadValue(i);
			ctx.LoadValue(1);
			ctx.Add();
			ctx.StoreValue(i);
			ctx.MarkLabel(label);
			ctx.LoadValue(i);
			ctx.LoadLength(arr, false);
			ctx.BranchIfLess(label2, false);
		}

		// Token: 0x17001B02 RID: 6914
		// (get) Token: 0x06007CE4 RID: 31972 RVA: 0x0024BC30 File Offset: 0x0024BC30
		private bool AppendToCollection
		{
			get
			{
				return (this.options & 2) == 0;
			}
		}

		// Token: 0x17001B03 RID: 6915
		// (get) Token: 0x06007CE5 RID: 31973 RVA: 0x0024BC40 File Offset: 0x0024BC40
		private bool SupportNull
		{
			get
			{
				return (this.options & 4) > 0;
			}
		}

		// Token: 0x06007CE6 RID: 31974 RVA: 0x0024BC50 File Offset: 0x0024BC50
		public override void Write(object value, ProtoWriter dest)
		{
			IList list = (IList)value;
			int count = list.Count;
			bool flag = (this.options & 1) > 0;
			bool flag2 = flag && this.CanUsePackedPrefix();
			SubItemToken token;
			if (flag)
			{
				ProtoWriter.WriteFieldHeader(this.fieldNumber, WireType.String, dest);
				if (flag2)
				{
					ProtoWriter.WritePackedPrefix(list.Count, this.packedWireType, dest);
					token = default(SubItemToken);
				}
				else
				{
					token = ProtoWriter.StartSubItem(value, dest);
				}
				ProtoWriter.SetPackedField(this.fieldNumber, dest);
			}
			else
			{
				token = default(SubItemToken);
			}
			bool flag3 = !this.SupportNull;
			for (int i = 0; i < count; i++)
			{
				object obj = list[i];
				if (flag3 && obj == null)
				{
					throw new NullReferenceException();
				}
				this.Tail.Write(obj, dest);
			}
			if (flag)
			{
				if (flag2)
				{
					ProtoWriter.ClearPackedField(this.fieldNumber, dest);
					return;
				}
				ProtoWriter.EndSubItem(token, dest);
			}
		}

		// Token: 0x06007CE7 RID: 31975 RVA: 0x0024BD54 File Offset: 0x0024BD54
		public override object Read(object value, ProtoReader source)
		{
			int field = source.FieldNumber;
			BasicList basicList = new BasicList();
			if (this.packedWireType != WireType.None && source.WireType == WireType.String)
			{
				SubItemToken token = ProtoReader.StartSubItem(source);
				while (ProtoReader.HasSubValue(this.packedWireType, source))
				{
					basicList.Add(this.Tail.Read(null, source));
				}
				ProtoReader.EndSubItem(token, source);
			}
			else
			{
				do
				{
					basicList.Add(this.Tail.Read(null, source));
				}
				while (source.TryReadFieldHeader(field));
			}
			int num = this.AppendToCollection ? ((value == null) ? 0 : ((Array)value).Length) : 0;
			Array array = Array.CreateInstance(this.itemType, num + basicList.Count);
			if (num != 0)
			{
				((Array)value).CopyTo(array, 0);
			}
			basicList.CopyTo(array, num);
			return array;
		}

		// Token: 0x06007CE8 RID: 31976 RVA: 0x0024BE40 File Offset: 0x0024BE40
		protected override void EmitRead(CompilerContext ctx, Local valueFrom)
		{
			Type type = ctx.MapType(typeof(List<>)).MakeGenericType(new Type[]
			{
				this.itemType
			});
			Type expectedType = this.ExpectedType;
			using (Local local = this.AppendToCollection ? ctx.GetLocalWithValue(expectedType, valueFrom) : null)
			{
				using (Local local2 = new Local(ctx, expectedType))
				{
					using (Local local3 = new Local(ctx, type))
					{
						ctx.EmitCtor(type);
						ctx.StoreValue(local3);
						ListDecorator.EmitReadList(ctx, local3, this.Tail, type.GetMethod("Add"), this.packedWireType, false);
						using (Local local4 = this.AppendToCollection ? new Local(ctx, ctx.MapType(typeof(int))) : null)
						{
							Type[] array = new Type[]
							{
								ctx.MapType(typeof(Array)),
								ctx.MapType(typeof(int))
							};
							if (this.AppendToCollection)
							{
								ctx.LoadLength(local, true);
								ctx.CopyValue();
								ctx.StoreValue(local4);
								ctx.LoadAddress(local3, type, false);
								ctx.LoadValue(type.GetProperty("Count"));
								ctx.Add();
								ctx.CreateArray(this.itemType, null);
								ctx.StoreValue(local2);
								ctx.LoadValue(local4);
								CodeLabel label = ctx.DefineLabel();
								ctx.BranchIfFalse(label, true);
								ctx.LoadValue(local);
								ctx.LoadValue(local2);
								ctx.LoadValue(0);
								ctx.EmitCall(expectedType.GetMethod("CopyTo", array));
								ctx.MarkLabel(label);
								ctx.LoadValue(local3);
								ctx.LoadValue(local2);
								ctx.LoadValue(local4);
							}
							else
							{
								ctx.LoadAddress(local3, type, false);
								ctx.LoadValue(type.GetProperty("Count"));
								ctx.CreateArray(this.itemType, null);
								ctx.StoreValue(local2);
								ctx.LoadAddress(local3, type, false);
								ctx.LoadValue(local2);
								ctx.LoadValue(0);
							}
							array[0] = expectedType;
							MethodInfo method = type.GetMethod("CopyTo", array);
							if (method == null)
							{
								array[1] = ctx.MapType(typeof(Array));
								method = type.GetMethod("CopyTo", array);
							}
							ctx.EmitCall(method);
						}
						ctx.LoadValue(local2);
					}
				}
			}
		}

		// Token: 0x04003C33 RID: 15411
		private readonly int fieldNumber;

		// Token: 0x04003C34 RID: 15412
		private const byte OPTIONS_WritePacked = 1;

		// Token: 0x04003C35 RID: 15413
		private const byte OPTIONS_OverwriteList = 2;

		// Token: 0x04003C36 RID: 15414
		private const byte OPTIONS_SupportNull = 4;

		// Token: 0x04003C37 RID: 15415
		private readonly byte options;

		// Token: 0x04003C38 RID: 15416
		private readonly WireType packedWireType;

		// Token: 0x04003C39 RID: 15417
		private readonly Type arrayType;

		// Token: 0x04003C3A RID: 15418
		private readonly Type itemType;
	}
}
