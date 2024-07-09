using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace ProtoBuf
{
	// Token: 0x02000C24 RID: 3108
	internal sealed class Helpers
	{
		// Token: 0x06007B8A RID: 31626 RVA: 0x002469AC File Offset: 0x002469AC
		private Helpers()
		{
		}

		// Token: 0x06007B8B RID: 31627 RVA: 0x002469B4 File Offset: 0x002469B4
		public static StringBuilder AppendLine(StringBuilder builder)
		{
			return builder.AppendLine();
		}

		// Token: 0x06007B8C RID: 31628 RVA: 0x002469BC File Offset: 0x002469BC
		[Conditional("DEBUG")]
		public static void DebugWriteLine(string message, object obj)
		{
		}

		// Token: 0x06007B8D RID: 31629 RVA: 0x002469C0 File Offset: 0x002469C0
		[Conditional("DEBUG")]
		public static void DebugWriteLine(string message)
		{
		}

		// Token: 0x06007B8E RID: 31630 RVA: 0x002469C4 File Offset: 0x002469C4
		[Conditional("TRACE")]
		public static void TraceWriteLine(string message)
		{
		}

		// Token: 0x06007B8F RID: 31631 RVA: 0x002469C8 File Offset: 0x002469C8
		[Conditional("DEBUG")]
		public static void DebugAssert(bool condition, string message)
		{
		}

		// Token: 0x06007B90 RID: 31632 RVA: 0x002469CC File Offset: 0x002469CC
		[Conditional("DEBUG")]
		public static void DebugAssert(bool condition, string message, params object[] args)
		{
		}

		// Token: 0x06007B91 RID: 31633 RVA: 0x002469D0 File Offset: 0x002469D0
		[Conditional("DEBUG")]
		public static void DebugAssert(bool condition)
		{
		}

		// Token: 0x06007B92 RID: 31634 RVA: 0x002469D4 File Offset: 0x002469D4
		public static void Sort(int[] keys, object[] values)
		{
			bool flag;
			do
			{
				flag = false;
				for (int i = 1; i < keys.Length; i++)
				{
					if (keys[i - 1] > keys[i])
					{
						int num = keys[i];
						keys[i] = keys[i - 1];
						keys[i - 1] = num;
						object obj = values[i];
						values[i] = values[i - 1];
						values[i - 1] = obj;
						flag = true;
					}
				}
			}
			while (flag);
		}

		// Token: 0x06007B93 RID: 31635 RVA: 0x00246A30 File Offset: 0x00246A30
		internal static MethodInfo GetInstanceMethod(Type declaringType, string name)
		{
			return declaringType.GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06007B94 RID: 31636 RVA: 0x00246A3C File Offset: 0x00246A3C
		internal static MethodInfo GetStaticMethod(Type declaringType, string name)
		{
			return declaringType.GetMethod(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06007B95 RID: 31637 RVA: 0x00246A48 File Offset: 0x00246A48
		internal static MethodInfo GetStaticMethod(Type declaringType, string name, Type[] parameterTypes)
		{
			return declaringType.GetMethod(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, parameterTypes, null);
		}

		// Token: 0x06007B96 RID: 31638 RVA: 0x00246A58 File Offset: 0x00246A58
		internal static MethodInfo GetInstanceMethod(Type declaringType, string name, Type[] types)
		{
			if (types == null)
			{
				types = Helpers.EmptyTypes;
			}
			return declaringType.GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
		}

		// Token: 0x06007B97 RID: 31639 RVA: 0x00246A74 File Offset: 0x00246A74
		internal static bool IsSubclassOf(Type type, Type baseClass)
		{
			return type.IsSubclassOf(baseClass);
		}

		// Token: 0x06007B98 RID: 31640 RVA: 0x00246A80 File Offset: 0x00246A80
		public static ProtoTypeCode GetTypeCode(Type type)
		{
			TypeCode typeCode = Type.GetTypeCode(type);
			switch (typeCode)
			{
			case TypeCode.Empty:
			case TypeCode.Boolean:
			case TypeCode.Char:
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
			case TypeCode.Single:
			case TypeCode.Double:
			case TypeCode.Decimal:
			case TypeCode.DateTime:
			case TypeCode.String:
				return (ProtoTypeCode)typeCode;
			}
			if (type == typeof(TimeSpan))
			{
				return ProtoTypeCode.TimeSpan;
			}
			if (type == typeof(Guid))
			{
				return ProtoTypeCode.Guid;
			}
			if (type == typeof(Uri))
			{
				return ProtoTypeCode.Uri;
			}
			if (type == typeof(byte[]))
			{
				return ProtoTypeCode.ByteArray;
			}
			if (type == typeof(Type))
			{
				return ProtoTypeCode.Type;
			}
			return ProtoTypeCode.Unknown;
		}

		// Token: 0x06007B99 RID: 31641 RVA: 0x00246B6C File Offset: 0x00246B6C
		internal static Type GetUnderlyingType(Type type)
		{
			return Nullable.GetUnderlyingType(type);
		}

		// Token: 0x06007B9A RID: 31642 RVA: 0x00246B74 File Offset: 0x00246B74
		internal static bool IsValueType(Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x06007B9B RID: 31643 RVA: 0x00246B7C File Offset: 0x00246B7C
		internal static bool IsSealed(Type type)
		{
			return type.IsSealed;
		}

		// Token: 0x06007B9C RID: 31644 RVA: 0x00246B84 File Offset: 0x00246B84
		internal static bool IsClass(Type type)
		{
			return type.IsClass;
		}

		// Token: 0x06007B9D RID: 31645 RVA: 0x00246B8C File Offset: 0x00246B8C
		internal static bool IsEnum(Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x06007B9E RID: 31646 RVA: 0x00246B94 File Offset: 0x00246B94
		internal static MethodInfo GetGetMethod(PropertyInfo property, bool nonPublic, bool allowInternal)
		{
			if (property == null)
			{
				return null;
			}
			MethodInfo methodInfo = property.GetGetMethod(nonPublic);
			if (methodInfo == null && !nonPublic && allowInternal)
			{
				methodInfo = property.GetGetMethod(true);
				if (methodInfo == null && !methodInfo.IsAssembly && !methodInfo.IsFamilyOrAssembly)
				{
					methodInfo = null;
				}
			}
			return methodInfo;
		}

		// Token: 0x06007B9F RID: 31647 RVA: 0x00246C08 File Offset: 0x00246C08
		internal static MethodInfo GetSetMethod(PropertyInfo property, bool nonPublic, bool allowInternal)
		{
			if (property == null)
			{
				return null;
			}
			MethodInfo methodInfo = property.GetSetMethod(nonPublic);
			if (methodInfo == null && !nonPublic && allowInternal)
			{
				methodInfo = property.GetGetMethod(true);
				if (methodInfo == null && !methodInfo.IsAssembly && !methodInfo.IsFamilyOrAssembly)
				{
					methodInfo = null;
				}
			}
			return methodInfo;
		}

		// Token: 0x06007BA0 RID: 31648 RVA: 0x00246C7C File Offset: 0x00246C7C
		internal static ConstructorInfo GetConstructor(Type type, Type[] parameterTypes, bool nonPublic)
		{
			return type.GetConstructor(nonPublic ? (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : (BindingFlags.Instance | BindingFlags.Public), null, parameterTypes, null);
		}

		// Token: 0x06007BA1 RID: 31649 RVA: 0x00246C98 File Offset: 0x00246C98
		internal static ConstructorInfo[] GetConstructors(Type type, bool nonPublic)
		{
			return type.GetConstructors(nonPublic ? (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : (BindingFlags.Instance | BindingFlags.Public));
		}

		// Token: 0x06007BA2 RID: 31650 RVA: 0x00246CB0 File Offset: 0x00246CB0
		internal static PropertyInfo GetProperty(Type type, string name, bool nonPublic)
		{
			return type.GetProperty(name, nonPublic ? (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : (BindingFlags.Instance | BindingFlags.Public));
		}

		// Token: 0x06007BA3 RID: 31651 RVA: 0x00246CC8 File Offset: 0x00246CC8
		internal static object ParseEnum(Type type, string value)
		{
			return Enum.Parse(type, value, true);
		}

		// Token: 0x06007BA4 RID: 31652 RVA: 0x00246CD4 File Offset: 0x00246CD4
		internal static MemberInfo[] GetInstanceFieldsAndProperties(Type type, bool publicOnly)
		{
			BindingFlags bindingAttr = publicOnly ? (BindingFlags.Instance | BindingFlags.Public) : (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			PropertyInfo[] properties = type.GetProperties(bindingAttr);
			FieldInfo[] fields = type.GetFields(bindingAttr);
			MemberInfo[] array = new MemberInfo[fields.Length + properties.Length];
			properties.CopyTo(array, 0);
			fields.CopyTo(array, properties.Length);
			return array;
		}

		// Token: 0x06007BA5 RID: 31653 RVA: 0x00246D28 File Offset: 0x00246D28
		internal static Type GetMemberType(MemberInfo member)
		{
			MemberTypes memberType = member.MemberType;
			if (memberType == MemberTypes.Field)
			{
				return ((FieldInfo)member).FieldType;
			}
			if (memberType != MemberTypes.Property)
			{
				return null;
			}
			return ((PropertyInfo)member).PropertyType;
		}

		// Token: 0x06007BA6 RID: 31654 RVA: 0x00246D70 File Offset: 0x00246D70
		internal static bool IsAssignableFrom(Type target, Type type)
		{
			return target.IsAssignableFrom(type);
		}

		// Token: 0x06007BA7 RID: 31655 RVA: 0x00246D7C File Offset: 0x00246D7C
		internal static Assembly GetAssembly(Type type)
		{
			return type.Assembly;
		}

		// Token: 0x06007BA8 RID: 31656 RVA: 0x00246D84 File Offset: 0x00246D84
		internal static byte[] GetBuffer(MemoryStream ms)
		{
			return ms.GetBuffer();
		}

		// Token: 0x04003BA6 RID: 15270
		public static readonly Type[] EmptyTypes = Type.EmptyTypes;
	}
}
