using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000ABF RID: 2751
	[NullableContext(1)]
	[Nullable(0)]
	internal class LateBoundReflectionDelegateFactory : ReflectionDelegateFactory
	{
		// Token: 0x170016C2 RID: 5826
		// (get) Token: 0x06006D82 RID: 28034 RVA: 0x00212B98 File Offset: 0x00212B98
		internal static ReflectionDelegateFactory Instance
		{
			get
			{
				return LateBoundReflectionDelegateFactory._instance;
			}
		}

		// Token: 0x06006D83 RID: 28035 RVA: 0x00212BA0 File Offset: 0x00212BA0
		public override ObjectConstructor<object> CreateParameterizedConstructor(MethodBase method)
		{
			ValidationUtils.ArgumentNotNull(method, "method");
			ConstructorInfo c = method as ConstructorInfo;
			if (c != null)
			{
				return ([Nullable(new byte[]
				{
					1,
					2
				})] object[] a) => c.Invoke(a);
			}
			return ([Nullable(new byte[]
			{
				1,
				2
			})] object[] a) => method.Invoke(null, a);
		}

		// Token: 0x06006D84 RID: 28036 RVA: 0x00212C04 File Offset: 0x00212C04
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public override MethodCall<T, object> CreateMethodCall<[Nullable(2)] T>(MethodBase method)
		{
			ValidationUtils.ArgumentNotNull(method, "method");
			ConstructorInfo c = method as ConstructorInfo;
			if (c != null)
			{
				return (T o, [Nullable(new byte[]
				{
					1,
					2
				})] object[] a) => c.Invoke(a);
			}
			return (T o, [Nullable(new byte[]
			{
				1,
				2
			})] object[] a) => method.Invoke(o, a);
		}

		// Token: 0x06006D85 RID: 28037 RVA: 0x00212C68 File Offset: 0x00212C68
		public override Func<T> CreateDefaultConstructor<[Nullable(2)] T>(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			if (type.IsValueType())
			{
				return () => (T)((object)Activator.CreateInstance(type));
			}
			ConstructorInfo constructorInfo = ReflectionUtils.GetDefaultConstructor(type, true);
			return () => (T)((object)constructorInfo.Invoke(null));
		}

		// Token: 0x06006D86 RID: 28038 RVA: 0x00212CD4 File Offset: 0x00212CD4
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public override Func<T, object> CreateGet<[Nullable(2)] T>(PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			return (T o) => propertyInfo.GetValue(o, null);
		}

		// Token: 0x06006D87 RID: 28039 RVA: 0x00212D00 File Offset: 0x00212D00
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public override Func<T, object> CreateGet<[Nullable(2)] T>(FieldInfo fieldInfo)
		{
			ValidationUtils.ArgumentNotNull(fieldInfo, "fieldInfo");
			return (T o) => fieldInfo.GetValue(o);
		}

		// Token: 0x06006D88 RID: 28040 RVA: 0x00212D2C File Offset: 0x00212D2C
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public override Action<T, object> CreateSet<[Nullable(2)] T>(FieldInfo fieldInfo)
		{
			ValidationUtils.ArgumentNotNull(fieldInfo, "fieldInfo");
			return delegate(T o, [Nullable(2)] object v)
			{
				fieldInfo.SetValue(o, v);
			};
		}

		// Token: 0x06006D89 RID: 28041 RVA: 0x00212D58 File Offset: 0x00212D58
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public override Action<T, object> CreateSet<[Nullable(2)] T>(PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			return delegate(T o, [Nullable(2)] object v)
			{
				propertyInfo.SetValue(o, v, null);
			};
		}

		// Token: 0x040036F9 RID: 14073
		private static readonly LateBoundReflectionDelegateFactory _instance = new LateBoundReflectionDelegateFactory();
	}
}
