using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000ACE RID: 2766
	[NullableContext(1)]
	[Nullable(0)]
	internal static class TypeExtensions
	{
		// Token: 0x06006E19 RID: 28185 RVA: 0x0021529C File Offset: 0x0021529C
		public static MethodInfo Method(this Delegate d)
		{
			return d.Method;
		}

		// Token: 0x06006E1A RID: 28186 RVA: 0x002152A4 File Offset: 0x002152A4
		public static MemberTypes MemberType(this MemberInfo memberInfo)
		{
			return memberInfo.MemberType;
		}

		// Token: 0x06006E1B RID: 28187 RVA: 0x002152AC File Offset: 0x002152AC
		public static bool ContainsGenericParameters(this Type type)
		{
			return type.ContainsGenericParameters;
		}

		// Token: 0x06006E1C RID: 28188 RVA: 0x002152B4 File Offset: 0x002152B4
		public static bool IsInterface(this Type type)
		{
			return type.IsInterface;
		}

		// Token: 0x06006E1D RID: 28189 RVA: 0x002152BC File Offset: 0x002152BC
		public static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x06006E1E RID: 28190 RVA: 0x002152C4 File Offset: 0x002152C4
		public static bool IsGenericTypeDefinition(this Type type)
		{
			return type.IsGenericTypeDefinition;
		}

		// Token: 0x06006E1F RID: 28191 RVA: 0x002152CC File Offset: 0x002152CC
		public static Type BaseType(this Type type)
		{
			return type.BaseType;
		}

		// Token: 0x06006E20 RID: 28192 RVA: 0x002152D4 File Offset: 0x002152D4
		public static Assembly Assembly(this Type type)
		{
			return type.Assembly;
		}

		// Token: 0x06006E21 RID: 28193 RVA: 0x002152DC File Offset: 0x002152DC
		public static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x06006E22 RID: 28194 RVA: 0x002152E4 File Offset: 0x002152E4
		public static bool IsClass(this Type type)
		{
			return type.IsClass;
		}

		// Token: 0x06006E23 RID: 28195 RVA: 0x002152EC File Offset: 0x002152EC
		public static bool IsSealed(this Type type)
		{
			return type.IsSealed;
		}

		// Token: 0x06006E24 RID: 28196 RVA: 0x002152F4 File Offset: 0x002152F4
		public static bool IsAbstract(this Type type)
		{
			return type.IsAbstract;
		}

		// Token: 0x06006E25 RID: 28197 RVA: 0x002152FC File Offset: 0x002152FC
		public static bool IsVisible(this Type type)
		{
			return type.IsVisible;
		}

		// Token: 0x06006E26 RID: 28198 RVA: 0x00215304 File Offset: 0x00215304
		public static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x06006E27 RID: 28199 RVA: 0x0021530C File Offset: 0x0021530C
		public static bool IsPrimitive(this Type type)
		{
			return type.IsPrimitive;
		}

		// Token: 0x06006E28 RID: 28200 RVA: 0x00215314 File Offset: 0x00215314
		public static bool AssignableToTypeName(this Type type, string fullTypeName, bool searchInterfaces, [Nullable(2)] [NotNullWhen(true)] out Type match)
		{
			Type type2 = type;
			while (type2 != null)
			{
				if (string.Equals(type2.FullName, fullTypeName, StringComparison.Ordinal))
				{
					match = type2;
					return true;
				}
				type2 = type2.BaseType();
			}
			if (searchInterfaces)
			{
				Type[] interfaces = type.GetInterfaces();
				for (int i = 0; i < interfaces.Length; i++)
				{
					if (string.Equals(interfaces[i].Name, fullTypeName, StringComparison.Ordinal))
					{
						match = type;
						return true;
					}
				}
			}
			match = null;
			return false;
		}

		// Token: 0x06006E29 RID: 28201 RVA: 0x00215394 File Offset: 0x00215394
		public static bool AssignableToTypeName(this Type type, string fullTypeName, bool searchInterfaces)
		{
			Type type2;
			return type.AssignableToTypeName(fullTypeName, searchInterfaces, out type2);
		}

		// Token: 0x06006E2A RID: 28202 RVA: 0x002153B0 File Offset: 0x002153B0
		public static bool ImplementInterface(this Type type, Type interfaceType)
		{
			Type type2 = type;
			while (type2 != null)
			{
				foreach (Type type3 in ((IEnumerable<Type>)type2.GetInterfaces()))
				{
					if (type3 == interfaceType || (type3 != null && type3.ImplementInterface(interfaceType)))
					{
						return true;
					}
				}
				type2 = type2.BaseType();
			}
			return false;
		}
	}
}
