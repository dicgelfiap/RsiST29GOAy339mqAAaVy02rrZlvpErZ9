using System;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000AF6 RID: 2806
	[NullableContext(1)]
	[Nullable(0)]
	internal class JsonSerializerProxy : JsonSerializer
	{
		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06006FE1 RID: 28641 RVA: 0x0021FF24 File Offset: 0x0021FF24
		// (remove) Token: 0x06006FE2 RID: 28642 RVA: 0x0021FF34 File Offset: 0x0021FF34
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public override event EventHandler<ErrorEventArgs> Error
		{
			add
			{
				this._serializer.Error += value;
			}
			remove
			{
				this._serializer.Error -= value;
			}
		}

		// Token: 0x17001737 RID: 5943
		// (get) Token: 0x06006FE3 RID: 28643 RVA: 0x0021FF44 File Offset: 0x0021FF44
		// (set) Token: 0x06006FE4 RID: 28644 RVA: 0x0021FF54 File Offset: 0x0021FF54
		[Nullable(2)]
		public override IReferenceResolver ReferenceResolver
		{
			[NullableContext(2)]
			get
			{
				return this._serializer.ReferenceResolver;
			}
			[NullableContext(2)]
			set
			{
				this._serializer.ReferenceResolver = value;
			}
		}

		// Token: 0x17001738 RID: 5944
		// (get) Token: 0x06006FE5 RID: 28645 RVA: 0x0021FF64 File Offset: 0x0021FF64
		// (set) Token: 0x06006FE6 RID: 28646 RVA: 0x0021FF74 File Offset: 0x0021FF74
		[Nullable(2)]
		public override ITraceWriter TraceWriter
		{
			[NullableContext(2)]
			get
			{
				return this._serializer.TraceWriter;
			}
			[NullableContext(2)]
			set
			{
				this._serializer.TraceWriter = value;
			}
		}

		// Token: 0x17001739 RID: 5945
		// (get) Token: 0x06006FE7 RID: 28647 RVA: 0x0021FF84 File Offset: 0x0021FF84
		// (set) Token: 0x06006FE8 RID: 28648 RVA: 0x0021FF94 File Offset: 0x0021FF94
		[Nullable(2)]
		public override IEqualityComparer EqualityComparer
		{
			[NullableContext(2)]
			get
			{
				return this._serializer.EqualityComparer;
			}
			[NullableContext(2)]
			set
			{
				this._serializer.EqualityComparer = value;
			}
		}

		// Token: 0x1700173A RID: 5946
		// (get) Token: 0x06006FE9 RID: 28649 RVA: 0x0021FFA4 File Offset: 0x0021FFA4
		public override JsonConverterCollection Converters
		{
			get
			{
				return this._serializer.Converters;
			}
		}

		// Token: 0x1700173B RID: 5947
		// (get) Token: 0x06006FEA RID: 28650 RVA: 0x0021FFB4 File Offset: 0x0021FFB4
		// (set) Token: 0x06006FEB RID: 28651 RVA: 0x0021FFC4 File Offset: 0x0021FFC4
		public override DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return this._serializer.DefaultValueHandling;
			}
			set
			{
				this._serializer.DefaultValueHandling = value;
			}
		}

		// Token: 0x1700173C RID: 5948
		// (get) Token: 0x06006FEC RID: 28652 RVA: 0x0021FFD4 File Offset: 0x0021FFD4
		// (set) Token: 0x06006FED RID: 28653 RVA: 0x0021FFE4 File Offset: 0x0021FFE4
		public override IContractResolver ContractResolver
		{
			get
			{
				return this._serializer.ContractResolver;
			}
			set
			{
				this._serializer.ContractResolver = value;
			}
		}

		// Token: 0x1700173D RID: 5949
		// (get) Token: 0x06006FEE RID: 28654 RVA: 0x0021FFF4 File Offset: 0x0021FFF4
		// (set) Token: 0x06006FEF RID: 28655 RVA: 0x00220004 File Offset: 0x00220004
		public override MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return this._serializer.MissingMemberHandling;
			}
			set
			{
				this._serializer.MissingMemberHandling = value;
			}
		}

		// Token: 0x1700173E RID: 5950
		// (get) Token: 0x06006FF0 RID: 28656 RVA: 0x00220014 File Offset: 0x00220014
		// (set) Token: 0x06006FF1 RID: 28657 RVA: 0x00220024 File Offset: 0x00220024
		public override NullValueHandling NullValueHandling
		{
			get
			{
				return this._serializer.NullValueHandling;
			}
			set
			{
				this._serializer.NullValueHandling = value;
			}
		}

		// Token: 0x1700173F RID: 5951
		// (get) Token: 0x06006FF2 RID: 28658 RVA: 0x00220034 File Offset: 0x00220034
		// (set) Token: 0x06006FF3 RID: 28659 RVA: 0x00220044 File Offset: 0x00220044
		public override ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return this._serializer.ObjectCreationHandling;
			}
			set
			{
				this._serializer.ObjectCreationHandling = value;
			}
		}

		// Token: 0x17001740 RID: 5952
		// (get) Token: 0x06006FF4 RID: 28660 RVA: 0x00220054 File Offset: 0x00220054
		// (set) Token: 0x06006FF5 RID: 28661 RVA: 0x00220064 File Offset: 0x00220064
		public override ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return this._serializer.ReferenceLoopHandling;
			}
			set
			{
				this._serializer.ReferenceLoopHandling = value;
			}
		}

		// Token: 0x17001741 RID: 5953
		// (get) Token: 0x06006FF6 RID: 28662 RVA: 0x00220074 File Offset: 0x00220074
		// (set) Token: 0x06006FF7 RID: 28663 RVA: 0x00220084 File Offset: 0x00220084
		public override PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				return this._serializer.PreserveReferencesHandling;
			}
			set
			{
				this._serializer.PreserveReferencesHandling = value;
			}
		}

		// Token: 0x17001742 RID: 5954
		// (get) Token: 0x06006FF8 RID: 28664 RVA: 0x00220094 File Offset: 0x00220094
		// (set) Token: 0x06006FF9 RID: 28665 RVA: 0x002200A4 File Offset: 0x002200A4
		public override TypeNameHandling TypeNameHandling
		{
			get
			{
				return this._serializer.TypeNameHandling;
			}
			set
			{
				this._serializer.TypeNameHandling = value;
			}
		}

		// Token: 0x17001743 RID: 5955
		// (get) Token: 0x06006FFA RID: 28666 RVA: 0x002200B4 File Offset: 0x002200B4
		// (set) Token: 0x06006FFB RID: 28667 RVA: 0x002200C4 File Offset: 0x002200C4
		public override MetadataPropertyHandling MetadataPropertyHandling
		{
			get
			{
				return this._serializer.MetadataPropertyHandling;
			}
			set
			{
				this._serializer.MetadataPropertyHandling = value;
			}
		}

		// Token: 0x17001744 RID: 5956
		// (get) Token: 0x06006FFC RID: 28668 RVA: 0x002200D4 File Offset: 0x002200D4
		// (set) Token: 0x06006FFD RID: 28669 RVA: 0x002200E4 File Offset: 0x002200E4
		[Obsolete("TypeNameAssemblyFormat is obsolete. Use TypeNameAssemblyFormatHandling instead.")]
		public override FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				return this._serializer.TypeNameAssemblyFormat;
			}
			set
			{
				this._serializer.TypeNameAssemblyFormat = value;
			}
		}

		// Token: 0x17001745 RID: 5957
		// (get) Token: 0x06006FFE RID: 28670 RVA: 0x002200F4 File Offset: 0x002200F4
		// (set) Token: 0x06006FFF RID: 28671 RVA: 0x00220104 File Offset: 0x00220104
		public override TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling
		{
			get
			{
				return this._serializer.TypeNameAssemblyFormatHandling;
			}
			set
			{
				this._serializer.TypeNameAssemblyFormatHandling = value;
			}
		}

		// Token: 0x17001746 RID: 5958
		// (get) Token: 0x06007000 RID: 28672 RVA: 0x00220114 File Offset: 0x00220114
		// (set) Token: 0x06007001 RID: 28673 RVA: 0x00220124 File Offset: 0x00220124
		public override ConstructorHandling ConstructorHandling
		{
			get
			{
				return this._serializer.ConstructorHandling;
			}
			set
			{
				this._serializer.ConstructorHandling = value;
			}
		}

		// Token: 0x17001747 RID: 5959
		// (get) Token: 0x06007002 RID: 28674 RVA: 0x00220134 File Offset: 0x00220134
		// (set) Token: 0x06007003 RID: 28675 RVA: 0x00220144 File Offset: 0x00220144
		[Obsolete("Binder is obsolete. Use SerializationBinder instead.")]
		public override SerializationBinder Binder
		{
			get
			{
				return this._serializer.Binder;
			}
			set
			{
				this._serializer.Binder = value;
			}
		}

		// Token: 0x17001748 RID: 5960
		// (get) Token: 0x06007004 RID: 28676 RVA: 0x00220154 File Offset: 0x00220154
		// (set) Token: 0x06007005 RID: 28677 RVA: 0x00220164 File Offset: 0x00220164
		public override ISerializationBinder SerializationBinder
		{
			get
			{
				return this._serializer.SerializationBinder;
			}
			set
			{
				this._serializer.SerializationBinder = value;
			}
		}

		// Token: 0x17001749 RID: 5961
		// (get) Token: 0x06007006 RID: 28678 RVA: 0x00220174 File Offset: 0x00220174
		// (set) Token: 0x06007007 RID: 28679 RVA: 0x00220184 File Offset: 0x00220184
		public override StreamingContext Context
		{
			get
			{
				return this._serializer.Context;
			}
			set
			{
				this._serializer.Context = value;
			}
		}

		// Token: 0x1700174A RID: 5962
		// (get) Token: 0x06007008 RID: 28680 RVA: 0x00220194 File Offset: 0x00220194
		// (set) Token: 0x06007009 RID: 28681 RVA: 0x002201A4 File Offset: 0x002201A4
		public override Formatting Formatting
		{
			get
			{
				return this._serializer.Formatting;
			}
			set
			{
				this._serializer.Formatting = value;
			}
		}

		// Token: 0x1700174B RID: 5963
		// (get) Token: 0x0600700A RID: 28682 RVA: 0x002201B4 File Offset: 0x002201B4
		// (set) Token: 0x0600700B RID: 28683 RVA: 0x002201C4 File Offset: 0x002201C4
		public override DateFormatHandling DateFormatHandling
		{
			get
			{
				return this._serializer.DateFormatHandling;
			}
			set
			{
				this._serializer.DateFormatHandling = value;
			}
		}

		// Token: 0x1700174C RID: 5964
		// (get) Token: 0x0600700C RID: 28684 RVA: 0x002201D4 File Offset: 0x002201D4
		// (set) Token: 0x0600700D RID: 28685 RVA: 0x002201E4 File Offset: 0x002201E4
		public override DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				return this._serializer.DateTimeZoneHandling;
			}
			set
			{
				this._serializer.DateTimeZoneHandling = value;
			}
		}

		// Token: 0x1700174D RID: 5965
		// (get) Token: 0x0600700E RID: 28686 RVA: 0x002201F4 File Offset: 0x002201F4
		// (set) Token: 0x0600700F RID: 28687 RVA: 0x00220204 File Offset: 0x00220204
		public override DateParseHandling DateParseHandling
		{
			get
			{
				return this._serializer.DateParseHandling;
			}
			set
			{
				this._serializer.DateParseHandling = value;
			}
		}

		// Token: 0x1700174E RID: 5966
		// (get) Token: 0x06007010 RID: 28688 RVA: 0x00220214 File Offset: 0x00220214
		// (set) Token: 0x06007011 RID: 28689 RVA: 0x00220224 File Offset: 0x00220224
		public override FloatFormatHandling FloatFormatHandling
		{
			get
			{
				return this._serializer.FloatFormatHandling;
			}
			set
			{
				this._serializer.FloatFormatHandling = value;
			}
		}

		// Token: 0x1700174F RID: 5967
		// (get) Token: 0x06007012 RID: 28690 RVA: 0x00220234 File Offset: 0x00220234
		// (set) Token: 0x06007013 RID: 28691 RVA: 0x00220244 File Offset: 0x00220244
		public override FloatParseHandling FloatParseHandling
		{
			get
			{
				return this._serializer.FloatParseHandling;
			}
			set
			{
				this._serializer.FloatParseHandling = value;
			}
		}

		// Token: 0x17001750 RID: 5968
		// (get) Token: 0x06007014 RID: 28692 RVA: 0x00220254 File Offset: 0x00220254
		// (set) Token: 0x06007015 RID: 28693 RVA: 0x00220264 File Offset: 0x00220264
		public override StringEscapeHandling StringEscapeHandling
		{
			get
			{
				return this._serializer.StringEscapeHandling;
			}
			set
			{
				this._serializer.StringEscapeHandling = value;
			}
		}

		// Token: 0x17001751 RID: 5969
		// (get) Token: 0x06007016 RID: 28694 RVA: 0x00220274 File Offset: 0x00220274
		// (set) Token: 0x06007017 RID: 28695 RVA: 0x00220284 File Offset: 0x00220284
		public override string DateFormatString
		{
			get
			{
				return this._serializer.DateFormatString;
			}
			set
			{
				this._serializer.DateFormatString = value;
			}
		}

		// Token: 0x17001752 RID: 5970
		// (get) Token: 0x06007018 RID: 28696 RVA: 0x00220294 File Offset: 0x00220294
		// (set) Token: 0x06007019 RID: 28697 RVA: 0x002202A4 File Offset: 0x002202A4
		public override CultureInfo Culture
		{
			get
			{
				return this._serializer.Culture;
			}
			set
			{
				this._serializer.Culture = value;
			}
		}

		// Token: 0x17001753 RID: 5971
		// (get) Token: 0x0600701A RID: 28698 RVA: 0x002202B4 File Offset: 0x002202B4
		// (set) Token: 0x0600701B RID: 28699 RVA: 0x002202C4 File Offset: 0x002202C4
		public override int? MaxDepth
		{
			get
			{
				return this._serializer.MaxDepth;
			}
			set
			{
				this._serializer.MaxDepth = value;
			}
		}

		// Token: 0x17001754 RID: 5972
		// (get) Token: 0x0600701C RID: 28700 RVA: 0x002202D4 File Offset: 0x002202D4
		// (set) Token: 0x0600701D RID: 28701 RVA: 0x002202E4 File Offset: 0x002202E4
		public override bool CheckAdditionalContent
		{
			get
			{
				return this._serializer.CheckAdditionalContent;
			}
			set
			{
				this._serializer.CheckAdditionalContent = value;
			}
		}

		// Token: 0x0600701E RID: 28702 RVA: 0x002202F4 File Offset: 0x002202F4
		internal JsonSerializerInternalBase GetInternalSerializer()
		{
			if (this._serializerReader != null)
			{
				return this._serializerReader;
			}
			return this._serializerWriter;
		}

		// Token: 0x0600701F RID: 28703 RVA: 0x00220310 File Offset: 0x00220310
		public JsonSerializerProxy(JsonSerializerInternalReader serializerReader)
		{
			ValidationUtils.ArgumentNotNull(serializerReader, "serializerReader");
			this._serializerReader = serializerReader;
			this._serializer = serializerReader.Serializer;
		}

		// Token: 0x06007020 RID: 28704 RVA: 0x00220338 File Offset: 0x00220338
		public JsonSerializerProxy(JsonSerializerInternalWriter serializerWriter)
		{
			ValidationUtils.ArgumentNotNull(serializerWriter, "serializerWriter");
			this._serializerWriter = serializerWriter;
			this._serializer = serializerWriter.Serializer;
		}

		// Token: 0x06007021 RID: 28705 RVA: 0x00220360 File Offset: 0x00220360
		[NullableContext(2)]
		internal override object DeserializeInternal([Nullable(1)] JsonReader reader, Type objectType)
		{
			if (this._serializerReader != null)
			{
				return this._serializerReader.Deserialize(reader, objectType, false);
			}
			return this._serializer.Deserialize(reader, objectType);
		}

		// Token: 0x06007022 RID: 28706 RVA: 0x0022038C File Offset: 0x0022038C
		internal override void PopulateInternal(JsonReader reader, object target)
		{
			if (this._serializerReader != null)
			{
				this._serializerReader.Populate(reader, target);
				return;
			}
			this._serializer.Populate(reader, target);
		}

		// Token: 0x06007023 RID: 28707 RVA: 0x002203B4 File Offset: 0x002203B4
		[NullableContext(2)]
		internal override void SerializeInternal([Nullable(1)] JsonWriter jsonWriter, object value, Type rootType)
		{
			if (this._serializerWriter != null)
			{
				this._serializerWriter.Serialize(jsonWriter, value, rootType);
				return;
			}
			this._serializer.Serialize(jsonWriter, value);
		}

		// Token: 0x040037B1 RID: 14257
		[Nullable(2)]
		private readonly JsonSerializerInternalReader _serializerReader;

		// Token: 0x040037B2 RID: 14258
		[Nullable(2)]
		private readonly JsonSerializerInternalWriter _serializerWriter;

		// Token: 0x040037B3 RID: 14259
		private readonly JsonSerializer _serializer;
	}
}
