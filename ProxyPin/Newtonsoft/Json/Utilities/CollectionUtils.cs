using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000AA2 RID: 2722
	[NullableContext(1)]
	[Nullable(0)]
	internal static class CollectionUtils
	{
		// Token: 0x06006C5C RID: 27740 RVA: 0x0020B260 File Offset: 0x0020B260
		public static bool IsNullOrEmpty<[Nullable(2)] T>(ICollection<T> collection)
		{
			return collection == null || collection.Count == 0;
		}

		// Token: 0x06006C5D RID: 27741 RVA: 0x0020B274 File Offset: 0x0020B274
		public static void AddRange<[Nullable(2)] T>(this IList<T> initial, IEnumerable<T> collection)
		{
			if (initial == null)
			{
				throw new ArgumentNullException("initial");
			}
			if (collection == null)
			{
				return;
			}
			foreach (T item in collection)
			{
				initial.Add(item);
			}
		}

		// Token: 0x06006C5E RID: 27742 RVA: 0x0020B2E0 File Offset: 0x0020B2E0
		public static bool IsDictionaryType(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			return typeof(IDictionary).IsAssignableFrom(type) || ReflectionUtils.ImplementsGenericDefinition(type, typeof(IDictionary<, >)) || ReflectionUtils.ImplementsGenericDefinition(type, typeof(IReadOnlyDictionary<, >));
		}

		// Token: 0x06006C5F RID: 27743 RVA: 0x0020B344 File Offset: 0x0020B344
		[return: Nullable(2)]
		public static ConstructorInfo ResolveEnumerableCollectionConstructor(Type collectionType, Type collectionItemType)
		{
			Type constructorArgumentType = typeof(IList<>).MakeGenericType(new Type[]
			{
				collectionItemType
			});
			return CollectionUtils.ResolveEnumerableCollectionConstructor(collectionType, collectionItemType, constructorArgumentType);
		}

		// Token: 0x06006C60 RID: 27744 RVA: 0x0020B378 File Offset: 0x0020B378
		[return: Nullable(2)]
		public static ConstructorInfo ResolveEnumerableCollectionConstructor(Type collectionType, Type collectionItemType, Type constructorArgumentType)
		{
			Type left = typeof(IEnumerable<>).MakeGenericType(new Type[]
			{
				collectionItemType
			});
			ConstructorInfo constructorInfo = null;
			foreach (ConstructorInfo constructorInfo2 in collectionType.GetConstructors(BindingFlags.Instance | BindingFlags.Public))
			{
				IList<ParameterInfo> parameters = constructorInfo2.GetParameters();
				if (parameters.Count == 1)
				{
					Type parameterType = parameters[0].ParameterType;
					if (left == parameterType)
					{
						constructorInfo = constructorInfo2;
						break;
					}
					if (constructorInfo == null && parameterType.IsAssignableFrom(constructorArgumentType))
					{
						constructorInfo = constructorInfo2;
					}
				}
			}
			return constructorInfo;
		}

		// Token: 0x06006C61 RID: 27745 RVA: 0x0020B420 File Offset: 0x0020B420
		public static bool AddDistinct<[Nullable(2)] T>(this IList<T> list, T value)
		{
			return list.AddDistinct(value, EqualityComparer<T>.Default);
		}

		// Token: 0x06006C62 RID: 27746 RVA: 0x0020B430 File Offset: 0x0020B430
		public static bool AddDistinct<[Nullable(2)] T>(this IList<T> list, T value, IEqualityComparer<T> comparer)
		{
			if (list.ContainsValue(value, comparer))
			{
				return false;
			}
			list.Add(value);
			return true;
		}

		// Token: 0x06006C63 RID: 27747 RVA: 0x0020B44C File Offset: 0x0020B44C
		public static bool ContainsValue<[Nullable(2)] TSource>(this IEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<TSource>.Default;
			}
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			foreach (TSource x in source)
			{
				if (comparer.Equals(x, value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006C64 RID: 27748 RVA: 0x0020B4CC File Offset: 0x0020B4CC
		public static bool AddRangeDistinct<[Nullable(2)] T>(this IList<T> list, IEnumerable<T> values, IEqualityComparer<T> comparer)
		{
			bool result = true;
			foreach (T value in values)
			{
				if (!list.AddDistinct(value, comparer))
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06006C65 RID: 27749 RVA: 0x0020B528 File Offset: 0x0020B528
		public static int IndexOf<[Nullable(2)] T>(this IEnumerable<T> collection, Func<T, bool> predicate)
		{
			int num = 0;
			foreach (T arg in collection)
			{
				if (predicate(arg))
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x06006C66 RID: 27750 RVA: 0x0020B590 File Offset: 0x0020B590
		public static bool Contains<[Nullable(2)] T>(this List<T> list, T value, IEqualityComparer comparer)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (comparer.Equals(value, list[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006C67 RID: 27751 RVA: 0x0020B5D8 File Offset: 0x0020B5D8
		public static int IndexOfReference<[Nullable(2)] T>(this List<T> list, T item)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (item == list[i])
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06006C68 RID: 27752 RVA: 0x0020B618 File Offset: 0x0020B618
		public static void FastReverse<[Nullable(2)] T>(this List<T> list)
		{
			int i = 0;
			int num = list.Count - 1;
			while (i < num)
			{
				T value = list[i];
				list[i] = list[num];
				list[num] = value;
				i++;
				num--;
			}
		}

		// Token: 0x06006C69 RID: 27753 RVA: 0x0020B664 File Offset: 0x0020B664
		private static IList<int> GetDimensions(IList values, int dimensionsCount)
		{
			IList<int> list = new List<int>();
			IList list2 = values;
			for (;;)
			{
				list.Add(list2.Count);
				if (list.Count == dimensionsCount || list2.Count == 0)
				{
					break;
				}
				IList list3 = list2[0] as IList;
				if (list3 == null)
				{
					break;
				}
				list2 = list3;
			}
			return list;
		}

		// Token: 0x06006C6A RID: 27754 RVA: 0x0020B6B8 File Offset: 0x0020B6B8
		private static void CopyFromJaggedToMultidimensionalArray(IList values, Array multidimensionalArray, int[] indices)
		{
			int num = indices.Length;
			if (num == multidimensionalArray.Rank)
			{
				multidimensionalArray.SetValue(CollectionUtils.JaggedArrayGetValue(values, indices), indices);
				return;
			}
			int length = multidimensionalArray.GetLength(num);
			if (((IList)CollectionUtils.JaggedArrayGetValue(values, indices)).Count != length)
			{
				throw new Exception("Cannot deserialize non-cubical array as multidimensional array.");
			}
			int[] array = new int[num + 1];
			for (int i = 0; i < num; i++)
			{
				array[i] = indices[i];
			}
			for (int j = 0; j < multidimensionalArray.GetLength(num); j++)
			{
				array[num] = j;
				CollectionUtils.CopyFromJaggedToMultidimensionalArray(values, multidimensionalArray, array);
			}
		}

		// Token: 0x06006C6B RID: 27755 RVA: 0x0020B758 File Offset: 0x0020B758
		private static object JaggedArrayGetValue(IList values, int[] indices)
		{
			IList list = values;
			for (int i = 0; i < indices.Length; i++)
			{
				int index = indices[i];
				if (i == indices.Length - 1)
				{
					return list[index];
				}
				list = (IList)list[index];
			}
			return list;
		}

		// Token: 0x06006C6C RID: 27756 RVA: 0x0020B7A4 File Offset: 0x0020B7A4
		public static Array ToMultidimensionalArray(IList values, Type type, int rank)
		{
			IList<int> dimensions = CollectionUtils.GetDimensions(values, rank);
			while (dimensions.Count < rank)
			{
				dimensions.Add(0);
			}
			Array array = Array.CreateInstance(type, dimensions.ToArray<int>());
			CollectionUtils.CopyFromJaggedToMultidimensionalArray(values, array, CollectionUtils.ArrayEmpty<int>());
			return array;
		}

		// Token: 0x06006C6D RID: 27757 RVA: 0x0020B7EC File Offset: 0x0020B7EC
		public static T[] ArrayEmpty<[Nullable(2)] T>()
		{
			return CollectionUtils.EmptyArrayContainer<T>.Empty;
		}

		// Token: 0x020010BE RID: 4286
		[NullableContext(0)]
		private static class EmptyArrayContainer<[Nullable(2)] T>
		{
			// Token: 0x04004828 RID: 18472
			[Nullable(1)]
			public static readonly T[] Empty = new T[0];
		}
	}
}
