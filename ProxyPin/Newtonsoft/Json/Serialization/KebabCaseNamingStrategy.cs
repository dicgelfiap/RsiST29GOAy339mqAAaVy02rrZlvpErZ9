using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000AF9 RID: 2809
	public class KebabCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x0600703A RID: 28730 RVA: 0x00220A40 File Offset: 0x00220A40
		public KebabCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x0600703B RID: 28731 RVA: 0x00220A58 File Offset: 0x00220A58
		public KebabCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames) : this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x0600703C RID: 28732 RVA: 0x00220A6C File Offset: 0x00220A6C
		public KebabCaseNamingStrategy()
		{
		}

		// Token: 0x0600703D RID: 28733 RVA: 0x00220A74 File Offset: 0x00220A74
		[NullableContext(1)]
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToKebabCase(name);
		}
	}
}
