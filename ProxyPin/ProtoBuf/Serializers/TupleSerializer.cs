using System;
using System.Reflection;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C6A RID: 3178
	internal sealed class TupleSerializer : IProtoTypeSerializer, IProtoSerializer
	{
		// Token: 0x06007E3F RID: 32319 RVA: 0x00251B70 File Offset: 0x00251B70
		public TupleSerializer(RuntimeTypeModel model, ConstructorInfo ctor, MemberInfo[] members)
		{
			if (ctor == null)
			{
				throw new ArgumentNullException("ctor");
			}
			this.ctor = ctor;
			if (members == null)
			{
				throw new ArgumentNullException("members");
			}
			this.members = members;
			this.tails = new IProtoSerializer[members.Length];
			ParameterInfo[] parameters = ctor.GetParameters();
			for (int i = 0; i < members.Length; i++)
			{
				Type parameterType = parameters[i].ParameterType;
				Type type = null;
				Type concreteType = null;
				MetaType.ResolveListTypes(model, parameterType, ref type, ref concreteType);
				Type type2 = (type == null) ? parameterType : type;
				bool asReference = false;
				int num = model.FindOrAddAuto(type2, false, true, false);
				if (num >= 0)
				{
					asReference = model[type2].AsReferenceDefault;
				}
				WireType wireType;
				IProtoSerializer protoSerializer = ValueMember.TryGetCoreSerializer(model, DataFormat.Default, type2, out wireType, asReference, false, false, true);
				if (protoSerializer == null)
				{
					throw new InvalidOperationException("No serializer defined for type: " + type2.FullName);
				}
				protoSerializer = new TagDecorator(i + 1, wireType, false, protoSerializer);
				IProtoSerializer protoSerializer2;
				if (type == null)
				{
					protoSerializer2 = protoSerializer;
				}
				else if (parameterType.IsArray)
				{
					protoSerializer2 = new ArrayDecorator(model, protoSerializer, i + 1, false, wireType, parameterType, false, false);
				}
				else
				{
					protoSerializer2 = ListDecorator.Create(model, parameterType, concreteType, protoSerializer, i + 1, false, wireType, true, false, false);
				}
				this.tails[i] = protoSerializer2;
			}
		}

		// Token: 0x06007E40 RID: 32320 RVA: 0x00251CD8 File Offset: 0x00251CD8
		public bool HasCallbacks(TypeModel.CallbackType callbackType)
		{
			return false;
		}

		// Token: 0x06007E41 RID: 32321 RVA: 0x00251CDC File Offset: 0x00251CDC
		public void EmitCallback(CompilerContext ctx, Local valueFrom, TypeModel.CallbackType callbackType)
		{
		}

		// Token: 0x17001B6D RID: 7021
		// (get) Token: 0x06007E42 RID: 32322 RVA: 0x00251CE0 File Offset: 0x00251CE0
		public Type ExpectedType
		{
			get
			{
				return this.ctor.DeclaringType;
			}
		}

		// Token: 0x06007E43 RID: 32323 RVA: 0x00251CF0 File Offset: 0x00251CF0
		void IProtoTypeSerializer.Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
		{
		}

		// Token: 0x06007E44 RID: 32324 RVA: 0x00251CF4 File Offset: 0x00251CF4
		object IProtoTypeSerializer.CreateInstance(ProtoReader source)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06007E45 RID: 32325 RVA: 0x00251CFC File Offset: 0x00251CFC
		private object GetValue(object obj, int index)
		{
			PropertyInfo propertyInfo;
			if ((propertyInfo = (this.members[index] as PropertyInfo)) != null)
			{
				if (obj != null)
				{
					return propertyInfo.GetValue(obj, null);
				}
				if (!Helpers.IsValueType(propertyInfo.PropertyType))
				{
					return null;
				}
				return Activator.CreateInstance(propertyInfo.PropertyType);
			}
			else
			{
				FieldInfo fieldInfo;
				if (!((fieldInfo = (this.members[index] as FieldInfo)) != null))
				{
					throw new InvalidOperationException();
				}
				if (obj != null)
				{
					return fieldInfo.GetValue(obj);
				}
				if (!Helpers.IsValueType(fieldInfo.FieldType))
				{
					return null;
				}
				return Activator.CreateInstance(fieldInfo.FieldType);
			}
		}

		// Token: 0x06007E46 RID: 32326 RVA: 0x00251DA8 File Offset: 0x00251DA8
		public object Read(object value, ProtoReader source)
		{
			object[] array = new object[this.members.Length];
			bool flag = false;
			if (value == null)
			{
				flag = true;
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.GetValue(value, i);
			}
			int num;
			while ((num = source.ReadFieldHeader()) > 0)
			{
				flag = true;
				if (num <= this.tails.Length)
				{
					IProtoSerializer protoSerializer = this.tails[num - 1];
					array[num - 1] = this.tails[num - 1].Read(protoSerializer.RequiresOldValue ? array[num - 1] : null, source);
				}
				else
				{
					source.SkipField();
				}
			}
			if (!flag)
			{
				return value;
			}
			return this.ctor.Invoke(array);
		}

		// Token: 0x06007E47 RID: 32327 RVA: 0x00251E6C File Offset: 0x00251E6C
		public void Write(object value, ProtoWriter dest)
		{
			for (int i = 0; i < this.tails.Length; i++)
			{
				object value2 = this.GetValue(value, i);
				if (value2 != null)
				{
					this.tails[i].Write(value2, dest);
				}
			}
		}

		// Token: 0x17001B6E RID: 7022
		// (get) Token: 0x06007E48 RID: 32328 RVA: 0x00251EB8 File Offset: 0x00251EB8
		public bool RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B6F RID: 7023
		// (get) Token: 0x06007E49 RID: 32329 RVA: 0x00251EBC File Offset: 0x00251EBC
		public bool ReturnsValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007E4A RID: 32330 RVA: 0x00251EC0 File Offset: 0x00251EC0
		private Type GetMemberType(int index)
		{
			Type memberType = Helpers.GetMemberType(this.members[index]);
			if (memberType == null)
			{
				throw new InvalidOperationException();
			}
			return memberType;
		}

		// Token: 0x06007E4B RID: 32331 RVA: 0x00251EF8 File Offset: 0x00251EF8
		bool IProtoTypeSerializer.CanCreateInstance()
		{
			return false;
		}

		// Token: 0x06007E4C RID: 32332 RVA: 0x00251EFC File Offset: 0x00251EFC
		public void EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			using (Local localWithValue = ctx.GetLocalWithValue(this.ctor.DeclaringType, valueFrom))
			{
				for (int i = 0; i < this.tails.Length; i++)
				{
					Type memberType = this.GetMemberType(i);
					ctx.LoadAddress(localWithValue, this.ExpectedType, false);
					if (this.members[i] is FieldInfo)
					{
						ctx.LoadValue((FieldInfo)this.members[i]);
					}
					else if (this.members[i] is PropertyInfo)
					{
						ctx.LoadValue((PropertyInfo)this.members[i]);
					}
					ctx.WriteNullCheckedTail(memberType, this.tails[i], null);
				}
			}
		}

		// Token: 0x06007E4D RID: 32333 RVA: 0x00251FE0 File Offset: 0x00251FE0
		void IProtoTypeSerializer.EmitCreateInstance(CompilerContext ctx)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06007E4E RID: 32334 RVA: 0x00251FE8 File Offset: 0x00251FE8
		public void EmitRead(CompilerContext ctx, Local incoming)
		{
			using (Local localWithValue = ctx.GetLocalWithValue(this.ExpectedType, incoming))
			{
				Local[] array = new Local[this.members.Length];
				try
				{
					for (int i = 0; i < array.Length; i++)
					{
						Type memberType = this.GetMemberType(i);
						bool flag = true;
						array[i] = new Local(ctx, memberType);
						if (!Helpers.IsValueType(this.ExpectedType))
						{
							if (Helpers.IsValueType(memberType))
							{
								ProtoTypeCode typeCode = Helpers.GetTypeCode(memberType);
								switch (typeCode)
								{
								case ProtoTypeCode.Boolean:
								case ProtoTypeCode.SByte:
								case ProtoTypeCode.Byte:
								case ProtoTypeCode.Int16:
								case ProtoTypeCode.UInt16:
								case ProtoTypeCode.Int32:
								case ProtoTypeCode.UInt32:
									ctx.LoadValue(0);
									goto IL_12B;
								case ProtoTypeCode.Char:
									break;
								case ProtoTypeCode.Int64:
								case ProtoTypeCode.UInt64:
									ctx.LoadValue(0L);
									goto IL_12B;
								case ProtoTypeCode.Single:
									ctx.LoadValue(0f);
									goto IL_12B;
								case ProtoTypeCode.Double:
									ctx.LoadValue(0.0);
									goto IL_12B;
								case ProtoTypeCode.Decimal:
									ctx.LoadValue(0m);
									goto IL_12B;
								default:
									if (typeCode == ProtoTypeCode.Guid)
									{
										ctx.LoadValue(Guid.Empty);
										goto IL_12B;
									}
									break;
								}
								ctx.LoadAddress(array[i], memberType, false);
								ctx.EmitCtor(memberType);
								flag = false;
							}
							else
							{
								ctx.LoadNullRef();
							}
							IL_12B:
							if (flag)
							{
								ctx.StoreValue(array[i]);
							}
						}
					}
					CodeLabel label = Helpers.IsValueType(this.ExpectedType) ? default(CodeLabel) : ctx.DefineLabel();
					if (!Helpers.IsValueType(this.ExpectedType))
					{
						ctx.LoadAddress(localWithValue, this.ExpectedType, false);
						ctx.BranchIfFalse(label, false);
					}
					for (int j = 0; j < this.members.Length; j++)
					{
						ctx.LoadAddress(localWithValue, this.ExpectedType, false);
						if (this.members[j] is FieldInfo)
						{
							ctx.LoadValue((FieldInfo)this.members[j]);
						}
						else if (this.members[j] is PropertyInfo)
						{
							ctx.LoadValue((PropertyInfo)this.members[j]);
						}
						ctx.StoreValue(array[j]);
					}
					if (!Helpers.IsValueType(this.ExpectedType))
					{
						ctx.MarkLabel(label);
					}
					using (Local local = new Local(ctx, ctx.MapType(typeof(int))))
					{
						CodeLabel label2 = ctx.DefineLabel();
						CodeLabel label3 = ctx.DefineLabel();
						CodeLabel label4 = ctx.DefineLabel();
						ctx.Branch(label2, false);
						CodeLabel[] array2 = new CodeLabel[this.members.Length];
						for (int k = 0; k < this.members.Length; k++)
						{
							array2[k] = ctx.DefineLabel();
						}
						ctx.MarkLabel(label3);
						ctx.LoadValue(local);
						ctx.LoadValue(1);
						ctx.Subtract();
						ctx.Switch(array2);
						ctx.Branch(label4, false);
						for (int l = 0; l < array2.Length; l++)
						{
							ctx.MarkLabel(array2[l]);
							IProtoSerializer protoSerializer = this.tails[l];
							Local valueFrom = protoSerializer.RequiresOldValue ? array[l] : null;
							ctx.ReadNullCheckedTail(array[l].Type, protoSerializer, valueFrom);
							if (protoSerializer.ReturnsValue)
							{
								if (Helpers.IsValueType(array[l].Type))
								{
									ctx.StoreValue(array[l]);
								}
								else
								{
									CodeLabel label5 = ctx.DefineLabel();
									CodeLabel label6 = ctx.DefineLabel();
									ctx.CopyValue();
									ctx.BranchIfTrue(label5, true);
									ctx.DiscardValue();
									ctx.Branch(label6, true);
									ctx.MarkLabel(label5);
									ctx.StoreValue(array[l]);
									ctx.MarkLabel(label6);
								}
							}
							ctx.Branch(label2, false);
						}
						ctx.MarkLabel(label4);
						ctx.LoadReaderWriter();
						ctx.EmitCall(ctx.MapType(typeof(ProtoReader)).GetMethod("SkipField"));
						ctx.MarkLabel(label2);
						ctx.EmitBasicRead("ReadFieldHeader", ctx.MapType(typeof(int)));
						ctx.CopyValue();
						ctx.StoreValue(local);
						ctx.LoadValue(0);
						ctx.BranchIfGreater(label3, false);
					}
					for (int m = 0; m < array.Length; m++)
					{
						ctx.LoadValue(array[m]);
					}
					ctx.EmitCtor(this.ctor);
					ctx.StoreValue(localWithValue);
				}
				finally
				{
					for (int n = 0; n < array.Length; n++)
					{
						if (array[n] != null)
						{
							array[n].Dispose();
						}
					}
				}
			}
		}

		// Token: 0x04003C8C RID: 15500
		private readonly MemberInfo[] members;

		// Token: 0x04003C8D RID: 15501
		private readonly ConstructorInfo ctor;

		// Token: 0x04003C8E RID: 15502
		private IProtoSerializer[] tails;
	}
}
