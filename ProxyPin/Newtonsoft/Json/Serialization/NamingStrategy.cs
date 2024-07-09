using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000AFB RID: 2811
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class NamingStrategy
	{
		// Token: 0x17001759 RID: 5977
		// (get) Token: 0x06007044 RID: 28740 RVA: 0x00220C40 File Offset: 0x00220C40
		// (set) Token: 0x06007045 RID: 28741 RVA: 0x00220C48 File Offset: 0x00220C48
		public bool ProcessDictionaryKeys { get; set; }

		// Token: 0x1700175A RID: 5978
		// (get) Token: 0x06007046 RID: 28742 RVA: 0x00220C54 File Offset: 0x00220C54
		// (set) Token: 0x06007047 RID: 28743 RVA: 0x00220C5C File Offset: 0x00220C5C
		public bool ProcessExtensionDataNames { get; set; }

		// Token: 0x1700175B RID: 5979
		// (get) Token: 0x06007048 RID: 28744 RVA: 0x00220C68 File Offset: 0x00220C68
		// (set) Token: 0x06007049 RID: 28745 RVA: 0x00220C70 File Offset: 0x00220C70
		public bool OverrideSpecifiedNames { get; set; }

		// Token: 0x0600704A RID: 28746 RVA: 0x00220C7C File Offset: 0x00220C7C
		public virtual string GetPropertyName(string name, bool hasSpecifiedName)
		{
			if (hasSpecifiedName && !this.OverrideSpecifiedNames)
			{
				return name;
			}
			return this.ResolvePropertyName(name);
		}

		// Token: 0x0600704B RID: 28747 RVA: 0x00220C98 File Offset: 0x00220C98
		public virtual string GetExtensionDataName(string name)
		{
			if (!this.ProcessExtensionDataNames)
			{
				return name;
			}
			return this.ResolvePropertyName(name);
		}

		// Token: 0x0600704C RID: 28748 RVA: 0x00220CB0 File Offset: 0x00220CB0
		public virtual string GetDictionaryKey(string key)
		{
			if (!this.ProcessDictionaryKeys)
			{
				return key;
			}
			return this.ResolvePropertyName(key);
		}

		// Token: 0x0600704D RID: 28749
		protected abstract string ResolvePropertyName(string name);

		// Token: 0x0600704E RID: 28750 RVA: 0x00220CC8 File Offset: 0x00220CC8
		public override int GetHashCode()
		{
			return ((base.GetType().GetHashCode() * 397 ^ this.ProcessDictionaryKeys.GetHashCode()) * 397 ^ this.ProcessExtensionDataNames.GetHashCode()) * 397 ^ this.OverrideSpecifiedNames.GetHashCode();
		}

		// Token: 0x0600704F RID: 28751 RVA: 0x00220D24 File Offset: 0x00220D24
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NamingStrategy);
		}

		// Token: 0x06007050 RID: 28752 RVA: 0x00220D34 File Offset: 0x00220D34
		[NullableContext(2)]
		protected bool Equals(NamingStrategy other)
		{
			return other != null && (base.GetType() == other.GetType() && this.ProcessDictionaryKeys == other.ProcessDictionaryKeys && this.ProcessExtensionDataNames == other.ProcessExtensionDataNames) && this.OverrideSpecifiedNames == other.OverrideSpecifiedNames;
		}
	}
}
