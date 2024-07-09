using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000AD1 RID: 2769
	public class CamelCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x06006E2E RID: 28206 RVA: 0x0021547C File Offset: 0x0021547C
		public CamelCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x06006E2F RID: 28207 RVA: 0x00215494 File Offset: 0x00215494
		public CamelCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames) : this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x06006E30 RID: 28208 RVA: 0x002154A8 File Offset: 0x002154A8
		public CamelCaseNamingStrategy()
		{
		}

		// Token: 0x06006E31 RID: 28209 RVA: 0x002154B0 File Offset: 0x002154B0
		[NullableContext(1)]
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToCamelCase(name);
		}
	}
}
