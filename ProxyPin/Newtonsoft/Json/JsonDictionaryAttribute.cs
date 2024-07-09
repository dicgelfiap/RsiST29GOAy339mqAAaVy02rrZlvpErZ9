using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000A7D RID: 2685
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class JsonDictionaryAttribute : JsonContainerAttribute
	{
		// Token: 0x060068B7 RID: 26807 RVA: 0x001FC21C File Offset: 0x001FC21C
		public JsonDictionaryAttribute()
		{
		}

		// Token: 0x060068B8 RID: 26808 RVA: 0x001FC224 File Offset: 0x001FC224
		[NullableContext(1)]
		public JsonDictionaryAttribute(string id) : base(id)
		{
		}
	}
}
