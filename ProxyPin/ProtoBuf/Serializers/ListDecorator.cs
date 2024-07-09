using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C5A RID: 3162
	internal class ListDecorator : ProtoDecoratorBase
	{
		// Token: 0x06007D8B RID: 32139 RVA: 0x0024E32C File Offset: 0x0024E32C
		internal static bool CanPack(WireType wireType)
		{
			return wireType <= WireType.Fixed64 || wireType == WireType.Fixed32 || wireType == WireType.SignedVariant;
		}

		// Token: 0x17001B34 RID: 6964
		// (get) Token: 0x06007D8C RID: 32140 RVA: 0x0024E348 File Offset: 0x0024E348
		private bool IsList
		{
			get
			{
				return (this.options & 1) > 0;
			}
		}

		// Token: 0x17001B35 RID: 6965
		// (get) Token: 0x06007D8D RID: 32141 RVA: 0x0024E358 File Offset: 0x0024E358
		private bool SuppressIList
		{
			get
			{
				return (this.options & 2) > 0;
			}
		}

		// Token: 0x17001B36 RID: 6966
		// (get) Token: 0x06007D8E RID: 32142 RVA: 0x0024E368 File Offset: 0x0024E368
		private bool WritePacked
		{
			get
			{
				return (this.options & 4) > 0;
			}
		}

		// Token: 0x17001B37 RID: 6967
		// (get) Token: 0x06007D8F RID: 32143 RVA: 0x0024E378 File Offset: 0x0024E378
		private bool SupportNull
		{
			get
			{
				return (this.options & 32) > 0;
			}
		}

		// Token: 0x17001B38 RID: 6968
		// (get) Token: 0x06007D90 RID: 32144 RVA: 0x0024E388 File Offset: 0x0024E388
		private bool ReturnList
		{
			get
			{
				return (this.options & 8) > 0;
			}
		}

		// Token: 0x06007D91 RID: 32145 RVA: 0x0024E398 File Offset: 0x0024E398
		internal static ListDecorator Create(TypeModel model, Type declaredType, Type concreteType, IProtoSerializer tail, int fieldNumber, bool writePacked, WireType packedWireType, bool returnList, bool overwriteList, bool supportNull)
		{
			MethodInfo builderFactory;
			PropertyInfo isEmpty;
			PropertyInfo length;
			MethodInfo methodInfo;
			MethodInfo addRange;
			MethodInfo finish;
			if (returnList && ImmutableCollectionDecorator.IdentifyImmutable(model, declaredType, out builderFactory, out isEmpty, out length, out methodInfo, out addRange, out finish))
			{
				return new ImmutableCollectionDecorator(model, declaredType, concreteType, tail, fieldNumber, writePacked, packedWireType, returnList, overwriteList, supportNull, builderFactory, isEmpty, length, methodInfo, addRange, finish);
			}
			return new ListDecorator(model, declaredType, concreteType, tail, fieldNumber, writePacked, packedWireType, returnList, overwriteList, supportNull);
		}

		// Token: 0x06007D92 RID: 32146 RVA: 0x0024E3FC File Offset: 0x0024E3FC
		protected ListDecorator(TypeModel model, Type declaredType, Type concreteType, IProtoSerializer tail, int fieldNumber, bool writePacked, WireType packedWireType, bool returnList, bool overwriteList, bool supportNull) : base(tail)
		{
			if (returnList)
			{
				this.options |= 8;
			}
			if (overwriteList)
			{
				this.options |= 16;
			}
			if (supportNull)
			{
				this.options |= 32;
			}
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
			if (writePacked)
			{
				this.options |= 4;
			}
			this.packedWireType = packedWireType;
			if (declaredType == null)
			{
				throw new ArgumentNullException("declaredType");
			}
			if (declaredType.IsArray)
			{
				throw new ArgumentException("Cannot treat arrays as lists", "declaredType");
			}
			this.declaredType = declaredType;
			this.concreteType = concreteType;
			if (this.RequireAdd)
			{
				bool flag;
				this.add = TypeModel.ResolveListAdd(model, declaredType, tail.ExpectedType, out flag);
				if (flag)
				{
					this.options |= 1;
					string fullName = declaredType.FullName;
					if (fullName != null && fullName.StartsWith("System.Data.Linq.EntitySet`1[["))
					{
						this.options |= 2;
					}
				}
				if (this.add == null)
				{
					throw new InvalidOperationException("Unable to resolve a suitable Add method for " + declaredType.FullName);
				}
			}
		}

		// Token: 0x17001B39 RID: 6969
		// (get) Token: 0x06007D93 RID: 32147 RVA: 0x0024E58C File Offset: 0x0024E58C
		protected virtual bool RequireAdd
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B3A RID: 6970
		// (get) Token: 0x06007D94 RID: 32148 RVA: 0x0024E590 File Offset: 0x0024E590
		public override Type ExpectedType
		{
			get
			{
				return this.declaredType;
			}
		}

		// Token: 0x17001B3B RID: 6971
		// (get) Token: 0x06007D95 RID: 32149 RVA: 0x0024E598 File Offset: 0x0024E598
		public override bool RequiresOldValue
		{
			get
			{
				return this.AppendToCollection;
			}
		}

		// Token: 0x17001B3C RID: 6972
		// (get) Token: 0x06007D96 RID: 32150 RVA: 0x0024E5A0 File Offset: 0x0024E5A0
		public override bool ReturnsValue
		{
			get
			{
				return this.ReturnList;
			}
		}

		// Token: 0x17001B3D RID: 6973
		// (get) Token: 0x06007D97 RID: 32151 RVA: 0x0024E5A8 File Offset: 0x0024E5A8
		protected bool AppendToCollection
		{
			get
			{
				return (this.options & 16) == 0;
			}
		}

		// Token: 0x06007D98 RID: 32152 RVA: 0x0024E5B8 File Offset: 0x0024E5B8
		protected override void EmitRead(CompilerContext ctx, Local valueFrom)
		{
			bool returnList = this.ReturnList;
			using (Local local = this.AppendToCollection ? ctx.GetLocalWithValue(this.ExpectedType, valueFrom) : new Local(ctx, this.declaredType))
			{
				using (Local local2 = (returnList && this.AppendToCollection && !Helpers.IsValueType(this.ExpectedType)) ? new Local(ctx, this.ExpectedType) : null)
				{
					if (!this.AppendToCollection)
					{
						ctx.LoadNullRef();
						ctx.StoreValue(local);
					}
					else if (returnList && local2 != null)
					{
						ctx.LoadValue(local);
						ctx.StoreValue(local2);
					}
					if (this.concreteType != null)
					{
						ctx.LoadValue(local);
						CodeLabel label = ctx.DefineLabel();
						ctx.BranchIfTrue(label, true);
						ctx.EmitCtor(this.concreteType);
						ctx.StoreValue(local);
						ctx.MarkLabel(label);
					}
					bool castListForAdd = !this.add.DeclaringType.IsAssignableFrom(this.declaredType);
					ListDecorator.EmitReadList(ctx, local, this.Tail, this.add, this.packedWireType, castListForAdd);
					if (returnList)
					{
						if (this.AppendToCollection && local2 != null)
						{
							ctx.LoadValue(local2);
							ctx.LoadValue(local);
							CodeLabel label2 = ctx.DefineLabel();
							CodeLabel label3 = ctx.DefineLabel();
							ctx.BranchIfEqual(label2, true);
							ctx.LoadValue(local);
							ctx.Branch(label3, true);
							ctx.MarkLabel(label2);
							ctx.LoadNullRef();
							ctx.MarkLabel(label3);
						}
						else
						{
							ctx.LoadValue(local);
						}
					}
				}
			}
		}

		// Token: 0x06007D99 RID: 32153 RVA: 0x0024E79C File Offset: 0x0024E79C
		internal static void EmitReadList(CompilerContext ctx, Local list, IProtoSerializer tail, MethodInfo add, WireType packedWireType, bool castListForAdd)
		{
			using (Local local = new Local(ctx, ctx.MapType(typeof(int))))
			{
				CodeLabel label = (packedWireType == WireType.None) ? default(CodeLabel) : ctx.DefineLabel();
				if (packedWireType != WireType.None)
				{
					ctx.LoadReaderWriter();
					ctx.LoadValue(typeof(ProtoReader).GetProperty("WireType"));
					ctx.LoadValue(2);
					ctx.BranchIfEqual(label, false);
				}
				ctx.LoadReaderWriter();
				ctx.LoadValue(typeof(ProtoReader).GetProperty("FieldNumber"));
				ctx.StoreValue(local);
				CodeLabel label2 = ctx.DefineLabel();
				ctx.MarkLabel(label2);
				ListDecorator.EmitReadAndAddItem(ctx, list, tail, add, castListForAdd);
				ctx.LoadReaderWriter();
				ctx.LoadValue(local);
				ctx.EmitCall(ctx.MapType(typeof(ProtoReader)).GetMethod("TryReadFieldHeader"));
				ctx.BranchIfTrue(label2, false);
				if (packedWireType != WireType.None)
				{
					CodeLabel label3 = ctx.DefineLabel();
					ctx.Branch(label3, false);
					ctx.MarkLabel(label);
					ctx.LoadReaderWriter();
					ctx.EmitCall(ctx.MapType(typeof(ProtoReader)).GetMethod("StartSubItem"));
					CodeLabel label4 = ctx.DefineLabel();
					CodeLabel label5 = ctx.DefineLabel();
					ctx.MarkLabel(label4);
					ctx.LoadValue((int)packedWireType);
					ctx.LoadReaderWriter();
					ctx.EmitCall(ctx.MapType(typeof(ProtoReader)).GetMethod("HasSubValue"));
					ctx.BranchIfFalse(label5, false);
					ListDecorator.EmitReadAndAddItem(ctx, list, tail, add, castListForAdd);
					ctx.Branch(label4, false);
					ctx.MarkLabel(label5);
					ctx.LoadReaderWriter();
					ctx.EmitCall(ctx.MapType(typeof(ProtoReader)).GetMethod("EndSubItem"));
					ctx.MarkLabel(label3);
				}
			}
		}

		// Token: 0x06007D9A RID: 32154 RVA: 0x0024E998 File Offset: 0x0024E998
		private static void EmitReadAndAddItem(CompilerContext ctx, Local list, IProtoSerializer tail, MethodInfo add, bool castListForAdd)
		{
			ctx.LoadAddress(list, list.Type, false);
			if (castListForAdd)
			{
				ctx.Cast(add.DeclaringType);
			}
			Type expectedType = tail.ExpectedType;
			bool returnsValue = tail.ReturnsValue;
			if (tail.RequiresOldValue)
			{
				if (Helpers.IsValueType(expectedType) || !returnsValue)
				{
					using (Local local = new Local(ctx, expectedType))
					{
						if (Helpers.IsValueType(expectedType))
						{
							ctx.LoadAddress(local, expectedType, false);
							ctx.EmitCtor(expectedType);
						}
						else
						{
							ctx.LoadNullRef();
							ctx.StoreValue(local);
						}
						tail.EmitRead(ctx, local);
						if (!returnsValue)
						{
							ctx.LoadValue(local);
						}
						goto IL_D3;
					}
				}
				ctx.LoadNullRef();
				tail.EmitRead(ctx, null);
			}
			else
			{
				if (!returnsValue)
				{
					throw new InvalidOperationException();
				}
				tail.EmitRead(ctx, null);
			}
			IL_D3:
			Type parameterType = add.GetParameters()[0].ParameterType;
			if (parameterType != expectedType)
			{
				if (parameterType == ctx.MapType(typeof(object)))
				{
					ctx.CastToObject(expectedType);
				}
				else
				{
					if (!(Helpers.GetUnderlyingType(parameterType) == expectedType))
					{
						throw new InvalidOperationException("Conflicting item/add type");
					}
					ConstructorInfo constructor = Helpers.GetConstructor(parameterType, new Type[]
					{
						expectedType
					}, false);
					ctx.EmitCtor(constructor);
				}
			}
			ctx.EmitCall(add, list.Type);
			if (add.ReturnType != ctx.MapType(typeof(void)))
			{
				ctx.DiscardValue();
			}
		}

		// Token: 0x06007D9B RID: 32155 RVA: 0x0024EB3C File Offset: 0x0024EB3C
		protected MethodInfo GetEnumeratorInfo(TypeModel model, out MethodInfo moveNext, out MethodInfo current)
		{
			return ListDecorator.GetEnumeratorInfo(model, this.ExpectedType, this.Tail.ExpectedType, out moveNext, out current);
		}

		// Token: 0x06007D9C RID: 32156 RVA: 0x0024EB68 File Offset: 0x0024EB68
		internal static MethodInfo GetEnumeratorInfo(TypeModel model, Type expectedType, Type itemType, out MethodInfo moveNext, out MethodInfo current)
		{
			Type type = null;
			MethodInfo instanceMethod = Helpers.GetInstanceMethod(expectedType, "GetEnumerator", null);
			Type returnType;
			Type type2;
			if (instanceMethod != null)
			{
				returnType = instanceMethod.ReturnType;
				type2 = returnType;
				moveNext = Helpers.GetInstanceMethod(type2, "MoveNext", null);
				PropertyInfo property = Helpers.GetProperty(type2, "Current", false);
				current = ((property == null) ? null : Helpers.GetGetMethod(property, false, false));
				if (moveNext == null && model.MapType(ListDecorator.ienumeratorType).IsAssignableFrom(type2))
				{
					moveNext = Helpers.GetInstanceMethod(model.MapType(ListDecorator.ienumeratorType), "MoveNext", null);
				}
				if (moveNext != null && moveNext.ReturnType == model.MapType(typeof(bool)) && current != null && current.ReturnType == itemType)
				{
					return instanceMethod;
				}
				MethodInfo methodInfo;
				current = (methodInfo = null);
				moveNext = methodInfo;
			}
			Type type3 = model.MapType(typeof(IEnumerable<>), false);
			if (type3 != null)
			{
				type3 = type3.MakeGenericType(new Type[]
				{
					itemType
				});
				type = type3;
			}
			if (type != null && type.IsAssignableFrom(expectedType))
			{
				instanceMethod = Helpers.GetInstanceMethod(type, "GetEnumerator");
				returnType = instanceMethod.ReturnType;
				type2 = returnType;
				moveNext = Helpers.GetInstanceMethod(model.MapType(ListDecorator.ienumeratorType), "MoveNext");
				current = Helpers.GetGetMethod(Helpers.GetProperty(type2, "Current", false), false, false);
				return instanceMethod;
			}
			type = model.MapType(ListDecorator.ienumerableType);
			instanceMethod = Helpers.GetInstanceMethod(type, "GetEnumerator");
			returnType = instanceMethod.ReturnType;
			type2 = returnType;
			moveNext = Helpers.GetInstanceMethod(type2, "MoveNext");
			current = Helpers.GetGetMethod(Helpers.GetProperty(type2, "Current", false), false, false);
			return instanceMethod;
		}

		// Token: 0x06007D9D RID: 32157 RVA: 0x0024ED4C File Offset: 0x0024ED4C
		protected override void EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			using (Local localWithValue = ctx.GetLocalWithValue(this.ExpectedType, valueFrom))
			{
				MethodInfo method;
				MethodInfo methodInfo;
				MethodInfo enumeratorInfo = this.GetEnumeratorInfo(ctx.Model, out method, out methodInfo);
				Type returnType = enumeratorInfo.ReturnType;
				bool writePacked = this.WritePacked;
				using (Local local = new Local(ctx, returnType))
				{
					using (Local local2 = writePacked ? new Local(ctx, ctx.MapType(typeof(SubItemToken))) : null)
					{
						if (writePacked)
						{
							ctx.LoadValue(this.fieldNumber);
							ctx.LoadValue(2);
							ctx.LoadReaderWriter();
							ctx.EmitCall(ctx.MapType(typeof(ProtoWriter)).GetMethod("WriteFieldHeader"));
							ctx.LoadValue(localWithValue);
							ctx.LoadReaderWriter();
							ctx.EmitCall(ctx.MapType(typeof(ProtoWriter)).GetMethod("StartSubItem"));
							ctx.StoreValue(local2);
							ctx.LoadValue(this.fieldNumber);
							ctx.LoadReaderWriter();
							ctx.EmitCall(ctx.MapType(typeof(ProtoWriter)).GetMethod("SetPackedField"));
						}
						ctx.LoadAddress(localWithValue, this.ExpectedType, false);
						ctx.EmitCall(enumeratorInfo, this.ExpectedType);
						ctx.StoreValue(local);
						using (ctx.Using(local))
						{
							CodeLabel label = ctx.DefineLabel();
							CodeLabel label2 = ctx.DefineLabel();
							ctx.Branch(label2, false);
							ctx.MarkLabel(label);
							ctx.LoadAddress(local, returnType, false);
							ctx.EmitCall(methodInfo, returnType);
							Type expectedType = this.Tail.ExpectedType;
							if (expectedType != ctx.MapType(typeof(object)) && methodInfo.ReturnType == ctx.MapType(typeof(object)))
							{
								ctx.CastFromObject(expectedType);
							}
							this.Tail.EmitWrite(ctx, null);
							ctx.MarkLabel(label2);
							ctx.LoadAddress(local, returnType, false);
							ctx.EmitCall(method, returnType);
							ctx.BranchIfTrue(label, false);
						}
						if (writePacked)
						{
							ctx.LoadValue(local2);
							ctx.LoadReaderWriter();
							ctx.EmitCall(ctx.MapType(typeof(ProtoWriter)).GetMethod("EndSubItem"));
						}
					}
				}
			}
		}

		// Token: 0x06007D9E RID: 32158 RVA: 0x0024F020 File Offset: 0x0024F020
		public override void Write(object value, ProtoWriter dest)
		{
			bool writePacked = this.WritePacked;
			bool flag = (writePacked & this.CanUsePackedPrefix(value)) && value is ICollection;
			SubItemToken token;
			if (writePacked)
			{
				ProtoWriter.WriteFieldHeader(this.fieldNumber, WireType.String, dest);
				if (flag)
				{
					ProtoWriter.WritePackedPrefix(((ICollection)value).Count, this.packedWireType, dest);
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
			bool flag2 = !this.SupportNull;
			foreach (object obj in ((IEnumerable)value))
			{
				if (flag2 && obj == null)
				{
					throw new NullReferenceException();
				}
				this.Tail.Write(obj, dest);
			}
			if (writePacked)
			{
				if (flag)
				{
					ProtoWriter.ClearPackedField(this.fieldNumber, dest);
					return;
				}
				ProtoWriter.EndSubItem(token, dest);
			}
		}

		// Token: 0x06007D9F RID: 32159 RVA: 0x0024F14C File Offset: 0x0024F14C
		private bool CanUsePackedPrefix(object obj)
		{
			return ArrayDecorator.CanUsePackedPrefix(this.packedWireType, this.Tail.ExpectedType);
		}

		// Token: 0x06007DA0 RID: 32160 RVA: 0x0024F164 File Offset: 0x0024F164
		public override object Read(object value, ProtoReader source)
		{
			object result;
			try
			{
				int field = source.FieldNumber;
				object obj = value;
				if (value == null)
				{
					value = Activator.CreateInstance(this.concreteType);
				}
				bool flag = this.IsList && !this.SuppressIList;
				if (this.packedWireType != WireType.None && source.WireType == WireType.String)
				{
					SubItemToken token = ProtoReader.StartSubItem(source);
					if (flag)
					{
						IList list = (IList)value;
						while (ProtoReader.HasSubValue(this.packedWireType, source))
						{
							list.Add(this.Tail.Read(null, source));
						}
					}
					else
					{
						object[] array = new object[1];
						while (ProtoReader.HasSubValue(this.packedWireType, source))
						{
							array[0] = this.Tail.Read(null, source);
							this.add.Invoke(value, array);
						}
					}
					ProtoReader.EndSubItem(token, source);
				}
				else if (flag)
				{
					IList list2 = (IList)value;
					do
					{
						list2.Add(this.Tail.Read(null, source));
					}
					while (source.TryReadFieldHeader(field));
				}
				else
				{
					object[] array2 = new object[1];
					do
					{
						array2[0] = this.Tail.Read(null, source);
						this.add.Invoke(value, array2);
					}
					while (source.TryReadFieldHeader(field));
				}
				result = ((obj == value) ? null : value);
			}
			catch (TargetInvocationException ex)
			{
				if (ex.InnerException != null)
				{
					throw ex.InnerException;
				}
				throw;
			}
			return result;
		}

		// Token: 0x04003C57 RID: 15447
		private readonly byte options;

		// Token: 0x04003C58 RID: 15448
		private const byte OPTIONS_IsList = 1;

		// Token: 0x04003C59 RID: 15449
		private const byte OPTIONS_SuppressIList = 2;

		// Token: 0x04003C5A RID: 15450
		private const byte OPTIONS_WritePacked = 4;

		// Token: 0x04003C5B RID: 15451
		private const byte OPTIONS_ReturnList = 8;

		// Token: 0x04003C5C RID: 15452
		private const byte OPTIONS_OverwriteList = 16;

		// Token: 0x04003C5D RID: 15453
		private const byte OPTIONS_SupportNull = 32;

		// Token: 0x04003C5E RID: 15454
		private readonly Type declaredType;

		// Token: 0x04003C5F RID: 15455
		private readonly Type concreteType;

		// Token: 0x04003C60 RID: 15456
		private readonly MethodInfo add;

		// Token: 0x04003C61 RID: 15457
		private readonly int fieldNumber;

		// Token: 0x04003C62 RID: 15458
		protected readonly WireType packedWireType;

		// Token: 0x04003C63 RID: 15459
		private static readonly Type ienumeratorType = typeof(IEnumerator);

		// Token: 0x04003C64 RID: 15460
		private static readonly Type ienumerableType = typeof(IEnumerable);
	}
}
