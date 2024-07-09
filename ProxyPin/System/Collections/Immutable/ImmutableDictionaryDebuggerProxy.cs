using System;
using System.Collections.Generic;

namespace System.Collections.Immutable
{
	// Token: 0x02000CAB RID: 3243
	internal class ImmutableDictionaryDebuggerProxy<TKey, TValue> : ImmutableEnumerableDebuggerProxy<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x0600824B RID: 33355 RVA: 0x002650A8 File Offset: 0x002650A8
		public ImmutableDictionaryDebuggerProxy(IImmutableDictionary<TKey, TValue> dictionary) : base(dictionary)
		{
		}
	}
}
