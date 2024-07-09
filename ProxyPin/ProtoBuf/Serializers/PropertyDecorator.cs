using System;
using System.Reflection;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C60 RID: 3168
	internal sealed class PropertyDecorator : ProtoDecoratorBase
	{
		// Token: 0x17001B4E RID: 6990
		// (get) Token: 0x06007DCF RID: 32207 RVA: 0x002509AC File Offset: 0x002509AC
		public override Type ExpectedType
		{
			get
			{
				return this.forType;
			}
		}

		// Token: 0x17001B4F RID: 6991
		// (get) Token: 0x06007DD0 RID: 32208 RVA: 0x002509B4 File Offset: 0x002509B4
		public override bool RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B50 RID: 6992
		// (get) Token: 0x06007DD1 RID: 32209 RVA: 0x002509B8 File Offset: 0x002509B8
		public override bool ReturnsValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007DD2 RID: 32210 RVA: 0x002509BC File Offset: 0x002509BC
		public PropertyDecorator(TypeModel model, Type forType, PropertyInfo property, IProtoSerializer tail) : base(tail)
		{
			this.forType = forType;
			this.property = property;
			PropertyDecorator.SanityCheck(model, property, tail, out this.readOptionsWriteValue, true, true);
			this.shadowSetter = PropertyDecorator.GetShadowSetter(model, property);
		}

		// Token: 0x06007DD3 RID: 32211 RVA: 0x002509F4 File Offset: 0x002509F4
		private static void SanityCheck(TypeModel model, PropertyInfo property, IProtoSerializer tail, out bool writeValue, bool nonPublic, bool allowInternal)
		{
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			writeValue = (tail.ReturnsValue && (PropertyDecorator.GetShadowSetter(model, property) != null || (property.CanWrite && Helpers.GetSetMethod(property, nonPublic, allowInternal) != null)));
			if (!property.CanRead || Helpers.GetGetMethod(property, nonPublic, allowInternal) == null)
			{
				throw new InvalidOperationException("Cannot serialize property without a get accessor");
			}
			if (!writeValue && (!tail.RequiresOldValue || Helpers.IsValueType(tail.ExpectedType)))
			{
				throw new InvalidOperationException("Cannot apply changes to property " + property.DeclaringType.FullName + "." + property.Name);
			}
		}

		// Token: 0x06007DD4 RID: 32212 RVA: 0x00250ADC File Offset: 0x00250ADC
		private static MethodInfo GetShadowSetter(TypeModel model, PropertyInfo property)
		{
			Type reflectedType = property.ReflectedType;
			MethodInfo instanceMethod = Helpers.GetInstanceMethod(reflectedType, "Set" + property.Name, new Type[]
			{
				property.PropertyType
			});
			if (instanceMethod == null || !instanceMethod.IsPublic || instanceMethod.ReturnType != model.MapType(typeof(void)))
			{
				return null;
			}
			return instanceMethod;
		}

		// Token: 0x06007DD5 RID: 32213 RVA: 0x00250B54 File Offset: 0x00250B54
		public override void Write(object value, ProtoWriter dest)
		{
			value = this.property.GetValue(value, null);
			if (value != null)
			{
				this.Tail.Write(value, dest);
			}
		}

		// Token: 0x06007DD6 RID: 32214 RVA: 0x00250B78 File Offset: 0x00250B78
		public override object Read(object value, ProtoReader source)
		{
			object value2 = this.Tail.RequiresOldValue ? this.property.GetValue(value, null) : null;
			object obj = this.Tail.Read(value2, source);
			if (this.readOptionsWriteValue && obj != null)
			{
				if (this.shadowSetter == null)
				{
					this.property.SetValue(value, obj, null);
				}
				else
				{
					this.shadowSetter.Invoke(value, new object[]
					{
						obj
					});
				}
			}
			return null;
		}

		// Token: 0x06007DD7 RID: 32215 RVA: 0x00250C08 File Offset: 0x00250C08
		protected override void EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.LoadAddress(valueFrom, this.ExpectedType, false);
			ctx.LoadValue(this.property);
			ctx.WriteNullCheckedTail(this.property.PropertyType, this.Tail, null);
		}

		// Token: 0x06007DD8 RID: 32216 RVA: 0x00250C4C File Offset: 0x00250C4C
		protected override void EmitRead(CompilerContext ctx, Local valueFrom)
		{
			bool flag;
			PropertyDecorator.SanityCheck(ctx.Model, this.property, this.Tail, out flag, ctx.NonPublic, ctx.AllowInternal(this.property));
			if (Helpers.IsValueType(this.ExpectedType) && valueFrom == null)
			{
				throw new InvalidOperationException("Attempt to mutate struct on the head of the stack; changes would be lost");
			}
			using (Local localWithValue = ctx.GetLocalWithValue(this.ExpectedType, valueFrom))
			{
				if (this.Tail.RequiresOldValue)
				{
					ctx.LoadAddress(localWithValue, this.ExpectedType, false);
					ctx.LoadValue(this.property);
				}
				Type propertyType = this.property.PropertyType;
				ctx.ReadNullCheckedTail(propertyType, this.Tail, null);
				if (flag)
				{
					using (Local local = new Local(ctx, this.property.PropertyType))
					{
						ctx.StoreValue(local);
						CodeLabel label = default(CodeLabel);
						if (!Helpers.IsValueType(propertyType))
						{
							label = ctx.DefineLabel();
							ctx.LoadValue(local);
							ctx.BranchIfFalse(label, true);
						}
						ctx.LoadAddress(localWithValue, this.ExpectedType, false);
						ctx.LoadValue(local);
						if (this.shadowSetter == null)
						{
							ctx.StoreValue(this.property);
						}
						else
						{
							ctx.EmitCall(this.shadowSetter);
						}
						if (!Helpers.IsValueType(propertyType))
						{
							ctx.MarkLabel(label);
						}
						return;
					}
				}
				if (this.Tail.ReturnsValue)
				{
					ctx.DiscardValue();
				}
			}
		}

		// Token: 0x06007DD9 RID: 32217 RVA: 0x00250E08 File Offset: 0x00250E08
		internal static bool CanWrite(TypeModel model, MemberInfo member)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo != null)
			{
				return propertyInfo.CanWrite || PropertyDecorator.GetShadowSetter(model, propertyInfo) != null;
			}
			return member is FieldInfo;
		}

		// Token: 0x04003C75 RID: 15477
		private readonly PropertyInfo property;

		// Token: 0x04003C76 RID: 15478
		private readonly Type forType;

		// Token: 0x04003C77 RID: 15479
		private readonly bool readOptionsWriteValue;

		// Token: 0x04003C78 RID: 15480
		private readonly MethodInfo shadowSetter;
	}
}
