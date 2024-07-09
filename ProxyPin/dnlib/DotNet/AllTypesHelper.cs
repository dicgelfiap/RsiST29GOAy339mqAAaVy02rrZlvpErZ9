using System;
using System.Collections.Generic;

namespace dnlib.DotNet
{
	// Token: 0x02000773 RID: 1907
	internal readonly struct AllTypesHelper
	{
		// Token: 0x060042E1 RID: 17121 RVA: 0x00166708 File Offset: 0x00166708
		public static IEnumerable<TypeDef> Types(IEnumerable<TypeDef> types)
		{
			Dictionary<TypeDef, bool> visited = new Dictionary<TypeDef, bool>();
			Stack<IEnumerator<TypeDef>> stack = new Stack<IEnumerator<TypeDef>>();
			if (types != null)
			{
				stack.Push(types.GetEnumerator());
			}
			while (stack.Count > 0)
			{
				IEnumerator<TypeDef> enumerator = stack.Pop();
				while (enumerator.MoveNext())
				{
					TypeDef type = enumerator.Current;
					if (!visited.ContainsKey(type))
					{
						visited[type] = true;
						yield return type;
						if (type.NestedTypes.Count > 0)
						{
							stack.Push(enumerator);
							enumerator = type.NestedTypes.GetEnumerator();
						}
						type = null;
					}
				}
				enumerator = null;
			}
			yield break;
		}
	}
}
