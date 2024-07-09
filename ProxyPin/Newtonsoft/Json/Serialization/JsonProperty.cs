using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000AF1 RID: 2801
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonProperty
	{
		// Token: 0x17001717 RID: 5911
		// (get) Token: 0x06006F3C RID: 28476 RVA: 0x002198A0 File Offset: 0x002198A0
		// (set) Token: 0x06006F3D RID: 28477 RVA: 0x002198A8 File Offset: 0x002198A8
		internal JsonContract PropertyContract { get; set; }

		// Token: 0x17001718 RID: 5912
		// (get) Token: 0x06006F3E RID: 28478 RVA: 0x002198B4 File Offset: 0x002198B4
		// (set) Token: 0x06006F3F RID: 28479 RVA: 0x002198BC File Offset: 0x002198BC
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
			set
			{
				this._propertyName = value;
				this._skipPropertyNameEscape = !JavaScriptUtils.ShouldEscapeJavaScriptString(this._propertyName, JavaScriptUtils.HtmlCharEscapeFlags);
			}
		}

		// Token: 0x17001719 RID: 5913
		// (get) Token: 0x06006F40 RID: 28480 RVA: 0x002198E0 File Offset: 0x002198E0
		// (set) Token: 0x06006F41 RID: 28481 RVA: 0x002198E8 File Offset: 0x002198E8
		public Type DeclaringType { get; set; }

		// Token: 0x1700171A RID: 5914
		// (get) Token: 0x06006F42 RID: 28482 RVA: 0x002198F4 File Offset: 0x002198F4
		// (set) Token: 0x06006F43 RID: 28483 RVA: 0x002198FC File Offset: 0x002198FC
		public int? Order { get; set; }

		// Token: 0x1700171B RID: 5915
		// (get) Token: 0x06006F44 RID: 28484 RVA: 0x00219908 File Offset: 0x00219908
		// (set) Token: 0x06006F45 RID: 28485 RVA: 0x00219910 File Offset: 0x00219910
		public string UnderlyingName { get; set; }

		// Token: 0x1700171C RID: 5916
		// (get) Token: 0x06006F46 RID: 28486 RVA: 0x0021991C File Offset: 0x0021991C
		// (set) Token: 0x06006F47 RID: 28487 RVA: 0x00219924 File Offset: 0x00219924
		public IValueProvider ValueProvider { get; set; }

		// Token: 0x1700171D RID: 5917
		// (get) Token: 0x06006F48 RID: 28488 RVA: 0x00219930 File Offset: 0x00219930
		// (set) Token: 0x06006F49 RID: 28489 RVA: 0x00219938 File Offset: 0x00219938
		public IAttributeProvider AttributeProvider { get; set; }

		// Token: 0x1700171E RID: 5918
		// (get) Token: 0x06006F4A RID: 28490 RVA: 0x00219944 File Offset: 0x00219944
		// (set) Token: 0x06006F4B RID: 28491 RVA: 0x0021994C File Offset: 0x0021994C
		public Type PropertyType
		{
			get
			{
				return this._propertyType;
			}
			set
			{
				if (this._propertyType != value)
				{
					this._propertyType = value;
					this._hasGeneratedDefaultValue = false;
				}
			}
		}

		// Token: 0x1700171F RID: 5919
		// (get) Token: 0x06006F4C RID: 28492 RVA: 0x00219970 File Offset: 0x00219970
		// (set) Token: 0x06006F4D RID: 28493 RVA: 0x00219978 File Offset: 0x00219978
		public JsonConverter Converter { get; set; }

		// Token: 0x17001720 RID: 5920
		// (get) Token: 0x06006F4E RID: 28494 RVA: 0x00219984 File Offset: 0x00219984
		// (set) Token: 0x06006F4F RID: 28495 RVA: 0x0021998C File Offset: 0x0021998C
		[Obsolete("MemberConverter is obsolete. Use Converter instead.")]
		public JsonConverter MemberConverter
		{
			get
			{
				return this.Converter;
			}
			set
			{
				this.Converter = value;
			}
		}

		// Token: 0x17001721 RID: 5921
		// (get) Token: 0x06006F50 RID: 28496 RVA: 0x00219998 File Offset: 0x00219998
		// (set) Token: 0x06006F51 RID: 28497 RVA: 0x002199A0 File Offset: 0x002199A0
		public bool Ignored { get; set; }

		// Token: 0x17001722 RID: 5922
		// (get) Token: 0x06006F52 RID: 28498 RVA: 0x002199AC File Offset: 0x002199AC
		// (set) Token: 0x06006F53 RID: 28499 RVA: 0x002199B4 File Offset: 0x002199B4
		public bool Readable { get; set; }

		// Token: 0x17001723 RID: 5923
		// (get) Token: 0x06006F54 RID: 28500 RVA: 0x002199C0 File Offset: 0x002199C0
		// (set) Token: 0x06006F55 RID: 28501 RVA: 0x002199C8 File Offset: 0x002199C8
		public bool Writable { get; set; }

		// Token: 0x17001724 RID: 5924
		// (get) Token: 0x06006F56 RID: 28502 RVA: 0x002199D4 File Offset: 0x002199D4
		// (set) Token: 0x06006F57 RID: 28503 RVA: 0x002199DC File Offset: 0x002199DC
		public bool HasMemberAttribute { get; set; }

		// Token: 0x17001725 RID: 5925
		// (get) Token: 0x06006F58 RID: 28504 RVA: 0x002199E8 File Offset: 0x002199E8
		// (set) Token: 0x06006F59 RID: 28505 RVA: 0x00219A00 File Offset: 0x00219A00
		public object DefaultValue
		{
			get
			{
				if (!this._hasExplicitDefaultValue)
				{
					return null;
				}
				return this._defaultValue;
			}
			set
			{
				this._hasExplicitDefaultValue = true;
				this._defaultValue = value;
			}
		}

		// Token: 0x06006F5A RID: 28506 RVA: 0x00219A10 File Offset: 0x00219A10
		internal object GetResolvedDefaultValue()
		{
			if (this._propertyType == null)
			{
				return null;
			}
			if (!this._hasExplicitDefaultValue && !this._hasGeneratedDefaultValue)
			{
				this._defaultValue = ReflectionUtils.GetDefaultValue(this._propertyType);
				this._hasGeneratedDefaultValue = true;
			}
			return this._defaultValue;
		}

		// Token: 0x17001726 RID: 5926
		// (get) Token: 0x06006F5B RID: 28507 RVA: 0x00219A68 File Offset: 0x00219A68
		// (set) Token: 0x06006F5C RID: 28508 RVA: 0x00219A78 File Offset: 0x00219A78
		public Required Required
		{
			get
			{
				return this._required.GetValueOrDefault();
			}
			set
			{
				this._required = new Required?(value);
			}
		}

		// Token: 0x17001727 RID: 5927
		// (get) Token: 0x06006F5D RID: 28509 RVA: 0x00219A88 File Offset: 0x00219A88
		public bool IsRequiredSpecified
		{
			get
			{
				return this._required != null;
			}
		}

		// Token: 0x17001728 RID: 5928
		// (get) Token: 0x06006F5E RID: 28510 RVA: 0x00219A98 File Offset: 0x00219A98
		// (set) Token: 0x06006F5F RID: 28511 RVA: 0x00219AA0 File Offset: 0x00219AA0
		public bool? IsReference { get; set; }

		// Token: 0x17001729 RID: 5929
		// (get) Token: 0x06006F60 RID: 28512 RVA: 0x00219AAC File Offset: 0x00219AAC
		// (set) Token: 0x06006F61 RID: 28513 RVA: 0x00219AB4 File Offset: 0x00219AB4
		public NullValueHandling? NullValueHandling { get; set; }

		// Token: 0x1700172A RID: 5930
		// (get) Token: 0x06006F62 RID: 28514 RVA: 0x00219AC0 File Offset: 0x00219AC0
		// (set) Token: 0x06006F63 RID: 28515 RVA: 0x00219AC8 File Offset: 0x00219AC8
		public DefaultValueHandling? DefaultValueHandling { get; set; }

		// Token: 0x1700172B RID: 5931
		// (get) Token: 0x06006F64 RID: 28516 RVA: 0x00219AD4 File Offset: 0x00219AD4
		// (set) Token: 0x06006F65 RID: 28517 RVA: 0x00219ADC File Offset: 0x00219ADC
		public ReferenceLoopHandling? ReferenceLoopHandling { get; set; }

		// Token: 0x1700172C RID: 5932
		// (get) Token: 0x06006F66 RID: 28518 RVA: 0x00219AE8 File Offset: 0x00219AE8
		// (set) Token: 0x06006F67 RID: 28519 RVA: 0x00219AF0 File Offset: 0x00219AF0
		public ObjectCreationHandling? ObjectCreationHandling { get; set; }

		// Token: 0x1700172D RID: 5933
		// (get) Token: 0x06006F68 RID: 28520 RVA: 0x00219AFC File Offset: 0x00219AFC
		// (set) Token: 0x06006F69 RID: 28521 RVA: 0x00219B04 File Offset: 0x00219B04
		public TypeNameHandling? TypeNameHandling { get; set; }

		// Token: 0x1700172E RID: 5934
		// (get) Token: 0x06006F6A RID: 28522 RVA: 0x00219B10 File Offset: 0x00219B10
		// (set) Token: 0x06006F6B RID: 28523 RVA: 0x00219B18 File Offset: 0x00219B18
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public Predicate<object> ShouldSerialize { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x1700172F RID: 5935
		// (get) Token: 0x06006F6C RID: 28524 RVA: 0x00219B24 File Offset: 0x00219B24
		// (set) Token: 0x06006F6D RID: 28525 RVA: 0x00219B2C File Offset: 0x00219B2C
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public Predicate<object> ShouldDeserialize { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x17001730 RID: 5936
		// (get) Token: 0x06006F6E RID: 28526 RVA: 0x00219B38 File Offset: 0x00219B38
		// (set) Token: 0x06006F6F RID: 28527 RVA: 0x00219B40 File Offset: 0x00219B40
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public Predicate<object> GetIsSpecified { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x17001731 RID: 5937
		// (get) Token: 0x06006F70 RID: 28528 RVA: 0x00219B4C File Offset: 0x00219B4C
		// (set) Token: 0x06006F71 RID: 28529 RVA: 0x00219B54 File Offset: 0x00219B54
		[Nullable(new byte[]
		{
			2,
			1,
			2
		})]
		public Action<object, object> SetIsSpecified { [return: Nullable(new byte[]
		{
			2,
			1,
			2
		})] get; [param: Nullable(new byte[]
		{
			2,
			1,
			2
		})] set; }

		// Token: 0x06006F72 RID: 28530 RVA: 0x00219B60 File Offset: 0x00219B60
		[NullableContext(1)]
		public override string ToString()
		{
			return this.PropertyName ?? string.Empty;
		}

		// Token: 0x17001732 RID: 5938
		// (get) Token: 0x06006F73 RID: 28531 RVA: 0x00219B74 File Offset: 0x00219B74
		// (set) Token: 0x06006F74 RID: 28532 RVA: 0x00219B7C File Offset: 0x00219B7C
		public JsonConverter ItemConverter { get; set; }

		// Token: 0x17001733 RID: 5939
		// (get) Token: 0x06006F75 RID: 28533 RVA: 0x00219B88 File Offset: 0x00219B88
		// (set) Token: 0x06006F76 RID: 28534 RVA: 0x00219B90 File Offset: 0x00219B90
		public bool? ItemIsReference { get; set; }

		// Token: 0x17001734 RID: 5940
		// (get) Token: 0x06006F77 RID: 28535 RVA: 0x00219B9C File Offset: 0x00219B9C
		// (set) Token: 0x06006F78 RID: 28536 RVA: 0x00219BA4 File Offset: 0x00219BA4
		public TypeNameHandling? ItemTypeNameHandling { get; set; }

		// Token: 0x17001735 RID: 5941
		// (get) Token: 0x06006F79 RID: 28537 RVA: 0x00219BB0 File Offset: 0x00219BB0
		// (set) Token: 0x06006F7A RID: 28538 RVA: 0x00219BB8 File Offset: 0x00219BB8
		public ReferenceLoopHandling? ItemReferenceLoopHandling { get; set; }

		// Token: 0x06006F7B RID: 28539 RVA: 0x00219BC4 File Offset: 0x00219BC4
		[NullableContext(1)]
		internal void WritePropertyName(JsonWriter writer)
		{
			string propertyName = this.PropertyName;
			if (this._skipPropertyNameEscape)
			{
				writer.WritePropertyName(propertyName, false);
				return;
			}
			writer.WritePropertyName(propertyName);
		}

		// Token: 0x04003787 RID: 14215
		internal Required? _required;

		// Token: 0x04003788 RID: 14216
		internal bool _hasExplicitDefaultValue;

		// Token: 0x04003789 RID: 14217
		private object _defaultValue;

		// Token: 0x0400378A RID: 14218
		private bool _hasGeneratedDefaultValue;

		// Token: 0x0400378B RID: 14219
		private string _propertyName;

		// Token: 0x0400378C RID: 14220
		internal bool _skipPropertyNameEscape;

		// Token: 0x0400378D RID: 14221
		private Type _propertyType;
	}
}
