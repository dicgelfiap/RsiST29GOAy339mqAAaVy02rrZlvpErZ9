using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x02000A8B RID: 2699
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonSerializerSettings
	{
		// Token: 0x17001658 RID: 5720
		// (get) Token: 0x060069BC RID: 27068 RVA: 0x001FF5D0 File Offset: 0x001FF5D0
		// (set) Token: 0x060069BD RID: 27069 RVA: 0x001FF5E0 File Offset: 0x001FF5E0
		public ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return this._referenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._referenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		// Token: 0x17001659 RID: 5721
		// (get) Token: 0x060069BE RID: 27070 RVA: 0x001FF5F0 File Offset: 0x001FF5F0
		// (set) Token: 0x060069BF RID: 27071 RVA: 0x001FF600 File Offset: 0x001FF600
		public MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return this._missingMemberHandling.GetValueOrDefault();
			}
			set
			{
				this._missingMemberHandling = new MissingMemberHandling?(value);
			}
		}

		// Token: 0x1700165A RID: 5722
		// (get) Token: 0x060069C0 RID: 27072 RVA: 0x001FF610 File Offset: 0x001FF610
		// (set) Token: 0x060069C1 RID: 27073 RVA: 0x001FF620 File Offset: 0x001FF620
		public ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return this._objectCreationHandling.GetValueOrDefault();
			}
			set
			{
				this._objectCreationHandling = new ObjectCreationHandling?(value);
			}
		}

		// Token: 0x1700165B RID: 5723
		// (get) Token: 0x060069C2 RID: 27074 RVA: 0x001FF630 File Offset: 0x001FF630
		// (set) Token: 0x060069C3 RID: 27075 RVA: 0x001FF640 File Offset: 0x001FF640
		public NullValueHandling NullValueHandling
		{
			get
			{
				return this._nullValueHandling.GetValueOrDefault();
			}
			set
			{
				this._nullValueHandling = new NullValueHandling?(value);
			}
		}

		// Token: 0x1700165C RID: 5724
		// (get) Token: 0x060069C4 RID: 27076 RVA: 0x001FF650 File Offset: 0x001FF650
		// (set) Token: 0x060069C5 RID: 27077 RVA: 0x001FF660 File Offset: 0x001FF660
		public DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return this._defaultValueHandling.GetValueOrDefault();
			}
			set
			{
				this._defaultValueHandling = new DefaultValueHandling?(value);
			}
		}

		// Token: 0x1700165D RID: 5725
		// (get) Token: 0x060069C6 RID: 27078 RVA: 0x001FF670 File Offset: 0x001FF670
		// (set) Token: 0x060069C7 RID: 27079 RVA: 0x001FF678 File Offset: 0x001FF678
		[Nullable(1)]
		public IList<JsonConverter> Converters { [NullableContext(1)] get; [NullableContext(1)] set; }

		// Token: 0x1700165E RID: 5726
		// (get) Token: 0x060069C8 RID: 27080 RVA: 0x001FF684 File Offset: 0x001FF684
		// (set) Token: 0x060069C9 RID: 27081 RVA: 0x001FF694 File Offset: 0x001FF694
		public PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				return this._preserveReferencesHandling.GetValueOrDefault();
			}
			set
			{
				this._preserveReferencesHandling = new PreserveReferencesHandling?(value);
			}
		}

		// Token: 0x1700165F RID: 5727
		// (get) Token: 0x060069CA RID: 27082 RVA: 0x001FF6A4 File Offset: 0x001FF6A4
		// (set) Token: 0x060069CB RID: 27083 RVA: 0x001FF6B4 File Offset: 0x001FF6B4
		public TypeNameHandling TypeNameHandling
		{
			get
			{
				return this._typeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._typeNameHandling = new TypeNameHandling?(value);
			}
		}

		// Token: 0x17001660 RID: 5728
		// (get) Token: 0x060069CC RID: 27084 RVA: 0x001FF6C4 File Offset: 0x001FF6C4
		// (set) Token: 0x060069CD RID: 27085 RVA: 0x001FF6D4 File Offset: 0x001FF6D4
		public MetadataPropertyHandling MetadataPropertyHandling
		{
			get
			{
				return this._metadataPropertyHandling.GetValueOrDefault();
			}
			set
			{
				this._metadataPropertyHandling = new MetadataPropertyHandling?(value);
			}
		}

		// Token: 0x17001661 RID: 5729
		// (get) Token: 0x060069CE RID: 27086 RVA: 0x001FF6E4 File Offset: 0x001FF6E4
		// (set) Token: 0x060069CF RID: 27087 RVA: 0x001FF6EC File Offset: 0x001FF6EC
		[Obsolete("TypeNameAssemblyFormat is obsolete. Use TypeNameAssemblyFormatHandling instead.")]
		public FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				return (FormatterAssemblyStyle)this.TypeNameAssemblyFormatHandling;
			}
			set
			{
				this.TypeNameAssemblyFormatHandling = (TypeNameAssemblyFormatHandling)value;
			}
		}

		// Token: 0x17001662 RID: 5730
		// (get) Token: 0x060069D0 RID: 27088 RVA: 0x001FF6F8 File Offset: 0x001FF6F8
		// (set) Token: 0x060069D1 RID: 27089 RVA: 0x001FF708 File Offset: 0x001FF708
		public TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling
		{
			get
			{
				return this._typeNameAssemblyFormatHandling.GetValueOrDefault();
			}
			set
			{
				this._typeNameAssemblyFormatHandling = new TypeNameAssemblyFormatHandling?(value);
			}
		}

		// Token: 0x17001663 RID: 5731
		// (get) Token: 0x060069D2 RID: 27090 RVA: 0x001FF718 File Offset: 0x001FF718
		// (set) Token: 0x060069D3 RID: 27091 RVA: 0x001FF728 File Offset: 0x001FF728
		public ConstructorHandling ConstructorHandling
		{
			get
			{
				return this._constructorHandling.GetValueOrDefault();
			}
			set
			{
				this._constructorHandling = new ConstructorHandling?(value);
			}
		}

		// Token: 0x17001664 RID: 5732
		// (get) Token: 0x060069D4 RID: 27092 RVA: 0x001FF738 File Offset: 0x001FF738
		// (set) Token: 0x060069D5 RID: 27093 RVA: 0x001FF740 File Offset: 0x001FF740
		public IContractResolver ContractResolver { get; set; }

		// Token: 0x17001665 RID: 5733
		// (get) Token: 0x060069D6 RID: 27094 RVA: 0x001FF74C File Offset: 0x001FF74C
		// (set) Token: 0x060069D7 RID: 27095 RVA: 0x001FF754 File Offset: 0x001FF754
		public IEqualityComparer EqualityComparer { get; set; }

		// Token: 0x17001666 RID: 5734
		// (get) Token: 0x060069D8 RID: 27096 RVA: 0x001FF760 File Offset: 0x001FF760
		// (set) Token: 0x060069D9 RID: 27097 RVA: 0x001FF778 File Offset: 0x001FF778
		[Obsolete("ReferenceResolver property is obsolete. Use the ReferenceResolverProvider property to set the IReferenceResolver: settings.ReferenceResolverProvider = () => resolver")]
		public IReferenceResolver ReferenceResolver
		{
			get
			{
				Func<IReferenceResolver> referenceResolverProvider = this.ReferenceResolverProvider;
				if (referenceResolverProvider == null)
				{
					return null;
				}
				return referenceResolverProvider();
			}
			set
			{
				this.ReferenceResolverProvider = ((value != null) ? (() => value) : null);
			}
		}

		// Token: 0x17001667 RID: 5735
		// (get) Token: 0x060069DA RID: 27098 RVA: 0x001FF7BC File Offset: 0x001FF7BC
		// (set) Token: 0x060069DB RID: 27099 RVA: 0x001FF7C4 File Offset: 0x001FF7C4
		public Func<IReferenceResolver> ReferenceResolverProvider { get; set; }

		// Token: 0x17001668 RID: 5736
		// (get) Token: 0x060069DC RID: 27100 RVA: 0x001FF7D0 File Offset: 0x001FF7D0
		// (set) Token: 0x060069DD RID: 27101 RVA: 0x001FF7D8 File Offset: 0x001FF7D8
		public ITraceWriter TraceWriter { get; set; }

		// Token: 0x17001669 RID: 5737
		// (get) Token: 0x060069DE RID: 27102 RVA: 0x001FF7E4 File Offset: 0x001FF7E4
		// (set) Token: 0x060069DF RID: 27103 RVA: 0x001FF828 File Offset: 0x001FF828
		[Obsolete("Binder is obsolete. Use SerializationBinder instead.")]
		public SerializationBinder Binder
		{
			get
			{
				if (this.SerializationBinder == null)
				{
					return null;
				}
				SerializationBinderAdapter serializationBinderAdapter = this.SerializationBinder as SerializationBinderAdapter;
				if (serializationBinderAdapter != null)
				{
					return serializationBinderAdapter.SerializationBinder;
				}
				throw new InvalidOperationException("Cannot get SerializationBinder because an ISerializationBinder was previously set.");
			}
			set
			{
				this.SerializationBinder = ((value == null) ? null : new SerializationBinderAdapter(value));
			}
		}

		// Token: 0x1700166A RID: 5738
		// (get) Token: 0x060069E0 RID: 27104 RVA: 0x001FF844 File Offset: 0x001FF844
		// (set) Token: 0x060069E1 RID: 27105 RVA: 0x001FF84C File Offset: 0x001FF84C
		public ISerializationBinder SerializationBinder { get; set; }

		// Token: 0x1700166B RID: 5739
		// (get) Token: 0x060069E2 RID: 27106 RVA: 0x001FF858 File Offset: 0x001FF858
		// (set) Token: 0x060069E3 RID: 27107 RVA: 0x001FF860 File Offset: 0x001FF860
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public EventHandler<ErrorEventArgs> Error { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x1700166C RID: 5740
		// (get) Token: 0x060069E4 RID: 27108 RVA: 0x001FF86C File Offset: 0x001FF86C
		// (set) Token: 0x060069E5 RID: 27109 RVA: 0x001FF8A0 File Offset: 0x001FF8A0
		public StreamingContext Context
		{
			get
			{
				StreamingContext? context = this._context;
				if (context == null)
				{
					return JsonSerializerSettings.DefaultContext;
				}
				return context.GetValueOrDefault();
			}
			set
			{
				this._context = new StreamingContext?(value);
			}
		}

		// Token: 0x1700166D RID: 5741
		// (get) Token: 0x060069E6 RID: 27110 RVA: 0x001FF8B0 File Offset: 0x001FF8B0
		// (set) Token: 0x060069E7 RID: 27111 RVA: 0x001FF8C4 File Offset: 0x001FF8C4
		[Nullable(1)]
		public string DateFormatString
		{
			[NullableContext(1)]
			get
			{
				return this._dateFormatString ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
			}
			[NullableContext(1)]
			set
			{
				this._dateFormatString = value;
				this._dateFormatStringSet = true;
			}
		}

		// Token: 0x1700166E RID: 5742
		// (get) Token: 0x060069E8 RID: 27112 RVA: 0x001FF8D4 File Offset: 0x001FF8D4
		// (set) Token: 0x060069E9 RID: 27113 RVA: 0x001FF8DC File Offset: 0x001FF8DC
		public int? MaxDepth
		{
			get
			{
				return this._maxDepth;
			}
			set
			{
				int? num = value;
				int num2 = 0;
				if (num.GetValueOrDefault() <= num2 & num != null)
				{
					throw new ArgumentException("Value must be positive.", "value");
				}
				this._maxDepth = value;
				this._maxDepthSet = true;
			}
		}

		// Token: 0x1700166F RID: 5743
		// (get) Token: 0x060069EA RID: 27114 RVA: 0x001FF92C File Offset: 0x001FF92C
		// (set) Token: 0x060069EB RID: 27115 RVA: 0x001FF93C File Offset: 0x001FF93C
		public Formatting Formatting
		{
			get
			{
				return this._formatting.GetValueOrDefault();
			}
			set
			{
				this._formatting = new Formatting?(value);
			}
		}

		// Token: 0x17001670 RID: 5744
		// (get) Token: 0x060069EC RID: 27116 RVA: 0x001FF94C File Offset: 0x001FF94C
		// (set) Token: 0x060069ED RID: 27117 RVA: 0x001FF95C File Offset: 0x001FF95C
		public DateFormatHandling DateFormatHandling
		{
			get
			{
				return this._dateFormatHandling.GetValueOrDefault();
			}
			set
			{
				this._dateFormatHandling = new DateFormatHandling?(value);
			}
		}

		// Token: 0x17001671 RID: 5745
		// (get) Token: 0x060069EE RID: 27118 RVA: 0x001FF96C File Offset: 0x001FF96C
		// (set) Token: 0x060069EF RID: 27119 RVA: 0x001FF99C File Offset: 0x001FF99C
		public DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				DateTimeZoneHandling? dateTimeZoneHandling = this._dateTimeZoneHandling;
				if (dateTimeZoneHandling == null)
				{
					return DateTimeZoneHandling.RoundtripKind;
				}
				return dateTimeZoneHandling.GetValueOrDefault();
			}
			set
			{
				this._dateTimeZoneHandling = new DateTimeZoneHandling?(value);
			}
		}

		// Token: 0x17001672 RID: 5746
		// (get) Token: 0x060069F0 RID: 27120 RVA: 0x001FF9AC File Offset: 0x001FF9AC
		// (set) Token: 0x060069F1 RID: 27121 RVA: 0x001FF9DC File Offset: 0x001FF9DC
		public DateParseHandling DateParseHandling
		{
			get
			{
				DateParseHandling? dateParseHandling = this._dateParseHandling;
				if (dateParseHandling == null)
				{
					return DateParseHandling.DateTime;
				}
				return dateParseHandling.GetValueOrDefault();
			}
			set
			{
				this._dateParseHandling = new DateParseHandling?(value);
			}
		}

		// Token: 0x17001673 RID: 5747
		// (get) Token: 0x060069F2 RID: 27122 RVA: 0x001FF9EC File Offset: 0x001FF9EC
		// (set) Token: 0x060069F3 RID: 27123 RVA: 0x001FF9FC File Offset: 0x001FF9FC
		public FloatFormatHandling FloatFormatHandling
		{
			get
			{
				return this._floatFormatHandling.GetValueOrDefault();
			}
			set
			{
				this._floatFormatHandling = new FloatFormatHandling?(value);
			}
		}

		// Token: 0x17001674 RID: 5748
		// (get) Token: 0x060069F4 RID: 27124 RVA: 0x001FFA0C File Offset: 0x001FFA0C
		// (set) Token: 0x060069F5 RID: 27125 RVA: 0x001FFA1C File Offset: 0x001FFA1C
		public FloatParseHandling FloatParseHandling
		{
			get
			{
				return this._floatParseHandling.GetValueOrDefault();
			}
			set
			{
				this._floatParseHandling = new FloatParseHandling?(value);
			}
		}

		// Token: 0x17001675 RID: 5749
		// (get) Token: 0x060069F6 RID: 27126 RVA: 0x001FFA2C File Offset: 0x001FFA2C
		// (set) Token: 0x060069F7 RID: 27127 RVA: 0x001FFA3C File Offset: 0x001FFA3C
		public StringEscapeHandling StringEscapeHandling
		{
			get
			{
				return this._stringEscapeHandling.GetValueOrDefault();
			}
			set
			{
				this._stringEscapeHandling = new StringEscapeHandling?(value);
			}
		}

		// Token: 0x17001676 RID: 5750
		// (get) Token: 0x060069F8 RID: 27128 RVA: 0x001FFA4C File Offset: 0x001FFA4C
		// (set) Token: 0x060069F9 RID: 27129 RVA: 0x001FFA60 File Offset: 0x001FFA60
		[Nullable(1)]
		public CultureInfo Culture
		{
			[NullableContext(1)]
			get
			{
				return this._culture ?? JsonSerializerSettings.DefaultCulture;
			}
			[NullableContext(1)]
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x17001677 RID: 5751
		// (get) Token: 0x060069FA RID: 27130 RVA: 0x001FFA6C File Offset: 0x001FFA6C
		// (set) Token: 0x060069FB RID: 27131 RVA: 0x001FFA7C File Offset: 0x001FFA7C
		public bool CheckAdditionalContent
		{
			get
			{
				return this._checkAdditionalContent.GetValueOrDefault();
			}
			set
			{
				this._checkAdditionalContent = new bool?(value);
			}
		}

		// Token: 0x060069FD RID: 27133 RVA: 0x001FFAA4 File Offset: 0x001FFAA4
		[DebuggerStepThrough]
		public JsonSerializerSettings()
		{
			this.Converters = new List<JsonConverter>();
		}

		// Token: 0x04003592 RID: 13714
		internal const ReferenceLoopHandling DefaultReferenceLoopHandling = ReferenceLoopHandling.Error;

		// Token: 0x04003593 RID: 13715
		internal const MissingMemberHandling DefaultMissingMemberHandling = MissingMemberHandling.Ignore;

		// Token: 0x04003594 RID: 13716
		internal const NullValueHandling DefaultNullValueHandling = NullValueHandling.Include;

		// Token: 0x04003595 RID: 13717
		internal const DefaultValueHandling DefaultDefaultValueHandling = DefaultValueHandling.Include;

		// Token: 0x04003596 RID: 13718
		internal const ObjectCreationHandling DefaultObjectCreationHandling = ObjectCreationHandling.Auto;

		// Token: 0x04003597 RID: 13719
		internal const PreserveReferencesHandling DefaultPreserveReferencesHandling = PreserveReferencesHandling.None;

		// Token: 0x04003598 RID: 13720
		internal const ConstructorHandling DefaultConstructorHandling = ConstructorHandling.Default;

		// Token: 0x04003599 RID: 13721
		internal const TypeNameHandling DefaultTypeNameHandling = TypeNameHandling.None;

		// Token: 0x0400359A RID: 13722
		internal const MetadataPropertyHandling DefaultMetadataPropertyHandling = MetadataPropertyHandling.Default;

		// Token: 0x0400359B RID: 13723
		internal static readonly StreamingContext DefaultContext = default(StreamingContext);

		// Token: 0x0400359C RID: 13724
		internal const Formatting DefaultFormatting = Formatting.None;

		// Token: 0x0400359D RID: 13725
		internal const DateFormatHandling DefaultDateFormatHandling = DateFormatHandling.IsoDateFormat;

		// Token: 0x0400359E RID: 13726
		internal const DateTimeZoneHandling DefaultDateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;

		// Token: 0x0400359F RID: 13727
		internal const DateParseHandling DefaultDateParseHandling = DateParseHandling.DateTime;

		// Token: 0x040035A0 RID: 13728
		internal const FloatParseHandling DefaultFloatParseHandling = FloatParseHandling.Double;

		// Token: 0x040035A1 RID: 13729
		internal const FloatFormatHandling DefaultFloatFormatHandling = FloatFormatHandling.String;

		// Token: 0x040035A2 RID: 13730
		internal const StringEscapeHandling DefaultStringEscapeHandling = StringEscapeHandling.Default;

		// Token: 0x040035A3 RID: 13731
		internal const TypeNameAssemblyFormatHandling DefaultTypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;

		// Token: 0x040035A4 RID: 13732
		[Nullable(1)]
		internal static readonly CultureInfo DefaultCulture = CultureInfo.InvariantCulture;

		// Token: 0x040035A5 RID: 13733
		internal const bool DefaultCheckAdditionalContent = false;

		// Token: 0x040035A6 RID: 13734
		[Nullable(1)]
		internal const string DefaultDateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

		// Token: 0x040035A7 RID: 13735
		internal Formatting? _formatting;

		// Token: 0x040035A8 RID: 13736
		internal DateFormatHandling? _dateFormatHandling;

		// Token: 0x040035A9 RID: 13737
		internal DateTimeZoneHandling? _dateTimeZoneHandling;

		// Token: 0x040035AA RID: 13738
		internal DateParseHandling? _dateParseHandling;

		// Token: 0x040035AB RID: 13739
		internal FloatFormatHandling? _floatFormatHandling;

		// Token: 0x040035AC RID: 13740
		internal FloatParseHandling? _floatParseHandling;

		// Token: 0x040035AD RID: 13741
		internal StringEscapeHandling? _stringEscapeHandling;

		// Token: 0x040035AE RID: 13742
		internal CultureInfo _culture;

		// Token: 0x040035AF RID: 13743
		internal bool? _checkAdditionalContent;

		// Token: 0x040035B0 RID: 13744
		internal int? _maxDepth;

		// Token: 0x040035B1 RID: 13745
		internal bool _maxDepthSet;

		// Token: 0x040035B2 RID: 13746
		internal string _dateFormatString;

		// Token: 0x040035B3 RID: 13747
		internal bool _dateFormatStringSet;

		// Token: 0x040035B4 RID: 13748
		internal TypeNameAssemblyFormatHandling? _typeNameAssemblyFormatHandling;

		// Token: 0x040035B5 RID: 13749
		internal DefaultValueHandling? _defaultValueHandling;

		// Token: 0x040035B6 RID: 13750
		internal PreserveReferencesHandling? _preserveReferencesHandling;

		// Token: 0x040035B7 RID: 13751
		internal NullValueHandling? _nullValueHandling;

		// Token: 0x040035B8 RID: 13752
		internal ObjectCreationHandling? _objectCreationHandling;

		// Token: 0x040035B9 RID: 13753
		internal MissingMemberHandling? _missingMemberHandling;

		// Token: 0x040035BA RID: 13754
		internal ReferenceLoopHandling? _referenceLoopHandling;

		// Token: 0x040035BB RID: 13755
		internal StreamingContext? _context;

		// Token: 0x040035BC RID: 13756
		internal ConstructorHandling? _constructorHandling;

		// Token: 0x040035BD RID: 13757
		internal TypeNameHandling? _typeNameHandling;

		// Token: 0x040035BE RID: 13758
		internal MetadataPropertyHandling? _metadataPropertyHandling;
	}
}
