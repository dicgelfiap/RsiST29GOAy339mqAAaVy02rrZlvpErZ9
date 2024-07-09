using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000AB1 RID: 2737
	[NullableContext(1)]
	[Nullable(0)]
	internal static class DynamicUtils
	{
		// Token: 0x06006D26 RID: 27942 RVA: 0x002106FC File Offset: 0x002106FC
		public static IEnumerable<string> GetDynamicMemberNames(this IDynamicMetaObjectProvider dynamicProvider)
		{
			return dynamicProvider.GetMetaObject(Expression.Constant(dynamicProvider)).GetDynamicMemberNames();
		}

		// Token: 0x020010D3 RID: 4307
		[Nullable(0)]
		internal static class BinderWrapper
		{
			// Token: 0x06009153 RID: 37203 RVA: 0x002BB6E8 File Offset: 0x002BB6E8
			private static void Init()
			{
				if (!DynamicUtils.BinderWrapper._init)
				{
					if (Type.GetType("Microsoft.CSharp.RuntimeBinder.Binder, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", false) == null)
					{
						throw new InvalidOperationException("Could not resolve type '{0}'. You may need to add a reference to Microsoft.CSharp.dll to work with dynamic types.".FormatWith(CultureInfo.InvariantCulture, "Microsoft.CSharp.RuntimeBinder.Binder, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"));
					}
					DynamicUtils.BinderWrapper._getCSharpArgumentInfoArray = DynamicUtils.BinderWrapper.CreateSharpArgumentInfoArray(new int[1]);
					DynamicUtils.BinderWrapper._setCSharpArgumentInfoArray = DynamicUtils.BinderWrapper.CreateSharpArgumentInfoArray(new int[]
					{
						0,
						3
					});
					DynamicUtils.BinderWrapper.CreateMemberCalls();
					DynamicUtils.BinderWrapper._init = true;
				}
			}

			// Token: 0x06009154 RID: 37204 RVA: 0x002BB764 File Offset: 0x002BB764
			private static object CreateSharpArgumentInfoArray(params int[] values)
			{
				Type type = Type.GetType("Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
				Type type2 = Type.GetType("Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfoFlags, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
				Array array = Array.CreateInstance(type, values.Length);
				for (int i = 0; i < values.Length; i++)
				{
					MethodBase method = type.GetMethod("Create", new Type[]
					{
						type2,
						typeof(string)
					});
					object obj = null;
					object[] array2 = new object[2];
					array2[0] = 0;
					object value = method.Invoke(obj, array2);
					array.SetValue(value, i);
				}
				return array;
			}

			// Token: 0x06009155 RID: 37205 RVA: 0x002BB7EC File Offset: 0x002BB7EC
			private static void CreateMemberCalls()
			{
				Type type = Type.GetType("Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true);
				Type type2 = Type.GetType("Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true);
				Type type3 = Type.GetType("Microsoft.CSharp.RuntimeBinder.Binder, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true);
				Type type4 = typeof(IEnumerable<>).MakeGenericType(new Type[]
				{
					type
				});
				MethodInfo method = type3.GetMethod("GetMember", new Type[]
				{
					type2,
					typeof(string),
					typeof(Type),
					type4
				});
				DynamicUtils.BinderWrapper._getMemberCall = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
				MethodInfo method2 = type3.GetMethod("SetMember", new Type[]
				{
					type2,
					typeof(string),
					typeof(Type),
					type4
				});
				DynamicUtils.BinderWrapper._setMemberCall = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method2);
			}

			// Token: 0x06009156 RID: 37206 RVA: 0x002BB8C8 File Offset: 0x002BB8C8
			public static CallSiteBinder GetMember(string name, Type context)
			{
				DynamicUtils.BinderWrapper.Init();
				return (CallSiteBinder)DynamicUtils.BinderWrapper._getMemberCall(null, new object[]
				{
					0,
					name,
					context,
					DynamicUtils.BinderWrapper._getCSharpArgumentInfoArray
				});
			}

			// Token: 0x06009157 RID: 37207 RVA: 0x002BB900 File Offset: 0x002BB900
			public static CallSiteBinder SetMember(string name, Type context)
			{
				DynamicUtils.BinderWrapper.Init();
				return (CallSiteBinder)DynamicUtils.BinderWrapper._setMemberCall(null, new object[]
				{
					0,
					name,
					context,
					DynamicUtils.BinderWrapper._setCSharpArgumentInfoArray
				});
			}

			// Token: 0x04004856 RID: 18518
			public const string CSharpAssemblyName = "Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x04004857 RID: 18519
			private const string BinderTypeName = "Microsoft.CSharp.RuntimeBinder.Binder, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x04004858 RID: 18520
			private const string CSharpArgumentInfoTypeName = "Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x04004859 RID: 18521
			private const string CSharpArgumentInfoFlagsTypeName = "Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfoFlags, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x0400485A RID: 18522
			private const string CSharpBinderFlagsTypeName = "Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x0400485B RID: 18523
			[Nullable(2)]
			private static object _getCSharpArgumentInfoArray;

			// Token: 0x0400485C RID: 18524
			[Nullable(2)]
			private static object _setCSharpArgumentInfoArray;

			// Token: 0x0400485D RID: 18525
			[Nullable(2)]
			private static MethodCall<object, object> _getMemberCall;

			// Token: 0x0400485E RID: 18526
			[Nullable(2)]
			private static MethodCall<object, object> _setMemberCall;

			// Token: 0x0400485F RID: 18527
			private static bool _init;
		}
	}
}
