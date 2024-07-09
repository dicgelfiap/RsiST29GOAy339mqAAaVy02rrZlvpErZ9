using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000A66 RID: 2662
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
	internal sealed class NotNullWhenAttribute : Attribute
	{
		// Token: 0x0600682C RID: 26668 RVA: 0x001FB150 File Offset: 0x001FB150
		public NotNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x170015F9 RID: 5625
		// (get) Token: 0x0600682D RID: 26669 RVA: 0x001FB160 File Offset: 0x001FB160
		public bool ReturnValue { get; }
	}
}
