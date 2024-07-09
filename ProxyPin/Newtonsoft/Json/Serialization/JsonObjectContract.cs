using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000AEF RID: 2799
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonObjectContract : JsonContainerContract
	{
		// Token: 0x17001709 RID: 5897
		// (get) Token: 0x06006F1F RID: 28447 RVA: 0x002194B4 File Offset: 0x002194B4
		// (set) Token: 0x06006F20 RID: 28448 RVA: 0x002194BC File Offset: 0x002194BC
		public MemberSerialization MemberSerialization { get; set; }

		// Token: 0x1700170A RID: 5898
		// (get) Token: 0x06006F21 RID: 28449 RVA: 0x002194C8 File Offset: 0x002194C8
		// (set) Token: 0x06006F22 RID: 28450 RVA: 0x002194D0 File Offset: 0x002194D0
		public MissingMemberHandling? MissingMemberHandling { get; set; }

		// Token: 0x1700170B RID: 5899
		// (get) Token: 0x06006F23 RID: 28451 RVA: 0x002194DC File Offset: 0x002194DC
		// (set) Token: 0x06006F24 RID: 28452 RVA: 0x002194E4 File Offset: 0x002194E4
		public Required? ItemRequired { get; set; }

		// Token: 0x1700170C RID: 5900
		// (get) Token: 0x06006F25 RID: 28453 RVA: 0x002194F0 File Offset: 0x002194F0
		// (set) Token: 0x06006F26 RID: 28454 RVA: 0x002194F8 File Offset: 0x002194F8
		public NullValueHandling? ItemNullValueHandling { get; set; }

		// Token: 0x1700170D RID: 5901
		// (get) Token: 0x06006F27 RID: 28455 RVA: 0x00219504 File Offset: 0x00219504
		[Nullable(1)]
		public JsonPropertyCollection Properties { [NullableContext(1)] get; }

		// Token: 0x1700170E RID: 5902
		// (get) Token: 0x06006F28 RID: 28456 RVA: 0x0021950C File Offset: 0x0021950C
		[Nullable(1)]
		public JsonPropertyCollection CreatorParameters
		{
			[NullableContext(1)]
			get
			{
				if (this._creatorParameters == null)
				{
					this._creatorParameters = new JsonPropertyCollection(base.UnderlyingType);
				}
				return this._creatorParameters;
			}
		}

		// Token: 0x1700170F RID: 5903
		// (get) Token: 0x06006F29 RID: 28457 RVA: 0x00219530 File Offset: 0x00219530
		// (set) Token: 0x06006F2A RID: 28458 RVA: 0x00219538 File Offset: 0x00219538
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public ObjectConstructor<object> OverrideCreator
		{
			[return: Nullable(new byte[]
			{
				2,
				1
			})]
			get
			{
				return this._overrideCreator;
			}
			[param: Nullable(new byte[]
			{
				2,
				1
			})]
			set
			{
				this._overrideCreator = value;
			}
		}

		// Token: 0x17001710 RID: 5904
		// (get) Token: 0x06006F2B RID: 28459 RVA: 0x00219544 File Offset: 0x00219544
		// (set) Token: 0x06006F2C RID: 28460 RVA: 0x0021954C File Offset: 0x0021954C
		[Nullable(new byte[]
		{
			2,
			1
		})]
		internal ObjectConstructor<object> ParameterizedCreator
		{
			[return: Nullable(new byte[]
			{
				2,
				1
			})]
			get
			{
				return this._parameterizedCreator;
			}
			[param: Nullable(new byte[]
			{
				2,
				1
			})]
			set
			{
				this._parameterizedCreator = value;
			}
		}

		// Token: 0x17001711 RID: 5905
		// (get) Token: 0x06006F2D RID: 28461 RVA: 0x00219558 File Offset: 0x00219558
		// (set) Token: 0x06006F2E RID: 28462 RVA: 0x00219560 File Offset: 0x00219560
		public ExtensionDataSetter ExtensionDataSetter { get; set; }

		// Token: 0x17001712 RID: 5906
		// (get) Token: 0x06006F2F RID: 28463 RVA: 0x0021956C File Offset: 0x0021956C
		// (set) Token: 0x06006F30 RID: 28464 RVA: 0x00219574 File Offset: 0x00219574
		public ExtensionDataGetter ExtensionDataGetter { get; set; }

		// Token: 0x17001713 RID: 5907
		// (get) Token: 0x06006F31 RID: 28465 RVA: 0x00219580 File Offset: 0x00219580
		// (set) Token: 0x06006F32 RID: 28466 RVA: 0x00219588 File Offset: 0x00219588
		public Type ExtensionDataValueType
		{
			get
			{
				return this._extensionDataValueType;
			}
			set
			{
				this._extensionDataValueType = value;
				this.ExtensionDataIsJToken = (value != null && typeof(JToken).IsAssignableFrom(value));
			}
		}

		// Token: 0x17001714 RID: 5908
		// (get) Token: 0x06006F33 RID: 28467 RVA: 0x002195BC File Offset: 0x002195BC
		// (set) Token: 0x06006F34 RID: 28468 RVA: 0x002195C4 File Offset: 0x002195C4
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		public Func<string, string> ExtensionDataNameResolver { [return: Nullable(new byte[]
		{
			2,
			1,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1,
			1
		})] set; }

		// Token: 0x17001715 RID: 5909
		// (get) Token: 0x06006F35 RID: 28469 RVA: 0x002195D0 File Offset: 0x002195D0
		internal bool HasRequiredOrDefaultValueProperties
		{
			get
			{
				if (this._hasRequiredOrDefaultValueProperties == null)
				{
					this._hasRequiredOrDefaultValueProperties = new bool?(false);
					if (this.ItemRequired.GetValueOrDefault(Required.Default) != Required.Default)
					{
						this._hasRequiredOrDefaultValueProperties = new bool?(true);
					}
					else
					{
						foreach (JsonProperty jsonProperty in this.Properties)
						{
							if (jsonProperty.Required == Required.Default)
							{
								DefaultValueHandling? defaultValueHandling = jsonProperty.DefaultValueHandling & DefaultValueHandling.Populate;
								DefaultValueHandling defaultValueHandling2 = DefaultValueHandling.Populate;
								if (!(defaultValueHandling.GetValueOrDefault() == defaultValueHandling2 & defaultValueHandling != null))
								{
									continue;
								}
							}
							this._hasRequiredOrDefaultValueProperties = new bool?(true);
							break;
						}
					}
				}
				return this._hasRequiredOrDefaultValueProperties.GetValueOrDefault();
			}
		}

		// Token: 0x06006F36 RID: 28470 RVA: 0x002196D4 File Offset: 0x002196D4
		[NullableContext(1)]
		public JsonObjectContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Object;
			this.Properties = new JsonPropertyCollection(base.UnderlyingType);
		}

		// Token: 0x06006F37 RID: 28471 RVA: 0x002196F8 File Offset: 0x002196F8
		[NullableContext(1)]
		[SecuritySafeCritical]
		internal object GetUninitializedObject()
		{
			if (!JsonTypeReflector.FullyTrusted)
			{
				throw new JsonException("Insufficient permissions. Creating an uninitialized '{0}' type requires full trust.".FormatWith(CultureInfo.InvariantCulture, this.NonNullableUnderlyingType));
			}
			return FormatterServices.GetUninitializedObject(this.NonNullableUnderlyingType);
		}

		// Token: 0x0400377F RID: 14207
		internal bool ExtensionDataIsJToken;

		// Token: 0x04003780 RID: 14208
		private bool? _hasRequiredOrDefaultValueProperties;

		// Token: 0x04003781 RID: 14209
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private ObjectConstructor<object> _overrideCreator;

		// Token: 0x04003782 RID: 14210
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private ObjectConstructor<object> _parameterizedCreator;

		// Token: 0x04003783 RID: 14211
		private JsonPropertyCollection _creatorParameters;

		// Token: 0x04003784 RID: 14212
		private Type _extensionDataValueType;
	}
}
