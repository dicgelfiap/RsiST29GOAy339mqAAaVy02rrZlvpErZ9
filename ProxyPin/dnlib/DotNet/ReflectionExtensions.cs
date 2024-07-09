using System;
using System.Reflection;

namespace dnlib.DotNet
{
	// Token: 0x02000836 RID: 2102
	internal static class ReflectionExtensions
	{
		// Token: 0x06004EB0 RID: 20144 RVA: 0x00186978 File Offset: 0x00186978
		public static void GetTypeNamespaceAndName_TypeDefOrRef(this Type type, out string @namespace, out string name)
		{
			name = (type.Name ?? string.Empty);
			if (!type.IsNested)
			{
				@namespace = (type.Namespace ?? string.Empty);
				return;
			}
			string fullName = type.DeclaringType.FullName;
			string fullName2 = type.FullName;
			if (fullName.Length + 1 + name.Length == fullName2.Length)
			{
				@namespace = string.Empty;
				return;
			}
			@namespace = fullName2.Substring(fullName.Length + 1, fullName2.Length - fullName.Length - 1 - name.Length - 1);
		}

		// Token: 0x06004EB1 RID: 20145 RVA: 0x00186A1C File Offset: 0x00186A1C
		public static bool IsSZArray(this Type self)
		{
			if (self == null || !self.IsArray)
			{
				return false;
			}
			PropertyInfo property = self.GetType().GetProperty("IsSzArray", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (property != null)
			{
				return (bool)property.GetValue(self, Array2.Empty<object>());
			}
			return (self.Name ?? string.Empty).EndsWith("[]");
		}

		// Token: 0x06004EB2 RID: 20146 RVA: 0x00186A88 File Offset: 0x00186A88
		public static ElementType GetElementType2(this Type a)
		{
			if (a == null)
			{
				return ElementType.End;
			}
			if (a.IsArray)
			{
				if (!a.IsSZArray())
				{
					return ElementType.Array;
				}
				return ElementType.SZArray;
			}
			else
			{
				if (a.IsByRef)
				{
					return ElementType.ByRef;
				}
				if (a.IsPointer)
				{
					return ElementType.Ptr;
				}
				if (a.IsGenericParameter)
				{
					if (a.DeclaringMethod != null)
					{
						return ElementType.MVar;
					}
					return ElementType.Var;
				}
				else
				{
					if (a.IsGenericType && !a.IsGenericTypeDefinition)
					{
						return ElementType.GenericInst;
					}
					if (a == typeof(void))
					{
						return ElementType.Void;
					}
					if (a == typeof(bool))
					{
						return ElementType.Boolean;
					}
					if (a == typeof(char))
					{
						return ElementType.Char;
					}
					if (a == typeof(sbyte))
					{
						return ElementType.I1;
					}
					if (a == typeof(byte))
					{
						return ElementType.U1;
					}
					if (a == typeof(short))
					{
						return ElementType.I2;
					}
					if (a == typeof(ushort))
					{
						return ElementType.U2;
					}
					if (a == typeof(int))
					{
						return ElementType.I4;
					}
					if (a == typeof(uint))
					{
						return ElementType.U4;
					}
					if (a == typeof(long))
					{
						return ElementType.I8;
					}
					if (a == typeof(ulong))
					{
						return ElementType.U8;
					}
					if (a == typeof(float))
					{
						return ElementType.R4;
					}
					if (a == typeof(double))
					{
						return ElementType.R8;
					}
					if (a == typeof(string))
					{
						return ElementType.String;
					}
					if (a == typeof(TypedReference))
					{
						return ElementType.TypedByRef;
					}
					if (a == typeof(IntPtr))
					{
						return ElementType.I;
					}
					if (a == typeof(UIntPtr))
					{
						return ElementType.U;
					}
					if (a == typeof(object))
					{
						return ElementType.Object;
					}
					if (!a.IsValueType)
					{
						return ElementType.Class;
					}
					return ElementType.ValueType;
				}
			}
		}

		// Token: 0x06004EB3 RID: 20147 RVA: 0x00186CC8 File Offset: 0x00186CC8
		public static bool IsGenericButNotGenericMethodDefinition(this MethodBase mb)
		{
			return mb != null && !mb.IsGenericMethodDefinition && mb.IsGenericMethod;
		}

		// Token: 0x06004EB4 RID: 20148 RVA: 0x00186CE4 File Offset: 0x00186CE4
		internal static bool MustTreatTypeAsGenericInstType(this Type declaringType, Type t)
		{
			return declaringType != null && declaringType.IsGenericTypeDefinition && t == declaringType;
		}

		// Token: 0x06004EB5 RID: 20149 RVA: 0x00186D00 File Offset: 0x00186D00
		public static bool IsTypeDef(this Type type)
		{
			return type != null && !type.HasElementType && (!type.IsGenericType || type.IsGenericTypeDefinition);
		}
	}
}
