using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000AB0 RID: 2736
	[NullableContext(1)]
	[Nullable(0)]
	internal class DynamicReflectionDelegateFactory : ReflectionDelegateFactory
	{
		// Token: 0x170016B5 RID: 5813
		// (get) Token: 0x06006D15 RID: 27925 RVA: 0x0020FD38 File Offset: 0x0020FD38
		internal static DynamicReflectionDelegateFactory Instance { get; } = new DynamicReflectionDelegateFactory();

		// Token: 0x06006D16 RID: 27926 RVA: 0x0020FD40 File Offset: 0x0020FD40
		private static DynamicMethod CreateDynamicMethod(string name, [Nullable(2)] Type returnType, Type[] parameterTypes, Type owner)
		{
			if (owner.IsInterface())
			{
				return new DynamicMethod(name, returnType, parameterTypes, owner.Module, true);
			}
			return new DynamicMethod(name, returnType, parameterTypes, owner, true);
		}

		// Token: 0x06006D17 RID: 27927 RVA: 0x0020FD68 File Offset: 0x0020FD68
		public override ObjectConstructor<object> CreateParameterizedConstructor(MethodBase method)
		{
			DynamicMethod dynamicMethod = DynamicReflectionDelegateFactory.CreateDynamicMethod(method.ToString(), typeof(object), new Type[]
			{
				typeof(object[])
			}, method.DeclaringType);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			this.GenerateCreateMethodCallIL(method, ilgenerator, 0);
			return (ObjectConstructor<object>)dynamicMethod.CreateDelegate(typeof(ObjectConstructor<object>));
		}

		// Token: 0x06006D18 RID: 27928 RVA: 0x0020FDCC File Offset: 0x0020FDCC
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public override MethodCall<T, object> CreateMethodCall<[Nullable(2)] T>(MethodBase method)
		{
			DynamicMethod dynamicMethod = DynamicReflectionDelegateFactory.CreateDynamicMethod(method.ToString(), typeof(object), new Type[]
			{
				typeof(object),
				typeof(object[])
			}, method.DeclaringType);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			this.GenerateCreateMethodCallIL(method, ilgenerator, 1);
			return (MethodCall<T, object>)dynamicMethod.CreateDelegate(typeof(MethodCall<T, object>));
		}

		// Token: 0x06006D19 RID: 27929 RVA: 0x0020FE3C File Offset: 0x0020FE3C
		private void GenerateCreateMethodCallIL(MethodBase method, ILGenerator generator, int argsIndex)
		{
			ParameterInfo[] parameters = method.GetParameters();
			Label label = generator.DefineLabel();
			generator.Emit(OpCodes.Ldarg, argsIndex);
			generator.Emit(OpCodes.Ldlen);
			generator.Emit(OpCodes.Ldc_I4, parameters.Length);
			generator.Emit(OpCodes.Beq, label);
			generator.Emit(OpCodes.Newobj, typeof(TargetParameterCountException).GetConstructor(ReflectionUtils.EmptyTypes));
			generator.Emit(OpCodes.Throw);
			generator.MarkLabel(label);
			if (!method.IsConstructor && !method.IsStatic)
			{
				generator.PushInstance(method.DeclaringType);
			}
			LocalBuilder local = generator.DeclareLocal(typeof(IConvertible));
			LocalBuilder local2 = generator.DeclareLocal(typeof(object));
			OpCode opcode = (parameters.Length < 256) ? OpCodes.Ldloca_S : OpCodes.Ldloca;
			OpCode opcode2 = (parameters.Length < 256) ? OpCodes.Ldloc_S : OpCodes.Ldloc;
			for (int i = 0; i < parameters.Length; i++)
			{
				ParameterInfo parameterInfo = parameters[i];
				Type type = parameterInfo.ParameterType;
				if (type.IsByRef)
				{
					type = type.GetElementType();
					LocalBuilder local3 = generator.DeclareLocal(type);
					if (!parameterInfo.IsOut)
					{
						generator.PushArrayInstance(argsIndex, i);
						if (type.IsValueType())
						{
							Label label2 = generator.DefineLabel();
							Label label3 = generator.DefineLabel();
							generator.Emit(OpCodes.Brtrue_S, label2);
							generator.Emit(opcode, local3);
							generator.Emit(OpCodes.Initobj, type);
							generator.Emit(OpCodes.Br_S, label3);
							generator.MarkLabel(label2);
							generator.PushArrayInstance(argsIndex, i);
							generator.UnboxIfNeeded(type);
							generator.Emit(OpCodes.Stloc_S, local3);
							generator.MarkLabel(label3);
						}
						else
						{
							generator.UnboxIfNeeded(type);
							generator.Emit(OpCodes.Stloc_S, local3);
						}
					}
					generator.Emit(opcode, local3);
				}
				else if (type.IsValueType())
				{
					generator.PushArrayInstance(argsIndex, i);
					generator.Emit(OpCodes.Stloc_S, local2);
					Label label4 = generator.DefineLabel();
					Label label5 = generator.DefineLabel();
					generator.Emit(OpCodes.Ldloc_S, local2);
					generator.Emit(OpCodes.Brtrue_S, label4);
					LocalBuilder local4 = generator.DeclareLocal(type);
					generator.Emit(opcode, local4);
					generator.Emit(OpCodes.Initobj, type);
					generator.Emit(opcode2, local4);
					generator.Emit(OpCodes.Br_S, label5);
					generator.MarkLabel(label4);
					if (type.IsPrimitive())
					{
						MethodInfo method2 = typeof(IConvertible).GetMethod("To" + type.Name, new Type[]
						{
							typeof(IFormatProvider)
						});
						if (method2 != null)
						{
							Label label6 = generator.DefineLabel();
							generator.Emit(OpCodes.Ldloc_S, local2);
							generator.Emit(OpCodes.Isinst, type);
							generator.Emit(OpCodes.Brtrue_S, label6);
							generator.Emit(OpCodes.Ldloc_S, local2);
							generator.Emit(OpCodes.Isinst, typeof(IConvertible));
							generator.Emit(OpCodes.Stloc_S, local);
							generator.Emit(OpCodes.Ldloc_S, local);
							generator.Emit(OpCodes.Brfalse_S, label6);
							generator.Emit(OpCodes.Ldloc_S, local);
							generator.Emit(OpCodes.Ldnull);
							generator.Emit(OpCodes.Callvirt, method2);
							generator.Emit(OpCodes.Br_S, label5);
							generator.MarkLabel(label6);
						}
					}
					generator.Emit(OpCodes.Ldloc_S, local2);
					generator.UnboxIfNeeded(type);
					generator.MarkLabel(label5);
				}
				else
				{
					generator.PushArrayInstance(argsIndex, i);
					generator.UnboxIfNeeded(type);
				}
			}
			if (method.IsConstructor)
			{
				generator.Emit(OpCodes.Newobj, (ConstructorInfo)method);
			}
			else
			{
				generator.CallMethod((MethodInfo)method);
			}
			Type type2 = method.IsConstructor ? method.DeclaringType : ((MethodInfo)method).ReturnType;
			if (type2 != typeof(void))
			{
				generator.BoxIfNeeded(type2);
			}
			else
			{
				generator.Emit(OpCodes.Ldnull);
			}
			generator.Return();
		}

		// Token: 0x06006D1A RID: 27930 RVA: 0x0021027C File Offset: 0x0021027C
		public override Func<T> CreateDefaultConstructor<[Nullable(2)] T>(Type type)
		{
			DynamicMethod dynamicMethod = DynamicReflectionDelegateFactory.CreateDynamicMethod("Create" + type.FullName, typeof(T), ReflectionUtils.EmptyTypes, type);
			dynamicMethod.InitLocals = true;
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			this.GenerateCreateDefaultConstructorIL(type, ilgenerator, typeof(T));
			return (Func<T>)dynamicMethod.CreateDelegate(typeof(Func<T>));
		}

		// Token: 0x06006D1B RID: 27931 RVA: 0x002102E8 File Offset: 0x002102E8
		private void GenerateCreateDefaultConstructorIL(Type type, ILGenerator generator, Type delegateType)
		{
			if (type.IsValueType())
			{
				generator.DeclareLocal(type);
				generator.Emit(OpCodes.Ldloc_0);
				if (type != delegateType)
				{
					generator.Emit(OpCodes.Box, type);
				}
			}
			else
			{
				ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, ReflectionUtils.EmptyTypes, null);
				if (constructor == null)
				{
					throw new ArgumentException("Could not get constructor for {0}.".FormatWith(CultureInfo.InvariantCulture, type));
				}
				generator.Emit(OpCodes.Newobj, constructor);
			}
			generator.Return();
		}

		// Token: 0x06006D1C RID: 27932 RVA: 0x00210378 File Offset: 0x00210378
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public override Func<T, object> CreateGet<[Nullable(2)] T>(PropertyInfo propertyInfo)
		{
			DynamicMethod dynamicMethod = DynamicReflectionDelegateFactory.CreateDynamicMethod("Get" + propertyInfo.Name, typeof(object), new Type[]
			{
				typeof(T)
			}, propertyInfo.DeclaringType);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			this.GenerateCreateGetPropertyIL(propertyInfo, ilgenerator);
			return (Func<T, object>)dynamicMethod.CreateDelegate(typeof(Func<T, object>));
		}

		// Token: 0x06006D1D RID: 27933 RVA: 0x002103E4 File Offset: 0x002103E4
		private void GenerateCreateGetPropertyIL(PropertyInfo propertyInfo, ILGenerator generator)
		{
			MethodInfo getMethod = propertyInfo.GetGetMethod(true);
			if (getMethod == null)
			{
				throw new ArgumentException("Property '{0}' does not have a getter.".FormatWith(CultureInfo.InvariantCulture, propertyInfo.Name));
			}
			if (!getMethod.IsStatic)
			{
				generator.PushInstance(propertyInfo.DeclaringType);
			}
			generator.CallMethod(getMethod);
			generator.BoxIfNeeded(propertyInfo.PropertyType);
			generator.Return();
		}

		// Token: 0x06006D1E RID: 27934 RVA: 0x00210454 File Offset: 0x00210454
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public override Func<T, object> CreateGet<[Nullable(2)] T>(FieldInfo fieldInfo)
		{
			if (fieldInfo.IsLiteral)
			{
				object constantValue = fieldInfo.GetValue(null);
				return (T o) => constantValue;
			}
			DynamicMethod dynamicMethod = DynamicReflectionDelegateFactory.CreateDynamicMethod("Get" + fieldInfo.Name, typeof(T), new Type[]
			{
				typeof(object)
			}, fieldInfo.DeclaringType);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			this.GenerateCreateGetFieldIL(fieldInfo, ilgenerator);
			return (Func<T, object>)dynamicMethod.CreateDelegate(typeof(Func<T, object>));
		}

		// Token: 0x06006D1F RID: 27935 RVA: 0x002104EC File Offset: 0x002104EC
		private void GenerateCreateGetFieldIL(FieldInfo fieldInfo, ILGenerator generator)
		{
			if (!fieldInfo.IsStatic)
			{
				generator.PushInstance(fieldInfo.DeclaringType);
				generator.Emit(OpCodes.Ldfld, fieldInfo);
			}
			else
			{
				generator.Emit(OpCodes.Ldsfld, fieldInfo);
			}
			generator.BoxIfNeeded(fieldInfo.FieldType);
			generator.Return();
		}

		// Token: 0x06006D20 RID: 27936 RVA: 0x00210544 File Offset: 0x00210544
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public override Action<T, object> CreateSet<[Nullable(2)] T>(FieldInfo fieldInfo)
		{
			DynamicMethod dynamicMethod = DynamicReflectionDelegateFactory.CreateDynamicMethod("Set" + fieldInfo.Name, null, new Type[]
			{
				typeof(T),
				typeof(object)
			}, fieldInfo.DeclaringType);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			DynamicReflectionDelegateFactory.GenerateCreateSetFieldIL(fieldInfo, ilgenerator);
			return (Action<T, object>)dynamicMethod.CreateDelegate(typeof(Action<T, object>));
		}

		// Token: 0x06006D21 RID: 27937 RVA: 0x002105B4 File Offset: 0x002105B4
		internal static void GenerateCreateSetFieldIL(FieldInfo fieldInfo, ILGenerator generator)
		{
			if (!fieldInfo.IsStatic)
			{
				generator.PushInstance(fieldInfo.DeclaringType);
			}
			generator.Emit(OpCodes.Ldarg_1);
			generator.UnboxIfNeeded(fieldInfo.FieldType);
			if (!fieldInfo.IsStatic)
			{
				generator.Emit(OpCodes.Stfld, fieldInfo);
			}
			else
			{
				generator.Emit(OpCodes.Stsfld, fieldInfo);
			}
			generator.Return();
		}

		// Token: 0x06006D22 RID: 27938 RVA: 0x00210624 File Offset: 0x00210624
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public override Action<T, object> CreateSet<[Nullable(2)] T>(PropertyInfo propertyInfo)
		{
			DynamicMethod dynamicMethod = DynamicReflectionDelegateFactory.CreateDynamicMethod("Set" + propertyInfo.Name, null, new Type[]
			{
				typeof(T),
				typeof(object)
			}, propertyInfo.DeclaringType);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			DynamicReflectionDelegateFactory.GenerateCreateSetPropertyIL(propertyInfo, ilgenerator);
			return (Action<T, object>)dynamicMethod.CreateDelegate(typeof(Action<T, object>));
		}

		// Token: 0x06006D23 RID: 27939 RVA: 0x00210694 File Offset: 0x00210694
		internal static void GenerateCreateSetPropertyIL(PropertyInfo propertyInfo, ILGenerator generator)
		{
			MethodInfo setMethod = propertyInfo.GetSetMethod(true);
			if (!setMethod.IsStatic)
			{
				generator.PushInstance(propertyInfo.DeclaringType);
			}
			generator.Emit(OpCodes.Ldarg_1);
			generator.UnboxIfNeeded(propertyInfo.PropertyType);
			generator.CallMethod(setMethod);
			generator.Return();
		}
	}
}
