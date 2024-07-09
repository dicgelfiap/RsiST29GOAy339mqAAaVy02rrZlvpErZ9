using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000B01 RID: 2817
	public class SnakeCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x06007060 RID: 28768 RVA: 0x00220F18 File Offset: 0x00220F18
		public SnakeCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x06007061 RID: 28769 RVA: 0x00220F30 File Offset: 0x00220F30
		public SnakeCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames) : this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x06007062 RID: 28770 RVA: 0x00220F44 File Offset: 0x00220F44
		public SnakeCaseNamingStrategy()
		{
		}

		// Token: 0x06007063 RID: 28771 RVA: 0x00220F4C File Offset: 0x00220F4C
		[NullableContext(1)]
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToSnakeCase(name);
		}
	}
}
