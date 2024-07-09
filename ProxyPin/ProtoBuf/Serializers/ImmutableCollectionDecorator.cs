using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C53 RID: 3155
	internal sealed class ImmutableCollectionDecorator : ListDecorator
	{
		// Token: 0x17001B26 RID: 6950
		// (get) Token: 0x06007D5B RID: 32091 RVA: 0x0024D7EC File Offset: 0x0024D7EC
		protected override bool RequireAdd
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007D5C RID: 32092 RVA: 0x0024D7F0 File Offset: 0x0024D7F0
		private static Type ResolveIReadOnlyCollection(Type declaredType, Type t)
		{
			if (ImmutableCollectionDecorator.CheckIsIReadOnlyCollectionExactly(declaredType))
			{
				return declaredType;
			}
			foreach (Type type in declaredType.GetInterfaces())
			{
				if (ImmutableCollectionDecorator.CheckIsIReadOnlyCollectionExactly(type))
				{
					return type;
				}
			}
			return null;
		}

		// Token: 0x06007D5D RID: 32093 RVA: 0x0024D83C File Offset: 0x0024D83C
		private static bool CheckIsIReadOnlyCollectionExactly(Type t)
		{
			if (t != null && t.IsGenericType && t.Name.StartsWith("IReadOnlyCollection`"))
			{
				Type[] genericArguments = t.GetGenericArguments();
				return genericArguments.Length == 1 || !(genericArguments[0] != t);
			}
			return false;
		}

		// Token: 0x06007D5E RID: 32094 RVA: 0x0024D8A0 File Offset: 0x0024D8A0
		internal static bool IdentifyImmutable(TypeModel model, Type declaredType, out MethodInfo builderFactory, out PropertyInfo isEmpty, out PropertyInfo length, out MethodInfo add, out MethodInfo addRange, out MethodInfo finish)
		{
			MethodInfo methodInfo;
			finish = (methodInfo = null);
			addRange = (methodInfo = methodInfo);
			add = (methodInfo = methodInfo);
			builderFactory = methodInfo;
			PropertyInfo propertyInfo;
			length = (propertyInfo = null);
			isEmpty = propertyInfo;
			if (model == null || declaredType == null)
			{
				return false;
			}
			if (!declaredType.IsGenericType)
			{
				return false;
			}
			Type[] genericArguments = declaredType.GetGenericArguments();
			int num = genericArguments.Length;
			Type[] array;
			if (num != 1)
			{
				if (num != 2)
				{
					return false;
				}
				Type type = model.MapType(typeof(KeyValuePair<, >));
				if (type == null)
				{
					return false;
				}
				type = type.MakeGenericType(genericArguments);
				array = new Type[]
				{
					type
				};
			}
			else
			{
				array = genericArguments;
			}
			if (ImmutableCollectionDecorator.ResolveIReadOnlyCollection(declaredType, null) == null)
			{
				return false;
			}
			string text = declaredType.Name;
			int num2 = text.IndexOf('`');
			if (num2 <= 0)
			{
				return false;
			}
			text = (declaredType.IsInterface ? text.Substring(1, num2 - 1) : text.Substring(0, num2));
			Type type2 = model.GetType(declaredType.Namespace + "." + text, declaredType.Assembly);
			if (type2 == null && text == "ImmutableSet")
			{
				type2 = model.GetType(declaredType.Namespace + ".ImmutableHashSet", declaredType.Assembly);
			}
			if (type2 == null)
			{
				return false;
			}
			foreach (MethodInfo methodInfo2 in type2.GetMethods())
			{
				if (methodInfo2.IsStatic && !(methodInfo2.Name != "CreateBuilder") && methodInfo2.IsGenericMethodDefinition && methodInfo2.GetParameters().Length == 0 && methodInfo2.GetGenericArguments().Length == genericArguments.Length)
				{
					builderFactory = methodInfo2.MakeGenericMethod(genericArguments);
					break;
				}
			}
			Type right = model.MapType(typeof(void));
			if (builderFactory == null || builderFactory.ReturnType == null || builderFactory.ReturnType == right)
			{
				return false;
			}
			isEmpty = Helpers.GetProperty(declaredType, "IsDefaultOrEmpty", false);
			if (isEmpty == null)
			{
				isEmpty = Helpers.GetProperty(declaredType, "IsEmpty", false);
			}
			if (isEmpty == null)
			{
				length = Helpers.GetProperty(declaredType, "Length", false);
				if (length == null)
				{
					length = Helpers.GetProperty(declaredType, "Count", false);
				}
				if (length == null)
				{
					length = Helpers.GetProperty(ImmutableCollectionDecorator.ResolveIReadOnlyCollection(declaredType, array[0]), "Count", false);
				}
				if (length == null)
				{
					return false;
				}
			}
			add = Helpers.GetInstanceMethod(builderFactory.ReturnType, "Add", array);
			if (add == null)
			{
				return false;
			}
			finish = Helpers.GetInstanceMethod(builderFactory.ReturnType, "ToImmutable", Helpers.EmptyTypes);
			if (finish == null || finish.ReturnType == null || finish.ReturnType == right)
			{
				return false;
			}
			if (!(finish.ReturnType == declaredType) && !Helpers.IsAssignableFrom(declaredType, finish.ReturnType))
			{
				return false;
			}
			addRange = Helpers.GetInstanceMethod(builderFactory.ReturnType, "AddRange", new Type[]
			{
				declaredType
			});
			if (addRange == null)
			{
				Type type3 = model.MapType(typeof(IEnumerable<>), false);
				if (type3 != null)
				{
					addRange = Helpers.GetInstanceMethod(builderFactory.ReturnType, "AddRange", new Type[]
					{
						type3.MakeGenericType(array)
					});
				}
			}
			return true;
		}

		// Token: 0x06007D5F RID: 32095 RVA: 0x0024DCA0 File Offset: 0x0024DCA0
		internal ImmutableCollectionDecorator(TypeModel model, Type declaredType, Type concreteType, IProtoSerializer tail, int fieldNumber, bool writePacked, WireType packedWireType, bool returnList, bool overwriteList, bool supportNull, MethodInfo builderFactory, PropertyInfo isEmpty, PropertyInfo length, MethodInfo add, MethodInfo addRange, MethodInfo finish) : base(model, declaredType, concreteType, tail, fieldNumber, writePacked, packedWireType, returnList, overwriteList, supportNull)
		{
			this.builderFactory = builderFactory;
			this.isEmpty = isEmpty;
			this.length = length;
			this.add = add;
			this.addRange = addRange;
			this.finish = finish;
		}

		// Token: 0x06007D60 RID: 32096 RVA: 0x0024DCF8 File Offset: 0x0024DCF8
		public override object Read(object value, ProtoReader source)
		{
			object obj = this.builderFactory.Invoke(null, null);
			int fieldNumber = source.FieldNumber;
			object[] array = new object[1];
			if (base.AppendToCollection && value != null && ((this.isEmpty != null) ? (!(bool)this.isEmpty.GetValue(value, null)) : ((int)this.length.GetValue(value, null) != 0)))
			{
				if (this.addRange != null)
				{
					array[0] = value;
					this.addRange.Invoke(obj, array);
				}
				else
				{
					foreach (object obj2 in ((ICollection)value))
					{
						array[0] = obj2;
						this.add.Invoke(obj, array);
					}
				}
			}
			if (this.packedWireType != WireType.None && source.WireType == WireType.String)
			{
				SubItemToken token = ProtoReader.StartSubItem(source);
				while (ProtoReader.HasSubValue(this.packedWireType, source))
				{
					array[0] = this.Tail.Read(null, source);
					this.add.Invoke(obj, array);
				}
				ProtoReader.EndSubItem(token, source);
			}
			else
			{
				do
				{
					array[0] = this.Tail.Read(null, source);
					this.add.Invoke(obj, array);
				}
				while (source.TryReadFieldHeader(fieldNumber));
			}
			return this.finish.Invoke(obj, null);
		}

		// Token: 0x06007D61 RID: 32097 RVA: 0x0024DE94 File Offset: 0x0024DE94
		protected override void EmitRead(CompilerContext ctx, Local valueFrom)
		{
			using (Local local = base.AppendToCollection ? ctx.GetLocalWithValue(this.ExpectedType, valueFrom) : null)
			{
				using (Local local2 = new Local(ctx, this.builderFactory.ReturnType))
				{
					ctx.EmitCall(this.builderFactory);
					ctx.StoreValue(local2);
					if (base.AppendToCollection)
					{
						CodeLabel label = ctx.DefineLabel();
						if (!Helpers.IsValueType(this.ExpectedType))
						{
							ctx.LoadValue(local);
							ctx.BranchIfFalse(label, false);
						}
						ctx.LoadAddress(local, local.Type, false);
						if (this.isEmpty != null)
						{
							ctx.EmitCall(Helpers.GetGetMethod(this.isEmpty, false, false));
							ctx.BranchIfTrue(label, false);
						}
						else
						{
							ctx.EmitCall(Helpers.GetGetMethod(this.length, false, false));
							ctx.BranchIfFalse(label, false);
						}
						Type right = ctx.MapType(typeof(void));
						if (this.addRange != null)
						{
							ctx.LoadValue(local2);
							ctx.LoadValue(local);
							ctx.EmitCall(this.addRange);
							if (this.addRange.ReturnType != null && this.add.ReturnType != right)
							{
								ctx.DiscardValue();
							}
						}
						else
						{
							MethodInfo method;
							MethodInfo method2;
							MethodInfo enumeratorInfo = base.GetEnumeratorInfo(ctx.Model, out method, out method2);
							Type returnType = enumeratorInfo.ReturnType;
							using (Local local3 = new Local(ctx, returnType))
							{
								ctx.LoadAddress(local, this.ExpectedType, false);
								ctx.EmitCall(enumeratorInfo);
								ctx.StoreValue(local3);
								using (ctx.Using(local3))
								{
									CodeLabel label2 = ctx.DefineLabel();
									CodeLabel label3 = ctx.DefineLabel();
									ctx.Branch(label3, false);
									ctx.MarkLabel(label2);
									ctx.LoadAddress(local2, local2.Type, false);
									ctx.LoadAddress(local3, returnType, false);
									ctx.EmitCall(method2);
									ctx.EmitCall(this.add);
									if (this.add.ReturnType != null && this.add.ReturnType != right)
									{
										ctx.DiscardValue();
									}
									ctx.MarkLabel(label3);
									ctx.LoadAddress(local3, returnType, false);
									ctx.EmitCall(method);
									ctx.BranchIfTrue(label2, false);
								}
							}
						}
						ctx.MarkLabel(label);
					}
					ListDecorator.EmitReadList(ctx, local2, this.Tail, this.add, this.packedWireType, false);
					ctx.LoadAddress(local2, local2.Type, false);
					ctx.EmitCall(this.finish);
					if (this.ExpectedType != this.finish.ReturnType)
					{
						ctx.Cast(this.ExpectedType);
					}
				}
			}
		}

		// Token: 0x04003C4E RID: 15438
		private readonly MethodInfo builderFactory;

		// Token: 0x04003C4F RID: 15439
		private readonly MethodInfo add;

		// Token: 0x04003C50 RID: 15440
		private readonly MethodInfo addRange;

		// Token: 0x04003C51 RID: 15441
		private readonly MethodInfo finish;

		// Token: 0x04003C52 RID: 15442
		private readonly PropertyInfo isEmpty;

		// Token: 0x04003C53 RID: 15443
		private readonly PropertyInfo length;
	}
}
