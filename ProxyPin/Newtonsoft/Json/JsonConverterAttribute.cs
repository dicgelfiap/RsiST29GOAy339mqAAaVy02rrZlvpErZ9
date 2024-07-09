using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000A7B RID: 2683
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Parameter, AllowMultiple = false)]
	public sealed class JsonConverterAttribute : Attribute
	{
		// Token: 0x1700160D RID: 5645
		// (get) Token: 0x060068B2 RID: 26802 RVA: 0x001FC1CC File Offset: 0x001FC1CC
		public Type ConverterType
		{
			get
			{
				return this._converterType;
			}
		}

		// Token: 0x1700160E RID: 5646
		// (get) Token: 0x060068B3 RID: 26803 RVA: 0x001FC1D4 File Offset: 0x001FC1D4
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public object[] ConverterParameters { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; }

		// Token: 0x060068B4 RID: 26804 RVA: 0x001FC1DC File Offset: 0x001FC1DC
		public JsonConverterAttribute(Type converterType)
		{
			if (converterType == null)
			{
				throw new ArgumentNullException("converterType");
			}
			this._converterType = converterType;
		}

		// Token: 0x060068B5 RID: 26805 RVA: 0x001FC204 File Offset: 0x001FC204
		public JsonConverterAttribute(Type converterType, params object[] converterParameters) : this(converterType)
		{
			this.ConverterParameters = converterParameters;
		}

		// Token: 0x0400353C RID: 13628
		private readonly Type _converterType;
	}
}
