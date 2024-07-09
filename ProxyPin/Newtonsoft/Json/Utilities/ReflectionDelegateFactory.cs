using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000AC4 RID: 2756
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class ReflectionDelegateFactory
	{
		// Token: 0x06006DA3 RID: 28067 RVA: 0x002132A4 File Offset: 0x002132A4
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public Func<T, object> CreateGet<[Nullable(2)] T>(MemberInfo memberInfo)
		{
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			if (propertyInfo != null)
			{
				if (propertyInfo.PropertyType.IsByRef)
				{
					throw new InvalidOperationException("Could not create getter for {0}. ByRef return values are not supported.".FormatWith(CultureInfo.InvariantCulture, propertyInfo));
				}
				return this.CreateGet<T>(propertyInfo);
			}
			else
			{
				FieldInfo fieldInfo = memberInfo as FieldInfo;
				if (fieldInfo != null)
				{
					return this.CreateGet<T>(fieldInfo);
				}
				throw new Exception("Could not create getter for {0}.".FormatWith(CultureInfo.InvariantCulture, memberInfo));
			}
		}

		// Token: 0x06006DA4 RID: 28068 RVA: 0x0021331C File Offset: 0x0021331C
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public Action<T, object> CreateSet<[Nullable(2)] T>(MemberInfo memberInfo)
		{
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			if (propertyInfo != null)
			{
				return this.CreateSet<T>(propertyInfo);
			}
			FieldInfo fieldInfo = memberInfo as FieldInfo;
			if (fieldInfo != null)
			{
				return this.CreateSet<T>(fieldInfo);
			}
			throw new Exception("Could not create setter for {0}.".FormatWith(CultureInfo.InvariantCulture, memberInfo));
		}

		// Token: 0x06006DA5 RID: 28069
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public abstract MethodCall<T, object> CreateMethodCall<[Nullable(2)] T>(MethodBase method);

		// Token: 0x06006DA6 RID: 28070
		public abstract ObjectConstructor<object> CreateParameterizedConstructor(MethodBase method);

		// Token: 0x06006DA7 RID: 28071
		public abstract Func<T> CreateDefaultConstructor<[Nullable(2)] T>(Type type);

		// Token: 0x06006DA8 RID: 28072
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public abstract Func<T, object> CreateGet<[Nullable(2)] T>(PropertyInfo propertyInfo);

		// Token: 0x06006DA9 RID: 28073
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public abstract Func<T, object> CreateGet<[Nullable(2)] T>(FieldInfo fieldInfo);

		// Token: 0x06006DAA RID: 28074
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public abstract Action<T, object> CreateSet<[Nullable(2)] T>(FieldInfo fieldInfo);

		// Token: 0x06006DAB RID: 28075
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public abstract Action<T, object> CreateSet<[Nullable(2)] T>(PropertyInfo propertyInfo);
	}
}
