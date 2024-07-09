using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000AB9 RID: 2745
	[NullableContext(1)]
	[Nullable(0)]
	internal class FSharpUtils
	{
		// Token: 0x06006D48 RID: 27976 RVA: 0x00211828 File Offset: 0x00211828
		private FSharpUtils(Assembly fsharpCoreAssembly)
		{
			this.FSharpCoreAssembly = fsharpCoreAssembly;
			Type type = fsharpCoreAssembly.GetType("Microsoft.FSharp.Reflection.FSharpType");
			MethodInfo methodWithNonPublicFallback = FSharpUtils.GetMethodWithNonPublicFallback(type, "IsUnion", BindingFlags.Static | BindingFlags.Public);
			this.IsUnion = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback);
			MethodInfo methodWithNonPublicFallback2 = FSharpUtils.GetMethodWithNonPublicFallback(type, "GetUnionCases", BindingFlags.Static | BindingFlags.Public);
			this.GetUnionCases = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback2);
			Type type2 = fsharpCoreAssembly.GetType("Microsoft.FSharp.Reflection.FSharpValue");
			this.PreComputeUnionTagReader = FSharpUtils.CreateFSharpFuncCall(type2, "PreComputeUnionTagReader");
			this.PreComputeUnionReader = FSharpUtils.CreateFSharpFuncCall(type2, "PreComputeUnionReader");
			this.PreComputeUnionConstructor = FSharpUtils.CreateFSharpFuncCall(type2, "PreComputeUnionConstructor");
			Type type3 = fsharpCoreAssembly.GetType("Microsoft.FSharp.Reflection.UnionCaseInfo");
			this.GetUnionCaseInfoName = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty("Name"));
			this.GetUnionCaseInfoTag = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty("Tag"));
			this.GetUnionCaseInfoDeclaringType = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty("DeclaringType"));
			this.GetUnionCaseInfoFields = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(type3.GetMethod("GetFields"));
			Type type4 = fsharpCoreAssembly.GetType("Microsoft.FSharp.Collections.ListModule");
			this._ofSeq = type4.GetMethod("OfSeq");
			this._mapType = fsharpCoreAssembly.GetType("Microsoft.FSharp.Collections.FSharpMap`2");
		}

		// Token: 0x170016B7 RID: 5815
		// (get) Token: 0x06006D49 RID: 27977 RVA: 0x00211978 File Offset: 0x00211978
		public static FSharpUtils Instance
		{
			get
			{
				return FSharpUtils._instance;
			}
		}

		// Token: 0x170016B8 RID: 5816
		// (get) Token: 0x06006D4A RID: 27978 RVA: 0x00211980 File Offset: 0x00211980
		// (set) Token: 0x06006D4B RID: 27979 RVA: 0x00211988 File Offset: 0x00211988
		public Assembly FSharpCoreAssembly { get; private set; }

		// Token: 0x170016B9 RID: 5817
		// (get) Token: 0x06006D4C RID: 27980 RVA: 0x00211994 File Offset: 0x00211994
		// (set) Token: 0x06006D4D RID: 27981 RVA: 0x0021199C File Offset: 0x0021199C
		[Nullable(new byte[]
		{
			1,
			2,
			1
		})]
		public MethodCall<object, object> IsUnion { [return: Nullable(new byte[]
		{
			1,
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			1,
			2,
			1
		})] private set; }

		// Token: 0x170016BA RID: 5818
		// (get) Token: 0x06006D4E RID: 27982 RVA: 0x002119A8 File Offset: 0x002119A8
		// (set) Token: 0x06006D4F RID: 27983 RVA: 0x002119B0 File Offset: 0x002119B0
		[Nullable(new byte[]
		{
			1,
			2,
			1
		})]
		public MethodCall<object, object> GetUnionCases { [return: Nullable(new byte[]
		{
			1,
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			1,
			2,
			1
		})] private set; }

		// Token: 0x170016BB RID: 5819
		// (get) Token: 0x06006D50 RID: 27984 RVA: 0x002119BC File Offset: 0x002119BC
		// (set) Token: 0x06006D51 RID: 27985 RVA: 0x002119C4 File Offset: 0x002119C4
		[Nullable(new byte[]
		{
			1,
			2,
			1
		})]
		public MethodCall<object, object> PreComputeUnionTagReader { [return: Nullable(new byte[]
		{
			1,
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			1,
			2,
			1
		})] private set; }

		// Token: 0x170016BC RID: 5820
		// (get) Token: 0x06006D52 RID: 27986 RVA: 0x002119D0 File Offset: 0x002119D0
		// (set) Token: 0x06006D53 RID: 27987 RVA: 0x002119D8 File Offset: 0x002119D8
		[Nullable(new byte[]
		{
			1,
			2,
			1
		})]
		public MethodCall<object, object> PreComputeUnionReader { [return: Nullable(new byte[]
		{
			1,
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			1,
			2,
			1
		})] private set; }

		// Token: 0x170016BD RID: 5821
		// (get) Token: 0x06006D54 RID: 27988 RVA: 0x002119E4 File Offset: 0x002119E4
		// (set) Token: 0x06006D55 RID: 27989 RVA: 0x002119EC File Offset: 0x002119EC
		[Nullable(new byte[]
		{
			1,
			2,
			1
		})]
		public MethodCall<object, object> PreComputeUnionConstructor { [return: Nullable(new byte[]
		{
			1,
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			1,
			2,
			1
		})] private set; }

		// Token: 0x170016BE RID: 5822
		// (get) Token: 0x06006D56 RID: 27990 RVA: 0x002119F8 File Offset: 0x002119F8
		// (set) Token: 0x06006D57 RID: 27991 RVA: 0x00211A00 File Offset: 0x00211A00
		public Func<object, object> GetUnionCaseInfoDeclaringType { get; private set; }

		// Token: 0x170016BF RID: 5823
		// (get) Token: 0x06006D58 RID: 27992 RVA: 0x00211A0C File Offset: 0x00211A0C
		// (set) Token: 0x06006D59 RID: 27993 RVA: 0x00211A14 File Offset: 0x00211A14
		public Func<object, object> GetUnionCaseInfoName { get; private set; }

		// Token: 0x170016C0 RID: 5824
		// (get) Token: 0x06006D5A RID: 27994 RVA: 0x00211A20 File Offset: 0x00211A20
		// (set) Token: 0x06006D5B RID: 27995 RVA: 0x00211A28 File Offset: 0x00211A28
		public Func<object, object> GetUnionCaseInfoTag { get; private set; }

		// Token: 0x170016C1 RID: 5825
		// (get) Token: 0x06006D5C RID: 27996 RVA: 0x00211A34 File Offset: 0x00211A34
		// (set) Token: 0x06006D5D RID: 27997 RVA: 0x00211A3C File Offset: 0x00211A3C
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public MethodCall<object, object> GetUnionCaseInfoFields { [return: Nullable(new byte[]
		{
			1,
			1,
			2
		})] get; [param: Nullable(new byte[]
		{
			1,
			1,
			2
		})] private set; }

		// Token: 0x06006D5E RID: 27998 RVA: 0x00211A48 File Offset: 0x00211A48
		public static void EnsureInitialized(Assembly fsharpCoreAssembly)
		{
			if (FSharpUtils._instance == null)
			{
				object @lock = FSharpUtils.Lock;
				lock (@lock)
				{
					if (FSharpUtils._instance == null)
					{
						FSharpUtils._instance = new FSharpUtils(fsharpCoreAssembly);
					}
				}
			}
		}

		// Token: 0x06006D5F RID: 27999 RVA: 0x00211AA8 File Offset: 0x00211AA8
		private static MethodInfo GetMethodWithNonPublicFallback(Type type, string methodName, BindingFlags bindingFlags)
		{
			MethodInfo method = type.GetMethod(methodName, bindingFlags);
			if (method == null && (bindingFlags & BindingFlags.NonPublic) != BindingFlags.NonPublic)
			{
				method = type.GetMethod(methodName, bindingFlags | BindingFlags.NonPublic);
			}
			return method;
		}

		// Token: 0x06006D60 RID: 28000 RVA: 0x00211AE8 File Offset: 0x00211AE8
		[return: Nullable(new byte[]
		{
			1,
			2,
			1
		})]
		private static MethodCall<object, object> CreateFSharpFuncCall(Type type, string methodName)
		{
			MethodInfo methodWithNonPublicFallback = FSharpUtils.GetMethodWithNonPublicFallback(type, methodName, BindingFlags.Static | BindingFlags.Public);
			MethodInfo method = methodWithNonPublicFallback.ReturnType.GetMethod("Invoke", BindingFlags.Instance | BindingFlags.Public);
			MethodCall<object, object> call = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback);
			MethodCall<object, object> invoke = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
			return ([Nullable(2)] object target, [Nullable(new byte[]
			{
				1,
				2
			})] object[] args) => new FSharpFunction(call(target, args), invoke);
		}

		// Token: 0x06006D61 RID: 28001 RVA: 0x00211B48 File Offset: 0x00211B48
		public ObjectConstructor<object> CreateSeq(Type t)
		{
			MethodInfo method = this._ofSeq.MakeGenericMethod(new Type[]
			{
				t
			});
			return JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(method);
		}

		// Token: 0x06006D62 RID: 28002 RVA: 0x00211B7C File Offset: 0x00211B7C
		public ObjectConstructor<object> CreateMap(Type keyType, Type valueType)
		{
			return (ObjectConstructor<object>)typeof(FSharpUtils).GetMethod("BuildMapCreator").MakeGenericMethod(new Type[]
			{
				keyType,
				valueType
			}).Invoke(this, null);
		}

		// Token: 0x06006D63 RID: 28003 RVA: 0x00211BC0 File Offset: 0x00211BC0
		[NullableContext(2)]
		[return: Nullable(1)]
		public ObjectConstructor<object> BuildMapCreator<TKey, TValue>()
		{
			ConstructorInfo constructor = this._mapType.MakeGenericType(new Type[]
			{
				typeof(TKey),
				typeof(TValue)
			}).GetConstructor(new Type[]
			{
				typeof(IEnumerable<Tuple<TKey, TValue>>)
			});
			ObjectConstructor<object> ctorDelegate = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor);
			return delegate([Nullable(new byte[]
			{
				1,
				2
			})] object[] args)
			{
				IEnumerable<Tuple<TKey, TValue>> enumerable = from kv in (IEnumerable<KeyValuePair<TKey, TValue>>)args[0]
				select new Tuple<TKey, TValue>(kv.Key, kv.Value);
				return ctorDelegate(new object[]
				{
					enumerable
				});
			};
		}

		// Token: 0x040036CC RID: 14028
		private static readonly object Lock = new object();

		// Token: 0x040036CD RID: 14029
		[Nullable(2)]
		private static FSharpUtils _instance;

		// Token: 0x040036CE RID: 14030
		private MethodInfo _ofSeq;

		// Token: 0x040036CF RID: 14031
		private Type _mapType;

		// Token: 0x040036DA RID: 14042
		public const string FSharpSetTypeName = "FSharpSet`1";

		// Token: 0x040036DB RID: 14043
		public const string FSharpListTypeName = "FSharpList`1";

		// Token: 0x040036DC RID: 14044
		public const string FSharpMapTypeName = "FSharpMap`2";
	}
}
