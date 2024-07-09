using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x02000A8A RID: 2698
	[NullableContext(1)]
	[Nullable(0)]
	public class JsonSerializer
	{
		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06006964 RID: 26980 RVA: 0x001FE2E8 File Offset: 0x001FE2E8
		// (remove) Token: 0x06006965 RID: 26981 RVA: 0x001FE324 File Offset: 0x001FE324
		[Nullable(new byte[]
		{
			2,
			1
		})]
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public virtual event EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs> Error;

		// Token: 0x1700163A RID: 5690
		// (get) Token: 0x06006966 RID: 26982 RVA: 0x001FE360 File Offset: 0x001FE360
		// (set) Token: 0x06006967 RID: 26983 RVA: 0x001FE368 File Offset: 0x001FE368
		[Nullable(2)]
		public virtual IReferenceResolver ReferenceResolver
		{
			[NullableContext(2)]
			get
			{
				return this.GetReferenceResolver();
			}
			[NullableContext(2)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", "Reference resolver cannot be null.");
				}
				this._referenceResolver = value;
			}
		}

		// Token: 0x1700163B RID: 5691
		// (get) Token: 0x06006968 RID: 26984 RVA: 0x001FE388 File Offset: 0x001FE388
		// (set) Token: 0x06006969 RID: 26985 RVA: 0x001FE3D0 File Offset: 0x001FE3D0
		[Obsolete("Binder is obsolete. Use SerializationBinder instead.")]
		public virtual SerializationBinder Binder
		{
			get
			{
				SerializationBinder serializationBinder = this._serializationBinder as SerializationBinder;
				if (serializationBinder != null)
				{
					return serializationBinder;
				}
				SerializationBinderAdapter serializationBinderAdapter = this._serializationBinder as SerializationBinderAdapter;
				if (serializationBinderAdapter != null)
				{
					return serializationBinderAdapter.SerializationBinder;
				}
				throw new InvalidOperationException("Cannot get SerializationBinder because an ISerializationBinder was previously set.");
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", "Serialization binder cannot be null.");
				}
				this._serializationBinder = ((value as ISerializationBinder) ?? new SerializationBinderAdapter(value));
			}
		}

		// Token: 0x1700163C RID: 5692
		// (get) Token: 0x0600696A RID: 26986 RVA: 0x001FE404 File Offset: 0x001FE404
		// (set) Token: 0x0600696B RID: 26987 RVA: 0x001FE40C File Offset: 0x001FE40C
		public virtual ISerializationBinder SerializationBinder
		{
			get
			{
				return this._serializationBinder;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", "Serialization binder cannot be null.");
				}
				this._serializationBinder = value;
			}
		}

		// Token: 0x1700163D RID: 5693
		// (get) Token: 0x0600696C RID: 26988 RVA: 0x001FE42C File Offset: 0x001FE42C
		// (set) Token: 0x0600696D RID: 26989 RVA: 0x001FE434 File Offset: 0x001FE434
		[Nullable(2)]
		public virtual ITraceWriter TraceWriter
		{
			[NullableContext(2)]
			get
			{
				return this._traceWriter;
			}
			[NullableContext(2)]
			set
			{
				this._traceWriter = value;
			}
		}

		// Token: 0x1700163E RID: 5694
		// (get) Token: 0x0600696E RID: 26990 RVA: 0x001FE440 File Offset: 0x001FE440
		// (set) Token: 0x0600696F RID: 26991 RVA: 0x001FE448 File Offset: 0x001FE448
		[Nullable(2)]
		public virtual IEqualityComparer EqualityComparer
		{
			[NullableContext(2)]
			get
			{
				return this._equalityComparer;
			}
			[NullableContext(2)]
			set
			{
				this._equalityComparer = value;
			}
		}

		// Token: 0x1700163F RID: 5695
		// (get) Token: 0x06006970 RID: 26992 RVA: 0x001FE454 File Offset: 0x001FE454
		// (set) Token: 0x06006971 RID: 26993 RVA: 0x001FE45C File Offset: 0x001FE45C
		public virtual TypeNameHandling TypeNameHandling
		{
			get
			{
				return this._typeNameHandling;
			}
			set
			{
				if (value < TypeNameHandling.None || value > TypeNameHandling.Auto)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._typeNameHandling = value;
			}
		}

		// Token: 0x17001640 RID: 5696
		// (get) Token: 0x06006972 RID: 26994 RVA: 0x001FE480 File Offset: 0x001FE480
		// (set) Token: 0x06006973 RID: 26995 RVA: 0x001FE488 File Offset: 0x001FE488
		[Obsolete("TypeNameAssemblyFormat is obsolete. Use TypeNameAssemblyFormatHandling instead.")]
		public virtual FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				return (FormatterAssemblyStyle)this._typeNameAssemblyFormatHandling;
			}
			set
			{
				if (value < FormatterAssemblyStyle.Simple || value > FormatterAssemblyStyle.Full)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._typeNameAssemblyFormatHandling = (TypeNameAssemblyFormatHandling)value;
			}
		}

		// Token: 0x17001641 RID: 5697
		// (get) Token: 0x06006974 RID: 26996 RVA: 0x001FE4AC File Offset: 0x001FE4AC
		// (set) Token: 0x06006975 RID: 26997 RVA: 0x001FE4B4 File Offset: 0x001FE4B4
		public virtual TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling
		{
			get
			{
				return this._typeNameAssemblyFormatHandling;
			}
			set
			{
				if (value < TypeNameAssemblyFormatHandling.Simple || value > TypeNameAssemblyFormatHandling.Full)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._typeNameAssemblyFormatHandling = value;
			}
		}

		// Token: 0x17001642 RID: 5698
		// (get) Token: 0x06006976 RID: 26998 RVA: 0x001FE4D8 File Offset: 0x001FE4D8
		// (set) Token: 0x06006977 RID: 26999 RVA: 0x001FE4E0 File Offset: 0x001FE4E0
		public virtual PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				return this._preserveReferencesHandling;
			}
			set
			{
				if (value < PreserveReferencesHandling.None || value > PreserveReferencesHandling.All)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._preserveReferencesHandling = value;
			}
		}

		// Token: 0x17001643 RID: 5699
		// (get) Token: 0x06006978 RID: 27000 RVA: 0x001FE504 File Offset: 0x001FE504
		// (set) Token: 0x06006979 RID: 27001 RVA: 0x001FE50C File Offset: 0x001FE50C
		public virtual ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return this._referenceLoopHandling;
			}
			set
			{
				if (value < ReferenceLoopHandling.Error || value > ReferenceLoopHandling.Serialize)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._referenceLoopHandling = value;
			}
		}

		// Token: 0x17001644 RID: 5700
		// (get) Token: 0x0600697A RID: 27002 RVA: 0x001FE530 File Offset: 0x001FE530
		// (set) Token: 0x0600697B RID: 27003 RVA: 0x001FE538 File Offset: 0x001FE538
		public virtual MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return this._missingMemberHandling;
			}
			set
			{
				if (value < MissingMemberHandling.Ignore || value > MissingMemberHandling.Error)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._missingMemberHandling = value;
			}
		}

		// Token: 0x17001645 RID: 5701
		// (get) Token: 0x0600697C RID: 27004 RVA: 0x001FE55C File Offset: 0x001FE55C
		// (set) Token: 0x0600697D RID: 27005 RVA: 0x001FE564 File Offset: 0x001FE564
		public virtual NullValueHandling NullValueHandling
		{
			get
			{
				return this._nullValueHandling;
			}
			set
			{
				if (value < NullValueHandling.Include || value > NullValueHandling.Ignore)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._nullValueHandling = value;
			}
		}

		// Token: 0x17001646 RID: 5702
		// (get) Token: 0x0600697E RID: 27006 RVA: 0x001FE588 File Offset: 0x001FE588
		// (set) Token: 0x0600697F RID: 27007 RVA: 0x001FE590 File Offset: 0x001FE590
		public virtual DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return this._defaultValueHandling;
			}
			set
			{
				if (value < DefaultValueHandling.Include || value > DefaultValueHandling.IgnoreAndPopulate)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._defaultValueHandling = value;
			}
		}

		// Token: 0x17001647 RID: 5703
		// (get) Token: 0x06006980 RID: 27008 RVA: 0x001FE5B4 File Offset: 0x001FE5B4
		// (set) Token: 0x06006981 RID: 27009 RVA: 0x001FE5BC File Offset: 0x001FE5BC
		public virtual ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return this._objectCreationHandling;
			}
			set
			{
				if (value < ObjectCreationHandling.Auto || value > ObjectCreationHandling.Replace)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._objectCreationHandling = value;
			}
		}

		// Token: 0x17001648 RID: 5704
		// (get) Token: 0x06006982 RID: 27010 RVA: 0x001FE5E0 File Offset: 0x001FE5E0
		// (set) Token: 0x06006983 RID: 27011 RVA: 0x001FE5E8 File Offset: 0x001FE5E8
		public virtual ConstructorHandling ConstructorHandling
		{
			get
			{
				return this._constructorHandling;
			}
			set
			{
				if (value < ConstructorHandling.Default || value > ConstructorHandling.AllowNonPublicDefaultConstructor)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._constructorHandling = value;
			}
		}

		// Token: 0x17001649 RID: 5705
		// (get) Token: 0x06006984 RID: 27012 RVA: 0x001FE60C File Offset: 0x001FE60C
		// (set) Token: 0x06006985 RID: 27013 RVA: 0x001FE614 File Offset: 0x001FE614
		public virtual MetadataPropertyHandling MetadataPropertyHandling
		{
			get
			{
				return this._metadataPropertyHandling;
			}
			set
			{
				if (value < MetadataPropertyHandling.Default || value > MetadataPropertyHandling.Ignore)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._metadataPropertyHandling = value;
			}
		}

		// Token: 0x1700164A RID: 5706
		// (get) Token: 0x06006986 RID: 27014 RVA: 0x001FE638 File Offset: 0x001FE638
		public virtual JsonConverterCollection Converters
		{
			get
			{
				if (this._converters == null)
				{
					this._converters = new JsonConverterCollection();
				}
				return this._converters;
			}
		}

		// Token: 0x1700164B RID: 5707
		// (get) Token: 0x06006987 RID: 27015 RVA: 0x001FE658 File Offset: 0x001FE658
		// (set) Token: 0x06006988 RID: 27016 RVA: 0x001FE660 File Offset: 0x001FE660
		public virtual IContractResolver ContractResolver
		{
			get
			{
				return this._contractResolver;
			}
			set
			{
				this._contractResolver = (value ?? DefaultContractResolver.Instance);
			}
		}

		// Token: 0x1700164C RID: 5708
		// (get) Token: 0x06006989 RID: 27017 RVA: 0x001FE678 File Offset: 0x001FE678
		// (set) Token: 0x0600698A RID: 27018 RVA: 0x001FE680 File Offset: 0x001FE680
		public virtual StreamingContext Context
		{
			get
			{
				return this._context;
			}
			set
			{
				this._context = value;
			}
		}

		// Token: 0x1700164D RID: 5709
		// (get) Token: 0x0600698B RID: 27019 RVA: 0x001FE68C File Offset: 0x001FE68C
		// (set) Token: 0x0600698C RID: 27020 RVA: 0x001FE69C File Offset: 0x001FE69C
		public virtual Formatting Formatting
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

		// Token: 0x1700164E RID: 5710
		// (get) Token: 0x0600698D RID: 27021 RVA: 0x001FE6AC File Offset: 0x001FE6AC
		// (set) Token: 0x0600698E RID: 27022 RVA: 0x001FE6BC File Offset: 0x001FE6BC
		public virtual DateFormatHandling DateFormatHandling
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

		// Token: 0x1700164F RID: 5711
		// (get) Token: 0x0600698F RID: 27023 RVA: 0x001FE6CC File Offset: 0x001FE6CC
		// (set) Token: 0x06006990 RID: 27024 RVA: 0x001FE6FC File Offset: 0x001FE6FC
		public virtual DateTimeZoneHandling DateTimeZoneHandling
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

		// Token: 0x17001650 RID: 5712
		// (get) Token: 0x06006991 RID: 27025 RVA: 0x001FE70C File Offset: 0x001FE70C
		// (set) Token: 0x06006992 RID: 27026 RVA: 0x001FE73C File Offset: 0x001FE73C
		public virtual DateParseHandling DateParseHandling
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

		// Token: 0x17001651 RID: 5713
		// (get) Token: 0x06006993 RID: 27027 RVA: 0x001FE74C File Offset: 0x001FE74C
		// (set) Token: 0x06006994 RID: 27028 RVA: 0x001FE75C File Offset: 0x001FE75C
		public virtual FloatParseHandling FloatParseHandling
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

		// Token: 0x17001652 RID: 5714
		// (get) Token: 0x06006995 RID: 27029 RVA: 0x001FE76C File Offset: 0x001FE76C
		// (set) Token: 0x06006996 RID: 27030 RVA: 0x001FE77C File Offset: 0x001FE77C
		public virtual FloatFormatHandling FloatFormatHandling
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

		// Token: 0x17001653 RID: 5715
		// (get) Token: 0x06006997 RID: 27031 RVA: 0x001FE78C File Offset: 0x001FE78C
		// (set) Token: 0x06006998 RID: 27032 RVA: 0x001FE79C File Offset: 0x001FE79C
		public virtual StringEscapeHandling StringEscapeHandling
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

		// Token: 0x17001654 RID: 5716
		// (get) Token: 0x06006999 RID: 27033 RVA: 0x001FE7AC File Offset: 0x001FE7AC
		// (set) Token: 0x0600699A RID: 27034 RVA: 0x001FE7C0 File Offset: 0x001FE7C0
		public virtual string DateFormatString
		{
			get
			{
				return this._dateFormatString ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
			}
			set
			{
				this._dateFormatString = value;
				this._dateFormatStringSet = true;
			}
		}

		// Token: 0x17001655 RID: 5717
		// (get) Token: 0x0600699B RID: 27035 RVA: 0x001FE7D0 File Offset: 0x001FE7D0
		// (set) Token: 0x0600699C RID: 27036 RVA: 0x001FE7E4 File Offset: 0x001FE7E4
		public virtual CultureInfo Culture
		{
			get
			{
				return this._culture ?? JsonSerializerSettings.DefaultCulture;
			}
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x17001656 RID: 5718
		// (get) Token: 0x0600699D RID: 27037 RVA: 0x001FE7F0 File Offset: 0x001FE7F0
		// (set) Token: 0x0600699E RID: 27038 RVA: 0x001FE7F8 File Offset: 0x001FE7F8
		public virtual int? MaxDepth
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

		// Token: 0x17001657 RID: 5719
		// (get) Token: 0x0600699F RID: 27039 RVA: 0x001FE848 File Offset: 0x001FE848
		// (set) Token: 0x060069A0 RID: 27040 RVA: 0x001FE858 File Offset: 0x001FE858
		public virtual bool CheckAdditionalContent
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

		// Token: 0x060069A1 RID: 27041 RVA: 0x001FE868 File Offset: 0x001FE868
		internal bool IsCheckAdditionalContentSet()
		{
			return this._checkAdditionalContent != null;
		}

		// Token: 0x060069A2 RID: 27042 RVA: 0x001FE878 File Offset: 0x001FE878
		public JsonSerializer()
		{
			this._referenceLoopHandling = ReferenceLoopHandling.Error;
			this._missingMemberHandling = MissingMemberHandling.Ignore;
			this._nullValueHandling = NullValueHandling.Include;
			this._defaultValueHandling = DefaultValueHandling.Include;
			this._objectCreationHandling = ObjectCreationHandling.Auto;
			this._preserveReferencesHandling = PreserveReferencesHandling.None;
			this._constructorHandling = ConstructorHandling.Default;
			this._typeNameHandling = TypeNameHandling.None;
			this._metadataPropertyHandling = MetadataPropertyHandling.Default;
			this._context = JsonSerializerSettings.DefaultContext;
			this._serializationBinder = DefaultSerializationBinder.Instance;
			this._culture = JsonSerializerSettings.DefaultCulture;
			this._contractResolver = DefaultContractResolver.Instance;
		}

		// Token: 0x060069A3 RID: 27043 RVA: 0x001FE8FC File Offset: 0x001FE8FC
		public static JsonSerializer Create()
		{
			return new JsonSerializer();
		}

		// Token: 0x060069A4 RID: 27044 RVA: 0x001FE904 File Offset: 0x001FE904
		public static JsonSerializer Create([Nullable(2)] JsonSerializerSettings settings)
		{
			JsonSerializer jsonSerializer = JsonSerializer.Create();
			if (settings != null)
			{
				JsonSerializer.ApplySerializerSettings(jsonSerializer, settings);
			}
			return jsonSerializer;
		}

		// Token: 0x060069A5 RID: 27045 RVA: 0x001FE92C File Offset: 0x001FE92C
		public static JsonSerializer CreateDefault()
		{
			Func<JsonSerializerSettings> defaultSettings = JsonConvert.DefaultSettings;
			return JsonSerializer.Create((defaultSettings != null) ? defaultSettings() : null);
		}

		// Token: 0x060069A6 RID: 27046 RVA: 0x001FE94C File Offset: 0x001FE94C
		public static JsonSerializer CreateDefault([Nullable(2)] JsonSerializerSettings settings)
		{
			JsonSerializer jsonSerializer = JsonSerializer.CreateDefault();
			if (settings != null)
			{
				JsonSerializer.ApplySerializerSettings(jsonSerializer, settings);
			}
			return jsonSerializer;
		}

		// Token: 0x060069A7 RID: 27047 RVA: 0x001FE974 File Offset: 0x001FE974
		private static void ApplySerializerSettings(JsonSerializer serializer, JsonSerializerSettings settings)
		{
			if (!CollectionUtils.IsNullOrEmpty<JsonConverter>(settings.Converters))
			{
				for (int i = 0; i < settings.Converters.Count; i++)
				{
					serializer.Converters.Insert(i, settings.Converters[i]);
				}
			}
			if (settings._typeNameHandling != null)
			{
				serializer.TypeNameHandling = settings.TypeNameHandling;
			}
			if (settings._metadataPropertyHandling != null)
			{
				serializer.MetadataPropertyHandling = settings.MetadataPropertyHandling;
			}
			if (settings._typeNameAssemblyFormatHandling != null)
			{
				serializer.TypeNameAssemblyFormatHandling = settings.TypeNameAssemblyFormatHandling;
			}
			if (settings._preserveReferencesHandling != null)
			{
				serializer.PreserveReferencesHandling = settings.PreserveReferencesHandling;
			}
			if (settings._referenceLoopHandling != null)
			{
				serializer.ReferenceLoopHandling = settings.ReferenceLoopHandling;
			}
			if (settings._missingMemberHandling != null)
			{
				serializer.MissingMemberHandling = settings.MissingMemberHandling;
			}
			if (settings._objectCreationHandling != null)
			{
				serializer.ObjectCreationHandling = settings.ObjectCreationHandling;
			}
			if (settings._nullValueHandling != null)
			{
				serializer.NullValueHandling = settings.NullValueHandling;
			}
			if (settings._defaultValueHandling != null)
			{
				serializer.DefaultValueHandling = settings.DefaultValueHandling;
			}
			if (settings._constructorHandling != null)
			{
				serializer.ConstructorHandling = settings.ConstructorHandling;
			}
			if (settings._context != null)
			{
				serializer.Context = settings.Context;
			}
			if (settings._checkAdditionalContent != null)
			{
				serializer._checkAdditionalContent = settings._checkAdditionalContent;
			}
			if (settings.Error != null)
			{
				serializer.Error += settings.Error;
			}
			if (settings.ContractResolver != null)
			{
				serializer.ContractResolver = settings.ContractResolver;
			}
			if (settings.ReferenceResolverProvider != null)
			{
				serializer.ReferenceResolver = settings.ReferenceResolverProvider();
			}
			if (settings.TraceWriter != null)
			{
				serializer.TraceWriter = settings.TraceWriter;
			}
			if (settings.EqualityComparer != null)
			{
				serializer.EqualityComparer = settings.EqualityComparer;
			}
			if (settings.SerializationBinder != null)
			{
				serializer.SerializationBinder = settings.SerializationBinder;
			}
			if (settings._formatting != null)
			{
				serializer._formatting = settings._formatting;
			}
			if (settings._dateFormatHandling != null)
			{
				serializer._dateFormatHandling = settings._dateFormatHandling;
			}
			if (settings._dateTimeZoneHandling != null)
			{
				serializer._dateTimeZoneHandling = settings._dateTimeZoneHandling;
			}
			if (settings._dateParseHandling != null)
			{
				serializer._dateParseHandling = settings._dateParseHandling;
			}
			if (settings._dateFormatStringSet)
			{
				serializer._dateFormatString = settings._dateFormatString;
				serializer._dateFormatStringSet = settings._dateFormatStringSet;
			}
			if (settings._floatFormatHandling != null)
			{
				serializer._floatFormatHandling = settings._floatFormatHandling;
			}
			if (settings._floatParseHandling != null)
			{
				serializer._floatParseHandling = settings._floatParseHandling;
			}
			if (settings._stringEscapeHandling != null)
			{
				serializer._stringEscapeHandling = settings._stringEscapeHandling;
			}
			if (settings._culture != null)
			{
				serializer._culture = settings._culture;
			}
			if (settings._maxDepthSet)
			{
				serializer._maxDepth = settings._maxDepth;
				serializer._maxDepthSet = settings._maxDepthSet;
			}
		}

		// Token: 0x060069A8 RID: 27048 RVA: 0x001FECC8 File Offset: 0x001FECC8
		[DebuggerStepThrough]
		public void Populate(TextReader reader, object target)
		{
			this.Populate(new JsonTextReader(reader), target);
		}

		// Token: 0x060069A9 RID: 27049 RVA: 0x001FECD8 File Offset: 0x001FECD8
		[DebuggerStepThrough]
		public void Populate(JsonReader reader, object target)
		{
			this.PopulateInternal(reader, target);
		}

		// Token: 0x060069AA RID: 27050 RVA: 0x001FECE4 File Offset: 0x001FECE4
		internal virtual void PopulateInternal(JsonReader reader, object target)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(target, "target");
			CultureInfo previousCulture;
			DateTimeZoneHandling? previousDateTimeZoneHandling;
			DateParseHandling? previousDateParseHandling;
			FloatParseHandling? previousFloatParseHandling;
			int? previousMaxDepth;
			string previousDateFormatString;
			this.SetupReader(reader, out previousCulture, out previousDateTimeZoneHandling, out previousDateParseHandling, out previousFloatParseHandling, out previousMaxDepth, out previousDateFormatString);
			TraceJsonReader traceJsonReader = (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose) ? this.CreateTraceJsonReader(reader) : null;
			new JsonSerializerInternalReader(this).Populate(traceJsonReader ?? reader, target);
			if (traceJsonReader != null)
			{
				this.TraceWriter.Trace(TraceLevel.Verbose, traceJsonReader.GetDeserializedJsonMessage(), null);
			}
			this.ResetReader(reader, previousCulture, previousDateTimeZoneHandling, previousDateParseHandling, previousFloatParseHandling, previousMaxDepth, previousDateFormatString);
		}

		// Token: 0x060069AB RID: 27051 RVA: 0x001FED8C File Offset: 0x001FED8C
		[DebuggerStepThrough]
		[return: Nullable(2)]
		public object Deserialize(JsonReader reader)
		{
			return this.Deserialize(reader, null);
		}

		// Token: 0x060069AC RID: 27052 RVA: 0x001FED98 File Offset: 0x001FED98
		[DebuggerStepThrough]
		[return: Nullable(2)]
		public object Deserialize(TextReader reader, Type objectType)
		{
			return this.Deserialize(new JsonTextReader(reader), objectType);
		}

		// Token: 0x060069AD RID: 27053 RVA: 0x001FEDA8 File Offset: 0x001FEDA8
		[DebuggerStepThrough]
		[return: MaybeNull]
		public T Deserialize<[Nullable(2)] T>(JsonReader reader)
		{
			return (T)((object)this.Deserialize(reader, typeof(T)));
		}

		// Token: 0x060069AE RID: 27054 RVA: 0x001FEDC0 File Offset: 0x001FEDC0
		[NullableContext(2)]
		[DebuggerStepThrough]
		public object Deserialize([Nullable(1)] JsonReader reader, Type objectType)
		{
			return this.DeserializeInternal(reader, objectType);
		}

		// Token: 0x060069AF RID: 27055 RVA: 0x001FEDCC File Offset: 0x001FEDCC
		[NullableContext(2)]
		internal virtual object DeserializeInternal([Nullable(1)] JsonReader reader, Type objectType)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			CultureInfo previousCulture;
			DateTimeZoneHandling? previousDateTimeZoneHandling;
			DateParseHandling? previousDateParseHandling;
			FloatParseHandling? previousFloatParseHandling;
			int? previousMaxDepth;
			string previousDateFormatString;
			this.SetupReader(reader, out previousCulture, out previousDateTimeZoneHandling, out previousDateParseHandling, out previousFloatParseHandling, out previousMaxDepth, out previousDateFormatString);
			TraceJsonReader traceJsonReader = (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose) ? this.CreateTraceJsonReader(reader) : null;
			object result = new JsonSerializerInternalReader(this).Deserialize(traceJsonReader ?? reader, objectType, this.CheckAdditionalContent);
			if (traceJsonReader != null)
			{
				this.TraceWriter.Trace(TraceLevel.Verbose, traceJsonReader.GetDeserializedJsonMessage(), null);
			}
			this.ResetReader(reader, previousCulture, previousDateTimeZoneHandling, previousDateParseHandling, previousFloatParseHandling, previousMaxDepth, previousDateFormatString);
			return result;
		}

		// Token: 0x060069B0 RID: 27056 RVA: 0x001FEE6C File Offset: 0x001FEE6C
		[NullableContext(2)]
		private void SetupReader([Nullable(1)] JsonReader reader, out CultureInfo previousCulture, out DateTimeZoneHandling? previousDateTimeZoneHandling, out DateParseHandling? previousDateParseHandling, out FloatParseHandling? previousFloatParseHandling, out int? previousMaxDepth, out string previousDateFormatString)
		{
			if (this._culture != null && !this._culture.Equals(reader.Culture))
			{
				previousCulture = reader.Culture;
				reader.Culture = this._culture;
			}
			else
			{
				previousCulture = null;
			}
			if (this._dateTimeZoneHandling != null)
			{
				DateTimeZoneHandling dateTimeZoneHandling = reader.DateTimeZoneHandling;
				DateTimeZoneHandling? dateTimeZoneHandling2 = this._dateTimeZoneHandling;
				if (!(dateTimeZoneHandling == dateTimeZoneHandling2.GetValueOrDefault() & dateTimeZoneHandling2 != null))
				{
					previousDateTimeZoneHandling = new DateTimeZoneHandling?(reader.DateTimeZoneHandling);
					reader.DateTimeZoneHandling = this._dateTimeZoneHandling.GetValueOrDefault();
					goto IL_9E;
				}
			}
			previousDateTimeZoneHandling = null;
			IL_9E:
			if (this._dateParseHandling != null)
			{
				DateParseHandling dateParseHandling = reader.DateParseHandling;
				DateParseHandling? dateParseHandling2 = this._dateParseHandling;
				if (!(dateParseHandling == dateParseHandling2.GetValueOrDefault() & dateParseHandling2 != null))
				{
					previousDateParseHandling = new DateParseHandling?(reader.DateParseHandling);
					reader.DateParseHandling = this._dateParseHandling.GetValueOrDefault();
					goto IL_101;
				}
			}
			previousDateParseHandling = null;
			IL_101:
			if (this._floatParseHandling != null)
			{
				FloatParseHandling floatParseHandling = reader.FloatParseHandling;
				FloatParseHandling? floatParseHandling2 = this._floatParseHandling;
				if (!(floatParseHandling == floatParseHandling2.GetValueOrDefault() & floatParseHandling2 != null))
				{
					previousFloatParseHandling = new FloatParseHandling?(reader.FloatParseHandling);
					reader.FloatParseHandling = this._floatParseHandling.GetValueOrDefault();
					goto IL_164;
				}
			}
			previousFloatParseHandling = null;
			IL_164:
			if (this._maxDepthSet)
			{
				int? maxDepth = reader.MaxDepth;
				int? maxDepth2 = this._maxDepth;
				if (!(maxDepth.GetValueOrDefault() == maxDepth2.GetValueOrDefault() & maxDepth != null == (maxDepth2 != null)))
				{
					previousMaxDepth = reader.MaxDepth;
					reader.MaxDepth = this._maxDepth;
					goto IL_1CA;
				}
			}
			previousMaxDepth = null;
			IL_1CA:
			if (this._dateFormatStringSet && reader.DateFormatString != this._dateFormatString)
			{
				previousDateFormatString = reader.DateFormatString;
				reader.DateFormatString = this._dateFormatString;
			}
			else
			{
				previousDateFormatString = null;
			}
			JsonTextReader jsonTextReader = reader as JsonTextReader;
			if (jsonTextReader != null && jsonTextReader.PropertyNameTable == null)
			{
				DefaultContractResolver defaultContractResolver = this._contractResolver as DefaultContractResolver;
				if (defaultContractResolver != null)
				{
					jsonTextReader.PropertyNameTable = defaultContractResolver.GetNameTable();
				}
			}
		}

		// Token: 0x060069B1 RID: 27057 RVA: 0x001FF0C4 File Offset: 0x001FF0C4
		[NullableContext(2)]
		private void ResetReader([Nullable(1)] JsonReader reader, CultureInfo previousCulture, DateTimeZoneHandling? previousDateTimeZoneHandling, DateParseHandling? previousDateParseHandling, FloatParseHandling? previousFloatParseHandling, int? previousMaxDepth, string previousDateFormatString)
		{
			if (previousCulture != null)
			{
				reader.Culture = previousCulture;
			}
			if (previousDateTimeZoneHandling != null)
			{
				reader.DateTimeZoneHandling = previousDateTimeZoneHandling.GetValueOrDefault();
			}
			if (previousDateParseHandling != null)
			{
				reader.DateParseHandling = previousDateParseHandling.GetValueOrDefault();
			}
			if (previousFloatParseHandling != null)
			{
				reader.FloatParseHandling = previousFloatParseHandling.GetValueOrDefault();
			}
			if (this._maxDepthSet)
			{
				reader.MaxDepth = previousMaxDepth;
			}
			if (this._dateFormatStringSet)
			{
				reader.DateFormatString = previousDateFormatString;
			}
			JsonTextReader jsonTextReader = reader as JsonTextReader;
			if (jsonTextReader != null && jsonTextReader.PropertyNameTable != null)
			{
				DefaultContractResolver defaultContractResolver = this._contractResolver as DefaultContractResolver;
				if (defaultContractResolver != null && jsonTextReader.PropertyNameTable == defaultContractResolver.GetNameTable())
				{
					jsonTextReader.PropertyNameTable = null;
				}
			}
		}

		// Token: 0x060069B2 RID: 27058 RVA: 0x001FF198 File Offset: 0x001FF198
		public void Serialize(TextWriter textWriter, [Nullable(2)] object value)
		{
			this.Serialize(new JsonTextWriter(textWriter), value);
		}

		// Token: 0x060069B3 RID: 27059 RVA: 0x001FF1A8 File Offset: 0x001FF1A8
		[NullableContext(2)]
		public void Serialize([Nullable(1)] JsonWriter jsonWriter, object value, Type objectType)
		{
			this.SerializeInternal(jsonWriter, value, objectType);
		}

		// Token: 0x060069B4 RID: 27060 RVA: 0x001FF1B4 File Offset: 0x001FF1B4
		public void Serialize(TextWriter textWriter, [Nullable(2)] object value, Type objectType)
		{
			this.Serialize(new JsonTextWriter(textWriter), value, objectType);
		}

		// Token: 0x060069B5 RID: 27061 RVA: 0x001FF1C4 File Offset: 0x001FF1C4
		public void Serialize(JsonWriter jsonWriter, [Nullable(2)] object value)
		{
			this.SerializeInternal(jsonWriter, value, null);
		}

		// Token: 0x060069B6 RID: 27062 RVA: 0x001FF1D0 File Offset: 0x001FF1D0
		private TraceJsonReader CreateTraceJsonReader(JsonReader reader)
		{
			TraceJsonReader traceJsonReader = new TraceJsonReader(reader);
			if (reader.TokenType != JsonToken.None)
			{
				traceJsonReader.WriteCurrentToken();
			}
			return traceJsonReader;
		}

		// Token: 0x060069B7 RID: 27063 RVA: 0x001FF1FC File Offset: 0x001FF1FC
		[NullableContext(2)]
		internal virtual void SerializeInternal([Nullable(1)] JsonWriter jsonWriter, object value, Type objectType)
		{
			ValidationUtils.ArgumentNotNull(jsonWriter, "jsonWriter");
			Formatting? formatting = null;
			if (this._formatting != null)
			{
				Formatting formatting2 = jsonWriter.Formatting;
				Formatting? formatting3 = this._formatting;
				if (!(formatting2 == formatting3.GetValueOrDefault() & formatting3 != null))
				{
					formatting = new Formatting?(jsonWriter.Formatting);
					jsonWriter.Formatting = this._formatting.GetValueOrDefault();
				}
			}
			DateFormatHandling? dateFormatHandling = null;
			if (this._dateFormatHandling != null)
			{
				DateFormatHandling dateFormatHandling2 = jsonWriter.DateFormatHandling;
				DateFormatHandling? dateFormatHandling3 = this._dateFormatHandling;
				if (!(dateFormatHandling2 == dateFormatHandling3.GetValueOrDefault() & dateFormatHandling3 != null))
				{
					dateFormatHandling = new DateFormatHandling?(jsonWriter.DateFormatHandling);
					jsonWriter.DateFormatHandling = this._dateFormatHandling.GetValueOrDefault();
				}
			}
			DateTimeZoneHandling? dateTimeZoneHandling = null;
			if (this._dateTimeZoneHandling != null)
			{
				DateTimeZoneHandling dateTimeZoneHandling2 = jsonWriter.DateTimeZoneHandling;
				DateTimeZoneHandling? dateTimeZoneHandling3 = this._dateTimeZoneHandling;
				if (!(dateTimeZoneHandling2 == dateTimeZoneHandling3.GetValueOrDefault() & dateTimeZoneHandling3 != null))
				{
					dateTimeZoneHandling = new DateTimeZoneHandling?(jsonWriter.DateTimeZoneHandling);
					jsonWriter.DateTimeZoneHandling = this._dateTimeZoneHandling.GetValueOrDefault();
				}
			}
			FloatFormatHandling? floatFormatHandling = null;
			if (this._floatFormatHandling != null)
			{
				FloatFormatHandling floatFormatHandling2 = jsonWriter.FloatFormatHandling;
				FloatFormatHandling? floatFormatHandling3 = this._floatFormatHandling;
				if (!(floatFormatHandling2 == floatFormatHandling3.GetValueOrDefault() & floatFormatHandling3 != null))
				{
					floatFormatHandling = new FloatFormatHandling?(jsonWriter.FloatFormatHandling);
					jsonWriter.FloatFormatHandling = this._floatFormatHandling.GetValueOrDefault();
				}
			}
			StringEscapeHandling? stringEscapeHandling = null;
			if (this._stringEscapeHandling != null)
			{
				StringEscapeHandling stringEscapeHandling2 = jsonWriter.StringEscapeHandling;
				StringEscapeHandling? stringEscapeHandling3 = this._stringEscapeHandling;
				if (!(stringEscapeHandling2 == stringEscapeHandling3.GetValueOrDefault() & stringEscapeHandling3 != null))
				{
					stringEscapeHandling = new StringEscapeHandling?(jsonWriter.StringEscapeHandling);
					jsonWriter.StringEscapeHandling = this._stringEscapeHandling.GetValueOrDefault();
				}
			}
			CultureInfo cultureInfo = null;
			if (this._culture != null && !this._culture.Equals(jsonWriter.Culture))
			{
				cultureInfo = jsonWriter.Culture;
				jsonWriter.Culture = this._culture;
			}
			string dateFormatString = null;
			if (this._dateFormatStringSet && jsonWriter.DateFormatString != this._dateFormatString)
			{
				dateFormatString = jsonWriter.DateFormatString;
				jsonWriter.DateFormatString = this._dateFormatString;
			}
			TraceJsonWriter traceJsonWriter = (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose) ? new TraceJsonWriter(jsonWriter) : null;
			new JsonSerializerInternalWriter(this).Serialize(traceJsonWriter ?? jsonWriter, value, objectType);
			if (traceJsonWriter != null)
			{
				this.TraceWriter.Trace(TraceLevel.Verbose, traceJsonWriter.GetSerializedJsonMessage(), null);
			}
			if (formatting != null)
			{
				jsonWriter.Formatting = formatting.GetValueOrDefault();
			}
			if (dateFormatHandling != null)
			{
				jsonWriter.DateFormatHandling = dateFormatHandling.GetValueOrDefault();
			}
			if (dateTimeZoneHandling != null)
			{
				jsonWriter.DateTimeZoneHandling = dateTimeZoneHandling.GetValueOrDefault();
			}
			if (floatFormatHandling != null)
			{
				jsonWriter.FloatFormatHandling = floatFormatHandling.GetValueOrDefault();
			}
			if (stringEscapeHandling != null)
			{
				jsonWriter.StringEscapeHandling = stringEscapeHandling.GetValueOrDefault();
			}
			if (this._dateFormatStringSet)
			{
				jsonWriter.DateFormatString = dateFormatString;
			}
			if (cultureInfo != null)
			{
				jsonWriter.Culture = cultureInfo;
			}
		}

		// Token: 0x060069B8 RID: 27064 RVA: 0x001FF544 File Offset: 0x001FF544
		internal IReferenceResolver GetReferenceResolver()
		{
			if (this._referenceResolver == null)
			{
				this._referenceResolver = new DefaultReferenceResolver();
			}
			return this._referenceResolver;
		}

		// Token: 0x060069B9 RID: 27065 RVA: 0x001FF564 File Offset: 0x001FF564
		[return: Nullable(2)]
		internal JsonConverter GetMatchingConverter(Type type)
		{
			return JsonSerializer.GetMatchingConverter(this._converters, type);
		}

		// Token: 0x060069BA RID: 27066 RVA: 0x001FF574 File Offset: 0x001FF574
		[return: Nullable(2)]
		internal static JsonConverter GetMatchingConverter([Nullable(new byte[]
		{
			2,
			1
		})] IList<JsonConverter> converters, Type objectType)
		{
			if (converters != null)
			{
				for (int i = 0; i < converters.Count; i++)
				{
					JsonConverter jsonConverter = converters[i];
					if (jsonConverter.CanConvert(objectType))
					{
						return jsonConverter;
					}
				}
			}
			return null;
		}

		// Token: 0x060069BB RID: 27067 RVA: 0x001FF5B8 File Offset: 0x001FF5B8
		internal void OnError(Newtonsoft.Json.Serialization.ErrorEventArgs e)
		{
			EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs> error = this.Error;
			if (error == null)
			{
				return;
			}
			error(this, e);
		}

		// Token: 0x04003573 RID: 13683
		internal TypeNameHandling _typeNameHandling;

		// Token: 0x04003574 RID: 13684
		internal TypeNameAssemblyFormatHandling _typeNameAssemblyFormatHandling;

		// Token: 0x04003575 RID: 13685
		internal PreserveReferencesHandling _preserveReferencesHandling;

		// Token: 0x04003576 RID: 13686
		internal ReferenceLoopHandling _referenceLoopHandling;

		// Token: 0x04003577 RID: 13687
		internal MissingMemberHandling _missingMemberHandling;

		// Token: 0x04003578 RID: 13688
		internal ObjectCreationHandling _objectCreationHandling;

		// Token: 0x04003579 RID: 13689
		internal NullValueHandling _nullValueHandling;

		// Token: 0x0400357A RID: 13690
		internal DefaultValueHandling _defaultValueHandling;

		// Token: 0x0400357B RID: 13691
		internal ConstructorHandling _constructorHandling;

		// Token: 0x0400357C RID: 13692
		internal MetadataPropertyHandling _metadataPropertyHandling;

		// Token: 0x0400357D RID: 13693
		[Nullable(2)]
		internal JsonConverterCollection _converters;

		// Token: 0x0400357E RID: 13694
		internal IContractResolver _contractResolver;

		// Token: 0x0400357F RID: 13695
		[Nullable(2)]
		internal ITraceWriter _traceWriter;

		// Token: 0x04003580 RID: 13696
		[Nullable(2)]
		internal IEqualityComparer _equalityComparer;

		// Token: 0x04003581 RID: 13697
		internal ISerializationBinder _serializationBinder;

		// Token: 0x04003582 RID: 13698
		internal StreamingContext _context;

		// Token: 0x04003583 RID: 13699
		[Nullable(2)]
		private IReferenceResolver _referenceResolver;

		// Token: 0x04003584 RID: 13700
		private Formatting? _formatting;

		// Token: 0x04003585 RID: 13701
		private DateFormatHandling? _dateFormatHandling;

		// Token: 0x04003586 RID: 13702
		private DateTimeZoneHandling? _dateTimeZoneHandling;

		// Token: 0x04003587 RID: 13703
		private DateParseHandling? _dateParseHandling;

		// Token: 0x04003588 RID: 13704
		private FloatFormatHandling? _floatFormatHandling;

		// Token: 0x04003589 RID: 13705
		private FloatParseHandling? _floatParseHandling;

		// Token: 0x0400358A RID: 13706
		private StringEscapeHandling? _stringEscapeHandling;

		// Token: 0x0400358B RID: 13707
		private CultureInfo _culture;

		// Token: 0x0400358C RID: 13708
		private int? _maxDepth;

		// Token: 0x0400358D RID: 13709
		private bool _maxDepthSet;

		// Token: 0x0400358E RID: 13710
		private bool? _checkAdditionalContent;

		// Token: 0x0400358F RID: 13711
		[Nullable(2)]
		private string _dateFormatString;

		// Token: 0x04003590 RID: 13712
		private bool _dateFormatStringSet;
	}
}
